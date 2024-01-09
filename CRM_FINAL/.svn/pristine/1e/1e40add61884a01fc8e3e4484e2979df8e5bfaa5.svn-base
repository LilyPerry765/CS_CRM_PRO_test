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
using Enterprise;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class CityList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public CityList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ProvinceComboBox.ItemsSource = Data.ProvinceDB.GetProvincesCheckable();
            ProvincesColumn.ItemsSource = Data.ProvinceDB.GetProvincesCheckable();          
        }

        private void LoadData()
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
            ItemsDataGrid.ItemsSource = Data.CityDB.SearchCity(ProvinceComboBox.SelectedIDs, CityTextBox.Text);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CityForm Window = new CityForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                City item = ItemsDataGrid.SelectedItem as City;

                if (item == null)
                    return;

                CityForm Window = new CityForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") 
                return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    City item = ItemsDataGrid.SelectedItem as City;
                    DB.Delete<City>(item.ID);

                    ShowSuccessMessage("شهر مورد نظر حذف شد");
                    
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شهر", ex);
            }
        }

        #endregion
    }
}
