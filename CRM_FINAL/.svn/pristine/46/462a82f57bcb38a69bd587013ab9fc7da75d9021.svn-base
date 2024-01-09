using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.ComponentModel;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class CabinetList : Local.TabWindow
    {
        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        #endregion

        #region Constructor

        public CabinetList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CabinetTypeComboBox.ItemsSource = Data.CabinetTypeDB.GetCabinetTypeCheckable();
            AorBTypeCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.AORBPostAndCabinet));
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetStatus));
            CabinetUsageTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetUsageType));
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            ActiveInputCountComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));
            ActivePostCountComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));

            RemainedQuotaReservationComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));
            QuotaReservationComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;


            int fromInputNo = -1;
            if (!string.IsNullOrWhiteSpace(FromInputNoTextBox.Text)) fromInputNo = Convert.ToInt32(FromInputNoTextBox.Text.Trim());

            int toInputNo = -1;
            if (!string.IsNullOrWhiteSpace(ToInputNoTextBox.Text)) toInputNo = Convert.ToInt32(ToInputNoTextBox.Text.Trim());

            int distanceFromCenter = -1;
            if (!string.IsNullOrWhiteSpace(DistanceFromCenterTextBox.Text)) distanceFromCenter = Convert.ToInt32(DistanceFromCenterTextBox.Text.Trim());

            int outBoundMeter = -1;
            if (!string.IsNullOrWhiteSpace(OutBoundMeterTextBox.Text)) outBoundMeter = Convert.ToInt32(OutBoundMeterTextBox.Text.Trim());

            long fromPostalCode = -1;
            if (!string.IsNullOrWhiteSpace(FromPostalCodeTextBox.Text)) fromPostalCode = Convert.ToInt64(FromPostalCodeTextBox.Text.Trim());

            long toPostalCode = -1;
            if (!string.IsNullOrWhiteSpace(ToPostalCodeTextBox.Text)) toPostalCode = Convert.ToInt64(ToPostalCodeTextBox.Text.Trim());

            int activeInputCount = 0;
            int.TryParse(ActiveInputCountTextBox.Text.Trim(), out activeInputCount);

            double activePostCount = 0;
            double.TryParse(ActivePostCountTextBox.Text.Trim(), out activePostCount);



            int remainedQuotaReservation = 0;
            int.TryParse(RemainedQuotaReservationTextBox.Text.Trim(), out remainedQuotaReservation);

            int quotaReservation = 0;
            int.TryParse(QuotaReservationTextBox.Text.Trim(), out quotaReservation);


            int count = 0;
            ItemsDataGrid.ItemsSource = Data.CabinetDB.SearchCabinet(CityComboBox.SelectedIDs,
                CenterComboBox.SelectedIDs,
                CabinetTypeComboBox.SelectedIDs,
                CabinetUsageTypeComboBox.SelectedIDs,
                fromInputNo,
                toInputNo,
                distanceFromCenter,
                outBoundMeter,
                StatusComboBox.SelectedIDs,
                fromPostalCode,
                toPostalCode,
                CabinetNumberComboBox.SelectedIDs,
                CabinetCodeTextBox.Text.Trim(),
                AorBTypeCheckableComboBox.SelectedIDs,
                AddressTextBox.Text.Trim(),
                PostCodeTextBox.Text.Trim(),
                OutBoundMeterCheckBox.IsChecked,
                (int?)ActiveInputCountComboBox.SelectedValue,
                activeInputCount,
                (int?)ActivePostCountComboBox.SelectedValue,
                activePostCount,
                (int?)RemainedQuotaReservationComboBox.SelectedValue,
                remainedQuotaReservation,
                (int?)QuotaReservationComboBox.SelectedValue,
                quotaReservation,
                startRowIndex,
                pageSize, out count);

            Pager.TotalRecords = count;

            this.Cursor = Cursors.Arrow;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            int fromInputNo = -1;
            if (!string.IsNullOrWhiteSpace(FromInputNoTextBox.Text)) fromInputNo = Convert.ToInt32(FromInputNoTextBox.Text.Trim());

            int toInputNo = -1;
            if (!string.IsNullOrWhiteSpace(ToInputNoTextBox.Text)) toInputNo = Convert.ToInt32(ToInputNoTextBox.Text.Trim());

            int distanceFromCenter = -1;
            if (!string.IsNullOrWhiteSpace(DistanceFromCenterTextBox.Text)) distanceFromCenter = Convert.ToInt32(DistanceFromCenterTextBox.Text.Trim());

            int outBoundMeter = -1;
            if (!string.IsNullOrWhiteSpace(OutBoundMeterTextBox.Text)) outBoundMeter = Convert.ToInt32(OutBoundMeterTextBox.Text.Trim());

            long fromPostalCode = -1;
            if (!string.IsNullOrWhiteSpace(FromPostalCodeTextBox.Text)) fromPostalCode = Convert.ToInt64(FromPostalCodeTextBox.Text.Trim());

            long toPostalCode = -1;
            if (!string.IsNullOrWhiteSpace(ToPostalCodeTextBox.Text)) toPostalCode = Convert.ToInt64(ToPostalCodeTextBox.Text.Trim());

            int activeInputCount = 0;
            int.TryParse(ActiveInputCountTextBox.Text.Trim(), out activeInputCount);

            double activePostCount = 0;
            double.TryParse(ActivePostCountTextBox.Text.Trim(), out activePostCount);


            int remainedQuotaReservation = 0;
            int.TryParse(RemainedQuotaReservationTextBox.Text.Trim(), out remainedQuotaReservation);

            int quotaReservation = 0;
            int.TryParse(QuotaReservationTextBox.Text.Trim(), out quotaReservation);



            int count = 0;
            DataSet data = new DataSet();

            data = Data.CabinetDB.SearchCabinet(CityComboBox.SelectedIDs,
                                                  CenterComboBox.SelectedIDs,
                                                  CabinetTypeComboBox.SelectedIDs,
                                                  CabinetUsageTypeComboBox.SelectedIDs,
                                                  fromInputNo,
                                                  toInputNo,
                                                  distanceFromCenter,
                                                  outBoundMeter,
                                                  StatusComboBox.SelectedIDs,
                                                  fromPostalCode,
                                                  toPostalCode,
                                                  CabinetNumberComboBox.SelectedIDs,
                                                  CabinetCodeTextBox.Text.Trim(),
                                                  AorBTypeCheckableComboBox.SelectedIDs,
                                                  AddressTextBox.Text.Trim(),
                                                  PostCodeTextBox.Text.Trim(),
                                                  OutBoundMeterCheckBox.IsChecked,
                                                  (int?)ActiveInputCountComboBox.SelectedValue,
                                                  activeInputCount,
                                                  (int?)ActivePostCountComboBox.SelectedValue,
                                                  activePostCount,
                                                  (int?)RemainedQuotaReservationComboBox.SelectedValue,
                                                  remainedQuotaReservation,
                                                  (int?)QuotaReservationComboBox.SelectedValue,
                                                  quotaReservation,
                                                  startRowIndex,
                                                  pageSize,
                                                  out count)
                                   .ToDataSet("Result", ItemsDataGrid);
            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

            //Print.DynamicPrint(data);

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
                MessageBoxResult result = MessageBox.Show("با حذف کافو، مرکزی ها آزاد حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CenterCabinetInfo item = ItemsDataGrid.SelectedItem as CRM.Data.CenterCabinetInfo;
                    List<int> cabinetIDs = new List<int> { (int)item.ID };
                    List<Bucht> buchts = Data.BuchtDB.GetBuchtByCabinetIDs(cabinetIDs);
                    if (buchts.Any(t => t.CabinetInputID != null)) { MessageBox.Show("فقط کافو ای که مرکزی های آن از کابل آزاد است را می توانید حذف کنید"); return; }
                    DB.Delete<Data.Cabinet>(item.ID);
                    ShowSuccessMessage("کافو مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کافو", ex);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CabinetForm window = new CabinetForm(0);
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CenterCabinetInfo item = ItemsDataGrid.SelectedItem as Data.CenterCabinetInfo;
                if (item == null) return;

                CabinetForm window = new CabinetForm((int)item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.Cabinet item = e.Row.Item as Data.Cabinet;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("کافو مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره کافو", ex);
            //}
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetNumberComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        #endregion

    }
}
