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
    /// Interaction logic for CustomerInformationStatisticsList.xaml
    /// </summary>
    public partial class CustomerInformationStatisticsList : TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();

        string _title = string.Empty;

        #endregion

        #region Constructor

        public CustomerInformationStatisticsList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods
        public void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim())) ? Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : -1;
                bool hasNationalCode = default(bool);
                if (HasNationalCodeRadioButton.IsChecked.HasValue && HasNationalCodeRadioButton.IsChecked.Value)
                {
                    hasNationalCode = true;
                }

                int startRowIndex = Pager.StartRowIndex;
                int count = 0;
                int pageSize = Pager.PageSize;

                CustomerInformationsDataGrid.ItemsSource = CustomerDB.SearchCustomerStatisticInfos(
                                                                                                    CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, hasNationalCode,
                                                                                                    telephoneNo, FromBirthdateDatePicker.SelectedDate, ToBirthdateDatePicker.SelectedDate,
                                                                                                    FirstNameOrTitleTextBox.Text.Trim(), LastNameTextBox.Text.Trim(),
                                                                                                    BirthCertificateIDTextBox.Text.Trim(), FatherNameTextBox.Text.Trim(),
                                                                                                    false, startRowIndex, pageSize, out count
                                                                                                   );

                Pager.TotalRecords = count;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی آمار اطلاعات");
                MessageBox.Show("خطا در جستجو", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
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

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim())) ? Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : -1;
                bool hasNationalCode = default(bool);
                if (HasNationalCodeRadioButton.IsChecked.HasValue && HasNationalCodeRadioButton.IsChecked.Value)
                {
                    hasNationalCode = true;
                }

                int startRowIndex = Pager.StartRowIndex;
                int count = 0;
                int pageSize = Pager.TotalRecords;

                List<CustomerStatisticsInfo> result = CustomerDB.SearchCustomerStatisticInfos(
                                                                                               CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, hasNationalCode,
                                                                                               telephoneNo, FromBirthdateDatePicker.SelectedDate, ToBirthdateDatePicker.SelectedDate,
                                                                                               FirstNameOrTitleTextBox.Text.Trim(), LastNameTextBox.Text.Trim(),
                                                                                               BirthCertificateIDTextBox.Text.Trim(), FatherNameTextBox.Text.Trim(),
                                                                                               true, startRowIndex, pageSize, out count
                                                                                             );

                DataSet data = result.ToDataSet("Result", CustomerInformationsDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی آمار اطلاعات");
                MessageBox.Show("خطا در جستجو", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = CustomerInformationsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = CustomerInformationsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, CustomerInformationsDataGrid.Name, CustomerInformationsDataGrid.Columns);
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(CustomerInformationsDataGrid.Columns);
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

        #endregion
    }
}
