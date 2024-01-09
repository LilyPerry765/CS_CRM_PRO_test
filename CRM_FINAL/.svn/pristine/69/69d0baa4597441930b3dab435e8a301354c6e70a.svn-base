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
using System.Collections.ObjectModel;
using Enterprise;
using CRM.Application.UserControls;
using CRM.Application.Views;

namespace CRM.Application.Views
{
    public partial class InquiryForm : Local.RequestFormBase
    {
        #region Properties

        Inquiry _Inquiry = new Inquiry();
        Request _Request { get; set; }
        Center _Center { get; set; }
        Customer _OldCustomer { get; set; }
        Customer _NewCustomer { get; set; }
        Telephone _Telephone { get; set; }
        Office _Office { get; set; }
        CRM.Data.ChangeName _ChangeName { get; set; }

        #endregion

        #region Constructors

        public InquiryForm(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _Request = Data.RequestDB.GetRequestByID( RequestID);
            _Center = Data.CenterDB.GetCenterById(  _Request.CenterID);
            _OldCustomer = Data.CustomerDB.GetCustomerByID((long)_Request.CustomerID);
            _ChangeName = Data.ChangeNameDB.GetChangeNameByID(RequestID);

            _NewCustomer = Data.CustomerDB.GetCustomerByID((long)_ChangeName.NewCustomerID);

            if (Data.InquiryDB.GetInquiryCountByRequestID( RequestID) == 0)
            {
                RequestForChangeNameDB.SaveInquiryRequest(_Inquiry, _Request, true);
                _Inquiry = Data.InquiryDB.GetInquiryByRequestID( RequestID);
            }
            else
                _Inquiry = Data.InquiryDB.GetInquiryByRequestID( RequestID);

            if (_Request != null)
            {
                InquiryResponseDateTextBlock.Text = Date.GetPersianDate(_Request.RequestDate, Date.DateStringType.Short);
                InquiryResponseNoTextBlock.Text = _Request.RequestLetterNo;
            }

            if (_Center != null)
            {
                CenterNameTextBlock1.Text = _Center.CenterName;
                CenterNameTextBlock.Text = _Center.CenterName;
            }

            if (_OldCustomer != null)
            {
                FirstNameOrTitleTextBlock.Text = _OldCustomer.FirstNameOrTitle;
                LastNameTextBlock.Text = _OldCustomer.LastName;              
            }

            if (_NewCustomer != null)
            {

                FirstNameOrTitleTextBlock2.Text = _NewCustomer.FirstNameOrTitle;
                LastNameTextBlock2.Text = _NewCustomer.LastName;
            }

            if (_ChangeName != null)
            {
                TelephoneNoTextBlock.Text = _Request.TelephoneNo.ToString();
                LastBillDate.Text = Helper.GetPersianDate(_ChangeName.LastBillDate, Helper.DateStringType.Short);
            }

            if (_Office != null)
                OfficeCodeTextBlock.Text = "";

            if (_Inquiry != null)
            {
                CounterNoTextBox.Text = _Inquiry.CounterNo;
                DebtTextBox.Text = _Inquiry.Debt;
                CommentTextBox.Text = _Inquiry.Commnet;
            }

            //if (_Inquiry.Status == null)
                ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
            //else
            //    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancel };
        }

        public override bool Confirm()
        {
            try
            {
                _Inquiry.InquiryResponseNo = "";
                _Inquiry.InquiryResponseDate = DB.GetServerDate();
                _Inquiry.CounterNo = CounterNoTextBox.Text;
                _Inquiry.Debt = DebtTextBox.Text;
                _Inquiry.Commnet = CommentTextBox.Text;
                _Inquiry.Status = (byte)DB.InquiryStatus.Accept;

                RequestForChangeNameDB.SaveInquiryRequest(_Inquiry, _Request, false);
                
                IsConfirmSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در تایید استعلام ", ex);
            }

            return IsConfirmSuccess;
        }

        public override bool Deny()
        {
            try
            {
                _Inquiry.InquiryResponseNo = "";
                _Inquiry.InquiryResponseDate = DB.GetServerDate();
                _Inquiry.CounterNo = CounterNoTextBox.Text;
                _Inquiry.Debt = DebtTextBox.Text;
                _Inquiry.Commnet = CommentTextBox.Text;
                _Inquiry.Status = (byte)DB.InquiryStatus.Deny;

                RequestForChangeNameDB.SaveInquiryRequest(_Inquiry, _Request, false);

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رد استعلام ", ex);
            }

            return IsRejectSuccess;
        }

        #endregion
    }
}
