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
    /// Interaction logic for ADSLInstallmentCostByExpertReportUserControl.xaml
    /// </summary>
    public partial class ADSLInstallmentCostByExpertReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLInstallmentCostByExpertReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            //CenterComboBox.ItemsSource=CenterDB.GetCenterCheckable();

        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<RequestPaymentReport> Result = LoadData();
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

            foreach (RequestPaymentReport requestpaymentreport in Result)
            {
                requestpaymentreport.FichePersianDate = (requestpaymentreport.FicheDate.HasValue) ? Helper.GetPersianDate(requestpaymentreport.FicheDate, Helper.DateStringType.Short).Replace("/", "") : "";
            }

            title = "هزینه نصب و راه اندازی ADSL توسط کارشناس ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<RequestPaymentReport> LoadData()
        {
            long TelNo = !string.IsNullOrWhiteSpace(TelNoTextBox.Text) ? Convert.ToInt64(TelNoTextBox.Text) : -1;
            long amountSum = !string.IsNullOrWhiteSpace(AmountSumTextBox.Text) ? Convert.ToInt64(AmountSumTextBox.Text) : -1;

            List<RequestPaymentReport> result = ReportDB.GetADSLInstallmentCostByExpertList
                                                        (Date.SelectedDate,
                                                         string.IsNullOrEmpty(NameTextBox.Text.Trim()) ? null : (NameTextBox.Text),
                                                         string.IsNullOrEmpty(FicheNumberTextBox.Text.Trim()) ? null : (FicheNumberTextBox.Text),
                                                         TelNo,
                                                        CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                        CityCenterComboBox.CityComboBox.SelectedIDs,
                                                         amountSum);
            return result;
        }

        #endregion Methods

    }
}
