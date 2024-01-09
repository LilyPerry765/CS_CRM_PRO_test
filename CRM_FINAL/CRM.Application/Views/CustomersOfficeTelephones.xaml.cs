using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using Stimulsoft.Report;
using System.Data;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Drawing;
using System.Collections;
using CRM.Application.Codes;
using CRM.Application.UserControls;

namespace CRM.Application.Views
{
    public partial class CustomersOfficeTelephones : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        public CustomersOfficeTelephones()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Load Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            RoundTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RoundType));

            List<CheckableItem> statues = Helper.GetEnumCheckable(typeof(DB.TelephoneStatus));
            statues.RemoveAll(t => t.ID == (int)DB.TelephoneStatus.ChangingLocation);
            statues.Add(new CheckableItem { ID = -1, Name = "فعال", IsChecked = false });
            StatusComboBox.ItemsSource = statues;

            PriceSumComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));
            DepositPriceSumComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));

            ChargingTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChargingGroup));
            PosessionTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PossessionType));

            UsageTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneUsageType));
        }



        public void LoadData()
        {
            // Search(null, null);
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

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.PageSize;

                long telephoneNo = -1;
                if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text)) telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

                long priceSumTextBox = 0;
                long.TryParse(PriceSumTextBox.Text.Trim(), out priceSumTextBox);

                long depositPriceSum = 0;
                long.TryParse(DepositPriceSumTextBox.Text.Trim(), out depositPriceSum);

                int count = 0;
                ItemsDataGrid.ItemsSource = Data.TelephoneDB.SearchCustomerOfficeTelephone(CityComboBox.SelectedIDs, PreCodeTypeComboBox.SelectedIDs, RoundTypeComboBox.SelectedIDs, StatusComboBox.SelectedIDs, SwitchPrecodeComboBox.SelectedIDs, CenterComboBox.SelectedIDs, IsVIPCheckBox.IsChecked, IsRoundCheckBox.IsChecked, telephoneNo, startRowIndex, pageSize, (int?)TelephoneTypeComboBox.SelectedValue, (int?)TelephoneTypeGroupComboBox.SelectedValue, InstallationDateFromDate.SelectedDate, InstallationDateToDate.SelectedDate, (int?)PriceSumComboBox.SelectedValue, priceSumTextBox, (int?)DepositPriceSumComboBox.SelectedValue, depositPriceSum, ChargingTypecomboBox.SelectedIDs, PosessionTypecomboBox.SelectedIDs, UsageTypeComboBox.SelectedIDs, InitialInstallationDateFromDate.SelectedDate, InitialInstallationDateToDate.SelectedDate, DischargeDateFromDate.SelectedDate, DischargeDateToDate.SelectedDate, InitialDischargeDateFromDate.SelectedDate, InitialDischargeDateToDate.SelectedDate, out count, false);
                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی تلفن ها");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            //if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        CRM.Data.Telephone item = ItemsDataGrid.SelectedItem as CRM.Data.Telephone;

            //        DB.Delete<Data.Telephone>(item.TelephoneNo);
            //        ShowSuccessMessage("تلفن مورد نظر حذف شد");
            //        LoadData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف تلفن", ex);
            //}
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                TelephoneInfo item = ItemsDataGrid.SelectedItem as TelephoneInfo;
                if (item == null) return;

                TelephoneForm window = new TelephoneForm(item.TelephoneNo);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.Telephone item = e.Row.Item as Data.Telephone;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("تلفن مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره تلفن", ex);
            //}
        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void TelephoneTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneTypeComboBox.SelectedValue != null)
            {
                TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
            }
        }

        #endregion

        #region Print
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text)) telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            long priceSumTextBox = 0;
            long.TryParse(PriceSumTextBox.Text.Trim(), out priceSumTextBox);

            long depositPriceSum = 0;
            long.TryParse(DepositPriceSumTextBox.Text.Trim(), out depositPriceSum);

            int count = 0;

            DataSet data = Data.TelephoneDB.SearchCustomerOfficeTelephone(CityComboBox.SelectedIDs, PreCodeTypeComboBox.SelectedIDs, RoundTypeComboBox.SelectedIDs, StatusComboBox.SelectedIDs, SwitchPrecodeComboBox.SelectedIDs, CenterComboBox.SelectedIDs, IsVIPCheckBox.IsChecked, IsRoundCheckBox.IsChecked, telephoneNo, startRowIndex, pageSize, (int?)TelephoneTypeComboBox.SelectedValue, (int?)TelephoneTypeGroupComboBox.SelectedValue, InstallationDateFromDate.SelectedDate, InstallationDateToDate.SelectedDate, (int?)PriceSumComboBox.SelectedValue, priceSumTextBox, (int?)DepositPriceSumComboBox.SelectedValue, depositPriceSum, ChargingTypecomboBox.SelectedIDs, PosessionTypecomboBox.SelectedIDs, UsageTypeComboBox.SelectedIDs, InitialInstallationDateFromDate.SelectedDate, InitialInstallationDateToDate.SelectedDate, DischargeDateFromDate.SelectedDate, DischargeDateToDate.SelectedDate, InitialDischargeDateFromDate.SelectedDate, InitialDischargeDateToDate.SelectedDate, out count, true).ToDataSet("Result", ItemsDataGrid);

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
            if (!CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns))
            {
                Folder.MessageBox.ShowError("خطا در ذخیره ستون");
            }
        }

        #endregion Print
        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SwitchPrecodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void FailureItem_Click(object sender, RoutedEventArgs e)
        {
            TelephoneInfo telephoneInfo = ItemsDataGrid.SelectedItem as TelephoneInfo;
            if (telephoneInfo != null)
            {

                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneInfo.TelephoneNo);
                if (telephone.Status == (int)DB.TelephoneStatus.Free || telephone.Status == (int)DB.TelephoneStatus.Discharge)
                {
                    telephone.Status = (int)DB.TelephoneStatus.Destruction;
                    telephone.Detach();
                    DB.Save(telephone);
                }
                else
                {
                    Folder.MessageBox.ShowError("فقط تلفن های آزاد و تخلیه امکان ثبت خرابی دارند");
                }

                Search(null, null);
            }

        }

        private void RepairItem_Click(object sender, RoutedEventArgs e)
        {

            TelephoneInfo telephoneInfo = ItemsDataGrid.SelectedItem as TelephoneInfo;
            if (telephoneInfo != null)
            {

                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneInfo.TelephoneNo);
                if (telephone.Status == (int)DB.TelephoneStatus.Destruction && telephone.DischargeDate.HasValue)
                {
                    telephone.Status = (int)DB.TelephoneStatus.Discharge;
                }
                else if (telephone.Status == (int)DB.TelephoneStatus.Destruction && !telephone.DischargeDate.HasValue)
                {
                    telephone.Status = (int)DB.TelephoneStatus.Free;
                }

                telephone.Detach();
                DB.Save(telephone);

                Search(null, null);
            }
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }
    }
}

