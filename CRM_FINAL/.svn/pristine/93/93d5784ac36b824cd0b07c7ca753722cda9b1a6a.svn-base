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
    public partial class PCMShelfForm : Local.PopupWindow
    {
        private int _ID = 0;
        private int CityID = 0;

        public PCMShelfForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PCMShelfForm(int id)
            : this()
        {
            _ID = id;
            Initialize();
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
            PCMShelf item = new PCMShelf();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.PCMShelfDB.GetPCMShelfByID(_ID);
                CityID = Data.PCMShelfDB.GetCity(item.ID);

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
                    CenterComboBox.SelectedValue = Data.PCMRockDB.GetPCMRockByID(item.PCMRockID).CenterID;
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
                if (PCMRockComboBox.SelectedValue == null || (Convert.ToInt32(PCMRockComboBox.SelectedValue) == 0))
                {
                    MessageBox.Show(".رک را مشخص نمایید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    PCMShelf item = this.DataContext as PCMShelf;

                    item.Detach();
                    DB.Save(item);

                    ShowSuccessMessage("شلف ذخیره شد");
                    this.DialogResult = true;
                }
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                string errorMessage = SqlExceptionHelper.ErrorMessage(se);
                ShowErrorMessage(errorMessage, se);
            }
            catch (Exception ex)
            {
                //if (ex.Message.Contains("Cannot insert duplicate key in object"))
                //{

                //}
                //else
                //{
                //ShowErrorMessage("خطا در ذخیره شلف", ex); 
                //}
                ShowErrorMessage("خطا در ذخیره شلف", ex);
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                PCMRockComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
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
