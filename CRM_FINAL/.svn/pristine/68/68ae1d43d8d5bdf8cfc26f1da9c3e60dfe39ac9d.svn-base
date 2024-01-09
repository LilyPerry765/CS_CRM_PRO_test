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
using CRM.Data;
using System.Collections.ObjectModel;
using Enterprise;
using CRM.Application.UserControls;
using CRM.Application.Views;
using System.Transactions;
using System.Threading;
using System.Collections;

namespace CRM.Application.Views
{

    public partial class ChooseNoForm : Local.RequestFormBase
    {


        #region fields && Properties
        private long _id = 0;
        public bool Mode = false;
        bool _IsRound = false;
        GSMSimCard GSMSimCardItem;

        public InstallRequest _installRequest { get; set; }
        public Request _request = new Request();
        private string _UserName;

        public static InvestigatePossibility investigate { get; set; }
        public SelectTelephone _SelectTelephone = new SelectTelephone();
        public static List<ConnectionSource> connectionList { get; set; }

        public static Switch switchInfo { get; set; }
        public static List<SwitchPrecode> switchPrecodeList { get; set; }
        public static SwitchPort switchPort { get; set; }
        public static List<Telephone> telList { get; set; }
        public static List<SwitchPort> portList { get; set; }
        SpecialCondition specialCondition { get; set; }
        public static Bucht bucht = new Bucht();
        SwitchPort switchPortReserv;

        Cabinet cabinet = new Cabinet();



        public UserControls.UserInfoSummary _userInfoSummary { get; set; }
        public UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        public UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        public UserControls.InstallInfoSummary _installInfoSummary { get; set; }
        public UserControls.InvestigateInfoSummary _investigateInfoSummary { get; set; }
        public UserControls.ChangeNoDetailUserControl _ChangeNoDetailUserControl { get; set; }

        private Data.ChangeNo _ChangeNo { get; set; }
        CRM.Data.ChangeLocation changeLocation;

        SwitchPrecode oldSwitchItem = new SwitchPrecode();
        Telephone oldTelItem = new Telephone();
        StepStatusInfo oldStatusItem = new StepStatusInfo();
        Bucht reserveBucht;
        Bucht OldBucht;
        Data.RequestType requestType { get; set; }
        AssignmentInfo assigmentinfo = new AssignmentInfo();
        #endregion

        #region Constructor
        public ChooseNoForm()
        {
            InitializeComponent();
            investigate = new InvestigatePossibility();

        }

        public ChooseNoForm(long id)
            : this()
        {
            _id = id;
            _request = Data.RequestDB.GetRequestByID(id);

            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;

            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    ChooseStatus.DataContext = investigate;
                    ChooseTelInfo.DataContext = investigate;

                    _installInfoSummary = new InstallInfoSummary(_request.ID);
                    InstallInfoUC.Content = _installInfoSummary;
                    InstallInfoUC.DataContext = _installInfoSummary;


                    _investigateInfoSummary = new InvestigateInfoSummary(_request.ID);
                    InvestigateInfoUC.Content = _investigateInfoSummary;
                    InvestigateInfoUC.DataContext = _investigateInfoSummary;
                    break;

                case (int)DB.RequestType.ChangeNo:
                    _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID(_request.ID);
                    break;
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_request.ID);
                    reserveBucht = new Bucht();
                    switchPortReserv = new SwitchPort();
                    break;
            }

            Initialize();

        }
        #endregion

        #region Methods
        private void Initialize()
        {
            _UserName = Folder.User.Current.Username;
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            switchDetail.DataContext = switchPrecodeList;
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
            this.ResizeWindow();
        }

        public void Load()
        {
            investigate = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).OrderByDescending(t => t.ConnectionReserveDate).FirstOrDefault();

            _SelectTelephone = SelectTelephoneDB.GetSelectTelephone(_request.ID);
            if (_SelectTelephone == null)
            {
                _SelectTelephone = new SelectTelephone();
                _SelectTelephone.ID = _request.ID;
            }

            specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);


            InvestigateStatusComboBox.DataContext = InvestigateStatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            InvestigateStatusComboBox.SelectedValue = _request.StatusID;


            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    DayriLoad();
                    break;

                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    ChangeLocationload();
                    break;

                case (int)DB.RequestType.ChangeNo:
                    ChangeNoLoad();
                    break;
            }
        }

        private bool CheckBeRound()
        {
            long? telephone = null;
            if (!Data.RequestDocumnetDB.CheckTelephoneBeRound(_request, out telephone))
            {
                return false;
            }
            else
            {
                _IsRound = true;
                TelRoundInfo roundTel = RoundListDB.GetRoundTelInfoByRequestID(_request.ID);
                SwitchPrecode switchPrecodeItem = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)roundTel.switchPreCodeID);
                Switch switchInfo = Data.SwitchDB.GetSwitchByID(switchPrecodeItem.SwitchID);
                SwitchInfo.DataContext = new { SwitchPrecode = switchPrecodeItem, Switch = switchInfo };
                switchPrecodeList = Data.SwitchPrecodeDB.GetSwitchPrecodeBySwitchID((int)roundTel.switchID);

                PreCodeTypeComboBox.SelectedValue = switchPrecodeItem.PreCodeType;
                PreCodeTypeComboBox_SelectionChanged(null, null);

                this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
                SwitchPreCodeComboBox_SelectionChanged(null, null);

                if (telList == null) telList = new List<Telephone>();

                telList = telList.Union(new List<Telephone> { Data.TelephoneDB.GetTelephoneByTelephoneNo((long)roundTel.TelephoneNo) }).ToList();
                this.TelephoneNoComboBox.ItemsSource = telList;
                this.TelephoneNoComboBox.SelectedValue = telList.Where(t => t.TelephoneNo == roundTel.TelephoneNo).SingleOrDefault().TelephoneNo;
                TelephoneNoComboBox_SelectionChanged(null, null);


                this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
                PreCodeTypeComboBox.SelectedValue = roundTel.switchPreCodeType;

                Mode = true;

                oldSwitchItem = SwitchPreCodeComboBox.SelectedItem as SwitchPrecode;
                oldTelItem = TelephoneNoComboBox.SelectedItem as Telephone;

                TelephoneNoComboBox.IsEnabled = false;
                PreCodeTypeComboBox.IsEnabled = false;
                SwitchPreCodeComboBox.IsEnabled = false;

                TelItemsGroupBox.Visibility = Visibility.Collapsed;

                return true;
            }
        }
        #endregion

        #region SaveMethode

        void DayriSave()
        {
            Telephone telItem = Data.TelephoneDB.GetTelephoneByTelephoneNo((TelephoneNoComboBox.SelectedItem as Telephone).TelephoneNo);
            StepStatusInfo statusItem = InvestigateStatusComboBox.SelectedItem as StepStatusInfo;

            if (!(telItem.Status == (byte)DB.TelephoneStatus.Free || telItem.Status == (byte)DB.TelephoneStatus.Discharge))
            {
                if (!(_SelectTelephone != null && _SelectTelephone.TelephoneNo == telItem.TelephoneNo))
                    throw new Exception(string.Format("تلفن {0} آزاد نمی باشد", telItem.TelephoneNo));
            }

            using (TransactionScope transaction = new TransactionScope())
            {

                if (Mode == false)
                {
                    // reserve telephone
                    telItem.Status = (byte)DB.TelephoneStatus.Reserv;
                    telItem.InstallAddressID = _installInfoSummary._InstallAdrressID;
                    telItem.ClassTelephone = _installInfoSummary.InstallRequest.ClassTelephone;
                    telItem.CorrespondenceAddressID = _installInfoSummary._CorrespondenceAddressID;
                    telItem.CustomerID = _request.CustomerID;


                    _SelectTelephone.ReserveDate = DB.GetServerDate();
                    _SelectTelephone.TelephoneNo = telItem.TelephoneNo;
                    _SelectTelephone.SwitchPortID = telItem.SwitchPortID;
                    _SelectTelephone.Detach();
                    DB.Save(_SelectTelephone, true);

                    Bucht bucht = null;
                    // save switchport in bucht
                    if (_installRequest.IsGSM == false)
                    {
                        bucht = Data.BuchtDB.GetBuchtByID((long)investigate.BuchtID);
                        bucht.SwitchPortID = telItem.SwitchPortID;
                    }


                    _request.StatusID = statusItem.StepStatusID;
                    _request.TelephoneNo = telItem.TelephoneNo;
                    InvestigatePossibilityDB.SaveTelAndPort(_request, investigate, telItem, bucht);
                }
                else if (Mode == true)
                {


                    // release telephone information
                    oldTelItem.Status = DB.GetTelephoneLastStatus(oldTelItem.TelephoneNo);
                    oldTelItem.InstallAddressID = null;
                    oldTelItem.CorrespondenceAddressID = null;
                    oldTelItem.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;
                    oldTelItem.CustomerID = null;

                    // release bucht of telephone
                    if (_installRequest.IsGSM == false)
                    {

                        bucht.SwitchPortID = null;
                    }



                    InvestigatePossibilityDB.SaveTelAndPort(_request, investigate, oldTelItem, bucht);

                    SwitchPrecode switchItem = SwitchPreCodeComboBox.SelectedItem as SwitchPrecode;

                    // reserve telephone
                    telItem.Status = (byte)DB.TelephoneStatus.Reserv;
                    telItem.InstallAddressID = _installInfoSummary._InstallAdrressID;
                    telItem.CorrespondenceAddressID = _installInfoSummary._CorrespondenceAddressID;
                    telItem.ClassTelephone = _installInfoSummary.InstallRequest.ClassTelephone;
                    telItem.CustomerID = _request.CustomerID;

                    if (!_IsRound)
                    {
                        _SelectTelephone.ReserveDate = DB.GetServerDate();
                        _SelectTelephone.TelephoneNo = telItem.TelephoneNo;
                        _SelectTelephone.SwitchPortID = telItem.SwitchPortID;
                        _SelectTelephone.Detach();
                        DB.Save(_SelectTelephone);
                    }

                    // Appointment switch port to bucht
                    if (_installRequest.IsGSM == false)
                    {
                        bucht = Data.BuchtDB.GetBuchetByID(investigate.BuchtID);
                        bucht.SwitchPortID = telItem.SwitchPortID;
                    }
                    _request.StatusID = statusItem.StepStatusID;

                    _request.TelephoneNo = telItem.TelephoneNo;

                    InvestigatePossibilityDB.SaveTelAndPort(_request, investigate, telItem, bucht);
                }
                transaction.Complete();
            }
        }

        private void CheckForChangePort(Telephone telItem, SwitchPort portItem)
        {
            if (telItem.SwitchPortID != null && telItem.SwitchPortID != portItem.ID)
            {
                SwitchPort teleSwitchPort = Data.SwitchPortDB.GetSwitchPortByID((int)telItem.SwitchPortID);
                teleSwitchPort.Status = (byte)DB.SwitchPortStatus.Free;
                teleSwitchPort.Detach();
                DB.Save(teleSwitchPort, false);

                portItem.Status = (byte)DB.SwitchPortStatus.Install;
                portItem.Detach();
                DB.Save(portItem, false);

                telItem.SwitchPortID = portItem.ID;
                telItem.Detach();
                DB.Save(telItem, false);
            }
        }


        void ChangeLocationSave()
        {
            if (InvestigateStatusComboBox.SelectedValue != null)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);


                    AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)changeLocation.OldTelephone);
                    BuchtType reserveBuchtType = Data.BuchtTypeDB.GetBuchtTypeByID(reserveBucht.BuchtTypeID);

                    //if (
                    //  _request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside &&
                    //  (specialCondition.EqualityOfBuchtTypeOptical || specialCondition.NotEqualityOfBuchtTypeOptical)
                    //  || ((reserveBuchtType.ID == (int)DB.BuchtType.OpticalBucht || reserveBuchtType.ParentID == (int)DB.BuchtType.OpticalBucht) && (specialCondition.NotEqualityOfBuchtType))
                    //     )
                    //{
                    //    changeLocation.Detach();// در لود تلفنش مقدار می گیرد
                    //    DB.Save(changeLocation);

                    //    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                    //    tele.InstallAddressID = changeLocation.NewInstallAddressID;
                    //    tele.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                    //    tele.CustomerID = _request.CustomerID;
                    //    tele.Status = (byte)DB.TelephoneStatus.Reserv;
                    //    tele.Detach();
                    //    DB.Save(tele);

                    //    switchPort.Status = (byte)DB.SwitchPortStatus.Install;
                    //    switchPort.Detach();
                    //    DB.Save(switchPort);
                    //}
                    //else if (
                    //    _request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter
                    //    && ((reserveBuchtType.ID == (int)DB.BuchtType.OpticalBucht || reserveBuchtType.ParentID == (int)DB.BuchtType.OpticalBucht))
                    //  )
                    //{
                    //    changeLocation.Detach();// در لود تلفنش مقدار می گیرد
                    //    DB.Save(changeLocation);

                    //    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                    //    tele.InstallAddressID = changeLocation.NewInstallAddressID;
                    //    tele.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                    //    tele.CustomerID = _request.CustomerID;
                    //    tele.Status = (byte)DB.TelephoneStatus.Reserv;
                    //    tele.Detach();
                    //    DB.Save(tele);
                    //}
                    //else
                    //{
                    // تلفنی که قبلا رزرو شده را ازاد میکند
                    if (changeLocation.NewTelephone != null)
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                        telephone.Status = DB.GetTelephoneLastStatus(telephone.TelephoneNo);
                        telephone.Detach();
                        DB.Save(telephone);

                    }
                    // ثبت تلفن جدید در حالت رزرو
                    if (TelephoneNoComboBox.SelectedValue != null)
                    {
                        changeLocation.NewTelephone = (long)TelephoneNoComboBox.SelectedValue;
                        changeLocation.Detach();
                        DB.Save(changeLocation);

                        Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);

                        if (!(tele.Status == (byte)DB.TelephoneStatus.Free || tele.Status == (byte)DB.TelephoneStatus.Discharge))
                        {
                            if (!(_SelectTelephone != null && _SelectTelephone.TelephoneNo == tele.TelephoneNo))
                                throw new Exception(string.Format("تلفن {0} آزاد نمی باشد", tele.TelephoneNo));
                        }


                        tele.Status = (byte)DB.TelephoneStatus.Reserv;
                        tele.Detach();
                        DB.Save(tele);

                        reserveBucht.SwitchPortID = tele.SwitchPortID;
                        reserveBucht.Detach();
                        DB.Save(reserveBucht);
                    }
                    // }
                    ts.Complete();
                }

            }

        }

        void ChangeNoSave()
        {

            //   if (_ChangeNoDetailUserControl.ADSLStatusCheckBox.IsChecked == true) { throw new Exception("این تلفن دارای ADSL میباشد لطفا ابتدا روال تخلیه ADSL را اجرا کنید"); }

            Telephone telephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo((TelephoneNoComboBox.SelectedItem as Telephone).TelephoneNo);
            AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(_ChangeNo.OldTelephoneNo);

            if (!(telephoneItem.Status == (byte)DB.TelephoneStatus.Free || telephoneItem.Status == (byte)DB.TelephoneStatus.Discharge))
            {
                if (!(_SelectTelephone != null && _SelectTelephone.TelephoneNo == telephoneItem.TelephoneNo))
                    throw new Exception(string.Format("تلفن {0} آزاد نمی باشد", telephoneItem.TelephoneNo));
            }

            _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

            using (TransactionScope transaction = new TransactionScope())
            {
                if (Mode == false)
                {
                    //////
                    telephoneItem.Status = (byte)DB.TelephoneStatus.Reserv;
                    _ChangeNo.NewTelephoneNo = telephoneItem.TelephoneNo;
                    //////

                    //////

                    _ChangeNo.NewSwitchPortID = telephoneItem.SwitchPortID;
                    //////

                    TelephoneDB.SaveChangeNo(_request, _ChangeNo, telephoneItem);
                }
                else
                {

                    //////
                    oldTelItem.Status = DB.GetTelephoneLastStatus(oldTelItem.TelephoneNo);

                    TelephoneDB.SaveChangeNo(_request, _ChangeNo, oldTelItem);

                    //////
                    _ChangeNo.NewTelephoneNo = telephoneItem.TelephoneNo;
                    _ChangeNo.NewSwitchPortID = telephoneItem.SwitchPortID;
                    //////

                    //////
                    telephoneItem.Status = (byte)DB.TelephoneStatus.Reserv;
                    //////

                    TelephoneDB.SaveChangeNo(_request, _ChangeNo, telephoneItem);
                }

                transaction.Complete();
            }

        }
        private void DeleteChangeNo()
        {
            if (!(oldTelItem == null || oldTelItem.TelephoneNo == 0))
            {
                oldTelItem.Status = DB.GetTelephoneLastStatus(oldTelItem.TelephoneNo);
            }
            if (
                _ChangeNo != null 
                && 
                _ChangeNo.NewTelephoneNo != null //TODO:rad 13950125
               )
            {
                // اگر تلفن جی اس ام است در هنگام رد اطلاعات سریال آن حذف می شود
                Telephone telItem = TelephoneDB.GetTelephoneByTelephoneNo((long)_ChangeNo.NewTelephoneNo);
                if (telItem.UsageType == (int)DB.TelephoneUsageType.GSM)
                {
                    GSMSimCardItem = GSMSimCardDB.GetGSMSimCardByTelephone(telItem.TelephoneNo);
                    if (GSMSimCardItem != null)
                    {
                        GSMCodeTextBox.Text = string.Empty;
                        GSMSimCardItem.Detach();
                        DB.Save(GSMSimCardItem, false);
                    }
                }
                _ChangeNo.NewTelephoneNo = null;

                //TODO:rad 13950125
                _ChangeNo.NewSwitchPortID = null;
            }


            TelephoneDB.SaveChangeNo(_request, _ChangeNo, oldTelItem);
        }

        #endregion

        #region LoadMethod

        void DayriLoad()
        {
            _installRequest = InstallRequestDB.GetInstallRequestByRequestID(_request.ID);
            Mode = false;

            if (_installInfoSummary.IsRoundNumbercheckBox.IsChecked == true)
            {
                SwitchPreCodeComboBox.IsEnabled = false;
                PreCodeTypeComboBox.IsEnabled = false;
                TelephoneNoComboBox.IsEnabled = false;
            }

            if (_installRequest.IsGSM == false)
            {
                bucht = Data.BuchtDB.GetBuchetByID(investigate.BuchtID);
                assigmentinfo = DB.GetAllInformationByBuchtID(bucht.ID);
                if (assigmentinfo == null)
                    Folder.MessageBox.ShowInfo("اطلاعات فنی تلفن یافت نشد");
            }

            if (CheckBeRound()) return;

            // پیش شماره اونو را استخراج میکند
            ProposalNoTextBox.Text = Data.SwitchPrecodeDB.GetProposalTelephone(bucht.ID);

            if (_SelectTelephone.TelephoneNo != null)
            {

                Mode = true;
                TelInfo telInfo = Data.SwitchDB.GetSwitchInfoByTelNo((long)_SelectTelephone.TelephoneNo);
                SwitchPrecode switchPrecodeItem = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)telInfo.SwitchPrecodeID);


                Switch switchInfo = Data.SwitchDB.GetSwitchByID(switchPrecodeItem.SwitchID);
                SwitchInfo.DataContext = new { SwitchPrecode = switchPrecodeItem, Switch = SwitchInfo };


                ///////////////////////////////

                PreCodeTypeComboBox.SelectedValue = switchPrecodeItem.PreCodeType;
                PreCodeTypeComboBox_SelectionChanged(null, null);

                this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
                SwitchPreCodeComboBox_SelectionChanged(null, null);

                telList = telList.Union(new List<Telephone> { Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_SelectTelephone.TelephoneNo) }).ToList();
                TelephoneNoComboBox.ItemsSource = telList;

                this.TelephoneNoComboBox.SelectedValue = (long)_SelectTelephone.TelephoneNo;
                TelephoneNoComboBox_SelectionChanged(null, null);

                ////////////////////////////////

                InvestigateStatusComboBox.SelectedValue = _request.StatusID;

                oldSwitchItem = SwitchPreCodeComboBox.SelectedItem as SwitchPrecode;
                oldTelItem = TelephoneNoComboBox.SelectedItem as Telephone;
                oldStatusItem = InvestigateStatusComboBox.SelectedItem as StepStatusInfo;

            }
            else
            {
                Mode = false;
            }

        }

        void ChangeLocationload()
        {
            Mode = false;
            InstallInfoUC.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;


            reserveBucht = Data.BuchtDB.GetBuchetByID(investigate.BuchtID);
            BuchtType reserveBuchtType = Data.BuchtTypeDB.GetBuchtTypeByID(reserveBucht.BuchtTypeID);
            assigmentinfo = DB.GetAllInformationByBuchtID(investigate.BuchtID ?? 0);
            if (assigmentinfo == null)
                Folder.MessageBox.ShowInfo("اطلاعات فنی تلفن یافت نشد");

            OldBucht = Data.BuchtDB.GetBuchetByID(changeLocation.OldBuchtID);

            OldTelephonNoBuchtTextBox.Text = changeLocation.OldTelephone.ToString(); // نمایش تلفن قدیم
            BuchtTextTextBox.Text = DB.GetConnectionByBuchtID(changeLocation.OldBuchtID);// نمایش بوخت پر
            reservBuchtTextBox.Text = DB.GetConnectionByBuchtID(reserveBucht.ID); // نمایش بوخت رزرو

            if (CheckBeRound()) return;

            //if (
            //    _request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside &&
            //    (specialCondition.EqualityOfBuchtTypeOptical || specialCondition.NotEqualityOfBuchtTypeOptical)
            //    || ((reserveBuchtType.ID == (int)DB.BuchtType.OpticalBucht || reserveBuchtType.ParentID == (int)DB.BuchtType.OpticalBucht) && (specialCondition.NotEqualityOfBuchtType))
            //    )
            //{
            //    Telephone telephon = Data.TelephoneDB.GetTelephoneNoBySwitchPortID((int)reserveBucht.SwitchPortID);
            //    changeLocation.NewTelephone = telephon.TelephoneNo;


            //    Telephone reservTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
            //    switchPortReserv = Data.SwitchPortDB.GetSwitchPortByID((int)reservTelephone.SwitchPortID);
            //    Switch switchReserv = Data.SwitchDB.GetSwitchByID(switchPortReserv.SwitchID);

            //    //
            //    List<SwitchPort> CurrentSwitchPorts = new List<SwitchPort>();
            //    CurrentSwitchPorts.Add(switchPortReserv);
            //    //

            //    TelInfo telInfo = Data.SwitchDB.GetSwitchInfoByTelNo((long)reservTelephone.TelephoneNo);
            //    SwitchPrecode switchPrecodeItem = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)telInfo.SwitchPrecodeID);

            //    ////////////////////////////


            //    PreCodeTypeComboBox.SelectedValue = switchPrecodeItem.PreCodeType;
            //    PreCodeTypeComboBox_SelectionChanged(null, null);

            //    this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
            //    SwitchPreCodeComboBox_SelectionChanged(null, null);

            //    telList = telList.Union(new List<Telephone> { reservTelephone }).ToList();
            //    TelephoneNoComboBox.ItemsSource = telList;
            //    this.TelephoneNoComboBox.SelectedValue = reservTelephone.TelephoneNo;
            //    TelephoneNoComboBox_SelectionChanged(null, null);


            //    portList = portList.Union(CurrentSwitchPorts).ToList();
            //    PortComboBox.ItemsSource = portList;
            //    this.PortComboBox.SelectedValue = switchPortReserv.ID;

            //    if (switchPortReserv != null && switchPortReserv.Type == true)
            //    {
            //        PortComboBox.IsEnabled = false;
            //    }
            //    ////////////////////////////


            //    PreCodeTypeComboBox.IsEnabled = false;
            //    SwitchPreCodeComboBox.IsEnabled = false;
            //    TelephoneNoComboBox.IsEnabled = false;
            //    TelItemsDataGrid.IsEnabled = false;

            //}
            //else if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter
            //        && ((reserveBuchtType.ID == (int)DB.BuchtType.OpticalBucht || reserveBuchtType.ParentID == (int)DB.BuchtType.OpticalBucht))
            //        )
            //{
            //    Telephone telephon = Data.TelephoneDB.GetTelephoneNoBySwitchPortID((int)reserveBucht.SwitchPortID);
            //    changeLocation.NewTelephone = telephon.TelephoneNo;


            //    Telephone reservTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
            //    switchPortReserv = Data.SwitchPortDB.GetSwitchPortByID((int)reservTelephone.SwitchPortID);
            //    Switch switchReserv = Data.SwitchDB.GetSwitchByID(switchPortReserv.SwitchID);

            //    //
            //    List<SwitchPort> CurrentSwitchPorts = new List<SwitchPort>();
            //    CurrentSwitchPorts.Add(switchPortReserv);
            //    //

            //    TelInfo telInfo = Data.SwitchDB.GetSwitchInfoByTelNo((long)reservTelephone.TelephoneNo);
            //    SwitchPrecode switchPrecodeItem = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)telInfo.SwitchPrecodeID);

            //    ////////////////////////////


            //    PreCodeTypeComboBox.SelectedValue = switchPrecodeItem.PreCodeType;
            //    PreCodeTypeComboBox_SelectionChanged(null, null);

            //    this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
            //    SwitchPreCodeComboBox_SelectionChanged(null, null);

            //    telList = telList.Union(new List<Telephone> { reservTelephone }).ToList();
            //    TelephoneNoComboBox.ItemsSource = telList;
            //    this.TelephoneNoComboBox.SelectedValue = reservTelephone.TelephoneNo;
            //    TelephoneNoComboBox_SelectionChanged(null, null);


            //    portList = portList.Union(CurrentSwitchPorts).ToList();
            //    PortComboBox.ItemsSource = portList;
            //    this.PortComboBox.SelectedValue = switchPortReserv.ID;

            //    if (switchPortReserv != null && switchPortReserv.Type == true)
            //    {
            //        PortComboBox.IsEnabled = false;
            //    }
            //    ////////////////////////////


            //    PreCodeTypeComboBox.IsEnabled = false;
            //    SwitchPreCodeComboBox.IsEnabled = false;
            //    TelephoneNoComboBox.IsEnabled = false;
            //    TelItemsDataGrid.IsEnabled = false;

            //}
            //else
            //{

            if (changeLocation.NewTelephone != null)
            {
                Mode = true;

                Telephone reservTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                Switch switchReserv = Data.SwitchDB.GetSwitchByTelephonNo(reservTelephone.TelephoneNo);

                TelInfo telInfo = Data.SwitchDB.GetSwitchInfoByTelNo((long)reservTelephone.TelephoneNo);
                SwitchPrecode switchPrecodeItem = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)telInfo.SwitchPrecodeID);

                ////////////////////////////


                PreCodeTypeComboBox.SelectedValue = switchPrecodeItem.PreCodeType;
                PreCodeTypeComboBox_SelectionChanged(null, null);

                this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
                SwitchPreCodeComboBox_SelectionChanged(null, null);

                telList = telList.Union(new List<Telephone> { reservTelephone }).ToList();
                TelephoneNoComboBox.ItemsSource = telList;

                this.TelephoneNoComboBox.SelectedValue = reservTelephone.TelephoneNo;
                TelephoneNoComboBox_SelectionChanged(null, null);
                ////////////////////////////
            }
            //  }
            InvestigateStatusComboBox.SelectedValue = _request.StatusID;

            //BuchtInfo.Visibility = Visibility.Visible;
        }

        void ChangeNoLoad()
        {

            Mode = false;
            InstallInfoUC.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;

            requestType = Data.RequestTypeDB.getRequestTypeByID((int)DB.RequestType.ChangeNo);

            ChooseNoDetail.Header = string.Format("جزئیات درخواست {0}", requestType.Title);
            _ChangeNoDetailUserControl = new UserControls.ChangeNoDetailUserControl(_request.ID);
            ChooseNoDetail.Content = _ChangeNoDetailUserControl;
            ChooseNoDetail.DataContext = _ChangeNoDetailUserControl;
            ChooseNoDetail.Visibility = Visibility.Visible;
            Title = "تعیین شماره ";


            _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_request.ID);

            assigmentinfo = DB.GetAllInformationByTelephoneNo(_ChangeNo.OldTelephoneNo);

            if (assigmentinfo == null)
                Folder.MessageBox.ShowInfo("اطلاعات فنی تلفن یافت نشد");

            if (CheckBeRound()) return;

            if (_ChangeNo.NewTelephoneNo != null)
            {
                Mode = true;

                Telephone reservTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_ChangeNo.NewTelephoneNo);

                Bucht bucht = Data.BuchtDB.GetBuchetByID((long)_ChangeNo.OldBuchtID);

                //
                Switch switchReserv = Data.SwitchDB.GetSwitchByTelephonNo((long)_ChangeNo.NewTelephoneNo);

                TelInfo telInfo = Data.SwitchDB.GetSwitchInfoByTelNo((long)reservTelephone.TelephoneNo);
                SwitchPrecode switchPrecodeItem = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)telInfo.SwitchPrecodeID);

                PreCodeTypeComboBox.SelectedValue = switchPrecodeItem.PreCodeType;
                PreCodeTypeComboBox_SelectionChanged(null, null);

                this.SwitchPreCodeComboBox.SelectedValue = switchPrecodeItem.ID;
                SwitchPreCodeComboBox_SelectionChanged(null, null);

                telList = telList.Union(new List<Telephone> { Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_ChangeNo.NewTelephoneNo) }).ToList();
                TelephoneNoComboBox.ItemsSource = telList;
                this.TelephoneNoComboBox.SelectedValue = _ChangeNo.NewTelephoneNo;
                TelephoneNoComboBox_SelectionChanged(null, null);

                oldSwitchItem = SwitchPreCodeComboBox.SelectedItem as SwitchPrecode;
                oldTelItem = TelephoneNoComboBox.SelectedItem as Telephone;

            }



        }

        #endregion

        #region Event
        private void PreCodeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PreCodeTypeComboBox.SelectedValue != null)
            {

                if (assigmentinfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet || assigmentinfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.WLL)
                {
                    switchPrecodeList = Data.SwitchPrecodeDB.GetOpticalCabinetSwitchPrecodeByCenterIDAndType(_request.CenterID, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assigmentinfo.CabinetSwitchID);
                }
                else if (_installRequest != null && _installRequest.IsGSM == true)
                {
                    switchPrecodeList = Data.SwitchPrecodeDB.GetSwitchPrecodeByCenterIDAndType(_request.CenterID, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), (int)DB.TelephoneUsageType.GSM);
                }
                else
                {
                    switchPrecodeList = Data.SwitchPrecodeDB.GetSwitchPrecodeByCenterIDAndType(_request.CenterID, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), (int)DB.TelephoneUsageType.Usuall);
                }
                SwitchPreCodeComboBox.ItemsSource = switchPrecodeList;

            }
        }

        private void SwitchPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchPreCodeComboBox.SelectedValue != null)
            {

                SwitchPrecode switchPrecodeItem = switchPrecodeList.Where(s => s.ID == (int)SwitchPreCodeComboBox.SelectedValue).SingleOrDefault();
                if (switchPrecodeItem != null)
                {
                    switchInfo = Data.SwitchDB.GetSwitchByID((int)switchPrecodeItem.SwitchID);
                    SwitchInfo.DataContext = new { SwitchPrecode = switchPrecodeItem, Switch = switchInfo };
                    // telList = TelephoneDB.GetTelephoneBySwitchPreCodeNo(switchPrecodeItem, (int)switchPrecodeItem.ID, (byte)switchPrecodeItem.PreCodeType).Where(t => t.Status == (byte)DB.TelephoneStatus.Free).ToList();

                    TelephoneList();
                    // لیست پورت ها مربوط به سوییچ را تهیه میکند
                    portList = SwitchPortDB.GetSwitchPortsFreeOfOpticalBuchtBySwitch(switchPrecodeItem);

                    PortComboBox.ItemsSource = portList;

                    //TelItemsDataGrid.ItemsSource = telList;
                    this.ResizeWindow();
                }
            }

        }

        private void TelephoneNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TelephoneNoComboBox.SelectedValue != null)
                {
                    Telephone telephone = telList.Where(t => t.TelephoneNo == (long)TelephoneNoComboBox.SelectedValue).Take(1).SingleOrDefault();

                    if (telephone.UsageType == (int)DB.TelephoneUsageType.GSM)
                    {
                        GSMCodeLabel.Visibility = Visibility.Visible;
                        GSMCodeTextBox.Visibility = Visibility.Visible;

                        GSMSimCardItem = GSMSimCardDB.GetGSMSimCardByTelephone(telephone.TelephoneNo);
                        if (GSMSimCardItem != null)
                        {
                            GSMCodeTextBox.Text = GSMSimCardItem.Code;
                        }
                        else
                        {
                            GSMCodeTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        GSMCodeLabel.Visibility = Visibility.Collapsed;
                        GSMCodeTextBox.Visibility = Visibility.Collapsed;
                    }



                    // برسی ثابت یا شناور بودن پورت
                    if (telephone != null)
                    {
                        SwitchPort switchPort = Data.SwitchPortDB.GetSwitchPortByID((int)telephone.SwitchPortID);
                        if (switchPort != null)
                        {
                            // تازه سازی لیست پورت ها
                            portList = portList.Where(t => t.Status == (byte)DB.SwitchPortStatus.Free).ToList();
                            if (!portList.Any(t => t.ID == switchPort.ID)) portList.Add(switchPort);
                            PortComboBox.ItemsSource = portList;
                            this.PortComboBox.SelectedValue = switchPort.ID;
                            if (switchPort.Type == true)
                            {
                                PortComboBox.IsEnabled = false;
                            }
                            else if (switchPort.Type == false)
                            {
                                this.PortComboBox.SelectedValue = switchPort.ID;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ShowErrorMessage("خطا در انتخاب شماره تلفن", ex);
                //throw;
            }
        }

        private void TelItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelItemsDataGrid.SelectedItem != null)
            {
                TelephoneInfo telephoneInfo = TelItemsDataGrid.SelectedItem as TelephoneInfo;
                if (telephoneInfo != null)
                {
                    this.TelephoneNoComboBox.SelectedValue = telList.Where(t => t.TelephoneNo == (long)telephoneInfo.TelephoneNo).Take(1).SingleOrDefault().TelephoneNo;

                    TelephoneNoComboBox_SelectionChanged(null, null);
                }

            }
        }
        #endregion

        #region Action

        public override bool Save()
        {

            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }


            try
            {

                Telephone telItem = TelephoneNoComboBox.SelectedItem as Telephone;
                SwitchPort portItem = PortComboBox.SelectedItem as SwitchPort;

                if (telItem == null)
                {
                    throw new Exception("برای تعیین شماره نیاز به انتخاب تلفن می باشد.");
                }

                if (portItem == null)
                {
                    throw new Exception("برای تعیین شماره نیاز به انتخاب پورت می باشد.");
                }


                CheckForChangePort(telItem, portItem);

                // اگر تلفن جی اس ام است شماره سریال سیم کارت آن ذخیره می شود
                if (telItem.UsageType == (int)DB.TelephoneUsageType.GSM)
                {
                    if (string.IsNullOrEmpty(GSMCodeTextBox.Text.Trim()))
                    {
                        GSMCodeTextBox.Focus();
                        throw new Exception("لطفاً سریال سیم کارت را مشخص نمائید");
                    }

                    if (GSMSimCardItem == null || GSMSimCardItem.TelephoneNo == 0)
                    {
                        GSMSimCardItem = new GSMSimCard();
                        if (!GSMSimCardDB.GsmCodeIsExist(GSMCodeTextBox.Text.Trim()))
                        {
                            GSMSimCardItem.TelephoneNo = telItem.TelephoneNo;
                            GSMSimCardItem.Code = GSMCodeTextBox.Text.Trim();
                            DB.Save(GSMSimCardItem, true);
                        }
                        else
                        {
                            GSMCodeTextBox.Focus();
                            throw new Exception("سریال وارد شده تکراری میباشد");
                        }
                    }
                    else
                    {
                        if (GSMSimCardItem.Code != GSMCodeTextBox.Text.Trim() && GSMSimCardDB.GsmCodeIsExist(GSMCodeTextBox.Text.Trim()))
                        {
                            GSMCodeTextBox.Focus();
                            throw new Exception("سریال وارد شده تکراری میباشد");
                        }
                        GSMSimCardItem.Code = GSMCodeTextBox.Text.Trim();
                        GSMSimCardItem.Detach();
                        DB.Save(GSMSimCardItem, false);
                    }
                }


                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.Dayri:
                    case (int)DB.RequestType.Reinstall:
                        DayriSave();
                        break;

                    case (int)DB.RequestType.ChangeLocationCenterInside:
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationSave();
                        break;

                    case (int)DB.RequestType.ChangeNo:
                        ChangeNoSave();
                        break;



                }

                ShowSuccessMessage("تعیین شماره انجام شد");

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    ShowErrorMessage("اطلاعات وارد شده در دیتا بیس موجود است", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در تعیین شماره", ex);
                }
                IsSaveSuccess = false;
            }

            base.Save();

            return IsSaveSuccess;
        }



        public override bool Deny()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    base.RequestID = _request.ID;


                    switch (_request.RequestTypeID)
                    {
                        case (int)DB.RequestType.Dayri:
                        case (int)DB.RequestType.Reinstall:
                            DeleteDayer();
                            break;
                        case (int)DB.RequestType.ChangeNo:
                            DeleteChangeNo();
                            break;
                        case (int)DB.RequestType.ChangeLocationCenterInside:
                        case (int)DB.RequestType.ChangeLocationCenterToCenter:
                            DeleteChangaLocation();
                            break;
                    }

                    ts.Complete();
                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطادر رد درخواست", ex);
            }

            base.Deny();

            return IsRejectSuccess;
        }

        private void DeleteChangaLocation()
        {
            if (InvestigateStatusComboBox.SelectedValue != null)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    //if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside && (specialCondition.EqualityOfBuchtTypeOptical || specialCondition.NotEqualityOfBuchtTypeOptical))
                    //{
                    //    changeLocation.NewTelephone = null;
                    //    changeLocation.Detach();
                    //    DB.Save(changeLocation);

                    //    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                    //    tele.InstallAddressID = null;
                    //    tele.CorrespondenceAddressID = null;
                    //    tele.CustomerID = null;
                    //    tele.Status = DB.GetTelephoneLastStatus(tele.TelephoneNo);
                    //    tele.Detach();
                    //    DB.Save(tele);
                    //}
                    //else
                    // if the telephone is reserved, Releases it.
                    if (changeLocation.NewTelephone != null)
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);

                        // اگر تلفن جی اس ام است در هنگام رد اطلاعات سریال آن حذف می شود
                        if (telephone.UsageType == (int)DB.TelephoneUsageType.GSM)
                        {
                            GSMSimCardItem = GSMSimCardDB.GetGSMSimCardByTelephone(telephone.TelephoneNo);
                            if (GSMSimCardItem != null)
                            {
                                GSMCodeTextBox.Text = string.Empty;
                                GSMSimCardItem.Detach();
                                DB.Save(GSMSimCardItem, false);
                            }
                        }


                        telephone.Status = DB.GetTelephoneLastStatus(telephone.TelephoneNo);
                        telephone.InstallAddressID = null;
                        telephone.CorrespondenceAddressID = null;
                        telephone.CustomerID = null;
                        telephone.Detach();
                        DB.Save(telephone);

                        changeLocation.NewTelephone = null;
                        changeLocation.Detach();
                        DB.Save(changeLocation);


                        reserveBucht.SwitchPortID = null;
                        reserveBucht.Detach();
                        DB.Save(reserveBucht);
                    }

                    ts.Complete();
                }

            }
        }



        private void DeleteDayer()
        {
            Telephone telItem = new Telephone();
            investigate = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).Take(1).SingleOrDefault();
            if (investigate != null && investigate.BuchtID != null)
            {
                bucht = Data.BuchtDB.GetBuchtByID((long)investigate.BuchtID);
            }
            if (investigate != null && _SelectTelephone.TelephoneNo != null)
            {
                telItem = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_SelectTelephone.TelephoneNo);
            }

            if (bucht != null)
            {
                bucht.SwitchPortID = null;
                bucht.Detach();
                DB.Save(bucht);
            }

            if (telItem.TelephoneNo != 0)
            {
                telItem.Status = DB.GetTelephoneLastStatus(oldTelItem.TelephoneNo);
                telItem.InstallAddressID = null;
                telItem.CorrespondenceAddressID = null;
                telItem.CustomerID = null;


                if (_installRequest.IsGSM == false)
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo((long)_SelectTelephone.TelephoneNo);
                    telephone.CustomerTypeID = null;
                    telephone.CustomerGroupID = null;
                    telephone.ChargingType = null;
                    telephone.PosessionType = null;
                    telephone.InstallationDate = null;
                    telephone.DischargeDate = null;
                    telephone.CauseOfTakePossessionID = null;
                }

                telItem.Detach();
                DB.Save(telItem);

            }

            // اگر تلفن جی اس ام است در هنگام رد اطلاعات سریال آن حذف می شود
            if (telItem.UsageType == (int)DB.TelephoneUsageType.GSM)
            {
                GSMSimCardItem = GSMSimCardDB.GetGSMSimCardByTelephone(telItem.TelephoneNo);
                if (GSMSimCardItem != null)
                {
                    GSMCodeTextBox.Text = string.Empty;
                    GSMSimCardItem.Detach();
                    DB.Save(GSMSimCardItem, false);
                }
            }

            _SelectTelephone = SelectTelephoneDB.GetSelectTelephone(_request.ID);
            if (_SelectTelephone != null)
            {
                DB.Delete<SelectTelephone>(_SelectTelephone.ID);
            }



            _request.TelephoneNo = 0;
            _request.Detach();
            DB.Save(_request);
        }

        public override bool Forward()
        {
            try
            {

                Status Status = Data.StatusDB.GetStatueByStatusID((int)InvestigateStatusComboBox.SelectedValue);
                base.RequestID = _request.ID;
                Save();

                if (IsSaveSuccess == true)
                {
                    IsForwardSuccess = true;

                    if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside && Status.StatusType == (byte)DB.RequestStatusType.Completed)
                    {
                        if (Data.ChangeLocationDB.ChechForAutoForwardInChangeLocation(ref _request, changeLocation) == true)
                        {
                            Data.WorkFlowDB.SetNextState(DB.Action.AutomaticForward, _request.StatusID, _request.ID);
                            IsForwardSuccess = false;
                        }
                        else
                        {
                            IsForwardSuccess = true;
                        }
                    }
                    else if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter && Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                    {
                        _request.CenterID = (int)changeLocation.SourceCenter;
                        _request.Detach();
                        DB.Save(_request);
                        IsForwardSuccess = true;
                    }
                    else if (_request.RequestTypeID == (byte)DB.RequestType.Dayri && _installRequest.IsGSM)
                    {
                        if (_installRequest.IsGSM)
                        {
                            Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo((long)_SelectTelephone.TelephoneNo);
                            telephone.CustomerTypeID = _installRequest.TelephoneType;
                            telephone.CustomerGroupID = _installRequest.TelephoneTypeGroup;
                            telephone.ChargingType = _installRequest.ChargingType;
                            telephone.PosessionType = _installRequest.PosessionType;
                            telephone.InstallationDate = DB.GetServerDate();
                            telephone.DischargeDate = null;
                            telephone.CauseOfTakePossessionID = null;
                            telephone.Detach();
                            DB.Save(telephone);
                        }
                    }
                }
                else
                {
                    IsForwardSuccess = false;
                }



            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ارجاع", ex);
            }


            return IsForwardSuccess;
        }

        #endregion

        #region Method

        private void TelephoneList()
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            if (PreCodeTypeComboBox.SelectedValue != null && SwitchPreCodeComboBox.SelectedValue != null)
            {
                SwitchPrecode switchPrecodeItem = switchPrecodeList.Where(s => s.ID == (int)SwitchPreCodeComboBox.SelectedValue).SingleOrDefault();
                if (assigmentinfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet || assigmentinfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.WLL)
                {
                    int count = 0;
                    telList = TelephoneDB.GetOpticalCabinetFreeTelephoneBySwitchPreCodeNo(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assigmentinfo.CabinetSwitchID);
                    TelItemsDataGrid.ItemsSource = TelephoneDB.GetOpticalCabinetFreeTelephoneInfoBySwitchPreCodeNo(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assigmentinfo.CabinetSwitchID, startRowIndex, pageSize, out count);
                    Pager.TotalRecords = count;
                }
                else if (_installRequest != null && _installRequest.IsGSM == true)
                {
                    int count = 0;
                    telList = TelephoneDB.GetFreeTelephoneBySwitchPreCodeNoWithoutOptiacalBucht(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), (int)DB.TelephoneUsageType.GSM);
                    TelItemsDataGrid.ItemsSource = TelephoneDB.GetFreeTelephoneInfoBySwitchPreCodeNoWithOutOpticalBucht(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), (int)DB.TelephoneUsageType.GSM, startRowIndex, pageSize, out count);
                    Pager.TotalRecords = count;
                }
                else
                {
                    int count = 0;
                    telList = TelephoneDB.GetFreeTelephoneBySwitchPreCodeNoWithoutOptiacalBucht(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), (int)DB.TelephoneUsageType.Usuall);
                    TelItemsDataGrid.ItemsSource = TelephoneDB.GetFreeTelephoneInfoBySwitchPreCodeNoWithOutOpticalBucht(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), (int)DB.TelephoneUsageType.Usuall, startRowIndex, pageSize, out count);
                    Pager.TotalRecords = count;
                }
                TelephoneNoComboBox.ItemsSource = telList;
            }

        }
        private void Pager_PageChanged(int pageNumber)
        {
            TelephoneList();
        }
        #endregion




    }
}