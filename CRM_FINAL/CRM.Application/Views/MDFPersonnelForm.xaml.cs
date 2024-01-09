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
    public partial class MDFPersonnelForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public MDFPersonnelForm()
        {
            InitializeComponent();
            Initialize();
        }

        public MDFPersonnelForm(int id)
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
            MDFPersonnel mDFPersonnel = new MDFPersonnel();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                mDFPersonnel = Data.MDFPersonnelDB.GetMDFPersonnelByID(_ID);
                CityID = Data.MDFPersonnelDB.GetCity(mDFPersonnel.ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = mDFPersonnel;

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
                MDFPersonnel mDFPersonnel = this.DataContext as MDFPersonnel;

                if (string.IsNullOrWhiteSpace(FirstNameTextbox.Text))
                    throw new Exception("لطفا نام را وارد نمایید");

                if (string.IsNullOrWhiteSpace(LastNameTextbox.Text))
                    throw new Exception("لطفا نام خانوادگی را وارد نمایید");

                mDFPersonnel.Detach();
                Save(mDFPersonnel);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کارمند MDF ،" + ex.Message + " !", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as MDFPersonnel).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        #endregion
    }
}
