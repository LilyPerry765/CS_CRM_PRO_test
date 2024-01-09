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
    public partial class SpaceAndPowerCustomerList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public SpaceAndPowerCustomerList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Load Methods

        private void Initialize()
        {
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
            TitleTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            TelephoneNoTextBox.Text = string.Empty;
            MobileTextBox.Text = string.Empty;
            FaxTextBox.Text = string.Empty;

            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.SpaceAndPowerCustomerDB.SearchCustomerCount(TitleTextBox.Text, AddressTextBox.Text, EmailTextBox.Text, TelephoneNoTextBox.Text, MobileTextBox.Text, FaxTextBox.Text, FromInsertDate.SelectedDate, ToInsertDate.SelectedDate);
            ItemsDataGrid.ItemsSource = Data.SpaceAndPowerCustomerDB.SearchCustomer(TitleTextBox.Text, AddressTextBox.Text, EmailTextBox.Text, TelephoneNoTextBox.Text, MobileTextBox.Text, FaxTextBox.Text, FromInsertDate.SelectedDate, ToInsertDate.SelectedDate, startRowIndex, pageSize);

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            SpaceAndPowerCustomerForm window = new SpaceAndPowerCustomerForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Data.SpaceAndPowerCustomer item = ItemsDataGrid.SelectedItem as Data.SpaceAndPowerCustomer;
                if (item == null) return;

                SpaceAndPowerCustomerForm window = new SpaceAndPowerCustomerForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Data.SpaceAndPowerCustomer item = ItemsDataGrid.SelectedItem as Data.SpaceAndPowerCustomer;

                    DB.Delete<SpaceAndPowerCustomer>(item.ID);

                    ShowSuccessMessage("متقاضی فضا و پاور مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف متقاضی فضا و پاور", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
