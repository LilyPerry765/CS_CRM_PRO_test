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
using System.Xml.Linq;
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationOpticalToNormalNetworkForm.xaml
    /// </summary>
    public partial class ExchangeCabinetInputNetworkForm : Local.RequestFormBase
    {

        #region properties
        private long _requestID;
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        ExchangeCabinetInput _exchangeCabinetInput { get; set; }

        Request _reqeust { get; set; }

        List<ExchangeCabinetInputRequestReportInfo> cabinetInputsList;

        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _oldBuchtListPCM { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        List<PostContact> _oldPostContactList { get; set; }

        List<Post> _oldAllPostList { get; set; }
        List<PostContact> _oldPostContactListPCM { get; set; }
        List<PostContact> _newPostContactList { get; set; }

        List<Telephone> _oldTelephones { get; set; }

        List<TelephonCustomer> _telephonCustomer { get; set; }

        #endregion properties

        #region constractor
        public ExchangeCabinetInputNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ExchangeCabinetInputNetworkForm(long requestID)
            :this()
        {
            this._requestID = requestID;
        }

        private void Initialize()
        {

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny};
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            _exchangeCabinetInput = Data.ExchangeCabinetInputDB.GetExchangeCabinetInputByRequestID(_requestID);
            if (_exchangeCabinetInput.NetworkAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeCabinetInput.NetworkAccomplishmentDate = dateTime.Date;
                _exchangeCabinetInput.NetworkAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }

            _reqeust = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;

            cabinetInputsList = Data.ExchangeCabinetInputDB.GetExchangeCabinetInputInfo(_exchangeCabinetInput);
            TelItemsDataGrid.DataContext = cabinetInputsList;

            if(_exchangeCabinetInput.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Slight)
             {
              ToPostNumberTextColumn.Visibility = Visibility.Visible;

              ToPostConntactNumberTextColumn.Visibility = Visibility.Visible;

                _oldPostContactList = new List<PostContact>();
                _newPostContactList = new List<PostContact>();
             }

            AccomplishmentGroupBox.DataContext = _exchangeCabinetInput;
        }

        #endregion constractor

        #region Filters
        private bool PredicateFilters(object obj)
        {
            ExchangeCabinetInputRequestReportInfo checkableObject = obj as ExchangeCabinetInputRequestReportInfo;
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

        #region action

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
               

                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _exchangeCabinetInput = AccomplishmentGroupBox.DataContext as ExchangeCabinetInput;
                    _exchangeCabinetInput.Detach();
                    DB.Save(_exchangeCabinetInput, false);

                    IsSaveSuccess = true;
                    ts2.Complete();
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

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    Save();
                    this.RequestID = _reqeust.ID;
                    if (IsSaveSuccess)
                    {
                        DoWork();
                        IsForwardSuccess = true;
                    }
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

        public override bool Print()
        {

                DataSet data = cabinetInputsList.ToDataSet("Result", TelItemsDataGrid);
                CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);
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

        #endregion action

        #region method
        private void ExitReserveCabinet(ExchangeCabinetInput exchangeCabinetInput)
        {
            using (TransactionScope ts4 = new TransactionScope(TransactionScopeOption.Required))
            {
                if (exchangeCabinetInput.Type == (byte)DB.ExchangeCabinetInputType.General)
                {
                    Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(exchangeCabinetInput.OldCabinetID);
                    oldCabinet.Status = (int)DB.CabinetStatus.Install;
                    oldCabinet.Detach();

                    Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(exchangeCabinetInput.NewCabinetID);
                    newCabinet.Status = (int)DB.CabinetStatus.Install;
                    newCabinet.Detach();

                    DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });
                }
                else if (exchangeCabinetInput.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Slight)
                {
                    List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(cabinetInputsList.Select(t => (long)t.NewCabinetInputID).ToList());
                    cabinetInputs.ToList().ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.healthy; t.Detach(); });
                    DB.UpdateAll(cabinetInputs);
                }

                ts4.Complete();
            }
        }

        private void DoWork()
        {
            try
            {


                if (_exchangeCabinetInput.CompletionDate == null)
                {
                    DateTime currentDataTime = DB.GetServerDate();
                    List<RequestLog> requestLogs = new List<RequestLog>();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {
                        // change waiting list
                        if (_exchangeCabinetInput.TransferWaitingList)
                        {

                            if (_exchangeCabinetInput.Type == (int)DB.ExchangeCabinetInputType.Post)
                            {
                                List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo = new List<InvestigatePossibilityWaitngListChangeInfo>();

                                cabinetInputsList.ForEach(c =>
                                {
                                    if (!investigatePossibilityWaitngListChangeInfo.Any(t => t.newPostID == c.NewPostID))
                                    {
                                        investigatePossibilityWaitngListChangeInfo.Add(
                                                new InvestigatePossibilityWaitngListChangeInfo
                                                {
                                                    newCabinetID = _exchangeCabinetInput.NewCabinetID,
                                                    newPostID = (int?)c.NewPostID,
                                                    oldCabinetID = _exchangeCabinetInput.OldCabinetID,
                                                    oldPostID = (int)c.OldPostID,
                                                }
                                        );
                                    }
                                });

                                InvestigatePossibilityDB.ChangePostInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);
                            }
                            else if (_exchangeCabinetInput.Type == (int)DB.ExchangeCabinetInputType.General)
                            {
                                List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo = new List<InvestigatePossibilityWaitngListChangeInfo>();

                                cabinetInputsList.ForEach(c =>
                                {
                                    if (!investigatePossibilityWaitngListChangeInfo.Any(t => t.newPostID == c.NewPostID))
                                    {
                                        investigatePossibilityWaitngListChangeInfo.Add(
                                                new InvestigatePossibilityWaitngListChangeInfo
                                                {
                                                    newCabinetID = _exchangeCabinetInput.NewCabinetID,
                                                    newPostID = (int?)c.NewPostID,
                                                    oldCabinetID = _exchangeCabinetInput.OldCabinetID,
                                                    oldPostID = (int)c.OldPostID,
                                                }
                                        );
                                    }
                                });
                                InvestigatePossibilityDB.ChangeCabinetInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);
                            }

                        }
                        //


                        // Transfer Broken PostContact
                        if (_exchangeCabinetInput.TransferBrokenPostContact && _exchangeCabinetInput.IsChangePost)
                        {

                            List<PostContactInfo> BrokenOldPostContactList = Data.PostContactDB.GetBrokenPostContactByCabinetID(_exchangeCabinetInput.OldCabinetID, cabinetInputsList.Select(t => (int)t.OldPostID).Distinct().ToList());
                            List<PostContact> newBrokenPostContactList = Data.PostContactDB.GetFreePostContactByCabinetID(_exchangeCabinetInput.NewCabinetID, cabinetInputsList.Select(t => (int)t.NewPostID).Distinct().ToList());
                            BrokenOldPostContactList.ForEach(t =>
                            {
                                int postID = (int)cabinetInputsList.Where(t2 => t2.OldPostID == t.PostID).Take(1).SingleOrDefault().NewPostID;
                                if (newBrokenPostContactList.Any(t3 => t3.PostID == postID && t3.ConnectionNo == t.ConnectionNo))
                                {
                                    newBrokenPostContactList.Where(t3 => t3.PostID == postID && t3.ConnectionNo == t.ConnectionNo).SingleOrDefault().Status = t.Status;
                                }

                            });

                            newBrokenPostContactList.ForEach(t => t.Detach());
                            DB.UpdateAll(newBrokenPostContactList);
                        }
                        //


                        _oldPostContactList = Data.PostContactDB.GetPostContactByIDs(cabinetInputsList.Where(t => t.OldPostContactID != null).Select(t => (long)t.OldPostContactID).ToList());

                        _oldAllPostList = Data.PostDB.GetPostByIDs(cabinetInputsList.Select(t => (int)t.OldPostID).Distinct().ToList());

                        _newPostContactList = Data.PostContactDB.GetPostContactByIDs(cabinetInputsList.Where(t => t.NewPostContactID != null).Select(t => (long)t.NewPostContactID).ToList());

                        _oldBuchtList = Data.BuchtDB.GetBuchtByIDs(cabinetInputsList.Where(t => t.OldBuchtID != null).Select(t => (long)t.OldBuchtID).ToList());
                        _oldBuchtListPCM = Data.BuchtDB.GetPCMBuchtByCabinetInputID(_oldBuchtList.Where(t => t.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).Select(t => (long)t.CabinetInputID).ToList());

                        _oldPostContactListPCM = Data.PostContactDB.GetPCMPostContactByPostIDs(_oldPostContactList.Select(t => t.PostID).ToList());
                        _newBuchtList = Data.BuchtDB.GetBuchtByIDs(cabinetInputsList.Where(t => t.NewBuchtID != null).Select(t => (long)t.NewBuchtID).ToList());



                        _oldTelephones = Data.TelephoneDB.GetTelephones(cabinetInputsList.Where(t => t.OldTelephonNo != null).Select(t => (long)t.OldTelephonNo).ToList()).Union(Data.TelephoneDB.GetTelephoneBySwitchIDs(_oldBuchtListPCM.Where(t2 => t2.SwitchPortID != null).Select(t2 => (int)t2.SwitchPortID).ToList())).Distinct().ToList();
                        _telephonCustomer = Data.CustomerDB.GetCustomerByTelephones(_oldTelephones);

                        cabinetInputsList = cabinetInputsList.Where(t => t.OldBuchtID != null).ToList();
                        int count = cabinetInputsList.Count();

                        for (int i = 0; i < count; i++)
                        {
                            RequestLog requestLog = new RequestLog();
                            ExchangeCabinetInputRequestReportInfo item = cabinetInputsList[i];

                            if (item.OldBuchtStatus != (int)DB.BuchtStatus.AllocatedToInlinePCM)
                            {
                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().SwitchPortID = item.OldSwitchPortID;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().SwitchPortID = null;

                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;
                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().Status = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.Free;

                                if (_exchangeCabinetInput.IsChangePost)
                                {
                                    _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ConnectionID = item.NewPostContactID;
                                    _newPostContactList.Where(t => t.ID == item.NewPostContactID).SingleOrDefault().Status = _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status;
                                    _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;
                                    if (!(_oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status == (byte)DB.PostContactStatus.PermanentBroken ))
                                    {
                                        _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;
                                    }
                                }
                                else
                                {
                                    _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ConnectionID = item.OldPostContactID;
                                    _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;
                                }

                                // create Log

                                requestLog.IsReject = false;
                                requestLog.RequestID = _reqeust.ID;
                                requestLog.RequestTypeID = _reqeust.RequestTypeID;
                                requestLog.TelephoneNo = item.OldTelephonNo;
                                requestLog.CustomerID = _telephonCustomer.Where(t => t.TelephonNo == (long)item.OldTelephonNo).Select(t => t.Customer.CustomerID).SingleOrDefault();
                                requestLog.UserID = DB.CurrentUser.ID;

                                Data.Schema.ExchangeCabinetInputLog exchangeCabinetInputLog = new Data.Schema.ExchangeCabinetInputLog();
                                exchangeCabinetInputLog.NewBucht = item.NewConnectionNo;
                                exchangeCabinetInputLog.OldBucht = item.OldConnectionNo;
                                exchangeCabinetInputLog.NewCabinet = item.NewCabinetNumber ?? 0;
                                exchangeCabinetInputLog.OldCabinet = (int)item.OldCabinetNumber;
                                exchangeCabinetInputLog.NewCabinetInput = item.NewCabinetInputNumber ?? 0;
                                exchangeCabinetInputLog.OldCabinetInput = item.OldCabinetInputNumber ?? 0;
                                exchangeCabinetInputLog.NewPost = item.NewPostNumber ?? 0;
                                exchangeCabinetInputLog.OldPost = item.OldPostNumber ?? 0;
                                exchangeCabinetInputLog.NewPostContact = item.NewPostConntactNumber ?? 0;
                                exchangeCabinetInputLog.OldPostContact = item.OldPostContactNumber ?? 0;
                                exchangeCabinetInputLog.OldTelephone = item.OldTelephonNo ?? 0;

                                requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ExchangeCabinetInputLog>(exchangeCabinetInputLog, true));

                                requestLog.Date = currentDataTime;
                                requestLog.Detach();

                            }
                            else if (item.OldBuchtStatus == (int)DB.BuchtStatus.AllocatedToInlinePCM)
                            {

                                #region pcm
                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().Status = (int)DB.BuchtStatus.AllocatedToInlinePCM;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status = (int)DB.BuchtStatus.Free;

                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht;

                                if (_exchangeCabinetInput.IsChangePost)
                                {
                                    _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ConnectionID = item.NewPostContactID;
                                    _oldBuchtListPCM.Where(t => t.CabinetInputID == item.OldCabinetInputID && t.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().ConnectionID = item.NewPostContactID;
                                    _newPostContactList.Where(t => t.ID == item.NewPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.NoCableConnection;
                                    _newPostContactList.Where(t => t.ID == item.NewPostContactID).SingleOrDefault().ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;

                                    _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;
                                    _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().ConnectionType = (byte)DB.PostContactConnectionType.Noraml;

                                    _oldPostContactListPCM.Where(t => t.PostID == item.OldPostID && t.ConnectionNo == item.OldPostContactNumber).ToList().ForEach(t => { t.PostID = (int)item.NewPostID; t.ConnectionNo = (int)item.NewPostConntactNumber; });
                                }
                                else
                                {
                                    _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ConnectionID = item.OldPostContactID;
                                    _oldBuchtListPCM.Where(t => t.CabinetInputID == item.OldCabinetInputID && t.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().ConnectionID = item.OldPostContactID;

                                }


                                _oldBuchtListPCM.Where(t => t.CabinetInputID == item.OldCabinetInputID && t.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().BuchtIDConnectedOtherBucht = _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ID;

                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;

                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;

                                _oldBuchtListPCM.Where(t => t.CabinetInputID == item.OldCabinetInputID).ToList().ForEach(t =>
                                {

                                    // create Log

                                    requestLog.IsReject = false;
                                    requestLog.RequestID = _reqeust.ID;
                                    requestLog.RequestTypeID = _reqeust.RequestTypeID;
                                    requestLog.TelephoneNo = item.OldTelephonNo;
                                    requestLog.CustomerID = _telephonCustomer.Where(t2 => t2.TelephonNo == (long)item.OldTelephonNo).Select(t2 => t2.Customer.CustomerID).SingleOrDefault();
                                    requestLog.UserID = DB.CurrentUser.ID;

                                    Data.Schema.ExchangeCabinetInputLog exchangeCabinetInputLog = new Data.Schema.ExchangeCabinetInputLog();
                                    exchangeCabinetInputLog.NewBucht = item.NewConnectionNo;
                                    exchangeCabinetInputLog.OldBucht = item.OldConnectionNo;
                                    exchangeCabinetInputLog.NewCabinet = item.NewCabinetNumber ?? 0;
                                    exchangeCabinetInputLog.OldCabinet = (int)item.OldCabinetNumber;
                                    exchangeCabinetInputLog.NewCabinetInput = item.NewCabinetInputNumber ?? 0;
                                    exchangeCabinetInputLog.OldCabinetInput = item.OldCabinetInputNumber ?? 0;
                                    exchangeCabinetInputLog.NewPost = item.NewPostNumber ?? 0;
                                    exchangeCabinetInputLog.OldPost = item.OldPostNumber ?? 0;
                                    exchangeCabinetInputLog.NewPostContact = item.NewPostConntactNumber ?? 0;
                                    exchangeCabinetInputLog.OldPostContact = item.OldPostContactNumber ?? 0;
                                    exchangeCabinetInputLog.OldTelephone = _oldTelephones.Where(t3 => t3.SwitchPortID == t.SwitchPortID).Take(1).Select(t3 => t3.TelephoneNo).SingleOrDefault();

                                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ExchangeCabinetInputLog>(exchangeCabinetInputLog, true));

                                    requestLog.Date = currentDataTime;
                                    requestLog.Detach();

                                    t.CabinetInputID = _newBuchtList.Where(t2 => t2.ID == item.NewBuchtID).SingleOrDefault().CabinetInputID;
                                });

                                #endregion pcm
                            }
                            // if bucht is PrivateWire change
                            else if (_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status == (int)DB.BuchtStatus.Connection && _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht != null && Data.ExchangeCabinetInputDB.IsSpecialWire(_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ID))
                            {
                                // save log

                                Data.Schema.ExchangeCabinetInputLog exchangeCabinetInputLog = new Data.Schema.ExchangeCabinetInputLog();
                                exchangeCabinetInputLog.NewBucht = item.NewConnectionNo;
                                exchangeCabinetInputLog.OldBucht = item.OldConnectionNo;
                                exchangeCabinetInputLog.NewCabinet = item.NewCabinetNumber ?? 0;
                                exchangeCabinetInputLog.OldCabinet = (int)item.OldCabinetNumber;
                                exchangeCabinetInputLog.NewCabinetInput = item.NewCabinetInputNumber ?? 0;
                                exchangeCabinetInputLog.OldCabinetInput = item.OldCabinetInputNumber ?? 0;
                                exchangeCabinetInputLog.NewPost = item.NewPostNumber ?? 0;
                                exchangeCabinetInputLog.OldPost = item.OldPostNumber ?? 0;
                                exchangeCabinetInputLog.NewPostContact = item.NewPostConntactNumber ?? 0;
                                exchangeCabinetInputLog.OldPostContact = item.OldPostContactNumber ?? 0;
                                exchangeCabinetInputLog.OldTelephone = item.OldTelephonNo ?? 0;

                                requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ExchangeCabinetInputLog>(exchangeCabinetInputLog, true));

                                requestLog.Date = currentDataTime;
                                requestLog.Detach();

                                //

                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().Status = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status = (int)DB.BuchtStatus.Free;

                                if (_exchangeCabinetInput.IsChangePost)
                                {
                                    _newBuchtList[i].ConnectionID = item.NewPostContactID;
                                    _newPostContactList.Where(t => t.ID == item.NewPostContactID).SingleOrDefault().Status = _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status;
                                    _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;
                                    if (!(_oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status == (byte)DB.PostContactStatus.PermanentBroken))
                                    {
                                        _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;
                                    }

                                }
                                else
                                {
                                    _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ConnectionID = item.OldPostContactID;
                                }

                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;

                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().SwitchPortID = _oldBuchtList[i].SwitchPortID;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().SwitchPortID = null;


                                int newBuchtType = _newBuchtList[i].BuchtTypeID;
                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().BuchtTypeID = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtTypeID;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtTypeID = newBuchtType;


                                Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht);
                                buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                if (_oldBuchtList.Any(b => b.ID == _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ID;
                                if (_newBuchtList.Any(b => b.ID == _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ID;
                                buchtPrivateWire.Detach();
                                DB.Save(buchtPrivateWire, false);

                                _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
                                _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;


                                SpecialWireAddress specialWireAddresses = SpecialWireAddressDB.GetSpecialWireAddressByBuchtID(_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ID);
                                SpecialWireAddress newSpecialWireAddress = new SpecialWireAddress();

                                newSpecialWireAddress.BuchtID = _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ID;
                                newSpecialWireAddress.TelephoneNo = specialWireAddresses.TelephoneNo;
                                newSpecialWireAddress.InstallAddressID = specialWireAddresses.InstallAddressID;
                                newSpecialWireAddress.CorrespondenceAddressID = specialWireAddresses.CorrespondenceAddressID;
                                newSpecialWireAddress.SecondBuchtID = specialWireAddresses.SecondBuchtID;
                                newSpecialWireAddress.SpecialTypeID = specialWireAddresses.SpecialTypeID;
                                newSpecialWireAddress.Detach();
                                DB.Save(newSpecialWireAddress, true);

                                DB.Delete<SpecialWireAddress>(specialWireAddresses.BuchtID);
                            }
                            else
                            {
                                throw new Exception("در میان بوخت های بوخت نامشخص وجود دارد");
                            }

                            if (!_exchangeCabinetInput.IsChangePost && _exchangeCabinetInput.OldCabinetID != _exchangeCabinetInput.NewCabinetID)
                            {
                                _oldAllPostList.Where(t => t.ID == item.OldPostID).SingleOrDefault().CabinetID = (int)item.NewCabinet;
                            }

                            requestLogs.Add(requestLog);

                        };





                        _newBuchtList.ForEach(t => t.Detach());
                        DB.UpdateAll(_newBuchtList);

                        _oldBuchtList.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtList);


                        _oldBuchtListPCM.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtListPCM);

                        _oldBuchtListPCM.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtListPCM);

                        _oldPostContactListPCM.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldPostContactListPCM);


                        DB.SaveAll(requestLogs);

                        _oldPostContactList.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldPostContactList);

                        _newPostContactList.ForEach(t => t.Detach());
                        DB.UpdateAll(_newPostContactList);


                        _oldAllPostList.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldAllPostList);



                        ExitReserveCabinet(_exchangeCabinetInput);

                        _exchangeCabinetInput.CompletionDate = currentDataTime;
                        _exchangeCabinetInput.Detach();
                        DB.Save(_exchangeCabinetInput);

                        ts3.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion method

        #region print
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
        #endregion print

    }
}

