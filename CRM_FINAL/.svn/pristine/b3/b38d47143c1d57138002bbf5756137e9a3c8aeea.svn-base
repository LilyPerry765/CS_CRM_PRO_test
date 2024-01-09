using CRM.Application;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRM.Website.Viewes
{
    public partial class PaidFactorForm : System.Web.UI.Page
    {
        #region Properties

        private long _Id = 0;
        private long _RequestID = 0;
        private string _BillID = "";
        private string _PaymentID = "";
        private Guid _FileID;
        private byte[] _FileBytes { get; set; }
        private string _Extension { get; set; }


        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["RequestID"], out _RequestID);
            Initialize();
            LoadData();
        }

        protected void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as TextBox).Text))
            {
                char c = Convert.ToChar((sender as TextBox).Text.Substring((sender as TextBox).Text.Length-1));
                if (!Char.IsNumber(c))
                    (sender as TextBox).Text = (sender as TextBox).Text.Substring(0, ((sender as TextBox).Text.Length - 2));
            }
        }

        protected void PaymentIDDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(PaymentIDDropDownList.SelectedValue) && int.Parse(PaymentIDDropDownList.SelectedValue) > 0)
                {
                    RequestPayment payment = new RequestPayment();

                    if (_RequestID == 0)
                    {
                        if (RequestPaymentDB.PaidAllPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim()))
                        {
                            payment = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim()).FirstOrDefault();
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim());

                            //this.DataContext = payment;
                            PaymentIDTextBox.Text = payment.PaymentID;
                            PaymentWayDropDownList.SelectedValue = payment.PaymentWay.ToString();
                            BankDropDownList.SelectedValue = payment.BankID.ToString();
                            FicheNunmberTextBox.Text = payment.FicheNunmber;

                            FicheDateTextBox.Text = Helper.GetPersianDate(payment.FicheDate, Helper.DateStringType.Short);
                            PaymentDateTextBox.Text = Helper.GetPersianDate(payment.PaymentDate, Helper.DateStringType.Short); 


                            PaymentWayDropDownList.Enabled = false;
                            BankDropDownList.Enabled = false;
                            FicheNunmberTextBox.ReadOnly = true;
                            FicheDateTextBox.Enabled = false;
                            PaymentDateTextBox.Enabled = false;
                            SaveButton.Enabled = false;

                            throw new Exception("فاکتور مورد نظر پیش از این پرداخت شده است !");
                        }
                        else
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDTextBox.Text.Trim());
                    }
                    else
                    {
                        if (RequestPaymentDB.PaidAllPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDDropDownList.SelectedItem.Text))
                        {
                            payment = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDDropDownList.SelectedItem.Text).FirstOrDefault();
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDDropDownList.SelectedItem.Text);

                            //this.DataContext = payment;

                            PaymentIDTextBox.Text = payment.PaymentID;
                            PaymentWayDropDownList.SelectedValue = payment.PaymentWay.ToString();
                            BankDropDownList.SelectedValue = payment.BankID.ToString();
                            FicheNunmberTextBox.Text = payment.FicheNunmber;

                            FicheDateTextBox.Text = Helper.GetPersianDate(payment.FicheDate, Helper.DateStringType.Short);
                            PaymentDateTextBox.Text = Helper.GetPersianDate(payment.PaymentDate, Helper.DateStringType.Short);

                            //if (payment.RequestPaymentTypeID == 0)
                            //    FileRadioButton.Checked;
                            //else
                            //    ScannerRadioButton.Checked;

                            PaymentWayDropDownList.Enabled = false;
                            BankDropDownList.Enabled = false;
                            FicheNunmberTextBox.ReadOnly = true;
                            FicheDateTextBox.Enabled = false;
                            PaymentDateTextBox.Enabled = false;
                            SaveButton.Enabled = false;

                            throw new Exception("فاکتور مورد نظر پیش از این پرداخت شده است !");
                        }
                        else
                        {
                            AmountSumTextBox.Text = RequestPaymentDB.GetPaymentSumAmountbyPaymentID(BillIDTextBox.Text.Trim(), PaymentIDDropDownList.SelectedItem.Text);
                            PaymentWayDropDownList.SelectedValue = null;
                            BankDropDownList.SelectedValue = null;
                            FicheNunmberTextBox.Text = string.Empty;
                            FicheDateTextBox.Text = null;
                            PaymentDateTextBox.Text = null;

                            PaymentWayDropDownList.Enabled = true;
                            BankDropDownList.Enabled = true;
                            FicheNunmberTextBox.ReadOnly = false;
                            FicheDateTextBox.Enabled = true;
                            PaymentDateTextBox.Enabled = true;
                            SaveButton.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + " !" + "');", true);
            }
        }

        //protected void FileRadioButton_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (FileRadioButton.Checked)
        //    {
        //        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //        dlg.Filter = "All files (*.*)|*.*";

        //        if (dlg.ShowDialog() == true)
        //        {
        //            FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
        //            Extension = System.IO.Path.GetExtension(dlg.FileName);
        //        }

        //        if (FileBytes != null && Extension != string.Empty)
        //            _fileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        //    }
        //    else if (ScannerRadioButton.Checked)
        //    {

        //        Scanner oScanner = new Scanner();
        //        string extension;

        //        FileBytes = oScanner.ScannWithExtension(out extension);
        //        Extension = extension;

        //    }
        //}

        protected void FileImageButton_Click(object sender, ImageClickEventArgs e)
        {
            //string tempPath;
            //string filePath;

            //tempPath = Server.MapPath("~/Files/RequestPayments");
            //string fileName = string.Format("CRM_ {0}_ {1}_ {2}.{6}", _requestID, BillIDTextBox.Text, PaymentIDDropDownList.SelectedItem.Text);
            //filePath = System.IO.Path.Combine(tempPath, fileName);

            //if (!System.IO.File.Exists(filePath))
            //{
            //    Enterprise.Logger.WriteInfo("Attached file is loading to : '{0}'", filePath);
            //    File.WriteAllBytes(filePath, _fileBytes);
            //}
            //Response.Redirect("/Files/RequestPayments/" + fileName, false);


            //try
            //{
            //    if (_fileBytes != null && _extension != string.Empty)
            //    {
            //        _fileBytes = DocumentsFileDB.GetDocumentsFileTable(_fileID).Content;
            //        DocumentViewForm window = new DocumentViewForm();
            //        HttpContext.Current.Session["FileBytes"] = _fileBytes;
            //        Page.ClientScript.RegisterStartupScript(GetType(), "OpenDocumentViewForm", "ModalDialog('/Viewes/DocumentViewForm.aspx',null,800,500);", false);
            //    }
            //    else
            //        throw new Exception("فایل موجود نمی باشد !");
            //}
            //catch (Exception ex)
            //{
            //    string message = ex.Message.Replace("\'", "");
            //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + " !" + "');", true);
            //}
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            //{
            //    return;
            //}
            try
            {
                if (PaymentWayDropDownList.SelectedValue == null)
                    throw new Exception("لطفا نحوه پرداخت را تعیین نمایید");
                if (BankDropDownList.SelectedValue == null)
                    throw new Exception("لطفا نام بانک را انتخاب نمایید");
                if (string.IsNullOrWhiteSpace(FicheNunmberTextBox.Text))
                    throw new Exception("لطفا شماره فیش را وارد نمایید");
                if (string.IsNullOrEmpty(FicheDateTextBox.Text))
                    throw new Exception("لطفا تاریخ فیش را وارد نمایید");
                if (string.IsNullOrEmpty(PaymentDateTextBox.Text))
                    throw new Exception("لطفا تاریخ پرداخت را وارد نمایید");

                if (PaymentReceiptUpload.HasFile)
                {
                    if (PaymentReceiptUpload.FileContent.Length > 1048576)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "فایل پیوست شده بیش از یک مگابایت حجم دارد" + " !" + "');", true);
                        return;
                    }

                    _FileBytes = PaymentReceiptUpload.FileBytes;
                    _Extension = System.IO.Path.GetExtension(PaymentReceiptUpload.FileName);
                }

                List<RequestPayment> paymentList = new List<RequestPayment>();

                if (_RequestID == 0)
                    paymentList = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text, PaymentIDTextBox.Text);
                else
                    paymentList = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text, PaymentIDDropDownList.SelectedItem.Text);
                    //paymentList = RequestPaymentDB.GetPaymentsbyPaymentID(BillIDTextBox.Text, PaymentIDDropDownList.SelectedValue.ToString());

                if (paymentList != null)
                {
                    if (paymentList.Count != 0)
                    {
                        foreach (RequestPayment currentPayment in paymentList)
                        {
                            currentPayment.PaymentWay = (byte)Convert.ToInt16(PaymentWayDropDownList.SelectedValue);
                            currentPayment.BankID = Convert.ToInt32(BankDropDownList.SelectedValue);
                            currentPayment.FicheNunmber = FicheNunmberTextBox.Text;
                            currentPayment.FicheDate = string.IsNullOrEmpty(FicheDateTextBox.Text) ? (DateTime?)null : (DateTime)Helper.PersianToGregorian(FicheDateTextBox.Text);
                            currentPayment.PaymentDate = string.IsNullOrEmpty(PaymentDateTextBox.Text) ? (DateTime?)null : (DateTime)Helper.PersianToGregorian(PaymentDateTextBox.Text);
                            currentPayment.IsPaid = true;
                            if (_FileBytes != null && _Extension != string.Empty)
                            {
                                _FileID = DocumentsFileDB.SaveFileInDocumentsFile(_FileBytes, _Extension);
                                currentPayment.DocumentsFileID = _FileID;
                            }

                            //if (currentPayment.DocumentsFileID == null)
                            //    throw new Exception("لطفا تصویر فاکتور پرداخت را اضافه نمایید");

                            currentPayment.Detach();
                            DB.Save(currentPayment);

                           // ScriptManager.RegisterStartupScript(this, this.GetType(), "Paid", "alert('پرداخت با موفقیت انجام شد.'); window.close();", true);
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
                                long paymentAmount = Convert.ToInt64(paymentList.Sum(t => t.AmountSum));

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
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + " !" + "');", true);
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PaymentWayDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.PaymentWay));
            PaymentWayDropDownList.DataBind();
            BankDropDownList.DataSource = Data.BankDB.GetBanksCheckable();
            BankDropDownList.DataBind();
        }

        private void LoadData()
        {
            if (_RequestID == 0)
            {
                PaymentIDTextBox.Style.Add("display", "block");
                PaymentIDDropDownList.Style.Add("display", "none");
            }
            else
            {
                PaymentIDTextBox.Style.Add("display", "none");
                PaymentIDDropDownList.Style.Add("display", "block");

                List<RequestPayment> paymentsList = RequestPaymentDB.GetRequestPaymentByRequestID(_RequestID, (byte)DB.PaymentType.Cash);
                List<string> paymentIDs = new List<string>();
                BillIDTextBox.Text = paymentsList.FirstOrDefault().BillID;

                if (!IsPostBack)
                {
                    PaymentIDDropDownList.DataSource = RequestPaymentDB.GetPaymentIDsCheckablebyBillID(_RequestID, BillIDTextBox.Text.Trim());
                    PaymentIDDropDownList.DataBind();
                    PaymentIDDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }
        }

        #endregion
    }
}