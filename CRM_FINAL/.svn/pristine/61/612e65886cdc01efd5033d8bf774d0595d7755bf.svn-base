using CRM.Application.Codes;
using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for GroupingCabinetInputList.xaml
    /// </summary>
    public partial class GroupingCabinetInputList : Local.TabWindow
    {
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        public GroupingCabinetInputList()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
          //  Search(null,null);

        }
        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.CabinetInputDB.GetGroupingCabinetInput(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetComboBox.SelectedIDs);
        }

        private void AssignBucht(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignCabinetInputToBuchtForm window = new AssignCabinetInputToBuchtForm((ItemsDataGrid.SelectedItem as GroupingCabinetInput).CabinetID ?? 0, (byte)DB.TypeCabinetInputToBucht.Assign);
                window.ShowDialog();
            }
        }

        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignCabinetInputToBuchtForm window = new AssignCabinetInputToBuchtForm((ItemsDataGrid.SelectedItem as GroupingCabinetInput).CabinetID ?? 0, (byte)DB.TypeCabinetInputToBucht.Leave);
                window.ShowDialog();
            }
        }


        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            if (!CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns))
            {
                Folder.MessageBox.ShowError("خطا در ذخیره اطلاعات");
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

                System.Data.DataSet data = Data.CabinetInputDB.GetGroupingCabinetInput(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetComboBox.SelectedIDs).ToDataSet("Result", ItemsDataGrid);
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


    }
}
