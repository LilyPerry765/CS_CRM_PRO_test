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
    public partial class E1LinkTypeList : Local.TabWindow
    {
        #region Constructor & Fields

        public E1LinkTypeList()
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

            ItemsDataGrid.ItemsSource = Data.E1LinkTypeDB.SearchE1LinkType(NameTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.E1LinkType item = ItemsDataGrid.SelectedItem as CRM.Data.E1LinkType;

                    DB.Delete<Data.E1LinkType>(item.ID);
                    ShowSuccessMessage("نوع لینک مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف E1LinkType", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                E1LinkType item = ItemsDataGrid.SelectedItem as Data.E1LinkType;
                if (item == null) return;

                E1LinkTypeForm window = new E1LinkTypeForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.E1LinkType item = e.Row.Item as Data.E1LinkType;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("نوع لینک مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره نوع لینک", ex);
            }
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {

            E1LinkTypeForm window = new E1LinkTypeForm();
            window.ShowDialog();
        }
        #endregion Event Handlers


    }
}
