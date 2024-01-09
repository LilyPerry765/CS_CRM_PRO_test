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
    /// Interaction logic for ZeroStatusReportUserControl.xaml
    /// </summary>
    public partial class BlockingReportUserControl : Local.ReportBase
    {
       #region Constructor

        public BlockingReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
           // ActionTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BlockZeroStatus));
           //  ZeroTypeComboBox.ItemsSource   = Helper.GetEnumCheckable(typeof(DB.ZeroStatus));
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ZeroStatusInfo> Result = LoadData();
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

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

           

            title = "  ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ZeroStatusInfo> LoadData()
        {
            long FromtelNo=(!string.IsNullOrEmpty(FromTelNoTextBox.Text)) ? Convert.ToInt64(FromTelNoTextBox.Text):-1;
            long ToTelNo = (!string.IsNullOrEmpty(ToTelNoTextBox.Text)) ? Convert.ToInt64(ToTelNoTextBox.Text) : -1;


            List<ZeroStatusInfo> result = ReportDB.GetZeroStatusReportInfo(FromDate.SelectedDate,ToDate.SelectedDate,
                                                                            FromtelNo,ToTelNo
                                                                            //,(int?)ActionTypeComboBox.SelectedValue,
                                                                            //(int?)ZeroTypeComboBox.SelectedValue
                                                                            );
           
            return result;
        }

        #endregion Methods
    }
}
