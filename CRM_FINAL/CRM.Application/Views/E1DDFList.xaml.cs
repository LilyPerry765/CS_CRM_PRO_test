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
    public partial class E1DDFList : Local.TabWindow
    {
        #region Constructor & Fields

        public E1DDFList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            DDFTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DDFType));
            DDFTypeColumn.ItemsSource   = Helper.GetEnumCheckable(typeof(DB.DDFType));

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

            int DDFNumber = -1;
            if (!int.TryParse(DDFTextBox.Text.Trim(), out DDFNumber)) { DDFNumber = -1; };

            Pager.TotalRecords = Data.E1DDFDB.SearchE1WayCount(
            CenterComboBox.SelectedIDs,
            DDFNumber
            );


            ItemsDataGrid.ItemsSource = Data.E1DDFDB.SearchE1Way(
             CenterComboBox.SelectedIDs,
              DDFNumber
             ,startRowIndex
             ,pageSize
            );
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    E1DDF item = ItemsDataGrid.SelectedItem as E1DDF;

                    DB.Delete<Data.E1DDF>(item.ID);
                    ShowSuccessMessage("ردیف مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف ردیف", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                E1DDF item = ItemsDataGrid.SelectedItem as Data.E1DDF;
                if (item == null) return;

                E1DDFForm window = new E1DDFForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.E1DDF item = e.Row.Item as Data.E1DDF;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("ردیف مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره ردیف", ex);
            }

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            E1DDFForm window = new E1DDFForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);

        }
        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }
        #endregion Event Handlers









    }
}
