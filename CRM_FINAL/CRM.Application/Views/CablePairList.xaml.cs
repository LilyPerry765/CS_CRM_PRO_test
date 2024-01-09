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
    public partial class CablePairList : Local.TabWindow
    {
        #region Constructor & Fields

        public CablePairList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            //  CableColumn.ItemsSource = Data.CableDB.GetCableCheckable();
            CableNumberComboBox.ItemsSource = Data.CableDB.GetCableCheckable();

            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CablePairStatus));
            // StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.CablePairStatus));

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }


        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        public void LoadData()
        {
            //  Search(null, null);
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
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;

            int fromCablePairNumber = -1;
            if (!string.IsNullOrWhiteSpace(FromCablePairNumberTextBox.Text)) fromCablePairNumber = Convert.ToInt32(FromCablePairNumberTextBox.Text);

            int toCablePairNumber = -1;
            if (!string.IsNullOrWhiteSpace(ToCablePairNumberTextBox.Text)) toCablePairNumber = Convert.ToInt32(ToCablePairNumberTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.CablePairDB.SearchCablePair(
                                                                          CityComboBox.SelectedIDs,
                                                                          CenterComboBox.SelectedIDs,
                                                                          CableNumberComboBox.SelectedIDs_l,
                                                                          fromCablePairNumber,
                                                                          toCablePairNumber,
                                                                          StatusComboBox.SelectedIDs,
                                                                          startRowIndex,
                                                                          pageSize, out count
                                                                       );

            Pager.TotalRecords = count;
            this.Cursor = Cursors.Arrow;
        }

        //private void DeleteItem(object sender, RoutedEventArgs e)
        //{
        //    if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

        //    try
        //    {
        //        MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

        //        if (result == MessageBoxResult.Yes)
        //        {
        //            CRM.Data.CablePair item = ItemsDataGrid.SelectedItem as CRM.Data.CablePair;

        //            DB.Delete<Data.CablePair>(item.ID);
        //            ShowSuccessMessage("زوچ سیم مورد نظر حذف شد");
        //            LoadData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage("خطا در حذف زوج سیم", ex);
        //    }
        //}

        //private void EditItem(object sender, RoutedEventArgs e)
        //{
        //    if (ItemsDataGrid.SelectedIndex >= 0)
        //    {
        //        CablePair item = ItemsDataGrid.SelectedItem as Data.CablePair;
        //        if (item == null) return;

        //        CablePairForm window = new CablePairForm(item.ID);
        //        window.ShowDialog();

        //        if (window.DialogResult == true)
        //            LoadData();
        //    }
        //}

        private void AssignBucht(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CablePairInfo item = ItemsDataGrid.SelectedItem as Data.CablePairInfo;
                if (item == null) return;

                AssignCablePairToBuchtForm window = new AssignCablePairToBuchtForm(item.CableID, (byte)DB.TypeCablePairToBucht.Assign);
                window.ShowDialog();

                LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.CablePair item = e.Row.Item as Data.CablePair;
            //    item.Detach();
            //    DB.Save(item);
            //    Search(null, null);
            //    ShowSuccessMessage("زوج سیم مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره زوج سیم", ex);
            //}
        }
        private void LeaveItems(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CablePairInfo item = ItemsDataGrid.SelectedItem as Data.CablePairInfo;
                if (item == null) return;

                AssignCablePairToBuchtForm window = new AssignCablePairToBuchtForm(item.CableID, (byte)DB.TypeCablePairToBucht.Leave);
                window.ShowDialog();

                LoadData();
            }

        }
        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CableNumberComboBox.ItemsSource = Data.CableDB.GetCableCheckableByCenterID(CenterComboBox.SelectedIDs);
        }

        #endregion Event Handlers

    }
}
