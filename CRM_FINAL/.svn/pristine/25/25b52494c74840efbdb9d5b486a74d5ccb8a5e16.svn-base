using CRM.Application.Codes;
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
    /// <summary>
    /// Interaction logic for TechnicalRequestList.xaml
    /// </summary>
    public partial class TechnicalRequestList : Local.TabWindow
    {
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        public TechnicalRequestList()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();

            List<CheckableItem> requestType = Data.RequestTypeDB.GetRequestTypeCheckable();
            requestType.RemoveAll(t => t.ID != (int)DB.RequestType.TranslationOpticalCabinetToNormal &&
                                       t.ID != (int)DB.RequestType.ExchangePost &&
                                       t.ID != (int)DB.RequestType.PCMToNormal &&
                                       t.ID != (int)DB.RequestType.SwapPCM &&
                                       t.ID != (int)DB.RequestType.SwapTelephone &&
                                       t.ID != (int)DB.RequestType.BuchtSwiching &&
                                       t.ID != (int)DB.RequestType.TranlationPostInput);
            RequestTypeComboBox.ItemsSource = requestType;
            // PersonTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }



        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            DateTime? fromDateTime = null;
            if (FromDate.SelectedDate.HasValue)
            {
                fromDateTime = FromDate.SelectedDate.Value;
            }

            DateTime? toDateTime = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDateTime = ToDate.SelectedDate.Value.AddDays(1);
            }

            long oldTelephoneNo = -1;
            if (!long.TryParse(OldTelehoneNoTextBox.Text.Trim(), out oldTelephoneNo)) { oldTelephoneNo = -1; };

            long newTelephoneNo = -1;
            if (!long.TryParse(NewTelehoneNoTextBox.Text.Trim(), out newTelephoneNo)) { newTelephoneNo = -1; };

            long requestID = -1;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };

            int count = 0;
            DataSet data = TechnicalRequestListDB.GetChangeTelephone(
                requestID,
                fromDateTime,
                toDateTime,
                CenterComboBox.SelectedIDs,
                RequestTypeComboBox.SelectedIDs,
                oldTelephoneNo,
                newTelephoneNo,
                startRowIndex,
                pageSize,
                out count
                ).ToDataSet("Result", ItemsDataGrid);

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


        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;


            //milad doran
            //int startRowIndex = 0;
            //int pageSize = Pager.TotalRecords;


            //TODO:rad 13950108
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            DateTime? fromDateTime = null;
            if (FromDate.SelectedDate.HasValue)
            {
                fromDateTime = FromDate.SelectedDate.Value;
            }

            DateTime? toDateTime = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDateTime = ToDate.SelectedDate.Value.AddDays(1);
            }

            long oldTelephoneNo = -1;
            if (!long.TryParse(OldTelehoneNoTextBox.Text.Trim(), out oldTelephoneNo)) { oldTelephoneNo = -1; };

            long newTelephoneNo = -1;
            if (!long.TryParse(NewTelehoneNoTextBox.Text.Trim(), out newTelephoneNo)) { newTelephoneNo = -1; };


            long requestID = -1;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };


            int count = 0;
            List<TechnicalRequestInfo> result = TechnicalRequestListDB.GetChangeTelephone(
                requestID,
                fromDateTime,
                toDateTime,
                CenterComboBox.SelectedIDs,
                RequestTypeComboBox.SelectedIDs,
                oldTelephoneNo,
                newTelephoneNo,
                startRowIndex,
                pageSize,
                out count
                );

            Pager.TotalRecords = count;

            ItemsDataGrid.ItemsSource = result;

            this.Cursor = Cursors.Arrow;
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

    }
}
