<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="PAPExchangeRequestForm.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.PAPExchangeRequestForm" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>ثبت درخواست</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header">
            <asp:Label ID="lblNewsHeader" runat="server" Text="ثبت درخواست تعویض پورت" />
        </div>
        <table class="RequestTabel">
            <tr class="RequestRow">
                <td class="RequestColumnHeader" colspan="2">
                    <asp:Label ID="MessageTelephoneLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن (8 رقمی) : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="TelephoneNoTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="TelephoneNoValidator" runat="server" ControlToValidate="TelephoneNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا شماره تلفن را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="TelephoneNoRegular" runat="server" ControlToValidate="TelephoneNoTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا شماره تلفن صحیح 8 رقمی را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d{8}$" />
                </td>
            </tr>
            <%--<tr class="RequestRow">
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
            </tr>--%>
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
                    <asp:Label ID="InstalDateLabel" runat="server" Text="تاریخ دایری : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:DropDownList ID="InstalTimeOutList" runat="server" CssClass="RequestValue">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="InstalTimeOutValidator" runat="server" ControlToValidate="InstalTimeOutList"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا مهلت دایری را انتخاب نمایید !"
                        InitialValue="0" CssClass="ErrorMessage" Display="Dynamic" />
                </td>
            </tr>--%>
            <%--<tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="OldRowLabel" runat="server" Text="ردیف قدیم : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="OldRowTextBox" runat="server" CssClass="RequestValue" />                    
                    <asp:RequiredFieldValidator ID="OldRowValidator" runat="server" ControlToValidate="OldRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا ردیف قدیم را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="OldRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا ردیف قدیم معتبر وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="OldColumnLabel" runat="server" Text="طبقه قدیم : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="OldColumnTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="OldColumnValidator" runat="server" ControlToValidate="OldColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا طبقه قدیم را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="OldColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا طبقه قدیم معتبر وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="OldBuchtLabel" runat="server" Text="اتصالی قدیم : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="OldBuchtTextBox" runat="server" CssClass="RequestValue" />
                    <asp:RequiredFieldValidator ID="SpliterColumnValidator" runat="server" ControlToValidate="OldBuchtTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا اتصالی قدیم را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="SpliterColumnRegular" runat="server" ControlToValidate="OldBuchtTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا اتصالی قدیم معتبر وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />
                </td>
            </tr>--%>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="NewRowLabel" runat="server" Text="ردیف جدید : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="NewRowTextBox" runat="server" CssClass="RequestValue" />
                    <a href="PAPEquipmentList.aspx" target="_blank" class="EquipmentLink">نمایش تجیهزات
                        فنی در پنجره جدید</a>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NewRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا ردیف جدید را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="NewRowTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا ردیف جدید معتبر وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="NewColumnLabel" runat="server" Text="طبقه جدید : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="NewColumnTextBox" runat="server" CssClass="RequestValue" />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا طبقه جدید را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="NewColumnTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا طبقه جدید معتبر وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />
                </td>
            </tr>
            <tr class="RequestRow">
                <td class="RequestColumnHeader">
                    <asp:Label ID="NewBuchtLabel" runat="server" Text="اتصالی جدید : " />
                </td>
                <td class="RequestColumnValue">
                    <asp:TextBox ID="NewBuchtTextBox" runat="server" CssClass="RequestValue" />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewBuchtTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا اتصالی جدید را وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" />--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NewBuchtTextBox"
                        ValidationGroup="SaveGroup" ErrorMessage="* لطفا اتصالی جدید معتبر وارد نمایید !"
                        CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />
                </td>
            </tr>
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
