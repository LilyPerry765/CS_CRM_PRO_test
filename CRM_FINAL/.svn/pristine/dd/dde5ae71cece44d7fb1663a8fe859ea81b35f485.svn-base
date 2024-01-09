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
using System.Windows.Shapes;
using CRM.Data;
using System.Collections;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Views
{
    public partial class ADSLSellerAgentUserReport : Local.PopupWindow
    {
        #region Properties

        public static bool GroupBoxDr = false;
        public static bool BandWidthDR = false;
        public static bool DurationDR = false;
        public static bool TrafficDR = false;
        private List<int> CityIDs = new List<int>();
        private List<int> CenterIDs = new List<int>();
        private List<int> ADSLSellerAgnetUserIDs = new List<int>();
        private List<int> ADSLSellerAgentIDs = new List<int>();
        private List<int> SaleWays = new List<int>();
        private int _SellerID = 0;
      

        #endregion

        #region Constructors

        public ADSLSellerAgentUserReport()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellerAgentUserReport(int sellerID,List<int> cityIDs)
            : this()
        {
            _SellerID = sellerID;
            CityIDs = cityIDs;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleType));
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            ServiceGroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            ServiceTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            IsAcceptedComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.IsAccepted));
        }

        private void LoadData()
        {
            if (_SellerID != 0)
            {
                ADSLSellerAgnetUserIDs.Add(_SellerID);
                ADSLSellerAgentIDs.Add(SearchADSLSellerAgentByCreatorUserID(_SellerID));
            }
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToDate.SelectedDate != null)
            {
                ToDate.SelectedDate.Value.AddDays(1);
            }

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
                ResultADSLRequestService = LoadDataADSLRequestService();
                ResultADSLChangeService = LoadDataADSLChangeSevice();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLTraffic))
            {
                ResultADSLRequestTraffic = LoadDataADSLRequestTraffic();
                ResultADSLSellTraffic = LoadDataADSlSellTraffic();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLIP))
            {
                ResultADSLRequestStaticIP = LoadDataADSLRequestStaticIP();
                ResultADSLRequestGroupIP = LoadDataADSLRequestGroupIP();
                ResultADSLChangeStaticIP = LoadADSLChangeStaticIP();
                ResultADSLChangeGroupIP = LoadADSLChangeGroupIP();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLModem))
            {
                ResultADSLRequestModem = LoadDataADSLRequestModem();
                ResultADSLChangeServiceModem = LoadDataADSLChangeServiceModem();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLRanje))
            {
                ResultADSLRequestRanje = LoadDataADSLRequestRanje();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLInstallment))
            {
                ResultADSLRequestInstallment = LoadDataADSLRequestInstallment();
            }
            if (TypeComboBox.SelectedIDs.Count == 0 || TypeComboBox.SelectedIDs.Contains((int)DB.ADSLSaleType.ADSLChangeNo))
            {
                ResultADSLChangeNo = LoadDataADSLChangeNo();
            }

            Result = ResultADSLRequestService.Union(ResultADSLChangeService.Distinct()).Union(ResultADSLRequestTraffic.Distinct())
                .Union(ResultADSLSellTraffic.Distinct()).Union(ResultADSLRequestStaticIP.Distinct()).Union(ResultADSLRequestGroupIP.Distinct()).Union(ResultADSLChangeStaticIP.Distinct())
                .Union(ResultADSLChangeGroupIP.Distinct()).Union(ResultADSLRequestModem.Distinct())
                .Union(ResultADSLChangeServiceModem.Distinct()).Union(ResultADSLRequestRanje).Union(ResultADSLRequestInstallment.Distinct()).Union(ResultADSLChangeNo.Distinct()).ToList();

            SendToPrint(Result);
        }

        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (ServiceGroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            GroupBoxDr = true;
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(ServiceGroupComboBox.SelectedIDs);
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(ServiceGroupComboBox.SelectedIDs, ServiceTypeComboBox.SelectedIDs);
            BandWidthComboBox.ItemsComboBox.DropDownClosed += new EventHandler(BandWidthItemsComboBox_DropDownClosed);
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

        public int SearchADSLSellerAgentByCreatorUserID(int CreatorUserID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers.Where(t =>
                    (t.ID == CreatorUserID)).Select(t => t.ADSLSellerAgent.ID).SingleOrDefault();
            }
        }


        private List<ADSLSellerAgentSaleDetailsInfo> LoadDataADSLRequestService()
        {
            
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
            List<ADSLSellerAgentSaleDetailsInfo> result = new List<ADSLSellerAgentSaleDetailsInfo>();

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityIDs,
                                                                                       CenterIDs,
                                                                                       ADSLSellerAgentIDs,
                                                                                       ADSLSellerAgnetUserIDs,
                                                                                       SaleWays,
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
                                                                                       ServiceTypeComboBox.SelectedIDs,
                                                                                       -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityIDs,
                                                                                       CenterIDs,
                                                                                       ADSLSellerAgentIDs,
                                                                                       ADSLSellerAgnetUserIDs,
                                                                                       SaleWays,
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
                                                                                       ServiceTypeComboBox.SelectedIDs,
                                                                                       -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityIDs,
                                                                                       CenterIDs,
                                                                                       ADSLSellerAgentIDs,
                                                                                       ADSLSellerAgnetUserIDs,
                                                                                       SaleWays,
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
                                                                                       ServiceTypeComboBox.SelectedIDs,
                                                                                       -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }
            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestServiceInfo(CityIDs,
                                                                                       CenterIDs,
                                                                                       ADSLSellerAgentIDs,
                                                                                       ADSLSellerAgnetUserIDs,
                                                                                       SaleWays,
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
                                                                                       ServiceTypeComboBox.SelectedIDs,
                                                                                       -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     SaleWays,
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
                                                                                     ServiceTypeComboBox.SelectedIDs,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     SaleWays,
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
                                                                                     ServiceTypeComboBox.SelectedIDs,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     SaleWays,
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
                                                                                     ServiceTypeComboBox.SelectedIDs,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     SaleWays,
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
                                                                                     ServiceTypeComboBox.SelectedIDs,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      SaleWays,
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
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     SaleWays,
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
                                                                                     ServiceTypeComboBox.SelectedIDs,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      SaleWays,
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
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestTrafficInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      SaleWays,
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
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
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
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
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
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
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
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLSellTrafficInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
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
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityIDs,
                                                                                       CenterIDs,
                                                                                       ADSLSellerAgentIDs,
                                                                                       ADSLSellerAgnetUserIDs,
                                                                                       PaymentTypeCombBox.SelectedIDs,
                                                                                       ServiceGroupComboBox.SelectedIDs,
                                                                                       CustomerGroupComboBox.SelectedIDs,
                                                                                       FromDate.SelectedDate,
                                                                                       toDate,
                                                                                       true,
                                                                                       ServiceTypeComboBox.SelectedIDs,
                                                                                       -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }                                                                         

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      false,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      true,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      false,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestGroupIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      ServiceTypeComboBox.SelectedIDs,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      true,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }                                                                        
                                                                                     
            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      false,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeStaticIPInfo(CityIDs,
                                                                                      CenterIDs,
                                                                                      ADSLSellerAgentIDs,
                                                                                      ADSLSellerAgnetUserIDs,
                                                                                      PaymentTypeCombBox.SelectedIDs,
                                                                                      ServiceGroupComboBox.SelectedIDs,
                                                                                      CustomerGroupComboBox.SelectedIDs,
                                                                                      FromDate.SelectedDate,
                                                                                      toDate,
                                                                                      null,
                                                                                      -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     PaymentTypeCombBox.SelectedIDs,
                                                                                     ServiceGroupComboBox.SelectedIDs,
                                                                                     CustomerGroupComboBox.SelectedIDs,
                                                                                     FromDate.SelectedDate,
                                                                                     toDate,
                                                                                     true,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     PaymentTypeCombBox.SelectedIDs,
                                                                                     ServiceGroupComboBox.SelectedIDs,
                                                                                     CustomerGroupComboBox.SelectedIDs,
                                                                                     FromDate.SelectedDate,
                                                                                     toDate,
                                                                                     false,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     PaymentTypeCombBox.SelectedIDs,
                                                                                     ServiceGroupComboBox.SelectedIDs,
                                                                                     CustomerGroupComboBox.SelectedIDs,
                                                                                     FromDate.SelectedDate,
                                                                                     toDate,
                                                                                     null,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeGroupIPInfo(CityIDs,
                                                                                     CenterIDs,
                                                                                     ADSLSellerAgentIDs,
                                                                                     ADSLSellerAgnetUserIDs,
                                                                                     PaymentTypeCombBox.SelectedIDs,
                                                                                     ServiceGroupComboBox.SelectedIDs,
                                                                                     CustomerGroupComboBox.SelectedIDs,
                                                                                     FromDate.SelectedDate,
                                                                                     toDate,
                                                                                     null,
                                                                                     -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityIDs,
                                                                                  CenterIDs,
                                                                                  ADSLSellerAgentIDs,
                                                                                  ADSLSellerAgnetUserIDs,
                                                                                  PaymentTypeCombBox.SelectedIDs,
                                                                                  ServiceGroupComboBox.SelectedIDs,
                                                                                  CustomerGroupComboBox.SelectedIDs,
                                                                                  FromDate.SelectedDate,
                                                                                  toDate,
                                                                                  true,
                                                                                  ServiceTypeComboBox.SelectedIDs,
                                                                                  ServiceComboBox.SelectedIDs,
                                                                                  -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);

            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityIDs,
                                                                                  CenterIDs,
                                                                                  ADSLSellerAgentIDs,
                                                                                  ADSLSellerAgnetUserIDs,
                                                                                  PaymentTypeCombBox.SelectedIDs,
                                                                                  ServiceGroupComboBox.SelectedIDs,
                                                                                  CustomerGroupComboBox.SelectedIDs,
                                                                                  FromDate.SelectedDate,
                                                                                  toDate,
                                                                                  false,
                                                                                  ServiceTypeComboBox.SelectedIDs,
                                                                                  ServiceComboBox.SelectedIDs,
                                                                                  -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);

            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityIDs,
                                                                                  CenterIDs,
                                                                                  ADSLSellerAgentIDs,
                                                                                  ADSLSellerAgnetUserIDs,
                                                                                  PaymentTypeCombBox.SelectedIDs,
                                                                                  ServiceGroupComboBox.SelectedIDs,
                                                                                  CustomerGroupComboBox.SelectedIDs,
                                                                                  FromDate.SelectedDate,
                                                                                  toDate,
                                                                                  null,
                                                                                  ServiceTypeComboBox.SelectedIDs,
                                                                                  ServiceComboBox.SelectedIDs,
                                                                                  -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);

            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestModemInfo(CityIDs,
                                                                                  CenterIDs,
                                                                                  ADSLSellerAgentIDs,
                                                                                  ADSLSellerAgnetUserIDs,
                                                                                  PaymentTypeCombBox.SelectedIDs,
                                                                                  ServiceGroupComboBox.SelectedIDs,
                                                                                  CustomerGroupComboBox.SelectedIDs,
                                                                                  FromDate.SelectedDate,
                                                                                  toDate,
                                                                                  null,
                                                                                  ServiceTypeComboBox.SelectedIDs,
                                                                                  ServiceComboBox.SelectedIDs,
                                                                                  -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         ServiceComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         false,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         ServiceComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         null,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         ServiceComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeServiceModemInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         null,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         ServiceComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
                                                                                   PaymentTypeCombBox.SelectedIDs,
                                                                                   ServiceGroupComboBox.SelectedIDs,
                                                                                   CustomerGroupComboBox.SelectedIDs,
                                                                                   FromDate.SelectedDate,
                                                                                   toDate,
                                                                                   true,
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
                                                                                   PaymentTypeCombBox.SelectedIDs,
                                                                                   ServiceGroupComboBox.SelectedIDs,
                                                                                   CustomerGroupComboBox.SelectedIDs,
                                                                                   FromDate.SelectedDate,
                                                                                   toDate,
                                                                                   false,
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
                                                                                   PaymentTypeCombBox.SelectedIDs,
                                                                                   ServiceGroupComboBox.SelectedIDs,
                                                                                   CustomerGroupComboBox.SelectedIDs,
                                                                                   FromDate.SelectedDate,
                                                                                   toDate,
                                                                                   null,
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestRanjeInfo(CityIDs,
                                                                                   CenterIDs,
                                                                                   ADSLSellerAgentIDs,
                                                                                   ADSLSellerAgnetUserIDs,
                                                                                   SaleWays,
                                                                                   PaymentTypeCombBox.SelectedIDs,
                                                                                   ServiceGroupComboBox.SelectedIDs,
                                                                                   CustomerGroupComboBox.SelectedIDs,
                                                                                   FromDate.SelectedDate,
                                                                                   toDate,
                                                                                   null,
                                                                                   ServiceTypeComboBox.SelectedIDs,
                                                                                   -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         SaleWays,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }
            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         SaleWays,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         SaleWays,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLRequestInstallmentInfo(CityIDs,
                                                                                         CenterIDs,
                                                                                         ADSLSellerAgentIDs,
                                                                                         ADSLSellerAgnetUserIDs,
                                                                                         SaleWays,
                                                                                         PaymentTypeCombBox.SelectedIDs,
                                                                                         ServiceGroupComboBox.SelectedIDs,
                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                         FromDate.SelectedDate,
                                                                                         toDate,
                                                                                         true,
                                                                                         ServiceTypeComboBox.SelectedIDs,
                                                                                         -1, FromPaymentDate.SelectedDate, toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
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
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityIDs,
                                                                                CenterIDs,
                                                                                ADSLSellerAgentIDs,
                                                                                ADSLSellerAgnetUserIDs,
                                                                                PaymentTypeCombBox.SelectedIDs,
                                                                                ServiceGroupComboBox.SelectedIDs,
                                                                                CustomerGroupComboBox.SelectedIDs,
                                                                                FromDate.SelectedDate,
                                                                                toDate,
                                                                                true,
                                                                                ServiceTypeComboBox.SelectedIDs,
                                                                                -1,
                                                                                FromPaymentDate.SelectedDate,
                                                                                toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);



            }
            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityIDs,
                                                                                CenterIDs,
                                                                                ADSLSellerAgentIDs,
                                                                                ADSLSellerAgnetUserIDs,
                                                                                PaymentTypeCombBox.SelectedIDs,
                                                                                ServiceGroupComboBox.SelectedIDs,
                                                                                CustomerGroupComboBox.SelectedIDs,
                                                                                FromDate.SelectedDate,
                                                                                toDate,
                                                                                false,
                                                                                ServiceTypeComboBox.SelectedIDs,
                                                                                -1,
                                                                                FromPaymentDate.SelectedDate,
                                                                                toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityIDs,
                                                                                CenterIDs,
                                                                                ADSLSellerAgentIDs,
                                                                                ADSLSellerAgnetUserIDs,
                                                                                PaymentTypeCombBox.SelectedIDs,
                                                                                ServiceGroupComboBox.SelectedIDs,
                                                                                CustomerGroupComboBox.SelectedIDs,
                                                                                FromDate.SelectedDate,
                                                                                toDate,
                                                                                null,
                                                                                ServiceTypeComboBox.SelectedIDs,
                                                                                -1,
                                                                                FromPaymentDate.SelectedDate,
                                                                                toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            if (IsAcceptedComboBox.SelectedIDs.Count == 0)
            {
                result = ReportDB.GetADSLSellerAgnetsaleDetailsADSLChangeNOInfo(CityIDs,
                                                                                CenterIDs,
                                                                                ADSLSellerAgentIDs,
                                                                                ADSLSellerAgnetUserIDs,
                                                                                PaymentTypeCombBox.SelectedIDs,
                                                                                ServiceGroupComboBox.SelectedIDs,
                                                                                CustomerGroupComboBox.SelectedIDs,
                                                                                FromDate.SelectedDate,
                                                                                toDate,
                                                                                null,
                                                                                ServiceTypeComboBox.SelectedIDs,
                                                                                -1,
                                                                                FromPaymentDate.SelectedDate,
                                                                                toPaymentDate,
                                                                                     FromInserDate.SelectedDate,
                                                                                     toInsertDate);
            }

            return result;
        }


        private void SendToPrint(IEnumerable Result)
        {
            

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ADSLSellerAgentSaleDetailsReport);
            stiReport.Load(path);
            string title = "گزارش ريز فروش نمايندگان فروش ";

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Header"].Value = title;
            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        #endregion
    }
}
