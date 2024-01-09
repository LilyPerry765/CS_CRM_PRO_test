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
    public partial class CustomerAddressForm : Local.PopupWindow
    {
        #region Properties

        public long ID = 0;
        private int CityID = 0;

        public string PostallCode { get; set; }

        #endregion

        #region Constructors

        public CustomerAddressForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CustomerAddressForm(long id)
            : this()
        {
            ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
            Address address = new Address();

            if (ID == 0)
            {
                SaveButton.Content = "ذخیره";
                address.PostalCode = PostallCode;
            }
            else
            {
                address = Data.AddressDB.GetAddressByID(ID);
                CityID = Data.AddressDB.GetCity(address.ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = address;

            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
                CityComboBox.SelectedValue = CityID;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
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
                Address address = this.DataContext as Address;
                if (address.ID == 0 && Data.AddressDB.ExistAddressPostalCode(address.PostalCode))
                {
                    throw new Exception("کد پستی قبلا در سیستم ثبت شده است");
                }

                if (string.IsNullOrEmpty(address.PostalCode))
                {
                    throw new Exception("لطفا کد پستی را وارد کنید");
                }
                else if (!string.IsNullOrEmpty(address.PostalCode) && address.PostalCode.Length != 10)
                {
                    throw new Exception("لطفا کد پستی را بصورت عدد ده رقمی وارد کنید");
                }
                else if (!string.IsNullOrEmpty(address.PostalCode) && address.PostalCode.Length == 10 && !Helper.AllCharacterIsNumber(postalCodeTextbox.Text))
                {
                    throw new Exception("لطفا کد پستی را بصورت 'عدد' ده رقمی وارد کنید");
                }
                else if (address.ID != 0)
                {
                    address.Detach();
                    address.ChangeDate = DB.GetServerDate();
                    Save(address);

                    this.DialogResult = true;
                    ID = address.ID;

                }
                else
                {
                    address.Detach();
                    address.ChangeDate = DB.GetServerDate();
                    Save(address);

                    this.DialogResult = true;
                    ID = address.ID;
                }
                                

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره آدرس", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as Address).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                      City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        #endregion
    }
}
