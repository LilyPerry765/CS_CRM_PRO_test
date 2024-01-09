<%@ Page Title="جستجوی مشترکین" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="SearchCustomer.aspx.cs" Inherits="CRM.Website.Viewes.SearchCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="SearchCustomerContent" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <div id="SearchCustomerDiv" class="MainADSLDD">
        <script>
            function OpenCustomerForm() {
                var AddedCustomerID = showModalDialog("/Viewes/CustomerForm.aspx", "window", "resizable: no; help: no; status: no; scroll: no;center:yes;dialogWidth:700;dialogHeight:270;");
                if (AddedCustomerID != null) {
                    $("#<%: this.DummyHidden.ClientID %>").val(AddedCustomerID.toString());
                     __doPostBack('ctl00$ContentsPlaceHolder$DummyLink', '')
                 }
             }
        </script>

        <asp:Panel ID="SearchCustomerPanel" runat="server">
            <div class="mainsearchcustomerdivcontainer">
                <div>
                    <label>نوع شخص :</label>
                    <asp:RadioButton ID="SearchPersonRadioButton" runat="server" Text="حقیقی" Checked="true" OnCheckedChanged="SearchPersonRadioButton_CheckedChanged" GroupName="SearchCustomerType" AutoPostBack="true" />
                    <asp:RadioButton ID="SearchCompanyRadioButton" runat="server" Text="حقوقی" OnCheckedChanged="SearchCompanyRadioButton_CheckedChanged" GroupName="SearchCustomerType" AutoPostBack="true" />
                </div>

                <div runat="server" id="SearchPersonTypeDiv">
                    <dl>
                        <dt>
                            <label>نام :</label></dt>
                        <dd>
                            <asp:TextBox ID="FirstNameTextBox" ClientIDMode="Static" runat="server" /></dd>
                        <dt>
                            <label>کد ملی :</label></dt>
                        <dd>
                            <asp:TextBox ID="NationalCodeTextBox" ClientIDMode="Static" runat="server" /></dd>

                        <dt>
                            <label>نام پدر :</label></dt>
                        <dd>
                            <asp:TextBox ID="FatherNameTextBox" ClientIDMode="Static" runat="server" /></dd>

                        <dt>
                            <label>جنسیت :</label></dt>
                        <dd>
                            <asp:RadioButton ID="FemaleRadioButton" runat="server" Text="زن" />
                            <asp:RadioButton ID="MaleRadioButton" runat="server" Checked="true" Text="مرد" />
                        </dd>
                    </dl>

                    <dl>
                        <dt>
                            <label>نام خانوادگی :</label></dt>
                        <dd>
                            <asp:TextBox ID="LastNameTextBox" ClientIDMode="Static" runat="server" /></dd>

                        <dt>
                            <label>شماره شناسنامه :</label></dt>
                        <dd>
                            <asp:TextBox ID="BirthCertificateIDTextBox" ClientIDMode="Static" runat="server" /></dd>

                        <dt>
                            <label>محل صدور :</label></dt>
                        <dd>
                            <asp:TextBox ID="IssuePlaceTextBox" ClientIDMode="Static" runat="server" /></dd>
                    </dl>
                </div>

                <div runat="server" id="SearchCompanyTypeDiv" visible="false">
                    <dl>
                        <dt>
                            <label>نام :</label></dt>
                        <dd>
                            <asp:TextBox ID="TitleTextBox" ClientIDMode="Static" runat="server" /></dd>
                    </dl>

                    <dl>
                        <dt>
                            <label>شماره ثبت :</label></dt>
                        <dd>
                            <asp:TextBox ID="RecordNoTextBox" ClientIDMode="Static" runat="server" /></dd>
                    </dl>
                </div>

                <asp:Button ID="SearchButton" ClientIDMode="Static" runat="server" Text="جستجو" OnClick="SearchButton_Click" />
            </div>
        </asp:Panel>
    </div>
    <div>
        <div class="searchresult-container">
            <asp:GridView ID="PersonSearchResultGridView" DataSourceID="CustomerInfoDataSource" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" AllowSorting="true" OnRowDataBound="PersonSearchResultGridView_RowDataBound"
                AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" PagerSettings-Mode="NumericFirstLast" PageSize="10"
                SortedAscendingCellStyle-CssClass="sortedascendingcellstyle" SortedAscendingHeaderStyle-CssClass="sortedascendingheaderstyle" SortedDescendingCellStyle-CssClass="sorteddescendingcellstyle" SortedDescendingHeaderStyle-CssClass="sorteddescendingheaderstyle" HeaderStyle-CssClass="HeaderStyle">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="CustomerID" HeaderText="شناسه" SortExpression="CustomerID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="NationalCodeOrRecordNo" HeaderText="کد ملی" SortExpression="NationalCodeOrRecordNo" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="FirstNameOrTitle" HeaderText="نام" SortExpression="FirstNameOrTitle" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="LastName" HeaderText="نام خانوادگی" SortExpression="LastName" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="FatherName" HeaderText="نام پدر" SortExpression="FatherName" HeaderStyle-CssClass="RequesterName" />
                    <asp:BoundField DataField="Gender" HeaderText="جنسیت" SortExpression="Gender" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="BirthCertificateID" HeaderText="شماره شناسنامه" SortExpression="BirthCertificateID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="IssuePlace" HeaderText="محل صدور" SortExpression="IssuePlace" HeaderStyle-CssClass="HeaderStyle" />
                </Columns>
            </asp:GridView>

            <asp:GridView ID="CompanySearchResultGridView" DataSourceID="CustomerInfoDataSource" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" AllowSorting="true" OnRowDataBound="CompanySearchResultGridView_RowDataBound"
                AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" PagerSettings-Mode="NumericFirstLast" PageSize="10"
                SortedAscendingCellStyle-CssClass="sortedascendingcellstyle" SortedAscendingHeaderStyle-CssClass="sortedascendingheaderstyle" SortedDescendingCellStyle-CssClass="sorteddescendingcellstyle" SortedDescendingHeaderStyle-CssClass="sorteddescendingheaderstyle" HeaderStyle-CssClass="HeaderStyle" Visible="false">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="CustomerID" HeaderText="شناسه" SortExpression="CustomerID" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="NationalCodeOrRecordNo" HeaderText="شماره ثبت" SortExpression="NationalCodeOrRecordNo" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="FirstNameOrTitle" HeaderText="عنوان" SortExpression="FirstNameOrTitle" HeaderStyle-CssClass="HeaderStyle" />
                    <asp:BoundField DataField="BirthCertificateID" HeaderText="تاریخ ثبت" SortExpression="BirthCertificateID" HeaderStyle-CssClass="HeaderStyle" />
                </Columns>
            </asp:GridView>

            <asp:ObjectDataSource ID="CustomerInfoDataSource" runat="server" EnablePaging="true" TypeName="CRM.Data.CustomerDB" SelectMethod="SearchCustomerInfoForWeb" SelectCountMethod="SearchCustomerInfoCount" OnSelecting="CustomerInfoDataSource_Selecting" SortParameterName="sortParameter" MaximumRowsParameterName="pageSize">
                <SelectParameters>
                    <asp:Parameter Name="personType" Type="Int32" />
                    <asp:Parameter Name="nationalCodeOrRecordNo" Type="String" />
                    <asp:Parameter Name="firstNameOrTitle" Type="String" />
                    <asp:Parameter Name="lastName" Type="String" />
                    <asp:Parameter Name="fatherName" Type="String" />
                    <asp:Parameter Name="BirthCertificateID" Type="String" />
                    <asp:Parameter Name="IssuePlace" Type="String" />
                    <asp:Parameter Name="gender" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
             <asp:HiddenField runat="server" ID="DummyHidden" Value='' />
             <asp:LinkButton ID="DummyLink" runat="server" OnClick="CustomerCreated"  style="display: none;" />
            <asp:Button ID="AddCustomerButton" runat="server" Text="افزودن مشتری جدید" OnClick="AddCustomerButton_Click" ClientIDMode="Static" CssClass="popupMasterSaveButton" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
