<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionUserControl.ascx.cs" Inherits="CRM.Website.UserControl.ActionUserControl" %>

<link href="../Content/Screen.css" rel="stylesheet" type="text/css" media="screen" />
<link href="../Content/HandHeld.css" rel="stylesheet" type="text/css" media="handheld" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script>

    function ClientSideReset() {
        $('#' + '<%= SavePanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= SaveWatingListPanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= ConfirmPanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= PrintPanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= ForwardPanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= DenyPanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= CancelPanel.ClientID %>').css({ display: 'none' });
        $('#' + '<%= ExitPanel.ClientID %>').css({ display: 'none' });

        $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'block' });
        //$('#ActionDropDownList').selectedIndex = 0;
        document.getElementById("ActionDropDownList").selectedIndex = 0;
    }

    function DoAction() {
        var e = document.getElementById("ActionDropDownList");
        var value = e.options[e.selectedIndex].value;
        if (value == "1") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= SavePanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "3") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= PrintPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "6") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= ForwardPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "9") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= CancelPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "14") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= ExitPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "10") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= SaveWatingListPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "5") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= DenyPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "4") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= ConfirmPanel.ClientID %>').css({ display: 'block' });
        }

        else if (value == "7") {
            $('#' + '<%= ActionPanel.ClientID %>').css({ display: 'none' });
            $('#' + '<%= RefundPanel.ClientID %>').css({ display: 'block' });
        }

}
</script>

<div class="ActionUserControl">

    <!--لیست عملیات به صورت آبشاری-->
    <asp:Panel ID="ActionPanel" runat="server" ClientIDMode="Static">
        <asp:Label ID="ActionLabel" runat="server" Text="عملیات" />
        <asp:DropDownList ID="ActionDropDownList" runat="server" DataTextField="Name" DataValueField="ID" ClientIDMode="Static" onchange="DoAction();" />
    </asp:Panel>

    <!--ذخیره-->
    <asp:Panel ID="SavePanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img3" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="SaveImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Save" />
                <%--  <asp:ImageButton ID="SaveImageButtonUndo" runat="server" ImageUrl="~/Images/undo.png" OnClick="Reset" />--%>
            </dd>
            <dt>
                <label>از ذخیره اطلاعات اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

    <!--ذخیره در لیست انتظار-->
    <asp:Panel ID="SaveWatingListPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img4" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="SaveWatingListImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="SaveWaitingList" />
            </dd>
            <dt>
                <label>از ذخیره در لیست انتظار اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

    <!--تایید-->
    <asp:Panel ID="ConfirmPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img5" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="ConfirmImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Confirm" />
            </dd>
            <dt>
                <label>از تایید درخواست اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>


    <!--چاپ-->
    <asp:Panel ID="PrintPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img6" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="PrintImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Print" />
            </dd>
            <dt>
                <label>از چاپ اطلاعات اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

    <!--ارجاع-->
    <asp:Panel ID="ForwardPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img7" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="ForwardImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Forward" />
            </dd>
            <dt>
                <label>از ارجاع به مرحله بعد اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

    <!--استرداد ودیعه-->
    <asp:Panel ID="RefundPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img2" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/check2.png" OnClick="Refund" />
            </dd>
            <dt>
                <label>آیا از استرداد ودیعه اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

    <!--رد-->
    <asp:Panel ID="DenyPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img1" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="DenyImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Deny" />
            </dd>
            <dt>
                <label>از رد درخواست اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>


    <!--ابطال-->
    <asp:Panel ID="CancelPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img8" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="CancelImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Cancel" />
            </dd>
            <dt>
                <label>از کنسل کردن درخواست اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

    <!--بستن -->
    <asp:Panel ID="ExitPanel" runat="server" CssClass="hidden" ClientIDMode="Static">
        <dl>
            <dd class="buttondl">
                <img id="Img9" runat="server" src="~/Images/undo.png" onclick="ClientSideReset();" />
                <asp:ImageButton ID="ExitImageButton" runat="server" ImageUrl="~/Images/check2.png" OnClick="Exit" />
            </dd>
            <dt>
                <label>از بستن فرم اطمینان دارید؟</label></dt>
        </dl>
    </asp:Panel>

</div>

