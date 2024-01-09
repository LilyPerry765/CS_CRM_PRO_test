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
using CRM.Application.UserControls;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class Failure117UBList : Local.TabWindow
    {
        #region Propperties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public Failure117UBList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {

        }

        private void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? fromDate = null;
                if (FromDate.SelectedDate == null)
                    fromDate = DateTime.Now.AddDays(-1);
                else
                    fromDate = FromDate.SelectedDate;

                DateTime? toDate = null;
                if (ToDate.SelectedDate == null)
                    toDate = DateTime.Now;
                else
                    toDate = ToDate.SelectedDate.Value.AddDays(1);

                List<long> telephonenoList = Failure117DB.GetFailure117UBList(fromDate, toDate);
                List<Failure117RequestPrintInfo> failureList = new List<Failure117RequestPrintInfo>();
                Failure117RequestPrintInfo failure = new Failure117RequestPrintInfo();

                foreach (long item in telephonenoList)
                {
                    Failure117RequestPrintInfo bucht = Failure117DB.GetFailure117RequestPrintbyTelephoneNos(item);
                    failure = new Failure117RequestPrintInfo();

                    failure.TelephoneNo = item.ToString();
                    failure.Date = Failure117DB.GetFailure117LastRequestDate(item);
                    failure.Count = Failure117DB.GetFailure117UBCount(item);

                    if (bucht != null)
                    {
                        failure.Bucht = bucht.Bucht;
                        failure.BuchtPCM = bucht.BuchtPCM;
                    }
                    else
                    {
                        failure.Bucht = "-";
                        failure.BuchtPCM = "-";
                    }

                    failureList.Add(failure);
                }

                ItemsDataGrid.ItemsSource = failureList;
            }
            catch (Exception ex)
            {

            }
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            FromDate.SelectedDate = null;
            ToDate.SelectedDate = null;

            Search(null, null);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                DateTime? fromDate = null;
                if (FromDate.SelectedDate == null)
                    fromDate = DateTime.Now.AddDays(-1);
                else
                    fromDate = FromDate.SelectedDate;

                DateTime? toDate = null;
                if (ToDate.SelectedDate == null)
                    toDate = DateTime.Now;
                else
                    toDate = ToDate.SelectedDate.Value.AddDays(1);

                List<long> telephonenoList = Failure117DB.GetFailure117UBList(fromDate, toDate);
                List<Failure117RequestPrintInfo> failureList = new List<Failure117RequestPrintInfo>();
                Failure117RequestPrintInfo failure = new Failure117RequestPrintInfo();

                foreach (long item in telephonenoList)
                {
                    Failure117RequestPrintInfo bucht = Failure117DB.GetFailure117RequestPrintbyTelephoneNos(item);
                    failure = new Failure117RequestPrintInfo();

                    failure.TelephoneNo = item.ToString();
                    failure.Date = Failure117DB.GetFailure117LastRequestDate(item);
                    failure.Count = Failure117DB.GetFailure117UBCount(item);

                    if (bucht != null)
                    {
                        failure.Bucht = bucht.Bucht;
                        failure.BuchtPCM = bucht.BuchtPCM;
                    }
                    else
                    {
                        failure.Bucht = "-";
                        failure.BuchtPCM = "-";
                    }

                    failureList.Add(failure);
                }

                DataSet data = failureList.ToDataSet("Result", ItemsDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی ");
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

