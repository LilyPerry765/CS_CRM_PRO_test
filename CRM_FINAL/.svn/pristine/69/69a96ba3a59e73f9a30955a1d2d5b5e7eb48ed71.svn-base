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
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using System.Xml.Linq;
using System.Collections;
using Stimulsoft.Report.Dictionary;
using System.Net;
using System.IO;
using CRM.WebAPI.Models.Shahkar.CustomClasses;
using System.Web.Script.Serialization;
using CRM.Application.Codes;
using CRM.Data.ShahkarBussines.Methods;

namespace CRM.Application.Views
{


    public partial class WiringForm : Local.RequestFormBase
    {


        #region fildes && properties
        public Request _request { get; set; }
        InvestigatePossibility _InvestigatePossibility { get; set; }
        public CRM.Data.E1 _e1 { get; set; }
        public CRM.Data.SpecialWire _specialWire { get; set; }

        public SelectTelephone _SelectTelephone = new SelectTelephone();
        public CRM.Data.VacateSpecialWire _vacateSpecialWire { get; set; }
        public CRM.Data.ChangeLocationSpecialWire _changeLocationSpecialWire { get; set; }
        public CRM.Data.ChangeNo _changeNo { get; set; }

        public static List<Switch> switchList { get; set; }
        public static List<Telephone> telList { get; set; }
        public static List<BuchtInfo> buchtList { get; set; }
        public static SwitchCodeInfo switchCode { get; set; }
        SpecialCondition specialCondition { get; set; }
        public static Wiring wiring { get; set; }
        public static ChangeLocation changeLocation;
        List<CRM.Data.AssignmentInfo> assingmentInfo = new List<CRM.Data.AssignmentInfo>();
        public static TakePossession takePossession;
        public static RefundDeposit _RefundDeposit;
        Telephone oldTelephone = new Telephone();
        Bucht bucht = new Bucht();
        Bucht buchtReserve = new Bucht();
        Bucht OldBucht = new Bucht();
        PostContact _oldPostContact;
        PostContact _newPostContact;

        public UserControls.UserInfoSummary _userInfoSummary { get; set; }
        public UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        public UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        public UserControls.InstallInfoSummary _installInfoSummary { get; set; }
        public UserControls.InvestigateInfoSummary _investigateInfoSummary { get; set; }
        public UserControls.ChangeLocationMDFWirigUserControl _ChangeLocationMDFWirig { get; set; }
        public UserControls.CustomerAddressUserControl _CustomerAddressUserControl { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        private long? _subID;
        E1Link _e1Link { get; set; }

        #endregion

        #region Constructor
        public WiringForm()
        {
            InitializeComponent();
            Initialize();

        }

        public WiringForm(long id, long? subID)
            : this()
        {
            this._subID = subID;
            wiring = new Wiring();
            _request = Data.RequestDB.GetRequestByID(id);
            switchList = Data.SwitchDB.GetSwitchByCenterID(_request.CenterID);


            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.Mode = true;
            _customerInfoSummary.IsExpandedProperty = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;

            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.Dayri:
                case (byte)DB.RequestType.Reinstall:

                    _installInfoSummary = new InstallInfoSummary(_request.ID);
                    _installInfoSummary.InstallInfoExpander.IsExpanded = true;
                    InstallInfoUC.Content = _installInfoSummary;
                    InstallInfoUC.DataContext = _installInfoSummary;

                    _investigateInfoSummary = new InvestigateInfoSummary(_request.ID);
                    InvestigateInfoUC.Content = _investigateInfoSummary;
                    InvestigateInfoUC.DataContext = _investigateInfoSummary;

                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;

                    break;
                case (byte)DB.RequestType.ChangeLocationCenterInside:
                case (byte)DB.RequestType.ChangeLocationCenterToCenter:

                    _CustomerAddressUserControl = new CustomerAddressUserControl(_request.ID);
                    _CustomerAddressUserControl.AddressInfoExpander.IsExpanded = true;
                    CustomerAddressUserControlUC.Content = _CustomerAddressUserControl;
                    CustomerAddressUserControlUC.DataContext = _CustomerAddressUserControl;

                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;

                    break;
                case (byte)DB.RequestType.ChangeNo:

                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Visible;

                    _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Visible;

                    changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                    changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                    _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

                    changeLocationMDFWirig.Visibility = Visibility.Visible;

                    break;

                case (byte)DB.RequestType.Dischargin:

                    AirNetInfo.Visibility = Visibility.Visible;
                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    InstallInfoUC.Visibility = Visibility.Collapsed;


                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;

                    _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;

                    changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                    changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                    _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

                    changeLocationMDFWirig.Visibility = Visibility.Visible;

                    break;
                case (byte)DB.RequestType.RefundDeposit:
                    AirNetInfo.Visibility = Visibility.Visible;

                    //ChooseNoGroupBox.Visibility = Visibility.Collapsed;
                    InvestigateInfoUC.Visibility = Visibility.Collapsed;

                    InstallInfoUC.Visibility = Visibility.Collapsed;


                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;
                    changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                    changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                    _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

                    changeLocationMDFWirig.Visibility = Visibility.Visible;

                    InstallInfoUC.Visibility = Visibility.Collapsed;
                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    break;
                case (byte)DB.RequestType.E1:
                case (byte)DB.RequestType.E1Link:

                    _E1InfoSummary = new E1InfoSummary(_request.ID, _subID);
                    _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                    //_E1InfoSummary.AddressLable.Visibility = Visibility.Visible;
                    //_E1InfoSummary.AddressTextBox.Visibility = Visibility.Visible;
                    //_E1InfoSummary.PostalCodeInstallLable.Visibility = Visibility.Visible;
                    //_E1InfoSummary.PostalCodeTextBox.Visibility = Visibility.Visible;

                    E1InfoSummaryUC.Content = _E1InfoSummary;
                    E1InfoSummaryUC.DataContext = _E1InfoSummary;

                    E1InfoSummaryUC.Visibility = Visibility.Visible;

                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    InstallInfoUC.Visibility = Visibility.Collapsed;

                    break;
                case (byte)DB.RequestType.VacateE1:

                    E1InfoSummaryUC.Visibility = Visibility.Collapsed;
                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    InstallInfoUC.Visibility = Visibility.Collapsed;

                    break;
                case (byte)DB.RequestType.SpecialWire:
                case (byte)DB.RequestType.SpecialWireOtherPoint:

                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    InstallInfoUC.Visibility = Visibility.Collapsed;
                    switchDetail.Visibility = Visibility.Collapsed;

                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;

                    changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                    changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                    _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

                    changeLocationMDFWirig.Visibility = Visibility.Visible;
                    break;
                case (byte)DB.RequestType.VacateSpecialWire:

                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    InstallInfoUC.Visibility = Visibility.Collapsed;
                    switchDetail.Visibility = Visibility.Collapsed;

                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;

                    changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                    changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                    _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

                    changeLocationMDFWirig.Visibility = Visibility.Visible;

                    break;
                case (byte)DB.RequestType.ChangeLocationSpecialWire:

                    InvestigateInfoUC.Visibility = Visibility.Collapsed;
                    InstallInfoUC.Visibility = Visibility.Collapsed;
                    switchDetail.Visibility = Visibility.Collapsed;

                    _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                    _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                    _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Visible;

                    changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                    changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                    _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

                    changeLocationMDFWirig.Visibility = Visibility.Visible;

                    break;
            }

        }

        #endregion

        #region methods
        private void Initialize()
        {

            NetworkOfficerComboBox.ItemsSource = Failure117NetworkContractorDB.GetContractorOfficerName();
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print };
        }
        public void LoadData()
        {
            if (!ChechForCreateWiring())
                return;


            specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);
            _SelectTelephone = SelectTelephoneDB.GetSelectTelephone(_request.ID);

            WiringStatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep); //WorkFlowDB.GetCurrentStep(_request.StatusID));  
            WiringStatusComboBox.SelectedValue = _request.StatusID;

            if (!(_request.RequestTypeID == (int)DB.RequestType.E1 || _request.RequestTypeID == (int)DB.RequestType.E1Link))
            {
                _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).Take(1).SingleOrDefault();
            }
            else
            {
                // in e1 switch case handled
            }


            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.Dayri:
                case (byte)DB.RequestType.Reinstall:
                    DayriInitialize();
                    break;
                case (byte)DB.RequestType.ChangeLocationCenterInside:
                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                    ChangeLocationInitialize();
                    break;
                case (byte)DB.RequestType.ChangeNo:
                    ChangeNoLoad();
                    break;
                case (byte)DB.RequestType.Dischargin:
                    DischarginInitialize();
                    break;
                case (byte)DB.RequestType.RefundDeposit:
                    RefundDepositInitialize();
                    break;
                case (byte)DB.RequestType.E1:
                case (byte)DB.RequestType.E1Link:
                    E1Load();
                    break;
                case (byte)DB.RequestType.VacateE1:
                    VacateE1Load();
                    break;
                case (byte)DB.RequestType.SpecialWire:
                    SpecialWireLoad();
                    break;
                case (byte)DB.RequestType.SpecialWireOtherPoint:
                    SpecialWireLoad();
                    WiringStatusComboBoxLabel.Visibility = Visibility.Hidden;
                    WiringStatusComboBox.Visibility = Visibility.Hidden;
                    break;
                case (byte)DB.RequestType.VacateSpecialWire:
                    VacateSpecialWireLoad();
                    break;
                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                    ChangeLocationSpecialWireLoad();
                    break;
            }

            wiring.WiringStatus = _request.StatusID;
        }

        private void VacateE1Load()
        {

            _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);
            switchCode = DB.GetSwitchCodeInfoWithUsingSwitchPort(_request.TelephoneNo ?? 0);
            switchDetail.DataContext = switchCode;

            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            // TODO : چون در تغییر مکان مرکز به مرکز در مبدا سیم بندی نیز صادر میشود 
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            DateTime dateTime = DB.GetServerDate();
            wiring.WiringHour = dateTime.ToShortTimeString();
            wiring.WiringDate = dateTime;

            if (_subID != null)
            {
                _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);
                bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);

                _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                _ChangeLocationMDFWirig.subID = _subID;
                _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;
                changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                changeLocationMDFWirig.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;
                AirNetInfoGroopBox.Visibility = Visibility.Collapsed;

                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
            }
            else
            {
                changeLocationMDFWirig.Visibility = Visibility.Collapsed;

            }

            assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(_e1.BuchtID ?? 0) };

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        private bool ChechForCreateWiring()
        {
            try
            {
                //IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                //if (issueWiring == null)
                //{
                IssueWiringForm issueWiringForm = new IssueWiringForm(_request.ID);
                issueWiringForm.Load();
                issueWiringForm.Save();
                //}

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            this.ResizeWindow();
        }



        private void ChangeNoLoad()
        {
            AirNetInfo.Visibility = Visibility.Visible;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;



            _changeNo = Data.ChangeNoDB.GetChangeNoDBByID(_request.ID);

            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        private void ChangeLocationSpecialWireLoad()
        {

            _changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_request.ID);


            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        private void VacateSpecialWireLoad()
        {

            _vacateSpecialWire = Data.VacateSpecialWireDB.GetVacateSpecialWireByRequestID(_request.ID);


            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
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
                long? telephone = null;
                if (Data.RequestDocumnetDB.CheckTelephoneBeRound(_request, out telephone))
                {
                    TelRoundInfo roundTel = RoundListDB.GetRoundTelInfoByRequestID(_request.ID);
                    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo(roundTel.TelephoneNo);
                    tele.InRoundSale = false;
                    tele.Detach();
                    DB.Save(tele);
                }
                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Reinstall:
                        DayriSave();
                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationSave();
                        break;
                    case (byte)DB.RequestType.Dischargin:
                        DischarginSave();
                        break;
                    case (byte)DB.RequestType.ChangeNo:
                        ChangeNoSave();
                        break;
                    case (byte)DB.RequestType.RefundDeposit:
                        RefundDepositSave();
                        break;
                    case (byte)DB.RequestType.E1:
                    case (byte)DB.RequestType.E1Link:
                        E1Save();
                        break;
                    case (byte)DB.RequestType.VacateE1:
                        VacateE1Save();
                        break;
                    case (byte)DB.RequestType.SpecialWire:
                    case (byte)DB.RequestType.SpecialWireOtherPoint:
                        SpecialWireSave();
                        break;
                    case (byte)DB.RequestType.VacateSpecialWire:
                        VacateSpecialWireSave();
                        break;
                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        ChangeLocationSpecialWireSave();
                        break;
                }

                ShowSuccessMessage("ذخیره انجام شد");
                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }
            base.Save();
            return IsSaveSuccess;
        }

        public override bool Deny()
        {
            try
            {

                if (_request.RequestTypeID == (byte)DB.RequestType.Dayri || _request.RequestTypeID == (byte)DB.RequestType.Reinstall)
                {

                    DayriDelete();
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside ||
                         _request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter)
                {
                    ChangeLocationDelete();
                }

                else if (_request.RequestTypeID == (byte)DB.RequestType.Dischargin)
                {
                    DischarginDelete();
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.RefundDeposit)
                {
                    RefundDepositDelete();
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.SpecialWire)
                {
                    SpecialWireDelete();
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.E1)
                {
                    E1Reject();
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.VacateSpecialWire)
                {
                    VacateSpecialWireDelete();
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationSpecialWire)
                {
                    ChangeLocationSpecialWireDelete();
                }

                IsRejectSuccess = true;

            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }
            base.RequestID = _request.ID;
            return IsRejectSuccess;
        }

        public override bool Forward()
        {
            try
            {
                base.RequestID = _request.ID;
                //
                Status Status = Data.StatusDB.GetStatueByStatusID((int)WiringStatusComboBox.SelectedValue);
                if (Status.StatusType == (byte)DB.RequestStatusType.Recursive)
                {
                    SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);
                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _request.ID;
                        specialCondition.ReturnedFromWiring = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _request.ID;
                        specialCondition.ReturnedFromWiring = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }
                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);
                    IsSaveSuccess = true;
                }
                else
                {
                    SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);
                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _request.ID;
                        specialCondition.ReturnedFromWiring = false;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _request.ID;
                        specialCondition.ReturnedFromWiring = false;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }
                    Save();
                }


                if (IsSaveSuccess == true)
                {

                    if (_request.RequestTypeID == (int)DB.RequestType.VacateE1)
                    {
                        if (Data.E1LinkDB.CheckALLNetwork(_request.ID))
                        {
                            ApplyChangesVacateE1(_e1);
                            IsForwardSuccess = true;
                        }
                        else
                        {
                            throw new Exception("برای همه درخواست ها اطلاعات شبکه هوایی ذخیره نشده است");
                        }
                    }
                    else if (_request.RequestTypeID == (int)DB.RequestType.E1)
                    {
                        if (Data.E1LinkDB.CheckALLNetwork(_request.ID))
                        {
                            IsForwardSuccess = true;
                        }
                        else
                        {
                            throw new Exception("برای همه درخواست ها اطلاعات شبکه هوایی ذخیره نشده است");
                        }
                    }
                    else if (_request.RequestTypeID == (int)DB.RequestType.Dischargin && Status.StatusType == (byte)DB.RequestStatusType.Completed)
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);
                        telephone.CustomerTypeID = null;
                        telephone.CustomerGroupID = null;
                        telephone.Detach();
                        DB.Save(telephone, false);
                    }
                    else if (_request.RequestTypeID == (int)DB.RequestType.RefundDeposit && Status.StatusType == (byte)DB.RequestStatusType.Completed)
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);
                        telephone.CustomerTypeID = null;
                        telephone.CustomerGroupID = null;
                        telephone.Detach();
                        DB.Save(telephone, false);
                    }
                    {
                        IsForwardSuccess = true;
                    }


                    //if (_request.RequestTypeID == (byte)DB.RequestType.SpecialWire)
                    //{
                    //    // if source center and target center is one, use from Automatic Forward to Froward
                    //    if (_privateWire.TargetCenter == null || _privateWire.SourceCenter == _privateWire.TargetCenter)
                    //    {

                    //        Data.WorkFlowDB.SetNextState(DB.Action.AutomaticForward, _request.StatusID, _request.ID);

                    //        IsForwardSuccess = false;
                    //    }
                    //}


                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیر اطلاعات", ex);
            }
            return IsForwardSuccess;
        }

        private void ApplyChangesVacateE1(CRM.Data.E1 e1)
        {

            using (TransactionScope subts = new TransactionScope())
            {
                List<E1Link> E1Links = Data.E1LinkDB.GetE1LinkByRequestID(e1.RequestID);
                int e1linkCount = Data.E1DB.GetE1Count((long)_request.TelephoneNo);
                if (E1Links.Count == e1linkCount)
                {

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                    telephone.Status = (byte)DB.TelephoneStatus.Free;
                    telephone.CustomerID = null;
                    telephone.InstallAddressID = null;
                    telephone.CorrespondenceAddressID = null;
                    telephone.Detach();
                    DB.Save(telephone);
                }
                else
                {
                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                    telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                    telephone.Detach();
                    DB.Save(telephone);
                }

                E1Links.ForEach(e1Link =>
                {
                    PostContact contact = Data.PostContactDB.GetPostContactByID((int)_InvestigatePossibility.PostContactID);
                    contact.Status = (byte)DB.PostContactStatus.Free;
                    contact.Detach();
                    DB.Save(contact);

                    Bucht bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                    bucht.ConnectionID = null;
                    bucht.BuchtIDConnectedOtherBucht = null;
                    bucht.Status = (int)DB.BuchtStatus.Free;
                    bucht.Detach();
                    DB.Save(bucht);

                    Bucht otherBucht = Data.BuchtDB.GetBuchetByID(e1Link.OtherBuchtID);
                    otherBucht.SwitchPortID = null;
                    otherBucht.BuchtIDConnectedOtherBucht = null;
                    otherBucht.Status = (int)DB.BuchtStatus.Free;
                    otherBucht.Detach();
                    DB.Save(otherBucht);

                    Bucht acessBucht = Data.BuchtDB.GetBuchetByID(e1Link.AcessBuchtID);
                    acessBucht.Status = (int)DB.BuchtStatus.Free;
                    acessBucht.E1NumberID = null;
                    acessBucht.Detach();
                    DB.Save(acessBucht);

                    E1Number e1AcessNumber = Data.E1NumberDB.GetE1NumberByID(e1Link.AcessE1NumberID ?? 0);
                    e1AcessNumber.OtherNumberID = null;
                    e1AcessNumber.Status = (byte)DB.E1NumberStatus.Free;
                    e1AcessNumber.Detach();
                    DB.Save(e1AcessNumber);

                    E1Number switchE1Number = Data.E1NumberDB.GetE1NumberByID((int)e1Link.SwitchE1NumberID);
                    switchE1Number.Status = (byte)DB.E1NumberStatus.Connection;
                    switchE1Number.Detach();
                    DB.Save(switchE1Number);

                    E1Number e1InterfaceNumber = Data.E1NumberDB.GetE1NumberByID((int)e1Link.SwitchInterfaceE1NumberID);
                    e1InterfaceNumber.OtherNumberID = null;
                    e1InterfaceNumber.Status = (byte)DB.E1NumberStatus.Free;
                    e1InterfaceNumber.Detach();
                    DB.Save(e1InterfaceNumber);
                });
                subts.Complete();
            }
        }
        #endregion

        #region InitializeMethods

        void DayriInitialize()
        {


            changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
            changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
            changeLocationMDFWirig.Visibility = Visibility.Visible;
            _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
            _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;
            _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;


            switchCode = DB.GetSwitchCodeInfo(_SelectTelephone.TelephoneNo ?? 0);
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));
            switchDetail.DataContext = switchCode;
            //TelInfo.DataContext = switchCode;

            AirNetInfo.Visibility = Visibility.Visible;
            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);

            //TODO
            wiring = Data.WiringDB.GetInfoWiringByInvestigatePossibility(_investigateInfoSummary.investigate.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();
            bucht = Data.BuchtDB.GetBuchtByID((long)_investigateInfoSummary.investigate.BuchtID);

            assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(bucht.ID) };

            wiring.ConnectionID = _investigateInfoSummary.investigate.BuchtID;
            //ReserveDate.SelectedDate = _investigateInfoSummary.investigate.TelephoneAssignDate;


            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        void DischarginInitialize()
        {

            takePossession = new TakePossession();
            takePossession = Data.TakePossessionDB.GetTakePossessionByID(_request.ID);

            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            // TODO : چون در تغییر مکان مرکز به مرکز در مبدا سیم بندی نیز صادر میشود 
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();
            bucht = Data.BuchtDB.GetBuchtByID((long)takePossession.BuchtID);
            _newPostContact = Data.PostContactDB.GetPostContactByID((long)takePossession.PostContactID);

            switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);

            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));
            switchDetail.DataContext = switchCode;
            //TelInfo.DataContext = switchCode;

            assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(bucht.ID) };

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        void ChangeLocationInitialize()
        {


            changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
            changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
            changeLocationMDFWirig.Visibility = Visibility.Visible;
            _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;

            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));

            //ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;

            CustomerAddressUserControlUC.Visibility = Visibility.Visible;
            //ChooseNoGroupBox.Visibility = Visibility.Visible;

            InstallInfoUC.Visibility = Visibility.Collapsed;
            AirNetInfo.Visibility = Visibility.Visible;
            //ChooseNoGroupBox.Visibility = Visibility.Collapsed;

            changeLocation = new ChangeLocation();
            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_request.ID);

            switchCode = DB.GetSwitchCodeInfo(changeLocation.NewTelephone ?? changeLocation.OldTelephone ?? 0);

            switchDetail.DataContext = switchCode;
            //TelInfo.DataContext = switchCode;



            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            // TODO : چون در تغییر مکان مرکز به مرکز در مبدا سیم بندی نیز صادر میشود 
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();

            buchtReserve = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
            OldBucht = Data.BuchtDB.GetBuchtByID((long)changeLocation.OldBuchtID);

            _oldPostContact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);
            _newPostContact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
            ////////
            // اگر درخواست در مبدا است 
            if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
            {
                _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Collapsed;
                _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Visible;
            }

            // اگر درخواست در مقصد است
            else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
            {
                _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;
                assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(buchtReserve.ID) };

            }

            else
            {
                _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Visible;
                assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(buchtReserve.ID) };
            }

            //////////

            switchCode = DB.GetSwitchCodeInfo(changeLocation.NewTelephone ?? 0);

            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));
            switchDetail.DataContext = switchCode;
            //TelInfo.DataContext = switchCode;

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }
            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        private void SpecialWireLoad()
        {


            _specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_request.ID);


            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        private void E1Load()
        {


            _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);
            switchCode = DB.GetSwitchCodeInfoWithUsingSwitchPort(_e1.TelephoneNo ?? 0);
            switchDetail.DataContext = switchCode;
            //TelInfo.DataContext = switchCode;

            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            // TODO : چون در تغییر مکان مرکز به مرکز در مبدا سیم بندی نیز صادر میشود 
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.WiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.WiringDate = DB.GetServerDate();

            if (_subID != null)
            {

                _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);
                _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByID(_e1Link.InvestigatePossibilityID ?? 0);
                if (_InvestigatePossibility != null)
                {
                    bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID ?? 0);
                    //_oldPostContact = PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                }
                else
                {
                    throw new Exception("اطلاعات بررسی امکانات یافت نشد");
                }
                _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                _ChangeLocationMDFWirig.subID = _subID;
                _ChangeLocationMDFWirig.NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                _ChangeLocationMDFWirig.OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                _ChangeLocationMDFWirig.NewPostContactEquipmentButton.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.OldPostContactEquipmentButton.Visibility = Visibility.Collapsed;
                changeLocationMDFWirig.Content = _ChangeLocationMDFWirig;
                changeLocationMDFWirig.DataContext = _ChangeLocationMDFWirig;
                changeLocationMDFWirig.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.PostInfo.IsEnabled = true;
                AirNetInfoGroopBox.Visibility = Visibility.Collapsed;

                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
            }
            else
            {
                changeLocationMDFWirig.Visibility = Visibility.Collapsed;

            }

            assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(_e1.BuchtID ?? 0) };

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;

                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        void RefundDepositInitialize()
        {


            _RefundDeposit = Data.RefundDepositDB.GetRefundDepositByID(_request.ID);
            switchCode = DB.GetSwitchCodeInfo(_RefundDeposit.TelephoneNo ?? 0);

            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));

            switchDetail.DataContext = switchCode;
            //TelInfo.DataContext = switchCode;

            AirNetInfo.Visibility = Visibility.Visible;
            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);

            //TODO
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            bucht = Data.BuchtDB.GetBuchetByID(_RefundDeposit.BuchtID);

            assingmentInfo = new List<AssignmentInfo> { DB.GetAllInformationByBuchtID(bucht.ID) };

            if (HasContinueWiringPermissionCheckBox.IsChecked == false)
            {
                obstdateLabel.Visibility = Visibility.Collapsed;
                obstHourLabel.Visibility = Visibility.Collapsed;
                ObstacleDatePicker.Visibility = Visibility.Collapsed;
                ResolveObstacleHourTextBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                obstdateLabel.Visibility = Visibility.Visible;
                obstHourLabel.Visibility = Visibility.Visible;
                ObstacleDatePicker.Visibility = Visibility.Visible;
                ResolveObstacleHourTextBox.Visibility = Visibility.Visible;
                WiringDatePicker.IsEnabled = false;
                WiringHourTextBox.IsEnabled = false;

            }

            AirNetInfoGroopBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        #endregion

        #region SaveMethods

        private void SpecialWireSave()
        {
            DateTime serverDate = DB.GetServerDate();
            using (TransactionScope ts = new TransactionScope())
            {
                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_specialWire.TelephoneNo);



                if (telephone.SwitchPortID == null)
                    throw new Exception("پورت تلفن یافت نشد لطفا اطلاعات پورت را اصلاح کنید");
                if (Data.SpecialWireDB.IsLastRequest(_request))
                {
                    telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                    telephone.InstallationDate = serverDate;
                    telephone.DischargeDate = null;
                    telephone.CauseOfTakePossessionID = null;
                    telephone.CustomerTypeID = _specialWire.CustomerTypeID;
                    telephone.CustomerGroupID = _specialWire.CustomerGroupID;
                    telephone.Detach();
                    DB.Save(telephone);
                }

                if (_specialWire.SpecialWireType == (int)DB.SpecialWireType.General)
                {
                    PostContact postContact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                    postContact.Status = (byte)DB.PostContactStatus.CableConnection;
                    postContact.Detach();
                    DB.Save(postContact);

                    SpecialWireAddress specialWireAddress = SpecialWireAddressDB.GetSpecialWireAddressByBuchtID((long)_InvestigatePossibility.BuchtID);
                    if (specialWireAddress == null)
                    {
                        specialWireAddress = new SpecialWireAddress();
                        specialWireAddress.BuchtID = (long)_InvestigatePossibility.BuchtID;
                        specialWireAddress.InstallAddressID = (long)_specialWire.InstallAddressID;
                        specialWireAddress.CorrespondenceAddressID = (long)_specialWire.CorrespondenceAddressID;
                        specialWireAddress.TelephoneNo = (long)_specialWire.TelephoneNo;
                        specialWireAddress.SecondBuchtID = _specialWire.OtherBuchtID;
                        specialWireAddress.SpecialTypeID = _specialWire.SpecialWireType;
                        specialWireAddress.Detach();
                        DB.Save(specialWireAddress, true);
                    }
                    else
                    {
                        specialWireAddress.InstallAddressID = (long)_specialWire.InstallAddressID;
                        specialWireAddress.CorrespondenceAddressID = (long)_specialWire.CorrespondenceAddressID;
                        specialWireAddress.TelephoneNo = (long)_specialWire.TelephoneNo;
                        specialWireAddress.SecondBuchtID = _specialWire.OtherBuchtID;
                        specialWireAddress.SpecialTypeID = _specialWire.SpecialWireType;
                        specialWireAddress.Detach();
                        DB.Save(specialWireAddress, false);
                    }




                    _specialWire.SetupDate = serverDate;
                    _specialWire.Detach();
                    DB.Save(_specialWire, false);

                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.WiringInsertDate = serverDate;
                    wiring.ConnectionID = postContact.ID;
                    wiring.Detach();
                    DB.Save(wiring);


                }

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;

                _request.Detach();
                DB.Save(_request);
                ts.Complete();
            }

        }

        private void VacateE1Save()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (_subID != null)
                {
                    if (_e1Link.NetworkDate == null)
                    {
                        _e1Link.NetworkDate = DB.GetServerDate();
                        _e1Link.Detach();
                        DB.Save(_e1Link);

                        RequestLog requestLog = new RequestLog(); ;
                        requestLog.RequestID = _request.ID;
                        requestLog.RequestTypeID = _request.RequestTypeID;
                        requestLog.TelephoneNo = _request.TelephoneNo;
                        requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_request.CustomerID);
                        requestLog.UserID = DB.CurrentUser.ID;

                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                        Data.Schema.VacateLinkLog VacateE1LinkLog = new Data.Schema.VacateLinkLog();

                        VacateE1LinkLog.BuchtID = (long)_e1Link.BuchtID;
                        VacateE1LinkLog.OtherBuchtID = (long)_e1Link.OtherBuchtID;
                        VacateE1LinkLog.PostContactID = (long)_e1Link.PostContactID;
                        VacateE1LinkLog.E1NumberAccesID = (long)_e1Link.AcessE1NumberID;
                        VacateE1LinkLog.E1NumberInterfaceSwitchID = (long)_e1Link.SwitchInterfaceE1NumberID;
                        VacateE1LinkLog.E1NumberSwitchID = (long)_e1Link.SwitchE1NumberID;


                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.VacateLinkLog>(VacateE1LinkLog, true));

                        requestLog.Date = DB.GetServerDate();
                        requestLog.Detach();
                        DB.Save(requestLog);
                    }
                }
                else
                {
                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);
                }
                ts.Complete();
            }
        }

        private void ChangeNoSave()
        {
            // در چرخه کاری فعلی تعویض شماره شبکه هوایی نمی آید
            using (TransactionScope ts = new TransactionScope())
            {
                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request, false);


                Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_changeNo.NewTelephoneNo);
                Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_changeNo.OldTelephoneNo);
                Bucht oldBucht = Data.BuchtDB.GetBuchetByID(_changeNo.OldBuchtID);
                Bucht newBucht = DB.GetBuchtIDByTelephonNo((long)_changeNo.NewTelephoneNo);


                ////////
                newBucht.ConnectionID = oldBucht.ConnectionID;
                newBucht.Status = oldBucht.Status;
                newBucht.ADSLStatus = oldBucht.ADSLStatus;
                newBucht.Detach();
                DB.Save(newBucht);

                ////////

                ////////
                oldBucht.ConnectionID = null;
                oldBucht.Status = (byte)DB.BuchtStatus.Free;
                //   oldBucht.ADSLPortID = null;
                oldBucht.ADSLStatus = false;
                //   oldBucht.ADSLType = null;
                oldBucht.Detach();
                DB.Save(oldBucht);
                ////////


                newTelephone.CustomerID = oldTelephone.CustomerID;
                newTelephone.InstallAddressID = oldTelephone.InstallAddressID;
                newTelephone.CorrespondenceAddressID = oldTelephone.CorrespondenceAddressID;
                newTelephone.CauseOfCutID = oldTelephone.CauseOfCutID;
                newTelephone.Status = oldTelephone.Status;
                newTelephone.Detach();
                DB.Save(newTelephone);

                ////////

                oldTelephone.CustomerID = null;
                oldTelephone.InstallAddressID = null;
                oldTelephone.CorrespondenceAddressID = null;
                oldTelephone.CauseOfCutID = null;
                oldTelephone.Detach();
                DB.Save(oldTelephone);


                ////////


                ///////صدور فرم سیم بندی

                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.OldBuchtID = oldBucht.ID;
                wiring.OldBuchtType = (byte?)oldBucht.BuchtTypeID;
                wiring.BuchtType = (byte?)newBucht.BuchtTypeID;
                wiring.OldConnectionID = oldBucht.ConnectionID;
                wiring.ConnectionID = newBucht.ConnectionID;
                wiring.NewTelephoneNo = _changeNo.NewTelephoneNo;
                wiring.OldTelephoneNo = _changeNo.OldTelephoneNo;
                wiring.RequestID = _request.ID;
                wiring.Status = _request.StatusID;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);
                ///////

                ts.Complete();
            }
        }

        //TOOD: milad doran 
        //void DayriSave()
        //{

        //    try
        //    {
        //        DateTime serverDate = DB.GetServerDate();

        //        using (TransactionScope ts = new TransactionScope())
        //        {


        //            InstallRequest installReqeust = Data.InstallRequestDB.GetInstallRequestByRequestID(_request.ID);
        //            installReqeust.InstallationDate = serverDate;
        //            installReqeust.Detach();
        //            DB.Save(installReqeust, false);

        //            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_request.TelephoneNo ?? 0);
        //            telephone.InstallationDate = serverDate;
        //            telephone.InitialInstallationDate = serverDate;
        //            telephone.DischargeDate = null;
        //            telephone.CauseOfTakePossessionID = null; 
        //            telephone.InitialDischargeDate = null;
        //            telephone.CustomerTypeID = installReqeust.TelephoneType;
        //            telephone.CustomerGroupID = installReqeust.TelephoneTypeGroup;
        //            telephone.ChargingType = installReqeust.ChargingType;
        //            telephone.PosessionType = installReqeust.PosessionType;
        //            telephone.Detach();
        //            DB.Save(telephone);


        //            wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
        //            wiring.WiringInsertDate = serverDate;

        //            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
        //            wiring.IssueWiringID = issueWiring.ID;

        //            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
        //            _request.ModifyDate = DB.GetServerDate();



        //            //BuchtType buchtType = Data.BuchtTypeDB.GetBuchtTypeByID(bucht.BuchtTypeID);
        //            //if (buchtType.ID == (int)DB.BuchtType.OpticalBucht || buchtType.ParentID == (int)DB.BuchtType.OpticalBucht)
        //            //{
        //            //    bucht.Status = (byte)DB.BuchtStatus.Connection;
        //            //    bucht.Detach();
        //            //    DB.Save(bucht);

        //            //}
        //            assingmentInfo = assingmentInfo.Where(t => t.BuchtID == bucht.ID).Distinct().ToList();
        //            WiringDB.SaveNetWiring(wiring, bucht.ConnectionID ?? -1, _request, assingmentInfo.SingleOrDefault());

        //            ts.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("خطا در ذخیره اطلاعات");
        //    }



        //}
        //*******************************

        //TODO:rad 13960412 - متد ذخیره بعد از مطرح شدن سامانه شاهکار
        void DayriSave()
        {
            PendarWebApiResult pendarWebApiResult = new PendarWebApiResult();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string responseFromPendarWebApi = string.Empty;
            ShahkarRawResult shahkarResultFromPendarWebApi = new ShahkarRawResult();

            try
            {
                DateTime serverDate = DB.GetServerDate();
                //TODO:rad 13960403 - بلاک زیر به علت ثبت سرویس تلفن ثابت برای مشترکین حقیقی در سامانه شاهکار اضافه شد
                Telephone requestTelephone = TelephoneDB.GetTelephoneByTelephoneNo(_request.TelephoneNo ?? 0);
                if (requestTelephone.UsageType == (byte)DB.TelephoneUsageType.Usuall && this._customerInfoSummary._customer.PersonType == (byte)DB.PersonType.Person)
                {
                    using (TransactionScope shahkarScope = new TransactionScope(TransactionScopeOption.Suppress, new TimeSpan(0, 2, 0)))
                    {
                        IranianPersonCustomerInstallTelephone iranianPersonCustomerInstallTelephone = ShahkarEntityCreators.CreateInstallTelephoneFromTelephone(requestTelephone.TelephoneNo);

                        if (iranianPersonCustomerInstallTelephone == null)
                        {
                            throw new WebException(".ایجاد موجودیت تلفن جدید برای ثبت در سامانه شاهکار با خطا مواجه شد");
                        }

                        //فراخوانی مقادیر تنظیمات سامانه خودمان
                        string pendarPajouhWebApiIP = SettingDB.GetSettingByKey("PendarPajouhWebApiIP").Value;
                        string pendarPajouhWebApiPort = SettingDB.GetSettingByKey("PendarPajouhWebApiPort").Value;
                        string pendarPajouhApiUserName = SettingDB.GetSettingByKey("PendarPajouhApiUserName").Value;
                        string pendarPajouhApiPassword = SettingDB.GetSettingByKey("PendarPajouhApiPassword").Value;
                        //string pendarPajouhWebApiIP = "78.39.252.109";
                        //string pendarPajouhWebApiPort = "8084";
                        //string pendarPajouhApiUserName = "ali";
                        //string pendarPajouhApiPassword = "rad";

                        //آدرس ای پی مربوط به احراز هویت سامانه شاهکار
                        string pendarWebApiAddress = string.Format("http://{0}:{1}/api/PendarPajouhCRM/IranianPersonCustomerInstallTelephone", pendarPajouhWebApiIP, pendarPajouhWebApiPort);

                        //بر اساس تصمیم گیری جدید ، قبل از هر گونه اقدامی باید مشترک از طرف شاهکار احراز هویت گردد
                        responseFromPendarWebApi = new Send<IranianPersonCustomerInstallTelephone>().SendHttpWebRequest(iranianPersonCustomerInstallTelephone, pendarWebApiAddress, "POST", pendarPajouhApiUserName, pendarPajouhApiPassword);

                        if (responseFromPendarWebApi == null)
                        {
                            throw new WebException(".پاسخ دریافتی از سامانه خالی بوده است");
                        }

                        pendarWebApiResult = serializer.Deserialize<PendarWebApiResult>(responseFromPendarWebApi);

                        shahkarResultFromPendarWebApi = serializer.Deserialize<ShahkarRawResult>(pendarWebApiResult.RawResultFromShahkar);

                        ShahkarWebApiLog log = new ShahkarWebApiLog();

                        log.ActionType = (int)DB.ShahkarActionType.IranianPersonCustomerInstallTelephone;
                        log.ActionRelativeURL = "api/PendarPajouhCRM/IranianPersonCustomerInstallTelephone";
                        log.CustomerID = requestTelephone.CustomerID.Value;
                        log.SendDate = serverDate;
                        log.UserID = DB.CurrentUser.ID;
                        log.TelephoneNo = requestTelephone.TelephoneNo;
                        log.RequestID = _request.ID;

                        Data.Schema.ShahkarResult shahkarResult = new Data.Schema.ShahkarResult();
                        shahkarResult.Comment = shahkarResultFromPendarWebApi.comment;
                        shahkarResult.RequestId = shahkarResultFromPendarWebApi.requestId;
                        shahkarResult.Response = shahkarResultFromPendarWebApi.response;
                        shahkarResult.Result = shahkarResultFromPendarWebApi.result;
                        shahkarResult.ID = shahkarResultFromPendarWebApi.id;
                        shahkarResult.FollowNo = shahkarResultFromPendarWebApi.followNo;

                        CRM.Data.Schema.ShahkarInstallTelephoneInfo shahkarInstallTelephoneInfo = new CRM.Data.Schema.ShahkarInstallTelephoneInfo();
                        shahkarInstallTelephoneInfo.ResultDetail = shahkarResult;

                        log.ActionDetails = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ShahkarInstallTelephoneInfo>(shahkarInstallTelephoneInfo, true));
                        DB.Save(log);
                        shahkarScope.Complete();
                    }

                    if (shahkarResultFromPendarWebApi.response == 200) //تلفن در سامانه شاهکار با موفقیت ثبت شده است
                    {
                        requestTelephone.ShahkarClassified = shahkarResultFromPendarWebApi.id;
                        requestTelephone.Detach();
                        DB.Save(requestTelephone, false);
                        MessageBox.Show(".تلفن در سامانه شاهکار با موفقیت ثبت شد", "نتیجه نهایی", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else //تلفن در سامانه شاهکار با موفقیت ثبت نشده است و باید پاسخ مناسب برای نمایش به کابر آماده شود
                    {
                        ShahkarMeaningfulResponse shahkarMeaningfulResponse = new ShahkarMeaningfulResponse();

                        if (shahkarResultFromPendarWebApi.response == 311)
                        {
                            shahkarMeaningfulResponse = ShahkarMeaningfulResponse.ProvideShahkarMeaningfulResponseByRawResultFromShahkar(shahkarResultFromPendarWebApi);

                            if (!string.IsNullOrEmpty(shahkarMeaningfulResponse.Descriptions))
                            {
                                MessageBox.Show(shahkarMeaningfulResponse.Descriptions, "پاسخ شاهکار", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        //بلاک زیر هر خطای دیگری غیر از 311 را شامل میشود
                        if (shahkarResultFromPendarWebApi.response != 311)
                        {
                            MessageBox.Show(shahkarResultFromPendarWebApi.comment, "پاسخ شاهکار", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        MessageBox.Show(".تلفن در سامانه شاهکار ثبت نشد", "نتیجه نهایی", MessageBoxButton.OK, MessageBoxImage.Warning);
                        throw new WebException(".ساختار اطلاعات تلفن جدید در شاهکار تایید نشده است");
                    }
                }
                //**************************************************************************************

                using (TransactionScope pendarScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 3, 0)))
                {
                    Logger.WriteInfo("Start Of Second Transaction - Step 1");
                    InstallRequest installReqeust = Data.InstallRequestDB.GetInstallRequestByRequestID(_request.ID);
                    installReqeust.InstallationDate = serverDate;
                    installReqeust.Detach();
                    DB.Save(installReqeust, false);
                    Logger.WriteInfo("Start Of Second Transaction - Step 2");

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_request.TelephoneNo ?? 0);
                    telephone.InstallationDate = serverDate;
                    telephone.InitialInstallationDate = serverDate;
                    telephone.DischargeDate = null;
                    telephone.CauseOfTakePossessionID = null;
                    telephone.InitialDischargeDate = null;
                    telephone.CustomerTypeID = installReqeust.TelephoneType;
                    telephone.CustomerGroupID = installReqeust.TelephoneTypeGroup;
                    telephone.ChargingType = installReqeust.ChargingType;
                    telephone.PosessionType = installReqeust.PosessionType;
                    telephone.Detach();
                    DB.Save(telephone);
                    Logger.WriteInfo("Start Of Second Transaction - Step 3");

                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.WiringInsertDate = serverDate;

                    IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                    wiring.IssueWiringID = issueWiring.ID;

                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.ModifyDate = DB.GetServerDate();



                    //BuchtType buchtType = Data.BuchtTypeDB.GetBuchtTypeByID(bucht.BuchtTypeID);
                    //if (buchtType.ID == (int)DB.BuchtType.OpticalBucht || buchtType.ParentID == (int)DB.BuchtType.OpticalBucht)
                    //{
                    //    bucht.Status = (byte)DB.BuchtStatus.Connection;
                    //    bucht.Detach();
                    //    DB.Save(bucht);

                    //}
                    assingmentInfo = assingmentInfo.Where(t => t.BuchtID == bucht.ID).Distinct().ToList();
                    WiringDB.SaveNetWiring(wiring, bucht.ConnectionID ?? -1, _request, assingmentInfo.SingleOrDefault());

                    pendarScope.Complete();
                }
            }
            catch (WebException we)
            {
                if (we.Response != null)
                {
                    var responseStream = we.Response.GetResponseStream();
                    string primaryErrorResponse = string.Empty;
                    pendarWebApiResult = null;
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        primaryErrorResponse = reader.ReadToEnd();
                        pendarWebApiResult = serializer.Deserialize<PendarWebApiResult>(primaryErrorResponse);
                    }
                    responseStream.Close();
                    if (pendarWebApiResult != null && pendarWebApiResult.SystemHasError)
                    {
                        MessageBox.Show("خطای سیستمی", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        Logger.WriteError("{0} - ثبت سرویس تلفن ثابت مشترک حقیقی خطا داشت", pendarWebApiResult.SystemError);
                    }
                    else
                    {
                        Logger.WriteError("{0} - خطای سیستمی", primaryErrorResponse);
                        MessageBox.Show("خطای سیستمی", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("خطا در ثبت تلفن در سامانه شاهکار", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Logger.WriteError("User :{0} in RequestID :{1} Following exception caught in {2}: ", DB.CurrentUser.ID, _request.ID.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Write(we);
                throw new Exception("خطا در ذخیره اطلاعات");
            }
            catch (Exception ex)
            {
                Logger.WriteError("User :{0} in RequestID :{1} Following exception caught in {2}: ", DB.CurrentUser.ID, _request.ID.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Write(ex);
                throw new Exception("خطا در ذخیره اطلاعات");
            }
        }

        void E1Save()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (_subID != null)
                {
                    if (_e1Link.NetworkDate == null)
                    {


                        PostContact contact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                        contact.Status = (byte)DB.PostContactStatus.CableConnection;
                        contact.Detach();
                        DB.Save(contact);

                        Bucht bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                        bucht.ConnectionID = _InvestigatePossibility.PostContactID;
                        bucht.Detach();
                        DB.Save(bucht);

                        _e1Link.NetworkDate = DB.GetServerDate();
                        _e1Link.Detach();
                        DB.Save(_e1Link);


                        RequestLog requestLog = new RequestLog(); ;
                        requestLog.RequestID = _request.ID;
                        requestLog.RequestTypeID = _request.RequestTypeID;
                        requestLog.TelephoneNo = _e1.TelephoneNo;
                        requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_request.CustomerID);
                        requestLog.UserID = DB.CurrentUser.ID;

                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                        Data.Schema.E1LinkLog E1Link = new Data.Schema.E1LinkLog();
                        E1Link.TelephoneNo = telephone.TelephoneNo;
                        E1Link.CenterID = _request.CenterID;
                        E1Link.CodeTypeID = (int)_e1.CodeTypeID;
                        E1Link.LinkTypeID = (int)_e1.LinkTypeID;
                        E1Link.CustomerID = _request.CustomerID ?? 0;
                        E1Link.InstalAddressID = telephone.InstallAddressID ?? 0;
                        E1Link.VirtualTelephoneNo = telephone.VirtualTelephoneNo ?? 0;
                        E1Link.BuchtID = (long)_InvestigatePossibility.BuchtID;
                        E1Link.PostContactID = (long)_InvestigatePossibility.PostContactID;
                        E1Link.OtherBuchtID = _e1Link.OtherBuchtID ?? 0;
                        E1Link.E1NumberAccesID = _e1Link.AcessE1NumberID ?? 0;

                        if (_e1Link.SwitchInterfaceE1NumberID != null)
                            E1Link.E1NumberInterfaceSwitchID = (long)_e1Link.SwitchInterfaceE1NumberID;

                        if (_e1Link.SwitchE1NumberID != null)
                            E1Link.E1NumberSwitchID = (long)_e1Link.SwitchE1NumberID;


                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.E1LinkLog>(E1Link, true));

                        requestLog.Date = DB.GetServerDate();
                        requestLog.Detach();
                        DB.Save(requestLog);

                    }
                }
                else
                {
                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.WiringInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);
                }
                ts.Complete();
            }
        }

        private void VacateSpecialWireSave()
        {
            DateTime serverDate = DB.GetServerDate();
            using (TransactionScope ts = new TransactionScope())
            {

                Bucht sourceBucht = new Bucht();

                if (Data.VacateSpecialWireDB.IsLastRequest(_request))
                {
                    //TODO:rad 13950614 - 1749
                    //if (Data.SpecialWireAddressDB.ExsistBuchtInSpecialWireAddress(_vacateSpecialWire.BuchtID))
                    //    DB.Delete<SpecialWireAddress>(_vacateSpecialWire.BuchtID);
                    //*********************************************************************************************

                    List<SpecialWirePoints> _specialWirePoints = Data.SpecialWirePointsDB.GetSpecialWirePointsByTelephone((long)_request.TelephoneNo);
                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);
                    // discharge telephone if this point is last point of special wire 
                    if (_specialWirePoints.Count == 0)
                    {

                        telephone.Status = (byte)DB.TelephoneStatus.Discharge;
                        telephone.DischargeDate = serverDate;
                        telephone.Detach();
                        DB.Save(telephone, false);

                    }
                    else
                    {
                        telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                        telephone.Detach();
                        DB.Save(telephone, false);
                    }
                }



                Center sourceCenter = Data.SpecialWireDB.GetSourceCenterSpecialWireByTelephoneNo((long)_request.TelephoneNo, out sourceBucht);
                if (sourceCenter != null && sourceCenter.ID == _request.CenterID && _vacateSpecialWire.BuchtID == sourceBucht.ID)
                {
                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);
                    telephone.DischargeDate = DB.GetServerDate();
                    //TODO:rad 13950614
                    telephone.Status = (byte)DB.TelephoneStatus.Discharge;
                    //************************************************************
                    telephone.InstallAddressID = null;
                    telephone.CorrespondenceAddressID = null;
                    telephone.CustomerID = null;
                    telephone.CustomerTypeID = null;
                    telephone.CustomerGroupID = null;
                    telephone.Detach();
                    DB.Save(telephone, false);

                }

                PostContact postContact = Data.PostContactDB.GetPostContactByID(_vacateSpecialWire.PostContactID ?? 0);
                postContact.Status = (byte)DB.PostContactStatus.Free;
                postContact.Detach();
                DB.Save(postContact);

                // Bucht OtherBucht = Data.BuchtDB.GetBuchetByID(_vacateSpecialWire.OtherBuchtID);
                Bucht bucht = Data.BuchtDB.GetBuchetByID(_vacateSpecialWire.BuchtID);
                bucht.ConnectionID = null;
                bucht.SwitchPortID = null;
                bucht.Detach();
                DB.Save(bucht);

                if (Data.SpecialWireAddressDB.ExsistBuchtInSpecialWireAddress(_vacateSpecialWire.BuchtID))
                    DB.Delete<SpecialWireAddress>(_vacateSpecialWire.BuchtID);

                _vacateSpecialWire.VacateDate = DB.GetServerDate();
                _vacateSpecialWire.Detach();
                DB.Save(_vacateSpecialWire);

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.ConnectionID = postContact.ID;
                wiring.WiringInsertDate = serverDate;
                wiring.Detach();
                DB.Save(wiring);

                ts.Complete();
            }

        }

        void ChangeLocationSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                DateTime curentServerDate = DB.GetServerDate();

                PostContact contact;
                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.WiringInsertDate = curentServerDate;
                IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                wiring.IssueWiringID = issueWiring.ID;
                wiring.Detach();
                DB.Save(wiring);


                /// is itself change location
                /// without change post contact
                if (changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.itself && changeLocation.OldPostContactID == _InvestigatePossibility.PostContactID)
                {
                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                    telephone.InstallAddressID = changeLocation.NewInstallAddressID;
                    telephone.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                    // if request is accompanied by change name
                    if (changeLocation.NewCustomerID != null)
                    {
                        // change customer Name
                        telephone.CustomerID = changeLocation.NewCustomerID;
                    }
                    //
                    telephone.Detach();
                    DB.Save(telephone);


                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    ts.Complete();
                    return;
                }
                // with change post contact
                else if (changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.itself && changeLocation.OldPostContactID != _InvestigatePossibility.PostContactID)
                {
                    AssignmentInfo OldTelephoneAssignmentInfo = DB.GetAllInformationByTelephoneNo(changeLocation.OldTelephone ?? 0);

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                    telephone.InstallAddressID = changeLocation.NewInstallAddressID;
                    telephone.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                    // if request is accompanied by change name
                    if (changeLocation.NewCustomerID != null)
                    {
                        // change customer Name
                        telephone.CustomerID = changeLocation.NewCustomerID;
                    }
                    //
                    telephone.Detach();
                    DB.Save(telephone);

                    // اتصالی پست قبلی را آزاد می کند.
                    _oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                    _oldPostContact.Detach();
                    DB.Save(_oldPostContact);

                    // بوخت قدیم را با اتصالی جدید متصل می کند.
                    OldBucht.ConnectionID = _InvestigatePossibility.PostContactID;
                    OldBucht.Detach();
                    DB.Save(OldBucht);

                    // اتصالی پست جدید به حالت متصل می رود
                    _newPostContact.Status = (byte)DB.PostContactStatus.CableConnection;
                    _newPostContact.Detach();
                    DB.Save(_newPostContact);

                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);
                    ts.Complete();
                    return;
                }

                /////
                // اگر درخواست در مبدا است 
                if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
                {

                    contact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);
                    if (contact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || contact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    {
                        contact.Status = (byte)DB.PostContactStatus.Free;

                        PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(OldBucht.PCMPortID ?? 0);
                        if (PCMPort != null)
                        {
                            PCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                            PCMPort.Detach();
                            DB.Save(PCMPort);
                        }
                    }
                    else
                    {
                        contact.Status = (byte)DB.PostContactStatus.Free;
                        OldBucht.ConnectionID = null;
                        OldBucht.Detach();
                        DB.Save(OldBucht);
                    }
                    contact.Detach();
                    DB.Save(contact);


                    //Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                    //oldTelephone.DischargeDate = curentServerDate;
                    //oldTelephone.Detach();
                    //DB.Save(oldTelephone);
                }

                // اگر درخواست در مقصد است
                else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
                {
                    contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                    contact.Status = (byte)DB.PostContactStatus.CableConnection;
                    contact.Detach();
                    DB.Save(contact);


                    Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);

                    Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);


                    newTelephone.CustomerID = oldTelephone.CustomerID;
                    newTelephone.InstallationDate = curentServerDate;
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
                    newTelephone.InstallAddressID = changeLocation.NewInstallAddressID;
                    newTelephone.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                    newTelephone.CustomerID = _request.CustomerID;
                    newTelephone.ClassTelephone = oldTelephone.ClassTelephone;
                    newTelephone.Detach();
                    DB.Save(newTelephone);

                    oldTelephone.DischargeDate = curentServerDate;
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


                }
                else
                {
                    if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside && (specialCondition.EqualityOfBuchtTypeOptical || specialCondition.NotEqualityOfBuchtTypeOptical))
                    {

                        throw new Exception("خطا در ذخیره درخواست لطفا با واحد پشتیبانی تماس بگیرید");
                        // این قسمت به درخواست سمنان گذاشته شد بعدا مشخص شد اشتباه می باشد 

                        //contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                        //contact.Status = (byte)DB.PostContactStatus.CableConnection;
                        //contact.Detach();
                        //DB.Save(contact);

                        //contact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);
                        //contact.Status = (byte)DB.PostContactStatus.Free;
                        //contact.Detach();
                        //DB.Save(contact);

                        //// release old Bucht
                        //Bucht OldBucht = Data.BuchtDB.GetBuchtByID((long)changeLocation.OldBuchtID);
                        //Bucht NewBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);

                        //NewBucht.BuchtIDConnectedOtherBucht = OldBucht.BuchtIDConnectedOtherBucht;

                        //OldBucht.Status = (byte)DB.BuchtStatus.Free;
                        //OldBucht.ConnectionID = null;
                        //OldBucht.BuchtIDConnectedOtherBucht = null;
                        //OldBucht.Detach();
                        //DB.Save(OldBucht);

                        //// release old Bucht
                        //NewBucht.Status = (byte)DB.BuchtStatus.Connection;
                        //NewBucht.Detach();
                        //DB.Save(NewBucht);

                        //Telephone Oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                        //Oldtele.CustomerID = null;
                        //Oldtele.InstallAddressID = null;
                        //Oldtele.CorrespondenceAddressID = null;
                        //Oldtele.DischargeDate = null;
                        //Oldtele.CauseOfCutID = null;
                        //Oldtele.Detach();
                        //DB.Save(Oldtele);
                    }
                    else
                    {


                        contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                        contact.Status = (byte)DB.PostContactStatus.CableConnection;
                        contact.Detach();
                        DB.Save(contact);

                        contact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);
                        if (contact != null)
                        {
                            if (contact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || contact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                contact.Status = (byte)DB.PostContactStatus.Free;
                                PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(OldBucht.PCMPortID ?? 0);
                                if (PCMPort != null)
                                {
                                    PCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                                    PCMPort.Detach();
                                    DB.Save(PCMPort);
                                }

                            }
                            else
                            {

                                contact.Status = (byte)DB.PostContactStatus.Free;
                                OldBucht.ConnectionID = null;
                                OldBucht.Detach();
                                DB.Save(OldBucht);
                            }

                            contact.Detach();
                            DB.Save(contact);
                        }
                        if (changeLocation.NewTelephone != null)
                        {

                            Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                            Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);

                            newTelephone.CustomerID = oldTelephone.CustomerID;
                            newTelephone.InstallationDate = curentServerDate;
                            newTelephone.InitialInstallationDate = oldTelephone.InitialInstallationDate;
                            newTelephone.DischargeDate = null;
                            newTelephone.CauseOfTakePossessionID = null;
                            newTelephone.CustomerTypeID = oldTelephone.CustomerTypeID;
                            newTelephone.CustomerGroupID = oldTelephone.CustomerGroupID;
                            newTelephone.ChargingType = oldTelephone.ChargingType;
                            newTelephone.PosessionType = oldTelephone.PosessionType;
                            newTelephone.Status = oldTelephone.Status;
                            newTelephone.CauseOfCutID = oldTelephone.CauseOfCutID;
                            newTelephone.CutDate = oldTelephone.CutDate;
                            newTelephone.InstallAddressID = changeLocation.NewInstallAddressID;
                            newTelephone.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                            newTelephone.CustomerID = _request.CustomerID;
                            newTelephone.ClassTelephone = oldTelephone.ClassTelephone;
                            newTelephone.Detach();
                            DB.Save(newTelephone);

                            oldTelephone.Status = (int)DB.TelephoneStatus.Discharge;
                            oldTelephone.DischargeDate = curentServerDate;
                            oldTelephone.CustomerTypeID = null;
                            oldTelephone.CustomerGroupID = null;
                            oldTelephone.InitialInstallationDate = null;
                            oldTelephone.ChargingType = null;
                            oldTelephone.PosessionType = null;
                            oldTelephone.CauseOfCutID = null;
                            oldTelephone.InstallAddressID = null;
                            oldTelephone.CorrespondenceAddressID = null;
                            oldTelephone.CustomerID = null;
                            oldTelephone.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;
                            oldTelephone.Detach();
                            DB.Save(oldTelephone);


                        }
                        else
                        {
                            Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                            oldTelephone.InstallAddressID = changeLocation.NewInstallAddressID;
                            oldTelephone.CorrespondenceAddressID = changeLocation.NewCorrespondenceAddressID;
                            oldTelephone.Detach();
                            DB.Save(oldTelephone);

                        }

                    }


                    // if request is accompanied by change name
                    if (changeLocation.NewCustomerID != null)
                    {
                        // if request is accompanied by change number
                        if (changeLocation.NewTelephone != null)
                        {
                            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                            telephone.CustomerID = changeLocation.NewCustomerID;
                            telephone.Detach();
                            DB.Save(telephone);
                        }
                        // if request not is accompanied by change number
                        else
                        {
                            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                            telephone.CustomerID = changeLocation.NewCustomerID;
                            telephone.Detach();
                            DB.Save(telephone);
                        }
                    }
                    //
                }

                //////////



                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                ts.Complete();
            }
        }

        void DischarginSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                DateTime curentServerDate = DB.GetServerDate();
                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.WiringInsertDate = curentServerDate;

                IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                wiring.IssueWiringID = issueWiring.ID;
                wiring.Detach();
                DB.Save(wiring);

                // Release PostContacct
                PostContact contact = Data.PostContactDB.GetPostContactByID((long)takePossession.PostContactID);
                contact.Status = (byte)DB.PostContactStatus.Free;
                contact.Detach();
                DB.Save(contact);



                //BuchtType buchtType = Data.BuchtTypeDB.GetBuchtTypeByID(bucht.BuchtTypeID);
                //if (buchtType.ID == (int)DB.BuchtType.OpticalBucht || buchtType.ParentID == (int)DB.BuchtType.OpticalBucht)
                //{
                //    bucht.Status = (int)DB.BuchtStatus.Free;
                //    bucht.ConnectionID = null;
                //    //     bucht.ADSLPortID = null;
                //    bucht.ADSLStatus = false;
                //    //      bucht.ADSLType = null;
                //    bucht.Detach();
                //    DB.Save(bucht);

                //    // Free switch post
                //    SwitchPort switchport = Data.SwitchPortDB.GetSwitchPortByID((int)takePossession.SwitchPortID);
                //    switchport.Status = (byte)DB.SwitchPortStatus.Free;
                //    switchport.Detach();
                //    DB.Save(switchport);

                //}
                //else
                if (bucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                {
                    // Release Bucht
                    bucht.ConnectionID = null;
                    bucht.Detach();
                    DB.Save(bucht);
                }
                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                ts.Complete();
            }
        }

        void RefundDepositSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.WiringInsertDate = DB.GetServerDate();
                IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                wiring.IssueWiringID = issueWiring.ID;
                wiring.Detach();
                DB.Save(wiring);


                PostContact contact = Data.PostContactDB.GetPostContactByID(_RefundDeposit.PostContactID ?? 0);
                contact.Status = (byte)DB.PostContactStatus.Free;
                contact.Detach();
                DB.Save(contact);

                if (bucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                {
                    // Release Bucht
                    bucht.ConnectionID = null;
                    bucht.Detach();
                    DB.Save(bucht);
                }
                else
                {
                    bucht.Status = (int)DB.BuchtStatus.Free;
                    bucht.ConnectionID = null;
                    bucht.ADSLStatus = false;
                    bucht.Detach();
                    DB.Save(bucht);
                }

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);
                ts.Complete();
            }
        }

        private void ChangeLocationSpecialWireSave()
        {
            DateTime serverDate = DB.GetServerDate();

            if (_changeLocationSpecialWire.SetupDate != null)
                return;

            using (TransactionScope ts = new TransactionScope())
            {


                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);

                if (Data.ChangeLocationSpecialWireDB.IsLastRequest(_request))
                {
                    telephone.Status = (byte)DB.TelephoneStatus.Connecting;

                }

                Bucht OldBucht = Data.BuchtDB.GetBuchetByID((long)_changeLocationSpecialWire.OldBuchtID);

                if (specialCondition != null && !specialCondition.ChangeLocationInsider)
                {

                    PostContact oldPostContact = Data.PostContactDB.GetPostContactByID(_changeLocationSpecialWire.OldPostContactID ?? 0);
                    PostContact newPostContact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);

                    ChangeLocationSpecialWirePoint changeLocationSpecialWirePoint = Data.ChangeLocationSpecialWirePointsDB.GetChangeLocationSpecialWirePointsByRequestID(_request.MainRequestID == null ? _request.ID : (long)_request.MainRequestID).Where(t => t.BuchtID == _changeLocationSpecialWire.OldBuchtID).SingleOrDefault();

                    SpecialWireAddress specialWireAddress = Data.SpecialWireAddressDB.GetSpecialWireAddressByBuchtID(_changeLocationSpecialWire.OldBuchtID);

                    // اگر آدرس تلفن با ادرس نقطه جاری یکی باشد مرکز مبدا می باشد پس آدرس درتلفن نیز تغییر می کند
                    if (telephone.InstallAddressID == specialWireAddress.InstallAddressID)
                    {
                        telephone.InstallAddressID = changeLocationSpecialWirePoint.NewInstallAddressID;
                        telephone.CorrespondenceAddressID = changeLocationSpecialWirePoint.NewInstallAddressID;
                    }


                    SpecialWireAddress newSpecialWireAddress = new SpecialWireAddress();
                    newSpecialWireAddress.InstallAddressID = changeLocationSpecialWirePoint.NewInstallAddressID;
                    newSpecialWireAddress.BuchtID = (long)_InvestigatePossibility.BuchtID;
                    newSpecialWireAddress.TelephoneNo = specialWireAddress.TelephoneNo;
                    newSpecialWireAddress.CorrespondenceAddressID = specialWireAddress.CorrespondenceAddressID;
                    newSpecialWireAddress.SpecialTypeID = specialWireAddress.SpecialTypeID;
                    if (_changeLocationSpecialWire.NewOtherBuchtID != null)
                    {
                        newSpecialWireAddress.SecondBuchtID = _changeLocationSpecialWire.NewOtherBuchtID;
                    }
                    else
                    {
                        newSpecialWireAddress.SecondBuchtID = _changeLocationSpecialWire.OldOtherBuchtID;
                    }
                    newSpecialWireAddress.Detach();
                    DB.Save(newSpecialWireAddress, true);

                    DB.Delete<SpecialWireAddress>(specialWireAddress.BuchtID);

                    OldBucht.ConnectionID = null;
                    OldBucht.Detach();
                    DB.Save(OldBucht);

                    oldPostContact.Status = (int)DB.PostContactStatus.Free;
                    oldPostContact.Detach();
                    DB.Save(oldPostContact);

                    newPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                    newPostContact.Detach();
                    DB.Save(newPostContact);

                    wiring.ConnectionID = newPostContact.ID;
                }
                else
                {
                    if (_changeLocationSpecialWire.OldPostContactID != _InvestigatePossibility.PostContactID)
                    {
                        if (OldBucht.ConnectionID != _InvestigatePossibility.PostContactID)
                        {

                            PostContact oldPostContact = Data.PostContactDB.GetPostContactByID(_changeLocationSpecialWire.OldPostContactID ?? 0);
                            oldPostContact.Status = (int)DB.PostContactStatus.Free;
                            oldPostContact.Detach();
                            DB.Save(oldPostContact);


                            PostContact newPostContact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                            newPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                            newPostContact.Detach();
                            DB.Save(newPostContact);


                            OldBucht.ConnectionID = _InvestigatePossibility.PostContactID;
                            OldBucht.Detach();
                            DB.Save(OldBucht);
                        }


                    }
                }

                telephone.Detach();
                DB.Save(telephone, false);

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                _changeLocationSpecialWire.SetupDate = serverDate;
                _changeLocationSpecialWire.Detach();
                DB.Save(_changeLocationSpecialWire, false);

                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.WiringInsertDate = serverDate;
                wiring.Detach();
                DB.Save(wiring);

                ts.Complete();
            }




        }

        #endregion

        #region Reject
        private void RefundDepositDelete()
        {

            wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
            wiring.WiringInsertDate = DB.GetServerDate();
            IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            wiring.IssueWiringID = issueWiring.ID;
            wiring.Detach();
            DB.Save(wiring);



            PostContact contact = Data.PostContactDB.GetPostContactByID(_RefundDeposit.PostContactID ?? 0);
            contact.Status = (byte)DB.PostContactStatus.CableConnection;
            contact.Detach();
            DB.Save(contact);

            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
            _request.Detach();
            DB.Save(_request);
        }

        private void ChangeNoReject()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request, false);


                Bucht oldBucht = Data.BuchtDB.GetBuchetByID(_changeNo.OldBuchtID);


                if (oldBucht.Status == (byte)DB.BuchtStatus.Free)
                {

                    Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_changeNo.NewTelephoneNo);
                    Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_changeNo.OldTelephoneNo);
                    Bucht newBucht = DB.GetBuchtIDByTelephonNo((long)_changeNo.NewTelephoneNo);

                    ////////
                    oldBucht.ConnectionID = newBucht.ConnectionID;
                    oldBucht.Status = newBucht.Status;
                    oldBucht.ADSLStatus = newBucht.ADSLStatus;
                    oldBucht.Detach();
                    DB.Save(oldBucht);

                    ////////

                    ////////
                    newBucht.ConnectionID = null;
                    newBucht.Status = (byte)DB.BuchtStatus.Free;
                    //   newBucht.ADSLPortID = null;
                    newBucht.ADSLStatus = false;
                    //    newBucht.ADSLType = null;
                    newBucht.Detach();
                    DB.Save(newBucht);
                    ////////


                    oldTelephone.CustomerID = newTelephone.CustomerID;
                    oldTelephone.InstallAddressID = newTelephone.InstallAddressID;
                    oldTelephone.CorrespondenceAddressID = newTelephone.CorrespondenceAddressID;
                    oldTelephone.Status = newTelephone.Status;
                    oldTelephone.CauseOfCutID = newTelephone.CauseOfCutID;
                    oldTelephone.Detach();
                    DB.Save(oldTelephone);

                    ////////

                    newTelephone.CustomerID = null;
                    newTelephone.InstallAddressID = null;
                    newTelephone.CorrespondenceAddressID = null;
                    newTelephone.CauseOfCutID = null;

                    newTelephone.Detach();
                    DB.Save(newTelephone);
                    ////////


                    ///////صدور فرم سیم بندی

                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.OldBuchtID = null;
                    wiring.OldBuchtType = null;
                    wiring.BuchtType = null;
                    wiring.OldConnectionID = null;
                    wiring.ConnectionID = null;
                    wiring.NewTelephoneNo = null;
                    wiring.OldTelephoneNo = null;
                    wiring.RequestID = _request.ID;
                    wiring.Status = _request.StatusID;
                    wiring.MDFInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);
                    ///////
                }
                ts.Complete();
            }
        }

        void E1Reject()
        {
            using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);


                PostContact contact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                if (contact.Status == (byte)DB.PostContactStatus.CableConnection)
                {
                    contact.Status = (byte)DB.PostContactStatus.FullBooking;
                    contact.Detach();
                    DB.Save(contact);

                    Bucht bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                    bucht.ConnectionID = null;
                    bucht.Detach();
                    DB.Save(bucht);

                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.ConnectionID = null;
                    wiring.Detach();
                    DB.Save(wiring);
                }
                Subts.Complete();
            }
        }
        private void DayriDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (bucht != null)
                {
                    PostContact contact = Data.PostContactDB.GetPostContactByID((long)bucht.ConnectionID);
                    if (contact != null)
                    {
                        contact.Status = (byte)DB.PostContactStatus.FullBooking;
                        contact.Detach();
                        DB.Save(contact);
                    }

                    AssignmentInfo assingmentInfoPCM = assingmentInfo.Where(t => t.BuchtID == bucht.ID).SingleOrDefault();
                    PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(assingmentInfoPCM.PCMPortIDInBuchtTable ?? 0);
                    if (PCMPort != null)
                    {
                        PCMPort.Status = (byte)DB.PCMPortStatus.Reserve;
                        PCMPort.Detach();
                        DB.Save(PCMPort);
                    }

                    InstallRequest installReqeust = Data.InstallRequestDB.GetInstallRequestByRequestID(_request.ID);
                    installReqeust.InstallationDate = null;
                    installReqeust.Detach();
                    DB.Save(installReqeust, false);


                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_request.TelephoneNo ?? 0);
                    telephone.InstallationDate = null;
                    telephone.DischargeDate = null;
                    telephone.CauseOfTakePossessionID = null;
                    telephone.CustomerTypeID = null;
                    telephone.CustomerGroupID = null;
                    telephone.ChargingType = null;
                    telephone.PosessionType = null;
                    telephone.Detach();
                    DB.Save(telephone);

                    wiring.ConnectionID = null;
                    wiring.WiringDate = null;
                    wiring.WiringHour = null;

                    wiring.Detach();
                    DB.Save(wiring);
                }
                ts.Complete();
            }
        }

        private void VacateSpecialWireDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                PostContact postContact = Data.PostContactDB.GetPostContactByID(_vacateSpecialWire.PostContactID ?? 0);
                if (postContact.Status == (byte)DB.PostContactStatus.Free)
                {
                    postContact.Status = (byte)DB.PostContactStatus.CableConnection;
                    postContact.Detach();
                    DB.Save(postContact);

                    // Bucht OtherBucht = Data.BuchtDB.GetBuchetByID(_vacateSpecialWire.OtherBuchtID);
                    Bucht bucht = Data.BuchtDB.GetBuchetByID(_vacateSpecialWire.BuchtID);
                    bucht.ConnectionID = postContact.ID;
                    bucht.Detach();
                    DB.Save(bucht);

                    SpecialWireAddress specialWireAddress = new SpecialWireAddress();
                    specialWireAddress.BuchtID = _vacateSpecialWire.BuchtID;
                    specialWireAddress.InstallAddressID = _vacateSpecialWire.OldInstallAddressID;
                    specialWireAddress.CorrespondenceAddressID = _vacateSpecialWire.OldCorrespondenceAddressID;
                    specialWireAddress.TelephoneNo = (long)_request.TelephoneNo;
                    DB.Save(specialWireAddress, true);

                    Bucht sourceBucht = new Bucht();

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);
                    if (telephone.Status == (byte)DB.TelephoneStatus.Discharge || telephone.Status == (byte)DB.TelephoneStatus.Connecting)
                    {
                        telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                        telephone.Detach();
                        DB.Save(telephone, false);
                    }

                    Center sourceCenter = Data.SpecialWireDB.GetSourceCenterSpecialWireByTelephoneNo((long)_request.TelephoneNo, out sourceBucht);
                    if (sourceCenter.ID == _request.CenterID && _vacateSpecialWire.BuchtID == sourceBucht.ID)
                    {
                        telephone.SwitchPortID = _vacateSpecialWire.SwitchPortID;
                        telephone.InstallAddressID = _vacateSpecialWire.OldInstallAddressID;
                        telephone.CorrespondenceAddressID = _vacateSpecialWire.OldCorrespondenceAddressID;
                        telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                        telephone.CustomerID = _request.CustomerID;
                        telephone.Detach();
                        DB.Save(telephone, false);

                    }

                    _vacateSpecialWire.VacateDate = null;
                    _vacateSpecialWire.Detach();
                    DB.Save(_vacateSpecialWire);
                }
                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.ConnectionID = null;
                wiring.Detach();
                DB.Save(wiring);

                ts.Complete();
            }

        }

        private void ChangeLocationDelete()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {

                if (WiringStatusComboBox.SelectedValue == null)
                    throw new Exception("لطفا وضعیت را انتخاب کنید");

                PostContact contact;
                if (wiring != null)
                {
                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.WiringInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);
                }

                /// is itself change location
                /// without change post contact
                if (changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.itself && changeLocation.OldPostContactID == _InvestigatePossibility.PostContactID)
                {
                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                    telephone.InstallAddressID = changeLocation.OldInstallAddressID;
                    telephone.CorrespondenceAddressID = changeLocation.OldCorrespondenceAddressID;
                    telephone.Detach();
                    DB.Save(telephone);


                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    ts.Complete();
                    return;
                }
                // with change post contact
                else if (changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.itself && changeLocation.OldPostContactID != _InvestigatePossibility.PostContactID)
                {

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                    telephone.InstallAddressID = changeLocation.OldInstallAddressID;
                    telephone.CorrespondenceAddressID = changeLocation.OldCorrespondenceAddressID;
                    telephone.Detach();
                    DB.Save(telephone);

                    // Pass postContact return to Connected
                    _oldPostContact.Status = (byte)DB.PostContactStatus.CableConnection;
                    _oldPostContact.Detach();
                    DB.Save(_oldPostContact);

                    // ConnectionID of old bucht  retuen to the oldPostContactID
                    OldBucht.ConnectionID = changeLocation.OldPostContactID;
                    OldBucht.Detach();
                    DB.Save(OldBucht);

                    // new post contact return to reserved
                    _newPostContact.Status = (byte)DB.PostContactStatus.FullBooking;
                    _newPostContact.Detach();
                    DB.Save(_newPostContact);

                    _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    ts.Complete();
                    return;
                }
                /////
                // if request in source center
                if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == _request.CenterID)
                {

                    contact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);
                    if (contact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || contact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    {
                        contact.Status = (byte)DB.PostContactStatus.CableConnection;
                        PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(OldBucht.PCMPortID ?? 0);
                        if (PCMPort != null)
                        {
                            PCMPort.Status = (byte)DB.PCMPortStatus.Connection;
                            PCMPort.Detach();
                            DB.Save(PCMPort);
                        }
                    }
                    else
                    {
                        contact.Status = (byte)DB.PostContactStatus.FullBooking;
                        OldBucht.ConnectionID = changeLocation.OldPostContactID;
                        OldBucht.Detach();
                        DB.Save(OldBucht);
                    }
                    contact.Detach();
                    DB.Save(contact);
                }
                // if reqeust in targer center
                else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == _request.CenterID)
                {
                    contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                    contact.Status = (byte)DB.PostContactStatus.FullBooking;
                    contact.Detach();
                    DB.Save(contact);



                    Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);

                    Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);

                    // if request is accompanied by change name
                    if (changeLocation.NewCustomerID != null)
                    {
                        //change customer of telephone
                        newTelephone.CustomerID = changeLocation.NewCustomerID;
                    }
                    //


                    if (oldTelephone.Status == (int)DB.TelephoneStatus.Discharge)
                        throw new Exception("عملیات انجام شده است امکان رد نمی باشد");


                }
                // if reqeust is inside center
                else
                {
                    if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside && (specialCondition.EqualityOfBuchtTypeOptical || specialCondition.NotEqualityOfBuchtTypeOptical))
                    {
                        contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                        if (contact.Status == (byte)DB.PostContactStatus.CableConnection)
                        {
                            contact.Status = (byte)DB.PostContactStatus.FullBooking;
                            contact.Detach();
                            DB.Save(contact);

                            contact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);
                            contact.Status = (byte)DB.PostContactStatus.CableConnection;
                            contact.Detach();
                            DB.Save(contact);

                            // release old Bucht
                            Bucht OldBucht = Data.BuchtDB.GetBuchtByID((long)changeLocation.OldBuchtID);
                            OldBucht.Status = (byte)DB.BuchtStatus.Connection;
                            OldBucht.ConnectionID = null;
                            OldBucht.Detach();
                            DB.Save(OldBucht);

                            // release old Bucht
                            Bucht NewBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                            NewBucht.Status = (byte)DB.BuchtStatus.Reserve;
                            NewBucht.Detach();
                            DB.Save(NewBucht);

                            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                            telephone.InstallAddressID = changeLocation.OldInstallAddressID;
                            telephone.CorrespondenceAddressID = changeLocation.OldCorrespondenceAddressID;
                            telephone.Detach();
                            DB.Save(telephone);

                            Telephone Oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                            if (Oldtele.Status == (int)DB.TelephoneStatus.Discharge)
                                throw new Exception("عملیات انجام شده است امکان رد نمی باشد");

                            //Telephone newtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                            //Telephone Oldtele = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                            //Oldtele.InstallAddressID = changeLocation.OldInstallAddressID;
                            //Oldtele.CorrespondenceAddressID = changeLocation.OldCorrespondenceAddressID;
                            //Oldtele.CustomerTypeID = newtele.CustomerTypeID;
                            //Oldtele.CustomerGroupID = newtele.CustomerGroupID;
                            //Oldtele.ChargingType = newtele.ChargingType;
                            //Oldtele.PosessionType = newtele.PosessionType;
                            //Oldtele.Status = newtele.Status;
                            //Oldtele.CauseOfCutID = newtele.CauseOfCutID;
                            //Oldtele.Detach();
                            //DB.Save(Oldtele);
                        }
                    }
                    else
                    {
                        contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                        contact.Status = (byte)DB.PostContactStatus.FullBooking;
                        contact.Detach();
                        DB.Save(contact);

                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                        telephone.InstallAddressID = changeLocation.OldInstallAddressID;
                        telephone.CorrespondenceAddressID = changeLocation.OldCorrespondenceAddressID;
                        telephone.Detach();
                        DB.Save(telephone);

                        contact = Data.PostContactDB.GetPostContactByID((long)changeLocation.OldPostContactID);

                        if (contact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || contact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                        {
                            contact.Status = (byte)DB.PostContactStatus.CableConnection;
                            PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(OldBucht.PCMPortID ?? 0);
                            if (PCMPort != null)
                            {
                                PCMPort.Status = (byte)DB.PCMPortStatus.Connection;
                                PCMPort.Detach();
                                DB.Save(PCMPort);
                            }

                        }
                        else
                        {

                            contact.Status = (byte)DB.PostContactStatus.CableConnection;
                            OldBucht.ConnectionID = changeLocation.OldPostContactID;
                            OldBucht.Detach();
                            DB.Save(OldBucht);
                        }
                        contact.Detach();
                        DB.Save(contact);
                    }
                }

                //////////
                // if request is accompanied by change name
                if (changeLocation.NewCustomerID != null)
                {
                    // if request is accompanied by change number
                    if (changeLocation.NewTelephone != null)
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                        telephone.CustomerID = _request.CustomerID;
                        telephone.InstallationDate = null;
                        telephone.Detach();
                        DB.Save(telephone);
                    }
                    // if request not is accompanied by change number
                    else
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
                        telephone.CustomerID = _request.CustomerID;
                        telephone.Detach();
                        DB.Save(telephone);
                    }
                }
                //

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                ts.Complete();
            }
        }

        private void DischarginDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                PostContact contact = Data.PostContactDB.GetPostContactByID((long)takePossession.PostContactID);
                contact.Status = (byte)DB.PostContactStatus.CableConnection;
                contact.Detach();
                DB.Save(contact);


                // اکر بوخت به پی سی ام متصل نیست اتصال ان پاک میشود
                if (bucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                {
                    bucht.ConnectionID = (long)takePossession.PostContactID;
                    bucht.Detach();
                    DB.Save(bucht);
                }


                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                ts.Complete();
            }
        }

        private void SpecialWireDelete()
        {

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {

                PostContact postContact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);

                if (postContact.Status == (byte)DB.PostContactStatus.CableConnection)
                {

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_specialWire.TelephoneNo);
                    if (telephone.Status == (byte)DB.TelephoneStatus.Connecting)
                    {
                        telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                        telephone.CustomerTypeID = null;
                        telephone.CustomerGroupID = null;
                        telephone.ChargingType = null;
                        telephone.PosessionType = null;
                        telephone.Detach();
                        DB.Save(telephone);
                    }


                    postContact.Status = (byte)DB.PostContactStatus.FullBooking;
                    postContact.Detach();
                    DB.Save(postContact);

                    SpecialWireAddress specialWireAddress = SpecialWireAddressDB.GetSpecialWireAddressByBuchtID((long)_InvestigatePossibility.BuchtID);
                    if (specialWireAddress != null)
                    {
                        DB.Delete<SpecialWireAddress>(specialWireAddress.BuchtID);
                    }
                    wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                    wiring.ConnectionID = null;
                    wiring.Detach();
                    DB.Save(wiring);
                }

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                _specialWire.SetupDate = null;
                _specialWire.Detach();
                DB.Save(_specialWire, false);

                ts.Complete();
            }
        }

        private void ChangeLocationSpecialWireDelete()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {

                //Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_request.TelephoneNo);
                //if (telephone.Status == (byte)DB.TelephoneStatus.Connecting)
                //{
                //    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                //    telephone.DischargeDate = null;
                //    telephone.Detach();
                //    DB.Save(telephone, false);
                //}

                if (_changeLocationSpecialWire.SetupDate == null)
                    return;


                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                if (Data.ChangeLocationSpecialWireDB.IsLastRequest(_request))
                {


                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                    telephone.DischargeDate = null;
                    telephone.CauseOfTakePossessionID = null;


                }

                if (telephone.InstallAddressID == _changeLocationSpecialWire.InstallAddressID)
                {
                    telephone.InstallAddressID = _changeLocationSpecialWire.OldInstallAddressID;
                    telephone.CorrespondenceAddressID = _changeLocationSpecialWire.OldInstallAddressID;
                }


                telephone.Detach();
                DB.Save(telephone, false);
                //Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                //if (oldTelephone.Status == (byte)DB.TelephoneStatus.Connecting || oldTelephone.Status == (byte)DB.TelephoneStatus.Discharge)
                //{
                //    oldTelephone.Status = (byte)DB.TelephoneStatus.Reserv;
                //    oldTelephone.DischargeDate = null;
                //    oldTelephone.Detach();
                //    DB.Save(oldTelephone, false);
                //}

                //if (changeLocation.NewTelephone != null)
                //{
                //    Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                //    if (newTelephone.Status == (byte)DB.TelephoneStatus.Connecting)
                //    {
                //        newTelephone.Status = (byte)DB.TelephoneStatus.Reserv;
                //        newTelephone.InstallationDate = null;
                //        newTelephone.Detach();
                //        DB.Save(newTelephone);
                //    }



                //}

                Bucht OldBucht = Data.BuchtDB.GetBuchetByID((long)_changeLocationSpecialWire.OldBuchtID);
                if (specialCondition != null && !specialCondition.ChangeLocationInsider)
                {
                    PostContact oldPostContact = Data.PostContactDB.GetPostContactByID(_changeLocationSpecialWire.OldPostContactID ?? 0);
                    PostContact newPostContact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);

                    if (oldPostContact.Status == (int)DB.PostContactStatus.Free)
                    {



                        SpecialWireAddress specialWireAddress = Data.SpecialWireAddressDB.GetSpecialWireAddressByBuchtID((long)_InvestigatePossibility.BuchtID);
                        SpecialWireAddress newSpecialWireAddress = new SpecialWireAddress();
                        newSpecialWireAddress.InstallAddressID = _changeLocationSpecialWire.OldInstallAddressID;
                        newSpecialWireAddress.BuchtID = (long)_changeLocationSpecialWire.OldBuchtID;
                        newSpecialWireAddress.TelephoneNo = specialWireAddress.TelephoneNo;
                        newSpecialWireAddress.CorrespondenceAddressID = specialWireAddress.CorrespondenceAddressID;
                        newSpecialWireAddress.Detach();
                        DB.Save(newSpecialWireAddress, true);

                        DB.Delete<SpecialWireAddress>(specialWireAddress.BuchtID);

                        oldPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                        oldPostContact.Detach();
                        DB.Save(oldPostContact);

                        newPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                        newPostContact.Detach();
                        DB.Save(newPostContact);

                        _changeLocationSpecialWire.SetupDate = null;
                        _changeLocationSpecialWire.Detach();
                        DB.Save(_changeLocationSpecialWire, false);

                        OldBucht.ConnectionID = _changeLocationSpecialWire.OldPostContactID;
                        OldBucht.Detach();
                        DB.Save(OldBucht);

                    }


                }
                else
                {
                    if (_changeLocationSpecialWire.OldPostContactID != _InvestigatePossibility.PostContactID)
                    {
                        if (OldBucht.ConnectionID == _InvestigatePossibility.PostContactID)
                        {
                            OldBucht.ConnectionID = _changeLocationSpecialWire.OldPostContactID;
                            OldBucht.Detach();
                            DB.Save(OldBucht);
                        }


                    }
                }

                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                wiring = (AirNetInfoGroopBox.DataContext as WiringInfo).Wiring;
                wiring.ConnectionID = null;
                wiring.Detach();
                DB.Save(wiring);
                ts.Complete();
            }
        }

        #endregion

        #region Print

        public override bool Print()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                PrintCertificate();
                this.Cursor = Cursors.Arrow;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در متد چاپ");
                this.Cursor = Cursors.Arrow;
                return false;
            }

        }
        private void PrintCertificate()
        {
            DateTime currentDateTime = DB.GetServerDate();
            List<DayeriWiringNetwork> result = new List<DayeriWiringNetwork>();
            List<EnumItem> AllPersonType = Helper.GetEnumItem(typeof(DB.PersonType));
            StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(currentDateTime));
            StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time));

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.SpecialWireOtherPoint:

                case (byte)DB.RequestType.SpecialWire:

                    var res = ReportDB.GetSpecialWireWiringNetwork(new List<long> { _request.ID });

                    res.ForEach(t => t.PersonType = string.IsNullOrEmpty(t.PersonType) ? "" : AllPersonType.Find(i => i.ID == byte.Parse(t.PersonType)).Name);

                    SendPrint(res, DB.UserControlNames.SpecialWiringWiringNetworkReport, "گواهی سیم بندی شبکه هوایی سیم خصوصی ");
                    break;

                case (byte)DB.RequestType.VacateSpecialWire:

                    SendPrint(ReportDB.GetNetworkVacateSpecialWire(new List<long> { _request.ID }), DB.UserControlNames.NetworkVacateSpecialWireReport, "چاپ مامور شبکه هوایی تخلیه سیم خصوصی ");
                    break;

                case (byte)DB.RequestType.Dayri:

                    // List<PCMInfo> DayeriResult_PCM = new List<PCMInfo>();
                    result = ReportDB.GetDayeriWiringNetwork(new List<long> { _request.ID });
                    CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.DayeriWiringNetwork, dateVariable, timeVariable);
                    // List<EnumItem> PersonType = Helper.GetEnumItem(typeof(DB.PersonType));
                    //    InstallRequest Dayeri= InstallRequestDB.GetInstallRequestByID(_request.ID);

                    //    InvestigatePossibility investigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).Take(1).SingleOrDefault();

                    //    bucht = Data.BuchtDB.GetBuchtByID((long)investigatePossibility.BuchtID);
                    //if (bucht != null)
                    //{
                    //    PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    //    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //    if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //    {
                    //        PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                    //        List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                    //        List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

                    //        DayeriResult_PCM = ReportDB.GetPCMInfo(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);
                    //    }
                    //}
                    //foreach (DayeriWiringNetwork item in result)
                    //{
                    //    item.PersonType = string.IsNullOrEmpty(item.PersonType) ? "" : PersonType.Find(i => i.ID == byte.Parse(item.PersonType)).Name;
                    //    item.PCM = string.IsNullOrEmpty(item.PCM) ? "" : "*";
                    //    item.UNO = string.IsNullOrEmpty(item.UNO) ? "" : "*";
                    //}
                    //SendToPrint(result/*, DayeriResult_PCM*/);

                    break;

                case (byte)DB.RequestType.Dischargin:

                    //List<PCMInfo> DischargeResult_PCM = new List<PCMInfo>();
                    //result = ReportDB.GetDischargeWiringNetwork(_request.ID);
                    //List<EnumItem> DischargePersonType = Helper.GetEnumItem(typeof(DB.PersonType));
                    //TakePossession takePossession = TakePossessionDB.GetTakePossessionByID(_request.ID);
                    //bucht = Data.BuchtDB.GetBuchtByID((long)takePossession.BuchtID);


                    // if (bucht != null)
                    // {
                    //     PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //     if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //     {
                    //         PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                    //         List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                    //         List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();
                    //
                    //         DischargeResult_PCM = ReportDB.GetPCMInfo(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);
                    //     }
                    // }
                    //foreach (DayeriWiringNetwork item in result)
                    //{
                    //    item.PersonType = string.IsNullOrEmpty(item.PersonType) ? "" : DischargePersonType.Find(i => i.ID == byte.Parse(item.PersonType)).Name;
                    //    item.PCM = string.IsNullOrEmpty(item.PCM) ? "" : "*";
                    //    item.UNO = string.IsNullOrEmpty(item.UNO) ? "" : "*";
                    //}
                    // SendToPrintDischarge(result, DischargeResult_PCM);

                    result = ReportDB.GetDischargeWiringNetwork(new List<long> { _request.ID });
                    CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.DischargeWiringNetworkReport, dateVariable, timeVariable);
                    break;

                case (byte)DB.RequestType.Reinstall:

                    result = ReportDB.GetDayeriWiringNetwork(_request.ID);
                    List<EnumItem> PersonReInstallType = Helper.GetEnumItem(typeof(DB.PersonType));
                    foreach (DayeriWiringNetwork item in result)
                    {
                        item.PersonType = string.IsNullOrEmpty(item.PersonType) ? "" : PersonReInstallType.Find(i => i.ID == byte.Parse(item.PersonType)).Name;
                        item.PCM = string.IsNullOrEmpty(item.PCM) ? "" : "*";
                        item.UNO = string.IsNullOrEmpty(item.UNO) ? "" : "*";
                    }
                    // SendToReInstallPrint(result);
                    CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ReInstallWiringReport, dateVariable, timeVariable);

                    break;
                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                    SendPrint(ReportDB.GetChangeLocationNetworkSpecialWire(new List<long>() { _request.ID }), DB.UserControlNames.ChangeLocationNetworkSpecialWire, null);
                    break;
                case (byte)DB.RequestType.ChangeLocationCenterInside:
                    // نمایش اطلاعات پی سی ام
                    //List<PCMInfo> Result_PCM = new List<PCMInfo>();
                    //List<ChangeLocationCenterInfo> Result = new List<ChangeLocationCenterInfo>();
                    //bucht = Data.BuchtDB.GetBuchtByID((long)changeLocation.ReservBuchtID);
                    //if (bucht != null)
                    //{
                    //    PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    //    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //    if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //    {
                    //        PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                    //        List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                    //        List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();
                    //
                    //        Result_PCM = ReportDB.GetPCMInfo(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);
                    //    }
                    //}
                    //SendToChangeLocationCenterInside(Result, Result_PCM);
                    var _result = ReportDB.GetChangeLocationInsideCenterInfo(new List<long> { _request.ID });
                    CRM.Application.Local.ReportBase.SendToPrint(_result, (int)DB.UserControlNames.ChangeLocationCenterInsideWiringReport, dateVariable, timeVariable);
                    break;

                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                    // نمایش اطلاعات پی سی ام
                    List<ChangeLocationCenterInfo> ResultCenterToCenter_Destination = new List<ChangeLocationCenterInfo>();
                    List<ChangeLocationCenterInfo> ResultCenterToCenter_Source = new List<ChangeLocationCenterInfo>();
                    List<PCMInfo> PCM_result = new List<PCMInfo>();

                    List<ChangeLocationCenterInfo> ResultCenterTocenter = new List<ChangeLocationCenterInfo>();
                    Data.Request Request = RequestDB.GetRequestByID(_request.ID);

                    //درخواست در مبدا
                    if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == Request.CenterID)
                    {
                        ResultCenterTocenter = ReportDB.GetChangeLocationInsideCenterInfo(new List<long> { _request.ID });
                        ResultCenterToCenter_Source = ReportDB.GetChangeLocationInsideCenterInfo(new List<long> { _request.ID });
                        bucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                        if (bucht != null)
                        {
                            PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                            // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                            if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                                List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                                List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

                                PCM_result = ReportDB.GetPCMInfo(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);

                            }
                        }

                    }
                    //درخواست در مقصد
                    else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == Request.CenterID)
                    {
                        ResultCenterTocenter = ReportDB.GetChangeLocationInsideCenterInfo(new List<long> { _request.ID });
                        ResultCenterToCenter_Destination = ReportDB.GetChangeLocationInsideCenterInfo(new List<long> { _request.ID });
                        bucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                        if (bucht != null)
                        {
                            PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                            // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                            if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                                List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                                List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

                                PCM_result = ReportDB.GetPCMInfo(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);

                            }
                        }
                    }


                    SendToChangeLocationCenterToCenter(ResultCenterTocenter, PCM_result, ResultCenterToCenter_Source, ResultCenterToCenter_Destination);
                    break;
                case (byte)DB.RequestType.E1:
                    SendPrint(ReportDB.GetE1NetworkWire(new List<long> { _request.ID }), DB.UserControlNames.E1WiringNetworkReport, null);
                    break;
                case (byte)DB.RequestType.E1Link:
                    SendPrint(ReportDB.GetE1LinkNetworkWire(new List<long> { _request.ID }), DB.UserControlNames.E1LINKWiringNetworkReport, null);
                    break;
                default:
                    Folder.MessageBox.ShowInfo("گزارش مربوط به این روال در دست تهیه می باشد.");
                    break;
            }


        }
        private void SendPrint(IEnumerable objresult, DB.UserControlNames ReportID, string HeaderTitle)
        {
            if (objresult != null)
            {
                DateTime currentDateTime = DB.GetServerDate();

                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                string path = ReportDB.GetReportPath((int)ReportID);
                stiReport.Load(path);


                stiReport.CacheAllData = true;
                stiReport.RegData("result", "result", objresult);
                stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

                stiReport.Dictionary.Variables.Add("HeaderTitle", HeaderTitle);


                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("اطلاعاتی برای نمایش وجود ندارد");
            }
        }

        private void SendToPrint(object objresult, List<PCMInfo> PCMResult)
        {
            List<DayeriWiringNetwork> result = (List<DayeriWiringNetwork>)objresult;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.DayeriWiringNetwork);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("PCMResult", "PCMResult", PCMResult);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private void SendToReInstallPrint(object objresult)
        {
            List<DayeriWiringNetwork> result = (List<DayeriWiringNetwork>)objresult;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ReInstallWiringReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintDischarge(object objresult, List<PCMInfo> PCMResult)
        {
            List<DayeriWiringNetwork> result = (List<DayeriWiringNetwork>)objresult;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.DischargeWiringNetworkReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("PCMResult", "PCMResult", PCMResult);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToChangeLocationCenterInside(object objresult, object Result_PCM)
        {
            List<ChangeLocationCenterInfo> result = (List<ChangeLocationCenterInfo>)objresult;
            List<PCMInfo> result_PCM = (List<PCMInfo>)Result_PCM;

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationCenterInsideWiringReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("PCMresult", "PCMresult", result_PCM);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private void SendToChangeLocationCenterToCenter(object objresult, object Result_PCM, object Result_Source, object Result_Destination)
        {
            DateTime currentDateTime = DB.GetServerDate();
            List<ChangeLocationCenterInfo> result = (List<ChangeLocationCenterInfo>)objresult;
            List<PCMInfo> result_PCM = (List<PCMInfo>)Result_PCM;
            List<ChangeLocationCenterInfo> result_Destination = (List<ChangeLocationCenterInfo>)Result_Destination;
            List<ChangeLocationCenterInfo> result_Source = (List<ChangeLocationCenterInfo>)Result_Source;

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationCenterToCenterWiringReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("result_Source", "result_Source", result_Source);
            stiReport.RegData("result_Destination", "result_Destination", result_Destination);
            stiReport.RegData("result_PCM", "result_PCM", result_PCM);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        #endregion



    }
}