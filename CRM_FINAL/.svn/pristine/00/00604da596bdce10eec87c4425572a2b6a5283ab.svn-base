using System;
using System.Collections.Generic;
using System.Windows.Documents;
using CRM.Data;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLSellerAgentSaleDetailesReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentADSLRequestSaleDetailesReportUserControl : Local.ReportBase
    {
        #region properties
     
        #endregion

        #region Constructor

        public ADSLSellerAgentADSLRequestSaleDetailesReportUserControl()
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
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            //SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanellLimited));
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
            SellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            SellerAgentComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ADSLSellerAgentItemsComboBox_DropDownClosed);
        }

        void ADSLSellerAgentItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (BandWidthComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            SellerAgentUsersComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(SellerAgentComboBox.SelectedIDs);
        }

        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (GroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(GroupComboBox.SelectedIDs);
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(GroupComboBox.SelectedIDs,ServicePaymentTypeCombBox.SelectedIDs);
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
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, GroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        }

        #endregion

        #region Methods

        public override void Search()
        {
            
            List<ADSLRequestInfo> Result = LoadData();
            List<ADSLRequestInfo> Result1 = LoadData1();
            List<ADSLRequestInfo> ResultIP = LoadDataIP();
            List<ADSLRequestInfo> ResultModem = LoadDataModem();
            List<ADSLRequestInfo> ResultTraffic = LoadDataTraffic();
            List<ADSLRequestInfo> ResultService = LoadDataService();

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

            long? fromCostText =(string.IsNullOrEmpty(FromCostTextBox.Text))?-1: Convert.ToInt64(FromCostTextBox.Text);
            long? toCostText = (string.IsNullOrEmpty(ToCostTextBox.Text))?-1:Convert.ToInt64(ToCostTextBox.Text);

          
                for (int i = 0; i < Result.Count; i++)
                {
                    RequestPaymentInfo amountSum = ReportDB.GetAmountSum(Result[i].ID);

                    if (amountSum != null)
                    {
                        Result[i].Cost = amountSum.AmountSum;
                    }
                    //if (Result[i].SaleWayByte == 1 || Result[i].SaleWayByte == 2)
                    //{
                    //    Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 1);
                    //}
                    //if (Result[i].SaleWayByte == 3)
                    //    Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 2);

                    //if (Result[i].SaleWayByte == 4)
                    //    Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 3);

                    if (Result[i].NeedModem == true)
                    {
                        Result[i].NeedModemString = "بلی";
                    }
                    else if (Result[i].NeedModem == false)
                        Result[i].NeedModemString = "خیر";



                    if (i == Result.Count - 1)
                        break;
                    else
                        for (int j = i + 1; j < Result.Count; j++)
                        {
                            if (Result[i].ID == Result[j].ID)
                            {
                                Result.Remove(Result[j]);
                                j--;
                            }

                        }

                    for (int k = 0; k < ResultIP.Count; k++)
                    {
                        if (Result[i].ID == ResultIP[k].ID)
                        {
                            Result[i].IPCost = ResultIP[k].IPCost;
                        }
                    }

                    for (int k = 0; k < ResultModem.Count; k++)
                    {
                        if (Result[i].ID == ResultModem[k].ID)
                        {
                            Result[i].ModemCost = ResultModem[k].ModemCost;
                        }
                    }

                    for (int k = 0; k < ResultTraffic.Count; k++)
                    {
                        if (Result[i].ID == ResultTraffic[k].ID)
                        {
                            Result[i].TrafficCost = ResultTraffic[k].TrafficCost;
                        }
                    }

                    for (int k = 0; k < ResultService.Count; k++)
                    {
                        if (Result[i].ID == ResultService[k].ID)
                        {
                            Result[i].ServiceCost = ResultService[k].ServiceCost;
                            Result[i].RequestPaymentType = ResultService[k].RequestPaymentType;
                        }
                    }

                }
                if (fromCostText == -1 && toCostText == -1)
                {
                }
                else
                {
                    for (int k = 0; k < Result.Count; k++)
                    {
                        if (fromCostText != -1 && Result[k].Cost < fromCostText)
                        {
                            Result.Remove(Result[k]);
                            k--;
                        }

                        if (toCostText != -1 && Result[k].Cost > toCostText)
                        {
                            Result.Remove(Result[k]);
                            k--;
                        }
                    }
                }

            stiReport.Dictionary.Variables["NumberOfRecords"].Value = Result.Count.ToString();

                title = "گزارش ریز ثبت نام ADSL هرنماينده فروش ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);
            stiReport.RegData("Result1", "Result1", Result1);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLRequestInfo> LoadData()
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
            
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgnetSaleDetailesInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate,toDate
                                                                                        ,CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;
            
        }

        private List<ADSLRequestInfo> LoadDataIP()
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
           
            List<ADSLRequestInfo> result = ReportDB.GetADSLRequestIPSellerAgnetSaleDetailesInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

        private List<ADSLRequestInfo> LoadDataModem()
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
           
            List<ADSLRequestInfo> result = ReportDB.GetADSLRequestModemSellerAgnetSaleDetailesInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

        private List<ADSLRequestInfo> LoadDataTraffic()
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
            List<ADSLRequestInfo> result = ReportDB.GetADSLRequestTrafficSellerAgnetSaleDetailesInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

        private List<ADSLRequestInfo> LoadDataService()
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
            List<ADSLRequestInfo> result = ReportDB.GetADSLRequestServiceSellerAgnetSaleDetailesInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

        //private List<ADSLRequestInfo> LoadDataChangeService()
        //{
        //    List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgnetADSLChangeServiceSaleDetailesInfo(SellerAgentComboBox.SelectedIDs,
        //                                                                                FromDate.SelectedDate, ToDate.SelectedDate
        //                                                                                , CityComboBox.SelectedIDs,
        //                                                                                CenterComboBox.SelectedIDs,
        //                                                                                ServiceComboBox.SelectedIDs,
        //                                                                                GroupComboBox.SelectedIDs,
        //                                                                                TypeComboBox.SelectedIDs,
        //                                                                                BandWidthComboBox.SelectedIDs,
        //                                                                                TrafficComboBox.SelectedIDs,
        //                                                                                DurationComboBox.SelectedIDs,
        //                                                                                SaleWayComboBox.SelectedIDs,
        //                                                                                HasModemCheckBox.IsChecked,
        //                                                                                PaymentTypeCombBox.SelectedIDs,
        //                                                                                SellerAgentUsersComboBox.SelectedIDs,
        //                                                                                CustomerGroupComboBox.SelectedIDs);

        //    return result;

        //}

        private  List<ADSLRequestInfo> LoadData1()
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
            List<ADSLRequestInfo> result = ReportDB.ADslSellerAgentSaleCount(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate
                                                                                        ,CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

        //private List<ADSLRequestInfo> LoadDataNumberOfChangeService()
        //{
        //    List<ADSLRequestInfo> result = ReportDB.GetNumberOfADSLSellerAgentADSLChangeService(SellerAgentComboBox.SelectedIDs,
        //                                                                                FromDate.SelectedDate, ToDate.SelectedDate
        //                                                                                , CityComboBox.SelectedIDs,
        //                                                                                CenterComboBox.SelectedIDs,
        //                                                                                ServiceComboBox.SelectedIDs,
        //                                                                                GroupComboBox.SelectedIDs,
        //                                                                                TypeComboBox.SelectedIDs,
        //                                                                                BandWidthComboBox.SelectedIDs,
        //                                                                                TrafficComboBox.SelectedIDs,
        //                                                                                DurationComboBox.SelectedIDs,
        //                                                                                SaleWayComboBox.SelectedIDs,
        //                                                                                HasModemCheckBox.IsChecked,
        //                                                                                PaymentTypeCombBox.SelectedIDs,
        //                                                                                SellerAgentUsersComboBox.SelectedIDs,
        //                                                                                CustomerGroupComboBox.SelectedIDs);
        //    return result;
        //}

        //private List<ADSLRequestInfo> LoadDataADSLRequestChangeIp()
        //{
        //    List<ADSLRequestInfo> result = ReportDB.GetADSLRequestChangeIPADSLSellerAgentUserDeetailsInfo(SellerAgentComboBox.SelectedIDs,
        //                                                                               FromDate.SelectedDate, ToDate.SelectedDate
        //                                                                               , CityComboBox.SelectedIDs,
        //                                                                               CenterComboBox.SelectedIDs,
        //                                                                               ServiceComboBox.SelectedIDs,
        //                                                                               GroupComboBox.SelectedIDs,
        //                                                                               TypeComboBox.SelectedIDs,
        //                                                                               BandWidthComboBox.SelectedIDs,
        //                                                                               TrafficComboBox.SelectedIDs,
        //                                                                               DurationComboBox.SelectedIDs,
        //                                                                               SaleWayComboBox.SelectedIDs,
        //                                                                               HasModemCheckBox.IsChecked,
        //                                                                               PaymentTypeCombBox.SelectedIDs,
        //                                                                               SellerAgentUsersComboBox.SelectedIDs,
        //                                                                               CustomerGroupComboBox.SelectedIDs);
        //    return result;
        //}

        //private List<ADSLRequestInfo> LoadDataGroupChangeIp()
        //{
        //    List<ADSLRequestInfo> result = ReportDB.GetChangeGroupStaticIPADSLSellerAgentUserDeetailsInfo(SellerAgentComboBox.SelectedIDs,
        //                                                                               FromDate.SelectedDate, ToDate.SelectedDate
        //                                                                               , CityComboBox.SelectedIDs,
        //                                                                               CenterComboBox.SelectedIDs,
        //                                                                               ServiceComboBox.SelectedIDs,
        //                                                                               GroupComboBox.SelectedIDs,
        //                                                                               TypeComboBox.SelectedIDs,
        //                                                                               BandWidthComboBox.SelectedIDs,
        //                                                                               TrafficComboBox.SelectedIDs,
        //                                                                               DurationComboBox.SelectedIDs,
        //                                                                               SaleWayComboBox.SelectedIDs,
        //                                                                               HasModemCheckBox.IsChecked,
        //                                                                               PaymentTypeCombBox.SelectedIDs,
        //                                                                               SellerAgentUsersComboBox.SelectedIDs,
        //                                                                               CustomerGroupComboBox.SelectedIDs);
        //    return result;
        //}

        //private List<ADSLRequestInfo> LoadSataticDataChangeIp()
        //{
        //    List<ADSLRequestInfo> result = ReportDB.GetChangeStaticIPADSLSellerAgentUserDeetailsInfo(SellerAgentComboBox.SelectedIDs,
        //                                                                               FromDate.SelectedDate, ToDate.SelectedDate
        //                                                                               , CityComboBox.SelectedIDs,
        //                                                                               CenterComboBox.SelectedIDs,
        //                                                                               ServiceComboBox.SelectedIDs,
        //                                                                               GroupComboBox.SelectedIDs,
        //                                                                               TypeComboBox.SelectedIDs,
        //                                                                               BandWidthComboBox.SelectedIDs,
        //                                                                               TrafficComboBox.SelectedIDs,
        //                                                                               DurationComboBox.SelectedIDs,
        //                                                                               SaleWayComboBox.SelectedIDs,
        //                                                                               HasModemCheckBox.IsChecked,
        //                                                                               PaymentTypeCombBox.SelectedIDs,
        //                                                                               SellerAgentUsersComboBox.SelectedIDs,
        //                                                                               CustomerGroupComboBox.SelectedIDs);
        //    return result;
        //}

        #endregion Methods
    }
}
