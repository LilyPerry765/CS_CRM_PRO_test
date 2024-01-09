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
    public partial class AbonmanChangesList : Local.TabWindow
    {
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        public AbonmanChangesList()
        {
            InitializeComponent();
            Initialize();
        }

       private void Initialize()
       {
          CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
          RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
          PersonTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
           
       }

                

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {

            DateTime currentDate = DB.GetServerDate(); 
            
            ToDate.SelectedDate = currentDate;
            FromDate.Text =  Helper.AddMonthToPersianDate(currentDate.ToPersian(Date.DateStringType.Short), -1);

        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if(FromDate.SelectedDate != null && ToDate.SelectedDate != null)
            {

            DateTime  fromDateTime = FromDate.SelectedDate.Value;
            DateTime  toDateTime = ToDate.SelectedDate.Value;
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
           // List<int> centerCode = Data.CenterDB.GetCenterCodeByCenterIDs(CenterComboBox.SelectedIDs);
            result = ServiceHostDB.GetChangeTelephone(fromDateTime, toDateTime, CenterComboBox.SelectedIDs, RequestTypeComboBox.SelectedIDs);

            //string   errorMessages = string.Empty;
            //bool error = false;

            //BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            //EndpointAddress endpointAddress = new EndpointAddress("http://192.168.240.102:83/CRM.ServiceHost.CRMService.svc");
            //basicHttpBinding.MaxBufferSize = 2147483647;
            //basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            //using (CRMServiceHost.CRMServiceClient CRMServiceClient = new CRMServiceHost.CRMServiceClient(basicHttpBinding, endpointAddress))
            //{
            //    result = CRMServiceClient.ChangeTelephoneInfo(out error, out errorMessages, "PendarPajouh", "Kermanshah@srv", fromDateTime, toDateTime, centerCode, RequestTypeComboBox.SelectedIDs);
            //    // result = CRMServiceClient.GetChangeTelephone(out error, out errorMessages, "Pendar", "Pajouh", fromDateTime, toDateTime, centerCode, RequestTypeComboBox.SelectedIDs);
            //    CRMServiceClient.Close();
            //}

             ItemsDataGrid.ItemsSource = result;
            
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

                if (FromDate.SelectedDate != null && ToDate.SelectedDate != null)
                {

                    DateTime fromDateTime = FromDate.SelectedDate.Value;
                    DateTime toDateTime = ToDate.SelectedDate.Value;
                    List<int> centerCode = Data.CenterDB.GetCenterCodeByCenterIDs(CenterComboBox.SelectedIDs);
                    DataSet data = ServiceHostDB.GetChangeTelephone(fromDateTime, toDateTime, centerCode, RequestTypeComboBox.SelectedIDs).ToDataSet("Result", ItemsDataGrid);

                    if (data.Tables["Result"].Columns.Contains("InsertDate"))
                    {
                        data.Tables["Result"].Columns.Add("PersianInsertDate");
                        data.Tables["Result"].Columns["PersianInsertDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("InsertDate"));
                        data.Tables["Result"].Columns["PersianInsertDate"].Caption = "تاریخ ثبت";

                        DataRow[] dataRow = data.Tables["Result"].Select();
                        for (int i = 0; i < dataRow.Count(); i++)
                        {
                            dataRow[i]["PersianInsertDate"] = Convert.ToDateTime(dataRow[i]["InsertDate"]).ToPersian(Date.DateStringType.Short);
                        }
                        data.Tables["Result"].Columns.Remove("InsertDate");

                    }

                    if (data.Tables["Result"].Columns.Contains("EndDate"))
                    {
                        data.Tables["Result"].Columns.Add("PersianEndDate");
                        data.Tables["Result"].Columns["PersianEndDate"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("EndDate"));
                        data.Tables["Result"].Columns["PersianEndDate"].Caption = "تاریخ پایان";

                        DataRow[] dataRow = data.Tables["Result"].Select();
                        for (int i = 0; i < dataRow.Count(); i++)
                        {
                            dataRow[i]["PersianEndDate"] = Convert.ToDateTime(dataRow[i]["EndDate"]).ToPersian(Date.DateStringType.Short);
                        }
                        data.Tables["Result"].Columns.Remove("EndDate");

                    }

                    if (data.Tables["Result"].Columns.Contains("PersonType"))
                    {
                        data.Tables["Result"].Columns.Add("PersonTypeName");
                        data.Tables["Result"].Columns["PersonTypeName"].SetOrdinal(data.Tables["Result"].Columns.IndexOf("PersonType"));
                        data.Tables["Result"].Columns["PersonTypeName"].Caption = "نوع مشترک";

                        DataRow[] dataRow = data.Tables["Result"].Select();
                        for (int i = 0; i < dataRow.Count(); i++)
                        {

                            dataRow[i]["PersonTypeName"] = Helper.GetEnumDescriptionByValue(typeof(DB.PersonType), Convert.ToInt32(dataRow[i]["PersonType"]));
                        }
                        data.Tables["Result"].Columns.Remove("PersonType");

                    }

                    //Print.DynamicPrint(data);
                    Print.DynamicPrintV2(data,_title, dataGridSelectedIndexs, _groupingColumn);

                }

            this.Cursor = Cursors.Arrow;
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
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
    }
}
