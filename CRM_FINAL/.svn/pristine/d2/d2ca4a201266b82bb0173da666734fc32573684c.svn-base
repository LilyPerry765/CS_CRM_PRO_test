<%@ Page Title="چک اقساط" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="InstallmentRequestPaymentChequeForm.aspx.cs" Inherits="CRM.Website.Viewes.InstallmentRequestPaymentChequeForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js"></script>
    <script src="../Scripts/calendar/calendar2.js"></script>

    <div>
        <label>تاریخ سررسید : </label>
        <asp:TextBox ID="EndDateDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
    </div>

      <div>
        <label>شماره چک : </label>
        <asp:TextBox ID="ChequeNumberTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
    </div>
    <asp:Button ID="SaveButton" runat="server" Text="ذخیره" OnClick="SaveButton_Click" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <asp:Label ID="ErrorLabel" runat="server" Text="" class="errorlabel" ></asp:Label>
</asp:Content>
