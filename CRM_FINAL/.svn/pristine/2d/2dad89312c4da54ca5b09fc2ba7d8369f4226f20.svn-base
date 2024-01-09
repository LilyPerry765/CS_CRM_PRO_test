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
    public partial class Failure117PostAccuracyForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public Failure117PostAccuracyForm()
        {
            InitializeComponent();
            Initialize();
        }

        public Failure117PostAccuracyForm(int id)
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
            Failure117PostAccuracy postAccuracy = new Failure117PostAccuracy();

            if (_ID == 0)
            {
                PostNoLabel.Visibility = Visibility.Collapsed;
                PostNoTextBox.Visibility = Visibility.Collapsed;
                PostNoRow.Height = GridLength.Auto;

                SaveButton.Content = "ذخیره";
            }
            else
            {
                FromPostNoLabel.Visibility = Visibility.Collapsed;
                FromPostNoTextBox.Visibility = Visibility.Collapsed;
                ToPostNoLabel.Visibility = Visibility.Collapsed;
                ToPostNoTextBox.Visibility = Visibility.Collapsed;
                FromPostNoRow.Height = GridLength.Auto;
                ToPostNoRow.Height = GridLength.Auto;

                postAccuracy = Data.Failure117CabenitAccuracyDB.GetPostAccuracyById(_ID);
                CityID = Data.Failure117CabenitAccuracyDB.GetCityForPost(postAccuracy.ID);

                if (postAccuracy.CorrectDate != null)
                {
                    CityComboBox.IsEnabled = false;
                    CenterComboBox.IsEnabled = false;
                    PostNoTextBox.IsReadOnly = true;
                    SaveButton.IsEnabled = false;
                }

                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = postAccuracy;

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
                Failure117PostAccuracy postAccuracy = this.DataContext as Failure117PostAccuracy;

                int fromPostNo = 0;
                int toPostNo = 0;

                if (_ID == 0)
                {
                    if (!string.IsNullOrEmpty(FromPostNoTextBox.Text))
                        fromPostNo = Convert.ToInt32(FromPostNoTextBox.Text);
                    else
                        throw new Exception("لطفا شماره کافو را وارد نمایید !");

                    if (!string.IsNullOrEmpty(ToPostNoTextBox.Text))
                        toPostNo = Convert.ToInt32(ToPostNoTextBox.Text);
                    if (toPostNo == 0)
                    {
                        postAccuracy.ID = 0;
                        postAccuracy.PostID = fromPostNo;

                        if (!Failure117CabenitAccuracyDB.CheckPostAccuracy(postAccuracy.CabinetID,postAccuracy.PostID, postAccuracy.CenterID))
                        {
                            postAccuracy.Detach();
                            Save(postAccuracy);
                        }
                        else
                            throw new Exception("پیش از این خرابی پست مورد نظر در این مرکز اعلام شده است");
                    }
                    else
                    {
                        for (int i = fromPostNo; i <= toPostNo; i++)
                        {
                            postAccuracy.ID = 0;
                            postAccuracy.PostID = i;

                            if (!Failure117CabenitAccuracyDB.CheckCabinetAccuracy(postAccuracy.CabinetID, postAccuracy.CenterID))
                            {
                                postAccuracy.Detach();
                                Save(postAccuracy);
                            }
                            else
                                throw new Exception("پیش از این خرابی پست " + postAccuracy.PostID.ToString() + " در این مرکز اعلام شده است");
                        }
                    }
                }
                else
                {
                    if (!Failure117CabenitAccuracyDB.CheckPostAccuracy(postAccuracy.CabinetID,postAccuracy.PostID, postAccuracy.CenterID))
                    {
                        postAccuracy.Detach();
                        Save(postAccuracy);
                    }
                    else
                        throw new Exception("پیش از این خرابی پست مورد نظر در این مرکز اعلام شده است");
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پست خراب" + " ، " + ex.Message + "!", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as Failure117PostAccuracy).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void PostNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
