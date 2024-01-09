using CRM.Application.Views;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for SpecialWireUserControl.xaml
    /// </summary>
    public partial class SpecialWireUserControl : Local.UserControlBase
    {
        #region Properties and Fields

        private long _requestID = 0;
        public Customer Customer { get; set; }
        public Request _request { get; set; }
        public long oldTelephone { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public CRM.Data.SpecialWire _specialWire;
        private List<CheckableItem> telephoneList { get; set; }
        public ObservableCollection<SpecialWirePoints> _specialWirePoints;

        private int _centerID = 0;
        public int CenterID
        {
            set
            {
                _centerID = value;

                telephoneList = Data.TelephoneDB.GetPrivateWireCheckableItemTelephoneBy(_centerID);
                TelephoneNoComboBox.ItemsSource = telephoneList;
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
                CentersComboBoxColumn.ItemsSource = Data.CenterDB.GetCenterByCityId(_targetCityID);

            }
        }

        #endregion

        #region Constructor

        public SpecialWireUserControl()
        {
            InitializeComponent();

        }

        public SpecialWireUserControl(long requestID)
            : this()
        {
            this._requestID = requestID;
            Initialize();

        }

        #endregion

        #region Methods

        public void Initialize()
        {
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
            BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetSubBuchtTypeCheckable((int)DB.BuchtType.PrivateWire);
            SpecialWireTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SpecialWireType));
        }

        private void LoadData()
        {

            if (_IsLoaded)
                return;
            else
                _IsLoaded = true;


            _specialWirePoints = new ObservableCollection<SpecialWirePoints>(Data.SpecialWirePointsDB.GetSpecialWirePointsByRequestID(_requestID));

            oldTelephone = 0;
            if (_requestID == 0)
            {
                _request = new Request();
                _specialWire = new Data.SpecialWire();
            }
            else
            {

                _request = Data.RequestDB.GetRequestByID(_requestID);
                CentersComboBoxColumn.ItemsSource = Data.CenterDB.GetCenterByCityId(Data.CityDB.GetCityByCenterID(_request.CenterID).ID);
                _specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_requestID);

                if (_request.MainRequestID != null)
                {
                    this.IsEnabled = false;
                    _specialWirePoints = new ObservableCollection<SpecialWirePoints>(Data.SpecialWirePointsDB.GetSpecialWirePointsByRequestID((long)_request.MainRequestID));
                }

                oldTelephone = (long)_request.TelephoneNo;

                if (_request.TelephoneNo != 0 && _request.TelephoneNo != null)
                {
                    if (telephoneList == null) telephoneList = new List<CheckableItem>();
                    telephoneList.Add(new CheckableItem { LongID = _request.TelephoneNo, Name = _request.TelephoneNo.ToString(), IsChecked = false });
                }
                TelephoneTypeComboBox.SelectedValue = _specialWire.CustomerTypeID;
                TelephoneTypecomboBox_SelectionChanged(null, null);
                TelephoneTypeGroupComboBox.SelectedValue = _specialWire.CustomerGroupID;

                Customer = Data.CustomerDB.GetCustomerByID(_request.CustomerID ?? 0);
                NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
                CustomerNameTextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;

                Address CorrespondenceAddress = Data.AddressDB.GetAddressByID(_specialWire.CorrespondenceAddressID ?? 0);
                CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                SearchCorrespondenceAddress_Click(null, null);
            }

            PointsInfoDataGrid.ItemsSource = _specialWirePoints;
            this.DataContext = _specialWire;
        }

        #endregion

        #region EventHandlers   
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
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
                    Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
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

        private void PointsInfoDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if ((PointsInfoDataGrid.SelectedItem as SpecialWirePoints) != null && !string.IsNullOrEmpty((PointsInfoDataGrid.SelectedItem as SpecialWirePoints).PostalCode))
            {
                string postalCode = (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).PostalCode;
                int countAddress = Data.AddressDB.GetAddressByPostalCode(postalCode).Count;
                if (countAddress == 0)
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = postalCode;
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        if (customerAddressForm.ID != 0)
                        {
                            Address address = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = string.Empty;
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = address.AddressContent;
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).InstallAddressID = address.ID;
                        }
                        else
                        {
                            Folder.MessageBox.ShowInfo("کد پستی یافت نشد");
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = string.Empty;
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).InstallAddressID = null;
                        }
                    }
                    else
                    {
                        Folder.MessageBox.ShowInfo("کد پستی یافت نشد");
                        (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = string.Empty;
                        (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).InstallAddressID = null;
                    }
                }

                else if (countAddress == 1)
                {
                    Address address = Data.AddressDB.GetAddressByPostalCode(postalCode).Take(1).SingleOrDefault();
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = string.Empty;
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = address.AddressContent;
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).InstallAddressID = address.ID;
                }
                else if (countAddress >= 2)
                {
                    Folder.MessageBox.ShowInfo("با کد پستی چند آدرس یافت شد لطفا اطلاعات مشترک را اصلاح کنید");
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).AddressContent = string.Empty;
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).InstallAddressID = null;
                }

            }



        }

        private void ItemDelete(object sender, RoutedEventArgs e)
        {
            if ((PointsInfoDataGrid.SelectedItem as SpecialWirePoints) != null)
            {
                _specialWirePoints.Remove((PointsInfoDataGrid.SelectedItem as SpecialWirePoints));
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
                        _specialWire.CorrespondenceAddressID = CorrespondenceAddress.ID;
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
                            _specialWire.CorrespondenceAddressID = CorrespondenceAddress.ID;

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
                    _specialWire.CorrespondenceAddressID = CorrespondenceAddress.ID;

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

        //private void PointsInfoDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //{
        //}

        #endregion
    }
}
