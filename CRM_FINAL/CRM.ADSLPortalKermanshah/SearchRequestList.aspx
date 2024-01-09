<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="SearchRequestList.aspx.cs" Inherits="CRM.ADSLPortalKermanshah.SearchRequestList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>--%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>جستجو درخواست</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header" style="width: 98%">
            <asp:Label ID="lblNewsHeader" runat="server" Text="جستجو درخواست" />
        </div>
        <div class="SearchRequest">
            <table class="Searchtable">
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="TelephoneNoTextBox" runat="server" CssClass="SearchValue" Width="200px" />
                    </td>
                    <%--<td class="SearchColumnHeader">
                        <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:DropDownList ID="CenterList" runat="server" CssClass="SearchValue">
                        </asp:DropDownList>
                    </td>--%>
                    <td class="SearchColumnHeader">
                    </td>
                    <td class="SearchColumnValue">
                    </td>
                    <td class="SearchColumnHeader">
                    </td>
                    <td class="SearchColumnValue">
                    </td>
                </tr>
                <%--<tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="FromInsertDateLabel" runat="server" Text="تاریخ ارسال درخواست از : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="FromInsertDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="FromEndDateLabel" runat="server" Text="تاریخ اتمام دایری از : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="FromEndDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="ToInsertDateLabel" runat="server" Text="تاریخ ارسال درخواست تا : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="ToInsertDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="ToEndDateLabel" runat="server" Text="تاریخ اتمام دایری تا : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="ToEndDateTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                </tr>--%>
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
            <asp:Label ID="ErrorLabel" runat="server" Visible="false" CssClass="ErrorMessage" />
            <asp:ListView ID="PendingRequestListView" runat="server">
                <LayoutTemplate>
                    <table class="RequestGridHeader">
                        <tr>
                            <td style="width: 30px; text-align: center;">
                                <asp:Label ID="IDLabel1" runat="server" Text="شناسه" />
                            </td>
                            <td style="width: 60px; text-align: center;">
                                <asp:Label ID="TelephoneNoLabel1" runat="server" Text="شماره تلفن" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="CustomerLabel1" runat="server" Text="نام مشترک" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="RequestTypeLabel1" runat="server" Text="نوع درخواست" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="InsertDateLabel1" runat="server" Text="تاریخ درخواست" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="EndDateLabel1" runat="server" Text="تاریخ اتمام درخواست" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="StepLabel1" runat="server" Text="مرحله" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="StatusLabel1" runat="server" Text="وضعیت" />
                            </td>
                            <td style="width: 90px; text-align: center;">
                                <asp:Label ID="SplitorBuchtLabel1" runat="server" Text="بوخت" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="CommentLabel1" runat="server" Text="توضیحات" />
                            </td>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <table width="100%" style="text-align: center;">
                        <tr class="RequestGridItems">
                            <td style="width: 30px; text-align: center;">
                                <asp:Label ID="IDLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" />
                            </td>
                            <td style="width: 60px; text-align: center;">
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
                                <asp:Label ID="CustomerStatusLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RequestType").ToString() %>' />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="InsertDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InsertDate").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="EndDateLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="StepLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Step").ToString() %>' />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="StatusLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status").ToString() %>' />
                            </td>
                            <td style="width: 90px; direction: ltr; text-align: center;">
                                <asp:Label ID="ADSLPortLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ADSLPAPPort").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" />
                            </td>
                            <td style="width: 90px; direction: ltr; text-align: center;">
                                <asp:Label ID="CommnetLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Comment").ToString() %>' />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
