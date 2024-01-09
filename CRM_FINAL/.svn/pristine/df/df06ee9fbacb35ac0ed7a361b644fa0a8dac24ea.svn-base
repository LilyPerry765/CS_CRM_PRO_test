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
    /// Interaction logic for CustomerGroupList.xaml
    /// </summary>
    public partial class CustomerGroupList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public CustomerGroupList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CustomerTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetCustomerTypesCheckable();
            CustomerTypeColumn.ItemsSource = Data.CustomerTypeDB.GetCustomerTypesCheckable();
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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.CustomerGroupDB.SearchCustomerGroup(CustomerTypeComboBox.SelectedIDs, TitleTextBox.Text);
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            CustomerGroupForm Window = new CustomerGroupForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CustomerGroup item = ItemsDataGrid.SelectedItem as CustomerGroup;

                if (item == null)
                    return;

                CustomerGroupForm Window = new CustomerGroupForm(item.ID);
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
                    CustomerGroup item = ItemsDataGrid.SelectedItem as CustomerGroup;
                    DB.Delete<CustomerGroup>(item.ID);

                    ShowSuccessMessage("گروه مشترکین مورد نظر حذف شد");
                    
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
