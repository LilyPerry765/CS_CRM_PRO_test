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
    public partial class ADSLCustomerGroupList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLCustomerGroupList()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

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
            ISPNameTextBox.Text = string.Empty;
            KeyWordTextBox.Text = string.Empty;
            
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = ADSLCustomerGroupDB.SearchADSLCustomerGroup(TitleTextBox.Text.Trim(), ISPNameTextBox.Text.Trim(), KeyWordTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLCustomerGroupForm window = new ADSLCustomerGroupForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLCustomerGroup item = ItemsDataGrid.SelectedItem as ADSLCustomerGroup;
                if (item == null) return;

                ADSLCustomerGroupForm window = new ADSLCustomerGroupForm(item.ID);
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
                    ADSLCustomerGroup item = ItemsDataGrid.SelectedItem as ADSLCustomerGroup;

                    DB.Delete<ADSLCustomerGroup>(item.ID);
                    ShowSuccessMessage("گروه مشتری مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد !", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف گروه مشتری", ex);
            }
        }

        #endregion
    }
}
