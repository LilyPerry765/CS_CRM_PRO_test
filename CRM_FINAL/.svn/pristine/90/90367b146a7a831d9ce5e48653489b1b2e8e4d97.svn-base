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
    public partial class CustomerSearchForm : Local.PopupWindow
    {
        #region Properties

        private long _ID;
        private string _NationalCode;
        private int _customerType = 0;

        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int CustomerType
        {
            get { return _customerType; }
            set { _customerType = value; }
        }
        private Customer _Customer { get; set; }

        #endregion

        #region Constructors

        public CustomerSearchForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CustomerSearchForm(long id)
            : this()
        {
            _ID = id;
        }

        public CustomerSearchForm(string nationalCode)
            : this()
        {
            _NationalCode = nationalCode;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            if (_ID == 0)
            {
                _Customer = new Customer();
                //if (_customerType != 0)
                //    _Customer.CustomerID = DB.GetCustomerID(_customerType);
            }
            else
            {
                //_Customer = DB.GetEntitybyID<Customer>(_id);
                _Customer = Data.CustomerDB.GetCustomerByID(_ID);
            }

            if (!string.IsNullOrWhiteSpace(_NationalCode))
            {
                ItemsDataGrid.ItemsSource = CustomerDB.GetCustomerListByNationalCode(_NationalCode);
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

        private void SearchButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerTypeListBox.SelectedValue != null)
                {
                    if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text.Trim()))
                        throw new Exception("لطفا نام / عنوان را وارد نمایید");

                    if (Convert.ToInt16(CustomerTypeListBox.SelectedValue) == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(FirstNameTextBox.Text) && (string.IsNullOrWhiteSpace(LastNameTextBox.Text)))
                            throw new Exception("لطفا نام خانوادگی را نیز وارد نمایید");

                        ItemsDataGrid.ItemsSource = CustomerDB.SearchCustomerInfo(Convert.ToInt16(CustomerTypeListBox.SelectedValue), NationalCodeTextBox.Text.Trim(), FirstNameTextBox.Text.Trim(), LastNameTextBox.Text.Trim(), FatherNameTextBox.Text.Trim(), BirthCertificateIDTextBox.Text.Trim(), IssuePlaceTextBox.Text.Trim(), (GenderListBox.SelectedValue != null) ? (int)GenderListBox.SelectedValue : -1);
                    }
                    else
                        ItemsDataGrid.ItemsSource = CustomerDB.SearchCustomerInfo(Convert.ToInt16(CustomerTypeListBox.SelectedValue), RecordNoTextBox.Text.Trim(), FirstNameTextBox.Text.Trim(), LastNameTextBox.Text.Trim(), FatherNameTextBox.Text.Trim(), BirthCertificateIDTextBox.Text.Trim(), IssuePlaceTextBox.Text.Trim(), (GenderListBox.SelectedValue != null) ? (int)GenderListBox.SelectedValue : -1);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در جستجو ، " + ex.Message + " !", ex);
            }
        }

        private void ShowCustomerForm(object sender, RoutedEventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();

            if (customerForm.DialogResult ?? false)
                ID = customerForm.ID;

            this.DialogResult = true;
        }

        private void ItemsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Customer item = ItemsDataGrid.SelectedItem as Data.Customer;
                if (item == null) return;

                ID = item.ID;
            }

            this.DialogResult = true;
        }

        #endregion
    }
}
