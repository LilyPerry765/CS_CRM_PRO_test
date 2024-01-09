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
    /// Interaction logic for ADSLSoldModemReportUserControl.xaml
    /// </summary>
    public partial class ADSLSoldModemReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLSoldModemReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Initializer

        private void Initialize()
        {
            SellerAgentComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentCheckable();
        }
        #endregion

        #region Methods

        public override void Search()
        {
            List<ADSLModemInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (Todate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(Todate.SelectedDate, Helper.DateStringType.Short).ToString();

            title = "مودم های فروخته شده ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLModemInfo> LoadData()
        {
            DateTime? toDate = null;
            if (Todate.SelectedDate.HasValue)
            {
                toDate = Todate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLModemInfo> result = ReportDB.GetADSLSoldModem(SellerAgentComboBox.SelectedIDs,
                                                                 FromDate.SelectedDate,
                                                                 toDate);
            return result;
        }
        #endregion Methods
    }
}
