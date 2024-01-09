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
    /// Interaction logic for ADSLPortsStatisticsList.xaml
    /// </summary>
    public partial class ADSLPortsStatisticsList : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        #region Constructor
        public ADSLPortsStatisticsList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods
        public void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                if (CentersComboBox.SelectedIDs.Count != 0)
                {
                    int count = 0;
                    int startRowIndex = Pager.StartRowIndex;
                    int pageSize = 10;

                    ADSLPortsStatisticsDataGrid.ItemsSource = AdslStatisticsDB.SearchAdslPortStatisticsInfo(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, startRowIndex, pageSize, out count);

                    Pager.TotalRecords = count;
                }
                else
                {
                    MessageBox.Show(".تعیین مراکز الزامی میباشد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی لیست پورت های شرکت های مخابرات");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = SearchExpander as UIElement;
            Helper.ResetSearch(container);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                if (CentersComboBox.SelectedIDs.Count != 0)
                {
                    int count = 0;
                    int startRowIndex = Pager.StartRowIndex;
                    int pageSize = Pager.TotalRecords;

                    DataSet data = AdslStatisticsDB.SearchAdslPortStatisticsInfo(
                                                                                 CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
                                                                                 FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                                 startRowIndex, pageSize, out count
                                                                                 )
                                                    .ToDataSet("Result", ADSLPortsStatisticsDataGrid);
                    Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
                }
                else
                {
                    MessageBox.Show(".تعیین مراکز الزامی میباشد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی لیست پورت های شرکت های مخابرات");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLPortsStatisticsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLPortsStatisticsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLPortsStatisticsDataGrid.Name, ADSLPortsStatisticsDataGrid.Columns);
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLPortsStatisticsDataGrid.Columns);
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
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
