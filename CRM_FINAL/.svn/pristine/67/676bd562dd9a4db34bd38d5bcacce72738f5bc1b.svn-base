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
    /// Interaction logic for RequestPaymentUserControl.xaml
    /// </summary>
    public partial class RequestPaymentUserControl : Local.ReportBase
    {
        #region Properties
        #endregion properties

        #region Constructor

        public RequestPaymentUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            BaseCostComboBox.ItemsSource = BaseCostDB.GetBaseCostCheckable();
            PaymentWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentWay));
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            BankIDComboBox.ItemsSource = BankDB.GetBanksCheckable();
           
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
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            //List<EnumItem> PaymentTypeList = Helper.GetEnumItem(typeof(DB.PaymentType));
            //List<EnumItem> PaymentWayList = Helper.GetEnumItem(typeof(DB.PaymentWay));
            //List<CheckableItem> BaseCostList = Data.BaseCostDB.GetBaseCostCheckable();
            //List<CheckableItem> BankNameList = Data.BankDB.GetBanksCheckable();

            foreach (RequestPaymentReport requestPaymentReport in Result)
            {
            //    //-----
            //    //adslRequestInfo.ServiceType = adslServiceTypeList.Find(item => item.ID == byte.Parse(adslRequestInfo.ServiceType)).Name;
            //    //-----
            //    if (requestPaymentReport.BankID >= 0)
            //    {
            //        requestPaymentReport.BankName = BankNameList.Find(item => item.ID == requestPaymentReport.BankID).Name;
            //    }
            //    if (requestPaymentReport.PaymentType >= 0)
            //    {
            //        requestPaymentReport.PaymentTypeName = PaymentTypeList.Find(item => item.ID == requestPaymentReport.PaymentType).Name;
            //    }

            //    if (requestPaymentReport.PaymentWay >= 0)
            //    {
            //        requestPaymentReport.PaymentWayName = PaymentWayList.Find(item => item.ID == requestPaymentReport.PaymentWay).Name;

            //    }

            //    if (requestPaymentReport.BaseCostID >= 0)
            //    {
            //        requestPaymentReport.BaseCost = BaseCostList.Find(item => item.ID == requestPaymentReport.BaseCostID).Name;

            //    }
               
            //}

            requestPaymentReport.PersianFicheDate = (requestPaymentReport.FicheDate.HasValue) ? Helper.GetPersianDate(requestPaymentReport.FicheDate, Helper.DateStringType.Short) : "";
        }
            title = " پرداخت ها ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<RequestPaymentReport> LoadData()
        {
            long requestID = !string.IsNullOrWhiteSpace(RequestIDTextBox.Text) ? Convert.ToInt64(RequestIDTextBox.Text) : -1;
            long cost = !string.IsNullOrWhiteSpace(CostTextBox.Text) ? Convert.ToInt64(CostTextBox.Text) : -1;
            int tax = !string.IsNullOrWhiteSpace(TaxTextBox.Text) ? Convert.ToInt32(TaxTextBox.Text) : -1;
            
                List<RequestPaymentReport> result =ReportDB.GetRequestPaymentList
                                                        (FromDate.SelectedDate, ToDate.SelectedDate,
                                                         string.IsNullOrEmpty(BillIDTextBox.Text.Trim()) ? null : (BillIDTextBox.Text),
                                                         string.IsNullOrEmpty(PaymentIDTextBox.Text.Trim()) ? null : (PaymentIDTextBox.Text),
                                                         requestID,
                                                         PaymentWayComboBox.SelectedIDs,
                                                         PaymentTypeComboBox.SelectedIDs,
                                                         BankIDComboBox.SelectedIDs,
                                                         string.IsNullOrEmpty(FicheNumberTextBox.Text.Trim()) ? null : (FicheNumberTextBox.Text),
                                                         cost,
                                                         tax,
                                                         IsPaidCheckBox.IsChecked,
                                                         BaseCostComboBox.SelectedIDs,
                                                         CityCenterComboBox.CityComboBox.SelectedIDs,
                                                         CityCenterComboBox.CenterComboBox.SelectedIDs);
                return result; 
        }

        #endregion Methods

    }
}

