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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Enterprise;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PCMContactsStatisticReportUserControl.xaml
    /// </summary>
    public partial class PCMContactsStatisticReportUserControl : Local.ReportBase
    {
        public PCMContactsStatisticReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

        }
        public override void Search()
        {

            Logger.WriteInfo("start search ...");
            List<PostContactInfo> result = LoadData();

            Logger.WriteInfo("Initial StiReport...");
            Stimulsoft.Report.StiReport stiReport = new Stimulsoft.Report.StiReport();

            Logger.WriteInfo("Clear Dictionary...");
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();

            Logger.WriteInfo("RemoveUnusedData Dictionary...");
            stiReport.Dictionary.RemoveUnusedData();

            string title = string.Empty;

            Logger.WriteInfo("GetReportPath ...");
            string path = ReportDB.GetReportPath(UserControlID);

            Logger.WriteInfo("Load path...");
            stiReport.Load(path);

            //stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            Logger.WriteInfo("CacheAllData ...");
            stiReport.CacheAllData = true;

            Logger.WriteInfo("RegData ...");
            stiReport.RegData("result", "result", result);
            stiReport.Dictionary.Variables["Report_Date"].ValueObject = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short);
            Logger.WriteInfo("CompileStandaloneReport ...");


            Logger.WriteInfo("reportViewerForm ...");
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);

            Logger.WriteInfo("ShowDialog ...");
            reportViewerForm.ShowDialog();

        }
        private List<PostContactInfo> LoadData()
        {
            //List<PostContactInfo> result = ReportDB.GetPostContactInfo(CityCenterUC.CenterCheckableComboBox.SelectedIDs);
            //result = result.Where(t => t.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote).ToList();

            List<PostContactInfo> result = ReportDB.GetPCMPostContactInfo(CityCenterUC.CenterCheckableComboBox.SelectedIDs);
            return result;
        }

    }
}
