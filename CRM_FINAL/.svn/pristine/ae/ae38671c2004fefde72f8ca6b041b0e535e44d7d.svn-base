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
    public partial class OfficeList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public OfficeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityColumn.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsSource = Data.CityDB.GetCitiesCheckable();
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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CityComboBox.Reset();
            CodeTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            ContactPersonNameTextBox.Text = string.Empty;
            TelephoneTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            IsActiveCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.OfficeDB.SearchOfficeCount(CityComboBox.SelectedIDs, CodeTextBox.Text, NameTextBox.Text, ContactPersonNameTextBox.Text, TelephoneTextBox.Text, AddressTextBox.Text, IsActiveCheckBox.IsChecked);
            ItemsDataGrid.ItemsSource = Data.OfficeDB.SearchOffice(CityComboBox.SelectedIDs, CodeTextBox.Text, NameTextBox.Text, ContactPersonNameTextBox.Text, TelephoneTextBox.Text, AddressTextBox.Text, IsActiveCheckBox.IsChecked,startRowIndex,pageSize);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            OfficeForm window = new OfficeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Office office = ItemsDataGrid.SelectedItem as Office;
                if (office == null) return;

                OfficeForm window = new OfficeForm(office.ID);
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
                    Office item = ItemsDataGrid.SelectedItem as Office;
                    DB.Delete<Data.Office>(item.ID);
                    ShowSuccessMessage("دفتر خدماتی مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف دفتر خدماتی", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
