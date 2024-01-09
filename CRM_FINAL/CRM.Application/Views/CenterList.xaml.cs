using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class CenterList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public CenterList()
        {
           // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
         //   {
                InitializeComponent();
                Initialize();
           // }
        }

        #endregion

        #region Methods

        private void Initialize()
        {

           CityColumn.ItemsSource = Data.RegionDB.GetRegions();
           CityComboBox.ItemsSource = Data.RegionDB.GetRegionsCheckable();

        }

        public void LoadData()
        {

         Search(null, null);

        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
                
        private void Search(object sender, RoutedEventArgs e)
        {
            int centerCode = !string.IsNullOrWhiteSpace(CenterCodeTextBox.Text) ? Convert.ToInt32(CenterCodeTextBox.Text) : -1;
            // int isActiveCheckBox = IsActiveCheckBox.IsChecked  == false ? 0 : 1;
            ItemsDataGrid.ItemsSource = Data.CenterDB.SearchCenter(CityComboBox.SelectedIDs, centerCode, CenterNameTextBox.Text, TelephoneTextBox.Text, AddressTextBox.Text, IsActiveCheckBox.IsChecked);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CenterForm window = new CenterForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Center center = ItemsDataGrid.SelectedItem as Center;
                if (center == null) return;
                
                CenterForm window = new CenterForm(center.ID);
                window.ShowDialog();
                
                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex == -1 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف کردن مطمئن هستید", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Center item = ItemsDataGrid.SelectedItem as Center;
                    DB.Delete<Data.Center>(item.ID);
                    ShowSuccessMessage("مرکز مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مرکز", ex);
            }
        }
                
        #endregion
    }
}
