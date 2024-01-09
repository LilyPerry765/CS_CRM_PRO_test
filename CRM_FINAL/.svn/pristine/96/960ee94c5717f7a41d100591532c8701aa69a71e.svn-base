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
    public partial class MDFList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public MDFList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.MDFType));
            TypeColmn.ItemsSource = Helper.GetEnumItem(typeof(DB.MDFType));
            UsesColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.MDFUses));
            UsesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.MDFUses));
           // BuchtTypeColumn.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

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

            int lastNoHorizontalFrames = -1;
            if (!string.IsNullOrWhiteSpace(LastNoHorizontalFramesTextBox.Text))
                lastNoHorizontalFrames = Convert.ToInt32(LastNoHorizontalFramesTextBox.Text);

            int LastNoVerticalFrames = -1;
            if (!string.IsNullOrWhiteSpace(LastNoVerticalFramesTextBox.Text))
                LastNoVerticalFrames = Convert.ToInt32(LastNoVerticalFramesTextBox.Text);
            //Pager.TotalRecords = Data.MDFDB.SearchMDFCount(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, lastNoHorizontalFrames, LastNoVerticalFrames, TypeComboBox.SelectedIDs);
            ItemsDataGrid.ItemsSource = Data.MDFDB.SearchMDF(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, lastNoHorizontalFrames, LastNoVerticalFrames, TypeComboBox.SelectedIDs, startRowIndex, pageSize,out count);
            Pager.TotalRecords = count;
            this.Cursor = Cursors.Arrow;
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            MDFForm window = new MDFForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                MDF item = ItemsDataGrid.SelectedItem as Data.MDF;
                if (item == null) return;

                MDFForm window = new MDFForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MDF item = ItemsDataGrid.SelectedItem as MDF;

                    DB.Delete<Data.MDF>(item.ID);
                    ShowSuccessMessage("MDF مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف MDF", ex);
            }
        }
        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }
        private void CenterComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }
        #endregion




    }
}
