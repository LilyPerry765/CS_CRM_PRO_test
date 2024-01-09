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
    public partial class VerticalMDFRowList : Local.TabWindow
    {
        #region Constructor & Fields

        public VerticalMDFRowList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            //VerticalMDFColumnColumn.ItemsSource = Data.VerticalMDFColumnDB.GetVerticalMDFColumnCheckable();

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
            int count = default(int);

            int verticalRowNo = -1;
            if (!string.IsNullOrWhiteSpace(VerticalRowNoTextBox.Text))
            {
                verticalRowNo = Convert.ToInt32(VerticalRowNoTextBox.Text);
            }

            int rowCapacity = -1;
            if (!string.IsNullOrWhiteSpace(RowCapacityTextBox.Text))
            {
                rowCapacity = Convert.ToInt32(RowCapacityTextBox.Text);
            }

            int verticalMDFColumn = -1;
            if (!string.IsNullOrWhiteSpace(VerticalMDFColumnTextBox.Text.Trim()))
            {
                verticalMDFColumn = Convert.ToInt32(VerticalMDFColumnTextBox.Text.Trim());
            }

            int mdfFrameNo = -1;
            if (!string.IsNullOrEmpty(MDFFrameNoNumericTextBox.Text.Trim()))
            {
                mdfFrameNo = Convert.ToInt32(MDFFrameNoNumericTextBox.Text.Trim());
            }

            //Pager.TotalRecords = Data.VerticalMDFRowDB.SearchVerticalMDFRowCount(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, verticalMDFColumn, verticalRowNo, rowCapacity);
            ItemsDataGrid.ItemsSource = Data.VerticalMDFRowDB.SearchVerticalMDFRow(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, mdfFrameNo, verticalMDFColumn, verticalRowNo, rowCapacity, startRowIndex, pageSize, out count);
            Pager.TotalRecords = count;

            this.Cursor = Cursors.Arrow;
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("با حذف طبقه، بو خت های آزاد نیز حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    VerticalMDFRowInfo item = ItemsDataGrid.SelectedItem as VerticalMDFRowInfo;
                    List<Bucht> Buchts = Data.VerticalMDFRowDB.GetAllBuchtByRowID(item.ID);

                    if (Buchts.Any(t => t.Status != (int)DB.BuchtStatus.Free)) { MessageBox.Show("فقط بوخت های آزاد را می توانید حذف کنید"); return; }
                    DB.Delete<Data.VerticalMDFRow>(item.ID);
                    ShowSuccessMessage("سطر ام دی اف مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف سطر ام دی اف", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                VerticalMDFRowInfo item = ItemsDataGrid.SelectedItem as Data.VerticalMDFRowInfo;
                if (item == null) return;

                VerticalMDFRowForm window = new VerticalMDFRowForm(item.ID, false);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.VerticalMDFRow item = e.Row.Item as Data.VerticalMDFRow;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("سطر ام دی اف مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره سطر ام دی اف", ex);
            }
        }

        private void MultiInsert(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                VerticalMDFRowInfo item = ItemsDataGrid.SelectedItem as Data.VerticalMDFRowInfo;
                if (item == null) return;

                VerticalMDFRowForm window = new VerticalMDFRowForm(item.ID, true);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
            else
            {
                Folder.MessageBox.ShowInfo("لطفا ردیفی را که می خواهید در آن طبقه اضافه کنید را انتخاب کنید");
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
