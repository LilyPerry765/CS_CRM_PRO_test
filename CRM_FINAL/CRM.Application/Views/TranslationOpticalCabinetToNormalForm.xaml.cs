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
    public partial class TranslationOpticalCabinetToNormalForm : Local.RequestFormBase
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

        int _pastNewCabinetID = 0;

        int _pastOldCabinetID = 0;

        List<TranslationOpticalCabinetToNormalInfo> cabinetInputsList = new List<TranslationOpticalCabinetToNormalInfo>();
        public ObservableCollection<CheckableItem> _FromPost { get; set; }

        List<CRM.Data.TranslationOpticalCabinetToNormalConncetion> _translationOpticalCabinetToNormalConncetions = new List<TranslationOpticalCabinetToNormalConncetion>();

        public ObservableCollection<CheckableItem> _FromPostConntact { get; set; }

        public ObservableCollection<CheckableItem> _ToPost { get; set; }

        public ObservableCollection<CheckableItem> _ToPostConntact { get; set; }

        Request _reqeust { get; set; }

        TranslationOpticalCabinetToNormal _translationOpticalCabinetToNormal { get; set; }

        ObservableCollection<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> _translationOpticalCabinetToNormalConncetion { get; set; }
        ObservableCollection<CRM.Data.TranslationOpticalCabinetToNormalPost> _translationOpticalCabinetToNormalPost { get; set; }

        List<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> _cloneOfTranslationOpticalCabinetToNormalConncetion { get; set; }

        #endregion

        #region Constructor

        public TranslationOpticalCabinetToNormalForm()
        {
            InitializeComponent();
            Initialize();
        }
        public TranslationOpticalCabinetToNormalForm(long requestID)
            : this()
        {
            _requestID = requestID;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            OrderTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ExchangeOrderType));
            OrderTypeComboBox.SelectedIndex = 0;
        }

        public void LoadData()
        {
            if (Data.StatusDB.IsFinalStep(this.currentStat))
            {
                //TODO:rad 13950621
                ExchangeRequestInfoUserControl.IsEnabled = false;
                //*************************
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
            _translationOpticalCabinetToNormalPost = new ObservableCollection<TranslationOpticalCabinetToNormalPost>();

            if (_requestID == 0)
            {
                _translationOpticalCabinetToNormal = new TranslationOpticalCabinetToNormal();
                _translationOpticalCabinetToNormalConncetion = new ObservableCollection<TranslationOpticalCabinetToNormalConnctionInfo>();
                _cloneOfTranslationOpticalCabinetToNormalConncetion = new List<TranslationOpticalCabinetToNormalConnctionInfo>();
            }
            else
            {

                _translationOpticalCabinetToNormal = Data.TranslationOpticalCabinetToNormalDB.GetTranslationOpticalCabinetToNormal(_requestID);
                _reqeust = Data.RequestDB.GetRequestByID(_requestID);
                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    TelephoneDetailGroupBox.Visibility = Visibility.Visible;
                    TranslationTypeDetailGroupBox.Visibility = Visibility.Collapsed;

                    cabinetInputsList = Data.TranslationOpticalCabinetToNormalDB.GetEquivalentCabinetInputs(_translationOpticalCabinetToNormal);
                    TelItemsDataGrid.DataContext = cabinetInputsList;

                }
                else
                {


                   _translationOpticalCabinetToNormalConncetion = new ObservableCollection<TranslationOpticalCabinetToNormalConnctionInfo>(Data.TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionInfoByRequestID(_requestID));
                   _cloneOfTranslationOpticalCabinetToNormalConncetion = _translationOpticalCabinetToNormalConncetion.Select(t => (TranslationOpticalCabinetToNormalConnctionInfo)t.Clone()).ToList();



                    _pastNewCabinetID = _translationOpticalCabinetToNormal.NewCabinetID;
                    _pastOldCabinetID = _translationOpticalCabinetToNormal.OldCabinetID;


                    OldCabinetComboBox.SelectedValue = _translationOpticalCabinetToNormal.OldCabinetID;
                    OldCabinetComboBox_SelectionChanged(null, null);

                    NewCabinetComboBox.SelectedValue = _translationOpticalCabinetToNormal.NewCabinetID;
                    NewCabinetComboBox_SelectionChanged(null, null);


                    if (_translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.General)
                    {
                        GeneralTranslationRadioButton.IsChecked = true;
                    }
                    else if (_translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Slight)
                    {
                        SlightTranslationRadioButton.IsChecked = true;
                    }
                    else if (_translationOpticalCabinetToNormal.Type == (byte)DB.TranslationOpticalCabinetToNormalType.Post)
                    {
                        PostTranslationRadioButton.IsChecked = true;
                        SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                    }
                }
            }

            this.DataContext = _translationOpticalCabinetToNormal;
            SlightTranslationDataGrid.ItemsSource = _translationOpticalCabinetToNormalConncetion;
            PostSelectorDataGrid.ItemsSource = _translationOpticalCabinetToNormalPost;
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            List<CheckableItem> newCabinetInputList = Data.CabinetDB.GetCabinetCheckableByType(_centerID);
            List<CheckableItem> oldCabinetInputList = Data.CabinetDB.GetCabinetCheckableByType(_centerID);
            if (_pastNewCabinetID != 0)
            {
                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_pastNewCabinetID);
                newCabinetInputList.Add(new CheckableItem { ID = newCabinet.ID, Name = newCabinet.CabinetNumber.ToString(), IsChecked = false });
            }
            NewCabinetComboBox.ItemsSource = newCabinetInputList;

            if (_pastOldCabinetID != 0)
            {
                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_pastOldCabinetID);
                oldCabinetInputList.Add(new CheckableItem { ID = oldCabinet.ID, Name = oldCabinet.CabinetNumber.ToString(), IsChecked = false });
            }

            OldCabinetComboBox.ItemsSource = oldCabinetInputList;

        }

        private void SelectTranslationType()
        {
            if (PostTranslationRadioButton.IsChecked == true)
            {
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;
                OldCabinetLabel.Visibility = Visibility.Visible;

                NewCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;
                NewCabinetLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountTextBox.Visibility = Visibility.Visible;


                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;

                TransferWaitingListLabel.Visibility = Visibility.Visible;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Visible;


                SelectCabinetInput.Visibility = Visibility.Collapsed;
                PCMToNormalLabel.Visibility = Visibility.Collapsed;
                PCMToNormalCheckBox.Visibility = Visibility.Collapsed;
                TelephoneSearchGroupBox.Visibility = Visibility.Collapsed;
                PostSelectorGroupBox.Visibility = Visibility.Visible;

                SettingGroupBox.Visibility = Visibility.Visible;
            }
            else if (SlightTranslationRadioButton.IsChecked == true)
            {
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;
                OldCabinetLabel.Visibility = Visibility.Visible;
                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                PostSelectorGroupBox.Visibility = Visibility.Collapsed;


                NewCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;
                NewCabinetLabel.Visibility = Visibility.Visible;

                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;


                TransferWaitingListLabel.Visibility = Visibility.Collapsed;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Collapsed;


                NewCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetGroupBox.Visibility = Visibility.Collapsed;
                PCMToNormalLabel.Visibility = Visibility.Collapsed;
                PCMToNormalCheckBox.Visibility = Visibility.Collapsed;


                SelectCabinetInput.Visibility = Visibility.Collapsed;
                PCMToNormalLabel.Visibility = Visibility.Collapsed;
                PCMToNormalCheckBox.Visibility = Visibility.Collapsed;
                TelephoneSearchGroupBox.Visibility = Visibility.Visible;

                SettingGroupBox.Visibility = Visibility.Collapsed;


            }
            else if (GeneralTranslationRadioButton.IsChecked == true)
            {
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;
                OldCabinetLabel.Visibility = Visibility.Visible;

                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;

                NewCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;
                NewCabinetLabel.Visibility = Visibility.Visible;

                NewCabinetInputCountLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountTextBox.Visibility = Visibility.Visible;

                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;


                TransferWaitingListLabel.Visibility = Visibility.Visible;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Visible;

                SelectCabinetInput.Visibility = Visibility.Visible;
                PCMToNormalLabel.Visibility = Visibility.Visible;
                PCMToNormalCheckBox.Visibility = Visibility.Visible;
                TelephoneSearchGroupBox.Visibility = Visibility.Collapsed;
                PostSelectorGroupBox.Visibility = Visibility.Collapsed;

                SettingGroupBox.Visibility = Visibility.Visible;
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
                        _translationOpticalCabinetToNormal = this.DataContext as TranslationOpticalCabinetToNormal;
                        Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_translationOpticalCabinetToNormal.OldCabinetID);
                        Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_translationOpticalCabinetToNormal.NewCabinetID);

                        _translationOpticalCabinetToNormal.OldCabinetUsageTypeID = oldCabinet.CabinetUsageType;
                        _translationOpticalCabinetToNormal.NewCabinetUsageTypeID = newCabinet.CabinetUsageType;

                        if (GeneralTranslationRadioButton.IsChecked == true)
                        {
                            _translationOpticalCabinetToNormal.Type = (byte)DB.TranslationOpticalCabinetToNormalType.General;
                            _translationOpticalCabinetToNormal.FromNewCabinetInputID = null;
                            _translationOpticalCabinetToNormal.FromOldCabinetInputID = null;
                            _translationOpticalCabinetToNormal.ToNewCabinetInputID = null;
                            _translationOpticalCabinetToNormal.ToOldCabinetInputID = null;
                        }
                        else if (SlightTranslationRadioButton.IsChecked == true)
                        {
                            _translationOpticalCabinetToNormal.Type = (byte)DB.TranslationOpticalCabinetToNormalType.Slight;
                            _translationOpticalCabinetToNormal.FromNewCabinetInputID = null;
                            _translationOpticalCabinetToNormal.FromOldCabinetInputID = null;
                            _translationOpticalCabinetToNormal.ToNewCabinetInputID = null;
                            _translationOpticalCabinetToNormal.ToOldCabinetInputID = null;
                            _translationOpticalCabinetToNormal.TransferWaitingList  = false;
                        }
                        else if (PostTranslationRadioButton.IsChecked == true)
                        {
                            _translationOpticalCabinetToNormal.Type = (byte)DB.TranslationOpticalCabinetToNormalType.Post;
                        }

                        // Verify data
                        VerifyData(_translationOpticalCabinetToNormal);

                        List<AssignmentInfo> asignmentInfos = DB.GetAllInformationPostContactIDs(_translationOpticalCabinetToNormalConncetion.Select(t => (long)t.FromPostContactID).ToList());
                        List<Bucht> cabinetInputBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(_translationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToCabinetInputID).ToList());

                        using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                        {

                            if (_translationOpticalCabinetToNormalConncetion.Count() == 1)
                            {

                                AssignmentInfo asignmentInfo = asignmentInfos[0];

                                if (_reqeust.TelephoneNo != asignmentInfo.TelePhoneNo)
                                {
                                    bool inWaitingList = false;
                                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(asignmentInfos.Select(t => (long)t.TelePhoneNo).ToList() , out inWaitingList);
                                    if (!string.IsNullOrEmpty(requestName))
                                    {
                                        Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                        throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                    }
                                    else
                                    {
                                        _reqeust.TelephoneNo = asignmentInfo.TelePhoneNo;
                                    }
                                }


                            }
                            else
                            {
                                List<long> newTelephones = asignmentInfos.Where(t => !_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t2 => t2.FromPostContactID).Contains(t.PostContactID)).Select(t => (long)t.TelePhoneNo).ToList();
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
                            _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.TranslationOpticalCabinetToNormal, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                            _reqeust.Detach();
                            DB.Save(_reqeust, true);

                            _translationOpticalCabinetToNormal.ID = _reqeust.ID;

                            _translationOpticalCabinetToNormal.Detach();
                            DB.Save(_translationOpticalCabinetToNormal, true);

                            _requestID = _reqeust.ID;
                        }
                        else
                        {
                            if (_cloneOfTranslationOpticalCabinetToNormalConncetion != null)
                            {
                                List<Bucht> buchts = BuchtDB.GetBuchtByCabinetInputIDs(_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToCabinetInputID).ToList());
                                buchts.ForEach(t => { t.Status = (int)DB.BuchtStatus.Free; t.Detach(); });
                                DB.UpdateAll(buchts);
                            }


                            if (_cloneOfTranslationOpticalCabinetToNormalConncetion != null)
                            {
                                List<PostContact> toPostContacts = PostContactDB.GetPostContactByIDs(_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToPostConntactID).ToList());
                                toPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.Free; t.Detach(); });
                                DB.UpdateAll(toPostContacts);
                            }


                            Data.TranslationOpticalCabinetToNormalConncetionDB.DeleteTranslationOpticalCabinetToNormalConncetionByRequestID(_translationOpticalCabinetToNormal.ID);

                            _reqeust.Detach();
                            DB.Save(_reqeust, false);

                            _translationOpticalCabinetToNormal.Detach();
                            DB.Save(_translationOpticalCabinetToNormal, false);
                        }



                        if (GeneralTranslationRadioButton.IsChecked == true)
                        {
                            if (_pastOldCabinetID != 0 && _pastOldCabinetID != _translationOpticalCabinetToNormal.OldCabinetID)
                            {
                                Cabinet oldPastCabinet = Data.CabinetDB.GetCabinetByID(_pastOldCabinetID);
                                oldPastCabinet.Status = (int)DB.CabinetStatus.Install;
                                oldPastCabinet.Detach();
                            }
       
                            oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                            oldCabinet.Detach();


                            if (_pastNewCabinetID != 0 && _pastNewCabinetID != _translationOpticalCabinetToNormal.NewCabinetID)
                            {
                                Cabinet newPastCabinet = Data.CabinetDB.GetCabinetByID(_pastOldCabinetID);
                                newPastCabinet.Status = (int)DB.CabinetStatus.Install;
                                newPastCabinet.Detach();
                            }
                          
                            newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                            newCabinet.Detach();

                            DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });

                        }

                            _translationOpticalCabinetToNormalConncetion.ToList().ForEach(item =>
                           {
                               CRM.Data.TranslationOpticalCabinetToNormalConncetion translationOpticalCabinetToNormalConncetion = new TranslationOpticalCabinetToNormalConncetion();
                               AssignmentInfo asignmentInfo = asignmentInfos.Find(t => t.PostContactID == item.FromPostContactID);
                               if (asignmentInfo == null) throw new Exception("اطلاعات مشترک یافت نشد");
                               translationOpticalCabinetToNormalConncetion.FromTelephoneNo = asignmentInfo.TelePhoneNo;
                               translationOpticalCabinetToNormalConncetion.FromCabinetInputID = asignmentInfo.InputNumberID;
                               translationOpticalCabinetToNormalConncetion.FromBucht = asignmentInfo.BuchtID;
                               translationOpticalCabinetToNormalConncetion.CustomerID = asignmentInfo.Customer.ID;
                               translationOpticalCabinetToNormalConncetion.InstallAddressID = asignmentInfo.InstallAddress.ID;
                               translationOpticalCabinetToNormalConncetion.CorrespondenceAddressID = asignmentInfo.CorrespondenceAddress.ID;

                               translationOpticalCabinetToNormalConncetion.RequestID = _reqeust.ID;
                               translationOpticalCabinetToNormalConncetion.FromPostID = (int)item.FromPostID;
                               translationOpticalCabinetToNormalConncetion.FromPostContactID = (long)item.FromPostContactID;
                               translationOpticalCabinetToNormalConncetion.ToPostID = (int)item.ToPostID;
                               translationOpticalCabinetToNormalConncetion.ToPostConntactID = (long)item.ToPostConntactID;
                               translationOpticalCabinetToNormalConncetion.ToCabinetInputID = (long)item.ToCabinetInputID;
                               if (item.ToConnectiontype == "پی سی ام")
                               {
                                   translationOpticalCabinetToNormalConncetion.ToBucht = cabinetInputBucht.Find(t => t.CabinetInputID == (long)item.ToCabinetInputID && t.ConnectionID == item.ToPostConntactID).ID;

                               }   
                               else
                               {
                                   translationOpticalCabinetToNormalConncetion.ToBucht = cabinetInputBucht.Find(t => t.CabinetInputID == (long)item.ToCabinetInputID && t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ID;
                               }
                               translationOpticalCabinetToNormalConncetion.Detach();
                               _translationOpticalCabinetToNormalConncetions.Add(translationOpticalCabinetToNormalConncetion);

                           });


                            List<Bucht> updateBucht = cabinetInputBucht.Where(t => _translationOpticalCabinetToNormalConncetions.Select(t2 => t2.ToBucht).Contains(t.ID)).ToList();
                            updateBucht.ForEach(t => { t.Status = (int)DB.BuchtStatus.Reserve; t.Detach(); });
                            DB.UpdateAll(updateBucht);


                            List<PostContact> ReserveToPostContacts = PostContactDB.GetPostContactByIDs(_translationOpticalCabinetToNormalConncetions.Select(t => (long)t.ToPostConntactID).ToList());
                            ReserveToPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.FullBooking; t.Detach(); });
                            DB.UpdateAll(ReserveToPostContacts);

                            DB.SaveAll(_translationOpticalCabinetToNormalConncetions);
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


        private void ReserveCabinet(TranslationOpticalCabinetToNormal translationOpticalCabinetToNormal)
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.OldCabinetID);
                oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                oldCabinet.Detach();

                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.NewCabinetID);
                newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                newCabinet.Detach();

                DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });

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


                    if (_cloneOfTranslationOpticalCabinetToNormalConncetion != null)
                    {
                        List<Bucht> buchts = BuchtDB.GetBuchtByCabinetInputIDs(_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToCabinetInputID).ToList());
                        buchts.ForEach(t => { t.Status = (int)DB.BuchtStatus.Free; t.Detach(); });
                        DB.UpdateAll(buchts);
                    }


                    if (_cloneOfTranslationOpticalCabinetToNormalConncetion != null)
                    {
                        List<PostContact> toPostContacts = PostContactDB.GetPostContactByIDs(_cloneOfTranslationOpticalCabinetToNormalConncetion.Select(t => (long)t.ToPostConntactID).ToList());
                        toPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.Free; t.Detach(); });
                        DB.UpdateAll(toPostContacts);
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



        private void VerifyData(TranslationOpticalCabinetToNormal translationOpticalCabinetToNormal)
        {
            Cabinet oldCabinet = CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.OldCabinetID);

            Cabinet newCabinet = CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.NewCabinetID);

            if (
                 (oldCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal && newCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal) //برگردان معمولی به معمولی غیرمعتبر میباشد
                 || 
                 (oldCabinet.ID == newCabinet.ID)
               )
            {
                throw new Exception("برگردان از کافو  " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), oldCabinet.CabinetUsageType) + " به کافو " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), newCabinet.CabinetUsageType) + " امکان پذیر نمی باشد ");
            }

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



                List<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> inputlist = (SlightTranslationDataGrid.ItemsSource as ObservableCollection<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo>).ToList();




                if (inputlist.Any(t => t.ToCabinetInputID == null))
                    throw new Exception("ورودی خالی نمی توان وارد کرد");

                if (inputlist.Any(t => t.ToPostConntactID == null))
                    throw new Exception("اتصالی پست خالی نمی توان وارد کرد");

                if (inputlist.Any(t => t.ToPostID == null))
                    throw new Exception("پست جدید خالی نمی توان وارد کرد");

                if (inputlist.Where(t => t.ToConnectiontype != "پی سی ام" ).GroupBy(t => t.ToCabinetInputID).Any(g => g.Count() > 1))
                    throw new Exception("ورودی تکراری نمی توان وارد کرد");

                if (inputlist.GroupBy(t => t.FromPostContactID).Any(g => g.Count() > 1))
                    throw new Exception("اتصالی پست تکراری نمی توان وارد کرد");

                if (inputlist.GroupBy(t => t.ToPostConntactID).Any(g => g.Count() > 1))
                    throw new Exception("اتصالی پست تکراری نمی توان وارد کرد");

                if (translationOpticalCabinetToNormal.ID == 0)
                {
                    if (Data.TranslationOpticalCabinetToNormalDB.ExistPostCntactReserve(inputlist.Select(t => (long)t.FromPostContactID).ToList()))
                        throw new Exception("اتصالی پست رزرو می باشد");
                }

                //if (Data.TranslationOpticalCabinetToNormalDB.ExistPCMInPostContacts(inputlist.Select(t => (long)t.FromPostContactID).ToList()))
                //     throw new Exception("برگردان پی سی ام های کافو معمولی به نوری امکان پذیر نمی باشد");

                if (Data.TranslationOpticalCabinetToNormalDB.ExistSpecialWireInPostContacts(inputlist.Select(t=>(long)t.FromPostContactID).ToList()))
                    throw new Exception("برگردان سیم خصوصی کافو معمولی به نوری امکان پذیر نمی باشد");


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
        ComboBox ToPostConntactComboBox;
        private void ToPostConntactComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ToPostConntactComboBox = sender as ComboBox;
        }
        #endregion Load Controls

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SlightTranslationDelete(object sender, RoutedEventArgs e)
        {
            if (SlightTranslationDataGrid.SelectedItem != null)
            {
                _translationOpticalCabinetToNormalConncetion.Remove(_translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex]);
            }
        }
        private void PostTranslationDetailDelete(object sender, RoutedEventArgs e)
        {
            if (PostSelectorDataGrid.SelectedItem != null)
            {
                _translationOpticalCabinetToNormalPost.Remove(_translationOpticalCabinetToNormalPost[PostSelectorDataGrid.SelectedIndex]);
            }
        }

        private void FromPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostComboBox!= null && FromPostComboBox.SelectedValue != null)
            {
                _FromPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetConnectionPostContactCheckableByPostID((int)FromPostComboBox.SelectedValue));

                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromPostConntactNumber = null;
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromPostContactID = null;

                if (FromPostComboBox.SelectedItem != null)
                {

                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromPostNumber = Convert.ToInt32((FromPostComboBox.SelectedItem as CheckableItem).Name);
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromPostID = Convert.ToInt32((FromPostComboBox.SelectedItem as CheckableItem).ID);
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromAorBTypeName = Convert.ToString((FromPostComboBox.SelectedItem as CheckableItem).Description);

                }
            }
        }

        private void ToPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostComboBox != null && ToPostComboBox.SelectedValue != null)
            {

                _ToPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetFreePostContactCheckableByPostID((int)ToPostComboBox.SelectedValue));
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactNumber = null;
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID = null;

                if (ToPostComboBox.SelectedItem != null)
                {
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostNumber = Convert.ToInt32((ToPostComboBox.SelectedItem as CheckableItem).Name);
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostID = Convert.ToInt32((ToPostComboBox.SelectedItem as CheckableItem).ID);
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToAorBTypeName = Convert.ToString((ToPostComboBox.SelectedItem as CheckableItem).Description);
                }
            }
        }

        private void FromPostConntactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostConntactComboBox.SelectedItem != null && FromPostConntactComboBox.SelectedValue != null )
            {

                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromPostConntactNumber = Convert.ToInt32((FromPostConntactComboBox.SelectedItem as CheckableItem).Name);
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromPostContactID = Convert.ToInt32((FromPostConntactComboBox.SelectedItem as CheckableItem).LongID);
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].FromConnectiontype = Convert.ToString((FromPostConntactComboBox.SelectedItem as CheckableItem).Description);
            }

        }

        private void ToPostConntactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostConntactComboBox.SelectedItem != null && ToPostConntactComboBox.SelectedValue != null )
            {
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactNumber = Convert.ToInt32((ToPostConntactComboBox.SelectedItem as CheckableItem).Name);
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID = Convert.ToInt32((ToPostConntactComboBox.SelectedItem as CheckableItem).LongID);
                _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToConnectiontype = Convert.ToString((ToPostConntactComboBox.SelectedItem as CheckableItem).Description);
                if (_translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToConnectiontype == "پی سی ام")
                {
                    long postcontactID = (long)_translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID;
                    long cabinetInput = CabinetInputDB.GetPCMCabinetinputIDByPostContactID(postcontactID);
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToCabinetInputID = cabinetInput;
                }
            }

        }
        private void ToCabinetInputColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID.HasValue)
            {
                if (_translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToConnectiontype == "پی سی ام")
                {
                    Folder.MessageBox.ShowError("امکان تعیین مرکزی برای اتصالی پی سی ام نمی باشد");

                    long postcontactID = (long)_translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID;
                    long cabinetInput = CabinetInputDB.GetPCMCabinetinputIDByPostContactID(postcontactID);
                    _translationOpticalCabinetToNormalConncetion[SlightTranslationDataGrid.SelectedIndex].ToCabinetInputID = cabinetInput;
                }
            }
            else
            {
                Folder.MessageBox.ShowError("لطفا ابتدا اتصالی پست را انتخاب کنید");
            }
        }



        private void OldCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldCabinetComboBox.SelectedValue != null)
            {
                OldCabinetInputCountTextBox.Text = Data.CabinetInputDB.GetCabinetInputCountByCabinetID((int)OldCabinetComboBox.SelectedValue).ToString();
              //  ToOldCabinetInputComboBox.ItemsSource = FromOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue);

                _FromPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)OldCabinetComboBox.SelectedValue));
                FromPostColumn.ItemsSource = _FromPost;

                ClearPostSelectorDataGrid();
            }
        }

        private void ClearPostSelectorDataGrid()
        {
            PostSelectorDataGrid.ItemsSource = new ObservableCollection<TranslationOpticalCabinetToNormalPost>();
        }

        private void NewCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewCabinetComboBox.SelectedValue != null)
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
            }
        }

        private void GeneralTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        private void SlightTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        private void PostTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }



        #endregion

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                TranslationOpticalCabinetToNormalConnctionInfo tempTranslationOpticalCabinetToNormalConncetion = new TranslationOpticalCabinetToNormalConnctionInfo();
                if (!string.IsNullOrEmpty(TelephonTextBox.Text.Trim()))
                {
                    long telephonNo = 0;
                    if (!long.TryParse(TelephonTextBox.Text.Trim(), out telephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");

                    string message = string.Empty;
                    bool inWaitingList = false;
                    if (DB.HasRestrictionsTelphone(telephonNo, out message, out inWaitingList))
                        throw new Exception(message);


                    Telephone telephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephonNo);
                    if (telephoneItem != null)
                    {

                       AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(telephonNo);

                        if (assignmentInfo != null)
                        {
                            if (assignmentInfo.CenterID != (int)_exchangeRequestInfo.CenterComboBox.SelectedValue)
                            {
                                Folder.MessageBox.ShowError(".تلفن وارد شده، مربوط به این مرکز نمی باشد");
                            }
                            else
                            {

                                OldCabinetComboBox.SelectedValue = assignmentInfo.CabinetID;
                                OldCabinetComboBox_SelectionChanged(null, null);
                                tempTranslationOpticalCabinetToNormalConncetion.FromPostID = assignmentInfo.PostID;
                                tempTranslationOpticalCabinetToNormalConncetion.FromTelephone = assignmentInfo.TelePhoneNo;
                                tempTranslationOpticalCabinetToNormalConncetion.FromPostNumber = assignmentInfo.PostName;
                                tempTranslationOpticalCabinetToNormalConncetion.FromPostContactID = assignmentInfo.PostContactID;
                                tempTranslationOpticalCabinetToNormalConncetion.FromPostConntactNumber = assignmentInfo.PostContact;
                                tempTranslationOpticalCabinetToNormalConncetion.FromAorBTypeName = assignmentInfo.AorBType;
                                tempTranslationOpticalCabinetToNormalConncetion.FromAorBType = assignmentInfo.AorBTypeID;
                                tempTranslationOpticalCabinetToNormalConncetion.FromConnectiontype = (assignmentInfo.PCMPortIDInBuchtTable != null ? "پی سی ام" : "معمولی");
                                _translationOpticalCabinetToNormalConncetion = new ObservableCollection<TranslationOpticalCabinetToNormalConnctionInfo>(new List<TranslationOpticalCabinetToNormalConnctionInfo> { tempTranslationOpticalCabinetToNormalConncetion });
                                SlightTranslationDataGrid.ItemsSource = _translationOpticalCabinetToNormalConncetion;
                            }

                        }
                        else
                        {
                              Folder.MessageBox.ShowError( string.Format("{2} {1} {0}", "موجود نیست", TelephonTextBox.Text.Trim(), ". اطلاعات شماره تلفن"));
                        }
                    }
                    else
                    {
                        Folder.MessageBox.ShowError(".تلفن وارد شده، موجود نیست");
                    }
                }
                else
                {

                    Folder.MessageBox.ShowError(".لطفاً فیلد شماره تلفن را پر نمائید");
                    TelephonTextBox.Focus();
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
            List<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> tempTranslationOpticalCabinetToNormalConncetion = new List<TranslationOpticalCabinetToNormalConnctionInfo>();

            tempTranslationOpticalCabinetToNormalConncetion = TelephoneDB.GetTelephoneTranslationOpticalCabinetToNormalConnctionByCabinetID(_translationOpticalCabinetToNormal.OldCabinetID, (bool)PCMToNormalCheckBox.IsChecked);

            List<CabinetInput> freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetID(_translationOpticalCabinetToNormal.NewCabinetID);
            List<PostContactInfo> freePostContact = PostContactDB.GetFreePostContactInfoByCabinetIDWithPCM(_translationOpticalCabinetToNormal.NewCabinetID);

            int count = tempTranslationOpticalCabinetToNormalConncetion.Count();
            int postContactCount = freePostContact.Count();
            int freeCabinetInputCount = freeCabinetInput.Count();

            for (int i = 0; i < count; i++)
            {
                PostContactInfo postContactInfoItem = new PostContactInfo();
                if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Sequential)
                {
                    postContactInfoItem = freePostContact.Where(t2 => t2.PostID == tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID)
                                                         .OrderBy(t => t.ConnectionNo)
                                                         .ThenBy(t => t.ID)
                                                         .Take(1)
                                                         .SingleOrDefault();
                }
                else if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Peer)
                {
                    postContactInfoItem = freePostContact.Where(t => 
                                                                    t.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType && 
                                                                    t.PostID == tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID && 
                                                                    t.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber
                                                                )
                                                         .OrderBy(t => t.ConnectionNo)
                                                         .ThenBy(t => t.ID)
                                                         .Take(1)
                                                         .SingleOrDefault();
                }
               // PostContactInfo postContactInfoItem = freePostContact.Where(t => t.ABType == tempTranslationOpticalCabinetToNormalConncetion[i].FromAorBType && t.PostNumber == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostNumber && t.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();

                if (postContactInfoItem != null)
                {
                    tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = postContactInfoItem.ID;
                    tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = postContactInfoItem.ConnectionNo;
                    tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = postContactInfoItem.PostID;
                    tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = postContactInfoItem.PostNumber;
                    tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = postContactInfoItem.ABTypeName;
                    tempTranslationOpticalCabinetToNormalConncetion[i].ToConnectiontype = postContactInfoItem.ToConnectiontype;
                    if (postContactInfoItem.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                    {
                        tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID = (long)postContactInfoItem.CabinetInputID;
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
                if (tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID == null)
                tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID = freeCabinetInput[i].ID;
            }


            _translationOpticalCabinetToNormalConncetion = new ObservableCollection<TranslationOpticalCabinetToNormalConnctionInfo>(tempTranslationOpticalCabinetToNormalConncetion);
            SlightTranslationDataGrid.ItemsSource = _translationOpticalCabinetToNormalConncetion;
        }
        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            List<CRM.Data.TranslationOpticalCabinetToNormalConnctionInfo> tempTranslationOpticalCabinetToNormalConncetion = new List<TranslationOpticalCabinetToNormalConnctionInfo>();

            List<TranslationOpticalCabinetToNormalPost> items = new List<TranslationOpticalCabinetToNormalPost>(PostSelectorDataGrid.ItemsSource as ObservableCollection<TranslationOpticalCabinetToNormalPost>);

            tempTranslationOpticalCabinetToNormalConncetion = TelephoneDB.GetTelephoneByCabinetID(_translationOpticalCabinetToNormal.OldCabinetID, items.Where(t=>t.PCMToNormal == true).Select(t=>t.FromPostID).ToList() , true);
            tempTranslationOpticalCabinetToNormalConncetion = tempTranslationOpticalCabinetToNormalConncetion.Union(TelephoneDB.GetTelephoneByCabinetID(_translationOpticalCabinetToNormal.OldCabinetID, items.Where(t => t.PCMToNormal == false).Select(t => t.FromPostID).ToList(), false)).ToList();
            List<CabinetInput> freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetID(_translationOpticalCabinetToNormal.NewCabinetID);
            List<PostContactInfo> freePostContact = PostContactDB.GetFreePostContactInfoByCabinetID(_translationOpticalCabinetToNormal.NewCabinetID, items.Select(t => t.ToPostID).ToList());
            int count = tempTranslationOpticalCabinetToNormalConncetion.Count();
            int postContactCount = freePostContact.Count();
            int freeCabinetInputCount = freeCabinetInput.Count();

             items.ForEach(item =>
               {

                   tempTranslationOpticalCabinetToNormalConncetion.Where(t2 => t2.FromPostID == item.FromPostID && t2.isApply == false).ToList().ForEach(subItem =>
                   {

                       PostContactInfo postContactInfoItem = null;
                       if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Sequential)
                       {
                           postContactInfoItem = freePostContact.Where(t2 => t2.PostID == item.ToPostID).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
                       }
                       else if ((int)OrderTypeComboBox.SelectedValue == (int)DB.ExchangeOrderType.Peer && subItem.FromConnectiontype != "پی سی ام")
                       {
                           postContactInfoItem = freePostContact.Where(t =>t.PostID == item.ToPostID && t.ConnectionNo == subItem.FromPostConntactNumber).OrderBy(t => t.ConnectionNo).ThenBy(t => t.ID).Take(1).SingleOrDefault();
                       }

                       if (postContactInfoItem != null)
                       {
                           subItem.ToPostConntactID = postContactInfoItem.ID;
                           subItem.ToPostConntactNumber = postContactInfoItem.ConnectionNo;
                           subItem.ToPostID = postContactInfoItem.PostID;
                           subItem.ToPostNumber = postContactInfoItem.PostNumber;
                           subItem.ToAorBTypeName = postContactInfoItem.ABTypeName;
                           subItem.ToConnectiontype = postContactInfoItem.ToConnectiontype;
                           if (postContactInfoItem.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                           {
                               subItem.ToCabinetInputID = (long)postContactInfoItem.CabinetInputID;
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
                if (tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID == null)
                tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID = freeCabinetInput[i].ID;
            }


            _translationOpticalCabinetToNormalConncetion = new ObservableCollection<TranslationOpticalCabinetToNormalConnctionInfo>(tempTranslationOpticalCabinetToNormalConncetion);
            SlightTranslationDataGrid.ItemsSource = _translationOpticalCabinetToNormalConncetion;
            SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
        }

        private void SlightTranslationDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if(SlightTranslationDataGrid.SelectedItem != null)
            {
                TranslationOpticalCabinetToNormalConnctionInfo item = SlightTranslationDataGrid.SelectedItem as TranslationOpticalCabinetToNormalConnctionInfo;
                List<TranslationOpticalCabinetToNormalConnctionInfo> items = SlightTranslationDataGrid.ItemsSource.Cast<TranslationOpticalCabinetToNormalConnctionInfo>().ToList();
                if (item != null)
               {
                    if(item.ToPostID != null)
                    {
                        List<CheckableItem> checkableItem = Data.PostContactDB.GetFreePostContactCheckableByPostID((int)item.ToPostID);
                        checkableItem.RemoveAll(t => items.Where(t2=>t2.ToPostConntactID != null).Select(t2 => t2.ToPostConntactID).Contains(t.LongID));
                        _ToPostConntact = new ObservableCollection<CheckableItem>(checkableItem);
                    }

                    if (item.FromPostID != null)
                    {
                       _FromPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetConnectionPostContactWithPCMCheckableByPostID((int)item.FromPostID, true));
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
            DataSet data = cabinetInputsList.ToDataSet("Result", TelItemsDataGrid);
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










    }
}
