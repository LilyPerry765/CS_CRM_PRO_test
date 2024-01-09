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
    public partial class BankList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public BankList()
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
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.BankDB.SearchBanks(BankNameTextBox.Text.Trim(), StatusCheckBox.IsChecked);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            BankForm window = new BankForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Bank item = ItemsDataGrid.SelectedItem as Bank;
                if (item == null) return;

                BankForm window = new BankForm(item.ID);
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
                    Bank item = ItemsDataGrid.SelectedItem as Bank;

                    DB.Delete<Data.Bank>(item.ID);
                    ShowSuccessMessage("بانک مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف بانک", ex);
            }
        }

        #endregion
    }
}
