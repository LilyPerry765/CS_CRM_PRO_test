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
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;
using CRM.Data;

namespace CRM.Application.Reports.ReportUserControls
{

    public partial class PAPDischargeRequestsReportUserControl : Local.ReportBase
    {
        #region Properties

        #endregion Peroperties

        #region Constructor

        public PAPDischargeRequestsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPAPRequestStatus));
            PAPComboBox.ItemsSource = Data.PAPInfoDB.GetPAPInfoCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            if (PAPComboBox.SelectedIndex < 0)
                MessageBox.Show(" لطفا نام شرکت را انتخاب کنید");
            else
            {
                List<ADSLPAPRequestInfo> Result = LoadData();
                string title = string.Empty;
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);

                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


                title = "درخواست های تخلیه شرکت های PAP";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        public List<ADSLPAPRequestInfo> LoadData()
        {
            DateTime? toDate = null;

            if (ToDate.SelectedDate != null)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLPAPRequestInfo> result = ReportDB.GetADSLDischargePapRequests((int)PAPComboBox.SelectedValue,FromDate.SelectedDate, toDate, StatusComboBox.SelectedIDs);

            return result;
        }

        #endregion Methods
    }
}
