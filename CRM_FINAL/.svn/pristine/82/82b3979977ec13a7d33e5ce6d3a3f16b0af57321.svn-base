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
    /// Interaction logic for RefunDepositReportUserControl.xaml
    /// </summary>
    public partial class RefundDepositReportUserControl : Local.ReportBase
    {
        public RefundDepositReportUserControl()
        {
            InitializeComponent();
        }

        public override void Search()
        {
            List<RefundDepositInfo> Result = LoadData();
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

            stiReport.Dictionary.Variables["fromDate"].Value = Date.GetPersianDate(FromDate.SelectedDate, Date.DateStringType.Short);
            stiReport.Dictionary.Variables["toDate"].Value = Date.GetPersianDate(ToDate.SelectedDate, Date.DateStringType.Short);

            title = "استرداد ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<RefundDepositInfo> LoadData()
        {
            long? fromTelNo = (string.IsNullOrEmpty(FromTelNo.Text) ? -1 : Convert.ToInt64(FromTelNo.Text));
            long? toTelNo = (string.IsNullOrEmpty(ToTelNo.Text) ? -1 : Convert.ToInt64(ToTelNo.Text));
            List<RefundDepositInfo> Result = ReportDB.GetReportDepositInfo(fromTelNo,toTelNo,FromDate.SelectedDate,ToDate.SelectedDate,
                                                                            CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterCheckableComboBox.SelectedIDs);

            return Result;
        }
    }
}
