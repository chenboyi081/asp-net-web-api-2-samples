using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace WebApi
{
    public class CacheKey
    {
        public MethodInfo MethodInfo { get; private set; }
        public object[] Arguments { get; private set; }

        public CacheKey(MethodInfo methodInfo, IDictionary<string, object> arguments)
        {
            this.MethodInfo = methodInfo;
            ParameterInfo[] parameters = methodInfo.GetParameters();
            this.Arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                this.Arguments[i] = arguments[parameters[i].Name];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:", this.MethodInfo.Name);
            Array.ForEach(this.Arguments, obj => sb.AppendFormat("{0}:",(obj == null ? "" : obj).GetHashCode()));
            return sb.ToString().TrimEnd(':');
        }
    }
}