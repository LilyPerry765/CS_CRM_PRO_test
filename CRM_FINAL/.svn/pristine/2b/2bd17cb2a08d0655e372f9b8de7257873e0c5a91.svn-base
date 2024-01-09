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
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLStatisticReportUserControl.xaml
    /// </summary>
    public partial class ADSLStatisticReportUserControl :Local.ReportBase
    {
        #region Constructor

        public ADSLStatisticReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            RegionIDComboBox.ItemsSource = RegionDB.GetRegions();
        }

        #endregion Initializer

        #region Event Handlers

        private void RegionIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterIdComboBox.ItemsSource = CenterDB.GetCenterCheckableByRegionID(int.Parse((sender as ComboBox).SelectedValue.ToString()));
        }

        #endregion Event Handlers

        #region Methods

        public override void Search()
        {
            List<ADSLStatisticsInfo> result = ReportDB.GetADSLStatistics(string.IsNullOrEmpty(RegionIDComboBox.Text) ? (int?)null : int.Parse((RegionIDComboBox.SelectedValue).ToString()),
                                                                           CenterIdComboBox.SelectedIDs);

            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            
            string title = "ADSL گزارش آماری";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);
              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion  Methods

  
    }
}
