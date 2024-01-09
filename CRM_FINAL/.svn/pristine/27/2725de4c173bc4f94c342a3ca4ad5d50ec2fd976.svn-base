using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationOpticalCabinetToNormalForm.xaml
    /// </summary>
    public partial class ExchangeGSMForm : Local.RequestFormBase
    {
        #region Properties and Fields

        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }

        private long _requestID;
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        int _centerID = 0;

        bool isCabinetInputLoad = false;

        //int _pastNewCabinetID = 0;

        int _pastCabinetID = 0;

        List<ExchangeGSMInfo> ExchangeGSMList = new List<ExchangeGSMInfo>();
        public ObservableCollection<CheckableItem> _FromPost { get; set; }

        public ObservableCollection<CheckableItem> _CabinetInput { get; set; }



        public ObservableCollection<CheckableItem> _Cabinet { get; set; }

        List<CRM.Data.ExchangeGSMConnection> _exchangeGSMInfoConncetions = new List<ExchangeGSMConnection>();

        public ObservableCollection<CheckableItem> _FromPostConntact { get; set; }

        public ObservableCollection<CheckableItem> _ToPost { get; set; }

        public ObservableCollection<CheckableItem> _ToPostConntact { get; set; }

        Request _reqeust { get; set; }

        ExchangeGSM _exchangeGSM { get; set; }

        ObservableCollection<CRM.Data.ExchangeGSMConnectionInfo> _exchangeGSMInfoConncetion { get; set; }

        List<CRM.Data.ExchangeGSMConnectionInfo> _cloneOfExchangeGSMConncetion { get; set; }

        #endregion

        #region Constructor

        public ExchangeGSMForm()
        {
            InitializeComponent();
            Initialize();
        }
        public ExchangeGSMForm(long requestID)
            : this()
        {
            _requestID = requestID;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            //OrderTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ExchangeOrderType));
            //OrderTypeComboBox.SelectedIndex = 0;
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            if (Data.StatusDB.IsFinalStep(this.currentStat))
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
            }
            else
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
            }

            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
            _exchangeRequestInfo.RequestType = (int)DB.RequestType.TranslationOpticalCabinetToNormal;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;
            // _translationOpticalCabinetToNormalPost = new ObservableCollection<TranslationOpticalCabinetToNormalPost>();

            if (_requestID == 0)
            {
                _exchangeGSM = new ExchangeGSM();
                _exchangeGSMInfoConncetion = new ObservableCollection<ExchangeGSMConnectionInfo>();
                _cloneOfExchangeGSMConncetion = new List<ExchangeGSMConnectionInfo>();
            }
            else
            {

                // _exchangeGSM = Data.TranslationOpticalCabinetToNormalDB.GetTranslationOpticalCabinetToNormal(_requestID);
                _exchangeGSM = Data.ExchangeGSMDB.GetExchangeGSMByRequestID(_requestID);
                _reqeust = Data.RequestDB.GetRequestByID(_requestID);
                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    TelephoneDetailGroupBox.Visibility = Visibility.Visible;
                    TranslationTypeDetailGroupBox.Visibility = Visibility.Collapsed;

                    ExchangeGSMList = Data.ExchangeGSMDB.GetExchangeGSMList(_exchangeGSM);
                    TelItemsDataGrid.DataContext = ExchangeGSMList;

                }
                else
                {


                    _exchangeGSMInfoConncetion = new ObservableCollection<ExchangeGSMConnectionInfo>(Data.ExchangeGSMDB.GetExchangeGSMConncetionInfoByRequestID(_requestID));
                    _cloneOfExchangeGSMConncetion = _exchangeGSMInfoConncetion.Select(t => (ExchangeGSMConnectionInfo)t.Clone()).ToList();


                    _pastCabinetID = _exchangeGSM.OldCabinetID;


                    CabinetComboBox.SelectedValue = _exchangeGSM.OldCabinetID;
                    CabinetComboBox_SelectionChanged(null, null);

                    //NewCabinetComboBox.SelectedValue = _translationOpticalCabinetToNormal.NewCabinetID;
                    //NewCabinetComboBox_SelectionChanged(null, null);


                    if (_exchangeGSM.Type == (byte)DB.ExchangeGSMType.GSMToNormal)
                    {
                        GSMToNormalRadioButton.IsChecked = true;
                    }
                    else if (_exchangeGSM.Type == (byte)DB.ExchangeGSMType.NormalToGSM)
                    {
                        NormalToGSMRadioButton.IsChecked = true;
                    }
                    //else if (_translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Post)
                    //{
                    //    PostTranslationRadioButton.IsChecked = true;
                    //    SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                    //}
                }
            }

            this.DataContext = _exchangeGSM;
            SlightTranslationDataGrid.ItemsSource = _exchangeGSMInfoConncetion;
            //PostSelectorDataGrid.ItemsSource = _translationOpticalCabinetToNormalPost;
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            //   List<CheckableItem> newCabinetInputList = Data.CabinetDB.GetCabinetCheckableByType(_centerID);
            List<CheckableItem> CabinetList = Data.CabinetDB.GetCabinetCheckableByType(_centerID);
            //if (_pastNewCabinetID != 0)
            //{
            //    Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_pastNewCabinetID);
            //    newCabinetInputList.Add(new CheckableItem { ID = newCabinet.ID, Name = newCabinet.CabinetNumber.ToString(), IsChecked = false });
            //}
            //NewCabinetComboBox.ItemsSource = newCabinetInputList;

            if (_pastCabinetID != 0)
            {
                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_pastCabinetID);
                CabinetList.Add(new CheckableItem { ID = oldCabinet.ID, Name = oldCabinet.CabinetNumber.ToString(), IsChecked = false });
            }

            CabinetComboBox.ItemsSource = CabinetList;

            _Cabinet = new ObservableCollection<CheckableItem>(CabinetList);

        }

        private void SelectTranslationType()
        {
            if (GSMToNormalRadioButton.IsChecked == true)
            {
                CabinetComboBox.Visibility = Visibility.Visible;
                CabinetGroupBox.Visibility = Visibility.Visible;
                TelephoneSearchGroupBox.Visibility = Visibility.Visible;
                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                SlightTranslationDetailGroupBox.IsEnabled = true;
            }
            else if (NormalToGSMRadioButton.IsChecked == true)
            {
                CabinetGroupBox.Visibility = Visibility.Visible;
                CabinetComboBox.Visibility = Visibility.Collapsed;
                CabinetLabel.Visibility = Visibility.Visible;
                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                SlightTranslationDetailGroupBox.IsEnabled = false;
                TelephoneSearchGroupBox.Visibility = Visibility.Visible;
                CabinetInputCountLabel.Visibility = Visibility.Visible;
                CabinetInputCountTextBox.Visibility = Visibility.Visible;
            }
            this.ResizeWindow();
        }

        public override bool Save()
        {
            //if (!Codes.Validation.WindowIsValid.IsValid(this))
            //{
            //    IsSaveSuccess = false;
            //    return false;
            //}
            try
            {


                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    IsSaveSuccess = true;

                }
                else
                {
                    _reqeust = _exchangeRequestInfo.Request;
                    _exchangeGSM = this.DataContext as ExchangeGSM;

                    _exchangeGSM.OldCabinetID = (int)CabinetComboBox.SelectedValue;
                    Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_exchangeGSM.OldCabinetID);
                    // Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_exchangeGSM.NewCabinetID);
                    // _exchangeGSM.OldCabinetUsageTypeID = oldCabinet.CabinetUsageType;
                    // _exchangeGSM.NewCabinetUsageTypeID = newCabinet.CabinetUsageType;

                    if (GSMToNormalRadioButton.IsChecked == true)
                    {
                        _exchangeGSM.Type = (byte)DB.ExchangeGSMType.GSMToNormal;
                        //_exchangeGSM.FromNewCabinetInputID = null;
                        //_exchangeGSM.FromOldCabinetInputID = null;
                        //_exchangeGSM.ToNewCabinetInputID = null;
                        //_exchangeGSM.ToOldCabinetInputID = null;
                    }
                    else if (NormalToGSMRadioButton.IsChecked == true)
                    {
                        _exchangeGSM.Type = (byte)DB.ExchangeGSMType.NormalToGSM;
                        // _exchangeGSM.FromNewCabinetInputID = null;
                        //_exchangeGSM.FromOldCabinetInputID = null;
                        //_exchangeGSM.ToNewCabinetInputID = null;
                        //_exchangeGSM.ToOldCabinetInputID = null;
                        //_exchangeGSM.TransferWaitingList  = false;
                    }
                    //  else if (PostTranslationRadioButton.IsChecked == true)
                    //   {
                    // _translationOpticalCabinetToNormal.Type = (byte)DB.TranslationOpticalCabinetToNormalType.Post;
                    // }
                    // Verify data

                    VerifyData(_exchangeGSM);

                    List<AssignmentInfo> asignmentInfos = DB.GetAllInformationByTelephoneNos(_exchangeGSMInfoConncetion.Select(t => (long)t.FromTelephone).ToList());
                    List<Bucht> cabinetInputBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(_exchangeGSMInfoConncetion.Select(t => (long)t.CabinetInputID).ToList());

                    using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        if (_exchangeGSMInfoConncetion.Count() == 1)
                        {

                            ExchangeGSMConnectionInfo exchangeGSMConnectionInfo = _exchangeGSMInfoConncetion.Take(1).SingleOrDefault();

                            if (_reqeust.TelephoneNo != exchangeGSMConnectionInfo.FromTelephone)
                            {
                                bool inWaitingList = false;
                                string requestName = Data.RequestDB.GetOpenRequestNameTelephone(_exchangeGSMInfoConncetion.Select(t => (long)t.FromTelephone).ToList(), out inWaitingList);
                                if (!string.IsNullOrEmpty(requestName))
                                {
                                    Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                    throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                }
                                else
                                {
                                    _reqeust.TelephoneNo = exchangeGSMConnectionInfo.FromTelephone;
                                }
                            }


                        }
                        else
                        {
                            List<long> newTelephones = _exchangeGSMInfoConncetion.Where(t => !_cloneOfExchangeGSMConncetion.Select(t2 => t2.FromTelephone).Contains(t.FromTelephone)).Select(t => (long)t.FromTelephone).ToList();
                            bool inWaitingList = false;
                            string requestName = Data.RequestDB.GetOpenRequestNameTelephone(newTelephones, out inWaitingList);
                            if (!string.IsNullOrEmpty(requestName))
                            {
                                Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                            }

                            _reqeust.TelephoneNo = null;
                        }

                        // cabinetInputsList = Data.TranslationOpticalCabinetToNormalDB.GetEquivalentCabinetInputs(_translationOpticalCabinetToNormal);
                        // Save reqeust
                        if (_requestID == 0)
                        {
                            // save telephone in reqeust when one Connection selected

                            _reqeust.ID = DB.GenerateRequestID();
                            _reqeust.RequestPaymentTypeID = 0;
                            _reqeust.IsViewed = false;
                            _reqeust.InsertDate = DB.GetServerDate();
                            _reqeust.RequestTypeID = (int)DB.RequestType.ExchangeGSM;
                            _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.ExchangeGSM, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                            _reqeust.Detach();
                            DB.Save(_reqeust, true);

                            _exchangeGSM.ID = _reqeust.ID;

                            _exchangeGSM.Detach();
                            DB.Save(_exchangeGSM, true);

                            _requestID = _reqeust.ID;
                        }
                        else
                        {
                            if (_cloneOfExchangeGSMConncetion != null)
                            {
                                List<Bucht> buchts = BuchtDB.GetBuchtByCabinetInputIDs(_cloneOfExchangeGSMConncetion.Select(t => (long)t.CabinetInputID).ToList());
                                buchts.ForEach(t => { t.Status = (int)DB.BuchtStatus.Free; t.Detach(); });
                                DB.UpdateAll(buchts);
                            }


                            if (_cloneOfExchangeGSMConncetion != null)
                            {
                                List<PostContact> toPostContacts = PostContactDB.GetPostContactByIDs(_cloneOfExchangeGSMConncetion.Select(t => (long)t.PostConntactID).ToList());
                                toPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.Free; t.Detach(); });
                                DB.UpdateAll(toPostContacts);
                            }


                            Data.ExchangeGSMDB.DeleteExchangeGSMConncetionByRequestID(_exchangeGSM.ID);

                            _reqeust.Detach();
                            DB.Save(_reqeust, false);

                            _exchangeGSM.Detach();
                            DB.Save(_exchangeGSM, false);
                        }



                        //if (GSMToNormalRadioButton.IsChecked == true)
                        //{
                        //    if (_pastCabinetID != 0 && _pastCabinetID != _exchangeGSM.OldCabinetID)
                        //    {
                        //        Cabinet oldPastCabinet = Data.CabinetDB.GetCabinetByID(_pastCabinetID);
                        //        oldPastCabinet.Status = (int)DB.CabinetStatus.Install;
                        //        oldPastCabinet.Detach();
                        //    }

                        //    oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                        //    oldCabinet.Detach();


                        //    //if (_pastNewCabinetID != 0 && _pastNewCabinetID != _exchangeGSM.NewCabinetID)
                        //    //{
                        //    //    Cabinet newPastCabinet = Data.CabinetDB.GetCabinetByID(_pastOldCabinetID);
                        //    //    newPastCabinet.Status = (int)DB.CabinetStatus.Install;
                        //    //    newPastCabinet.Detach();
                        //    //}

                        //    //newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                        //    //newCabinet.Detach();

                        //    DB.UpdateAll(new List<Cabinet> { oldCabinet /*, newCabinet*/ });

                        //}

                        _exchangeGSMInfoConncetion.ToList().ForEach(item =>
                       {
                           CRM.Data.ExchangeGSMConnection exchangeGSMConnection = new ExchangeGSMConnection();

                           exchangeGSMConnection.RequestID = _reqeust.ID;
                           exchangeGSMConnection.PostID = (int)item.PostID;
                           exchangeGSMConnection.PostContactID = (long)item.PostConntactID;
                           exchangeGSMConnection.CabinetID = (int)item.CabinetID;
                           exchangeGSMConnection.CabinetInputID = (long)item.CabinetInputID;
                           exchangeGSMConnection.BuchtStatus = (byte)item.BuchtStatus;
                           exchangeGSMConnection.FromTelephoneNo = (long)item.FromTelephone;
                           exchangeGSMConnection.FromTelephoneStatus = (byte)item.FromTelephoneStatus;
                           exchangeGSMConnection.BuchtID = (byte)item.BuchtID;
                           exchangeGSMConnection.FromSwitchPreCodeID = (byte)item.FromSwitchPreCodeID;

                               //if (item.Connectiontype == "پی سی ام")
                               //{
                               //  exchangeGSMConnection.Bucht = cabinetInputBucht.Find(t => t.CabinetInputID == (long)item.CabinetInputID && t.ConnectionID == item.PostConntactID).ID;
                               //}   
                               // else
                               //{
                               //   exchangeGSMConnection.Bucht = cabinetInputBucht.Find(t => t.CabinetInputID == (long)item.CabinetInputID && t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ID;
                               //}

                               exchangeGSMConnection.Detach();
                           _exchangeGSMInfoConncetions.Add(exchangeGSMConnection);

                       });


                        List<Bucht> updateBucht = cabinetInputBucht.Where(t => _exchangeGSMInfoConncetions.Select(t2 => (long)t2.BuchtID).Contains(t.ID)).ToList();
                        updateBucht.ForEach(t => { t.Status = (int)DB.BuchtStatus.Reserve; t.Detach(); });
                        DB.UpdateAll(updateBucht);


                        List<PostContact> ReserveToPostContacts = PostContactDB.GetPostContactByIDs(_exchangeGSMInfoConncetions.Select(t => (long)t.PostContactID).ToList());
                        ReserveToPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.FullBooking; t.Detach(); });
                        DB.UpdateAll(ReserveToPostContacts);

                        DB.SaveAll(_exchangeGSMInfoConncetions);
                        ts2.Complete();
                        IsSaveSuccess = true;
                        ShowSuccessMessage("ذخیره انجام شد");

                    }

                }
            }


            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره برگردان کافو انجام نشد .", ex);
            }
            return IsSaveSuccess;
        }

        public override bool Forward()
        {
            //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            Save();

            this.RequestID = _reqeust.ID;

            if (IsSaveSuccess == true)
            {
                //if (Data.StatusDB.IsFinalStep(this.currentStat))
                //{

                //}
                //else
                //{
                //}
                IsForwardSuccess = true;
            }

            //    ts.Complete();
            //}
            return IsForwardSuccess;
        }

        //private void ReserveSlightCabinet(List<TranslationOpticalCabinetToNormalConncetion> translationOpticalCabinetToNormalConncetions)
        //{
        //    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(translationOpticalCabinetToNormalConncetions.Select(t => t.ToCabinetInputID).ToList());
        //        cabinetInputs.ToList().ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.Exchange; t.Detach(); });
        //        DB.UpdateAll(cabinetInputs);


        //        List<PostContact> postContacts = Data.PostContactDB.GetPostContactByIDs(translationOpticalCabinetToNormalConncetions.Select(t => t.ToPostConntactID).ToList());
        //        postContacts.ToList().ForEach(t => { t.Status = (byte)DB.PostContactStatus.ReserveExchangePost; t.Detach(); });
        //        DB.UpdateAll(postContacts);

        //        ts3.Complete();
        //    }
        //}


        private void ReserveCabinet(ExchangeGSM exchangeGSM)
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(exchangeGSM.OldCabinetID);
                oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                oldCabinet.Detach();

                //Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(exchangeGSM.NewCabinetID);
                //newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                //newCabinet.Detach();

                DB.UpdateAll(new List<Cabinet> { oldCabinet /*, newCabinet*/ });

                ts3.Complete();
            }
        }
        public override bool Cancel()
        {
            try
            {

                using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
                {
                    //if (_translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.General)
                    //{
                    //    Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_translationOpticalCabinetToNormal.OldCabinetID);
                    //    if (oldCabinet != null)
                    //    {
                    //        oldCabinet.Status = (int)DB.CabinetStatus.Install;
                    //        oldCabinet.Detach();
                    //    }

                    //    Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_translationOpticalCabinetToNormal.NewCabinetID);
                    //    if (newCabinet != null)
                    //    {
                    //        newCabinet.Status = (int)DB.CabinetStatus.Install;
                    //        newCabinet.Detach();
                    //    }

                    //    DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });
                    //}
                    //else if (_translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Slight)
                    //{
                    //List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToCabinetInputID).ToList());
                    //cabinetInputs.ToList().ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.healthy; t.Detach(); });
                    //DB.UpdateAll(cabinetInputs);


                    //List<PostContact> postContacts = Data.PostContactDB.GetPostContactByIDs(_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToPostConntactID).ToList());
                    //postContacts.ToList().ForEach(t => { t.Status = (byte)DB.PostContactStatus.Free; t.Detach(); });
                    //DB.UpdateAll(postContacts);

                    //  }


                    if (_cloneOfExchangeGSMConncetion != null)
                    {
                        List<Bucht> buchts = BuchtDB.GetBuchtByCabinetInputIDs(_cloneOfExchangeGSMConncetion.Select(t => (long)t.CabinetInputID).ToList());
                        buchts.ForEach(t => { t.Status = (byte)_cloneOfExchangeGSMConncetion.Find(t2 => t2.BuchtID == t.ID).BuchtStatus; t.Detach(); });
                        DB.UpdateAll(buchts);
                    }


                    if (_cloneOfExchangeGSMConncetion != null)
                    {
                        List<PostContact> toPostContacts = PostContactDB.GetPostContactByIDs(_cloneOfExchangeGSMConncetion.Select(t => (long)t.PostConntactID).ToList());
                        toPostContacts.ForEach(t => { t.Status = (byte)_cloneOfExchangeGSMConncetion.Find(t2 => t2.BuchtID == t.ID).PostConntactStatus; t.Detach(); });
                        DB.UpdateAll(toPostContacts);
                    }


                    if (_cloneOfExchangeGSMConncetion != null)
                    {
                        List<Telephone> telephone = TelephoneDB.GetTelephoneByTelephoneNos(_cloneOfExchangeGSMConncetion.Select(t => (long)t.FromTelephone).ToList());
                        telephone.ForEach(t => { t.Status = (byte)_cloneOfExchangeGSMConncetion.Find(t2 => t2.FromTelephone == t.TelephoneNo).FromTelephoneStatus; t.Detach(); });
                        DB.UpdateAll(telephone);
                    }


                    // Data.TranslationOpticalCabinetToNormalConncetionDB.DeleteTranslationOpticalCabinetToNormalConncetionByRequestID(_translationOpticalCabinetToNormal.ID);

                    _reqeust.IsCancelation = true;

                    Data.CancelationRequestList cancelationRequest = new CancelationRequestList();

                    cancelationRequest.ID = _reqeust.ID;
                    cancelationRequest.EntryDate = DB.GetServerDate();
                    cancelationRequest.UserID = Folder.User.Current.ID;

                    cancelationRequest.Detach();
                    DB.Save(cancelationRequest, true);

                    _reqeust.Detach();
                    DB.Save(_reqeust);

                    ts3.Complete();

                }
                IsCancelSuccess = true;

                ShowSuccessMessage("ابطال درخواست انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رد درخواست، " + ex.Message + " !", ex);
            }

            return IsCancelSuccess;
        }



        private void VerifyData(ExchangeGSM exchangeGSM)
        {
            Cabinet cabinet = CabinetDB.GetCabinetByID(exchangeGSM.OldCabinetID);

            //  Cabinet NewCabinet = CabinetDB.GetCabinetByID(exchangeGSM.NewCabinetID);

            // if ((cabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal && NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal) || (cabinet.ID == NewCabinet.ID))
            //{
            //   throw new Exception("برگردان از کافو  " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), cabinet.CabinetUsageType) + " به کافو " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), NewCabinet.CabinetUsageType) + " امکان پذیر نمی باشد ");
            //}

            //if ((cabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL))
            // || (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL))
            // || (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal))
            // || (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet && NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet) )
            //{

            //}
            //else
            //{

            //}

            //if (translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.General)
            //{
            //    if (Data.TranslationOpticalCabinetToNormalDB.CheckAllBuchtCabinetIsFree(translationOpticalCabinetToNormal.NewCabinetID))
            //        throw new Exception("همه بوخت های کافو جدید باید آزاد باشند");

            //if (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL))
            //{
            //    if (Data.TranslationOpticalCabinetToNormalDB.ExistPCMInCabinet(translationOpticalCabinetToNormal.OldCabinetID))
            //        throw new Exception("برگردان پی سی ام های کافو معمولی به نوری امکان پذیر نمی باشد");

            //if (Data.TranslationOpticalCabinetToNormalDB.ExistSpecialWireInCabinet(translationOpticalCabinetToNormal.NewCabinetID))
            //    throw new Exception("برگردان سیم خصوصی کافو معمولی به نوری امکان پذیر نمی باشد");
            // }

            //int oldCabinetCount = OldCabinetInputCountTextBox.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(OldCabinetInputCountTextBox.Text.Trim());

            //int newCabinetCount = NewCabinetInputCountTextBox.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(NewCabinetInputCountTextBox.Text.Trim());

            //if (newCabinetCount < oldCabinetCount)
            //    throw new Exception("تعداد ورودی های کافو جدید کمتر از کافو قدیم می باشد");



            List<CRM.Data.ExchangeGSMConnectionInfo> inputlist = (SlightTranslationDataGrid.ItemsSource as ObservableCollection<CRM.Data.ExchangeGSMConnectionInfo>).ToList();




            if (inputlist.Any(t => t.CabinetInputID == null))
                throw new Exception("ورودی خالی نمی توان وارد کرد");

            if (inputlist.Any(t => t.PostConntactID == null))
                throw new Exception("اتصالی پست خالی نمی توان وارد کرد");

            if (inputlist.Any(t => t.PostID == null))
                throw new Exception("پست جدید خالی نمی توان وارد کرد");

            if (inputlist.Where(t => t.Connectiontype != "پی سی ام").GroupBy(t => t.CabinetInputID).Any(g => g.Count() > 1))
                throw new Exception("ورودی تکراری نمی توان وارد کرد");

            if (inputlist.GroupBy(t => t.PostConntactID).Any(g => g.Count() > 1))
                throw new Exception("اتصالی پست تکراری نمی توان وارد کرد");

            if (inputlist.GroupBy(t => t.PostConntactID).Any(g => g.Count() > 1))
                throw new Exception("اتصالی پست تکراری نمی توان وارد کرد");

            if (exchangeGSM.ID == 0)
            {
                if (Data.TranslationOpticalCabinetToNormalDB.ExistPostCntactReserve(inputlist.Select(t => (long)t.PostConntactID).ToList()))
                    throw new Exception("اتصالی پست رزرو می باشد");
            }

            //if (Data.TranslationOpticalCabinetToNormalDB.ExistPCMInPostContacts(inputlist.Select(t => (long)t.FromPostContactID).ToList()))
            //     throw new Exception("برگردان پی سی ام های کافو معمولی به نوری امکان پذیر نمی باشد");

            if (Data.TranslationOpticalCabinetToNormalDB.ExistSpecialWireInPostContacts(inputlist.Select(t => (long)t.PostConntactID).ToList()))
                throw new Exception("برگردان سیم خصوصی کافو معمولی به GSM امکان پذیر نمی باشد");


            //}
            //else if (translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Slight)
            //{
            //    List<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> inputlist = (SlightTranslationDataGrid.ItemsSource as ObservableCollection<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo>).ToList();
            //    if (inputlist.GroupBy(t => t.ToCabinetInputID).Any(g => g.Count() > 1))
            //        throw new Exception("ورودی تکراری نمی توان وارد کرد");

            //    if (inputlist.GroupBy(t => t.FromPostContactID).Any(g => g.Count() > 1))
            //        throw new Exception("اتصالی پست تکراری نمی توان وارد کرد");

            //    if (inputlist.GroupBy(t => t.ToPostConntactID).Any(g => g.Count() > 1))
            //        throw new Exception("اتصالی پست تکراری نمی توان وارد کرد");
            //}
            //else if (translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Post)
            //{

            //}


        }
        #endregion

        #region Load Controls
        ComboBox FromPostComboBox;
        private void FromPostComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            FromPostComboBox = sender as ComboBox;
        }

        ComboBox FromPostConntactComboBox;
        private void FromPostConntactComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            FromPostConntactComboBox = sender as ComboBox;
        }



        ComboBox ToPostComboBox;
        private void ToPostComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ToPostComboBox = sender as ComboBox;
        }
        //ComboBox Cabinet;
        //private void CabinetColumnComboBox_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Cabinet = sender as ComboBox;
        //}
        ComboBox ToPostConntactComboBox;
        private void ToPostConntactComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ToPostConntactComboBox = sender as ComboBox;
        }

        ComboBox CabinetInputComboBox;
        private void CabinetInputComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            CabinetInputComboBox = sender as ComboBox;
        }
        #endregion Load Controls

        #region EventHandlers



        private void SlightTranslationDelete(object sender, RoutedEventArgs e)
        {
            if (SlightTranslationDataGrid.SelectedItem != null)
            {
                _exchangeGSMInfoConncetion.Remove(_exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex]);
            }
        }
        private void PostTranslationDetailDelete(object sender, RoutedEventArgs e)
        {
            //  if (PostSelectorDataGrid.SelectedItem != null)
            //{
            //_translationOpticalCabinetToNormalPost.Remove(_translationOpticalCabinetToNormalPost[PostSelectorDataGrid.SelectedIndex]);
            // }
        }

        private void FromPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostComboBox != null && FromPostComboBox.SelectedValue != null)
            {
                _FromPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetConnectionPostContactCheckableByPostID((int)FromPostComboBox.SelectedValue));

                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactNumber = null;
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID = null;

                if (FromPostComboBox.SelectedItem != null)
                {

                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostNumber = Convert.ToInt32((FromPostComboBox.SelectedItem as CheckableItem).Name);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostID = Convert.ToInt32((FromPostComboBox.SelectedItem as CheckableItem).ID);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].AorBTypeName = Convert.ToString((FromPostComboBox.SelectedItem as CheckableItem).Description);

                }
            }
        }

        private void ToPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostComboBox != null && ToPostComboBox.SelectedValue != null)
            {

                _ToPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetFreePostContactCheckableByPostID((int)ToPostComboBox.SelectedValue));
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactNumber = null;
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID = null;

                if (ToPostComboBox.SelectedItem != null)
                {
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostNumber = Convert.ToInt32((ToPostComboBox.SelectedItem as CheckableItem).Name);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostID = Convert.ToInt32((ToPostComboBox.SelectedItem as CheckableItem).ID);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].AorBTypeName = Convert.ToString((ToPostComboBox.SelectedItem as CheckableItem).Description);
                }
            }
        }

        private void FromPostConntactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostConntactComboBox.SelectedItem != null && FromPostConntactComboBox.SelectedValue != null)
            {

                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactNumber = Convert.ToInt32((FromPostConntactComboBox.SelectedItem as CheckableItem).Name);
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID = Convert.ToInt32((FromPostConntactComboBox.SelectedItem as CheckableItem).LongID);
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].Connectiontype = Convert.ToString((FromPostConntactComboBox.SelectedItem as CheckableItem).Description);
            }

        }

        private void ToPostConntactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostConntactComboBox.SelectedItem != null && ToPostConntactComboBox.SelectedValue != null)
            {
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactNumber = Convert.ToInt32((ToPostConntactComboBox.SelectedItem as CheckableItem).Name);
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID = Convert.ToInt32((ToPostConntactComboBox.SelectedItem as CheckableItem).LongID);
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].Connectiontype = Convert.ToString((ToPostConntactComboBox.SelectedItem as CheckableItem).Description);
                if (_exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].Connectiontype == "پی سی ام")
                {
                    long postcontactID = (long)_exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID;
                    long cabinetInput = CabinetInputDB.GetPCMCabinetinputIDByPostContactID(postcontactID);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetInputID = cabinetInput;
                }
            }

        }
        private void ToCabinetInputColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID.HasValue)
            {
                if (_exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].Connectiontype == "پی سی ام")
                {
                    Folder.MessageBox.ShowError("امکان تعیین مرکزی برای اتصالی پی سی ام نمی باشد");

                    long postcontactID = (long)_exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].PostConntactID;
                    long cabinetInput = CabinetInputDB.GetPCMCabinetinputIDByPostContactID(postcontactID);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetInputID = cabinetInput;
                }
            }
            else
            {
                Folder.MessageBox.ShowError("لطفا ابتدا اتصالی پست را انتخاب کنید");
            }
        }



        private void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                CabinetInputCountTextBox.Text = Data.CabinetInputDB.GetCabinetInputCountByCabinetID((int)CabinetComboBox.SelectedValue).ToString();
                //  ToOldCabinetInputComboBox.ItemsSource = FromOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue);

                //_FromPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue));
                //FromPostColumn.ItemsSource = _FromPost;

                //ClearPostSelectorDataGrid();

                _ToPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue));
                _CabinetInput = new ObservableCollection<CheckableItem>(CabinetInputDB.GetFreeCabinetInputCheckableByCabinetID((int)CabinetComboBox.SelectedValue));
            }
        }

        private void ClearPostSelectorDataGrid()
        {
            //PostSelectorDataGrid.ItemsSource = new ObservableCollection<TranslationOpticalCabinetToNormalPost>();
        }

        private void NewCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* if (NewCabinetComboBox.SelectedValue != null)
             {
                 NewCabinetInputCountTextBox.Text = Data.CabinetInputDB.GetCabinetInputCountByCabinetID((int)NewCabinetComboBox.SelectedValue).ToString();
               //  ToOldCabinetInputComboBox.ItemsSource = FromOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);

                 List<CheckableItem> CabinetInputs = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);
                 if (_translationOpticalCabinetToNormalConncetion.Count != 0)
                 {
                     List<CheckableItem> cabinetInputs = CabinetInputDB.GetCabinetInputChechableByID(_translationOpticalCabinetToNormalConncetion.Select(t => t.ToCabinetInputID ?? 0).ToList());
                     CabinetInputs.AddRange(cabinetInputs);
                 }

                 ToCabinetInputColumn.ItemsSource = CabinetInputs; 

                 _ToPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)NewCabinetComboBox.SelectedValue));

                 ToPostColumn.ItemsSource = _ToPost;

                 ClearPostSelectorDataGrid();
             }*/
        }

        private void GSMToNormalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        private void NormalToGSMRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        #endregion

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                if (!string.IsNullOrEmpty(FromTelephonTextBox.Text.Trim()))
                {
                    long fromTelephonNo = 0;
                    if (!long.TryParse(FromTelephonTextBox.Text.Trim(), out fromTelephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");


                    long toTelephonNo = 0;
                    if (!long.TryParse(ToTelephonTextBox.Text.Trim(), out toTelephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");

                    string message = string.Empty;
                    bool inWaitingList = false;
                    if (DB.HasRestrictionsTelphone(fromTelephonNo, out message, out inWaitingList))
                        throw new Exception(message);


                    //Telephone telephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(fromTelephonNo);

                    if (NormalToGSMRadioButton.IsChecked == true)
                    {

                        List<AssignmentInfo> assignmentInfos = DB.GetAllInformationByTelephoneNoToTelephone(fromTelephonNo, toTelephonNo);

                        if (assignmentInfos.Count != 0)
                        {

                            assignmentInfos.ForEach(t =>
                            {

                                if (t.TelePhoneNo != 0)
                                {
                                    if (t.CenterID != (int)_exchangeRequestInfo.CenterComboBox.SelectedValue)
                                    {
                                        Folder.MessageBox.ShowError(".تلفن وارد شده، مربوط به این مرکز نمی باشد");
                                    }
                                    else
                                    {

                                        ExchangeGSMConnectionInfo tempExchangeGSMConnectionInfo = new ExchangeGSMConnectionInfo();
                                        //CabinetComboBox.SelectedValue = t.CabinetID;
                                        //CabinetComboBox_SelectionChanged(null, null);
                                        tempExchangeGSMConnectionInfo.CabinetID = t.CabinetID;
                                        tempExchangeGSMConnectionInfo.CabinetNumber = t.CabinetName;
                                        tempExchangeGSMConnectionInfo.BuchtID = t.BuchtID;
                                        tempExchangeGSMConnectionInfo.BuchtStatus = t.BuchtStatus;
                                        tempExchangeGSMConnectionInfo.FromTelephoneStatus = t.TelephoneStatus;
                                        tempExchangeGSMConnectionInfo.CabinetInputID = t.CabinetInputID;
                                        tempExchangeGSMConnectionInfo.CabinetInputNumber = t.InputNumber;
                                        tempExchangeGSMConnectionInfo.FromSwitchPreCodeID = t.SwitchPreCodeID;
                                        tempExchangeGSMConnectionInfo.PostID = t.PostID;
                                        tempExchangeGSMConnectionInfo.PostNumber = t.PostName;
                                        tempExchangeGSMConnectionInfo.FromTelephone = t.TelePhoneNo;
                                        tempExchangeGSMConnectionInfo.PostConntactID = t.PostContactID;
                                        tempExchangeGSMConnectionInfo.PostConntactNumber = t.PostContact;
                                        tempExchangeGSMConnectionInfo.AorBTypeName = t.AorBType;
                                        tempExchangeGSMConnectionInfo.AorBType = t.AorBTypeID;
                                        tempExchangeGSMConnectionInfo.Connectiontype = (t.PCMPortIDInBuchtTable != null ? "پی سی ام" : "معمولی");

                                        // _CabinetInput = new ObservableCollection<CheckableItem>(CabinetInputDB.GetCabinetInputByCabinetID((int)assignmentInfo.CabinetID));

                                        _exchangeGSMInfoConncetion.Add(tempExchangeGSMConnectionInfo);
                                        //new ObservableCollection<ExchangeGSMConnectionInfo>(new List<ExchangeGSMConnectionInfo> { tempExchangeGSMConnectionInfo });
                                    }

                                }
                            });
                        }
                        else
                        {
                            Folder.MessageBox.ShowError(string.Format("{2} {1} {0}", "موجود نیست", FromTelephonTextBox.Text.Trim(), ". اطلاعات شماره تلفن"));
                        }

                        //}
                        //else
                        //{
                        //    Folder.MessageBox.ShowError(".تلفن وارد شده، موجود نیست");
                        //}
                    }
                    else if (GSMToNormalRadioButton.IsChecked == true)
                    {
                        List<Telephone> telephones = TelephoneDB.GetTelephoneFromTelToTel(fromTelephonNo, toTelephonNo);

                        if (telephones.Any(t => t.UsageType != (int)DB.TelephoneUsageType.GSM))
                        {
                            throw new Exception("همه تلفن های باید GSM باشند.");
                        }

                        telephones.ForEach(t =>
                        {
                            ExchangeGSMConnectionInfo tempExchangeGSMConnectionInfo = new ExchangeGSMConnectionInfo();
                            tempExchangeGSMConnectionInfo.FromTelephone = t.TelephoneNo;
                            tempExchangeGSMConnectionInfo.FromTelephoneStatus = t.Status;
                            tempExchangeGSMConnectionInfo.FromSwitchPreCodeID = t.SwitchPrecodeID;
                            tempExchangeGSMConnectionInfo.CabinetID = (int)CabinetComboBox.SelectedValue;
                            tempExchangeGSMConnectionInfo.CabinetNumber = int.Parse(((CheckableItem)CabinetComboBox.SelectedItem).Name);
                            _exchangeGSMInfoConncetion.Add(tempExchangeGSMConnectionInfo);
                        });


                    }


                    SlightTranslationDataGrid.ItemsSource = _exchangeGSMInfoConncetion;
                }
                else
                {

                    Folder.MessageBox.ShowError(".لطفاً فیلد شماره تلفن را پر نمائید");
                    FromTelephonTextBox.Focus();
                }



            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی تلفن - اصلاح مشخصات - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        private void SelectCabinetInput_Click(object sender, RoutedEventArgs e)
        {
            List<CRM.Data.ExchangeGSMConnectionInfo> tempTranslationOpticalCabinetToNormalConncetion = new List<ExchangeGSMConnectionInfo>();

            tempTranslationOpticalCabinetToNormalConncetion = new List<ExchangeGSMConnectionInfo>();
            //TelephoneDB.GetTelephoneTranslationOpticalCabinetToNormalConnctionByCabinetID(_exchangeGSM.OldCabinetID, false);

            List<CabinetInput> freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetID(_exchangeGSM.OldCabinetID);
            List<PostContactInfo> freePostContact = PostContactDB.GetFreePostContactInfoByCabinetIDWithPCM(_exchangeGSM.OldCabinetID);

            int count = tempTranslationOpticalCabinetToNormalConncetion.Count();
            int postContactCount = freePostContact.Count();
            int freeCabinetInputCount = freeCabinetInput.Count();

            for (int i = 0; i < count; i++)
            {
                PostContactInfo postContactInfoItem = new PostContactInfo();
                // if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Sequential)
                //{
                //    postContactInfoItem = freePostContact.Where(t2 => t2.PostID == tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
                // }
                // else if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Peer)
                //{
                //    postContactInfoItem = freePostContact.Where(t => t.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType && t.PostID == tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID && t.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
                // }
                // PostContactInfo postContactInfoItem = freePostContact.Where(t => t.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType && t.PostNumber == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostNumber && t.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();

                if (postContactInfoItem != null)
                {
                    tempTranslationOpticalCabinetToNormalConncetion[i].PostConntactID = postContactInfoItem.ID;
                    tempTranslationOpticalCabinetToNormalConncetion[i].PostConntactNumber = postContactInfoItem.ConnectionNo;
                    tempTranslationOpticalCabinetToNormalConncetion[i].PostID = postContactInfoItem.PostID;
                    tempTranslationOpticalCabinetToNormalConncetion[i].PostNumber = postContactInfoItem.PostNumber;
                    tempTranslationOpticalCabinetToNormalConncetion[i].AorBTypeName = postContactInfoItem.ABTypeName;
                    tempTranslationOpticalCabinetToNormalConncetion[i].Connectiontype = postContactInfoItem.ToConnectiontype;
                    if (postContactInfoItem.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                    {
                        tempTranslationOpticalCabinetToNormalConncetion[i].CabinetInputID = (long)postContactInfoItem.CabinetInputID;
                    }
                    freePostContact.Remove(postContactInfoItem);
                }

                //if ((bool)PCMToNormalCheckBox.IsChecked)
                //{
                //    List<PostContactInfo> PostContactInfos = freePostContact.Where(t2 => t2.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType &&  t2.PostNumber == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostNumber).OrderBy(t => t.ConnectionNo).ToList();

                //    if (i < PostContactInfos.Count())
                //    {
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = PostContactInfos[i].ID;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = PostContactInfos[i].ConnectionNo;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = PostContactInfos[i].PostID;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = PostContactInfos[i].PostNumber;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = PostContactInfos[i].ABTypeName;
                //    }
                //}
                //else
                //{
                //    PostContactInfo item = freePostContact.Where(t => t.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType && t.PostNumber == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostNumber && t.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber).Take(1).SingleOrDefault();
                //    if (item != null)
                //    {
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = item.ID;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = item.ConnectionNo;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = item.PostID;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = item.PostNumber;
                //        tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = item.ABTypeName;
                //    }
                //}

            }


            for (int i = 0; i < count && i < freeCabinetInputCount; i++)
            {
                if (tempTranslationOpticalCabinetToNormalConncetion[i].CabinetInputID == null)
                    tempTranslationOpticalCabinetToNormalConncetion[i].CabinetInputID = freeCabinetInput[i].ID;
            }


            _exchangeGSMInfoConncetion = new ObservableCollection<ExchangeGSMConnectionInfo>(tempTranslationOpticalCabinetToNormalConncetion);
            SlightTranslationDataGrid.ItemsSource = _exchangeGSMInfoConncetion;
        }
        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            List<CRM.Data.ExchangeGSMConnectionInfo> tempTranslationOpticalCabinetToNormalConncetion = new List<ExchangeGSMConnectionInfo>();

            List<TranslationOpticalCabinetToNormalPost> items = new List<TranslationOpticalCabinetToNormalPost>();//(PostSelectorDataGrid.ItemsSource as ObservableCollection<TranslationOpticalCabinetToNormalPost>);

            tempTranslationOpticalCabinetToNormalConncetion = new List<ExchangeGSMConnectionInfo>();
            //TelephoneDB.GetTelephoneByCabinetID(_exchangeGSM.OldCabinetID, items.Where(t=>t.PCMToNormal == true).Select(t=>t.FromPostID).ToList() , true);
            tempTranslationOpticalCabinetToNormalConncetion = new List<ExchangeGSMConnectionInfo>();
            //tempTranslationOpticalCabinetToNormalConncetion.Union(TelephoneDB.GetTelephoneByCabinetID(_exchangeGSM.OldCabinetID, items.Where(t => t.PCMToNormal == false).Select(t => t.FromPostID).ToList(), false)).ToList();
            List<CabinetInput> freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetID(_exchangeGSM.OldCabinetID);
            List<PostContactInfo> freePostContact = PostContactDB.GetFreePostContactInfoByCabinetID(_exchangeGSM.OldCabinetID, items.Select(t => t.ToPostID).ToList());
            int count = tempTranslationOpticalCabinetToNormalConncetion.Count();
            int postContactCount = freePostContact.Count();
            int freeCabinetInputCount = freeCabinetInput.Count();

            items.ForEach(item =>
              {

                  tempTranslationOpticalCabinetToNormalConncetion.Where(t2 => t2.PostID == item.FromPostID && t2.isApply == false).ToList().ForEach(subItem =>
                  {

                      PostContactInfo postContactInfoItem = null;
                       // if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Sequential)
                       //  {
                       //      postContactInfoItem = freePostContact.Where(t2 => t2.PostID == item.ToPostID).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
                       //  }
                       // else if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Peer && subItem.FromConnectiontype != "پی سی ام")
                       // {
                       //   postContactInfoItem = freePostContact.Where(t =>t.PostID == item.ToPostID && t.ConnectionNo == subItem.FromPostConntactNumber).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
                       // }

                       if (postContactInfoItem != null)
                      {
                          subItem.PostConntactID = postContactInfoItem.ID;
                          subItem.PostConntactNumber = postContactInfoItem.ConnectionNo;
                          subItem.PostID = postContactInfoItem.PostID;
                          subItem.PostNumber = postContactInfoItem.PostNumber;
                          subItem.AorBTypeName = postContactInfoItem.ABTypeName;
                          subItem.Connectiontype = postContactInfoItem.ToConnectiontype;
                          if (postContactInfoItem.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                          {
                              subItem.CabinetInputID = (long)postContactInfoItem.CabinetInputID;
                          }
                          freePostContact.Remove(postContactInfoItem);
                          subItem.isApply = true;
                      }
                  });
              });


            //for(int i = 0 ; i< count; i++)
            //{

            //    items.Where(t => t.FromPostID == (int)tempTranslationOpticalCabinetToNormalConncetion[i].FromPostID).ToList().ForEach(item =>
            //    {

            //        PostContactInfo postContactInfoItem = new PostContactInfo();
            //        if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Sequential)
            //        {
            //             postContactInfoItem = freePostContact.Where(t2 => t2.PostID == item.ToPostID).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
            //        }
            //        else if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Peer)
            //        {
            //            postContactInfoItem = freePostContact.Where(t => t.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType && t.PostID == item.ToPostID && t.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
            //        }

            //        if (postContactInfoItem != null)
            //        {
            //            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = postContactInfoItem.ID;
            //            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = postContactInfoItem.ConnectionNo;
            //            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = postContactInfoItem.PostID;
            //            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = postContactInfoItem.PostNumber;
            //            tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = postContactInfoItem.ABTypeName;
            //            tempTranslationOpticalCabinetToNormalConncetion[i].ToConnectiontype = postContactInfoItem.ToConnectiontype;
            //            if (postContactInfoItem.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
            //            {
            //                tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID = (long)postContactInfoItem.CabinetInputID;
            //            }
            //            freePostContact.Remove(postContactInfoItem);
            //        }
            //        //if (!item.PCMToNormal)
            //        //{
            //        //    PostContactInfo postContactInfoItem = freePostContact.Find(t2 => t2.PostID == item.ToPostID && t2.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber);

            //        //    if (postContactInfoItem != null)
            //        //    {
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = postContactInfoItem.ID;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = postContactInfoItem.ConnectionNo;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = postContactInfoItem.PostID;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = postContactInfoItem.PostNumber;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = postContactInfoItem.ABTypeName;
            //        //        freePostContact.Remove(postContactInfoItem);
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    List<PostContactInfo> PostContactInfos = freePostContact.Where(t2 => t2.PostID == item.ToPostID).OrderBy(t => t.ConnectionNo).ThenBy(t=>t.ID).ToList();

            //        //    if (i < PostContactInfos.Count())
            //        //    {
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = PostContactInfos.Take(1).SingleOrDefault().ID;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = PostContactInfos.Take(1).SingleOrDefault().ConnectionNo;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = PostContactInfos.Take(1).SingleOrDefault().PostID;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = PostContactInfos.Take(1).SingleOrDefault().PostNumber;
            //        //        tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = PostContactInfos.Take(1).SingleOrDefault().ABTypeName;
            //        //        freePostContact.Remove(PostContactInfos.Take(1).SingleOrDefault());
            //        //    }
            //        //}
            //    });

            //};

            for (int i = 0; i < count && i < freeCabinetInputCount; i++)
            {
                if (tempTranslationOpticalCabinetToNormalConncetion[i].CabinetInputID == null)
                    tempTranslationOpticalCabinetToNormalConncetion[i].CabinetInputID = freeCabinetInput[i].ID;
            }


            _exchangeGSMInfoConncetion = new ObservableCollection<ExchangeGSMConnectionInfo>(tempTranslationOpticalCabinetToNormalConncetion);
            SlightTranslationDataGrid.ItemsSource = _exchangeGSMInfoConncetion;
            SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;


        }

        private void SlightTranslationDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (SlightTranslationDataGrid.SelectedItem != null && GSMToNormalRadioButton.IsChecked == true)
            {
                ExchangeGSMConnectionInfo item = SlightTranslationDataGrid.SelectedItem as ExchangeGSMConnectionInfo;
                List<ExchangeGSMConnectionInfo> items = SlightTranslationDataGrid.ItemsSource.Cast<ExchangeGSMConnectionInfo>().ToList();
                if (item != null)
                {
                    if (item.PostID != null)
                    {
                        List<CheckableItem> checkableItem = Data.PostContactDB.GetFreePostContactCheckableByPostID((int)item.PostID);
                        checkableItem.RemoveAll(t => items.Where(t2 => t2.PostConntactID != null).Select(t2 => t2.PostConntactID).Contains(t.LongID));
                        _ToPostConntact = new ObservableCollection<CheckableItem>(checkableItem);
                    }

                    if (item.PostID != null)
                    {
                        _FromPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetConnectionPostContactWithPCMCheckableByPostID((int)item.PostID, true));
                    }
                }
            }
        }

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

        private void PrintItem_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            DataSet data = ExchangeGSMList.ToDataSet("Result", TelItemsDataGrid);
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


        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns);
        }
        public override bool Print()
        {

            PrintItem_Click(null, null);
            IsPrintSuccess = true;
            //try
            //{
            //    if (this._requestID >= 0)
            //    {
            //        List<long> requestsId = new List<long> { this._requestID };

            //        var result = ReportDB.GetTranslationOpticalCabinetToNormalReport(requestsId);

            //        StiVariable dateVarible = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

            //        ReportBase.SendToPrint(result, (int)DB.UserControlNames.TranslationOpticalCabinetToNormalReport, dateVarible);
            //    }
            //    IsPrintSuccess = true;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Write(ex, "خطا در گزارش چاپ گواهی برای برگردان کافو نوری به کافو معمولی");
            //    MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            //    IsPrintSuccess = false;
            //}

            return IsPrintSuccess;
        }

        #endregion print

        private void CabinetInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetInputComboBox != null && CabinetInputComboBox.SelectedValue != null)
            {
                _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetInputNumber = null;
                if (CabinetInputComboBox.SelectedItem != null)
                {
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetInputNumber = Convert.ToInt32((CabinetInputComboBox.SelectedItem as CheckableItem).Name);
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetInputID = Convert.ToInt32((CabinetInputComboBox.SelectedItem as CheckableItem).LongID);

                    Bucht bucht = BuchtDB.GetBuchtByCabinetInputID((long)CabinetInputComboBox.SelectedValue);

                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].BuchtID = bucht.ID;
                    _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].BuchtStatus = bucht.Status;

                }
            }
        }





        //private void CabinetColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (Cabinet.SelectedItem != null && Cabinet.SelectedValue != null)
        //    {

        //        _ToPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)Cabinet.SelectedValue));
        //        _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetNumber = null;
        //        _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetID = null;

        //        if (Cabinet.SelectedItem != null)
        //        {
        //            _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetNumber = Convert.ToInt32((Cabinet.SelectedItem as CheckableItem).Name);
        //            _exchangeGSMInfoConncetion[SlightTranslationDataGrid.SelectedIndex].CabinetID = Convert.ToInt32((Cabinet.SelectedItem as CheckableItem).ID);

        //            _CabinetInput = new ObservableCollection<CheckableItem>(CabinetInputDB.GetFreeCabinetInputCheckableByCabinetID((int)Cabinet.SelectedValue));
        //        }
        //    }
        //}
    }
}




