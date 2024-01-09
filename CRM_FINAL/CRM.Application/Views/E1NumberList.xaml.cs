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
    public partial class E1NumberList : Local.TabWindow
    {
        #region Constructor & Fields

        public E1NumberList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PositionColumn.ItemsSource = Data.E1PositionDB.GetPositionCheckable();
            StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.E1NumberStatus));
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


            int number = -1;
            if (!int.TryParse(NumberTextBox.Text.Trim(), out number)) { number = -1; };


            Pager.TotalRecords = Data.E1NumberDB.SearchE1NumberCount(
            CityComboBox.SelectedIDs,
            CenterComboBox.SelectedIDs,
            DDFComboBox.SelectedIDs,
            BayComboBox.SelectedIDs,
            PositionComboBox.SelectedIDs,
            number);
            
            ItemsDataGrid.ItemsSource = Data.E1NumberDB.SearchNumber(
            CityComboBox.SelectedIDs,
            CenterComboBox.SelectedIDs,
            DDFComboBox.SelectedIDs,
            BayComboBox.SelectedIDs,
            PositionComboBox.SelectedIDs,
            number,
            startRowIndex,
            pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.E1Number item = ItemsDataGrid.SelectedItem as CRM.Data.E1Number;
                    DB.Delete<Data.E1Number>(item.ID);
                    ShowSuccessMessage("پی سی ام مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پی سی ام", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.E1Number item = ItemsDataGrid.SelectedItem as CRM.Data.E1Number;
                if (item == null) return;

                E1NumberForm window = new E1NumberForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            E1NumberForm window = new E1NumberForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);

        }



        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }
        private void CenterUserControl_DoCenterComboBoxLostFocus_1(object sender, RoutedEventArgs e)
        {
            DDFComboBox.ItemsSource = Data.E1DDFDB.GetDDFCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }
        private void DDFComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BayComboBox.ItemsSource = Data.E1BayDB.GetBayCheckableByDDFIDs(DDFComboBox.SelectedIDs);
        }
        private void BayComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PositionComboBox.ItemsSource = Data.E1PositionDB.GetPositionCheckableByBayIDs(BayComboBox.SelectedIDs);
        }
        #endregion Event Handlers







    }
}

