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
using System.Windows.Shapes;
using CRM.Data;
using System.Transactions;
using Enterprise;
using System.Threading;
using System.Collections.ObjectModel;
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    public partial class ADSLCutomerInformationList : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public ADSLCutomerInformationList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLStatus));
            StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLStatus));
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            serviceComboBox.ItemsSource = ADSLServiceDB.GetAllADSLServiceCheckable();
            ADSLCustomerTypeComboBox.ItemsSource = ADSLCustomerTypeDB.GetADSLCustomerTypesCheckable();
            PersonTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
        }

        private void LoadData()
        {
            Search(null, null);
        }

        #endregion Methods

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.ADSLDB.SearchADSLInfoCount(CityComboBox.SelectedIDs,
                                                                 CenterComboBox.SelectedIDs,
                                                                 StatusComboBox.SelectedIDs,
                                                                 ServiceGroupComboBox.SelectedIDs,
                                                                 serviceComboBox.SelectedIDs,
                                                                 TelNoTextBox.Text,
                                                                 CustomerNameTextBox.Text,
                                                                 CustomerLastNameTextBox.Text,
                                                                 UserNameTextBox.Text,
                                                                 HasIPCheckBox.IsChecked,
                                                                 ADSLCustomerTypeComboBox.SelectedIDs,
                                                                 PersonTypeComboBox.SelectedIDs,
                                                                 MobileNoTextBox.Text.Trim());

            ItemsDataGrid.ItemsSource = Data.ADSLDB.SearchADSLInfo(CityComboBox.SelectedIDs,
                                                                    CenterComboBox.SelectedIDs,
                                                                    StatusComboBox.SelectedIDs,
                                                                    ServiceGroupComboBox.SelectedIDs,
                                                                    serviceComboBox.SelectedIDs,
                                                                    TelNoTextBox.Text,
                                                                    CustomerNameTextBox.Text,
                                                                    CustomerLastNameTextBox.Text,
                                                                    UserNameTextBox.Text,
                                                                    HasIPCheckBox.IsChecked,
                                                                    ADSLCustomerTypeComboBox.SelectedIDs,
                                                                    PersonTypeComboBox.SelectedIDs,
                                                                    MobileNoTextBox.Text.Trim(),
                                                                    startRowIndex, pageSize);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CityComboBox.Reset();
            CenterComboBox.Reset();
            ServiceGroupComboBox.Reset();
            serviceComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
            UserNameTextBox.Text = string.Empty;
            CustomerNameTextBox.Text = string.Empty;
            TelNoTextBox.Text = string.Empty;
            HasIPCheckBox.IsChecked = null;
            CustomerLastNameTextBox.Text = string.Empty;
            PersonTypeComboBox.Reset();
            ADSLCustomerTypeComboBox.Reset();
            MobileNoTextBox.Text = string.Empty;
            Search(null, null);
        }

        private void ShowItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLCustomerInfo item = ItemsDataGrid.SelectedItem as ADSLCustomerInfo;

                if (item == null)
                    return;

                ADSLCutomerInformationForm Window = new ADSLCutomerInformationForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void ServiceGroupComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            serviceComboBox.ItemsSource = ADSLServiceDB.GetAllADSLServiceCheckablebyGroupID(ServiceGroupComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.TotalRecords;

                DataSet data = ADSLDB.SearchADSLInfo(CityComboBox.SelectedIDs,
                                                     CenterComboBox.SelectedIDs,
                                                     StatusComboBox.SelectedIDs,
                                                     ServiceGroupComboBox.SelectedIDs,
                                                     serviceComboBox.SelectedIDs,
                                                     TelNoTextBox.Text,
                                                     CustomerNameTextBox.Text,
                                                     CustomerLastNameTextBox.Text,
                                                     UserNameTextBox.Text,
                                                     HasIPCheckBox.IsChecked,
                                                     ADSLCustomerTypeComboBox.SelectedIDs,
                                                     PersonTypeComboBox.SelectedIDs,
                                                     MobileNoTextBox.Text.Trim(),
                                                     startRowIndex, pageSize).ToDataSet("Result", ItemsDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی آمار سرویس ای دی اس ال");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
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

        #endregion
    }
}
