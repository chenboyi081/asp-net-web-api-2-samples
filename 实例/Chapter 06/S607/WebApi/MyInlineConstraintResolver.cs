using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;

namespace WebApi
{
    public class MyInlineConstraintResolver : IInlineConstraintResolver
    {
        public IDictionary<string, Type> ConstraintMap { get; private set; }
        public MyInlineConstraintResolver()
        {
            this.ConstraintMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            this.ConstraintMap.Add("bool", typeof(BoolRouteConstraint));
            this.ConstraintMap.Add("datetime", typeof(DateTimeRouteConstraint));
            this.ConstraintMap.Add("decimal", typeof(DecimalRouteConstraint));
            this.ConstraintMap.Add("double", typeof(DoubleRouteConstraint));
            this.ConstraintMap.Add("float", typeof(FloatRouteConstraint));
            this.ConstraintMap.Add("guid", typeof(GuidRouteConstraint));
            this.ConstraintMap.Add("int", typeof(IntRouteConstraint));
            this.ConstraintMap.Add("long", typeof(LongRouteConstraint));
            this.ConstraintMap.Add("minlength", typeof(MinLengthRouteConstraint));
            this.ConstraintMap.Add("maxlength", typeof(MaxLengthRouteConstraint));
            this.ConstraintMap.Add("length", typeof(LengthRouteConstraint));
            this.ConstraintMap.Add("min", typeof(MinRouteConstraint));
            this.ConstraintMap.Add("max", typeof(MaxRouteConstraint));
            this.ConstraintMap.Add("range", typeof(RangeRouteConstraint));
            this.ConstraintMap.Add("alpha", typeof(AlphaRouteConstraint));
            this.ConstraintMap.Add("regex", typeof(RegexRouteConstraint));
        }

        public IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
        {
            string[] split = inlineConstraint.Split('(');
            string type = split[0];
            string argumentList = split.Length > 1 ? split[1].Trim().TrimEnd(')') : "";
            Type constraintType;
            if (this.ConstraintMap.TryGetValue(type, out constraintType))
            {
                split = string.IsNullOrEmpty(argumentList) ? new string[0] : argumentList.Split(',');
                ConstructorInfo[] constructors = (from c in constraintType.GetConstructors()
                                                  where c.GetParameters().Count() == split.Length
                                                  select c).ToArray();
                if (constructors.Length != 1)
                {
                    throw new InvalidOperationException("找不到与指定参数匹配的构造函数");
                }
                ConstructorInfo constructor = constructors[0];
                ParameterInfo[] parameters = constructor.GetParameters();
                object[] arguments = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    arguments[i] = Convert.ChangeType(split[i], parameters[i].ParameterType);
                }
                return (IHttpRouteConstraint)constructor.Invoke(arguments);
            }
            return null;
        }
    }
}