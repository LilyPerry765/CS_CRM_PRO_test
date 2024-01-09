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
    /// Interaction logic for ADSLSellerAgnetSAleDetailsReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentSaleDetailsReportUserControl : Local.ReportBase
    {
        #region properties
        public static bool GroupBoxDr = false;
        public static bool BandWidthDR = false;
        public static bool DurationDR = false;
        public static bool TrafficDR = false;
        bool? ISAccepted;
        DateTime? toPaymentDate = null;
        #endregion

        #region Constructor

        public ADSLSellerAgentSaleDetailsReportUserControl()
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
            PaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            ServiceTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleType));
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            ServiceGroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));
            IsAcceptedComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.IsAccepted));

        }

        #endregion Intitializer

        #region EventHAndler

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
            SellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            SellerAgentComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ADSLSellerAgentItemsComboBox_DropDownClosed);
        }
        
        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (ServiceGroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            GroupBoxDr = true;
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(ServiceGroupComboBox.SelectedIDs);
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(ServiceGroupComboBox.SelectedIDs, PaymentTypeCombBox.SelectedIDs);
            BandWidthComboBox.ItemsComboBox.DropDownClosed += new EventHandler(BandWidthItemsComboBox_DropDownClosed);
        }

        void ADSLSellerAgentItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (BandWidthComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            SellerAgentUsersComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(SellerAgentComboBox.SelectedIDs);
        }
        
        void BandWidthItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (BandWidthComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationByBandwidtIDs(BandWidthComboBox.SelectedIDs);
            DurationComboBox.ItemsComboBox.DropDownClosed += new EventHandler(DurationItemsComboBox_DropDownClosed);
        }

        void DurationItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (DurationComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationIDs(DurationComboBox.SelectedIDs);
            TrafficComboBox.ItemsComboBox.DropDownClosed += new EventHandler(TrafficItemsComboBox_DropDownClosed);
        }

        void TrafficItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (TrafficComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, ServiceGroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        }
        
        #endregion

        #region Methods

        public override void Search()
        {
            List<ADSLSellerAgentSaleDetailsInfo> Result = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestService = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLChangeService = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestTraffic = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLSellTraffic = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestStaticIP = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestGroupIP = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLChangeStaticIP = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLChangeGroupIP = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestModem = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLChangeServiceModem = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestRanje = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLRequestInstallment = new List<ADSLSellerAgentSaleDetailsInfo>();
            List<ADSLSellerAgentSaleDetailsInfo> ResultADSLChangeNo = new List<ADSLSellerAgentSaleDetailsInfo>();

            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLService))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                    ResultADSLRequestService = LoadDataADSLRequestService();

                ResultADSLChangeService = LoadDataADSLChangeSevice();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLTraffic))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                    ResultADSLRequestTraffic = LoadDataADSLRequestTraffic();

                ResultADSLSellTraffic = LoadDataADSlSellTraffic();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLIP))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                {
                    ResultADSLRequestStaticIP = LoadDataADSLRequestStaticIP();
                    ResultADSLRequestGroupIP = LoadDataADSLRequestGroupIP();
                    ResultADSLChangeStaticIP = LoadADSLChangeStaticIP();
                    ResultADSLChangeGroupIP = LoadADSLChangeGroupIP();
                }
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLModem))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                {
                     ResultADSLRequestModem = LoadDataADSLRequestModem();
                   // ResultADSLChangeServiceModem = LoadDataADSLChangeServiceModem();
                }
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLRanje))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                    ResultADSLRequestRanje = LoadDataADSLRequestRanje();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLInstallment))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                    ResultADSLRequestInstallment = LoadDataADSLRequestInstallment();
            }

            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLChangeNo))
            {
                if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                    ResultADSLChangeNo = LoadDataADSLChangeNo();
            }

            Result = ResultADSLRequestService.Union(ResultADSLChangeService.Distinct()).Union(ResultADSLRequestTraffic.Distinct())
               .Union(ResultADSLSellTraffic.Distinct()).Union(ResultADSLRequestStaticIP.Distinct()).Union(ResultADSLRequestGroupIP.Distinct()).Union(ResultADSLChangeStaticIP.Distinct())
               .Union(ResultADSLChangeGroupIP.Distinct()).Union(ResultADSLRequestModem.Distinct())
               .Union(ResultADSLChangeServiceModem.Distinct()).Union(ResultADSLRequestRanje).Union(ResultADSLRequestInstallment.Distinct()).Union(ResultADSLChangeNo.Distinct()).ToList();

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

            title = "گزارش ريز فروش نمايندگان فروش";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        
        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestService()
        {

            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
                toDate = ToDate.SelectedDate.Value.AddDays(1);

            if (ToPaymentDate.SelectedDate.HasValue)
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);

            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityComboBox.SelectedIDs,
                                                                               CenterComboBox.SelectedIDs,
                                                                               SellerAgentComboBox.SelectedIDs,
                                                                               SellerAgentUsersComboBox.SelectedIDs,
                                                                               SaleWayComboBox.SelectedIDs,
                                                                               PaymentTypeCombBox.SelectedIDs,
                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                               BandWidthComboBox.SelectedIDs,
                                                                               DurationComboBox.SelectedIDs,
                                                                               TrafficComboBox.SelectedIDs,
                                                                               ServiceComboBox.SelectedIDs,
                                                                               FromDate.SelectedDate,
                                                                               toDate,
                                                                               true,
                                                                               ServiceTypeCombBox.SelectedIDs,
                                                                               -1,
                                                                               FromPaymentDate.SelectedDate,
                                                                               toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityComboBox.SelectedIDs,
                                                                               CenterComboBox.SelectedIDs,
                                                                               SellerAgentComboBox.SelectedIDs,
                                                                               SellerAgentUsersComboBox.SelectedIDs,
                                                                               SaleWayComboBox.SelectedIDs,
                                                                               PaymentTypeCombBox.SelectedIDs,
                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                               BandWidthComboBox.SelectedIDs,
                                                                               DurationComboBox.SelectedIDs,
                                                                               TrafficComboBox.SelectedIDs,
                                                                               ServiceComboBox.SelectedIDs,
                                                                               FromDate.SelectedDate,
                                                                               toDate,
                                                                               false,
                                                                               ServiceTypeCombBox.SelectedIDs,
                                                                               -1,
                                                                               FromPaymentDate.SelectedDate,
                                                                               toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityComboBox.SelectedIDs,
                                                                               CenterComboBox.SelectedIDs,
                                                                               SellerAgentComboBox.SelectedIDs,
                                                                               SellerAgentUsersComboBox.SelectedIDs,
                                                                               SaleWayComboBox.SelectedIDs,
                                                                               PaymentTypeCombBox.SelectedIDs,
                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                               BandWidthComboBox.SelectedIDs,
                                                                               DurationComboBox.SelectedIDs,
                                                                               TrafficComboBox.SelectedIDs,
                                                                               ServiceComboBox.SelectedIDs,
                                                                               FromDate.SelectedDate,
                                                                               toDate,
                                                                               null,
                                                                               ServiceTypeCombBox.SelectedIDs,
                                                                               -1,
                                                                               FromPaymentDate.SelectedDate,
                                                                               toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }
            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityComboBox.SelectedIDs,
                                                                               CenterComboBox.SelectedIDs,
                                                                               SellerAgentComboBox.SelectedIDs,
                                                                               SellerAgentUsersComboBox.SelectedIDs,
                                                                               SaleWayComboBox.SelectedIDs,
                                                                               PaymentTypeCombBox.SelectedIDs,
                                                                               ServiceGroupComboBox.SelectedIDs,
                                                                               CustomerGroupComboBox.SelectedIDs,
                                                                               BandWidthComboBox.SelectedIDs,
                                                                               DurationComboBox.SelectedIDs,
                                                                               TrafficComboBox.SelectedIDs,
                                                                               ServiceComboBox.SelectedIDs,
                                                                               FromDate.SelectedDate,
                                                                               toDate,
                                                                               null,
                                                                               ServiceTypeCombBox.SelectedIDs,
                                                                               -1,
                                                                               FromPaymentDate.SelectedDate,
                                                                               toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }



            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLChangeSevice()
        {

            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    BandWidthComboBox.SelectedIDs,
                                                                                    DurationComboBox.SelectedIDs,
                                                                                    TrafficComboBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    true,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    BandWidthComboBox.SelectedIDs,
                                                                                    DurationComboBox.SelectedIDs,
                                                                                    TrafficComboBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    false,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    BandWidthComboBox.SelectedIDs,
                                                                                    DurationComboBox.SelectedIDs,
                                                                                    TrafficComboBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    BandWidthComboBox.SelectedIDs,
                                                                                    DurationComboBox.SelectedIDs,
                                                                                    TrafficComboBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestTraffic()
        {

            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      SaleWayComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      BandWidthComboBox.SelectedIDs,
                                                                                      DurationComboBox.SelectedIDs,
                                                                                      TrafficComboBox.SelectedIDs,
                                                                                      ServiceComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      true,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      SaleWayComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      BandWidthComboBox.SelectedIDs,
                                                                                      DurationComboBox.SelectedIDs,
                                                                                      TrafficComboBox.SelectedIDs,
                                                                                      ServiceComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      false,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      SaleWayComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      BandWidthComboBox.SelectedIDs,
                                                                                      DurationComboBox.SelectedIDs,
                                                                                      TrafficComboBox.SelectedIDs,
                                                                                      ServiceComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      SaleWayComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      BandWidthComboBox.SelectedIDs,
                                                                                      DurationComboBox.SelectedIDs,
                                                                                      TrafficComboBox.SelectedIDs,
                                                                                      ServiceComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSlSellTraffic()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityComboBox.SelectedIDs,
                                                                             CenterComboBox.SelectedIDs,
                                                                             SellerAgentComboBox.SelectedIDs,
                                                                             SellerAgentUsersComboBox.SelectedIDs,
                                                                             SaleWayComboBox.SelectedIDs,
                                                                             PaymentTypeCombBox.SelectedIDs,
                                                                             ServiceGroupComboBox.SelectedIDs,
                                                                             CustomerGroupComboBox.SelectedIDs,
                                                                             BandWidthComboBox.SelectedIDs,
                                                                             DurationComboBox.SelectedIDs,
                                                                             TrafficComboBox.SelectedIDs,
                                                                             ServiceComboBox.SelectedIDs,
                                                                             FromDate.SelectedDate,
                                                                             toDate,
                                                                             true,
                                                                             ServiceTypeCombBox.SelectedIDs,
                                                                             -1,
                                                                             FromPaymentDate.SelectedDate,
                                                                             toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityComboBox.SelectedIDs,
                                                                             CenterComboBox.SelectedIDs,
                                                                             SellerAgentComboBox.SelectedIDs,
                                                                             SellerAgentUsersComboBox.SelectedIDs,
                                                                             SaleWayComboBox.SelectedIDs,
                                                                             PaymentTypeCombBox.SelectedIDs,
                                                                             ServiceGroupComboBox.SelectedIDs,
                                                                             CustomerGroupComboBox.SelectedIDs,
                                                                             BandWidthComboBox.SelectedIDs,
                                                                             DurationComboBox.SelectedIDs,
                                                                             TrafficComboBox.SelectedIDs,
                                                                             ServiceComboBox.SelectedIDs,
                                                                             FromDate.SelectedDate,
                                                                             toDate,
                                                                             false,
                                                                             ServiceTypeCombBox.SelectedIDs,
                                                                             -1,
                                                                             FromPaymentDate.SelectedDate,
                                                                             toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityComboBox.SelectedIDs,
                                                                             CenterComboBox.SelectedIDs,
                                                                             SellerAgentComboBox.SelectedIDs,
                                                                             SellerAgentUsersComboBox.SelectedIDs,
                                                                             SaleWayComboBox.SelectedIDs,
                                                                             PaymentTypeCombBox.SelectedIDs,
                                                                             ServiceGroupComboBox.SelectedIDs,
                                                                             CustomerGroupComboBox.SelectedIDs,
                                                                             BandWidthComboBox.SelectedIDs,
                                                                             DurationComboBox.SelectedIDs,
                                                                             TrafficComboBox.SelectedIDs,
                                                                             ServiceComboBox.SelectedIDs,
                                                                             FromDate.SelectedDate,
                                                                             toDate,
                                                                             null,
                                                                             ServiceTypeCombBox.SelectedIDs,
                                                                             -1,
                                                                             FromPaymentDate.SelectedDate,
                                                                             toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityComboBox.SelectedIDs,
                                                                             CenterComboBox.SelectedIDs,
                                                                             SellerAgentComboBox.SelectedIDs,
                                                                             SellerAgentUsersComboBox.SelectedIDs,
                                                                             SaleWayComboBox.SelectedIDs,
                                                                             PaymentTypeCombBox.SelectedIDs,
                                                                             ServiceGroupComboBox.SelectedIDs,
                                                                             CustomerGroupComboBox.SelectedIDs,
                                                                             BandWidthComboBox.SelectedIDs,
                                                                             DurationComboBox.SelectedIDs,
                                                                             TrafficComboBox.SelectedIDs,
                                                                             ServiceComboBox.SelectedIDs,
                                                                             FromDate.SelectedDate,
                                                                             toDate,
                                                                             null,
                                                                             ServiceTypeCombBox.SelectedIDs,
                                                                             -1,
                                                                             FromPaymentDate.SelectedDate,
                                                                             toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestStaticIP()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                       CenterComboBox.SelectedIDs,
                                                                                       SellerAgentComboBox.SelectedIDs,
                                                                                       SellerAgentUsersComboBox.SelectedIDs,
                                                                                       PaymentTypeCombBox.SelectedIDs,
                                                                                       ServiceGroupComboBox.SelectedIDs,
                                                                                       CustomerGroupComboBox.SelectedIDs,
                                                                                       FromDate.SelectedDate,
                                                                                       toDate,
                                                                                       true,
                                                                                       ServiceTypeCombBox.SelectedIDs,
                                                                                        -1,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                       CenterComboBox.SelectedIDs,
                                                                                       SellerAgentComboBox.SelectedIDs,
                                                                                       SellerAgentUsersComboBox.SelectedIDs,
                                                                                       PaymentTypeCombBox.SelectedIDs,
                                                                                       ServiceGroupComboBox.SelectedIDs,
                                                                                       CustomerGroupComboBox.SelectedIDs,
                                                                                       FromDate.SelectedDate,
                                                                                       toDate,
                                                                                       false,
                                                                                       ServiceTypeCombBox.SelectedIDs,
                                                                                        -1,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                       CenterComboBox.SelectedIDs,
                                                                                       SellerAgentComboBox.SelectedIDs,
                                                                                       SellerAgentUsersComboBox.SelectedIDs,
                                                                                       PaymentTypeCombBox.SelectedIDs,
                                                                                       ServiceGroupComboBox.SelectedIDs,
                                                                                       CustomerGroupComboBox.SelectedIDs,
                                                                                       FromDate.SelectedDate,
                                                                                       toDate,
                                                                                       null,
                                                                                       ServiceTypeCombBox.SelectedIDs,
                                                                                        -1,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                       CenterComboBox.SelectedIDs,
                                                                                       SellerAgentComboBox.SelectedIDs,
                                                                                       SellerAgentUsersComboBox.SelectedIDs,
                                                                                       PaymentTypeCombBox.SelectedIDs,
                                                                                       ServiceGroupComboBox.SelectedIDs,
                                                                                       CustomerGroupComboBox.SelectedIDs,
                                                                                       FromDate.SelectedDate,
                                                                                       toDate,
                                                                                       null,
                                                                                       ServiceTypeCombBox.SelectedIDs,
                                                                                        -1,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestGroupIP()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      true,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      false,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeCombBox.SelectedIDs,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadADSLChangeStaticIP()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();

            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      true,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      false,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityComboBox.SelectedIDs,
                                                                                      CenterComboBox.SelectedIDs,
                                                                                      SellerAgentComboBox.SelectedIDs,
                                                                                      SellerAgentUsersComboBox.SelectedIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      -1,
                                                                                      FromPaymentDate.SelectedDate,
                                                                                      toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadADSLChangeGroupIP()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    true,
                                                                                    -1
                                                                                    ,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    false,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestModem()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    true,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);

            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    false,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);

            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);

            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);

            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLChangeServiceModem()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityComboBox.SelectedIDs,
                                                                                          CenterComboBox.SelectedIDs,
                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                          PaymentTypeCombBox.SelectedIDs,
                                                                                          ServiceGroupComboBox.SelectedIDs,
                                                                                          CustomerGroupComboBox.SelectedIDs,
                                                                                          FromDate.SelectedDate,
                                                                                          toDate,
                                                                                          true,
                                                                                          ServiceTypeCombBox.SelectedIDs,
                                                                                          ServiceComboBox.SelectedIDs,
                                                                                          -1,
                                                                                          FromPaymentDate.SelectedDate,
                                                                                          toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityComboBox.SelectedIDs,
                                                                                          CenterComboBox.SelectedIDs,
                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                          PaymentTypeCombBox.SelectedIDs,
                                                                                          ServiceGroupComboBox.SelectedIDs,
                                                                                          CustomerGroupComboBox.SelectedIDs,
                                                                                          FromDate.SelectedDate,
                                                                                          toDate,
                                                                                          false,
                                                                                          ServiceTypeCombBox.SelectedIDs,
                                                                                          ServiceComboBox.SelectedIDs,
                                                                                          -1,
                                                                                          FromPaymentDate.SelectedDate,
                                                                                          toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityComboBox.SelectedIDs,
                                                                                          CenterComboBox.SelectedIDs,
                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                          PaymentTypeCombBox.SelectedIDs,
                                                                                          ServiceGroupComboBox.SelectedIDs,
                                                                                          CustomerGroupComboBox.SelectedIDs,
                                                                                          FromDate.SelectedDate,
                                                                                          toDate,
                                                                                          null,
                                                                                          ServiceTypeCombBox.SelectedIDs,
                                                                                          ServiceComboBox.SelectedIDs,
                                                                                          -1,
                                                                                          FromPaymentDate.SelectedDate,
                                                                                          toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityComboBox.SelectedIDs,
                                                                                          CenterComboBox.SelectedIDs,
                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                          PaymentTypeCombBox.SelectedIDs,
                                                                                          ServiceGroupComboBox.SelectedIDs,
                                                                                          CustomerGroupComboBox.SelectedIDs,
                                                                                          FromDate.SelectedDate,
                                                                                          toDate,
                                                                                          null,
                                                                                          ServiceTypeCombBox.SelectedIDs,
                                                                                          ServiceComboBox.SelectedIDs,
                                                                                          -1,
                                                                                          FromPaymentDate.SelectedDate,
                                                                                          toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestRanje()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    true,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    false,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    SellerAgentComboBox.SelectedIDs,
                                                                                    SellerAgentUsersComboBox.SelectedIDs,
                                                                                    SaleWayComboBox.SelectedIDs,
                                                                                    PaymentTypeCombBox.SelectedIDs,
                                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    FromDate.SelectedDate,
                                                                                    toDate,
                                                                                    null,
                                                                                    ServiceTypeCombBox.SelectedIDs,
                                                                                    -1,
                                                                                    FromPaymentDate.SelectedDate,
                                                                                    toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }


            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestInstallment()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         SaleWayComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }
            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         SaleWayComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         false,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         SaleWayComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         null,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         SaleWayComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         null,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            return result;
        }

        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLChangeNo()
        {
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }


            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toInsertDate = null;

            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);



            }
            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         false,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         null,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityComboBox.SelectedIDs,
                                                                                         CenterComboBox.SelectedIDs,
                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         null,
                                                                                         ServiceTypeCombBox.SelectedIDs,
                                                                                         -1,
                                                                                         FromPaymentDate.SelectedDate,
                                                                                         toPaymentDate,
                                                                               FromInsertDate.SelectedDate,
                                                                               toInsertDate);
            }

            return result;
        }


        #endregion Methods
    }
}
