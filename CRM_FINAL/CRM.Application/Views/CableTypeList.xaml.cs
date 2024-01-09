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
    public partial class CableTypeList : Local.TabWindow
    {
        #region Constructor & Fields

        public CableTypeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {

        }


        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
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

            Pager.TotalRecords = Data.CableTypeDB.SearchCableTypeCount(CableTypeNameTextBox.Text.Trim());
            ItemsDataGrid.ItemsSource = Data.CableTypeDB.SearchCableType(CableTypeNameTextBox.Text.Trim(), startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.CableType item = ItemsDataGrid.SelectedItem as CRM.Data.CableType;
                    if (item.IsReadOnly == false)
                    {
                        DB.Delete<Data.CableType>(item.ID);
                        ShowSuccessMessage("نوع کابل مورد نظر حذف شد");
                    }
                    else
                    {
                        MessageBox.Show("امکان حذف این نوع کابل موجود نمی باشد");
                    }
                
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نوع کابل", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CableType item = ItemsDataGrid.SelectedItem as Data.CableType;
                if (item == null) return;

                CableTypeForm window = new CableTypeForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.CableType item = e.Row.Item as Data.CableType;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("نوع کابل مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره نوع کابل", ex);
            }
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            CableTypeForm window = new CableTypeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }
        #endregion Event Handlers


    }
}
