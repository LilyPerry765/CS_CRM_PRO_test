<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestsInbox.ascx.cs" Inherits="CRM.Website.UserControl.RequestsInbox" %>
<script src="../Scripts/filebrowser.js"></script>
<div class="requestsinboxcontainer">
    <div class="search-container">
        <asp:ImageButton ID="ExpandImageButton" runat="server" OnClick="ExpandImageButton_Click" AlternateText="جستجو" ImageUrl="~/Images/collapse.png" />
        <asp:Panel ID="FullSearchPanel" runat="server" class="expandpanel">
            <dl>
                <dt>
                    <label>شناسه :</label></dt>
                <dd>
                    <asp:TextBox ID="IDTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>تاریخ درخواست از :</label></dt>
                <dd>
                    <asp:TextBox ID="RequestStartDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>

                <dt>
                    <label>تاریخ درخواست تا :</label></dt>
                <dd>
                    <asp:TextBox ID="RequestEndDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>

                <dt>
                    <label>تاریخ درج از :</label></dt>
                <dd>
                    <asp:TextBox ID="ModifyStartDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>

                <dt>
                    <label>تاریخ درج تا :</label></dt>
                <dd>
                    <asp:TextBox ID="ModifyEndDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>

                <dt>
                    <label>پیگیری</label></dt>
                <dd>
                    <asp:CheckBox ID="InquiryModeCheckBoxCheckBox" ClientIDMode="Static" runat="server" />
            </dl>
            <dl>
                <dt>
                    <label>شماره تلفن :</label></dt>
                <dd>
                    <asp:TextBox ID="TelephoneNoTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>نوع درخواست :</label></dt>
                <dd>
                    <asp:DropDownList ID="RequestTypeDropDownList" ClientIDMode="Static" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                <dt>
                    <label>مرکز :</label></dt>
                <dd>
                    <asp:DropDownList ID="CentersDropDownList" ClientIDMode="Static" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                <dt>
                    <label>درخواست کننده :</label></dt>
                <dd>
                    <asp:TextBox ID="RequesterNameTextBox" ClientIDMode="Static" runat="server" /></dd>
                <dt>
                    <label>شماره نامه :</label></dt>
                <dd>
                    <asp:TextBox ID="RequestLetterNoTextBox" ClientIDMode="Static" runat="server" /></dd>

            </dl>
            <dl>
                <dt>
                    <label>وضعیت :</label></dt>
                <dd>
                    <asp:DropDownList ID="StepDropDownList" ClientIDMode="Static" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                <dt>
                    <label>مشترک :</label></dt>
                <dd>
                    <asp:TextBox ID="CustomerNameTextBox" ClientIDMode="Static" runat="server" /></dd>

                <dt>
                    <label>نحوه پرداخت :</label></dt>
                <dd>
                    <asp:DropDownList ID="PaymentTypeDropDownList" ClientIDMode="Static" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                <dt>
                    <label>تاریخ نامه :</label></dt>
                <dd>
                    <asp:TextBox ID="LetterDateTextBox" ClientIDMode="Static" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>
                <dt></dt>
                <dd class="righttoleftdd">
                    <asp:Button ID="ResetButton" runat="server" Text="بازنشانی" OnClick="ResetButton_Click" CssClass="searchbutton" />
                    <asp:Button ID="SearchButton" runat="server" Text="نمایش" OnClick="SearchButton_Click" CssClass="searchbutton" />
                </dd>
            </dl>
        </asp:Panel>
    </div>
    <div class="searchresult-container">
      
        <asp:GridView ID="SearchResultGridView" DataSourceID="RequestInboxDataSource" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" AllowSorting="true" OnRowDataBound="SearchResultGridView_RowDataBound"
            AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" PageSize="20"
            SortedAscendingCellStyle-CssClass="sortedascendingcellstyle" SortedAscendingHeaderStyle-CssClass="sortedascendingheaderstyle" SortedDescendingCellStyle-CssClass="sorteddescendingcellstyle" SortedDescendingHeaderStyle-CssClass="sorteddescendingheaderstyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="شناسه" SortExpression="ID" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="TelephoneNo" HeaderText=" تلفن" SortExpression="TelephoneNo" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="RequestTypeName" HeaderText="نوع" SortExpression="RequestTypeName" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="CenterName" HeaderText="مرکز" SortExpression="CenterName" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="CustomerName" HeaderText="مشترک" SortExpression="CustomerName" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="RequesterName" HeaderText="درخواست کننده" HeaderStyle-CssClass="RequesterName" SortExpression="RequesterName" />
                <asp:BoundField DataField="InsertDate" HeaderText="تاریخ درج" SortExpression="InsertDate" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="CreatorUser" HeaderText="کاربر درج کننده" SortExpression="CreatorUser" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="ModifyDate" HeaderText="تاریخ آخرین ویرایش" SortExpression="ModifyDate" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="ModifyUser" HeaderText="کاربر ویرایش کننده" SortExpression="ModifyUser" HeaderStyle-CssClass="HeaderStyle" />
                <asp:BoundField DataField="StatusName" HeaderText="وضعیت" SortExpression="StatusName" HeaderStyle-CssClass="HeaderStyle" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="RequestInboxDataSource" runat="server" EnablePaging="true" TypeName="CRM.Data.RequestDB" SelectMethod="SearchRequests" SelectCountMethod="SearchRequestsCount" OnSelecting="RequestInboxDataSource_Selecting" SortParameterName="sortParameter" MaximumRowsParameterName="pageSize">
            <SelectParameters>
                <asp:Parameter Name="id" Type="String" />
                <asp:Parameter Name="telephoneNo" Type="String" />
                <asp:Parameter Name="requestStartDate" Type="DateTime" />
                <asp:Parameter Name="requestEndDate" Type="DateTime" />
                <asp:Parameter Name="modifyStartDate" Type="DateTime" />
                <asp:Parameter Name="modifyEndDate" Type="DateTime" />
                <asp:Parameter Name="requestTypesIDs" Type="Object" />
                <asp:Parameter Name="centerIDs" Type="Object" />
                <asp:Parameter Name="customerName" Type="String" />
                <asp:Parameter Name="requesterName" Type="String" />
                <asp:Parameter Name="paymentTypesIDs" Type="Object" />
                <asp:Parameter Name="stepIDs" Type="Object" />
                <asp:Parameter Name="requestLetterNo" Type="String" />
                <asp:Parameter Name="letterDate" Type="DateTime" />
                <asp:Parameter Name="isInquiryMode" Type="Boolean" />
                <asp:Parameter Name="isArchived" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="contentFooter_div">
         <asp:LinkButton ID="DummyLink" runat="server" OnClick="DummyLink_Click" Style="display: none;" />
    </div>
</div>
<script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
<script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
<script type="text/javascript">
    function popItUp(RequestId, rowindex) {
        try {
            window.open('RequestForm.aspx?RequestID=' + RequestId);
        }
        catch (error) {
            window.status = error.Message;
        }
    }

</script>
