using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Weather : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("areaCode", "0512");
            routeData.Values.Add("days", "1");
            RequestContext requestContext = new RequestContext();
            requestContext.HttpContext = new HttpContextWrapper(HttpContext.Current);
            requestContext.RouteData = routeData;

            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("areaCode", "028");
            values.Add("days", "3");

            Response.Write(RouteTable.Routes.GetVirtualPath(null, null).VirtualPath+ "<br/>");
            Response.Write(RouteTable.Routes.GetVirtualPath(requestContext,null).VirtualPath + "<br/>");
            Response.Write(RouteTable.Routes.GetVirtualPath(requestContext,values).VirtualPath + "<br/>");
        }
    }
}