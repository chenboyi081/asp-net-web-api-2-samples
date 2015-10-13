<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>路由解析</title> 
    <link rel="stylesheet" href="/Content/bootstrap.css" /> 
    <link rel="stylesheet" href="/Content/site.css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <table class="table table-bordered">
        <thead>
          <tr>
            <th>RouteCollection.RouteExistingFiles</th>
            <th colspan="2">True</th>
            <th colspan="2">False</th>
          </tr>
          <tr>
            <th>Route.RouteExistingFiles</th>
            <th>True</th>
            <th>False</th>
            <th>True</th>
            <th>False</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Route.GetRouteData()</td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.Route,true,true) == null ? "Null":"RouteData" %></td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.Route,true,false) == null ? "Null":"RouteData" %></td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.Route,false,true) == null ? "Null":"RouteData" %></td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.Route,false,false) == null ? "Null":"RouteData" %></td>
          </tr>
          <tr>
            <td>RouteCollection.GetRouteData()</td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.RouteCollection,true,true) == null ? "Null":"RouteData" %></td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.RouteCollection,true,false) == null ? "Null":"RouteData" %></td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.RouteCollection,false,true) == null ? "Null":"RouteData" %></td>
            <td><%=this.GetRouteData(RouteOrRouteCollection.RouteCollection,false,false) == null ? "Null":"RouteData" %></td>
          </tr>
        </tbody>
  </table>
</form>

</body>
</html>
