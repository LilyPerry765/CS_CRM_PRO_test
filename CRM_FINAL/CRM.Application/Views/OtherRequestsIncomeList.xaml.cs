using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
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
    /// Interaction logic for OtherRequestsIncomeList.xaml
    /// </summary>
    public partial class OtherRequestsIncomeList : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public OtherRequestsIncomeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void HeaderChechBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void HeaderChechBox_Unchecked(object sender, RoutedEventArgs e)
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

        private void PrintClick(object sender, RoutedEventArgs e)
        {
            RequestThatHasTelecomminucationServiceStatistics selectedItem = ItemsDataGrid.SelectedItem as RequestThatHasTelecomminucationServiceStatistics;
            if (selectedItem != null)
            {
                List<CustomerReportInfo> customersInfo = new List<CustomerReportInfo>();
                int requestTypeId = Convert.ToInt32(RequestTypeComboBox.SelectedValue);
                List<TelecomminucationServicePaymentReportInfo> telecomminucationServicePaymentReportInfo = TelecomminucationServicePaymentDB.SearchTelecomminucationServicePayments(
                                                                                                                                                                                     selectedItem.NationalCodeOrRecordNo,
                                                                                                                                                                                     selectedItem.RequestID,
                                                                                                                                                                                     out customersInfo
                                                                                                                                                                                    );

                StiReport report = new StiReport();
                string path = ReportDB.GetReportPath((int)DB.UserControlNames.TelecomminucationServicePaymentStatisticsReport);
                report.Load(path);
                report.RegData("Categories", "Categories", customersInfo);
                report.RegData("Products", "Products", telecomminucationServicePaymentReportInfo);
                report.Dictionary.Variables["ReportDate"].ValueObject = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                report.CacheAllData = true;
                ReportViewerForm reportViewer = new ReportViewerForm(report);
                reportViewer.ShowDialog();
            }
        }

        private void ItemsDataGrid_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            RequestThatHasTelecomminucationServiceStatistics selectedItem = ItemsDataGrid.SelectedItem as RequestThatHasTelecomminucationServiceStatistics;
            if (selectedItem != null)
            {
                DataGrid detail = e.DetailsElement.FindName("TelecomminucationServicePaymentDataGrid") as DataGrid;
                detail.ItemsSource = TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentStatisticsInfoByRequestID(selectedItem.RequestID);
            }
        }

        private void CityCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int count = 0;
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.PageSize;
                int requestTypeId = (RequestTypeComboBox.SelectedValue != null) ? Convert.ToInt32(RequestTypeComboBox.SelectedValue) : -1;

                ItemsDataGrid.ItemsSource = RequestListDB.SearchRequestThatHasTelecomminucationService(
                                                                                                       CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CustomersComboBox.SelectedIDs_l,
                                                                                                       requestTypeId, FromDatePicker.SelectedDate,
                                                                                                       ToDatePicker.SelectedDate, startRowIndex, pageSize, out count
                                                                                                       );
                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی سایر درآمدها");
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
                int pageSize = Pager.PageSize;
                int requestTypeId = (RequestTypeComboBox.SelectedValue != null) ? Convert.ToInt32(RequestTypeComboBox.SelectedValue) : -1;

                DataSet data = RequestListDB.SearchRequestThatHasTelecomminucationService(
                                                                                          CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CustomersComboBox.SelectedIDs_l,
                                                                                          requestTypeId, FromDatePicker.SelectedDate,
                                                                                          ToDatePicker.SelectedDate, startRowIndex, pageSize, out count
                                                                                          ).ToDataSet("Result", ItemsDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی سایر درآمدها");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            RequestTypeComboBox.ItemsSource = RequestTypeDB.GetAllEntity();
            CustomersComboBox.ItemsSource = TelecomminucationServicePaymentDB.GetCustomersOfTelecomminucationService();
        }

        #endregion

    }
}
