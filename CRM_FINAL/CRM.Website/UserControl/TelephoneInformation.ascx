<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TelephoneInformation.ascx.cs" Inherits="CRM.Website.UserControl.TelephoneInformation" %>
<div id="MainTelephoneInformationDiv" class="inlineDiv">
    <dl>
        <dt>شماره تلفن : </dt>
        <dd>
            <asp:TextBox ID="PhoneNoTextBox" runat="server" TabIndex="1" ReadOnly="true" /></dd>
        <dt>کدملی/شماره ثبت : </dt>
        <dd>
            <asp:TextBox ID="NationalCodeOrRecordNoTextBox" runat="server" TabIndex="2" ReadOnly="true"/></dd>
        <dt>تلفن ضروری :</dt>
        <dd>
            <asp:TextBox ID="CustomerTelephoneTextBox" runat="server" TabIndex="4" ReadOnly="true"/></dd>

        <dt>نام مرکز :</dt>
        <dd>
            <asp:TextBox ID="CenterTextBox" runat="server" TabIndex="6" ReadOnly="true"/></dd>
        <dt>آدرس :</dt>
        <dd>
            <asp:TextBox ID="AddressTextBox" runat="server"  TabIndex="8" ReadOnly="true" /></dd>
    </dl>
    <dl>
        <dt></dt>
        <dd></dd>
        <dt>نام مشترک :</dt>
        <dd>
            <asp:TextBox ID="CustomerNameTextBox" runat="server" TabIndex="3"  ReadOnly="true"/></dd>
        <dt>تلفن همراه :</dt>
        <dd>
            <asp:TextBox ID="MobileTextBox" runat="server" TabIndex="5" ReadOnly="true" /></dd>
        <dt>کد پستی :</dt>
        <dd>
            <asp:TextBox ID="PostalCodeTextBox" runat="server"  TabIndex="7" ReadOnly="true" /></dd>
        <dt></dt>
        <dd></dd>
    </dl>

</div>
