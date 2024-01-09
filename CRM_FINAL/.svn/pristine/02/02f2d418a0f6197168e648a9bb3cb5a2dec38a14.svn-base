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
using CRM.Data;
using Stimulsoft.Base;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CustomerPersonalInformationReportUserControl.xaml
    /// </summary>
    public partial class CustomerPersonalInformationReportUserControl : Local.ReportBase
    {
        #region Constructor

        public CustomerPersonalInformationReportUserControl()
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
            if (string.IsNullOrEmpty(TelNoTextBox.Text))
            {
                MessageBox.Show("لطفا شماره تلفن مورد نظر خود را وارد بفرمایید");
            }
            else
            {
                CustomerPersonalInfo Result = LoadData();
                List<CustomerPersonalInfo> ResultPayment = LoadPaymentData();
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
                title = "اطلاعات فردی مشترکین";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);
                stiReport.RegData("ResultPayment", "ResultPayment", ResultPayment);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();

            }
        }
    

        private CustomerPersonalInfo LoadData()
        {
            CustomerPersonalInfo Result = new CustomerPersonalInfo();
            long TelNo = string.IsNullOrEmpty(TelNoTextBox.Text) ? -1 : Convert.ToInt64(TelNoTextBox.Text);
            CustomerPersonalInfo result = ReportDB.GetCustomerPersonalLastIDInfo(TelNo);
            Result = ReportDB.GetCustomerPersonalInfo(TelNo);
            if(Result.DayeriTakePossessionStatus.Contains("تخلیه"))
            {
                Result.TakePossessionReason = ReportDB.GetCustomerPersonalInfoTakePossessionReason(TelNo).TakePossessionReason;
            }
            else
            {
                Result.TakePossessionReason = "برقرار می باشد";
            }
            if (ReportDB.GetCustomerPersonalInfoOuterBandMetraj(TelNo) != null)
            {
                Result.MetrajOuterBand = ReportDB.GetCustomerPersonalInfoOuterBandMetraj(TelNo).MetrajOuterBand;
            }
            CustomerPersonalInfo res = ReportDB.GetCustomerPersonalInfoRequestPayment(result.ID);
            
            Result.AmountSum = (res != null )? res.AmountSum :0 ;

            return Result;
        }

        private List<CustomerPersonalInfo> LoadPaymentData()
        {
            List<CustomerPersonalInfo> InstallmentResult = new List<CustomerPersonalInfo>();
            long TelNo = string.IsNullOrEmpty(TelNoTextBox.Text) ? -1 : Convert.ToInt64(TelNoTextBox.Text);
            CustomerPersonalInfo result = ReportDB.GetCustomerPersonalLastIDInfo(TelNo);
            List<CustomerPersonalInfo> Result = ReportDB.GetCustomerPersonalInfoRequestPaymentList(result.ID);
            foreach (CustomerPersonalInfo info in Result)
            {
                if (info.PaymentType.Contains("اقساط"))
                {
                    InstallmentResult = ReportDB.GetCustomerPersonalInfoInstalmentInfo(info.ID);
                }
            }
            return Result.Union(InstallmentResult).ToList();
        }
        #endregion Methods
    }
}
