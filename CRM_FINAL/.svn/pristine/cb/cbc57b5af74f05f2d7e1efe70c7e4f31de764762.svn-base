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
    public partial class ADSLRequestTimeList : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>(); 
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public ADSLRequestTimeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CitiesComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PersonTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType)).Where(ch => ch.ID <= 1).ToList(); // حقیقی یا حقوقی
            AdslCustomerGroupsComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            SaleWaiesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleWays));
            AdslServiceTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
            AdslServiceGroupsComboBox.ItemsSource = ADSLServiceGroupDB.GetADSLServiceGroupCheckable();
            BandWidthComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            DurationComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceDurationCheckable();
            TrafficComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceTrafficCheckable();
            AdslServicesComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableNew();
            PaymentTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));            
        }

        #endregion

        #region EventHandlers

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;

                ADSLRequestTimelDataGrid.ItemsSource = AdslStatisticsDB.SearchADSLRequestTime(
                                                                                             CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, PersonTypesComboBox.SelectedIDs,
                                                                                             AdslCustomerGroupsComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs, AdslServiceGroupsComboBox.SelectedIDs,
                                                                                             BandWidthComboBox.SelectedIDs, TrafficComboBox.SelectedIDs, DurationComboBox.SelectedIDs, AdslServicesComboBox.SelectedIDs,
                                                                                             PaymentTypesComboBox.SelectedIDs,FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                                             startRowIndex, pageSize, out count
                                                                                             );

                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی فروش سرویس ای دی اس ال  - ADSLServiceSellList");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;

                DataSet data = AdslStatisticsDB.SearchADSLRequestTime(
                                                                      CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, PersonTypesComboBox.SelectedIDs,
                                                                      AdslCustomerGroupsComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs, AdslServiceGroupsComboBox.SelectedIDs,
                                                                      BandWidthComboBox.SelectedIDs, TrafficComboBox.SelectedIDs, DurationComboBox.SelectedIDs, AdslServicesComboBox.SelectedIDs,
                                                                      PaymentTypesComboBox.SelectedIDs, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                      startRowIndex, pageSize, out count
                                                                      ).ToDataSet("Result", ADSLRequestTimelDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی فروش سرویس ای دی اس ال  - ADSLServiceSellList");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLRequestTimelDataGrid.Columns);
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

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLRequestTimelDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLRequestTimelDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLRequestTimelDataGrid.Name, ADSLRequestTimelDataGrid.Columns);
        }

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        #endregion
    }
}
