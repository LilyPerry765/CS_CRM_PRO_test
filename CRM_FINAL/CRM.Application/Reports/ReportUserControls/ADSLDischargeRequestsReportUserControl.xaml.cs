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
    /// Interaction logic for ADSLDischargeRequestsReportUserControl.xaml
    /// </summary>
    public partial class ADSLDischargeRequestsReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLDischargeRequestsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLDischargeInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            if (fromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

            title = "تعداد درخواست هاي تخليه  ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLDischargeInfo> LoadData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLDischargeInfo> result = ReportDB.GetADSLDischargeInfo(fromDate.SelectedDate, ToDate);

            return result;
        }

        #endregion Methods
    }
}
