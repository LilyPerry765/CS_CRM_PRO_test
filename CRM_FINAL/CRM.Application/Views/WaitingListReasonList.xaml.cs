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
    public partial class WaitingListReasonList : Local.TabWindow
    {
        #region Constructor

        public WaitingListReasonList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Load Methods
        private void Initialize()
        {
            RequestTypeColumn.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();

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
            ItemsDataGrid.ItemsSource = Data.WaitingListReasonDB.SearchWaitingListReason(RequestTypeComboBox.SelectedIDs, TitleTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.WaitingListReason item = ItemsDataGrid.SelectedItem as CRM.Data.WaitingListReason;

                    DB.Delete<Data.WaitingListReason>(item.ID);
                    ShowSuccessMessage("علت لیست انتظار مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف علت لیست انتظار", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                WaitingListReason item = ItemsDataGrid.SelectedItem as Data.WaitingListReason;
                if (item == null) return;

                WaitingListReasonForm window = new WaitingListReasonForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            WaitingListReasonForm window = new WaitingListReasonForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        #endregion Event Handlers

        
    }
}
