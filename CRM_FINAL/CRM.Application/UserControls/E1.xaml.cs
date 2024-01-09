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
    /// Interaction logic for E1.xaml
    /// </summary>
    public partial class E1 : Local.UserControlBase
    {
        private long _requestID = 0;
        public Customer Customer { get; set; }
        public Address InstallAddress { get; set; }

        public Address TargetInstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public Request _request { get; set; }
        private int _centerID = 0;
        public CRM.Data.E1 _E1;
        private DB.RequestType _requestType;
        private long _telephoneNo = 0;

        public int CenterID
        {
            set { _centerID = value; }
        }


        public E1()
        {
            InitializeComponent();
            Initialize();
        }

        public E1(long requestID, DB.RequestType requestType, long telephone = 0)
            : this()
        {
            this._requestID = requestID;
            this._requestType = requestType;
            this._telephoneNo = telephone;

        }

        private void Initialize()
        {
            LinkTypeComboBox.ItemsSource = Data.E1LinkTypeDB.GetE1LinkTypeCheckable();
            CodeTypeComboBox.ItemsSource = Data.E1CodeTypeDB.GetE1CodeTypeCheckable();
            ChanalTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.E1ChanalType));
            LineTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.E1Type)).Where(t => t.ID == (int)DB.E1Type.Fiber || t.ID == (int)DB.E1Type.Wire).ToList();
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            if (_requestID == 0)
            {
                if (_requestType == DB.RequestType.E1Link)
                {
                    _request = Data.RequestDB.LastInstalledRequestByTelephone(this._telephoneNo);
                    _E1 = Data.E1DB.GetE1ByRequestID(_request.ID);

                    CustomeGroupBox.IsEnabled = false;
                    LineInfo.IsEnabled = false;
                    AddressInfoGroupBox.IsEnabled = false;

                    TelephoneTypeComboBox.SelectedValue = _E1.TelephoneType;
                    TelephoneTypecomboBox_SelectionChanged(null, null);
                    TelephoneTypeGroupComboBox.SelectedValue = _E1.TelephoneTypeGroup;

                    Customer = Data.CustomerDB.GetCustomerByID(_request.CustomerID ?? 0);
                    NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;


                    InstallAddress = Data.AddressDB.GetAddressByID((long)_E1.InstallAddressID);
                    InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                    InstallAddressTextBox.Text = InstallAddress.AddressContent;

                    if (_E1.TargetInstallAddressID != null)
                    {
                        TargetInstallAddress = Data.AddressDB.GetAddressByID((long)_E1.TargetInstallAddressID);
                        TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                        TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                    }

                    CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)_E1.CorrespondenceAddressID);
                    CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

                    LinkNumberGroupBox.Visibility = Visibility.Visible;
                }
                else if (_requestType == DB.RequestType.E1)
                {
                    _E1 = new Data.E1();
                    _request = new Request();
                    Customer = new Data.Customer();
                    ExtendCheckBox.Visibility = Visibility.Visible;
                }
                else
                {
                    _E1 = new Data.E1();
                    _E1.LineType = (byte)DB.E1Type.Wire;
                    _request = new Request();
                }
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

                if (_E1.TargetInstallAddressID != null)
                {
                    TargetInstallAddress = Data.AddressDB.GetAddressByID((long)_E1.TargetInstallAddressID);
                    TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                    TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                }

                CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)_E1.CorrespondenceAddressID);
                CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

                if (_requestType == DB.RequestType.E1Link)
                {
                    LinkNumberGroupBox.Visibility = Visibility.Visible;
                    CustomeGroupBox.IsEnabled = false;
                    LineInfo.IsEnabled = false;
                    AddressInfoGroupBox.IsEnabled = false;
                    LinkNumberTextBox.Text = _E1.NumberOfLine.ToString();
                }
                else if (_requestType == DB.RequestType.E1)
                {
                    ExtendCheckBox.Visibility = Visibility.Visible;
                    if (Customer != null)
                    {
                        CustomerNameTextBox.Text = string.Format("{0} {1}", Customer.FirstNameOrTitle, Customer.LastName);
                        NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;

                        //آیا درخواست جاری ، توسعه یافته ی یک درخواست قبلی بوده است یا خیر
                        Status status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);

                        //اگر درخواست جاری ، توسعه یافته درخواست دیگری باشد و در مرحله آغازین باشد، آنگاه کاربر میتوانددرخواست اصلی را در صورت نیاز تغییر دهد
                        if (_request.MainRequestID.HasValue)// && status.StatusType == (byte)DB.RequestStatusType.Start)
                        {
                            ExtendCheckBox.IsChecked = true;
                            ExtendCheckBox.IsEnabled = false;

                            //لود کردن درخواست های ای وان مشترک در صورت وجود
                            List<E1Info> e1Requests = E1DB.GetE1RequestsByCustomerID(Customer.ID);
                            if (e1Requests.Count > 0)//اگر درخواست ای وان داشت ، در آنصورت باید لیست آنها را نمایش دهیم
                            {
                                PreviousE1RequestsDataGrid.Visibility = Visibility.Visible;
                                PreviousE1RequestsDataGrid.ItemsSource = e1Requests;
                                if (_request.MainRequestID.HasValue)//اگر کاربر قبلاً چک باکس توسعه زده باشد و درخواست را ذخیره کرده باشد،مسلماً شناسه درخواست اصلی مقدار دارد
                                {
                                    //در این حالت بعد از لود فرم باید درخواست مربوط به شناسه اصلی در وضعیت انتخاب شده ، باشد 
                                    foreach (var item in PreviousE1RequestsDataGrid.Items)
                                    {
                                        E1Info currentItem = item as E1Info;
                                        if (
                                             (currentItem != null)
                                             &&
                                             (currentItem.RequestID == _request.MainRequestID.Value)
                                           )
                                        {
                                            PreviousE1RequestsDataGrid.SelectedItem = currentItem;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
            ExtendCheckBox.IsEnabled = false;

            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                //if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
                //{ MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }


                if (Data.BlackListDB.ExistNationalCodeInBlackList(NationalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد ملی در لیست سیاه قرار دارد");
                }
                else
                {
                    Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                    if (Customer != null)
                    {
                        _request.CustomerID = Customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                        ExtendCheckBox.IsEnabled = true;
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
                            ExtendCheckBox.IsEnabled = true;
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
                    ExtendCheckBox.IsEnabled = true;
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

        private void TargetInstallAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetInstallAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(TargetInstallAddress.ID);
                window.ShowDialog();
                SearchInstallAddress_Click(null, null);
            }
        }

        private void TargetSearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TargetInstallPostalCodeTextBox.Text.Trim()))
            {
                TargetInstallAddressTextBox.Text = string.Empty;

                if (BlackListDB.ExistPostallCodeInBlackList(TargetInstallPostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {
                    if (Data.AddressDB.GetAddressByPostalCode(TargetInstallPostalCodeTextBox.Text.Trim()).Count != 0)
                    {
                        TargetInstallAddress = Data.AddressDB.GetAddressByPostalCode(TargetInstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                        _E1.TargetInstallAddressID = TargetInstallAddress.ID;
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
                            _E1.TargetInstallAddressID = TargetInstallAddress.ID;

                            TargetInstallPostalCodeTextBox.Text = string.Empty;
                            TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                            TargetInstallAddressTextBox.Text = string.Empty;
                            TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                        }
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
                    _E1.TargetInstallAddressID = TargetInstallAddress.ID;

                    TargetInstallPostalCodeTextBox.Text = string.Empty;
                    TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                    TargetInstallAddressTextBox.Text = string.Empty;
                    TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                }
            }
        }

        private void ExtendCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PreviousE1RequestsDataGrid.Visibility = Visibility.Collapsed;
        }

        private void ExtendCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CRM.Data.Customer customer = CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                if (customer != null)
                {
                    //لود کردن درخواست های ای وان مشترک در صورت وجود
                    List<E1Info> e1Requests = E1DB.GetE1RequestsByCustomerID(customer.ID);
                    if (e1Requests.Count > 0)//اگر درخواست ایوان داشت ، در آنصورت باید لیست آنها را نمایش دهیم
                    {
                        PreviousE1RequestsDataGrid.Visibility = Visibility.Visible;
                        PreviousE1RequestsDataGrid.ItemsSource = e1Requests;
                    }
                    else
                    {
                        MessageBox.Show(".این مشترک تاکنون درخواست ایوان نداشته است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                        ExtendCheckBox.IsChecked = false;
                    }
                }
                else
                {
                    //زمانی که بعد از جستجو مشترکی پیدا نشود پس توسعه برای این مشترک بی معنی است
                    MessageBox.Show(".برای کد ملی وارد شده،مشترکی یافت نشد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                    ExtendCheckBox.IsChecked = false;
                }
            }
            else
            {
                MessageBox.Show(".بمنظور توسعه باید ابتدا کد ملی مشترک را مشخص نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                ExtendCheckBox.IsChecked = false;
            }
        }


    }
}
