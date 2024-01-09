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
    /// Interaction logic for ADSLDayeriDischargeReportUserControl.xaml
    /// </summary>
    public partial class ADSLDayeriDischargeReportUserControl : Local.ReportBase
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLDayeriDischargeReportUserControl()
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
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));
            PaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));

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

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
           List< ADSLChargedDischargedInfo> Result = LoadData();
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
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            if ( fromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

            List<ADSLChargedDischargedInfo> NumberofChargedPrePaidList = new List<ADSLChargedDischargedInfo>();
            List<ADSLChargedDischargedInfo> NumberofDisChargedList = new List<ADSLChargedDischargedInfo>();
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }

            if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
            {
                NumberofChargedPrePaidList = ReportDB.GetNumberofADSLRequestCharged(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs
                                                                                                            , fromDate.SelectedDate, ToDate
                                                                                                            , ServiceComboBox.SelectedIDs,
                                                                                                             GroupComboBox.SelectedIDs,
                                                                                                             BandWidthComboBox.SelectedIDs,
                                                                                                             TrafficComboBox.SelectedIDs,
                                                                                                             DurationComboBox.SelectedIDs,
                                                                                                             SaleWayComboBox.SelectedIDs,
                                                                                                             CustomerGroupComboBox.SelectedIDs,
                                                                                                             ServicePaymentTypeCombBox.SelectedIDs,
                                                                                                             PaymentTypeCombBox.SelectedIDs);


                 NumberofDisChargedList = ReportDB.GetNumberofDisCharged(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs
                                                                                                            , fromDate.SelectedDate, ToDate
                                                                                                            , ServiceComboBox.SelectedIDs,
                                                                                                             GroupComboBox.SelectedIDs,
                                                                                                             BandWidthComboBox.SelectedIDs,
                                                                                                             TrafficComboBox.SelectedIDs,
                                                                                                             DurationComboBox.SelectedIDs,
                                                                                                             SaleWayComboBox.SelectedIDs,
                                                                                                             CustomerGroupComboBox.SelectedIDs,
                                                                                                             ServicePaymentTypeCombBox.SelectedIDs);
            }
                List<ADSLChargedDischargedInfo> NumberOfUsedPortList = ReportDB.GetNumberOfUsedPorts(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs);
                //**ADSL table doe not have any relation with others so we can not check it out!!
                List<ADSLChargedDischargedInfo> NumberOfActiveCustomers = ReportDB.GetNumberOfActiveCustomers(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs);
            

            List<ADSLChargedDischargedInfo> NumberofADSLChangeServiceList = ReportDB.GetNumberofADSLChangeService(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs
                                                                                                        , fromDate.SelectedDate, ToDate
                                                                                                        , ServiceComboBox.SelectedIDs,
                                                                                                         GroupComboBox.SelectedIDs,
                                                                                                         BandWidthComboBox.SelectedIDs,
                                                                                                         TrafficComboBox.SelectedIDs,
                                                                                                         DurationComboBox.SelectedIDs,
                                                                                                         SaleWayComboBox.SelectedIDs,
                                                                                                         CustomerGroupComboBox.SelectedIDs,
                                                                                                         ServicePaymentTypeCombBox.SelectedIDs,
                                                                                                         PaymentTypeCombBox.SelectedIDs);
            //************************************************************
            foreach (ADSLChargedDischargedInfo Info in Result)
            {
                for (int i = 0; i < NumberofChargedPrePaidList.Count; i++)
                {
                    if (Info.Center == NumberofChargedPrePaidList[i].Center)
                        Info.NumberPrePaidOfCharged = NumberofChargedPrePaidList[i].NumberPrePaidOfCharged;
                }


                for (int i = 0; i < NumberofDisChargedList.Count; i++)
                {
                    if (Info.Center == NumberofDisChargedList[i].Center)
                        Info.NumberOfDischarge = NumberofDisChargedList[i].NumberOfDischarge;
                }

                for (int i = 0; i < NumberOfUsedPortList.Count; i++)
                {
                    if (Info.Center == NumberOfUsedPortList[i].Center)
                        Info.NumberOfUsedPorts = NumberOfUsedPortList[i].NumberOfUsedPorts;
                }

                for (int i = 0; i < NumberOfActiveCustomers.Count; i++)
                {
                    if (Info.Center == NumberOfActiveCustomers[i].Center)
                        Info.NumberOfActiveCustomers = NumberOfActiveCustomers[i].NumberOfActiveCustomers;
                }

                for (int i = 0; i < NumberofADSLChangeServiceList.Count; i++)
                {
                    if (Info.Center == NumberofADSLChangeServiceList[i].Center)
                        Info.NumberOfADSLChangeService = NumberofADSLChangeServiceList[i].NumberOfADSLChangeService;
                }

            }
            title = "اطلاعات تخلیه/دایری ADSL";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLChargedDischargedInfo> LoadData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLChargedDischargedInfo> result = ReportDB.GetADSLDyeriDischargeInfo(CenterComboBox.SelectedIDs,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        fromDate.SelectedDate,
                                                                                        ToDate);
            return result;
        }
        #endregion Methods
    }
}
