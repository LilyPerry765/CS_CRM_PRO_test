using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Transactions;
using CRM.Application;


namespace CRM.Website.Viewes
{
    public partial class InstallmentRequestPaymentForm : System.Web.UI.Page
    {
        #region Properties

        private List<InstallmentRequestPayment> _installmentRequestPayments { get; set; }
        private RequestPayment _requestPayment { get; set; }
        private long _requestPaymentID = 0;
        private const int _floorValue = 1000;
        private int _requestTypeID = 0;
        private int? _serviceDuration = 0;

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["RequestPaymentID"], out _requestPaymentID);

            Initialize();
            LoadData();
        }

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            char c = Convert.ToChar((sender as TextBox).Text);
            if (!Char.IsNumber(c))
                (sender as TextBox).Text = (sender as TextBox).Text.Substring(0, ((sender as TextBox).Text.Length - 2));
        }

        private void PopupWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_installmentRequestPayments.Any(t => t.IsCheque == true && (t.ChequeNumber == null || t.ChequeNumber == string.Empty)))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "وارد کردن شماره چک و تاریخ سر رسید چک برای همه اقساط اجباری می باشد." + "');", true);
                //System.Windows.Forms.DialogResult result = FMessegeBox.FarsiMessegeBox.Show("وارد کردن شماره چک و تاریخ سر رسید چک برای همه اقساط اجباری می باشد.", "پرسش",FMessegeBox.FMessegeBoxButtons.Ok , FMessegeBox.FMessegeBoxIcons.Error);
                e.Cancel = true;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool daily = (bool)DailyCheckBox.Checked;

                if (_requestTypeID == (byte)DB.RequestType.ADSL)
                    GenerateInstalments(true);
                else
                    GenerateInstalments(daily);

                LoadData();
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                //using (TransactionScope tsScope = new TransactionScope(TransactionScopeOption.Required))
                //{
                    DB.DeleteAll<InstallmentRequestPayment>(_installmentRequestPayments.Select(t => t.ID).ToList());

                    RequestPayment currentRequestPayment = RequestPaymentDB.GetRequestPaymentByID(_requestPaymentID);
                    RequestPayment preReqeustPayment = Data.RequestPaymentDB.GetPrePaymentOfRequestPayment(_requestPaymentID);

                    if (preReqeustPayment != null && preReqeustPayment.IsPaid == false)
                        DB.Delete<RequestPayment>(preReqeustPayment.ID);
                    else
                        if (preReqeustPayment != null && preReqeustPayment.IsPaid == true)
                            throw new Exception("پیش پرداخت این هزینه پرداخت شده است امکان حذف اقساط نمی باشد");

                    currentRequestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    currentRequestPayment.Detach();
                    DB.Save(currentRequestPayment);

                //    tsScope.Complete();
                //}

                InstallmentRequestPaymentGridView.DataSource = null;
                InstallmentRequestPaymentGridView.DataBind();

                PrePaymentAmountTextBox.Enabled = true;
                StartDateTextBox.Enabled = true;
                DailyCheckBox.Enabled = true;
                InstallmentCountTextBox.Enabled = true;
                EndDateTextBox.Enabled = true;
                IsChequeCheckBox.Enabled = true;
                SaveButton.Enabled = true;

                PrePaymentAmountTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "خطا در حذف اطلاعات، " + message + "');", true);
            }
        }

        protected void InstallmentRequestPaymentGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            if (e.CommandName == "Modify")
            {
                index = Convert.ToInt32(e.CommandArgument.ToString());
                if (InstallmentRequestPaymentGridView.SelectedIndex >= 0)
                {
                    int id = 0;
                    int.TryParse(InstallmentRequestPaymentGridView.SelectedRow.Cells[0].Text, out id);
                    if (id > 0)
                    {
                        InstallmentRequestPayment item = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByID(id);
                        if (item != null)
                        {
                            if (item.IsCheque == true)
                            {
                                string url = string.Format("/Viewes/InstallmentRequestPaymentChequeForm.aspx ? installmentRequestPaymentID={0}", item.ID);
                                Page.ClientScript.RegisterStartupScript(GetType(), "OpenInstallmentRequestPaymentChequeForm", string.Format("ModalDialog({0},null,800,500);", url), false);
                            }
                            else
                                Folder.MessageBox.ShowInfo("قسط از نوع چک نمی باشد");
                        }
                    }
                }
                LoadData();

            }
        }

        protected void InstallmentRequestPaymentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text != "&nbsp;")
                {
                    try
                    {
                        DateTime date = DateTime.Parse(e.Row.Cells[0].Text.Substring(0, 19));
                        e.Row.Cells[0].Text = Date.GetPersianDate(date, Date.DateStringType.Short);
                    }
                    catch { }
                }

                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    try
                    {
                        DateTime date = DateTime.Parse(e.Row.Cells[1].Text.Substring(0, 19));
                        e.Row.Cells[1].Text = Date.GetPersianDate(date, Date.DateStringType.Short);
                    }
                    catch { }
                }
            }
        }


        #endregion

        #region Methods

        private void Initialize()
        {
            if (_requestPaymentID != 0)
            {
                _requestTypeID = RequestPaymentDB.GetRequestTypeIDbyRequestPaymentID(_requestPaymentID);

                if (_requestTypeID == (byte)DB.RequestType.ADSL)
                {
                    StartDateTextBox.Enabled = false;
                    EndDateTextBox.Enabled = false;
                    PrePaymentAmountTextBox.Enabled = false;
                }
                else
                {
                    EndDateTextBox.Enabled = false;
                }
            }
        }

        private void LoadData()
        {
            _requestPayment = Data.RequestPaymentDB.GetRequestPaymentByID(_requestPaymentID);
            _installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(_requestPaymentID);

            if (_requestPayment.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID || _requestPayment.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
            {
                ADSLRequest aDSLRequest = ADSLRequestDB.GetADSLRequestByID(_requestPayment.RequestID);

                if (aDSLRequest != null)
                {
                    _serviceDuration = ADSLServiceDB.GetADSLServiceDurationByServiceID((int)aDSLRequest.ServiceID);
                    InstallmentCountTextBox.Text = ADSLServiceDB.GetADSLServiceDurationTitleByServiceID((int)aDSLRequest.ServiceID);
                    StartDateTextBox.Text = Helper.GetPersianDate(DB.GetServerDate(),Helper.DateStringType.Short);
                    EndDateTextBox.Text = Helper.GetPersianDate(DB.GetServerDate().AddMonths(Convert.ToInt32(InstallmentCountTextBox.Text.Trim())), Helper.DateStringType.Short);
                }

                if (_installmentRequestPayments.Count == 0)
                    GenerateInstalments(true);
            }

            BaseCost baseCost = Data.BaseCostDB.GetBaseCostByID(_requestPayment.BaseCostID ?? 0);
            _installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(_requestPaymentID);

            if (baseCost != null && baseCost.IsCheque == false)
                IsChequeCheckBox.Enabled = false;

            if (_installmentRequestPayments.Count > 0)
            {
                InstallmentCountTextBox.Text = _installmentRequestPayments.Count.ToString();
                StartDateTextBox.Text = Helper.GetPersianDate(_installmentRequestPayments.OrderBy(t => t.StartDate).FirstOrDefault().StartDate , Helper.DateStringType.Short);
                
                PrePaymentAmountTextBox.Enabled = false;
                StartDateTextBox.Enabled = false;
                DailyCheckBox.Enabled = false;
                InstallmentCountTextBox.Enabled = false;
                EndDateTextBox.Enabled = false;
                IsChequeCheckBox.Enabled = false;
                SaveButton.Enabled = false;

                bool isCheque = _installmentRequestPayments.Any(t => t.IsCheque == true);
                IsChequeCheckBox.Checked = isCheque;

                //if (isCheque == true)
                //    ChequeNumberColumn.Visibility = Visibility.Visible;
                //else
                //    ChequeNumberColumn.Visibility = Visibility.Collapsed;
            }
            else
            {
                PrePaymentAmountTextBox.Enabled = true;
                StartDateTextBox.Enabled = true;
                DailyCheckBox.Enabled = true;
                InstallmentCountTextBox.Enabled = true;
                EndDateTextBox.Enabled = true;
                IsChequeCheckBox.Enabled = true;
                SaveButton.Enabled = true;
                PrePaymentAmountTextBox.Text = "0";
            }

            SumOfInstallment.Text = _installmentRequestPayments.Sum(t => t.Cost).ToString();

            InstallmentRequestPaymentGridView.DataSource = _installmentRequestPayments;
            InstallmentRequestPaymentGridView.DataBind();
        }

        public void GenerateInstalments(bool daily)
        {
            //using (TransactionScope ts = new TransactionScope())
            //{
                List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
                int PaymentAmountEachPart = 0;

                long prePaymentAmount = 0;
                if (!string.IsNullOrWhiteSpace(PrePaymentAmountTextBox.Text.Trim()))
                    prePaymentAmount = Convert.ToInt64(PrePaymentAmountTextBox.Text.Trim());

                int installmentCount = 0;
                if (!string.IsNullOrWhiteSpace(InstallmentCountTextBox.Text.Trim()))
                    installmentCount = Convert.ToInt32(InstallmentCountTextBox.Text.Trim());

                if (_requestTypeID == (byte)DB.RequestType.ADSL)
                {
                    string startDate = StartDateTextBox.Text;
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
                        installmentRequestPayment.IsCheque = (bool)IsChequeCheckBox.Checked;

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
                else
                {
                    string startDate = StartDateTextBox.Text;
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
                        installmentRequestPayment.IsCheque = (bool)IsChequeCheckBox.Checked;

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

                RequestPayment currentPayment = RequestPaymentDB.GetRequestPaymentByID(_requestPaymentID);

                currentPayment.PaymentType = (byte)DB.PaymentType.Instalment;

                currentPayment.Detach();
                DB.Save(currentPayment);

            //    ts.Complete();
            //}
        }

        #endregion
    }
}