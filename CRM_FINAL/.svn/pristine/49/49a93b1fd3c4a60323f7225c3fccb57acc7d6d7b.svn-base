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
using System.Xml.Linq;
using System.Transactions;

namespace CRM.Application.Views
{


    public partial class DayeriForm : Local.RequestFormBase
    {
        #region fileds && Properties
        private long _conterID = 0;
        public Request _request { get; set; }
        public SwitchCodeInfo _switchCode { get; set; }
        public SelectTelephone _SelectTelephone = new SelectTelephone();
        public Wiring _wiring { get; set; }
        public IssueWiring _issueWiring { get; set; }
        public InstallLine _instLine { get; set; }
        public Counter _counter { get; set; }
        public Counter _oldCounter;
        public ChangeNo _changeNo { get; set; }
        public TakePossession _takePossession { get; set; }
        public InstallRequest _installRequset { get; set; }
        public Data.RefundDeposit _refundDeposit { get; set; }


        private CRM.Data.CutAndEstablish _cutAndEstablish { get; set; }
        public UserControls.ChangeLocationMDFWirigUserControl _ChangeLocationMDFWirig { get; set; }
        private ChangeLocation changeLocation { get; set; }
        private CRM.Data.SpecialService _specialService { get; set; }
        CRM.Data.Schema.SequenceIDs InstallSpecialServiceSequenceIDs;
        CRM.Data.Schema.SequenceIDs UninstallSpecialServiceSequenceIDs;

        private UserControls.OpenAndCloseZero _openAndCloseZeroUserControl;
        private ZeroStatus _zeroStatus;

        public UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        public UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        public UserControls.InstallInfoSummary _installInfoSummary { get; set; }
        public UserControls.InvestigateInfoSummary _investigateInfoSummary { get; set; }
        public UserControls.SpecialService _specialServiceUserControl { get; set; }
        public UserControls.TelephoneFeaturesUserControl _telephoneFeaturesUserControl { get; set; }

        private UserControls.PBXUserControl _PBXUserControl;
        #endregion

        #region Constructor
        public DayeriForm()
        {

            InitializeComponent();
            Initialize();
        }

        public DayeriForm(long id)
            : this()
        {
            _instLine = new InstallLine();
            _counter = new Counter();
            _request = Data.RequestDB.GetRequestByID(id);
            base.RequestID = _request.ID;


            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.Dayri:
                case (byte)DB.RequestType.Reinstall:

                    RequestInfoGroupBox.Visibility = Visibility.Visible;
                    _installInfoSummary = new InstallInfoSummary(_request.ID);
                    _installInfoSummary.InstallInfoExpander.IsExpanded = true;
                    _installInfoSummary.AddressInfoGroupBox.Visibility = Visibility.Collapsed;
                    RequestInfoGroupBox.Content = _installInfoSummary;
                    RequestInfoGroupBox.DataContext = _installInfoSummary;
                    break;
            }

        }

        #endregion

        #region Method
        private void Initialize()
        {
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {

            _issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            _SelectTelephone = SelectTelephoneDB.GetSelectTelephone(_request.ID);
            _installRequset = Data.InstallRequestDB.GetInstallRequestByRequestID(_request.ID);

            if (_issueWiring == null)
            {
                if (_request.RequestTypeID != (byte)DB.RequestType.Dayri)
                {
                    IssueWiringForm issueWiringForm = new IssueWiringForm(_request.ID);
                    issueWiringForm.Load();
                    issueWiringForm.Save();
                    _issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                }
                else
                {
                    if (_installRequset != null && !_installRequset.IsGSM)
                    {
                        IssueWiringForm issueWiringForm = new IssueWiringForm(_request.ID);
                        issueWiringForm.Load();
                        issueWiringForm.Save();
                        _issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                    }
                }
            }

            _wiring = new Wiring();


            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            Center center = Data.CenterDB.GetCenterById((int)_request.CenterID);

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.Dayri:
                case (byte)DB.RequestType.Reinstall:

                    _installInfoSummary = new InstallInfoSummary(_request.ID);
                    _investigateInfoSummary = new InvestigateInfoSummary(_request.ID);



                    _switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);
                    if (!_installRequset.IsGSM)
                    {
                        _wiring = Data.WiringDB.GetInfoWiringByInvestigatePossibility(_investigateInfoSummary.investigate.ID);
                        _wiring.InvestigatePossibilityID = _investigateInfoSummary.investigate.ID;
                    }
                    PreCodeTypeComboBox.SelectedValue = _switchCode.SwitchPreNo;
                    _counter.TelephoneNo = _switchCode.TelephoneNo;
                    NewTelphoneCounter.TelephoneNo = _switchCode.TelephoneNo;

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;

                case (byte)DB.RequestType.ChangeNo:

                    OldCountorInfo.Visibility = Visibility.Visible;
                    _changeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_request.ID);

                    _telephoneFeaturesUserControl = new TelephoneFeaturesUserControl(_changeNo.OldTelephoneNo);
                    TelephoneFeaturesUserControl.DataContext = _telephoneFeaturesUserControl;
                    TelephoneFeaturesUserControl.Content = _telephoneFeaturesUserControl;
                    TelephoneFeaturesUserControl.Visibility = Visibility.Visible;

                    _switchCode = DB.GetSwitchCodeInfo(_changeNo.NewTelephoneNo ?? 0);
                    _wiring = Data.WiringDB.GetWiringByIssueWiringID(_issueWiring.ID);

                    OldTelphoneCounter.TelephoneNo = _changeNo.OldTelephoneNo;
                    NewTelphoneCounter.TelephoneNo = _changeNo.NewTelephoneNo ?? 0;

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    if (_switchCode != null)
                        PreCodeTypeComboBox.SelectedValue = _switchCode.SwitchPreNo;
                    break;

                case (byte)DB.RequestType.RefundDeposit:
                    //WiringNoTextBox.Visibility = Visibility.Collapsed;
                    //WiringTypeComboBox.Visibility = Visibility.Collapsed;
                    //WiringNoTextBoxLabel.Visibility = Visibility.Collapsed;
                    //WiringTypeComboBoxLabel.Visibility = Visibility.Collapsed;
                    //SwitchDayeriInfoGroupBox.Visibility = Visibility.Collapsed;

                    _wiring = Data.WiringDB.GetWiringByIssueWiringID(_issueWiring.ID);
                    _refundDeposit = Data.RefundDepositDB.GetRefundDepositByID(_request.ID);
                    _switchCode = DB.GetSwitchCodeInfo(_refundDeposit.TelephoneNo ?? 0);
                    _counter.TelephoneNo = _refundDeposit.TelephoneNo ?? 0;
                    NewTelphoneCounter.TelephoneNo = _refundDeposit.TelephoneNo ?? 0;

                    PreCodeTypeComboBox.SelectedValue = _switchCode.SwitchPreNo;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;

                case (byte)DB.RequestType.Dischargin:

                    _takePossession = Data.TakePossessionDB.GetTakePossessionByID(_request.ID);
                    _telephoneFeaturesUserControl = new TelephoneFeaturesUserControl((long)_takePossession.OldTelephone);

                    TelephoneFeaturesUserControl.DataContext = _telephoneFeaturesUserControl;
                    TelephoneFeaturesUserControl.Content = _telephoneFeaturesUserControl;
                    TelephoneFeaturesUserControl.Visibility = Visibility.Visible;

                    if (_issueWiring != null)
                    {
                        _wiring = Data.WiringDB.GetWiringByIssueWiringID(_issueWiring.ID);

                        if (_wiring == null) MessageBox.Show("اطلاعات سیم بندی یافت نشد");
                    }

                    NewCountorInfo.Header = "تلفن تخلیه";
                    NewTelphoneCounter.NewTelephoneTextBox.IsReadOnly = true;
                    NewTelphoneCounter.TelephoneNo = (long)_request.TelephoneNo;
                    _switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;
                case (byte)DB.RequestType.ChangeLocationCenterInside:

                    changeLocation = new ChangeLocation();
                    OldCountorInfo.Visibility = Visibility.Visible;
                    changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_request.ID);

                    if (changeLocation.OldTelephone != null)
                    {
                        _telephoneFeaturesUserControl = new TelephoneFeaturesUserControl((long)changeLocation.OldTelephone);
                        TelephoneFeaturesUserControl.DataContext = _telephoneFeaturesUserControl;
                        TelephoneFeaturesUserControl.Content = _telephoneFeaturesUserControl;
                        TelephoneFeaturesUserControl.Visibility = Visibility.Visible;
                    }

                    _switchCode = DB.GetSwitchCodeInfo(changeLocation.NewTelephone ?? 0);
                    _wiring = Data.WiringDB.GetWiringByIssueWiringID(_issueWiring.ID);
                    if (changeLocation != null)
                    {
                        OldTelphoneCounter.TelephoneNo = changeLocation.OldTelephone ?? 0;
                        NewTelphoneCounter.TelephoneNo = changeLocation.NewTelephone ?? 0;
                    }
                    else
                    {
                        Folder.MessageBox.ShowInfo("اطلاعات در خواست تغییر مکان یافت نشد");
                        throw new Exception("اطلاعات در خواست تغییر مکان یافت نشد");
                    }
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;

                case (byte)DB.RequestType.ChangeLocationCenterToCenter:

                    changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_request.ID);
                    OldCountorInfo.Visibility = Visibility.Visible;
                    NewCountorInfo.Visibility = Visibility.Visible;

                    _telephoneFeaturesUserControl = new TelephoneFeaturesUserControl((long)changeLocation.OldTelephone);
                    TelephoneFeaturesUserControl.DataContext = _telephoneFeaturesUserControl;
                    TelephoneFeaturesUserControl.Content = _telephoneFeaturesUserControl;
                    TelephoneFeaturesUserControl.Visibility = Visibility.Visible;
                    TelephoneFeaturesUserControl.TelephoneFeatuerExpander.IsExpanded = true;

                    _wiring = Data.WiringDB.GetWiringByIssueWiringID(_issueWiring.ID);
                    _switchCode = DB.GetSwitchCodeInfo(changeLocation.NewTelephone ?? 0);

                    // اگر درخواست در مبدا است
                    if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
                    {
                        _switchCode = DB.GetSwitchCodeInfo(changeLocation.OldTelephone ?? 0);
                        NewCountorInfo.IsEnabled = false;
                    }
                    else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
                    {
                        _switchCode = DB.GetSwitchCodeInfo(changeLocation.NewTelephone ?? 0);
                        OldCountorInfo.IsEnabled = false;
                    }

                    if (changeLocation != null)
                    {
                        OldTelphoneCounter.TelephoneNo = changeLocation.OldTelephone ?? 0;
                        NewTelphoneCounter.TelephoneNo = changeLocation.NewTelephone ?? 0;
                    }
                    else
                    {
                        Folder.MessageBox.ShowInfo("اطلاعات در خواست تغییر مکان یافت نشد");
                        throw new Exception("اطلاعات در خواست تغییر مکان یافت نشد");
                    }
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;

                case (byte)DB.RequestType.SpecialService:
                    WiringNoTextBoxLabel.Visibility = Visibility.Collapsed;
                    WiringNoTextBox.Visibility = Visibility.Collapsed;


                    WiringTypeComboBoxLabel.Visibility = Visibility.Collapsed;
                    WiringTypeComboBox.Visibility = Visibility.Collapsed;

                    TelephoneLabel.Visibility = Visibility.Visible;
                    TelephoneTextBox.Visibility = Visibility.Visible;

                    NewCountorInfo.Visibility = Visibility.Collapsed;
                    OldCountorInfo.Visibility = Visibility.Collapsed;

                    _switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);

                    _specialServiceUserControl = new UserControls.SpecialService(_request.ID, (long)_request.TelephoneNo);

                    _specialService = Data.SpecialServiceDB.GetSpecialServiceByID(_request.ID);

                    InstallSpecialServiceSequenceIDs = LogSchemaUtility.Deserialize<CRM.Data.Schema.SequenceIDs>(_specialService.InstallSpecialService.ToString());

                    UninstallSpecialServiceSequenceIDs = LogSchemaUtility.Deserialize<CRM.Data.Schema.SequenceIDs>(_specialService.UninstallSpecialService.ToString());


                    _specialServiceUserControl.SpecialServiceListView.ItemContainerGenerator.StatusChanged += new EventHandler(ContainerStatusChanged);

                    _specialServiceUserControl.SpecialServiceListView.IsEnabled = false;
                    RequestDetail.Content = _specialServiceUserControl;
                    RequestDetail.DataContext = _specialServiceUserControl;

                    RequestDetail.Visibility = Visibility.Visible;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;

                case (byte)DB.RequestType.PBX:

                    WiringNoTextBoxLabel.Visibility = Visibility.Collapsed;
                    WiringNoTextBox.Visibility = Visibility.Collapsed;


                    WiringTypeComboBoxLabel.Visibility = Visibility.Collapsed;
                    WiringTypeComboBox.Visibility = Visibility.Collapsed;

                    TelephoneLabel.Visibility = Visibility.Visible;
                    TelephoneTextBox.Visibility = Visibility.Visible;

                    NewCountorInfo.Visibility = Visibility.Collapsed;
                    OldCountorInfo.Visibility = Visibility.Collapsed;

                    _switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);

                    _PBXUserControl = new CRM.Application.UserControls.PBXUserControl(RequestID, (long)_request.TelephoneNo);
                    RequestDetail.Content = _PBXUserControl;
                    RequestDetail.DataContext = _PBXUserControl;
                    RequestDetail.Visibility = Visibility.Visible;
                    RequestDetail.IsEnabled = false;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
                    break;

                case (byte)DB.RequestType.OpenAndCloseZero:

                    WiringNoTextBoxLabel.Visibility = Visibility.Collapsed;
                    WiringNoTextBox.Visibility = Visibility.Collapsed;


                    WiringTypeComboBoxLabel.Visibility = Visibility.Collapsed;
                    WiringTypeComboBox.Visibility = Visibility.Collapsed;

                    TelephoneLabel.Visibility = Visibility.Visible;
                    TelephoneTextBox.Visibility = Visibility.Visible;

                    NewCountorInfo.Visibility = Visibility.Collapsed;
                    OldCountorInfo.Visibility = Visibility.Collapsed;


                    _switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                    _openAndCloseZeroUserControl = new UserControls.OpenAndCloseZero(_request.ID, (long)_request.TelephoneNo);
                    _openAndCloseZeroUserControl.ZeroBlockComboBox.IsEnabled = false;
                    _openAndCloseZeroUserControl.SecondZeroBlockingWithCostCheckBox.Visibility = Visibility.Visible;
                    _openAndCloseZeroUserControl.SecondZeroBlockingWithCostLabel.Visibility = Visibility.Visible;
                    _openAndCloseZeroUserControl.SecondZeroBlockingWithCostCheckBox.IsEnabled = false;
                    _zeroStatus = Data.ZeroStatusDB.GetZeroStatusByID(_request.ID);

                    RequestDetail.Content = _openAndCloseZeroUserControl;
                    RequestDetail.DataContext = _openAndCloseZeroUserControl;

                    RequestDetail.Visibility = Visibility.Visible;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;

                case (byte)DB.RequestType.CutAndEstablish:
                case (byte)DB.RequestType.Connect:

                    WiringNoTextBoxLabel.Visibility = Visibility.Collapsed;
                    WiringNoTextBox.Visibility = Visibility.Collapsed;


                    WiringTypeComboBoxLabel.Visibility = Visibility.Collapsed;
                    WiringTypeComboBox.Visibility = Visibility.Collapsed;

                    TelephoneLabel.Visibility = Visibility.Visible;
                    TelephoneTextBox.Visibility = Visibility.Visible;


                    SwitchEstablishTimeLabel.Visibility = Visibility.Visible;
                    SwitchEstablishTimeTextBox.Visibility = Visibility.Visible;

                    OldCountorInfo.Visibility = Visibility.Collapsed;

                    _switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);



                    _cutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_request.ID);
                    if (_cutAndEstablish.Status == (int)DB.RequestType.CutAndEstablish)
                    {

                        NewTelphoneCounter.TelephoneNo = _request.TelephoneNo ?? 0;
                        NewCountorInfo.Header = "قطع تلفن";

                        CauseOfCutLabel.Visibility = Visibility.Visible;
                        CauseOfCutTextBox.Visibility = Visibility.Visible;

                        CauseOfCutTextBox.Text = Data.CauseOfCutDB.GetCauseOfCutID(_cutAndEstablish.CutType ?? 0).Name;
                    }
                    else if (_cutAndEstablish.Status == (int)DB.RequestType.Connect)
                    {
                        NewTelphoneCounter.TelephoneNo = 0;
                        NewCountorInfo.Visibility = Visibility.Collapsed;
                        CounterGroupBox.Header = "وصل تلفن";
                    }

                    DetailGroupBox.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    DetailGroupBox.Content = _ChangeLocationMDFWirig;
                    DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    break;
                default:
                    MessageBox.Show("روال یافت نشد");
                    break;
            }

            DayeriResultComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            DayeriResultComboBox.SelectedValue = _request.StatusID;




            if (_counter != null)
                NewTelphoneCounter.CountorID = _counter.ID;
            else
                NewTelphoneCounter.CountorID = 0;

            if (_counter != null && _counter.ID != 0)
            {
                _conterID = _counter.ID;
                _instLine = Data.instLineDB.GetinstLineByCounterID(_conterID);
                SwitchEstablishDatePicker.SelectedDate = _instLine.SwitchEstablishDate;
                SwitchEstablishTimeTextBox.Text = _instLine.SwitchEstablishTime;
            }
            else
            {

            }

            _wiring.RequestID = _request.ID;
            CounterInfo.DataContext = new { Wiring = _issueWiring, SwitchCode = _switchCode };
            _instLine.WiringID = _wiring.ID;

            this.DataContext = this;
            if (_conterID != 0)
            {

                SwitchEstablishDatePicker.SelectedDate = _instLine.SwitchEstablishDate;
            }
            else
            {
            }


            DateTime dateTime = DB.GetServerDate();
            SwitchEstablishDatePicker.SelectedDate = dateTime.Date;
            SwitchEstablishTimeTextBox.Text = dateTime.ToShortTimeString();




        }

        private void ContainerStatusChanged(object sender, EventArgs e)
        {
            if (_specialServiceUserControl.SpecialServiceListView.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            {


                foreach (CheckableItem checkableItem in _specialServiceUserControl.SpecialServiceListView.Items)
                {
                    ListViewItem lvitem = _specialServiceUserControl.SpecialServiceListView.ItemContainerGenerator.ContainerFromItem(checkableItem) as ListViewItem;
                    if (UninstallSpecialServiceSequenceIDs.Ids.Contains(checkableItem.ID))
                    {
                        lvitem.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF9696"));
                    }

                    if (InstallSpecialServiceSequenceIDs.Ids.Contains(checkableItem.ID))
                    {
                        lvitem.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#96FF96"));
                    }


                }
            }
        }

        private void SaveLogDischargeOldTelephone(Telephone oldtele, DB.RequestType requestType, DB.LogType logType, long OldCabinetInputID, long OldPostContactID, long buchtID, int switchportID, long installAddressID, long correspondenceAddressID)
        {
            RequestLog requestLog = new RequestLog();
            requestLog.RequestID = _request.ID;
            requestLog.RequestTypeID = (int)requestType;
            requestLog.LogType = (byte)logType;
            requestLog.IsReject = false;
            requestLog.TelephoneNo = _request.TelephoneNo;
            requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_request.CustomerID);

            CRM.Data.Schema.DischargeTelephone dischargeTelephone = new Data.Schema.DischargeTelephone();



            dischargeTelephone.BuchtID = buchtID;
            dischargeTelephone.TelephoneNo = oldtele.TelephoneNo;
            dischargeTelephone.PortID = switchportID;
            dischargeTelephone.CustomerID = _request.CustomerID ?? 0;
            dischargeTelephone.InstallAddressID = installAddressID;
            dischargeTelephone.CorrespondenceAddressID = correspondenceAddressID;
            dischargeTelephone.PostContactID = OldPostContactID;
            dischargeTelephone.CabinetInputID = OldCabinetInputID;
            dischargeTelephone.CenterID = _request.CenterID;



            requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DischargeTelephone>(dischargeTelephone, true));

            requestLog.Date = DB.GetServerDate();
            requestLog.Detach();
            DB.Save(requestLog);
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Telephone tele = new Telephone();
                    DateTime currentServerDate = DB.GetServerDate();

                    _counter = NewTelphoneCounter.DataContext as Counter;
                    _counter.ID = 0;
                    _counter.InsertDate = currentServerDate;
                    _instLine.SwitchEstablishDate = SwitchEstablishDatePicker.SelectedDate ?? DB.GetServerDate();
                    _instLine.SwitchEstablishTime = SwitchEstablishTimeTextBox.Text;
                    _instLine.Status = (int)DayeriResultComboBox.SelectedValue;
                    _request.StatusID = (int)DayeriResultComboBox.SelectedValue;

                    _instLine.WiringID = _wiring.ID;


                    RequestLog requestLog = new RequestLog();
                    switch (_request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.Dayri:
                        case (byte)DB.RequestType.Reinstall:
                            tele = Data.TelephoneDB.GetTelephoneByTelephoneNo(_request.TelephoneNo ?? 0);
                            tele.Status = (byte)DB.TelephoneStatus.Connecting;
                            break;

                        case (byte)DB.RequestType.ChangeNo:

                            //ChangeNo changeNo = ChangeNoDB.GetChangeNoDBByID(_request.ID);
                            //changeNo.ChangeDate = currentServerDate;
                            //changeNo.Detach();
                            //DB.Save(changeNo);

                            // Telephone oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo(_changeNo.OldTelephoneNo);

                            //tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_changeNo.NewTelephoneNo);
                            //tele.InstallationDate = currentServerDate;
                            //tele.Status = (byte)DB.TelephoneStatus.Connecting;
                            //tele.DischargeDate = null;

                            //if (oldtele.Status == (int)DB.TelephoneStatus.Cut)
                            //{
                            //    tele.Status = oldtele.Status;
                            //    tele.CauseOfCutID = oldtele.CauseOfCutID;
                            //    tele.CutDate = oldtele.CutDate;
                            //    tele.ConnectDate = oldtele.ConnectDate;
                            //}

                            //tele.CustomerTypeID = oldtele.CustomerTypeID;
                            //tele.CustomerGroupID = oldtele.CustomerGroupID;
                            //tele.ChargingType = oldtele.ChargingType;
                            //tele.PosessionType = oldtele.PosessionType;

                            //oldtele.Status = (byte)DB.TelephoneStatus.Discharge;
                            //oldtele.DischargeDate = currentServerDate;
                            //oldtele.CustomerTypeID = null;
                            //oldtele.CustomerGroupID = null;
                            //oldtele.ChargingType = null;
                            //oldtele.PosessionType = null;
                            //oldtele.CauseOfCutID = null;
                            //oldtele.Detach();
                            //DB.Save(oldtele);

                            _oldCounter = OldTelphoneCounter.DataContext as Counter;
                            _oldCounter.TelephoneNo = (long)_changeNo.OldTelephoneNo;
                            _oldCounter.ID = 0;
                            _oldCounter.InsertDate = DB.GetServerDate();
                            this._oldCounter.Detach();
                            DB.Save(this._oldCounter);


                            break;

                        case (byte)DB.RequestType.Dischargin:

                            _counter.TelephoneNo = (long)_request.TelephoneNo;
                            _counter.ID = 0;
                            _counter.InsertDate = DB.GetServerDate();
                            this._counter.Detach();
                            DB.Save(this._counter);
                            break;

                        case (byte)DB.RequestType.CutAndEstablish:

                            tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                            tele.Status = (byte)DB.TelephoneStatus.Cut;
                            tele.CutDate = currentServerDate;
                            tele.CauseOfCutID = _cutAndEstablish.CutType;
                            tele.ConnectDate = null;

                            _cutAndEstablish.CutDate = SwitchEstablishDatePicker.SelectedDate;
                            _cutAndEstablish.InsertDate = currentServerDate;
                            _cutAndEstablish.Detach();
                            DB.Save(_cutAndEstablish, false);

                            break;

                        case (byte)DB.RequestType.Connect:

                            tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                            tele.Status = (byte)DB.TelephoneStatus.Connecting;
                            tele.ConnectDate = currentServerDate;
                            tele.CauseOfCutID = null;

                            _cutAndEstablish.EstablishDate = SwitchEstablishDatePicker.SelectedDate;
                            _cutAndEstablish.InsertDate = currentServerDate;

                            _cutAndEstablish.Detach();
                            DB.Save(_cutAndEstablish, false);

                            break;

                        case (byte)DB.RequestType.RefundDeposit:

                            // telephon discharg in forward method

                            //tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_refundDeposit.TelephoneNo);
                            //tele.Status = (int)DB.TelephoneStatus.Discharge;
                            //tele.DischargeDate = currentServerDate;
                            //tele.Detach();
                            //DB.Save(tele);

                            //SaveLogDischargeOldTelephone(tele, DB.RequestType.RefundDeposit, DB.LogType.OldTelephoneDischarge, _refundDeposit.CabinetInputID ?? 0, _refundDeposit.PostContactID ?? 0, _refundDeposit.BuchtID ?? 0, _refundDeposit.SwitchPortID ?? 0, tele.InstallAddressID ?? 0, tele.CorrespondenceAddressID ?? 0);

                            break;

                        case (byte)DB.RequestType.ChangeLocationCenterInside:

                            // به متد فوروارد انتقال داده شد
                            //tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);

                            //Telephone Oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                            //// if telephon is cut status , must move to new telephone their status

                            //if (Oldtele.Status == (int)DB.TelephoneStatus.Cut)
                            //{
                            //    tele.Status = (int)DB.TelephoneStatus.Cut;
                            //    tele.CauseOfCutID = Oldtele.CauseOfCutID;
                            //    tele.CutDate = Oldtele.CutDate;
                            //    tele.ConnectDate = Oldtele.ConnectDate;
                            //}

                            //Oldtele.Status = (byte)DB.TelephoneStatus.Discharge;
                            //Oldtele.Detach();
                            //DB.Save(Oldtele);



                            // transfor telephone feature
                            //Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeNo, DB.LogType.TransforFeature, (long)changeLocation.OldTelephone, (long)changeLocation.NewTelephone, false);

                            //SaveLogDischargeOldTelephone(tele, DB.RequestType.ChangeLocationCenterInside, DB.LogType.OldTelephoneDischarge, changeLocation.OldCabinetInputID ?? 0, changeLocation.OldPostContactID ?? 0, changeLocation.OldBuchtID ?? 0, changeLocation.OldSwitchPortID ?? 0, changeLocation.OldInstallAddressID ?? 0, changeLocation.OldCorrespondenceAddressID ?? 0);

                            _oldCounter = OldTelphoneCounter.DataContext as Counter;
                            _oldCounter.TelephoneNo = (long)changeLocation.OldTelephone;
                            _oldCounter.ID = 0;
                            _oldCounter.InsertDate = DB.GetServerDate();

                            this._oldCounter.Detach();
                            DB.Save(this._oldCounter);


                            //changeLocation.Detach();
                            //DB.Save(changeLocation);
                            _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
                            break;


                        case (byte)DB.RequestType.ChangeLocationCenterToCenter:

                            //// اگر درخواست در مبدا است
                            if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
                            {

                                //    //tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                                //    //tele.Status = (byte)DB.TelephoneStatus.Discharge;
                                //    //tele.CustomerID = null;
                                //    //tele.InstallAddressID = null;
                                //    //tele.CorrespondenceAddressID = null;

                                //    SaveLogDischargeOldTelephone(tele, DB.RequestType.ChangeLocationCenterToCenter, DB.LogType.OldTelephoneDischarge, changeLocation.OldCabinetInputID ?? 0, changeLocation.OldPostContactID ?? 0, changeLocation.OldBuchtID ?? 0, changeLocation.OldSwitchPortID ?? 0, changeLocation.OldInstallAddressID ?? 0, changeLocation.OldCorrespondenceAddressID ?? 0);

                                _counter = OldTelphoneCounter.DataContext as Counter;
                                _counter.TelephoneNo = (long)changeLocation.OldTelephone;
                                _counter.ID = 0;
                                _counter.InsertDate = DB.GetServerDate();
                                _counter.Detach();
                                DB.Save(_counter);

                                _request.StatusID = (int)DayeriResultComboBox.SelectedValue;

                                changeLocation.OldCounterID = _counter.ID;
                                changeLocation.Detach();
                                DB.Save(changeLocation);



                            }
                            // اگر درخواست در مقصد
                            else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
                            {
                                //   // tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                                //    //Oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                                //    //if (Oldtele.CutDate.HasValue && !Oldtele.ConnectDate.HasValue)
                                //    //{
                                //    //    tele.Status = (int)DB.TelephoneStatus.Cut;
                                //    //    tele.CauseOfCutID = Oldtele.CauseOfCutID;
                                //    //    tele.CutDate = Oldtele.CutDate;
                                //    //    tele.ConnectDate = Oldtele.ConnectDate;
                                //    //}

                                //    //Oldtele.Status = (byte)DB.TelephoneStatus.Discharge;
                                //    //Oldtele.Detach();
                                //    //DB.Save(Oldtele);

                                //    // transfor telephone feature
                                //    Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeNo, DB.LogType.TransforFeature, (long)changeLocation.OldTelephone, (long)changeLocation.NewTelephone, false);

                                _counter = NewTelphoneCounter.DataContext as Counter;
                                _counter.TelephoneNo = (long)changeLocation.NewTelephone;
                                _counter.ID = 0;
                                _counter.InsertDate = DB.GetServerDate();
                                _counter.Detach();
                                DB.Save(_counter);


                                _request.StatusID = (int)DayeriResultComboBox.SelectedValue;

                                changeLocation.NewCounterID = _counter.ID;
                                changeLocation.Detach();
                                DB.Save(changeLocation);

                            }
                            break;
                        case (byte)DB.RequestType.PBX:
                            {
                                _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
                                tele = null;
                                break;
                            }
                        case (byte)DB.RequestType.SpecialService:
                            {

                                List<CheckableItem> currentspecialServiceTypes = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckableForTelephone(_request.TelephoneNo ?? 0).Where(t => t.IsChecked == true).ToList();

                                List<CheckableItem> selectedListSpecialServiceTypes = (_specialServiceUserControl.SpecialServiceListView.ItemsSource as List<CheckableItem>).Where(t => t.IsChecked == true).ToList(); ;

                                // Get install list
                                List<CheckableItem> InstallSpecialServices = selectedListSpecialServiceTypes.Where(t => !currentspecialServiceTypes.Select(st => st.ID).Contains(t.ID)).ToList(); // selectedListSpecialServiceTypes.Except(currentspecialServiceTypes).ToList();

                                // Get Uninstall list
                                List<CheckableItem> UnistallSpecialServices = currentspecialServiceTypes.Where(t => !selectedListSpecialServiceTypes.Select(st => st.ID).Contains(t.ID)).ToList();  //currentspecialServiceTypes.Except(selectedListSpecialServiceTypes).ToList();

                                List<CRM.Data.TelephoneSpecialServiceType> installTelephoneSpecialServiceType = new List<TelephoneSpecialServiceType>();

                                // create install list
                                InstallSpecialServices.ForEach(item =>
                                                                        {
                                                                            CRM.Data.TelephoneSpecialServiceType telephoneSpecialServiceType = new TelephoneSpecialServiceType();
                                                                            telephoneSpecialServiceType.SpecialServiceTypeID = item.ID;
                                                                            telephoneSpecialServiceType.TelephoneNo = (long)_request.TelephoneNo;
                                                                            telephoneSpecialServiceType.Detach();
                                                                            installTelephoneSpecialServiceType.Add(telephoneSpecialServiceType);
                                                                        }
                                                               );

                                // Get UnInstall List For Log
                                List<int> UnInstallListForLog = Data.TelephoneSpecialServiceTypeDB.GetTelephoneSpecialServiceTypeDB((long)_request.TelephoneNo, UnistallSpecialServices.Select(t => t.ID).ToList()).Select(t => t.SpecialServiceTypeID).ToList();

                                // Unstallinstall list
                                Data.TelephoneSpecialServiceTypeDB.DeleteTelephoneSpecialServiceType((long)_request.TelephoneNo, UnistallSpecialServices.Select(t => t.ID).ToList());


                                DB.SaveAll(installTelephoneSpecialServiceType);

                                _request.Detach();
                                DB.Save(_request, false);

                                _specialService.InsertDate = currentServerDate;
                                _specialService.InstallDate = SwitchEstablishDatePicker.SelectedDate;
                                _specialService.InstallHour = SwitchEstablishTimeTextBox.Text.Trim();
                                _specialService.IsApplied = true;
                                _specialService.Detach();
                                DB.Save(_specialService, false);

                                // اگر قبلا لاگ نخورده باشد لاگ می خورد
                                if (Data.RequestLogDB.ExistReqeustLog(_request.ID, _request.RequestTypeID, (long)_request.TelephoneNo))
                                {
                                    // save log
                                    RequestLog _RequestLog = new RequestLog();
                                    _RequestLog = new RequestLog();
                                    _RequestLog.RequestID = _request.ID;
                                    _RequestLog.RequestTypeID = _request.RequestTypeID;
                                    _RequestLog.TelephoneNo = _request.TelephoneNo;
                                    _RequestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_request.CustomerID);

                                    CRM.Data.Schema.SpecialService SpecialServiceLog = new Data.Schema.SpecialService();
                                    SpecialServiceLog.InstallSpesiallService = installTelephoneSpecialServiceType.Select(t => t.SpecialServiceTypeID).ToList();
                                    SpecialServiceLog.UnInstallSpesiallService = UnInstallListForLog;
                                    _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.SpecialService>(SpecialServiceLog, true));

                                    _RequestLog.Date = DB.GetServerDate();
                                    _RequestLog.Detach();
                                    DB.Save(_RequestLog, true);
                                }


                                ShowSuccessMessage("ذخیره انجام شد");

                                ts.Complete();
                                IsSaveSuccess = true;
                                return true;
                            }
                            break;
                        case (byte)DB.RequestType.OpenAndCloseZero:
                            {
                                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);

                                telephone.ClassTelephone = (byte)_openAndCloseZeroUserControl.ZeroBlockComboBox.SelectedValue;

                                telephone.Detach();
                                DB.Save(telephone);

                                _request.Detach();
                                DB.Save(_request, false);


                                _zeroStatus.InstallDate = (DateTime)SwitchEstablishDatePicker.SelectedDate;
                                _zeroStatus.InstallHour = SwitchEstablishTimeTextBox.Text.Trim();
                                _zeroStatus.InsertDate = currentServerDate;
                                _zeroStatus.Detach();
                                DB.Save(_zeroStatus, false);


                                //// اگر قبلا لاگ نخورده باشد لاگ می خورد
                                //if (Data.RequestLogDB.ExistReqeustLog(_request.ID, _request.RequestTypeID,(long) _request.TelephoneNo))
                                //{
                                //    // save log
                                //    RequestLog _RequestLog = new RequestLog();
                                //    _RequestLog = new RequestLog();
                                //    _RequestLog.RequestID = _request.ID;
                                //    _RequestLog.RequestTypeID = _request.RequestTypeID;
                                //    _RequestLog.TelephoneNo = _request.TelephoneNo;
                                //    _RequestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_request.CustomerID);

                                //    CRM.Data.Schema.OpenAndCloseZero OpenAndCloseZeroLog = new Data.Schema.OpenAndCloseZero();
                                //    OpenAndCloseZeroLog.ClassTelephone = (byte)_openAndCloseZeroUserControl.ZeroBlockComboBox.SelectedValue;
                                //    _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.OpenAndCloseZero>(OpenAndCloseZeroLog, true));

                                //    _RequestLog.Date = DB.GetServerDate();
                                //    _RequestLog.Detach();
                                //    DB.Save(_RequestLog, true);
                                //}
                                ShowSuccessMessage("ذخیره انجام شد");


                                ts.Complete();
                                IsSaveSuccess = true;
                                return true;
                            }
                            break;



                        default:
                            MessageBox.Show("روال مورد نظر یافت نشد");
                            break;

                    }

                    InstallLineDB.SaveInstallLine(_request, _counter, _instLine, tele);

                    ts.Complete();
                    ShowSuccessMessage("ذخیره انجام شد");

                    IsSaveSuccess = true;
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }

            base.Confirm();

            return IsSaveSuccess;
        }




        public override bool Deny()
        {

            try
            {
                base.RequestID = _request.ID;
                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                        DayriDelete();
                        break;
                    case (byte)DB.RequestType.Dischargin:
                        DischarginDelete();
                        break;
                    case (byte)DB.RequestType.ChangeNo:
                        ChangeNoDelete();
                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                        changeLocationCenterInsideDelete();
                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationCenterToCenterDelete();
                        break;
                    case (byte)DB.RequestType.CutAndEstablish:
                        CutAndEstablishDelete();
                        break;
                    case (byte)DB.RequestType.SpecialService:
                        SpecialServiceDelete();
                        break;
                    case (byte)DB.RequestType.OpenAndCloseZero:
                        OpenAndZeroDelete();
                        break;
                    case (byte)DB.RequestType.RefundDeposit:
                        RefundDepositDelete();
                        break;
                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد دخواست", ex);
            }

            return IsRejectSuccess;

        }

        private void RefundDepositDelete()
        {

            //Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_refundDeposit.TelephoneNo);
            //if (tele.Status == (byte)DB.TelephoneStatus.Discharge)
            //{
            //    tele.Status = (byte)DB.TelephoneStatus.Connecting;
            //    tele.CustomerID = _takePossession.CustomerID;
            //    tele.InstallAddressID = _takePossession.InstallAddressID;
            //    tele.CorrespondenceAddressID = _takePossession.CorrespondenceAddressID;
            //    tele.DischargeDate = null;
            //    tele.Detach();
            //    DB.Save(tele);
            //}

            _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
            _request.Detach();
            DB.Save(_request);
        }

        private void OpenAndZeroDelete()
        {
            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
            ZeroStatus zeroStatus = Data.ZeroStatusDB.GetZeroStatusByID((long)_request.ID);

            telephone.ClassTelephone = zeroStatus.OldClassTelephone;

            telephone.Detach();
            DB.Save(telephone);

            _request.Detach();
            DB.Save(_request, false);
        }
        private void SpecialServiceDelete()
        {
            CRM.Data.SpecialService specialService = Data.SpecialServiceDB.GetSpecialServiceByID(_request.ID);
            if (_specialService.IsApplied == true)
            {
                CRM.Data.Schema.SequenceIDs InstallSpecialServiceSequenceIDs = LogSchemaUtility.Deserialize<CRM.Data.Schema.SequenceIDs>(specialService.InstallSpecialService.ToString());

                CRM.Data.Schema.SequenceIDs UninstallSpecialServiceSequenceIDs = LogSchemaUtility.Deserialize<CRM.Data.Schema.SequenceIDs>(specialService.UninstallSpecialService.ToString());

                // حذف سرویس ویژهای نصب شده
                Data.TelephoneSpecialServiceTypeDB.DeleteTelephoneSpecialServiceType((long)_request.TelephoneNo, InstallSpecialServiceSequenceIDs.Ids);


                // دایری سرویس ویژه های حذف شده
                List<CRM.Data.TelephoneSpecialServiceType> UninstallTelephoneSpecialServiceType = new List<TelephoneSpecialServiceType>();

                // create install list
                UninstallSpecialServiceSequenceIDs.Ids.ForEach(item =>
                {
                    CRM.Data.TelephoneSpecialServiceType telephoneSpecialServiceType = new TelephoneSpecialServiceType();
                    telephoneSpecialServiceType.SpecialServiceTypeID = item;
                    telephoneSpecialServiceType.TelephoneNo = (long)_request.TelephoneNo;
                    telephoneSpecialServiceType.Detach();
                    UninstallTelephoneSpecialServiceType.Add(telephoneSpecialServiceType);
                }
                                               );

                DB.SaveAll(UninstallTelephoneSpecialServiceType);



            }

        }
        private void CutAndEstablishDelete()
        {
            Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);

            if (_cutAndEstablish.Status == (int)DB.RequestType.CutAndEstablish)
            {
                if (tele.Status == (byte)DB.TelephoneStatus.Cut)
                {

                    tele.Status = (byte)DB.TelephoneStatus.Connecting;
                    tele.CauseOfCutID = null;

                }

            }
            else if (_cutAndEstablish.Status == (int)DB.RequestType.Connect)
            {

                if (tele.Status == (byte)DB.TelephoneStatus.Connecting)
                {
                    tele.Status = (byte)DB.TelephoneStatus.Cut;
                }
            }

            tele.Detach();
            DB.Save(tele);
        }

        private void changeLocationCenterInsideDelete()
        {
            Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.NewTelephone ?? 0);
            Telephone Oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
            RequestLog requestLog = Data.RequestLogDB.GetLastRequestLogByReqeusetID(_request.ID, (int)DB.LogType.TransforFeature);
            if (requestLog != null || requestLog.ID != 0)
            {


                Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeLocationCenterInside, DB.LogType.TransforFeature, (long)changeLocation.NewTelephone, (long)changeLocation.OldTelephone, true);
            }

            requestLog = null;
            requestLog = Data.RequestLogDB.GetLastRequestLogByReqeusetID(_request.ID, (int)DB.LogType.OldTelephoneDischarge);
            if (requestLog != null || requestLog.ID != 0)
            {

                Data.RequestLogDB.RejectSaveLogDischargeOldTelephone(_request, DB.RequestType.ChangeLocationCenterToCenter, DB.LogType.OldTelephoneDischarge);

            }

        }

        private void ChangeLocationCenterToCenterDelete()
        {
            // اگر درخواست در مبدا است
            if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
            {
                // release Telephone
                //Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                //if (tele.Status == (byte)DB.TelephoneStatus.Discharge)
                //{
                //    tele.SwitchPortID = changeLocation.OldSwitchPortID;



                //    tele.CustomerID = _request.CustomerID;
                //    tele.InstallAddressID = changeLocation.OldInstallAddressID;
                //    tele.CorrespondenceAddressID = changeLocation.OldCorrespondenceAddressID;
                //    tele.Detach();
                //    DB.Save(tele);
                RequestLog requestLog = Data.RequestLogDB.GetLastRequestLogByReqeusetID(_request.ID, (int)DB.LogType.OldTelephoneDischarge);
                if (requestLog != null || requestLog.ID != 0)
                {
                    Data.RequestLogDB.RejectSaveLogDischargeOldTelephone(_request, DB.RequestType.ChangeLocationCenterToCenter, DB.LogType.OldTelephoneDischarge);
                }


            }

            // اگر درخواست در مقصد
            else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
            {
                Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                Telephone oldTele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                //if (oldTele.Status == (byte)DB.TelephoneStatus.Discharge)
                //{
                //    if (oldTele.CutDate.HasValue && !oldTele.ConnectDate.HasValue)
                //    {
                //        tele.Status = (byte)DB.TelephoneStatus.Cut;
                //        oldTele.CauseOfCutID = tele.CauseOfCutID;

                //    }
                //    else
                //    {
                //        tele.Status = (byte)DB.TelephoneStatus.Connecting;
                //    }
                //}

                //if (tele.Status == (byte)DB.TelephoneStatus.Connecting)
                //{
                //    tele.Status = (byte)DB.TelephoneStatus.Reserv;
                RequestLog requestLog = Data.RequestLogDB.GetLastRequestLogByReqeusetID(_request.ID, (int)DB.LogType.TransforFeature);
                if (requestLog != null || requestLog.ID != 0)
                {
                    Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeLocationCenterToCenter, DB.LogType.TransforFeature, (long)changeLocation.NewTelephone, (long)changeLocation.OldTelephone, true);
                }
                //else if (tele.Status == (byte)DB.TelephoneStatus.Cut)
                //{
                //    tele.Status = (byte)DB.TelephoneStatus.Reserv;
                //    tele.CutDate = null;
                //    tele.CauseOfCutID = null;
                //    Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeLocationCenterToCenter, DB.LogType.TransforFeature, (long)changeLocation.NewTelephone, (long)changeLocation.OldTelephone, true);

                //}
                tele.Detach();
                DB.Save(tele);

                oldTele.Detach();
                DB.Save(oldTele);

            }
        }


        private void ChangeNoDelete()
        {
            //using (TransactionScope parentTs = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{

            //    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_changeNo.NewTelephoneNo);
            //    Telephone oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo(_changeNo.OldTelephoneNo);

            //    if (oldtele.Status == (byte)DB.TelephoneStatus.Discharge)
            //    {

            //        if(oldtele.CutDate.HasValue && !oldtele.ConnectDate.HasValue)
            //        {
            //            oldtele.Status = (byte)DB.TelephoneStatus.Cut;
            //            oldtele.CauseOfCutID = tele.CauseOfCutID;
            //        }
            //        else
            //        {
            //            oldtele.Status = (byte)DB.TelephoneStatus.Connecting;
            //        }

            //        oldtele.CustomerTypeID = tele.CustomerTypeID;
            //        oldtele.CustomerGroupID = tele.CustomerGroupID;
            //        oldtele.Detach();
            //        DB.Save(oldtele);


            //        if (tele != null && tele.Status == (byte)DB.TelephoneStatus.Connecting)
            //        {
            //            tele.Status = (byte)DB.TelephoneStatus.Reserv;
            //            tele.InstallationDate = null;
            //            tele.Detach();
            //            DB.Save(tele);
            //        }
            //        else if (tele.Status == (byte)DB.TelephoneStatus.Reserv)
            //        {
            //            tele.InstallationDate = null;
            //            tele.Detach();
            //            DB.Save(tele);
            //        }
            //        else if (tele.Status == (byte)DB.TelephoneStatus.Cut)
            //        {
            //            tele.Status = (byte)DB.TelephoneStatus.Reserv;
            //            tele.InstallationDate = null;
            //            tele.CutDate = null;
            //            tele.CauseOfCutID = null;
            //            tele.CustomerTypeID = null;
            //            tele.CustomerGroupID = null;
            //            tele.Detach();
            //            DB.Save(tele);
            //        }

            //      //  Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeNo, DB.LogType.TransforFeature, (long)_changeNo.NewTelephoneNo, (long)_changeNo.OldTelephoneNo, true);
            //        Data.RequestLogDB.RejectSaveLogDischargeOldTelephone(_request, DB.RequestType.ChangeNo, DB.LogType.OldTelephoneDischarge);

            //    }


            //    parentTs.Complete();
            //}
        }
        private void DischarginDelete()
        {


            //Telephone tele = new Telephone();
            //tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_takePossession.OldTelephone);
            //if (tele.Status == (byte)DB.TelephoneStatus.Discharge)
            //{
            //    tele.Status = (byte)DB.TelephoneStatus.Connecting;
            //    tele.CustomerID = _takePossession.CustomerID;
            //    tele.InstallAddressID = _takePossession.InstallAddressID;
            //    tele.CorrespondenceAddressID = _takePossession.CorrespondenceAddressID;
            //    tele.DischargeDate = null;
            //    tele.Detach();
            //    DB.Save(tele);
            //}
            _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
            _request.Detach();
            DB.Save(_request);

        }

        private void DayriDelete()
        {
            _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
            _request.Detach();
            DB.Save(_request);

            /// Discharge Telephone
            Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_SelectTelephone.TelephoneNo);
            if (tele != null)
            {
                tele.Status = (byte)DB.TelephoneStatus.Reserv;
                tele.Detach();
                DB.Save(tele);
            }

            /// Discharge Conter && InstallLine
            //_counter = Data.CounterDB.GetCounterByInvestigatePossibilityID(_investigateInfoSummary.investigate.ID);

            //if (_counter != null)
            //{

            //    _instLine = Data.instLineDB.GetinstLineByCounterID(_conterID);
            //    DB.Delete<Data.InstallLine>(_instLine.ID);
            //    DB.Delete<Data.Counter>(_counter.ID);

            //}
        }

        public override bool Forward()
        {

            try
            {
                DateTime currentServerDate = DB.GetServerDate();
                IsForwardSuccess = false;
                Save();

                if (IsSaveSuccess == true)
                {
                    Status Status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);
                    Telephone tele = new Telephone();

                    switch (_request.RequestTypeID)
                    {

                        case (byte)DB.RequestType.Connect:
                            {

                                tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                                tele.CauseOfCutID = null;
                                tele.Detach();
                                DB.Save(tele);
                                break;
                            }
                        case (byte)DB.RequestType.Dischargin:
                            {
                                tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                                tele.CauseOfTakePossessionID = _takePossession.CauseOfTakePossessionID;

                                // اگر تلفن جی اس ام است شماره سریال سیم کارت آن ذخیره می شود
                                if (tele.UsageType == (int)DB.TelephoneUsageType.GSM)
                                {
                                    GSMSimCard GSMSimCardItem = GSMSimCardDB.GetGSMSimCardByTelephone(tele.TelephoneNo);
                                    if (GSMSimCardItem != null && GSMSimCardItem.TelephoneNo != 0)
                                    {
                                        GSMSimCardItem.Code = string.Empty;
                                        GSMSimCardItem.Detach();
                                        DB.Save(GSMSimCardItem, false);
                                    }
                                }

                                //چنانچه علت درخواست تخلیه ، جمع آوری منصوبه باشد آنگاه وضعیت تلفن باید با "جمع آوری منصوبات" مقداردهی شود
                                if (_takePossession.CauseOfTakePossessionID == (int)DB.CauseOfTakePossession.CollectingEquipment)
                                {
                                    tele.Status = (int)DB.TelephoneStatus.CollectingEquipment;
                                }
                                else
                                {
                                    tele.Status = (byte)DB.TelephoneStatus.Discharge;
                                }
                                tele.DischargeDate = currentServerDate;
                                tele.InitialDischargeDate = currentServerDate;
                                tele.CustomerID = null;
                                tele.InstallAddressID = null;
                                tele.CorrespondenceAddressID = null;
                                tele.CustomerTypeID = null;
                                tele.CustomerGroupID = null;
                                tele.PosessionType = null;
                                tele.ChargingType = null;
                                tele.Detach();
                                DB.Save(tele);


                                _takePossession.TakePossessionDate = currentServerDate;
                                _takePossession.Detach();
                                DB.Save(_takePossession);

                                break;

                            }
                        case (byte)DB.RequestType.RefundDeposit:
                            {
                                tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                                tele.Status = (byte)DB.TelephoneStatus.Discharge;
                                tele.CauseOfTakePossessionID = _takePossession.CauseOfTakePossessionID;
                                tele.DischargeDate = currentServerDate;
                                tele.InitialDischargeDate = currentServerDate;
                                tele.CustomerID = null;
                                tele.InstallAddressID = null;
                                tele.CorrespondenceAddressID = null;
                                tele.CustomerTypeID = null;
                                tele.CustomerGroupID = null;
                                tele.PosessionType = null;
                                tele.ChargingType = null;
                                tele.Detach();
                                DB.Save(tele);


                                _takePossession.TakePossessionDate = currentServerDate;
                                _takePossession.Detach();
                                DB.Save(_takePossession);
                                break;
                            }

                        case (byte)DB.RequestType.ChangeLocationCenterInside:
                            {
                                tele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);

                                Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeNo, DB.LogType.TransforFeature, (long)changeLocation.OldTelephone, (long)changeLocation.NewTelephone, false);

                                SaveLogDischargeOldTelephone(tele, DB.RequestType.ChangeLocationCenterInside, DB.LogType.OldTelephoneDischarge, changeLocation.OldCabinetInputID ?? 0, changeLocation.OldPostContactID ?? 0, changeLocation.OldBuchtID ?? 0, changeLocation.OldSwitchPortID ?? 0, changeLocation.OldInstallAddressID ?? 0, changeLocation.OldCorrespondenceAddressID ?? 0);

                                changeLocation.Detach();
                                DB.Save(changeLocation);
                                _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
                                break;
                            }
                        case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                            {

                                if (Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                                {
                                    _request.CenterID = (int)changeLocation.TargetCenter;
                                }
                                // اگر درخواست در مبدا است
                                if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
                                {

                                    SaveLogDischargeOldTelephone(tele, DB.RequestType.ChangeLocationCenterToCenter, DB.LogType.OldTelephoneDischarge, changeLocation.OldCabinetInputID ?? 0, changeLocation.OldPostContactID ?? 0, changeLocation.OldBuchtID ?? 0, changeLocation.OldSwitchPortID ?? 0, changeLocation.OldInstallAddressID ?? 0, changeLocation.OldCorrespondenceAddressID ?? 0);

                                    _request.StatusID = (int)DayeriResultComboBox.SelectedValue;
                                }
                                // اگر درخواست در مقصد
                                else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
                                {

                                    // transfor telephone feature
                                    Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeNo, DB.LogType.TransforFeature, (long)changeLocation.OldTelephone, (long)changeLocation.NewTelephone, false);

                                    _request.StatusID = (int)DayeriResultComboBox.SelectedValue;

                                }
                                _request.Detach();
                                DB.Save(_request);
                                break;
                            }

                        case (byte)DB.RequestType.ChangeNo:
                            {
                                ChangeNo changeNo = ChangeNoDB.GetChangeNoDBByID(_request.ID);
                                changeNo.ChangeDate = currentServerDate;
                                changeNo.Detach();
                                DB.Save(changeNo);


                                Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_changeNo.NewTelephoneNo);
                                Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_changeNo.OldTelephoneNo);

                                // Transfer telephone feature
                                Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(_request, DB.RequestType.ChangeNo, DB.LogType.TransforFeature, _changeNo.OldTelephoneNo, (long)_changeNo.NewTelephoneNo, false);

                                AssignmentInfo assingnmentInfo = DB.GetAllInformationByTelephoneNo((long)_changeNo.NewTelephoneNo);

                                SaveLogDischargeOldTelephone(oldTelephone, DB.RequestType.ChangeNo, DB.LogType.OldTelephoneDischarge, _changeNo.OldCabinetInputID ?? 0, _changeNo.OldPostContactID ?? 0, assingnmentInfo.BuchtID ?? 0, assingnmentInfo.SwitchPortID ?? 0, oldTelephone.InstallAddressID ?? 0, oldTelephone.CorrespondenceAddressID ?? 0);

                                newTelephone.CustomerID = oldTelephone.CustomerID;
                                newTelephone.InstallationDate = currentServerDate;
                                newTelephone.InitialInstallationDate = oldTelephone.InitialInstallationDate;
                                newTelephone.DischargeDate = null;
                                newTelephone.CauseOfTakePossessionID = null; 
                                newTelephone.Status = oldTelephone.Status;
                                newTelephone.CauseOfCutID = oldTelephone.CauseOfCutID;
                                newTelephone.CustomerTypeID = oldTelephone.CustomerTypeID;
                                newTelephone.CustomerGroupID = oldTelephone.CustomerGroupID;
                                newTelephone.ChargingType = oldTelephone.ChargingType;
                                newTelephone.PosessionType = oldTelephone.PosessionType;
                                newTelephone.CutDate = oldTelephone.CutDate;
                                newTelephone.InstallAddressID = oldTelephone.InstallAddressID;
                                newTelephone.CorrespondenceAddressID = oldTelephone.CorrespondenceAddressID;
                                newTelephone.ClassTelephone = oldTelephone.ClassTelephone;
                                newTelephone.Detach();
                                DB.Save(newTelephone);

                                oldTelephone.DischargeDate = currentServerDate;
                                oldTelephone.Status = (int)DB.TelephoneStatus.Discharge;
                                oldTelephone.CustomerTypeID = null;
                                oldTelephone.InitialInstallationDate = null;
                                oldTelephone.CustomerGroupID = null;
                                oldTelephone.ChargingType = null;
                                oldTelephone.PosessionType = null;
                                oldTelephone.CauseOfCutID = null;
                                oldTelephone.InstallAddressID = null;
                                oldTelephone.CorrespondenceAddressID = null;
                                oldTelephone.CustomerID = null;
                                oldTelephone.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;
                                oldTelephone.Detach();
                                DB.Save(oldTelephone);

                                break;

                            }
                    }

                    IsForwardSuccess = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع عملیات", ex);
            }
            return IsForwardSuccess;
        }

        #endregion

    }
}





