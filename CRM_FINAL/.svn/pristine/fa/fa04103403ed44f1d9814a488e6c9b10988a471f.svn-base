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
    public partial class PostGroupList : Local.TabWindow
    {
        #region Constructor & Fields

        public PostGroupList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            //CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
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


            int groupNo = -1;
            if (!string.IsNullOrWhiteSpace(GroupNoTextBox.Text)) groupNo = Convert.ToInt32(GroupNoTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.PostGroupDB.SearchPostGroup(groupNo, startRowIndex, pageSize);
            Pager.TotalRecords = Data.PostGroupDB.SearchPostGroupCount(groupNo);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PostGroupInfo item = ItemsDataGrid.SelectedItem as PostGroupInfo;

                    DB.Delete<Data.PostGroup>(item.ID);
                    ShowSuccessMessage("گروه پست مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف گروه پست", ex);
            }
        }

      

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PostGroupInfo item = ItemsDataGrid.SelectedItem as Data.PostGroupInfo;
                if (item == null) return;

                PostGroupForm window = new PostGroupForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    PostGroup item = e.Row.Item as PostGroup;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("گروه پست مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره گروه پست", ex);
            //}
        }
        #endregion Event Handlers

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PostGroupForm window = new PostGroupForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }
    }
}
