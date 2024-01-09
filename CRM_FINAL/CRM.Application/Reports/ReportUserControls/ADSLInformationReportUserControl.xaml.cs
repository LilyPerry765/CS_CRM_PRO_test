﻿using System;
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
    /// Interaction logic for ADSLInformationReportUserControl.xaml
    /// </summary>
    public partial class ADSLInformationReportUserControl : Local.ReportBase
    {
        #region properties
        public static bool GroupBoxDr = false;
        public static bool BandWidthDR = false;
        public static bool DurationDR = false;
        public static bool TrafficDR = false;
        #endregion

        #region Constructor

        public ADSLInformationReportUserControl()
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
            //SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanellLimited));
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
            List<ADSLInfo> Result = new List<ADSLInfo>();
            if (ExpiredCustomersCheckBox.IsChecked == true)
                Result = LoadExpiredADSLInfo();

            if (ExpiredCustomersCheckBox.IsChecked == false)
                Result = LoadNONExpiredADSLInfo();
            else
                if (ExpiredCustomersCheckBox.IsChecked == null)
                    Result = LoadData();

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

            //for (int i = 0; i < Result.Count; i++)
            //{

            //    if (Result[i].SaleWayByte == 1 || Result[i].SaleWayByte == 2)
            //    {
            //        Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 1);
            //    }
            //    if (Result[i].SaleWayByte == 3)
            //        Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 2);

            //    if (Result[i].SaleWayByte == 4)
            //        Result[i].SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 3);

            //}

            title = "گزارش اطلاعات ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLInfo> LoadData()
        // because all of sale ways in data base are null we dididant chack it out in ReportDB
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLInfo> result = ReportDB.GetADSLInformationALLReportInfo(FromDate.SelectedDate, toDate,
                                                                            CityComboBox.SelectedIDs,
                                                                            CenterComboBox.SelectedIDs,
                                                                            ServiceComboBox.SelectedIDs,
                                                                            GroupComboBox.SelectedIDs,
                                                                            BandWidthComboBox.SelectedIDs,
                                                                            TrafficComboBox.SelectedIDs,
                                                                            DurationComboBox.SelectedIDs,
                //SaleWayComboBox.SelectedIDs,
                                                                            CustomerGroupComboBox.SelectedIDs,
                                                                            ServicePaymentTypeCombBox.SelectedIDs,
                                                                            CustomerTypeCombBox.SelectedIDs,
                                                                            PersonTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLInfo> LoadExpiredADSLInfo()
        // because all of sale ways in data base are null we dididant chack it out in ReportDB
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLInfo> result = ReportDB.GetADSLInformationExpiredReportInfo(FromDate.SelectedDate, toDate, FromEXPDate.SelectedDate,
                                                                                 CityComboBox.SelectedIDs,
                                                                                 CenterComboBox.SelectedIDs,
                                                                                 ServiceComboBox.SelectedIDs,
                                                                                 GroupComboBox.SelectedIDs,
                                                                                 BandWidthComboBox.SelectedIDs,
                                                                                 TrafficComboBox.SelectedIDs,
                                                                                 DurationComboBox.SelectedIDs,
                                                                                 CustomerGroupComboBox.SelectedIDs,
                                                                                 ServicePaymentTypeCombBox.SelectedIDs,
                                                                                 CustomerTypeCombBox.SelectedIDs,
                                                                                 PersonTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLInfo> LoadNONExpiredADSLInfo()
        // because all of sale ways in data base are null we dididant chack it out in ReportDB
        {
            List<ADSLInfo> result = ReportDB.GetADSLInformationNONExpiredReportInfo(FromDate.SelectedDate, ToDate.SelectedDate,
                                                                                    CityComboBox.SelectedIDs,
                                                                                    CenterComboBox.SelectedIDs,
                                                                                    ServiceComboBox.SelectedIDs,
                                                                                    GroupComboBox.SelectedIDs,
                                                                                    BandWidthComboBox.SelectedIDs,
                                                                                    TrafficComboBox.SelectedIDs,
                                                                                    DurationComboBox.SelectedIDs,
                                                                                    CustomerGroupComboBox.SelectedIDs,
                                                                                    ServicePaymentTypeCombBox.SelectedIDs,
                                                                                    CustomerTypeCombBox.SelectedIDs,
                                                                                    PersonTypeCombBox.SelectedIDs);

            return result;

        }

        #endregion Methods
    }
}
