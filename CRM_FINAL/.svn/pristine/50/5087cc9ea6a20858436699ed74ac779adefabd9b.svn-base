using CRM.Application.Codes;
using CRM.Application.Local;
using CRM.Data;
using Enterprise;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationOpticalCabinetToNormalGeneralInformationList.xaml
    /// </summary>
    public partial class TranslationOpticalCabinetToNormalGeneralInformationList : TabWindow
    {
        #region Properties And Fields

        //TODO:rad باید کلیه جاهای که فیلتر تکتست باکس چند تا دارند از طریق روش این فرم پیاده سازی شوند
        /// <summary>
        /// .اگر این ویژگی درست باشد به معنای آن است که کاربر مقادیر عددی فیلتر ها را به درستی وارد کرده است در غیر این صورت مقدار ان نادرست است
        /// </summary>
        private bool FiltersValueIsNumeric { get; set; }

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public TranslationOpticalCabinetToNormalGeneralInformationList()
        {
            InitializeComponent();
            Initialize();
            this.FiltersValueIsNumeric = true;
        }

        #endregion

        #region EventHandlers

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TranslationOpticalCabinetToNormalDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TranslationOpticalCabinetToNormalDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TranslationOpticalCabinetToNormalDataGrid.Name, TranslationOpticalCabinetToNormalDataGrid.Columns);
        }

        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;
            if (source != null)
            {
                //آیا یک مقدار عددی برای فیلترها مشخص شده است یا خیر
                long? inputValue = -1;
                bool inputValueIsValid = Helper.CheckDigitDataEntry(source, out inputValue);
                if (!inputValueIsValid)
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن» و «شناسه درخواست» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.FiltersValueIsNumeric = false;
                    source.Focus();
                }
                else
                {
                    this.FiltersValueIsNumeric = true;
                }
            }
        }

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void CentersComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FromCabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(CentersComboBox.SelectedIDs, FromCabinetUsageTypeComboBox.SelectedIDs);
            ToCabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(CentersComboBox.SelectedIDs, ToCabinetUsageTypeComboBox.SelectedIDs);
        }

        private void FromCabinetUsageTypeComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FromCabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(CentersComboBox.SelectedIDs, FromCabinetUsageTypeComboBox.SelectedIDs);
        }

        private void ToCabinetUsageTypeComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ToCabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(CentersComboBox.SelectedIDs, ToCabinetUsageTypeComboBox.SelectedIDs);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                if (this.FiltersValueIsNumeric)
                {
                    int count = 0;
                    int startRowIndex = Pager.StartRowIndex;
                    int pageSize = 10;

                    long requestId = !string.IsNullOrEmpty(RequestIdTextBox.Text.Trim()) ? Convert.ToInt64(RequestIdTextBox.Text.Trim()) : -1;
                    long fromTelephoneNo = !string.IsNullOrEmpty(FromTelephoneNoTextBox.Text.Trim()) ? Convert.ToInt64(FromTelephoneNoTextBox.Text.Trim()) : -1;
                    long toTelephoneNo = !string.IsNullOrEmpty(ToTelephoneNoTextbox.Text.Trim()) ? Convert.ToInt64(ToTelephoneNoTextbox.Text.Trim()) : -1;

                    TranslationOpticalCabinetToNormalDataGrid.ItemsSource = TranslationOpticalCabinetToNormalDB.SearchTranslationOpticalCabinetToNormalGeneralInformation(CitiesComboBox.SelectedIDs,
                                                                                                                                              CentersComboBox.SelectedIDs,
                                                                                                                                              FromCabinetComboBox.SelectedIDs,
                                                                                                                                              ToCabinetComboBox.SelectedIDs,
                                                                                                                                              FromCabinetUsageTypeComboBox.SelectedIDs,
                                                                                                                                              ToCabinetUsageTypeComboBox.SelectedIDs,
                                                                                                                                              FromDatePicker.SelectedDate,
                                                                                                                                              ToDatePicker.SelectedDate,
                                                                                                                                              requestId,
                                                                                                                                              fromTelephoneNo,
                                                                                                                                              toTelephoneNo,
                                                                                                                                              startRowIndex,
                                                                                                                                              pageSize, out count);
                    Pager.TotalRecords = count;
                }
                else
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن قدیم» و «تلفن جدید» و «شناسه درخواست» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی اطلاعات کلی برگردان کافو نوری به معمولی");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                if (this.FiltersValueIsNumeric)
                {
                    int count = 0;
                    int startRowIndex = Pager.StartRowIndex;
                    int pageSize = Pager.TotalRecords;

                    long requestId = !string.IsNullOrEmpty(RequestIdTextBox.Text.Trim()) ? Convert.ToInt64(RequestIdTextBox.Text.Trim()) : -1;
                    long fromTelephoneNo = !string.IsNullOrEmpty(FromTelephoneNoTextBox.Text.Trim()) ? Convert.ToInt64(FromTelephoneNoTextBox.Text.Trim()) : -1;
                    long toTelephoneNo = !string.IsNullOrEmpty(ToTelephoneNoTextbox.Text.Trim()) ? Convert.ToInt64(ToTelephoneNoTextbox.Text.Trim()) : -1;

                    System.Data.DataSet data = TranslationOpticalCabinetToNormalDB.SearchTranslationOpticalCabinetToNormalGeneralInformation(
                                                                                                                                              CitiesComboBox.SelectedIDs,
                                                                                                                                              CentersComboBox.SelectedIDs,
                                                                                                                                              FromCabinetComboBox.SelectedIDs,
                                                                                                                                              ToCabinetComboBox.SelectedIDs,
                                                                                                                                              FromCabinetUsageTypeComboBox.SelectedIDs,
                                                                                                                                              ToCabinetUsageTypeComboBox.SelectedIDs,
                                                                                                                                              FromDatePicker.SelectedDate,
                                                                                                                                              ToDatePicker.SelectedDate,
                                                                                                                                              requestId,
                                                                                                                                              fromTelephoneNo,
                                                                                                                                              toTelephoneNo,
                                                                                                                                              startRowIndex,
                                                                                                                                              pageSize,
                                                                                                                                              out count
                                                                                                                                            ).ToDataSet("Result", TranslationOpticalCabinetToNormalDataGrid);

                    Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
                }
                else
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن» و «شناسه درخواست» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی اطلاعات کلی برگردان کافو نوری به معمولی");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(TranslationOpticalCabinetToNormalDataGrid.Columns);
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

        #region Methods

        public void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            FromCabinetUsageTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetUsageType));
            ToCabinetUsageTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetUsageType));
            FromCabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(CentersComboBox.SelectedIDs, FromCabinetUsageTypeComboBox.SelectedIDs);
            ToCabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCentersIdAndCabinetUsageTypesId(CentersComboBox.SelectedIDs, ToCabinetUsageTypeComboBox.SelectedIDs);
        }

        #endregion

    }
}
