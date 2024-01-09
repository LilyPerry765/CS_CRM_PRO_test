using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationPostForm.xaml
    /// </summary>
    public partial class TranslationPostForm : Local.RequestFormBase
    {

        #region Properties And Fields

        private long _RequestID;
        private int _RequestType;
        int _centerID = 0;

        CRM.Data.TranslationPost _translationPost { get; set; }
        Request _reqeust { get; set; }
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }

        #endregion

        #region constractor

        public TranslationPostForm()
        {
            InitializeComponent();
            Initialize();
        }

        public TranslationPostForm(long reqeustID)
            : this()
        {
            this._RequestID = reqeustID;

        }

        public TranslationPostForm(int requestType)
            : this()
        {
            this._RequestType = requestType;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
        }

        private void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_RequestID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            if (_RequestID == 0)
            {
                _translationPost = new CRM.Data.TranslationPost();
            }
            else
            {
                _translationPost = Data.TranslationPostDB.GetTranslationPostByID(_RequestID);
                _reqeust = Data.RequestDB.GetRequestByID(_RequestID);

                BeforCabinetNumberComboBox.SelectedValue = _translationPost.OldCabinetID;
                BeforCabinetNumberComboBox_SelectionChanged(null, null);

                BeforPostComboBox.SelectedValue = _translationPost.OldPostID;
                BeforPostComboBox_SelectionChanged(null, null);

                AfterPostComboBox.SelectedValue = _translationPost.NewPostID;
                AfterPostComboBox_SelectionChanged(null, null);

                if (_translationPost.OverallTransfer == true) AllTransferButton.IsSelected = true; else PartialTransferButtom.IsSelected = true;

            }
            this.DataContext = _translationPost;
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            BeforCabinetNumberComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_centerID);
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

                    _reqeust = _exchangeRequestInfo.Request;
                    _translationPost = this.DataContext as CRM.Data.TranslationPost;

                    if (AllTransferButton.IsSelected == true) { _translationPost.OverallTransfer = true; _translationPost.OldPostContactID = null; _translationPost.NewPostContactID = null; } else { _translationPost.OverallTransfer = false; }
                    Data.TranslationPostDB.VerifyData(_translationPost, this.currentStat);

                    if (_RequestID == 0)
                    {
                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.RequestPaymentTypeID = 0;
                        _reqeust.IsViewed = false;
                        _reqeust.InsertDate = DB.GetServerDate();
                        _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);

                        _translationPost.RequestID = _reqeust.ID;
                        _translationPost.InsertDate = DB.GetServerDate();
                        _translationPost.Detach();
                        DB.Save(_translationPost, true);
                    }
                    else
                    {
                        _reqeust.Detach();
                        DB.Save(_reqeust, false);

                        _translationPost.Detach();
                        DB.Save(_translationPost, false);
                    }


                    ts2.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره اطلاعات انجام شد.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
        }

        public override bool Forward()
        {
            try
            {
                using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                {
                    Save();
                    this.RequestID = _reqeust.ID;
                    if (IsSaveSuccess)
                    {
                        ReservePosts(_translationPost);

                        IsForwardSuccess = true;
                    }
                    ts1.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ارجاع درخواست", ex);
            }

            return IsForwardSuccess;
        }

        public override bool Cancel()
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                if (AllTransferButton.IsSelected == true)
                {

                    Post OldPost = Data.PostDB.GetPostByID(_translationPost.OldPostID);
                    Post NewPost = Data.PostDB.GetPostByID(_translationPost.NewPostID);


                    OldPost.Status = (byte)DB.PostStatus.Dayer;
                    OldPost.Detach();
                    DB.Save(OldPost, false);

                    NewPost.Status = (byte)DB.PostStatus.Dayer;
                    NewPost.Detach();
                    DB.Save(NewPost, false);
                }
                else if (PartialTransferButtom.IsSelected == true)
                {
                    PostContact NewPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_translationPost.NewPostContactID);
                    NewPartialPostContact.Status = (int)DB.PostContactStatus.Free;
                    NewPartialPostContact.Detach();
                    DB.Save(NewPartialPostContact, false);

                    PostContact OldPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_translationPost.OldPostContactID);
                    OldPartialPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                    OldPartialPostContact.Detach();
                    DB.Save(OldPartialPostContact, false);

                }

                Data.CancelationRequestList cancelationRequest = new CancelationRequestList();
                cancelationRequest.ID = _reqeust.ID;
                cancelationRequest.EntryDate = DB.GetServerDate();
                cancelationRequest.UserID = Folder.User.Current.ID;
                cancelationRequest.Detach();
                DB.Save(cancelationRequest, true);

                _reqeust.IsCancelation = true;
                _reqeust.Detach();
                DB.Save(_reqeust);

                IsCancelSuccess = true;

                ts3.Complete();
            }

            return true;
        }

        private void ReservePosts(Data.TranslationPost translationPost)
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                #region Reserve
                if (AllTransferButton.IsSelected == true)
                {

                    Post OldPost = Data.PostDB.GetPostByID(_translationPost.OldPostID);
                    Post NewPost = Data.PostDB.GetPostByID(_translationPost.NewPostID);


                    OldPost.Status = (byte)DB.PostStatus.ReserveForExchange;
                    OldPost.Detach();
                    DB.Save(OldPost, false);

                    NewPost.Status = (byte)DB.PostStatus.ReserveForExchange;
                    NewPost.Detach();
                    DB.Save(NewPost, false);
                }
                else if (PartialTransferButtom.IsSelected == true)
                {
                    PostContact NewPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_translationPost.NewPostContactID);
                    NewPartialPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                    NewPartialPostContact.Detach();
                    DB.Save(NewPartialPostContact, false);

                    PostContact OldPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_translationPost.OldPostContactID);
                    OldPartialPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                    OldPartialPostContact.Detach();
                    DB.Save(OldPartialPostContact, false);



                }
                #endregion Reserve
                ts3.Complete();
            }
        }

        #endregion

        #region EventHandlers

        private void BeforCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (BeforCabinetNumberComboBox.SelectedValue != null)
            {
                AfterPostComboBox.ItemsSource = BeforPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)BeforCabinetNumberComboBox.SelectedValue);
            }

        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BeforPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeforPostComboBox.SelectedValue != null)
            {
                FromOldConnectionNo.ItemsSource = Data.PostContactDB.GetPostContactByPostID((int)BeforPostComboBox.SelectedValue).Where(t => (t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal && t.Status == (byte)DB.PostContactStatus.CableConnection) || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : " " }).ToList();
                ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)BeforPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);

                PostGroup postGroup = Data.PostGroupDB.GetPostGroupByPostID((int)BeforPostComboBox.SelectedValue);
                BeforGroupPostNumberTextBox.Text = postGroup != null ? postGroup.GroupNo.ToString() : string.Empty;
                ConnectionDataGrid.Visibility = Visibility.Visible;
                this.ResizeWindow();
            }
        }

        private void AfterPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterPostComboBox.SelectedValue != null)
            {
                ToOldConnectionNo.ItemsSource = Data.PostContactDB.GetPostContactByPostID((int)AfterPostComboBox.SelectedValue).Where(t => t.Status == (byte)DB.PostContactStatus.Free && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false }).ToList();
                NewConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)AfterPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);

                PostGroup postGroup = Data.PostGroupDB.GetPostGroupByPostID((int)AfterPostComboBox.SelectedValue);
                BeforGroupPostNumberTextBox.Text = postGroup != null ? postGroup.GroupNo.ToString() : string.Empty;

                NewConnectionDataGrid.Visibility = Visibility.Visible;

                this.ResizeWindow();

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartialTransferGrid != null)
            {
                if (AllTransferButton.IsSelected == true)
                {
                    PartialTransferGrid.Visibility = Visibility.Hidden;
                }
                else if (PartialTransferButtom.IsSelected == true)
                {

                    PartialTransferGrid.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion

        #region Empty EventHandlers

        private void AllTransfer_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void PartialTransfer_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void FromOldConnectionNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    if (FromOldConnectionNo.SelectedValue != null)
            //        FromCabinetInputID.SelectedValue = oldBuchtList.Where(t => t.ConnectionID == (long)FromOldConnectionNo.SelectedValue && (t.BuchtTypeID != (int)DB.BuchtType.OutLine && t.BuchtTypeID != (int)DB.BuchtType.InLine)).Select(t => t.CabinetInputID).SingleOrDefault();
        }

        private void FirstInputAfterTransferComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion

    }
}
