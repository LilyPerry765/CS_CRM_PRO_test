<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>تغییر رمز عبور</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header">
            <asp:Label ID="HeaderLabel" runat="server" Text="تغییر رمز عبور" />
        </div>
        <table class="RequestTabel">
            <tr class="RequestRow">
                <td class="RequestColumnHeader" colspan="2">
                    <asp:Label ID="MessageLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
                </td>
            </tr>
            <%--<tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="EmailLabel" runat="server" Text="آدرس الکترونیکی : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="EmailTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا آدرس الکترونیکی را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="EmailRegular" runat="server" ControlToValidate="EmailTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا آدرس الکترونکی صحیح وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </td>
            </tr>--%>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="UserNameLabel" runat="server" Text="نام کاربری : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ControlToValidate="UserNameTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا نام کاربری را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="OldPasswordLabel" runat="server" Text="رمز عبور قدیم : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="OldPasswordTextBox" runat="server" CssClass="RequestValue" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="OldPasswordTextBoxValidator" runat="server" ControlToValidate="OldPasswordTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا رمز عبور قدیم را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="NewPasswordLabel" runat="server" Text="رمز عبور جدید : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="NewPasswordTextBox" runat="server" CssClass="RequestValue" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="NewPasswordTextBoxValidator" runat="server" ControlToValidate="NewPasswordTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا رمز عبور جدید را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="ReNewPasswordLabel" runat="server" Text="تکرار رمز عبور جدید : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="ReNewPasswordTextBox" runat="server" CssClass="RequestValue" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="ReNewPasswordTextBoxValidator" runat="server" ControlToValidate="ReNewPasswordTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا  رمز عبور جدید را تکرار وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
        </table>
        <div class="Footer">
            <div class="MessageContainer" style="float: right;">
                <asp:Label ID="SaveSuccessMessageLabel" runat="server" Visible="false" CssClass="SuccessMessage" />
                <asp:Label ID="SaveErrorMessageLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
            </div>
            <div style="float: left;">
                <asp:Button ID="SaveButton" runat="server" Text="ذخیره" CssClass="ButtonValue" OnClick="ChangeButton_Click"
                    ValidationGroup="SaveGroup" />
                <asp:Button ID="ResetButton" runat="server" Text="بازنشانی" CssClass="ButtonValue"
                    OnClick="ResetButton_Click" />
            </div>
        </div>
    </div>
</asp:Content>
