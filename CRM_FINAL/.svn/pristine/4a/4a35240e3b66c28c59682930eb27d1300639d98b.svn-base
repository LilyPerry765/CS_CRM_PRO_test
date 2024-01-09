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
    public partial class CauseOfBuchtSwitchingList : Local.TabWindow
    {
        #region Constructor & Fields

        public CauseOfBuchtSwitchingList()
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

            ItemsDataGrid.ItemsSource = Data.CauseBuchtSwitchingDB.Search(NameTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.CauseBuchtSwitching item = ItemsDataGrid.SelectedItem as CRM.Data.CauseBuchtSwitching;
                    if (item.IsReadOnly == true)
                    {
                        throw new Exception("این گزینه قابل حذف نمی باشد");
                    }
                    else
                    {
                        DB.Delete<Data.CauseBuchtSwitching>(item.ID);
                    }
                    ShowSuccessMessage("علت مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف علت", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CauseBuchtSwitching item = ItemsDataGrid.SelectedItem as Data.CauseBuchtSwitching;
                if (item == null) return;

                CauseOfBuchtSwitchingForm window = new CauseOfBuchtSwitchingForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.CauseBuchtSwitching item = e.Row.Item as Data.CauseBuchtSwitching;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("علت مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره علت", ex);
            }
        }
        #endregion Event Handlers

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CauseOfBuchtSwitchingForm window = new CauseOfBuchtSwitchingForm();
            window.ShowDialog();
        }
    }
}
