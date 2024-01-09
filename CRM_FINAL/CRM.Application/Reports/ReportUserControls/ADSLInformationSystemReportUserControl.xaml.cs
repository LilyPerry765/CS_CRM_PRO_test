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
    /// Interaction logic for ADSLInformationSystemReportUserControl.xaml
    /// </summary>
    public partial class ADSLInformationSystemReportUserControl : Local.ReportBase
    {
        #region properties
        public static bool GroupBoxDr = false;
        public static bool BandWidthDR = false;
        public static bool DurationDR = false;
        public static bool TrafficDR = false;
        #endregion

        #region Constructor

        public ADSLInformationSystemReportUserControl()
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
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            CustomerTypeCombBox.ItemsSource = ADSLCustomerTypeDB.GetADSLCustomerTypesCheckable();
            PersonTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
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
            
            List<ADSLInfo> Result= new List<ADSLInfo>();
            List<ADSLInfo> ResultADSLRequest = new List<ADSLInfo>();
            List<ADSLInfo> ResultADSLRequestRanje = new List<ADSLInfo>();
            List<ADSLInfo> ResultADSLRequestInstallment = new List<ADSLInfo>();
            List<ADSLInfo> ResultADSlChangeServiceRanjeCost = new List<ADSLInfo>();
            List<ADSLInfo> ResultADSLchangeServiceInstallmentCost = new List<ADSLInfo>();

            if(SaleWayComboBox.SelectedIDs.Count==0 || SaleWayComboBox.SelectedIDs.Count==2 || (SaleWayComboBox.SelectedIDs.Count==1 && SaleWayComboBox.SelectedIndex!=1))
            {

                ResultADSLRequest = LoadData();
                ResultADSLRequestRanje = LoadADSLRequestRanjeData();
                ResultADSLRequestInstallment = LoadADSLRequestInstallmentData();
                ResultADSlChangeServiceRanjeCost = LoadChangeServiceRanjeData();
                ResultADSLchangeServiceInstallmentCost = LoadChangeServiceInstallmentData();
            }
            
            List<ADSLInfo> ResultADSLChangeService = new List<ADSLInfo>();

            ResultADSLChangeService = LoadChangeServiceData();

            for (int i = 0; i < ResultADSLRequest.Count; i++)
            {
                for (int j = 0; j < ResultADSLRequestRanje.Count; j++)
                {
                    if (ResultADSLRequest[i].Center == ResultADSLRequestRanje[j].Center && ResultADSLRequest[i].TelephoneNo == ResultADSLRequestRanje[j].TelephoneNo)
                    {
                        ResultADSLRequest[i].RanjeCost = ResultADSLRequestRanje[j].RanjeCost;
                    }
                }

                for (int j = 0; j < ResultADSLRequestInstallment.Count; j++)
                {
                    if (ResultADSLRequest[i].Center == ResultADSLRequestInstallment[j].Center && ResultADSLRequest[i].TelephoneNo == ResultADSLRequestInstallment[j].TelephoneNo)
                    {
                        ResultADSLRequest[i].InstallmentCost = ResultADSLRequestInstallment[j].InstallmentCost;
                    }
                }
            }

            for (int i = 0; i < ResultADSLChangeService.Count; i++)
            {
                for (int j = 0; j < ResultADSlChangeServiceRanjeCost.Count; j++)
                {
                    if (ResultADSLChangeService[i].Center == ResultADSlChangeServiceRanjeCost[j].Center && ResultADSlChangeServiceRanjeCost[i].TelephoneNo == ResultADSLRequestRanje[j].TelephoneNo)
                    {
                        ResultADSLChangeService[i].RanjeCost = ResultADSlChangeServiceRanjeCost[j].RanjeCost;
                    }
                }

                for (int j = 0; j < ResultADSLchangeServiceInstallmentCost.Count; j++)
                {
                    if (ResultADSLChangeService[i].Center == ResultADSLchangeServiceInstallmentCost[j].Center && ResultADSLchangeServiceInstallmentCost[i].TelephoneNo == ResultADSLchangeServiceInstallmentCost[j].TelephoneNo)
                    {
                        ResultADSLChangeService[i].InstallmentCost = ResultADSLchangeServiceInstallmentCost[j].InstallmentCost;
                    }
                }
            }
               
            Result = ResultADSLRequest.Union(ResultADSLChangeService).ToList();

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

           
                title = "گزارش اطلاعات ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLRequestInformationInSystemReportInfo(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs, 
                                                                                        CustomerTypeCombBox.SelectedIDs,
                                                                                        PersonTypeCombBox.SelectedIDs);
            return result;

        }

        private List<ADSLInfo> LoadADSLRequestRanjeData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLRequestInformationInSystemRanjeCostReportInfo(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerTypeCombBox.SelectedIDs,
                                                                                        PersonTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLInfo> LoadADSLRequestInstallmentData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLRequestInformationInSystemInstallmentCostReportInfo(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerTypeCombBox.SelectedIDs,
                                                                                        PersonTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLInfo> LoadChangeServiceData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLChangeServiceInformationInSystemReportInfo(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerTypeCombBox.SelectedIDs,
                                                                                        PersonTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLInfo> LoadChangeServiceRanjeData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLChangeServiceInformationInSystemRanjeCostReportInfo(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerTypeCombBox.SelectedIDs,
                                                                                        PersonTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLInfo> LoadChangeServiceInstallmentData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLChangeServiceInformationInSystemInstallmentCostReportInfo(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerTypeCombBox.SelectedIDs,
                                                                                        PersonTypeCombBox.SelectedIDs);

            return result;

        }

        #endregion Methods
    }
}
