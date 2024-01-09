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
    public partial class ADSLTelephoneAccuracyForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public ADSLTelephoneAccuracyForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLTelephoneAccuracyForm(int id)
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
            ADSLTelephoneAccuracy telephoneAccuracy = new ADSLTelephoneAccuracy();

            if (_ID == 0)
            {
                TelephoneNoLabel.Visibility = Visibility.Collapsed;
                TelephoneNoTextBox.Visibility = Visibility.Collapsed;
                TelephoneNoRow.Height = GridLength.Auto;

                SaveButton.Content = "ذخیره";
            }
            else
            {
                FromTelephoneNoLabel.Visibility = Visibility.Collapsed;
                FromTelephoneNoTextBox.Visibility = Visibility.Collapsed;
                ToTelephoneNoLabel.Visibility = Visibility.Collapsed;
                ToTelephoneNoTextBox.Visibility = Visibility.Collapsed;
                FromTelephoneNoRow.Height = GridLength.Auto;
                ToTelephoneNoRow.Height = GridLength.Auto;

                telephoneAccuracy = Data.ADSLTelephoneAccuracyDB.GetTelephoneAccuracyById(_ID);
                CityID = Data.ADSLTelephoneAccuracyDB.GetCityForTelephone(telephoneAccuracy.ID);

                if (telephoneAccuracy.CorrectDate != null)
                {
                    CityComboBox.IsEnabled = false;
                    CenterComboBox.IsEnabled = false;
                    TelephoneNoTextBox.IsReadOnly = true;
                    SaveButton.IsEnabled = false;
                }

                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = telephoneAccuracy;

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
                ADSLTelephoneAccuracy telephoneAccuracy = this.DataContext as ADSLTelephoneAccuracy;

                long fromTelephoneNo = 0;
                long toTelephoneNo = 0;

                if (_ID == 0)
                {
                    if (!string.IsNullOrEmpty(FromTelephoneNoTextBox.Text))
                        fromTelephoneNo = Convert.ToInt64(FromTelephoneNoTextBox.Text);
                    else
                        throw new Exception("لطفا شماره تلفن را وارد نمایید !");

                    if (!string.IsNullOrEmpty(ToTelephoneNoTextBox.Text))
                        toTelephoneNo = Convert.ToInt64(ToTelephoneNoTextBox.Text);
                    if (toTelephoneNo == 0)
                    {
                        telephoneAccuracy.ID = 0;
                        telephoneAccuracy.TelephoneNo = fromTelephoneNo;

                        if (!ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneAccuracy.TelephoneNo, telephoneAccuracy.CenterID))
                        {
                            telephoneAccuracy.Detach();
                            Save(telephoneAccuracy);
                        }
                        else
                            throw new Exception("پیش از این خرابی تلفن مورد نظر در این مرکز اعلام شده است");
                    }
                    else
                    {
                        for (long i = fromTelephoneNo; i <= toTelephoneNo; i++)
                        {
                            telephoneAccuracy.ID = 0;
                            telephoneAccuracy.TelephoneNo = i;

                            if (!ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneAccuracy.TelephoneNo, telephoneAccuracy.CenterID))
                            {
                                telephoneAccuracy.Detach();
                                Save(telephoneAccuracy);
                            }
                            else
                                throw new Exception("پیش از این خرابی تلفن " + telephoneAccuracy.TelephoneNo.ToString() + " در این مرکز اعلام شده است");
                        }
                    }
                }
                else
                {
                    if (!ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneAccuracy.TelephoneNo, telephoneAccuracy.CenterID))
                    {
                        telephoneAccuracy.Detach();
                        Save(telephoneAccuracy);
                    }
                    else
                        throw new Exception("پیش از این خرابی تلفن مورد نظر در این مرکز اعلام شده است");
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تلفن خراب" + " ، " + ex.Message + "!", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as ADSLTelephoneAccuracy).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void TelephoneNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
