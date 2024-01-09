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
    public partial class ADSLPAPPortGroupList : Local.TabWindow
    {
        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public ADSLPAPPortGroupList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ItemsBuchtDataGrid.Visibility = Visibility.Visible;

            PAPInfoComboBox.ItemsSource = PAPInfoDB.GetPAPInfoCheckable();
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckable();
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

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long rowNo = -1;
            if (!string.IsNullOrWhiteSpace(RowNoTextBox.Text))
                rowNo = Convert.ToInt64(RowNoTextBox.Text);

            long columnNo = -1;
            if (!string.IsNullOrWhiteSpace(ColumnNoTextBox.Text))
                columnNo = Convert.ToInt64(ColumnNoTextBox.Text);

            Pager.TotalRecords = Data.ADSLPAPPortDB.SearchPAPBuchtsGroupCount(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, rowNo, columnNo);
            ItemsBuchtDataGrid.ItemsSource = Data.ADSLPAPPortDB.SearchPAPBuchtsGroup(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, rowNo, columnNo, startRowIndex, pageSize);
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

                long rowNo = -1;
                if (!string.IsNullOrWhiteSpace(RowNoTextBox.Text))
                    rowNo = Convert.ToInt64(RowNoTextBox.Text);

                long columnNo = -1;
                if (!string.IsNullOrWhiteSpace(ColumnNoTextBox.Text))
                    columnNo = Convert.ToInt64(ColumnNoTextBox.Text);

                DataSet data = Data.ADSLPAPPortDB.SearchPAPBuchtsGroup(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs,rowNo,columnNo, startRowIndex, pageSize).ToDataSet("Result", ItemsBuchtDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی ");
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
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsBuchtDataGrid.Name, ItemsBuchtDataGrid.Columns);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsBuchtDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsBuchtDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        #endregion
    }
}
