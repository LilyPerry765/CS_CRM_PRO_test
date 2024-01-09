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
    public partial class ContractorList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ContractorList()
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
            ItemsDataGrid.ItemsSource = Data.ContractorDB.SearchContractors(TitleTextBox.Text.Trim(), HeaderNameTextBox.Text.Trim(),AddressTextBox.Text.Trim(), TelephoneTextBox.Text.Trim(), MobileTextBox.Text.Trim(), FaxTextBox.Text.Trim(), ContractStartDate.SelectedDate, ContractEndDate.SelectedDate);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ContractorForm window = new ContractorForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Contractor item = ItemsDataGrid.SelectedItem as Contractor;
                if (item == null) return;

                ContractorForm window = new ContractorForm(item.ID);
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
                    Contractor item = ItemsDataGrid.SelectedItem as Contractor;

                    DB.Delete<Contractor>(item.ID);
                    ShowSuccessMessage("پیمانکار مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پیمانکار", ex);
            }
        }

        #endregion
    }
}
