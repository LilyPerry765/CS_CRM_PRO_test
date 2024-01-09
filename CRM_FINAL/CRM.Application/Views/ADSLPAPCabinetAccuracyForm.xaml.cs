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
    public partial class ADSLPAPCabinetAccuracyForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public ADSLPAPCabinetAccuracyForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLPAPCabinetAccuracyForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
            ADSLPAPCabinetAccuracy cabinetAccuracy = new ADSLPAPCabinetAccuracy();

            if (_ID == 0)
            {
                CabinetNoLabel.Visibility = Visibility.Collapsed;
                CabinetNoTextBox.Visibility = Visibility.Collapsed;
                CabinetNoRow.Height = GridLength.Auto;

                SaveButton.Content = "ذخیره";
            }
            else
            {
                FromCabinetNoLabel.Visibility = Visibility.Collapsed;
                FromCabinetNoTextBox.Visibility = Visibility.Collapsed;
                ToCabinetNoLabel.Visibility = Visibility.Collapsed;
                ToCabinetNoTextBox.Visibility = Visibility.Collapsed;
                FromCabinetNoRow.Height = GridLength.Auto;
                ToCabinetNoRow.Height = GridLength.Auto;

                cabinetAccuracy = Data.ADSLPAPCabinetAccuracyDB.GetCabenitAccuracyById(_ID);
                CityID = Data.ADSLPAPCabinetAccuracyDB.GetCity(cabinetAccuracy.ID);

                if (cabinetAccuracy.CorrectDate != null)
                {
                    CityComboBox.IsEnabled = false;
                    CenterComboBox.IsEnabled = false;
                    CabinetNoTextBox.IsReadOnly = true;
                    SaveButton.IsEnabled = false;
                }

                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = cabinetAccuracy;

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
            try
            {
                ADSLPAPCabinetAccuracy cabenitAccuracy = this.DataContext as ADSLPAPCabinetAccuracy;
                int fromCabinetNo = 0;
                int toCabinetNo = 0;

                if (_ID == 0)
                {
                    if (!string.IsNullOrEmpty(FromCabinetNoTextBox.Text))
                        fromCabinetNo = Convert.ToInt32(FromCabinetNoTextBox.Text);
                    else
                        throw new Exception("لطفا شماره کافو را وارد نمایید !");

                    if (!string.IsNullOrEmpty(ToCabinetNoTextBox.Text))
                        toCabinetNo = Convert.ToInt32(ToCabinetNoTextBox.Text);
                    if (toCabinetNo == 0)
                    {
                        cabenitAccuracy.ID = 0;
                        cabenitAccuracy.CabinetID = fromCabinetNo;

                        if (!ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabenitAccuracy.CabinetID, cabenitAccuracy.CenterID))
                        {
                            cabenitAccuracy.Detach();
                            Save(cabenitAccuracy);
                        }
                        else
                            throw new Exception("پیش از این خرابی کافو مورد نظر در این مرکز اعلام شده است");
                    }
                    else
                    {
                        for (int i = fromCabinetNo; i <= toCabinetNo; i++)
                        {
                            cabenitAccuracy.ID = 0;
                            cabenitAccuracy.CabinetID = i;

                            if (!ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabenitAccuracy.CabinetID, cabenitAccuracy.CenterID))
                            {
                                cabenitAccuracy.Detach();
                                Save(cabenitAccuracy);
                            }
                            else
                                throw new Exception("پیش از این خرابی کافو " + cabenitAccuracy.CabinetID.ToString() + " در این مرکز اعلام شده است");
                        }
                    }
                }
                else
                {
                    if (!ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabenitAccuracy.CabinetID, cabenitAccuracy.CenterID))
                    {
                        cabenitAccuracy.Detach();
                        Save(cabenitAccuracy);
                    }
                    else
                        throw new Exception("پیش از این خرابی کافو مورد نظر در این مرکز اعلام شده است");
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کافو خراب" + " ، " + ex.Message + "!", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as ADSLPAPCabinetAccuracy).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void CabinetNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
