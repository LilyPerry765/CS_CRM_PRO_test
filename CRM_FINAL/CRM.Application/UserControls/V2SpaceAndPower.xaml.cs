using CRM.Application.Local;
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
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for V2SpaceAndPower.xaml
    /// </summary>
    public partial class V2SpaceAndPower : UserControlBase
    {
        #region Properties And Fields

        private long _RequestID = 0;

        public Customer _Customer { get; set; }

        public Request _Request { get; set; }

        public SpaceAndPower _SpaceAndPower { get; set; }

        public Address _Address { get; set; }

        public Antenna _Antenna { get; set; }


        private int _centerID;
        public int CenterID
        {
            set { _centerID = value; }
        }

        #endregion

        #region Constructor

        public V2SpaceAndPower()
        {
            InitializeComponent();
            Initialize();
        }

        public V2SpaceAndPower(long requestId)
            : this()
        {
            this._RequestID = requestId;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            EquipmentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.EquipmentType));
            SpaceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SpaceType));
            PowerTypesComboBox.ItemsSource = PowerTypeDB.GetCheckablePowerTypes();
            this.CenterID = 0;
        }

        public void LoadData()
        {
            if (this._RequestID == 0)
            {
                this._Request = new Request();
                this._SpaceAndPower = new SpaceAndPower();
                this._Customer = new Customer();
                this._Antenna = new Antenna();
            }
            else
            {
                _Request = RequestDB.GetRequestByID(_RequestID);
                _SpaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_RequestID);
                _Customer = CustomerDB.GetCustomerByID(_Request.CustomerID ?? 0);
                _Antenna = SpaceAndPowerDB.GetAntennaBySpaceAndPowerId(_SpaceAndPower.ID);
                _Address = (_SpaceAndPower.AddressID.HasValue) ? AddressDB.GetAddressByID(_SpaceAndPower.AddressID.Value) : new Address();

                //بازیابی رکورد های نوع پاور مصرفی برای تیک زدن آیتم مربوطه در کمبوباکس
                List<PowerType> currentPowerTypes = new List<PowerType>();
                currentPowerTypes = PowerTypeDB.GetPowerTypesBySpaceAndPowerID(_SpaceAndPower.ID);

                List<int> currentPowerTypesID = new List<int>();
                currentPowerTypesID = currentPowerTypes.Select(pt => pt.ID).ToList();
                foreach (CheckableItem item in PowerTypesComboBox.ItemsSource.Cast<CheckableItem>().Where(ci => currentPowerTypesID.Contains(ci.ID)).ToList())
                {
                    item.IsChecked = true;
                }

                PostalCodeTextBox.Text = _Address.PostalCode;
                AddressTextBox.Text = _Address.AddressContent;
                if (_Customer != null)
                {
                    CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                    NationalCodeTextBox.Text = _Customer.NationalCodeOrRecordNo;

                    Status status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);

                    //اگر درخواست جاری ، توسعه یافته درخواست دیگری باشد و در مرحله آغازین باشد، آنگاه کاربر میتوانددرخواست اصلی را در صورت نیاز تغییر دهد
                    if (_Request.MainRequestID.HasValue)// && status.StatusType == (byte)DB.RequestStatusType.Start)
                    {
                        ExtendCheckBox.IsChecked = true;
                        ExtendCheckBox.IsEnabled = false;
                        PreviousSpaceAndPowerRequestsDataGrid.IsEnabled = false;
                        //لود کردن درخواست های فضا و پاور مشترک در صورت وجود
                        List<SpaceAndPowerInfo> spaceAndPowerRequests = SpaceAndPowerDB.GetSpaceAndPowerRequestsByCustomerID(_Customer.ID);
                        if (spaceAndPowerRequests.Count > 0)//اگر درخواست فضا و پاوری داشت ، در آنصورت باید لیست آنها را نمایش دهیم
                        {
                            PreviousSpaceAndPowerRequestsDataGrid.Visibility = Visibility.Visible;
                            PreviousSpaceAndPowerRequestsDataGrid.ItemsSource = spaceAndPowerRequests;
                            if (_Request.MainRequestID.HasValue)//اگر کاربر قبلاً چک باکس توسعه زده باشد و درخواست را ذخیره کرده باشد،مسلماً شناسه درخواست اصلی مقدار دارد
                            {
                                //در این حالت بعد از لود فرم باید درخواست مربوط به شناسه اصلی در وضعیت انتخاب شده ، باشد 
                                foreach (var item in PreviousSpaceAndPowerRequestsDataGrid.Items)
                                {
                                    SpaceAndPowerInfo currentItem = item as SpaceAndPowerInfo;
                                    if (
                                         (currentItem != null)
                                         &&
                                         (currentItem.RequestID == _Request.MainRequestID.Value)
                                       )
                                    {
                                        PreviousSpaceAndPowerRequestsDataGrid.SelectedItem = currentItem;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            SpaceAndPowerGroupBox.DataContext = _SpaceAndPower;
            AntennaInfoGrid.DataContext = (_Antenna != null) ? _Antenna : new Antenna();
        }

        #endregion

        #region EventHandlers

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();


            //چنانچه درخواست فضا و پاور در مرحله ثبت درخواست مانده باشد و کاربر قصد ویرایش درخواست را داشته باشد
            //وبخواهد آنتن را ویرایش کند باید متد لود فرخوانی میشد
            //چون آیتم زیر نال بود
            //(AntennaInfoGrid.DataContext as Antenna)
            //بنابراین در بلاک زیر موجود بودن آنتن را بررسی کردیم . چون این بلاک بعد از لودد اجرا میشود
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if ((this._Antenna != null) && (this._Antenna.ID != 0))
                {
                    AntennaInfoGrid.Visibility = Visibility.Visible;
                    AntennaCheckBox.IsChecked = true;
                }
            }
                                                    ),
                                         System.Windows.Threading.DispatcherPriority.ContextIdle,
                                         null
                                       );
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            ExtendCheckBox.IsEnabled = false;
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                string nationalCode = NationalCodeTextBox.Text.Trim();
                CustomerNameTextBox.Clear();
                bool isNationalCodeInBlackList = BlackListDB.ExistNationalCodeInBlackList(nationalCode);
                if (isNationalCodeInBlackList)
                {
                    Folder.MessageBox.ShowWarning(".کد ملی در لیست سیاه قرار دارد");
                }
                else
                {
                    _Customer = CustomerDB.GetCustomerByNationalCode(nationalCode);
                    if (_Customer != null)
                    {
                        _Request.CustomerID = _Customer.ID;
                        CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                        ExtendCheckBox.IsEnabled = true;
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.CenterID = _centerID;
                        customerForm.ShowDialog();
                        if (customerForm.DialogResult ?? false)
                        {
                            _Customer = CustomerDB.GetCustomerByID(customerForm.ID);
                            _Request.CustomerID = _Customer.ID;
                            CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                            NationalCodeTextBox.Text = _Customer.NationalCodeOrRecordNo;
                            ExtendCheckBox.IsEnabled = true;
                        }
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.CenterID = _centerID;
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    _Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    _Request.CustomerID = _Customer.ID;
                    CustomerNameTextBox.Clear();
                    CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                    NationalCodeTextBox.Text = _Customer.NationalCodeOrRecordNo;
                    ExtendCheckBox.IsEnabled = true;
                }
            }
            if (_Customer != null)
            {
                (Window.GetWindow(this) as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
            }
        }

        private void EditSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_Request != null && _Request.CustomerID.HasValue)
            {
                CustomerForm window = new CustomerForm(_Request.CustomerID.Value);
                window.ShowDialog();
            }
        }

        private void AntennaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AntennaInfoGrid.Visibility = Visibility.Visible;
        }

        private void AntennaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AntennaInfoGrid.Visibility = Visibility.Collapsed;
        }

        private void HasFibreCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _SpaceAndPower.HasFibre = false;
        }

        private void HasFibreCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _SpaceAndPower.HasFibre = true;
        }

        private void ExtendCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                Customer customer = CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                if (customer != null)
                {
                    //لود کردن درخواست های فضا و پاور مشترک در صورت وجود
                    List<SpaceAndPowerInfo> spaceAndPowerRequests = SpaceAndPowerDB.GetSpaceAndPowerRequestsByCustomerID(customer.ID);
                    if (spaceAndPowerRequests.Count > 0)//اگر درخواست فضا و پاوری داشت ، در آنصورت باید لیست آنها را نمایش دهیم
                    {
                        PreviousSpaceAndPowerRequestsDataGrid.Visibility = Visibility.Visible;
                        PreviousSpaceAndPowerRequestsDataGrid.ItemsSource = spaceAndPowerRequests;
                    }
                    else
                    {
                        MessageBox.Show(".این مشترک تاکنون درخواست فضا و پاور نداشته است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void ExtendCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PreviousSpaceAndPowerRequestsDataGrid.Visibility = Visibility.Collapsed;
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
                        _SpaceAndPower.AddressID = _Address.ID;
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
                            _SpaceAndPower.AddressID = _Address.ID;

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
                    _SpaceAndPower.AddressID = _Address.ID;

                    PostalCodeTextBox.Clear();
                    AddressTextBox.Clear();

                    PostalCodeTextBox.Text = _Address.PostalCode;
                    AddressTextBox.Text = _Address.AddressContent;
                }
            }
        }

        private void EditAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (_Address != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(_Address.ID);
                window.ShowDialog();
                SearchAddress_Click(null, null);
            }
        }

        private void PreviousSpaceAndPowerRequestsDataGrid_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            SpaceAndPowerInfo selectedItem = PreviousSpaceAndPowerRequestsDataGrid.SelectedItem as SpaceAndPowerInfo;
            if (selectedItem != null)
            {
                DataGrid detail = e.DetailsElement.FindName("PowerTypesDataGrid") as DataGrid;
                detail.ItemsSource = PowerTypeDB.GetPowerTypeInfosBySpaceAndPowerID(selectedItem.RequestID);
            }
        }

        private void PreviousSpaceAndPowerRequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AntennaCheckBox.IsChecked = false;
            SpaceAndPowerInfo selectedItem = PreviousSpaceAndPowerRequestsDataGrid.SelectedItem as SpaceAndPowerInfo;
            if (selectedItem != null)
            {
                //بدست آوردن جزئیات فضا و پاور درخواست در حال توسعه
                SpaceAndPowerEquivalentDatabaseTableInfo extendingSpaceAndPowerRequest = SpaceAndPowerDB.GetSpaceAndPowerEquivalentDatabaseTableInfoByID(selectedItem.ID);

                //خط زیر در زمان ذخیره درخواست ایجاد خطا میکند. چون باید دیتاکانتکس از نوع فضاپاور دیتابیس باشد
                //SpaceAndPowerGroupBox.DataContext = extendingSpaceAndPowerRequest;
                SpaceAndPower extendingSpaceAndPower = new SpaceAndPower();
                extendingSpaceAndPower.EquipmentType = extendingSpaceAndPowerRequest.EquipmentType;
                extendingSpaceAndPower.EquipmentWeight = extendingSpaceAndPowerRequest.EquipmentWeight;
                extendingSpaceAndPower.SpaceSize = extendingSpaceAndPowerRequest.SpaceSize;
                extendingSpaceAndPower.SpaceType = extendingSpaceAndPowerRequest.SpaceType;
                extendingSpaceAndPower.SpaceUsage = extendingSpaceAndPowerRequest.SpaceUsage;
                extendingSpaceAndPower.HeatWasteRate = extendingSpaceAndPowerRequest.HeatWasteRate;
                extendingSpaceAndPower.Duration = extendingSpaceAndPowerRequest.Duration;
                extendingSpaceAndPower.HasFibre = extendingSpaceAndPowerRequest.HasAntenna;
                extendingSpaceAndPower.RigSpace = extendingSpaceAndPowerRequest.RigSpace;
                SpaceAndPowerGroupBox.DataContext = extendingSpaceAndPower;

                //مقداردهی بخش آنتن
                AntennaInfoGrid.DataContext = extendingSpaceAndPowerRequest.Antenna;
                if (extendingSpaceAndPowerRequest.HasAntenna)
                {
                    AntennaCheckBox.IsChecked = true;
                }

                //بازیابی رکورد های نوع پاور مصرفی برای تیک زدن آیتم مربوطه در کمبوباکس
                List<PowerType> extendedSpaceAndPower_PowerTypes = new List<PowerType>();
                extendedSpaceAndPower_PowerTypes = PowerTypeDB.GetPowerTypesBySpaceAndPowerID(selectedItem.ID);
                //currentPowerTypes = extendingSpaceAndPowerRequest.PowerTypes;

                //چنانچه کاربر قبل از توسعه دادن درخواست های قبلی مشترک جاری ، پاورهایی را انتخاب کرده باشد و سپس تصمیم به توسعه بگیرد 
                //میبایست پاورهای انتخاب شده از حالت انتخاب دربیایند
                //CheckableItem.IsChecked = false;
                //PowerTypesComboBox.Items.Cast<CheckableItem>().ToList().ForEach(new Action<CheckableItem>(checkableItem => checkableItem.IsChecked = false));
                foreach (CheckableItem item in PowerTypesComboBox.Items
                                                                 .Cast<CheckableItem>()
                                                                 .Where(ci => ci.IsChecked.Value)
                                                                 .ToList()
                        )
                {
                    item.IsChecked = false;
                }
                PowerTypesComboBox.SelectedIndex = -1;

                List<int> extendedSpaceAndPower_PowerTypesID = new List<int>();
                extendedSpaceAndPower_PowerTypesID = extendedSpaceAndPower_PowerTypes.Select(pt => pt.ID).ToList();

                foreach (CheckableItem item in PowerTypesComboBox.ItemsSource
                                                                 .Cast<CheckableItem>()
                                                                 .Where(ci => extendedSpaceAndPower_PowerTypesID.Contains(ci.ID))
                                                                 .ToList()
                        )
                {
                    item.IsChecked = true;
                }
                PowerTypesComboBox.Items.Refresh();
            }
        }

        #endregion

    }
}
