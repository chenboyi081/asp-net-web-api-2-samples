using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApi.Controllers
{
public class DemoController : ApiController
{
    public IEnumerable<string> Get()
    {
        string routeTemplate = "weather/{areacode}/{days}";
        IHttpRoute route = new HttpRoute(routeTemplate);
        route.Defaults.Add("days", 2);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/");
        IHttpVirtualPathData pathData;

        //1. 不能提供路由变量areacode的值
        Dictionary<string, object> values = new Dictionary<string, object>();
        pathData = route.GetVirtualPath(request, values);
        yield return pathData == null ? "N/A" : pathData.VirtualPath;

        //2. values无Key为"httproute"的元素
        values.Add("areaCode", "028");
        pathData = route.GetVirtualPath(request, values);
        yield return pathData == null ? "N/A" : pathData.VirtualPath;

        //3. 所有的路由变量值通过values提供
        values.Add("httproute", true);
        values.Add("days", 3);
        IHttpRouteData routeData = new HttpRouteData(route);
        routeData.Values.Add("areacode", "0512");
        routeData.Values.Add("days", 4);
        request.SetRouteData(routeData);
        pathData = route.GetVirtualPath(request, values);
        yield return pathData == null ? "N/A" : pathData.VirtualPath;

        //4. 所有的路由变量值通过request提供
        values.Clear();
        values.Add("httproute", true);
        pathData = route.GetVirtualPath(request, values);
        yield return pathData == null ? "N/A" : pathData.VirtualPath;

        //5. 采用定义在HttpRoute上的默认值(days = 2)
        routeData.Values.Remove("days");
        pathData = route.GetVirtualPath(request, values);
        yield return pathData == null ? "N/A" : pathData.VirtualPath;

    }
}
}
