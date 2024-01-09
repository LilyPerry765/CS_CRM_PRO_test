using CRM.Application.Views;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.Website.UserControl
{
    public partial class TelephoneInformation : System.Web.UI.UserControl
    {
        #region Properties

        private long _telephoneNo;
        private long _requestTypeID;

        public long TelephoneNo
        {
            get { return _telephoneNo; }
            set { _telephoneNo = value; }
        }

        public long RequestTypeID
        {
            get { return _requestTypeID; }
            set { _requestTypeID = value; }
        }
        private TelephoneInfoForRequest _telephoneInfoForRequest = new TelephoneInfoForRequest();

        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            //TelephoneNo = HttpContext.Current.Request.QueryString["TelephoneNo"].ToString() == string.Empty ? (long?)null : long.Parse(HttpContext.Current.Request.QueryString["TelephoneNo"].ToString());
           // LoadData();
        }
        #region Methods

        public void LoadData()
        {
            switch (_requestTypeID)
            {
                case (int)DB.RequestType.Failure117:
                case (int)DB.RequestType.ADSL:
                case (int)DB.RequestType.ADSLChangeService:
                case (int)DB.RequestType.ADSLChangeIP:
                case (int)DB.RequestType.ADSLInstall:
                case (int)DB.RequestType.ADSLDischarge:
                case (int)DB.RequestType.ADSLChangePort:
                case (int)DB.RequestType.ADSLSellTraffic:
                case (int)DB.RequestType.ADSLChangePlace:
                    Service1 service = new Service1();
                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _telephoneNo.ToString());
                    if (telephoneInfo.Rows.Count != 0)
                    {
                        _telephoneInfoForRequest = new TelephoneInfoForRequest();
                        _telephoneInfoForRequest.TelephoneNo = Convert.ToInt64(_telephoneNo);
                        _telephoneInfoForRequest.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MelliCode"].ToString();
                        _telephoneInfoForRequest.CustomerName = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                        _telephoneInfoForRequest.CustomerTelephone = "";
                        _telephoneInfoForRequest.Mobile = telephoneInfo.Rows[0]["MOBILE"].ToString();
                        _telephoneInfoForRequest.Center = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                        _telephoneInfoForRequest.PostalCode = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                        _telephoneInfoForRequest.Address = telephoneInfo.Rows[0]["ADDRESS"].ToString();

                        int sellerGroupType = ADSLSellerGroupDB.GetADSLSellerGroupTypebyUserID(DB.CurrentUser.ID);
                        if (sellerGroupType != 0)
                        {
                            switch (sellerGroupType)
                            {
                                case (byte)DB.ADSLSellerGroupType.Marketer:

                                    _telephoneInfoForRequest.CustomerName = "*";
                                    _telephoneInfoForRequest.PostalCode = "*";
                                    _telephoneInfoForRequest.Address = "*";

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_telephoneNo);

                        if (_telephoneNo != null)
                        {
                            if (telephone.CustomerID != null)
                            {
                                Customer customer = CustomerDB.GetCustomerByID((long)telephone.CustomerID);

                                _telephoneInfoForRequest = new TelephoneInfoForRequest();
                                _telephoneInfoForRequest.TelephoneNo = Convert.ToInt64(_telephoneNo);
                                _telephoneInfoForRequest.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                                _telephoneInfoForRequest.CustomerName = customer.FirstNameOrTitle + " " + customer.LastName;
                                _telephoneInfoForRequest.CustomerTelephone = "";
                                _telephoneInfoForRequest.Mobile = customer.MobileNo;
                                _telephoneInfoForRequest.Center = "";
                                _telephoneInfoForRequest.PostalCode = "";
                                _telephoneInfoForRequest.Address = "";
                            }
                        }
                        else
                            throw new Exception("شماره تلفن موجود نمی باشد !");
                    }
                    break;

                default:
                    _telephoneInfoForRequest = DB.GetTelephoneInfoForRequest(_telephoneNo);
                    break;
            }
            PhoneNoTextBox.Text = _telephoneInfoForRequest.TelephoneNo.ToString();
            NationalCodeOrRecordNoTextBox.Text = _telephoneInfoForRequest.NationalCodeOrRecordNo;
            CustomerTelephoneTextBox.Text = _telephoneInfoForRequest.CustomerTelephone;
            CenterTextBox.Text = _telephoneInfoForRequest.Center;
            AddressTextBox.Text = _telephoneInfoForRequest.Address;
            CustomerNameTextBox.Text = _telephoneInfoForRequest.CustomerName;
            MobileTextBox.Text = _telephoneInfoForRequest.Mobile;
            PostalCodeTextBox.Text = _telephoneInfoForRequest.PostalCode;
        }

        #endregion

        #region Event Handlers

        //private void EditCustomerButto_Click(object sender, RoutedEventArgs e)
        //{
        //    ADSLEditMobileNoForm window = new ADSLEditMobileNoForm();
        //    window.ShowDialog();
        //}

        #endregion
    }
}