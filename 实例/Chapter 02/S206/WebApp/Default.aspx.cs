using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        public enum RouteOrRouteCollection
        {
            Route,
            RouteCollection
        }

        public RouteData GetRouteData(RouteOrRouteCollection routeOrCollection,
        bool routeExistingFiles4Collection, bool routeExistingFiles4Route)
        {
            Route route = new Route("{areaCode}/{days}", new RouteValueDictionary { { "areacode", "010" }, { "days", 2 } }, null);
            route.RouteExistingFiles = routeExistingFiles4Route;
            HttpContextBase context = CreateHttpContext();

            if (routeOrCollection == RouteOrRouteCollection.Route)
            {
                return route.GetRouteData(context);
            }

            RouteCollection routes = new RouteCollection();
            routes.Add(route);
            routes.RouteExistingFiles = routeExistingFiles4Collection;
            return routes.GetRouteData(context);
        }

        private static HttpContextBase CreateHttpContext()
        {
            HttpRequest request = new HttpRequest("~/weather.aspx", "http://localhost:3721/weather.aspx", null);
            HttpResponse response = new HttpResponse(new StringWriter());
            HttpContext context = new HttpContext(request, response);
            HttpContextBase contextWrapper = new HttpContextWrapper(context);
            return contextWrapper;
        }
    }
}