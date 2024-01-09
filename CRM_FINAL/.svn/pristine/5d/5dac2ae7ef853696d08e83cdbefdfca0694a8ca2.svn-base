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

namespace CRM.Application.Views
{
    public partial class CustomerForm : Local.PopupWindow
    {
        #region Properties

        private long _id;
        private int _centerID = 0;
        private int _customerType = 0;
        string city = string.Empty;
        public Address _Address { get; set; }

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public int CenterID
        {
            get { return _centerID; }
            set { _centerID = value; }
        }

        public int CustomerType
        {
            get { return _customerType; }
            set { _customerType = value; }
        }
        private Customer _Customer { get; set; }

        #endregion

        #region Constructors

        public CustomerForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CustomerForm(long id)
            : this()
        {
            _id = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CompanyTypesComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.CompanyType));
        }

        private void LoadData()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();

            if (_id == 0)
            {
                _Customer = new Customer();
                Title = "ثبت مشخصات مشترکین";
                if (_centerID != 0 && _customerType != 0)
                    _Customer.CustomerID = DB.GetCustomerID(_centerID, _customerType);

                if (city == "tehran")
                {
                    PersonCreateCustomerIDButton.IsEnabled = true;
                    CompanyCreateCustomerIDButton.IsEnabled = true;
                }
            }
            else
            {
                Title = "بروز رسانی مشخصات مشترکین";
                _Customer = Data.CustomerDB.GetCustomerByID(_id);

                if (_Customer.PersonType == (int)DB.PersonType.Company) //اگر نوع مشترک حقوقی باشد ، آنگاه باید آدرسش لود شود
                {
                    if (_Customer.AddressID.HasValue)
                    {
                        _Address = AddressDB.GetAddressByID(_Customer.AddressID.Value);
                        AddressInfoGrid.DataContext = _Address;
                    }
                }
            }

            this.DataContext = _Customer;
        }

        private static void RefreshForm(Customer customer)
        {
            customer.BirthDateOrRecordDate = null;
            customer.BirthCertificateID = "";
            customer.IssuePlace = "";
            customer.FirstNameOrTitle = "";
            customer.LastName = "";
            customer.FatherName = "";
            customer.NationalCodeOrRecordNo = "";
            //customer.PersonType = true;
            customer.Email = "";
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                _Customer = this.DataContext as Customer;

                if (string.IsNullOrEmpty(_Customer.LastName))
                    _Customer.LastName = "";

                if (city == "tehran")
                {
                    if (_Customer.CustomerID == null || _Customer.CustomerID == string.Empty)
                    {
                        throw new Exception("لطفا شناسه مشترک را ایجاد کنید");
                    }
                }

                if (_Customer.PersonType == (int)DB.PersonType.Person)
                {
                    if (string.IsNullOrEmpty(_Customer.NationalCodeOrRecordNo))
                    {
                        throw new Exception("لطفاً کد ملی/شماره ثبت را وارد کنید");
                    }
                    else if (_Customer.NationalCodeOrRecordNo.Length != 10)
                    {
                        throw new Exception("کدملی/شماره ثبت باید 10 رقمی باشد");
                    }
                }

                if (_Customer.PersonType == (int)DB.PersonType.Person && (string.IsNullOrEmpty(_Customer.FirstNameOrTitle) || string.IsNullOrEmpty(_Customer.LastName)))
                {
                    throw new Exception("لطفا نام و نام خانوادگی را وارد کنید");
                }

                if (_Customer.Gender == null && _Customer.PersonType == (byte)DB.PersonType.Person) //بر اساس داکیومنت شاهکار جنسیت مشترک حقیقی باید مشخص باشد
                {
                    MessageBox.Show(".تعیین جنسیت برای مشترک حقیقی الزامی است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    GendersListBox.Focus();
                    return;
                }

                if (_Customer.PersonType == (int)DB.PersonType.Company && string.IsNullOrEmpty(_Customer.FirstNameOrTitle))
                {
                    throw new Exception("لطفا نام را وارد کنید");
                }

                if (_id == 0)
                {
                    if (NationalCodeTextBox.Text.Trim() != string.Empty)
                    {
                        List<Customer> customerList = CustomerDB.GetCustomerListByNationalCode(NationalCodeTextBox.Text);
                        if (customerList != null)
                            throw new Exception("کد ملی/شماره ثبت وارد شده تکراری است ");

                        _Customer.Detach();
                        _Customer.ChangeDate = DB.GetServerDate();
                        DB.Save(_Customer, true);
                    }
                }
                else
                {
                    _Customer.Detach();
                    _Customer.ChangeDate = DB.GetServerDate();
                    DB.Save(_Customer, false);
                }

                this.DialogResult = true;
                _id = _Customer.ID;

                RefreshForm(_Customer);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات، " + ex.Message + " !", ex);
            }
        }

        private void PersonCreateCustomerIDButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
                {
                    List<Customer> customerList = CustomerDB.GetCustomerListByNationalCode(NationalCodeTextBox.Text);
                    if (customerList != null)
                        throw new Exception("کد ملی/شماره ثبت وارد شده تکراری است ");

                    //_Customer.Detach();
                    //DB.Save(_Customer, true);
                }

                //var city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
                //if (city == "tehran" || city == "kermanshah")
                //{
                CustomerIDGeneratorForm window = new CustomerIDGeneratorForm();
                window.ShowDialog();

                CenterID = window.CenterID;
                CustomerType = window.CustomerTypeID;
                if (_centerID != 0 && _customerType != 0)
                    _Customer.CustomerID = DB.GetCustomerID(_centerID, _customerType);
                //}
                //else if (city == "semnan")
                //{
                //    _Customer.CustomerID = DB.GetCustomerID(_centerID, _customerType);
                //}

            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        #endregion

        private void EditAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (_Customer != null && _Customer.AddressID.HasValue)
            {
                CustomerAddressForm window = new CustomerAddressForm(_Customer.AddressID.Value);
                window.ShowDialog();
                SearchAddress_Click(null, null);
            }
        }

        private void SearchAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PostalCodeTextBox.Text.Trim()))
            {
                AddressTextBox.Text = string.Empty;
                if (BlackListDB.ExistPostallCodeInBlackList(PostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {
                    if (AddressDB.GetAddressByPostalCode(PostalCodeTextBox.Text.Trim()).Count != 0)
                    {
                        _Address = AddressDB.GetAddressByPostalCode(PostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();
                        _Customer.AddressID = _Address.ID;
                        AddressTextBox.Text = string.Empty;
                        AddressTextBox.Text = _Address.AddressContent;
                    }
                    else
                    {
                        CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                        customerAddressForm.PostallCode = PostalCodeTextBox.Text.Trim();
                        customerAddressForm.ShowDialog();
                        if (customerAddressForm.DialogResult ?? false)
                        {
                            _Address = AddressDB.GetAddressByID(customerAddressForm.ID);
                            _Customer.AddressID = _Address.ID;

                            PostalCodeTextBox.Clear();
                            AddressTextBox.Clear();

                            PostalCodeTextBox.Text = _Address.PostalCode;
                            AddressTextBox.Text = _Address.AddressContent;
                        }
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = PostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    _Address = AddressDB.GetAddressByID(customerAddressForm.ID);
                    _Customer.AddressID = _Address.ID;

                    PostalCodeTextBox.Clear();
                    AddressTextBox.Clear();

                    PostalCodeTextBox.Text = _Address.PostalCode;
                    AddressTextBox.Text = _Address.AddressContent;
                }
            }
        }

        private void PersonTypeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PersonTypeListBox.SelectedItem != null)
            {
                ListBoxItem selectedItem = PersonTypeListBox.SelectedItem as ListBoxItem;
                if (selectedItem != null)
                {
                    int tag = Convert.ToInt16(selectedItem.Tag);
                    switch (tag)
                    {
                        case 0: //PersonRadioButton
                            {
                                if (CompanyGrid != null)
                                {
                                    PostalCodeTextBox.Text = string.Empty;
                                    AddressTextBox.Text = string.Empty;
                                    if (_Customer != null) _Customer.AddressID = null;
                                }
                            }
                            break;
                        case 1: //CompanyRadioButton
                            {

                            }
                            break;
                    }
                }
            }
        }
    }
}
