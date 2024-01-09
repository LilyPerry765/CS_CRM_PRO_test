using CRM.Application.Codes;
using CRM.Data;
using CRM.Data.ServiceHost;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for AbonmanChangesList.xaml
    /// </summary>
    public partial class RequestList : Local.TabWindow
    {
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        public RequestList()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            List<int> removeItem = new List<int> { 8, 35, 38, 39, 43, 45, 46, 57, 60, 65, 67, 76, 79, 80, 83, 85, 111, 103, 107, (int)DB.RequestType.TranslationOpticalCabinetToNormal, (int)DB.RequestType.TranlationPostInput, (int)DB.RequestType.ExchangePost, (int)DB.RequestType.ExchangePostWithChangeCabintInput, (int)DB.RequestType.PCMToNormal, (int)DB.RequestType.CenterToCenterTranslation, (int)DB.RequestType.ExchangeCenralCableMDF, (int)DB.RequestType.PCMInstallation, (int)DB.RequestType.DeletePCMInstallation, (int)DB.RequestType.SwapPCM, (int)DB.RequestType.SwapTelephone};

            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable().Where(t=> !removeItem.Contains(t.ID)).OrderBy(t=>t.Name).ToList();
            //PersonTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));

            CauseOfCutComboBox.ItemsSource = CauseOfCutDB.GetCauseOfCutCheckableItem();
            CauseOfChangeNoComboBox.ItemsSource = CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();
            CauseOfRefundDepositComboBox.ItemsSource = CauseOfRefundDepositDB.GetCauseOfRefundDepositCheckableItem();
            CauseOfTakePossessionComboBox.ItemsSource = CauseOfTakePossessionDB.GetCauseOfTakePossessionCheckableItem();
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
            ZeroBlockComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ClassTelephone));
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            long telephoneNo = -1;
            if (!long.TryParse(TelehoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

            if ((FromDate.SelectedDate != null && ToDate.SelectedDate != null) || telephoneNo != -1)
            {
                int startRowIndex = Pager.StartRowIndex;
                int pageSize = Pager.PageSize;


                DateTime? fromDateTime = null;
                if (FromDate.SelectedDate.HasValue)
                {
                     fromDateTime = FromDate.SelectedDate.Value;
                }

                DateTime? toDateTime = null;
                if (ToDate.SelectedDate.HasValue)
                {
                     toDateTime = ToDate.SelectedDate.Value.AddDays(1);
                }

              //  var result1 = ServiceHostDB.GetChangeTelephone((DateTime)fromDateTime, (DateTime)toDateTime, new List<int> { }, new List<int> { });

                int count = 0;
                List<ChangeTelephone> result = RequestListDB.GetChangeTelephone(
                                                                                IsOutBoundCheckBox.IsChecked,
                                                                                fromDateTime,
                                                                                toDateTime,
                                                                                CenterComboBox.SelectedIDs,
                                                                                RequestTypeComboBox.SelectedIDs,
                                                                                CauseOfCutComboBox.SelectedIDs,
                                                                                CauseOfChangeNoComboBox.SelectedIDs,
                                                                                CauseOfRefundDepositComboBox.SelectedIDs,
                                                                                CauseOfTakePossessionComboBox.SelectedIDs,
                                                                                telephoneNo,
                                                                                TelephoneTypeComboBox.SelectedIDs,
                                                                                TelephoneTypeGroupComboBox.SelectedIDs,
                                                                                ZeroBlockComboBox.SelectedIDs,
                                                                                IsChangeNameCheckBox.IsChecked,
                                                                                PreCodeTypeComboBox.SelectedIDs,
                                                                                startRowIndex,
                                                                                pageSize,
                                                                                out count
                                                                               );

                Pager.TotalRecords = count;

                ItemsDataGrid.ItemsSource = result;

            }
            else
            {
                MessageBox.Show(".تعیین فیلترهای «از تاریخ» و «تا تاریخ» ضروری میباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }



        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            long telephoneNo = -1;
            if (!long.TryParse(TelehoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

            if ((FromDate.SelectedDate != null && ToDate.SelectedDate != null ) || telephoneNo != -1)
            {

                int startRowIndex = 0;
                int pageSize = Pager.TotalRecords;

                DateTime? fromDateTime = null;
                if (FromDate.SelectedDate.HasValue)
                {
                    fromDateTime = FromDate.SelectedDate.Value;
                }

                DateTime? toDateTime = null;
                if (ToDate.SelectedDate.HasValue)
                {
                    toDateTime = ToDate.SelectedDate.Value.AddDays(1);
                }

                int count = 0;
                DataSet data = RequestListDB.GetChangeTelephone(
                    IsOutBoundCheckBox.IsChecked,
                    fromDateTime,
                    toDateTime,
                    CenterComboBox.SelectedIDs,
                    RequestTypeComboBox.SelectedIDs,
                    CauseOfCutComboBox.SelectedIDs,
                    CauseOfChangeNoComboBox.SelectedIDs,
                    CauseOfRefundDepositComboBox.SelectedIDs,
                    CauseOfTakePossessionComboBox.SelectedIDs,
                    telephoneNo,
                    TelephoneTypeComboBox.SelectedIDs,
                    TelephoneTypeGroupComboBox.SelectedIDs,
                    ZeroBlockComboBox.SelectedIDs,
                    IsChangeNameCheckBox.IsChecked,
                    PreCodeTypeComboBox.SelectedIDs,
                    startRowIndex,
                    pageSize,
                    out count
                    ).ToDataSet("Result", ItemsDataGrid);

                //if (data.Tables["Result"].Columns.Contains("InsertDate"))
                //{
                //    data.Tables["Result"].Columns.Add("PersianInsertDate");
                //    data.Tables["Result"].Columns["PersianInsertDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("InsertDate"));
                //    data.Tables["Result"].Columns["PersianInsertDate"].Caption = "تاریخ ثبت";

                //    DataRow[] dataRow = data.Tables["Result"].Select();
                //    for (int i = 0; i < dataRow.Count(); i++)
                //    {
                //        dataRow[i]["PersianInsertDate"] = Convert.ToDateTime(dataRow[i]["InsertDate"]).ToPersian(Date.DateStringType.Short);
                //    }
                //    data.Tables["Result"].Columns.Remove("InsertDate");

                //}

                //if (data.Tables["Result"].Columns.Contains("EndDate"))
                //{
                //    data.Tables["Result"].Columns.Add("PersianEndDate");
                //    data.Tables["Result"].Columns["PersianEndDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("EndDate"));
                //    data.Tables["Result"].Columns["PersianEndDate"].Caption = "تاریخ پایان";

                //    DataRow[] dataRow = data.Tables["Result"].Select();
                //    for (int i = 0; i < dataRow.Count(); i++)
                //    {
                //        dataRow[i]["PersianEndDate"] = Convert.ToDateTime(dataRow[i]["EndDate"]).ToPersian(Date.DateStringType.Short);
                //    }
                //    data.Tables["Result"].Columns.Remove("EndDate");

                //}

                //if (data.Tables["Result"].Columns.Contains("PersonType"))
                //{
                //    data.Tables["Result"].Columns.Add("PersonTypeName");
                //    data.Tables["Result"].Columns["PersonTypeName"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("PersonType"));
                //    data.Tables["Result"].Columns["PersonTypeName"].Caption = "نوع مشترک";

                //    DataRow[] dataRow = data.Tables["Result"].Select();
                //    for (int i = 0; i < dataRow.Count(); i++)
                //    {

                //        dataRow[i]["PersonTypeName"] = Helper.GetEnumDescriptionByValue(typeof(DB.PersonType), Convert.ToInt32(dataRow[i]["PersonType"]));
                //    }
                //    data.Tables["Result"].Columns.Remove("PersonType");

                //}
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
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

        private void TelephoneTypeComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeIDs(TelephoneTypeComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }


    }
}

