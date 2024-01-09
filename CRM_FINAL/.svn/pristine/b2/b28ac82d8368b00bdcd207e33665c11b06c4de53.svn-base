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
using System.Data;
using CRM.Application.Codes;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class Failure117RequestList : Local.TabWindow
    {
        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public Failure117RequestList()
        {
            Logger.WriteInfo("Start Constructor");
            try
            {
                InitializeComponent();
                Initialize();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message,ex);
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {

        }

        public void LoadData()
        {
            Logger.WriteInfo("Start Load");
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TelephoneNoTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            Logger.WriteInfo("Start Search");
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            Pager.TotalRecords = Data.Failure117DB.GetKarajRequestCount(TelephoneNoTextBox.Text.Trim());
            ItemsPortDataGrid.ItemsSource = Data.Failure117DB.GetKarajRequest(TelephoneNoTextBox.Text.Trim(), startRowIndex, pageSize);
            Logger.WriteInfo(Pager.TotalRecords.ToString());
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

                long telephoneNo = -1;
                if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                    telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

                DataSet data = Failure117DB.GetKarajRequest(TelephoneNoTextBox.Text.Trim(), startRowIndex, pageSize).ToDataSet("Result", ItemsPortDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در چاپ");
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsPortDataGrid.Columns);
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
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsPortDataGrid.Name, ItemsPortDataGrid.Columns);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsPortDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsPortDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void TelephoneNoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
