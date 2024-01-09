using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using CRM.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangeCentralCableMDF.xaml
    /// </summary> 
    public partial class TranslationCentralCableMDFForm : Local.RequestFormBase
    {
        # region Initialize && Fildes
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        CRM.Data.ExchangeCentralCableMDF _exchangeCentralCableMDF { get; set; }
        Cabinet _pastCabinet { get; set; }
        Cable _pastCable { get; set; }
        Request _reqeust { get; set; }

        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<CablePair> _newCablePairs { get; set; }
        List<CablePair> _oldCablePairs { get; set; }

        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        List<Bucht> _oldPassBuchtList { get; set; }
        List<Bucht> _newPassBuchtList { get; set; }

        private long _requestID;
        ConnectionInfo oldConnectionInfo;
        ConnectionInfo newConnectionInfo;

        List<AssignmentInfo> asignmentInfos = new List<AssignmentInfo>();

        List<ExchangeCentralCableMDFConncetion> passExchangeConncetion = new List<ExchangeCentralCableMDFConncetion>();

        private int _RequestType;
        int _centerID = 0;
        private int CityID = 0;
        public TranslationCentralCableMDFForm()
        {
            InitializeComponent();
            Initialize();

        }
        public TranslationCentralCableMDFForm(int requestType)
            : this()
        {
            this._RequestType = requestType;
        }
        public TranslationCentralCableMDFForm(long id)
            : this()
        {

            _requestID = id;
        }


        public void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
           // BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetSubBuchtTypeCheckableByParents(new List<int> { (int)DB.BuchtType.CustomerSide, (int)DB.BuchtType.OpticalBucht});
        }
        #endregion

        #region Load
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Load();
        }
        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {

            _centerID = centerID;
            OldMDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_centerID);
            NewMDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_centerID);
            //  OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_centerID);

            if (oldConnectionInfo != null && _centerID == oldConnectionInfo.CenterID)
            {
                OldMDFComboBox.SelectedValue = oldConnectionInfo.MDFID;
                OldMDFComboBox_SelectionChanged(null, null);

                OldConnectionColumnComboBox.SelectedValue = oldConnectionInfo.VerticalColumnID;
                OldConnectionColumnComboBox_SelectionChanged(null, null);

                OldConnectionRowComboBox.SelectedValue = oldConnectionInfo.VerticalRowID;
                OldConnectionRowComboBox_SelectionChanged(null, null);

            }
            else
            {
                OldConnectionColumnComboBox.ItemsSource = null;
                OldConnectionRowComboBox.ItemsSource = null;
            }

            if (newConnectionInfo != null && _centerID == newConnectionInfo.CenterID)
            {
                NewMDFComboBox.SelectedValue = newConnectionInfo.MDFID;
                NewMDFComboBox_SelectionChanged(null, null);

                NewConnectionColumnComboBox.SelectedValue = newConnectionInfo.VerticalColumnID;
                NewConnectionColumnComboBox_SelectionChanged(null, null);

                NewConnectionRowComboBox.SelectedValue = newConnectionInfo.VerticalRowID;
                NewConnectionRowComboBox_SelectionChanged(null, null);

            }
            else
            {
                NewConnectionColumnComboBox.ItemsSource = null;
                NewConnectionRowComboBox.ItemsSource = null;
            }
        }
        private void Load()
        {

            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;

            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            if (_requestID == 0)
            {
                _exchangeCentralCableMDF = new ExchangeCentralCableMDF();
                _oldPassBuchtList = new List<Bucht>();
                _newPassBuchtList = new List<Bucht>();
            }
            else
            {
                _reqeust = Data.RequestDB.GetRequestByID(_requestID);
                _exchangeCentralCableMDF = Data.ExchangeCenralCableMDFDB.GetExchangeCentralCableByID(_requestID);

                oldConnectionInfo = DB.GetConnectionInfoByBuchtID(_exchangeCentralCableMDF.FromOldBuchtID);
                newConnectionInfo = DB.GetConnectionInfoByBuchtID(_exchangeCentralCableMDF.FromNewBuchtID);

                ExchangeRequestInfoUserControl_DoCenterChange(_reqeust.CenterID);


                _oldPassBuchtList = Data.BuchtDB.GetAllBuchtFromBuchtIDToBuchtID(_exchangeCentralCableMDF.FromOldBuchtID, _exchangeCentralCableMDF.ToOldBuchtID);
                _newPassBuchtList = Data.BuchtDB.GetAllBuchtFromBuchtIDToBuchtID(_exchangeCentralCableMDF.FromNewBuchtID, _exchangeCentralCableMDF.ToNewBuchtID);

                passExchangeConncetion = Data.ExchangeCenralCableMDFDB.GetExchangeCentralCableConnectionByID(_requestID);

            }
            this.DataContext = _exchangeCentralCableMDF;
        }
        #endregion

        # region save
        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    _reqeust = _exchangeRequestInfo.Request;
                    _exchangeCentralCableMDF = this.DataContext as ExchangeCentralCableMDF;

                    // Verify data
                    VerifyData(_exchangeCentralCableMDF);

                    if (_requestID == 0)
                    {
                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.RequestPaymentTypeID = 0;
                        _reqeust.IsViewed = false;
                        _reqeust.InsertDate = DB.GetServerDate();
                        _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);


                        _exchangeCentralCableMDF.ID = _reqeust.ID;
                        _exchangeCentralCableMDF.InsertDate = DB.GetServerDate();
                        _exchangeCentralCableMDF.Detach();
                        DB.Save(_exchangeCentralCableMDF, true);

                    }
                    else
                    {
                        _reqeust.Detach();
                        DB.Save(_reqeust, false);

                        _exchangeCentralCableMDF.Detach();
                        DB.Save(_exchangeCentralCableMDF, false);
                    }



                    _oldPassBuchtList.ForEach(t =>
                    {
                        t.Status = passExchangeConncetion.Where(t3 => t3.OldBuchtID == t.ID).SingleOrDefault().OldStatusID;
                        t.Detach();
                    });

                    _newPassBuchtList.ForEach(t =>
                    {
                        t.Status = passExchangeConncetion.Where(t3 => t3.NewBuchtID == t.ID).SingleOrDefault().NewStatusID;
                        t.Detach();
                    });

                    Data.ExchangeCenralCableMDFDB.DeleteExchangeCenralCableMDFDBConncetionByRequestID(_exchangeCentralCableMDF.ID);


                    DB.UpdateAll(_oldPassBuchtList);

                    DB.UpdateAll(_newPassBuchtList);

                    List<CRM.Data.ExchangeCentralCableMDFConncetion> translationConncetions = new List<ExchangeCentralCableMDFConncetion>();
                    int i = 0;
                    _oldBuchtList.ForEach(t =>
                    {
                        CRM.Data.ExchangeCentralCableMDFConncetion translationConncetion = new ExchangeCentralCableMDFConncetion();
                        AssignmentInfo asignmentInfo = asignmentInfos.Find(t2 => t2.BuchtID == t.ID);
                        if (asignmentInfo != null)
                        {
                            translationConncetion.TelephoneNo = asignmentInfo.TelePhoneNo;
                        }
                        translationConncetion.RequestID = _reqeust.ID;
                        translationConncetion.OldBuchtID = t.ID;
                        translationConncetion.OldStatusID = t.Status;
                        translationConncetion.NewBuchtID = _newBuchtList[i].ID;
                        translationConncetion.NewStatusID = _newBuchtList[i].Status;
                        translationConncetions.Add(translationConncetion);

                        i++;
                    });


                    _oldBuchtList.ForEach(t =>
                    {
                        t.Status = (int)DB.BuchtStatus.Reserve;
                        t.Detach();
                    });

                    _newBuchtList.ForEach(t =>
                    {
                        t.Status = (int)DB.BuchtStatus.Reserve;
                        t.Detach();
                    });


                    DB.SaveAll(translationConncetions);

                    DB.UpdateAll(_oldBuchtList);

                    DB.UpdateAll(_newBuchtList);

                    IsSaveSuccess = true;
                    ts.Complete();
                }
                _requestID = _reqeust.ID;
                Load();
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex.InnerException);
            }
            return IsSaveSuccess;

        }

        private void VerifyData(ExchangeCentralCableMDF exchangeCentralCableMDF)
        {
            // number new cabinetInput
            //_oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(exchangeCentralCableMDF.FromCabinetInputID, exchangeCentralCableMDF.ToCabinetInputID);
           // _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList()).Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ToList();


            _oldBuchtList = Data.BuchtDB.GetAllBuchtFromBuchtIDToBuchtID(exchangeCentralCableMDF.FromOldBuchtID, exchangeCentralCableMDF.ToOldBuchtID);

            if (!(_oldBuchtList.Where(t => t.BuchtTypeID != (byte)DB.BuchtType.InLine && t.BuchtTypeID != (byte)DB.BuchtType.OutLine).GroupBy(t => t.BuchtTypeID).Any(t => t.Count() > 1)))
                throw new Exception("بوخت های قدیم هم نوع نمی باشند");
            List<Bucht> ChangedOldBucht = _oldBuchtList.Where(t => !_oldPassBuchtList.Select(t3=>t3.ID).Contains(t.ID)).ToList();
            if (ChangedOldBucht.Any(t => t.Status != (int)DB.BuchtStatus.Free &&
                                      t.Status != (int)DB.BuchtStatus.AllocatedToInlinePCM &&
                                      t.Status != (int)DB.BuchtStatus.Connection &&
                                      t.Status != (int)DB.BuchtStatus.Destroy &&
                                      t.BuchtTypeID != (int)DB.BuchtType.InLine &&
                                      t.BuchtTypeID != (int)DB.BuchtType.OutLine))
            {
                throw new Exception("در میان بوخت ها قبل برگردان بوخت رزرو وجود دارد");
            }

            asignmentInfos = DB.GetAllInformationPostContactIDs(_oldBuchtList.Where(t=>t.ConnectionID != null).Select(t => (long)t.ConnectionID).ToList());
            bool inWaitingList = false;
            string requestName = Data.RequestDB.GetOpenRequestNameTelephone(asignmentInfos.Select(t => (long)t.TelePhoneNo).ToList(), out inWaitingList);
            if (!string.IsNullOrEmpty(requestName))
            {
                Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
            }

            _newBuchtList = Data.BuchtDB.GetAllBuchtFromBuchtIDToBuchtID(exchangeCentralCableMDF.FromNewBuchtID, exchangeCentralCableMDF.ToNewBuchtID);
            List<Bucht> ChangedNewBucht = _newBuchtList.Where(t => !_newPassBuchtList.Select(t3 => t3.ID).Contains(t.ID)).ToList();
            if (ChangedNewBucht.Any(t => t.Status != (int)DB.BuchtStatus.Free))
            {
                throw new Exception("همه بوخت های جدید باید آزاد باشند");
            }

            if (ChangedNewBucht.Any(t => t.CabinetInputID != null || t.CablePairID != null))
            {
                MessageBoxResult result = Folder.MessageBox.Show("بوخت های جدید به کابل متصل می باشند بعد از برگردان این بوخت های به کابل جدید متصل خواهد شد", "", MessageBoxImage.Question, MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.No:
                        throw new Exception("بوخت های جدید به کابل متصل می باشند بعد از برگردان این بوخت های به کابل جدید متصل خواهد شد");
                        break;
                    case MessageBoxResult.Yes:
                        break;
                    default:
                        break;
                }
            }

            if (ChangedNewBucht.Count() > 0 && !(ChangedNewBucht.GroupBy(t => t.BuchtTypeID).Any(t => t.Count() > 1)))
                throw new Exception("بوخت های جدید هم نوع نمی باشند");

            if (_newBuchtList.GroupBy(t => t.BuchtTypeID).Select(t => t.Key) == _oldBuchtList.Where(t => t.BuchtTypeID != (byte)DB.BuchtType.InLine && t.BuchtTypeID != (byte)DB.BuchtType.OutLine).GroupBy(t => t.BuchtTypeID).Select(t => t.Key))
                throw new Exception("نوع بوخت جدید با نوع بوخت قدیم منطبق نمی باشند");

            if (_oldBuchtList.Count() != _newBuchtList.Count()) { throw new Exception("تعداد بوخت های متصل انتخاب شده برابر نمی باشد."); }
        }

        #endregion

        #region action
        public override bool Forward()
        {
            try
            {
                using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Save();
                    this.RequestID = _reqeust.ID;
                    if (IsSaveSuccess)
                    {
                          ReserveData();
                        IsForwardSuccess = true;
                    }

                    ts1.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا رد ارجاع اطلاعات" , ex);
            }
            return IsForwardSuccess;
        }

        private void ReserveData()
        {
            // Reservations the cabinet
            //Cabinet currentCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCentralCableMDF.CabinetID);
            //currentCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
            //currentCabinet.Detach();
            //DB.Save(currentCabinet);


            _newBuchtList.ForEach(t => { t.Status = (byte)DB.BuchtStatus.ExchangeCentralCableMDF; t.Detach(); });
            DB.UpdateAll(_newBuchtList);

        }
        #endregion action

        #region Events

        #endregion Events




        #region bucht Event

        private void OldMDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldMDFComboBox.SelectedValue != null)
                OldConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)OldMDFComboBox.SelectedValue);
        }

        private void NewMDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewMDFComboBox.SelectedValue != null)
                NewConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)NewMDFComboBox.SelectedValue);
        }

        private void NewConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewConnectionColumnComboBox.SelectedValue != null)
                NewConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)NewConnectionColumnComboBox.SelectedValue);
        }

        private void OldConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldConnectionColumnComboBox.SelectedValue != null)
                OldConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)OldConnectionColumnComboBox.SelectedValue);
        }

        private void OldConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldConnectionRowComboBox.SelectedValue != null)
            {
                FromOldConnectionBuchtComboBox.ItemsSource = ToOldConnectionBuchtComboBox.ItemsSource = DB.GetBuchtsNotConnectedToCable((int)OldConnectionRowComboBox.SelectedValue);
            }
        }

        private void NewConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewConnectionRowComboBox.SelectedValue != null)
            {
                FromNewConnectionBuchtComboBox.ItemsSource = ToNewConnectionBuchtComboBox.ItemsSource = DB.GetBuchtsNotConnectedToCable((int)NewConnectionRowComboBox.SelectedValue);
            }
        }

        #endregion bucht Event

        public override bool Cancel()
        {
            try
            {

                using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
                {


                    _oldPassBuchtList.ForEach(t =>
                    {
                        t.Status = passExchangeConncetion.Where(t3 => t3.OldBuchtID == t.ID).SingleOrDefault().OldStatusID;
                        t.Detach();
                    });

                    _newPassBuchtList.ForEach(t =>
                    {
                        t.Status = passExchangeConncetion.Where(t3 => t3.NewBuchtID == t.ID).SingleOrDefault().NewStatusID;
                        t.Detach();
                    });

                    DB.UpdateAll(_oldPassBuchtList);

                    DB.UpdateAll(_newPassBuchtList);


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

    }
}

