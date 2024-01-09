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
    /// Interaction logic for ChangeNumberReportUserControl.xaml
    /// </summary>
    public partial class Failure117RequestRemaindInNetworkReportUserControl : Local.ReportBase
    {
        #region Properties And Fields
        #endregion  Properties And Fields

        #region Constructor

        public Failure117RequestRemaindInNetworkReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            RegionIdComboBox.ItemsSource = RegionDB.GetRegions();
           
        }
        #endregion  Initializer

        #region Event Handlers
        
        private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterIdComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)((sender as ComboBox).SelectedValue));
        }

        #endregion  Event Handlers

        #region Methods

        public override void Search()
        {
            List<uspReportFailure117RequestRemaindInNetworkResult> result = LoadData();
            string title = string.Empty;
            string path;

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                       
            
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<uspReportFailure117RequestRemaindInNetworkResult> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
                toDate = ToDate.SelectedDate.Value.AddDays(1);

            List<uspReportFailure117RequestRemaindInNetworkResult> result = ReportDB.GetFailure117RequestRemaindInNetworkInfo(FromDate.SelectedDate,
                                                                                                                              toDate,
                                                                                                                              string.IsNullOrEmpty(RegionIdComboBox.Text) ? (int?)null : int.Parse(RegionIdComboBox.SelectedValue.ToString()),
                                                                                                                              string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString()));
            return result;
        }
        #endregion  Methods
        
    }
}
