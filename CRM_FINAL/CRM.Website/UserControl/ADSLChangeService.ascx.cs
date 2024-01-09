using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.Application;
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;

namespace CRM.Website.UserControl
{
    public partial class ADSLChangeService : System.Web.UI.UserControl
    {
        #region Properties

        private long _RequestID = 0;
        private long _TelephoneNo = 0;
        private long _CustomerID = 0;
        private Data.ADSLChangeService _ADSLChangeService { get; set; }
        private Request _Request { get; set; }
        private ADSLServiceInfo _ServiceInfo { get; set; }
        private int _CustomerGroupID { get; set; }
        private int _SellerAgentID { get; set; }
        private List<int> _ServiceAccessList { get; set; }
        private List<int> _ServiceGroupAccessList { get; set; }
        private int _CenterID = 0;
        private int _CityID = 0;
        private Service1 _Service = new Service1();
        DateTime? _Now = DB.GetServerDate();

        public bool _HasCreditAgent = true;
        public bool _HasCreditUser = true;
        public ADSLService ADSLService { get; set; }
        public Data.ADSL ADSL { get; set; }
        public TelephoneDB.TeleInfo TeleInfo { get; set; }
        public Customer ADSLCustomer { get; set; }
        //public ADSLRequest _ADSLRequest { get; set; }
        public long SumPriceService = 0;

        public string ReturnedCost
        {
            get
            {
                return ReturnedCostTextbox.Text;
            }
        }

        public long RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }

        public long CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public long TelephoneNo
        {
            get { return _TelephoneNo; }
            set { _TelephoneNo = value; }
        }


        long _RefundCustomer = 0;
        double _DayCount = 0;
        double _UseDayCount = 0;
        DateTime? _DateNow = DB.GetServerDate();
        long _RefundAmount = 0;
        long _OldCost = 0;
        long _OldCostExeptTraffic = 0;
        long _TrafficCost = 0;
        double _DayCost = 0;

        #region Controls Content

        public string ServiceValue
        {
            get
            {
                return ServiceDropDownList.SelectedValue;
            }
        }

        public bool? HasNeedModem
        {
            get
            {
                return NeedModemCheckBox.Checked;
            }
        }

        public string ModemTypeValue
        {
            get
            {
                return ModemTypeDropDownList.SelectedValue;
            }
        }

        public string ModemMACAddress
        {
            get
            {
                return ModemMACAddressTextBox.Text;
            }
        }

        public string ModemSerialNo
        {
            get
            {
                return ModemSerialNoDropDownList.SelectedValue;
            }
        }

        public string ActionTypeValue
        {
            get
            {
                return ActionTypeDropDownList.SelectedValue;
            }
        }

        public string NewLicenceLetterNoValue
        {
            get
            {
                return LicenceLetterNoTextBox.Text;
            }
        }

        #endregion

        #endregion Properties

        #region Methods

        private void DisableControls()
        {
            ServiceDropDownList.Enabled = false;
        }

        public void LoadData()
        {
            if (!IsPostBack)
            {
                ADSLCustomer = new Customer();

                ActionTypeDropDownList.DataSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceActionType));
                ActionTypeDropDownList.DataBind();
                ActionTypeDropDownList.ClearSelection();

                //ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLService();
                //ServiceDropDownList.DataBind();
                //ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
                GroupDropDownList.DataBind();
                GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                TypeDropDownList.DataSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
                TypeDropDownList.DataBind();
                TypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                BandWidthDropDownList.DataSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
                BandWidthDropDownList.DataBind();
                BandWidthDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                TrafficDropDownList.DataSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
                TrafficDropDownList.DataBind();
                TrafficDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                DurationDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
                DurationDropDownList.DataBind();
                DurationDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                ModemTypeDropDownList.DataSource = ADSLModemDB.GetSalableModemsTitle();
                ModemTypeDropDownList.DataBind();
                ModemTypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }

            System.Data.DataTable telephoneInfo = _Service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

            if (telephoneInfo.Rows.Count != 0)
                _CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
            else
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone != null)
                    _CenterID = telephone.CenterID;
            }
            _CityID = CityDB.GetCityByCenterID(_CenterID).ID;
            _SellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);


            if (_RequestID == 0)
            {
                TeleInfo = Data.TelephoneDB.GetTelephoneInfoByTelephoneNo(_TelephoneNo);
                _ADSLChangeService = new Data.ADSLChangeService();
                ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);
                if (ADSL.TariffID.HasValue)
                    _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ADSL.TariffID);

                string trafficString = ADSLServiceDB.GetADSLServiceTrafficbyServiceID(_ServiceInfo.ID);

                double traffic = 0;
                if (string.IsNullOrWhiteSpace(trafficString))
                    traffic = 0;
                else
                    traffic = Convert.ToDouble(trafficString);

                string creditString = ADSLServiceDB.GetADSLServiceCreditbyServiceID(_ServiceInfo.ID);
                double credit = 0;
                if (string.IsNullOrWhiteSpace(creditString))
                    credit = 0;
                else
                    credit = Convert.ToDouble(creditString);
                double useCredit = 0;

                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct userAuthentication = new XmlRpcStruct();
                XmlRpcStruct userInfos = new XmlRpcStruct();
                XmlRpcStruct userInfo = new XmlRpcStruct();

                userAuthentication.Clear();
                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                try
                {
                    userAuthentication.Add("normal_username", ADSL.TelephoneNo.ToString());
                    userInfos = ibsngService.GetUserInfo(userAuthentication);
                }
                catch (Exception ex)
                {

                }

                foreach (DictionaryEntry User in userInfos)
                {
                    userInfo = (XmlRpcStruct)User.Value;
                }
                try
                {
                    useCredit = (credit - Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"])) / 1000;
                }
                catch (Exception ex)
                {
                    useCredit = 0;
                }

                if (traffic <= 5)
                    _TrafficCost = Convert.ToInt64(traffic * 40000);

                if (traffic >= 6 && traffic < 10)
                    _TrafficCost = (Convert.ToInt64((traffic - 5) * 30000)) + 200000;

                if (traffic >= 10)
                    _TrafficCost = (Convert.ToInt64((traffic - 10) * 20000)) + 150000 + 200000;

                double useCreditCost = 0;

                if (useCredit < 5)
                    useCreditCost = useCredit * 40000;

                if (useCredit >= 6 && useCredit < 10)
                    useCreditCost = ((useCredit - 5) * 30000) + 200000;

                if (useCredit >= 10)
                    useCreditCost = ((useCredit - 10) * 20000) + 150000 + 200000;

                //ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLService().ToList();
                //ServiceDropDownList.DataBind();
                //ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                if (ADSL.TariffID.HasValue)
                    _CustomerGroupID = ADSLServiceDB.GetADSLServiceCustomerGroupIDbyServiceID((int)ADSL.TariffID);

                if (_SellerAgentID != 0)
                {
                    _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(_SellerAgentID);
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(_SellerAgentID);

                    ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent(_CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList).ToList();
                    ServiceDropDownList.DataBind();
                    ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                    GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(_CustomerGroupID, _ServiceGroupAccessList);
                    GroupDropDownList.DataBind();
                    GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
                else
                {
                    ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLService();
                    ServiceDropDownList.DataBind();
                    ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }

                if (ADSL.TariffID.HasValue)
                {
                    ADSLServiceInfo aDSLServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ADSL.TariffID);
                    PreviousADSLTitleTextBox.Text = aDSLServiceInfo.Title;
                    PreviousADSLBandWidthTextBox.Text = aDSLServiceInfo.BandWidth;
                    PreviousADSLTrafficTextBox.Text = aDSLServiceInfo.Traffic;
                    PreviousADSLDurationTextBox.Text = aDSLServiceInfo.Duration;
                    PreviousADSLPriceSumTextBox.Text = aDSLServiceInfo.PriceSum;
                    ModemDiscountPercentageTextBox.Text = aDSLServiceInfo.ModemDiscount.ToString();
                }

                if (!IsPostBack)
                {
                    ActionTypeDropDownList.ClearSelection();
                    ActionTypeDropDownList.Items.FindByValue(((int)DB.ADSLChangeServiceActionType.ExtensionService).ToString()).Selected = true;
                    //ActionTypeDropDownList.SelectedValue = ((int)DB.ADSLChangeServiceActionType.ExtensionService).ToString();
                    ActionTypeDropDownList_SelectedIndexChanged(null, null);

                    NeedModemCheckBox.Enabled = false;

                    ServiceDropDownList.ClearSelection();
                    if (ADSL.TariffID.HasValue)
                        ServiceDropDownList.Items.FindByValue(ADSL.TariffID.ToString()).Selected = true;
                    ServiceDropDownList_SelectedIndexChanged(null, null);
                }

                string PriceSumStr = _ServiceInfo.PriceSum.Remove(_ServiceInfo.PriceSum.Length - 5);
                _OldCost = Convert.ToInt64(PriceSumStr);
                _OldCostExeptTraffic = _OldCost - _TrafficCost;
                _DayCount = (int)_ServiceInfo.DurationID * 30;

                if (_DayCount != 0)
                {
                    _DayCost = _OldCostExeptTraffic / _DayCount;

                    if (ADSL.InstallDate != null)
                    {
                        _UseDayCount = _Now.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                        _RefundAmount = _OldCost - Convert.ToInt64((_UseDayCount * _DayCost) + useCreditCost);

                        double UsedCost = Math.Truncate(((_UseDayCount * _DayCost) + useCreditCost) * 100);
                        UsedCost = UsedCost / 100;
                        PreviousADSLUsedCostTextBox.Text = (UsedCost).ToString();

                        ReturnedCostTextbox.Text = _RefundAmount.ToString();

                        //double UsedContent = Math.Truncate(((credit - Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"])) / 1000) * 100);
                        //UsedContent = UsedContent / 100;
                        //PreviousADSLUsedContentTextBox.Text = UsedCost.ToString();

                        PreviousADSLUsedContentTextBox.Text = (credit - Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"])).ToString();
                    }
                }

                //double RemainedContent = Math.Truncate((Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"]) / 1000) * 100);
                //RemainedContent = RemainedContent / 100;
                //PreviousADSLRemainedContentTextBox.Text = RemainedContent.ToString();
                PreviousADSLRemainedContentTextBox.Text = ((XmlRpcStruct)(userInfo["basic_info"]))["credit"].ToString();

                string firstLogin = "";

                try
                {
                    firstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["first_login"]);
                }
                catch (Exception ex)
                {
                    PreviousADSLUsedDaysTextBox.Text = "اتصالی صورت نگرفته است.";
                }

                if (!string.IsNullOrWhiteSpace(firstLogin))
                {
                    DateTime firstLoginDate = Convert.ToDateTime(firstLogin);
                    PreviousADSLUsedDaysTextBox.Text = Math.Abs((DB.GetServerDate() - firstLoginDate).Days).ToString() + "روز";
                }

                if (ADSL.ModemID.HasValue)
                {
                    ADSLModemPropertyInfo modemProperty = ADSLModemPropertyDB.GetADSLModemPropertyById((int)ADSL.ModemID);

                    PreviousADSLHasModemChecktBox.Checked = true;
                    if (modemProperty != null)
                        PreviousADSLModemTypeTextBox.Text = modemProperty.ModemModel + ":" + modemProperty.SerialNo;

                    if (_ServiceInfo.ModemDiscount != null && _ServiceInfo.ModemDiscount != 0)
                    {
                        ModemCostDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + "درصد";
                        ADSLModem modem = new ADSLModem();
                        if (modemProperty.ADSLModemID.HasValue)
                            modem = ADSLModemDB.GetADSLModemById((int)modemProperty.ADSLModemID);
                        double discountCost = (long)modem.Price * (long)_ServiceInfo.ModemDiscount * 0.01;
                        _RefundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / _DayCount) * Convert.ToDouble(_DayCount - _UseDayCount));
                        PreviousADSLRemainedModemCostTextBox.Text = _RefundCustomer.ToString();
                    }
                }
                else
                {
                    PreviousADSLHasModemChecktBox.Checked = false;
                    PreviousADSLRemainedModemCostTextBox.Text = "0";
                    ModemCostDiscountTextBox.Text = "0";
                }
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequestID);
                _ADSLChangeService = ADSLChangeTariffDB.GetADSLChangeServicebyID(_Request.ID);
                ADSL = Data.ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                if (ADSL.TariffID.HasValue)
                {
                    _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ADSL.TariffID);
                    //_SellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);
                    _CustomerGroupID = ADSLServiceDB.GetADSLServiceCustomerGroupIDbyServiceID((int)ADSL.TariffID);
                }

                string trafficString = ADSLServiceDB.GetADSLServiceTrafficbyServiceID(_ServiceInfo.ID);
                double traffic = 0;
                if (string.IsNullOrWhiteSpace(trafficString))
                    traffic = 0;
                else
                    traffic = Convert.ToDouble(trafficString);
                string creditString = ADSLServiceDB.GetADSLServiceCreditbyServiceID(_ServiceInfo.ID);
                double credit = 0;
                if (string.IsNullOrWhiteSpace(creditString))
                    credit = 0;
                else
                    credit = Convert.ToDouble(creditString);

                double useCredit = 0;

                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct userAuthentication = new XmlRpcStruct();
                XmlRpcStruct userInfos = new XmlRpcStruct();
                XmlRpcStruct userInfo = new XmlRpcStruct();

                userAuthentication.Clear();
                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                try
                {
                    userAuthentication.Add("normal_username", ADSL.TelephoneNo.ToString());
                    userInfos = ibsngService.GetUserInfo(userAuthentication);
                }
                catch (Exception ex)
                {

                }

                foreach (DictionaryEntry User in userInfos)
                {
                    userInfo = (XmlRpcStruct)User.Value;
                }
                try
                {
                    useCredit = (credit - Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"])) / 1000;
                }
                catch (Exception ex)
                {
                    useCredit = 0;
                }

                if (traffic <= 5)
                    _TrafficCost = Convert.ToInt64(traffic * 40000);

                if (traffic >= 6 && traffic < 10)
                    _TrafficCost = (Convert.ToInt64((traffic - 5) * 30000)) + 200000;

                if (traffic >= 10)
                    _TrafficCost = (Convert.ToInt64((traffic - 10) * 20000)) + 150000 + 200000;

                double useCreditCost = 0;

                if (useCredit < 5)
                    useCreditCost = useCredit * 40000;

                if (useCredit >= 6 && useCredit < 10)
                    useCreditCost = ((useCredit - 5) * 30000) + 200000;

                if (useCredit >= 10)
                    useCreditCost = ((useCredit - 10) * 20000) + 150000 + 200000;

                if (!IsPostBack)
                {
                    if (_SellerAgentID == 0 && _ADSLChangeService.ChangeServiceActionType != (int)DB.ADSLChangeServiceActionType.ChangeService)
                    {
                        ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup(_CustomerGroupID);
                        ServiceDropDownList.DataBind();
                        ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupID(_CustomerGroupID);
                        GroupDropDownList.DataBind();
                        GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    }

                    else if (_SellerAgentID == 0 && _ADSLChangeService.ChangeServiceActionType == (int)DB.ADSLChangeServiceActionType.ChangeService)
                    {
                        ADSLService = ADSLServiceDB.GetADSLServiceById(ADSL.TariffID);

                        ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLExtentionServiceInfoById(ADSLService.PriceSum, _CustomerGroupID);
                        ServiceDropDownList.DataBind();
                        ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupID(_CustomerGroupID);
                        GroupDropDownList.DataBind();
                        GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        ServiceDropDownList.Enabled = false;
                        TypeDropDownList.Enabled = false;
                        GroupDropDownList.Enabled = false;
                        BandWidthDropDownList.Enabled = false;
                        TrafficDropDownList.Enabled = false;
                        DurationDropDownList.Enabled = false;
                    }

                    else
                    {
                        _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(_SellerAgentID);
                        _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(_SellerAgentID);

                        ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent(_CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList);
                        ServiceDropDownList.DataBind();
                        ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(_CustomerGroupID, _ServiceGroupAccessList);
                        GroupDropDownList.DataBind();
                        GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    }

                    ActionTypeDropDownList.ClearSelection();
                    ActionTypeDropDownList.Items.FindByValue(_ADSLChangeService.ChangeServiceActionType.ToString()).Selected = true;
                    ActionTypeDropDownList_SelectedIndexChanged(null, null);

                    //ServiceDropDownList.SelectedValue = _ADSLChangeService.NewServiceID.ToString();
                    //ServiceDropDownList_SelectedIndexChanged(null, null);
                }

                TeleInfo = Data.TelephoneDB.GetTelephoneInfoByTelephoneNo(_TelephoneNo);
                ADSLServiceInfo aDSLServiceInfo = new ADSLServiceInfo();

                if (ADSL.TariffID.HasValue)
                    aDSLServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ADSL.TariffID);
                PreviousADSLTitleTextBox.Text = aDSLServiceInfo.Title;
                PreviousADSLBandWidthTextBox.Text = aDSLServiceInfo.BandWidth;
                PreviousADSLTrafficTextBox.Text = aDSLServiceInfo.Traffic;
                PreviousADSLDurationTextBox.Text = aDSLServiceInfo.Duration;
                PreviousADSLPriceSumTextBox.Text = aDSLServiceInfo.PriceSum;
                ModemDiscountPercentageTextBox.Text = aDSLServiceInfo.ModemDiscount.ToString();

                string PriceSumStr = _ServiceInfo.PriceSum.Remove(_ServiceInfo.PriceSum.Length - 5);
                _OldCost = Convert.ToInt64(PriceSumStr);
                _OldCostExeptTraffic = _OldCost - _TrafficCost;
                _DayCount = (int)_ServiceInfo.DurationID * 30;
                _DayCost = _OldCostExeptTraffic / _DayCount;
                _UseDayCount = _DateNow.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                _RefundAmount = _OldCost - Convert.ToInt64((_UseDayCount * _DayCost) + useCreditCost);

                double UsedCost = Math.Truncate(((_UseDayCount * _DayCost) + useCreditCost) * 100);
                UsedCost = UsedCost / 100;
                PreviousADSLUsedCostTextBox.Text = (UsedCost).ToString();

                ReturnedCostTextbox.Text = _RefundAmount.ToString();

                //double UsedContent = Math.Truncate(((credit - Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"])) / 1000) * 100);
                //UsedContent = UsedContent / 100;
                //PreviousADSLUsedContentTextBox.Text = UsedContent.ToString();
                PreviousADSLUsedContentTextBox.Text = (credit - Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"])).ToString();

                //double RemainedContent = Math.Truncate((Convert.ToDouble(((XmlRpcStruct)(userInfo["basic_info"]))["credit"]) / 1000) * 100);
                //RemainedContent = RemainedContent / 100;
                //PreviousADSLRemainedContentTextBox.Text = RemainedContent.ToString();
                PreviousADSLRemainedContentTextBox.Text = ((XmlRpcStruct)(userInfo["basic_info"]))["credit"].ToString();

                string firstLogin = "";
                try
                {
                    firstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["first_login"]);
                }
                catch (Exception ex)
                {
                    PreviousADSLUsedDaysTextBox.Text = "اتصالی صورت نگرفته است.";
                }

                if (!string.IsNullOrWhiteSpace(firstLogin))
                {
                    DateTime firstLoginDate = Convert.ToDateTime(firstLogin);
                    PreviousADSLUsedDaysTextBox.Text = Math.Abs((DB.GetServerDate() - firstLoginDate).Days).ToString() + "روز";
                }

                if (ADSL.ModemID != null)
                {
                    ADSLModem _ADSLModem = Data.ADSLModemDB.GetADSLModemById((int)ADSL.ModemID);
                    PreviousADSLHasModemChecktBox.Checked = true;
                    PreviousADSLModemTypeTextBox.Text = _ADSLModem.Title + ":" + _ADSLModem.Model;
                    ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSL.ModemID);
                    double discountCost = (long)modem.Price * (long)_ServiceInfo.ModemDiscount * 0.01;
                    _RefundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / _DayCount) * Convert.ToDouble(_DayCount - _UseDayCount));
                    PreviousADSLRemainedModemCostTextBox.Text = _RefundCustomer.ToString();
                    if (_ServiceInfo.ModemDiscount != 0)
                    {
                        ModemCostDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + "درصد";
                    }
                }
                else
                {
                    PreviousADSLHasModemChecktBox.Checked = false;
                    PreviousADSLRemainedModemCostTextBox.Text = "0";
                    ModemCostDiscountTextBox.Text = "0";
                }

                ServiceDropDownList.ClearSelection();
                if (_ADSLChangeService.NewServiceID.HasValue)
                    ServiceDropDownList.Items.FindByValue(_ADSLChangeService.NewServiceID.ToString()).Selected = true;
                ServiceDropDownList_SelectedIndexChanged(null, null);

                if (_ADSLChangeService.NeedModem == null)
                {
                    NeedModemCheckBox.Checked = false;

                    ModemTypeDT.Visible = false;
                    ModemTypeDD.Visible = false;
                    ModemCostDT.Visible = false;
                    ModemCostDD.Visible = false;
                    ModemSerialNoDT.Visible = false;
                    ModemSerialNoDD.Visible = false;
                    ModemMACAddressDT.Visible = false;
                    ModemMACAddressDD.Visible = false;
                }
                else
                {
                    NeedModemCheckBox.Checked = _ADSLChangeService.NeedModem.HasValue ? _ADSLChangeService.NeedModem.Value : false;

                    if (_ADSLChangeService.NeedModem.HasValue && (bool)_ADSLChangeService.NeedModem)
                    {
                        ModemTypeDropDownList.ClearSelection();
                        if (_ADSLChangeService.ModemID.HasValue)
                            ModemTypeDropDownList.Items.FindByValue(_ADSLChangeService.ModemID.ToString()).Selected = true;
                        ModemTypeDropDownList_SelectedIndexChanged(null, null);

                        ModemSerialNoDropDownList.ClearSelection();
                        if (_ADSLChangeService.ModemSerialNoID.HasValue)
                            ModemSerialNoDropDownList.Items.FindByValue(_ADSLChangeService.ModemSerialNoID.ToString()).Selected = true;
                        ModemMACAddressTextBox.Text = _ADSLChangeService.ModemMACAddress;

                        ModemTypeDT.Visible = true;
                        ModemTypeDD.Visible = true;
                        ModemCostDT.Visible = true;
                        ModemCostDD.Visible = true;
                        ModemSerialNoDT.Visible = true;
                        ModemSerialNoDD.Visible = true;
                        ModemMACAddressDT.Visible = true;
                        ModemMACAddressDD.Visible = true;
                    }
                    else
                    {
                        ModemTypeDropDownList.ClearSelection();
                        ModemTypeDropDownList_SelectedIndexChanged(null, null);
                        ModemSerialNoDropDownList.ClearSelection();

                        ModemCostTextBox.Text = string.Empty;
                        ModemDiscountTextBox.Text = string.Empty;
                        ModemCostDiscountTextBox.Text = string.Empty;
                        ModemMACAddressTextBox.Text = string.Empty;

                        ModemTypeDT.Visible = false;
                        ModemTypeDD.Visible = false;
                        ModemCostDT.Visible = false;
                        ModemCostDD.Visible = false;
                        ModemSerialNoDT.Visible = false;
                        ModemSerialNoDD.Visible = false;
                        ModemMACAddressDT.Visible = false;
                        ModemMACAddressDD.Visible = false;
                    }
                }
            }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //  LoadData();
        }

        private string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        protected void ActionTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeDropDownList.ClearSelection();
            GroupDropDownList.ClearSelection();
            BandWidthDropDownList.ClearSelection();
            DurationDropDownList.ClearSelection();
            TrafficDropDownList.ClearSelection();

            if (!string.IsNullOrEmpty(ActionTypeDropDownList.SelectedValue))
            {
                switch (int.Parse(ActionTypeDropDownList.SelectedValue))
                {
                    case (int)DB.ADSLChangeServiceActionType.ChangeService:

                        ADSLService = ADSLServiceDB.GetADSLServiceById(ADSL.TariffID);

                        if (ADSLService.IsInstalment == true)
                        {
                            ServiceDropDownList.Enabled = false;
                            ServiceDropDownList.SelectedValue = null;
                        }
                        else
                        {
                            if (_SellerAgentID == 0)
                            {
                                ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupwithPrice(ADSLService.PriceSum, _CustomerGroupID);
                                ServiceDropDownList.DataBind();
                                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            }
                            else
                            { 
                                ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgentwithPrice(ADSLService.PriceSum, _CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList).ToList();
                                ServiceDropDownList.DataBind();
                                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            }

                            ServiceDropDownList.ClearSelection();
                            if (ADSL.TariffID.HasValue)
                                ServiceDropDownList.SelectedValue = ADSL.TariffID.ToString();
                            ServiceDropDownList_SelectedIndexChanged(null, null);
                        }

                        if (ServiceDropDownList.SelectedValue == null)
                        {
                            BandWidthTextBox.Text = string.Empty;
                            ADSLServiceTrafficTextBox.Text = string.Empty;
                            DurationTextBox.Text = string.Empty;
                            PriceTextBox.Text = string.Empty;
                            TaxTextBox.Text = string.Empty;
                            ServiceSumPriceTextBox.Text = string.Empty;
                        }

                        TypeDropDownList.Enabled = false;
                        GroupDropDownList.Enabled = false;
                        BandWidthDropDownList.Enabled = false;
                        TrafficDropDownList.Enabled = false;
                        DurationDropDownList.Enabled = false;
                        NeedModemCheckBox.Enabled = true;

                        break;


                    case (int)DB.ADSLChangeServiceActionType.ExtensionService:

                        if (_SellerAgentID == 0)
                        {
                            ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup(_CustomerGroupID);
                            ServiceDropDownList.DataBind();
                            ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                        }
                        else
                        {
                            ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent(_CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList).ToList();
                            ServiceDropDownList.DataBind();
                            ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                            GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(_CustomerGroupID, _ServiceGroupAccessList);
                            GroupDropDownList.DataBind();
                            GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                        }

                        if (ADSL.TariffID.HasValue)
                            ServiceDropDownList.Items.FindByValue(ADSL.TariffID.ToString()).Selected = true;

                        _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById(ADSL.TariffID);

                        BandWidthTextBox.Text = _ServiceInfo.BandWidth;
                        ADSLServiceTrafficTextBox.Text = _ServiceInfo.Traffic;
                        DurationTextBox.Text = _ServiceInfo.Duration;
                        PriceTextBox.Text = _ServiceInfo.Price;
                        TaxTextBox.Text = _ServiceInfo.Tax;
                        ServiceSumPriceTextBox.Text = _ServiceInfo.PriceSum;

                        ServiceDropDownList.Enabled = true;
                        TypeDropDownList.Enabled = true;
                        GroupDropDownList.Enabled = true;
                        BandWidthDropDownList.Enabled = true;
                        TrafficDropDownList.Enabled = true;
                        DurationDropDownList.Enabled = true;
                        NeedModemCheckBox.Enabled = false;

                        break;

                    default:
                        break;
                }
            }

            ADSLServiceUpdatePanel.Update();
        }

        protected void MultipleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceDropDownList.ClearSelection();
            ServiceDropDownList.DataBind();

            BandWidthTextBox.Text = string.Empty;
            DurationTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            TaxTextBox.Text = string.Empty;
            ADSLServiceTrafficTextBox.Text = string.Empty;
            ServiceSumPriceTextBox.Text = string.Empty;

            int typeID = -1;
            if (!string.IsNullOrEmpty(TypeDropDownList.SelectedValue))
                typeID = Convert.ToInt32(TypeDropDownList.SelectedValue);

            int groupID = -1;
            if (!string.IsNullOrEmpty(GroupDropDownList.SelectedValue))
                groupID = Convert.ToInt32(GroupDropDownList.SelectedValue);

            int bandWidthID = -1;
            if (!string.IsNullOrEmpty(BandWidthDropDownList.SelectedValue))
                bandWidthID = Convert.ToInt32(BandWidthDropDownList.SelectedValue);

            int durationID = -1;
            if (!string.IsNullOrEmpty(DurationDropDownList.SelectedValue))
                durationID = Convert.ToInt32(DurationDropDownList.SelectedValue);

            int trafficID = -1;
            if (!string.IsNullOrEmpty(TrafficDropDownList.SelectedValue))
                trafficID = Convert.ToInt32(TrafficDropDownList.SelectedValue);

            if (_SellerAgentID == 0)
            {
                if (Convert.ToInt32(ActionTypeDropDownList.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                {
                    ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID);
                    ServiceDropDownList.DataBind();
                    ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
                else
                {
                    ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(_CustomerGroupID, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID);
                    ServiceDropDownList.DataBind();
                    ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }
            else
            {
                if (Convert.ToInt32(ActionTypeDropDownList.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                {
                    ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID, _ServiceAccessList, _ServiceGroupAccessList);
                    ServiceDropDownList.DataBind();
                    ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
                else
                {
                    ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(_CustomerGroupID, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID, _ServiceAccessList, _ServiceGroupAccessList);
                    ServiceDropDownList.DataBind();
                    ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }

            ADSLServiceUpdatePanel.Update();
        }

        protected void ServiceDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ServiceInfo = null;
            if (!string.IsNullOrEmpty(ServiceDropDownList.SelectedValue) && ServiceDropDownList.SelectedIndex > -1)
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById(int.Parse(ServiceDropDownList.SelectedValue));

            else if (!IsPostBack)
            {
                if (_ADSLChangeService.NewServiceID != null && _ADSLChangeService.NewServiceID != 0)
                    _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLChangeService.NewServiceID);
                else
                    if (ADSL.TariffID.HasValue && ADSL.TariffID != 0)
                        _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ADSL.TariffID);
            }

            else
            {
                BandWidthTextBox.Text = string.Empty;
                ADSLServiceTrafficTextBox.Text = string.Empty;
                DurationTextBox.Text = string.Empty;
                PriceTextBox.Text = string.Empty;
                TaxTextBox.Text = string.Empty;
                ServiceSumPriceTextBox.Text = string.Empty;
            }

            if (_ServiceInfo != null)
            {
                SumPriceService = Convert.ToInt64(_ServiceInfo.PriceSum.Split(' ')[0]);

                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                if (user != null && _ServiceInfo.IsInstalment == false)
                {
                    long creditRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditRemain <= SumPriceService)
                    {
                        //Folder.MessageBox.ShowError("اعتبار نمایندگی شما برای فروش کافی نمی باشد!");
                        BandWidthTextBox.Text = string.Empty;
                        ADSLServiceTrafficTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        PriceTextBox.Text = string.Empty;
                        TaxTextBox.Text = string.Empty;
                        ServiceSumPriceTextBox.Text = string.Empty;

                        _HasCreditAgent = false;
                        ErrorCreditLabel.Text = "اعتبار نمایندگی شما تمام شده است!";
                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= SumPriceService)
                    {
                        BandWidthTextBox.Text = string.Empty;
                        ADSLServiceTrafficTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        PriceTextBox.Text = string.Empty;
                        TaxTextBox.Text = string.Empty;
                        ServiceSumPriceTextBox.Text = string.Empty;
                        _HasCreditUser = false;

                        ErrorCreditLabel.Text = "اعتبار کاربری شما تمام شده است!";

                        return;
                    }

                }

                ErrorCreditLabel.Text = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;

                BandWidthTextBox.Text = _ServiceInfo.BandWidth;
                ADSLServiceTrafficTextBox.Text = _ServiceInfo.Traffic;
                DurationTextBox.Text = _ServiceInfo.Duration;
                PriceTextBox.Text = _ServiceInfo.Price;
                TaxTextBox.Text = _ServiceInfo.Tax;
                ServiceSumPriceTextBox.Text = _ServiceInfo.PriceSum;

                switch (_ServiceInfo.IsRequiredLicense)
                {
                    case true:
                        LicenceLetterNoDT.Visible = true;
                        LicenceLetterNoDD.Visible = true;
                        break;

                    case false:
                    case null:
                        LicenceLetterNoDT.Visible = false;
                        LicenceLetterNoDD.Visible = false;
                        break;

                    default:
                        break;
                }

                if (_ServiceInfo.IsModemMandatory)
                {
                    NeedModemCheckBox.Checked = true;
                    NeedModemCheckBox.Enabled = false;
                }

                if (_ServiceInfo.ModemDiscount == 0)
                {
                    ModemDiscountDT.Visible = false;
                    ModemDiscountDD.Visible = false;
                    ModemCostDiscountDT.Visible = false;
                    ModemCostDiscountDD.Visible = false;
                }
                else
                {
                    ModemDiscountDT.Visible = true;
                    ModemDiscountDD.Visible = true;
                    ModemCostDiscountDT.Visible = true;
                    ModemCostDiscountDD.Visible = true;
                }
            }
            ADSLServiceUpdatePanel.Update();
        }

        protected void NeedModemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NeedModemCheckBox.Checked)
            {
                ModemTypeDT.Visible = true;
                ModemTypeDD.Visible = true;
                ModemCostDT.Visible = true;
                ModemCostDD.Visible = true;
                ModemDiscountDT.Visible = true;
                ModemDiscountDD.Visible = true;
                ModemCostDiscountDT.Visible = true;
                ModemCostDiscountDD.Visible = true;
                ModemSerialNoDT.Visible = true;
                ModemSerialNoDD.Visible = true;
                ModemMACAddressDT.Visible = true;
                ModemMACAddressDD.Visible = true;

                ModemTypeDropDownList.ClearSelection();
                ModemCostTextBox.Text = string.Empty;
                ModemDiscountTextBox.Text = string.Empty;
                ModemCostDiscountTextBox.Text = string.Empty;
                ModemSerialNoDropDownList.ClearSelection();
                ModemMACAddressTextBox.Text = string.Empty;
            }
            else
            {
                ModemTypeDT.Visible = false;
                ModemTypeDD.Visible = false;
                ModemCostDT.Visible = false;
                ModemCostDD.Visible = false;
                ModemDiscountDT.Visible = false;
                ModemDiscountDD.Visible = false;
                ModemCostDiscountDT.Visible = false;
                ModemCostDiscountDD.Visible = false;
                ModemSerialNoDT.Visible = false;
                ModemSerialNoDD.Visible = false;
                ModemMACAddressDT.Visible = false;
                ModemMACAddressDD.Visible = false;

                ModemTypeDropDownList.ClearSelection();
                ModemSerialNoDropDownList.Items.Clear();
                ModemSerialNoDropDownList.ClearSelection();
                ModemCostTextBox.Text = string.Empty;
            }
        }

        protected void ModemTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ModemTypeDropDownList.SelectedValue))
            {
                ADSLModem modem = ADSLModemDB.GetADSLModemById(int.Parse(ModemTypeDropDownList.SelectedValue));

                ModemSerialNoDropDownList.DataSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);
                ModemSerialNoDropDownList.DataBind();
                ModemSerialNoDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                if (modem != null)
                {
                    ModemCostTextBox.Text = modem.Price + " ریا ل";

                    if (_ServiceInfo.ModemDiscount != 0)
                    {
                        ModemDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + " درصد";
                        ModemCostDiscountTextBox.Text = (modem.Price - (modem.Price * _ServiceInfo.ModemDiscount * 0.01)).ToString() + " ریا ل";
                    }
                }
            }
            else if (_ADSLChangeService.ModemID.HasValue)
            {
                ADSLModem modem = ADSLModemDB.GetADSLModemById((int)_ADSLChangeService.ModemID);

                ModemSerialNoDropDownList.DataSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);
                ModemSerialNoDropDownList.DataBind();
                ModemSerialNoDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                if (modem != null)
                {
                    ModemCostTextBox.Text = modem.Price + " ریا ل";
                    if (_ServiceInfo.ModemDiscount != 0)
                    {
                        ModemDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + " درصد";
                        ModemCostDiscountTextBox.Text = (modem.Price - (modem.Price * _ServiceInfo.ModemDiscount * 0.01)).ToString() + " ریا ل";
                    }
                }
            }
        }

        protected void ModemSerialNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ModemSerialNoDropDownList.SelectedValue))
            {
                ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById(int.Parse(ModemSerialNoDropDownList.SelectedValue));
                ModemMACAddressTextBox.Text = modemProperty.MACAddress;
            }
            else
                if (_ADSLChangeService.ModemSerialNoID != null)
                {
                    ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_ADSLChangeService.ModemSerialNoID);
                    ModemMACAddressTextBox.Text = modemProperty.MACAddress;
                }
        }

        #endregion Event Handlers
    }
}