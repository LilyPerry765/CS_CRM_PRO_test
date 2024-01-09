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
    /// Interaction logic for ADSLServiceCollectionTimeAverageList.xaml
    /// </summary>
    public partial class ADSLServiceCollectionTimeAverageList : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        public ADSLServiceCollectionTimeAverageList()
        {
            InitializeComponent();
            Initialize();
        }

        #region Methods
        public void Initialize()
        {
            AdslCustomerTypesComboBox.ItemsSource = ADSLCustomerTypeDB.GetADSLCustomerTypesCheckable();
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

            //SellTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType)); 
            //ADSLChangeServiceType

        }

        #endregion

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;

                int fromDaysCount = !string.IsNullOrEmpty(FromDayCountTextBox.Text.Trim()) ? int.Parse(FromDayCountTextBox.Text) : 0;
                int toDaysCount = !string.IsNullOrEmpty(ToDayCountTextBox.Text.Trim()) ? int.Parse(ToDayCountTextBox.Text) : 0;
                ADSLServiceCollectionTimeAverageDataGrid.ItemsSource = AdslStatisticsDB.SearchADSLServiceCollectionTimeAverage(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, PersonTypesComboBox.SelectedIDs,
                                                                                                                               fromDaysCount, toDaysCount, AdslCustomerTypesComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs,
                                                                                                                               AdslServiceGroupsComboBox.SelectedIDs, AdslCustomerGroupsComboBox.SelectedIDs, PaymentTypesComboBox.SelectedIDs,
                                                                                                                               BandWidthComboBox.SelectedIDs, DurationComboBox.SelectedIDs, TrafficComboBox.SelectedIDs,
                                                                                                                               AdslServicesComboBox.SelectedIDs, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate, startRowIndex, pageSize, out count);


                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی لیست متوسط زمان جمع آوری خدمات  - ADSLServiceCollectionTimeAverageList");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = this.SearchExpander as UIElement;
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

                int fromDaysCount = !string.IsNullOrEmpty(FromDayCountTextBox.Text.Trim()) ? int.Parse(FromDayCountTextBox.Text) : 0;
                int toDaysCount = !string.IsNullOrEmpty(ToDayCountTextBox.Text.Trim()) ? int.Parse(ToDayCountTextBox.Text) : 0;

                DataSet data = AdslStatisticsDB.SearchADSLServiceCollectionTimeAverage(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, PersonTypesComboBox.SelectedIDs,
                                                                                       fromDaysCount, toDaysCount, AdslCustomerTypesComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs,
                                                                                       AdslServiceGroupsComboBox.SelectedIDs, AdslCustomerGroupsComboBox.SelectedIDs, PaymentTypesComboBox.SelectedIDs,
                                                                                       BandWidthComboBox.SelectedIDs, DurationComboBox.SelectedIDs, TrafficComboBox.SelectedIDs,
                                                                                       AdslServicesComboBox.SelectedIDs, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate, startRowIndex, pageSize, out count)
                                               .ToDataSet("Result", ADSLServiceCollectionTimeAverageDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی لیست متوسط زمان جمع آوری خدمات  - ADSLServiceCollectionTimeAverageList");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ADSLServiceCollectionTimeAverageDataGrid.Name, ADSLServiceCollectionTimeAverageDataGrid.Columns);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLServiceCollectionTimeAverageDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ADSLServiceCollectionTimeAverageDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ADSLServiceCollectionTimeAverageDataGrid.Columns);
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

        private void AdslServiceGroupComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AdslServicesComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByGroupIDs(AdslServiceGroupsComboBox.SelectedIDs);
        }        
    }
}
