using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Data;
using Enterprise;
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
    /// Interaction logic for PCMStatisticsList.xaml
    /// </summary>
    public partial class PCMStatisticsList : ExtendedTabWindowBase
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        #region Constructor

        public PCMStatisticsList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMStatus));
        }

        #endregion

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RockCheckableComboBox.ItemsSource = PCMRockDB.GetPCMRockCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void RockCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ShelfCheckableComboBox.ItemsSource = PCMShelfDB.GetCheckableItemPCMShelfByRockIDs(RockCheckableComboBox.SelectedIDs);
        }

        private void ShelfCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CardCheckableComboBox.ItemsSource = PCMDB.GetCheckableItemPCMCardInfoByShelfID(ShelfCheckableComboBox.SelectedIDs);
        }

        //milad doran
        //private void Search(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    try
        //    {
        //        int startRowIndex = Pager.StartRowIndex;
        //        int pageSize = Pager.PageSize;
        //        int count = 0;

        //        ItemsDataGrid.ItemsSource = PCMRemoveFormDB.SearchPCMStatistics(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs,
        //                                                                        RockCheckableComboBox.SelectedIDs, ShelfCheckableComboBox.SelectedIDs,
        //                                                                        CardCheckableComboBox.SelectedIDs, startRowIndex, pageSize, out count);

        //        Pager.TotalRecords = count;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex, "خطا در جستجوی آمار کلی PCM");
        //        MessageBox.Show("خطا در جستجو", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    this.Cursor = Cursors.Arrow;
        //}

        //TODO:rad 13950711
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;

            List<int> citiesId = CityComboBox.SelectedIDs;
            List<int> centersId = CenterComboBox.SelectedIDs;
            List<int> rocksId = RockCheckableComboBox.SelectedIDs;
            List<int> shelvesId = ShelfCheckableComboBox.SelectedIDs;
            List<int> cardsId = CardCheckableComboBox.SelectedIDs;

            Action mainAction = new Action(() =>
                                                    {
                                                        List<PCMStatisticsInfo> result = PCMRemoveFormDB.SearchPCMStatistics(citiesId, centersId, rocksId, shelvesId, cardsId, startRowIndex, pageSize, out count);
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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = SearchExpander as UIElement;
            Helper.ResetSearch(container);
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
            Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
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

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.TotalRecords;
            int count = 0;

            DataSet data = PCMRemoveFormDB.SearchPCMStatistics(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs,
                                                               RockCheckableComboBox.SelectedIDs, ShelfCheckableComboBox.SelectedIDs,
                                                               CardCheckableComboBox.SelectedIDs, startRowIndex, pageSize, out count
                                                               ).ToDataSet("Result", ItemsDataGrid);

            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            this.Cursor = Cursors.Arrow;
        }

        #endregion

    }
}
