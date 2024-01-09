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
using System.Collections;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for SpecialWireCertificatePrintUserControl.xaml
    /// </summary>
    public partial class SpecialWireCertificatePrintUserControl : Local.ReportBase
    {
        public SpecialWireCertificatePrintUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            LoadData();
        }
        private void LoadData()
        {

        }
        public override void Search()
        {
           
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            List<long> RequestID = new List<long>();
            if (!string.IsNullOrEmpty(RequestIDTextBox.Text))
                RequestID.Add(Convert.ToInt64(RequestIDTextBox.Text));
            IEnumerable result = ReportDB.GetSpecialWireCertificatePrint(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs
                                                                       , RequestID.Cast<long?>().ToList()
                                                                       ,null
                                                                       ,null
                                                                       , Convert.ToInt64(TelephoneNoTextBox.Text)
                                                                       , FromDate.SelectedDate
                                                                       , ToDate.SelectedDate);
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            title = "چاپ گواهی سیم خصوصی";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


    }
}
