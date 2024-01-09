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
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLSellerAgentSaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentSaleReportUserControl : Local.ReportBase
    {
        #region properties
        public static bool GroupBoxDr = false;
        public static bool BandWidthDR = false;
        public static bool DurationDR = false;
        public static bool TrafficDR = false;
        
        #endregion

        #region Constructor

        public ADSLSellerAgentSaleReportUserControl()
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


        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (GroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            GroupBoxDr = true;

            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(GroupComboBox.SelectedIDs);
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(GroupComboBox.SelectedIDs, ServicePaymentTypeCombBox.SelectedIDs);
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
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, GroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        }
        #endregion

        #region Methods

        public override void Search()
        {


            List<ADSLRequestInfo> Result = LoadData();
            List<ADSLRequestInfo> Result1 = LoadData1();
            List<ADSLRequestInfo> Result2 = LoadData2();
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

            for (int i = 0; i < Result.Count; i++)
            {
                RequestPaymentInfo amountSum = ReportDB.GetAmountSum(Result[i].ID);

                Result[i].Cost = amountSum.AmountSum;
                //if (Result[i].SaleWayByte == 1 || Result[i].SaleWayByte == 2)
                //{
                //    Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 1);
                //}
                //if (Result[i].SaleWayByte == 3)
                //    Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 2);

                //if (Result[i].SaleWayByte == 4)
                //    Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 3);
                
            }

            for (int i = 0; i < Result.Count; i++)
            {
                if (i == Result.Count - 1)
                    break;
                else
                {
                    for (int j = i + 1; j < Result.Count; j++)
                    {

        if (Result[i].SaleWay == Result[j].SaleWay && Result[i].SellerAgentName == Result[j].SellerAgentName 
            && Result[i].ADSLSellerAgentUSer == Result[j].ADSLSellerAgentUSer && Result[i].ServiceTitle == Result[j].ServiceTitle)

                        {
                                if (Result[i].ID == Result[j].ID)
                            {
                                Result[i].Cost = (Result[i].Cost) + (Result[j].Cost);
                                    Result.Remove(Result[j]);
                                    j--;


                            }
                       
                        }

                    }

           for (int j = i + 1; j < Result.Count; j++)
                 {
                        if (Result[i].SaleWay == Result[j].SaleWay && Result[i].SellerAgentName == Result[j].SellerAgentName &&
                            Result[i].ADSLSellerAgentUSer == Result[j].ADSLSellerAgentUSer && Result[i].ServiceTitle == Result[j].ServiceTitle)
                        {
                            if (Result[i].ID != Result[j].ID)
                            {
                                Result[i].NumberOfADSLSaled = (Result[i].NumberOfADSLSaled) + (Result[j].NumberOfADSLSaled);
                                Result[i].Cost = (Result[i].Cost) + (Result[j].Cost);
                                Result.Remove(Result[j]);
                                j--;

                            }
                        }
                    }
                }
            }


            long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
            long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

            if (fromCost == -1 && toCost == -1)
            {
            }
            else
            {
                for (int k = 0; k < Result.Count; k++)
                {
                    if (fromCost != -1 && Result[k].Cost < fromCost)
                    {
                        Result.Remove(Result[k]);
                        k--;
                    }


                    if (toCost != -1 && Result[k].Cost > toCost)
                    {
                        Result.Remove(Result[k]);
                        k--;
                    }
                }
            }

            for (int y = 0; y < Result.Count; y++)
            {
                for (int j = 0; j < Result1.Count; j++)
                {
                    if (Result[y].CityName == Result1[j].CityName)
                    {
                        Result1[j].Cost = Result1[j].Cost + Result[y].Cost;
                    }
                }
                for (int j = 0; j < Result2.Count; j++)
                {
                    if (Result[y].Center == Result2[j].Center)
                    {
                        Result2[j].Cost = Result2[j].Cost + Result[y].Cost;
                    }
                }
            }

                title = "گزارش فروش نمايندکان فروش ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);
            stiReport.RegData("Result1", "Result1", Result1);
            stiReport.RegData("Result2", "Result2", Result2);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLRequestInfo> LoadData()
        // because all of sale ways in data base are null we dididant chack it out in ReportDB
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgentSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
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
                                                                                        toPaymentDate);

            return result;

        }

        private List<ADSLRequestInfo> LoadData1()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgentSaleCityChartInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
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
                                                                                        toPaymentDate);

            return result;

        }

        private List<ADSLRequestInfo> LoadData2()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgentSaleCenterChartInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
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
                                                                                        toPaymentDate);

            return result;

        }
        #endregion Methods
    }
}
