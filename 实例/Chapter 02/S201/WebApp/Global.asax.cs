using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var defaults = new RouteValueDictionary { { "name", "*" }, { "id", "*" } };
            RouteTable.Routes.MapPageRoute("", "employees/{name}/{id}","~/default.aspx", true, defaults);
        }
    }
}