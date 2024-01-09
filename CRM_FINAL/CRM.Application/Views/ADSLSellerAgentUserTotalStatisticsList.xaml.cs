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
    /// Interaction logic for ADSLSellerAgentUserTotalStatisticsList.xaml
    /// </summary>
    public partial class ADSLSellerAgentUserTotalStatisticsList : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        #region Constructor

        public ADSLSellerAgentUserTotalStatisticsList()
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
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;
                int count = 0;

                ADSLSellerAgenrUserTotalStatisticsDataGrid.ItemsSource = AdslStatisticsDB.SearchADSLSellerAgentUserTotalStatisticsInfos(
                                                                                                                                        CitiesComboBox.SelectedIDs, SellerAgentsComboBox.SelectedIDs, SellerAgentUsersComboBox.SelectedIDs,
                                                                                                                                        FromEndDatePicker.SelectedDate, ToEndDatePicker.SelectedDate, startRowIndex, pageSize, out count
                                                                                                                                       );
                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی لیست کل فروش کاربران فروش  - ADSLSellerAgentUserTotalStatisticsList");
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
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;
                int count = 0;

                DataSet data = AdslStatisticsDB.SearchADSLSellerAgentUserTotalStatisticsInfos(
                                                                                                CitiesComboBox.SelectedIDs, SellerAgentsComboBox.SelectedIDs, SellerAgentUsersComboBox.SelectedIDs,
                                                                                                FromEndDatePicker.SelectedDate, ToEndDatePicker.SelectedDate, startRowIndex, pageSize, out count
                                                                                              ).ToDataSet("Result", ADSLSellerAgenrUserTotalStatisticsDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی لیست کل فروش کاربران فروش  - ADSLSellerAgentUserTotalStatisticsList");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLSellerAgenrUserTotalStatisticsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLSellerAgenrUserTotalStatisticsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLSellerAgenrUserTotalStatisticsDataGrid.Name, ADSLSellerAgenrUserTotalStatisticsDataGrid.Columns);
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLSellerAgenrUserTotalStatisticsDataGrid.Columns);
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
            SellerAgentsComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CitiesComboBox.SelectedIDs);
        }

        private void SellerAgentsComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SellerAgentUsersComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(SellerAgentUsersComboBox.SelectedIDs);
        }

        #endregion
    }
}
