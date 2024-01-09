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
    /// <summary>
    /// Interaction logic for PostGroupForm.xaml
    /// </summary>
    public partial class PostGroupForm : Local.PopupWindow 
    {
        private int _ID=0;
        private int CityID = 0;
        public PostGroupForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        public PostGroupForm(int id):this()
        {
            _ID = id;
            Initialize();
        }
        private void LoadData()
        {
            PostGroup postGroup = new PostGroup();
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                postGroup = PostGroupDB.GetPostGroupByID(_ID);
                CityID = Data.PostGroupDB.GetCity(postGroup.ID);
                SaveButton.Content = "بروز رسانی";
            }
            this.DataContext = postGroup;

            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;

            else
                CityComboBox.SelectedValue = CityID;
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }



        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as PostGroup).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                PostGroup postGroup = this.DataContext as PostGroup;
                postGroup.Detach();
                Save(postGroup);
                ShowSuccessMessage("ذخیره گروه پست انجام شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("نمی توان گروه پست هم شماره وارد کرد .خطا در ذخیره گروه پست", ex);
                }
                else
                {
                    ShowErrorMessage("ذخیره گروه پست انجام نشد", ex);
                }
               
            }
        }
    }
}
