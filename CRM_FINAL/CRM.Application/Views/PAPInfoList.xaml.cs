using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Collections.ObjectModel;
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    public partial class PAPInfoList : Local.TabWindow
    {
        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public PAPInfoList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            OperatingStatusColumn.ItemsSource = DB.GetAllEntity<Data.PAPInfoOperatingStatus>();
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
            TitleTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {            
            ItemsDataGrid.ItemsSource = PAPInfoDB.SearchPAPInfo(TitleTextBox.Text.Trim(), AddressTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PAPInfoForm window = new PAPInfoForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPInfo item = ItemsDataGrid.SelectedItem as Data.PAPInfo;
                if (item == null) return;

                PAPInfoForm window = new PAPInfoForm(item.ID);
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
                    PAPInfo item = ItemsDataGrid.SelectedItem as PAPInfo;

                    DB.Delete<PAPInfo>(item.ID);

                    ShowSuccessMessage("کاربر شرکت PAP مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کاربر شرکت PAP", ex);
            }
        }

        private void ShowPAPUser(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPInfo item = ItemsDataGrid.SelectedItem as Data.PAPInfo;
                if (item == null) return;

                PAPInfoUserList window = new PAPInfoUserList(item.ID);
                Folder.Console.Navigate(window, "کاربران شرکت PAP");
            }
        }

        private void ShowSpaceandPowerInfo(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPInfo item = ItemsDataGrid.SelectedItem as Data.PAPInfo;
                if (item == null) return;

                PAPInfoSpaceandPowerList window = new PAPInfoSpaceandPowerList(item.ID);
                Folder.Console.Navigate(window, "فضا و پاور شرکت PAP");

                //PAPInfo item = ItemsDataGrid.SelectedItem as Data.PAPInfo;
                //if (item == null) return;

                //PAPInfoSpaceandPowerForm window = new PAPInfoSpaceandPowerForm(item.ID);
                //window.ShowDialog();

                //if (window.DialogResult == true)
                //    LoadData();
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //if (dataGridSelectedIndexs.Count > 0)
            //{
                DataSet data = PAPInfoDB.SearchPAPInfo(TitleTextBox.Text.Trim(), AddressTextBox.Text.Trim()).ToDataSet("Result", ItemsDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            //}
            //else
            //{
            //    MessageBox.Show(".تعیین ستونهای مورد نظر برای ایجاد گزارش ضروی میباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
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
