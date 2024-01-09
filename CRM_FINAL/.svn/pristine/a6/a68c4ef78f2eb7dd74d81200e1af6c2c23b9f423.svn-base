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

namespace CRM.Application.Views.InvestigatePossibilityFolder
{
    /// <summary>
    /// Interaction logic for InvestigatePossibilityWaitinglistForm.xaml
    /// </summary>
    public partial class InvestigatePossibilityWaitinglistForm : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public InvestigatePossibilityWaitinglistForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            //milad doran
            //this.Cursor = Cursors.Wait;

            //int startRowIndex = Pager.StartRowIndex;
            //int pageSize = Pager.PageSize;
            //int count = default(int);

            //int Cabinet = -1;
            //if (!int.TryParse(CabinetTextBox.Text.Trim(), out Cabinet)) { Cabinet = -1; };

            //int Post = -1;
            //if (!int.TryParse(PostTextBox.Text.Trim(), out Post)) { Post = -1; };

            //long Telephone = -1;
            //if (!long.TryParse(TelephoneTextBox.Text.Trim(), out Telephone)) { Telephone = -1; };

            //bool isOutOfWaitingList = (OutOfWaitingListCheckBox.IsChecked.HasValue && OutOfWaitingListCheckBox.IsChecked.Value) ? true : false;

            //long requestID = -1;
            //if (!long.TryParse(ReqeustIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };

            //List<InvestigatePossibilityWaitinglistInfo> result = CRM.Data.InvestigatePossibilityWaitinglistDB.Serach(
            //                                                                                                          CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, requestID,
            //                                                                                                          Cabinet, Post, Telephone, OneYearCheckBox.IsChecked,
            //                                                                                                          CauseComboBox.SelectedIDs, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate,
            //                                                                                                          isOutOfWaitingList, false,
            //                                                                                                          startRowIndex, pageSize, out count
            //                                                                                                         );
            //Pager.TotalRecords = count;
            //ItemsDataGrid.ItemsSource = result;

            //this.Cursor = Cursors.Arrow;
            //TODO:rad 13951103
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = default(int);

            int cabinet = !(string.IsNullOrEmpty(CabinetTextBox.Text.Trim())) ? Convert.ToInt32(CabinetTextBox.Text.Trim()) : -1;

            int post = !(string.IsNullOrEmpty(PostTextBox.Text.Trim())) ? Convert.ToInt32(PostTextBox.Text.Trim()) : -1;

            long telephone = !(string.IsNullOrEmpty(TelephoneTextBox.Text.Trim())) ? Convert.ToInt32(TelephoneTextBox.Text.Trim()) : -1;

            long requestID = !(string.IsNullOrEmpty(ReqeustIDTextBox.Text.Trim())) ? Convert.ToInt64(ReqeustIDTextBox.Text.Trim()) : -1;

            bool isOutOfWaitingList = (OutOfWaitingListCheckBox.IsChecked.HasValue && OutOfWaitingListCheckBox.IsChecked.Value) ? true : false;

            List<InvestigatePossibilityWaitinglistInfo> result = CRM.Data.InvestigatePossibilityWaitinglistDB.Serach(
                                                                                                                      CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, requestID,
                                                                                                                      cabinet, post, telephone, OneYearCheckBox.IsChecked,
                                                                                                                      CauseComboBox.SelectedIDs, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate,
                                                                                                                      isOutOfWaitingList, false,
                                                                                                                      startRowIndex, pageSize, out count
                                                                                                                     );
            Pager.TotalRecords = count;
            ItemsDataGrid.ItemsSource = result;

            this.Cursor = Cursors.Arrow;
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Load();
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItems.Count != 0)
            {

                ActionUserControl.ItemIDs.Clear();
                foreach (object currentItem in ItemsDataGrid.SelectedItems)
                {
                    InvestigatePossibilityWaitinglistInfo waitingListInfo = currentItem as InvestigatePossibilityWaitinglistInfo;
                    ActionUserControl.ItemIDs.Add(waitingListInfo.ID);
                }
            }
        }

        private void ItemsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {


            if ((e.Row.Item as InvestigatePossibilityWaitinglistInfo) != null && (e.Row.Item as InvestigatePossibilityWaitinglistInfo).DoProvidedFacility == true)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else if ((e.Row.Item as InvestigatePossibilityWaitinglistInfo) != null && (e.Row.Item as InvestigatePossibilityWaitinglistInfo).HasFreePostContact == true)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightBlue);
            }
            else if ((e.Row.Item as InvestigatePossibilityWaitinglistInfo) != null && (e.Row.Item as InvestigatePossibilityWaitinglistInfo).HasFreeBucht == true)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightYellow);
            }
            else if ((e.Row.Item as InvestigatePossibilityWaitinglistInfo) != null && (e.Row.Item as InvestigatePossibilityWaitinglistInfo).HasFreeBucht == true && (e.Row.Item as InvestigatePossibilityWaitinglistInfo).HasFreePostContact == true)
            {
                e.Row.Background = new SolidColorBrush(Colors.Red);
            }

            if ((e.Row.Item as InvestigatePossibilityWaitinglistInfo) != null && (e.Row.Item as InvestigatePossibilityWaitinglistInfo).isValidTime == true)
            {
                e.Row.FontWeight = FontWeights.Bold;
                e.Row.Background = Brushes.Red;
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CauseComboBox.ItemsSource = Data.StatusDB.GetStatusCheckableByType(DB.RequestStatusType.WaitingList);
            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.ExitWaitingList };
        }

        public override void Load()
        {
            Search(null, null);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = default(int);

            int cabinet = !(string.IsNullOrEmpty(CabinetTextBox.Text.Trim())) ? Convert.ToInt32(CabinetTextBox.Text.Trim()) : -1;

            int post = !(string.IsNullOrEmpty(PostTextBox.Text.Trim())) ? Convert.ToInt32(PostTextBox.Text.Trim()) : -1;

            long telephone = !(string.IsNullOrEmpty(TelephoneTextBox.Text.Trim())) ? Convert.ToInt32(TelephoneTextBox.Text.Trim()) : -1;

            long requestID = !(string.IsNullOrEmpty(ReqeustIDTextBox.Text.Trim())) ? Convert.ToInt64(ReqeustIDTextBox.Text.Trim()) : -1;

            bool isOutOfWaitingList = (OutOfWaitingListCheckBox.IsChecked.HasValue && OutOfWaitingListCheckBox.IsChecked.Value) ? true : false;

            List<InvestigatePossibilityWaitinglistInfo> result = CRM.Data.InvestigatePossibilityWaitinglistDB.Serach(
                                                                                                                        CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, requestID,
                                                                                                                        cabinet, post, telephone, OneYearCheckBox.IsChecked,
                                                                                                                        CauseComboBox.SelectedIDs, FromInsertDatePicker.SelectedDate, ToInsertDatePicker.SelectedDate,
                                                                                                                        isOutOfWaitingList, true,
                                                                                                                        startRowIndex, pageSize, out count
                                                                                                                    );
            DataSet data = result.ToDataSet("Result", ItemsDataGrid);

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

        #endregion
    }
}
