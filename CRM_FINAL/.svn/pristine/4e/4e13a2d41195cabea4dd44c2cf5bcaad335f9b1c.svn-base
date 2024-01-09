<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="PAPDischargeRequestSearchList.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.PAPDischargeRequestSearchList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>--%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>جستجو درخواست تخلیه</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header" style="width: 98%;">
            <asp:Label ID="lblNewsHeader" runat="server" Text="جستجو درخواست تخلیه" />
        </div>
        <div class="SearchRequest">
            <table class="Searchtable">
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="TelephoneNoTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:DropDownList ID="CenterList" runat="server" CssClass="SearchValue">
                        </asp:DropDownList>
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="CustomerNameLabel" runat="server" Text="نام مشترک : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="CustomerNameTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                </tr>
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="FromInsertDateLabel" runat="server" Text="تاریخ ارسال درخواست از : " />
                    </td>
                    <td class="SearchColumnValue">
                        <%-- <pdc:PersianDateTextBox ID="PersianDateTextBox4" runat="server" IconUrl="~/Images/Calendar.gif" Width="173px" SetDefaultDateOnEvent="OnClick" AutoPostBack="True" OnTextChanged="PersianDateTextBox4_TextChanged">
                        </pdc:PersianDateTextBox>
                        <pdc:PersianDateScriptManager ID="PersianDateScriptManager1" runat="server" CalendarCSS="PickerCalendarCSS"
                            FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS" FrameCSS="PickerCSS"
                            HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS"
                            WorkDayCSS="PickerWorkDayCSS" ForbidenDates="[0,11,22],[0,12,29],[0,0,13]" ForbidenWeekDays="5,6">
                        </pdc:PersianDateScriptManager>--%>
                        <asp:TextBox ID="FromInsertDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="FromEndDateLabel" runat="server" Text="تاریخ اتمام تخلیه از : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="FromEndDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="CustomerStatusLabel" runat="server" Text="وضعیت مشترک : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:DropDownList ID="CustomerStatusDropDown" runat="server" CssClass="SearchValue">
                            <asp:ListItem Text="-- انتخاب نمایید --" Value="0" />
                            <asp:ListItem Text="مالک" Value="1" />
                            <asp:ListItem Text="نماینده" Value="2" />
                            <asp:ListItem Text="مستاجر" Value="3" />
                        </asp:DropDownList>
                    </td>
                    <%--<td class="SearchColumnHeader">
                        <asp:Label ID="InstalTimeOutLabel" runat="server" Text="مهلت تخلیه : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:DropDownList ID="InstalTimeOutList" runat="server" CssClass="SearchValue">
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="ToInsertDateLabel" runat="server" Text="تاریخ ارسال درخواست تا : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="ToInsertDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="ToEndDateLabel" runat="server" Text="تاریخ اتمام تخلیه تا : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="ToEndDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                </tr>
                <tr class="SearchRow">
                    <td class="SearchColumnHeader" colspan="6" style="text-align: left">
                        <asp:Button ID="SearchButton" runat="server" Text="جستجو" CssClass="SearchButtonValue"
                            OnClick="SearchButton_Click" />
                        <asp:Button ID="ResetButton" runat="server" Text="بازنشانی" CssClass="SearchButtonValue"
                            OnClick="ResetButton_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="direction: rtl; text-align: right;">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:TabContainer ID="RequestListTabContainer" runat="server">
                <asp:TabPanel ID="PendingRequestPanel" runat="server">
                    <HeaderTemplate>
                        <asp:Label ID="PendingRequestHeader" runat="server" Text="ارسال شده ها" CssClass="TabContainerHeader" />
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:ListView ID="PendingRequestListView" runat="server">
                            <LayoutTemplate>
                                <table class="RequestGridHeader">
                                    <tr>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="IDLabel1" runat="server" Text="شناسه" />
                                        </td>
                                        <td style="width: 80px; text-align: center;">
                                            <asp:Label ID="TelephoneNoLabel1" runat="server" Text="شماره تلفن" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerLabel1" runat="server" Text="نام مشترک" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerStatusLabel1" runat="server" Text="وضعیت مشترک" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="SplitorBuchtLabel1" runat="server" Text="بوخت" />
                                        </td>
                                        <%--<td style="width: 50px; text-align: center;">
                                            <asp:Label ID="LineBuchtLabel1" runat="server" Text="بوخت لاین" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InsertDateLabel1" runat="server" Text="تاریخ ارسال درخواست" />
                                        </td>
                                        <%--<td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InstalDateLabel1" runat="server" Text="مهلت تخلیه" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="StepLabel1" runat="server" Text="مرحله" />
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <table width="100%" style="text-align: center;">
                                    <tr class="RequestGridItems">
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="IDLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 80px; text-align: center;">
                                            <asp:Label ID="TelephoneNoLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TelephoneNo").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" CssClass="ddd" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="CenterLabel1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Center").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Customer").ToString() %>' />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerStatusLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustomerStatus").ToString() %>' />
                                        </td>
                                        <td style="width: 50px; direction: ltr; text-align: center;">
                                            <asp:Label ID="SplitorBuchtLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ADSLPAPPort").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <%-- <td style="width: 50px; direction: ltr; text-align: center;">
                                            <asp:Label ID="LineBuchtLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineBucht").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InsertDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InsertDate").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <%--<td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InstalDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InstalTimeOut").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="StepLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Step").ToString() %>' />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="RejectRequestPanel" runat="server" HeaderText="رد شده ها">
                    <HeaderTemplate>
                        <asp:Label ID="RejectRequestHeader" runat="server" Text="رد شده ها" CssClass="TabContainerHeader" />
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:ListView ID="RejectRequestListView" runat="server">
                            <LayoutTemplate>
                                <table class="RequestGridHeader">
                                    <tr>
                                        <td style="width: 50px;">
                                            <asp:Label ID="IDLabel" runat="server" Text="شناسه" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز" />
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:Label ID="CustomerLabel" runat="server" Text="نام مشترک" />
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:Label ID="CustomerStatusLabel" runat="server" Text="وضعیت مشترک" />
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:Label ID="SplitorBuchtLabel" runat="server" Text="پورت" />
                                        </td>
                                        <%--<td style="width: 50px;">
                                            <asp:Label ID="LineBuchtLabel" runat="server" Text="بوخت لاین" />
                                        </td>--%>
                                        <td style="width: 50px;">
                                            <asp:Label ID="InsertDateLabel" runat="server" Text="تاریخ ارسال درخواست" />
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:Label ID="RejectCommnetLabel" runat="server" Text="علت رد درخواست" />
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <table width="100%">
                                    <tr class="RequestGridItems">
                                        <td style="width: 50px;">
                                            <asp:Label ID="IDLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="TelephoneNoLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TelephoneNo").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="CenterLabel1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Center").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:Label ID="CustomerLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Customer").ToString() %>' />
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:Label ID="CustomerStatusLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustomerStatus").ToString() %>' />
                                        </td>
                                        <td style="width: 50px; direction: ltr;">
                                            <asp:Label ID="SplitorBuchtLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ADSLPAPPort").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <%--<td style="width: 50px; direction: ltr;">
                                            <asp:Label ID="LineBuchtLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineBucht").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>--%>
                                        <td style="width: 50px;">
                                            <asp:Label ID="InsertDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InsertDate").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:Label ID="RejectCommnetLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Comment").ToString() %>' />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="CompletedRequestTabPanel" runat="server" HeaderText="تمام شده ها">
                    <HeaderTemplate>
                        <asp:Label ID="CompletedRequestHeader" runat="server" Text="تمام شده ها" CssClass="TabContainerHeader" />
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:ListView ID="CompletedRequestListView" runat="server">
                            <LayoutTemplate>
                                <table class="RequestGridHeader">
                                    <tr>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="IDLabel" runat="server" Text="شناسه" />
                                        </td>
                                        <td style="width: 80px; text-align: center;">
                                            <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerLabel" runat="server" Text="نام مشترک" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerStatusLabel" runat="server" Text="وضعیت مشترک" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="SplitorBuchtLabel" runat="server" Text="پورت" />
                                        </td>
                                        <%--<td style="width: 50px; text-align: center;">
                                            <asp:Label ID="LineBuchtLabel" runat="server" Text="بوخت لاین" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InsertDateLabel" runat="server" Text="تاریخ ارسال درخواست" />
                                        </td>
                                        <%--<td style="width: 50px;">
                                            <asp:Label ID="InstalDateLabel" runat="server" Text="مهلت تخلیه" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="EndDateLabel" runat="server" Text="تاریخ تخلیه" />
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <table width="100%">
                                    <tr class="RequestGridItems">
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="IDLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 80px; text-align: center;">
                                            <asp:Label ID="TelephoneNoLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TelephoneNo").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="CenterLabel1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Center").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Customer").ToString() %>' />
                                        </td>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="CustomerStatusLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustomerStatus").ToString() %>' />
                                        </td>
                                        <td style="width: 50px; direction: ltr; text-align: center;">
                                            <asp:Label ID="SplitorBuchtLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ADSLPAPPort").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <%--<td style="width: 50px; direction: ltr; text-align: center;">
                                            <asp:Label ID="LineBuchtLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineBucht").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InsertDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InsertDate").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                        <%--<td style="width: 50px; text-align: center;">
                                            <asp:Label ID="InstalDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InstalTimeOut").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>--%>
                                        <td style="width: 50px; text-align: center;">
                                            <asp:Label ID="EndDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate").ToString() %>'
                                                Font-Names="B Yekan" Font-Size="9pt" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </div>
</asp:Content>
