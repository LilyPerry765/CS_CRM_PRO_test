using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for BlackListCustomerReportUserControl.xaml
    /// </summary>
    public partial class BlackListCustomerReportUserControl : Local.ReportBase
    {

        public BlackListCustomerReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

        }
        public override void Search()
        {
            List<BlackListCustomerReportInfo> result = ReportDB.GetBlackListCustomerInfo( FromDate.SelectedDate, ToDate.SelectedDate, NationalCodeTextBox.Text.Trim() , AllCheckBox.IsChecked);

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            string title = string.Empty;
            title = " لیست سیاه مشترکین  ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
    }
}
