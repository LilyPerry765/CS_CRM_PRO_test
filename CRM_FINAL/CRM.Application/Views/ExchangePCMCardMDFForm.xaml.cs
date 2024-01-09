using CRM.Data;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangePCMCardMDFForm.xaml
    /// </summary>
    public partial class ExchangePCMCardMDFForm : Local.RequestFormBase
    {
        private long requestID;
        Request _reqeust { get; set; }

        List<ExchangeBrokenPCM> _exchangeBrokenPCMs { get; set; }
        List<PCMBuchtTelephonReportInfo> _PCMBuchtTelephonInfo { get; set; }
        
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;


        // بوخت های پی سی ام 
        List<Bucht> _oldPCMBuchtList { get; set; }
        List<Bucht> _newPCMBuchtList { get; set; }

        // اتصالی های پست
        List<PostContact> _postContact { get; set; }
        public ExchangePCMCardMDFForm()
        {
            InitializeComponent();
            Initialize();


        }

        private void Initialize()
        {

            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }

        public ExchangePCMCardMDFForm(long requestID) :this()
        {
            this.requestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {

            _PCMBuchtTelephonInfo = ExchangeBrokenPCMDB.GetExchangeBrokenPCMInfoByRequestID(this.requestID);
            _reqeust = RequestDB.GetRequestByID(this.requestID);



            TelItemsDataGrid.ItemsSource = _PCMBuchtTelephonInfo;
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

        public override bool Print()
        {
            this.Cursor = Cursors.Wait;
            DataSet data = _PCMBuchtTelephonInfo.ToDataSet("Result", TelItemsDataGrid);
            CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);

            this.Cursor = Cursors.Arrow;

            IsPrintSuccess = true;

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


        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns);
        }

        public override bool Deny()
        {

            try
            {
                base.RequestID = this.requestID;
                if (_exchangeBrokenPCMs.All(t=> t.CompletionDate == null))
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

        public override bool Forward()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    this.RequestID = this.requestID;
                     DoWork();
                       IsForwardSuccess = true;
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsForwardSuccess;
        }

        private void DoWork()
        {

            DateTime currentDate = DB.GetServerDate();
            _exchangeBrokenPCMs = ExchangeBrokenPCMDB.GetExchangeBrokenPCMByRequestID(this.requestID);

            List<RequestLog> requestLogs = new List<RequestLog>();

            _oldPCMBuchtList = Data.BuchtDB.GetBuchtByIDs(_exchangeBrokenPCMs.Select(t => (long)t.OldBuchtID).ToList());
            _newPCMBuchtList = Data.BuchtDB.GetBuchtByIDs(_exchangeBrokenPCMs.Select(t => (long)t.NewBuchtID).ToList());

            _postContact = Data.PostContactDB.GetPostContactByIDs(_oldPCMBuchtList.Select(t => (long)t.ConnectionID).ToList());

            string fromMUID = PCMDB.GetMUIDByPCMID(_exchangeBrokenPCMs.Take(1).SingleOrDefault().OldPCMID);
            string toMUID = PCMDB.GetMUIDByPCMID(_exchangeBrokenPCMs.Take(1).SingleOrDefault().NewPCMID);

            _exchangeBrokenPCMs.ForEach(t =>
            {
                if(t.CompletionDate == null)
                {


                    // بازگشت وضعیت اتصالی پست از رزرو
                    _postContact.Where(t2 => t2.ID == _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().ConnectionID).SingleOrDefault().Status = t.OldPostContactStatus;

                    // بازگشت وضعیت بوخت از رزرو
                    _newPCMBuchtList.Where(t2 => t2.ID == t.NewBuchtID).SingleOrDefault().Status = t.OldBuchtStatus;

                    // انتقال پی سی ام
                    _newPCMBuchtList.Where(t2 => t2.ID == t.NewBuchtID).SingleOrDefault().SwitchPortID   = _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().SwitchPortID;
                    _newPCMBuchtList.Where(t2 => t2.ID == t.NewBuchtID).SingleOrDefault().CabinetInputID = _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().CabinetInputID;
                    _newPCMBuchtList.Where(t2 => t2.ID == t.NewBuchtID).SingleOrDefault().ConnectionID = _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().ConnectionID;
                    _newPCMBuchtList.Where(t2 => t2.ID == t.NewBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                    _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().Status = (int)DB.BuchtStatus.ConnectedToPCM;
                    _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().SwitchPortID = null;
                    _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().ConnectionID = null;
                    _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;
                    _oldPCMBuchtList.Where(t3 => t3.ID == t.OldBuchtID).SingleOrDefault().CabinetInputID = null;


                    if (t.TelephoneNo != null)
                    {
                        RequestLog requestLog = new RequestLog();
                        requestLog.RequestID = this.requestID;
                        requestLog.IsReject = false;
                        requestLog.RequestTypeID = (int)DB.RequestType.BrokenPCM;
                        requestLog.UserID = DB.currentUser.ID;
                        requestLog.Date = currentDate;
                        requestLog.TelephoneNo = t.TelephoneNo;

                        Data.Schema.ExchangePCMCardInfo exchangePCMCardInfo = new Data.Schema.ExchangePCMCardInfo();
                        exchangePCMCardInfo.TelephoneNo = t.TelephoneNo ?? 0;
                        exchangePCMCardInfo.FromMuID = fromMUID;
                        exchangePCMCardInfo.ToMUID = toMUID;
                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ExchangePCMCardInfo>(exchangePCMCardInfo, true));
                        requestLogs.Add(requestLog);
                    }

                    t.CompletionDate = currentDate;
                }
            });


            PCM oldPCM = PCMDB.GetPCMByID(_exchangeBrokenPCMs.Take(1).SingleOrDefault().OldPCMID);
            oldPCM.Status = (int)DB.PCMStatus.Destruction;

            PCM newPCM = PCMDB.GetPCMByID(_exchangeBrokenPCMs.Take(1).SingleOrDefault().NewPCMID);
            newPCM.Status = (int)DB.PCMStatus.Connection;

            Malfuction malfuction = new Malfuction();
            malfuction.PCMID = oldPCM.ID;
            malfuction.TimeMalfunction = currentDate.ToShortTimeString();
            malfuction.DateMalfunction = currentDate;


            Bucht PCMCustomerSideBucht = BuchtDB.GetPCMCabinetInputBucht((long)_newPCMBuchtList.Take(1).SingleOrDefault().CabinetInputID);

            PCMCustomerSideBucht.BuchtIDConnectedOtherBucht = _newPCMBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault().ID;

            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
            {

                _postContact.ForEach(t => t.Detach());
                DB.UpdateAll(_postContact);
                
                _newPCMBuchtList.ForEach(t => t.Detach());
                DB.UpdateAll(_newPCMBuchtList);

                _oldPCMBuchtList.ForEach(t => t.Detach());
                DB.UpdateAll(_oldPCMBuchtList);

                _exchangeBrokenPCMs.ForEach(t => t.Detach());
                DB.UpdateAll(_exchangeBrokenPCMs);

                oldPCM.Detach();
                DB.Save(oldPCM);

                newPCM.Detach();
                DB.Save(newPCM);

                malfuction.Detach();
                DB.Save(malfuction);

                PCMCustomerSideBucht.Detach();
                DB.Save(PCMCustomerSideBucht);


                requestLogs.ForEach(t => t.Detach());
                DB.SaveAll(requestLogs);
                

                ts3.Complete();
            }
        }

        #region Filters
        private bool PredicateFilters(object obj)
        {
            PCMBuchtTelephonReportInfo checkableObject = obj as PCMBuchtTelephonReportInfo;
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


    }
}
