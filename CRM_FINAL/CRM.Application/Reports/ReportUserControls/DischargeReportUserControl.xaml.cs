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
    /// Interaction logic for DischargeReportUserControl.xaml
    /// </summary>
    public partial class DischargeReportUserControl : Local.ReportBase
    {
       #region Constructor

        public DischargeReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            TelephoneTypeComboBox.ItemsSource = CustomerTypeDB.GetCustomerTypesCheckable();
           // TelephoneTypeGroupComboBox.ItemsSource = CustomerGroupDB.GetCustomerGroupsCheckable();
            DischargeReasonComboBox.ItemsSource = CauseOfTakePossessionDB.GetCauseOfTakePossessionCheckableItem();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<TakePossessionInfo> Result = LoadData();
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

            if (fromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

           

            title = " تخلیه ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", Result);
            //stiReport.RegData("installRequest", "installRequest", Result.Select(t=>t.installRequest).ToList());


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<TakePossessionInfo> LoadData()
        {
                List<long?> List = new List<long?>();
                long FromtelNo = (!string.IsNullOrEmpty(FromTelNoTextBox.Text)) ? Convert.ToInt64(FromTelNoTextBox.Text) : 0;
                long ToTelNo = (!string.IsNullOrEmpty(ToTelNoTextBox.Text)) ? Convert.ToInt64(ToTelNoTextBox.Text) : 0;


                //List<InstallRequestInfo> TakePossessionInfo = ReportDB.GetDayeriInfo(TelephoneTypeComboBox.SelectedIDs,
                //                                                              TelephoneTypeGroupComboBox.SelectedIDs,
                //                                                              FromtelNo,
                //                                                              ToTelNo , CityCenterUC.CenterCheckableComboBox.SelectedIDs);
                List<TakePossessionInfo> result = ReportDB.GetTakePossessionInfo(
                                                                                  CityComboBox.SelectedIDs,
                                                                                  CenterComboBox.SelectedIDs,
                                                                                  fromDate.SelectedDate,
                                                                                  toDate.SelectedDate,
                                                                                  FromtelNo,
                                                                                  ToTelNo,
                                                                                  DischargeReasonComboBox.SelectedIDs,
                                                                                  TelephoneTypeComboBox.SelectedIDs,
                                                                                  TelephoneTypeGroupComboBox.SelectedIDs,
                                                                                  List);
                return result;
        }

        #endregion Methods

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void TelephoneTypeComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            TelephoneTypeGroupComboBox.ItemsSource = CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeIDs(TelephoneTypeComboBox.SelectedIDs);
        }
    }
}
