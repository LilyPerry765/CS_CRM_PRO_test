using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CRM.Data;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class ADSLPAPPortList : Local.TabWindow
    {
        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public ADSLPAPPortList()
        {
            //ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            //popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            //base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (DB.City == "semnan")
            {
                PortNoLabel.Visibility = Visibility.Visible;
                PortNoTextBox.Visibility = Visibility.Visible;
                ItemsPortDataGrid.Visibility = Visibility.Visible;
            }
            if (DB.City == "kermanshah" || DB.City == "gilan")
            {
                RowNoLabel.Visibility = Visibility.Visible;
                RowNoTextBox.Visibility = Visibility.Visible;
                ColumnNoLabel.Visibility = Visibility.Visible;
                ColumnNoTextBox.Visibility = Visibility.Visible;
                BuchtNoLabel.Visibility = Visibility.Visible;
                BuchtNoTextBox.Visibility = Visibility.Visible;
                ItemsBuchtDataGrid.Visibility = Visibility.Visible;
            }

            PAPInfoComboBox.ItemsSource = PAPInfoDB.GetPAPInfoCheckable();
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPAPPortStatus));
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
            PAPInfoComboBox.Reset();
            CentersComboBox.Reset();
            TelephoneNoTextBox.Text = string.Empty;
            PortNoTextBox.Text = string.Empty;
            FromDateDate.SelectedDate = null;
            RowNoTextBox.Text = string.Empty;
            ColumnNoTextBox.Text = string.Empty;
            BuchtNoTextBox.Text = string.Empty;
            ToDateDate.SelectedDate = null;
            StatusComboBox.Reset();

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            if (DB.City == "semnan")
            {
                long portNo = -1;
                if (!string.IsNullOrWhiteSpace(PortNoTextBox.Text))
                    portNo = Convert.ToInt64(PortNoTextBox.Text);

                DateTime? toDate = null;
                if (ToDateDate.SelectedDate.HasValue)
                    toDate = ToDateDate.SelectedDate.Value.AddDays(1);

                Pager.TotalRecords = Data.ADSLPAPPortDB.SearchPAPPortsCount(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, portNo, telephoneNo, StatusComboBox.SelectedIDs, FromDateDate.SelectedDate, toDate);
                ItemsPortDataGrid.ItemsSource = Data.ADSLPAPPortDB.SearchPAPPorts(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, portNo, telephoneNo, StatusComboBox.SelectedIDs, FromDateDate.SelectedDate, toDate, startRowIndex, pageSize);
            }
            if (DB.City == "kermanshah" || DB.City == "gilan")
            {
                long rowNo = -1;
                if (!string.IsNullOrWhiteSpace(RowNoTextBox.Text))
                    rowNo = Convert.ToInt64(RowNoTextBox.Text);

                long columnNo = -1;
                if (!string.IsNullOrWhiteSpace(ColumnNoTextBox.Text))
                    columnNo = Convert.ToInt64(ColumnNoTextBox.Text);

                long buchtNo = -1;
                if (!string.IsNullOrWhiteSpace(BuchtNoTextBox.Text))
                    buchtNo = Convert.ToInt64(BuchtNoTextBox.Text);

                DateTime? toDate = null;
                if (ToDateDate.SelectedDate.HasValue)
                    toDate = ToDateDate.SelectedDate.Value.AddDays(1);

                Pager.TotalRecords = Data.ADSLPAPPortDB.SearchPAPBuchtsCount(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, rowNo, columnNo, buchtNo, telephoneNo, StatusComboBox.SelectedIDs, FromDateDate.SelectedDate, toDate);
                ItemsBuchtDataGrid.ItemsSource = Data.ADSLPAPPortDB.SearchPAPBuchts(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, rowNo, columnNo, buchtNo, telephoneNo, StatusComboBox.SelectedIDs, FromDateDate.SelectedDate, toDate, startRowIndex, pageSize);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLPAPPortForm window = new ADSLPAPPortForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsPortDataGrid.SelectedIndex >= 0 || ItemsBuchtDataGrid.SelectedIndex >= 0)
            {
                PAPPortInfo item = null;

                if (DB.City == "semnan")
                    item = ItemsPortDataGrid.SelectedItem as PAPPortInfo;
                if (DB.City == "kermanshah" || DB.City == "gilan")
                    item = ItemsBuchtDataGrid.SelectedItem as PAPPortInfo;

                if (item == null) return;

                ADSLPAPPortForm window = new ADSLPAPPortForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsPortDataGrid.SelectedIndex < 0 || ItemsPortDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            if (ItemsBuchtDataGrid.SelectedIndex < 0 || ItemsBuchtDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PAPPortInfo item = null;
                    if (DB.City == "semnan")
                        item = ItemsPortDataGrid.SelectedItem as PAPPortInfo;
                    if (DB.City == "kermanshah" || DB.City == "gilan")
                        item = ItemsBuchtDataGrid.SelectedItem as PAPPortInfo;

                    DB.Delete<Data.ADSLPAPPort>(item.ID);
                    ShowSuccessMessage("پورت مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف استان", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;
                
                long telephoneNo = -1;
                if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                    telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);
                
                DateTime? toDate = null;
                if (ToDateDate.SelectedDate.HasValue)
                    toDate = ToDateDate.SelectedDate.Value.AddDays(1);

                if (DB.City == "semnan")
                {
                    long portNo = -1;
                    if (!string.IsNullOrWhiteSpace(PortNoTextBox.Text))
                        portNo = Convert.ToInt64(PortNoTextBox.Text);

                    DataSet data = Data.ADSLPAPPortDB.SearchPAPPorts(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, portNo, telephoneNo, StatusComboBox.SelectedIDs,FromDateDate.SelectedDate,toDate, startRowIndex, pageSize).ToDataSet("Result", ItemsPortDataGrid);
                    Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
                }
                if (DB.City == "kermanshah" || DB.City == "gilan")
                {
                    long rowNo = -1;
                    if (!string.IsNullOrWhiteSpace(RowNoTextBox.Text))
                        rowNo = Convert.ToInt64(RowNoTextBox.Text);

                    long columnNo = -1;
                    if (!string.IsNullOrWhiteSpace(ColumnNoTextBox.Text))
                        columnNo = Convert.ToInt64(ColumnNoTextBox.Text);

                    long buchtNo = -1;
                    if (!string.IsNullOrWhiteSpace(BuchtNoTextBox.Text))
                        buchtNo = Convert.ToInt64(BuchtNoTextBox.Text);

                    DataSet data = Data.ADSLPAPPortDB.SearchPAPBuchts(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, rowNo, columnNo, buchtNo, telephoneNo, StatusComboBox.SelectedIDs, FromDateDate.SelectedDate, toDate, startRowIndex, pageSize).ToDataSet("Result", ItemsBuchtDataGrid);
                    Print.DynamicPrintV2(data,_title, dataGridSelectedIndexs, _groupingColumn);
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی آمار سرویس ای دی اس ال");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsBuchtDataGrid.Columns);
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

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            if (DB.City == "semnan")
                CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsPortDataGrid.Name, ItemsPortDataGrid.Columns);
            if (DB.City == "kermanshah" || DB.City == "gilan")
                CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsBuchtDataGrid.Name, ItemsBuchtDataGrid.Columns);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                if (DB.City == "semnan")
                {
                    DataGridColumn dataGridColumn = ItemsPortDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                    dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
                }
                if (DB.City == "kermanshah" || DB.City == "gilan")
                {
                    DataGridColumn dataGridColumn = ItemsBuchtDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                    dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
                }
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                if (DB.City == "semnan")
                {
                    DataGridColumn dataGridColumn = ItemsPortDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                    dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
                }
                if (DB.City == "kermanshah" || DB.City == "gilan")
                {
                    DataGridColumn dataGridColumn = ItemsBuchtDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                    dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
                }
            }
        }

        #endregion
    }
}
