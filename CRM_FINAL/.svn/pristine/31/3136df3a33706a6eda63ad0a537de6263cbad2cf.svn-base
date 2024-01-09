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
    public partial class BuchtTypeList : Local.TabWindow
    {
        #region Constructor & Fields

        public BuchtTypeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
           // CityComboBox.ItemsSource = Data.CityDB.GetAvailableCity();
           // CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
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
            Pager.TotalRecords = Data.BuchtTypeDB.SearchBuchtTypeCount( BuchtTypeNameTextBox.Text.Trim());
            ItemsDataGrid.ItemsSource = Data.BuchtTypeDB.SearchBuchtType( BuchtTypeNameTextBox.Text.Trim(), startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.BuchtType item = ItemsDataGrid.SelectedItem as CRM.Data.BuchtType;

                    if (item.IsReadOnly == true)
                    {
                        MessageBox.Show("این نوع بوخت ثابت می باشد، حذف آن ممکن نمی باشد.");
                        return;
                    }

                    DB.Delete<Data.BuchtType>(item.ID);
                    ShowSuccessMessage("نوع بوخت مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نوع بوخت", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                BuchtType item = ItemsDataGrid.SelectedItem as Data.BuchtType;
                if (item == null) return;

                BuchtTypeForm window = new BuchtTypeForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            BuchtTypeForm window = new BuchtTypeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.BuchtType item = e.Row.Item as Data.BuchtType;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("نوع بوخت مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره نوع بوخت", ex);
            }
        }
        //private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        //{
        //    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        //}
        #endregion Event Handlers
    }
}
