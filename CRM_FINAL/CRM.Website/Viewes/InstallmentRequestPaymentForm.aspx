<%@ Page Title="اقساط" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="InstallmentRequestPaymentForm.aspx.cs" Inherits="CRM.Website.Viewes.InstallmentRequestPaymentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js"></script>
    <script src="../Scripts/calendar/calendar2.js"></script>
    <link href="../Contents/Screen.css" rel="stylesheet" />

    <div id="MainInstallmentRequestPaymentFormDiv" class="MainInstallmentRequestPaymentFormDiv">
        <div id="MainInstallmentDiv" class="MainInstallmentDiv">

            <dl>
                <dt>مبلغ پیش پرداخت :</dt>
                <dd>
                    <asp:TextBox ID="PrePaymentAmountTextBox" runat="server" OnTextChanged="TextBox_TextChanged" />
                </dd>

                <dt>شروع دوره :</dt>
                <dd>
                    <asp:TextBox ID="StartDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
                </dd>

                <dt>روز شمار :</dt>
                <dd>
                    <asp:CheckBox ID="DailyCheckBox" runat="server" />
                </dd>

            </dl>

            <dl>
                <dt>تعداد اقساط : </dt>
                <dd>
                    <asp:TextBox ID="InstallmentCountTextBox" runat="server" OnTextChanged="TextBox_TextChanged" />
                </dd>

                <dt>پایان دوره :</dt>
                <dd>
                    <asp:TextBox ID="EndDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
                </dd>

                <dt>دریافت از طریق چک :</dt>
                <dd>
                    <asp:CheckBox ID="IsChequeCheckBox" runat="server" />
                </dd>
            </dl>
            <br />
            <asp:Button ID="SaveButton" runat="server" Text="ذخیره" OnClick="SaveButton_Click" />
        </div>
        <br />
        <div class="InstallmentRequestPaymentDiv">
            <asp:GridView ID="InstallmentRequestPaymentGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowCommand="InstallmentRequestPaymentGridView_RowCommand" OnRowDataBound="InstallmentRequestPaymentGridView_RowDataBound"
                AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                <Columns>
                    <asp:BoundField DataField="StartDate" HeaderText=" تاریخ شروع" HeaderStyle-CssClass="HeaderStyle"  />
                    <asp:BoundField DataField="EndDate" HeaderText="تاریخ پایان" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="ChequeNumber" HeaderText="شماره چک" HeaderStyle-CssClass="HeaderStyle"  />
                    <asp:BoundField DataField="Cost" HeaderText="مبلغ" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Modify" runat="server" Text="چک" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="floatleft">
            <label>جمع اقساط : </label>
            <asp:TextBox ID="SumOfInstallment" runat="server" />
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
     <asp:Button ID="DeleteButton" runat="server" Text="حذف اقساط" OnClick="DeleteButton_Click" /> 
</asp:Content>
