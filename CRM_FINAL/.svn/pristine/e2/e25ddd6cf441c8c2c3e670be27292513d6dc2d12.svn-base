<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="PAPDischargeRequestForm.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.PAPDischargeRequestForm" %>

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
                <td class="RequestColumnHeader" style="float: left;">
                    <asp:Button ID="TestPannelButton" Text="نمایش مشخصات فنی" runat="server" OnClick="Click"
                        CssClass="ButtonValue" />
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TestPannelButton" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table class="RequestTabel" style="border-color: White">
                    <tr class="RequestRow">
                        <td class="RequestColumnHeader">
                            <asp:Label ID="RowLabel1" runat="server" Text="ردیف : " />
                        </td>
                        <td class="RequestColumnValue">
                            <asp:Label ID="RowLabel" runat="server" CssClass="RequestValue" />
                            <%--<asp:TextBox ID="RowTextBox" runat="server" CssClass="RequestValue" />
                            <a href="PAPEquipmentList.aspx" target="_blank" class="EquipmentLink">نمایش تجیهزات
                                فنی در پنجره جدید</a>
                            <asp:RequiredFieldValidator ID="RowValidator" runat="server" ControlToValidate="RowTextBox"
                                ValidationGroup="SaveGroup" ErrorMessage="* لطفا ردیف را وارد نمایید !" CssClass="ErrorMessage"
                                Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="RowTextBox"
                                ValidationGroup="SaveGroup" ErrorMessage="* لطفا ردیف معتبر وارد نمایید !" CssClass="ErrorMessage"
                                Display="Dynamic" ValidationExpression="^\d+$" />--%>
                        </td>
                    </tr>
                    <tr class="RequestRow">
                        <td class="RequestColumnHeader">
                            <asp:Label ID="ColumnLabel1" runat="server" Text="طبقه : " />
                        </td>
                        <td class="RequestColumnValue">
                        <asp:Label ID="ColumnLabel" runat="server" CssClass="RequestValue" />
                            <%--<asp:TextBox ID="ColumnTextBox" runat="server" CssClass="RequestValue" />
                            <asp:RequiredFieldValidator ID="ColumnValidator" runat="server" ControlToValidate="ColumnTextBox"
                                ValidationGroup="SaveGroup" ErrorMessage="* لطفا طبقه را وارد نمایید !" CssClass="ErrorMessage"
                                Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ColumnTextBox"
                                ValidationGroup="SaveGroup" ErrorMessage="* لطفا طبقه معتبر وارد نمایید !" CssClass="ErrorMessage"
                                Display="Dynamic" ValidationExpression="^\d+$" />--%>
                        </td>
                    </tr>
                    <tr class="RequestRow">
                        <td class="RequestColumnHeader">
                            <asp:Label ID="BuchtLabel1" runat="server" Text="اتصالی : " />
                        </td>
                        <td class="RequestColumnValue">
                            <asp:Label ID="BuchtLabel" runat="server" CssClass="RequestValue" />
                            <%--<asp:TextBox ID="BuchtTextBox" runat="server" CssClass="RequestValue" />
                            <asp:RequiredFieldValidator ID="BuchtValidator" runat="server" ControlToValidate="BuchtTextBox"
                                ValidationGroup="SaveGroup" ErrorMessage="* لطفا اتصالی را وارد نمایید !" CssClass="ErrorMessage"
                                Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="BuchtRegular" runat="server" ControlToValidate="BuchtTextBox"
                                ValidationGroup="SaveGroup" ErrorMessage="* لطفا اتصالی معتبر وارد نمایید !"
                                CssClass="ErrorMessage" Display="Dynamic" ValidationExpression="^\d+$" />--%>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
