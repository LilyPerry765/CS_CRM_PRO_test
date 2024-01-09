using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
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
    public partial class ChangeLocationUserControl : Local.UserControlBase
    {
        #region Properties

        private long _customerID = 0;
        private long _ReqID = 0;
        private int _RequestTypeID = 0;
        private int _currentStep = 0 ;
        private int _targetCityID = 0;

        private Customer customer { get; set; }
        public Customer Customer { get; set; }
        
        public CRM.Data.ChangeLocation changeLocation = new Data.ChangeLocation();
        public Address InstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public long TelephoneNo { get; set; }
        Request request { get; set; }
        public long NewCustomerID { get; set; }

        public int TargetCityID 
        {
            get {
                return _targetCityID;
                }
            set { 
                _targetCityID = value;
                TargetComboBox.ItemsSource = Data.CenterDB.GetAllCenterByCityId(_targetCityID);
                }
        }
        public int CurrentStep
        { 
          get {return _currentStep ;} 
          set {_currentStep = value ;} 
         }

       
        #endregion

        #region Costructors

        public ChangeLocationUserControl()
        {
            InitializeComponent();
            
        }

        public ChangeLocationUserControl(long requestID, long customerID, long telephoneNo,int requestType)
            : this()
        {
            _RequestTypeID = requestType;
            _ReqID = requestID;
            _customerID = customerID;
            TelephoneNo = telephoneNo;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            customer = new Customer();
            request = Data.RequestDB.GetRequestByID(_ReqID);
            EquipmentComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Equipment));
        }


        #endregion

        #region Event Handlers
        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ReqID == 0)
            {
                changeLocation = new ChangeLocation();

                if (_RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside)
                {
                    TargetComboBox.Visibility = Visibility.Collapsed;
                    TargetLabel.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_ReqID);
                if (changeLocation.NewCustomerID != null)
                {
                    WithChangeNameCheckBox.IsChecked = true;
                    Customer = Data.CustomerDB.GetCustomerByID((long)changeLocation.NewCustomerID);
                    NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
                    SearchCustomer_Click(null, null);

                }


                Status Status = Data.StatusDB.GetStatueByStatusID(request.StatusID);
                if (Status.StatusType == (byte)DB.RequestStatusType.Observation || Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                {
                    TelephoneInfoGroupBox.Visibility = Visibility.Visible;

                    OldTelTextBox.Text = changeLocation.OldTelephone.ToString();
                    NewTelTextBox.Text = changeLocation.NewTelephone.ToString();
                }



                TargetComboBox.SelectedValue = changeLocation.TargetCenter;

                if (_RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside)
                {
                    TargetComboBox.Visibility = Visibility.Collapsed;
                    TargetLabel.Visibility = Visibility.Collapsed;
                }

                NearestTelephonTextBox.Text = changeLocation.NearestTelephon.ToString();

                if (changeLocation.Equipment != null)
                    EquipmentComboBox.SelectedValue = changeLocation.Equipment;

                InstallAddress = Data.AddressDB.GetAddressByID((long)changeLocation.NewInstallAddressID);
                InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                InstallAddressTextBox.Text = InstallAddress.AddressContent;

                CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)changeLocation.NewCorrespondenceAddressID);
                CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

                if (_RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter && request.CenterID == changeLocation.TargetCenter)
                {
                    SourceComboBox.SelectedValue = changeLocation.SourceCenter;
                    SourceComboBox.Visibility = Visibility.Visible;
                    SourceLabel.Visibility = Visibility.Visible;
                    SourceComboBox.IsEnabled = false;
                    SourceLabel.IsEnabled = false;
                }

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

                if ( Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Count != 0)
                {
                    InstallAddress = Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim())[0];

                    changeLocation.NewInstallAddressID = InstallAddress.ID;
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
                        InstallAddress =Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                        changeLocation.NewInstallAddressID= InstallAddress.ID;

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
                    changeLocation.NewInstallAddressID= InstallAddress.ID;

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
                    CorrespondenceAddress = Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim())[0];

                    changeLocation.NewCorrespondenceAddressID = CorrespondenceAddress.ID;
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
                        changeLocation.NewCorrespondenceAddressID = CorrespondenceAddress.ID;

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
                    changeLocation.NewCorrespondenceAddressID = CorrespondenceAddress.ID;

                    CorrespondencePostalCodeTextBox.Text = string.Empty;
                    CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    CorrespondenceAddressTextBox.Text = string.Empty;
                    CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }
            }
        }

        private void TargetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                    NewCustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }

                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.ShowDialog();
                    if (customerForm.DialogResult ?? false)
                    {
                        Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                        NewCustomerID = Customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = DB.GetEntitybyID<Customer>(customerForm.ID);
                    NewCustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }
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

        private void CustomerTelephoneInfo_Click(object sender, RoutedEventArgs e)
        {

            if (Customer != null)
            {
                CustomerTelephoneInfoForm window = new CustomerTelephoneInfoForm(Customer.ID);
                window.ShowDialog();
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

        #endregion

    }
}
