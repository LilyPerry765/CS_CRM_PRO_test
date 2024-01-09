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
    /// Interaction logic for InstallRequestReportUserControl.xaml
    /// </summary>
    public partial class InstallRequestReportUserControl : Local.ReportBase
    {
       #region Constructor

        public InstallRequestReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            ChargingTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChargingGroup));
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
            PosessionTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PossessionType));
            OrderTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.OrderType));
            CityComboBox.ItemsSource = CityDB.GetAllCity();
        }


        #endregion Initializer

        #region Event Handlers

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)CityComboBox.SelectedValue);

        }

        private void Center_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void TelephoneTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    if (TelephoneTypeComboBox.SelectedValue != null)
        //    {
        //        TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
        //    }
        //}
        #endregion

        #region Methods

        public override void Search()
        {
            List<InstallRequestInfo> Result = LoadData();
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

           

            title = " دایری ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<InstallRequestInfo> LoadData()
        {

            long FromtelNo=(!string.IsNullOrEmpty(FromTelNoTextBox.Text)) ? Convert.ToInt64(FromTelNoTextBox.Text):-1;
            long ToTelNo = (!string.IsNullOrEmpty(ToTelNoTextBox.Text)) ? Convert.ToInt64(ToTelNoTextBox.Text) : -1;
           long TelNo = (!string.IsNullOrEmpty(TelNoTextBox.Text)) ? Convert.ToInt64(TelNoTextBox.Text) : -1;

            List<CounterLastInfo> CounterLastInfo = ReportDB.GetCounterLast();

            List<InstallRequestInfo> Result = ReportDB.GetDayeriInstallRequestInfo(  FromDate.SelectedDate, ToDate.SelectedDate,
                                                                                     FromtelNo, ToTelNo,
                                                                                     (int?)TelephoneTypeComboBox.SelectedValue,
                                                                                     (int?)TelephoneTypeGroupComboBox.SelectedValue,
                                                                                     (int?)ChargingTypecomboBox.SelectedValue,
                                                                                     (int?) OrderTypecomboBox.SelectedValue,
                                                                                     (int?) PosessionTypecomboBox.SelectedValue,
                                                                                     TelNo,
                                                                                     (int?)CenterComboBox.SelectedValue
                                                                                     );

            foreach (InstallRequestInfo Info in Result)
            {
                for (int i = 0; i < CounterLastInfo.Count; i++)
                {
                    if (Info.TelephoneNo == CounterLastInfo[i].TelephonNo.ToString())
                    {
                        Info.CounterNo = CounterLastInfo[i].CounterNo.ToString();
                    }
                }

            }
            return Result;
        }

        private void TelephoneTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneTypeComboBox.SelectedValue != null)
            {
                TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
            }
        }

        #endregion Methods






    }
}
