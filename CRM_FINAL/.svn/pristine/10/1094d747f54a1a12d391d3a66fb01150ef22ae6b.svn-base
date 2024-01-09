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
    /// Interaction logic for E1FiberUseControl.xaml
    /// </summary>
    public partial class E1FiberUseControl : UserControl
    {

        private long _requestID;
        public Customer Customer { get; set; }
        public Address InstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public Request _request { get; set; }
        private int _centerID = 0;
        public CRM.Data.E1 _E1;

        public int CenterID
        {
            set { _centerID = value; }
        }

        public E1FiberUseControl()
        {
            InitializeComponent();
            Initialize();
        }

        public E1FiberUseControl(long requestID)
        {
            InitializeComponent();
            Initialize();
            this._requestID = requestID;
           
        }
        private void Initialize()
        {
            LinkTypeComboBox.ItemsSource = Data.E1LinkTypeDB.GetE1LinkTypeCheckable();
            CodeTypeComboBox.ItemsSource = Data.E1CodeTypeDB.GetE1CodeTypeCheckable();
            ChanalTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.E1ChanalType));

            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            if (_requestID == 0)
            {
                _request = new Request();
                _E1 = new Data.E1();
            }
            else
            {
                _request = Data.RequestDB.GetRequestByID(_requestID);
                _E1 = Data.E1DB.GetE1ByRequestID(_requestID);

                TelephoneTypeComboBox.SelectedValue = _E1.TelephoneType;
                TelephoneTypecomboBox_SelectionChanged(null, null);
                TelephoneTypeGroupComboBox.SelectedValue = _E1.TelephoneTypeGroup;

                Customer = Data.CustomerDB.GetCustomerByID(_request.CustomerID ?? 0);
                NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
                CustomerNameTextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;


                InstallAddress = Data.AddressDB.GetAddressByID((long)_E1.InstallAddressID);
                InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                InstallAddressTextBox.Text = InstallAddress.AddressContent;

                CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)_E1.CorrespondenceAddressID);
                CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

            }

            this.DataContext = _E1;
        }

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

                if (Data.BlackListDB.ExistNationalCodeInBlackList(NationalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد ملی در لیست سیاه قرار دارد");
                }
                else
                {
                if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
                { MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }

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

        private void SearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(InstallPostalCodeTextBox.Text.Trim()))
            {
                InstallAddressTextBox.Text = string.Empty;
                                if (BlackListDB.ExistPostallCodeInBlackList(InstallPostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {
                if (Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Count != 0)
                {
                    InstallAddress = Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    _E1.InstallAddressID = InstallAddress.ID;
                    InstallAddressTextBox.Text = string.Empty;
                    InstallAddressTextBox.Text = InstallAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = InstallPostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                        _E1.InstallAddressID = InstallAddress.ID;

                        InstallPostalCodeTextBox.Text = string.Empty;
                        InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                        InstallAddressTextBox.Text = string.Empty;
                        InstallAddressTextBox.Text = InstallAddress.AddressContent;
                    }
                }
              }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = InstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                    _E1.InstallAddressID = InstallAddress.ID;

                    InstallPostalCodeTextBox.Text = string.Empty;
                    InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                    InstallAddressTextBox.Text = string.Empty;
                    InstallAddressTextBox.Text = InstallAddress.AddressContent;
                }
            }
        }

        private void SearchCorrespondenceAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CorrespondencePostalCodeTextBox.Text.Trim()))
            {
                CorrespondenceAddressTextBox.Text = string.Empty;

                if (BlackListDB.ExistPostallCodeInBlackList(CorrespondencePostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {
                if (Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim()).Count != 0)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    _E1.CorrespondenceAddressID = CorrespondenceAddress.ID;
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
                        _E1.CorrespondenceAddressID = CorrespondenceAddress.ID;

                        CorrespondencePostalCodeTextBox.Text = string.Empty;
                        CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                        CorrespondenceAddressTextBox.Text = string.Empty;
                        CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                    }
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
                    _E1.CorrespondenceAddressID = CorrespondenceAddress.ID;

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

        private void InstallAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (InstallAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(InstallAddress.ID);
                window.ShowDialog();
                SearchInstallAddress_Click(null, null);
            }
        }
    }
}
