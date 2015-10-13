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
    public IEnumerable<UriResolutionResult> Get()
    {
        string routeTemplate = "movies/{genre}/{title}/{id}";
        IHttpRoute route = new HttpRoute(routeTemplate);
        IHttpRouteConstraint contraint = new HttpMethodConstraint(HttpMethod.Post);
        route.Constraints.Add("httpMethod", contraint);

        string requestUri = "http://www.artech.com/api/movies/romance/titanic/001";
        HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, requestUri);
        HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Post, requestUri);

        string root1 = "/";
        string root2 = "/api/";

        IHttpRouteData routeData1 = route.GetRouteData(root1, request1);
        IHttpRouteData routeData2 = route.GetRouteData(root1, request2);
        IHttpRouteData routeData3 = route.GetRouteData(root2, request1);
        IHttpRouteData routeData4 = route.GetRouteData(root2, request2);

        yield return new UriResolutionResult(root1,"GET", routeData1 != null);
        yield return new UriResolutionResult(root1,"POST", routeData2 != null);
        yield return new UriResolutionResult(root2,"GET", routeData3 != null);
        yield return new UriResolutionResult(root2, "POST", routeData4 != null);
    }
}

public class UriResolutionResult
{
    public string VirtualPathRoot { get; set; }
    public string Method { get; set; }
    public bool Matched { get; set; }

    public UriResolutionResult() { }
    public UriResolutionResult(string virtualPathRoot, string method, bool matched)
    {
        this.VirtualPathRoot = virtualPathRoot;
        this.Method = method;
        this.Matched = matched;
    }
}
}
