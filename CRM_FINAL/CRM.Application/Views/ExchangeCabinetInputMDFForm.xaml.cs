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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangeCabinetInputMDFForm.xaml
    /// </summary>
    public partial class ExchangeCabinetInputMDFForm : Local.RequestFormBase
    {
        private long _requestID;
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        ExchangeCabinetInput _exchangeCabinetInput { get; set; }

        Request _reqeust { get; set; }

        ObservableCollection<ExchangeCabinetInputRequestReportInfo> cabinetInputsList;

        public ExchangeCabinetInputMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }

        public ExchangeCabinetInputMDFForm(long requestID)
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

                    _exchangeCabinetInput = AccomplishmentGroupBox.DataContext as ExchangeCabinetInput;
                    _exchangeCabinetInput.Detach();
                    DB.Save(_exchangeCabinetInput, false);

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

        public void LoadData()
        {
            _exchangeCabinetInput = Data.ExchangeCabinetInputDB.GetExchangeCabinetInputByRequestID(_requestID);
            if (_exchangeCabinetInput.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeCabinetInput.MDFAccomplishmentDate = dateTime.Date;
                _exchangeCabinetInput.MDFAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
            _reqeust = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;

            cabinetInputsList = new ObservableCollection<ExchangeCabinetInputRequestReportInfo>(Data.ExchangeCabinetInputDB.GetExchangeCabinetInputInfo(_exchangeCabinetInput));
            TelItemsDataGrid.DataContext = cabinetInputsList;

            AccomplishmentGroupBox.DataContext = _exchangeCabinetInput;
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

                DataSet data = cabinetInputsList.ToDataSet("Result", TelItemsDataGrid);
                CRM.Application.Codes.Print.DynamicPrintV2(data , _title, dataGridSelectedIndexs, _groupingColumn);
                    IsPrintSuccess = true;

            }
            catch (Exception)
            {
                IsPrintSuccess = true;
            }
            return IsPrintSuccess;
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


        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns);
        }


        public override bool Deny()
        {

            try
            {
                base.RequestID = _requestID;
                if (_exchangeCabinetInput.CompletionDate == null)
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


    }
}
