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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;
using CRM.Application.Views;

namespace CRM.Application.UserControls
{
    public partial class TelephoneInformation : UserControl
    {
        #region Properties

        private TelephoneInfoForRequest _TelephoneInfoForRequest { get; set; }
        static Request _Request { get; set; }
        private string city = string.Empty;

        #endregion

        #region Constructors
        public TelephoneInformation(long requestID)
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();

            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
             _Request = Data.RequestDB.GetRequestByID(requestID);

            switch (_Request.RequestTypeID)
            {
              
                case (int)DB.RequestType.Wireless:
                    Service1 serviceADSL = new Service1();

                    WirelessRequest _WirelessRequest = WirelessRequestDB.GetWirelessRequestByID(requestID);

                    System.Data.DataTable telephoneInfoADSL = new System.Data.DataTable();
                    if (DB.City.ToString().ToLower() == "semnan")
                    {

                        if (_Request.TelephoneNo.HasValue)
                        {
                            Data.Service service = new Data.Service();
                            if (_Request.TelephoneNo.HasValue)
                            {
                                service.CallParammeter = (byte)DB.ServicCallParameter.ByTelephone;
                                service.Telephone = _Request.TelephoneNo ?? 0;
                            
                            }
                            else
                            {
                                service.CallParammeter = (byte)DB.ServicCallParameter.ByNationalCode;
                                Customer customer = CustomerDB.GetCustomerByID(_WirelessRequest.CustomerOwnerID);
                                service.NationalCode = customer.NationalCodeOrRecordNo;
                            }

                            telephoneInfoADSL = service.GetSMSService();

                            if (telephoneInfoADSL.Rows.Count != 0)
                            {
                                _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                                _TelephoneInfoForRequest.TelephoneNo = _Request.TelephoneNo.Value;
                                _TelephoneInfoForRequest.NationalCodeOrRecordNo = telephoneInfoADSL.Rows[0]["MelliCode"].ToString();
                                _TelephoneInfoForRequest.CustomerName = telephoneInfoADSL.Rows[0]["FirstName"].ToString() + " " + telephoneInfoADSL.Rows[0]["Lastname"].ToString();
                                _TelephoneInfoForRequest.CustomerTelephone = "";
                                _TelephoneInfoForRequest.Mobile = telephoneInfoADSL.Rows[0]["MOBILE"].ToString();
                                _TelephoneInfoForRequest.Center = telephoneInfoADSL.Rows[0]["CEN_NAME"].ToString();
                                _TelephoneInfoForRequest.PostalCode = telephoneInfoADSL.Rows[0]["CODE_POSTI"].ToString();
                                _TelephoneInfoForRequest.Address = telephoneInfoADSL.Rows[0]["ADDRESS"].ToString();

                                int sellerGroupType = ADSLSellerGroupDB.GetADSLSellerGroupTypebyUserID(DB.CurrentUser.ID);
                                if (sellerGroupType != 0)
                                {
                                    switch (sellerGroupType)
                                    {
                                        case (byte)DB.ADSLSellerGroupType.Marketer:

                                            _TelephoneInfoForRequest.CustomerName = "*";
                                            _TelephoneInfoForRequest.PostalCode = "*";
                                            _TelephoneInfoForRequest.Address = "*";

                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_Request.TelephoneNo.Value);

                                if (telephone != null)
                                {
                                    if (telephone.CustomerID != null)
                                    {
                                        Customer customer = CustomerDB.GetCustomerByID((long)telephone.CustomerID);

                                        _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                                        _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(_Request.TelephoneNo.Value);
                                        _TelephoneInfoForRequest.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                                        _TelephoneInfoForRequest.CustomerName = customer.FirstNameOrTitle + " " + customer.LastName;
                                        _TelephoneInfoForRequest.CustomerTelephone = "";
                                        _TelephoneInfoForRequest.Mobile = customer.MobileNo;
                                        _TelephoneInfoForRequest.Center = "";
                                        _TelephoneInfoForRequest.PostalCode = "";
                                        _TelephoneInfoForRequest.Address = "";
                                    }
                                }
                                else
                                    throw new Exception("شماره تلفن موجود نمی باشد !");
                            }
                        }
                        else
                        {
                            Customer _customer = CustomerDB.GetCustomerByID(_WirelessRequest.CustomerOwnerID);
                            Address _installAddress = AddressDB.GetAddressByID(_WirelessRequest.AddressID);

                            _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                            _TelephoneInfoForRequest.TelephoneNo = 0;
                            _TelephoneInfoForRequest.NationalCodeOrRecordNo = _customer.NationalCodeOrRecordNo;
                            _TelephoneInfoForRequest.CustomerName = _customer.FirstNameOrTitle + " " + _customer.LastName;
                            _TelephoneInfoForRequest.CustomerTelephone = _customer.UrgentTelNo;
                            _TelephoneInfoForRequest.Mobile = _customer.MobileNo;
                            _TelephoneInfoForRequest.Center = "";
                            _TelephoneInfoForRequest.PostalCode = _installAddress.PostalCode;
                            _TelephoneInfoForRequest.Address = _installAddress.AddressContent;

                        }
                    }
                    else
                    {
                        Customer _customer = CustomerDB.GetCustomerByID(_WirelessRequest.CustomerOwnerID);
                        Address _installAddress = AddressDB.GetAddressByID(_WirelessRequest.AddressID);

                        _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                        _TelephoneInfoForRequest.TelephoneNo = 0;
                        _TelephoneInfoForRequest.NationalCodeOrRecordNo = _customer.NationalCodeOrRecordNo;
                        _TelephoneInfoForRequest.CustomerName = _customer.FirstNameOrTitle + " " + _customer.LastName;
                        _TelephoneInfoForRequest.CustomerTelephone = _customer.UrgentTelNo;
                        _TelephoneInfoForRequest.Mobile = _customer.MobileNo;
                        _TelephoneInfoForRequest.Center = "";
                        _TelephoneInfoForRequest.PostalCode = _installAddress.PostalCode;
                        _TelephoneInfoForRequest.Address = _installAddress.AddressContent;
                    }
                    break;
                default:
                    break;
            }

            LoadData();
        }

        public TelephoneInformation(long telephoneNo, int requestTypeID)
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();

            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();

            switch (requestTypeID)
            {
                case (int)DB.RequestType.Failure117:
                    if (city == "semnan")
                    {
                        Service1 serviceFailure = new Service1();
                        System.Data.DataTable telephoneInfoFailure = serviceFailure.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());
                        if (telephoneInfoFailure.Rows.Count != 0)
                        {
                            _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                            _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                            _TelephoneInfoForRequest.NationalCodeOrRecordNo = telephoneInfoFailure.Rows[0]["MelliCode"].ToString();
                            _TelephoneInfoForRequest.CustomerName = telephoneInfoFailure.Rows[0]["FirstName"].ToString() + " " + telephoneInfoFailure.Rows[0]["Lastname"].ToString();
                            _TelephoneInfoForRequest.CustomerTelephone = "";
                            _TelephoneInfoForRequest.Mobile = telephoneInfoFailure.Rows[0]["MOBILE"].ToString();
                            _TelephoneInfoForRequest.Center = telephoneInfoFailure.Rows[0]["CEN_NAME"].ToString();
                            _TelephoneInfoForRequest.PostalCode = telephoneInfoFailure.Rows[0]["CODE_POSTI"].ToString();
                            _TelephoneInfoForRequest.Address = telephoneInfoFailure.Rows[0]["ADDRESS"].ToString();                            
                        }
                        else
                        {
                            Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                            if (telephoneNo != null)
                            {
                                if (telephone.CustomerID != null)
                                {
                                    Customer customer = CustomerDB.GetCustomerByID((long)telephone.CustomerID);

                                    _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                                    _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                                    _TelephoneInfoForRequest.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                                    _TelephoneInfoForRequest.CustomerName = customer.FirstNameOrTitle + " " + customer.LastName;
                                    _TelephoneInfoForRequest.CustomerTelephone = "";
                                    _TelephoneInfoForRequest.Mobile = customer.MobileNo;
                                    _TelephoneInfoForRequest.Center = "";
                                    _TelephoneInfoForRequest.PostalCode = "";
                                    _TelephoneInfoForRequest.Address = "";
                                }
                            }
                            else
                                throw new Exception("شماره تلفن موجود نمی باشد !");
                        }
                    }

                    if (city == "kermanshah")
                    {
                        Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                        if (telephone != null)
                        {
                            _TelephoneInfoForRequest = new TelephoneInfoForRequest();

                            if (telephone.CustomerID != null)
                            {
                                Customer customer = CustomerDB.GetCustomerByID((long)telephone.CustomerID);                                
                                
                                _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                                _TelephoneInfoForRequest.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                                _TelephoneInfoForRequest.CustomerName = customer.FirstNameOrTitle + " " + customer.LastName;
                                _TelephoneInfoForRequest.CustomerTelephone = customer.UrgentTelNo;
                                _TelephoneInfoForRequest.Mobile = customer.MobileNo;                                
                            }

                            if (telephone.InstallAddressID != null)
                            {
                                Address address = AddressDB.GetAddressByID((long)telephone.InstallAddressID);

                                _TelephoneInfoForRequest.PostalCode = address.PostalCode;
                                _TelephoneInfoForRequest.Address = address.AddressContent;
                            }

                            _TelephoneInfoForRequest.Center = CenterDB.GetCenterNamebyCenterID(telephone.CenterID);
                        }
                        else
                        {
                            TelephoneTemp telephoneTemp = TelephoneDB.GetTelephoneTemp(telephoneNo);

                            if (telephoneTemp != null)
                            {
                                _TelephoneInfoForRequest = new TelephoneInfoForRequest();

                                _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                                _TelephoneInfoForRequest.NationalCodeOrRecordNo = "";
                                _TelephoneInfoForRequest.CustomerName = "";
                                _TelephoneInfoForRequest.CustomerTelephone = "";
                                _TelephoneInfoForRequest.Mobile = "";
                                _TelephoneInfoForRequest.Center = CenterDB.GetCenterNamebyCenterID((int)telephoneTemp.CenterID);
                                _TelephoneInfoForRequest.PostalCode = "";
                                _TelephoneInfoForRequest.Address = "";
                            }
                            else
                                throw new Exception("شماره تلفن موجود نمی باشد !");
                        }
                    }

                    break;

                case (int)DB.RequestType.ADSL:
                case (int)DB.RequestType.Wireless:
          
                    System.Data.DataTable telephoneInfoADSL = new System.Data.DataTable();
                    if (DB.City.ToString().ToLower() == "semnan")
                    {
                        Data.Service service = new  Data.Service();

                        service.CallParammeter = (byte)DB.ServicCallParameter.ByTelephone;
                        service.Telephone = telephoneNo;
                        telephoneInfoADSL = service.GetSMSService();
                    }
                    if (telephoneInfoADSL.Rows.Count != 0)
                    {
                        _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                        _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                        _TelephoneInfoForRequest.NationalCodeOrRecordNo = telephoneInfoADSL.Rows[0]["MelliCode"].ToString();
                        _TelephoneInfoForRequest.CustomerName = telephoneInfoADSL.Rows[0]["FirstName"].ToString() + " " + telephoneInfoADSL.Rows[0]["Lastname"].ToString();
                        _TelephoneInfoForRequest.CustomerTelephone = "";
                        _TelephoneInfoForRequest.Mobile = telephoneInfoADSL.Rows[0]["MOBILE"].ToString();
                        _TelephoneInfoForRequest.Center = telephoneInfoADSL.Rows[0]["CEN_NAME"].ToString();
                        _TelephoneInfoForRequest.PostalCode = telephoneInfoADSL.Rows[0]["CODE_POSTI"].ToString();
                        _TelephoneInfoForRequest.Address = telephoneInfoADSL.Rows[0]["ADDRESS"].ToString();

                        int sellerGroupType = ADSLSellerGroupDB.GetADSLSellerGroupTypebyUserID(DB.CurrentUser.ID);
                        if (sellerGroupType != 0)
                        {
                            switch (sellerGroupType)
                            {
                                case (byte)DB.ADSLSellerGroupType.Marketer:

                                    _TelephoneInfoForRequest.CustomerName = "*";
                                    _TelephoneInfoForRequest.PostalCode = "*";
                                    _TelephoneInfoForRequest.Address = "*";

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                        if (telephone != null)
                        {
                            if (telephone.CustomerID != null)
                            {
                                Customer customer = CustomerDB.GetCustomerByID((long)telephone.CustomerID);

                                _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                                _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                                _TelephoneInfoForRequest.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                                _TelephoneInfoForRequest.CustomerName = customer.FirstNameOrTitle + " " + customer.LastName;
                                _TelephoneInfoForRequest.CustomerTelephone = "";
                                _TelephoneInfoForRequest.Mobile = customer.MobileNo;
                                _TelephoneInfoForRequest.Center = "";
                                _TelephoneInfoForRequest.PostalCode = "";
                                _TelephoneInfoForRequest.Address = "";
                            }
                        }
                        //else
                        //    throw new Exception("شماره تلفن موجود نمی باشد !");
                    }
                    break;
                //case (int)DB.RequestType.Wireless:
                //    WirelessRequest _WirelessRequest = WirelessRequestDB.GetWirelessRequestByID(_Request.ID);
                //    Customer _customer = CustomerDB.GetCustomerByID(_WirelessRequest.CustomerOwnerID);
                //    Service1 serviceWireless = new Service1();
                //    System.Data.DataTable telephoneInfoWireless = new System.Data.DataTable();
                //    if (DB.City.ToString().ToLower() == "semnan")
                //    {
                //        telephoneInfoADSL = serviceWireless.GetInformationByNationalCode("Admin", "alibaba123", _customer.NationalCodeOrRecordNo);
                //    }
                //    if (telephoneInfoWireless.Rows.Count != 0)
                //    {
                //        _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                //        _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                //        _TelephoneInfoForRequest.NationalCodeOrRecordNo = telephoneInfoWireless.Rows[0]["MelliCode"].ToString();
                //        _TelephoneInfoForRequest.CustomerName = telephoneInfoWireless.Rows[0]["FirstName"].ToString() + " " + telephoneInfoWireless.Rows[0]["Lastname"].ToString();
                //        _TelephoneInfoForRequest.CustomerTelephone = "";
                //        _TelephoneInfoForRequest.Mobile = telephoneInfoWireless.Rows[0]["MOBILE"].ToString();
                //        _TelephoneInfoForRequest.Center = telephoneInfoWireless.Rows[0]["CEN_NAME"].ToString();
                //        _TelephoneInfoForRequest.PostalCode = telephoneInfoWireless.Rows[0]["CODE_POSTI"].ToString();
                //        _TelephoneInfoForRequest.Address = telephoneInfoWireless.Rows[0]["ADDRESS"].ToString();

                //        int sellerGroupType = ADSLSellerGroupDB.GetADSLSellerGroupTypebyUserID(DB.CurrentUser.ID);
                //        if (sellerGroupType != 0)
                //        {
                //            switch (sellerGroupType)
                //            {
                //                case (byte)DB.ADSLSellerGroupType.Marketer:

                //                    _TelephoneInfoForRequest.CustomerName = "*";
                //                    _TelephoneInfoForRequest.PostalCode = "*";
                //                    _TelephoneInfoForRequest.Address = "*";

                //                    break;
                //                default:
                //                    break;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                //        if (telephone != null)
                //        {
                //            if (telephone.CustomerID != null)
                //            {
                //                Customer customer = CustomerDB.GetCustomerByID((long)telephone.CustomerID);

                //                _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                //                _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                //                _TelephoneInfoForRequest.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                //                _TelephoneInfoForRequest.CustomerName = customer.FirstNameOrTitle + " " + customer.LastName;
                //                _TelephoneInfoForRequest.CustomerTelephone = "";
                //                _TelephoneInfoForRequest.Mobile = customer.MobileNo;
                //                _TelephoneInfoForRequest.Center = "";
                //                _TelephoneInfoForRequest.PostalCode = "";
                //                _TelephoneInfoForRequest.Address = "";
                //            }
                //        }
                //        //else
                //        //    throw new Exception("شماره تلفن موجود نمی باشد !");
                //    }
                //    break;

                case (int)DB.RequestType.ADSLChangeService:
                case (int)DB.RequestType.ADSLChangeIP:
                case (int)DB.RequestType.ADSLInstall:
                case (int)DB.RequestType.ADSLDischarge:
                case (int)DB.RequestType.ADSLChangePort:
                case (int)DB.RequestType.ADSLSellTraffic:
                case (int)DB.RequestType.ADSLChangePlace:
                case (int)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:

                    Data.ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(telephoneNo);

                    if (aDSL != null)
                    {
                        if (aDSL.CustomerOwnerID != null)
                        {
                            Customer customet = CustomerDB.GetCustomerByID(Convert.ToInt64(aDSL.CustomerOwnerID));

                            _TelephoneInfoForRequest = new TelephoneInfoForRequest();
                            _TelephoneInfoForRequest.TelephoneNo = Convert.ToInt64(telephoneNo);
                            _TelephoneInfoForRequest.NationalCodeOrRecordNo = customet.NationalCodeOrRecordNo;// telephoneInfo.Rows[0]["MelliCode"].ToString();
                            _TelephoneInfoForRequest.CustomerName = customet.FirstNameOrTitle + " " + ((customet.LastName != null) ? customet.LastName : "");// telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                            _TelephoneInfoForRequest.CustomerTelephone = "";
                            _TelephoneInfoForRequest.Mobile = customet.MobileNo;
                        }
                    }

                    Service1 service1 = new Service1();
                    System.Data.DataTable telephoneInfo1 = service1.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());
                    if (telephoneInfo1.Rows.Count != 0)
                    {
                        if (_TelephoneInfoForRequest == null)
                            _TelephoneInfoForRequest = new TelephoneInfoForRequest();

                        _TelephoneInfoForRequest.Center = telephoneInfo1.Rows[0]["CEN_NAME"].ToString();
                        _TelephoneInfoForRequest.PostalCode = telephoneInfo1.Rows[0]["CODE_POSTI"].ToString();
                        _TelephoneInfoForRequest.Address = telephoneInfo1.Rows[0]["ADDRESS"].ToString();
                    }
                    break;

                default:
                    _TelephoneInfoForRequest = DB.GetTelephoneInfoForRequest(telephoneNo);
                    break;
            }

            LoadData();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (_TelephoneInfoForRequest != null)
            {
                TelephoneInfo.DataContext = _TelephoneInfoForRequest;

                if (!string.IsNullOrWhiteSpace(_TelephoneInfoForRequest.PostalCode))
                    if (string.Equals(_TelephoneInfoForRequest.PostalCode, "*"))
                        PostalCodeTextBox.FontFamily = new FontFamily("Tahoma");
            }
        }

        #endregion

        #region Event Handlers

        private void EditCustomerButto_Click(object sender, RoutedEventArgs e)
        {
            ADSLEditMobileNoForm window = new ADSLEditMobileNoForm();
            window.ShowDialog();
        }

        #endregion
    }
}
