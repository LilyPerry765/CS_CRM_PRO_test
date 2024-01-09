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
    public partial class Failure117TelephoneAccuracyRemovalForm : Local.PopupWindow
    {
        #region Properties

        private int CityID = 0;

        #endregion

        #region Constructors

        public Failure117TelephoneAccuracyRemovalForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void RemovalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Failure117TelephoneAccuracy telephoneAccuracy = new Failure117TelephoneAccuracy();

                long fromTelephoneNo = 0;
                long toTelephoneNo = 0;

                if (!string.IsNullOrEmpty(FromTelephoneNoTextBox.Text))
                    fromTelephoneNo = Convert.ToInt64(FromTelephoneNoTextBox.Text.Trim());
                else
                    throw new Exception("لطفا شماره تلفن را وارد نمایید !");

                if (!string.IsNullOrEmpty(ToTelephoneNoTextBox.Text))
                    toTelephoneNo = Convert.ToInt64(ToTelephoneNoTextBox.Text.Trim());

                if (toTelephoneNo == 0)
                {
                    if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(fromTelephoneNo, (int)CenterComboBox.SelectedValue))
                    {
                        telephoneAccuracy = Failure117CabenitAccuracyDB.GetTelephoneAccuracy(fromTelephoneNo);                        
                        telephoneAccuracy.CorrectDate = DB.GetServerDate();

                        telephoneAccuracy.Detach();
                        Save(telephoneAccuracy);
                    }
                    else
                        throw new Exception("این تلفن در لیست خرابی های رفع نشده موجود نمی باشد");
                }
                else
                {
                    for (long i = fromTelephoneNo; i <= toTelephoneNo; i++)
                    {
                        if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(fromTelephoneNo, (int)CenterComboBox.SelectedValue))
                        {
                            telephoneAccuracy = Failure117CabenitAccuracyDB.GetTelephoneAccuracy(fromTelephoneNo);
                            telephoneAccuracy.CorrectDate = DB.GetServerDate();

                            telephoneAccuracy.Detach();
                            Save(telephoneAccuracy);
                        }
                        else
                            throw new Exception("این تلفن در لیست خرابی های رفع نشده موجود نمی باشد");
                    }
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رفع تلفن خراب" + " ، " + ex.Message + "!", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                //(this.DataContext as Failure117TelephoneAccuracy).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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
