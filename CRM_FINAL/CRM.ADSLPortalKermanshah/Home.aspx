<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.Home" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>صفحه اصلی</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="padding: 10px; height: 500px;">
        <div style="float: right; width: 29%; text-align: right;">
            <a id="LoginLink" runat="server" href="Login.aspx">
                <img src="Images/LoginImage.png" alt="" style="vertical-align: middle;" />
            </a>
        </div>
        <div style="float: left; width: 69%;">
            &nbsp;</div>
    </div>
</asp:Content>
