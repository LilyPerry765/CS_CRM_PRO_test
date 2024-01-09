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
    public partial class BuchtTypeForm : Local.PopupWindow
    {
        private int _ID = 0;
      //  private int CityID = 0;

        public BuchtTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public BuchtTypeForm(int id)
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
           // CityComboBox.ItemsSource = Data.CityDB.GetAvailableCity();
            BuchtTypeNameParent.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable().Union(new List<CheckableItem> { new CheckableItem { ID = -1, Name = string.Empty, IsChecked = false } });
        }

        private void LoadData()
        {
            BuchtType item = new BuchtType();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.BuchtTypeDB.GetBuchtTypeByID(_ID);
               // CityID = Data.BuchtTypeDB.GetCity(item.ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;

            //if (CityID == 0)
            //    CityComboBox.SelectedIndex = 0;
            //else
            //    CityComboBox.SelectedValue = CityID;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                BuchtType item = this.DataContext as BuchtType;
                if (item.IsReadOnly == true)
                {
                    MessageBox.Show("این نوع بوخت ثابت می باشد، تغییر آن ممکن نمی باشد.");
                    return;
                }

                if (item.ParentID == -1) item.ParentID = null;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("نوع بوخت ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره نوع بوخت", ex);
            }
        }

        //private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (CityID == 0)
        //    {
        //        City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
        //        CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
        //      //  (this.DataContext as BuchtType).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
        //    }
        //    else
        //    {
        //        if (CityComboBox.SelectedValue == null)
        //        {
        //              City city = Data.CityDB.GetCityById(CityID);
        //            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
        //        }
        //        else
        //        {
        //            City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
        //            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
        //        }
        //    }
        //}
    }
}
