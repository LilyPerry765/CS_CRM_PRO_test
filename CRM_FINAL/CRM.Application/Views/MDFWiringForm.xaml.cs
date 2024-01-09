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
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using System.Collections;

namespace CRM.Application.Views
{
    public partial class MDFWiringForm : Local.RequestFormBase
    {
        #region fields and Properies

        private long _relatedRequestID;
        private long _customerAddressID;
        private bool _rejectCondition = false;
        private ChangeNo _ChangeNo { get; set; }
        private Bucht _oldOtherBucht;

        private Bucht _oldSecondOtherBucht;

        public Request _request { get; set; }
        public CRM.Data.E1 _e1 { get; set; }
        public CRM.Data.SpecialWire _SpecialWire { get; set; }
        public CRM.Data.VacateSpecialWire _VacateSpecialWire { get; set; }
        public CRM.Data.ChangeLocationSpecialWire _ChangeLocationSpecialWire { get; set; }

        public InvestigatePossibility _InvestigatePossibility { get; set; }

        public static List<Switch> switchList { get; set; }
        public static List<Telephone> telList { get; set; }
        // public static BuchtInfo buchtList { get; set; }
        public static SwitchCodeInfo switchCode { get; set; }
        public static Wiring wiring { get; set; }
        SpecialCondition specialCondition { get; set; }
        public static EnumItem sourceItem;
        private ChangeLocation _changeLocation;

        private TakePossession takePossession;
        List<CRM.Data.AssignmentInfo> assingmentInfo = new List<CRM.Data.AssignmentInfo>();
        IssueWiring issueWiring = new IssueWiring();
        Bucht bucht = new Bucht();
        Telephone oldTelephone = new Telephone();
        public static RefundDeposit _RefundDeposit;

        private long? _subID;

        int buchtType = 0;

        public UserControls.UserInfoSummary _userInfoSummary { get; set; }
        public UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        public UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        public UserControls.InstallInfoSummary _installInfoSummary { get; set; }
        public UserControls.InvestigateInfoSummary _investigateInfoSummary { get; set; }
        public UserControls.TechnicalSpecificationsOfADSL _TechnicalSpecificationsOfADSL { get; set; }
        public UserControls.ADSLPort _ADSLPort { get; set; }
        public UserControls.MDFChangeNoUC _MDFChangeNoUC { get; set; }

        public UserControls.ChangeLocationMDFWirigUserControl _ChangeLocationMDFWirig { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        public SpecialWireReportInfo _SpecialWireReportInfo;

        E1Link _e1Link { get; set; }

        #endregion

        #region Constructor
        public MDFWiringForm()
        {
            InitializeComponent();
            Initialize();
        }

        public MDFWiringForm(long id, long? subID)
            : this()
        {
            this._subID = subID;
            wiring = new Wiring();
            _request = Data.RequestDB.GetRequestByID(id);
            base.RequestID = _request.ID;
            switchList = Data.SwitchDB.GetSwitchByCenterID(_request.CenterID);
        }

        #endregion

        #region Method

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print };
        }

        private bool ChechForCreateWiring()
        {
            try
            {
                // issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            this.ResizeWindow();
        }

        public void LoadData()
        {

            // وجود اطلاعات سیم بندی  را برسی میکند اگر موجود نباشد اطلاعات را ایجاد میکند
            if (!ChechForCreateWiring())
                return;


            if (!(_request.RequestTypeID == (int)DB.RequestType.E1 || _request.RequestTypeID == (int)DB.RequestType.E1Link))
            {
                _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).Take(1).SingleOrDefault();
            }
            else
            {
                // in E1 switch case handeled
            }

            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;

            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;
            specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);


            MDFWiringResultComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            MDFWiringResultComboBox.SelectedValue = _request.StatusID;
            issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    DayeiInitialize();
                    break;
                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    ChangeLocationCenterInsideInitialize();
                    break;
                case (int)DB.RequestType.ADSLDischarge:
                    DischargeADSLInitialize();
                    break;
                case (int)DB.RequestType.Dischargin:
                    DischarginInitialize();

                    break;
                case (int)DB.RequestType.ADSLChangePort:
                    ChangePortADSLInitialize();
                    break;
                case (int)DB.RequestType.ChangeNo:
                    ChangeNoInitializ();
                    break;
                case (int)DB.RequestType.RefundDeposit:
                    RefundDepositInitializ();
                    break;
                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                    E1Load();
                    break;
                case (int)DB.RequestType.VacateE1:
                    VacateE1Load();
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    SpecialWireLoad();
                    break;
                case (int)DB.RequestType.VacateSpecialWire:
                    VacateSpecialWireLoad();
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    ChangeLocationSpecialWireLoad();
                    break;
            }

        }

        private void ChangeLocationSpecialWireLoad()
        {
            ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;
            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;
            OtherBuchtGroupBox.Visibility = Visibility.Visible;
            OtherBuchtGroupBox.IsEnabled = false;
            _oldOtherBucht = new Bucht();
            _oldSecondOtherBucht = new Bucht();

            DetailGroupBox.Visibility = Visibility.Visible;
            _ChangeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_request.ID);
            if (_ChangeLocationSpecialWire.OldOtherBuchtID != null)
            {
                _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_ChangeLocationSpecialWire.OldOtherBuchtID);
                buchtType = _oldOtherBucht.BuchtTypeID;

                BuchtTypeTextBox.Text = Data.BuchtTypeDB.GetBuchtTypeByID(_oldOtherBucht.BuchtTypeID).BuchtTypeName;
            }

            switchDetail.Visibility = Visibility.Collapsed;
            TelInfo.Visibility = Visibility.Collapsed;

            ////
            SecondMDFComboBox.ItemsSource = MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_request.CenterID);
            ////

            if (_oldOtherBucht != null && _oldOtherBucht.ID != 0)
            {
                ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                MDFComboBox.SelectedValue = connectionInfo.MDFID;
                MDFComboBox_SelectionChanged(null, null);

                ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                ConnectionColumnComboBox_SelectionChanged(null, null);

                ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                ConnectionRowComboBox_SelectionChanged(null, null);

                ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
            }

            _oldSecondOtherBucht = Data.BuchtDB.GetBuchetByID(_ChangeLocationSpecialWire.NewOtherBuchtID);
            if (_oldSecondOtherBucht != null)
            {
                ConnectionInfo SecondconnectionInfo = DB.GetConnectionInfoByBuchtID(_oldSecondOtherBucht.ID);
                buchtType = _oldSecondOtherBucht.BuchtTypeID;
                SecondBuchtTypeTextBox.Text = Data.BuchtTypeDB.GetBuchtTypeByID(_oldSecondOtherBucht.BuchtTypeID).BuchtTypeName;

                SecondMDFComboBox.SelectedValue = SecondconnectionInfo.MDFID;
                SecondMDFComboBox_SelectionChanged(null, null);

                SecondConnectionColumnComboBox.SelectedValue = SecondconnectionInfo.VerticalColumnID;
                SecondConnectionColumnComboBox_SelectionChanged(null, null);

                SecondConnectionRowComboBox.SelectedValue = SecondconnectionInfo.VerticalRowID;
                SecondConnectionRowComboBox_SelectionChanged(null, null);

                SecondConnectionBuchtComboBox.SelectedValue = SecondconnectionInfo.BuchtID;

                SecondBuchtTypeLabel.Visibility = Visibility.Visible;
                SecondBuchtTypeLabel.Content = "نوع بوخت جدید";
                SecondBuchtTypeTextBox.Visibility = Visibility.Visible;

                SecondMDFLabel.Visibility = Visibility.Visible;
                SecondMDFLabel.Content = "ام دی اف جدید";
                SecondMDFComboBox.Visibility = Visibility.Visible;

                SecondColumnLable.Visibility = Visibility.Visible;
                SecondColumnLable.Content = "ردیف جدید";
                SecondConnectionColumnComboBox.Visibility = Visibility.Visible;

                SecondRowLable.Visibility = Visibility.Visible;
                SecondRowLable.Content = "طبقه جدید";
                SecondConnectionRowComboBox.Visibility = Visibility.Visible;

                SecondConnectionLable.Visibility = Visibility.Visible;
                SecondConnectionLable.Content = "اتصالی جدید";
                SecondConnectionBuchtComboBox.Visibility = Visibility.Visible;
            }

            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();
            wiring.RequestID = _request.ID;

            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }



        private void SpecialWireLoad()
        {
            _SpecialWireReportInfo = new SpecialWireReportInfo();

            OtherBuchtGroupBox.IsEnabled = false;
            ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;
            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;



            OtherBuchtGroupBox.Visibility = Visibility.Visible;
            _oldOtherBucht = new Bucht();
            _oldSecondOtherBucht = new Bucht();

            DetailGroupBox.Visibility = Visibility.Visible;
            _SpecialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_request.ID);

            //milda doran
            //if (_InvestigatePossibility.BuchtID != null && _InvestigatePossibility.BuchtID != 0)

            //TODO:rad 13950613
            //اگر نقطه میانی باشد طبیعتاً کافوپستی برای آن تعیین نمیشود که رکورد بررسی امکانات بخورد
            if (_SpecialWire.SpecialWireType == (int)DB.SpecialWireType.General && _InvestigatePossibility.BuchtID != null && _InvestigatePossibility.BuchtID != 0)
            {
                ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(_InvestigatePossibility.BuchtID ?? 0);
                _SpecialWireReportInfo.Bucht = "ام دی اف: " + connectionInfo.MDF + "ردیف :" + connectionInfo.VerticalColumnNo + "طبقه :" + connectionInfo.VerticalRowNo + "اتصالی :" + connectionInfo.BuchtNo;
                _SpecialWireReportInfo.ColumnNo = connectionInfo.VerticalColumnNo;
                _SpecialWireReportInfo.RowNo = connectionInfo.VerticalRowNo;
                _SpecialWireReportInfo.BuchtNo = connectionInfo.BuchtNo;

            }
            Address address = AddressDB.GetAddressByID(_SpecialWire.InstallAddressID ?? 0);
            if (address != null)
            {
                _SpecialWireReportInfo.CustomerAddress = address.AddressContent;
                _SpecialWireReportInfo.PostalCode = address.PostalCode;
            }

            Customer customer = CustomerDB.GetCustomerByID(_request.CustomerID ?? 0);
            if (customer != null)
            {
                _SpecialWireReportInfo.CustomerName = customer.FirstNameOrTitle ?? "" + customer.LastName ?? "";
            }

            if (_SpecialWire.OtherBuchtID != null)
                _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_SpecialWire.OtherBuchtID);

            _SpecialWireReportInfo.BuchtType = BuchtTypeTextBox.Text = Data.BuchtTypeDB.GetBuchtTypeByID(_SpecialWire.BuchtType).BuchtTypeName;
            _SpecialWireReportInfo.SpecialType = Helper.GetEnumDescriptionByValue(typeof(DB.SpecialWireType), _SpecialWire.SpecialWireType);
            _SpecialWireReportInfo.Telephone = _request.TelephoneNo;


            switchDetail.Visibility = Visibility.Collapsed;
            TelInfo.Visibility = Visibility.Collapsed;

            ////
            SecondMDFComboBox.ItemsSource = MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_request.CenterID);
            ////

            if (_oldOtherBucht.ID != 0)
            {
                ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                _SpecialWireReportInfo.OtherBucht = "ام دی اف: " + connectionInfo.MDF + "ردیف :" + connectionInfo.VerticalColumnNo + "طبقه :" + connectionInfo.VerticalRowNo + "اتصالی :" + connectionInfo.BuchtNo;

                _SpecialWireReportInfo.OtherColumnNo = connectionInfo.VerticalColumnNo;
                _SpecialWireReportInfo.OtherRowNo = connectionInfo.VerticalRowNo;
                _SpecialWireReportInfo.OtherBuchtNo = connectionInfo.BuchtNo;

                MDFComboBox.SelectedValue = connectionInfo.MDFID;
                MDFComboBox_SelectionChanged(null, null);

                ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                ConnectionColumnComboBox_SelectionChanged(null, null);

                ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                ConnectionRowComboBox_SelectionChanged(null, null);

                ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
            }

            if (_SpecialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
            {
                _oldSecondOtherBucht = Data.BuchtDB.GetBuchetByID(_SpecialWire.SecondOtherBuchtID);
                if (_oldSecondOtherBucht != null)
                {
                    ConnectionInfo SecondconnectionInfo = DB.GetConnectionInfoByBuchtID(_oldSecondOtherBucht.ID);

                    _SpecialWireReportInfo.SecondOtherBucht = "ام دی اف: " + SecondconnectionInfo.MDF + "ردیف :" + SecondconnectionInfo.VerticalColumnNo + "طبقه :" + SecondconnectionInfo.VerticalRowNo + "اتصالی :" + SecondconnectionInfo.BuchtNo;

                    SecondMDFComboBox.SelectedValue = SecondconnectionInfo.MDFID;
                    SecondMDFComboBox_SelectionChanged(null, null);

                    SecondConnectionColumnComboBox.SelectedValue = SecondconnectionInfo.VerticalColumnID;
                    SecondConnectionColumnComboBox_SelectionChanged(null, null);

                    SecondConnectionRowComboBox.SelectedValue = SecondconnectionInfo.VerticalRowID;
                    SecondConnectionRowComboBox_SelectionChanged(null, null);

                    SecondConnectionBuchtComboBox.SelectedValue = SecondconnectionInfo.BuchtID;

                    SecondBuchtTypeLabel.Visibility = Visibility.Visible;
                    SecondBuchtTypeTextBox.Visibility = Visibility.Visible;

                    SecondMDFLabel.Visibility = Visibility.Visible;
                    SecondMDFComboBox.Visibility = Visibility.Visible;

                    SecondColumnLable.Visibility = Visibility.Visible;
                    SecondConnectionColumnComboBox.Visibility = Visibility.Visible;

                    SecondRowLable.Visibility = Visibility.Visible;
                    SecondConnectionRowComboBox.Visibility = Visibility.Visible;

                    SecondConnectionLable.Visibility = Visibility.Visible;
                    SecondConnectionBuchtComboBox.Visibility = Visibility.Visible;
                }

            }
            else
            {

                List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(_InvestigatePossibility.BuchtID).ToList();
                if (TelephonInfoByBuchtID != null)
                {
                    _SpecialWireReportInfo.Cabinet = TelephonInfoByBuchtID.Take(1).SingleOrDefault().CabinetNumber;
                    _SpecialWireReportInfo.CabinetInput = TelephonInfoByBuchtID.Take(1).SingleOrDefault().CabinetInputNumber;
                    _SpecialWireReportInfo.Post = TelephonInfoByBuchtID.Take(1).SingleOrDefault().PostNumber;
                    _SpecialWireReportInfo.PostContact = TelephonInfoByBuchtID.Take(1).SingleOrDefault().ConnectionNo.ToString();
                }
            }


            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();
            wiring.RequestID = _request.ID;

            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };


            //    MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }
        private void VacateSpecialWireLoad()
        {
            ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;
            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;
            OtherBuchtGroupBox.Visibility = Visibility.Visible;
            OtherBuchtGroupBox.IsEnabled = false;
            _oldOtherBucht = new Bucht();

            DetailGroupBox.Visibility = Visibility.Visible;
            _VacateSpecialWire = Data.VacateSpecialWireDB.GetVacateSpecialWireByRequestID(_request.ID);


            switchDetail.Visibility = Visibility.Collapsed;
            TelInfo.Visibility = Visibility.Collapsed;

            ////
            SecondMDFComboBox.ItemsSource = MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_request.CenterID);
            ////

            if (_VacateSpecialWire.SpecialTypeID == (int)DB.SpecialWireType.Middle)
            {
                _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.BuchtID);


                if (_oldOtherBucht != null && _oldOtherBucht.ID != 0)
                {
                    BuchtTypeTextBox.Text = Data.BuchtTypeDB.GetBuchtTypeByID(_oldOtherBucht.BuchtTypeID).BuchtTypeName;
                    ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                    MDFComboBox.SelectedValue = connectionInfo.MDFID;
                    MDFComboBox_SelectionChanged(null, null);

                    ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                    ConnectionColumnComboBox_SelectionChanged(null, null);

                    ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                    ConnectionRowComboBox_SelectionChanged(null, null);

                    ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                }

                _oldSecondOtherBucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.OtherBuchtID);
                if (_oldSecondOtherBucht != null)
                {
                    SecondBuchtTypeTextBox.Text = Data.BuchtTypeDB.GetBuchtTypeByID(_oldSecondOtherBucht.BuchtTypeID).BuchtTypeName;
                    ConnectionInfo SecondconnectionInfo = DB.GetConnectionInfoByBuchtID(_oldSecondOtherBucht.ID);

                    SecondMDFComboBox.SelectedValue = SecondconnectionInfo.MDFID;
                    SecondMDFComboBox_SelectionChanged(null, null);

                    SecondConnectionColumnComboBox.SelectedValue = SecondconnectionInfo.VerticalColumnID;
                    SecondConnectionColumnComboBox_SelectionChanged(null, null);

                    SecondConnectionRowComboBox.SelectedValue = SecondconnectionInfo.VerticalRowID;
                    SecondConnectionRowComboBox_SelectionChanged(null, null);

                    SecondConnectionBuchtComboBox.SelectedValue = SecondconnectionInfo.BuchtID;

                    SecondBuchtTypeLabel.Visibility = Visibility.Visible;
                    SecondBuchtTypeTextBox.Visibility = Visibility.Visible;

                    SecondMDFLabel.Visibility = Visibility.Visible;
                    SecondMDFComboBox.Visibility = Visibility.Visible;

                    SecondColumnLable.Visibility = Visibility.Visible;
                    SecondConnectionColumnComboBox.Visibility = Visibility.Visible;

                    SecondRowLable.Visibility = Visibility.Visible;
                    SecondConnectionRowComboBox.Visibility = Visibility.Visible;

                    SecondConnectionLable.Visibility = Visibility.Visible;
                    SecondConnectionBuchtComboBox.Visibility = Visibility.Visible;
                }

            }
            else
            {
                if (_VacateSpecialWire.OtherBuchtID != null)
                {
                    _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.OtherBuchtID);

                    BuchtTypeTextBox.Text = Data.BuchtTypeDB.GetBuchtTypeByID(_oldOtherBucht.BuchtTypeID).BuchtTypeName;
                }

                if (_oldOtherBucht != null && _oldOtherBucht.ID != 0)
                {
                    ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                    MDFComboBox.SelectedValue = connectionInfo.MDFID;
                    MDFComboBox_SelectionChanged(null, null);

                    ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                    ConnectionColumnComboBox_SelectionChanged(null, null);

                    ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                    ConnectionRowComboBox_SelectionChanged(null, null);

                    ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                }
            }

            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();
            wiring.RequestID = _request.ID;

            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }
        private void ConnectionBuchtTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_request.RequestTypeID == (byte)DB.RequestType.Dayri)
            {
                //  EnumItem item = ConnectionBuchtTypeComboBox.SelectedItem as EnumItem;
            }

        }

        #endregion

        #region InitializeMethods

        void DayeiInitialize()
        {
            _installInfoSummary = new InstallInfoSummary(_request.ID);
            InstallInfoUC.Content = _installInfoSummary;
            InstallInfoUC.DataContext = _installInfoSummary;

            _investigateInfoSummary = new InvestigateInfoSummary(_request.ID);
            InvestigateInfoUC.Content = _investigateInfoSummary;
            InvestigateInfoUC.DataContext = _investigateInfoSummary;

            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

            DetailGroupBox.Visibility = Visibility.Visible;


            wiring = Data.WiringDB.GetInfoWiringByInvestigatePossibility(_investigateInfoSummary.investigate.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();

            switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));
            switchDetail.DataContext = switchCode;
            TelInfo.DataContext = switchCode;

            wiring.InvestigatePossibilityID = _investigateInfoSummary.investigate.ID;
            wiring.RequestID = _request.ID;


            bucht = Data.BuchtDB.GetBuchtByID((long)_investigateInfoSummary.investigate.BuchtID);


            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        private void E1Load()
        {

            E1InfoSummaryUC.Visibility = Visibility.Visible;

            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;

            _E1InfoSummary = new E1InfoSummary(_request.ID, _subID);
            _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
            E1InfoSummaryUC.Content = _E1InfoSummary;
            E1InfoSummaryUC.DataContext = _E1InfoSummary;

            _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);

            switchCode = DB.GetSwitchCodeInfoWithUsingSwitchPort(_e1.TelephoneNo ?? 0);
            switchDetail.DataContext = switchCode;
            TelInfo.DataContext = switchCode;

            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();
            wiring.RequestID = _request.ID;

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

                MDFWiringInfoGroupBox.Visibility = Visibility.Collapsed;

                _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                _ChangeLocationMDFWirig.subID = _subID;
                _ChangeLocationMDFWirig.OtherBuchtGroupBox.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.AcessBuchtGroupBox.Visibility = Visibility.Visible;
                DetailGroupBox.Content = _ChangeLocationMDFWirig;
                DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

                DetailGroupBox.Visibility = Visibility.Visible;

                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print };
            }
            else
            {
                DetailGroupBox.Visibility = Visibility.Collapsed;
            }


            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        private void VacateE1Load()
        {

            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;
            E1InfoSummaryUC.Visibility = Visibility.Collapsed;

            _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);

            switchCode = DB.GetSwitchCodeInfoWithUsingSwitchPort(_request.TelephoneNo ?? 0);
            switchDetail.DataContext = switchCode;
            TelInfo.DataContext = switchCode;

            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();
            wiring.RequestID = _request.ID;

            if (_subID != null)
            {
                _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);
                bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                MDFWiringInfoGroupBox.Visibility = Visibility.Collapsed;

                _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
                _ChangeLocationMDFWirig.subID = _subID;
                _ChangeLocationMDFWirig.OtherBuchtGroupBox.Visibility = Visibility.Visible;
                _ChangeLocationMDFWirig.AcessBuchtGroupBox.Visibility = Visibility.Visible;
                DetailGroupBox.Content = _ChangeLocationMDFWirig;
                DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

                DetailGroupBox.Visibility = Visibility.Visible;
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
            }
            else
            {
                DetailGroupBox.Visibility = Visibility.Collapsed;
            }

            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        void ChangeLocationCenterInsideInitialize()
        {

            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

            DetailGroupBox.Visibility = Visibility.Visible;

            ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;

            InstallInfoUC.Visibility = Visibility.Collapsed;



            _changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_request.ID);



            // TODO : چون در تغییر مکان مرکز به مرکز در مبدا سیم بندی نیز صادر میشود 
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();

            switchCode = DB.GetSwitchCodeInfo(_changeLocation.NewTelephone ?? _changeLocation.OldTelephone ?? 0);
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));
            switchDetail.DataContext = switchCode;
            TelInfo.DataContext = switchCode;

            wiring.RequestID = _request.ID;

            Bucht buchtReserve = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
            wiring.BuchtType = (byte)buchtReserve.BuchtTypeID;
            oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_changeLocation.OldTelephone);
            bucht = Data.BuchtDB.GetBuchtByID((long)_changeLocation.OldBuchtID);

            ////////
            // اگر درخواست در مبدا است 
            if (_changeLocation.SourceCenter != null && _changeLocation.SourceCenter == _request.CenterID)
            {

            }
            // اگر درخواست در مقصد است
            else if (_changeLocation.SourceCenter != null && _changeLocation.TargetCenter == _request.CenterID)
            {

            }
            else
            {

            }

            //////////



            wiring.MDFStatus = _request.StatusID;

            MDFWiringInfoGroupBox.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        void DischargeADSLInitialize()
        {

            ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;


            InstallInfoUC.Visibility = Visibility.Collapsed;
            MDFWiringInfoGroupBox.Visibility = Visibility.Collapsed;
            switchDetail.Visibility = Visibility.Collapsed;

            // Will be depleted before your arrival.
            Data.ADSLDischarge aDSLDischarge = Data.ADSLDischargeDB.GetADSLDischargeByID(_request.ID);
            Data.ADSL aDSL = new Data.ADSL();// Data.ADSLDB.GetADSLByTelephoneNo(aDSLDischarge.TelephoneNo);
            if (Data.TechnicalSpecificationsOfADSLDB.Search(_request.ID).CustomerPort == null)
            {
                base.ActionIDs.Remove((byte)DB.NewAction.Save);
                base.ActionIDs.Remove((byte)DB.NewAction.Forward);
                base.ActionIDs.Remove((byte)DB.NewAction.Deny);
            }
            DetailGroupBox.Visibility = Visibility.Visible;

            _TechnicalSpecificationsOfADSL = new UserControls.TechnicalSpecificationsOfADSL(_request.ID);
            DetailGroupBox.Content = _TechnicalSpecificationsOfADSL;
            DetailGroupBox.DataContext = _TechnicalSpecificationsOfADSL;
        }

        void DischarginInitialize()
        {
            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

            DetailGroupBox.Visibility = Visibility.Visible;

            ChooseNoGroupBox.Visibility = Visibility.Visible;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;

            InstallInfoUC.Visibility = Visibility.Collapsed;

            takePossession = new TakePossession();
            takePossession = Data.TakePossessionDB.GetTakePossessionByID(_request.ID);


            // TODO : چون در تغییر مکان مرکز به مرکز در مبدا سیم بندی نیز صادر میشود 
            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();

            switchCode = DB.GetSwitchCodeInfo(_request.TelephoneNo ?? 0);
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));
            switchDetail.DataContext = switchCode;
            TelInfo.DataContext = switchCode;

            wiring.RequestID = _request.ID;

            oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);

            bucht = Data.BuchtDB.GetBuchtByID((long)takePossession.BuchtID);
            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };
        }

        void ChangePortADSLInitialize()
        {
            ChooseNoGroupBox.Visibility = Visibility.Collapsed;
            InvestigateInfoUC.Visibility = Visibility.Collapsed;

            InstallInfoUC.Visibility = Visibility.Collapsed;
            MDFWiringInfoGroupBox.Visibility = Visibility.Collapsed;
            switchDetail.Visibility = Visibility.Collapsed;

            // Will be depleted before your arrival.

            DetailGroupBox.Visibility = Visibility.Visible;
            _ADSLPort = new UserControls.ADSLPort(_request.ID);
            DetailGroupBox.Content = _ADSLPort;
            DetailGroupBox.DataContext = _ADSLPort;
        }

        void ChangeNoInitializ()
        {
            DetailGroupBox.Visibility = Visibility.Visible;
            ChooseNoGroupBox.Visibility = Visibility.Collapsed;

            _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_request.ID);
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));

            InvestigateInfoUC.Visibility = Visibility.Collapsed;
            InstallInfoUC.Visibility = Visibility.Collapsed;

            WiringNoLable.Visibility = Visibility.Collapsed;
            WiringNoTextBox.Visibility = Visibility.Collapsed;

            WiringTypeLable.Visibility = Visibility.Collapsed;
            WiringTypeComboBox.Visibility = Visibility.Collapsed;

            DetailGroupBox.Header = "جزئیات سیم بندی تعویض شماره";
            _MDFChangeNoUC = new MDFChangeNoUC(_request.ID);
            DetailGroupBox.Content = _MDFChangeNoUC;
            DetailGroupBox.DataContext = _MDFChangeNoUC;

            DetailGroupBox.Visibility = Visibility.Visible;

            switchCode = DB.GetSwitchCodeInfo(_ChangeNo.NewTelephoneNo ?? 0);
            switchDetail.DataContext = switchCode;

            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();

            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        void RefundDepositInitializ()
        {
            _ChangeLocationMDFWirig = new ChangeLocationMDFWirigUserControl(_request.ID);
            DetailGroupBox.Content = _ChangeLocationMDFWirig;
            DetailGroupBox.DataContext = _ChangeLocationMDFWirig;

            DetailGroupBox.Visibility = Visibility.Visible;

            InstallInfoUC.Visibility = Visibility.Collapsed;

            InvestigateInfoUC.Visibility = Visibility.Collapsed;

            wiring = Data.WiringDB.GetWiringByIssueWiringID(issueWiring.ID);
            wiring.MDFWiringHour = DB.GetServerDate().ToShortTimeString();
            wiring.MDFWiringDate = DB.GetServerDate();

            _RefundDeposit = Data.RefundDepositDB.GetRefundDepositByID(_request.ID);
            switchCode = DB.GetSwitchCodeInfo(_RefundDeposit.TelephoneNo ?? 0);

            bucht = Data.BuchtDB.GetBuchetByID(_RefundDeposit.BuchtID);
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));

            switchDetail.DataContext = switchCode;
            TelInfo.DataContext = switchCode;
            wiring.RequestID = _request.ID;

            MDFWiringInfo.DataContext = new WiringInfo { Wiring = wiring, IssueWiring = issueWiring };

        }

        #endregion

        #region SaveMethods

        void Dayeisave()
        {
            using (TransactionScope tsParent = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                wiring.BuchtID = bucht.ID;
                wiring.MDFInsertDate = DB.GetServerDate();

                bucht.Status = (byte)DB.BuchtStatus.Connection;

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;

                wiring.Detach();
                DB.Save(wiring);

                bucht.Detach();
                DB.Save(bucht);

                _request.Detach();
                DB.Save(_request);

                //WiringDB.SaveMDFWiring(wiring, bucht, (byte)wiring.ConnectionType, _request);
                tsParent.Complete();
            }
        }

        void ChangeLocationSave()
        {

            using (TransactionScope ts = new TransactionScope())
            {
                // اگر درخواست در مبدا است 
                if (_changeLocation.SourceCenter != null && _changeLocation.SourceCenter == _request.CenterID)
                {
                    // release old Bucht
                    Bucht OldBucht = Data.BuchtDB.GetBuchtByID((long)_changeLocation.OldBuchtID);
                    OldBucht.Status = (byte)DB.BuchtStatus.Free;
                    OldBucht.ADSLStatus = false;
                    OldBucht.SwitchPortID = null;
                    OldBucht.Detach();
                    DB.Save(OldBucht);



                    wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                    wiring.BuchtID = OldBucht.ID;
                    wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                    wiring.MDFInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);



                }

                // اگر درخواست در مقصد است
                else if (_changeLocation.SourceCenter != null && _changeLocation.TargetCenter == _request.CenterID)
                {
                    // Connect New Bucht
                    Bucht newBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                    newBucht.Status = (byte)DB.BuchtStatus.Connection;
                    newBucht.Detach();
                    DB.Save(newBucht);

                    wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                    wiring.BuchtID = newBucht.ID;
                    wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                    wiring.MDFInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);

                }

                else
                {

                    Bucht newBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                    if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside && !specialCondition.EqualityOfBuchtTypeCusromerSide)
                    {
                     
                        Cabinet cabinet = Data.CabinetDB.GetCabinetByBuchtID(newBucht.ID);
                        if (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
                        {
                            // oldTelephone.Status = (byte)DB.TelephoneStatus.Discharge;
                            // oldTelephone.SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(oldTelephone);
                            //oldTelephone.CustomerID = null;
                            //oldTelephone.InstallAddressID = null;
                            //oldTelephone.CorrespondenceAddressID = null;
                            //oldTelephone.Detach();
                            //DB.Save(oldTelephone);

                            newBucht.ADSLStatus = bucht.ADSLStatus;
                            newBucht.BuchtIDConnectedOtherBucht = bucht.BuchtIDConnectedOtherBucht;
                            // newBucht.ADSLType = bucht.ADSLType;

                            bucht.Status = (byte)DB.BuchtStatus.Free;
                            bucht.BuchtIDConnectedOtherBucht = null;
                            bucht.SwitchPortID = null;
                            bucht.Detach();
                            DB.Save(bucht);


                            newBucht.Status = (byte)DB.BuchtStatus.Connection;
                            newBucht.Detach();
                            DB.Save(newBucht);



                            wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                            wiring.BuchtID = newBucht.ID;
                            wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                            wiring.MDFInsertDate = DB.GetServerDate();
                            wiring.Detach();
                            DB.Save(wiring);
                        }
                        else
                        {

                            //oldTelephone.CustomerID = null;
                            //oldTelephone.InstallAddressID = null;
                            //oldTelephone.CorrespondenceAddressID = null;
                            //oldTelephone.Detach();
                            //DB.Save(oldTelephone);

                            newBucht.ADSLStatus = bucht.ADSLStatus;
                            bucht.Status = (byte)DB.BuchtStatus.Free;
                            //TODO:rad 13950713
                            bucht.SwitchPortID = null;
                            //***********************************
                            bucht.Detach();
                            DB.Save(bucht);


                            newBucht.Status = (byte)DB.BuchtStatus.Connection;
                            newBucht.Detach();
                            DB.Save(newBucht);



                            wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                            wiring.BuchtID = newBucht.ID;
                            wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                            wiring.MDFInsertDate = DB.GetServerDate();
                            wiring.Detach();
                            DB.Save(wiring);
                        }
                    }
                    else
                    {

                        // اگر تعویض شماره جدید ثبت شده باشد تلفن جدید دایر میشود
                        //if (_changeLocation.NewTelephone != null)
                        //{
                        //    oldTelephone.CustomerID = null;
                        //    oldTelephone.InstallAddressID = null;
                        //    oldTelephone.CorrespondenceAddressID = null;
                        //    oldTelephone.Detach();
                        //    DB.Save(oldTelephone);
                        //}
                        //else
                        //{
                        //    oldTelephone.InstallAddressID = _changeLocation.NewInstallAddressID;
                        //    oldTelephone.CorrespondenceAddressID = _changeLocation.NewCorrespondenceAddressID;

                        //    //oldTelephone.Status = (byte)DB.TelephoneStatus.Connecting;
                        //    oldTelephone.Detach();
                        //    DB.Save(oldTelephone);

                           
                        //}

                        newBucht.SwitchPortID = oldTelephone.SwitchPortID;

                        newBucht.ADSLStatus = bucht.ADSLStatus;

                        bucht.Status = (byte)DB.BuchtStatus.Free;
                        bucht.SwitchPortID = null;
                        bucht.Detach();
                        DB.Save(bucht);


                        newBucht.Status = (byte)DB.BuchtStatus.Connection;
                        newBucht.Detach();
                        DB.Save(newBucht);



                        wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                        wiring.BuchtID = newBucht.ID;
                        wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                        wiring.MDFInsertDate = DB.GetServerDate();
                        wiring.Detach();
                        DB.Save(wiring);
                    }
                }
                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);


                ts.Complete();
            }
        }

        void DischargeADSLSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Data.ADSLDischarge aDSLDischarge = Data.ADSLDischargeDB.GetADSLDischargeByID(_request.ID);
                Data.ADSL aDSL = new Data.ADSL();// Data.ADSLDB.GetADSLByTelephoneNo(aDSLDischarge.TelephoneNo);
                Data.ADSLPort aDSLPort = Data.ADSLPortDB.GetADSlPortByID((long)aDSL.ADSLPortID);
                Data.Bucht Inputbucht = Data.BuchtDB.GetBuchetByID(aDSLPort.InputBucht);
                Data.Bucht Outbucht = Data.BuchtDB.GetBuchetByID(aDSLPort.OutBucht);
                Data.Bucht CustomerBucht = Data.BuchtDB.GetBuchetByID(Outbucht.BuchtIDConnectedOtherBucht);


                // Released the InputBucht
                Inputbucht.Status = (byte)DB.BuchtStatus.ADSLFree;
                Inputbucht.ADSLStatus = false;
                Inputbucht.SwitchPortID = null;
                Inputbucht.Detach();
                DB.Save(Inputbucht);

                // Released the OutPutBucht
                Outbucht.Status = (byte)DB.BuchtStatus.ADSLFree;
                Outbucht.ADSLStatus = false;
                Outbucht.BuchtIDConnectedOtherBucht = null;
                Outbucht.Detach();
                DB.Save(Outbucht);

                // Released The CustomerBucht
                CustomerBucht.ADSLStatus = false;
                CustomerBucht.BuchtIDConnectedOtherBucht = null;
                CustomerBucht.Detach();
                DB.Save(CustomerBucht);

                // Relaeased the ADSLPort 
                aDSLPort.Status = (byte)DB.ADSLPortStatus.Free;
                aDSLPort.Detach();
                DB.Save(aDSLPort);

                //relaeased the ADSL
                aDSL.Status = (byte)DB.ADSLStatus.Discharge;
                aDSL.Detach();
                DB.Save(aDSL);


                ts.Complete();
            }
        }

        void DischarginSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // Release bucht switchPort
                if (bucht.BuchtTypeID == (int)DB.BuchtType.InLine)
                {
                    bucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;

                    PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByBuchtID(takePossession.BuchtID ?? 0);
                    if (PCMPort != null)
                    {
                        PCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                        PCMPort.Detach();
                        DB.Save(PCMPort);
                    }
                }
                else
                {
                    bucht.Status = (byte)DB.BuchtStatus.Free;
                }

                bucht.SwitchPortID = null;
                bucht.ADSLStatus = false;
                bucht.Detach();
                DB.Save(bucht);


                // save wiring
                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.OldTelephoneNo = oldTelephone.TelephoneNo;
                wiring.OldConnectionID = bucht.ConnectionID;
                wiring.OldBuchtID = bucht.ID;
                wiring.OldBuchtType = (byte?)bucht.BuchtTypeID;
                wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);
                ts.Complete();
            }
        }
        void RefundDepositSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                RefundDeposit refundDesposit = Data.RefundDepositDB.GetRefundDepositByID(_request.ID);


                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.BuchtID = bucht.ID;
                wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);

                if (bucht.BuchtTypeID == (byte)DB.BuchtType.InLine || bucht.BuchtTypeID == (byte)DB.BuchtType.OutLine)
                {
                    bucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;

                    PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByBuchtID(_RefundDeposit.BuchtID ?? 0);
                    if (PCMPort != null)
                    {
                        PCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                        PCMPort.Detach();
                        DB.Save(PCMPort);
                    }

                }
                else
                {
                    bucht.Status = (byte)DB.BuchtStatus.Free;
                    bucht.ConnectionID = null;
                }


                bucht.SwitchPortID = null;
                bucht.Detach();
                DB.Save(bucht);


                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_RefundDeposit.TelephoneNo);
                //  وضعیت تلفن در مرحله دایری آزاد میشود
                //  telephone.SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(telephone);
                telephone.CustomerID = null;
                telephone.InstallAddressID = null;
                telephone.CorrespondenceAddressID = null;
                telephone.Detach();
                DB.Save(telephone);

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();

                _request.Detach();
                DB.Save(_request);


                ts.Complete();
            }

        }

        void ChangePortADSLSave()
        {

            using (TransactionScope ts = new TransactionScope())
            {

                if (_ADSLPort.PortInfo.DataContext != null)
                {
                    ADSLPortsInfo portInfo = _ADSLPort.PortInfo.DataContext as ADSLPortsInfo;

                    if (portInfo.InputBucht == null)
                        throw new Exception("برای این پورت بوخت ورودی تعیین نشده است ! ");

                    //if (portInfo.OutputBucht == null)
                    //    throw new Exception("برای این پورت بوخت خروجی تعیین نشده است ! ");

                    Data.ADSLPort newPort = Data.ADSLPortDB.GetADSlPortByID(portInfo.ID);
                    Data.ADSLChangePort1 oldADSLChangePort = Data.ADSLChangePortDB.GetADSLChangePortByID(_request.ID);
                    Data.ADSL oldADSL = null;// Data.ADSLDB.GetADSLByTelephoneNo(oldADSLChangePort.TelephoneNo);

                    if (oldADSL.ADSLPortID != null)
                    {
                        if (oldADSL.ADSLPortID != newPort.ID)
                        {
                            Data.ADSLPort oldPort = Data.ADSLPortDB.GetADSlPortByID((long)oldADSL.ADSLPortID);
                            oldPort.Status = (byte)DB.ADSLPortStatus.Free;
                            oldPort.Detach();
                            DB.Save(oldPort);

                            newPort.Status = (byte)DB.ADSLPortStatus.Install;
                            newPort.Detach();
                            DB.Save(newPort);

                            //oldADSLChangePort.FormerPort = oldPort.ID;
                            oldADSLChangePort.Detach();
                            DB.Save(oldADSLChangePort);

                            Data.Bucht oldInputbucht = Data.BuchtDB.GetBuchetByID(oldPort.InputBucht);
                            Data.Bucht oldOutbucht = Data.BuchtDB.GetBuchetByID(oldPort.OutBucht);
                            Data.Bucht oldCustomerBucht = Data.BuchtDB.GetBuchetByID(oldOutbucht.BuchtIDConnectedOtherBucht);

                            Data.Bucht newInputbucht = Data.BuchtDB.GetBuchetByID(newPort.InputBucht);
                            Data.Bucht newOutbucht = Data.BuchtDB.GetBuchetByID(newPort.OutBucht);


                            /////////////
                            newInputbucht.SwitchPortID = oldInputbucht.SwitchPortID;

                            //  oldInputbucht.SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(oldTelephone);
                            oldInputbucht.ADSLStatus = false;
                            oldInputbucht.Status = (byte)DB.BuchtStatus.ConnectedToSpliter;
                            oldInputbucht.Detach();
                            DB.Save(oldInputbucht);

                            newInputbucht.ADSLStatus = true;
                            newInputbucht.Status = (byte)DB.BuchtStatus.ADSLConnection;
                            newInputbucht.Detach();
                            DB.Save(newInputbucht);
                            //////////////




                            /////////////
                            newOutbucht.BuchtIDConnectedOtherBucht = oldOutbucht.BuchtIDConnectedOtherBucht;

                            oldOutbucht.BuchtIDConnectedOtherBucht = null;
                            oldOutbucht.ADSLStatus = false;
                            oldOutbucht.Status = (byte)DB.BuchtStatus.ConnectedToSpliter;
                            oldOutbucht.Detach();
                            DB.Save(oldOutbucht);

                            newOutbucht.ADSLStatus = true;
                            newOutbucht.Status = (byte)DB.BuchtStatus.ADSLConnection;
                            newOutbucht.Detach();
                            DB.Save(newOutbucht);
                            ////////////


                            ////////////
                            oldCustomerBucht.BuchtIDConnectedOtherBucht = newInputbucht.ID;
                            oldCustomerBucht.Detach();
                            DB.Save(oldCustomerBucht);
                            ////////////
                        }
                    }
                    oldADSL.ADSLPortID = newPort.ID;
                    oldADSL.Detach();
                    DB.Save(oldADSL);
                }




                else
                    throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");

                ts.Complete();
            }
        }

        void ChangeNoSave()
        {

            using (TransactionScope ts = new TransactionScope())
            {
                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request, false);

                Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_ChangeNo.NewTelephoneNo);
                Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_ChangeNo.OldTelephoneNo);

                Bucht oldBucht = Data.BuchtDB.GetBuchetByID(_ChangeNo.OldBuchtID);
                oldBucht.SwitchPortID = newTelephone.SwitchPortID;
                oldBucht.Detach();
                DB.Save(oldBucht);


                //newTelephone.CustomerID = _ChangeNo.CustomerID;
                //newTelephone.InstallAddressID = _ChangeNo.InstallAddressID;
                //newTelephone.CorrespondenceAddressID = _ChangeNo.CorrespondenceAddressID;
                //newTelephone.Detach();
                //DB.Save(newTelephone);

                //////////

                //// oldTelephone.SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(oldTelephone);
                //oldTelephone.CustomerID = null;
                //oldTelephone.InstallAddressID = null;
                //oldTelephone.CorrespondenceAddressID = null;
                //oldTelephone.Detach();
                //DB.Save(oldTelephone);
                //////////


                ///////صدور فرم سیم بندی

                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.OldBuchtID = oldBucht.ID;
                wiring.OldBuchtType = (byte?)oldBucht.BuchtTypeID;
                wiring.BuchtType = (byte?)bucht.BuchtTypeID;
                wiring.OldConnectionID = oldBucht.ConnectionID;
                wiring.ConnectionID = bucht.ConnectionID;
                wiring.NewTelephoneNo = _ChangeNo.NewTelephoneNo;
                wiring.OldTelephoneNo = _ChangeNo.OldTelephoneNo;
                wiring.RequestID = _request.ID;
                wiring.Status = _request.StatusID;
                wiring.IssueWiringID = issueWiring.ID;
                wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);
                ///////

                ts.Complete();
            }

        }

        private void E1save()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                if (_subID != null)
                {

                    if (_e1Link.MDFDate == null)
                    {

                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_request.TelephoneNo ?? 0);

                        Bucht otherBucht = null;

                        if (_e1Link.OtherBuchtID != null)
                        {
                            otherBucht = Data.BuchtDB.GetBuchetByID(_e1Link.OtherBuchtID);
                            otherBucht.SwitchPortID = telephone.SwitchPortID;
                            otherBucht.Status = (int)DB.BuchtStatus.Connection;
                            bucht.BuchtIDConnectedOtherBucht = otherBucht.ID;

                            otherBucht.Detach();
                            DB.Save(otherBucht);
                        }

                        if (_e1Link.AcessBuchtID != null)
                        {
                            Bucht acessBucht = Data.BuchtDB.GetBuchetByID(_e1Link.AcessBuchtID);
                            acessBucht.Status = (int)DB.BuchtStatus.Connection;
                            acessBucht.SwitchPortID = telephone.SwitchPortID;
                            if (otherBucht != null)
                            {
                                otherBucht.BuchtIDConnectedOtherBucht = acessBucht.ID;
                            }
                            acessBucht.Detach();
                            DB.Save(acessBucht);
                        }

                        bucht.SwitchPortID = telephone.SwitchPortID;
                        bucht.Status = (int)DB.BuchtStatus.Connection;
                        bucht.Detach();
                        DB.Save(bucht);

                        _e1Link.MDFDate = DB.GetServerDate();
                        _e1Link.Detach();
                        DB.Save(_e1Link);

                    }

                }
                else
                {
                    _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;

                    _request.Detach();
                    DB.Save(_request);

                    wiring.MDFInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);
                }

                ts.Complete();
            }
        }

        #endregion

        #region RejectMethods
        void DayeiDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                bucht.Status = (byte)DB.BuchtStatus.Reserve;
                bucht.Detach();
                DB.Save(bucht);


                wiring.BuchtID = null;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);



                ts.Complete();
            }

        }

        private void ChangeNoDelete()
        {

            Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_ChangeNo.NewTelephoneNo);
            Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_ChangeNo.OldTelephoneNo);
            Bucht oldBucht = Data.BuchtDB.GetBuchetByID(_ChangeNo.OldBuchtID);

            ////////
            oldBucht.SwitchPortID = (int)_ChangeNo.OldSwitchPortID;
            oldBucht.Detach();
            DB.Save(oldBucht);
            ////////

            ////////
            newTelephone.CustomerID = null;
            newTelephone.InstallAddressID = null;
            newTelephone.CorrespondenceAddressID = null;
            newTelephone.Detach();
            DB.Save(newTelephone);
            ////////

            ////////
            oldTelephone.CustomerID = _ChangeNo.CustomerID;
            oldTelephone.InstallAddressID = _ChangeNo.InstallAddressID;
            oldTelephone.CorrespondenceAddressID = _ChangeNo.CorrespondenceAddressID;
            oldTelephone.Detach();
            DB.Save(oldTelephone);
            ////////
        }

        private void DischarginDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                if (bucht.BuchtTypeID == (byte)DB.BuchtType.InLine || bucht.BuchtTypeID == (byte)DB.BuchtType.OutLine)
                {


                    PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByBuchtID(takePossession.BuchtID ?? 0);
                    if (PCMPort != null)
                    {
                        PCMPort.Status = (byte)DB.PCMPortStatus.Connection;
                        PCMPort.Detach();
                        DB.Save(PCMPort);
                    }

                }
                bucht.Status = (byte)DB.BuchtStatus.Connection;
                bucht.SwitchPortID = (int)takePossession.SwitchPortID;
                bucht.Detach();
                DB.Save(bucht);


                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                ts.Complete();
            }
        }
        void RefundDepositDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                RefundDeposit refundDesposit = Data.RefundDepositDB.GetRefundDepositByID(_request.ID);
                Bucht bucht = Data.BuchtDB.GetBuchetByID(refundDesposit.BuchtID);


                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.BuchtID = bucht.ID;
                wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);

                if (bucht.Status == (byte)DB.BuchtStatus.Free || bucht.Status == (byte)DB.BuchtStatus.ConnectedToPCM)
                {
                    if (bucht.BuchtTypeID == (byte)DB.BuchtType.InLine || bucht.BuchtTypeID == (byte)DB.BuchtType.OutLine)
                    {


                        PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByBuchtID(_RefundDeposit.BuchtID ?? 0);
                        if (PCMPort != null)
                        {
                            PCMPort.Status = (byte)DB.PCMPortStatus.Connection;
                            PCMPort.Detach();
                            DB.Save(PCMPort);
                        }

                    }
                    else
                    {
                        bucht.ConnectionID = refundDesposit.PostContactID;
                    }

                    bucht.Status = (byte)DB.BuchtStatus.Connection;
                    bucht.SwitchPortID = refundDesposit.SwitchPortID;
                    bucht.Detach();
                    DB.Save(bucht);


                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_RefundDeposit.TelephoneNo);
                    //  وضعیت تلفن در مرحله دایری آزاد میشود
                    // telephone.SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(telephone);
                    telephone.Status = (int)DB.TelephoneStatus.Connecting;
                    telephone.CustomerID = _request.CustomerID;
                    telephone.InstallAddressID = refundDesposit.InstallAddressID;
                    telephone.CorrespondenceAddressID = refundDesposit.CorrespondenceAddressID;
                    telephone.Detach();
                    DB.Save(telephone);
                }
                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);


                ts.Complete();
            }

        }

        private void ChangeLocationDelete()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                // اگر درخواست در مبدا است 
                if (_changeLocation.SourceCenter != null && _changeLocation.SourceCenter == _request.CenterID)
                {
                    // RollBack old Bucht
                    Bucht OldBucht = Data.BuchtDB.GetBuchtByID((long)_changeLocation.OldBuchtID);
                    OldBucht.Status = (byte)DB.BuchtStatus.Connection;
                    OldBucht.SwitchPortID = _changeLocation.OldSwitchPortID;
                    OldBucht.Detach();
                    DB.Save(OldBucht);

                    if (wiring != null)
                    {
                        wiring.BuchtID = null;
                        wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                        wiring.MDFInsertDate = DB.GetServerDate();
                        wiring.Detach();
                        DB.Save(wiring);
                    }



                }

                // اگر درخواست در مقصد است
                else if (_changeLocation.SourceCenter != null && _changeLocation.TargetCenter == _request.CenterID)
                {
                    // RollBack New Bucht
                    Bucht newBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                    newBucht.Status = (byte)DB.BuchtStatus.Reserve;
                    newBucht.Detach();
                    DB.Save(newBucht);

                    if (wiring != null)
                    {
                        wiring.BuchtID = null;
                        wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                        wiring.MDFInsertDate = DB.GetServerDate();
                        wiring.Detach();
                        DB.Save(wiring);
                    }

                }

                else
                {

                    Bucht newBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                    if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside)
                    {
                        if (oldTelephone.InstallAddressID == _changeLocation.NewInstallAddressID || oldTelephone.InstallAddressID == null)
                        {

                            oldTelephone.InstallAddressID = _changeLocation.OldInstallAddressID;
                            oldTelephone.CustomerID = _request.CustomerID;
                            oldTelephone.CorrespondenceAddressID = _changeLocation.OldCorrespondenceAddressID;
                            if (oldTelephone.CutDate.HasValue && !oldTelephone.ConnectDate.HasValue)
                            {
                                oldTelephone.Status = (byte)DB.TelephoneStatus.Cut;
                            }
                            else
                            {
                                oldTelephone.Status = (byte)DB.TelephoneStatus.Connecting;
                            }

                            oldTelephone.Detach();
                            DB.Save(oldTelephone);

                            bucht.ADSLStatus = newBucht.ADSLStatus;
                            bucht.Status = (byte)DB.BuchtStatus.Connection;
                            bucht.SwitchPortID = _changeLocation.OldSwitchPortID;
                            bucht.ConnectionID = _changeLocation.OldPostContactID;
                            bucht.Detach();
                            DB.Save(bucht);


                            newBucht.Status = (byte)DB.BuchtStatus.Reserve;
                            newBucht.SwitchPortID = null;
                            newBucht.Detach();
                            DB.Save(newBucht);



                            wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                            wiring.BuchtID = null;
                            wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                            wiring.MDFInsertDate = DB.GetServerDate();
                            wiring.Detach();
                            DB.Save(wiring);
                        }
                    }
                    else
                    {
                        oldTelephone.InstallAddressID = _changeLocation.OldInstallAddressID;
                        oldTelephone.CustomerID = _request.CustomerID;
                        oldTelephone.CorrespondenceAddressID = _changeLocation.OldCorrespondenceAddressID;
                        oldTelephone.Status = (byte)DB.TelephoneStatus.Connecting;
                        oldTelephone.Detach();
                        DB.Save(oldTelephone);
                        newBucht.SwitchPortID = null;

                        newBucht.Status = (byte)DB.BuchtStatus.Reserve;
                        newBucht.Detach();
                        DB.Save(newBucht);


                        bucht.Status = (byte)DB.BuchtStatus.Connection;
                        bucht.SwitchPortID = _changeLocation.OldSwitchPortID;
                        bucht.Detach();
                        DB.Save(bucht);





                        if (wiring != null)
                        {
                            wiring.BuchtID = null;
                            wiring.MDFStatus = (int)MDFWiringResultComboBox.SelectedValue;
                            wiring.MDFInsertDate = DB.GetServerDate();
                            wiring.Detach();
                            DB.Save(wiring);
                        }
                    }

                }




                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);


                ts.Complete();
            }
        }

        private void E1Reject()
        {
            using (TransactionScope Subts = new TransactionScope())
            {

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;



                Bucht otherBucht = Data.BuchtDB.GetBuchetByID(_e1Link.OtherBuchtID);
                if (otherBucht.SwitchPortID != null)
                {
                    otherBucht.SwitchPortID = null;
                    otherBucht.Status = (int)DB.BuchtStatus.Reserve;

                    Bucht acessBucht = Data.BuchtDB.GetBuchetByID(_e1Link.AcessBuchtID);
                    acessBucht.Status = (int)DB.BuchtStatus.Reserve;
                    otherBucht.BuchtIDConnectedOtherBucht = null;

                    bucht.BuchtIDConnectedOtherBucht = null;
                    bucht.Status = (int)DB.BuchtStatus.Reserve;


                    otherBucht.Detach();
                    DB.Save(otherBucht);

                    acessBucht.Detach();
                    DB.Save(acessBucht);

                    bucht.Detach();
                    DB.Save(bucht);
                }
                //Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_e1.TelephoneNo ?? 0);
                //if (bucht.SwitchPortID != null)
                //{
                //    bucht.SwitchPortID = null;
                //    bucht.Detach();
                //    DB.Save(bucht);

                //    wiring.BuchtID = null;
                //    wiring.Detach();
                //    DB.Save(wiring);
                //}
                Subts.Complete();
            }
        }

        #endregion

        #region ActionMethods


        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }

            try
            {

                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Reinstall:
                        Dayeisave();
                        break;

                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                        ChangeLocationSave();
                        break;

                    case (byte)DB.RequestType.ADSLDischarge:
                        DischargeADSLSave();
                        break;

                    case (byte)DB.RequestType.Dischargin:
                        DischarginSave();
                        break;

                    case (byte)DB.RequestType.ADSLChangePort:
                        ChangePortADSLSave();
                        break;

                    case (byte)DB.RequestType.ChangeNo:
                        ChangeNoSave();
                        break;

                    case (byte)DB.RequestType.RefundDeposit:
                        RefundDepositSave();
                        break;
                    case (byte)DB.RequestType.E1:
                    case (byte)DB.RequestType.E1Link:
                        E1save();
                        break;
                    case (byte)DB.RequestType.VacateE1:
                        VacateE1save();
                        break;
                    case (byte)DB.RequestType.SpecialWire:
                    case (byte)DB.RequestType.SpecialWireOtherPoint:
                        SpecialWiresave();
                        break;
                    case (byte)DB.RequestType.VacateSpecialWire:
                        VacateSpecialWiresave();
                        break;
                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        ChangeLocationSpecialWiresave();
                        break;
                }
                ShowSuccessMessage("ذخیره انجام شد");

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }

            base.Confirm();

            return IsSaveSuccess;
        }

        private void VacateE1save()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                if (_subID != null)
                {
                    if (_e1Link.MDFDate == null)
                    {
                        _e1Link.MDFDate = DB.GetServerDate();
                        _e1Link.Detach();
                        DB.Save(_e1Link);
                    }
                }
                else
                {
                    _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);
                }
                ts.Complete();
            }
        }

        private void ChangeLocationSpecialWiresave()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Bucht OldBucht = Data.BuchtDB.GetBuchetByID((long)_ChangeLocationSpecialWire.OldBuchtID);
                Bucht NewBucht = Data.BuchtDB.GetBuchetByID((long)_InvestigatePossibility.BuchtID);

                if (_ChangeLocationSpecialWire.NewOtherBuchtID != null)
                {
                    Bucht newOtherBucht = Data.BuchtDB.GetBuchetByID(_ChangeLocationSpecialWire.NewOtherBuchtID);
                    newOtherBucht.Status = (byte)DB.BuchtStatus.Connection;
                    newOtherBucht.Detach();
                    DB.Save(newOtherBucht);


                }

                OldBucht.Status = (byte)DB.BuchtStatus.Free;
                NewBucht.Status = (byte)DB.BuchtStatus.Connection;

                NewBucht.ADSLStatus = OldBucht.ADSLStatus;
                OldBucht.ADSLStatus = false;

                NewBucht.BuchtIDConnectedOtherBucht = OldBucht.BuchtIDConnectedOtherBucht;
                OldBucht.BuchtIDConnectedOtherBucht = null;

                NewBucht.SwitchPortID = OldBucht.SwitchPortID;
                OldBucht.SwitchPortID = null;

                //NewBucht.ADSLType = OldBucht.ADSLType;
                //OldBucht.ADSLType = null;
                //NewBucht.ADSLPortID = OldBucht.ADSLPortID;
                //OldBucht.ADSLPortID = null;
                NewBucht.PortNo = OldBucht.PortNo;
                OldBucht.PortNo = null;

                OldBucht.Detach();
                DB.Save(OldBucht);

                NewBucht.Detach();
                DB.Save(NewBucht);

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request);



                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.BuchtID = NewBucht.ID;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);

                ts.Complete();
            }
        }

        private void DeleteChangeLocationSpecialWire()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Bucht OldBucht = Data.BuchtDB.GetBuchetByID((long)_ChangeLocationSpecialWire.OldBuchtID);
                Bucht NewBucht = Data.BuchtDB.GetBuchetByID((long)_InvestigatePossibility.BuchtID);

                if (_ChangeLocationSpecialWire.NewOtherBuchtID != null)
                {
                    Bucht newOtherBucht = Data.BuchtDB.GetBuchetByID(_ChangeLocationSpecialWire.NewOtherBuchtID);
                    newOtherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                    newOtherBucht.Detach();
                    DB.Save(newOtherBucht);


                }

                if (NewBucht.Status == (byte)DB.BuchtStatus.Connection)
                {
                    OldBucht.Status = (byte)DB.BuchtStatus.Connection;
                    NewBucht.Status = (byte)DB.BuchtStatus.Reserve;

                    OldBucht.ADSLStatus = NewBucht.ADSLStatus;
                    NewBucht.ADSLStatus = false;

                    OldBucht.BuchtIDConnectedOtherBucht = NewBucht.BuchtIDConnectedOtherBucht;
                    NewBucht.BuchtIDConnectedOtherBucht = null;

                    OldBucht.SwitchPortID = NewBucht.SwitchPortID;
                    NewBucht.SwitchPortID = null;

                    //OldBucht.ADSLType = NewBucht.ADSLType;
                    //NewBucht.ADSLType = null;
                    //OldBucht.ADSLPortID = NewBucht.ADSLPortID;
                    //NewBucht.ADSLPortID = null;
                    OldBucht.PortNo = NewBucht.PortNo;
                    NewBucht.PortNo = null;

                    OldBucht.Detach();
                    DB.Save(OldBucht);

                    NewBucht.Detach();
                    DB.Save(NewBucht);
                }
                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request);



                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.BuchtID = null;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);

                ts.Complete();
            }
        }

        private void SpecialWiresave()
        {


            Bucht Bucht = new Data.Bucht();

            Bucht OtherBucht = Data.BuchtDB.GetBuchetByID((long)_SpecialWire.OtherBuchtID);



            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_SpecialWire.TelephoneNo);
            if (telephone.SwitchPortID == null)
                throw new Exception("پورت تلفن یافت نشد لطفا اطلاعات پورت را اصلاح کنید");

            if (_request.MainRequestID == null)
            {
                telephone.InstallAddressID = _SpecialWire.InstallAddressID;
                telephone.CorrespondenceAddressID = _SpecialWire.CorrespondenceAddressID;
                telephone.CustomerID = _request.CustomerID;
                telephone.Detach();
                DB.Save(telephone, false);
            }


            OtherBucht.Status = (byte)DB.BuchtStatus.Connection;
            OtherBucht.SwitchPortID = telephone.SwitchPortID;
            OtherBucht.Detach();
            DB.Save(OtherBucht, false);



            if (_SpecialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
            {
                _SpecialWire.SetupDate = DB.ServerDate();

                if (Data.SpecialWireDB.IsLastRequest(_request))
                {
                    telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                    telephone.Detach();
                    DB.Save(telephone);
                }

                SpecialWireAddress specialWireAddress = SpecialWireAddressDB.GetSpecialWireAddressByBuchtID((long)_SpecialWire.OtherBuchtID);
                if (specialWireAddress == null)
                {
                    specialWireAddress = new SpecialWireAddress();
                    specialWireAddress.TelephoneNo = (long)_SpecialWire.TelephoneNo;
                    specialWireAddress.BuchtID = (long)_SpecialWire.OtherBuchtID;
                    specialWireAddress.SecondBuchtID = _SpecialWire.SecondOtherBuchtID;
                    specialWireAddress.SpecialTypeID = _SpecialWire.SpecialWireType;
                    specialWireAddress.Detach();
                    DB.Save(specialWireAddress, true);
                }
                else
                {
                    specialWireAddress.TelephoneNo = (long)_SpecialWire.TelephoneNo;
                    specialWireAddress.SecondBuchtID = _SpecialWire.OtherBuchtID;
                    specialWireAddress.SecondBuchtID = _SpecialWire.SecondOtherBuchtID;
                    specialWireAddress.SpecialTypeID = _SpecialWire.SpecialWireType;
                    specialWireAddress.Detach();
                    DB.Save(specialWireAddress, false);
                }


                Bucht SecondOtherBucht = Data.BuchtDB.GetBuchetByID((long)_SpecialWire.SecondOtherBuchtID);
                SecondOtherBucht.Status = (byte)DB.BuchtStatus.Connection;
                SecondOtherBucht.SwitchPortID = telephone.SwitchPortID;
                SecondOtherBucht.Detach();
                DB.Save(SecondOtherBucht, false);

            }
            else
            {
                Bucht = Data.BuchtDB.GetBuchetByID((long)_InvestigatePossibility.BuchtID);
                Bucht.BuchtIDConnectedOtherBucht = OtherBucht.ID;
                Bucht.SwitchPortID = telephone.SwitchPortID;
                Bucht.Status = (byte)DB.BuchtStatus.Connection;
                Bucht.Detach();
                DB.Save(Bucht, false);
            }

            _SpecialWire.Detach();
            DB.Save(_SpecialWire, false);

            _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
            _request.ModifyDate = DB.GetServerDate();
            _request.Detach();
            DB.Save(_request);



            wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
            if (Bucht.ID != 0)
            {
                wiring.BuchtID = Bucht.ID;
            }
            wiring.MDFInsertDate = DB.GetServerDate();
            wiring.Detach();
            DB.Save(wiring);
        }

        private void PrivateWireDelete()
        {
            using (TransactionScope child = new TransactionScope(TransactionScopeOption.Required))
            {
                if (_SpecialWire.OtherBuchtID != null)
                {

                    if (_SpecialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
                    {
                        Bucht SecondOtherBucht = Data.BuchtDB.GetBuchetByID((long)_SpecialWire.SecondOtherBuchtID);
                        SecondOtherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                        SecondOtherBucht.SwitchPortID = null;
                        SecondOtherBucht.Detach();
                        DB.Save(SecondOtherBucht, false);
                        _SpecialWire.OtherBuchtID = null;

                    }
                    else
                    {
                        Bucht bucht = Data.BuchtDB.GetBuchetByID((long)_InvestigatePossibility.BuchtID);
                        bucht.Status = (byte)DB.BuchtStatus.Reserve;
                        bucht.BuchtIDConnectedOtherBucht = null;
                        bucht.Detach();
                        DB.Save(bucht);
                    }

                    _SpecialWire.Detach();
                    DB.Save(_SpecialWire);

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_SpecialWire.TelephoneNo);
                    if (_request.MainRequestID == null)
                    {
                        telephone.InstallAddressID = null;
                        telephone.CorrespondenceAddressID = null;
                        telephone.CustomerID = null;
                        telephone.Detach();
                        DB.Save(telephone, false);
                    }


                    wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                    wiring.BuchtID = null;
                    wiring.MDFInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);
                }

                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);


                child.Complete();
            }
        }


        private void VacateSpecialWiresave()
        {
            using (TransactionScope child = new TransactionScope(TransactionScopeOption.Required))
            {


                Bucht OtherBucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.OtherBuchtID);
                Bucht Bucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.BuchtID);

                if (OtherBucht != null)
                {
                    OtherBucht.Status = (byte)DB.BuchtStatus.Free;
                    OtherBucht.SwitchPortID = null;
                    OtherBucht.Detach();
                    DB.Save(OtherBucht, false);
                }

                Bucht.BuchtIDConnectedOtherBucht = null;
                Bucht.SwitchPortID = null;
                bucht.SwitchPortID = null;
                Bucht.Status = (byte)DB.BuchtStatus.Free;
                Bucht.Detach();
                DB.Save(Bucht, false);


                if (_VacateSpecialWire.SpecialTypeID == (int)DB.SpecialWireType.Middle)
                {

                    if (Data.SpecialWireAddressDB.ExsistBuchtInSpecialWireAddress(_VacateSpecialWire.BuchtID))
                        DB.Delete<SpecialWireAddress>(_VacateSpecialWire.BuchtID);

                    _VacateSpecialWire.VacateDate = DB.GetServerDate();
                }


                _VacateSpecialWire.Detach();
                DB.Save(_VacateSpecialWire);
                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request);


                wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                wiring.BuchtID = _VacateSpecialWire.BuchtID;
                wiring.MDFInsertDate = DB.GetServerDate();
                wiring.Detach();
                DB.Save(wiring);

                child.Complete();

            }
        }

        private void VacatePrivateWireDelete()
        {
            using (TransactionScope child = new TransactionScope(TransactionScopeOption.Required))
            {
                Bucht OtherBucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.OtherBuchtID);
                Bucht Bucht = Data.BuchtDB.GetBuchetByID(_VacateSpecialWire.BuchtID);
                if (OtherBucht.Status == (byte)DB.BuchtStatus.Free)
                {
                    OtherBucht.Status = (byte)DB.BuchtStatus.Connection;
                    OtherBucht.SwitchPortID = _VacateSpecialWire.SwitchPortID;
                    OtherBucht.Detach();
                    DB.Save(OtherBucht, false);

                    Bucht.BuchtIDConnectedOtherBucht = OtherBucht.ID;
                    Bucht.Status = (byte)DB.BuchtStatus.Connection;
                    Bucht.Detach();
                    DB.Save(Bucht, false);

                    wiring = (MDFWiringInfo.DataContext as WiringInfo).Wiring;
                    wiring.BuchtID = null;
                    wiring.MDFInsertDate = DB.GetServerDate();
                    wiring.Detach();
                    DB.Save(wiring);
                }
                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.ModifyDate = DB.GetServerDate();
                _request.Detach();
                DB.Save(_request);





                _request.StatusID = (int)MDFWiringResultComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);


                child.Complete();
            }

        }



        public override bool Deny()
        {

            try
            {
                base.RequestID = _request.ID;
                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Reinstall:
                        DayeiDelete();
                        break;
                    case (byte)DB.RequestType.Dischargin:
                        DischarginDelete();
                        break;
                    case (byte)DB.RequestType.ChangeNo:
                        ChangeNoDelete();
                        break;
                    case (byte)DB.RequestType.RefundDeposit:
                        RefundDepositDelete();
                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationDelete();
                        break;
                    case (byte)DB.RequestType.SpecialWire:
                        PrivateWireDelete();
                        break;
                    case (byte)DB.RequestType.E1:
                        E1Reject();
                        break;
                    case (byte)DB.RequestType.VacateSpecialWire:
                        VacatePrivateWireDelete();
                        break;
                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        DeleteChangeLocationSpecialWire();
                        break;

                }

                if (_rejectCondition == false)
                {

                    IsRejectSuccess = true;
                }
                else
                {
                    IsRejectSuccess = false;
                    Data.WorkFlowDB.SetNextState(DB.Action.RejectConditional, _request.StatusID, _request.ID);
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;
        }


        public override bool Forward()
        {

            try
            {
                Save();
                if (IsSaveSuccess == true)
                {
                    if (_request.RequestTypeID == (int)DB.RequestType.E1 || _request.RequestTypeID == (int)DB.RequestType.VacateE1)
                    {
                        if (Data.E1LinkDB.CheckALLMDF(_request.ID))
                        {
                            IsForwardSuccess = true;
                        }
                        else
                        {
                            throw new Exception("برای همه درخواست ها اطلاعات ام دی اف ذخیره نشده است");
                        }
                    }
                    else
                    {
                        IsForwardSuccess = true;

                    }
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsForwardSuccess;
        }

        public override bool Print()
        {
            this.Cursor = Cursors.Wait;
            try
            {

                DateTime currentDateTime = DB.GetServerDate();
                base.RequestID = _request.ID;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();
                StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(currentDateTime));
                StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time));
                StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);
                string Path = string.Empty;
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.ChangeLocationSpecialWire:
                        {
                            Path = Data.ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationMDFSpecialWire);
                            List<Report_ChangeLocationMDFSpecialWireResult> res = ReportDB.GetChangeLocationMDFSpecialwire(new List<long> { RequestID });
                            //res.ForEach(t =>
                            //{
                            //    AssignmentInfo oasi = DB.GetAllInformationByBuchtID(string.IsNullOrEmpty(t.OldOtherBuchtID) ? 0 : Convert.ToInt64(t.OldOtherBuchtID));
                            //    t.OldOtherBuchtID = ((oasi != null) ? oasi.Connection : "");

                            //    AssignmentInfo nasi = DB.GetAllInformationByBuchtID(string.IsNullOrEmpty(t.NewOtherBuchtID) ? 0 : Convert.ToInt64(t.NewOtherBuchtID));
                            //    t.NewOtherBuchtID = ((nasi != null) ? nasi.Connection : "");
                            //});
                            //stiReport.Load(Path);
                            //stiReport.RegData("result", "result", res);
                            //var frm = new ReportViewerForm(stiReport);
                            //frm.ShowDialog();

                            CRM.Application.Local.ReportBase.SendToPrint(res, (int)DB.UserControlNames.ChangeLocationMDFSpecialWire, dateVariable, timeVariable);


                            break;
                        }
                    case (int)DB.RequestType.VacateSpecialWire:
                        {
                            IEnumerable result = ReportDB.GetMDFVacatePrivateWire(new List<long> { RequestID });
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.MDFVacateSpecialWireReport, dateVariable, timeVariable);

                            //Path = Data.ReportDB.GetReportPath((int)DB.UserControlNames.MDFVacateSpecialWireReport);
                            //stiReport.Load(Path);
                            //stiReport.RegData("result", "result", ReportDB.GetMDFVacatePrivateWire(new List<long> { RequestID }));
                            //frm = new ReportViewerForm(stiReport);
                            //frm.ShowDialog();
                            break;
                        }
                    case (int)DB.RequestType.Dayri:
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetDayeriMDFWiring(new List<long> { _request.ID });
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.DayeriMDFWiringReport, dateVariable, timeVariable);
                            break;
                        }
                    case (int)DB.RequestType.ChangeLocationCenterInside:
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetChangeLocationInsideCenterMDFWiringInfo(new List<long> { _request.ID });
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationInsideCenterMDFWiringReport, dateVariable, timeVariable);
                            break;
                        }
                    // List<ChangeLocation> changeLocationInside = ChangeLocationDB.GetChangeLocationByRequestIDs(new List<long> { RequestID });
                    // List<ConnectionInfo> ChangeLocationInsideOldResult = new List<ConnectionInfo>();
                    // List<ConnectionInfo> ChangeLocationInsideNewResult = new List<ConnectionInfo>();
                    // List<ConnectionInfo> ChangeLocationInsideResult = new List<ConnectionInfo>();
                    // List<AssignmentInfo> OldPCMResult = new List<AssignmentInfo>();
                    // List<AssignmentInfo> NewPCMResult = new List<AssignmentInfo>();
                    //List<BuchtNoInfo> OldBuchtnoInoCentral = new List<BuchtNoInfo>();
                    //List<BuchtNoInfo> NewBuchtnoInoCentral = new List<BuchtNoInfo>();
                    //
                    // foreach (ChangeLocation Info in changeLocationInside)
                    // {
                    //     ChangeLocationInsideOldResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.OldBuchtID, Info.ID, null, Info.OldTelephone));
                    //     AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)Info.OldTelephone);
                    //     if (assignmentInfo != null)
                    //     {
                    //         ChangeLocationInsideOldResult.Where(t => t.OldTelephoneNo == Info.OldTelephone).Take(1).SingleOrDefault().MUID = assignmentInfo.MUID;
                    //         ChangeLocationInsideOldResult.Where(t => t.OldTelephoneNo == Info.OldTelephone).Take(1).SingleOrDefault().ADSLBucht = assignmentInfo.ADSLBucht;
                    //     }
                    //
                    // }
                    //
                    // foreach (ChangeLocation Info in changeLocationInside)
                    // {
                    //     ChangeLocationInsideNewResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.ReservBuchtID, Info.ID, Info.NewTelephone, null));
                    //
                    // }
                    //
                    // for (int i = 0; i < ChangeLocationInsideOldResult.Count; i++)
                    // {
                    //     for (int j = 0; j < ChangeLocationInsideNewResult.Count; j++)
                    //
                    //         if (ChangeLocationInsideOldResult[i].RequestID == ChangeLocationInsideNewResult[j].RequestID)
                    //         {
                    //             ChangeLocationInsideResult.Add(ChangeLocationInsideOldResult[i]);
                    //             ChangeLocationInsideResult[i].NewBuchtNo = ChangeLocationInsideNewResult[j].BuchtNo;
                    //             ChangeLocationInsideResult[i].NewTelephoneNo = ChangeLocationInsideNewResult[j].NewTelephoneNo;
                    //             ChangeLocationInsideResult[i].NewVerticalColumnNo = ChangeLocationInsideNewResult[j].VerticalColumnNo;
                    //             ChangeLocationInsideResult[i].NewVerticalRowNo = ChangeLocationInsideNewResult[j].VerticalRowNo;
                    //             ChangeLocationInsideResult[i].NewMDF = ChangeLocationInsideNewResult[j].MDF;
                    //             ChangeLocationInsideResult[i].NewBuchtID = ChangeLocationInsideNewResult[j].BuchtID;
                    //             ChangeLocationInsideResult[i].NewCabinetInputID = ChangeLocationInsideNewResult[j].CabinetInputID;
                    //
                    //             ChangeLocationInsideResult[i].OldBuchtNo = ChangeLocationInsideOldResult[j].BuchtNo;
                    //             ChangeLocationInsideResult[i].OldTelephoneNo = ChangeLocationInsideOldResult[j].OldTelephoneNo;
                    //             ChangeLocationInsideResult[i].OldVerticalColumnNo = ChangeLocationInsideOldResult[j].VerticalColumnNo;
                    //             ChangeLocationInsideResult[i].OldVerticalRowNo = ChangeLocationInsideOldResult[j].VerticalRowNo;
                    //             ChangeLocationInsideResult[i].OldMDF = ChangeLocationInsideOldResult[j].MDF;
                    //             ChangeLocationInsideResult[i].OldBuchtID = ChangeLocationInsideNewResult[j].BuchtID;
                    //             ChangeLocationInsideResult[i].OldCabinetInputID = ChangeLocationInsideNewResult[j].CabinetInputID;
                    //         }
                    // }
                    //
                    // foreach (ConnectionInfo Info in ChangeLocationInsideResult)
                    // {
                    //     Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.OldBuchtID);
                    //     if (bucht != null)
                    //     {
                    //         PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    //         // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //         if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //         {
                    //
                    //             IEnumerable AssignmentList = ReportDB.LoadPCMInfo(bucht);
                    //             IEnumerable BuchtList = ReportDB.GetBuchtNoCentralInfo(bucht);
                    //             foreach (AssignmentInfo info in AssignmentList)
                    //             {
                    //                 OldPCMResult.Add(info);
                    //             }
                    //
                    //             foreach (BuchtNoInfo info in BuchtList)
                    //             {
                    //                 OldBuchtnoInoCentral.Add(info);
                    //             }
                    //
                    //         }
                    //     }
                    //
                    //     Bucht Newbucht = Data.BuchtDB.GetBuchtByID((long)Info.NewBuchtID);
                    //     if (bucht != null)
                    //     {
                    //         PostContact postContact = Data.PostContactDB.GetPostContactByID(Newbucht.ConnectionID ?? 0);
                    //         // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //         if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //         {
                    //
                    //             IEnumerable AssignmentList = ReportDB.LoadPCMInfo(bucht);
                    //             IEnumerable BuchtList = ReportDB.GetBuchtNoCentralInfo(bucht);
                    //             foreach (AssignmentInfo info in AssignmentList)
                    //             {
                    //                 NewPCMResult.Add(info);
                    //             }
                    //
                    //             foreach (BuchtNoInfo info in BuchtList)
                    //             {
                    //                 NewBuchtnoInoCentral.Add(info);
                    //             }
                    //         }
                    //     }
                    //
                    // }
                    // //SendToPrintChangeLocationCenterInsideMDFWiring(ChangeLocationInsideResult, NewPCMResult, OldPCMResult, NewBuchtnoInoCentral, OldBuchtnoInoCentral);
                    // string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationcenterTocenterMDFWiringReport);
                    // stiReport.Load(path);
                    //
                    //
                    // stiReport.CacheAllData = true;
                    // stiReport.RegData("ChangeLocationCenterInsideresult", "ChangeLocationCenterInsideresult", ChangeLocationInsideResult);
                    //
                    // //if (NewResultPCM.Count != 0)
                    // //stiReport.RegData("NewResultPCM", "NewResultPCM", NewResultPCM);
                    //
                    // //if (OldPCMResult.Count != 0)
                    // //stiReport.RegData("OldPCMResult", "OldPCMResult", OldPCMResult);
                    //
                    // stiReport.RegData("NewBuchtNoInfoResult", "NewBuchtNoInfoResult", NewBuchtnoInoCentral);
                    // stiReport.RegData("OldBuchtNoInfoResult", "OldBuchtNoInfoResult", OldBuchtnoInoCentral);
                    case (int)DB.RequestType.Dischargin:
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetDischargeConfirmByMDF(new List<long> { _request.ID });
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.DischargeConfirmByMDFReport, dateVariable, timeVariable);
                            break;
                        }
                    //List<BuchtNoInfo> BuchtnoInoCentral = new List<BuchtNoInfo>();
                    //List<TakePossession> takePossession = TakePossessionDB.GetTakePossessionByIDs(new List<long> { RequestID });
                    //List<ConnectionInfo> DischargeResult = new List<ConnectionInfo>();
                    //List<AssignmentInfo> DischargePCMResult = new List<AssignmentInfo>();

                    //foreach (TakePossession Info in takePossession)
                    //{
                    //    DischargeResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.BuchtID, Info.ID, null, null));
                    //}

                    //foreach (ConnectionInfo info in DischargeResult)
                    //{
                    //    info.TelephoneNo = (RequestDB.GetTeleponeNoByRequestID(info.RequestID));
                    //    AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)info.TelephoneNo);

                    //    if (assignmentInfo != null)
                    //    {
                    //        info.MUID = assignmentInfo.MUID;
                    //        info.ADSLBucht = assignmentInfo.ADSLBucht;
                    //    }
                    //}
                    //foreach (ConnectionInfo Info in DischargeResult)
                    //{
                    //    Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.BuchtID);
                    //    if (bucht != null)
                    //    {
                    //        PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    //        // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //        if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //        {
                    //            IEnumerable AssignmentList = ReportDB.LoadPCMInfo(bucht);
                    //            IEnumerable BuchtList = ReportDB.GetBuchtNoCentralInfo(bucht);
                    //            foreach (AssignmentInfo info in AssignmentList)
                    //            {
                    //                DischargePCMResult.Add(info);
                    //            }

                    //            foreach (BuchtNoInfo info in BuchtList)
                    //            {
                    //                BuchtnoInoCentral.Add(info);
                    //            }
                    //        }
                    //    }
                    //}
                    //Path = ReportDB.GetReportPath((int)DB.UserControlNames.DayeriDischargeChangeLocationCenterTocenterReInstallMDFReport);
                    //stiReport.Load(Path);
                    //stiReport.CacheAllData = true;
                    //stiReport.RegData("result", "result", DischargeResult);


                    //stiReport.RegData("BuchtNoInfoResult", "BuchtNoInfoResult", BuchtnoInoCentral);

                    case (int)DB.RequestType.ChangeNo:
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetChangeNoMDFWiring(new List<long> { _request.ID });
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeNoMDFWiringReport, dateVariable, timeVariable);
                            break;
                        }
                    //BuchtnoInoCentral = new List<BuchtNoInfo>();
                    //List<ChangeNo> changeNo = ChangeNoDB.GetChangeNoDBByIDs(new List<long> { RequestID });
                    //List<ConnectionInfo> ResultChangeNo = new List<ConnectionInfo>();
                    //List<AssignmentInfo> ResultPCMChangeNO = new List<AssignmentInfo>();
                    //
                    //foreach (ChangeNo Info in changeNo)
                    //{
                    //    ResultChangeNo.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, (long)Info.OldBuchtID, -1, Info.ID, Info.NewTelephoneNo, Info.OldTelephoneNo));
                    //    AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)Info.OldTelephoneNo);
                    //    if (assignmentInfo != null)
                    //    {
                    //        ResultChangeNo.Where(t => t.OldTelephoneNo == Info.OldTelephoneNo).Take(1).SingleOrDefault().MUID = assignmentInfo.MUID;
                    //        ResultChangeNo.Where(t => t.OldTelephoneNo == Info.OldTelephoneNo).Take(1).SingleOrDefault().ADSLBucht = assignmentInfo.ADSLBucht;
                    //    }
                    //
                    //}
                    //foreach (ConnectionInfo Info in ResultChangeNo)
                    //{
                    //    Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.BuchtID);
                    //    if (bucht != null)
                    //    {
                    //        PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    //        // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    //        if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    //        {
                    //            IEnumerable AssignmentList = ReportDB.LoadPCMInfo(bucht);
                    //            IEnumerable BuchtList = ReportDB.GetBuchtNoCentralInfo(bucht);
                    //            foreach (AssignmentInfo info in AssignmentList)
                    //            {
                    //                ResultPCMChangeNO.Add(info);
                    //            }
                    //
                    //            foreach (BuchtNoInfo info in BuchtList)
                    //            {
                    //                BuchtnoInoCentral.Add(info);
                    //            }
                    //        }
                    //    }
                    //}
                    //
                    //
                    //
                    //Path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeNoMDFWiringReport);
                    //stiReport.Load(Path);
                    //stiReport.CacheAllData = true; stiReport.RegData("ResultChangeNo", "ResultChangeNo", ResultChangeNo);
                    //
                    ////if (ResultPCM.Count != 0)
                    ////stiReport.RegData("ResultPCM", "ResultPCM", ResultPCM);
                    //
                    //stiReport.RegData("BuchtNoInfoResult", "BuchtNoInfoResult", BuchtnoInoCentral);

                    //break;
                    case (int)DB.RequestType.E1:
                        {
                            IEnumerable result = ReportDB.GetMDFE1Dayeri(new List<long> { RequestID });
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.MDFE1DayeriReport, true, dateVariable, timeVariable, cityVariable);

                            //Path = Data.ReportDB.GetReportPath((int)DB.UserControlNames.MDFE1DayeriReport);
                            //stiReport.Load(Path);
                            //stiReport.RegData("result", "result", ReportDB.GetMDFE1Dayeri(new List<long> { RequestID }));
                            //frm = new ReportViewerForm(stiReport);
                            //frm.ShowDialog();
                            break;
                        }
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        {//TODO:rad
                            if (_changeLocation.SourceCenter.HasValue && _changeLocation.SourceCenter == _request.CenterID)//درخواست در مبدا
                            {
                                IEnumerable result = ReportDB.GetChangeLocationCenterToCenterMdfWriringOfSourceCenter(new List<long> { _request.ID });
                                CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationCenterToCenterMdfWriringOfSourceCenterReport, dateVariable, timeVariable);
                            }
                            else if (_changeLocation.SourceCenter.HasValue && _changeLocation.TargetCenter == _request.CenterID) //درخواست در مقصد
                            {
                                IEnumerable result = ReportDB.GetChangeLocationCenterToCenterMdfWriringOfTargetCenter(new List<long> { _request.ID });
                                CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationCenterToCenterMdfWriringOfTargetCenterReport, dateVariable, timeVariable);
                            }
                        }
                        break;
                    case (int)DB.RequestType.SpecialWire:
                        {
                            List<SpecialWireReportInfo> result = new List<SpecialWireReportInfo> { _SpecialWireReportInfo };
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.SpecialWirelMDFReport, dateVariable, timeVariable);

                            //stiReport.Dictionary.DataStore.Clear();
                            //stiReport.Dictionary.Databases.Clear();
                            //stiReport.Dictionary.RemoveUnusedData();

                            //string path = ReportDB.GetReportPath((int)DB.UserControlNames.SpecialWirelMDFReport);
                            //stiReport.Load(path);

                            //stiReport.CacheAllData = true;
                            //stiReport.RegData("Result", "Result", _SpecialWireReportInfo);

                            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                            //reportViewerForm.ShowDialog();
                            break;
                        }
                }



                // stiReport.Dictionary.Variables["HeaderTitle"].Value = Title;




            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رد درخواست", ex);
            }
            this.Cursor = Cursors.Arrow;
            return true;
        }
        private void SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(IEnumerable result, List<AssignmentInfo> ResultPCM, List<BuchtNoInfo> BuchtNoInfoResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.DayeriDischargeChangeLocationCenterTocenterReInstallMDFReport);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            //if (ResultPCM.Count != 0)
            //stiReport.RegData("ResultPCM", "ResultPCM", ResultPCM);

            stiReport.RegData("BuchtNoInfoResult", "BuchtNoInfoResult", BuchtNoInfoResult);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private void SendToPrintChangeLocationCenterInsideMDFWiring(IEnumerable ChangeLocationCenterInsideresult, List<AssignmentInfo> NewResultPCM, List<AssignmentInfo> OldPCMResult, List<BuchtNoInfo> NewBuchtNoInfoResult, List<BuchtNoInfo> OldBuchtNoInfoResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationcenterTocenterMDFWiringReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("ChangeLocationCenterInsideresult", "ChangeLocationCenterInsideresult", ChangeLocationCenterInsideresult);

            //if (NewResultPCM.Count != 0)
            //stiReport.RegData("NewResultPCM", "NewResultPCM", NewResultPCM);

            //if (OldPCMResult.Count != 0)
            //stiReport.RegData("OldPCMResult", "OldPCMResult", OldPCMResult);

            stiReport.RegData("NewBuchtNoInfoResult", "NewBuchtNoInfoResult", NewBuchtNoInfoResult);
            stiReport.RegData("OldBuchtNoInfoResult", "OldBuchtNoInfoResult", OldBuchtNoInfoResult);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        #endregion

        # region OtherBucht
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
                if (_request.RequestTypeID == (int)DB.RequestType.SpecialWire || _request.RequestTypeID == (int)DB.RequestType.SpecialWireOtherPoint)
                    buchtType = _SpecialWire.BuchtType;
                else if (_request.RequestTypeID == (int)DB.RequestType.VacateSpecialWire)
                    buchtType = _oldOtherBucht.BuchtTypeID;

                if (_oldOtherBucht.ID != 0)
                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, true, buchtType).Union(new List<CheckableItem> { new CheckableItem { LongID = _oldOtherBucht.ID, Name = _oldOtherBucht.BuchtNo.ToString(), IsChecked = false } });
                else
                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, true, buchtType);
            }
        }

        private void SecondMDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondMDFComboBox.SelectedValue != null)
                SecondConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)SecondMDFComboBox.SelectedValue);
        }

        private void SecondConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondConnectionColumnComboBox.SelectedValue != null)
                SecondConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)SecondConnectionColumnComboBox.SelectedValue);
        }

        private void SecondConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondConnectionRowComboBox.SelectedValue != null)
            {

                if (_request.RequestTypeID == (int)DB.RequestType.SpecialWire || _request.RequestTypeID == (int)DB.RequestType.SpecialWireOtherPoint)
                    buchtType = _SpecialWire.BuchtType;
                else if (_request.RequestTypeID == (int)DB.RequestType.VacateSpecialWire)
                    buchtType = _oldSecondOtherBucht.BuchtTypeID;

                if (_oldSecondOtherBucht != null)

                    SecondConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)SecondConnectionRowComboBox.SelectedValue, true, buchtType).Union(new List<CheckableItem> { new CheckableItem { LongID = _oldSecondOtherBucht.ID, Name = _oldSecondOtherBucht.BuchtNo.ToString(), IsChecked = false } });

                else

                    SecondConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)SecondConnectionRowComboBox.SelectedValue, true, buchtType);
            }
        }
        #endregion OtherBucht

    }
}
