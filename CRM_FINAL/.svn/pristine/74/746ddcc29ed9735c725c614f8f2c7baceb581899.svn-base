<%@ Page Title="پرداخت با شناسه" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="PaidFactorForm.aspx.cs" Inherits="CRM.Website.Viewes.PaidFactorForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js"></script>
    <script src="../Scripts/calendar/calendar2.js"></script>
    <div class="MainPaidFactorDiv">
        <dl>
            <dt class="hidden" >شناسه قبض :</dt>
            <dd class="hidden">
                <asp:TextBox ID="BillIDTextBox" runat="server" OnTextChanged="AmountTextBox_TextChanged" /></dd>

            <dt class="hidden">شناسه پرداخت :</dt>
            <dd class="hidden">
                <asp:TextBox ID="PaymentIDTextBox" runat="server" CssClass="hidden" OnTextChanged="AmountTextBox_TextChanged" />
                <asp:DropDownList ID="PaymentIDDropDownList" runat="server" CssClass="hidden" OnSelectedIndexChanged="PaymentIDDropDownList_SelectedIndexChanged" DataValueField="ID" DataTextField="Name" AutoPostBack="true" />
            </dd>

            <dt>نحوه پرداخت :</dt>
            <dd>
                <asp:DropDownList ID="PaymentWayDropDownList" runat="server" DataValueField="ID" DataTextField="Name" />
            </dd>

            <dt>بانک :</dt>
            <dd>
                <asp:DropDownList ID="BankDropDownList" runat="server" DataValueField="ID" DataTextField="Name" />
            </dd>

            <dt>مبلغ :</dt>
            <dd>
                <asp:TextBox ID="AmountSumTextBox" runat="server" ReadOnly="true" />
            </dd>

            <dt>شماره فیش  :</dt>
            <dd>
                <asp:TextBox ID="FicheNunmberTextBox" runat="server" />
            </dd>

            <dt>تاریخ فیش  :</dt>
            <dd>
                <asp:TextBox ID="FicheDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
            </dd>

            <dt>تاریخ دریافت فیش  :</dt>
            <dd>
                <asp:TextBox ID="PaymentDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
            </dd>


            <dt>افزودن رسید پرداخت :</dt>
            <dd>
                <asp:FileUpload ID="PaymentReceiptUpload" runat="server" />
                <%--<asp:RadioButton ID="FileRadioButton" runat="server" OnCheckedChanged="FileRadioButton_CheckedChanged" Text="فایل" />
                <asp:RadioButton ID="ScannerRadioButton" runat="server" OnCheckedChanged="FileRadioButton_CheckedChanged" Text="اسکنر"  />--%>
                <asp:ImageButton ID="FileImageButton" runat="server" ImageUrl="~/Images/DocumentPicture16x16.png" OnClick="FileImageButton_Click" />
            </dd>

        </dl>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <asp:Button ID="SaveButton" runat="server" Text="ذخیره" OnClick="SaveButton_Click" />
</asp:Content>
