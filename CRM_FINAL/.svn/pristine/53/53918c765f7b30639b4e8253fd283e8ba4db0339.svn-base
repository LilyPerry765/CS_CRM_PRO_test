 using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class TranslationPostInputFom : Local.RequestFormBase
    {
        private long _RequestID;
        private int _RequestType;
        int _centerID = 0;

        CRM.Data.TranslationPostInput _translationPostInput { get; set; }
        List<CRM.Data.TranslationPostInputConectionSelection> _translationPostInputConectionSelection { get; set; }

        ObservableCollection<CRM.Data.TranslationPostInputConectionSelection> _cloneOfTranslationPostInputConectionSelection { get; set; }

        List<CRM.Data.TranslationPostInputConnection> translationPostInputConnections = new List<TranslationPostInputConnection>();
        Request _reqeust { get; set; }
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }


        #region constractor
        public TranslationPostInputFom()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
        }
        public TranslationPostInputFom(int requestType)
            : this()
        {
            this._RequestType = requestType;
        }
        public TranslationPostInputFom(long reqeustID): this()
        {
            this._RequestID = reqeustID;

        }

        #endregion

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_RequestID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;


            _translationPostInputConectionSelection = Data.TranslationPostInputDB.GetTranslationPostInputConectionSelectionByRequestID(_RequestID);
           // _oldGroupingCablePairs = new List<GroupingCablePair>(_groupingCablePairs.Select(t => (GroupingCablePair)t.Clone()));
            _cloneOfTranslationPostInputConectionSelection = new ObservableCollection<TranslationPostInputConectionSelection>( _translationPostInputConectionSelection.Select(t => (TranslationPostInputConectionSelection)t.Clone()).ToList());
            ConnectionSelectionDataGrid.ItemsSource = _cloneOfTranslationPostInputConectionSelection;

            if (_RequestID == 0)
            {

                _translationPostInput = new CRM.Data.TranslationPostInput();
            }
            else
            {
                _translationPostInput = Data.TranslationPostInputDB.GetTranslationPostInputByID(_RequestID);   
                _reqeust = Data.RequestDB.GetRequestByID(_RequestID);

                FromCabinetNumberComboBox.SelectedValue = _translationPostInput.FromCabinetID;
                FromCabinetNumberComboBox_SelectionChanged(null, null);
                
                FromPostComboBox.SelectedValue = _translationPostInput.FromPostID;
                FromPostComboBox_SelectionChanged(null, null);

                ToCabinetNumberComboBox.SelectedValue = _translationPostInput.ToCabinetID;
                ToCabinetNumberComboBox_SelectionChanged(null, null);

                ToPostComboBox.SelectedValue = _translationPostInput.ToPostID;
                ToPostComboBox_SelectionChanged(null, null);
            }
            this.DataContext = _translationPostInput;
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            ToCabinetNumberComboBox.ItemsSource = FromCabinetNumberComboBox.ItemsSource = Data.CabinetDB.GetNormalCabinetCheckableByCenterID(_centerID);
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

                    // در این برگردان مرکزی های اتصالی های پست عوض می شود
                    // دو حالت برای این برگردان درنظر گرفته شده است 
                    // یک : کافو و پست تغییر نمی کند که در این حالت فقط مرکزی عوض می شود 
                    // دو : در این حالت کافو و پست عوض می شود و نیاز می باشد معادل اتصالی در پست اولیه در پست بعد از برگردان آزاد باشد  

                    _reqeust = _exchangeRequestInfo.Request;
                    _translationPostInput = this.DataContext as CRM.Data.TranslationPostInput;



                    Data.TranslationPostInputDB.VerifyData(_translationPostInput, _cloneOfTranslationPostInputConectionSelection.ToList());

                    if (_RequestID == 0)
                    {
                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.RequestPaymentTypeID = 0;
                        _reqeust.IsViewed = false;
                        _reqeust.InsertDate = DB.GetServerDate();
                        _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);
                        _RequestID = _reqeust.ID;
                        
 

                        _translationPostInput.RequestID = _reqeust.ID;
                        _translationPostInput.InsertDate = DB.GetServerDate();
                        _translationPostInput.Detach();
                        DB.Save(_translationPostInput, true);
                    }
                    else
                    {
                        _reqeust.Detach();
                        DB.Save(_reqeust, false);

                        _translationPostInput.Detach();
                        DB.Save(_translationPostInput, false);

                        DB.DeleteAll<TranslationPostInputConnection>(_translationPostInputConectionSelection.Select(t =>t.ID).ToList());
                    }

                    

                    _cloneOfTranslationPostInputConectionSelection.ToList().ForEach(item =>
                    {
                        CRM.Data.TranslationPostInputConnection translationPostInputConnection = new TranslationPostInputConnection();
                        translationPostInputConnection.RequestID = _RequestID;
                        translationPostInputConnection.ConnectionID = item.Connection;
                        translationPostInputConnection.NewConnectionID = item.NewConnection;
                        translationPostInputConnection.CabinetInputID = item.CabinetInput;
                        translationPostInputConnection.Detach();
                        translationPostInputConnections.Add(translationPostInputConnection);
                    });

                    DB.SaveAll(translationPostInputConnections);

                    ts2.Complete();

                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره اطلاعات انجام شد.");
                }
                LoadData();
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
                        ReservePosts(translationPostInputConnections);

                        IsForwardSuccess = true;
                    }
                    ts1.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ارجاع درخواست", ex.InnerException);
            }

            return IsForwardSuccess;
        }

        public override bool Cancel()
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {


                  List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(_translationPostInputConectionSelection.Select(t => t.CabinetInput).ToList());
                cabinetInputs.ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.healthy; t.Detach(); });
                DB.UpdateAll(cabinetInputs);

                List<PostContact> postContact = Data.PostContactDB.GetPostContactByIDs(_translationPostInputConectionSelection.Select(t => t.Connection).ToList());
                postContact.ForEach(t => { t.Status = (byte)DB.PostContactStatus.CableConnection; t.Detach(); });
                DB.UpdateAll(postContact);

                List<PostContact> newPostContact = Data.PostContactDB.GetPostContactByIDs(_translationPostInputConectionSelection.Select(t => t.NewConnection).ToList());
                newPostContact.ForEach(t => { t.Status = (byte)DB.PostContactStatus.Free; t.Detach(); });
                DB.UpdateAll(newPostContact);

               

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

        private void ReservePosts(List<CRM.Data.TranslationPostInputConnection> translationPostInputConnection)
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(translationPostInputConnection.Select(t => t.CabinetInputID).ToList());
                cabinetInputs.ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.Exchange; t.Detach(); });
                DB.UpdateAll(cabinetInputs);

                List<PostContact> postContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.ConnectionID).ToList());
                postContact.ForEach(t => { t.Status = (byte)DB.PostContactStatus.FullBooking; t.Detach(); });
                DB.UpdateAll(postContact);

                List<PostContact> newPostContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.NewConnectionID).ToList());
                newPostContact.ForEach(t => { t.Status = (byte)DB.PostContactStatus.FullBooking; t.Detach(); });
                DB.UpdateAll(newPostContact);

                ts3.Complete();
            }
        }

        private void FromCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromCabinetNumberComboBox.SelectedValue != null)
            {
                FromPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)FromCabinetNumberComboBox.SelectedValue);
            }
        }

        private void ToCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToCabinetNumberComboBox.SelectedValue != null)
            {
                ToPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)ToCabinetNumberComboBox.SelectedValue);
                CabinetInputComboBoxColumn.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)ToCabinetNumberComboBox.SelectedValue);
            }
        }

        private void ToPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostComboBox.SelectedValue != null)
            {
                NewConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)ToPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);
                NewConnectionComboBoxColumn.ItemsSource = Data.PostContactDB.GetPostContactByPostID((int)ToPostComboBox.SelectedValue).Where(t => t.Status == (byte)DB.PostContactStatus.Free && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false }).ToList();

                PostGroup postGroup = Data.PostGroupDB.GetPostGroupByPostID((int)ToPostComboBox.SelectedValue);
                ToGroupPostNumberTextBox.Text = postGroup != null ? postGroup.GroupNo.ToString() : string.Empty;

                NewConnectionDataGrid.Visibility = Visibility.Visible;

                this.ResizeWindow();
            }
        }

        private void FromPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostComboBox.SelectedValue != null)
            {
                ConnectionComboBoxColumn.ItemsSource = Data.PostContactDB.GetPostContactByPostID((int)FromPostComboBox.SelectedValue).Where(t => (t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal && t.Status == (byte)DB.PostContactStatus.CableConnection) || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : " " }).ToList();

                ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)FromPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);

                PostGroup postGroup = Data.PostGroupDB.GetPostGroupByPostID((int)FromPostComboBox.SelectedValue);
                FromGroupPostNumberTextBox.Text = postGroup != null ? postGroup.GroupNo.ToString() : string.Empty;

                ConnectionDataGrid.Visibility = Visibility.Visible;

                this.ResizeWindow();

            }
        }

        private void ItemDelete(object sender, RoutedEventArgs e)
        {
            if ((ConnectionSelectionDataGrid.SelectedItem as TranslationPostInputConectionSelection) != null)
            {
                _cloneOfTranslationPostInputConectionSelection.Remove((ConnectionSelectionDataGrid.SelectedItem as TranslationPostInputConectionSelection));
            }
        }
    }
}

