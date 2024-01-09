<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="PAPDischargeRequestForm.aspx.cs" Inherits="CRM.ADSLPortal.PAPDischargeRequestForm" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>ثبت درخواست</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header">
            <asp:Label ID="lblNewsHeader" runat="server" Text="ثبت درخواست تخلیه" />
        </div>
        <table class="RequestTabel">
            <tr class="RequestRow">
                <td class="RequestColumnHeader" colspan="2">
                    <asp:Label ID="MessageTelephoneLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="TelephoneNoTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="TelephoneNoValidator" runat="server" ControlToValidate="TelephoneNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا شماره تلفن را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="TelephoneNoRegular" runat="server" ControlToValidate="TelephoneNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا شماره تلفن صحیح 10 رقمی را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d{10}$" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="Label1" runat="server" Text="مرکز : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:DropDownList ID="CenterList" runat="server" CssClass="RequestValue">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="CenterValidator" runat="server" ControlToValidate="CenterList"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا نام مرکز را وارد نمایید !" CssClass="ErrorMessage"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="FirstNameLabel" runat="server" Text="نام مشترک / عنوان شرکت : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="FirstNameValidator" runat="server" ControlToValidate="FirstNameTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا نام مشترک / عنوان شرکت را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="LastNameLabel" runat="server" Text="نام خانوادگی مشترک : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="LastNameTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LastNameTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا نام خانوادگی مشترک را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="CustomerStatusLabel" runat="server" Text="وضعیت مشترک : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:DropDownList ID="CustomerStatusList" runat="server" CssClass="RequestValue">
                        <asp:ListItem Text="-- انتخاب نمایید --" Value="0" Selected="True" />
                        <asp:ListItem Text="مالک" Value="1" />
                        <asp:ListItem Text="نماینده" Value="2" />
                        <asp:ListItem Text="مستاجر" Value="3" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="CustomerStatusValidator" runat="server" ControlToValidate="CustomerStatusList"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا وضعیت مشترک را انتخاب نمایید !"
                        InitialValue="0" CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>
            <%--<tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="InstalDateLabel" runat="server" Text="مهلت تخلیه : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:DropDownList ID="InstalTimeOutList" runat="server" CssClass="RequestValue">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="InstalTimeOutValidator" runat="server" ControlToValidate="InstalTimeOutList"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا مهلت تخلیه را انتخاب نمایید !"
                        InitialValue="0" CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>--%>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="SpliterBuchtLabel" runat="server" Text="پورت : " />
                </td>
                <td class="RequestColumnValue">
                    <%--<asp:Label ID="SpliterColumnLabel" runat="server" Text="ردیف : " CssClass="BuchtHeader" />--%>
                    <asp:TextBox ID="PortColumnTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="SpliterColumnValidator" runat="server" ControlToValidate="PortColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا پورت را وارد نمایید !" CssClass="ErrorMessage"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="SpliterColumnRegular" runat="server" ControlToValidate="PortColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا پورت معتبر وارد نمایید !" CssClass="ErrorMessage"
                        Display="Dynamic" ValidationExpression="^\d+$" />
                    <%--<asp:Label ID="SpliterRowLabel" runat="server" Text="طبقه : " CssClass="BuchtHeader" />
                    <asp:RequiredFieldValidator ID="SpliterRowValidator" runat="server" ControlToValidate="SpliterRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="SpliterRowRegular" runat="server" ControlToValidate="SpliterRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic"
                        ValidationExpression="^\d+$" />
                    <asp:TextBox ID="SpliterRowTextBox" runat="server" CssClass="BuchtValue" />
                    <asp:Label ID="SpliterNoLabel" runat="server" Text="اتصالی : " CssClass="BuchtHeader" />
                    <asp:RequiredFieldValidator ID="SpliterNoValidator" runat="server" ControlToValidate="SpliterNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="SpliterNoRegular" runat="server" ControlToValidate="SpliterNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic"
                        ValidationExpression="^\d+$" />
                    <asp:TextBox ID="SpliterNoTextBox" runat="server" CssClass="BuchtValue" />--%>
                </td>
            </tr>
            <%-- <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="LineBuchtLabel" runat="server" Text="بوخت لاین : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:Label ID="LineColumnLabel" runat="server" Text="ردیف : " CssClass="BuchtHeader" />
                    <asp:RequiredFieldValidator ID="LineColumnValidator" runat="server" ControlToValidate="LineColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="LineColumnRegular" runat="server" ControlToValidate="LineColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic"
                        ValidationExpression="^\d+$" />
                    <asp:TextBox ID="LineColumnTextBox" runat="server" CssClass="BuchtValue" />
                    <asp:Label ID="LineRowLabel" runat="server" Text="طبقه : " CssClass="BuchtHeader" />
                    <asp:RequiredFieldValidator ID="LineRowValidator" runat="server" ControlToValidate="LineRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="LineRowRegular" runat="server" ControlToValidate="LineRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic"
                        ValidationExpression="^\d+$" />
                    <asp:TextBox ID="LineRowTextBox" runat="server" CssClass="BuchtValue" />
                    <asp:Label ID="LineNoLabel" runat="server" Text="اتصالی : " CssClass="BuchtHeader" />
                    <asp:RequiredFieldValidator ID="LineNoValidator" runat="server" ControlToValidate="LineNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="LineNoRegular" runat="server" ControlToValidate="LineNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage=" * " CssClass="ErrorMessage" Display="Dynamic"
                        ValidationExpression="^\d+$" />
                    <asp:TextBox ID="LineNoTextBox" runat="server" CssClass="BuchtValue" />
                </td>
            </tr>--%>
        </table>
        <div class="Footer">
            <div class="MessageContainer" style="float: right;">
                <asp:Label ID="SaveSuccessMessageLabel" runat="server" Visible="false" CssClass="SuccessMessage" />
                <asp:Label ID="SaveErrorMessageLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
            </div>
            <div style="float: left;">
                <asp:Button ID="SaveButton" runat="server" Text="ذخیره" CssClass="ButtonValue" OnClick="SaveButton_Click"
                    ValidationGroup="SaveGroup" />
                <asp:Button ID="ResetButton" runat="server" Text="بازنشانی" CssClass="ButtonValue"
                    OnClick="ResetButton_Click" />
            </div>
        </div>
    </div>
</asp:Content>
