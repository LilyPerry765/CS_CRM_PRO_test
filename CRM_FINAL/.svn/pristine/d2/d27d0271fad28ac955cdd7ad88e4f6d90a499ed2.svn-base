using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class RequestLogList : Local.ExtendedTabWindowBase
    {
        #region Properties

        RequestLog requestLog { get; set; }
        List<RequestLog> requestLogList = new List<RequestLog>();
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public RequestLogList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            List<CheckableItem> list = Data.RequestTypeDB.GetRequestTypeCheckable();
            list.RemoveAll(t => t.ID == (int)DB.RequestType.EditTelephone || t.ID == (int)DB.RequestType.EditCustomer || t.ID == (int)DB.RequestType.EditAddress);
            RequestTypeComboBox.ItemsSource = list;
            CitiesComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        public void LoadData()
        {
            //Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        //milad doran
        //private void Search(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    int startRowIndex = Pager.StartRowIndex;
        //    int pageSize = Pager.PageSize;

        //    long telephoneNo = -1;
        //    if (!long.TryParse(TelephoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

        //    int count = 0;
        //    ItemsDataGrid.ItemsSource = RequestLogDB.SearchRequestLogs(RequsetIDTextBox.Text, RequestTypeComboBox.SelectedIDs, telephoneNo, FromDate.SelectedDate, ToDate.SelectedDate, CustomerIDTextBox.Text, startRowIndex, pageSize, out count);
        //    Pager.TotalRecords = count;
        //    this.Cursor = Cursors.Arrow;
        //}

        //TODO:rad 13950711
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;

            long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim())) ? Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : -1;
            string requestId = RequsetIDTextBox.Text.Trim();
            string customerId = CustomerIDTextBox.Text.Trim();
            List<int> requestTypesId = RequestTypeComboBox.SelectedIDs;
            DateTime? fromDate = FromDate.SelectedDate;
            DateTime? toDate = ToDate.SelectedDate;
            List<int> centersId = CentersComboBox.SelectedIDs;

            Action mainAction = new Action(() =>
                                                    {
                                                        List<RequestLogReport> result = RequestLogDB.SearchRequestLogs(requestId, requestTypesId, telephoneNo, fromDate, toDate, customerId, centersId, startRowIndex, pageSize, out count);
                                                        Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                                                                                                                            {
                                                                                                                                ItemsDataGrid.ItemsSource = result;
                                                                                                                                Pager.TotalRecords = count;
                                                                                                                            }
                                                                                                                      )
                                                                               );
                                                    }
                                            );

            //مقداردهی عملیات اطلاع رسانی از وضعیت اجرای عملیات اصلی
            Action duringOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = true;
                MainExtendedStatusBar.MessageLabel.FontSize = 13;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Bold;
                MainExtendedStatusBar.MessageLabel.Text = "درحال بارگذاری...";
                Pager.IsEnabled = false;
                SearchExpander.IsEnabled = false;
                ItemsDataGrid.IsEnabled = false;
                this.Cursor = Cursors.Wait;
            });

            //مقداردهی عملیاتی که باید بعد از اتمام عملیات اصلی اجرا شود 
            Action afterOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = false;
                MainExtendedStatusBar.MessageLabel.FontSize = 8;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Normal;
                MainExtendedStatusBar.MessageLabel.Text = string.Empty;
                Pager.IsEnabled = true;
                SearchExpander.IsEnabled = true;
                ItemsDataGrid.IsEnabled = true;
                this.Cursor = Cursors.Arrow;
            });

            CRM.Application.Local.TimeConsumingOperation timeConsumingOperation = new Local.TimeConsumingOperation
            {
                MainOperationAction = mainAction,
                DuringOperationAction = duringOperationAction,
                AfterOperationAction = afterOperationAction
            };

            //اجرای عملیات
            this.RunTimeConsumingOperation(timeConsumingOperation);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;
            int count = 0;

            long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim())) ? Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : -1;

            DataSet data = RequestLogDB.SearchRequestLogs(RequsetIDTextBox.Text, RequestTypeComboBox.SelectedIDs, telephoneNo, FromDate.SelectedDate, ToDate.SelectedDate, CustomerIDTextBox.Text, CentersComboBox.SelectedIDs, startRowIndex, pageSize, out count).ToDataSet("Result", ItemsDataGrid);

            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

            this.Cursor = Cursors.Arrow;
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

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    RequestLog item = ItemsDataGrid.SelectedItem as RequestLog;

                    DB.Delete<Data.RequestLog>(item.ID);
                    ShowSuccessMessage("لاگ مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف لاگ", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        #endregion Event Handlers

    }
}
