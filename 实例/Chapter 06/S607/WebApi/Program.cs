using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inlineConstraints = new string[] { "bool", "datetime", "decimal", "double", "float", "guid", "int", "long", "alpha", @"regex(^\d{3}-\d{7}$)", "max(50)", "min(10)", "range(10,50)", "maxlength(50)", "minlength(10)", "length(10,50)" };
            MyInlineConstraintResolver constraintResolver = new MyInlineConstraintResolver();
            IDictionary<string, IHttpRouteConstraint> constraints = inlineConstraints.ToDictionary(inlineConstraint => inlineConstraint,
                inlineConstraint => constraintResolver.ResolveConstraint(inlineConstraint));
            Console.WriteLine("{0,-24}{1}", "Expression", "HttpRouteConstraint");
            foreach (var item in constraints)
            {
                Console.WriteLine("{0,-24}{1}", item.Key, item.Value.GetType().Name);
            }
        }
    }
}
