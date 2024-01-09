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
using Enterprise;
using CRM.Data;
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    public partial class RequestPaymentList : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public RequestPaymentList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesCheckableComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();

            //BankColumn.ItemsSource = BankDB.GetBanksCheckable();
            BankComboBox.ItemsSource = BankDB.GetBanksCheckable();

            //BaseCostColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();
            BaseCostComboBox.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();

            //PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));

            PaymentWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentWay));
            //PaymentWayColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentWay));
        }

        private void LoadData()
        {
            //Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void CitiesCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersCheckableComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesCheckableComboBox.SelectedIDs);
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.PageSize;
                int count = default(int);

                long requestId = !string.IsNullOrEmpty(RequestIDTextBox.Text) ? Convert.ToInt64(RequestIDTextBox.Text) : -1;

                //Pager.TotalRecords = Data.RequestPaymentDB.SearchPaymentRequestCount(TelephonenoTextBox.Text.Trim(), BaseCostComboBox.SelectedIDs,
                //    PaymentTypeComboBox.SelectedIDs, PaymentWayComboBox.SelectedIDs, requestIDs, FicheNumberTextBox.Text.Trim(),
                //    /*PaymentIDTextBox.Text.Trim(), BillIDTextBox.Text.Trim(), */ BankComboBox.SelectedIDs, IsPaidCheckBox.IsChecked, IsAcceptedCheckBox.IsChecked);

                ItemsDataGrid.ItemsSource = Data.RequestPaymentDB.SearchPaymentRequest(
                                                                                        TelephonenoTextBox.Text.Trim(), BaseCostComboBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs, PaymentWayComboBox.SelectedIDs,
                                                                                        requestId, FicheNumberTextBox.Text.Trim(),
                                                                                        BankComboBox.SelectedIDs, CitiesCheckableComboBox.SelectedIDs,
                                                                                        CentersCheckableComboBox.SelectedIDs, IsPaidCheckBox.IsChecked,
                                                                                        IsAcceptedCheckBox.IsChecked, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate,
                                                                                        false, startRowIndex, pageSize, out count
                                                                                       );
                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void ShowItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RequestPaymentInfo item = ItemsDataGrid.SelectedItem as RequestPaymentInfo;
                if (item == null) return;

                if (string.IsNullOrEmpty(item.BillID) || string.IsNullOrEmpty(item.PaymentID))
                {
                    MessageBox.Show(". برای این پرداخت فاکتور صادر نشده است ", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                SaleFactorForm window = new SaleFactorForm(item.RequestID, item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;
                int count = default(int);

                long requestId = !string.IsNullOrEmpty(RequestIDTextBox.Text) ? Convert.ToInt64(RequestIDTextBox.Text) : -1;

                List<RequestPaymentInfo> result = Data.RequestPaymentDB.SearchPaymentRequest(
                                                                                        TelephonenoTextBox.Text.Trim(), BaseCostComboBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs, PaymentWayComboBox.SelectedIDs,
                                                                                        requestId, FicheNumberTextBox.Text.Trim(),
                                                                                        BankComboBox.SelectedIDs, CitiesCheckableComboBox.SelectedIDs,
                                                                                        CentersCheckableComboBox.SelectedIDs, IsPaidCheckBox.IsChecked,
                                                                                        IsAcceptedCheckBox.IsChecked, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate,
                                                                                        true, startRowIndex, pageSize, out count
                                                                                       );
                DataSet data = result.ToDataSet("Result", ItemsDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

                Pager.TotalRecords = count;

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
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
