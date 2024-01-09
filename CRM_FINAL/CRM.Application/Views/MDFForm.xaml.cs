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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class MDFForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public MDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        public MDFForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            TypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.MDFType));
            UsesComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.MDFUses));
            
        }

        private void LoadData()
        {
            MDF item = new MDF();


            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.MDFDB.GetMDFByID(_ID);
                CityID = Data.MDFDB.GetCity(item.ID);
               
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
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
                MDF item = this.DataContext as MDF;

                //if (item.Type == (byte)Data.DB.MDFType.ONU && item.Uses != (byte)Data.DB.MDFUses.Normal)
                //{
                //    throw new Exception("امکان نصب اونوها فقط در نوع معمولی میباشد");
                //}

                item.Detach();
                Save(item);

                ShowSuccessMessage("MDF با موفقیت ذخیره شد.");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("ایتم وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره MDF", ex);
            }
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as MDF).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //if (CenterComboBox.SelectedValue != null)
            //{
            //    BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckableByCenterID((int)CenterComboBox.SelectedValue);
            //}
        }
    }
}
