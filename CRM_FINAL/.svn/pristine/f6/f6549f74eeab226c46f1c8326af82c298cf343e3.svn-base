<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="PAPEquipmentList.aspx.cs" Inherits="CRM.ADSLPortal.PAPEquipmentList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>تجهیزات فنی</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Container">
        <div class="Header" style="width: 98%">
            <asp:Label ID="lblNewsHeader" runat="server" Text="جستجو" />
        </div>
        <div class="SearchRequest">
            <table class="Searchtable">
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="CenterLabel" runat="server" Text="نام مرکز : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:DropDownList ID="CenterList" runat="server" CssClass="SearchValue">
                        </asp:DropDownList>
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="PortNoLabel" runat="server" Text="شماره پورت : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="PortNoTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                    <td class="SearchColumnHeader">
                        <asp:Label ID="TelephoneNoLabel" runat="server" Text="شماره تلفن : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:TextBox ID="TelephoneNoTextBox" runat="server" CssClass="SearchValue" />
                    </td>
                </tr>
                <tr class="SearchRow">
                    <td class="SearchColumnHeader">
                        <asp:Label ID="StatusLabel" runat="server" Text="وضعیت پورت : " />
                    </td>
                    <td class="SearchColumnValue">
                        <asp:DropDownList ID="StatusDropDown" runat="server" CssClass="SearchValue">
                            <asp:ListItem Text="-- انتخاب نمایید --" Value="-1" />
                            <asp:ListItem Text="آزاد" Value="0" />
                            <asp:ListItem Text="دایر" Value="1" />
                            <asp:ListItem Text="تخلیه" Value="2" />
                            <asp:ListItem Text="خراب" Value="3" />
                        </asp:DropDownList>
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
        <div class="Header" style="width: 98%; margin-top:20px;">
            <asp:Label ID="Label1" runat="server" Text="تجهیزات فنی" />
        </div>
        <div style="direction: rtl; text-align: right; border:1px solid #c0c0c0; padding:5px;">
            <asp:ListView ID="EquipmentListView" runat="server">
                <LayoutTemplate>
                    <table class="RequestGridHeader">
                        <tr>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="CenterIDLabel1" runat="server" Text="نام مرکز" />
                            </td>
                            <td style="width: 80px; text-align: center;">
                                <asp:Label ID="PortNoLabel1" runat="server" Text="پورت" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="TelephoneLabel1" runat="server" Text="شماره تلفن" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="StatusLabel1" runat="server" Text="وضعیت" />
                            </td>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <table width="100%" style="text-align: center;">
                        <tr class="EquipmentGridItems">
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="CenterIDLabel2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Center").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" />
                            </td>
                            <td style="width: 80px; text-align: center;">
                                <asp:Label ID="PortNoLabel2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PortNo").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" CssClass="ddd" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="TelephoneNoLabel2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TelephoneNo").ToString() == null ? "" : DataBinder.Eval(Container.DataItem,"TelephoneNo").ToString() %>'
                                    Font-Names="B Yekan" Font-Size="9pt" CssClass="ddd" />
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="StatusLabel2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status").ToString() %>' />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:ListView>
            <div class="PagerContainer">
                <asp:DataPager ID="PendingRequestPager" runat="server" PageSize="10" OnPreRender="RequestPager_PreRender"
                    PagedControlID="EquipmentListView">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Image" ButtonCssClass="PagerImage" ShowFirstPageButton="true"
                            ShowLastPageButton="true" NextPageImageUrl="~/Images/next_16x16.png" PreviousPageImageUrl="~/Images/previous_16x16.png"
                            LastPageImageUrl="~/Images/last_16x16.png" FirstPageImageUrl="~/Images/first_16x16.png" />
                        <asp:NumericPagerField CurrentPageLabelCssClass="PagerCurrentNumber" NumericButtonCssClass="PagerNumber" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
    </div>
</asp:Content>
