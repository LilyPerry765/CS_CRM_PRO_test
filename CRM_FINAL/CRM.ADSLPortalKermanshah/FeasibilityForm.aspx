<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="FeasibilityForm.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.FeasibilityForm" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>امکان سنجی</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header">
            <asp:Label ID="lblNewsHeader" runat="server" Text="امکان سنجی" />
        </div>
        <table class="RequestTabel">
            <tr class="RequestRow">
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن (8 رقمی) : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="TelephoneNoTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="TelephoneNoValidator" runat="server" ControlToValidate="TelephoneNoTextBox"
                        ValidationGroup="SendGroup" ErrorMessage="* لطفا شماره تلفن را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="TelephoneNoRegular" runat="server" ControlToValidate="TelephoneNoTextBox"
                        ValidationGroup="SendGroup" ErrorMessage="* لطفا شماره تلفن صحیح 8 رقمی را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d{8}$" />
                    <asp:Label ID="ErrorMessageLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
                    <asp:Label ID="SuccessMessageLabel" runat="server" Visible="false" CssClass="SuccessMessage" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <div class="Footer">
            <div class="MessageContainer" style="float: right;">
                <asp:Label ID="MessageLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
            </div>
            <div style="float: left;">
                <asp:Button ID="SendButton" runat="server" Text="ارسال" CssClass="ButtonValue" OnClick="SendButton_Click"
                    ValidationGroup="SendGroup" />
                <asp:Button ID="ResetButton" runat="server" Text="بازنشانی" CssClass="ButtonValue"
                    OnClick="ResetButton_Click" />
            </div>
        </div>
    </div>
</asp:Content>
