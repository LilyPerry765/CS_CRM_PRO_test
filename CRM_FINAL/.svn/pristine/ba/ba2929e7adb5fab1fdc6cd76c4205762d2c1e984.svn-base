<%@ Page Title="ثبت مدرک" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="RequestDocumentForm.aspx.cs" Inherits="CRM.Website.Viewes.RequestDocumentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js"></script>
    <script src="../Scripts/calendar/calendar2.js"></script>
    <link href="../Contents/Screen.css" rel="stylesheet" />

    <div class="MainRequestDocumentFormDiv">
        <label class="TitleLabel">عنوان</label>
        <asp:RadioButton ID="NewRadioButton" runat="server" Text="درج مدرک جدید" GroupName="Radio" Checked="true" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true" />
        <asp:RadioButton ID="CopyRadioButton" runat="server" Text="کپی مدرک از آخرین درخواست" GroupName="Radio" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true" />
        <br />
        <div id="mojavezDiv" runat="server">
            <dl>
                <dt>شماره نامه</dt>
                <dd>
                    <asp:TextBox ID="DocumentNotextBox" runat="server" /></dd>

                <dt>تاریخ نامه</dt>
                <dd>
                    <asp:TextBox ID="DocumentDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>
            </dl>

            <dl>
                <dt>مرجع صادرکننده مجوز</dt>
                <dd>
                    <asp:TextBox ID="IssuingOfficetextBox" runat="server" /></dd>

                <dt>سمت شغلی صادرکننده مجوز</dt>
                <dd>
                    <asp:TextBox ID="IssuingRoletextBox" runat="server" /></dd>
            </dl>
        </div>
         <br />
        <div id="InsertNewDocDiv">
            <div>
                <label class="TitleLabel">فایل پیوست</label>
                <%--<asp:Button ID="UploadFileButton" runat="server" Text="آپلود" OnClick="UploadFileButton_Click" />--%>
                <asp:FileUpload ID="DocumentFileUpload" runat="server" />
            </div>
            <br />
            <div>
                <label class="TitleLabel">اعتبار تا تاریخ </label>
                <asp:TextBox ID="ValidToDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" />
            </div>
              <br />
            <div>
                <asp:Label runat="server" ID="CopyDocSavedValueLabel" Text="فایل دریافت شد برای مشاهده اینجا را کلیک کنید" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
      <asp:Button ID="ViewButton" runat="server" Text="مشاهده" CssClass="hidden" OnClick="ViewButton_Click" />
      <asp:Button ID="SaveButton" runat="server" Text="ذخیره" OnClick="SaveButton_Click" />
</asp:Content>
