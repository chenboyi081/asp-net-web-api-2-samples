using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http;
using WebApi;

namespace WebApi
{
    public class MyApiControllerActionInvoker: IHttpActionInvoker
    {
        public Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            ReflectedHttpActionDescriptor actionDescriptor = (ReflectedHttpActionDescriptor)actionContext.ActionDescriptor;

            //提取参数数组
            List<object> arguments = new List<object>();
            ParameterInfo[] parameters = actionDescriptor.MethodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                string parameterName = parameters[i].Name;
                arguments.Add(actionContext.ActionArguments[parameterName]);
            }

            //利用ActionExecutor执行目标Action方法
            Task<object> task = new ActionExecutor(actionDescriptor.MethodInfo).Execute(actionContext.ControllerContext.Controller, arguments.ToArray());

            //创建HttpResponseMessage
            object result = task.Result;
            HttpResponseMessage response = result as HttpResponseMessage;
            if (null == response)
            {
                //利用"媒体类型协商机制"创建MediaTypeFormatter
                IContentNegotiator negotiator = actionContext.ControllerContext.Configuration.Services.GetContentNegotiator();
                MediaTypeFormatter formatter = negotiator.Negotiate(result.GetType(), actionContext.Request, actionContext.ControllerContext.Configuration.Formatters).Formatter;
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent(result.GetType(), result, formatter)
                };
            }

            //创建并返回Task<HttpResponseMessage>
            TaskCompletionSource<HttpResponseMessage> completionSource = new TaskCompletionSource<HttpResponseMessage>();
            completionSource.SetResult(response);
            return completionSource.Task;
        }
    }
}

