using CRM.Data;
using Enterprise;
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
    /// Interaction logic for BuchtSwitchingForm.xaml
    /// </summary>
    public partial class BuchtSwitchingForm : Local.RequestFormBase
    {

        #region Properties and Fields

        private long _requestID = 0;
        int _centerID = 0;
        int buchtType = 0;
        AssignmentInfo assignmentInfo = new AssignmentInfo();
        ConnectionInfo connectionInfo;
        ConnectionInfo otherConnectionInfo;
        private Bucht _newBuchtID;
        private Bucht _OldOtherBucht;
        private PostContact _newPostContact;
        private BuchtSwitching _buchtSwitching;
        public Telephone _telephone { get; set; }

        public ObservableCollection<SpecialWirePoints> _specialWirePoints;


        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        public Request _request { get; set; }
        private int _RequestType;

        #endregion

        #region Constructors

        public BuchtSwitchingForm()
        {
            InitializeComponent();
            Initialize();
        }

        public BuchtSwitchingForm(long requestID)
            : this()
        {
            this._requestID = requestID;
        }

        public BuchtSwitchingForm(int requestType)
            : this()
        {
            this._RequestType = requestType;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CauseBuchtSwitchingComboBox.ItemsSource = Data.CauseBuchtSwitchingDB.GatCheckableCauseBuchtSwitching();
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
        }

        public void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            _request = Data.RequestDB.GetRequestByID(_requestID);

            if (_request != null)
            {
                _centerID = _request.CenterID;
            }
            _buchtSwitching = Data.BuchtSwitchingDB.GetBuchtSwitchingByID(_requestID);

            if (_request != null && _request.TelephoneNo != 0 && _request.TelephoneNo != null)
            {
                TelephonTextBox.Text = _request.TelephoneNo.ToString();
                SearchButton_Click(null, null);
            }

            if (_buchtSwitching != null)
            {
                _newBuchtID = Data.BuchtDB.GetBuchetByID(_buchtSwitching.NewBuchtID);

                _newPostContact = Data.PostContactDB.GetPostContactByID(_buchtSwitching.PostContactID ?? 0);

                if (_newBuchtID != null && _newBuchtID.ID != 0)
                {
                    connectionInfo = DB.GetConnectionInfoByBuchtID(_newBuchtID.ID);
                }

                if (_buchtSwitching.OtherBuchtID != null)
                {
                    _OldOtherBucht = Data.BuchtDB.GetBuchetByID(_buchtSwitching.OtherBuchtID);
                    buchtType = _OldOtherBucht.BuchtTypeID;
                    otherConnectionInfo = DB.GetConnectionInfoByBuchtID((long)_buchtSwitching.OtherBuchtID);
                }
            }
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            OtherBuchtMDFComboBox.ItemsSource = MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_centerID);

            if (connectionInfo != null && _centerID == connectionInfo.CenterID)
            {
                MDFComboBox.SelectedValue = connectionInfo.MDFID;
                MDFComboBox_SelectionChanged(null, null);

                ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                ConnectionColumnComboBox_SelectionChanged(null, null);

                ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                ConnectionRowComboBox_SelectionChanged(null, null);

                ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                ConnectionBuchtComboBox_SelectionChanged(null, null);
            }
            else
            {
                ConnectionColumnComboBox.ItemsSource = null;
                ConnectionRowComboBox.ItemsSource = null;
                ConnectionBuchtComboBox.ItemsSource = null;
            }



            if (otherConnectionInfo != null && _centerID == otherConnectionInfo.CenterID)
            {
                OtherBuchtMDFComboBox.SelectedValue = otherConnectionInfo.MDFID;
                OtherBuchtMDFComboBox_SelectionChanged(null, null);

                OtherBuchtConnectionColumnComboBox.SelectedValue = otherConnectionInfo.VerticalColumnID;
                OtherBuchtConnectionColumnComboBox_SelectionChanged(null, null);

                OtherBuchtConnectionRowComboBox.SelectedValue = otherConnectionInfo.VerticalRowID;
                OtherBuchtConnectionRowComboBox_SelectionChanged(null, null);

                OtherBuchtConnectionBuchtComboBox.SelectedValue = otherConnectionInfo.BuchtID;
            }
            else
            {
                OtherBuchtConnectionColumnComboBox.ItemsSource = null;
                OtherBuchtConnectionRowComboBox.ItemsSource = null;
                OtherBuchtConnectionBuchtComboBox.ItemsSource = null;
            }
        }

        #endregion

        #region ActionCombobox Methods

        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                if (assignmentInfo.MUID != null)
                {
                    throw new Exception("این تلفن بر روی پی سی ام قرار دارد");
                }

                if (_requestID == 0)
                {
                    _buchtSwitching = new BuchtSwitching();
                    // check to exist telephone on other request
                    bool inWaitingList = false;
                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(new List<long> { (long)assignmentInfo.TelePhoneNo }, out inWaitingList);
                    if (!string.IsNullOrWhiteSpace(requestName))
                    {
                        MessageBox.Show("این تلفن در روال " + requestName + " در حال پیگیری می باشد.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                    }
                }

                if (OtherBuchtConnectionBuchtComboBox.SelectedValue == null && ConnectionBuchtComboBox.SelectedValue == null)
                {
                    throw new Exception("لطفا اطلاعات را کامل وارد کنید");
                }

                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    _request = _exchangeRequestInfo.Request;

                    if (_OldOtherBucht != null)
                    {
                        _OldOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                        _OldOtherBucht.Detach();
                        DB.Save(_OldOtherBucht);
                    }

                    if (OtherBuchtConnectionBuchtComboBox.SelectedValue != null)
                    {
                        Bucht OtherBucht = Data.BuchtDB.GetBuchetByID(Convert.ToInt64(OtherBuchtConnectionBuchtComboBox.SelectedValue));
                        OtherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                        OtherBucht.Detach();
                        DB.Save(OtherBucht);
                        _buchtSwitching.OtherBuchtID = Convert.ToInt64(OtherBuchtConnectionBuchtComboBox.SelectedValue);
                    }

                    if (ConnectionBuchtComboBox.SelectedValue != null)
                    {
                        Cabinet newCabinet = Data.CabinetDB.GetCabinetByBuchtID(Convert.ToInt64(ConnectionBuchtComboBox.SelectedValue));

                        if (newCabinet != null && newCabinet.ID != assignmentInfo.CabinetID && ToPostContactComboBox.SelectedValue == null)
                        {
                            //milad doran
                            //throw new Exception("بوخت جدید انتخاب شده متصل به کافو دیگر است .لطفا اتصالی جدید را انتخاب کنید");

                            //TODO:rad 13950117
                            MessageBox.Show("بوخت جدید انتخاب شده متصل به کافو دیگر است .لطفا اتصالی جدید را انتخاب کنید", "", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.IsSaveSuccess = false;
                            return this.IsSaveSuccess;
                        }

                        if (_newBuchtID != null)
                        {
                            _newBuchtID.Status = (byte)DB.BuchtStatus.Free;
                            _newBuchtID.Detach();
                            DB.Save(_newBuchtID);
                        }

                        Bucht bucht = Data.BuchtDB.GetBuchetByID(Convert.ToInt64(ConnectionBuchtComboBox.SelectedValue));
                        bucht.Status = (byte)DB.BuchtStatus.Reserve;
                        bucht.Detach();
                        DB.Save(bucht);

                        if (_newPostContact != null)
                        {
                            _newPostContact.Status = (byte)DB.PostContactStatus.Free;
                            _newPostContact.Detach();
                            DB.Save(_newPostContact);
                        }

                        if (ToPostContactComboBox.SelectedValue != null)
                        {
                            PostContact postContact = Data.PostContactDB.GetPostContactByID(Convert.ToInt64(ToPostContactComboBox.SelectedValue));
                            postContact.Status = (byte)DB.PostContactStatus.FullBooking;
                            postContact.Detach();
                            DB.Save(postContact);
                        }

                        _buchtSwitching.NewBuchtID = Convert.ToInt64(ConnectionBuchtComboBox.SelectedValue);
                    }

                    if (_requestID == 0)
                    {
                        _request.ID = DB.GenerateRequestID();
                        _request.RequestPaymentTypeID = 0;
                        _request.IsViewed = false;
                        _request.TelephoneNo = (long)assignmentInfo.TelePhoneNo;
                        _request.InsertDate = DB.GetServerDate();
                        _request.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status

                        _buchtSwitching.OldBuchtID = (long)assignmentInfo.BuchtID;

                        _buchtSwitching.InsertDate = DB.GetServerDate();

                        if (CauseBuchtSwitchingComboBox.SelectedValue == null)
                        {
                            _buchtSwitching.CauseBuchtSwitchingID = null;
                        }
                        else
                        {
                            _buchtSwitching.CauseBuchtSwitchingID = Convert.ToInt32(CauseBuchtSwitchingComboBox.SelectedValue); ;
                        }

                        if (ToPostContactComboBox.SelectedValue != null)
                        {
                            _buchtSwitching.PostContactID = Convert.ToInt64(ToPostContactComboBox.SelectedValue);
                        }
                        else
                        {
                            _buchtSwitching.PostContactID = null;
                        }

                        _request.Detach();
                        DB.Save(_request, true);

                        _requestID = _request.ID;
                        _buchtSwitching.ID = _request.ID;
                        _buchtSwitching.Detach();
                        DB.Save(_buchtSwitching, true);
                    }
                    else
                    {
                        if (CauseBuchtSwitchingComboBox.SelectedValue == null)
                        {
                            _buchtSwitching.CauseBuchtSwitchingID = null;
                        }
                        else
                        {
                            _buchtSwitching.CauseBuchtSwitchingID = Convert.ToInt32(CauseBuchtSwitchingComboBox.SelectedValue); ;
                        }

                        if (ToPostContactComboBox.SelectedValue != null)
                        {
                            _buchtSwitching.PostContactID = Convert.ToInt64(ToPostContactComboBox.SelectedValue);
                        }
                        else
                        {
                            _buchtSwitching.PostContactID = null;
                        }

                        _request.Detach();
                        DB.Save(_request, false);


                        _buchtSwitching.Detach();
                        DB.Save(_buchtSwitching, false);
                    }

                    ts2.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در زخیره اطلاعات", ex);
                Logger.Write(ex, "خطا در فرم تعویض بوخت");
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
                    this.RequestID = _request.ID;
                    if (IsSaveSuccess)
                    {
                        IsForwardSuccess = true;
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در زخیره اطلاعات", ex);
            }

            return IsForwardSuccess;
        }

        public override bool Cancel()
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {


                if (_OldOtherBucht != null)
                {
                    _OldOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                    _OldOtherBucht.Detach();
                    DB.Save(_OldOtherBucht);
                }


                if (_newBuchtID != null)
                {
                    _newBuchtID.Status = (byte)DB.BuchtStatus.Free;
                    _newBuchtID.Detach();
                    DB.Save(_newBuchtID);
                }


                if (_newPostContact != null)
                {
                    _newPostContact.Status = (byte)DB.PostContactStatus.Free;
                    _newPostContact.Detach();
                    DB.Save(_newPostContact);
                }




                Data.CancelationRequestList cancelationRequest = new CancelationRequestList();
                cancelationRequest.ID = _request.ID;
                cancelationRequest.EntryDate = DB.GetServerDate();
                cancelationRequest.UserID = Folder.User.Current.ID;
                cancelationRequest.Detach();
                DB.Save(cancelationRequest, true);

                _request.IsCancelation = true;
                _request.Detach();
                DB.Save(_request);

                IsCancelSuccess = true;

                ts3.Complete();
            }

            return true;
        }

        #endregion

        #region Event Handlers

        private void PointsInfoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpecialWirePoints specialWirePoints = PointsInfoDataGrid.SelectedItem as SpecialWirePoints;
            if (specialWirePoints != null && specialWirePoints.SpecialWireTypeID == (int)DB.SpecialWireType.General)
            {
                assignmentInfo = DB.GetTechInfoByBuchtID((long)specialWirePoints.BuchtID);

                if (assignmentInfo != null)
                {

                    if (assignmentInfo.MUID == null)
                    {
                        if (_centerID != assignmentInfo.CenterID)
                            MessageBox.Show("تلفن وارد شده در مرکز جاری نمی باشد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            TechInformationGroupBox.DataContext = assignmentInfo;
                    }
                    else
                    {
                        PcmWarningAnouncementUserControl.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Folder.MessageBox.ShowError("اطلاعات فنی تلفن یافت نشد");
                }

                OtherBuchtGroupBox.Visibility = Visibility.Visible;
                if (specialWirePoints.OtherBuchtID != null)
                {
                    Bucht OtherBucht = Data.BuchtDB.GetBuchetByID(specialWirePoints.OtherBuchtID);

                    OtherBuchtBuchtTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.BuchtType), OtherBucht.BuchtTypeID);
                    buchtType = OtherBucht.BuchtTypeID;
                    OtherBuchtBuchtTypeTextBox.Visibility = Visibility.Visible;
                    OtherBuchtTypeComboBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    OtherBuchtBuchtTypeTextBox.Visibility = Visibility.Collapsed;
                    OtherBuchtTypeComboBox.Visibility = Visibility.Visible;
                    OtherBuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetSubBuchtTypeCheckable((int)DB.BuchtType.PrivateWire);
                    OtherBuchtTypeComboBox.SelectedValue = buchtType;
                }
            }
            else if (specialWirePoints != null && specialWirePoints.SpecialWireTypeID != (int)DB.SpecialWireType.General)
            {
                MessageBox.Show("تعویض بوخت فقط برای نقاط اصلی سیم خصوصی امکان پذیر است", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuchtTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OtherBuchtBuchtTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OtherBuchtTypeComboBox.SelectedValue != null)
            {
                buchtType = (int)OtherBuchtTypeComboBox.SelectedValue;
            }
        }

        private void OtherBuchtMDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OtherBuchtMDFComboBox.SelectedValue != null)
                OtherBuchtConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)OtherBuchtMDFComboBox.SelectedValue);
        }

        private void OtherBuchtConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OtherBuchtConnectionColumnComboBox.SelectedValue != null)
                OtherBuchtConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)OtherBuchtConnectionColumnComboBox.SelectedValue);
        }

        private void OtherBuchtConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OtherBuchtConnectionRowComboBox.SelectedValue != null)
            {

                if (_OldOtherBucht != null)

                    OtherBuchtConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)OtherBuchtConnectionRowComboBox.SelectedValue, false, buchtType).Union(new List<CheckableItem> { new CheckableItem { LongID = _OldOtherBucht.ID, Name = _OldOtherBucht.BuchtNo.ToString(), IsChecked = false } });

                else

                    OtherBuchtConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)OtherBuchtConnectionRowComboBox.SelectedValue, false, buchtType);
            }
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO:rad 13950116
            PcmWarningAnouncementUserControl.Visibility = Visibility.Collapsed;
            //TODO:end

            long telephonNo = 0;
            if (!long.TryParse(TelephonTextBox.Text.Trim(), out telephonNo))
                throw new Exception("تلفن وارد شده مقدار عددی نمی باشد");

            _telephone = TelephoneDB.GetTelephoneByTelePhoneNo(telephonNo);

            if (_telephone != null && _telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire)
            {
                City city = Data.CityDB.GetCityByCenterID(_telephone.CenterID);
                CentersComboBoxColumn.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                _specialWirePoints = new ObservableCollection<SpecialWirePoints>(Data.SpecialWirePointsDB.GetSpecialWirePointsByTelephone(telephonNo));
                PointsInfoDataGrid.ItemsSource = _specialWirePoints;

                if (_buchtSwitching != null && _buchtSwitching.OldBuchtID != null)
                    PointsInfoDataGrid.SelectedValue = _buchtSwitching.OldBuchtID;
                PointsInfoDataGrid.Visibility = Visibility.Visible;
            }
            else if (_telephone != null)
            {
                PointsInfoDataGrid.Visibility = Visibility.Collapsed;
                assignmentInfo = DB.GetAllInformationByTelephoneNo(telephonNo);

                if (assignmentInfo != null)
                {
                    if (assignmentInfo.MUID == null)
                    {
                        if (_centerID != assignmentInfo.CenterID)
                            MessageBox.Show("تلفن وارد شده در مرکز جاری نمی باشد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            TechInformationGroupBox.DataContext = assignmentInfo;
                    }
                    else
                    {
                        PcmWarningAnouncementUserControl.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("اطلاعات فنی تلفن یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
                ConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue);
        }

        private void ConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionColumnComboBox.SelectedValue != null)
                ConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)ConnectionColumnComboBox.SelectedValue);
        }

        private void ConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ConnectionRowComboBox.SelectedValue != null)
            {
                if (_newBuchtID != null)
                {
                    List<CheckableItem> checkableItems = new List<CheckableItem>();
                    checkableItems = DB.GetBuchtsConnectedToCable((int)ConnectionRowComboBox.SelectedValue, _newBuchtID.BuchtTypeID);

                    ConnectionBuchtComboBox.ItemsSource = checkableItems.Union(new List<CheckableItem> 
                                                                                   { 
                                                                                       new CheckableItem 
                                                                                       { 
                                                                                           LongID = _newBuchtID.ID, 
                                                                                           Name = _newBuchtID.BuchtNo.ToString(), 
                                                                                           IsChecked = false 
                                                                                       } 
                                                                                   }
                                                                               );
                }
                else
                {
                    ConnectionBuchtComboBox.ItemsSource = DB.GetBuchtsConnectedToCable((int)ConnectionRowComboBox.SelectedValue, assignmentInfo.BuchtType);
                }
            }
        }

        private void ConnectionBuchtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionBuchtComboBox.SelectedValue != null)
            {
                ToPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByBuchtID((long)ConnectionBuchtComboBox.SelectedValue);

                if (_newPostContact != null)
                {
                    ToPostComboBox.SelectedValue = _newPostContact.PostID;
                    ToPostComboBox_SelectionChanged(null, null);
                }
            }

        }

        private void ToPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostComboBox.SelectedValue != null)
            {
                HideMessage();
                if (_newPostContact != null)
                {
                    List<CheckableItem> checkableItems = new List<CheckableItem>();
                    checkableItems = Data.PostContactDB.GetFreePostContactByPostIDWithOutPCM((int)ToPostComboBox.SelectedValue);

                    ToPostContactComboBox.ItemsSource = checkableItems.Union(new List<CheckableItem> 
                                                                                { 
                                                                                    new CheckableItem 
                                                                                    { 
                                                                                        LongID = _newPostContact.ID, 
                                                                                        Name = _newPostContact.ConnectionNo.ToString(), 
                                                                                        IsChecked = false 
                                                                                    } 
                                                                                }
                                                                            );
                    ToPostContactComboBox.SelectedValue = _newPostContact.ID;
                }
                else
                {
                    ToPostContactComboBox.ItemsSource = Data.PostContactDB.GetFreePostContactByPostIDWithOutPCM((int)ToPostComboBox.SelectedValue);
                }
            }
        }

        #endregion

    }
}
