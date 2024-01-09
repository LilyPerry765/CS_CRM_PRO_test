<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ADSL.ascx.cs" Inherits="CRM.Website.UserControl.ADSL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<link rel="stylesheet" type="text/css" href="/Contents/Screen.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Contents/HandHeld.css" media="handheld" />

<script src="../Scripts/calendar/calendar.js" type="text/javascript"></script>
<script src="../Scripts/calendar/calendar2.js" type="text/javascript"></script>
<script src="../Scripts/filebrowser.js"></script>

<div id="MainADSLDiv" class="MainADSLDiv">
    <asp:UpdatePanel ID="ADSLCustomerUpdatePanel" runat="server">
        <ContentTemplate>
            <script>
                function ShowSearchCustomer(NationalCode) {
                    var urlString = NationalCode ? "/Viewes/SearchCustomer.aspx?NationalCode=" + NationalCode : "/Viewes/SearchCustomer.aspx";
                    var SearchedCustomerID = showModalDialog(urlString, "window", "resizable: no; help: no; status: no; scroll: no;center:yes;dialogWidth:750;dialogHeight:540;");
                    //var SearchedCustomerID = showModalDialog("/Viewes/SearchCustomer.aspx", "window", "resizable: no; help: no; status: no; scroll: no;center:yes;dialogWidth:750;dialogHeight:540;");
                    if (SearchedCustomerID != null) {
                        $("#<%: this.DummyHidden.ClientID %>").val(SearchedCustomerID.toString());
                        __doPostBack('ctl00$ContentsPlaceHolder$ADSLUserControl$DummyLink', '');
                    }
                }
            </script>

            <div id="ADSLCustomerDiv" class="MainADSLDD">
                <asp:Panel ID="ADSLCustomerPanel" runat="server">
                    <asp:Label ID="ADSLCustomerLabel" runat="server" Text="مشخصات مالک ADSL" CssClass="PanelHeaderTitle" />

                    <dl>
                        <dt>وضعیت متقاضی : </dt>
                        <dd>
                            <asp:DropDownList ID="ADSLOwnerStatusDropDownList" runat="server" OnSelectedIndexChanged="ADSLOwnerStatusDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>
                        <dt>کدملی/شماره ثبت : </dt>
                        <dd>
                            <asp:HiddenField runat="server" ID="DummyHidden" Value='' />
                            <asp:TextBox ID="NationalCodeTextBox" runat="server" />
                            <asp:Button ID="SearchButton" runat="server" Text="جستجو" OnClick="SearchButton_Click" />
                            <asp:LinkButton ID="DummyLink" runat="server" OnClick="CustomerIDChanged" Style="display: none;" />
                        </dd>
                        <dt visible="false" runat="server" id="CustomerEndRentDateDT">تاریخ پایان اجاره نامه : </dt>
                        <dd visible="false" runat="server" id="CustomerEndRentDateDD">
                            <asp:TextBox ID="CustomerEndRentDate" runat="server" onclick="ShowDateTimePicker(this.id);" /></dd>
                        <dt>شماره همراه مالک ADSL :</dt>
                        <dd>
                            <asp:TextBox ID="MobileNoTextBox" runat="server" /></dd>

                        <dt>گروه مشتری :</dt>
                        <dd>
                            <asp:DropDownList ID="CustomerGroupDropDownList" runat="server" DataTextField="Title" DataValueField="ID" /></dd>
                    </dl>
                    <dl>
                        <dt></dt>
                        <dd></dd>
                        <dt>نام مشترک :</dt>
                        <dd>
                            <asp:TextBox ID="CustomerNameTextBox" runat="server" ReadOnly="true" /></dd>
                        <dt>شماره تلفن معرف :</dt>
                        <dd>
                            <asp:TextBox ID="ReagentTelephoneNoTextBox" runat="server" /></dd>
                        <dt>گروه شغلی :</dt>
                        <dd>
                            <asp:DropDownList ID="JobGroupDropDownList" runat="server" DataTextField="Title" DataValueField="ID" /></dd>

                    </dl>

                    <asp:Label Visible="false" CssClass="errorlabel" ID="PCMErrorrLabel" runat="server" Text="* توجه : این شماره روی PCM قرار دارد !" />
                    <asp:Label Visible="false" CssClass="errorlabel" ID="PortErrorLabel" runat="server" Text="* به دلیل عدم وجود پورت خالی هم اکنون امکان تخصیص ADSL وجود ندارد !" />
                    <asp:Label Visible="false" CssClass="errorlabel" ID="MDFInfoLabel" runat="server" ForeColor="Orange" />
                </asp:Panel>
            </div>
            <asp:UpdateProgress ID="ADSLCustomerUpdateProgress" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="ADSLCustomerUpdatePanel">
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="ADSLServiceUpdatePanel" runat="server" >
        <ContentTemplate>
            <div id="ADSLServiceDiv" class="MainADSLDD" runat="server">
                <asp:Panel ID="ADSLServicePanel" runat="server">
                    <asp:Label ID="ADSLServiceLabel" runat="server" CssClass="PanelHeaderTitle" Text="سرویس ADSL" />
                    <br />
                    <br />
                    <asp:Label ID="ADSLServicePropertiesLabel" runat="server" Text="جستجوی سرویس" CssClass="PanelHeaderTitle" />
                    <dl>
                        <dt>نوع سرویس : </dt>
                        <dd>
                            <%-- <asp:DropDownList ID="TypeDropDownList" runat="server" OnSelectedIndexChanged="MultipleDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>--%>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>
                        <dt>گروه سرویس: </dt>
                        <dd>
                            <asp:DropDownList ID="GroupDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                        <dt>مدت استفاده: </dt>
                        <dd>
                            <asp:DropDownList ID="DurationDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>
                    </dl>
                    <dl>


                        <dt>پهنای باند : </dt>
                        <dd>
                            <asp:DropDownList ID="BandWidthDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                        <dt>ترافیک : </dt>
                        <dd>
                            <asp:DropDownList ID="TrafficDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></dd>

                        <dt></dt>
                        <dd>
                            <asp:Button ID="SearchServiceButton" runat="server" Text="جستجو" OnClick="SearchServiceButton_Click" /></dd>

                    </dl>

                    <asp:Label ID="ServiceLabel" runat="server" Text=" سرویس " CssClass="ServicePanelHeaderTitle" ClientIDMode="Static" />
                    <dl>
                        <dt>سرویس ADSL  : </dt>
                        <dd>
                            <asp:DropDownList ID="ServiceDropDownList" runat="server" OnSelectedIndexChanged="ServiceDropDownList_SelectedIndexChanged" DataTextField="Title" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt>پهنای باند : </dt>
                        <dd>
                            <asp:TextBox ID="BandWidthTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>مدت زمان استفاده :</dt>
                        <dd>
                            <asp:TextBox ID="DurationTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>هزینه :</dt>
                        <dd>
                            <asp:TextBox ID="PriceTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>مالیات : </dt>
                        <dd>
                            <asp:TextBox ID="TaxTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt class="hidden" runat="server" id="LicenceLetterNoDT">شماره نامه مجوز : </dt>
                        <dd class="hidden" runat="server" id="LicenceLetterNoDD">
                            <asp:TextBox ID="LicenceLetterNoTextBox" runat="server" /></dd>
                    </dl>
                    <dl>
                        <dt></dt>
                        <dd></dd>

                        <dt>ترافیک : </dt>
                        <dd>
                            <asp:TextBox ID="ADSLServiceTrafficTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt />
                        <dd />

                        <dt>آبونمان :</dt>
                        <dd>
                            <asp:TextBox ID="AbonmanTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt>هزینه کل سرویس :</dt>
                        <dd>
                            <asp:TextBox ID="ServiceSumPriceTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt />
                        <dd />
                    </dl>
                </asp:Panel>
            </div>

            <div>
                <asp:Label runat="server" ID="MainADSLErrorCreditLabel" CssClass="errorlabel"></asp:Label>
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

    <asp:UpdatePanel ID="ADSLServiceAdditionalUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="ADSLServiceAdditionalDiv" class="MainADSLDD" runat="server">
                <asp:Panel ID="ADSLServiceAdditionalPanel" runat="server">
                    <asp:Label ID="ADSLServiceAdditionalLabel" runat="server" CssClass="PanelHeaderTitle" Text="ترافیک اضافی" />

                    <asp:CheckBox ID="HasAdditionalTrafficCheckBox" runat="server" Text="درخواست ترافیک اضافی" OnCheckedChanged="HasAdditionalTrafficCheckBox_CheckedChanged" AutoPostBack="true" />
                    <div id="AdditionalTrafficInfoDiv" runat="server" visible="false">
                        <dl>
                            <dt>ترافیک اضافی ADSL  : </dt>
                            <dd>
                                <asp:DropDownList ID="AdditionalServiceDropDownList" runat="server" DataTextField="Title" DataValueField="ID" OnSelectedIndexChanged="AdditionalServiceDropDownList_SelectedIndexChanged" AutoPostBack="true" /></dd>
                            <dt>ترافیک : </dt>
                            <dd>
                                <asp:TextBox ID="TrafficTextBox" runat="server" ReadOnly="true" /></dd>
                            <dt>هزینه :</dt>
                            <dd>
                                <asp:TextBox ID="AdditionalPriceTextBox" runat="server" ReadOnly="true" /></dd>

                            <dt>هزینه کل ترافیک اضافی :</dt>
                            <dd>
                                <asp:TextBox ID="PriceSumTextBox" runat="server" ReadOnly="true" /></dd>

                            <dt visible="false" runat="server" id="AdditionalLicenceLetterNoDT">شماره نامه مجوز</dt>
                            <dd visible="false" runat="server" id="AdditionalLicenceLetterNoDD">
                                <asp:TextBox ID="AdditionalLicenceLetterNoTextBox" runat="server" /></dd>

                        </dl>
                        <dl>
                            <dt></dt>
                            <dd></dd>
                            <dt>مدت زمان استفاده :</dt>
                            <dd>
                                <asp:TextBox ID="AdditionalDurationTextBox" runat="server" ReadOnly="true" /></dd>
                            <dt>مالیات : </dt>
                            <dd>
                                <asp:TextBox ID="AdditionalTaxTextBox" runat="server" ReadOnly="true" />
                            </dd>

                            <dt></dt>
                            <dd></dd>

                            <dt></dt>
                            <dd></dd>

                        </dl>
                    </div>
                </asp:Panel>
            </div>

            <div>
                <asp:Label runat="server" ID="ADSLServiceAdditionalErrorCreditLabel" CssClass="errorlabel"></asp:Label>
            </div>

            <asp:UpdateProgress ID="ADSLServiceAdditionalUpdateProgress" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="ADSLServiceAdditionalUpdatePanel">
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

    <asp:UpdatePanel ID="ADSLIPUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="ADSLIPDiv" class="MainADSLDD" runat="server">
                <asp:Panel ID="ADSLIPPanel" runat="server">
                    <asp:Label ID="ADSLIPLabel" runat="server" CssClass="PanelHeaderTitle" Text="IP استاتیک" />

                    <dl>
                        <dt>IP استاتیک</dt>
                        <dd>
                            <asp:CheckBox ID="HasIPStaticCheckBox" runat="server" OnCheckedChanged="HasIPStaticCheckBox_CheckedChanged" AutoPostBack="true" /></dd>
                        <dt>
                            <label id="SingleIPLabel" runat="server" visible="false">IP :</label>
                            <label id="GroupIPTypeLabel" runat="server" visible="false">نوع IP گروهی : </label>
                        </dt>
                        <dd>
                            <asp:TextBox ID="SingleIPTextBox" runat="server" ReadOnly="true" Visible="false" />
                            <asp:DropDownList ID="GroupIPTypeDropDownList" runat="server" Visible="false" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="GroupIPTypeDropDownList_SelectedIndexChanged" AutoPostBack="true" />
                        </dd>

                        <dt id="IPTimeDT" runat="server" visible="false">مدت زمان استفاده: 
                        </dt>
                        <dd id="IPTimeDD" runat="server" visible="false">
                            <asp:DropDownList ID="IPTimeDropDownList" runat="server" OnSelectedIndexChanged="IPTimeDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt id="IPDiscountDT" runat="server" visible="false">تخفیف IP: 
                        </dt>
                        <dd id="IPDiscountDD" runat="server" visible="false">
                            <asp:TextBox ID="IPDiscountTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt id="IPTaxDT" runat="server" visible="false">مالیات : </dt>
                        <dd id="IPTaxDD" runat="server" visible="false">
                            <asp:TextBox ID="IPTaxTextBox" runat="server" ReadOnly="true" Enabled="false" /></dd>
                    </dl>
                    <dl>
                        <dt>نوع IP  : </dt>
                        <dd>
                            <asp:DropDownList ID="IPTypeDropDownList" runat="server" OnSelectedIndexChanged="IPTypeDropDownList_SelectedIndexChanged" DataTextField="Name" DataValueField="ID" AutoPostBack="true" /></dd>
                        <dt>
                            <label id="GroupIPLabel" runat="server" visible="false">نوع IP گروهی  : </label>
                        </dt>
                        <dd>
                            <asp:TextBox ID="GroupIPTextBox" runat="server" ReadOnly="true" Visible="false" />
                            <asp:Label ID="ErrorGroupIPLabel" runat="server" Text=" بلاک IP مورد نظر موجود نمی باشد" Visible="false" />
                        </dd>

                        <dt id="IPCostDT" runat="server" visible="false">هزینه IP : </dt>
                        <dd id="IPCostDD" runat="server" visible="false">
                            <asp:TextBox ID="IPCostTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt id="IPCostDiscountDT" runat="server" visible="false">قیمت IP با تخفیف : 
                        </dt>
                        <dd id="IPCostDiscountDD" runat="server" visible="false">
                            <asp:TextBox ID="IPCostDiscountTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt id="IPSumCostDT" runat="server" visible="false">هزینه کل IP : </dt>
                        <dd id="IPSumCostDD" runat="server" visible="false">
                            <asp:TextBox ID="IPSumCostTextBox" runat="server" ReadOnly="true" Enabled="false" /></dd>

                    </dl>
                </asp:Panel>
            </div>

            <div>
                <asp:Label runat="server" ID="ADSLIPErrorCreditLabel" CssClass="errorlabel"></asp:Label>
            </div>

            <asp:UpdateProgress ID="ADSLIPUpdateProgress" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="ADSLIPUpdatePanel">
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

    <asp:UpdatePanel ID="ADSLFacilitiesUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="ADSLFacilitiesDiv" class="MainADSLDD" runat="server">
                <asp:Panel ID="ADSLFacilitiesPanel" runat="server">
                    <asp:Label ID="ADSLFacilitiesLabel" CssClass="PanelHeaderTitle" runat="server" Text="امکانات ADSL" />

                    <dl>
                        <dt style="width: 200px;">نصب و راه اندازی توسط مخابرات : </dt>
                        <dd style="width: 100px;">
                            <asp:CheckBox ID="RequiredInstalationCheckBox" runat="server" />

                        </dd>

                        <dt visible="false" runat="server" id="ModemTypeDT">نوع مودم : </dt>
                        <dd visible="false" runat="server" id="ModemTypeDD">
                            <asp:DropDownList ID="ModemTypeDropDownList" runat="server" OnSelectedIndexChanged="ModemTypeDropDownList_SelectedIndexChanged" DataTextField="Title" DataValueField="ID" AutoPostBack="true" /></dd>

                        <dt visible="false" id="ModemDiscountDT" runat="server">تخفیف مودم :</dt>
                        <dd visible="false" id="ModemDiscountDD" runat="server">
                            <asp:TextBox ID="ModemDiscountTextBox" runat="server" ReadOnly="true" /></dd>

                        <dt visible="false" id="ModemSerialNoDT" runat="server">شماره سریال مودم : </dt>
                        <dd visible="false" id="ModemSerialNoDD" runat="server">
                            <%--<asp:TextBox ID="ModemSerialNoTextBox" runat="server" />--%>
                            <asp:DropDownList ID="ModemSerialNoDropDownList" runat="server" OnSelectedIndexChanged="ModemSerialNoDropDownList_SelectedIndexChanged" DataTextField="Title" DataValueField="ID" AutoPostBack="true" />
                        </dd>

                    </dl>
                    <dl>
                        <dt>درخواست مودم : </dt>
                        <dd>
                            <asp:CheckBox ID="NeedModemCheckBox" runat="server" OnCheckedChanged="NeedModemCheckBox_CheckedChanged" AutoPostBack="true" /></dd>

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

            <div>
                <asp:Label runat="server" ID="ADSLFacilitiesErrorCreditLabel" CssClass="errorlabel"></asp:Label>
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

    <div id="CommentsDiv" class="MainADSLDD" runat="server" visible="false">
        <asp:Panel ID="CommentsPanel" runat="server">
            <asp:Label runat="server" ID="CommentCustomersLabel" CssClass="PanelHeaderTitle" Text="توضیحات فروش" />
            <asp:TextBox runat="server" ID="CommentCustomersTextBox" TextMode="MultiLine" CssClass="hightextbox" />
        </asp:Panel>
    </div>

    <div id="ADSLPortDiv" class="MainADSLDD" runat="server" visible="false">
        <asp:Panel ID="ADSLPortPanel" runat="server">
            <label class="PanelHeaderTitle">پورت انتخابی</label>
            <dl>
                <dt>نام تجهیزات : </dt>
                <dd>
                    <asp:TextBox runat="server" ID="EquipmentTextBox" ReadOnly="true" />
                </dd>

                <dt>بوخت ورودی : </dt>
                <dd>
                    <asp:TextBox runat="server" ID="InputBuchtTextBox" ReadOnly="true" />
                </dd>

            </dl>

            <dl>
                <dt>شماره پورت : </dt>
                <dd>
                    <asp:TextBox runat="server" ID="PortNoTextBox" ReadOnly="true" />
                </dd>

                <dt>بوخت خروجی : </dt>
                <dd>
                    <asp:TextBox runat="server" ID="OutputBuchtTextBox" ReadOnly="true" />
                </dd>
            </dl>

        </asp:Panel>
    </div>

</div>

