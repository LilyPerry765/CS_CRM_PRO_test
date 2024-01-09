<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>ورود کاربران</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="LoginContainer">
        <div class="Header">
            <asp:Label ID="lblNewsHeader" runat="server" Text="ورود کاربران" />
        </div>
        <table class="LoginTable">
            <tr id="ErroRow" runat="server" class="RequestRow" visible="false">
                <td colspan="2">
                    <asp:Label ID="messageLabel" runat="server" Text="نام کاربری و / یا رمز عبور صحیح وارد نشده است !"
                        CssClass="LoginMessage" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="UserNameLabel" runat="server" Text="نام کاربری : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" CssClass="LoginError"
                        ControlToValidate="UserNameTextBox" ValidationGroup="LoginGroup" ErrorMessage="*" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="PasswordLabel" runat="server" Text="رمز عبور : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="RequestValue" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="LoginError"
                        ControlToValidate="PasswordTextBox" ValidationGroup="LoginGroup" ErrorMessage="*" />
                </td>
            </tr>
        </table>
        <div class="Footer">
            <div style="float: right; width: 29%; text-align: right; padding: 10px 10px 0px 0px;">
                <a href="ChangePassword.aspx">تغییر رمز عبور</a>
            </div>
            <div style="float: left; width: 50%;">
                <asp:Button ID="LoginButton" runat="server" Text="ورود" CssClass="ButtonValue" OnClick="LoginButton_Click"
                    ValidationGroup="LoginGroup" />
            </div>
        </div>
    </div>
</asp:Content>
