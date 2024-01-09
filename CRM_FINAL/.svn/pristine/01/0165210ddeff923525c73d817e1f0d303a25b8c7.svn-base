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
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLEquipmentReportUserControl.xaml
    /// </summary>
    public partial class ADSLEquipmentReportUserControl : Local.ReportBase
    {
        #region Properties And Fields


        #endregion Properties And Fields

        #region Constructor
        public ADSLEquipmentReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer
        private void Initialize()
        {
            EquipmentInstallLocationComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLEquimentLocationInstall));
            ProductCompanyComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLEquimentProduct));
            AAATypeComboBox.ItemsSource = ADSLAAATypeDB.GetADSLAAATypeCheckable();
            EquipmentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLEquimentType));
        }
        #endregion Initializer

        #region Event Handler

        #endregion Event Handler

        #region Methods
        
        public override void Search()
        {
            string Title = string.Empty;
            string path;
            List<ADSLEquipmentInfo> result = ReportDB.GetADSLEquipments(string.IsNullOrEmpty(RegionCenterComboBox.RegionComboBox.Text) ? (int?)null : int.Parse(RegionCenterComboBox.RegionComboBox.SelectedValue.ToString())
                , string.IsNullOrEmpty(RegionCenterComboBox.CenterComboBox.Text) ? (int?)null : int.Parse(RegionCenterComboBox.CenterComboBox.SelectedValue.ToString())
                , EquipmentTypeComboBox.SelectedIDs
                , EquipmentInstallLocationComboBox.SelectedIDs
                , ProductCompanyComboBox.SelectedIDs
                , AAATypeComboBox.SelectedIDs).ToList();

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TotalCount"].Value = "8";
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;
            Title = "ADSL گزارش تجهیزات ";
            stiReport.Dictionary.Variables["Header"].Value = Title;
            stiReport.RegData("result", "result", result);
              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        
        #endregion Methods


    }
}
