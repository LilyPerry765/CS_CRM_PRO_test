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
    /// لیست پی سی ام های آزاد شده بعد از انتقال(برگردان) به کافو نوری
    /// </summary>
    public partial class ReleasedPCMAfterTranslationOpticalCabinetToNormalList : ExtendedTabWindowBase
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public ReleasedPCMAfterTranslationOpticalCabinetToNormalList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CitiesCheckableComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void CitiesCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersCheckableComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesCheckableComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;
            List<int> cities = CitiesCheckableComboBox.SelectedIDs;
            List<int> centers = CentersCheckableComboBox.SelectedIDs;
            DateTime? fromDate = FromCompletionDatePicker.SelectedDate;
            DateTime? toDate = ToCompletionDatePicker.SelectedDate;

            Action mainAction = new Action(() =>
            {
                List<TranslationOpticalCabinetToNormalInfo> result = TranslationOpticalCabinetToNormalDB.GetReleasedPcmAfterTranslationOpticalCabinetToNormal(cities, centers, fromDate, toDate, false, startRowIndex, pageSize, out count);
                Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                                                                                    {
                                                                                        ItemsDataGrid.ItemsSource = result;
                                                                                        Pager.TotalRecords = count;
                                                                                    }
                                                                             )
                                       );
            });

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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.TotalRecords;
            int count = 0;

            List<TranslationOpticalCabinetToNormalInfo> result = TranslationOpticalCabinetToNormalDB.GetReleasedPcmAfterTranslationOpticalCabinetToNormal(
                                                                                                             CitiesCheckableComboBox.SelectedIDs, CentersCheckableComboBox.SelectedIDs,
                                                                                                             FromCompletionDatePicker.SelectedDate, ToCompletionDatePicker.SelectedDate,
                                                                                                             true, startRowIndex, pageSize, out count
                                                                                                            );
            DataSet data = result.ToDataSet("Result", ItemsDataGrid);

            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

            Pager.TotalRecords = count;

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

        #endregion


    }
}
