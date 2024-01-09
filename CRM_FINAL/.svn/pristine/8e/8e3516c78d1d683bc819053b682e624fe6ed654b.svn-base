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
    public partial class E1BayForm : Local.PopupWindow
    {
        private int _ID = 0;
        private int CityID = 0;

        public E1BayForm()
        {
            InitializeComponent();
            Initialize();
        }

        public E1BayForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
            E1Bay item = new E1Bay();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.E1BayDB.GetE1BayByID(_ID);
                CityID = Data.E1BayDB.GetCity(item.ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
            {
                CityComboBox.SelectedValue = CityID;
                CityComboBox_SelectionChanged(null, null);
                if (item != null)
                {
                    CenterComboBox.SelectedValue = Data.E1DDFDB.GetE1DDFByID(item.DDFID).CenterID;
                    CenterComboBox_SelectionChanged(null, null);
                }
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                E1Bay item = this.DataContext as E1Bay;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("ردیف ذخیره  شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("نمی توان دو ردیف هم شماره در یک دی دی اف وارد کرد .خطا در ذخیره ردیف", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره ردیف", ex);
                }
            }

           }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                DDFComboBox.ItemsSource = Data.E1DDFDB.GetDDFCheckableByCenterIDs(new List<int> {(int)CenterComboBox.SelectedValue });
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
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
    }
}
