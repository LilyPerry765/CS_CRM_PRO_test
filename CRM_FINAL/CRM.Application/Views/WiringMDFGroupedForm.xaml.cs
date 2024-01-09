using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using CRM.Application.Codes;
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for WiringMDFGroupedForm.xaml
    /// </summary>
    public partial class WiringMDFGroupedForm : Local.RequestFormBase
    {
        private long? _subID;
        List<long> _RequestIDs = new List<long>();
        List<Request> _Requests = new List<Request>();
        MDFWiring _MDFWiring = new MDFWiring();
        ObservableCollection<WiringGroupedInfo> _InvestigatePossibilityInfos = new ObservableCollection<WiringGroupedInfo>();
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        public WiringMDFGroupedForm()
        {
            InitializeComponent();
            Initialize();
        }
        public WiringMDFGroupedForm(List<long> ids, long? subID)
            : this()
        {
            this._subID = subID;
            _RequestIDs = ids;
        }
        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print };
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {

            if (_RequestIDs.Count() <= 0) return;

            DateTime currentDateTime = DB.GetServerDate();

            _Requests = RequestDB.GetRequestListByID(_RequestIDs);
            _InvestigatePossibilityInfos = new ObservableCollection<WiringGroupedInfo>(InvestigatePossibilityDB.GetInvestigatePossibilityInfoByRequestIDs(_RequestIDs));
            StatusComboBox.SelectedValue = _Requests.Take(1).SingleOrDefault().StatusID;
            if(_RequestIDs.Count() == 1)
            {
                _MDFWiring = MDFWiringDB.GetMDFWiringByRequestID((long)_RequestIDs.Take(1).SingleOrDefault());

            }
            if (_MDFWiring == null || _MDFWiring.ID == 0)
            {
                _MDFWiring = new MDFWiring();
                _MDFWiring.MDFWiringHour = currentDateTime.ToString("hh:mm:ss");
                _MDFWiring.MDFWiringDate = currentDateTime;
            }
            StatusComboBox.ItemsSource = DB.GetStepStatus(_Requests.Take(1).SingleOrDefault().RequestTypeID, this.currentStep);

            switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    break;
            }

            TelItemsDataGrid.ItemsSource = _InvestigatePossibilityInfos;
            
            MDFWiringInfoGroupBox.DataContext = _MDFWiring;
        }

        public override bool Save()
        {



            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }

            try
            {

                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
                    {
                        case (byte)DB.RequestType.Dayri:
                        case (byte)DB.RequestType.Reinstall:
                            {
                                SaveDayeri();
                            }
                            break;


                    }

                    ts2.Complete();
                }
                ShowSuccessMessage("ذخیره انجام شد");

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }

            base.Confirm();

            return IsSaveSuccess;
        }

        public override bool Forward()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    RequestIDs = _RequestIDs;
                    Save();
                    if (IsSaveSuccess)
                    {
                        if (StatusComboBox.SelectedValue != null)
                        {
                            Status Status = Data.StatusDB.GetStatueByStatusID(_Requests.Take(1).SingleOrDefault().StatusID);
                            if (Status.StatusType == (byte)DB.RequestStatusType.Completed)
                            {

                                switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
                                {
                                    case (byte)DB.RequestType.Dayri:
                                    case (byte)DB.RequestType.Reinstall:
                                        {
                                            ForwardDayeri();
                                        }
                                        break;


                                }


                            }

                        }
                        IsForwardSuccess = true;
                    }
                    else
                    {
                        IsForwardSuccess = false;
                    }

                    ts.Complete();
                }
           }
            catch(Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
            }


                  return IsForwardSuccess;

        }


        public override bool Deny()
        {

            try
            {
                base.RequestIDs = _RequestIDs;
                switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Reinstall:
                        DayeriDeny();
                        break;
                    case (byte)DB.RequestType.Dischargin:

                        break;
                    case (byte)DB.RequestType.ChangeNo:

                        break;
                    case (byte)DB.RequestType.RefundDeposit:

                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:

                        break;
                    case (byte)DB.RequestType.SpecialWire:

                        break;
                    case (byte)DB.RequestType.E1:
                        break;
                    case (byte)DB.RequestType.VacateSpecialWire:

                        break;
                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        break;

                }
                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;
        }

        private void DayeriDeny()
        {
        }

        private void ForwardDayeri()
        {
            DateTime currentDateTime = DB.GetServerDate();

            List<Bucht> bucht = BuchtDB.GetBuchtByIDs(_InvestigatePossibilityInfos.Select(t=>(long)t.BuchtID).ToList());
            bucht.ForEach(t => { t.Status = (byte)DB.BuchtStatus.Connection; t.Detach(); });
            DB.UpdateAll(bucht);


                List<MDFWiring> InserMDFWirings = new List<MDFWiring>();

                List<MDFWiring> MDFWiringItems = MDFWiringDB.GetMDFWiringByRequestIDs(_RequestIDs);
                MDFWiringItems.ForEach(t =>
                {
                    t.MDFWiringDate = (MDFWiringInfoGroupBox.DataContext as MDFWiring).MDFWiringDate;
                    t.MDFWiringHour = (MDFWiringInfoGroupBox.DataContext as MDFWiring).MDFWiringHour;
                    t.MDFComment = (MDFWiringInfoGroupBox.DataContext as MDFWiring).MDFComment;
                    t.Detach();
                });
                DB.UpdateAll(MDFWiringItems);


                _InvestigatePossibilityInfos.Where(t=>!MDFWiringItems.Select(t2=>t2.ID).Contains((long)t.RequestID)).ToList().ForEach(t =>
                      {
                            MDFWiring MDFWiring = new Data.MDFWiring();
                            MDFWiring.ID = (long)t.RequestID;
                            MDFWiring.MDFWiringDate = (MDFWiringInfoGroupBox.DataContext as MDFWiring).MDFWiringDate;
                            MDFWiring.MDFWiringHour = (MDFWiringInfoGroupBox.DataContext as MDFWiring).MDFWiringHour;
                            MDFWiring.MDFComment = (MDFWiringInfoGroupBox.DataContext as MDFWiring).MDFComment;
                            MDFWiring.InsertDate = currentDateTime;
                            MDFWiring.Detach();
                            InserMDFWirings.Add(MDFWiring);
                      });

                DB.SaveAll(InserMDFWirings);
                
            }
        private void SaveDayeri()
        {
                if (StatusComboBox.SelectedValue != null)
                {


                    int status = (int)StatusComboBox.SelectedValue;
                    _Requests.ForEach(t => 
                            { 
                                t.StatusID = status;
                                t.Detach();
                            
                            });


                    DB.UpdateAll(_Requests);

                }
                else
                {
                    throw new Exception("لطفا وضعیت را انتخاب کنید");
                }
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

                DataSet data = _InvestigatePossibilityInfos.ToList().ToDataSet("Result", TelItemsDataGrid);
                CRM.Application.Codes.Print.DynamicPrintV2(data,_title ,dataGridSelectedIndexs, _groupingColumn);

            this.Cursor = Cursors.Arrow;
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

        public override bool Print()
        {
            this.Cursor = Cursors.Wait;
            try
            {
                    DataSet data = _InvestigatePossibilityInfos.ToDataSet("Result", TelItemsDataGrid);
                    CRM.Application.Codes.Print.DynamicPrintV2(data);
                    IsPrintSuccess = true;
            }
            catch(Exception ex)
            {
                IsPrintSuccess = false;
            }
            this.Cursor = Cursors.Arrow;
            return IsPrintSuccess;
        }





        #region Filters
        private bool PredicateFilters(object obj)
        {
            WiringGroupedInfo checkableObject = obj as WiringGroupedInfo;
            return checkableObject.TelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
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

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            if (!CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns))
            {
                Folder.MessageBox.ShowError("خطا در ذخیره اطلاعات");
            }
        }
    }
}
