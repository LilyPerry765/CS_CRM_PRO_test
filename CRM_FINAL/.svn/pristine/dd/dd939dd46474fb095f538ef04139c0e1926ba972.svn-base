<%@ Page Title="" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="RequestForm.aspx.cs" Inherits="CRM.Website.Viewes.RequestForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/TelephoneInformation.ascx" TagPrefix="TicketDetail" TagName="TelephoneInformation" %>
<%@ Register Src="~/UserControl/ActionUserControl.ascx" TagPrefix="Action" TagName="ActionUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script>
        function ShowQuestionMessageBox() {
            var r = confirm("آیا از حذف مطمئن هستید؟");
            $("#<%: this.QuestionResultHiddenField.ClientID %>").val(r.toString());
        }

        function changeVisibility(imagename) {
            if (imagename == 'RequestDetailImage') {

                $('#' + '<%= RequestDetailPanel.ClientID %>').toggle();
                var display = $('#' + '<%= RequestDetailPanel.ClientID %>').css('display');
                try {
                    if (display == 'none')
                        document.getElementById('<%= RequestDetailImage.ClientID %>').setAttribute('src', '../Images/expand.png');
                    else
                        document.getElementById('<%= RequestDetailImage.ClientID %>').setAttribute('src', '../Images/collapse.png');
                }
                catch (err) { }
            }
            else if (imagename == 'TelephoneInfoImage') {

                $('#' + '<%= TelephoneInfoPanel.ClientID %>').toggle();
                var display = $('#' + '<%= TelephoneInfoPanel.ClientID %>').css('display');
                try {
                    if (display == 'none')
                        document.getElementById('<%= TelephoneInfoImage.ClientID %>').setAttribute('src', '../Images/expand.png');
                    else
                        document.getElementById('<%= TelephoneInfoImage.ClientID %>').setAttribute('src', '../Images/collapse.png');
                }
                catch (err) { }
            }
            else if (imagename == 'RequestPropertiesImage') {

                $('#' + '<%= RequestPropertiesPanel.ClientID %>').toggle();
                var display = $('#' + '<%= RequestPropertiesPanel.ClientID %>').css('display');
                try {
                    if (display == 'none')
                        document.getElementById('<%= RequestPropertiesImage.ClientID %>').setAttribute('src', '../Images/expand.png');
                    else
                        document.getElementById('<%= RequestPropertiesImage.ClientID %>').setAttribute('src', '../Images/collapse.png');
                }
                catch (err) { }
            }
            else if (imagename == 'RelatedDocumentImage') {

                $('#' + '<%= RelatedDocumentPanel.ClientID %>').toggle();
                var display = $('#' + '<%= RelatedDocumentPanel.ClientID %>').css('display');
                try {
                    if (display == 'none')
                        document.getElementById('<%= RelatedDocumentImage.ClientID %>').setAttribute('src', '../Images/expand.png');
                    else
                        document.getElementById('<%= RelatedDocumentImage.ClientID %>').setAttribute('src', '../Images/collapse.png');
                }
                catch (err) { }
            }
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="RequestFormContent" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">
    <script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
    <script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
    <script src="../Scripts/filebrowser.js"></script>

    <div class="MainRequestFormContainer">
        <div id="MainRequestFormDL" runat="server" clientidmode="static">
            <asp:HiddenField runat="server" ID="QuestionResultHiddenField" ClientIDMode="Static" />
            <div id="TelephoneInfoDiv" class="MainDD">

                <asp:Panel ID="TelephoneInfoHeaderPanel" runat="server" CssClass="panelsheader">
                    <asp:Image ID="TelephoneInfoImage" runat="server" ImageUrl="~/Images/collapse.png" onclick="changeVisibility('TelephoneInfoImage');" />
                    <asp:Label ID="TelephoneInfoLabel" runat="server" Text="مشخصات تلفن" CssClass="headertext-printmode" />
                </asp:Panel>
                <asp:Panel ID="TelephoneInfoPanel" runat="server">
                    <TicketDetail:TelephoneInformation runat="server" ID="TelephoneInformationControl" />
                </asp:Panel>
            </div>

            <div id="RequestPropertiesDiv" class="MainDD">
                <asp:UpdatePanel ID="RequestPropertiesUpdatePanel" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="RequestPropertiesHeaderPanel" runat="server" CssClass="panelsheader">
                            <asp:Image ID="RequestPropertiesImage" runat="server" ImageUrl="~/Images/collapse.png" onclick="changeVisibility('RequestPropertiesImage');" />
                            <label class="headertext-printmode">مشخصات درخواست</label>
                        </asp:Panel>
                        <asp:Panel ID="RequestPropertiesPanel" runat="server" CssClass="inlineDiv">
                            <dl>
                                <dt>شهر :</dt>
                                <dd>
                                    <asp:DropDownList ID="CityDropDownList" runat="server" CssClass="requestdropdownlist" OnSelectedIndexChanged="CityDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>
                                <dt>نام درخواست کننده :</dt>
                                <dd>
                                    <asp:TextBox ID="RequestorNameTextBox" runat="server" /></dd>
                                <dt>شماره نامه درخواست :</dt>
                                <dd>
                                    <asp:TextBox ID="RequestLetterNoTextBox" runat="server" /></dd>
                            </dl>
                            <dl>
                                <dt>نام مرکز :</dt>
                                <dd>
                                    <asp:DropDownList ID="CenterNameDropDownList" runat="server" CssClass="requestdropdownlist" DataTextField="CenterName" DataValueField="ID"></asp:DropDownList>
                                </dd>
                                <dt>تاریخ درخواست :</dt>
                                <dd>
                                    <asp:TextBox ID="RequestDateTextBox" runat="server" ClientIDMode="Static" onclick="ShowDateTimePicker(this.id);" /></dd>
                                <dt>تاریخ نامه درخواست :</dt>
                                <dd>
                                    <asp:TextBox ID="RequestLetterDateTextBox" runat="server" ClientIDMode="Static" onclick="ShowDateTimePicker(this.id);" /></dd>
                            </dl>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="RequestDetailDiv" class="MainDD">
                <asp:Panel ID="RequestDetailHeaderPanel" runat="server" CssClass="panelsheader">
                    <asp:Image ID="RequestDetailImage" runat="server" ImageUrl="~/Images/collapse.png" onclick="changeVisibility('RequestDetailImage');" />
                    <asp:Label ID="RequestDetailLabel" runat="server" Text="جزئیات درخواست" CssClass="headertext-printmode" />
                </asp:Panel>
                <asp:Panel ID="RequestDetailPanel" runat="server">
                </asp:Panel>
            </div>

            <div id="RelatedDocumentDiv" class="MainDD">
                <telerik:RadAjaxLoadingPanel ID="DocumentsRadRadAjaxLoadingPanel" runat="server" Skin="" Transparency="30">
                    <div class="loading">
                        <label>...لطفا منتظر بمانید</label>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajax-loader.gif" AlternateText="...لطفا منتظر بمانید" ToolTip="...لطفا منتظر بمانید"></asp:Image>
                    </div>
                </telerik:RadAjaxLoadingPanel>

                <telerik:RadAjaxPanel ID="DocumentsRadAjaxPanel" runat="server" LoadingPanelID="DocumentsRadRadAjaxLoadingPanel" ClientEvents-OnRequestStart="">

                    <%--  <asp:UpdatePanel ID="DocumentsUpdatePanel" runat="server">
                    <ContentTemplate>--%>

                    <asp:Panel ID="RelatedDocumentHeaderPanel" runat="server" CssClass="panelsheader">
                        <asp:Image ID="RelatedDocumentImage" runat="server" ImageUrl="~/Images/collapse.png" onclick="changeVisibility('RelatedDocumentImage');" />
                        <label class="headertext-printmode">اسناد مرتبط با درخواست</label>
                    </asp:Panel>
                    <asp:Panel ID="RelatedDocumentPanel" runat="server" ClientIDMode="Static" CssClass="radtabclass inlineDiv">
                        <telerik:RadTabStrip ID="RelatedDocumentRadTabStrip" runat="server" Orientation="HorizontalTop" SelectedIndex="0" MultiPageID="RelatedDocumentRadMultiPage" Skin="Vista">
                            <Tabs>
                                <telerik:RadTab Text="دریافت /پرداخت ها" PageViewID="RequestPaymentPageView" />
                                <telerik:RadTab Text="مدارک" PageViewID="RequestDocPageView" />
                                <telerik:RadTab Text="مجوز" PageViewID="RequestPermissionPageView" />
                                <telerik:RadTab Text="قرارداد" PageViewID="RequestContractPageView" />
                                
                                <telerik:RadTab Text="سایر درخواستهای مرتبط" PageViewID="RelatedRequestsPageView" />
                                <telerik:RadTab Text="بازدید از محل" PageViewID="VisitsPageView" Visible="false" />
                                <telerik:RadTab Text="سوابق تلفن" PageViewID="PhoneRecordsInfoPageView" Visible="false" />
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage runat="server" ID="RelatedDocumentRadMultiPage" SelectedIndex="0">
                            
                            <telerik:RadPageView runat="server" ID="RequestPaymentPageView" ClientIDMode="Static">
                                <asp:GridView ID="RequestPaymentGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowCommand="RequestPaymentGridView_RowCommand"
                                    AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="عنوان">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="CostTitleDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="TitlesDataSource" SelectedValue='<%# Eval("BaseCostID") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="هزینه متفرقه">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="OtherCostTitleDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="OtherTitlesDataSource" SelectedValue='<%# Eval("OtherCostID") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:BoundField DataField="BaseCostTitle" HeaderText="عنوان" HeaderStyle-CssClass="HeaderStyle" />
                                        <asp:BoundField DataField="OtherCostTitle" HeaderText="هزینه متفرقه" HeaderStyle-CssClass="HeaderStyle" />

                                        <asp:TemplateField HeaderText="نحوه پرداخت">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="PaymentTypeDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="PaymentTypeDataSource" SelectedValue='<%# Eval("RequestPayment.PaymentType") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="AmountSum" HeaderText="مبلغ" HeaderStyle-CssClass="HeaderStyle" />
                                     <%--   <asp:BoundField DataField="BillID" HeaderText=" شناسه قبض " HeaderStyle-CssClass="HeaderStyle" />
                                        <asp:BoundField DataField="PaymentID" HeaderText=" شناسه پرداخت " HeaderStyle-CssClass="HeaderStyle" />--%>
                                        <asp:BoundField DataField="FicheNumber" HeaderText=" شماره فیش " HeaderStyle-CssClass="HeaderStyle" />
                                        <asp:CheckBoxField DataField="IsPaid" HeaderText="پرداخت شده " HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />

                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="WideColumn">
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="PaymentItemLinkButton" ID="ViewLinkButton" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="RequestPaymentEdit" AlternateText="پرداخت" ToolTip="پرداخت">
                                                   <img src="../Images/pencil_16x16.png" alt="پرداخت" title="پرداخت" />
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="PaymentItemLinkButton" ID="InstallmentPaymentLinkButton" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="InstallmentRequestPayment" AlternateText="اقساط" ToolTip="اقساط">
                                                   <img src="../Images/pencil_16x16.png" alt="اقساط" title="اقساط" />
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="KickBackPaymentLinkButton" ID="LinkButton1" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="requestPaymentKickBack" AlternateText="بازپرداخت" ToolTip="بازپرداخت">
                                                   <img src="../Images/undo_16x16.png" alt="بازپرداخت" title="بازپرداخت" />
                                                </asp:LinkButton>
                                                 <asp:LinkButton CssClass="ChangeCashInstalmentButton" ID="LinkButton2" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ChangeCashInstalment" AlternateText="تبدیل نقدی/اقساط" ToolTip="تبدیل نقدی/اقساط">
                                                   <img src="../Images/replace2_16x16.png" alt="تبدیل نقدی/اقساط" title="تبدیل نقدی/اقساط" />
                                                </asp:LinkButton>
                                                 <asp:LinkButton CssClass="DeleteLinkButton" ID="LinkButton3" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteRelatedRequestItem" AlternateText="حذف" ToolTip="حذف">
                                                   <img src="../Images/delete2_16x16.png" alt="حذف" title="حذف" />
                                                </asp:LinkButton>

                                                <%-- <asp:Button ID="EditPaymentButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="RequestPaymentEdit" runat="server" Text="پرداخت" />
                                                <asp:Button ID="InstallmentPaymentButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="InstallmentRequestPayment" runat="server" Text="اقساط" />
                                                <asp:Button ID="KickBackPaymentButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="requestPaymentKickBack" runat="server" Text="بازپرداخت" />
                                                <asp:Button ID="ChangeCashInstalmentButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ChangeCashInstalment" runat="server" Text="تبدیل نقدی/اقساط" />
                                                <asp:Button ID="DeleteButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteRelatedRequestItem" runat="server" Text="حذف" OnClientClick="ShowQuestionMessageBox();" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="RequestPaymentInsertButton" runat="server" Text="هزینه متفرقه" OnClick="RequestPaymentInsertButton_Click" CssClass="InsertDocumentToRequestButton" />
                                <%--  <asp:Button ID="RequestPaymentInsertButton" runat="server" Text="هزینه متفرقه" CssClass="InsertDocumentToRequestButton" OnClientClick="ModalDialog('/Viewes/WebForm1.aspx',null,400,300);" />--%>
                                <asp:Button ID="PrintFactorButton" runat="server" Text="صدور فاکتور" OnClick="PrintFactorButton_Click" CssClass="InsertDocumentToRequestButton" />
                                <asp:Button ID="PaidFactorButton" runat="server" Text="پرداخت فاکتور" OnClick="PaidFactorButton_Click" CssClass="InsertDocumentToRequestButton" />

                                <%-- <asp:ObjectDataSource ID="TitlesDataSource" runat="server" TypeName="CRM.Data.BaseCostDB" SelectMethod="GetBaseCostCheckable" />
                                <asp:ObjectDataSource ID="OtherTitlesDataSource" runat="server" TypeName="CRM.Data.OtherCostDB" SelectMethod="GetOtherCostCheckable" />--%>
                                <asp:ObjectDataSource ID="PaymentTypeDataSource" runat="server" TypeName="CRM.Application.Helper" SelectMethod="GetEnumCheckable" OnSelecting="PaymentTypeDataSource_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="enumType" Type="Object" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:LinkButton runat="server" ID="OtherCostDummyLink" OnClick="OtherCostDummyLink_Click" Style="display: none;" />
                                <asp:LinkButton runat="server" ID="PaymentDummyLink" OnClick="PaymentDummyLink_Click" Style="display: none;" />
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="RequestDocPageView" ClientIDMode="Static">
                                <asp:GridView ID="RequestDocGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowCommand="RequestDocGridView_RowCommand"
                                    AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <asp:BoundField DataField="DocumentName" HeaderText=" عنوان مدرک" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:CheckBoxField DataField="IsAvailable" HeaderText="وضعیت درج برای  درخواست جاری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:CheckBoxField DataField="IsForcible" HeaderText="اجباری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="مشاهده" HeaderStyle-CssClass="HeaderStyle">
                                            <ItemTemplate>
                                                <%--<asp:ImageButton ID="ViewDocumentImageButton" runat="server" ImageUrl="~/Images/DocumentPicture16x16.png" ToolTip="مشاهده" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="View" />--%>
                                                <%--<asp:Button ID="ViewButton" runat="server" Text="مشاهده" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ViewDocument" />--%>
                                                <asp:LinkButton CssClass="ServiceItemLinkButton" ID="ViewLinkButton" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ViewDocument" AlternateText="مشاهده" ToolTip="مشاهده">
                                                   <img src="../Images/DocumentPicture16x16.png" alt="مشاهده" title="مشاهده" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="ServiceItemLinkButton" ID="NewLinkButton" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="InsertDocument" AlternateText="جدید" ToolTip="جدید">
                                                   <img src="../Images/add_16x16.png" alt="جدید" title="جدید" />
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="ServiceItemLinkButton" ID="EditLinkButton" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ModifyDocument" AlternateText="ویرایش" ToolTip="ویرایش">
                                                   <img src="../Images/pencil_16x16.png" alt="ویرایش" title="ویرایش" />
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="ServiceItemLinkButton" ID="DeleteLinkButton" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteDocument" AlternateText="حذف" ToolTip="حذف">
                                                   <img src="../Images/delete2_16x16.png" alt="حذف" title="حذف" />
                                                </asp:LinkButton>

                                                <%--   <asp:Button ID="InsertButton" runat="server" Text="جدید" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="InsertDocument" />
                                                <asp:Button ID="EditDocumentButton" runat="server" Text="ویرایش" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ModifyDocument" />
                                                <asp:Button ID="DeleteButton" runat="server" Text="حذف" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteDocument" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinkButton runat="server" ID="DocumentDummyLink" OnClick="DocumentDummyLink_Click" Style="display: none;" />
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="RequestPermissionPageView">
                                <%-- <asp:GridView ID="RequestPermissionGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowCommand="RequestPermissionGridView_RowCommand"
                                    AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <asp:BoundField DataField="DocumentsByCustomer.DocumentName" HeaderText=" عنوان مجوز" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:CheckBoxField DataField="IsAvailable" HeaderText="وضعیت درج جاری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:CheckBoxField DataField="DocumentsByCustomer.IsForcible" HeaderText="اجباری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:BoundField DataField="DocumentsByCustomer.InsertDate" HeaderText=" تاریخ درج" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="EditButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edit" runat="server" Text="ویرایش" />
                                                <asp:Button ID="DeleteButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Delete" runat="server" Text="حذف" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="InsertRequestPermissioButton" runat="server" Text="جدید" OnClick="InsertRequestPermissioButton_Click" CssClass="InsertDocumentToRequestButton" />--%>
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="RequestContractPageView">
                                <%-- <asp:GridView ID="RequestContractGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowCommand="RequestContractGridView_RowCommand"
                                        AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                        <Columns>
                                            <asp:BoundField DataField="DocumentsByCustomer.DocumentName" HeaderText=" عنوان قرارداد" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:CheckBoxField DataField="IsAvailable" HeaderText="وضعیت درج برای  درخواست جاری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:CheckBoxField DataField="DocumentsByCustomer.IsForcible" HeaderText="اجباری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edit" runat="server" Text="ویرایش" />
                                                    <asp:Button ID="DeleteButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Delete" runat="server" Text="حذف" />
                                                </ItemTemplate>
                                            </asp:TemplateField>برای درخواست 
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="InsertRequestContractButton" runat="server" Text="جدید" OnClick="InsertRequestContractButton_Click" CssClass="InsertDocumentToRequestButton" />--%>
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="RelatedRequestsPageView">
                                <%-- <asp:GridView ID="RelatedRequestGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowCommand="RelatedRequestGridView_RowCommand"
                                        AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText=" عنوان درخواست" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="RequestLetterNo" HeaderText="شماره درخواست" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="RequestDate" HeaderText="تاریخ درخواست" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditButton" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edit" runat="server" Text="ویرایش" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="InsertRelatedRequestButton" runat="server" Text="جدید" OnClick="InsertRelatedRequestButton_Click" CssClass="InsertDocumentToRequestButton" />--%>
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="VisitsPageView">
                                <%--<asp:GridView ID="VisitInfoGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None"
                                        AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                        <Columns>
                                            <asp:BoundField DataField="VisitDate" HeaderText=" تاریخ بازدید" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="VisitHour" HeaderText="ساعت بازدید" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="OutBoundMeter" HeaderText="متراژ خارج از مرز" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="SixMeterMasts" HeaderText=" تیر شش متری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="EightMeterMasts" HeaderText="تیر هشت متری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="ThroughWidth" HeaderText="عرض عبوری" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="پست عبوری">
                                                <ItemTemplate>
                                                    <asp:DropDownList Enabled="false" runat="server" ID="CrossPostDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="CrossPostDataSource" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CommentStatus" HeaderText="توضیحات" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="CrossPostDataSource" runat="server" TypeName="CRM.Data.PostDB" SelectMethod="GetPostCheckable" />--%>
                            </telerik:RadPageView>

                            <telerik:RadPageView runat="server" ID="PhoneRecordsInfoPageView">
                                <%-- <asp:GridView ID="PhoneRecordsInfoGridView" ShowHeaderWhenEmpty="true" ShowHeader="true" CssClass="searchresult_gridview" runat="server" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None"
                                        AlternatingRowStyle-CssClass="AlternatingRowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" EditRowStyle-CssClass="EditRowStyle" FooterStyle-CssClass="FooterStyle" RowStyle-CssClass="RowStyle" PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" PagerSettings-Mode="NumericFirstLast">
                                        <Columns>
                                            <asp:TemplateField HeaderText="درخواست">
                                                <ItemTemplate>
                                                    <asp:DropDownList Enabled="false" runat="server" ID="RequestTypeDropDownList" DataValueField="ID" DataTextField="Name" ClientIDMode="Static" DataSourceID="PhoneRecordsRequestTypeDataSource" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VisitHour" HeaderText="تاریخ" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="OutBoundMeter" HeaderText="تلفن" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                            <asp:BoundField DataField="SixMeterMasts" HeaderText=" به تلفن" HeaderStyle-CssClass="HeaderStyle" ReadOnly="true" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="PhoneRecordsRequestTypeDataSource" runat="server" TypeName="CRM.Application.Helper" SelectMethod="GetEnumCheckable" OnSelecting="PhoneRecordsRequestTypeDataSource_Selecting">
                                        <SelectParameters>
                                            <asp:Parameter Name="enumType" Type="Object" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>--%>
                            </telerik:RadPageView>

                        </telerik:RadMultiPage>
                    </asp:Panel>

                    <%-- <asp:UpdateProgress ID="RequestDocumentsProgress" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="DocumentsUpdatePanel" ClientIDMode="Static">
                            <ProgressTemplate>
                                <span class="progressspan">
                                    <img src="../Images/ajax-loader.gif" alt="" />
                                    <label>
                                        لطفا منتظر بمانید . . .
                                    </label>
                                </span>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                </telerik:RadAjaxPanel>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <Action:ActionUserControl runat="server" ID="ActionUserControl" ClientIDMode="Static" />
</asp:Content>


