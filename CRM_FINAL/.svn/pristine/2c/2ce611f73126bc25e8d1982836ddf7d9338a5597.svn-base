using System;
using System.Collections;
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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for InstallPCMReportUserControl.xaml
    /// </summary>
    public partial class InstallPCMReportUserControl : Local.ReportBase
    {
        public InstallPCMReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

        }
        public override void Search()
        {
            IEnumerable result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<PCMStatisticDetails> result = new List<PCMStatisticDetails>();// PCMDB.GetInstallPCMs( FromDate.SelectedDate, ToDate.SelectedDate);

            List<EnumItem> ps = Helper.GetEnumItem(typeof(DB.PCMStatus));
            foreach (PCMStatisticDetails item in result)
            {
                item.InstallDate = Helper.GetPersianDate(DateTime.Parse(item.InstallDate), Helper.DateStringType.Short).ToString();

            }

            return result;//.Where(t => t.BuchtType == (int)DB.BuchtType.CustomerSide || t.BuchtType == (int)DB.BuchtType.InLine).ToList();
        }
    }
}
