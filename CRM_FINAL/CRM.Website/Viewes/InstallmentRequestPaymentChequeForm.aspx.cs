using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRM.Website.Viewes
{
    public partial class InstallmentRequestPaymentChequeForm : System.Web.UI.Page
    {
        #region Properties & Field
        private long _insatallmentRequestPaymentID = 0;
        private InstallmentRequestPayment _insatallmentRequestPayment;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["installmentRequestPaymentID"], out _insatallmentRequestPaymentID);
            LoadData();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            //{
            //    return;
            //}
            try
            {
                InstallmentRequestPayment item = new InstallmentRequestPayment();
                item.EndDate = DateTime.Parse(EndDateDateTextBox.Text);
                item.ChequeNumber = ChequeNumberTextBox.Text;

                if (Data.InstallmentRequestPaymentDB.ChechExistEndDate(item))
                {
                    throw new Exception("این تاریخ قبلا ثبت شده است");
                }
                item.Detach();
                DB.Save(item, false);
                Response.Write("<script> window.close(); </script>");
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + " خطا در ذخیره اطلاعات :" + message  + "');", true); 
            }
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (_insatallmentRequestPaymentID > 0)
            {
                _insatallmentRequestPayment = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByID(_insatallmentRequestPaymentID);
                EndDateDateTextBox.Text = _insatallmentRequestPayment.EndDate.ToString();
                ChequeNumberTextBox.Text = _insatallmentRequestPayment.ChequeNumber;
            }
        }

        #endregion

    }
}