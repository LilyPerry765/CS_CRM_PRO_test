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
    /// Interaction logic for ADSLBandwidthReportUserControl.xaml
    /// </summary>
    public partial class ADSLBandwidthReportUserControl : Local.ReportBase
    {
        #region Constructor
        public ADSLBandwidthReportUserControl()
        {
            InitializeComponent();
             Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {

           
            //BandwidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            //TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
            //DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
            ADSLBandwidth Result = LoadData();
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

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (Todate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(Todate.SelectedDate, Helper.DateStringType.Short).ToString();

            title = "پهنای باند ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private ADSLBandwidth LoadData()
        {
            DateTime? toDate = null;
            if (Todate.SelectedDate.HasValue)
            {
                toDate = Todate.SelectedDate.Value.AddDays(1);
            }
           ADSLBandwidth result = ReportDB.GetADSLBandwidthInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                FromDate.SelectedDate,
                                                                toDate);
           return result;
        }
        #endregion Methods
    }
}
