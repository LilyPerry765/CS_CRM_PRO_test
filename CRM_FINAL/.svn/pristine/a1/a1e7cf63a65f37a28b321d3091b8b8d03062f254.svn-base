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
    public partial class PostTypeList : Local.TabWindow
    {
        #region Constructor & Fields

        public PostTypeList()
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
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        private void Search(object sender, RoutedEventArgs e)
        {


            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            Pager.TotalRecords = Data.PostTypeDB.SearchPostTypeCount(PostTypeNameTextBox.Text.Trim());
            ItemsDataGrid.ItemsSource = Data.PostTypeDB.SearchPostType(PostTypeNameTextBox.Text.Trim(), startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PostType item = ItemsDataGrid.SelectedItem as PostType;

                    DB.Delete<Data.PostType>(item.ID);
                    ShowSuccessMessage("نوع پست مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نوع پست", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PostType item = ItemsDataGrid.SelectedItem as Data.PostType;
                if (item == null) return;

                PostTypeForm window = new PostTypeForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.PostType item = e.Row.Item as Data.PostType;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("نوع پست مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره نوع پست", ex);
            }
        }

        #endregion Event Handlers

        private void AddItem(object sender, RoutedEventArgs e)
        {

            PostTypeForm window = new PostTypeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }
    }
}
