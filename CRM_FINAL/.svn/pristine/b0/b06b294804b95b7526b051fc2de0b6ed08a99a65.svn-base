using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Application.Views;
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
using System.Windows.Shapes;

namespace CRM.Application.Views
{    
    public partial class ADSLServiceBandWidthRMSBasedList : TabWindow
    {
        #region Proeprties And Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        #region Constructor
        public ADSLServiceBandWidthRMSBasedList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;

                ADSLServiceBandWidthRMDBasedDataGrid.ItemsSource = AdslStatisticsDB.SearchADSlServiceBandWidthInfos(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
                                                                                                                    BandWidthesComboBox.SelectedIDs, ToDatePicker.SelectedDate,
                                                                                                                    startRowIndex, pageSize, out count);

                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی RMS");
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
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords; ;

                DataSet result = AdslStatisticsDB.SearchADSlServiceBandWidthInfos(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
                                                                                                                    BandWidthesComboBox.SelectedIDs, ToDatePicker.SelectedDate,
                                                                                                                    startRowIndex, pageSize, out count).ToDataSet("Result", ADSLServiceBandWidthRMDBasedDataGrid);
                Print.DynamicPrintV2(result, string.Empty, dataGridSelectedIndexs, _groupingColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی RMS");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLServiceBandWidthRMDBasedDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLServiceBandWidthRMDBasedDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLServiceBandWidthRMDBasedDataGrid.Name, ADSLServiceBandWidthRMDBasedDataGrid.Columns);
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLServiceBandWidthRMDBasedDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm.ShowDialog();
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            BandWidthesComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();

            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion
    }
}
