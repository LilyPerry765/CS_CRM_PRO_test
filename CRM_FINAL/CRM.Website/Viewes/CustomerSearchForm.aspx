<%@ Page Title="" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="CustomerSearchForm.aspx.cs" Inherits="CRM.Website.Viewes.CustomerSearchForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SearchCustomerFormHeader" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <label>جستجوی مشترکین</label>
</asp:Content>
<asp:Content ID="SearchCustomerFormContent" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <div class="mainsearchcustomerformcontainer">
        <div>
            <label>نوع شخص :</label>
            <asp:RadioButton ID="PersonRadioButton" runat="server" Text="حقیقی" TabIndex="1" Checked="true" OnCheckedChanged="PersonRadioButton_CheckedChanged" GroupName="CustomerType" />
            <asp:RadioButton ID="CompanyRadioButton" runat="server" Text="حقوقی" TabIndex="2" OnCheckedChanged="CompanyRadioButton_CheckedChanged" GroupName="CustomerType" />
        </div>
        <div runat="server" id="PersonTypeDiv">
            <dl>
                <dt>
                    <label>نام :</label></dt>
                <dd>
                    <asp:TextBox ID="FirstNameTextBox" ClientIDMode="Static" runat="server" TabIndex="7" /></dd>


                <dt>
                    <label>کد ملی :</label></dt>
                <dd>
                    <asp:TextBox ID="NationalCodeTextBox" ClientIDMode="Static" runat="server" TabIndex="5" /></dd>


                <dt>
                    <label>نام پدر :</label></dt>
                <dd>
                    <asp:TextBox ID="FatherNameTextBox" ClientIDMode="Static" runat="server" TabIndex="9" /></dd>

                <dt>
                    <label>جنسیت :</label></dt>
                <dd>
                    <asp:RadioButton ID="FemaleRadioButton" runat="server" Text="زن" TabIndex="3" />
                    <asp:RadioButton ID="MaleRadioButton" runat="server" Text="مرد" TabIndex="4" />
                </dd>
            </dl>

            <dl>
                <dt>
                    <label>نام خانوادگی :</label></dt>
                <dd>
                    <asp:TextBox ID="LastNameTextBox" ClientIDMode="Static" runat="server" TabIndex="6" /></dd>

                <dt>
                    <label>شماره شناسنامه :</label></dt>
                <dd>
                    <asp:TextBox ID="BirthCertificateIDTextBox" ClientIDMode="Static" runat="server" TabIndex="8" /></dd>

                <dt>
                    <label>محل صدور :</label></dt>
                <dd>
                    <asp:TextBox ID="IssuePlaceTextBox" ClientIDMode="Static" runat="server" TabIndex="11" /></dd>
            </dl>
        </div>

        <div runat="server" id="CompanyTypeDiv" visible="false">
            <dl>
                <dt>
                    <label>نام :</label></dt>
                <dd>
                    <asp:TextBox ID="TitleTextBox" ClientIDMode="Static" runat="server"  /></dd>
            </dl>

            <dl>
                <dt>
                    <label>شماره ثبت :</label></dt>
                <dd>
                    <asp:TextBox ID="RecordNoTextBox" ClientIDMode="Static" runat="server"  /></dd>
            </dl>
        </div>

        <asp:Button ID="SearchButton" runat="server" Text="جستجو" OnClick="SearchButton_Click" />

        <div class="searchresult-container">
            <asp:GridView ID="PersonSearchResultGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" DataSourceID="RequestInboxDataSource" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" AllowSorting="true" OnRowDataBound="PersonSearchResultGridView_RowDataBound"
                AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle"  PagerStyle-CssClass="PagerStyle" PagerSettings-Mode="NumericFirstLast"
                SortedAscendingCellStyle-CssClass="sortedascendingcellstyle" SortedAscendingHeaderStyle-CssClass="sortedascendingheaderstyle" SortedDescendingCellStyle-CssClass="sorteddescendingcellstyle" SortedDescendingHeaderStyle-CssClass="sorteddescendingheaderstyle" HeaderStyle-CssClass="HeaderStyle" >
                <Columns>
                    <asp:BoundField DataField="CustomerID" HeaderText="شناسه" SortExpression="CustomerID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="NationalCodeOrRecordNo" HeaderText="کد ملی" SortExpression="NationalCodeOrRecordNo" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="FirstNameOrTitle" HeaderText="نام" SortExpression="FirstNameOrTitle" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="LastName" HeaderText="نام خانوادگی" SortExpression="LastName" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="FatherName" HeaderText="نام پدر" SortExpression="FatherName" HeaderStyle-CssClass="RequesterName" />
                    <asp:BoundField DataField="Gender" HeaderText="جنسیت" SortExpression="Gender" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="BirthCertificateID" HeaderText="تاریخ تولد" SortExpression="BirthCertificateID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="IssuePlace" HeaderText="محل صدور" SortExpression="IssuePlace" HeaderStyle-CssClass="HeaderStyle" />
                </Columns>
            </asp:GridView>

             <asp:GridView ID="CompanySearchResultGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" DataSourceID="RequestInboxDataSource" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" AllowSorting="true" 
                AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle"  PagerStyle-CssClass="PagerStyle" PagerSettings-Mode="NumericFirstLast"
                SortedAscendingCellStyle-CssClass="sortedascendingcellstyle" SortedAscendingHeaderStyle-CssClass="sortedascendingheaderstyle" SortedDescendingCellStyle-CssClass="sorteddescendingcellstyle" SortedDescendingHeaderStyle-CssClass="sorteddescendingheaderstyle" HeaderStyle-CssClass="HeaderStyle" Visible="false" >
                <Columns>
                    <asp:BoundField DataField="CustomerID" HeaderText="شناسه" SortExpression="CustomerID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="NationalCodeOrRecordNo" HeaderText="شماره ثبت" SortExpression="NationalCodeOrRecordNo" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="FirstNameOrTitle" HeaderText="عنوان" SortExpression="FirstNameOrTitle" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="BirthCertificateID" HeaderText="تاریخ ثبت" SortExpression="BirthCertificateID" HeaderStyle-CssClass="HeaderStyle" />
                </Columns>
            </asp:GridView>

            <asp:ObjectDataSource ID="RequestInboxDataSource" runat="server" EnablePaging="true" TypeName="CRM.Data.CustomerDB" SelectMethod="SearchCustomerInfo" SelectCountMethod="SearchCustomerInfoCount" OnSelecting="RequestInboxDataSource_Selecting" SortParameterName="sortParameter" MaximumRowsParameterName="pageSize">
                <SelectParameters>
                    <asp:Parameter Name="personType" Type="Object" />
                    <asp:Parameter Name="nationalCodeOrRecordNo" Type="String" />
                    <asp:Parameter Name="firstNameOrTitle" Type="String" />
                    <asp:Parameter Name="lastName" Type="String" />
                    <asp:Parameter Name="fatherName" Type="String" />
                    <asp:Parameter Name="gender" Type="Object" />
                    <asp:Parameter Name="birthCertificateID" Type="String" />
                    <asp:Parameter Name="birthDateOrRecord" Type="DateTime" />
                    <asp:Parameter Name="issuePlace" Type="String" />
                    <asp:Parameter Name="urgentTelNo" Type="String" />
                    <asp:Parameter Name="mobileNo" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="String" />
                    <asp:Parameter Name="pageSize" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>

    </div>
</asp:Content>
<asp:Content ID="SearchCustomerFormFooter" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
        <asp:Button ID="ConfirmButton" runat="server" Text="تأیید" OnClick="ConfirmButton_Click" ClientIDMode="Static" CssClass="popupMasterSaveButton" />
    <asp:Button ID="AddCustomerButton" runat="server" Text="افزودن مشتری جدید" OnClick="AddCustomerButton_Click" ClientIDMode="Static" CssClass="popupMasterSaveButton" />
</asp:Content>
