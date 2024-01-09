using CRM.Application.Codes;
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
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ReservesDayeriList.xaml
    /// </summary>
    public partial class ReservesDayeriList : Local.TabWindow
    {
        #region Properties and Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public ReservesDayeriList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int cabinet;
            if (!int.TryParse(CabinetTextBox.Text.Trim(), out cabinet)) { cabinet = -1; };

            int cabinetInput;
            if (!int.TryParse(CabinetInputTextBox.Text.Trim(), out cabinetInput)) { cabinetInput = -1; };

            int post;
            if (!int.TryParse(PostTextBox.Text.Trim(), out post)) { post = -1; };

            int postContact;
            if (!int.TryParse(PostContactTextBox.Text.Trim(), out postContact)) { postContact = -1; };

            long requestID;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };

            int totalRecords = 0;

            ItemsDataGrid.ItemsSource = Data.ReservesDayeriListDB.SearchReservesDayeri(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, cabinet, cabinetInput, post, postContact, requestID, startRowIndex, pageSize, out totalRecords);

            Pager.TotalRecords = totalRecords;
            this.Cursor = Cursors.Arrow;
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.PageSize;

                int cabinet;
                if (!int.TryParse(CabinetTextBox.Text.Trim(), out cabinet)) { cabinet = -1; };

                int cabinetInput;
                if (!int.TryParse(CabinetInputTextBox.Text.Trim(), out cabinetInput)) { cabinetInput = -1; };

                int post;
                if (!int.TryParse(PostTextBox.Text.Trim(), out post)) { post = -1; };

                int postContact;
                if (!int.TryParse(PostContactTextBox.Text.Trim(), out postContact)) { postContact = -1; };

                long requestID;
                if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };

                int totalRecords = 0;
                DataSet data = Data.ReservesDayeriListDB.SearchReservesDayeri(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, cabinet, cabinetInput, post, postContact, requestID, true, startRowIndex, pageSize, out totalRecords)
                                                        .ToDataSet("Result", ItemsDataGrid);

                //milad doran
                //if (data.Tables["Result"].Columns.Contains("InsertDate"))
                //{
                //    data.Tables["Result"].Columns.Add("PersianInsertDate");
                //    data.Tables["Result"].Columns["PersianInsertDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("InsertDate"));
                //    data.Tables["Result"].Columns["PersianInsertDate"].Caption = "تاریخ ثبت";
                //
                //    DataRow[] dataRow = data.Tables["Result"].Select();
                //    for (int i = 0; i < dataRow.Count(); i++)
                //    {
                //        dataRow[i]["PersianInsertDate"] = Convert.ToDateTime(dataRow[i]["InsertDate"]).ToPersian(Date.DateStringType.Short);
                //    }
                //    data.Tables["Result"].Columns.Remove("InsertDate");
                //
                //}
                //
                //if (data.Tables["Result"].Columns.Contains("ModifyDate"))
                //{
                //    data.Tables["Result"].Columns.Add("PersianModifyDate");
                //    data.Tables["Result"].Columns["PersianModifyDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("ModifyDate"));
                //    data.Tables["Result"].Columns["PersianModifyDate"].Caption = "تاریخ رزرو";
                //
                //    DataRow[] dataRow = data.Tables["Result"].Select();
                //    for (int i = 0; i < dataRow.Count(); i++)
                //    {
                //        dataRow[i]["PersianModifyDate"] = Convert.ToDateTime(dataRow[i]["ModifyDate"]).ToPersian(Date.DateStringType.Short);
                //    }
                //    data.Tables["Result"].Columns.Remove("ModifyDate");
                //
                //}
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در چاپ - اطلاعات فنی - رزرو");
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
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
