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
    public partial class SaleFactorForm : System.Web.UI.Page
    {
        #region Properties & Fields

        private long _requestID = 0;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["RequestID"], out _requestID);
            Initialize();
            LoadData();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            //ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print};
            ActionUserControl.LoadData();
        }

        private void LoadData()
        {
            bool hasBillID = false;
            RequestInfo requestInfo = RequestDB.GetRequestInfoByID(_requestID);

            // ADSLRequestFullViewInfo aDSLRequestInfo = ADSLRequestDB.GetADSLRequestInfo(_RequestID);
            //CustomerNameTextBox.Text = aDSLRequestInfo.CustomerName;
            CustomerNameTextBox.Text = requestInfo.CustomerName;
            RequestTypeTextBox.Text = requestInfo.RequestTypeName;
            RequestIDTextBox.Text = requestInfo.ID.ToString();
            InsertDateTextBox.Text = requestInfo.InsertDate;
            PrintDateTextBox.Text = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.DateTime);

            TelephoneNoTextBox.Text = requestInfo.TelephoneNo.ToString();
            CenterTextBox.Text = requestInfo.CenterName;

            List<RequestPayment> payments = RequestPaymentDB.GetNoPaidRequestPaymentByRequestID(_requestID, (int)DB.PaymentType.Cash);
            PaymentsGridView.DataSource = payments;
            PaymentsGridView.DataBind();

            hasBillID = RequestPaymentDB.GetNoPaidRequestPaymentHasBillID(_requestID, (int)DB.PaymentType.Cash);

            long sumAmount = RequestPaymentDB.GetAmountSumforAllPayment(_requestID, (int)DB.PaymentType.Cash);
            CostSumTextBox.Text = sumAmount.ToString();

            string billID = "";
            string paymentID = "";

            if (sumAmount != 0)
            {
                switch (requestInfo.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:
                    case (byte)DB.RequestType.ADSLChangeService:
                    case (byte)DB.RequestType.ADSLChangeIP:
                    case (byte)DB.RequestType.ADSLCutTemporary:
                        billID = DB.GenerateBillID((long)requestInfo.TelephoneNo, requestInfo.CenterID, (byte)DB.SubsidiaryCodeType.ADSL);
                        paymentID = DB.GeneratePaymentID(sumAmount, (long)requestInfo.TelephoneNo, BillIDTextBox.Text, (byte)DB.SubsidiaryCodeType.ADSL, hasBillID);
                        break;

                    default:
                        billID = DB.GenerateBillID((long)requestInfo.TelephoneNo, requestInfo.CenterID, (byte)DB.SubsidiaryCodeType.Service);
                        paymentID = DB.GeneratePaymentID(sumAmount, (long)requestInfo.TelephoneNo, BillIDTextBox.Text, (byte)DB.SubsidiaryCodeType.Service, hasBillID);
                        break;
                }

                foreach (RequestPayment currenetPayment in payments)
                {
                    currenetPayment.BillID = billID;
                    currenetPayment.PaymentID = paymentID;

                    currenetPayment.Detach();
                    DB.Save(currenetPayment);
                }
            }

            BillIDTextBox.Text = billID;
            PaymentIDTextBox.Text = paymentID;
        }

        protected void PaymentsPaymentTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["enumType"] = typeof(DB.PaymentType);
        }
        #endregion
    }
}