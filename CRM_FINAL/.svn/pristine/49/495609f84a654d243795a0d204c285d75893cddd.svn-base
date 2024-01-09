using CRM.Application.Codes;
using CRM.Application.UserControls;
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
    public partial class ADSLLucrativeCustomer : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        public bool IsReal { get; set; }

        public bool IsLegal { get; set; }

        #endregion

        #region Constructor
        public ADSLLucrativeCustomer()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CitiesComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            AdslCustomerGroupsComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            SaleWaiesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleWays));
            AdslServiceTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
            AdslServiceGroupsComboBox.ItemsSource = ADSLServiceGroupDB.GetADSLServiceGroupCheckable();
            BandWidthComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            DurationComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceDurationCheckable();
            TrafficComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceTrafficCheckable();
            AdslServicesComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableNew();
            PaymentTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            AdslCustomerTypesComboBox.ItemsSource = ADSLCustomerTypeDB.GetADSLCustomerTypesCheckable();
        }

        #endregion

        #region EventHandlers

        private void PersonTypeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton originalSource = e.OriginalSource as RadioButton;
            if (originalSource != null)
            {
                string tag = (originalSource.Tag != null) ? originalSource.Tag.ToString() : string.Empty;
                switch (tag)
                {
                    case "1"://مشترک حقیقی
                        {
                            this.IsReal = true;
                            this.IsLegal = false;
                            break;
                        }
                    case "2"://مشترک حقوقی
                        {
                            this.IsLegal = true;
                            this.IsReal = false;
                            break;
                        }
                }
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, AdslLucrativeCustomersDataGrid.Name, AdslLucrativeCustomersDataGrid.Columns);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            SearchButton_Click(null, null);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = 10;
                AdslLucrativeCustomersDataGrid.ItemsSource = AdslStatisticsDB.SearchADSLLucrativeCustomer(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, this.IsReal, this.IsLegal,
                                                                                                          AdslCustomerGroupsComboBox.SelectedIDs, AdslCustomerTypesComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs,
                                                                                                          AdslServiceGroupsComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs, DurationComboBox.SelectedIDs,
                                                                                                          TrafficComboBox.SelectedIDs, AdslServicesComboBox.SelectedIDs, PaymentTypesComboBox.SelectedIDs,
                                                                                                          FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                                                          startRowIndex, pageSize, out count);
                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی مشترکین پرسود ADSL");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var child in Helper.FindVisualChildren<CheckableComboBox>(this))
            {
                child.Reset();
            }
            foreach (var child in Helper.FindVisualChildren<RadioButton>(this))
            {
                child.IsChecked = false;
            }
            FromDatePicker.SelectedDate = null;
            ToDatePicker.SelectedDate = null;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                //if (dataGridSelectedIndexs.Count > 0)
                //{
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;
                DataSet data = AdslStatisticsDB.SearchADSLLucrativeCustomer(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, this.IsReal, this.IsLegal,
                                                                            AdslCustomerGroupsComboBox.SelectedIDs, AdslCustomerTypesComboBox.SelectedIDs, AdslServiceTypesComboBox.SelectedIDs,
                                                                            AdslServiceGroupsComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs, DurationComboBox.SelectedIDs,
                                                                            TrafficComboBox.SelectedIDs, AdslServicesComboBox.SelectedIDs, PaymentTypesComboBox.SelectedIDs,
                                                                            FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                            startRowIndex, pageSize, out count).ToDataSet("Result", AdslLucrativeCustomersDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
                //}
                //else
                //{
                //    MessageBox.Show(".تعیین ستونهای مورد نظر برای ایجاد گزارش ضروی میباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی مشترکین پرسود ADSL");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(AdslLucrativeCustomersDataGrid.Columns);
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

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = AdslLucrativeCustomersDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = AdslLucrativeCustomersDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        #endregion

    }
}
