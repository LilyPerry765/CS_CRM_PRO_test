<%@ Page Title="هزینه های متفرقه" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="OtherCostForm.aspx.cs" Inherits="CRM.Website.Viewes.OtherCostForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="OtherCostContent" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
    <script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
    <div runat="server" class="OtherCostDiv">
        <dl>
            <dt>هزینه مربوط است به :</dt>
            <dd>
                <asp:DropDownList ID="WorkUnitDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

            <dt>فعال :</dt>
            <dd>
                <asp:DropDownList ID="IsActiveDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

            <dt>تاریخ درج :</dt>
            <dd>
                <asp:TextBox ID="InsertDateTextBox" runat="server" ClientIDMode="Static" onclick="ShowDateTimePicker(this.id);" /></dd>

            <dt id="RequestDT" runat="server" style="display: none">درخواست</dt>
            <dd id="RequestDD" runat="server" style="display: none">
                <asp:TextBox ID="RequestTextBox" runat="server" /></dd>

            <dt>مبلغ قیمت پایه :</dt>
            <dd>
                <asp:TextBox ID="BasePriceTextBox" runat="server" /></dd>

            <dt>عنوان هزینه :</dt>
            <dd>
                <asp:TextBox ID="CostTitleTextBox" runat="server" /></dd>

            <dt>علت :</dt>
            <dd>
                <asp:TextBox ID="ReasonTextBox" runat="server" /></dd>
        </dl>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <asp:Button ID="SaveButton" runat="server" Text="ذخیره" OnClick="SaveButton_Click" />
</asp:Content>
