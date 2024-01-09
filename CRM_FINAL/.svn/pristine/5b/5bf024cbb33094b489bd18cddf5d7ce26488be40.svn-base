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
    /// Interaction logic for ADSLTelephoneNoHistoryReportUserControl.xaml
    /// </summary>
    public partial class ADSLTelephoneNoHistoryReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion Properties

        #region Constructor

        public ADSLTelephoneNoHistoryReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            //CentersComboBox.ItemsSource = ADSLTelephoneHistoryDB.GetADSLTelNoCentersReport();
            PAPInfoComboBox.ItemsSource = ADSLTelephoneHistoryDB.GetADSLTelNoPAPInfoReport();
        }

        #endregion Intitializer

    #region Methods

        public override void Search()
        {
            if (PAPInfoComboBox.SelectedIndex < 0)
                MessageBox.Show("لطفا شرکت PAP مورد نظر را انتخاب بفرمایید.");
            else
            {
                List<ADSLTelephoneHistoryInfo> Result = LoadData();
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
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

                foreach (ADSLTelephoneHistoryInfo adsltelHistoryInfo in Result)
                {
                    adsltelHistoryInfo.InstalDate = (adsltelHistoryInfo.InstallDate_date.HasValue) ? Helper.GetPersianDate(adsltelHistoryInfo.InstallDate_date, Helper.DateStringType.Short) : "";
                }

                title = "گزارش تاریخچه شماره تلفن های ADSL ";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
         }

        private List<ADSLTelephoneHistoryInfo> LoadData()
        {
            long TelNumber = !string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text) ? Convert.ToInt64(TelephoneNoTextBox.Text) : -1;
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTelephoneHistoryInfo> result = ReportDB.GetADSLTelNoInfoList
                                                    (TelNumber,
                                                    (int?)PAPInfoComboBox.SelectedValue,
                                                    CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                    CityCenterComboBox.CityComboBox.SelectedIDs,
                                                    FromDate.SelectedDate,
                                                    toDate);
            return result;
        }
#endregion Methods
}
}
