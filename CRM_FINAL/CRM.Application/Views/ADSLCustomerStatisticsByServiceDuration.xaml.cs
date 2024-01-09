using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace CRM.Application.Views
{
    public partial class ADSLCustomerStatisticsByServiceDuration : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        public ADSLCustomerStatisticsByServiceDuration()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            CitiesComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PersonTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType)).Where(ch => ch.ID <= 1).ToList(); // حقیقی یا حقوقی
            AdslCustomerGroupsComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            //SaleWaiesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleWays));
            AdslServiceTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
            AdslServiceGroupsComboBox.ItemsSource = ADSLServiceGroupDB.GetADSLServiceGroupCheckable();
            BandWidthComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            DurationComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceDurationCheckable();
            TrafficComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceTrafficCheckable();
            AdslServicesComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableNew();
            PaymentTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            CustomerTypesComboBox.ItemsSource = ADSLCustomerTypeDB.GetADSLCustomerTypesCheckable();
        }

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;

                var result = AdslStatisticsDB.SearchADSLCustomerStatisticsByServiceDuration(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, PersonTypesComboBox.SelectedIDs,
                                                                                             AdslCustomerGroupsComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs, AdslServiceGroupsComboBox.SelectedIDs,
                                                                                             BandWidthComboBox.SelectedIDs, TrafficComboBox.SelectedIDs, DurationComboBox.SelectedIDs, AdslServicesComboBox.SelectedIDs,
                                                                                             PaymentTypesComboBox.SelectedIDs,  ServiceCodeTextBox.Text.Trim(), FromDate.SelectedDate,ToDate.SelectedDate,                                                                                            
                                                                                             startRowIndex, pageSize);
                ADSLCustomerStatisticsByServiceDurationDataGrid.ItemsSource = result;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی گزارش نسبت مدت زمان قرارداد مشتریان به تعداد مشتریان  - ADSLCustomerStatisticsByServiceDurationDataGrid");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = SearchExpander as UIElement;
            Helper.ResetSearch(container);

            Search(null, null);
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

                    DataSet data = AdslStatisticsDB.SearchADSLCustomerStatisticsByServiceDuration(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, PersonTypesComboBox.SelectedIDs,
                                                                                             AdslCustomerGroupsComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs, AdslServiceGroupsComboBox.SelectedIDs,
                                                                                             BandWidthComboBox.SelectedIDs, TrafficComboBox.SelectedIDs, DurationComboBox.SelectedIDs, AdslServicesComboBox.SelectedIDs,
                                                                                             PaymentTypesComboBox.SelectedIDs, ServiceCodeTextBox.Text.Trim(), FromDate.SelectedDate, ToDate.SelectedDate,
                                                                                             startRowIndex, pageSize)
                                                    .ToDataSet("Result", ADSLCustomerStatisticsByServiceDurationDataGrid);
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

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLCustomerStatisticsByServiceDurationDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLCustomerStatisticsByServiceDurationDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLCustomerStatisticsByServiceDurationDataGrid.Name, ADSLCustomerStatisticsByServiceDurationDataGrid.Columns);
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLCustomerStatisticsByServiceDurationDataGrid.Columns);
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
    }
}
