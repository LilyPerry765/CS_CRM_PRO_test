using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class WarningHistoryList : Local.TabWindow
    {
        #region Constructor & Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        public WarningHistoryList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.WarningHistory));
            TypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.WarningHistory));
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion Load Methods

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
            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephonNoTextBox.Text.Trim())) telephoneNo = Convert.ToInt64(TelephonNoTextBox.Text.Trim());

            ItemsDataGrid.ItemsSource = Data.WarningHistoryDB.SearchWarningHistory(
                                                                                    TypeComboBox.SelectedIDs,
                                                                                    Date.SelectedDate,
                                                                                    InsertDate.SelectedDate,
                                                                                    telephoneNo,
                                                                                    TimeTextBox.Text.Trim(),
                                                                                    ArrestLetterNoDatePicker.SelectedDate,
                                                                                    ArrestReferenceTextBox.Text.Trim(),
                                                                                    ArrestLetterNoTextBox.Text.Trim()
                                                                                   );
            this.Cursor = Cursors.Arrow;
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            WarningHistoryForm window = new WarningHistoryForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = Folder.MessageBox.ShowQuestion("آیا از حذف مطمئن هستید؟");
                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.WarningHistory item = ItemsDataGrid.SelectedItem as CRM.Data.WarningHistory;

                    DB.Delete<Data.WarningHistory>(item.ID);
                    ShowSuccessMessage("رکود مورد نظر با موفقیت حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف تاریخچه اخطار", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                WarningHistory item = ItemsDataGrid.SelectedItem as Data.WarningHistory;
                if (item == null) return;

                WarningHistoryForm window = new WarningHistoryForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.WarningHistory item = e.Row.Item as Data.WarningHistory;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("WarningHistory مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره WarningHistory", ex);
            }
        }

        //TODO:rad
        private void PrintItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                try
                {
                    WarningHistory selectedItem = ItemsDataGrid.SelectedItem as WarningHistory;
                    if (selectedItem != null)
                    {
                        string customerName = string.Empty;
                        WarnedTelephoneInfo primaryResult = WarningHistoryDB.GetWarnedTelephoneTrustByTelephoneNo(selectedItem.TelephonNo);
                        if (primaryResult != null)
                        {
                            string warningMessage = WarningMessageDB.GetMessageByWarningTypeId(selectedItem.Type);
                            primaryResult.WarningMessage = string.IsNullOrEmpty(warningMessage) ? string.Empty : warningMessage;
                            primaryResult.SetWarningType(selectedItem.Type);
                            primaryResult.Date = selectedItem.Date.ToPersian(Data.Date.DateStringType.Short);
                            //primaryResult.CheckMembersValue();


                            //تنظیمات برای نمایش
                            List<WarnedTelephoneInfo> listForPrint = new List<WarnedTelephoneInfo>() { primaryResult };
                            StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                            if (selectedItem.Type == (byte)DB.WarningHistory.arrest)
                            {
                                CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.DetentionAndArrestReport, variable);
                            }
                            else
                            {
                                CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.WarningReport, variable);
                            }
                        }
                        else
                        {
                            Folder.MessageBox.ShowInfo("عدم وجود سابقه برای تلفن جاری");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, "خطا در چاپ - لیست اخطارها - بخش مشترکین");
                    Folder.MessageBox.ShowError("خطا در چاپ");
                }
            }
            else
            {
                Folder.MessageBox.ShowInfo("ابتدا باید یکی از رکوردها را برای چاپ انتخاب نمائید.");
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {

                long telephoneNo = -1;
                if (!string.IsNullOrWhiteSpace(TelephonNoTextBox.Text.Trim())) telephoneNo = Convert.ToInt64(TelephonNoTextBox.Text.Trim());

                DataSet data = Data.WarningHistoryDB.SearchWarningHistory(
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        Date.SelectedDate,
                                                                                        InsertDate.SelectedDate,
                                                                                        telephoneNo,
                                                                                        TimeTextBox.Text.Trim(),
                                                                                        ArrestLetterNoDatePicker.SelectedDate,
                                                                                        ArrestReferenceTextBox.Text.Trim(),
                                                                                        ArrestLetterNoTextBox.Text.Trim()
                                                                                       ).ToDataSet("Result", ItemsDataGrid);

                if (data.Tables["Result"].Columns.Contains("InsertDate"))
                {
                    data.Tables["Result"].Columns.Add("PersianInsertDate");
                    data.Tables["Result"].Columns["PersianInsertDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("InsertDate"));
                    data.Tables["Result"].Columns["PersianInsertDate"].Caption = "تاریخ ثبت";

                    DataRow[] dataRow = data.Tables["Result"].Select();
                    for (int i = 0; i < dataRow.Count(); i++)
                    {
                        dataRow[i]["PersianInsertDate"] = Convert.ToDateTime(dataRow[i]["InsertDate"]).ToPersian(CRM.Data.Date.DateStringType.Short);
                    }
                    data.Tables["Result"].Columns.Remove("InsertDate");

                }
                if (data.Tables["Result"].Columns.Contains("Date"))
                {
                    data.Tables["Result"].Columns.Add("PersianDate");
                    data.Tables["Result"].Columns["PersianDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("Date"));
                    data.Tables["Result"].Columns["PersianDate"].Caption = "تاریخ اخطار";

                    DataRow[] dataRow = data.Tables["Result"].Select();
                    for (int i = 0; i < dataRow.Count(); i++)
                    {
                        dataRow[i]["PersianDate"] = Convert.ToDateTime(dataRow[i]["Date"]).ToPersian(CRM.Data.Date.DateStringType.Short);
                    }
                    data.Tables["Result"].Columns.Remove("Date");

                }
                if (data.Tables["Result"].Columns.Contains("ArrestLetterNoDate"))
                {
                    data.Tables["Result"].Columns.Add("PersianArrestLetterNoDate");
                    data.Tables["Result"].Columns["PersianArrestLetterNoDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("ArrestLetterNoDate"));
                    data.Tables["Result"].Columns["PersianArrestLetterNoDate"].Caption = "تاریخ ثبت";

                    DataRow[] dataRow = data.Tables["Result"].Select();
                    for (int i = 0; i < dataRow.Count(); i++)
                    {
                        dataRow[i]["PersianArrestLetterNoDate"] = Convert.ToDateTime(dataRow[i]["ArrestLetterNoDate"]).ToPersian(CRM.Data.Date.DateStringType.Short);
                    }
                    data.Tables["Result"].Columns.Remove("ArrestLetterNoDate");

                }

                CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
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

        #endregion Event Handlers



    }
}
