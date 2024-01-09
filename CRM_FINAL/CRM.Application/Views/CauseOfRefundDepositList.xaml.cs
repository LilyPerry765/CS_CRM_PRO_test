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
    public partial class CauseOfRefundDepositList : Local.TabWindow
    {
        #region Constructor & Fields

        public CauseOfRefundDepositList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {

        }

        public void LoadData()
        {
            Search(null, null);
        }
        #endregion Load Methods

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

            ItemsDataGrid.ItemsSource = Data.CauseOfRefundDepositDB.Search(NameTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.CauseOfRefundDeposit item = ItemsDataGrid.SelectedItem as CRM.Data.CauseOfRefundDeposit;

                    DB.Delete<Data.CauseOfRefundDeposit>(item.ID);
                    ShowSuccessMessage("مودم مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف علت", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CauseOfRefundDeposit item = ItemsDataGrid.SelectedItem as Data.CauseOfRefundDeposit;
                if (item == null) return;

                CauseOfRefundDepositForm window = new CauseOfRefundDepositForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.CauseOfRefundDeposit item = e.Row.Item as Data.CauseOfRefundDeposit;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("علت مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره علت", ex);
            }
        }
        #endregion Event Handlers

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CauseOfRefundDepositForm window = new CauseOfRefundDepositForm();
            window.ShowDialog();
        }
    }
}
