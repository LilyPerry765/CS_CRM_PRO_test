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
    public partial class CabinetTypeList : Local.TabWindow
    {
        #region Constructor & Fields

        public CabinetTypeList()
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
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int cabinetCapacity = -1;
            if (!string.IsNullOrWhiteSpace(CabinetCapacityTextBox.Text)) cabinetCapacity = Convert.ToInt32(CabinetCapacityTextBox.Text);

            Pager.TotalRecords = Data.CabinetTypeDB.SearchCabinetTypeCount(cabinetCapacity, CabinetTypeNameTextBox.Text.Trim());
            ItemsDataGrid.ItemsSource = Data.CabinetTypeDB.SearchCabinetType(cabinetCapacity, CabinetTypeNameTextBox.Text.Trim(), startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CabinetType item = ItemsDataGrid.SelectedItem as CabinetType;

                    DB.Delete<CabinetType>(item.ID);
                    ShowSuccessMessage("نوع کافو مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نوع کافو", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CabinetType item = ItemsDataGrid.SelectedItem as CabinetType;
                if (item == null) return;
                CabinetTypeForm window = new CabinetTypeForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.CabinetType item = e.Row.Item as Data.CabinetType;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("نوع کافو مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره نوع کافو", ex);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CabinetTypeForm window = new CabinetTypeForm();
            window.ShowDialog();
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        #endregion Event Handlers


    }
}
