<%@ Page Title="فاکتور فروش" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="SaleFactorForm.aspx.cs" Inherits="CRM.Website.Viewes.SaleFactorForm" %>
<%@ Register Src="~/UserControl/ActionUserControl.ascx" TagPrefix="Action" TagName="ActionUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <div runat="server" id="SaleFactorDiv" class="MainSaleFactorDiv">

        <%--   <asp:Panel ID="RequestDetailHeaderPanel" runat="server" CssClass="panelsheader">
                    <asp:Label ID="RequestDetailLabel" runat="server" Text="مشخصات"  />
                </asp:Panel>
                <asp:Panel ID="RequestDetailPanel" runat="server">
                </asp:Panel>--%>

        <div class="SaleFactorProperties">
            <label class="panelsheader">مشخصات</label>
            <dl>
                <dt class="hidden">شناسه قبض :</dt>
                <dd class="hidden">
                    <asp:TextBox ID="BillIDTextBox" runat="server" /></dd>

                <dt >نام درخواست :</dt>
                <dd>
                    <asp:TextBox ID="RequestTypeTextBox" runat="server" /></dd>

                <dt>تاریخ درخواست :</dt>
                <dd>
                    <asp:TextBox ID="InsertDateTextBox" runat="server" /></dd>

                <dt>نام مشترک :</dt>
                <dd>
                    <asp:TextBox ID="CustomerNameTextBox" runat="server" /></dd>

                <dt>نام مرکز :</dt>
                <dd>
                    <asp:TextBox ID="CenterTextBox" runat="server" /></dd>
            </dl>
            <dl>
                <dt class="hidden">شناسه پرداخت :</dt>
                <dd class="hidden">
                    <asp:TextBox ID="PaymentIDTextBox" runat="server" /></dd>

                <dt>شماره درخواست :</dt>
                <dd>
                    <asp:TextBox ID="RequestIDTextBox" runat="server" /></dd>

                <dt>تاریخ صدور فاکتور :</dt>
                <dd>
                    <asp:TextBox ID="PrintDateTextBox" runat="server" /></dd>

                <dt>شماره تلفن :</dt>
                <dd>
                    <asp:TextBox ID="TelephoneNoTextBox" runat="server" /></dd>

                <dt></dt>
                <dd></dd>

            </dl>
        </div>

        <div class="SaleFactorProperties">
            <label class="panelsheader">پرداخت ها</label>
            <div class="PaymentsDiv">
                <asp:GridView ID="PaymentsGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None"
                    AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                    <Columns>
                        <asp:TemplateField HeaderText="عنوان">
                            <ItemTemplate>
                                <asp:DropDownList Enabled="false" runat="server" ID="PaymentTitleDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="PaymentTitleDataSource" SelectedValue='<%# Bind("BaseCostID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="نحوه پرداخت">
                            <ItemTemplate>
                                <asp:DropDownList Enabled="false" runat="server" ID="PaymentTypeDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="PaymentsPaymentTypeDataSource" SelectedValue='<%# Bind("PaymentType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Cost" HeaderText="مبلغ" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                        <asp:BoundField DataField="Abonman" HeaderText="آبونمان" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                        <asp:BoundField DataField="Tax" HeaderText="مالیات" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                        <asp:BoundField DataField="AmountSum" HeaderText="جمع" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />

                    </Columns>
                </asp:GridView>
            </div>
            <asp:ObjectDataSource ID="PaymentTitleDataSource" runat="server" TypeName="CRM.Data.BaseCostDB" SelectMethod="GetBaseCostCheckable" />
            <asp:ObjectDataSource ID="PaymentsPaymentTypeDataSource" runat="server" TypeName="CRM.Application.Helper" SelectMethod="GetEnumCheckable" OnSelecting="PaymentsPaymentTypeDataSource_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="enumType" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <div class="floatleft">
                <label>جمع پرداخت ها</label>
                <asp:TextBox ID="CostSumTextBox" runat="server" ReadOnly="true" />
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
     <Action:ActionUserControl runat="server" ID="ActionUserControl" ClientIDMode="Static" />
</asp:Content>
