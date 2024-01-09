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
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for AllPCMsReportUserControl.xaml
    /// </summary>
    public partial class AllPCMsReportUserControl : Local.ReportBase
    {
        public AllPCMsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
          
        }
        public override void Search()
        {
            List<PCMInfoReport> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = "گزارش همه پی سی ام ها";


            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.CacheAllData = true;


            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);
            stiReport.CompileStandaloneReport("", StiStandaloneReportType.ShowWithWpf);
              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private List<PCMInfoReport> LoadData()
        {
            List<PCMInfoReport> result = ReportDB.GetPCM(CityCenterUC.CenterCheckableComboBox.SelectedIDs);
            List<EnumItem> ActionEnumTemp = Helper.GetEnumItem(typeof(DB.ActionLog));
            List<Center> CenterTemp = CenterDB.GetAllCenter();
            foreach (PCMInfoReport item in result)
            {
                item.CenterName = (string.IsNullOrEmpty(item.CenterName)  || item.CenterName == "0") ? "نامشخص" : CenterTemp.Find(i => i.ID == int.Parse(item.CenterName)).CenterName;
                item.strPCMActionDate = Helper.GetPersianDate(item.PCMActionDate, Helper.DateStringType.Short);
                item.PCMActionTime = Helper.GetPersianDate(item.PCMActionDate, Helper.DateStringType.Time);
                item.PCMActionName = (string.IsNullOrEmpty(item.PCMActionID.ToString()) || item.PCMActionID.ToString() == "0") ? "نامشخص" : ActionEnumTemp.Find(i => i.ID == item.PCMActionID).Name;
            }
            return result;
        }
    }
}
