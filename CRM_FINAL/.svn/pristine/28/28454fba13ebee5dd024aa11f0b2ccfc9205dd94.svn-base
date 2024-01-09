<%@ Page Title="دریافت/ پرداخت" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="RequestPaymentForm.aspx.cs" Inherits="CRM.Website.Viewes.RequestPaymentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MainRequestPaymentContent" ContentPlaceHolderID="ContentsPlaceHolder" runat="server">

    <script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
    <script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
    <script src="../Scripts/filebrowser.js"></script>
    <script>
        function OpenOtherCostForm() {
            var AddedOtherCostID = showModalDialog("/Viewes/OtherCostForm.aspx", "window", "resizable: no; help: no; status: no; scroll: no;center:yes;dialogWidth:400;dialogHeight:250;");
            if (AddedOtherCostID != null) {
                $("#<%: this.DummyHidden.ClientID %>").val(AddedOtherCostID.toString());
                __doPostBack('ctl00$ContentsPlaceHolder$AddCostDummyLink', '')
             }
         }
    </script>

    <div runat="server" id="MainRequestPaymentDiv" class="MainRequestPaymentDiv">
        <asp:LinkButton runat="server" ID="AddCostDummyLink" OnClick="AddCostDummyLink_Click" Style="display: none;" />
        <asp:HiddenField runat="server" ID="DummyHidden"/>
        <dl>
            <dt>شناسه قبض :</dt>
            <dd>
                <asp:TextBox ID="BillIDTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>شناسه پرداخت :</dt>
            <dd>
                <asp:TextBox ID="PaymentIDTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>نوع پرداخت : </dt>
            <dd>
                <asp:DropDownList ID="PaymentTypeDropDownList" runat="server" DataValueField="ID" DataTextField="Name" /></dd>

            <dt>پرداخت : </dt>
            <dd>
                <asp:RadioButton ID="BaseCostRadioButton" runat="server" GroupName="PaymentGroup" Checked="true" OnCheckedChanged="PaymentRadioButton_CheckedChanged" Text="هزینه پایه" />
                <asp:RadioButton ID="OtherCostRadioButton" runat="server" GroupName="PaymentGroup" OnCheckedChanged="PaymentRadioButton_CheckedChanged" Text="هزینه متفرقه" />
                <asp:RadioButton ID="PaymentFicheRadioButton" runat="server" GroupName="PaymentGroup" OnCheckedChanged="PaymentRadioButton_CheckedChanged" Text="فیش تقسیط" />
            </dd>

            <dt style="display: none" runat="server" id="BaseCostDT">هزینه پایه : </dt>
            <dd style="display: none" runat="server" id="BaseCostDD">
                <asp:DropDownList ID="BaseCostDropDownList" runat="server" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="BaseCostDropDownList_SelectedIndexChanged" AutoPostBack="true" /></dd>

            <dt style="display: none" runat="server" id="PaymentFicheDT">فیش تقسیط : </dt>
            <dd style="display: none" runat="server" id="PaymentFicheDD">
                <asp:DropDownList ID="PaymentFicheDropDownList" runat="server" DataValueField="LongID" DataTextField="Name" /></dd>

            <dt style="display: none" runat="server" id="OtherCostDT">هزینه : </dt>
            <dd style="display: none" runat="server" id="OtherCostDD" clientidmode="static">
                <asp:DropDownList ID="OtherCostDropDownList" ClientIDMode="Static" runat="server" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="OtherCostDropDownList_SelectedIndexChanged" AutoPostBack="true" />
                <asp:Button ID="AddCostButton" ClientIDMode="Static" runat="server" Text="اضافه" OnClick="AddCostButton_Click" />
            </dd>

            <dt>نحوه پرداخت : </dt>
            <dd>
                <asp:DropDownList ID="PaymentWayDropDownList" runat="server" DataValueField="ID" DataTextField="Name" /></dd>


            <dt>بانک : </dt>
            <dd>
                <asp:DropDownList ID="BankDropDownList" runat="server" DataValueField="ID" DataTextField="Name" /></dd>


            <dt>مبلغ : </dt>
            <dd>
                <asp:TextBox ID="AmountSumTextBox" runat="server" ReadOnly="true" OnTextChanged="AmountSumTextBox_TextChanged" /></dd>


            <dt>شماره فیش : </dt>
            <dd>
                <asp:TextBox ID="FicheNunmberTextBox" runat="server" /></dd>


            <dt>تاریخ فیش : </dt>
            <dd>
                <asp:TextBox ID="FicheDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" ClientIDMode="Static" /></dd>


            <dt>تاریخ دریافت فیش : </dt>
            <dd>
                <asp:TextBox ID="PaymentDateTextBox" runat="server" onclick="ShowDateTimePicker(this.id);" ClientIDMode="Static" /></dd>


            <dt>پرداخت شده : </dt>
            <dd>
                <asp:CheckBox ID="IsActiveCheckBox" runat="server" /></dd>

        </dl>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <asp:Button ID="SavePaymentButton" runat="server" Text="ذخیره" OnClick="SavePaymentButton_Click" />
</asp:Content>
