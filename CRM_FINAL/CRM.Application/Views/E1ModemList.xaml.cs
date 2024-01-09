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
    public partial class E1ModemList : Local.TabWindow
    {
        #region Constructor & Fields

        public E1ModemList()
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

            ItemsDataGrid.ItemsSource = Data.E1ModemDB.SearchE1Modem(NameTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.E1Modem item = ItemsDataGrid.SelectedItem as CRM.Data.E1Modem;

                    DB.Delete<Data.E1Modem>(item.ID);
                    ShowSuccessMessage("مودم مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مودم", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                E1Modem item = ItemsDataGrid.SelectedItem as Data.E1Modem;
                if (item == null) return;

                E1ModemForm window = new E1ModemForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.E1Modem item = e.Row.Item as Data.E1Modem;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("مودم مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره مودم", ex);
            }
        }
        #endregion Event Handlers

        private void AddItem(object sender, RoutedEventArgs e)
        {
            E1ModemForm window = new E1ModemForm();
            window.ShowDialog();
        }
    }
}
