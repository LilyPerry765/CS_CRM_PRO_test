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
    public partial class StatusList : Local.TabWindow
    {
        #region Constructor

        public StatusList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestStepComboBox.ItemsSource = RequestStepDB.GetRequestStepCheckable();
            RequestStepColumn.ItemsSource = RequestStepDB.GetRequestStepCheckable();
            RequestStatusTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestStatusType));
            RequestStatusTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.RequestStatusType));
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
            ItemsDataGrid.ItemsSource = Data.StatusDB.SearchStatus(RequestStepComboBox.SelectedIDs, RequestStatusTypeComboBox.SelectedIDs, TitleTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StatusDB.StatusInfo item = ItemsDataGrid.SelectedItem as StatusDB.StatusInfo;

                    DB.Delete<Data.Status>(item.ID);
                    ShowSuccessMessage("وضعیت مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف وضعیت", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                StatusDB.StatusInfo item = ItemsDataGrid.SelectedItem as StatusDB.StatusInfo;
                if (item == null) return;

                StatusForm window = new StatusForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {

            StatusForm window = new StatusForm();
            window.ShowDialog();
            LoadData();
        }

        #endregion


    }
}
