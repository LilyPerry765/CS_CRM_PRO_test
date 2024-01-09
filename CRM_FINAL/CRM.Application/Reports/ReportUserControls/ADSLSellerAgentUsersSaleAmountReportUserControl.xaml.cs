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
    /// Interaction logic for ADSLSellerAgentUsersSaleAmountReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentUsersSaleAmountReportUserControl : Local.ReportBase
    {
        #region properties
        public static bool GroupBoxDr = false;
        public static bool BandWidthDR = false;
        public static bool DurationDR = false;
        public static bool TrafficDR = false;
       
        #endregion

        #region Constructor

        public ADSLSellerAgentUsersSaleAmountReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Consructor

        #region Initializer

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
            ServicePaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));
            //PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            
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
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndPaymentTypeID(GroupComboBox.SelectedIDs, (int?)ServicePaymentTypeCombBox.SelectedValue);
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
       
        #endregion

        #region Methods

        public override void Search()
        {
            List<ADSLRequestInfo> Result=new List<ADSLRequestInfo>();
            List<ADSLRequestInfo> Result1 = new List<ADSLRequestInfo>();
            if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
            {
                 Result = LoadData();
                Result1 = LoadData1();
            }
            List<ADSLRequestInfo> Result3 = LoadAdditinal();
            List<ADSLRequestInfo> Result4 = LoadADSlChangeServiceData();
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

            string All = "همه";
            if (ServicePaymentTypeCombBox.SelectedIndex <= -1)
            {
                stiReport.Dictionary.Variables["PreOrPostPaid"].Value = All;
            }
            else
            {
            stiReport.Dictionary.Variables["PreOrPostPaid"].Value = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLPaymentType),(int)ServicePaymentTypeCombBox.SelectedValue);
            }

            for(int j=0; j<Result1.Count;j++)
            {
                for (int i = 0; i < Result.Count; i++)
                {
                    if (Result[i].ADSLSellerAgentUSer == Result1[j].ADSLSellerAgentUSer)
                    {
                        Result[j].TrafficSaleAmount = Result1[j].TrafficSaleAmount;
                        //Result[i].ADSLSellerAgent = Result[j].ADSLSellerAgent;
                        break;
                        
                    }
                    else if (i == Result.Count - 1)
                    {
                        Result.Add(Result1[j]);
                    }
                }
            }

            foreach (ADSLRequestInfo Info in Result)
            {
                for (int i = 0; i < Result3.Count; i++)
                {
                    if (Info.ADSLSellerAgentUSer == Result3[i].ADSLSellerAgentUSer && Result3[i].ServiceSaleAmount!=null)
                    {
                        Info.ServiceSaleAmount += Result3[i].ServiceSaleAmount;
                    }
                }
            }

            foreach (ADSLRequestInfo Info in Result)
            {
                for (int i = 0; i < Result4.Count; i++)
                {
                    if (Info.ADSLSellerAgentUSer == Result4[i].ADSLSellerAgentUSer && Result4[i].ServiceSaleAmount!=null)
                    {
                        Info.ServiceSaleAmount += Result4[i].ServiceSaleAmount;
                    }
                }
            }
            long? fromTrafficCost = (string.IsNullOrEmpty(FromTrafficCostTextBox.Text)) ? -1 : Convert.ToInt64(FromTrafficCostTextBox.Text);
            long? toTrafficCostText = (string.IsNullOrEmpty(ToTrafficCostTextBox.Text)) ? -1 : Convert.ToInt64(ToTrafficCostTextBox.Text);

            long? fromServiceCost = (string.IsNullOrEmpty(FromServiceCostTextBox.Text)) ? -1 : Convert.ToInt64(FromServiceCostTextBox.Text);
            long? toServiceCostText = (string.IsNullOrEmpty(ToServiceCostTextBox.Text)) ? -1 : Convert.ToInt64(ToServiceCostTextBox.Text);



            if (fromTrafficCost == -1 && toTrafficCostText == -1 && fromServiceCost == -1 && toServiceCostText==-1)
            {
            }
            else
            {
                for (int k = 0; k < Result.Count; k++)
                {
                    if (fromServiceCost != -1 && Result[k].ServiceSaleAmount < fromServiceCost)
                    {
                        Result.Remove(Result[k]);
                        k--;
                    }

                    if (toServiceCostText != -1 && Result[k].ServiceSaleAmount > toServiceCostText)
                    {
                        Result.Remove(Result[k]);
                        k--;
                    }

                    if (fromTrafficCost != -1 && Result[k].TrafficSaleAmount < fromTrafficCost)
                    {
                        Result.Remove(Result[k]);
                        k--;
                    }

                    if (toTrafficCostText != -1 && Result[k].TrafficSaleAmount > toTrafficCostText)
                    {
                        Result.Remove(Result[k]);
                        k--;
                    }
                }
            }

                title = "ميزان فروش نمايندگان فروش";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLRequestInfo> LoadData()
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

            DateTime? toInsertDate = null;
            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgentUsersServiceSaleAmount(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        (int?)ServicePaymentTypeCombBox.SelectedValue,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);
            return result;
            
        }

        public List<ADSLRequestInfo> LoadAdditinal()
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

            DateTime? toInsertDate = null;
            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.getADSLSEllerAGnetUserAdditionalServiceSaleAmount(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        (int?)ServicePaymentTypeCombBox.SelectedValue,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

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

            DateTime? toInsertDate = null;
            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgentUsersTrafficSaleAmount(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

        private List<ADSLRequestInfo> LoadADSlChangeServiceData()
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
            DateTime? toInsertDate = null;
            if (ToInsertDate.SelectedDate.HasValue)
            {
                toInsertDate = ToInsertDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLSellerAgentUsersADSLChangeServiceSaleAmount(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate,
                                                                                        FromInsertDate.SelectedDate,
                                                                                        toInsertDate);

            return result;

        }

       

        #endregion Methods
    }
}
