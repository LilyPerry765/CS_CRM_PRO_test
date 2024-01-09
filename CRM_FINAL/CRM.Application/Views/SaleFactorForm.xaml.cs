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
using System.Windows.Shapes;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Views
{
    public partial class SaleFactorForm : Local.RequestFormBase
    {
        #region Properties

        private long _RequestID = 0;
        private long _RequestPaymentID = 0;
        private bool _IsShowFactor = false;
        private bool _IsKickedBack = false;
        RequestInfo _requestInfo;
        List<RequestPayment> _requestPayments = new List<RequestPayment>();

        List<RequestPayment> payments = new List<RequestPayment>();

        #endregion

        #region Constructors

        public SaleFactorForm()
        {
            InitializeComponent();
        }

        public SaleFactorForm(long requestID)
            : this()
        {
            _RequestID = requestID;
            Initialize();
        }
        public SaleFactorForm(long requestID, List<RequestPayment> requestPayments, bool isKickedBack = false)
            : this()
        {
            _IsKickedBack = isKickedBack;
            _requestPayments = requestPayments;
            _RequestID = requestID;
            Initialize();
        }
        public SaleFactorForm(long requestID, long requestPaymentID)
            : this()
        {

            _RequestID = requestID;
            _RequestPaymentID = requestPaymentID;
            _IsShowFactor = true;
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CostTitleColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();
            PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));

            ActionIDs = new List<byte> { (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }

        private void LoadData()
        {
            _requestInfo = RequestDB.GetRequestInfoByID(_RequestID);

            CustomerNameTextBox.Text = _requestInfo.CustomerName;
            RequestTypeTextBox.Text = _requestInfo.RequestTypeName;
            RequestIDTextBox.Text = _requestInfo.ID.ToString();
            InsertDateTextBox.Text = _requestInfo.InsertDate;
            PrintDateTextBox.Text = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.DateTime);
            TelephoneNoTextBox.Text = _requestInfo.TelephoneNo.ToString();
            CenterTextBox.Text = _requestInfo.CenterName;

            if (!_IsKickedBack)
            {
                if (_IsShowFactor)
                {
                    RequestPayment payment = RequestPaymentDB.GetRequestPaymentByID(_RequestPaymentID);
                    //if (string.IsNullOrWhiteSpace(payment.BillID) || string.IsNullOrWhiteSpace(payment.PaymentID))
                    //{
                    //    Folder.MessageBox.ShowError("برای این پرداخت فاکتور صادر نشده است !");
                    //    return;
                    //}
                    BillIDTextBox.Text = payment.BillID;
                    PaymentIDTextBox.Text = payment.PaymentID;

                    List<RequestPayment> payments = RequestPaymentDB.GetPaymentsbyPaymentID(payment.BillID, payment.PaymentID);
                    ItemsDataGrid.ItemsSource = payments;
                }
                else
                {
                    bool hasBillID = false;
                    long sumAmount = 0;



                    if (DB.IsFixRequest(_requestInfo.RequestTypeID))
                    {
                        FactorNumberLabel.Visibility = Visibility.Visible;
                        FactorNumberTextBox.Visibility = Visibility.Visible;

                        if (_requestPayments.Count() == 0)
                            payments = RequestPaymentDB.GetNoPaidRequestPaymentByRequestIDByPaymentType(_RequestID, new List<int> { (int)DB.PaymentType.Cash, (int)DB.PaymentType.NonForcedCash });
                        else
                            payments = _requestPayments;

                        if (payments.Count() != 0)
                            FactorNumberTextBox.Text = payments.Take(1).SingleOrDefault().FactorNumber.ToString();

                        sumAmount = _requestPayments.Sum(t => t.AmountSum) ?? 0;
                    }
                    else
                    {
                        payments = RequestPaymentDB.GetNoPaidRequestPaymentByRequestID(_RequestID, (int)DB.PaymentType.Cash);
                        sumAmount = RequestPaymentDB.GetAmountSumforAllPayment(_RequestID, (int)DB.PaymentType.Cash);
                    }

                    ItemsDataGrid.ItemsSource = payments;

                    hasBillID = RequestPaymentDB.GetNoPaidRequestPaymentHasBillID(_RequestID, (int)DB.PaymentType.Cash);


                    CostSumTextBox.Text = sumAmount.ToString();

                    string billID = "";
                    string paymentID = "";

                    if (sumAmount != 0)
                    {
                        if (DB.IsFixRequest(_requestInfo.RequestTypeID))
                        {
                            paymentID = billID = "0";

                            List<RequestPayment> Allpayments = new List<RequestPayment>();
                            int maxFactor = (Data.RequestPaymentDB.GetMaxFactorNumber(_RequestID) ?? 0) + 1;
                            foreach (RequestPayment currenetPayment in payments)
                            {
                                currenetPayment.BillID = billID;
                                currenetPayment.PaymentID = paymentID;
                                currenetPayment.FactorNumber = maxFactor;
                                currenetPayment.Detach();
                                Allpayments.Add(currenetPayment);

                            }

                            DB.UpdateAll(Allpayments);
                            FactorNumberTextBox.Text = maxFactor.ToString();
                        }
                        else
                        {
                            if (_requestInfo.TelephoneNo != null)
                            {
                                billID = DB.GenerateBillID((long)_requestInfo.TelephoneNo, _requestInfo.CenterID, (byte)DB.SubsidiaryCodeType.ADSL);
                                paymentID = DB.GeneratePaymentID(sumAmount, (long)_requestInfo.TelephoneNo, billID, (byte)DB.SubsidiaryCodeType.ADSL, hasBillID);
                            }
                            else
                            {
                                billID = paymentID = "0";
                            }

                            foreach (RequestPayment currenetPayment in payments)
                            {
                                currenetPayment.BillID = billID;
                                currenetPayment.PaymentID = paymentID;

                                currenetPayment.Detach();
                                Save(currenetPayment);
                            }
                        }
                    }

                    BillIDTextBox.Text = billID;
                    PaymentIDTextBox.Text = paymentID;
                }
            }
            else
            {
                this.Title = "فاکتور باز پرداخت";
                PrintDateLabel.Content = "تاریخ فاکتور بازپرداخت";
                List<RequestPayment> payments = new List<RequestPayment>();
                long sumAmount = 0;

                FactorNumberLabel.Visibility = Visibility.Visible;
                FactorNumberTextBox.Visibility = Visibility.Visible;

                if (_requestPayments.Count() == 0)
                    payments = RequestPaymentDB.GetPaidedRequestPaymentByRequestIDByPaymentType(_RequestID, new List<int> { (int)DB.PaymentType.Cash, (int)DB.PaymentType.NonForcedCash });
                else
                    payments = _requestPayments;

                if (payments.Count() != 0)
                    FactorNumberTextBox.Text = payments.Take(1).SingleOrDefault().FactorNumber.ToString();

                sumAmount = _requestPayments.Sum(t => t.AmountSum) ?? 0;

                ItemsDataGrid.ItemsSource = payments;
                CostSumTextBox.Text = sumAmount.ToString();

                ActionIDs = new List<byte> { (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.KickedBack };
            }
        }

        public override bool Print()
        {
            try
            {
                SaleFactor SFTemp = new SaleFactor();
                SFTemp.RequestType = RequestTypeTextBox.Text;
                SFTemp.RequestID = RequestIDTextBox.Text;
                SFTemp.InsertDate = InsertDateTextBox.Text;
                SFTemp.PrintDate = PrintDateTextBox.Text;
                SFTemp.CustomerName = CustomerNameTextBox.Text;
                SFTemp.TelephoneNo = TelephoneNoTextBox.Text;
                SFTemp.Center = CenterTextBox.Text;
                SFTemp.CostSum = CostSumTextBox.Text;
                SFTemp.BillID = BillIDTextBox.Text;
                SFTemp.PaymentID = PaymentIDTextBox.Text;
                SFTemp.BarCode = DB.GenerateBarCode(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim());
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();
                string title = string.Empty;
                string path = ReportDB.GetReportPath((int)DB.UserControlNames.SaleFactor);
                stiReport.Load(path);

                List<Data.RequestPaymentList> RPTemp = new List<Data.RequestPaymentList>();
                if (DB.IsFixRequest(_requestInfo.RequestTypeID))
                {

                    if (_requestPayments.Count() == 0)
                    {
                        if (!_IsKickedBack)
                        {
                            RPTemp = RequestPaymentDB.GetRequestPaymentListByPaymentTypes(_RequestID, new List<int> { (int)DB.PaymentType.Cash, (int)DB.PaymentType.NonForcedCash });
                        }
                        else
                        {
                            RPTemp = RequestPaymentDB.GetIsPaidRequestPaymentListByPaymentTypes(_RequestID, new List<int> { (int)DB.PaymentType.Cash, (int)DB.PaymentType.NonForcedCash });

                        }
                    }
                    else
                    {
                        RPTemp = RequestPaymentDB.GetReqeustPaymentListBylistPament(_requestPayments);
                    }

                }
                else
                    RPTemp = RequestPaymentDB.GetRequestPaymentList(_RequestID, (byte)DB.PaymentType.Cash);

                List<EnumItem> PaymentTypeName = Helper.GetEnumItem(typeof(DB.PaymentType));
                foreach (Data.RequestPaymentList item in RPTemp)
                {
                    item.PaymentType = string.IsNullOrEmpty(item.PaymentType) ? "" : PaymentTypeName.Find(t => t.ID == byte.Parse(item.PaymentType)).Name;
                }

                stiReport.RegData("result", "result", SFTemp);
                stiReport.RegData("RequestPaymentList", "RequestPaymentList", RPTemp);

                if (_IsKickedBack)
                {
                    stiReport.Dictionary.Variables["DateFactorName"].Value = PrintDateLabel.Content.ToString();
                }
                else
                {
                    stiReport.Dictionary.Variables["DateFactorName"].Value = PrintDateLabel.Content.ToString();
                }


                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            catch (Exception ex)
            {
            }
            return base.Print();
        }


        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public override bool KickedBack()
        {
            try
            {
                payments = ItemsDataGrid.ItemsSource.Cast<RequestPayment>().ToList();
                payments.ForEach(t => { t.IsKickedBack = true; t.Detach(); });
                DB.UpdateAll(payments);
                IsKickedBackSuccess = true;
            }
            catch
            {
                IsKickedBackSuccess = false;
            }

            return IsKickedBackSuccess;
        }

        #endregion
    }
}
