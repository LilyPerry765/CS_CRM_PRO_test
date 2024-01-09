<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ADSLChangeService.ascx.cs" Inherits="CRM.Website.UserControl.ADSLChangeService" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<link rel="stylesheet" type="text/css" href="/Contents/Screen.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Contents/HandHeld.css" media="handheld" />

<script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
<script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
<script src="../Scripts/filebrowser.js"></script>

<div id="MainADSLChangeServiceDiv" class="MainADSLChangeServiceDiv">

    <div id="PreviousADSLServiceDiv" class="MainADSLChangeServiceDD" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="PanelHeaderTitle" Text="مشخصات سرویس ADSL قبلی" />
        <br />
        <div id="PreviousADSLTitle" clientidmode="static">
            <label>تعرفه ADSL</label>
            <asp:TextBox ID="PreviousADSLTitleTextBox" runat="server"  ReadOnly="true" />
        </div>
        <dl>
            <dt>پهنای باند : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLBandWidthTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>مدت استفاده : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLDurationTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>تعداد روز استفاده شده : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLUsedDaysTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>حجم استفاده شده : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLUsedContentTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>هزینه مقدار مصرف شده : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLUsedCostTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>مودم : </dt>
            <dd>
                <asp:CheckBox ID="PreviousADSLHasModemChecktBox" runat="server" ReadOnly="true" Enabled="false" /></dd>

            <dt>هزینه باقی مانده مودم : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLRemainedModemCostTextBox" runat="server" ReadOnly="true" /></dd>
        </dl>

        <dl>
            <dt>حجم : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLTrafficTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>هزینه : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLPriceSumTextBox" runat="server" ReadOnly="true" /></dd>

            <dt id="LetterNoDT" runat="server" class="hidden">شماره نامه مجوز : </dt>
            <dd id="LetterNoDD" runat="server" class="hidden">
                <asp:TextBox ID="PreviousADSLLetterNoTextBox" runat="server" /></dd>

            <dt>حجم باقی مانده : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLRemainedContentTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>هزینه عودت شده : </dt>
            <dd>
                <asp:TextBox ID="ReturnedCostTextbox" runat="server" ReadOnly="true" /></dd>

            <dt>نوع مودم : </dt>
            <dd>
                <asp:TextBox ID="PreviousADSLModemTypeTextBox" runat="server" ReadOnly="true" /></dd>

            <dt>درصد تخفیف مودم : </dt>
            <dd>
                <asp:TextBox ID="ModemDiscountPercentageTextBox" runat="server" ReadOnly="true" /></dd>
        </dl>

    </div>

    <asp:UpdatePanel ID="ADSLServiceUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="ADSLServiceDiv" class="MainADSLChangeServiceDD" runat="server">
                <asp:Panel ID="ADSLServicePanel" runat="server">
                    <asp:Label ID="ADSLServiceLabel" runat="server" CssClass="PanelHeaderTitle" Text="مشخصات سرویس ADSL درخواستی" />
                    <br />
                    <div id="ActionDiv" clientidmode="static">
                        <label>نوع عملیات :</label>
                        <asp:DropDownList ID="ActionTypeDropDownList" runat="server" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ActionTypeDropDownList_SelectedIndexChanged" AutoPostBack="true" /></dd>
                    </div>
                    <br />
                    <asp:Label ID="ADSLServicePropertiesLabel" runat="server" Text="جستجوی سرویس" CssClass="PanelHeaderTitle" />
                    <dl>
                        <dt>نوع سرویس : </dt>
                        <dd>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" OnSelectedIndexChanged="MultipleDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt>گروه سرویس: </dt>
                        <dd>
                            <asp:DropDownList ID="GroupDropDownList" runat="server" OnSelectedIndexChanged="MultipleDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt>مدت استفاده: </dt>
                        <dd>
                            <asp:DropDownList ID="DurationDropDownList" runat="server" OnSelectedIndexChanged="MultipleDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>
                    </dl>
                    <dl>
                        <dt></dt>
                        <dd></dd>

                        <dt>پهنای باند : </dt>
                        <dd>
                            <asp:DropDownList ID="BandWidthDropDownList" runat="server" OnSelectedIndexChanged="MultipleDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt>ترافیک : </dt>
                        <dd>
                            <asp:DropDownList ID="TrafficDropDownList" runat="server" OnSelectedIndexChanged="MultipleDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>

                    </dl>

                    <asp:Label ID="ServiceLabel" runat="server" Text=" سرویس " CssClass="ServicePanelHeaderTitle" ClientIDMode="Static" />

                    <div id="ServiceDiv" clientidmode="static">
                        <label>سرویس ADSL  : </label>
                        <asp:DropDownList ID="ServiceDropDownList" runat="server" OnSelectedIndexChanged="ServiceDropDownList_SelectedIndexChanged" DataTextField="Title" DataValueField="ID" AutoPostBack="true" />

                    </div>

                    <dl>

                        <dt>پهنای باند : </dt>
                        <dd>
                            <asp:TextBox ID="BandWidthTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>مدت زمان استفاده :</dt>
                        <dd>
                            <asp:TextBox ID="DurationTextBox" runat="server" ReadOnly="true" /></dd>


                        <dt>مالیات : </dt>
                        <dd>
                            <asp:TextBox ID="TaxTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt class="hidden" runat="server" id="LicenceLetterNoDT">شماره نامه مجوز : </dt>
                        <dd class="hidden" runat="server" id="LicenceLetterNoDD">
                            <asp:TextBox ID="LicenceLetterNoTextBox" runat="server" /></dd>
                    </dl>
                    <dl>
                        <dt>ترافیک : </dt>
                        <dd>
                            <asp:TextBox ID="ADSLServiceTrafficTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>هزینه :</dt>
                        <dd>
                            <asp:TextBox ID="PriceTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>هزینه کل سرویس :</dt>
                        <dd>
                            <asp:TextBox ID="ServiceSumPriceTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt />
                        <dd >
                            <asp:Label ID="ErrorCreditLabel" runat="server" />
                        </dd>
                    </dl>
                </asp:Panel>
            </div>
            <asp:UpdateProgress ID="ADSLServiceUpdateProgress" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="ADSLServiceUpdatePanel">
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
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="ADSLFacilitiesUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="ADSLFacilitiesDiv" class="MainADSLChangeServiceDD" runat="server">
                <asp:Panel ID="ADSLFacilitiesPanel" runat="server">
                    <asp:Label ID="ADSLFacilitiesLabel" CssClass="PanelHeaderTitle" runat="server" Text="امکانات ADSL" />

                    <dl>
                        <dt>درخواست مودم : </dt>
                        <dd>
                            <asp:CheckBox ID="NeedModemCheckBox" runat="server" OnCheckedChanged="NeedModemCheckBox_CheckedChanged" AutoPostBack="true" /></dd>

                        <dt visible="false" runat="server" id="ModemTypeDT">نوع مودم : </dt>
                        <dd visible="false" runat="server" id="ModemTypeDD">
                            <asp:DropDownList ID="ModemTypeDropDownList" runat="server" OnSelectedIndexChanged="ModemTypeDropDownList_SelectedIndexChanged" DataTextField="Title" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt visible="false" id="ModemDiscountDT" runat="server">تخفیف مودم :</dt>
                        <dd visible="false" id="ModemDiscountDD" runat="server">
                            <asp:TextBox ID="ModemDiscountTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt visible="false" id="ModemSerialNoDT" runat="server">شماره سریال مودم : </dt>
                        <dd visible="false" id="ModemSerialNoDD" runat="server">
                            <asp:DropDownList ID="ModemSerialNoDropDownList" runat="server" OnSelectedIndexChanged="ModemSerialNoDropDownList_SelectedIndexChanged" /></dd>

                    </dl>
                    <dl>

                        <dt></dt>
                        <dd></dd>

                        <dt visible="false" runat="server" id="ModemCostDT">قیمت مودم :</dt>
                        <dd visible="false" runat="server" id="ModemCostDD">
                            <asp:TextBox ID="ModemCostTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt visible="false" id="ModemCostDiscountDT" runat="server">قیمت مودم با تخفیف : </dt>
                        <dd visible="false" id="ModemCostDiscountDD" runat="server">
                            <asp:TextBox ID="ModemCostDiscountTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt visible="false" id="ModemMACAddressDT" runat="server">آدرس فیزیکی مودم : </dt>
                        <dd visible="false" id="ModemMACAddressDD" runat="server">
                            <asp:TextBox ID="ModemMACAddressTextBox" runat="server" /></dd>

                    </dl>
                </asp:Panel>
            </div>

            <asp:UpdateProgress ID="ADSLFacilitiesUpdateProgress" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="ADSLFacilitiesUpdatePanel">
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
    </asp:UpdatePanel>

</div>
