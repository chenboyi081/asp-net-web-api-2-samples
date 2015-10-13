using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace WebApi
{
    public class ActionExecutor
    {
        private static Dictionary<MethodInfo, object> delegates = new Dictionary<MethodInfo, object>();
        private static object syncHelper = new object();
        public MethodInfo MethodInfo { get; private set; }

        public ActionExecutor(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
        }

        public Task<object> Execute(object target, object[] arguments)
        {
           object actionOrFunc;
           if (delegates.TryGetValue(this.MethodInfo, out actionOrFunc))
           {
               return this.ExecuteCore(target, arguments, actionOrFunc);
           }

           lock (syncHelper)
           {
               if (delegates.TryGetValue(this.MethodInfo, out actionOrFunc))
               {
                   return this.ExecuteCore(target, arguments, actionOrFunc);
               }
               actionOrFunc = CreateExecutor(this.MethodInfo);
               delegates[this.MethodInfo] = actionOrFunc;
               return this.ExecuteCore(target, arguments, actionOrFunc);
           }
        }

        private Task<object> ExecuteCore(object target, object[] arguments, object executor)
        {
            TaskCompletionSource<object> completionSource = new TaskCompletionSource<object>();
            Action<object, object[]> action = executor as Action<object, object[]>;
            if (null != action)
            {
                action(target, arguments);
                completionSource.SetResult(null);
                return completionSource.Task;
            }

            Func<object, object[], object> func = executor as Func<object, object[], object>;
            if (null != func)
            {
                object result = func(target, arguments);
                completionSource.SetResult(result);
                return completionSource.Task;
            }

            return null;
        }

        private static object CreateExecutor(MethodInfo methodInfo)
        {
            ParameterExpression target = Expression.Parameter(typeof(object), "target");
            ParameterExpression arguments = Expression.Parameter(typeof(object[]), "arguments");

            List<Expression> parameters = new List<Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                ParameterInfo paramInfo = paramInfos[i];
                BinaryExpression getElementByIndex = Expression.ArrayIndex(arguments, Expression.Constant(i));
                UnaryExpression convertToParameterType = Expression.Convert(getElementByIndex, paramInfo.ParameterType);
                parameters.Add(convertToParameterType);
            }

            UnaryExpression instanceCast = Expression.Convert(target, methodInfo.ReflectedType);
            MethodCallExpression methodCall = methodCall = Expression.Call(instanceCast, methodInfo, parameters);

            if (methodInfo.ReflectedType == typeof(void))
            {
                return Expression.Lambda<Action<object, object[]>>(methodCall, target, arguments).Compile();
            }
            else
            {
                UnaryExpression convertToObjectType = Expression.Convert(methodCall, typeof(object));
                return Expression.Lambda<Func<object, object[], object>>(convertToObjectType, target, arguments).Compile();
            }
        }
    }
}