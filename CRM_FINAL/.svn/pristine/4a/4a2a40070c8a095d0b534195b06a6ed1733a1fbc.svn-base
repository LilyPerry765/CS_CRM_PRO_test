using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CRM.Application.Codes;
using System.Data;
using Stimulsoft.Report.Dictionary;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationOpticalToNormalMDFForm.xaml
    /// </summary>
    public partial class TranslationOpticalToNormalMDFForm : Local.RequestFormBase
    {
        private long _requestID;
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        TranslationOpticalCabinetToNormal _translationOpticalCabinetToNormal { get; set; }

        Request _reqeust { get; set; }

        ObservableCollection<TranslationOpticalCabinetToNormalInfo> cabinetInputsList;



        public TranslationOpticalToNormalMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }

        public TranslationOpticalToNormalMDFForm(long requestID)
            : this()
        {
            this._requestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public override bool Save()
        {
            try
            {
                DateTime currentDate = DB.GetServerDate();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _translationOpticalCabinetToNormal = AccomplishmentGroupBox.DataContext as TranslationOpticalCabinetToNormal;
                    _translationOpticalCabinetToNormal.Detach();
                    DB.Save(_translationOpticalCabinetToNormal, false);

                    ts.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;

        }

        public override bool Forward()
        {


            using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
            {
                try
                {

                    Save();
                    this.RequestID = _requestID;
                    if (IsSaveSuccess)
                    {
                        IsForwardSuccess = true;
                    }

                    ts1.Complete();
                }
                catch (Exception ex)
                {

                    ShowErrorMessage("خطا در ذخیره سازی", ex);
                }
            }

            return IsForwardSuccess;

        }
        public override bool Deny()
        {

            try
            {
                base.RequestID = _requestID;
                if (_translationOpticalCabinetToNormal.CompletionDate == null)
                {
                    IsRejectSuccess = true;
                }
                else
                {
                    IsRejectSuccess = false;
                    Folder.MessageBox.ShowWarning("بعد از تایید نهایی امکان رد درخواست نمی باشد.");
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;
        }

        public void LoadData()
        {
            _translationOpticalCabinetToNormal = Data.TranslationOpticalCabinetToNormalDB.GetTranslationOpticalCabinetToNormal(_requestID);
            if (_translationOpticalCabinetToNormal.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationOpticalCabinetToNormal.MDFAccomplishmentDate = dateTime.Date;
                _translationOpticalCabinetToNormal.MDFAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
            _reqeust = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;

            cabinetInputsList = new ObservableCollection<TranslationOpticalCabinetToNormalInfo>(Data.TranslationOpticalCabinetToNormalDB.GetEquivalentCabinetInputs(_translationOpticalCabinetToNormal));
            TelItemsDataGrid.DataContext = cabinetInputsList;

            AccomplishmentGroupBox.DataContext = _translationOpticalCabinetToNormal;
        }



        #region Filters
        private bool PredicateFilters(object obj)
        {
            TranslationOpticalCabinetToNormalInfo checkableObject = obj as TranslationOpticalCabinetToNormalInfo;
            return checkableObject.OldTelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

        private void FilterTelephonNoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(TelItemsDataGrid.ItemsSource);
            if (view != null)
            {
                view.Filter = new Predicate<object>(PredicateFilters);
            }
        }

        #endregion Filters


        public override bool Print()
        {
            try
            {
                List<long> requestsID = new List<long>();
                requestsID.Add(_requestID);

                //متغیر هایی که در الگوی گزارش مورد استفاده قرار خواهند گرفت
                DateTime now = DB.GetServerDate();
                StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(now, Helper.DateStringType.Short));
                StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(now, Helper.DateStringType.Time));
                StiVariable cityVariable = new StiVariable("City", "City", DB.PersianCity);

                //دیتای مورد نیاز برای ایجاد گزارش
                var result = TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionInfoSummariesByRequestIDs(requestsID);

                //تنظیمات برای نمایش گزارش
                CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.TranslationOpticalCabinetToNormallMDFWiringReport, true, dateVariable, timeVariable, cityVariable);

                IsPrintSuccess = true;
            }
            catch (Exception)
            {
                IsPrintSuccess = false;
            }
            return IsPrintSuccess;
        }

        private void SendToPrintTranslationOpticalCabinetToNormallMDFReport(ObservableCollection<TranslationOpticalCabinetToNormalInfo> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationOpticalCabinetToNormallNetwrokWiringReport);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);



            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TelItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TelItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void PrintItem_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            DataSet data = cabinetInputsList.ToDataSet("Result", TelItemsDataGrid);
            CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);

            this.Cursor = Cursors.Arrow;
        }
        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns);
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(TelItemsDataGrid.Columns);
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



    }
}
