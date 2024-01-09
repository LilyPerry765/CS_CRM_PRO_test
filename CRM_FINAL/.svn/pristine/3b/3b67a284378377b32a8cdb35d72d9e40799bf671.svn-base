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
    /// Interaction logic for CabinetInputList.xaml
    /// </summary>
    public partial class CabinetInputList : Local.TabWindow
    {
        #region Properties and Fields

        List<CabinetInputInfo> CabinetInputInfos = new List<CabinetInputInfo>();
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public CabinetInputList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CabinetInputStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetInputStatus));
            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();
            BuchtStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BuchtStatus)).Where(ci => ci.ID <= 3).ToList();

        }

        #endregion

        #region Event Handlers

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int fromCabinetInput = -1;
            if (!int.TryParse(FromCabinetInput.Text.Trim(), out fromCabinetInput)) { fromCabinetInput = -1; };

            int toCabinetInput = -1;
            if (!int.TryParse(ToCabinetInput.Text.Trim(), out toCabinetInput)) { toCabinetInput = -1; };

            long telephoneNo = -1;
            if (!long.TryParse(TelephoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

            long requestID = -1;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };


            int totalRecords = default(int);
            ItemsDataGrid.ItemsSource = CabinetInputDB.CabinetInputSearch(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetCheckableComboBox.SelectedIDs, fromCabinetInput, toCabinetInput, PCMTypeComboBox.SelectedIDs, startRowIndex, pageSize, CabinetInputStatusComboBox.SelectedIDs, BuchtStatusComboBox.SelectedIDs, out totalRecords, telephoneNo, requestID);

            Pager.TotalRecords = totalRecords;
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void AssignToCable(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignCabinetInputToBuchtForm window = new AssignCabinetInputToBuchtForm((ItemsDataGrid.SelectedItem as CabinetInputInfo).CabinetID, (byte)DB.TypeCabinetInputToBucht.Assign);
                window.ShowDialog();
            }
        }

        private void LeaveCable(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignCabinetInputToBuchtForm window = new AssignCabinetInputToBuchtForm((ItemsDataGrid.SelectedItem as CabinetInputInfo).CabinetID, (byte)DB.TypeCabinetInputToBucht.Leave);
                window.ShowDialog();
            }
        }

        private void Direction(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                CabinetInputDirectionForm cabinetInputDirectionWindows = new CabinetInputDirectionForm((ItemsDataGrid.SelectedItem as CabinetInputInfo).CabinetID);
                cabinetInputDirectionWindows.ShowDialog();

                TabWindow_Loaded(null, null);
            }

        }

        private void MalfactionClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                MalfuctionForm cabinetInputMalfuctionFormWindow = new MalfuctionForm((ItemsDataGrid.SelectedItem as CabinetInputInfo).ID, (Byte)DB.MalfuctionType.CabinetInput);
                cabinetInputMalfuctionFormWindow.Title = "خراب / اصلاح مرکزی";
                cabinetInputMalfuctionFormWindow.ShowDialog();

                //milad doran
                //TabWindow_Loaded(null, null);

                Search(null, null);
            }
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CabinetCheckableComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void DeleteCabinetInput_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignCabinetInputToBuchtForm window = new AssignCabinetInputToBuchtForm((ItemsDataGrid.SelectedItem as CabinetInputInfo).CabinetID, (byte)DB.TypeCabinetInputToBucht.Delete);
                window.ShowDialog();
            }
        }

        private void CorrectionRegisterationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO:rad 13950228
            MessageBox.Show("در دست اقدام", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MalfunctionRegisterationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO:rad 13950228
            CabinetInputInfo selectedItem = ItemsDataGrid.SelectedItem as CabinetInputInfo;
            if (selectedItem != null)
            {
                CabinetInputMalfunctionRegistrationForm form = new CabinetInputMalfunctionRegistrationForm(selectedItem.ID);
                form.ShowDialog();
                Search(null, null);
            }
        }

        #endregion

        #region Print

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            int fromCabinetInput = -1;
            if (!int.TryParse(FromCabinetInput.Text.Trim(), out fromCabinetInput)) { fromCabinetInput = -1; };

            int toCabinetInput = -1;
            if (!int.TryParse(ToCabinetInput.Text.Trim(), out toCabinetInput)) { toCabinetInput = -1; };

            long telephoneNo = -1;
            if (!long.TryParse(TelephoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

            long requestID = -1;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };

            int totalRecords = default(int);
            DataSet data = CabinetInputDB.CabinetInputSearch(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetCheckableComboBox.SelectedIDs, fromCabinetInput, toCabinetInput, PCMTypeComboBox.SelectedIDs, startRowIndex, pageSize, CabinetInputStatusComboBox.SelectedIDs, BuchtStatusComboBox.SelectedIDs, out totalRecords, telephoneNo, requestID).ToDataSet("Result", ItemsDataGrid); ;

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
