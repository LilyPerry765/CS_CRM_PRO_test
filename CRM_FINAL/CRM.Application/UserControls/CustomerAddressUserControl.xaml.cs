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

namespace CRM.Application.UserControls
{
    public partial class CustomerAddressUserControl : Local.UserControlBase
    {
        #region Propertes

        static long _ReqID = 0;
        public List<Address> adress;
        private string addressInfoExpanderHeader = "آدرس";
        private Request _Request;
        private Data.ChangeLocation _ChangeLocation;
        private Data.Address _Address;
        private Data.Telephone _Telephone;
        private TakePossession _TakePossession;
        public string AddressInfoExpanderHeader
        {
            get { return addressInfoExpanderHeader; }
            set { addressInfoExpanderHeader = value; }
        }

        #endregion

        #region Constructors

        public CustomerAddressUserControl()
        {
            InitializeComponent();
        }

        public CustomerAddressUserControl(long requestID)
            : this()
        {
            _ReqID = requestID;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        #endregion

        #region Event Handlers

        public void LoadData(object sender, RoutedEventArgs e)
        {
            AddressInfoExpander.Header = addressInfoExpanderHeader;

            if (_ReqID != 0)
            {
                _Request = Data.RequestDB.GetRequestByID( _ReqID);

                switch (_Request.RequestTypeID)
                {
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    case (int)DB.RequestType.ChangeLocationCenterInside:
                        {
                            _ChangeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID( _ReqID);
                            _Address =Data.AddressDB.GetAddressByID((long)_ChangeLocation.OldInstallAddressID);
                            OldPostalCodeTextBox.Text = _Address.PostalCode;
                            OldAddressTextBox.Text = _Address.AddressContent;

                            _Address = Data.AddressDB.GetAddressByID( (long)_ChangeLocation.NewInstallAddressID);
                            NewPostalCodeTextBox.Text = _Address.PostalCode;
                            NewAddressTextBox.Text = _Address.AddressContent;

                            _Telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(_ChangeLocation.NearestTelephon ?? 0);

                            if (_Telephone != null && _Telephone.InstallAddressID != null)
                            {
                                _Address = Data.AddressDB.GetAddressByID( (long)_Telephone.InstallAddressID);
                                NearestPostalCodeTextBox.Text = _Address.PostalCode;
                                NearestAddressTextBox.Text = _Address.AddressContent;
                            }
                            break;
                        }
                    case (int)DB.RequestType.Dischargin:
                        {

                            OldPostalCodeInstallLable.Visibility = Visibility.Collapsed;
                            OldAddressLable.Visibility = Visibility.Collapsed;
                            OldPostalCodeTextBox.Visibility = Visibility.Collapsed;
                            OldAddressTextBox.Visibility = Visibility.Collapsed;

                            NearestPostalCodeInstallLable.Visibility = Visibility.Collapsed;
                            NearestAddressLable.Visibility = Visibility.Collapsed;
                            NearestPostalCodeTextBox.Visibility = Visibility.Collapsed;
                            NearestAddressTextBox.Visibility = Visibility.Collapsed;

                            _TakePossession = Data.TakePossessionDB.GetTakePossessionByID(_Request.ID);
                            Address address = Data.AddressDB.GetAddressByID((long)_TakePossession.InstallAddressID);
                            NewPostalCodeTextBox.Text = address.PostalCode;
                            NewAddressTextBox.Text = address.AddressContent;
                        }
                     break;
                }
               
            }
        }

        #endregion
    }
}
