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
    /// Interaction logic for ADSLPaymentReportUserControl.xaml
    /// </summary>
    public partial class ADSLPaymentReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLPaymentReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            //CenterComboBox.ItemsSource=CenterDB.GetCenterCheckable();
            ServiceComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentServiceType));

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
            stiReport.Dictionary.Variables["Date"].Value = Helper.GetPersianDate(Date.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            long TelNo = !string.IsNullOrWhiteSpace(TelNoTextBox.Text) ? Convert.ToInt64(TelNoTextBox.Text) : -1;
            long amountSum = !string.IsNullOrWhiteSpace(AmountSumTextBox.Text) ? Convert.ToInt64(AmountSumTextBox.Text) : -1;

            List<RequestPaymentReport> Information = ReportDB.GetRequestIDs(Date.SelectedDate,
                                                         string.IsNullOrEmpty(NameTextBox.Text.Trim()) ? null : (NameTextBox.Text),
                                                         string.IsNullOrEmpty(FicheNumberTextBox.Text.Trim()) ? null : (FicheNumberTextBox.Text),
                                                         TelNo,
                                                         CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                         CityCenterComboBox.CityComboBox.SelectedIDs,
                                                         ServiceComboBox.SelectedIDs,
                                                         amountSum);
            foreach (RequestPaymentReport requestpaymentreport in Result)
            {

                requestpaymentreport.FichePersianDate = (requestpaymentreport.FicheDate.HasValue) ? Helper.GetPersianDate(requestpaymentreport.FicheDate, Helper.DateStringType.Short).Replace("/", "") : "";
                for (int i = 0; i < Information.Count; i++)
                {
                    if (requestpaymentreport.RequestID == Information[i].RequestID)
                    {
                        requestpaymentreport.Bandwidth = Information[i].Bandwidth;
                        requestpaymentreport.Duration = Information[i].Duration;
                        requestpaymentreport.IpDuration = Information[i].IpDuration;
                        if (Information[i].OldServiceID == Information[i].NewServiceID)
                            Information[i].PaymentServiceType = DB.GetEnumDescriptionByValue(typeof(DB.PaymentServiceType), 2);
                        else if (Information[i].OldServiceID != Information[i].NewServiceID)
                            Information[i].PaymentServiceType = DB.GetEnumDescriptionByValue(typeof(DB.PaymentServiceType), 1);
                        else if (Information[i].AdditionalTrafficID != null)
                            Information[i].PaymentServiceType = DB.GetEnumDescriptionByValue(typeof(DB.PaymentServiceType), 3);
                    }
                }
            }

            title = " پرداخت های ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<RequestPaymentReport> LoadData()
        {
            long TelNo = !string.IsNullOrWhiteSpace(TelNoTextBox.Text) ? Convert.ToInt64(TelNoTextBox.Text) : -1;
            long amountSum = !string.IsNullOrWhiteSpace(AmountSumTextBox.Text) ? Convert.ToInt64(AmountSumTextBox.Text) : -1;

            List<RequestPaymentReport> result = ReportDB.GetADSLPaymentList
                                                    (Date.SelectedDate,
                                                     string.IsNullOrEmpty(NameTextBox.Text.Trim()) ? null : (NameTextBox.Text),
                                                     string.IsNullOrEmpty(FicheNumberTextBox.Text.Trim()) ? null : (FicheNumberTextBox.Text),
                                                     TelNo,
                                                     CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                     CityCenterComboBox.CityComboBox.SelectedIDs,
                                                     ServiceComboBox.SelectedIDs,
                                                     amountSum);
            return result;
        }

        #endregion Methods

    }
}

