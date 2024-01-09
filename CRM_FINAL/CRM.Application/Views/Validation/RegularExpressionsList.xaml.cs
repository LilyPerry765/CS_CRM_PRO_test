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
    public partial class RegularExpressionsList : Local.TabWindow
    {
        #region Constructor & Fields

        public RegularExpressionsList()
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

            ItemsDataGrid.ItemsSource = Data.RegularExpressionsDB.SearchRegularExpressions(NameTextBox.Text.Trim(), RegularExpressinonTextBox.Text.Trim(), ErrorMessageTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.RegularExpression item = ItemsDataGrid.SelectedItem as CRM.Data.RegularExpression;

                    DB.Delete<Data.RegularExpression>(item.Id);
                    ShowSuccessMessage("عبارت منظم مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف عبارت منظم", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RegularExpression item = ItemsDataGrid.SelectedItem as Data.RegularExpression;
                if (item == null) return;

                RegularExpressionsForm window = new RegularExpressionsForm(item.Id);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.RegularExpression item = e.Row.Item as Data.RegularExpression;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("عبارت منظم مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره عبارت منظم", ex);
            }
        }
        #endregion Event Handlers

        private void ADDItem(object sender, RoutedEventArgs e)
        {

            RegularExpressionsForm window = new RegularExpressionsForm();
            window.ShowDialog();
        }
    }
}
