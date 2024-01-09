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
    /// Interaction logic for InstalmentRequestReportUserControl.xaml
    /// </summary>
    public partial class InstalmentRequestReportUserControl : Local.ReportBase
    {
       #region Constructor

        public InstalmentRequestReportUserControl()
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
            List<InstalmentRequestPaymentInfo > Result = LoadData();
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

            if (fromStartDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromStartDate"].Value = Helper.GetPersianDate(fromStartDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toStartDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toStartDate"].Value = Helper.GetPersianDate(toStartDate.SelectedDate, Helper.DateStringType.Short).ToString();

            if (fromEndDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromEndDate"].Value = Helper.GetPersianDate(fromEndDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toEndDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toEndDate"].Value = Helper.GetPersianDate(toEndDate.SelectedDate, Helper.DateStringType.Short).ToString();

          
            title = "اقساط";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<InstalmentRequestPaymentInfo> LoadData()
        {
            long fromTel=(!string.IsNullOrEmpty(fromTelNo.Text)) ? Convert.ToInt64(fromTelNo.Text):-1;
            long toTel=(!string.IsNullOrEmpty(toTelNo.Text)) ? Convert.ToInt64(toTelNo.Text) :-1;

            List<InstalmentRequestPaymentInfo> Result = ReportDB.GetInstalmentRequestPaymentInfo(fromStartDate.SelectedDate, toStartDate.SelectedDate,
                                                                                                fromEndDate.SelectedDate, toEndDate.SelectedDate,
                                                                                                fromTel, toTel);

            return Result;
        }

        #endregion Methods
    }
}
