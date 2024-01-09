using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.Application;

namespace CRM.Website.Viewes
{
    public partial class RequestPaymentForm : System.Web.UI.Page
    {
        #region Peroperties & Fields

        private long _PaymentID = 0;
        private long _RequestID = 0;
        private Request _Request;
        private bool _IsOtherCost = false;

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long.TryParse(Request.QueryString["RequestPaymentID"], out _PaymentID);
                long.TryParse(Request.QueryString["RequestID"], out _RequestID);
                bool.TryParse(Request.QueryString["IsOtherCost"], out _IsOtherCost);

                Initialize();
                PaymentFicheDropDownList.DataSource = Data.PaymentFicheDB.GetPaymentFicheCheckable(_RequestID);
                PaymentFicheDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                PaymentFicheDropDownList.DataBind();
                LoadData();
            }
        }

        protected void PaymentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BaseCostDD == null) return;
            BaseCostDT.Style.Add("display", "none");
            BaseCostDD.Style.Add("display", "none");
            PaymentFicheDT.Style.Add("display", "none");
            PaymentFicheDD.Style.Add("display", "none");
            OtherCostDT.Style.Add("display", "none");
            OtherCostDD.Style.Add("display", "none");
            if (BaseCostRadioButton.Checked)
            {
                BaseCostDT.Style.Add("display", "block");
                BaseCostDD.Style.Add("display", "block");
                OtherCostDropDownList.SelectedIndex = -1;
                PaymentFicheDropDownList.SelectedIndex = -1;
            }
            else if (OtherCostRadioButton.Checked)
            {
                OtherCostDT.Style.Add("display", "block");
                OtherCostDD.Style.Add("display", "block");
                BaseCostDropDownList.SelectedIndex = -1;
                PaymentFicheDropDownList.SelectedIndex = -1;
            }
            else if (PaymentFicheRadioButton.Checked)
            {
                PaymentFicheDT.Style.Add("display", "block");
                PaymentFicheDD.Style.Add("display", "block");
                BaseCostDropDownList.SelectedIndex = -1;
                OtherCostDropDownList.SelectedIndex = -1;
            }
        }

        protected void BaseCostDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BaseCostDropDownList.SelectedValue))
            {
                Data.BaseCost baseCost = Data.BaseCostDB.GetBaseCostByID(int.Parse(BaseCostDropDownList.SelectedValue));
                AmountSumTextBox.Text = baseCost.Cost.ToString();
            }
        }

        protected void OtherCostDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OtherCostDropDownList.SelectedValue))
            {
                OtherCost otherCost = Data.OtherCostDB.GetOtherCostByID(Convert.ToInt32(OtherCostDropDownList.SelectedValue));
                AmountSumTextBox.Text = otherCost.BasePrice.ToString();
            }
        }

        protected void AmountSumTextBox_TextChanged(object sender, EventArgs e)
        {
            char c = Convert.ToChar(AmountSumTextBox.Text.Substring(AmountSumTextBox.Text.Length - 1));
            if (!Char.IsNumber(c))
                AmountSumTextBox.Text = AmountSumTextBox.Text.Substring(0, (AmountSumTextBox.Text.Length - 2));

        }

        protected void SavePaymentButton_Click(object sender, EventArgs e)
        {
            //if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            //{
            //    return;
            //}
            long.TryParse(Request.QueryString["RequestID"], out _RequestID);
            long.TryParse(Request.QueryString["RequestPaymentID"], out _PaymentID);
            try
            {
                RequestPayment item = new RequestPayment();
                if (_PaymentID > 0)
                {
                    item.ID = _PaymentID;
                    RequestPayment requestPayment = Data.RequestPaymentDB.GetRequestPaymentByID(_PaymentID);
                    item.InsertDate = requestPayment.InsertDate;
                    item.RequestID = requestPayment.RequestID;
                }
                item.BillID = BillIDTextBox.Text;
                item.PaymentID = PaymentIDTextBox.Text;
                item.PaymentType = byte.Parse(PaymentTypeDropDownList.SelectedValue);
                item.BaseCostID = string.IsNullOrEmpty(BaseCostDropDownList.SelectedValue) ? (int?)null : int.Parse(BaseCostDropDownList.SelectedValue);
                item.PaymentFicheID = string.IsNullOrEmpty(PaymentFicheDropDownList.SelectedValue) ? (long?)null : int.Parse(PaymentFicheDropDownList.SelectedValue);
                item.OtherCostID = string.IsNullOrEmpty(OtherCostDropDownList.SelectedValue) ? (int?)null : int.Parse(OtherCostDropDownList.SelectedValue);
                item.PaymentWay = string.IsNullOrEmpty(PaymentWayDropDownList.SelectedValue) ? (byte?)null : byte.Parse(PaymentWayDropDownList.SelectedValue);
                item.BankID = string.IsNullOrEmpty(BankDropDownList.SelectedValue) ? (int?)null : int.Parse(BankDropDownList.SelectedValue);
                item.AmountSum = string.IsNullOrEmpty(AmountSumTextBox.Text) ? (long?)null : int.Parse(AmountSumTextBox.Text);
                item.FicheNunmber = FicheNunmberTextBox.Text;
                item.FicheDate = string.IsNullOrEmpty(FicheDateTextBox.Text) ? (DateTime?)null : Helper.PersianToGregorian(FicheDateTextBox.Text);
                item.PaymentDate = string.IsNullOrEmpty(FicheDateTextBox.Text) ? (DateTime?)null : Helper.PersianToGregorian(PaymentDateTextBox.Text);
                item.IsPaid = IsActiveCheckBox.Checked;


                if (OtherCostRadioButton.Checked)
                {
                    item.OtherCostID = Convert.ToInt32(OtherCostDropDownList.SelectedValue);
                    item.InsertDate = DB.GetServerDate();
                    item.RequestID = _RequestID;
                    item.PaymentWay = byte.Parse(PaymentWayDropDownList.SelectedValue);
                    item.PaymentType = byte.Parse(PaymentTypeDropDownList.SelectedValue);
                    item.IsKickedBack = false;
                    item.Cost = Convert.ToInt64(AmountSumTextBox.Text);
                    item.Tax = 0;
                }
                item.AmountSum = Convert.ToInt64(AmountSumTextBox.Text);

                if (_PaymentID == 0)
                    item.RequestID = _RequestID;

                if (item.PaymentWay == null)
                    throw new Exception("لطفا نحوه پرداخت را تعیین نمایید");
                if (item.BankID == null)
                    throw new Exception("لطفا نام بانک را انتخاب نمایید");
                if (string.IsNullOrEmpty(item.FicheNunmber))
                    throw new Exception("لطفا شماره فیش را وارد نمایید");
                if (item.FicheDate == null)
                    throw new Exception("لطفا تاریخ فیش را وارد نمایید");
                if (item.PaymentDate == null)
                    throw new Exception("لطفا تاریخ پرداخت را وارد نمایید");

                item.Detach();
                DB.Save(item);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('دریافت/ پرداخت با موفقیت ذخیره شد.'); window.close();", true);
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در ذخیره دریافت / پرداخت : " + message + "');", true);
            }
        }

        protected void AddCostButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenOtherCostForm", "OpenOtherCostForm();", true);
        }

        protected void AddCostDummyLink_Click(object sender, EventArgs e)
        {
            Initialize();
            long customerID = 0;
            long.TryParse(this.DummyHidden.Value, out customerID);
            if (!string.IsNullOrEmpty(this.DummyHidden.Value))
            {
                try
                {
                    OtherCostDropDownList.Items.FindByValue(this.DummyHidden.Value).Selected = true;
                    OtherCostDropDownList_SelectedIndexChanged(null, null);

                    this.DummyHidden.Value = string.Empty;
                }
                catch { }
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            long.TryParse(Request.QueryString["RequestID"], out _RequestID);
            _Request = Data.RequestDB.GetRequestByID(_RequestID);

            BaseCostDropDownList.DataSource = Data.BaseCostDB.GetBaseCostCheckableByRequestTypeID(_Request.RequestTypeID);
            BaseCostDropDownList.DataBind();
            BaseCostDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

            OtherCostDropDownList.DataSource = Data.OtherCostDB.GetOtherCostCheckable();
            OtherCostDropDownList.DataBind();
            OtherCostDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

            PaymentWayDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.PaymentWay));
            PaymentWayDropDownList.DataBind();
            PaymentWayDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

            BankDropDownList.DataSource = Data.BankDB.GetBanksCheckable();
            BankDropDownList.DataBind();
            BankDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

            PaymentTypeDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.PaymentType));
            PaymentTypeDropDownList.DataBind();
            PaymentTypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

            if (_IsOtherCost == false)
            {
                PaymentTypeDropDownList.Enabled = false;
                BaseCostRadioButton.Enabled = false;
                OtherCostRadioButton.Enabled = false;
                PaymentFicheRadioButton.Enabled = false;
                BaseCostDropDownList.Enabled = false;
                AmountSumTextBox.Enabled = false;
            }
            else
            {
                BaseCostRadioButton.Checked = false;
                PaymentFicheRadioButton.Checked = false;
                OtherCostRadioButton.Checked = true;
                PaymentRadioButton_CheckedChanged(null, null);
                //PaymentTypeListBox.SelectedValue = 2;
                BaseCostRadioButton.Enabled = false;
                OtherCostRadioButton.Enabled = false;
                PaymentFicheRadioButton.Enabled = false;
            }
        }

        private void LoadData()
        {
            RequestPayment item = new RequestPayment();
            //   item.PaymentType = (byte)_request.RequestPaymentTypeID;
            if (_PaymentID == 0)
                SavePaymentButton.Text = "ذخیره";
            else
            {
                item = Data.RequestPaymentDB.GetRequestPaymentByID(_PaymentID);

                if (item.OtherCostID != null)
                {
                    OtherCostRadioButton.Checked = true;
                    PaymentRadioButton_CheckedChanged(null, null);
                }

                if (item.BaseCostID != null)
                {
                    BaseCostRadioButton.Checked = true;
                    PaymentRadioButton_CheckedChanged(null, null);
                }

                if (item.PaymentFicheID != null)
                {
                    PaymentFicheRadioButton.Checked = true;
                    PaymentRadioButton_CheckedChanged(null, null);
                }

                SavePaymentButton.Text = "بروزرسانی";
            }

            BillIDTextBox.Text = item.BillID;
            PaymentIDTextBox.Text = item.PaymentID;

            //PaymentTypeDropDownList.SelectedValue = item.PaymentType.ToString();
            //BaseCostDropDownList.SelectedValue = item.BaseCostID.ToString();
            //PaymentFicheDropDownList.SelectedValue = item.PaymentFicheID.ToString();
            //OtherCostDropDownList.SelectedValue = item.OtherCostID.ToString();
            //PaymentWayDropDownList.SelectedValue = item.PaymentWay.ToString();
            //BankDropDownList.SelectedValue = item.BankID.ToString();

            PaymentTypeDropDownList.Items.FindByValue(item.PaymentType.ToString()).Selected = true;
            if (!item.BaseCostID.HasValue)
                BaseCostDropDownList.ClearSelection();
            else
            {
                if (BaseCostDropDownList.Items.FindByValue(item.BaseCostID.ToString()) != null)
                    BaseCostDropDownList.Items.FindByValue(item.BaseCostID.ToString()).Selected = true;
            }

            if (!item.PaymentFicheID.HasValue)
                PaymentFicheDropDownList.ClearSelection();
            else
                PaymentFicheDropDownList.Items.FindByValue(item.PaymentFicheID.ToString()).Selected = true;

            if (!item.OtherCostID.HasValue)
                OtherCostDropDownList.ClearSelection();
            else
                OtherCostDropDownList.Items.FindByValue(item.OtherCostID.ToString()).Selected = true;

            if (!item.PaymentWay.HasValue)
                PaymentWayDropDownList.ClearSelection();
            else
                PaymentWayDropDownList.Items.FindByValue(item.PaymentWay.ToString()).Selected = true;

            if (!item.BankID.HasValue)
                BankDropDownList.ClearSelection();
            else
                BankDropDownList.Items.FindByValue(item.BankID.ToString()).Selected = true;


            BaseCostDropDownList_SelectedIndexChanged(null, null);
            OtherCostDropDownList_SelectedIndexChanged(null, null);

            AmountSumTextBox.Text = item.AmountSum.ToString();
            FicheNunmberTextBox.Text = item.FicheNunmber;
            FicheDateTextBox.Text = item.FicheDate.HasValue ? Date.GetPersianDate(item.FicheDate, Date.DateStringType.Short) : string.Empty;
            PaymentDateTextBox.Text = item.PaymentDate.HasValue ? Date.GetPersianDate(item.PaymentDate, Date.DateStringType.Short) : string.Empty;
            IsActiveCheckBox.Checked = item.IsPaid.HasValue ? item.IsPaid.Value : false;

            PaymentRadioButton_CheckedChanged(null, null);
        }

        #endregion
    }
}