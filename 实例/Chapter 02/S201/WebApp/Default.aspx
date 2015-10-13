<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>      
        <title>员工管理</title>
        <link rel="stylesheet" href="/Content/bootstrap.css" /> 
        <link rel="stylesheet" href="/Content/site.css" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="page">
                    <asp:GridView ID="GridViewEmployees" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered">
                    <Columns>
                        <asp:HyperLinkField HeaderText="姓名" DataTextField="Name" DataNavigateUrlFields="Name,Id" DataNavigateUrlFormatString="~/employees/{0}/{1}" />
                        <asp:BoundField DataField="Gender" HeaderText="性别" />
                        <asp:BoundField DataField="BirthDate" HeaderText="出生日期" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Department" HeaderText="部门" />
                    </Columns>
                </asp:GridView>
                <asp:DetailsView ID="DetailsViewEmployee" runat="server" AutoGenerateRows="false"  Width="100%" CssClass="table table-bordered">
                     <Fields>
                          <asp:BoundField DataField="ID" HeaderText= "ID"  />
                          <asp:BoundField DataField="Name" HeaderText= "姓名"  />
                          <asp:BoundField DataField="Gender" HeaderText="性别" />
                          <asp:BoundField DataField="BirthDate" HeaderText="出生日期" DataFormatString="{0:dd/MM/yyyy}" />
                          <asp:BoundField DataField="Department" HeaderText="部门" />
                    </Fields>
                </asp:DetailsView>
            </div>
        </form>
    </body>
</html>
