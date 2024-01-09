<%@ Page Title="ثبت مشخصات مشترکین" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="CustomerForm.aspx.cs" Inherits="CRM.Website.Viewes.CustomerForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
    <script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="CustomerFormHeader" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="CustomerFormContent" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <div class="maincustomerformcontainer">
        <br />

        <div>
            <label>نوع شخص :</label>
            <asp:RadioButton ID="PersonRadioButton" runat="server" Text="حقیقی" TabIndex="1" Checked="true" OnCheckedChanged="PersonRadioButton_CheckedChanged" GroupName="CustomerType" AutoPostBack="true" />
            <asp:RadioButton ID="CompanyRadioButton" runat="server" Text="حقوقی" TabIndex="2" OnCheckedChanged="CompanyRadioButton_CheckedChanged" GroupName="CustomerType" AutoPostBack="true" />
        </div>

        <div runat="server" id="PersonTypeDiv">
            <dl>
                <dt>
                    <label>کد ملی :</label></dt>
                <dd>
                    <asp:TextBox ID="NationalCodeTextBox" ClientIDMode="Static" runat="server" /></dd>


                <dt>
                    <label>نام :</label></dt>
                <dd>
                    <asp:TextBox ID="FirstNameTextBox" ClientIDMode="Static" runat="server" /></dd>


                <dt>
                    <label>نام پدر :</label></dt>
                <dd>
                    <asp:TextBox ID="FatherNameTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>محل صدور :</label></dt>
                <dd>
                    <asp:TextBox ID="IssuePlaceTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>تلفن تماس :</label></dt>
                <dd>
                    <asp:TextBox ID="UrgentTelNoTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>ایمیل :</label></dt>
                <dd>
                    <asp:TextBox ID="EmailTextBox" ClientIDMode="Static" runat="server" /></dd>



            </dl>

            <dl>
                <dt>
                    <label>جنسیت :</label></dt>
                <dd>
                    <asp:RadioButton ID="FemaleRadioButton" runat="server" Text="زن" />
                    <asp:RadioButton ID="MaleRadioButton" runat="server" Text="مرد" Checked="true" />
                </dd>
                <dt>
                    <label>نام خانوادگی :</label></dt>
                <dd>
                    <asp:TextBox ID="LastNameTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>شماره شناسنامه :</label></dt>
                <dd>
                    <asp:TextBox ID="BirthCertificateIDTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>تاریخ تولد :</label></dt>
                <dd>
                    <asp:TextBox ID="BirthDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>

                <dt>
                    <label>تلفن همراه :</label></dt>
                <dd>
                    <asp:TextBox ID="MobileNoTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>شماره پرونده:</label></dt>
                <dd>
                    <asp:TextBox ID="PersonCreateCustomerIDTextBox" ClientIDMode="Static" runat="server" />
                    <asp:Button ID="PersonCreateCustomerIDButton" runat="server" Text="ایجاد خودکار" OnClick="PersonCreateCustomerIDButton_Click" />

                </dd>
            </dl>
        </div>

        <div runat="server" id="CompanyTypeDiv" visible="false">
            <dl>
                <dt>
                    <label>نام :</label></dt>
                <dd>
                    <asp:TextBox ID="TitleTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>شماره ثبت :</label></dt>
                <dd>
                    <asp:TextBox ID="RecordNoTextBox" ClientIDMode="Static" runat="server" /></dd>


                <dt>
                    <label>تلفن تماس :</label></dt>
                <dd>
                    <asp:TextBox ID="PhoneNoTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>محل صدور :</label></dt>
                <dd>
                    <asp:TextBox ID="CompanyEmailTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>شماره نمایندگی :</label></dt>
                <dd>
                    <asp:TextBox ID="AgencyNoTextBox" ClientIDMode="Static" runat="server" /></dd>
            </dl>

            <dl>
                <dt>
                    <label>شناسه ملی :</label></dt>
                <dd>
                    <asp:TextBox ID="NationIDTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>تاریخ ثبت :</label></dt>
                <dd>
                    <asp:TextBox ID="RecordDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>

                <dt>
                    <label>تلفن همراه :</label></dt>
                <dd>
                    <asp:TextBox ID="CompanyMobileNoTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>نمایندگی :</label></dt>
                <dd>
                    <asp:TextBox ID="AgencyTextBox" ClientIDMode="Static" runat="server" /></dd>
                <dt>
                    <label>شماره پرونده:</label></dt>
                <dd>
                    <asp:TextBox ID="CompanyCreateCustomerIDTextBox" ClientIDMode="Static" runat="server" />
                    <asp:Button ID="CompanyCreateCustomerIDButton" runat="server" Text="ایجاد خودکار" OnClick="PersonCreateCustomerIDButton_Click" />

                </dd>

            </dl>
        </div>

    </div>
</asp:Content>

<asp:Content ID="CustomerFormFooter" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <asp:Button ID="SaveButton" runat="server" Text="ذخیره" OnClick="SaveButton_Click" ClientIDMode="Static" CssClass="popupMasterSaveButton" />
</asp:Content>
