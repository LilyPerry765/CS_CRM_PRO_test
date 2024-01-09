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
    public partial class VerticalMDFColumnList : Local.TabWindow
    {
        #region Constructor & Fields

        public VerticalMDFColumnList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
           // MDFFrameComboBox.ItemsSource = Data.MDFFrameDB.GetMDFFrameCheckable();
            MDFFrameColumn.ItemsSource = Data.MDFFrameDB.GetMDFFrameCheckable();
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

        private void Search(object sender, RoutedEventArgs e)
        {

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int verticalCloumnNo = -1;
            if (!string.IsNullOrWhiteSpace(VerticalCloumnNoTextBox.Text.Trim())) verticalCloumnNo = Convert.ToInt32(VerticalCloumnNoTextBox.Text.Trim());

            int MDFFrame = -1;
            if (!string.IsNullOrWhiteSpace(MDFFrameTextBox.Text.Trim())) MDFFrame = Convert.ToInt32(MDFFrameTextBox.Text.Trim());
            Pager.TotalRecords = Data.VerticalMDFColumnDB.SearchVerticalMDFColumnCount
            (
            CityComboBox.SelectedIDs,
            CenterComboBox.SelectedIDs,
            MDFComboBox.SelectedIDs,
            MDFFrame,
            verticalCloumnNo
            );

            ItemsDataGrid.ItemsSource = Data.VerticalMDFColumnDB.SearchVerticalMDFColumn
            (
            CityComboBox.SelectedIDs,
            CenterComboBox.SelectedIDs,
            MDFComboBox.SelectedIDs,
            MDFFrame,
            verticalCloumnNo
            , startRowIndex
            , pageSize
            );
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("با حذف ردیف، طبقه ها و بو خت های آزاد نیز حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    VerticalMDFColumn item = ItemsDataGrid.SelectedItem as VerticalMDFColumn;

                    List<Bucht> Buchts = Data.VerticalMDFColumnDB.GetAllBuchtByColumnID(item.ID);

                    if (Buchts.Any(t => t.Status != (int)DB.BuchtStatus.Free)) { MessageBox.Show("فقط بوخت های آزاد را می توانید حذف کنید"); return; }

                    DB.Delete<Data.VerticalMDFColumn>(item.ID);
                    ShowSuccessMessage("ستون ام دی اف مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف ستون ام دی اف", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                VerticalMDFColumn item = ItemsDataGrid.SelectedItem as Data.VerticalMDFColumn;
                if (item == null) return;

                VerticalMDFColumnForm window = new VerticalMDFColumnForm(item.ID,false);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.VerticalMDFColumn item = e.Row.Item as Data.VerticalMDFColumn;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("ستون ام دی اف مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {


                e.Cancel = true;

                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("نمی توان دو ردیف هم شماره وارد کرد .خطا در ذخیره ستون ام دی اف", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره ستون ام دی اف", ex);
                }
            }
        }

        private void MultiInsert(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                VerticalMDFColumn item = ItemsDataGrid.SelectedItem as Data.VerticalMDFColumn;
                if (item == null) return;


                VerticalMDFColumnForm window = new VerticalMDFColumnForm(item.ID, true);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
            else
            {
                Folder.MessageBox.ShowInfo("برای مشخص شدن اینکه ردیف در چه فریمی اضافه شود لطفا یک ردیف انتخاب کنید.");
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
        #endregion Event Handlers

        private void RowInset(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                VerticalMDFColumn item = ItemsDataGrid.SelectedItem as Data.VerticalMDFColumn;
                if (item == null) return;

                VerticalMDFColumnForm window = new VerticalMDFColumnForm(item.ID, 1);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

     

    }
}
