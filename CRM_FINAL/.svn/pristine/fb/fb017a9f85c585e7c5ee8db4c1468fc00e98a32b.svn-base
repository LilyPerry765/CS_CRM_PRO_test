using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Data;
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
    /// Interaction logic for SwitchPrecodeList.xaml
    /// </summary>
    public partial class SwitchPrecodeList : TabWindow
    {

        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public SwitchPrecodeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesCheckableComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            SwitchTypesCheckableComboBox.ItemsSource = SwitchTypeDB.GetSwitchCheckable();
        }

        #endregion

        #region EventHandlers

        private void CitiesCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersCheckableComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesCheckableComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;

            long switchPreNo = (!string.IsNullOrEmpty(SwitchPreNoTextBox.Text.Trim())) ? Convert.ToInt64(SwitchPreNoTextBox.Text.Trim()) : -1;
            long fromNumber = (!string.IsNullOrEmpty(FromNumberTextBox.Text.Trim())) ? Convert.ToInt64(FromNumberTextBox.Text.Trim()) : -1;
            long toNumber = (!string.IsNullOrEmpty(ToNumberTextBox.Text.Trim())) ? Convert.ToInt64(ToNumberTextBox.Text.Trim()) : -1;

            List<SwitchPrecodeInfo> result = SwitchPrecodeDB.GetSwitchPrecodes(
                                                                               CitiesCheckableComboBox.SelectedIDs, CentersCheckableComboBox.SelectedIDs, SwitchTypesCheckableComboBox.SelectedIDs,
                                                                               switchPreNo, fromNumber, toNumber, false, startRowIndex, pageSize, out count
                                                                               );

            if (result.Count != 0)
            {
                ItemsDataGrid.ItemsSource = result;
                Pager.TotalRecords = count;
            }
            else
            {
                ItemsDataGrid.ItemsSource = result;
                MessageBox.Show(".رکوردی یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;
            int count = 0;

            long switchPreNo = (!string.IsNullOrEmpty(SwitchPreNoTextBox.Text.Trim())) ? Convert.ToInt64(SwitchPreNoTextBox.Text.Trim()) : -1;
            long fromNumber = (!string.IsNullOrEmpty(FromNumberTextBox.Text.Trim())) ? Convert.ToInt64(FromNumberTextBox.Text.Trim()) : -1;
            long toNumber = (!string.IsNullOrEmpty(ToNumberTextBox.Text.Trim())) ? Convert.ToInt64(ToNumberTextBox.Text.Trim()) : -1;

            List<SwitchPrecodeInfo> result = SwitchPrecodeDB.GetSwitchPrecodes(
                                                                                CitiesCheckableComboBox.SelectedIDs, CentersCheckableComboBox.SelectedIDs, SwitchTypesCheckableComboBox.SelectedIDs,
                                                                                switchPreNo, fromNumber, toNumber, true, startRowIndex, pageSize, out count
                                                                               );

            if (result.Count != 0)
            {
                DataSet data = result.ToDataSet("Result", ItemsDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            else
            {
                MessageBox.Show(".رکوردی یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

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
