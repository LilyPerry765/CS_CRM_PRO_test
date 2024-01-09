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
using Microsoft.Win32;
using System.ComponentModel;

namespace CRM.Application.Views
{
    public partial class CityForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public CityForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CityForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ProvinceComboBox.ItemsSource = Data.ProvinceDB.GetProvincesCheckable();
        }
        
        private void LoadData()
        {
            City city = new City();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                city = Data.CityDB.GetCityById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = city;
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
                City city = this.DataContext as City;
                city.Detach();
                Save(city);

                ShowSuccessMessage("شهر ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره شهر", ex);
            }
        }

        #endregion
    }
}
