using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CabinetCapacityReportUserControl.xaml
    /// </summary>
    public partial class CabinetCapacityReportUserControl : Local.ReportBase
    {
        public CabinetCapacityReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CabinetCapacityComboBox.ItemsSource = CabinetTypeDB.GetAllCabinetType(); //Helper.GetEnumItem(typeof(DB.TimeTable));
                

                

            }
        }
        public override void Search()
        {
            IEnumerable result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            string CenterIds = string.Empty;
            string RegionIds = string.Empty;

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();







            stiReport.RegData("result", "result", result);

              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private List<CabinetCapacity> LoadData()
        {
            List<CabinetCapacity> result = ReportDB.GetCabinetCapacity(string.IsNullOrEmpty(CabinetCapacityComboBox.Text) ? (int?)null : int.Parse(CabinetCapacityComboBox.SelectedValue.ToString()));
            return result;
        }
    }
}
