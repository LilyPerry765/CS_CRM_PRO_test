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
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using CRM.Data;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLTrafficSaleCustomerSeperationReportUserControl.xaml
    /// </summary>
    public partial class ADSLTrafficSaleCustomerSeperationReportUserControl : Local.ReportBase
    {
       #region properties
        #endregion

        #region Constructor

        public ADSLTrafficSaleCustomerSeperationReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
            ServicePaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            //TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleTypeDetails));
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            //ServiceGroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));

        }

        #endregion Intitializer

        #region EventHAndler

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
        }

        //void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        //{
        //    bool AllChecked = false;
        //    if (ServiceGroupComboBox.SelectedIDs.Count > 0)
        //        AllChecked = true;
        //    CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(ServiceGroupComboBox.SelectedIDs);
        //    BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(ServiceGroupComboBox.SelectedIDs, PaymentTypeCombBox.SelectedIDs);
        //    BandWidthComboBox.ItemsComboBox.DropDownClosed += new EventHandler(BandWidthItemsComboBox_DropDownClosed);
        //}

        //void BandWidthItemsComboBox_DropDownClosed(object sender, EventArgs e)
        //{
        //    bool AllChecked = false;
        //    if (BandWidthComboBox.SelectedIDs.Count > 0)
        //        AllChecked = true;
        //    DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationByBandwidtIDs(BandWidthComboBox.SelectedIDs);
        //    DurationComboBox.ItemsComboBox.DropDownClosed += new EventHandler(DurationItemsComboBox_DropDownClosed);
        //}

        //void DurationItemsComboBox_DropDownClosed(object sender, EventArgs e)
        //{
        //    bool AllChecked = false;
        //    if (DurationComboBox.SelectedIDs.Count > 0)
        //        AllChecked = true;
        //    TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationIDs(DurationComboBox.SelectedIDs);
        //    TrafficComboBox.ItemsComboBox.DropDownClosed += new EventHandler(TrafficItemsComboBox_DropDownClosed);
        //}

        //void TrafficItemsComboBox_DropDownClosed(object sender, EventArgs e)
        //{
        //    bool AllChecked = false;
        //    if (TrafficComboBox.SelectedIDs.Count > 0)
        //        AllChecked = true;
        //    ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, ServiceGroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        //}

        #endregion

        #region Methods

        public override void Search()
        {
            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLRequestTrafficSale = new List<ADSLTrafficSaleCustomerSeperationInfo>();
            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLRequestModem = new List<ADSLTrafficSaleCustomerSeperationInfo>();
            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLRequestIP = new List<ADSLTrafficSaleCustomerSeperationInfo>();
            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLChangeGroupIP = new List<ADSLTrafficSaleCustomerSeperationInfo>();
            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLChangeStaticIP = new List<ADSLTrafficSaleCustomerSeperationInfo>();
            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLChangeServiceModem = new List<ADSLTrafficSaleCustomerSeperationInfo>();

            if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
            {
                ResultADSLRequestTrafficSale = LoadADSLRequestTrafficSale();
                ResultADSLRequestModem = LoadADSLRequestModem();
                ResultADSLRequestIP = LoadADSLRequestIP();
                ResultADSLChangeGroupIP = LoadADSlChangeGroupIP();
                ResultADSLChangeStaticIP = LoadADSlChangeStaticIP();
                ResultADSLChangeServiceModem = LoadADSLChangeServiceModem();
            }

            List<ADSLTrafficSaleCustomerSeperationInfo> ResultADSLSellTraffic = LoadADSLsellTraffic();
           
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = new List<ADSLTrafficSaleCustomerSeperationInfo>();

            Result = ResultADSLRequestTrafficSale.Union(ResultADSLSellTraffic).Union(ResultADSLRequestIP).Union(ResultADSLChangeGroupIP).Union(ResultADSLChangeStaticIP)
                .Union(ResultADSLRequestModem).Union(ResultADSLChangeServiceModem).ToList();
                //.Union(ResultADSLRequestRanje).Union(ResultADSLRequestInstallment).ToList();

            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            title = "خريد ترافيک ADSL به تفکيک مشتري ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLRequestTrafficSale()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLRequestSaleTrafficCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                                      CenterComboBox.SelectedIDs,
                                                                                                                      PaymentTypeComboBox.SelectedIDs,
                                                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                                                      TrafficComboBox.SelectedIDs,
                                                                                                                      FromDate.SelectedDate,
                                                                                                                      toDate,
                                                                                                                      ServicePaymentTypeCombBox.SelectedIDs);
            return Result;
        }

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLsellTraffic()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLSellTrafficCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                                                           CenterComboBox.SelectedIDs,
                                                                                                                                           PaymentTypeComboBox.SelectedIDs,
                                                                                                                                           ServiceGroupComboBox.SelectedIDs,
                                                                                                                                           CustomerGroupComboBox.SelectedIDs,
                                                                                                                                           //BandWidthComboBox.SelectedIDs,
                                                                                                                                           //DurationComboBox.SelectedIDs,
                                                                                                                                           TrafficComboBox.SelectedIDs,
                                                                                                                                           //ServiceComboBox.SelectedIDs,
                                                                                                                                           FromDate.SelectedDate,
                                                                                                                                           toDate,
                                                                                                                                           ServicePaymentTypeCombBox.SelectedIDs,
                                                                                                                                           SaleWayComboBox.SelectedIDs);
            return Result;
        }

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLRequestIP()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLRequestIPCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                               CenterComboBox.SelectedIDs,
                                                                                                               PaymentTypeComboBox.SelectedIDs,
                                                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                                                               FromDate.SelectedDate,
                                                                                                               toDate,
                                                                                                               ServicePaymentTypeCombBox.SelectedIDs);
            return Result;
        }

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLRequestModem()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLRequestModemCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                               CenterComboBox.SelectedIDs,
                                                                                                               PaymentTypeComboBox.SelectedIDs,
                                                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                                                               FromDate.SelectedDate,
                                                                                                               toDate
                                                                                                               ,
                                                                                                               ServicePaymentTypeCombBox.SelectedIDs);
            return Result;
        }

        //public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLRequestInstallment()
        //{
        //    DateTime? toDate = null;
        //    if (ToDate.SelectedDate.HasValue)
        //    {
        //        toDate = ToDate.SelectedDate.Value.AddDays(1);
        //    }
        //    List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLRequestInstallmentCustomerSeperation(CityComboBox.SelectedIDs,
        //                                                                                                       CenterComboBox.SelectedIDs,
        //                                                                                                       PaymentTypeComboBox.SelectedIDs,
        //                                                                                                       ServiceGroupComboBox.SelectedIDs,
        //                                                                                                       CustomerGroupComboBox.SelectedIDs,
        //                                                                                                       FromDate.SelectedDate,
        //                                                                                                       toDate,
        //                                                                                                       ServicePaymentTypeCombBox.SelectedIDs);
        //    return Result;
        //}

        //public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLRequestRanje()
        //{
        //    DateTime? toDate = null;
        //    if (ToDate.SelectedDate.HasValue)
        //    {
        //        toDate = ToDate.SelectedDate.Value.AddDays(1);
        //    }
        //    List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLRequestRanjeCustomerSeperation(CityComboBox.SelectedIDs,
        //                                                                                                       CenterComboBox.SelectedIDs,
        //                                                                                                       PaymentTypeComboBox.SelectedIDs,
        //                                                                                                       ServiceGroupComboBox.SelectedIDs,
        //                                                                                                       CustomerGroupComboBox.SelectedIDs,
        //                                                                                                       FromDate.SelectedDate,
        //                                                                                                       toDate,
        //                                                                                                       ServicePaymentTypeCombBox.SelectedIDs);
        //    return Result;
        //}

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSlChangeGroupIP()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLChangeGroupIPCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                               CenterComboBox.SelectedIDs,
                                                                                                               PaymentTypeComboBox.SelectedIDs,
                                                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                                                               FromDate.SelectedDate,
                                                                                                               toDate);
            return Result;
        }

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSlChangeStaticIP()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLChangeStaticIPCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                               CenterComboBox.SelectedIDs,
                                                                                                               PaymentTypeComboBox.SelectedIDs,
                                                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                                                               FromDate.SelectedDate,
                                                                                                               toDate);
            return Result;
        }

        public List<ADSLTrafficSaleCustomerSeperationInfo> LoadADSLChangeServiceModem()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTrafficSaleCustomerSeperationInfo> Result = ReportDB.GetADSLChangeServiceModemCustomerSeperation(CityComboBox.SelectedIDs,
                                                                                                               CenterComboBox.SelectedIDs,
                                                                                                               PaymentTypeComboBox.SelectedIDs,
                                                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                                                               FromDate.SelectedDate,
                                                                                                               toDate,
                                                                                                               ServicePaymentTypeCombBox.SelectedIDs);
            return Result;
        }

        #endregion
    }
}
