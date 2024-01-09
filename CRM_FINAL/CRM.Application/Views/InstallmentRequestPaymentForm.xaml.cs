using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Views
{
    public partial class InstallmentRequestPaymentForm : Local.RequestFormBase
    {
        #region Properties

        private List<InstallmentRequestPayment> _installmentRequestPayments { get; set; }
        private RequestPayment _requestPayment { get; set; }
        private long _RequestPaymentID = 0;
        private const int _floorValue = 1000;
        private int _RequestTypeID = 0;
        private int? _ServiceDuration = 0;

        #endregion

        #region Constructors

        public InstallmentRequestPaymentForm()
        {
            InitializeComponent();
        }

        public InstallmentRequestPaymentForm(long requestPaymentID)
            : this()
        {
            _RequestPaymentID = requestPaymentID;

            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (_RequestPaymentID != 0)
            {
                _RequestTypeID = RequestPaymentDB.GetRequestTypeIDbyRequestPaymentID(_RequestPaymentID);

                if (_RequestTypeID == (byte)DB.RequestType.ADSL || _RequestTypeID == (byte)DB.RequestType.Wireless)
                {
                    StartDatePicker.IsEnabled = false;
                    EndDatePicker.IsEnabled = false;
                    PrePaymentAmountTextBox.IsEnabled = false;
                }
                else
                {
                    EndDatePicker.IsEnabled = false;
                }
            }

            ActionIDs = new List<byte> { (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }

        private void LoadData()
        {
            _requestPayment = Data.RequestPaymentDB.GetRequestPaymentByID(_RequestPaymentID);
            _installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(_RequestPaymentID);

            if (_requestPayment.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID || _requestPayment.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
            {
                int? serviceID = null;
                if (_RequestTypeID == (byte)DB.RequestType.Wireless)
                {
                    WirelessRequest wirelessRequest = WirelessRequestDB.GetWirelessRequestByID(_requestPayment.RequestID);
                    serviceID = wirelessRequest.ServiceID;
                }
                else
                {
                    ADSLRequest aDSLRequest = ADSLRequestDB.GetADSLRequestByID(_requestPayment.RequestID);
                    serviceID = aDSLRequest.ServiceID;
                }



                if (serviceID != null)
                {
                    _ServiceDuration = ADSLServiceDB.GetADSLServiceDurationByServiceID((int)serviceID);
                    InstallmentCountTextBox.Text = ADSLServiceDB.GetADSLServiceDurationTitleByServiceID((int)serviceID);
                    StartDatePicker.SelectedDate = DB.GetServerDate();
                    EndDatePicker.SelectedDate = DB.GetServerDate().AddMonths(Convert.ToInt32(InstallmentCountTextBox.Text.Trim()));
                }

                if (_installmentRequestPayments.Count == 0)
                    GenerateInstalments(true);
            }


            BaseCost baseCost = Data.BaseCostDB.GetBaseCostByID(_requestPayment.BaseCostID ?? 0);
            _installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(_RequestPaymentID);

            if (baseCost != null && baseCost.IsCheque == false)
                IsChequeCheckBox.IsEnabled = false;

            if (_installmentRequestPayments.Count > 0)
            {
                InstallmentCountTextBox.Text = _installmentRequestPayments.Count.ToString();
                StartDatePicker.SelectedDate = _installmentRequestPayments.OrderBy(t => t.StartDate).FirstOrDefault().StartDate;
                InstallmentGroupBox.IsEnabled = false;
                bool isCheque = _installmentRequestPayments.Any(t => t.IsCheque == true);
                IsChequeCheckBox.IsChecked = isCheque;

                if (isCheque == true)
                    ChequeNumberColumn.Visibility = Visibility.Visible;
                else
                    ChequeNumberColumn.Visibility = Visibility.Collapsed;
            }
            else
            {
                InstallmentGroupBox.IsEnabled = true;
                PrePaymentAmountTextBox.Text = "0";
            }

            SumOfInstallment.Text = _installmentRequestPayments.Sum(t => t.Cost).ToString();

            InstallmentRequestPaymentDataGrid.ItemsSource = _installmentRequestPayments;
        }

        public void GenerateInstalments(bool daily)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
                int PaymentAmountEachPart = 0;

                long prePaymentAmount = 0;
                if (!string.IsNullOrWhiteSpace(PrePaymentAmountTextBox.Text.Trim()))
                    prePaymentAmount = Convert.ToInt64(PrePaymentAmountTextBox.Text.Trim());

                int installmentCount = 0;
                if (!string.IsNullOrWhiteSpace(InstallmentCountTextBox.Text.Trim()))
                    installmentCount = Convert.ToInt32(InstallmentCountTextBox.Text.Trim());

                if (_RequestTypeID == (byte)DB.RequestType.ADSL)
                {
                    string startDate = StartDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                    string endateCount = Helper.AddMonthToPersianDate(startDate, installmentCount);
                    //string endDate = EndDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                    string endDate = Helper.AddMonthToPersianDate(startDate, installmentCount);

                    decimal installmentPayment = (decimal)_requestPayment.AmountSum - (decimal)prePaymentAmount;

                    string startDateEachPart = startDate;

                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(installmentPayment / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(installmentPayment / (decimal)installmentCount);

                    if (prePaymentAmount > 0)
                    {
                        RequestPayment requestPayment = new RequestPayment();
                        requestPayment.InsertDate = (DateTime)DB.ServerDate();
                        requestPayment.BaseCostID = (int)DB.SpecialCostID.PrePaymentTypeCostID;
                        requestPayment.RequestID = _requestPayment.RequestID;
                        requestPayment.RelatedRequestPaymentID = _requestPayment.ID;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.AmountSum = prePaymentAmount;
                        requestPayment.Cost = 0;
                        requestPayment.Tax = 0;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsPaid = false;
                        DB.Save(requestPayment);
                    }

                    //int count = installmentCount;

                    for (int i = 1; i <= installmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = _requestPayment.ID;
                        installmentRequestPayment.TelephoneNo = RequestPaymentDB.GetTelephoneNobyRequestPaymentID(_requestPayment.ID);
                        installmentRequestPayment.IsCheque = (bool)IsChequeCheckBox.IsChecked;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;
                        
                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (installmentCount == i)
                            installmentRequestPayment.Cost = (long)(installmentPayment - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);
                        
                        installmentRequestPayments.Add(installmentRequestPayment);
                    }

                    //for (int i = 1; i <= _ServiceDuration; i++)
                    //{
                    //    InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                    //    int dateEachPart = 1;
                    //    installmentRequestPayment.RequestPaymentID = _requestPayment.ID;
                    //    installmentRequestPayment.IsCheque = (bool)IsChequeCheckBox.IsChecked;

                    //    installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                    //    string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                    //    installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;

                    //    if (daily == true)
                    //        dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                    //    if (installmentCount == i)
                    //        installmentRequestPayment.Cost = (long)(installmentPayment - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                    //    else
                    //        installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                    //    startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);

                    //    count = count - 1;

                    //    if (count == 0)
                    //    {
                    //        installmentRequestPayment.Cost = (long)installmentPayment - installmentRequestPayments.Sum(t => t.Cost);
                    //        installmentRequestPayments.Add(installmentRequestPayment);
                    //        break;
                    //    }

                    //    installmentRequestPayments.Add(installmentRequestPayment);
                    //}
                }
                if (_RequestTypeID == (byte)DB.RequestType.Wireless)
                {
                    string startDate = StartDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                    string endateCount = Helper.AddMonthToPersianDate(startDate, installmentCount);
                    //string endDate = EndDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                    string endDate = Helper.AddMonthToPersianDate(startDate, installmentCount);

                    decimal installmentPayment = (decimal)_requestPayment.AmountSum - (decimal)prePaymentAmount;

                    string startDateEachPart = startDate;

                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(installmentPayment / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(installmentPayment / (decimal)installmentCount);

                    if (prePaymentAmount > 0)
                    {
                        RequestPayment requestPayment = new RequestPayment();
                        requestPayment.InsertDate = (DateTime)DB.ServerDate();
                        requestPayment.BaseCostID = (int)DB.SpecialCostID.PrePaymentTypeCostID;
                        requestPayment.RequestID = _requestPayment.RequestID;
                        requestPayment.RelatedRequestPaymentID = _requestPayment.ID;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.AmountSum = prePaymentAmount;
                        requestPayment.Cost = 0;
                        requestPayment.Tax = 0;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsPaid = false;
                        DB.Save(requestPayment);
                    }

                    //int count = installmentCount;

                    for (int i = 1; i <= installmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = _requestPayment.ID;
                        installmentRequestPayment.TelephoneNo = RequestPaymentDB.GetTelephoneNobyRequestPaymentID(_requestPayment.ID);
                        installmentRequestPayment.IsCheque = (bool)IsChequeCheckBox.IsChecked;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;

                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (installmentCount == i)
                            installmentRequestPayment.Cost = (long)(installmentPayment - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);

                        installmentRequestPayments.Add(installmentRequestPayment);
                    }
                }
                else
                {
                    string startDate = StartDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                    string endDate = Helper.AddMonthToPersianDate(startDate, installmentCount);

                    decimal installmentPayment = (decimal)_requestPayment.AmountSum - (decimal)prePaymentAmount;

                    string startDateEachPart = startDate;

                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(installmentPayment / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(installmentPayment / (decimal)installmentCount);

                    if (prePaymentAmount > 0)
                    {
                        RequestPayment requestPayment = new RequestPayment();
                        requestPayment.InsertDate = (DateTime)DB.ServerDate();
                        requestPayment.BaseCostID = (int)DB.SpecialCostID.PrePaymentTypeCostID;
                        requestPayment.RequestID = _requestPayment.RequestID;
                        requestPayment.RelatedRequestPaymentID = _requestPayment.ID;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.AmountSum = prePaymentAmount;
                        requestPayment.Cost = 0;
                        requestPayment.Tax = 0;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsPaid = false;
                        DB.Save(requestPayment);
                    }
                    for (int i = 1; i <= installmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = _requestPayment.ID;
                        installmentRequestPayment.TelephoneNo = RequestPaymentDB.GetTelephoneNobyRequestPaymentID(_requestPayment.ID);
                        installmentRequestPayment.IsCheque = (bool)IsChequeCheckBox.IsChecked;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;
                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (installmentCount == i)
                            installmentRequestPayment.Cost = (long)(installmentPayment - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);
                        installmentRequestPayments.Add(installmentRequestPayment);
                    }
                }

                DB.SaveAll(installmentRequestPayments);

                RequestPayment currentPayment = RequestPaymentDB.GetRequestPaymentByID(_RequestPaymentID);

                currentPayment.PaymentType = (byte)DB.PaymentType.Instalment;

                currentPayment.Detach();
                Save(currentPayment);

                ts.Complete();
            }
        }

        public override bool Print()
        {
            try
            {
                InstalmentRequestPaymentInfo IRPTemp = new InstalmentRequestPaymentInfo();
                IRPTemp.PrePaymentAmount = PrePaymentAmountTextBox.Text;
                IRPTemp.InstallmentCount = InstallmentCountTextBox.Text;
                IRPTemp.StartDate = Date.GetPersianDate(StartDatePicker.SelectedDate, Date.DateStringType.Short);
                IRPTemp.EndDate = Date.GetPersianDate(EndDatePicker.SelectedDate, Date.DateStringType.Short);
                if (DailyCheckBox.IsChecked == true)
                {
                    IRPTemp.Daily = "بلی";
                }
                else
                {
                    IRPTemp.Daily = "خیر";
                }
                if (IsChequeCheckBox.IsChecked == true)
                {
                    IRPTemp.IsCheque ="بلی";
                }
                else
                {
                    IRPTemp.IsCheque = "خیر";
                }
                
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();
                string title = string.Empty;
                string path = ReportDB.GetReportPath((int)DB.UserControlNames.ADSLInstalmentRequestPaymentReport);
                stiReport.Load(path);

                List<Data.InstalmentRequestPaymentList> RPTemp = InstallmentRequestPaymentDB.GetInstalmentRequestPaymentList(_RequestPaymentID);

                stiReport.RegData("Result", "Result", IRPTemp);
                stiReport.RegData("InstalmentRequestPaymentList", "InstalmentRequestPaymentList", RPTemp);
             

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

        private void PopupWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_installmentRequestPayments.Any(t => t.IsCheque == true && (t.ChequeNumber == null || t.ChequeNumber == string.Empty)))
            {
                MessageBoxResult result = Folder.MessageBox.Show("وارد کردن شماره چک و تاریخ سر رسید چک برای همه اقساط اجباری می باشد.", "پرسش", MessageBoxImage.Error, MessageBoxButton.OK);
                e.Cancel = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool daily = (bool)DailyCheckBox.IsChecked;

                if (_RequestTypeID == (byte)DB.RequestType.ADSL)
                    GenerateInstalments(true);
                else
                    GenerateInstalments(daily);

                LoadData();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TransactionScope tsScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DB.DeleteAll<InstallmentRequestPayment>(_installmentRequestPayments.Select(t => t.ID).ToList());

                    RequestPayment currentRequestPayment = RequestPaymentDB.GetRequestPaymentByID(_RequestPaymentID);
                    RequestPayment preReqeustPayment = Data.RequestPaymentDB.GetPrePaymentOfRequestPayment(_RequestPaymentID);


                    if (preReqeustPayment != null && preReqeustPayment.IsPaid == false)
                        DB.Delete<RequestPayment>(preReqeustPayment.ID);
                    else
                        if (preReqeustPayment != null && preReqeustPayment.IsPaid == true)
                            throw new Exception("پیش پرداخت این هزینه پرداخت شده است امکان حذف اقساط نمی باشد");

                    currentRequestPayment.PaymentType = (byte)DB.PaymentType.Instalment;

                    currentRequestPayment.Detach();
                    Save(currentRequestPayment);

                    tsScope.Complete();
                }

                InstallmentRequestPaymentDataGrid.ItemsSource = null;
                InstallmentGroupBox.IsEnabled = true;
                PrePaymentAmountTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف اطلاعات، " + ex.Message + " !", ex);
            }

        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (InstallmentRequestPaymentDataGrid.SelectedIndex < 0 || InstallmentRequestPaymentDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            if (InstallmentRequestPaymentDataGrid.SelectedValue != null)
            {
                InstallmentRequestPayment item = InstallmentRequestPaymentDataGrid.SelectedItem as InstallmentRequestPayment;
                if (item.IsCheque == true)
                {
                    InstallmentRequestPaymentChequeForm window = new InstallmentRequestPaymentChequeForm(item.ID);
                    window.ShowDialog();
                }
                else
                    Folder.MessageBox.ShowInfo("قسط از نوع چک نمی باشد");
            }

            LoadData();

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_RequestTypeID != (byte)DB.RequestType.ADSL)
            {
                int installmentCount = 0;
                if (!string.IsNullOrWhiteSpace(InstallmentCountTextBox.Text.Trim()))
                    installmentCount = Convert.ToInt32(InstallmentCountTextBox.Text.Trim());

                string startDate = StartDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                string endDate = Helper.AddMonthToPersianDate(startDate, installmentCount);

                EndDatePicker.SelectedDate = Helper.PersianToGregorian(endDate).Value.Date;
            }
        }
    }
}
