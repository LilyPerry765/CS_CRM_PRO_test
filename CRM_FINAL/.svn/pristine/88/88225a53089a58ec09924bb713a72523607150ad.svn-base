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
using CRM.Data;
using System.Windows.Forms;
using Folder;

namespace CRM.Application.Views
{
    public partial class PaidFactorForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private long _RequestID = 0;
        RequestType RequestType;
        private string _BillID = "";
        private string _PaymentID = "";
        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }


        #endregion

        #region Constructor

        public PaidFactorForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PaidFactorForm(long requestID)
        {
            InitializeComponent();
            Initialize();

            _RequestID = requestID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PaymentWayComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PaymentWay));
            BankComboBox.ItemsSource = Data.BankDB.GetBanksCheckable();
        }

        private void LoadData()
        {
            if (_RequestID == 0)
            {
                PaymentIDTextBox.Visibility = Visibility.Visible;
                PaymentIDComboBox.Visibility = Visibility.Collapsed;
            }
            else
            {

                 RequestType = Data.RequestTypeDB.GetRequestTypeByRequestID(_RequestID);
                 if (DB.IsFixRequest(RequestType.ID))
                 {
                     factorNumberLabel.Visibility = Visibility.Visible;
                     factorNumberComboBox.Visibility = Visibility.Visible;

                     factorNumberComboBox.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentFactorByRequestID(_RequestID);
                 }
                 else
                 {
                     AmountSumTextBox.Text = RequestPaymentDB.GetNoPaidPaymentAmountByRequestID(_RequestID, (byte)DB.PaymentType.Cash).ToString() + " ریا ل";
                 }


                PaymentIDTextBox.Visibility = Visibility.Collapsed;
               // PaymentIDComboBox.Visibility = Visibility.Visible;

                // ----- For BillID and PaymentID
                //List<RequestPayment> paymentsList = RequestPaymentDB.GetRequestPaymentByRequestID(_RequestID, (byte)DB.PaymentType.Cash);                
                //List<string> paymentIDs = new List<string>();

                //BillIDTextBox.Text = paymentsList.FirstOrDefault().BillID;
                //PaymentIDComboBox.ItemsSource = RequestPaymentDB.GetPaymentIDsCheckablebyBillID(_RequestID, BillIDTextBox.Text.Trim());
                // ----- END.

               
            }
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                // ----- For BillID and PaymentID
                //if (BillIDTextBox.Text == "")
                //    throw new Exception("لطفا ابتدا فاکتور را صادر کنید");
                //if (PaymentIDComboBox.SelectedValue == null)
                //    throw new Exception("لطفا شناسه پرداخت را تعیین نمایید");
                // ----- END.

                if (PaymentWayComboBox.SelectedValue == null)
                    throw new Exception("لطفا نحوه پرداخت را تعیین نمایید");
                if (BankComboBox.SelectedValue == null)
                    throw new Exception("لطفا نام بانک را انتخاب نمایید");
                if (string.IsNullOrWhiteSpace(FicheNunmberTextBox.Text))
                    throw new Exception("لطفا شماره فیش را وارد نمایید");
                if (FicheDate.SelectedDate == null)
                    throw new Exception("لطفا تاریخ فیش را وارد نمایید");
                //if (PaymentDate == null)
                //    throw new Exception("لطفا تاریخ پرداخت را وارد نمایید");

                List<RequestPayment> paymentList = new List<RequestPayment>();

                // ----- For BillID and PaymentID
                //if (_RequestID == 0)
                //    paymentList = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text, PaymentIDTextBox.Text);
                //else
                //    paymentList = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text, PaymentIDComboBox.SelectedValue.ToString());
                // ------ END.
                if (DB.IsFixRequest(RequestType.ID))
                {
                    long requestID = 0;

                    if (RequestPaymentDB.CheckUniqueId(FicheNunmberTextBox.Text.Trim(), out requestID))
                    {
                        throw new Exception(string.Format("فیش برای درخواست {0} قبلا ثبت شده است", requestID));
                    }

                    if (factorNumberComboBox.SelectedValue != null)
                    {
                        paymentList = RequestPaymentDB.GetNoPaidRequestPaymentByRequestIDAndFactorNumber(_RequestID, (int)factorNumberComboBox.SelectedValue, new List<int> { (int)DB.PaymentType.Cash, (int)DB.PaymentType.NonForcedCash });
                    }
                    else
                    {
                        throw new Exception("لطفا ابتدا فاکتور را انتخاب نمایید");
                    }
                    
                }
                else
                {
                    paymentList = RequestPaymentDB.GetNoPaidRequestPaymentByRequestID(_RequestID, (byte)DB.PaymentType.Cash);
                }

                if (paymentList != null)
                {
                    if (paymentList.Count != 0)
                    {
                        foreach (RequestPayment currentPayment in paymentList)
                        {
                            currentPayment.PaymentWay = (byte)Convert.ToInt16(PaymentWayComboBox.SelectedValue);
                            currentPayment.BankID = Convert.ToInt32(BankComboBox.SelectedValue);
                            currentPayment.FicheNunmber = FicheNunmberTextBox.Text;
                            currentPayment.FicheDate = FicheDate.SelectedDate;
                            currentPayment.PaymentDate = DB.GetServerDate();
                            currentPayment.UserID = DB.CurrentUser.ID;
                            currentPayment.IsPaid = true;
                            if (FileBytes != null && Extension != string.Empty)
                                currentPayment.DocumentsFileID = _FileID;

                            //if (currentPayment.DocumentsFileID == null)
                            //    throw new Exception("لطفا تصویر فاکتور پرداخت را اضافه نمایید");

                            currentPayment.Detach();
                            Save(currentPayment);
                        }

                        ADSLSellerAgentUser user = ADSLSellerGroupDB.GetADSLSellerAgentUserByID(DB.CurrentUser.ID);

                        if (user == null)
                        {
                            Request request = RequestDB.GetRequestByID(_RequestID);
                            user = ADSLSellerGroupDB.GetADSLSellerAgentUserByID((int)request.CreatorUserID);
                        }

                        if (user != null)
                        {
                            ADSLSellerAgent sellerAgent = ADSLSellerGroupDB.GetADSLSellerAgentByID(user.SellerAgentID);

                            if (ADSLSellerGroupDB.IsSellerAgentUserPaidbyRequestID(_RequestID, user.ID))
                            {
                                long paymentAmount = Convert.ToInt64(paymentList.Sum(t=>t.AmountSum));

                                sellerAgent.CreditCashUse = sellerAgent.CreditCashUse + paymentAmount;
                                sellerAgent.CreditCashRemain = sellerAgent.CreditCashRemain - paymentAmount;

                                user.CreditCashUse = user.CreditCashUse + paymentAmount;
                                user.CreditCashRemain = user.CreditCashRemain - paymentAmount;

                                sellerAgent.Detach();
                                DB.Save(sellerAgent);

                                user.Detach();
                                DB.Save(user);

                                ADSLSellerAgentUserCredit credit = new ADSLSellerAgentUserCredit();

                                credit.RequestID = _RequestID;
                                credit.UserID = user.ID;
                                credit.Cost = paymentAmount;
                                credit.IsPaid = false;

                                credit.Detach();
                                DB.Save(credit);
                            }
                        }
                    }
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پرداخت ، " + ex.Message + " !", ex);
            }
        }

        private void PaymentIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (PaymentIDComboBox.SelectedValue != null)
                {
                    RequestPayment payment = new RequestPayment();

                    if (_RequestID == 0)
                    {
                        if (RequestPaymentDB.PaidAllPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim()))
                        {
                            payment = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim()).FirstOrDefault();
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim());

                            this.DataContext = payment;

                            PaymentWayComboBox.IsEnabled = false;
                            BankComboBox.IsEnabled = false;
                            FicheNunmberTextBox.IsReadOnly = true;
                            FicheDate.IsEnabled = false;
                            //PaymentDate.IsEnabled = false;
                            SaveButton.IsEnabled = false;

                            throw new Exception("فاکتور مورد نظر پیش از این پرداخت شده است !");
                        }
                        else
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim());
                    }
                    else
                    {
                        if (RequestPaymentDB.PaidAllPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDComboBox.SelectedValue.ToString()))
                        {
                            payment = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDComboBox.SelectedValue.ToString()).FirstOrDefault();
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDComboBox.SelectedValue.ToString());

                            this.DataContext = payment;

                            PaymentWayComboBox.IsEnabled = false;
                            BankComboBox.IsEnabled = false;
                            FicheNunmberTextBox.IsReadOnly = true;
                            FicheDate.IsEnabled = false;
                            //PaymentDate.IsEnabled = false;
                            SaveButton.IsEnabled = false;

                            throw new Exception("فاکتور مورد نظر پیش از این پرداخت شده است !");
                        }
                        else
                        {
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDComboBox.SelectedValue.ToString());
                            PaymentWayComboBox.SelectedValue = null;
                            BankComboBox.SelectedValue = null;
                            FicheNunmberTextBox.Text = string.Empty;
                            FicheDate.SelectedDate = null;
                            //PaymentDate.SelectedDate = null;

                            PaymentWayComboBox.IsEnabled = true;
                            BankComboBox.IsEnabled = true;
                            FicheNunmberTextBox.IsReadOnly = false;
                            FicheDate.IsEnabled = true;
                            //PaymentDate.IsEnabled = true;
                            SaveButton.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void FileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
                Extension = System.IO.Path.GetExtension(dlg.FileName);
            }

            if (FileBytes != null && Extension != string.Empty)
                _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        }

        private void ScannerLisBox_Selected(object sender, RoutedEventArgs e)
        {
            Scanner oScanner = new Scanner();
            string extension;

            FileBytes = oScanner.ScannWithExtension(out extension);
            Extension = extension;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (FileBytes != null && Extension != string.Empty)
                {
                    FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
                    FileBytes = fileInfo.Content;
                    CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                    window.FileBytes = FileBytes;
                    window.FileType = fileInfo.FileType;
                    window.ShowDialog();
                }
                else
                    throw new Exception("فایل موجود نمی باشد !");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

        }

        private void Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion

        private void factorNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(factorNumberComboBox.SelectedValue != null)
            {
                AmountSumTextBox.Text = Data.RequestPaymentDB.GetRequestPaymentCostByFactorNumber(_RequestID, (int)factorNumberComboBox.SelectedValue, new List<int> { (int)DB.PaymentType.Cash, (int)DB.PaymentType.NonForcedCash }).ToString() + " ریا ل"; ;
            }
        }
    }
}
