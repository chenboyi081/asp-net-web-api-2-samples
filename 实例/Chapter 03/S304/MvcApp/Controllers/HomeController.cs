using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public void Index()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("wheather", "wheather/{areaCode}/{days}");

            HttpRequest request = new HttpRequest("wheather.aspx", "http://www.artech.com/wheather/010/2", null);
            HttpResponse response = new HttpResponse(new StringWriter());
            HttpContext context = new HttpContext(request, response);

            RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
            HttpControllerHandler httpHandler = new HttpControllerHandler(routeData, new HttpRouteDataTraceHandler());
            httpHandler.ProcessRequestAsync(context).Wait();
        }
    }
}