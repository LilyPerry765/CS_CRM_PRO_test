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
using Microsoft.Win32;
using System.ComponentModel;


namespace CRM.Application.Views
{    
    public partial class ADSLInstallCostCenterForm : Local.PopupWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLInstallCostCenterForm()
        {
            InitializeComponent();
            Initialize();
        }
        
        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetCitiesCheckable();
        }

        private void LoadData()
        {
            SaveButton.Content = "بروز رسانی";
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void LoadGrid(object sender, SelectionChangedEventArgs e)
        {
            CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
            ItemsDataGrid.ItemsSource = Data.ADSLInstallCostCenterDB.GetAdsLInstallCostByCityId((int)CityComboBox.SelectedValue);
            
            ResizeWindow();
        }

        private void Save(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                ADSLInstalCostCenter adsl = ItemsDataGrid.SelectedItem as ADSLInstalCostCenter;
                adsl.Detach();
                Save(adsl);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره مرکز ", ex);
            }

        }

        #endregion        
    }
}
