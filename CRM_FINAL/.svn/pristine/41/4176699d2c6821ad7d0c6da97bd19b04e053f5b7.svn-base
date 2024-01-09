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
    public partial class QuotaJobTitleList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public QuotaJobTitleList()
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
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.QuotaJobTitleDB.SearchQuotaJobTitle(JobTitleTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            QuotaJobTitleForm window = new QuotaJobTitleForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                QuotaJobTitle item = ItemsDataGrid.SelectedItem as Data.QuotaJobTitle;
                if (item == null) return;
                QuotaJobTitleForm window = new QuotaJobTitleForm(item.ID);
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
                    QuotaJobTitle item = ItemsDataGrid.SelectedItem as QuotaJobTitle;
                    DB.Delete<Data.QuotaJobTitle>(item.ID);
                    ShowSuccessMessage("شغل مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شغل", ex);
            }
        }
        
        #endregion
    }
}
