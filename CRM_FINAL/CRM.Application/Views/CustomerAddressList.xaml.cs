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
    public partial class CustomerAddressList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public CustomerAddressList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();            
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
           // LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            //for test
            //DateTime fromDateTime = Convert.ToDateTime("9-13-2015 00:00:00");
            //DateTime toDateTime = Convert.ToDateTime("9-13-2015 00:00:00");
            //var x = CRM.Data.ServiceHost.ServiceHostDB.GetChangeTelephone(fromDateTime, toDateTime, new List<int> { }, new List<int> { }, DB.ServiceHostCity.Kermanshah);

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.AddressDB.SearchAddressCount(CentersComboBox.SelectedIDs, PostalCodeTextBox.Text, AddressTextBox.Text);
            ItemsDataGrid.ItemsSource = AddressDB.SearchAddresses(CentersComboBox.SelectedIDs, PostalCodeTextBox.Text, AddressTextBox.Text, startRowIndex, pageSize);
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            CustomerAddressForm window = new CustomerAddressForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Address item = ItemsDataGrid.SelectedItem as Address;
                if (item == null) return;

                CustomerAddressForm window = new CustomerAddressForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
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
                    Address item = ItemsDataGrid.SelectedItem as Address;

                    DB.Delete<Data.Address>(item.ID);
                    ShowSuccessMessage("آدرس مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف آدرس", ex);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Address address = e.Row.Item as Address;

                address.Detach();
                DB.Save(address);

                Search(null, null);

                ShowSuccessMessage("استان مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره استان", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
