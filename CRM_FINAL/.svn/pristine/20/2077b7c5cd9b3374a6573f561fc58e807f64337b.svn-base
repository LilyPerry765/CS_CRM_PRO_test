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
    public partial class MDFFrameList : Local.TabWindow
    {
        #region Constructor & Fields

        public MDFFrameList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckable();
            MDFColumn.ItemsSource = Data.MDFDB.GetMDFCheckable();
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
        private void AddItem(object sender, RoutedEventArgs e)
        {
            MDFFrameForm window = new MDFFrameForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int verticalRowsCount = -1;
            if (!string.IsNullOrWhiteSpace(VerticalRowsCountTextBox.Text)) verticalRowsCount = Convert.ToInt32(VerticalRowsCountTextBox.Text);

            int verticalRowCapacity = -1;
            if (!string.IsNullOrWhiteSpace(VerticalRowCapacityTextBox.Text)) verticalRowCapacity = Convert.ToInt32(VerticalRowCapacityTextBox.Text);

            int verticalFirstColumn = -1;
            if (!string.IsNullOrWhiteSpace(VerticalFirstColumnTextBox.Text)) verticalFirstColumn = Convert.ToInt32(VerticalFirstColumnTextBox.Text);

            int verticalLastColumn = -1;
            if (!string.IsNullOrWhiteSpace(VerticalLastColumnTextBox.Text)) verticalLastColumn = Convert.ToInt32(VerticalLastColumnTextBox.Text);

            Pager.TotalRecords = Data.MDFFrameDB.SearchMDFFrameCount(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, FrameNoComboBox.SelectedIDs, verticalRowsCount, verticalRowCapacity, verticalFirstColumn, verticalLastColumn);
            ItemsDataGrid.ItemsSource = Data.MDFFrameDB.SearchMDFFrame(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, FrameNoComboBox.SelectedIDs, verticalRowsCount, verticalRowCapacity, verticalFirstColumn, verticalLastColumn, startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {

                MessageBoxResult result = MessageBox.Show("با حذف فریم ردیف و طبقه و بو خت های آزاد نیز حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
               

                if (result == MessageBoxResult.Yes)
                {
                    MDFFrame item = ItemsDataGrid.SelectedItem as MDFFrame;
                    List<Bucht> Buchts = Data.MDFFrameDB.GetAllBuchtByFramID(item.ID);

                    if (Buchts.Any(t => t.Status != (int)DB.BuchtStatus.Free)) { MessageBox.Show("فقط بوخت های آزاد را می توانید حذف کنید"); return; }

                    DB.Delete<Data.MDFFrame>(item.ID);
                    ShowSuccessMessage("فریم ام دی اف مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف فریم ام دی اف", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                MDFFrame item = ItemsDataGrid.SelectedItem as Data.MDFFrame;
                if (item == null) return;

                MDFFrameForm window = new MDFFrameForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.MDFFrame item = e.Row.Item as Data.MDFFrame;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("فریم ام دی اف مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره فریم ام دی اف", ex);
            }
        }

        private void AddBuchtItem(object sender, RoutedEventArgs e)
        {

            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                MDFFrame item = ItemsDataGrid.SelectedItem as Data.MDFFrame;
                if (item == null) return;

                BuchtForm window = new BuchtForm(item.ID);
                window.ShowDialog();
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
        private void MDFComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            FrameNoComboBox.ItemsSource = Data.MDFFrameDB.GetMDFFrameCheckableByMDFIDs(MDFComboBox.SelectedIDs);
        }
        #endregion Event Handlers
    }
}
