using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.UserControls
{    
    public partial class WirelessChangeService : UserControl
    {
        #region Properties

        private long _ReqID = 0;
       // private long _TelephoneNo = 0;
        private string _WirelessCode = "";
        private int _RequestTypeID = 0;
        private int _CenterID = 0;
        int _CityID = 0;

        private Data.WirelessChangeService _WirelessChangeService { get; set; }
        private Request _Request { get; set; }
        public Data.ADSL _ADSL { get; set; }        
        public Customer ADSLCustomer { get; set; }
        private ADSLServiceInfo _ServiceInfo { get; set; }
        public ADSLService _ADSLService { get; set; }

        private int customerGroupID { get; set; }

        public long _SumPriceService = 0;
        private int sellerAgentID { get; set; }
        private List<int> _ServiceAccessList { get; set; }
        private List<int> _ServiceGroupAccessList { get; set; }


        double dayCount = 0;
        double useDayCount = 0;
        DateTime? now = DB.GetServerDate();
        public long refundAmount = 0;
        public long refundCustomer = 0;
        long oldCost = 0;
        long oldCostExeptTraffic = 0;
        long trafficCost = 0;
        double dayCost = 0;

        public bool _HasCreditAgent = true;
        public bool _HasCreditUser = true;

        private Service1 service = new Service1();
        public System.Data.DataTable telephoneInfo { get; set; }

        #endregion

        #region Costructors

        public WirelessChangeService()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public WirelessChangeService(long requestID, string wirelessCode, int requestTypeID)
            : this()
        {
            _ReqID = requestID;
            //_TelephoneNo = telephoneNo;
            _WirelessCode = wirelessCode;
            _RequestTypeID = requestTypeID;

            LoadData(null, null);
        }        

        #endregion

        #region Methods

        private void Initialize()
        {
            ADSLCustomer = new Customer();
            ActionTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceActionType));
            ActionTypeComboBox.SelectedValue = null;
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
            ModemTypeComboBox.ItemsSource = ADSLModemDB.GetSalableModemsTitle();
        }

        private void DisableControls()
        {
            ServiceComboBox.IsEnabled = false;
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            //System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

            //if (telephoneInfo.Rows.Count != 0)
            //    _CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
            //else
            //{
            //    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
            //    if (telephone != null)
            //        _CenterID = telephone.CenterID;
            //}
           // _CityID = CityDB.GetCityByCenterID(_CenterID).ID;
            sellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);

            if (_ReqID == 0)
            {                
                _WirelessChangeService = new Data.WirelessChangeService();
                _ADSL = Data.ADSLDB.GetWirelessbyCode(_WirelessCode);
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);

                CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
                CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
                CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();

                if (_ADSL.TelephoneNo == 2333388205)
                {
                    TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType)).Where(t => t.ID == 1);
                    GroupComboBox.IsEnabled = false;
                    BandWidthComboBox.IsEnabled = false;
                    TrafficComboBox.IsEnabled = false;
                    DurationComboBox.IsEnabled = false;
                    ServiceComboBox.ItemsSource = null;
                }

                ibsngInputInfo.NormalUsername = _ADSL.UserName.ToString();
                ibsngUserInfo = webService.GetUserInfo(ibsngInputInfo);

                if (_ServiceInfo.PaymentTypeID == (byte)DB.ADSLPaymentType.PrePaid)
                {
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

                    try
                    {
                        useCredit = (credit - Convert.ToDouble(ibsngUserInfo.Credit)) / 1000;
                    }
                    catch (Exception ex)
                    {
                        useCredit = 0;
                    }

                    if (traffic <= 5)
                        trafficCost = Convert.ToInt64(traffic * ADSLTrafficBaseCostDB.GetTrafficCostbyID(1));

                    if (traffic >= 6 && traffic < 10)
                        trafficCost = (Convert.ToInt64((traffic - 5) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(2))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    if (traffic >= 10)
                        trafficCost = (Convert.ToInt64((traffic - 10) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(3))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(2) * 5) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    double useCreditCost = 0;

                    if (useCredit < 5)
                        useCreditCost = useCredit * ADSLTrafficBaseCostDB.GetTrafficCostbyID(1);

                    if (useCredit >= 6 && useCredit < 10)
                        useCreditCost = (Convert.ToInt64((useCredit - 5) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(2))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    if (useCredit >= 10)
                        useCreditCost = (Convert.ToInt64((useCredit - 10) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(3))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(2) * 5) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    string PriceSumStr = _ServiceInfo.PriceSum.Remove(_ServiceInfo.PriceSum.Length - 5);
                    oldCost = Convert.ToInt64(PriceSumStr);
                    oldCostExeptTraffic = oldCost - trafficCost;
                    dayCount = (int)_ServiceInfo.DurationID * 30;

                    if (dayCount != 0)
                    {
                        dayCost = oldCostExeptTraffic / dayCount;

                        if (_ADSL.InstallDate != null)
                        {
                            useDayCount = now.Value.Date.Subtract((DateTime)_ADSL.InstallDate).TotalDays;
                            refundAmount = oldCost - Convert.ToInt64((useDayCount * dayCost) + useCreditCost);

                            double UsedCost = Math.Truncate(((useDayCount * dayCost) + useCreditCost) * 100);
                            UsedCost = UsedCost / 100;
                            UsedCostTextBox.Text = (UsedCost).ToString();
                            ReturnedCostTextbox.Text = refundAmount.ToString();
                            UsedContentTextBox.Text = useCredit.ToString();
                        }
                        else
                        {
                            if (ibsngUserInfo.FirstLogin != null && !string.IsNullOrWhiteSpace(ibsngUserInfo.FirstLogin))
                            {
                                DateTime firstLoginDate = Convert.ToDateTime(ibsngUserInfo.FirstLogin);
                                useDayCount = 1;// now.Value.Date.Subtract(firstLoginDate).TotalDays;
                                refundAmount = oldCost - Convert.ToInt64((useDayCount * dayCost) + useCreditCost);

                                double UsedCost = Math.Truncate(((useDayCount * dayCost) + useCreditCost) * 100);
                                UsedCost = UsedCost / 100;
                                UsedCostTextBox.Text = (UsedCost).ToString();
                                ReturnedCostTextbox.Text = refundAmount.ToString();
                                UsedContentTextBox.Text = useCredit.ToString();
                            }
                        }
                    }
                }

                if (_ServiceInfo.PaymentTypeID == (byte)DB.ADSLPaymentType.PostPaid)
                {
                    DateTime now = DateTime.Now;

                    InstallmentRequestPayment instalment = null;// InstallmentRequestPaymentDB.GetLastPaidInstalmentbyTelephoneNo(_TelephoneNo);

                    long instalmentDayCount = 0;
                    dayCount = (int)_ServiceInfo.DurationID * 30;

                    if (instalment != null)
                        instalmentDayCount = Convert.ToInt32((now - instalment.EndDate).TotalDays);
                    else
                        if (_ADSL.InstallDate != null)
                            instalmentDayCount = Convert.ToInt32((now - Convert.ToDateTime(_ADSL.InstallDate)).TotalDays);
                        else
                            instalmentDayCount = 0;

                    string PriceSumStr = _ServiceInfo.PriceSum.Remove(_ServiceInfo.PriceSum.Length - 5);
                    oldCost = Convert.ToInt64(PriceSumStr);

                    if (instalmentDayCount != 0)
                    {
                        dayCost = oldCost / (_ServiceInfo.DurationID * 30);
                    }

                    refundAmount = Convert.ToInt64(dayCost * instalmentDayCount);
                    ReturnedCostTextbox.Text = refundAmount.ToString();
                }

                customerGroupID = ADSLServiceDB.GetADSLServiceCustomerGroupIDbyServiceID((int)_ADSL.TariffID);

                if (sellerAgentID != 0)
                {
                    _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(sellerAgentID);
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(sellerAgentID);

                    if (_ADSL.TelephoneNo != 2333388205)
                    {
                        ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent(customerGroupID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                        GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(customerGroupID, _ServiceGroupAccessList);
                    }
                }
                else
                {
                    if (_ADSL.TelephoneNo != 2333388205)
                    {
                        ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLService(true);
                    }
                }
                OldServiceInfo.DataContext = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);

                ActionTypeComboBox.SelectedValue = (int)DB.ADSLChangeServiceActionType.ExtensionService;
                ServiceComboBox.SelectedValue = _ADSL.TariffID;

                RemainedContentTextBox.Text = ibsngUserInfo.Credit;

                string firstLogin = "";
                firstLogin = ibsngUserInfo.FirstLogin;

                if (string.IsNullOrWhiteSpace(firstLogin))
                    UsedDaysTextBox.Text = "اتصالی صورت نگرفته است.";
                else
                {
                    DateTime firstLoginDate = Convert.ToDateTime(firstLogin);
                    UsedDaysTextBox.Text = (DB.GetServerDate() - firstLoginDate).Days.ToString() + "روز";
                }

                string nearestExpDateString = "";
                nearestExpDateString = ibsngUserInfo.NearestExpDate;
                if (!string.IsNullOrWhiteSpace(nearestExpDateString))
                {
                    DateTime nearestExpDate = Convert.ToDateTime(nearestExpDateString);
                    int remainDays = (nearestExpDate - (DB.GetServerDate())).Days;

                    if (remainDays > 0)
                    {
                        MessageBoxResult result = MessageBox.Show(" روز از سرویس قدیم باقیمانده است" + remainDays.ToString(), "توجه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        RemainedDaysTextBox.Text = remainDays.ToString() + " روز";
                    }
                }

                if (_ADSL.ModemID != null)
                {
                    ADSLModemPropertyInfo modemProperty = ADSLModemPropertyDB.GetADSLModemPropertyById((int)_ADSL.ModemID);

                    HasModemChecktBox.IsChecked = true;
                    if (modemProperty != null)
                        ModemTypeTextBox.Text = modemProperty.ModemModel + ":" + modemProperty.SerialNo;

                    if (_ServiceInfo.ModemDiscount != null && _ServiceInfo.ModemDiscount != 0)
                    {
                        ModemCostDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + "درصد";
                        ADSLModem modem = ADSLModemDB.GetADSLModemById((int)modemProperty.ADSLModemID);
                        double discountCost = (long)modem.Price * (long)_ServiceInfo.ModemDiscount * 0.01;
                        refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                        RemainedModemCostTextBox.Text = refundCustomer.ToString();
                    }
                }
                else
                {
                    HasModemChecktBox.IsChecked = false;
                    RemainedModemCostTextBox.Text = "0";
                    ModemCostDiscountTextBox.Text = "0";
                }

                NeedModemCheckBox.IsEnabled = false;
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_ReqID);
                _WirelessChangeService = WirelessChangeServiceDB.GetWirelessChangeServicebyID(_Request.ID);
                _ADSL = Data.ADSLDB.GetWirelessbyCode(_WirelessChangeService.WirelessCode);
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);
                customerGroupID = ADSLServiceDB.GetADSLServiceCustomerGroupIDbyServiceID((int)_ADSL.TariffID);

                CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
                CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
                CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();

                ibsngInputInfo.NormalUsername = _ADSL.UserName.ToString();
                ibsngUserInfo = webService.GetUserInfo(ibsngInputInfo);

                if (_ServiceInfo.PaymentTypeID == (byte)DB.ADSLPaymentType.PrePaid)
                {
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
                    
                    try
                    {
                        useCredit = (credit - Convert.ToDouble(ibsngUserInfo.Credit)) / 1000;
                    }
                    catch (Exception ex)
                    {
                        useCredit = 0;
                    }

                    if (traffic <= 5)
                        trafficCost = Convert.ToInt64(traffic * ADSLTrafficBaseCostDB.GetTrafficCostbyID(1));

                    if (traffic >= 6 && traffic < 10)
                        trafficCost = (Convert.ToInt64((traffic - 5) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(2))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    if (traffic >= 10)
                        trafficCost = (Convert.ToInt64((traffic - 10) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(3))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(2) * 5) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    double useCreditCost = 0;

                    if (useCredit < 5)
                        useCreditCost = useCredit * ADSLTrafficBaseCostDB.GetTrafficCostbyID(1);

                    if (useCredit >= 6 && useCredit < 10)
                        useCreditCost = (Convert.ToInt64((useCredit - 5) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(2))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);

                    if (useCredit >= 10)
                        useCreditCost = (Convert.ToInt64((useCredit - 10) * ADSLTrafficBaseCostDB.GetTrafficCostbyID(3))) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(2) * 5) + (ADSLTrafficBaseCostDB.GetTrafficCostbyID(1) * 5);


                    string PriceSumStr = _ServiceInfo.PriceSum.Remove(_ServiceInfo.PriceSum.Length - 5);
                    oldCost = Convert.ToInt64(PriceSumStr);
                    oldCostExeptTraffic = oldCost - trafficCost;
                    dayCount = (int)_ServiceInfo.DurationID * 30;

                    if (dayCount != 0)
                    {
                        dayCost = oldCostExeptTraffic / dayCount;

                        if (_ADSL.InstallDate != null)
                        {
                            useDayCount = now.Value.Date.Subtract((DateTime)_ADSL.InstallDate).TotalDays;
                            refundAmount = oldCost - Convert.ToInt64((useDayCount * dayCost) + useCreditCost);

                            double UsedCost = Math.Truncate(((useDayCount * dayCost) + useCreditCost) * 100);
                            UsedCost = UsedCost / 100;
                            UsedCostTextBox.Text = (UsedCost).ToString();
                            ReturnedCostTextbox.Text = refundAmount.ToString();
                            UsedContentTextBox.Text = useCredit.ToString();
                        }
                        else
                        {
                            if (ibsngUserInfo.FirstLogin != null && !string.IsNullOrWhiteSpace(ibsngUserInfo.FirstLogin))
                            {
                                DateTime firstLoginDate = Convert.ToDateTime(ibsngUserInfo.FirstLogin);
                                useDayCount = 1;// now.Value.Date.Subtract(firstLoginDate).TotalDays;
                                refundAmount = oldCost - Convert.ToInt64((useDayCount * dayCost) + useCreditCost);

                                double UsedCost = Math.Truncate(((useDayCount * dayCost) + useCreditCost) * 100);
                                UsedCost = UsedCost / 100;
                                UsedCostTextBox.Text = (UsedCost).ToString();
                                ReturnedCostTextbox.Text = refundAmount.ToString();
                                UsedContentTextBox.Text = useCredit.ToString();
                            }
                        }
                    }
                }

                if (_ServiceInfo.PaymentTypeID == (byte)DB.ADSLPaymentType.PostPaid)
                {
                    DateTime now = DateTime.Now;

                    InstallmentRequestPayment instalment = null;// InstallmentRequestPaymentDB.GetLastPaidInstalmentbyTelephoneNo(_TelephoneNo);

                    long instalmentDayCount = 0;
                    dayCount = (int)_ServiceInfo.DurationID * 30;

                    if (instalment != null)
                        instalmentDayCount = Convert.ToInt32((now - instalment.EndDate).TotalDays);
                    else
                        if (_ADSL.InstallDate != null)
                            instalmentDayCount = Convert.ToInt32((now - Convert.ToDateTime(_ADSL.InstallDate)).TotalDays);
                        else
                            instalmentDayCount = 0;

                    string PriceSumStr = _ServiceInfo.PriceSum.Remove(_ServiceInfo.PriceSum.Length - 5);
                    oldCost = Convert.ToInt64(PriceSumStr);

                    if (instalmentDayCount != 0)
                    {
                        dayCost = oldCost / (_ServiceInfo.DurationID * 30);
                    }

                    refundAmount = Convert.ToInt64(dayCost * instalmentDayCount);
                    ReturnedCostTextbox.Text = refundAmount.ToString();
                }

                if (sellerAgentID != 0)
                {
                    _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(sellerAgentID);
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(sellerAgentID);
                }

                switch (_WirelessChangeService.ChangeServiceActionType)
                {
                    case (int)DB.ADSLChangeServiceActionType.ChangeService:
                        _ADSLService = ADSLServiceDB.GetADSLServiceById(_ADSL.TariffID);

                        //if (_ADSLService.IsInstalment == true)
                        //{
                        //    ServiceComboBox.IsEnabled = false;
                        //    ServiceComboBox.SelectedValue = null;
                        //}
                        //else
                        //{
                        if (sellerAgentID == 0)
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupwithPrice(_ADSLService.PriceSum, customerGroupID,true);
                        else
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgentwithPrice(_ADSLService.PriceSum, customerGroupID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();

                        //}

                        if (ServiceComboBox.SelectedValue == null)
                            NewServiceInfo.DataContext = null;

                        TypeComboBox.IsEnabled = false;
                        GroupComboBox.IsEnabled = false;
                        BandWidthComboBox.IsEnabled = false;
                        TrafficComboBox.IsEnabled = false;
                        DurationComboBox.IsEnabled = false;
                        NeedModemCheckBox.IsEnabled = true;
                        break;

                    case (int)DB.ADSLChangeServiceActionType.ExtensionService:
                        if (sellerAgentID == 0)
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup(customerGroupID,true);
                        else
                        {
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent(customerGroupID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(customerGroupID, _ServiceGroupAccessList);
                        }

                        NewServiceInfo.DataContext = ADSLServiceDB.GetADSLServiceInfoById(_WirelessChangeService.NewServiceID);

                        ServiceComboBox.IsEnabled = true;
                        TypeComboBox.IsEnabled = true;
                        GroupComboBox.IsEnabled = true;
                        BandWidthComboBox.IsEnabled = true;
                        TrafficComboBox.IsEnabled = true;
                        DurationComboBox.IsEnabled = true;
                        NeedModemCheckBox.IsEnabled = false;
                        break;

                    default:
                        break;
                }

                ActionTypeComboBox.SelectedValue = _WirelessChangeService.ChangeServiceActionType;
                OldServiceInfo.DataContext = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);
                RemainedContentTextBox.Text = ibsngUserInfo.Credit;

                string firstLogin = "";
                firstLogin = ibsngUserInfo.FirstLogin;

                if (string.IsNullOrWhiteSpace(ibsngUserInfo.FirstLogin))
                    UsedDaysTextBox.Text = "اتصالی صورت نگرفته است.";
                else
                {
                    DateTime firstLoginDate = Convert.ToDateTime(firstLogin);
                    UsedDaysTextBox.Text = Math.Abs((DB.GetServerDate() - firstLoginDate).Days).ToString() + "روز";

                    if (_ServiceInfo.DurationID != null && _ServiceInfo.DurationID != 0)
                        RemainedDaysTextBox.Text = ((_ServiceInfo.DurationID * 30) - (DB.GetServerDate() - firstLoginDate).Days).ToString() + " روز";
                }

                if (_ADSL.InstallDate != null)
                    useDayCount = DB.GetServerDate().Date.Subtract((DateTime)_ADSL.InstallDate).TotalDays;

                if (_ADSL.ModemID != null)
                {
                    ADSLModemPropertyInfo modemProperty = ADSLModemPropertyDB.GetADSLModemPropertyById((int)_ADSL.ModemID);

                    HasModemChecktBox.IsChecked = true;
                    if (modemProperty != null)
                        ModemTypeTextBox.Text = modemProperty.ModemModel + ":" + modemProperty.SerialNo;

                    if (_ServiceInfo.ModemDiscount != null && _ServiceInfo.ModemDiscount != 0)
                    {
                        ModemCostDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + "درصد";
                        ADSLModem modem = ADSLModemDB.GetADSLModemById((int)modemProperty.ADSLModemID);
                        double discountCost = (long)modem.Price * (long)_ServiceInfo.ModemDiscount * 0.01;
                        refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                        RemainedModemCostTextBox.Text = refundCustomer.ToString();
                    }
                }
                else
                {
                    HasModemChecktBox.IsChecked = false;
                    RemainedModemCostTextBox.Text = "0";
                    ModemCostDiscountTextBox.Text = "0";
                }

                ServiceComboBox.SelectedValue = _WirelessChangeService.NewServiceID;

                if (_WirelessChangeService.NeedModem == null)
                {
                    NeedModemCheckBox.IsChecked = false;

                    ModemTypeLabel.Visibility = Visibility.Collapsed;
                    ModemTypeComboBox.Visibility = Visibility.Collapsed;
                    ModemCostLabel.Visibility = Visibility.Collapsed;
                    ModemCostTextBox.Visibility = Visibility.Collapsed;
                    ModemSerilaNoLabel.Visibility = Visibility.Collapsed;
                    ModemSerilaNoComboBox.Visibility = Visibility.Collapsed;
                    ModemMACAddressLabel.Visibility = Visibility.Collapsed;
                    ModemMACAddressTextBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    NeedModemCheckBox.IsChecked = _WirelessChangeService.NeedModem;

                    if ((bool)_WirelessChangeService.NeedModem)
                    {
                        ModemTypeComboBox.SelectedValue = _WirelessChangeService.ModemID;
                        if (_WirelessChangeService.ModemSerialNoID != null)
                            ModemSerilaNoComboBox.SelectedValue = (int)_WirelessChangeService.ModemSerialNoID;
                        ModemMACAddressTextBox.Text = _WirelessChangeService.ModemMACAddress;

                        ModemTypeLabel.Visibility = Visibility.Visible;
                        ModemTypeComboBox.Visibility = Visibility.Visible;
                        ModemCostLabel.Visibility = Visibility.Visible;
                        ModemCostTextBox.Visibility = Visibility.Visible;
                        ModemSerilaNoLabel.Visibility = Visibility.Visible;
                        ModemSerilaNoComboBox.Visibility = Visibility.Visible;
                        ModemMACAddressLabel.Visibility = Visibility.Visible;
                        ModemMACAddressTextBox.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ModemTypeLabel.Visibility = Visibility.Collapsed;
                        ModemTypeComboBox.Visibility = Visibility.Collapsed;
                        ModemCostLabel.Visibility = Visibility.Collapsed;
                        ModemCostTextBox.Visibility = Visibility.Collapsed;
                        ModemSerilaNoLabel.Visibility = Visibility.Collapsed;
                        ModemSerilaNoComboBox.Visibility = Visibility.Collapsed;
                        ModemMACAddressLabel.Visibility = Visibility.Collapsed;
                        ModemMACAddressTextBox.Visibility = Visibility.Collapsed;
                    }
                }

                if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID) && (_Request.PreviousAction == (byte)DB.Action.Reject))
                {
                    CommentCustomersTextBox.Text = _WirelessChangeService.CommentCustomers;
                    CommentCustomersTextBox.IsReadOnly = true;
                    CommentsGroupBox.Visibility = Visibility.Visible;
                    OMCCommentLabel.Visibility = Visibility.Collapsed;
                    OMCCommentTextBox.Visibility = Visibility.Collapsed;
                    FinalCommentLabel.Visibility = Visibility.Collapsed;
                    FinalCommentTextBox.Visibility = Visibility.Collapsed;
                }

                //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                //{
                //    DisableControls();

                //    CommentCustomersTextBox.Text = _WirelessChangeService.CommentCustomers;
                //    CommentsGroupBox.Visibility = Visibility.Visible;
                //    OMCCommentLabel.Visibility = Visibility.Collapsed;
                //    OMCCommentTextBox.Visibility = Visibility.Collapsed;
                //    FinalCommentLabel.Visibility = Visibility.Collapsed;
                //    FinalCommentTextBox.Visibility = Visibility.Collapsed;                    
                //}

                if (_WirelessChangeService.ChangeServiceActionType == (byte)DB.ADSLChangeServiceActionType.ExtensionService)
                    NeedModemCheckBox.IsEnabled = false;
            }
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ServiceInfo = new ADSLServiceInfo();

            if (ServiceComboBox.SelectedValue != null)
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ServiceComboBox.SelectedValue);
            else
            {
                if (_WirelessChangeService.NewServiceID != null && _WirelessChangeService.NewServiceID != 0)
                    _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_WirelessChangeService.NewServiceID);
                else
                    if (_ADSL.TariffID != 0)
                        _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);
            }

            if (_ServiceInfo != null)
            {
                _SumPriceService = Convert.ToInt64(_ServiceInfo.PriceSum.Split(' ')[0]);

                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                if (user != null && _ServiceInfo.IsInstalment == false)
                {
                    long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditAgentRemain <= _SumPriceService)
                    {
                        NewServiceInfo.DataContext = null;
                        _HasCreditAgent = false;

                        ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= _SumPriceService)
                    {
                        NewServiceInfo.DataContext = null;
                        _HasCreditUser = false;

                        ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                        return;
                    }
                }

                ErrorCreditLabel.Content = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;

                NewServiceInfo.DataContext = _ServiceInfo;

                switch (_ServiceInfo.IsRequiredLicense)
                {
                    case true:
                        NewLicenceLetterNoLabel.Visibility = Visibility.Visible;
                        NewLicenceLetterNoTextBox.Visibility = Visibility.Visible;
                        break;

                    case false:
                    case null:
                        NewLicenceLetterNoLabel.Visibility = Visibility.Collapsed;
                        NewLicenceLetterNoTextBox.Visibility = Visibility.Collapsed;
                        break;

                    default:
                        break;
                }

                if (_ServiceInfo.IsModemMandatory)
                {
                    NeedModemCheckBox.IsChecked = true;
                    NeedModemCheckBox.IsEnabled = false;
                }
                else
                {
                    NeedModemCheckBox.IsChecked = false;
                    NeedModemCheckBox.IsEnabled = true;
                }

                if (_ServiceInfo.ModemDiscount == 0)
                {
                    ModemDiscountLabel.Visibility = Visibility.Collapsed;
                    ModemDiscountTextBox.Visibility = Visibility.Collapsed;
                    ModemCostDiscountLabel.Visibility = Visibility.Collapsed;
                    ModemCostDiscountTextBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ModemDiscountLabel.Visibility = Visibility.Visible;
                    ModemDiscountTextBox.Visibility = Visibility.Visible;
                    ModemCostDiscountLabel.Visibility = Visibility.Visible;
                    ModemCostDiscountTextBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupComboBox.SelectedValue = null;
            BandWidthComboBox.SelectedValue = null;
            DurationComboBox.SelectedValue = null;
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            NewServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);

            if (sellerAgentID == 0)
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
            else
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BandWidthComboBox.SelectedValue = null;
            DurationComboBox.SelectedValue = null;
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            NewServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID, typeID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);

            if (sellerAgentID == 0)
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
            else
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
        }

        private void BandWidthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DurationComboBox.SelectedValue = null;
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            NewServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID, groupID, typeID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);

            if (sellerAgentID == 0)
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
            else
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();

        }

        private void DurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            NewServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID, bandWidthID, groupID, typeID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);

            if (sellerAgentID == 0)
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
            else
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
        }

        private void TrafficComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceComboBox.SelectedValue = null;
            NewServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);

            if (sellerAgentID == 0)
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds(null, typeID, groupID, bandWidthID, trafficID, durationID,true).ToList();
            else
                if (Convert.ToInt32(ActionTypeComboBox.SelectedValue) == (int)DB.ADSLChangeServiceActionType.ExtensionService)
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                else
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent(null, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
        }

        private void NeedModemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ModemTypeLabel.Visibility = Visibility.Visible;
            ModemTypeComboBox.Visibility = Visibility.Visible;
            ModemCostLabel.Visibility = Visibility.Visible;
            ModemCostTextBox.Visibility = Visibility.Visible;
            ModemDiscountLabel.Visibility = Visibility.Visible;
            ModemDiscountTextBox.Visibility = Visibility.Visible;
            ModemCostDiscountLabel.Visibility = Visibility.Visible;
            ModemCostDiscountTextBox.Visibility = Visibility.Visible;
            ModemSerilaNoLabel.Visibility = Visibility.Visible;
            ModemSerilaNoComboBox.Visibility = Visibility.Visible;
            ModemMACAddressLabel.Visibility = Visibility.Visible;
            ModemMACAddressTextBox.Visibility = Visibility.Visible;

            ModemTypeComboBox.SelectedValue = null;
            ModemCostTextBox.Text = string.Empty;
            ModemDiscountTextBox.Text = string.Empty;
            ModemCostDiscountTextBox.Text = string.Empty;
            ModemSerilaNoComboBox.SelectedValue = null;
            ModemMACAddressTextBox.Text = string.Empty;
        }

        private void NeedModemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ModemTypeLabel.Visibility = Visibility.Collapsed;
            ModemTypeComboBox.Visibility = Visibility.Collapsed;
            ModemCostLabel.Visibility = Visibility.Collapsed;
            ModemCostTextBox.Visibility = Visibility.Collapsed;
            ModemDiscountLabel.Visibility = Visibility.Collapsed;
            ModemDiscountTextBox.Visibility = Visibility.Collapsed;
            ModemCostDiscountLabel.Visibility = Visibility.Collapsed;
            ModemCostDiscountTextBox.Visibility = Visibility.Collapsed;
            ModemSerilaNoLabel.Visibility = Visibility.Collapsed;
            ModemSerilaNoComboBox.Visibility = Visibility.Collapsed;
            ModemMACAddressLabel.Visibility = Visibility.Collapsed;
            ModemMACAddressTextBox.Visibility = Visibility.Collapsed;

            ModemTypeComboBox.SelectedValue = null;
            ModemSerilaNoComboBox.ItemsSource = null;
            ModemSerilaNoComboBox.SelectedValue = null;
            ModemCostTextBox.Text = string.Empty;
        }

        private void ModemTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModemTypeComboBox.SelectedValue != null)
            {
                ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ModemTypeComboBox.SelectedValue);
                ModemSerilaNoComboBox.ItemsSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);

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
            else
            {
                ADSLModem modem = ADSLModemDB.GetADSLModemById((int)_WirelessChangeService.ModemID);
                ModemSerilaNoComboBox.ItemsSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);

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

        private void ModemSerilaNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModemSerilaNoComboBox.SelectedValue != null)
            {
                ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ModemSerilaNoComboBox.SelectedValue);
                ModemMACAddressTextBox.Text = modemProperty.MACAddress;
            }
            else
                if (_WirelessChangeService.ModemSerialNoID != null)
                {
                    ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_WirelessChangeService.ModemSerialNoID);
                    ModemMACAddressTextBox.Text = modemProperty.MACAddress;
                }
        }

        private void ActionTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActionTypeComboBox.SelectedValue != null)
            {
                switch ((int)ActionTypeComboBox.SelectedValue)
                {
                    case (int)DB.ADSLChangeServiceActionType.ChangeService:
                        _ADSLService = ADSLServiceDB.GetADSLServiceById(_ADSL.TariffID);

                        //if (_ADSLService.IsInstalment == true)
                        //{
                        //    ServiceComboBox.IsEnabled = false;
                        //    ServiceComboBox.SelectedValue = null;
                        //}
                        //else
                        //{
                        if (sellerAgentID == 0)
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupwithPrice(_ADSLService.PriceSum, customerGroupID,true);
                        else
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgentwithPrice(_ADSLService.PriceSum, customerGroupID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();

                        ServiceComboBox.SelectedValue = _ADSL.TariffID;
                        //}

                        if (ServiceComboBox.SelectedValue == null)
                            NewServiceInfo.DataContext = null;

                        TypeComboBox.IsEnabled = false;
                        GroupComboBox.IsEnabled = false;
                        BandWidthComboBox.IsEnabled = false;
                        TrafficComboBox.IsEnabled = false;
                        DurationComboBox.IsEnabled = false;
                        NeedModemCheckBox.IsEnabled = true;

                        break;

                    case (int)DB.ADSLChangeServiceActionType.ExtensionService:
                        if (sellerAgentID == 0)
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup(customerGroupID);
                        else
                        {
                            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent(customerGroupID, _ServiceAccessList, _ServiceGroupAccessList,true).ToList();
                            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent(customerGroupID, _ServiceGroupAccessList);
                        }

                        ServiceComboBox.SelectedValue = _ADSL.TariffID;
                        NewServiceInfo.DataContext = ADSLServiceDB.GetADSLServiceInfoById(_ADSL.TariffID);

                        ServiceComboBox.IsEnabled = true;
                        TypeComboBox.IsEnabled = true;
                        GroupComboBox.IsEnabled = true;
                        BandWidthComboBox.IsEnabled = true;
                        TrafficComboBox.IsEnabled = true;
                        DurationComboBox.IsEnabled = true;
                        NeedModemCheckBox.IsEnabled = false;

                        break;

                    default:
                        break;
                }
            }
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

        #endregion
    }
}
