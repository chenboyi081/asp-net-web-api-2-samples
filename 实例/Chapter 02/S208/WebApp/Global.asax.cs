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
            RouteTable.Routes.RouteExistingFiles = true;
            RouteTable.Routes.Ignore("css/{filename}.css/{*pathInfo}");
            var defaults = new RouteValueDictionary { { "areacode", "010" }, { "days", 2 } }; 
            var dataTokens = new RouteValueDictionary { { "defaultCity", "BeiJing" }, { "defaultDays", 2 } };
            RouteTable.Routes.MapPageRoute("default", "{areacode}/{days}","~/weather.aspx", false, defaults, null, dataTokens);
        }
    }
}