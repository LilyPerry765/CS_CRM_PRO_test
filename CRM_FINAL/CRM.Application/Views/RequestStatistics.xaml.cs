using CRM.Data;
using CRM.Data.ServiceHost;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for AbonmanChangesList.xaml
    /// </summary>
    public partial class RequestStatistics : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public RequestStatistics()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;

            DateTime? fromDateTime = FromDate.SelectedDate;

            DateTime? toDateTime = ToDate.SelectedDate;
            //برا اساس مورد 42 فکس خانم باقری بلاک زیر را کامنت کردم
            //13950229
            //if (ToDate.SelectedDate.HasValue)
            //{
            //    toDateTime = ToDate.SelectedDate.Value.AddDays(1);
            //}

            List<CRM.Data.RequestStatistics> result = RequestListDB.GetRequestStatistics(
                                                                                            fromDateTime,
                                                                                            toDateTime,
                                                                                            CityComboBox.SelectedIDs,
                                                                                            CenterComboBox.SelectedIDs,
                                                                                            startRowIndex,
                                                                                            pageSize,
                                                                                            out count
                                                                                         );

            Pager.TotalRecords = count;

            ItemsDataGrid.ItemsSource = result;

            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;
            int count = 0;

            DateTime? fromDateTime = FromDate.SelectedDate;
            DateTime? toDateTime = ToDate.SelectedDate;
            if (ToDate.SelectedDate.HasValue)
            {
                toDateTime = ToDate.SelectedDate.Value.AddDays(1);
            }

            DataSet result = RequestListDB.GetRequestStatistics(
                                                                    fromDateTime,
                                                                    toDateTime,
                                                                    CityComboBox.SelectedIDs,
                                                                    CenterComboBox.SelectedIDs,
                                                                    startRowIndex,
                                                                    pageSize,
                                                                    out count
                                                               )
                                           .ToDataSet("Result", ItemsDataGrid); ;

            Print.DynamicPrintV2(result, _title, dataGridSelectedIndexs, _groupingColumn);

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

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        } 

        #endregion

    }
}

