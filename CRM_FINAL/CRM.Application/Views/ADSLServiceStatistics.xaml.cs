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
    public partial class ADSLServiceStatistics : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor
        public ADSLServiceStatistics()
        {
            InitializeComponent();
            Initialize();
            AdslServiceTypesComboBox.LostFocus += AdslServiceTypesComboBox_LostFocus;
            AdslServiceGroupsComboBox.LostFocus += AdslServiceGroupsComboBox_LostFocus;
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

        private void AdslServiceGroupsComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BandWidthComboBox.Reset();
            DurationComboBox.Reset();
            TrafficComboBox.Reset();
            AdslServicesComboBox.Reset();

            List<int> groupsId = new List<int>();
            if (AdslServiceGroupsComboBox.SelectedIDs.Count != 0)
            {
                groupsId = AdslServiceGroupsComboBox.SelectedIDs;
            }

            List<int> typesId = new List<int>();
            if (AdslServiceTypesComboBox.SelectedIDs.Count != 0)
            {
                typesId = AdslServiceTypesComboBox.SelectedIDs;
            }


        }

        private void AdslServiceTypesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            List<int> typesId = new List<int>();
            if (AdslServiceTypesComboBox.SelectedIDs.Count != 0)
            {
                typesId = AdslServiceTypesComboBox.SelectedIDs;
            }

            List<int> groupsId = new List<int>();
            if (AdslServiceGroupsComboBox.SelectedIDs.Count != 0)
            {
                groupsId = AdslServiceGroupsComboBox.SelectedIDs;
            }

            List<int> bandWidthsId = new List<int>();
            if (BandWidthComboBox.SelectedIDs.Count != 0)
            {
                bandWidthsId = BandWidthComboBox.SelectedIDs;
            }

            List<int> durationsId = new List<int>();
            if (DurationComboBox.SelectedIDs.Count != 0)
            {
                durationsId = DurationComboBox.SelectedIDs;
            }

            List<int> trafficsId = new List<int>();
            if (TrafficComboBox.SelectedIDs.Count != 0)
            {
                trafficsId = TrafficComboBox.SelectedIDs;
            }

            List<int> customerGroupsId = new List<int>();
            if (AdslCustomerGroupsComboBox.SelectedIDs.Count != 0)
            {
                customerGroupsId = AdslCustomerGroupsComboBox.SelectedIDs;
            }

            AdslServicesComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByPropertiesIds(customerGroupsId, typesId, groupsId, bandWidthsId, trafficsId, durationsId);
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLStatisticsDataGrid.Name, ADSLStatisticsDataGrid.Columns);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {

                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;

                ADSLStatisticsDataGrid.ItemsSource = AdslStatisticsDB.SearchADSLStatisticsHasGroupBy(
                                                                                  CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
                                                                                  PersonTypesComboBox.SelectedIDs, AdslCustomerGroupsComboBox.SelectedIDs,
                                                                                  SaleWaiesComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs,
                                                                                  AdslServiceGroupsComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs,
                                                                                  DurationComboBox.SelectedIDs, TrafficComboBox.SelectedIDs,
                                                                                  AdslServicesComboBox.SelectedIDs, PaymentTypesComboBox.SelectedIDs,
                                                                                  FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                                  startRowIndex, pageSize, out count
                                                                                 );

                Pager.TotalRecords = count;

            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی آمار سرویس ای دی اس ال");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var child in Helper.FindVisualChildren<CRM.Application.UserControls.CheckableComboBox>(this))
            {
                child.Reset();
            }
            ServiceCodeTextBox.Clear();
            FromDatePicker.SelectedDate = null;
            ToDatePicker.SelectedDate = null;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {

                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;

                DataSet data = AdslStatisticsDB.SearchADSLStatisticsHasGroupBy(
                                                                              CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
                                                                              PersonTypesComboBox.SelectedIDs, AdslCustomerGroupsComboBox.SelectedIDs,
                                                                              SaleWaiesComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs,
                                                                              AdslServiceGroupsComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs,
                                                                              DurationComboBox.SelectedIDs, TrafficComboBox.SelectedIDs,
                                                                              AdslServicesComboBox.SelectedIDs, PaymentTypesComboBox.SelectedIDs,
                                                                              FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                              startRowIndex, pageSize, out count
                                                                             ).ToDataSet("Result", ADSLStatisticsDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);


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
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLStatisticsDataGrid.Columns);
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
            SearchButton_Click(null, null);
        }

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLStatisticsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLStatisticsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        #endregion
    }
}
