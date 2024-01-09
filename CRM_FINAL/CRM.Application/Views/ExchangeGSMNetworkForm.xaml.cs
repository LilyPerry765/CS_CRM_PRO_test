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
    public partial class ExchangeGSMNetworkForm : Local.RequestFormBase
    {
        private long _requestID;
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        ExchangeGSM _exchangeGSM { get; set; }

        Request _reqeust { get; set; }

        List<ExchangeGSMInfo> exchangeGSMInfo;

        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        List<PostContact> _oldPostContactList { get; set; }


        List<PostContact> _newPostContactList { get; set; }

        List<Telephone> _newTelephones { get; set; }
        List<Telephone> _oldTelephones { get; set; }

        List<TelephoneSpecialServiceType> _oldTelephoneSpecialServiceType { get; set; }


        List<TelephoneSpecialServiceType> _newTelephoneSpecialServiceType = new List<TelephoneSpecialServiceType>();

        List<TelephonCustomer> _telephonCustomer { get; set; }
        public ExchangeGSMNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ExchangeGSMNetworkForm(long requestID)
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
            _exchangeGSM = Data.ExchangeGSMDB.GetExchangeGSMByRequestID(_requestID);
            if (_exchangeGSM.NetworkAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeGSM.NetworkAccomplishmentDate = dateTime.Date;
                _exchangeGSM.NetworkAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
            _reqeust = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;

            exchangeGSMInfo =  Data.ExchangeGSMDB.GetExchangeGSMList(_exchangeGSM);
            TelItemsDataGrid.DataContext = exchangeGSMInfo;

            if(_exchangeGSM.Type == (byte)DB.ExchangeGSMType.NormalToGSM)
             {
              //ToPostNumberTextColumn.Visibility = Visibility.Visible;

              //ToPostConntactNumberTextColumn.Visibility = Visibility.Visible;

                _oldPostContactList = new List<PostContact>();
                _newPostContactList = new List<PostContact>();
             }

            AccomplishmentGroupBox.DataContext = _exchangeGSM;
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
          

                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _exchangeGSM = AccomplishmentGroupBox.DataContext as ExchangeGSM;
                    _exchangeGSM.Detach();
                    DB.Save(_exchangeGSM, false);

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


        private void DoWork()
        {
            try
            {
                if (_exchangeGSM.CompletionDate == null)
                {
                    DateTime currentDataTime = DB.GetServerDate();
                    List<RequestLog> requestLogs = new List<RequestLog>();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        // change waiting list

                        //if (_exchangeGSM.TransferWaitingList)
                        //{

                        //    if (_exchangeGSM.Type == (int)DB.TranslationOpticalCabinetToNormalType.Post)
                        //    {
                        //        List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo = new List<InvestigatePossibilityWaitngListChangeInfo>();

                        //        exchangeGSMInfo.ForEach(c =>
                        //        {
                        //            if (!investigatePossibilityWaitngListChangeInfo.Any(t => t.oldPostID == c.OldPostID))
                        //            {
                        //                investigatePossibilityWaitngListChangeInfo.Add(
                        //                        new InvestigatePossibilityWaitngListChangeInfo
                        //                        {
                        //                            newCabinetID = _exchangeGSM.NewCabinetID,
                        //                            newPostID    = (int)c.NewPostID,
                        //                            oldCabinetID = _exchangeGSM.OldCabinetID,
                        //                            oldPostID = (int)c.OldPostID,
                        //                        }
                        //                );
                        //            }
                        //        });

                        //        InvestigatePossibilityDB.ChangePostInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);
                        //    }
                        //    else if (_exchangeGSM.Type == (int)DB.TranslationOpticalCabinetToNormalType.General)
                        //    {
                        //        List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo = new List<InvestigatePossibilityWaitngListChangeInfo>();

                        //        exchangeGSMInfo.ForEach(c =>
                        //        {
                        //            if (!investigatePossibilityWaitngListChangeInfo.Any(t => t.oldPostID == c.OldPostID))
                        //            {
                        //                investigatePossibilityWaitngListChangeInfo.Add(
                        //                        new InvestigatePossibilityWaitngListChangeInfo
                        //                        {
                        //                            newCabinetID = _exchangeGSM.NewCabinetID,
                        //                            newPostID    = (int)c.NewPostID,
                        //                            oldCabinetID = _exchangeGSM.OldCabinetID,
                        //                            oldPostID = (int)c.OldPostID,
                        //                        }
                        //                );
                        //            }
                        //        });
                        //        InvestigatePossibilityDB.ChangeCabinetInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);
                        //    }

                        //}
                        ////


                        // Transfer Broken PostContact
                        //if (_exchangeGSM.TransferBrokenPostContact)
                        //{

                        //    List<PostContactInfo> BrokenOldPostContactList = Data.PostContactDB.GetBrokenPostContactByCabinetID(_exchangeGSM.OldCabinetID, exchangeGSMInfo.Select(t => (int)t.OldPostID).Distinct().ToList());
                        //    List<PostContact> newBrokenPostContactList = Data.PostContactDB.GetFreePostContactByCabinetID(_exchangeGSM.NewCabinetID, exchangeGSMInfo.Select(t => (int)t.NewPostID).Distinct().ToList());
                        //    BrokenOldPostContactList.ForEach(t =>
                        //    {
                        //       int postID = exchangeGSMInfo.Where(t2 => t2.OldPostID == t.PostID).Take(1).SingleOrDefault().NewPostID ?? 0; 
                        //       if( newBrokenPostContactList.Any(t3=>t3.PostID == postID && t3.ConnectionNo == t.ConnectionNo))
                        //       {
                        //           newBrokenPostContactList.Where(t3 => t3.PostID == postID && t3.ConnectionNo == t.ConnectionNo).SingleOrDefault().Status = t.Status;
                        //       }

                        //    });

                        //    newBrokenPostContactList.ForEach(t => t.Detach());
                        //    DB.UpdateAll(newBrokenPostContactList);
                        //}
                        ////

                        if (_exchangeGSM.Type == (int)DB.ExchangeGSMType.NormalToGSM)
                        {

                            _oldPostContactList = Data.PostContactDB.GetPostContactByIDs(exchangeGSMInfo.Where(t => t.PostContactID != null).Select(t => (long)t.PostContactID).ToList());

                            _oldBuchtList = Data.BuchtDB.GetBuchtByIDs(exchangeGSMInfo.Where(t => t.BuchtID != null).Select(t => (long)t.BuchtID).ToList());



                            _oldTelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.FromTelephonNo != null).Select(t => (long)t.FromTelephonNo).ToList());
                            _telephonCustomer = Data.CustomerDB.GetCustomerByTelephones(_oldTelephones);
                            _newTelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.ToTelephonNo != null).Select(t => (long)t.ToTelephonNo).ToList());


                            _oldTelephoneSpecialServiceType = TelephoneSpecialServiceTypeDB.GetSpecialServicesOfTelephone(exchangeGSMInfo.Where(t => t.FromTelephonNo != null).Select(t => (long)t.FromTelephonNo).ToList());


                            exchangeGSMInfo = exchangeGSMInfo.Where(t => t.BuchtID != null).ToList();
                            int count = exchangeGSMInfo.Count();

                            for (int i = 0; i < count; i++)
                            {
                                ExchangeGSMInfo item = exchangeGSMInfo[i];

                                if (item.ToTelephonNo != null)
                                {
                                    
                                    // انتقال اطلاعات تلفن قدیم به جدید
                                    _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().SwitchPortID = null;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().InstallationDate = currentDataTime;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().InitialInstallationDate = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InitialInstallationDate;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().InstallAddressID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InstallAddressID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CorrespondenceAddressID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CorrespondenceAddressID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().Status = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().Status;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CauseOfCutID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CauseOfCutID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CutDate = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CutDate;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().ConnectDate = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ConnectDate;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CustomerID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CustomerGroupID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerGroupID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CustomerTypeID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerTypeID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().ChargingType = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ChargingType;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().PosessionType = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().PosessionType;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().ClassTelephone = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ClassTelephone;


                                    // انتقال سرویس های ویژه تلفن قدیم به جدید
                                    if (_oldTelephoneSpecialServiceType.Any(t2 => t2.TelephoneNo == _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().TelephoneNo))
                                    {
                                        _oldTelephoneSpecialServiceType.Where(t2 => t2.TelephoneNo == _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().TelephoneNo).ToList()
                                            .ForEach(t3 =>
                                            {
                                                _newTelephoneSpecialServiceType.Add(new TelephoneSpecialServiceType { TelephoneNo = _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().TelephoneNo, SpecialServiceTypeID = t3.SpecialServiceTypeID });
                                            });
                                    }

                                    // context.ExecuteCommand(@"UPDATE [dbo].[TelephoneSpecialServiceType] SET [TelephoneNo] = {0} WHERE TelephoneNo = {1}", newTelephone, oldTelephone);
                                    // تخلیه تلفن قدیم
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().Status = (byte)DB.TelephoneStatus.Discharge;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().DischargeDate = currentDataTime;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InstallationDate = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerGroupID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerTypeID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InstallAddressID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CorrespondenceAddressID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CauseOfCutID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ClassTelephone = (int)DB.ClassTelephone.LimitLess;
                                }

                                // if new bucht is not pcm. change ConnectionID
                                //if (_newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().PCMPortID == null)
                                //{
                                //    _newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().ConnectionID = item.PostContactID;

                                //}
                                //_newPostContactList.Where(t => t.ID == item.NewPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.CableConnection;


                                // if telephone is not pcm. remove ConnectionID
                                if (_oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().PCMPortID == null)
                                {
                                    _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().ConnectionID = null;
                                }

                                _oldPostContactList.Where(t => t.ID == item.PostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;


                              //  _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                                _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;

                                //_newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().Status =
                              //  _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().Status = (int)DB.BuchtStatus.Free;

                                // if telephone is not pcm. free status
                                if (_oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().PCMPortID == null)
                                {
                                    _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.Free;
                                }
                                else
                                {
                                    _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                                }


                                // create Log
                                RequestLog requestLog = new RequestLog();
                                requestLog.IsReject = false;
                                requestLog.RequestID = _reqeust.ID;
                                requestLog.RequestTypeID = _reqeust.RequestTypeID;
                                requestLog.TelephoneNo = item.FromTelephonNo;
                                requestLog.ToTelephoneNo = item.ToTelephonNo;
                                requestLog.CustomerID = _telephonCustomer.Where(t => t.TelephonNo == (long)item.ToTelephonNo).Select(t => t.Customer.CustomerID).SingleOrDefault();
                                requestLog.UserID = DB.CurrentUser.ID;

                                //TODO:rad باید برای این روال کلاس مربوطه جهت لاگ زدن اضافه شود
                                Data.Schema.TranslationOpticalToNormal translationOpticalToNormal = new Data.Schema.TranslationOpticalToNormal();
                                translationOpticalToNormal.NewBucht = item.ConnectionNo;
                                translationOpticalToNormal.OldBucht = item.ConnectionNo;
                                translationOpticalToNormal.NewCabinet = item.CabinetNumber ?? 0;
                                translationOpticalToNormal.OldCabinet = item.CabinetNumber ?? 0;
                                translationOpticalToNormal.NewCabinetInput = item.CabinetInputNumber ?? 0;
                                translationOpticalToNormal.OldCabinetInput = item.CabinetInputNumber ?? 0;
                                translationOpticalToNormal.NewPost = item.PostNumber ?? 0;
                                translationOpticalToNormal.OldPost = item.PostNumber ?? 0;
                                translationOpticalToNormal.NewPostContact = item.PostContactNumber ?? 0;
                                translationOpticalToNormal.OldPostContact = item.PostContactNumber ?? 0;
                                translationOpticalToNormal.NewTelephone = item.FromTelephonNo ?? 0;
                                translationOpticalToNormal.OldTelephone = item.ToTelephonNo ?? 0;

                                requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationOpticalToNormal>(translationOpticalToNormal, true));

                                requestLog.Date = currentDataTime;
                                requestLog.Detach();

                                requestLogs.Add(requestLog);

                            };


                          //  _newBuchtList.ForEach(t => t.Detach());
                           // DB.UpdateAll(_newBuchtList);

                            _oldBuchtList.ForEach(t => t.Detach());
                            DB.UpdateAll(_oldBuchtList);


                            _oldTelephones.ForEach(t => t.Detach());
                            DB.UpdateAll(_oldTelephones);

                            _newTelephones.ForEach(t => t.Detach());
                            DB.UpdateAll(_newTelephones);

                            DB.SaveAll(requestLogs);

                            _oldPostContactList.ForEach(t => t.Detach());
                            DB.UpdateAll(_oldPostContactList);

                            _newPostContactList.ForEach(t => t.Detach());
                            DB.UpdateAll(_newPostContactList);

                            DB.DeleteAll<TelephoneSpecialServiceType>(_oldTelephoneSpecialServiceType.Select(t => t.TelephoneNo).ToList());

                            DB.SaveAll<TelephoneSpecialServiceType>(_newTelephoneSpecialServiceType);

                          //  ExitReserveCabinet(_exchangeGSM);

                            _exchangeGSM.CompletionDate = currentDataTime;
                            _exchangeGSM.Detach();
                            DB.Save(_exchangeGSM);


                            CRM.Data.Interface.DischargeTelephones(_oldTelephones);




                        }
                        else
                        {

                            // _oldPostContactList = Data.PostContactDB.GetPostContactByIDs(exchangeGSMInfo.Where(t => t.PostContactID != null).Select(t => (long)t.PostContactID).ToList());

                             _newPostContactList = Data.PostContactDB.GetPostContactByIDs(exchangeGSMInfo.Where(t => t.PostContactID != null).Select(t => (long)t.PostContactID).ToList());

                            // _oldBuchtList = Data.BuchtDB.GetBuchtByIDs(exchangeGSMInfo.Where(t => t.OldBuchtID != null).Select(t => (long)t.OldBuchtID).ToList());
                             _newBuchtList = Data.BuchtDB.GetBuchtByIDs(exchangeGSMInfo.Where(t => t.BuchtID != null).Select(t => (long)t.BuchtID).ToList());


                             _oldTelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.FromTelephonNo != null).Select(t => (long)t.FromTelephonNo).ToList());
                             _telephonCustomer = Data.CustomerDB.GetCustomerByTelephones(_oldTelephones);
                            _newTelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.ToTelephonNo != null).Select(t => (long)t.ToTelephonNo).ToList());


                             _oldTelephoneSpecialServiceType = TelephoneSpecialServiceTypeDB.GetSpecialServicesOfTelephone(exchangeGSMInfo.Where(t => t.FromTelephonNo != null).Select(t => (long)t.FromTelephonNo).ToList());

                             exchangeGSMInfo = exchangeGSMInfo.Where(t => t.BuchtID != null).ToList();
                             int count = exchangeGSMInfo.Count();

                             for (int i = 0; i < count; i++)
                             {
                               ExchangeGSMInfo item = exchangeGSMInfo[i];

                                if (item.ToTelephonNo != null)
                                {
                                    _newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().SwitchPortID = _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().SwitchPortID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().InstallationDate = currentDataTime;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().InstallAddressID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InstallAddressID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CorrespondenceAddressID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CorrespondenceAddressID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().Status = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().Status;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().InitialInstallationDate = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InitialInstallationDate;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CauseOfCutID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CauseOfCutID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CutDate = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CutDate;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().ConnectDate = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ConnectDate;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CustomerID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CustomerGroupID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerGroupID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().CustomerTypeID = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerTypeID;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().ChargingType = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ChargingType;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().PosessionType = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().PosessionType;
                                    _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().ClassTelephone = _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ClassTelephone;

                                    if (_oldTelephoneSpecialServiceType.Any(t2 => t2.TelephoneNo == _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().TelephoneNo))
                                    {
                                        _oldTelephoneSpecialServiceType.Where(t2 => t2.TelephoneNo == _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().TelephoneNo).ToList()
                                            .ForEach(t3 =>
                                        {
                                            _newTelephoneSpecialServiceType.Add(new TelephoneSpecialServiceType { TelephoneNo = _newTelephones.Where(t => t.TelephoneNo == item.ToTelephonNo).SingleOrDefault().TelephoneNo, SpecialServiceTypeID = t3.SpecialServiceTypeID });
                                        });
                                    }

                                    // context.ExecuteCommand(@"UPDATE [dbo].[TelephoneSpecialServiceType] SET [TelephoneNo] = {0} WHERE TelephoneNo = {1}", newTelephone, oldTelephone);

                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().Status = (byte)DB.TelephoneStatus.Discharge;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().DischargeDate = currentDataTime;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InstallationDate = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerGroupID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CustomerTypeID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().InstallAddressID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CorrespondenceAddressID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().CauseOfCutID = null;
                                    _oldTelephones.Where(t => t.TelephoneNo == item.FromTelephonNo).SingleOrDefault().ClassTelephone = (int)DB.ClassTelephone.LimitLess;
                                }

                                // if new bucht is not pcm. change ConnectionID
                                if (_newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().PCMPortID == null)
                                {
                                    _newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().ConnectionID = item.PostContactID;

                                }
                                _newPostContactList.Where(t => t.ID == item.PostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.CableConnection;


                                // if telephone is not pcm. remove ConnectionID
                                //if (_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().PCMPortID == null)
                                //{
                                //    _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;
                                //}

                                //_oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;


                                // _newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                                // _oldBuchtList.Where(t => t.ID == item.OBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;

                                _newBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().Status = (int)DB.BuchtStatus.Connection;

                                // if telephone is not pcm. free status
                                //if (_oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().PCMPortID == null)
                                //{
                                //    _oldBuchtList.Where(t => t.ID == item.BuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.Free;
                                //}
                                //else
                                //{
                                //    _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                                //}


                                // create Log
                                RequestLog requestLog = new RequestLog();
                                requestLog.IsReject = false;
                                requestLog.RequestID = _reqeust.ID;
                                requestLog.RequestTypeID = _reqeust.RequestTypeID;
                                requestLog.TelephoneNo = item.FromTelephonNo;
                                requestLog.ToTelephoneNo = item.ToTelephonNo;
                                requestLog.CustomerID = _telephonCustomer.Where(t => t.TelephonNo == (long)item.ToTelephonNo).Select(t => t.Customer.CustomerID).SingleOrDefault();
                                requestLog.UserID = DB.CurrentUser.ID;

                                Data.Schema.TranslationOpticalToNormal translationOpticalToNormal = new Data.Schema.TranslationOpticalToNormal();
                                translationOpticalToNormal.NewBucht = item.ConnectionNo;
                                translationOpticalToNormal.OldBucht = item.ConnectionNo;
                                translationOpticalToNormal.NewCabinet = item.CabinetNumber ?? 0;
                                translationOpticalToNormal.OldCabinet = item.CabinetNumber ?? 0;
                                translationOpticalToNormal.NewCabinetInput = item.CabinetInputNumber ?? 0;
                                translationOpticalToNormal.OldCabinetInput = item.CabinetInputNumber ?? 0;
                                translationOpticalToNormal.NewPost = item.PostNumber ?? 0;
                                translationOpticalToNormal.OldPost = item.PostNumber ?? 0;
                                translationOpticalToNormal.NewPostContact = item.PostContactNumber ?? 0;
                                //TODO:
                               // translationOpticalToNormal.OldPostContact = item.PostContactID ?? 0;
                                translationOpticalToNormal.NewTelephone = item.FromTelephonNo ?? 0;
                                translationOpticalToNormal.OldTelephone = item.ToTelephonNo ?? 0;

                                requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationOpticalToNormal>(translationOpticalToNormal, true));

                                requestLog.Date = currentDataTime;
                                requestLog.Detach();

                                requestLogs.Add(requestLog);

                            };


                            _newBuchtList.ForEach(t => t.Detach());
                            DB.UpdateAll(_newBuchtList);

                          //  _oldBuchtList.ForEach(t => t.Detach());
                           // DB.UpdateAll(_oldBuchtList);


                            _oldTelephones.ForEach(t => t.Detach());
                            DB.UpdateAll(_oldTelephones);

                            _newTelephones.ForEach(t => t.Detach());
                            DB.UpdateAll(_newTelephones);

                            DB.SaveAll(requestLogs);

                           // _oldPostContactList.ForEach(t => t.Detach());
                           // DB.UpdateAll(_oldPostContactList);

                            _newPostContactList.ForEach(t => t.Detach());
                            DB.UpdateAll(_newPostContactList);

                            DB.DeleteAll<TelephoneSpecialServiceType>(_oldTelephoneSpecialServiceType.Select(t => t.TelephoneNo).ToList());

                            DB.SaveAll<TelephoneSpecialServiceType>(_newTelephoneSpecialServiceType);

                          //  ExitReserveCabinet(_exchangeGSM);

                            _exchangeGSM.CompletionDate = currentDataTime;
                            _exchangeGSM.Detach();
                            DB.Save(_exchangeGSM);


                            CRM.Data.Interface.DischargeTelephones(_oldTelephones);

                        }


                        // _oldPostContactList = Data.PostContactDB.GetPostContactByIDs(exchangeGSMInfo.Where(t => t.PostContactID != null).Select(t => (long)t.PostContactID).ToList());

                        // _newPostContactList = Data.PostContactDB.GetPostContactByIDs(exchangeGSMInfo.Where(t => t.NewPostContactID != null).Select(t => (long)t.NewPostContactID).ToList());

                        // _oldBuchtList = Data.BuchtDB.GetBuchtByIDs(exchangeGSMInfo.Where(t => t.OldBuchtID != null).Select(t => (long)t.OldBuchtID).ToList());
                        // _newBuchtList = Data.BuchtDB.GetBuchtByIDs(exchangeGSMInfo.Where(t => t.NewBuchtID != null).Select(t => (long)t.NewBuchtID).ToList());


                        // _oldTelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.OldTelephonNo != null).Select(t => (long)t.OldTelephonNo).ToList());
                        // _telephonCustomer = Data.CustomerDB.GetCustomerByTelephones(_oldTelephones);
                        // _newTelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.NewTelephonNo != null).Select(t => (long)t.NewTelephonNo).ToList());


                        // _oldTelephoneSpecialServiceType = TelephoneSpecialServiceTypeDB.GetSpecialServicesOfTelephone(exchangeGSMInfo.Where(t => t.OldTelephonNo != null).Select(t => (long)t.OldTelephonNo).ToList());

                        // exchangeGSMInfo = exchangeGSMInfo.Where(t => t.OldBuchtID != null).ToList();
                        // int count = exchangeGSMInfo.Count();

                        // for (int i = 0; i < count; i++)
                        // {
                        //    TranslationOpticalCabinetToNormalInfo item = exchangeGSMInfo[i];

                        //    if (item.NewTelephonNo != null)
                        //    {
                        //        _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().SwitchPortID = _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().SwitchPortID;
                        //        _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().SwitchPortID = null;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().InstallationDate        = currentDataTime;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().InstallAddressID        = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().InstallAddressID;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().CorrespondenceAddressID = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CorrespondenceAddressID;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().Status                  = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().Status;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().InitialInstallationDate = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().InitialInstallationDate;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().CauseOfCutID            = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CauseOfCutID;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().CutDate                 = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CutDate;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().ConnectDate             = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().ConnectDate;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().CustomerID              = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CustomerID;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().CustomerGroupID         = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CustomerGroupID;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().CustomerTypeID          = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CustomerTypeID;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().ChargingType            = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().ChargingType;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().PosessionType           = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().PosessionType;
                        //        _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().ClassTelephone          = _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().ClassTelephone;

                        //        if(_oldTelephoneSpecialServiceType.Any(t2=>t2.TelephoneNo == _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().TelephoneNo))
                        //        {
                        //            _oldTelephoneSpecialServiceType.Where(t2 => t2.TelephoneNo == _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().TelephoneNo).ToList()
                        //                .ForEach(t3 =>
                        //            {
                        //                _newTelephoneSpecialServiceType.Add(new TelephoneSpecialServiceType { TelephoneNo = _newTelephones.Where(t => t.TelephoneNo == item.NewTelephonNo).SingleOrDefault().TelephoneNo, SpecialServiceTypeID = t3.SpecialServiceTypeID });
                        //            });
                        //        }

                        //       // context.ExecuteCommand(@"UPDATE [dbo].[TelephoneSpecialServiceType] SET [TelephoneNo] = {0} WHERE TelephoneNo = {1}", newTelephone, oldTelephone);

                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().Status = (byte)DB.TelephoneStatus.Discharge;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().DischargeDate = currentDataTime;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().InstallationDate = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CustomerID = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CustomerGroupID = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CustomerTypeID = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().InstallAddressID = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CorrespondenceAddressID = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().CauseOfCutID = null;
                        //        _oldTelephones.Where(t => t.TelephoneNo == item.OldTelephonNo).SingleOrDefault().ClassTelephone = (int)DB.ClassTelephone.LimitLess;
                        //    }

                        //    // if new bucht is not pcm. change ConnectionID
                        //    if (_newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().PCMPortID == null)
                        //    {
                        //        _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().ConnectionID = item.NewPostContactID;

                        //    }
                        //    _newPostContactList.Where(t => t.ID == item.NewPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.CableConnection;


                        //        // if telephone is not pcm. remove ConnectionID
                        //     if (_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().PCMPortID == null)
                        //     {
                        //         _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().ConnectionID = null;
                        //     }

                        //     _oldPostContactList.Where(t => t.ID == item.OldPostContactID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;


                        //     _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                        //     _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().BuchtIDConnectedOtherBucht = null;

                        //     _newBuchtList.Where(t => t.ID == item.NewBuchtID).SingleOrDefault().Status = _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status;

                        //     // if telephone is not pcm. free status
                        //     if (_oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().PCMPortID == null)
                        //     {
                        //         _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.Free;
                        //     }
                        //     else
                        //     {
                        //         _oldBuchtList.Where(t => t.ID == item.OldBuchtID).SingleOrDefault().Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                        //     }


                        //     // create Log
                        //     RequestLog requestLog = new RequestLog();
                        //     requestLog.IsReject = false;
                        //     requestLog.RequestID = _reqeust.ID;
                        //     requestLog.RequestTypeID = _reqeust.RequestTypeID;
                        //     requestLog.TelephoneNo = item.OldTelephonNo;
                        //     requestLog.ToTelephoneNo = item.NewTelephonNo;
                        //     requestLog.CustomerID = _telephonCustomer.Where(t => t.TelephonNo == (long)item.NewTelephonNo).Select(t => t.Customer.CustomerID).SingleOrDefault();
                        //     requestLog.UserID = DB.CurrentUser.ID;

                        //     Data.Schema.TranslationOpticalToNormal translationOpticalToNormal = new Data.Schema.TranslationOpticalToNormal();
                        //     translationOpticalToNormal.NewBucht = item.NewConnectionNo;
                        //     translationOpticalToNormal.OldBucht = item.OldConnectionNo;
                        //     translationOpticalToNormal.NewCabinet = item.NewCabinetNumber ?? 0;
                        //     translationOpticalToNormal.OldCabinet = item.OldCabinetNumber ?? 0;
                        //     translationOpticalToNormal.NewCabinetInput = item.NewCabinetInputNumber ?? 0;
                        //     translationOpticalToNormal.OldCabinetInput = item.OldCabinetInputNumber ?? 0;
                        //     translationOpticalToNormal.NewPost = item.NewPostNumber ?? 0;
                        //     translationOpticalToNormal.OldPost = item.OldPostNumber ?? 0;
                        //     translationOpticalToNormal.NewPostContact = item.NewPostConntactNumber ?? 0;
                        //     translationOpticalToNormal.OldPostContact = item.OldPostContactNumber ?? 0;
                        //     translationOpticalToNormal.NewTelephone = item.NewTelephonNo ?? 0;
                        //     translationOpticalToNormal.OldTelephone = item.OldTelephonNo ?? 0;

                        //     requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationOpticalToNormal>(translationOpticalToNormal, true));

                        //     requestLog.Date = currentDataTime;
                        //     requestLog.Detach();

                        //     requestLogs.Add(requestLog);

                        // };


                        //_newBuchtList.ForEach(t => t.Detach());
                        //DB.UpdateAll(_newBuchtList);

                        //_oldBuchtList.ForEach(t => t.Detach());
                        //DB.UpdateAll(_oldBuchtList);


                        //_oldTelephones.ForEach(t => t.Detach());
                        //DB.UpdateAll(_oldTelephones);

                        //_newTelephones.ForEach(t => t.Detach());
                        //DB.UpdateAll(_newTelephones);

                        //DB.SaveAll(requestLogs);

                        //_oldPostContactList.ForEach(t => t.Detach());
                        //DB.UpdateAll(_oldPostContactList);

                        //_newPostContactList.ForEach(t => t.Detach());
                        //DB.UpdateAll(_newPostContactList);

                        //DB.DeleteAll<TelephoneSpecialServiceType>(_oldTelephoneSpecialServiceType.Select(t => t.TelephoneNo).ToList());

                        //DB.SaveAll<TelephoneSpecialServiceType>(_newTelephoneSpecialServiceType);

                        //ExitReserveCabinet(_exchangeGSM);

                        //_exchangeGSM.CompletionDate = currentDataTime;
                        //_exchangeGSM.Detach();
                        //DB.Save(_exchangeGSM);


                        //CRM.Data.Interface.DischargeTelephones( _oldTelephones );

                        ts3.Complete(); 
                    }


                
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //private void ExitReserveCabinet(TranslationOpticalCabinetToNormal translationOpticalCabinetToNormal)
        //{
        //    using (TransactionScope ts4 = new TransactionScope(TransactionScopeOption.RequiresNew))
        //    {
        //        if (translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.General)
        //        {
        //            Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.OldCabinetID);
        //            oldCabinet.Status = (int)DB.CabinetStatus.Install;
        //            oldCabinet.Detach();

        //            Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.NewCabinetID);
        //            newCabinet.Status = (int)DB.CabinetStatus.Install;
        //            newCabinet.Detach();

        //            DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });
        //        }
        //        else if (translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Slight)
        //        {
        //            List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(exchangeGSMInfo.Select(t => (long)t.NewCabinetInputID).ToList());
        //            cabinetInputs.ToList().ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.healthy; t.Detach(); });
        //            DB.UpdateAll(cabinetInputs);
        //        }

        //        ts4.Complete();
        //    }
        //}

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSuccessMessage(e.ProgressPercentage.ToString() + "%");
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_exchangeGSM.CompletionDate != null)
            {
                ShowSuccessMessage("اتمام عملیات برگردان");
                this.IsEnabled = true;
                _Action.InternalForward(null, null);
            }
        }


        private void SendToPrintTranslationOpticalCabinetToNormallMDFReport(ObservableCollection<TranslationOpticalCabinetToNormalInfo> Result)
        {
            Stimulsoft.Report.StiReport stiReport = new StiReport();
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

        public override bool Print()
        {

            ObservableCollection<TranslationOpticalCabinetToNormalInfo> TranslationOpticalCabinetToNormalInfo = new ObservableCollection<TranslationOpticalCabinetToNormalInfo>();
            TranslationOpticalCabinetToNormalInfo = new ObservableCollection<Data.TranslationOpticalCabinetToNormalInfo>(Data.TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionInfoByRequestIDs(new List<long> { _requestID }));
            SendToPrintTranslationOpticalCabinetToNormallMDFReport(TranslationOpticalCabinetToNormalInfo);
            IsPrintSuccess = true;

            return IsPrintSuccess; 
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

        private void PrintItem_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

                DataSet data = exchangeGSMInfo.ToDataSet("Result", TelItemsDataGrid);
                CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);

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

        public override bool Deny()
        {

            try
            {
                base.RequestID = _requestID;
                if (_exchangeGSM.CompletionDate == null)
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

