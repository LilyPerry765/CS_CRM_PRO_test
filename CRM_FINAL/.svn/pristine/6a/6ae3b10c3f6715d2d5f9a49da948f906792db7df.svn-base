using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class AdameEmkanatList : Local.TabWindow
    {
        #region Constructor & Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();


        public AdameEmkanatList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();

        }

        public void LoadData()
        {

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
            int count = 0;
            ItemsDataGrid.ItemsSource = Data.AdameEmkanatDB.SearchAdameEmkanat(CenterComboBox.SelectedIDs, FISH_NOTextBox.Text.Trim(), KAFOTextBox.Text.Trim(), POSTTextBox.Text.Trim(), TYPE_POSTTextBox.Text.Trim(), ELATTextBox.Text.Trim(), TA_SABTTextBox.Text.Trim(), TA_BARETextBox.Text.Trim(), NAMETextBox.Text.Trim(), ADRTextBox.Text.Trim(), TOZEHTextBox.Text.Trim(), startRowIndex, pageSize, out count);
            Pager.TotalRecords = count;
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
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.AdameEmkanat item = ItemsDataGrid.SelectedItem as CRM.Data.AdameEmkanat;

                    DB.Delete<Data.AdameEmkanat>(item.ID);
                    ShowSuccessMessage("عدم امکانات مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف عدم امکانات", ex);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.AdameEmkanat item = e.Row.Item as Data.AdameEmkanat;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("عدم امکانات مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره عدم امکانات", ex);
            }
        }
        #endregion Event Handlers

        #region print

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;
            int count = 0;
            DataSet data = Data.AdameEmkanatDB.SearchAdameEmkanat(CenterComboBox.SelectedIDs, FISH_NOTextBox.Text.Trim(), KAFOTextBox.Text.Trim(), POSTTextBox.Text.Trim(), TYPE_POSTTextBox.Text.Trim(), ELATTextBox.Text.Trim(), TA_SABTTextBox.Text.Trim(), TA_BARETextBox.Text.Trim(), NAMETextBox.Text.Trim(), ADRTextBox.Text.Trim(), TOZEHTextBox.Text.Trim(), startRowIndex, pageSize, out count)
                .ToDataSet("Result", ItemsDataGrid);
            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            this.Cursor = Cursors.Arrow;
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _sumColumn = reportSettingForm._sumCheckedList;
            _title = reportSettingForm._title;

        }
        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }


        #endregion
    }
}
