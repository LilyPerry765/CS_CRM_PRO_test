using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// لیست پی سی ام های جمع آوری شده
    /// </summary>
    public partial class DroppedPCMList : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor
        public DroppedPCMList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        private void Pager_PageChanged(int pageNumber)
        {
            SearchButton_Click(null, null);
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = DroppedPcmDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = DroppedPcmDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, DroppedPcmDataGrid.Name, DroppedPcmDataGrid.Columns);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 50;

                DroppedPcmDataGrid.ItemsSource = ActionLogDB.SearchDropedPcm(CentersComboBox.SelectedIDs, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, startRowIndex, pageSize, out count);

                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی لیست پی سی ام های جمع آوری شده");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            FromDatePicker.SelectedDate = null;
            ToDatePicker.SelectedDate = null;
            CitiesComboBox.Reset();
            CentersComboBox.Reset();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 50;

                DataSet data = ActionLogDB.SearchDropedPcm(CentersComboBox.SelectedIDs, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, startRowIndex, pageSize, out count).ToDataSet("Result", DroppedPcmDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی لیست پی سی ام های جمع آوری شده");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(DroppedPcmDataGrid.Columns);
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

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion
    }
}
