using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for MapItemForm.xaml
    /// </summary>
    public partial class MapItemForm : Local.PopupWindow
    {
        private Telerik.Windows.Controls.Map.Location _mouseLocation;

        public MapItemForm()
        {
            InitializeComponent();
            Initialize();
        }

        public MapItemForm(Telerik.Windows.Controls.Map.Location mouseLocation):this()
        {
            this._mouseLocation = mouseLocation;
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CityRadioButton.IsChecked = true;
        }

        private void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                if (PostRadioButton.IsChecked == true)
                {
                    PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetIDWithoutCoordinates((int)CabinetComboBox.SelectedValue);
                }
                else
                {
                    PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID(new List<int> { (int)CabinetComboBox.SelectedValue });
                }
            }
        }

        private void PostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                if (CenterRadioButton.IsChecked == true)
                {
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityIdWithoutCoordinates((int)CityComboBox.SelectedValue);
                }
                else
                {
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId((int)CityComboBox.SelectedValue);
                }
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                if (CabinetRadioButton.IsChecked == true)
                {
                    CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDWithoutCoordinates((int)CenterComboBox.SelectedValue);
                }
                else
                {
                    CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)CenterComboBox.SelectedValue);
                }
            }
        }

        private void CityRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            CityLabel.Visibility = Visibility.Visible;
            CityComboBox.Visibility = Visibility.Visible;

          CenterLabel.Visibility = Visibility.Collapsed;
          CenterComboBox.Visibility = Visibility.Collapsed;

          CabinetLabel.Visibility = Visibility.Collapsed;
          CabinetComboBox.Visibility = Visibility.Collapsed;

          PostLabel.Visibility = Visibility.Collapsed;
          PostComboBox.Visibility = Visibility.Collapsed;

        }

        private void CenterRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            CityLabel.Visibility = Visibility.Visible;
            CityComboBox.Visibility = Visibility.Visible;

            CenterLabel.Visibility = Visibility.Visible;
            CenterComboBox.Visibility = Visibility.Visible;

            CabinetLabel.Visibility = Visibility.Collapsed;
            CabinetComboBox.Visibility = Visibility.Collapsed;

            PostLabel.Visibility = Visibility.Collapsed;
            PostComboBox.Visibility = Visibility.Collapsed;
        }

        private void CabinetRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CityLabel.Visibility = Visibility.Visible;
            CityComboBox.Visibility = Visibility.Visible;

            CenterLabel.Visibility = Visibility.Visible;
            CenterComboBox.Visibility = Visibility.Visible;

            CabinetLabel.Visibility = Visibility.Visible;
            CabinetComboBox.Visibility = Visibility.Visible;

            PostLabel.Visibility = Visibility.Collapsed;
            PostComboBox.Visibility = Visibility.Collapsed;
        }

        private void PostRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CityLabel.Visibility = Visibility.Visible;
            CityComboBox.Visibility = Visibility.Visible;

            CenterLabel.Visibility = Visibility.Visible;
            CenterComboBox.Visibility = Visibility.Visible;

            CabinetLabel.Visibility = Visibility.Visible;
            CabinetComboBox.Visibility = Visibility.Visible;

            PostLabel.Visibility = Visibility.Visible;
            PostComboBox.Visibility = Visibility.Visible;
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CenterRadioButton.IsChecked == true)
                {

                    if (CenterComboBox.SelectedValue != null)
                    {
                        Center center = Data.CenterDB.GetCenterById((int)CenterComboBox.SelectedValue);
                        center.Latitude = _mouseLocation.Latitude;
                        center.Longitude = _mouseLocation.Longitude;
                        center.Detach();
                        DB.Save(center);
                    }
                }

                else if (CabinetRadioButton.IsChecked == true)
                {

                    if (CabinetComboBox.SelectedValue != null)
                    {
                        Cabinet cabinet = Data.CabinetDB.GetCabinetByID((int)CabinetComboBox.SelectedValue);
                        cabinet.Latitude = _mouseLocation.Latitude;
                        cabinet.Longitude = _mouseLocation.Longitude;
                        cabinet.Detach();
                        DB.Save(cabinet);
                    }
                }


                else if (PostRadioButton.IsChecked == true)
                {

                    if (PostComboBox.SelectedValue != null)
                    {
                        Post post = Data.PostDB.GetPostByID((int)PostComboBox.SelectedValue);
                        post.Latitude = _mouseLocation.Latitude;
                        post.Longitude = _mouseLocation.Longitude;
                        post.Detach();
                        DB.Save(post);
                    }
                }

                this.Close();
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره" , ex);
            }
        }
    }
}
