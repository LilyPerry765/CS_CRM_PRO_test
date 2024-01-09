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
    public partial class E1PositionForm : Local.PopupWindow
    {
        private int _ID = 0;
        private int CityID = 0;

        public E1PositionForm()
        {
            InitializeComponent();
            Initialize();
        }

        public E1PositionForm(int id)
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
            E1Position item = new E1Position();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.E1PositionDB.GetE1PositionByID(_ID);
                CityID = Data.E1PositionDB.GetCityByPositionID(item.ID);
                E1Bay e1Bay = Data.E1BayDB.GetE1BayByID(item.BayID);
                DDFComboBox.SelectedValue = e1Bay.DDFID;

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
                    CenterComboBox.SelectedValue = Data.E1PositionDB.GetCenterByPositionID(item.ID).ID;
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
                E1Position item = this.DataContext as E1Position;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage(" تیغه ذخیره  شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("نمی توان دو تیغه هم شماره در یک ردیف وارد کرد .خطا در ذخیره تیغه", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره  تیغه", ex);
                }
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                DDFComboBox.ItemsSource = Data.E1DDFDB.GetDDFCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
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

        private void DDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DDFComboBox.SelectedValue != null)
            {
                BayComboBox.ItemsSource = Data.E1BayDB.GetBayCheckableByDDFID((int)DDFComboBox.SelectedValue);
            }
        }
    }
}
