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
    public partial class E1NumberForm : Local.PopupWindow
    {
        private int _ID = 0;
        private int CityID = 0;

        public E1NumberForm()
        {
            InitializeComponent();
            Initialize();
        }

        public E1NumberForm(int id)
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
            E1Number item = new E1Number();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.E1NumberDB.GetE1NumberByID(_ID);
                CityID = Data.E1PositionDB.GetCityByPositionID(item.PositionID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
            {
                CityComboBox.SelectedValue = CityID;
                CityComboBox_SelectionChanged(null, null);
            }


            if (_ID != 0)
            {
                CenterComboBox.SelectedValue = Data.E1PositionDB.GetCenterByPositionID(item.ID).ID;
                CenterComboBox_SelectionChanged(null, null);

                E1Position e1Position = Data.E1PositionDB.GetE1PositionByID(item.PositionID);
                E1Bay E1Bay = Data.E1BayDB.GetE1BayByID(e1Position.BayID);
                E1DDF E1DDf = Data.E1DDFDB.GetE1DDFByID(E1Bay.DDFID);

                DDFComboBox.SelectedValue = E1DDf.ID;
                DDFComboBox_SelectionChanged(null, null);

                BayComboBox.SelectedValue = E1Bay.ID;
                BayComboBox_SelectionChanged(null, null);
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
                E1Number item = this.DataContext as E1Number;
                item.Status = (byte)DB.E1NumberStatus.Free;
                item.Detach();
                DB.Save(item);

                ShowSuccessMessage(" پی سی ام ذخیره  شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("نمی توان دو پی سی ام هم شماره در یک ردیف وارد کرد .خطا در ذخیره پی سی ام", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره  پی سی ام", ex);
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

        private void BayComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BayComboBox.SelectedValue != null)
             {
                 PositionCombobox.ItemsSource = Data.E1PositionDB.GetPositionCheckableByBayIDs(new List<int> { (int)BayComboBox.SelectedValue });
             }
        }
    }
}

