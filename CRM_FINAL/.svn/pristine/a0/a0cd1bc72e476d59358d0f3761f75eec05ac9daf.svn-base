using CRM.Application.Views;
using CRM.Data;
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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for PrivateWireUserControl.xaml
    /// </summary>
    public partial class PrivateWireUserControl : Local.UserControlBase
    {
        private long _requestID;
        public Customer Customer { get; set; }
        public Address SourceInstallAddress { get; set; }
        public Address TargetInstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public Request _request { get; set; }
       // public CRM.Data.PrivateWire _priverWire;
        private List<CheckableItem> telephoneList { get; set; }
        public long oldTelephone { get; set; }

        private int _centerID = 0;
        public int CenterID 
            {
                set
                {
                    _centerID = value;
                   
                        telephoneList  = Data.TelephoneDB.GetPrivateWireCheckableItemTelephoneBy(_centerID);
                }
            }

        private int _targetCityID = 0;
        public int TargetCityID
        {
            get
            {
                return _targetCityID;
            }
            set
            {
                _targetCityID = value;
                TargetComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(_targetCityID);
            }
        }

        public PrivateWireUserControl()
        {
            InitializeComponent();
        }

        public PrivateWireUserControl(long requestID)
        {
            InitializeComponent();
            Initialize();
            this._requestID = requestID;

        }
        private void Initialize()
        {
         
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          //  LoadData();
        }

        //private void LoadData()
        //{

        //    if (_IsLoaded)
        //        return;
        //    else
        //        _IsLoaded = true;

        //    oldTelephone = 0;
        //    if (_requestID == 0)
        //    {
        //        _request = new Request();
        //        _priverWire = new Data.PrivateWire();
        //    }
        //    else
        //    {
        //        _request = Data.RequestDB.GetRequestByID(_requestID);
        //        _priverWire = Data.PrivateWireDB.GetPrivateWireByRequestID(_requestID);
        //        oldTelephone = _priverWire.TelephoneNo;

        //        if (_priverWire.TelephoneNo != 0)
        //        {
        //            telephoneList.Add(new CheckableItem { LongID = _priverWire.TelephoneNo, Name = _priverWire.TelephoneNo.ToString(), IsChecked = false });
        //        }
        //        TelephoneTypeComboBox.SelectedValue = _priverWire.CustomerTypeID;
        //        TelephoneTypecomboBox_SelectionChanged(null, null);
        //        TelephoneTypeGroupComboBox.SelectedValue = _priverWire.CustomerGroupID;

        //        Customer = Data.CustomerDB.GetCustomerByID(_request.CustomerID ?? 0);
        //        NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
        //        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;


        //        SourceInstallAddress = Data.AddressDB.GetAddressByID((long)_priverWire.SourceInstallAddressID);
        //        SourceInstallPostalCodeTextBox.Text = SourceInstallAddress.PostalCode;
        //        SourceInstallAddressTextBox.Text = SourceInstallAddress.AddressContent;


        //        SourceInstallAddress = Data.AddressDB.GetAddressByID((long)_priverWire.TargetInstallAddressID);
        //        TargetInstallPostalCodeTextBox.Text = SourceInstallAddress.PostalCode;
        //        TargetInstallAddressTextBox.Text = SourceInstallAddress.AddressContent;

        //        CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)_priverWire.CorrespondenceAddressID);
        //        CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
        //        CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

        //        TargetComboBox.SelectedValue = _priverWire.TargetCenter;

        //    }
        //    TelephoneNoComboBox.ItemsSource = telephoneList;
        //    this.DataContext = _priverWire;
        //}

        private void CustomerTelephoneInfo_Click(object sender, RoutedEventArgs e)
        {
            if (Customer != null)
            {
                CustomerTelephoneInfoForm window = new CustomerTelephoneInfoForm(Customer.ID);
                window.ShowDialog();
            }

        }

        private void EditSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Customer != null)
            {
                CustomerForm window = new CustomerForm(Customer.ID);
                window.ShowDialog();
            }
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                //if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
                //{ MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }

                Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                if (Customer != null)
                {
                    _request.CustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }

                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.CenterID = _centerID;
                    customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                    customerForm.ShowDialog();
                    if (customerForm.DialogResult ?? false)
                    {
                        Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                        _request.CustomerID = Customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.CenterID = _centerID;
                customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = DB.GetEntitybyID<Customer>(customerForm.ID);
                    _request.CustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }
            }
        }

        private void TelephoneTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneTypeComboBox.SelectedValue != null)
            {
                TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
            }
        }

        private void SearchSourceInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SourceInstallPostalCodeTextBox.Text.Trim()))
            {
                SourceInstallAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCode(SourceInstallPostalCodeTextBox.Text.Trim()).Count != 0)
                {
                    SourceInstallAddress = Data.AddressDB.GetAddressByPostalCode(SourceInstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    //_priverWire.SourceInstallAddressID = SourceInstallAddress.ID;
                    SourceInstallAddressTextBox.Text = string.Empty;
                    SourceInstallAddressTextBox.Text = SourceInstallAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = SourceInstallPostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        SourceInstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                        //_priverWire.SourceInstallAddressID = SourceInstallAddress.ID;

                        SourceInstallPostalCodeTextBox.Text = string.Empty;
                        SourceInstallPostalCodeTextBox.Text = SourceInstallAddress.PostalCode;
                        SourceInstallAddressTextBox.Text = string.Empty;
                        SourceInstallAddressTextBox.Text = SourceInstallAddress.AddressContent;
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = SourceInstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    SourceInstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                    //_priverWire.SourceInstallAddressID = SourceInstallAddress.ID;

                    SourceInstallPostalCodeTextBox.Text = string.Empty;
                    SourceInstallPostalCodeTextBox.Text = SourceInstallAddress.PostalCode;
                    SourceInstallAddressTextBox.Text = string.Empty;
                    SourceInstallAddressTextBox.Text = SourceInstallAddress.AddressContent;
                }
            }
        }

        private void SearchCorrespondenceAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CorrespondencePostalCodeTextBox.Text.Trim()))
            {
                CorrespondenceAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim()).Count != 0)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    //_priverWire.CorrespondenceAddressID = CorrespondenceAddress.ID;
                    CorrespondenceAddressTextBox.Text = string.Empty;
                    CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = CorrespondencePostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);
                        //_priverWire.CorrespondenceAddressID = CorrespondenceAddress.ID;

                        CorrespondencePostalCodeTextBox.Text = string.Empty;
                        CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                        CorrespondenceAddressTextBox.Text = string.Empty;
                        CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = CorrespondencePostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);
                    //_priverWire.CorrespondenceAddressID = CorrespondenceAddress.ID;

                    CorrespondencePostalCodeTextBox.Text = string.Empty;
                    CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    CorrespondenceAddressTextBox.Text = string.Empty;
                    CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }
            }
        }

        private void CorrespondenceAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (CorrespondenceAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(CorrespondenceAddress.ID);
                window.ShowDialog();
                SearchCorrespondenceAddress_Click(null, null);
            }
        }

        private void SourceInstallAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (SourceInstallAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(SourceInstallAddress.ID);
                window.ShowDialog();
                SearchSourceInstallAddress_Click(null, null);
            }
        }

        private void TargetInstallAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetInstallAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(TargetInstallAddress.ID);
                window.ShowDialog();
                SearchTargetInstallAddress_Click(null, null);
            }
        }

        private void SearchTargetInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TargetInstallPostalCodeTextBox.Text.Trim()))
            {
                TargetInstallAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCode(TargetInstallPostalCodeTextBox.Text.Trim()).Count != 0)
                {
                    TargetInstallAddress = Data.AddressDB.GetAddressByPostalCode(TargetInstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    //_priverWire.TargetInstallAddressID = TargetInstallAddress.ID;
                    TargetInstallAddressTextBox.Text = string.Empty;
                    TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = TargetInstallPostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        TargetInstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                        //_priverWire.TargetInstallAddressID = TargetInstallAddress.ID;

                        TargetInstallPostalCodeTextBox.Text = string.Empty;
                        TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                        TargetInstallAddressTextBox.Text = string.Empty;
                        TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = TargetInstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    TargetInstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                    //_priverWire.TargetInstallAddressID = TargetInstallAddress.ID;

                    TargetInstallPostalCodeTextBox.Text = string.Empty;
                    TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                    TargetInstallAddressTextBox.Text = string.Empty;
                    TargetInstallAddressTextBox.Text = SourceInstallAddress.AddressContent;
                }
            }
        }
    }
}
