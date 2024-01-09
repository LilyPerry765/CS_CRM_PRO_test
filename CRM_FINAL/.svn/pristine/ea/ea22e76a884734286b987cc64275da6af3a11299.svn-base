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
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using Enterprise;
using CRM.Data;
using CRM.Application.UserControls;
using CRM.Application.Views;
using CRM.Data.Schema;
using System.Transactions;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using Folder.Printing;
using System.Collections;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using System.Security.Cryptography;
using CookComputing.XmlRpc;
using CRM.Data.Services;

namespace CRM.Application.Views
{
    public partial class RequestForm : Local.RequestFormBase
    {
        #region Properties

        //public long _id = 0;
        private long _relatedRequestID;
        private long _customerID;
        private long? pastTelephone = 0;
        private byte _Mode { get; set; }
        private bool _IsSalable = true;
        private string _UserID { get; set; }
        private string _WirelessCode { get; set; }

        private UserControls.TelephoneInformation _TelephoneInformation;
        private UserControls.Install _installdetail;
        private UserControls.ChangeAddressUserControl _ChangeAddressUserControl;
        private UserControls.ChangeLocationUserControl _ChangeLocation;
        private UserControls.DischargeTelephonUserControl _DischargeTelephone;
        private UserControls.ChangeName _ChangeName;
        private UserControls.CutAndEstablish _CutAndEstablish;
        private UserControls.SpecialService _SpecialServiceUserControl;
        private UserControls.OpenAndCloseZero _openAndCloseZeroUserControl;
        private UserControls.ADSL _ADSL;
        private UserControls.ADSLChangeService _ADSLChangeService;
        private UserControls.WirelessChangeService _WirelessChangeService;
        private UserControls.ADSLChangeIP _ADSLChangeIP;
        private UserControls.ADSLInstallRequestUserControl _ADSLInstallRequest;
        private UserControls.ADSLCutTemporary _ADSLCutTemporary;
        private UserControls.ADSLDischargeUserControl _ADSLDischarge;
        private UserControls.ADSLChangePort _ADSLChangePort;
        private UserControls.ChangeNoUserControl _changeNo;
        private UserControls.RefundDepositUserControl _RefundDepositUserControl;
        private UserControls.Title118 _Title118;
        private UserControls.SpaceandPower _SpaceandPower;
        private V2SpaceAndPower _V2SpaceAndPower;
        private UserControls.V2SpaceAndPowerInfoSummary _V2SpaceAndPowerSummaryInfo { get; set; }
        private CRM.Application.ExtendedSpaceAndPower.V3SpaceAndPowerUserControl _V3SpaceAndPower { get; set; }
        private UserControls.Failure117 _Failure117;
        private UserControls.E1 _E1;
        private UserControls.E1InfoSummary _E1InfoSummary;
        private UserControls.VacateE1UserControl _VacateE1UserControl;
        private UserControls.ADSLSellTrafficUserControl _ADSLSellTrafficUserControl;
        private UserControls.ADSLChangePlaceUserControl _ADSLChangePlaceUserControl;
        private UserControls.E1FiberUseControl _E1FiberUseControl;
        private UserControls.SpecialWireUserControl _SpecialWireUserControl;
        private UserControls.VacateSpecialWireUserControl _VacateSpecialWireUserControl;
        private UserControls.ChangeLocationSpecialWireUserControl _ChangeLocationSpecialWireUserControl;
        private UserControls.PBXUserControl _PBXUserControl;
        private UserControls.ADSLChangeCustomerOwnerCharacteristicsUserControl _ADSLChangeCustomerOwnerCharacteristicsUserControl;
        private UserControls.InvestigateInfoSummary _investigateInfoSummary;
        private UserControls.Wireless _wirelessUserControl;


        private CustomerInfoSummary _customerInfoSummary { get; set; }

        private Data.ChangeLocation _changeLocation;
        private Data.TakePossession _takePossession;
        private Data.ChangeNo _ChangeNo;
        private Data.E1 _e1;
        private Data.RefundDeposit _RefundDeposit;
        public Data.InstallRequest _installReqeust { get; set; }
        private Data.ZeroStatus _zeroStatus { get; set; }
        private Data.SpecialWire _specialWire;
        private Data.VacateSpecialWire _vacateSpecialWire;

        private Data.ChangeLocationSpecialWire _changeLocationSpecialWire;
        private Data.TelephonePBX _telephonePBX;

        private Data.WirelessRequest wirelessRequest = new Data.WirelessRequest();

        private Data.ChangeLocationSpecialWirePoint _changeLocationSpecialWirePoint;

        private Data.SpecialService _specialService;
        private Data.TitleIn118 _titleIn118;

        public List<UsedDocs> refDocs { get; set; }

        private RequestLog requestLog { get; set; }

        public Request _Request { get; set; }
        public Customer Customer { get; set; }
        public Telephone Telephone { get; set; }
        public RequestType RequestType { get; set; }
        public List<VisitAddress> _VisitInfoList { get; set; }
        public ADSLDischargeReason _ADSLDischargeReason { get; set; }


        private int CityID = 0;
        private string city = string.Empty;

        private long _TelephoneNo = 0;

        private byte[] FileBytes { get; set; }

        // if action is forward, IsForward be true;
        private bool IsForward = false;

        // use in changeLocation when reject from target to source
        private int requestCenterID = 0;

        public int SelectedReportTemplateId
        {
            get;
            set;
        }


        #endregion

        #region Constuctors

        public RequestForm()
        {
            InitializeComponent();
            Initialize();
        }

        public RequestForm(long id)
            : this()
        {
            RequestID = id;
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            if (_Request.CustomerID != null)
            {
                Customer = Data.CustomerDB.GetCustomerByID(_Request.CustomerID ?? 0);
                _customerID = Customer.ID;
            }

            refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();
            if (_Request != null && _Request.TelephoneNo != null && _Request.TelephoneNo != 0)
            {
                if (_Request.RequestTypeID != (int)DB.RequestType.ADSL)
                {
                    Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_Request.TelephoneNo ?? 0);
                    if (Customer == null)
                        Customer = Data.CustomerDB.GetCustomerByID(Telephone.CustomerID ?? 0);
                }
                else
                    Customer = Data.CustomerDB.GetCustomerByID(_Request.CustomerID ?? 0);

                _TelephoneInformation = new TelephoneInformation(_Request.TelephoneNo ?? 0, _Request.RequestTypeID);
            }

            Initialize();
        }

        public RequestForm(long relatedRequestID, long customerID)
            : this()
        {
            _Request = new Request();
            Customer = Data.CustomerDB.GetCustomerByID(customerID);
            _Request.CustomerID = Customer.ID;
            //searchButton.IsEnabled = false;
            RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType((int)DB.RequestType.Dayri);

            _relatedRequestID = relatedRequestID;
            _Request.RelatedRequestID = _relatedRequestID;
        }

        public RequestForm(byte requestTypeID, long telephoneNo, byte mode = 0)
            : this()
        {
            RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType(requestTypeID);
            _Mode = mode;

            if (telephoneNo != 0)
            {
                _TelephoneInformation = new TelephoneInformation(telephoneNo, requestTypeID);

                switch (requestTypeID)
                {
                    case (byte)DB.RequestType.Failure117:
                        _TelephoneNo = telephoneNo;
                        break;

                    case (byte)DB.RequestType.ADSL:
                    case (byte)DB.RequestType.ADSLChangeService:
                    case (byte)DB.RequestType.ADSLSellTraffic:
                    case (byte)DB.RequestType.ADSLChangeIP:
                    case (byte)DB.RequestType.ADSLDischarge:
                    case (byte)DB.RequestType.ADSLCutTemporary:
                    case (byte)DB.RequestType.ADSLChangePlace:

                        _TelephoneNo = telephoneNo;
                        Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                        if (Telephone != null)
                            Customer = Data.CustomerDB.GetCustomerByID(Telephone.CustomerID ?? 0);

                        int centerID = 0;

                        if (Customer == null)
                        {
                            Service1 service = new Service1();
                            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                            //Customer = CustomerDB.GetCustomerbyElkaID(Convert.ToInt64(telephoneInfo.Rows[0]["FI_CODE"].ToString()));

                            if (telephoneInfo.Rows.Count > 0)
                            {
                                string FI_CODE = string.Empty;

                                try
                                {
                                    FI_CODE = ToStringSpecial(telephoneInfo.Rows[0]["FI_CODE"].ToString());
                                }
                                catch (Exception ex)
                                {
                                    FI_CODE = string.Empty;
                                }

                                if (!string.IsNullOrWhiteSpace(FI_CODE))
                                    Customer = CustomerDB.GetCustomerbyElkaID(Convert.ToInt64(telephoneInfo.Rows[0]["FI_CODE"].ToString()));

                                if (Customer == null)
                                {
                                    Customer = new Customer();

                                    Customer.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                                    Customer.FirstNameOrTitle = telephoneInfo.Rows[0]["FirstName"].ToString();
                                    Customer.LastName = telephoneInfo.Rows[0]["Lastname"].ToString();
                                    Customer.FatherName = telephoneInfo.Rows[0]["FATHERNAME"].ToString();
                                    Customer.BirthCertificateID = telephoneInfo.Rows[0]["SHENASNAME"].ToString();
                                    Customer.MobileNo = telephoneInfo.Rows[0]["MOBILE"].ToString();
                                    Customer.Email = telephoneInfo.Rows[0]["EMAIL"].ToString();
                                    Customer.PersonType = (telephoneInfo.Rows[0]["CustumerType"].ToString() == "1") ? (byte)Convert.ToInt16(0) : (byte)Convert.ToInt16(1);
                                    // Customer.ElkaID = Convert.ToInt64(telephoneInfo.Rows[0]["FI_CODE"].ToString());

                                    centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                                    Customer.CustomerID = DB.GetCustomerID(centerID, (byte)DB.PersonType.Person);

                                    if (string.IsNullOrWhiteSpace(Customer.CustomerID))
                                        Customer.CustomerID = "000000000000000";

                                    Customer.Detach();
                                    Save(Customer, true);
                                }

                            }
                        }

                        if (Telephone != null)
                        {
                            if (Customer != null)
                            {
                                Telephone.CustomerID = Customer.ID;

                                Telephone.Detach();
                                DB.Save(Telephone);
                            }
                            else
                            {
                                throw new Exception("اطلاعات این شماره تلفن در سیستم الکاپرداز موجود نمی باشد");
                            }
                        }
                        else
                        {
                            if (centerID == 0)
                            {
                                Service1 service = new Service1();
                                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());
                                centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                            }

                            Telephone = new Telephone();

                            Telephone.TelephoneNo = _TelephoneNo;
                            Telephone.TelephoneNoIndividual = Convert.ToInt64(_TelephoneNo.ToString().Substring(2));
                            Telephone.CenterID = centerID;
                            Telephone.Status = 2;
                            Telephone.ClassTelephone = 1;
                            Telephone.CustomerID = Customer.ID;

                            Telephone.Detach();
                            DB.Save(Telephone, true);
                        }

                        break;

                    case (byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                        _TelephoneNo = telephoneNo;
                        Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                        Customer = CustomerDB.GetCustomerByTelephone((long)_TelephoneNo);
                        break;
                    default:
                        Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
                        Customer = Data.CustomerDB.GetCustomerByID(Telephone.CustomerID ?? 0);

                        break;
                }
            }

            _Request = new Request();
        }

        public RequestForm(byte requestTypeID, string wirelessCode, byte mode = 0)
            : this()
        {
            RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType(requestTypeID);
            _WirelessCode = wirelessCode;
            _Request = new Request();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
            RequesterNametextBox.Focus();

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            CostTitleColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();
            OtherCostTitleColumn.ItemsSource = Data.OtherCostDB.GetOtherCostCheckable();
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
        }

        public void LoadData()
        {
            try
            {

                if (RequestID == 0)
                {
                    _Request = new Request();

                    if (RequestType == null)
                        return;

                    #region Initial Request

                    switch (RequestType.ID)
                    {
                        case (int)DB.RequestType.Dayri:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _installReqeust = new InstallRequest();
                            _installdetail = new Install(RequestID);
                            RequestDetail.Content = _installdetail;
                            RequestDetail.DataContext = _installdetail;

                            RequestDetail.Visibility = Visibility.Visible;
                            _installdetail.ReinsrallInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            //TODO:rad 13950803
                            ConvertCashPaymentToInstallmentMenuItem.Visibility = Visibility.Collapsed;
                            InstallmentsMenuItem.Visibility = Visibility.Collapsed;
                            //
                            Title = "ثبت درخواست دایری ";
                            break;

                        case (int)DB.RequestType.ChangeLocationCenterInside:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ChangeLocation = new UserControls.ChangeLocationUserControl(RequestID, Customer.ID, Telephone.TelephoneNo, RequestType.ID);
                            _changeLocation = new Data.ChangeLocation();
                            RequestDetail.Content = _ChangeLocation;
                            RequestDetail.DataContext = _ChangeLocation;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست تغییر مکان ";
                            break;

                        case (int)DB.RequestType.ChangeLocationCenterToCenter:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ChangeLocation = new UserControls.ChangeLocationUserControl(RequestID, Customer.ID, Telephone.TelephoneNo, RequestType.ID);
                            _changeLocation = new Data.ChangeLocation();
                            RequestDetail.Content = _ChangeLocation;
                            RequestDetail.DataContext = _ChangeLocation;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست تغییر مکان ";
                            break;

                        case (int)DB.RequestType.Dischargin:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _DischargeTelephone = new UserControls.DischargeTelephonUserControl(RequestID, Customer.ID, Telephone.TelephoneNo);
                            RequestDetail.Content = _DischargeTelephone;
                            RequestDetail.DataContext = _DischargeTelephone;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست تخلیه ";
                            break;

                        case (int)DB.RequestType.ChangeName:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ChangeName = new UserControls.ChangeName(RequestID, Customer.ID, Telephone.TelephoneNo);
                            RequestDetail.Content = _ChangeName;
                            RequestDetail.DataContext = _ChangeName;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست تغییر نام ";
                            break;

                        case (int)DB.RequestType.CutAndEstablish:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _CutAndEstablish = new UserControls.CutAndEstablish(RequestID, Customer.ID, Telephone.TelephoneNo, (int)DB.RequestType.CutAndEstablish);
                            RequestDetail.Content = _CutAndEstablish;
                            RequestDetail.DataContext = _CutAndEstablish;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "قطع";
                            break;

                        case (int)DB.RequestType.Connect:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _CutAndEstablish = new UserControls.CutAndEstablish(RequestID, Customer.ID, Telephone.TelephoneNo, (int)DB.RequestType.Connect);
                            RequestDetail.Content = _CutAndEstablish;
                            RequestDetail.DataContext = _CutAndEstablish;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "وصل";
                            break;

                        case (int)DB.RequestType.SpecialService:
                            _specialService = new Data.SpecialService();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _SpecialServiceUserControl = new UserControls.SpecialService(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _SpecialServiceUserControl;
                            RequestDetail.DataContext = _SpecialServiceUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست سرویس ویژه ";
                            break;

                        case (int)DB.RequestType.OpenAndCloseZero:
                            _zeroStatus = new ZeroStatus();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _openAndCloseZeroUserControl = new UserControls.OpenAndCloseZero(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _openAndCloseZeroUserControl;
                            RequestDetail.DataContext = _openAndCloseZeroUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست انسداد صفر ";
                            break;

                        case (int)DB.RequestType.ADSL:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            long customerID = 0;
                            if (Customer != null)
                            {
                                customerID = Customer.ID;
                                _Request.RequesterName = Customer.FirstNameOrTitle + " " + Customer.LastName;
                            }
                            _ADSL = new UserControls.ADSL(RequestID, customerID, _TelephoneNo);
                            RequestDetail.Content = _ADSL;
                            RequestDetail.DataContext = _ADSL;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست ADSL ";

                            //PaymentLabel.Visibility = Visibility.Collapsed;
                            //RequestPaymentTypeListBox.Visibility = Visibility.Collapsed;

                            if (_ADSL._IsWaitingList)
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLChangeService:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            if (Telephone != null)
                                _ADSLChangeService = new UserControls.ADSLChangeService(RequestID, Telephone.TelephoneNo, RequestType.ID);
                            else
                                _ADSLChangeService = new UserControls.ADSLChangeService(RequestID, _TelephoneNo, RequestType.ID);
                            RequestDetail.Content = _ADSLChangeService;
                            RequestDetail.DataContext = _ADSLChangeService;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست شارژ مجدد ADSL ";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.WirelessChangeService:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _WirelessChangeService = new UserControls.WirelessChangeService(RequestID, _WirelessCode, RequestType.ID);
                            RequestDetail.Content = _WirelessChangeService;
                            RequestDetail.DataContext = _WirelessChangeService;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست شارژ مجدد Wireless ";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLChangeIP:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            if (Telephone != null)
                                _ADSLChangeIP = new UserControls.ADSLChangeIP(RequestID, Customer.ID, Telephone.TelephoneNo);
                            else
                                _ADSLChangeIP = new UserControls.ADSLChangeIP(RequestID, Customer.ID, _TelephoneNo);
                            RequestDetail.Content = _ADSLChangeIP;
                            RequestDetail.DataContext = _ADSLChangeIP;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست تغییر IP";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLInstall:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            if (Telephone != null)
                                _ADSLInstallRequest = new UserControls.ADSLInstallRequestUserControl(RequestID, Customer.ID, Telephone.TelephoneNo);
                            else
                                _ADSLInstallRequest = new UserControls.ADSLInstallRequestUserControl(RequestID, Customer.ID, _TelephoneNo);
                            RequestDetail.Content = _ADSLInstallRequest;
                            RequestDetail.DataContext = _ADSLInstallRequest;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست نصب";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLDischarge:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            if (Telephone != null)
                                _ADSLDischarge = new UserControls.ADSLDischargeUserControl(RequestID, Telephone.TelephoneNo);
                            else
                                _ADSLDischarge = new UserControls.ADSLDischargeUserControl(RequestID, _TelephoneNo);

                            RequestDetail.Content = _ADSLDischarge;
                            RequestDetail.DataContext = _ADSLDischarge;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست تخلیه";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLSellTraffic:
                        case (int)DB.RequestType.WirelessSellTraffic:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            if (Telephone != null)
                                _ADSLSellTrafficUserControl = new UserControls.ADSLSellTrafficUserControl(RequestID, Telephone.TelephoneNo, RequestType.ID);
                            else
                                _ADSLSellTrafficUserControl = new UserControls.ADSLSellTrafficUserControl(RequestID, _TelephoneNo, RequestType.ID);
                            RequestDetail.Content = _ADSLSellTrafficUserControl;
                            RequestDetail.DataContext = _ADSLSellTrafficUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست خرید ترافیک";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLCutTemporary:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ADSLCutTemporary = new UserControls.ADSLCutTemporary(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _ADSLCutTemporary;
                            RequestDetail.DataContext = _ADSLCutTemporary;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست قطع و وصل موقت ADSL ";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLChangePlace:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ADSLChangePlaceUserControl = new UserControls.ADSLChangePlaceUserControl(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _ADSLChangePlaceUserControl;
                            RequestDetail.DataContext = _ADSLChangePlaceUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست تغییر مکان ADSL ";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Print };
                            break;

                        case (int)DB.RequestType.ADSLChangePort:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            if (Telephone != null)
                                _ADSLChangePort = new UserControls.ADSLChangePort(RequestID, Telephone.TelephoneNo);
                            else
                                _ADSLChangePort = new UserControls.ADSLChangePort(RequestID, _TelephoneNo);
                            RequestDetail.Content = _ADSLChangePort;
                            RequestDetail.DataContext = _ADSLChangePort;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست تعویض پورت";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ADSLChangeCustomerOwnerCharacteristicsUserControl = new UserControls.ADSLChangeCustomerOwnerCharacteristicsUserControl(RequestID, Customer.ID, Telephone.TelephoneNo);
                            RequestDetail.Content = _ADSLChangeCustomerOwnerCharacteristicsUserControl;
                            RequestDetail.DataContext = _ADSLChangeCustomerOwnerCharacteristicsUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            if (Customer.ID != null)
                            {
                                _customerInfoSummary = new CustomerInfoSummary(Customer.ID);
                                CustomerInfoUC.Mode = true;
                                CustomerInfoUC.Content = _customerInfoSummary;
                                CustomerInfoUC.DataContext = _customerInfoSummary;
                                CustomerInfoUC.Visibility = Visibility.Visible;
                            }


                            Title = "تغییر مشخصات مالک ADSL ";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;


                        case (int)DB.RequestType.ChangeNo:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _changeNo = new UserControls.ChangeNoUserControl(_Request.ID, Telephone.TelephoneNo);
                            RequestDetail.Content = _changeNo;
                            RequestDetail.DataContext = _changeNo;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "تعویض شماره";
                            break;

                        case (int)DB.RequestType.RefundDeposit:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _RefundDepositUserControl = new UserControls.RefundDepositUserControl(_Request.ID, Telephone.TelephoneNo);
                            RequestDetail.Content = _RefundDepositUserControl;
                            RequestDetail.DataContext = _RefundDepositUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            DepositTabItem.Visibility = Visibility.Visible;
                            DepositDataGrid.Visibility = Visibility.Visible;
                            DepositDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentDepositByTelephoneNo(Telephone.TelephoneNo);
                            Title = "استرداد ودیعه";
                            break;

                        case (int)DB.RequestType.Reinstall:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _installReqeust = new InstallRequest();
                            _installdetail = new Install(_Request.ID);
                            RequestDetail.Content = _installdetail;
                            RequestDetail.DataContext = _installdetail;
                            RequestDetail.Visibility = Visibility.Visible;
                            _installdetail.ReinsrallInfo.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست دایری مجدد ";
                            break;

                        case (int)DB.RequestType.TitleIn118:
                            _titleIn118 = new TitleIn118();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _Title118 = new UserControls.Title118(RequestID, Telephone.TelephoneNo, (int)DB.RequestType.TitleIn118);
                            RequestDetail.Content = _Title118;
                            RequestDetail.DataContext = _Title118;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            Title = "ثبت عنوان در 118";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };

                            break;


                        case (int)DB.RequestType.RemoveTitleIn118:
                            _titleIn118 = new TitleIn118();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _Title118 = new UserControls.Title118(RequestID, Telephone.TelephoneNo, (int)DB.RequestType.RemoveTitleIn118);
                            RequestDetail.Content = _Title118;
                            RequestDetail.DataContext = _Title118;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            Title = "حذف عنوان در 118";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ChangeTitleIn118:
                            _titleIn118 = new TitleIn118();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _Title118 = new UserControls.Title118(RequestID, Telephone.TelephoneNo, (int)DB.RequestType.ChangeTitleIn118);
                            RequestDetail.Content = _Title118;
                            RequestDetail.DataContext = _Title118;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            Title = "تغییر عنوان در 118";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ChangeAddress:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ChangeAddressUserControl = new ChangeAddressUserControl(_Request.ID, Telephone.TelephoneNo);
                            RequestDetail.Content = _ChangeAddressUserControl;
                            RequestDetail.DataContext = _ChangeAddressUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست اصلاح آدرس ";
                            break;

                        //case (int)DB.RequestType.SpaceandPower:
                        //    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                        //    _SpaceandPower = new UserControls.SpaceandPower(_Request.ID);
                        //    RequestDetail.Content = _SpaceandPower;
                        //    RequestDetail.DataContext = _SpaceandPower;
                        //    RequestDetail.Visibility = Visibility.Visible;
                        //    TelephoneInfo.Visibility = Visibility.Collapsed;
                        //    FormTabItem.Visibility = Visibility.Visible;
                        //    Title = "ثبت درخواست فضا و پاور ";
                        //    break;
                        case (int)DB.RequestType.SpaceandPower:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _V2SpaceAndPower = new UserControls.V2SpaceAndPower(_Request.ID);

                            //پر کردن کمبو باکس مربوط به کالا و خدمات مخابرات که مربوط به درخواست فضاپاور هستند
                            this._TelecomminucationServiceInfos = TelecomminucationServiceDB.GetTelecomminucationServiceInfosByRequestTypeID((int)DB.RequestType.SpaceandPower);

                            List<TelecomminucationServicePaymentInfo> telecomminucationServicePaymentInfos = TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentInfos(RequestID);
                            this._TelecomminucationServicePaymentInfos = new ObservableCollection<TelecomminucationServicePaymentInfo>(telecomminucationServicePaymentInfos);
                            RequestTelecomminucationServiceDataGrid.ItemsSource = this._TelecomminucationServicePaymentInfos;

                            RequestDetail.Content = _V2SpaceAndPower;
                            RequestDetail.DataContext = _V2SpaceAndPower;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست فضا و پاور ";
                            break;
                        //TODO:rad 13950523 فضاپاور جدید بر اساس مصوبه
                        case (int)DB.RequestType.StandardSpaceAndPower:
                            {
                                RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                                _V3SpaceAndPower = new ExtendedSpaceAndPower.V3SpaceAndPowerUserControl();
                                RequestDetail.Content = _V3SpaceAndPower;
                                RequestDetail.DataContext = _V3SpaceAndPower;
                                RequestDetail.Visibility = Visibility.Visible;
                                TelephoneInfo.Visibility = Visibility.Collapsed;
                                FormTabItem.Visibility = Visibility.Visible;
                                this.Title = "ثبت درخواست فضاپاور";
                                break;
                            }
                        case (int)DB.RequestType.Failure117:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _Failure117 = new UserControls.Failure117(RequestID, /* Customer.ID,*/ _TelephoneNo);
                            RequestDetail.Content = _Failure117;
                            RequestDetail.DataContext = _Failure117;
                            RequestDetail.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست اعلام خرابی ";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.E1:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _E1 = new CRM.Application.UserControls.E1(RequestID, DB.RequestType.E1);
                            this._TelecomminucationServiceInfos = TelecomminucationServiceDB.GetTelecomminucationServiceInfosByRequestTypeID((int)DB.RequestType.E1);
                            this._TelecomminucationServicePaymentInfos = new ObservableCollection<TelecomminucationServicePaymentInfo>(TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentInfos(RequestID));
                            RequestTelecomminucationServiceDataGrid.ItemsSource = this._TelecomminucationServicePaymentInfos;
                            RequestDetail.Content = _E1;
                            RequestDetail.DataContext = _E1;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست E1,STM1,...";
                            break;

                        case (int)DB.RequestType.E1Link:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _E1 = new CRM.Application.UserControls.E1(RequestID, DB.RequestType.E1Link, Telephone.TelephoneNo);
                            RequestDetail.Content = _E1;
                            RequestDetail.DataContext = _E1;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "لینک";
                            break;

                        case (int)DB.RequestType.VacateE1:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _VacateE1UserControl = new CRM.Application.UserControls.VacateE1UserControl(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _VacateE1UserControl;
                            RequestDetail.DataContext = _VacateE1UserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;

                            Title = "تخلیه";
                            break;

                        case (int)DB.RequestType.E1Fiber:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _E1FiberUseControl = new CRM.Application.UserControls.E1FiberUseControl(RequestID);
                            RequestDetail.Content = _E1FiberUseControl;
                            RequestDetail.DataContext = _E1FiberUseControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "ثبت درخواست E1(فیبر) ";

                            break;
                        case (int)DB.RequestType.SpecialWire:

                            _specialWire = new Data.SpecialWire();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _SpecialWireUserControl = new CRM.Application.UserControls.SpecialWireUserControl(RequestID);
                            RequestDetail.Content = _SpecialWireUserControl;
                            RequestDetail.DataContext = _SpecialWireUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;

                            Title = "سیم خصوصی";

                            break;

                        case (int)DB.RequestType.VacateSpecialWire:
                            _vacateSpecialWire = new Data.VacateSpecialWire();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _VacateSpecialWireUserControl = new CRM.Application.UserControls.VacateSpecialWireUserControl(RequestID);
                            RequestDetail.Content = _VacateSpecialWireUserControl;
                            RequestDetail.DataContext = _VacateSpecialWireUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;

                            Title = "تخلیه سیم خصوصی";

                            break;

                        case (int)DB.RequestType.ChangeLocationSpecialWire:
                            _changeLocationSpecialWire = new Data.ChangeLocationSpecialWire();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ChangeLocationSpecialWireUserControl = new CRM.Application.UserControls.ChangeLocationSpecialWireUserControl(RequestID);
                            RequestDetail.Content = _ChangeLocationSpecialWireUserControl;
                            RequestDetail.DataContext = _ChangeLocationSpecialWireUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "تغییر مکان سیم خصوصی";
                            break;

                        case (int)DB.RequestType.PBX:
                            _telephonePBX = new Data.TelephonePBX();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _PBXUserControl = new CRM.Application.UserControls.PBXUserControl(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _PBXUserControl;
                            RequestDetail.DataContext = _PBXUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "PBX";
                            break;

                        case (int)DB.RequestType.Wireless:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _wirelessUserControl = new CRM.Application.UserControls.Wireless(RequestID);
                            RequestDetail.Content = _wirelessUserControl;
                            RequestDetail.DataContext = _wirelessUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "Wireless";
                            break;



                        default:
                            RequestDetail.Content = null;
                            RequestDetail.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست ";
                            break;
                    }

                    _Request.RequestDate = DB.GetServerDate();
                    this.RequestInfo.DataContext = _Request;
                    TelephoneInfo.Content = _TelephoneInformation;
                    TelephoneInfo.DataContext = _TelephoneInformation;
                    #endregion initilal reqeust
                }
                else
                {
                    #region Load Reqeust

                    requestCenterID = _Request.CenterID;
                    EnableFormDetails();
                    // if request is on status Observation this form be disable
                    Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                    switch (_Request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.ChangeLocationCenterInside:
                        case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                        case (byte)DB.RequestType.ChangeAddress:
                        case (byte)DB.RequestType.ChangeName:
                        case (byte)DB.RequestType.ChangeNo:
                        case (byte)DB.RequestType.CutAndEstablish:
                        case (byte)DB.RequestType.Connect:
                        case (byte)DB.RequestType.Dischargin:
                        case (byte)DB.RequestType.E1:
                        case (byte)DB.RequestType.E1Fiber:
                        case (byte)DB.RequestType.OpenAndCloseZero:
                        case (byte)DB.RequestType.SpecialWire:
                        case (byte)DB.RequestType.RefundDeposit:
                        case (byte)DB.RequestType.Reinstall:
                        case (byte)DB.RequestType.SpecialService:
                        case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        case (byte)DB.RequestType.VacateSpecialWire:
                        case (byte)DB.RequestType.TitleIn118:
                        case (byte)DB.RequestType.RemoveTitleIn118:
                        case (byte)DB.RequestType.ChangeTitleIn118:
                            {
                                FactorNumberColumn.Visibility = Visibility.Visible;
                                if (Status.StatusType == (byte)DB.RequestStatusType.Observation)
                                {
                                    TelephoneInfo.IsEnabled = false;
                                    RequestInfo.IsEnabled = false;
                                    RequestDetail.IsEnabled = false;
                                    RequestDocGrid.IsEnabled = true;
                                    RequestDocGrid.IsEnabled = false;
                                    RequestFormDataGrid.IsEnabled = true;
                                    RequestPermissionGrid.IsEnabled = false;
                                    RequestContractGrid.IsEnabled = false;
                                    RequestPaymentDataGrid.IsEnabled = false;
                                    RelatedRequestsGrid.IsEnabled = false;
                                }

                                if (Status.StatusType == (byte)DB.RequestStatusType.GetCosts || Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                                {
                                    TelephoneInfo.IsEnabled = false;
                                    RequestInfo.IsEnabled = false;
                                    RequestDetail.IsEnabled = false;
                                }

                                if (Data.StatusDB.IsFinalStep(this.currentStat))
                                {
                                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print };
                                    //Print();
                                }
                                else
                                {
                                    if (!Data.StatusDB.IsStartStep(this.currentStat))
                                    {
                                        ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny };
                                    }
                                }
                                break;
                            }
                        case (byte)DB.RequestType.Dayri:
                            {
                                FactorNumberColumn.Visibility = Visibility.Visible;
                                if (Status.StatusType == (byte)DB.RequestStatusType.Observation)
                                {
                                    TelephoneInfo.IsEnabled = false;
                                    RequestInfo.IsEnabled = false;
                                    RequestDetail.IsEnabled = false;
                                    RequestDocGrid.IsEnabled = true;
                                    RequestDocGrid.IsEnabled = false;
                                    RequestFormDataGrid.IsEnabled = true;
                                    RequestPermissionGrid.IsEnabled = false;
                                    RequestContractGrid.IsEnabled = false;
                                    RequestPaymentDataGrid.IsEnabled = false;
                                    RelatedRequestsGrid.IsEnabled = false;
                                }
                                if (Status.StatusType == (byte)DB.RequestStatusType.GetCosts || Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                                {
                                    TelephoneInfo.IsEnabled = false;
                                    RequestInfo.IsEnabled = false;
                                    RequestDetail.IsEnabled = false;

                                    _investigateInfoSummary = new InvestigateInfoSummary(_Request.ID);
                                    InvestigateInfoExpander.Content = _investigateInfoSummary;
                                    InvestigateInfoExpander.DataContext = _investigateInfoSummary;
                                    InvestigateInfoExpander.Visibility = Visibility.Visible;

                                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print };
                                }
                                if (Data.StatusDB.IsFinalStep(this.currentStat))
                                {
                                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print };
                                    RequestPaymentContextMenu.Visibility = Visibility.Collapsed;
                                    // PrintCertificate.Visibility = Visibility.Visible;
                                }

                            }
                            break;
                    }
                    //
                    Title = "بروز رسانی درخواست ";
                    this.RequestInfo.DataContext = _Request;
                    TelephoneInfo.DataContext = _TelephoneInformation;
                    RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType((int)_Request.RequestTypeID);
                    EnableRequestDetails();
                    EnableFormDetails();
                    // get doucument information
                    if (Customer != null)
                    {
                        refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();
                    }
                    else
                    {
                        refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID).ToList();
                    }
                    //

                    //
                    CityID = Data.RequestDB.GetCity(_Request.ID);



                    switch (_Request.RequestTypeID)
                    {
                        case (int)DB.RequestType.Dayri:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _installReqeust = Data.InstallRequestDB.GetInstallRequestByRequestID(_Request.ID);
                            _installdetail = new Install(_Request.ID);
                            RequestDetail.Content = _installdetail;
                            RequestDetail.DataContext = _installdetail;
                            RequestDetail.Visibility = Visibility.Visible;
                            //searchButton.IsEnabled = true;
                            _installdetail.ReinsrallInfo.Visibility = Visibility.Collapsed;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            //TODO:rad 13950803
                            ConvertCashPaymentToInstallmentMenuItem.Visibility = Visibility.Collapsed;
                            InstallmentsMenuItem.Visibility = Visibility.Collapsed;


                            //فقط در صورتی به کاربر امکان ثبت اقساط داده میشود که نحوه پرداخت هزینه اتصال تلفن 'اقساط' باشد
                            if (
                                this._installReqeust != null &&
                                this._installReqeust.MethodOfPaymentForTelephoneConnection.HasValue &&
                                this._installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Installment &&
                                Status.StatusType == (byte)DB.RequestStatusType.GetCosts
                               )
                            {
                                InstallmentRegistrationMenuItem.Visibility = Visibility.Visible;
                                bool currentRequestHasTelephoneConnectionInstallment = RequestDB.HasTelephoneConnectionInstallment(_Request.ID);
                                if (currentRequestHasTelephoneConnectionInstallment)
                                {
                                    InstallmentRegistrationMenuItem.Header = "ویرایش اقساط هزینه اتصال تلفن";
                                }
                            }

                            //


                            Title = "دایری";

                            //  اگر در خواست تعیین شماره شده باشد امکان تغییر مرکز نیست
                            // مثلا در هنکام عدم شناسای ادرس در شبکه هوایی که به مشترکین ارجا داده می شود
                            if (_Request.TelephoneNo != 0 || _Request.TelephoneNo != null)
                            {
                                CityComboBox.IsEnabled = false;
                                CenterComboBox.IsEnabled = false;
                            }
                            break;

                        case (int)DB.RequestType.ChangeLocationCenterInside:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(RequestID);

                            _TelephoneInformation = new TelephoneInformation(_changeLocation.NewTelephone ?? _changeLocation.OldTelephone ?? 0, _Request.RequestTypeID);
                            _ChangeLocation = new UserControls.ChangeLocationUserControl(RequestID, _Request.CustomerID ?? 0, _changeLocation.OldTelephone ?? 0, RequestType.ID);

                            pastTelephone = _changeLocation.OldTelephone;
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ChangeLocation;
                            RequestDetail.DataContext = _ChangeLocation;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تغییر مکان ";
                            break;

                        case (int)DB.RequestType.ChangeLocationCenterToCenter:

                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(RequestID);

                            _TelephoneInformation = new TelephoneInformation((long)_changeLocation.OldTelephone, _Request.RequestTypeID);
                            _ChangeLocation = new UserControls.ChangeLocationUserControl(RequestID, _Request.CustomerID ?? 0, _changeLocation.OldTelephone ?? -1, RequestType.ID);

                            if (CityComboBox.SelectedValue != null)
                            {
                                _ChangeLocation.TargetCityID = (int)CityComboBox.SelectedValue;
                            }

                            _ChangeLocation.CurrentStep = this.currentStep;

                            pastTelephone = _changeLocation.OldTelephone;
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ChangeLocation;
                            RequestDetail.DataContext = _ChangeLocation;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تغییر مکان ";
                            break;

                        case (int)DB.RequestType.Dischargin:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _takePossession = Data.TakePossessionDB.GetTakePossessionByID(RequestID);
                            pastTelephone = _Request.TelephoneNo ?? 0;

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _DischargeTelephone = new UserControls.DischargeTelephonUserControl(_Request.ID, _Request.CustomerID ?? 0, (long)pastTelephone);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _DischargeTelephone;
                            RequestDetail.DataContext = _DischargeTelephone;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = " تخلیه ";
                            break;

                        case (int)DB.RequestType.ChangeName:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ChangeName = new UserControls.ChangeName(RequestID, Customer.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ChangeName;
                            RequestDetail.DataContext = _ChangeName;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = " تغییر نام ";
                            break;

                        case (int)DB.RequestType.CutAndEstablish:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            CRM.Data.CutAndEstablish cutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_Request.ID);
                            _CutAndEstablish = new UserControls.CutAndEstablish(RequestID, Customer.ID, (long)_Request.TelephoneNo, (int)DB.RequestType.CutAndEstablish);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _CutAndEstablish;
                            RequestDetail.DataContext = _CutAndEstablish;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "قطع";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Cancelation };

                            break;

                        case (int)DB.RequestType.Connect:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            cutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_Request.ID);
                            _CutAndEstablish = new UserControls.CutAndEstablish(RequestID, Customer.ID, (long)_Request.TelephoneNo, (int)DB.RequestType.Connect);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _CutAndEstablish;
                            RequestDetail.DataContext = _CutAndEstablish;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "وصل";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (int)DB.NewAction.Print };

                            break;

                        case (int)DB.RequestType.SpecialService:
                            _specialService = Data.SpecialServiceDB.GetSpecialServiceByID(_Request.ID);
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;

                            _SpecialServiceUserControl = new UserControls.SpecialService(_Request.ID, (long)_Request.TelephoneNo);
                            RequestDetail.Content = _SpecialServiceUserControl;
                            RequestDetail.DataContext = _SpecialServiceUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = " سرویس ویژه ";

                            break;

                        case (int)DB.RequestType.OpenAndCloseZero:

                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _zeroStatus = Data.ZeroStatusDB.GetZeroStatusByID(_Request.ID);

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;

                            _openAndCloseZeroUserControl = new UserControls.OpenAndCloseZero(_Request.ID, (long)_Request.TelephoneNo);
                            RequestDetail.Content = _openAndCloseZeroUserControl;
                            RequestDetail.DataContext = _openAndCloseZeroUserControl;

                            Title = " انسداد صفر ";

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };

                            break;

                        case (int)DB.RequestType.ADSL:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            //Data.ADSLRequest ADSLRequest = DB.SearchByPropertyName<Data.ADSLRequest>("ID", RequestID).SingleOrDefault();

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSL = new UserControls.ADSL(RequestID, Customer.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSL;
                            RequestDetail.DataContext = _ADSL;
                            RequestDetail.Visibility = Visibility.Visible;

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                            {
                                Title = "ثبت درخواست ADSL ";
                                //PaymentLabel.Visibility = Visibility.Collapsed;
                                //RequestPaymentTypeListBox.Visibility = Visibility.Collapsed;

                                if (_ADSL._IsWaitingList)
                                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Exit };
                            }

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                            {
                                Title = "فروش سرویس ADSL ";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Exit };
                                DisableRequestInfo();
                                //RequestPaymentTypeListBox.Visibility = Visibility.Collapsed;
                                //PaymentLabel.Visibility = Visibility.Collapsed;
                                TelephoneInfo.IsExpanded = false;
                                RequestInfo.IsExpanded = false;

                                if (_ADSL._IsWaitingList)
                                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Exit };
                            }

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Feedback).ID)
                            {
                                Title = "واگذاری خطوط ADSL ";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                                DisableRequestInfo();
                            }

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Observation).ID)
                            {
                                Title = "رئیس مرکز ADSL ";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                                DisableRequestInfo();
                            }

                            break;

                        case (int)DB.RequestType.ADSLChangeService:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLChangeService = new UserControls.ADSLChangeService(RequestID, (long)_Request.TelephoneNo, _Request.RequestTypeID);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLChangeService;
                            RequestDetail.DataContext = _ADSLChangeService;
                            RequestDetail.Visibility = Visibility.Visible;

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                            {
                                Title = "فروش شارژ مجدد ADSL ";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            }

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                            {
                                Title = "اعلام نظر امور مشترکین برای تغییر تعرفه ADSL";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Exit };
                                DisableRequestInfo();
                            }

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                            {
                                Title = "اعمال تغییر تعرفه ADSL";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                                DisableRequestInfo();
                            }

                            //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID)
                            //{
                            //    Title = " تغییر تعرفه ADSL";
                            //    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            //    DisableRequestInfo();
                            //}

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                            {
                                Title = " تغییر تعرفه ADSL";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                                DisableRequestInfo();
                            }

                            break;

                        case (int)DB.RequestType.WirelessChangeService:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            // _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            Data.WirelessChangeService wirelessChangeService = WirelessChangeServiceDB.GetWirelessChangeServicebyID(_Request.ID);
                            _WirelessChangeService = new UserControls.WirelessChangeService(RequestID, wirelessChangeService.WirelessCode, _Request.RequestTypeID);

                            //TelephoneInfo.Content = _TelephoneInformation;
                            //TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _WirelessChangeService;
                            RequestDetail.DataContext = _WirelessChangeService;
                            RequestDetail.Visibility = Visibility.Visible;

                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                            {
                                Title = "فروش شارژ مجدد Wireless ";
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            }

                            break;

                        case (int)DB.RequestType.ADSLChangeIP:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLChangeIP = new UserControls.ADSLChangeIP(RequestID, Customer.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLChangeIP;
                            RequestDetail.DataContext = _ADSLChangeIP;
                            RequestDetail.Visibility = Visibility.Visible;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };

                            break;

                        case (int)DB.RequestType.ADSLCutTemporary:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            //Data.ADSLCutTemporary ADSLCutTemporary = DB.SearchByPropertyName<Data.ADSLCutTemporary>("ID", RequestID).SingleOrDefault();

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLCutTemporary = new UserControls.ADSLCutTemporary(RequestID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLCutTemporary;
                            RequestDetail.DataContext = _ADSLCutTemporary;
                            RequestDetail.Visibility = Visibility.Visible;
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };

                            //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                            //    Title = "ثبت درخواست قطع و وصل موقت ADSL ";

                            //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                            //{
                            //    Title = "اعلام نظر امور مشترکین برای قطع موقت ADSL";
                            //    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            //    DisableRequestInfo();
                            //}

                            //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                            //{
                            //    Title = "اعمال قطع موقت ADSL";
                            //    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            //    DisableRequestInfo();
                            //}

                            //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID)
                            //{
                            //    Title = " قطع موقت ADSL";
                            //    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            //    DisableRequestInfo();
                            //}

                            //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                            //{
                            //    Title = " قطع موقت ADSL";
                            //    ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                            //    DisableRequestInfo();
                            //}

                            break;

                        case (int)DB.RequestType.ADSLInstall:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLInstallRequest = new UserControls.ADSLInstallRequestUserControl(_Request.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLInstallRequest;
                            RequestDetail.DataContext = _ADSLInstallRequest;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "نصب ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLDischarge:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLDischarge = new UserControls.ADSLDischargeUserControl(_Request.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLDischarge;
                            RequestDetail.DataContext = _ADSLDischarge;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تخلیه ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLSellTraffic:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLSellTrafficUserControl = new UserControls.ADSLSellTrafficUserControl(_Request.ID, (long)_Request.TelephoneNo, _Request.RequestTypeID);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLSellTrafficUserControl;
                            RequestDetail.DataContext = _ADSLSellTrafficUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "خرید ترافیک ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.WirelessSellTraffic:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            //_TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLSellTrafficUserControl = new UserControls.ADSLSellTrafficUserControl(_Request.ID, 0, RequestType.ID);

                            //  TelephoneInfo.Content = _TelephoneInformation;
                            //  TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLSellTrafficUserControl;
                            RequestDetail.DataContext = _ADSLSellTrafficUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "خرید ترافیک ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLChangePlace:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLChangePlaceUserControl = new UserControls.ADSLChangePlaceUserControl(_Request.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLChangePlaceUserControl;
                            RequestDetail.DataContext = _ADSLChangePlaceUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تغییر مکان ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ADSLChangePort:

                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLChangePort = new UserControls.ADSLChangePort(_Request.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLChangePort;
                            RequestDetail.DataContext = _ADSLChangePort;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تعویض پورت ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Refund, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.ChangeNo:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            // Data.ChangeNo _changeNo = DB.SearchByPropertyName<Data.ChangeNo>("ID", _Request.ID).SingleOrDefault();
                            _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID(_Request.ID);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _changeNo = new UserControls.ChangeNoUserControl(_Request.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _changeNo;
                            RequestDetail.DataContext = _changeNo;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تعویض شماره";
                            break;

                        case (int)DB.RequestType.Reinstall:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _installReqeust = Data.InstallRequestDB.GetInstallRequestByRequestID(_Request.ID);
                            _installdetail = new Install(_Request.ID);
                            RequestDetail.Content = _installdetail;
                            RequestDetail.DataContext = _installdetail;
                            RequestDetail.Visibility = Visibility.Visible;
                            _installdetail.ReinsrallInfo.Visibility = Visibility.Visible;
                            Title = "در خواست دایری مجدد";
                            break;

                        case (int)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ADSLChangeCustomerOwnerCharacteristicsUserControl = new UserControls.ADSLChangeCustomerOwnerCharacteristicsUserControl(_Request.ID, Customer.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ADSLChangeCustomerOwnerCharacteristicsUserControl;
                            RequestDetail.DataContext = _ADSLChangeCustomerOwnerCharacteristicsUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "تغییر مشخصات مالک ADSL";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            break;

                        case (int)DB.RequestType.TitleIn118:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _titleIn118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(RequestID);
                            _TelephoneInformation = new TelephoneInformation(_titleIn118.TelephoneNo, _Request.RequestTypeID);
                            _Title118 = new UserControls.Title118(RequestID, (long)_Request.TelephoneNo, (int)DB.RequestType.TitleIn118);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _Title118;
                            RequestDetail.DataContext = _Title118;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "ثبت عنوان در 118";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };

                            break;

                        case (int)DB.RequestType.RemoveTitleIn118:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _titleIn118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(RequestID);
                            _TelephoneInformation = new TelephoneInformation(_titleIn118.TelephoneNo, _Request.RequestTypeID);
                            _Title118 = new UserControls.Title118(RequestID, (long)_Request.TelephoneNo, (int)DB.RequestType.RemoveTitleIn118);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _Title118;
                            RequestDetail.DataContext = _Title118;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "حذف عنوان در 118";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };

                            break;

                        case (int)DB.RequestType.ChangeTitleIn118:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _titleIn118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(RequestID);
                            _TelephoneInformation = new TelephoneInformation(_titleIn118.TelephoneNo, _Request.RequestTypeID);
                            _Title118 = new UserControls.Title118(RequestID, (long)_Request.TelephoneNo, (int)DB.RequestType.ChangeTitleIn118);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _Title118;
                            RequestDetail.DataContext = _Title118;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = " تغییر عنوان در 118";
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };

                            break;

                        case (int)DB.RequestType.ChangeAddress:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                            _ChangeAddressUserControl = new ChangeAddressUserControl(_Request.ID, (long)_Request.TelephoneNo);

                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _ChangeAddressUserControl;
                            RequestDetail.DataContext = _ChangeAddressUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            Title = "در خواست اصلاح آدرس";
                            break;

                        case (int)DB.RequestType.RefundDeposit:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);

                            _RefundDepositUserControl = new RefundDepositUserControl(_Request.ID, _Request.TelephoneNo ?? 0);
                            TelephoneInfo.Content = _TelephoneInformation;
                            TelephoneInfo.DataContext = _TelephoneInformation;
                            RequestDetail.Content = _RefundDepositUserControl;
                            RequestDetail.DataContext = _RefundDepositUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            DepositTabItem.Visibility = Visibility.Visible;
                            DepositDataGrid.Visibility = Visibility.Visible;
                            DepositDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentDepositByTelephoneNo(Telephone.TelephoneNo);
                            Title = "استرداد ودیعه";
                            break;
                        //TODO:rad 13950523
                        case (int)DB.RequestType.StandardSpaceAndPower:
                            {
                                RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                                _V3SpaceAndPower = new ExtendedSpaceAndPower.V3SpaceAndPowerUserControl();
                                RequestDetail.Content = _V3SpaceAndPower;
                                RequestDetail.DataContext = _V3SpaceAndPower;
                                RequestDetail.Visibility = Visibility.Visible;
                                TelephoneInfo.Visibility = Visibility.Collapsed;
                                FormTabItem.Visibility = Visibility.Visible;
                                this.Title = "ثبت درخواست فضاپاور";
                                break;
                            }
                        case (int)DB.RequestType.SpaceandPower:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);

                            Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                            //چون باید مشابه سایر روال ها می بود و در مراحل مالی فقط یک فرم باز میشد بنابراین فرم جدیدی برای مرحله حوزه مالی فضا و پاور تعریف نکردیم
                            //فقط چون در مرحله حوزه مالی نباید برا روی درخواست اولیه کاری صورت میگرفت ، بلاک ایف زیر اضافه شد تا فقط اطلاعات فضا و پاور
                            //نمایش داده شود
                            if (Status != null && Status.StatusType == (byte)DB.RequestStatusType.GetCosts)
                            {
                                _V2SpaceAndPowerSummaryInfo = new V2SpaceAndPowerInfoSummary(RequestID);
                                RequestDetail.Content = _V2SpaceAndPowerSummaryInfo;
                                RequestDetail.DataContext = _V2SpaceAndPowerSummaryInfo;
                                RequestInfo.IsEnabled = false;
                                _V2SpaceAndPowerSummaryInfo.IsEnabled = true;
                                _V2SpaceAndPowerSummaryInfo.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;

                                //TODO:rad temporary block - SpaceAndPower
                                //این بلاک به صورت موقت میباشد تا زمانی که کاوسی جدول مخابرات مربوط به نرخ متراژ فضا و پاور ها را به ما بدهد
                                if (Data.StatusDB.IsFinalStep(_Request.StatusID))
                                {
                                    //مقداردهی عملیات های مربوطه - با توجه به چرخه کاری
                                    this.ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print };
                                }
                                //در صورت تحویل جدول باید 
                                //1- بلاک بالا حذف گردد
                                //2- در چرخه کاری ، فرم متعلق به مرحله صدور صورتحساب به "صدورصورتحساب" تغییر می یابد
                                // InvoiceIssuanceForm
                                //*******************************************************************************************************************
                            }
                            else if (Status != null && Status.StatusType == (byte)DB.RequestStatusType.TelecomminucationServicePaymentChecking)
                            {
                                //_V2SpaceAndPower = new V2SpaceAndPower(RequestID);
                                //RequestDetail.Content = _V2SpaceAndPower;
                                //RequestDetail.DataContext = _V2SpaceAndPower;
                                //RequestInfo.IsEnabled = false;
                                ////در مرحله عقد قرارداد نیاز به تغییر مشخصات فضا و پاور نیست . فقط باید در هنگام ارجاع کالاوخدمات مخابرات بررسی شوند
                                //_V2SpaceAndPower.IsEnabled = false;
                                //بر اساس فکس های آقای کاوسی در مرحله عقد قرارداد موارد زیر چک لیست هستند
                                //1- بررسی و ثبت کالاخدمات
                                //2- مشاهده تصویر درخواست متقاضی
                                //3- مشاهده فایل هایی که در مراحل طراحی اضافه شده اند
                                //به همین علت از یوزر کنترل دیتا اینتری مربوط به فضاپاور نباید اینجا استفاده میشد و من کامنت کردم

                                _V2SpaceAndPowerSummaryInfo = new V2SpaceAndPowerInfoSummary(RequestID);
                                RequestDetail.Content = _V2SpaceAndPowerSummaryInfo;
                                RequestDetail.DataContext = _V2SpaceAndPowerSummaryInfo;
                                RequestInfo.IsEnabled = false;
                                _V2SpaceAndPowerSummaryInfo.IsEnabled = true;
                                _V2SpaceAndPowerSummaryInfo.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print };
                            }
                            else
                            {
                                _V2SpaceAndPower = new V2SpaceAndPower(RequestID);
                                RequestDetail.Content = _V2SpaceAndPower;
                                RequestDetail.DataContext = _V2SpaceAndPower;
                                RequestInfo.IsEnabled = false;
                                _V2SpaceAndPower.IsEnabled = true;
                            }
                            //پر کردن کمبو باکس مربوط به کالا و خدمات مخابرات
                            this._TelecomminucationServiceInfos = TelecomminucationServiceDB.GetTelecomminucationServiceInfosByRequestTypeID((int)DB.RequestType.SpaceandPower);
                            this._TelecomminucationServicePaymentInfos = new ObservableCollection<TelecomminucationServicePaymentInfo>(TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentInfos(RequestID));
                            RequestTelecomminucationServiceDataGrid.ItemsSource = this._TelecomminucationServicePaymentInfos;

                            //چنانچه در مرحله نهایی روال باشد، نباید امکان حذف کالا و خدمات مخابرات داده شود
                            if (StatusDB.IsFinalStep(_Request.StatusID))
                            {
                                DeleteTelecomminucationServicePaymentMenuItem.IsEnabled = false;
                            }
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                                Title = "ثبت درخواست فضا و پاور ";
                            break;

                        case (int)DB.RequestType.E1:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _e1 = Data.E1DB.GetE1ByRequestID(_Request.ID);
                            if (StatusDB.IsStartStep(Status.ID))
                            {
                                _E1 = new CRM.Application.UserControls.E1(RequestID, DB.RequestType.E1);
                                RequestDetail.Content = _E1;
                                RequestDetail.DataContext = _E1;
                            }
                            else
                            {
                                //چون در گیلان در مرحله مدیرمالی-صدور قرارداد وارد این بلاک میشود و ازانجایی که در متد ذخیره از یوزر کنترل زیر استفاده میشود
                                //in body of Save_E1Request() uses _E1
                                //not _E1InfoSummary
                                //خطای نال رفرنس میدهد دو خط زیر فقط برای جلوگیری از ایجاد خطا اضافه شده است
                                //TODO:rad 13950316
                                _E1 = new CRM.Application.UserControls.E1(RequestID, DB.RequestType.E1);
                                _E1.LoadData();
                                //end TODO:rad

                                _E1InfoSummary = new E1InfoSummary(_Request.ID, null);
                                _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                                RequestDetail.Content = _E1InfoSummary;
                                RequestDetail.DataContext = _E1InfoSummary;
                                RequestDetail.IsEnabled = true;
                            }
                            //پر کردن کومبوباکس مربوط به کالا و خدمات مخابرات
                            this._TelecomminucationServiceInfos = TelecomminucationServiceDB.GetTelecomminucationServiceInfosByRequestTypeID((int)DB.RequestType.E1);
                            this._TelecomminucationServicePaymentInfos = new ObservableCollection<TelecomminucationServicePaymentInfo>(TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentInfos(RequestID));
                            RequestTelecomminucationServiceDataGrid.ItemsSource = this._TelecomminucationServicePaymentInfos;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست E1,STM1,...";
                            break;

                        case (int)DB.RequestType.E1Link:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _e1 = Data.E1DB.GetE1ByRequestID(_Request.ID);
                            _E1 = new CRM.Application.UserControls.E1(RequestID, DB.RequestType.E1Link, Telephone.TelephoneNo);
                            RequestDetail.Content = _E1;
                            RequestDetail.DataContext = _E1;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            Title = "لینک";
                            break;

                        case (int)DB.RequestType.VacateE1:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _e1 = Data.E1DB.GetE1ByRequestID(_Request.ID);
                            _VacateE1UserControl = new CRM.Application.UserControls.VacateE1UserControl(RequestID, Telephone.TelephoneNo);

                            if (Data.StatusDB.IsFinalStep(_Request.StatusID))
                                _VacateE1UserControl.PointsInfoDataGrid.IsEnabled = false;

                            RequestDetail.Content = _VacateE1UserControl;
                            RequestDetail.DataContext = _VacateE1UserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            Title = "تخلیه";



                            break;

                        case (int)DB.RequestType.E1Fiber:
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _E1FiberUseControl = new CRM.Application.UserControls.E1FiberUseControl(RequestID);
                            RequestDetail.Content = _E1FiberUseControl;
                            RequestDetail.DataContext = _E1FiberUseControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            Title = "ثبت درخواست E1(فیبر) ";

                            break;

                        case (int)DB.RequestType.SpecialWire:

                            if (!CheckInvestigatePosibilityOtherPoint(_Request.ID) && Status.StatusType == (byte)DB.RequestStatusType.GetCosts)
                            {
                                Folder.MessageBox.ShowInfo("تا امکان سنجی شدن همه نقاط دیگر سیم خصوصی  امکان استفاده از این فرم نمی باشد");
                                this.Close();
                                return;
                            }

                            if (RequestDB.CheckForReuqestChild(_Request.ID) && StatusDB.IsStartStep(_Request.StatusID))
                            {
                                this.IsEnabled = false;
                            }

                            _specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_Request.ID);
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _SpecialWireUserControl = new CRM.Application.UserControls.SpecialWireUserControl(RequestID);
                            _SpecialWireUserControl.CenterID = _Request.CenterID;
                            RequestDetail.Content = _SpecialWireUserControl;
                            RequestDetail.DataContext = _SpecialWireUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;

                            if (_Request.CustomerID != null)
                            {
                                _customerInfoSummary = new CustomerInfoSummary(_Request.CustomerID);
                                CustomerInfoUC.Mode = true;
                                CustomerInfoUC.Content = _customerInfoSummary;
                                CustomerInfoUC.DataContext = _customerInfoSummary;
                                CustomerInfoUC.Visibility = Visibility.Visible;
                            }
                            Title = "سیم خصوصی";

                            break;

                        case (int)DB.RequestType.VacateSpecialWire:
                            _vacateSpecialWire = Data.VacateSpecialWireDB.GetVacateSpecialWireByRequestID(_Request.ID);
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _VacateSpecialWireUserControl = new CRM.Application.UserControls.VacateSpecialWireUserControl(RequestID);
                            RequestDetail.Content = _VacateSpecialWireUserControl;
                            RequestDetail.DataContext = _VacateSpecialWireUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            Title = "تخلیه سیم خصوصی";
                            break;

                        case (int)DB.RequestType.ChangeLocationSpecialWire:
                            _changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_Request.ID);
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _ChangeLocationSpecialWireUserControl = new CRM.Application.UserControls.ChangeLocationSpecialWireUserControl(RequestID);
                            RequestDetail.Content = _ChangeLocationSpecialWireUserControl;
                            RequestDetail.DataContext = _ChangeLocationSpecialWireUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            Title = "تغییر مکان سیم خصوصی";
                            break;
                        case (int)DB.RequestType.PBX:
                            _telephonePBX = new Data.TelephonePBX();
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _PBXUserControl = new CRM.Application.UserControls.PBXUserControl(RequestID, Telephone.TelephoneNo);
                            RequestDetail.Content = _PBXUserControl;
                            RequestDetail.DataContext = _PBXUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Collapsed;
                            if (_Request != null && _Request.ID != 0 && Data.StatusDB.IsFinalStep(_Request.StatusID))
                            {
                                RequestDetail.IsEnabled = false;
                            }
                            Title = "PBX";
                            break;
                        case (int)DB.RequestType.Wireless:

                            // روال وایرلس از روی ای دی اس ال کپی شده
                            // 
                            Data.RequestStep reqeustStep = RequestStepDB.GetRequestStepByID(this.currentStep);
                            if (reqeustStep.StepTitle.Contains("فروش"))
                            {
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Refund };
                            }
                            RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                            _wirelessUserControl = new CRM.Application.UserControls.Wireless(RequestID);
                            RequestDetail.Content = _wirelessUserControl;
                            RequestDetail.DataContext = _wirelessUserControl;
                            RequestDetail.Visibility = Visibility.Visible;
                            TelephoneInfo.Visibility = Visibility.Visible;
                            FormTabItem.Visibility = Visibility.Visible;
                            Title = "Wireless";
                            break;

                        default:
                            break;
                    }

                    // get cost information
                    InsertRequestPayment();
                    //

                    //
                    FillVisitInfo();
                    //
                    this.RequestContractGrid.Items.Refresh();

                    #endregion Load reqeust
                }
                if (Telephone != null)
                {
                    if (_Request != null && _Request.RequestTypeID != (int)DB.RequestType.ChangeLocationSpecialWire)
                    {
                        CityID = Data.CityDB.GetCityByCenterID(Telephone.CenterID).ID;
                        (this.RequestInfo.DataContext as Request).CenterID = Telephone.CenterID;
                        CenterComboBox.IsEnabled = false;
                        CityComboBox.IsEnabled = false;
                    }
                    // get Phone Records information in current cycle

                    Cycle cycle = new Cycle();
                    // for not Hamper error in get current cycle in load form, The Try was considered to be
                    try
                    {
                        cycle = Data.CycleDB.GetDateCurrentCycle();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Sequence contains more than one element"))
                            Folder.MessageBox.ShowInfo("با تاریخ فعلی چند دوره یافت شد. سوابق تلفن در دوره جاری قابل در یافت نیست لطفا اطلاعات دوره ها را اصلاح کنید.");
                    }

                    if (cycle != null)
                    {

                        List<RequestLog> reqeustLogs = new List<RequestLog>();
                        // ReqeustDataGridComboBoxColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestType));


                        //// for not Hamper error in get reqeustLogs in load form, The Try was considered to be
                        //try
                        //{
                        //    reqeustLogs = Data.RequestLogDB.GetReqeustLogByTelephone(Telephone.TelephoneNo, (DateTime)cycle.FromDate, (DateTime)cycle.ToDate);
                        //}
                        //catch
                        //{
                        //    Folder.MessageBox.ShowInfo("در دریافت سوابق تلفن خطا رخ داده است.");
                        //}


                        //PhoneRecordsInfoDataGrid.ItemsSource = reqeustLogs;
                        //PhoneRecordsInfo.Visibility = Visibility.Visible;
                    }

                    //
                }
                if (_TelephoneNo != 0)
                {
                    Telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                    if (Telephone != null)
                    {
                        CityID = Data.CityDB.GetCityByCenterID(Telephone.CenterID).ID;
                        (this.RequestInfo.DataContext as Request).CenterID = Telephone.CenterID;
                        CenterComboBox.IsEnabled = false;
                        CityComboBox.IsEnabled = false;
                    }
                    else
                    {
                        if (city == "kermanshah" && RequestType.ID == (byte)DB.RequestType.Failure117)
                        {
                            TelephoneTemp telephoneTemp = TelephoneDB.GetTelephoneTemp(_TelephoneNo);

                            if (telephoneTemp != null)
                            {
                                CityID = Data.CityDB.GetCityByCenterID((int)telephoneTemp.CenterID).ID;
                                (this.RequestInfo.DataContext as Request).CenterID = (int)telephoneTemp.CenterID;
                                CenterComboBox.IsEnabled = false;
                                CityComboBox.IsEnabled = false;
                            }
                        }
                    }
                }

                if (CityID == 0)
                    CityComboBox.SelectedIndex = 0;
                else
                    CityComboBox.SelectedValue = CityID;

                if (fixTelephoneRequests() && Customer != null)
                    _Request.RequesterName = Customer.FirstNameOrTitle + " " + Customer.LastName;

                ResizeWindow();

            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در نمایش اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در نمایش اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در نمایش اطلاعات، " + ex.Message + " !", ex);
            }
        }

        private bool CheckInvestigatePosibilityOtherPoint(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.MainRequestID == requestID && t.IsCancelation == false).All(t => t.WaitForToBeCalculate == true);
            }
        }

        private void FillVisitInfo()
        {
            long addressID = -1;
            addressID = GetAddressOfRequest();
            // get OutBound information
            if (!(addressID == -1 || addressID == null))
            {

                _VisitInfoList = Data.VisitAddressDB.GetVisitAddressByRequestID(_Request.ID, (long)addressID).OrderByDescending(t => t.ID).ToList();
                if (_VisitInfoList.Count != 0)
                {
                    VisitInfo.Visibility = Visibility.Visible;
                    CrossPostComboBoxColumn.ItemsSource = Data.PostDB.GetPostCheckable();
                    VisitInfoGrid.ItemsSource = _VisitInfoList;
                    VisitInfo.DataContext = _VisitInfoList;
                }
            }
            //else
            //{
            //Folder.MessageBox.ShowInfo("آدرس برای این روال  یافت نشد لطفا با مدیر سیستم تماس بگیرید.");
            //}
            ////
        }

        private long GetAddressOfRequest()
        {
            long? addressID = -1;
            switch (_Request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    addressID = _installReqeust.InstallAddressID;
                    break;
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    addressID = _changeLocation.NewInstallAddressID;
                    break;
                case (int)DB.RequestType.E1:
                    addressID = _e1.InstallAddressID;
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    addressID = _specialWire != null ? _specialWire.InstallAddressID : 0;
                    break;
            }

            return addressID ?? -1;
        }

        private void SearchCustomerClick(object sender, RoutedEventArgs e)
        {
        }

        private void DisableRequestInfo()
        {
            RequesterNametextBox.IsReadOnly = true;
            CityComboBox.IsEnabled = false;
            CenterComboBox.IsEnabled = false;
            //RequestPaymentTypeListBox.IsEnabled = false;
            ReqDate.IsEnabled = false;
            RequestLetterNotextBox.IsReadOnly = true;
            ReqLetterDate.IsEnabled = false;
            //ReqRepresentitiveExpireDate.IsEnabled = false;
            //RepresentitiveNoTextBox.IsReadOnly = true;
            //ReqRepresentitiveDate.IsEnabled = false;
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
                _Request.IsViewed = true;
                _Request.WaitForToBeCalculate = null;

                if (RequestType == null)
                    return false;

                Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                if (Status != null && (Status.StatusType == (byte)DB.RequestStatusType.Observation || Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter || Status.StatusType == (byte)DB.RequestStatusType.GetCosts))
                {
                    IsSaveSuccess = true;
                    return true;
                }

                //TODO:rad 13950524
                if (_Request.RequestTypeID == (int)DB.RequestType.SpaceandPower && Status.StatusType == (byte)DB.RequestStatusType.TelecomminucationServicePaymentChecking) //اگر در روال فضاپاور در مرحله عقدقرارداد و وضعیت بررسی کالاخدمات باشد
                {
                    IsSaveSuccess = true;
                    return true;
                }

                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.Dayri:
                    case (int)DB.RequestType.Reinstall:
                        Save_InstallRequest();
                        break;

                    case (int)DB.RequestType.ChangeLocationCenterInside:
                        Save_ChangeLocationCenterInsideRequest();
                        break;

                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        Save_ChangeLocationCenterToCenterRequest();

                        break;

                    case (int)DB.RequestType.Dischargin:
                        Save_DischarginTelephoneRequest();
                        break;

                    case (int)DB.RequestType.RefundDeposit:
                        Save_RefundDepositRequest();
                        break;

                    case (int)DB.RequestType.ChangeName:
                        Save_ChangeNameRequest();
                        break;

                    case (int)DB.RequestType.CutAndEstablish:
                    case (int)DB.RequestType.Connect:
                        Save_CutAndEstablish();
                        break;

                    case (int)DB.RequestType.SpecialService:
                        Save_SpecialServiceRequest();
                        break;

                    case (int)DB.RequestType.OpenAndCloseZero:
                        Save_OpenAndCloseZeroRequest();
                        break;

                    case (int)DB.RequestType.ADSL:
                        Save_ADSLRequest();
                        break;
                    case (int)DB.RequestType.Wireless:
                        Save_WirelessRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        Save_ADSLChangeServiceRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeIP:
                        Save_ADSLChangeIPRequest();
                        break;

                    case (int)DB.RequestType.ADSLInstall:
                        Save_ADSLInstallRequest();
                        break;

                    case (int)DB.RequestType.ADSLCutTemporary:
                        Save_ADSLCutRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangePlace:
                        Save_ADSLChangePlaceRequest();
                        break;

                    case (int)DB.RequestType.ADSLDischarge:
                        Save_ADSLDischargeRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangePort:
                        Save_ADSLChangePortLRequest();
                        break;

                    case (int)DB.RequestType.ADSLSellTraffic:
                        Save_ADSLSellTraffic();
                        break;

                    case (int)DB.RequestType.WirelessSellTraffic:
                        Save_WirelessSellTraffic();
                        break;

                    case (int)DB.RequestType.WirelessChangeService:
                        Save_WirelessChangeServiceRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                        Save_ADSLChangeCustomerOwnerCharacteristics();
                        break;

                    case (int)DB.RequestType.ChangeNo:
                        Save_ChangeNoRequest();
                        break;

                    case (int)DB.RequestType.TitleIn118:
                    case (int)DB.RequestType.RemoveTitleIn118:
                    case (int)DB.RequestType.ChangeTitleIn118:
                        Save_TitleIn118Request();

                        break;

                    case (int)DB.RequestType.ChangeAddress:
                        Save_ChangeAddressRequest();
                        break;

                    case (int)DB.RequestType.SpaceandPower:
                        {
                            Save_SpaceAndPowerRequest();
                            Status status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);

                            //کلیه ی کالا و خدمات مخابرات ، درخواست درحال توسعه باید برای درخواست جاری کپی شود
                            if (_Request.MainRequestID.HasValue && status.StatusType == (byte)DB.RequestStatusType.Start)
                            {
                                List<TelecomminucationServicePayment> mainRequestTelecomminucationServicePayments = TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentsByRequestID(_Request.MainRequestID.Value);
                                DB.CopyTelecomminucationServicePayments(_Request.ID, mainRequestTelecomminucationServicePayments);
                            }
                        }
                        break;
                    case (int)DB.RequestType.Failure117:
                        Save_Failure117Request();
                        break;

                    case (int)DB.RequestType.E1:
                    case (int)DB.RequestType.E1Link:
                        {
                            Save_E1Request();
                            Status status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);

                            //کلیه ی کالا و خدمات مخابرات ، درخواست درحال توسعه باید برای درخواست جاری کپی شود
                            if (_Request.MainRequestID.HasValue && status.StatusType == (byte)DB.RequestStatusType.Start)
                            {
                                List<TelecomminucationServicePayment> mainRequestTelecomminucationServicePayments = TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentsByRequestID(_Request.MainRequestID.Value);
                                DB.CopyTelecomminucationServicePayments(_Request.ID, mainRequestTelecomminucationServicePayments);
                            }
                        }
                        break;
                    case (int)DB.RequestType.VacateE1:
                        Save_VacateE1Request();
                        break;

                    case (int)DB.RequestType.E1Fiber:
                        Save_E1FiberRequest();
                        break;

                    case (int)DB.RequestType.SpecialWire:
                        Save_specialWireRequest();
                        break;

                    case (int)DB.RequestType.VacateSpecialWire:
                        Save_VacateSpecialWireRequest();
                        break;

                    case (int)DB.RequestType.ChangeLocationSpecialWire:
                        Save_ChangeLocationSpecialWireRequest();
                        break;

                    case (int)DB.RequestType.PBX:
                        Save_PBXRequest();
                        break;


                    default:
                        break;
                }

                RequestID = _Request.ID;
                Documents.IsEnabled = true;

                // در پایان متد لود این متد مجدد اجرا می شود
                //  InsertRequestPayment();
                LoadData();

                IsSaveSuccess = true;

                ShowSuccessMessage("با موفقیت ذخیره شد");
            }
            catch (NullReferenceException ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات، " + ex.Message + " !", ex);
            }

            return IsSaveSuccess;
        }

        private void Save_PBXRequest()
        {

            using (TransactionScope ts = new TransactionScope())
            {



                List<PBXTelephone> PBXTelephones = new List<PBXTelephone>(_PBXUserControl.TelephoneDataGrid.ItemsSource as ObservableCollection<PBXTelephone>);
                List<PBXTelephone> OldPBXTelephones = _PBXUserControl.OldPBXs;

                if (_PBXUserControl.HeadTelephoneComboBox.SelectedValue == null)
                    throw new Exception("لطفا شماره سر شماره را انتخاب کنید");

                if (PBXTelephones.Any(t => t.TelephoneNo == (long)_PBXUserControl.HeadTelephoneComboBox.SelectedValue))
                    throw new Exception("تلفن سرشماره نمی تواند در لیست تلفن های دیگر بیاید");

                if (PBXTelephones.GroupBy(t => t.TelephoneNo).Any(t => t.Count() > 1))
                    throw new Exception("امکان قرار دادن تلفن تکراری در لیست تلفن ها نیست");

                if (PBXTelephones.Count == 0)
                    throw new Exception("هیچ تلفنی انتخاب نشده است");


                if (_relatedRequestID != 0)
                    _Request.RelatedRequestID = _relatedRequestID;
                else
                    _Request.RelatedRequestID = null;

                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Telephone.CustomerID;

                if (RequestID == 0)
                {
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.RequestTypeID = RequestType.ID;
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);
                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);


                }

                if (OldPBXTelephones.Count() > 0)
                {
                    DB.DeleteAll<TelephonePBX>(OldPBXTelephones.Select(t => t.HeadTelephoneNo).Take(1).ToList());
                }

                List<TelephonePBX> telephonePBX = new List<TelephonePBX>();

                for (int i = 1; i <= PBXTelephones.Count(); i++)
                {
                    TelephonePBX item = new TelephonePBX();
                    item.HeadTelephone = Telephone.TelephoneNo;
                    item.OtherTelephone = PBXTelephones[i - 1].TelephoneNo;
                    item.priority = i;
                    telephonePBX.Add(item);
                }

                DB.SaveAll(telephonePBX);
                ts.Complete();
                IsSaveSuccess = true;
            }
        }

        private void Save_VacateSpecialWireRequest()
        {
            // only source center can change information
            if (_Request.MainRequestID != null)
            {
                return;
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                List<SpecialWirePoints> specialWirePoints = _VacateSpecialWireUserControl.SpecialWirePoints;

                if (specialWirePoints.All(t => t.IsSelect == false))
                    throw new Exception("هیچ مرکزی برای تخلیه انتخاب نشده است");

                long telephoneNo = 0;
                if (!long.TryParse(_VacateSpecialWireUserControl.TelephoneNoTextBox.Text.Trim(), out telephoneNo))
                    throw new Exception("تلفن یافت نشد");

                Bucht sourceBucht = new Bucht();
                Center sourceCenter = Data.SpecialWireDB.GetSourceCenterSpecialWireByTelephoneNo(telephoneNo, out sourceBucht);

                if (sourceCenter.ID == 0 || sourceBucht.ID == 0)
                    throw new Exception("بوخت یا مرکز مبدا سیم خصوصی یافت نشد.");


                if (!specialWirePoints.Where(t => t.IsSelect == true).Select(t => t.CenterID).Contains((int)CenterComboBox.SelectedValue))
                    throw new Exception("حداقل یک نقطه از مرکز جاری در بین نقاط انتخابی باید باشد.");

                if ((int)CenterComboBox.SelectedValue == sourceCenter.ID)
                {
                    if (specialWirePoints.Where(t => t.IsSelect == true).Any(t => t.BuchtID == sourceBucht.ID))
                    {
                        if (!specialWirePoints.All(t => t.IsSelect == true))
                        {
                            throw new Exception("مرکز مبدا در صورتی که همه نقاط تخلیه شوند امکان تخلیه دارد.");
                        }
                    }

                }
                else
                {
                    if (specialWirePoints.Where(t => t.IsSelect == true).Any(t => t.CenterID != (int)CenterComboBox.SelectedValue))
                        throw new Exception("در این مرکز فقط می توان نقطه مربوط به این مرکز را تخلیه کنید.");
                }

                specialWirePoints = specialWirePoints.Where(t => t.IsSelect == true).ToList();

                if (_relatedRequestID != 0)
                    _Request.RelatedRequestID = _relatedRequestID;
                else
                    _Request.RelatedRequestID = null;

                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = _VacateSpecialWireUserControl.customer.ID;
                int startStatus = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;

                if (RequestID == 0)
                {
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.RequestTypeID = RequestType.ID;
                    _Request.TelephoneNo = telephoneNo;
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(telephoneNo);
                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                    telephone.Detach();
                    DB.Save(telephone);
                }
                else
                {
                    DB.DeleteAll<VacateSpecialWirePoint>(Data.VacateSpecialWirePointsDB.GetVacateSpecialWirePointsByRequestID(_Request.ID).Select(t => t.ID).ToList());
                    DB.Delete<CRM.Data.VacateSpecialWire>(_Request.ID);
                }
                List<VacateSpecialWirePoint> vacateSpecialWirePoints = new List<VacateSpecialWirePoint>();
                specialWirePoints.ForEach(item =>
                {
                    VacateSpecialWirePoint vacateSpecialWirePoint = new VacateSpecialWirePoint();
                    vacateSpecialWirePoint.BuchtID = (long)item.BuchtID;
                    vacateSpecialWirePoint.CenterID = item.CenterID;
                    vacateSpecialWirePoint.RequestID = _Request.ID;
                    vacateSpecialWirePoints.Add(vacateSpecialWirePoint);
                });
                DB.SaveAll(vacateSpecialWirePoints);

                SpecialWirePoints firstItem = specialWirePoints.Where(t => t.CenterID == _Request.CenterID).Take(1).SingleOrDefault();
                if (firstItem == null) throw new Exception("حداقل یک مرکز در لیست انتخابی باید در مرکز فعلی باشد");

                CRM.Data.VacateSpecialWire vacatespecialWirePoint = new Data.VacateSpecialWire();
                vacatespecialWirePoint.RequestID = _Request.ID;
                vacatespecialWirePoint.BuchtID = (long)firstItem.BuchtID;
                vacatespecialWirePoint.CabinetInputID = (long)firstItem.CabinetInputID;
                vacatespecialWirePoint.OtherBuchtID = (long?)firstItem.OtherBuchtID;
                vacatespecialWirePoint.PostContactID = (long)firstItem.PostContactID;
                vacatespecialWirePoint.SwitchPortID = (int)firstItem.SwitchPortID;
                vacatespecialWirePoint.SpecialTypeID = (int)DB.SpecialWireType.General;
                if (firstItem.InstallAddressID != null)
                {
                    vacatespecialWirePoint.OldInstallAddressID = (int)firstItem.InstallAddressID;
                    vacatespecialWirePoint.OldCorrespondenceAddressID = (int)firstItem.CorrespondenceAddressID;
                }
                else
                {
                    Telephone telephon = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
                    vacatespecialWirePoint.OldInstallAddressID = (int)telephon.InstallAddressID;
                    vacatespecialWirePoint.OldCorrespondenceAddressID = (int)telephon.CorrespondenceAddressID;
                }
                vacatespecialWirePoint.OldCustomerID = (long)_Request.CustomerID;
                vacatespecialWirePoint.Detach();
                DB.Save(vacatespecialWirePoint, true);


                ts.Complete();
            }
        }

        private void Save_specialWireRequest()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                _specialWire = _SpecialWireUserControl.DataContext as CRM.Data.SpecialWire;

                if (_SpecialWireUserControl.TelephoneNoComboBox.SelectedValue == null)
                    throw new Exception("لطفا تلفن را انتخاب کنید");

                if (_SpecialWireUserControl.Customer == null)
                    throw new Exception("لطفا اطلاعات مشترک را وارد کنید ");

                if (_SpecialWireUserControl.BuchtTypeComboBox.SelectedValue == null)
                    throw new Exception("لطفا نوع سیم خصوصی را وارد کنید ");

                if (_SpecialWireUserControl._specialWirePoints.Count == 0)
                    throw new Exception("لطفا اطلاعات نقاط سیم خصوصی را وارد کنید ");

                if (_SpecialWireUserControl._specialWirePoints.Any(t => t.InstallAddressID == null && t.SpecialWireTypeID == (int)DB.SpecialWireType.General))
                    throw new Exception("اطلاعات کد پستی " + _SpecialWireUserControl._specialWirePoints.FirstOrDefault(t => t.InstallAddressID == null && t.SpecialWireTypeID == (byte)DB.SpecialWireType.General) + " یافت نشده است");

                if (_SpecialWireUserControl._specialWirePoints.Select(t => t.InstallAddressID).GroupBy(t => t).Any(t => t.Count() > 1))
                    throw new Exception("آدرس تکراری در نقاط قایل تعریف نیست");

                SpecialWirePoints sourceSpecialWire = _SpecialWireUserControl._specialWirePoints.Where(t => t.CenterID == (int)CenterComboBox.SelectedValue).Take(1).SingleOrDefault();
                if (sourceSpecialWire == null)
                    throw new Exception("مرکز مبدا یافت نشد (مرکز ثبت کننده درخواست باید حداقل یک نقطه داشته باشد( .");

                if (_SpecialWireUserControl.CorrespondenceAddress == null)
                {
                    _SpecialWireUserControl.CorrespondencePostalCodeTextBox.Focus();
                    throw new Exception("آدرس مکاتبه را وارد کنید");
                }

                Customer = _SpecialWireUserControl.Customer;

                int startStatus = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                // Reserve telephone inforamtion
                Telephone telephone = new Telephone();
                if ((long)_specialWire.TelephoneNo != (long)_SpecialWireUserControl.oldTelephone)
                {
                    if (_SpecialWireUserControl.oldTelephone != 0)
                    {
                        telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(_SpecialWireUserControl.oldTelephone);
                        telephone.CustomerID = null;
                        telephone.InstallAddressID = null;
                        telephone.CorrespondenceAddressID = null;
                        telephone.Status = (byte)DB.TelephoneStatus.Free;
                        telephone.Detach();
                        DB.Save(telephone);
                    }
                    telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo((long)_specialWire.TelephoneNo);
                    telephone.CustomerID = Customer.ID;
                    telephone.InstallAddressID = (long)sourceSpecialWire.InstallAddressID;
                    telephone.CorrespondenceAddressID = _specialWire.CorrespondenceAddressID;
                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                    telephone.Detach();
                    DB.Save(telephone);
                }

                _Request.CustomerID = Customer.ID;
                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.TelephoneNo = Convert.ToInt64(_SpecialWireUserControl.TelephoneNoComboBox.SelectedValue);

                if (_Request.ID == 0)
                {
                    // Save request
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = startStatus;
                    _Request.RequestTypeID = (int)DB.RequestType.SpecialWire;
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);


                    _specialWire.RequestID = _Request.ID;
                    _specialWire.NearestTelephone = sourceSpecialWire.NearestTelephoneNo;
                    _specialWire.InstallAddressID = (long)sourceSpecialWire.InstallAddressID;
                    _specialWire.CorrespondenceAddressID = (long)_SpecialWireUserControl.CorrespondenceAddress.ID;
                    _specialWire.SpecialWireType = (int)DB.SpecialWireType.General;
                    _specialWire.Detach();
                    DB.Save(_specialWire, true);
                }
                else
                {
                    // delete all info spciallWire for this request
                    if (_Request.MainRequestID == null)
                    {
                        DB.DeleteAll<SpecialWirePoint>(Data.SpecialWirePointsDB.GetSpecialWirePointsByRequestID(_Request.ID).Select(t => t.ID).ToList());

                        _specialWire.NearestTelephone = sourceSpecialWire.NearestTelephoneNo;
                        _specialWire.InstallAddressID = (long)sourceSpecialWire.InstallAddressID;
                        _specialWire.CorrespondenceAddressID = (long)_SpecialWireUserControl.CorrespondenceAddress.ID;
                    }

                    _Request.Detach();
                    DB.Save(_Request, false);


                    _specialWire.Detach();
                    DB.Save(_specialWire, false);
                }
                // insert new info specialWire for this  request
                if (_Request.MainRequestID == null)
                {
                    List<SpecialWirePoint> specialWirePointList = new List<SpecialWirePoint>();
                    _SpecialWireUserControl._specialWirePoints.ToList().ForEach(item =>
                                                                          {
                                                                              SpecialWirePoint specialWirePoint = new SpecialWirePoint();
                                                                              specialWirePoint.RequestID = _Request.ID;
                                                                              specialWirePoint.CenterID = item.CenterID;
                                                                              specialWirePoint.NearestTelephoneNo = item.NearestTelephoneNo;
                                                                              specialWirePoint.PostalCode = item.PostalCode;
                                                                              specialWirePoint.InstallAddressID = (long?)item.InstallAddressID;
                                                                              specialWirePoint.AddressContent = item.AddressContent;
                                                                              specialWirePoint.SpecialWireType = item.SpecialWireTypeID;
                                                                              specialWirePointList.Add(specialWirePoint);
                                                                          });
                    DB.SaveAll<SpecialWirePoint>(specialWirePointList);
                    RequestPayment requestPayment = Data.RequestPaymentDB.GetRequestPaymentByBaseCostID((int)DB.SpecialCostID.BetweenCenterSpecialWireCostID, _Request.ID);
                    if (specialWirePointList.Count > 1 && requestPayment == null)
                    {
                        BaseCost cost = Data.BaseCostDB.GetBaseCostByID((int)DB.SpecialCostID.BetweenCenterSpecialWireCostID);
                        requestPayment = new RequestPayment();
                        requestPayment.InsertDate = DB.GetServerDate();
                        requestPayment.BaseCostID = cost.ID;
                        requestPayment.RequestID = _Request.ID;
                        requestPayment.PaymentType = (byte)cost.PaymentType;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;
                        requestPayment.IsPaid = false;
                        requestPayment.Cost = cost.Cost;
                        requestPayment.Tax = cost.Tax;

                        if (cost.Tax != null)
                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                        else
                            requestPayment.AmountSum = cost.Cost;

                        DB.Save(requestPayment);
                    }
                    else if (specialWirePointList.Count == 1)
                    {
                        if (requestPayment != null)
                        {

                            List<InstallmentRequestPayment> installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                            if (installmentRequestPayments.Count != 0)
                            {
                                DB.DeleteAll<InstallmentRequestPayment>(installmentRequestPayments.Select(t => t.ID).ToList());

                                if (requestPayment.RelatedRequestPaymentID != null)
                                {
                                    RequestPayment PrePayment = Data.RequestPaymentDB.GetRequestPaymentByID((long)requestPayment.RelatedRequestPaymentID);
                                    if (PrePayment != null && PrePayment.IsPaid == false)
                                        DB.Delete<RequestPayment>(PrePayment.ID);
                                    else
                                        if (PrePayment != null && PrePayment.IsPaid == true)
                                            Folder.MessageBox.ShowError("پیش پرداخت هزینه پرداخت شده است امکان حذف اقساط نمی باشد");

                                }
                            }

                            if (requestPayment.IsPaid == false)
                                DB.Delete<RequestPayment>(requestPayment.ID);
                            else
                                if (requestPayment.IsPaid == true)
                                    Folder.MessageBox.ShowError("هزینه پرداخت شده است امکان حذف نمی باشد");
                        }

                    }

                }

                ts.Complete();
            }


        }

        private void Save_ChangeLocationSpecialWireRequest()
        {
            // only source center can change information
            if (_Request.MainRequestID != null)
            {
                return;
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                List<SpecialWirePoints> specialWirePoints = _ChangeLocationSpecialWireUserControl.SpecialWirePoints;

                if (specialWirePoints.All(t => t.IsSelect == false))
                    throw new Exception("هیچ مرکزی برای تخلیه انتخاب نشده است");

                long telephoneNo = 0;
                if (!long.TryParse(_ChangeLocationSpecialWireUserControl.TelephoneNoTextBox.Text.Trim(), out telephoneNo))
                    throw new Exception("تلفن یافت نشد");

                Bucht sourceBucht = new Bucht();
                Center sourceCenter = Data.SpecialWireDB.GetSourceCenterSpecialWireByTelephoneNo(telephoneNo, out sourceBucht);

                if (sourceCenter.ID == 0 || sourceBucht.ID == 0)
                    throw new Exception("بوخت یا مرکز مبدا سیم خصوصی یافت نشد.");


                if ((int)CenterComboBox.SelectedValue != sourceCenter.ID)
                {
                    if (specialWirePoints.Where(t => t.IsSelect == true).Any(t => t.CenterID != (int)CenterComboBox.SelectedValue))
                        throw new Exception("در این مرکز فقط می توان نقاط مربوط به این مرکز را تغییر مکان دهید.");
                }
                else
                {
                    //  if (specialWirePoints.Where(t => t.IsSelect == true).Any(t => t.CenterID != (int)CenterComboBox.SelectedValue))
                    //       throw new Exception("ثبت درخواست مرکز مبدا به همراه دیگر مراکز امکان پذیر می باشد");
                }

                if (specialWirePoints.Where(t => t.IsSelect == true).Any(t => t.SpecialWireTypeID == (int)DB.SpecialWireType.Middle))
                    throw new Exception("امکان تغییر مکان نقطه میانی سیم خصوصی نمی باشد");


                if (specialWirePoints.Where(t => t.IsSelect == true && t.SpecialWireTypeID == (int)DB.SpecialWireType.General).Any(t => t.NewInstallAddressID == null))
                    throw new Exception("اطلاعات آدرس جدید یافت نشد.");

                if (specialWirePoints.Where(t => t.IsSelect == true).Select(t => t.NewInstallAddressID).GroupBy(t => t).Any(t => t.Count() > 1))
                    throw new Exception("آدرس تکراری در نقاط قایل تعریف نیست");

                specialWirePoints = specialWirePoints.Where(t => t.IsSelect == true).ToList();

                if (_relatedRequestID != 0)
                    _Request.RelatedRequestID = _relatedRequestID;
                else
                    _Request.RelatedRequestID = null;

                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = _ChangeLocationSpecialWireUserControl.customer.ID;

                // when request save to create one requet for calculation of costs and in time forward create other requst

                if (RequestID == 0)
                {
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.RequestTypeID = RequestType.ID;
                    _Request.TelephoneNo = telephoneNo;
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(telephoneNo);
                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                    telephone.Detach();
                    DB.Save(telephone);


                }
                else
                {
                    DB.DeleteAll<ChangeLocationSpecialWirePoint>(Data.ChangeLocationSpecialWirePointsDB.GetChangeLocationSpecialWirePointsByRequestID(_Request.ID).Select(t => t.ID).ToList());
                    DB.Delete<CRM.Data.ChangeLocationSpecialWire>(_Request.ID);
                }
                List<ChangeLocationSpecialWirePoint> changeLocationSpecialWirePoints = new List<ChangeLocationSpecialWirePoint>();
                specialWirePoints.ForEach(item =>
                {
                    ChangeLocationSpecialWirePoint changeLocationSpecialWirePoint = new ChangeLocationSpecialWirePoint();
                    changeLocationSpecialWirePoint.BuchtID = (long)item.BuchtID;
                    changeLocationSpecialWirePoint.CenterID = item.CenterID;
                    changeLocationSpecialWirePoint.RequestID = _Request.ID;
                    changeLocationSpecialWirePoint.NewInstallAddressID = item.NewInstallAddressID;
                    changeLocationSpecialWirePoint.NearestTelephoneNo = item.NearestTelephoneNo;
                    changeLocationSpecialWirePoint.NewAddressContent = item.NewAddressContent;
                    changeLocationSpecialWirePoint.NewPostalCode = item.NewPostCode;
                    changeLocationSpecialWirePoints.Add(changeLocationSpecialWirePoint);
                });
                DB.SaveAll(changeLocationSpecialWirePoints);


                //چون در تبدیل اطلاعات کرمانشاه برای یک سیم خصوص چندین شماره داده شد 
                SpecialWirePoints firstItem = new SpecialWirePoints();
                if (specialWirePoints.Count > 1)
                {
                    firstItem = specialWirePoints.Where(t => t.CenterID == _Request.CenterID).Take(1).SingleOrDefault();
                    if (firstItem == null) throw new Exception("حداقل یک مرکز در لیست انتخابی باید در مرکز فعلی باشد");
                }
                else
                {
                    firstItem = specialWirePoints.Take(1).SingleOrDefault();
                }
                CRM.Data.ChangeLocationSpecialWire changeLocationspecialWire = new Data.ChangeLocationSpecialWire();
                changeLocationspecialWire.RequestID = _Request.ID;
                changeLocationspecialWire.OldBuchtID = (long)firstItem.BuchtID;
                changeLocationspecialWire.OldOtherBuchtID = firstItem.OtherBuchtID;
                changeLocationspecialWire.OldCabinetInputID = firstItem.CabinetInputID;
                changeLocationspecialWire.OldPostContactID = firstItem.PostContactID;
                changeLocationspecialWire.OldSwitchPortID = (int)firstItem.SwitchPortID;
                changeLocationspecialWire.OldInstallAddressID = firstItem.InstallAddressID;
                changeLocationspecialWire.InstallAddressID = firstItem.NewInstallAddressID;
                changeLocationspecialWire.NearestTelephone = firstItem.NearestTelephoneNo;
                changeLocationspecialWire.SpecialWireTypeID = firstItem.SpecialWireTypeID;
                changeLocationspecialWire.Detach();
                DB.Save(changeLocationspecialWire, true);

                _Request.Detach();
                DB.Save(_Request, false);

                ts.Complete();
            }
        }

        private bool OtherRequestCenterSavedInChangeLocationSpecialWire()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    List<SpecialWirePoints> specialWirePoints = Data.SpecialWirePointsDB.GetSpecialWirePointsByTelephone((long)_Request.TelephoneNo);
                    List<ChangeLocationSpecialWirePoint> changeLocationSpecialWirePoint = Data.ChangeLocationSpecialWirePointsDB.GetChangeLocationSpecialWirePointsByRequestID((long)_Request.ID);
                    if (specialWirePoints.Count > 0)
                    {
                        changeLocationSpecialWirePoint.ForEach(item =>
                        {

                            specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().IsSelect = true;
                            specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NearestTelephoneNo = item.NearestTelephoneNo;
                            specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewInstallAddressID = item.NewInstallAddressID;
                            specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewAddressContent = item.NewAddressContent;
                            specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewPostContact = item.NewPostalCode;
                        }
                        );
                    }

                    List<ChangeLocationSpecialWirePoint> changeLocationSpecialWirePoints = Data.ChangeLocationSpecialWirePointsDB.GetChangeLocationSpecialWirePointsByRequestID(_Request.ID);
                    CRM.Data.ChangeLocationSpecialWire changeLocationspecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_Request.ID);

                    specialWirePoints = specialWirePoints.Where(t => t.BuchtID != changeLocationspecialWire.OldBuchtID).ToList();
                    specialWirePoints = specialWirePoints.Where(t => changeLocationSpecialWirePoints.Select(t2 => t2.BuchtID).Contains((long)t.BuchtID)).ToList();
                    int startStatus = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;

                    CRM.Data.ChangeLocationSpecialWire changeLocationspecialWirePoint = new Data.ChangeLocationSpecialWire();
                    specialWirePoints.ForEach(item =>
                    {

                        Request requestPoint = new Request();
                        requestPoint.ID = DB.GenerateRequestID();

                        //if (item.CenterID == _Request.CenterID)
                        requestPoint.StatusID = WorkFlowDB.GetNextStatesID(DB.Action.Confirm, startStatus);
                        //else
                        //  requestPoint.StatusID = startStatus;

                        requestPoint.CenterID = item.CenterID;
                        requestPoint.InsertDate = _Request.InsertDate;
                        requestPoint.RequestDate = _Request.RequestDate;
                        requestPoint.MainRequestID = _Request.ID;
                        requestPoint.CustomerID = _Request.CustomerID;
                        requestPoint.IsCancelation = false;
                        requestPoint.IsVisible = true;
                        requestPoint.IsViewed = false;
                        requestPoint.IsWaitingList = false;
                        requestPoint.RequestTypeID = (int)DB.RequestType.ChangeLocationSpecialWire;
                        requestPoint.TelephoneNo = _Request.TelephoneNo;
                        requestPoint.Detach();
                        DB.Save(requestPoint, true);
                        changeLocationspecialWirePoint.RequestID = requestPoint.ID;

                        changeLocationspecialWirePoint.OldBuchtID = (long)item.BuchtID;
                        changeLocationspecialWirePoint.OldOtherBuchtID = item.OtherBuchtID;
                        changeLocationspecialWirePoint.OldCabinetInputID = item.CabinetInputID;
                        changeLocationspecialWirePoint.OldPostContactID = item.PostContactID;
                        changeLocationspecialWirePoint.OldSwitchPortID = (int)item.SwitchPortID;
                        changeLocationspecialWirePoint.OldInstallAddressID = item.InstallAddressID;

                        changeLocationspecialWirePoint.InstallAddressID = item.NewInstallAddressID;
                        changeLocationspecialWirePoint.NearestTelephone = item.NearestTelephoneNo;

                        changeLocationspecialWirePoint.SpecialWireTypeID = item.SpecialWireTypeID;

                        changeLocationspecialWirePoint.Detach();
                        DB.Save(changeLocationspecialWirePoint, true);

                        SpecialCondition _specialCondition = null;

                        if (item.SpecialWireTypeID == (int)DB.SpecialWireType.Middle)
                        {

                            if (_specialCondition == null)
                            {
                                _specialCondition = new SpecialCondition();
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = true;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, true);
                            }
                            else
                            {
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = true;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, false);
                            }
                        }
                        else
                        {

                            if (_specialCondition == null)
                            {
                                _specialCondition = new SpecialCondition();
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = false;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, true);
                            }
                            else
                            {
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = false;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, false);
                            }
                        }

                    }
                  );
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع درخواست", ex);
                return false;
            }

        }

        private bool OtherRequestCenterSavedInVacateSpecialWire()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    List<SpecialWirePoints> specialWirePoints = Data.SpecialWirePointsDB.GetSpecialWirePointsByTelephone((long)_Request.TelephoneNo);

                    List<VacateSpecialWirePoint> vacateSpecialWirePoints = Data.VacateSpecialWirePointsDB.GetVacateSpecialWirePointsByRequestID(_Request.ID);
                    CRM.Data.VacateSpecialWire vacateSpecialWire = Data.VacateSpecialWireDB.GetVacateSpecialWireByRequestID(_Request.ID);

                    specialWirePoints = specialWirePoints.Where(t => t.BuchtID != vacateSpecialWire.BuchtID).ToList();
                    specialWirePoints = specialWirePoints.Where(t => vacateSpecialWirePoints.Select(t2 => t2.BuchtID).Contains((long)t.BuchtID)).ToList();

                    int startStatus = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;


                    CRM.Data.VacateSpecialWire vacatespecialWirePoint = new Data.VacateSpecialWire();
                    specialWirePoints.ForEach(item =>
                    {
                        Request requestPoint = new Request();
                        requestPoint.ID = DB.GenerateRequestID();

                        //if (item.CenterID == _Request.CenterID)
                        requestPoint.StatusID = WorkFlowDB.GetNextStatesID(DB.Action.Confirm, startStatus);
                        // else
                        //   requestPoint.StatusID = startStatus;

                        requestPoint.CenterID = item.CenterID;
                        requestPoint.InsertDate = _Request.InsertDate;
                        requestPoint.RequestDate = _Request.RequestDate;
                        requestPoint.MainRequestID = _Request.ID;
                        requestPoint.CustomerID = _Request.CustomerID;
                        requestPoint.IsCancelation = false;
                        requestPoint.IsVisible = true;
                        requestPoint.IsViewed = false;
                        requestPoint.IsWaitingList = false;
                        requestPoint.RequestTypeID = (int)DB.RequestType.VacateSpecialWire;
                        requestPoint.TelephoneNo = _Request.TelephoneNo;
                        requestPoint.Detach();
                        DB.Save(requestPoint, true);

                        vacatespecialWirePoint.RequestID = requestPoint.ID;
                        vacatespecialWirePoint.BuchtID = (long)item.BuchtID;
                        vacatespecialWirePoint.CabinetInputID = item.CabinetInputID;
                        vacatespecialWirePoint.OtherBuchtID = item.OtherBuchtID;
                        vacatespecialWirePoint.PostContactID = item.PostContactID;
                        vacatespecialWirePoint.SwitchPortID = (int)item.SwitchPortID;
                        vacatespecialWirePoint.OldInstallAddressID = item.InstallAddressID;
                        vacatespecialWirePoint.OldCorrespondenceAddressID = item.CorrespondenceAddressID;
                        vacatespecialWirePoint.OldCustomerID = (long)_Request.CustomerID;
                        vacatespecialWirePoint.SpecialTypeID = item.SpecialWireTypeID;

                        vacatespecialWirePoint.Detach();
                        DB.Save(vacatespecialWirePoint, true);

                        SpecialCondition _specialCondition = null;

                        if (item.SpecialWireTypeID == (int)DB.SpecialWireType.Middle)
                        {

                            if (_specialCondition == null)
                            {
                                _specialCondition = new SpecialCondition();
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = true;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, true);
                            }
                            else
                            {
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = true;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, false);
                            }
                        }
                        else
                        {

                            if (_specialCondition == null)
                            {
                                _specialCondition = new SpecialCondition();
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = false;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, true);
                            }
                            else
                            {
                                _specialCondition.RequestID = requestPoint.ID;
                                _specialCondition.MiddlePointSpecialWire = false;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, false);
                            }
                        }



                    }
                   );
                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع درخواست", ex);
                return false;
            }
        }

        private void Save_E1FiberRequest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Customer = _E1FiberUseControl.Customer;

                _Request.RequestTypeID = RequestType.ID;
                // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Customer.ID;



                CRM.Data.E1 e1 = _E1FiberUseControl.DataContext as CRM.Data.E1;
                e1.E1Type = (byte)DB.E1Type.Fiber;
                if (RequestID == 0)
                {
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.ID = DB.GenerateRequestID();
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);
                    e1.RequestID = _Request.ID;
                    e1.Detach();
                    DB.Save(e1, true);
                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);
                    e1.Detach();
                    DB.Save(e1, false);
                }

                ts.Complete();
            }
        }

        private void Save_E1Request()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Customer = _E1.Customer;

                CRM.Data.E1 e1 = _E1.DataContext as CRM.Data.E1;

                if (Customer == null)
                    throw new Exception("لطفا مشترک را وارد کنید");

                if (e1.InstallAddressID == null || e1.CorrespondenceAddressID == null)
                    throw new Exception("لطفا آدرس نصب و مکاتبه ای را وارد کنید");




                _Request.RequestTypeID = RequestType.ID;
                //_Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Customer.ID;

                if (RequestType.ID == (int)DB.RequestType.E1Link)
                {
                    _Request.TelephoneNo = _E1._request.TelephoneNo;
                    e1.TelephoneNo = _E1._request.TelephoneNo;
                    int num = 0;
                    if (int.TryParse(_E1.LinkNumberTextBox.Text.Trim(), out num))
                    {
                        if (num > 0)
                            e1.NumberOfLine = num;
                        else
                            throw new Exception("لطفا تعداد لینک جدید را وارد کنید");
                    }
                    else
                    {
                        throw new Exception("لطفا تعداد لینک جدید را وارد کنید");
                    }
                }
                else
                {
                    if (e1.NumberOfLine <= 0)
                        throw new Exception("لطفا تعداد لینک را وارد کنید");
                }

                e1.E1Type = (byte)_E1.LineTypeComboBox.SelectedValue;

                if (RequestID == 0)
                {
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.ID = DB.GenerateRequestID();
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);
                    e1.RequestID = _Request.ID;
                    e1.Detach();
                    DB.Save(e1, true);



                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);
                    e1.Detach();
                    DB.Save(e1, false);
                }

                SpecialCondition _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_Request.ID);

                #region fibre
                if ((byte)_E1.LineTypeComboBox.SelectedValue == (byte)DB.E1Type.Fiber)
                {
                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsOpticalE1 = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsOpticalE1 = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }
                else
                {

                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsOpticalE1 = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsOpticalE1 = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }
                #endregion fiber

                #region ptp
                CheckableItem item = _E1.LinkTypeComboBox.SelectedItem as CheckableItem;
                if (item.Name.Contains("PTP"))
                {
                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsE1PTP = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsE1PTP = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }
                else
                {

                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsE1PTP = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _Request.ID;
                        _specialCondition.IsE1PTP = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }
                #endregion ptp


                ts.Complete();
            }
        }

        private void Save_VacateE1Request()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(Telephone.TelephoneNo);

                _Request.RequestTypeID = RequestType.ID;


                List<VacateE1Info> vacateE1Infos = _VacateE1UserControl.PointsInfoDataGrid.ItemsSource as List<VacateE1Info>;
                vacateE1Infos = vacateE1Infos.Where(t => t.IsSelected == true).ToList();


                if (RequestID == 0)
                {
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.ID = DB.GenerateRequestID();
                    _Request.CenterID = (int)CenterComboBox.SelectedValue;
                    _Request.CustomerID = telephone.CustomerID;
                    _Request.TelephoneNo = telephone.TelephoneNo;
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);

                    CRM.Data.E1 e1 = new Data.E1();
                    e1.RequestID = _Request.ID;
                    e1.E1Type = (byte)DB.E1Type.VacateWire;
                    e1.Detach();
                    DB.Save(e1, true);

                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                    telephone.Detach();
                    DB.Save(telephone);

                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);

                    _e1.Detach();
                    DB.Save(_e1, false);

                    DB.DeleteAll<E1Link>(_VacateE1UserControl.oldVacateE1.Select(t => t.ID).ToList());
                }


                List<E1Link> VacateE1s = new List<E1Link>();
                vacateE1Infos.ForEach(t =>
                {
                    E1Link vacateE1 = new E1Link();
                    vacateE1.ReqeustID = _Request.ID;
                    vacateE1.BuchtID = t.E1Link.BuchtID;
                    vacateE1.OtherBuchtID = t.E1Link.OtherBuchtID;
                    vacateE1.AcessE1NumberID = t.E1Link.AcessE1NumberID;
                    vacateE1.AcessBuchtID = t.E1Link.AcessBuchtID;
                    vacateE1.SwitchE1NumberID = t.E1Link.SwitchE1NumberID;
                    vacateE1.SwitchInterfaceE1NumberID = t.E1Link.SwitchInterfaceE1NumberID;
                    vacateE1.PostContactID = t.E1Link.PostContactID;
                    vacateE1.LinkNumber = t.E1Link.LinkNumber;
                    VacateE1s.Add(vacateE1);
                });

                DB.SaveAll(VacateE1s);


                ts.Complete();
            }
        }

        private void Save_ChangeAddressRequest()
        {
            Data.ChangeAddress changeAddress = new CRM.Data.ChangeAddress();

            changeAddress.OldInstallAddressID = Telephone.InstallAddressID;
            changeAddress.OldCorrespondenceAddressID = Telephone.CorrespondenceAddressID;
            changeAddress.NewInstallAddressID = _ChangeAddressUserControl.InstallAddress.ID;
            changeAddress.NewCorrespondenceAddressID = _ChangeAddressUserControl.CorrespondenceAddress.ID;

            _Request.TelephoneNo = Telephone.TelephoneNo;
            _Request.RequestTypeID = RequestType.ID;
            // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.IsVisible = true;

            Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
            _Request.StatusID = status.ID;

            if (RequestID == 0)
                RequestForChangeAddress.SaveChangeAddressRequest(_Request, changeAddress, null, null, true);
            else
                RequestForChangeAddress.SaveChangeAddressRequest(_Request, changeAddress, null, null, false);
        }

        private void Save_RefundDepositRequest()
        {

            using (TransactionScope ts = new TransactionScope())
            {
                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                //_Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
                _Request.CreatorUserID = DB.CurrentUser.ID;
                _Request.RequestTypeID = RequestType.ID;
                _Request.CustomerID = Customer.ID;

                _RefundDeposit = _RefundDepositUserControl.DataContext as Data.RefundDeposit;

                AssignmentInfo assingnmentInfo = DB.GetAllInformationByTelephoneNo(Telephone.TelephoneNo);
                if (assingnmentInfo == null) throw new Exception("اطلاعات فنی تلفن یافت نشد");

                if (_Request.ID == 0)
                {
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                    _Request.IsVisible = true;
                    _Request.Detach();
                    DB.Save(_Request, true);


                    _RefundDeposit.BuchtID = assingnmentInfo.BuchtID;
                    _RefundDeposit.PostContactID = assingnmentInfo.PostContactID;
                    _RefundDeposit.CabinetInputID = assingnmentInfo.InputNumberID;
                    _RefundDeposit.SwitchPortID = assingnmentInfo.SwitchPortID;
                    _RefundDeposit.InstallAddressID = Telephone.InstallAddressID;
                    _RefundDeposit.CorrespondenceAddressID = Telephone.CorrespondenceAddressID;
                    _RefundDeposit.TelephoneNo = Telephone.TelephoneNo;

                    RequestID = _RefundDeposit.ID = _Request.ID;
                    _RefundDeposit.Detach();
                    DB.Save(_RefundDeposit, true);
                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);

                    _RefundDeposit.Detach();
                    DB.Save(_RefundDeposit, false);

                }


                SpecialConditionFactory specialConditionFactory = new SpecialConditionFactory(_Request.ID, DB.SpecialConditions.IsGSM, (Telephone.UsageType == (int)DB.TelephoneUsageType.GSM ? true : false));
                SpecialCondition _specialCondition = specialConditionFactory.SpecialCondition;

                if (assingnmentInfo != null)
                {
                    specialConditionFactory = new SpecialConditionFactory(_Request.ID, DB.SpecialConditions.IsOpticalCabinet, (assingnmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet ? true : false));
                    _specialCondition.IsOpticalCabinet = specialConditionFactory.SpecialCondition.IsOpticalCabinet;
                }
                _specialCondition.Detach();
                DB.Save(_specialCondition, specialConditionFactory.IsNew);

                //SpecialCondition _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_Request.ID);
                //if (assingnmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet)
                //{
                //    if (_specialCondition == null)
                //    {
                //        _specialCondition = new SpecialCondition();
                //        _specialCondition.RequestID = _Request.ID;
                //        _specialCondition.IsOpticalCabinet = true;
                //        _specialCondition.Detach();
                //        DB.Save(_specialCondition, true);
                //    }
                //    else
                //    {
                //        _specialCondition.RequestID = _Request.ID;
                //        _specialCondition.IsOpticalCabinet = true;
                //        _specialCondition.Detach();
                //        DB.Save(_specialCondition, false);
                //    }
                //}
                ts.Complete();
            }
        }

        private void Save_ChangeNoRequest()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _Request.CenterID = (int)CenterComboBox.SelectedValue;
                    _Request.CreatorUserID = DB.CurrentUser.ID;
                    _Request.RequestTypeID = RequestType.ID;
                    //_Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
                    _Request.CustomerID = Customer.ID;
                    _Request.TelephoneNo = Telephone.TelephoneNo;

                    if (_Request.ID == 0)
                    {
                        _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                        _Request.ID = DB.GenerateRequestID();
                        _Request.IsVisible = true;
                        _Request.Detach();
                        DB.Save(_Request, true);

                        AssignmentInfo assingnmentInfo = DB.GetAllInformationByTelephoneNo(Telephone.TelephoneNo);
                        if (assingnmentInfo == null)
                            throw new Exception("اطلاعات این تلفن یافت نشد");

                        _ChangeNo = _changeNo.DataContext as Data.ChangeNo;
                        _ChangeNo.ID = _Request.ID;

                        _ChangeNo.OldSwitchPortID = assingnmentInfo.SwitchPortID;
                        _ChangeNo.OldBuchtID = assingnmentInfo.BuchtID;
                        _ChangeNo.OldCabinetInputID = assingnmentInfo.InputNumberID;
                        _ChangeNo.OldPostContactID = assingnmentInfo.PostContactID;

                        _ChangeNo.OldTelephoneNo = Telephone.TelephoneNo;
                        _ChangeNo.CustomerID = (long)Telephone.CustomerID;
                        _ChangeNo.InstallAddressID = (long)Telephone.InstallAddressID;
                        _ChangeNo.CorrespondenceAddressID = (long)Telephone.CorrespondenceAddressID;

                        _ChangeNo.Detach();
                        DB.Save(_ChangeNo, true);

                        SpecialCondition _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_Request.ID);
                        if (assingnmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet)
                        {
                            if (_specialCondition == null)
                            {
                                _specialCondition = new SpecialCondition();
                                _specialCondition.RequestID = _Request.ID;
                                _specialCondition.IsOpticalCabinet = true;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, true);
                            }
                            else
                            {
                                _specialCondition.RequestID = _Request.ID;
                                _specialCondition.IsOpticalCabinet = true;
                                _specialCondition.Detach();
                                DB.Save(_specialCondition, false);
                            }
                        }

                    }
                    else
                    {
                        _Request.Detach();
                        DB.Save(_Request, false);

                        _ChangeNo = _changeNo.DataContext as Data.ChangeNo;
                        _ChangeNo.ID = _Request.ID;

                        _ChangeNo.Detach();
                        DB.Save(_ChangeNo, false);

                    }



                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
        }

        private void Save_InstallRequest()
        {
            DateTime dateTime = DB.GetServerDate();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                Customer = _installdetail.Customer;

                if (Customer == null) throw new Exception("لطفا اطلاعات مشترک را وارد کنید");

                if (_installdetail.CorrespondenceAddress == null || _installdetail.InstallAddress.ID == 0) throw new Exception("لطفا اطلاعات آدرس مشترک را وارد کنید");

                if (_installdetail.InstallAddress == null || _installdetail.CorrespondenceAddress.ID == 0) throw new Exception("لطفا اطلاعات آدرس مکاتبه ای مشترک را وارد کنید");

                if (ReqDate.SelectedDate.HasValue && Customer != null)
                {

                    _installdetail.InstallReq.SaleFicheID = FicheDB.GetCurrentFiche(dateTime).ID;
                    _installdetail.InstallReq.Status = 1;
                    _installdetail.InstallReq.InstallRequestTypeID = RequestType.ID;
                    _installdetail.InstallReq.InstallAddressID = _installdetail.InstallAddress.ID;
                    _installdetail.InstallReq.CorrespondenceAddressID = _installdetail.CorrespondenceAddress.ID;
                    if (RequestType.ID == (int)DB.RequestType.Reinstall && _installdetail.PassTelephoneTextBox.Text.Trim() == string.Empty)
                    {
                        throw new Exception("لطفا شماره قبلی را وارد کنید");
                    }
                    if (_installdetail.RegisterAt118CheckBox.IsChecked == false)
                    {
                        _installdetail.NameTitleAt118TextBox.Text = string.Empty;
                        _installdetail.LastNameAt118TextBox.Text = string.Empty;
                        _installdetail.TitleAt118TextBox.Text = string.Empty;

                        // remove payment of reqister at 118
                        removePaymentByRequestType(DB.RequestType.TitleIn118, _Request.ID);
                        //
                    };

                    if (Convert.ToInt32(_installdetail.ZeroBlockComboBox.SelectedValue) == (int)DB.ClassTelephone.LimitLess)
                    {
                        // remove payment of reqister at OpenAndCloseZero
                        removePaymentByRequestType(DB.RequestType.OpenAndCloseZero, _Request.ID);
                        //
                    }

                    //for (int i = 0; i < loop; i++)
                    //    _installdetail.installReq.DepositeNo = DB.GetDepositeNumber((int)CentercomboBox.SelectedValue, (ReqDate.Text).Substring(0, 4));

                    _Request.RequestTypeID = RequestType.ID;
                    //_Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
                    _Request.CenterID = (int)CenterComboBox.SelectedValue;
                    _Request.CustomerID = Customer.ID;
                    _Request.IsVisible = true;

                    //   if (_Request.TelephoneNo == 0 || _Request.TelephoneNo == null)
                    //     _Request.TelephoneNo = _installdetail.PassTelephoneTextBox.Text.Trim() != string.Empty ? Convert.ToInt64(_installdetail.PassTelephoneTextBox.Text.Trim()) : 0;

                    if (_relatedRequestID != 0)
                        _Request.RelatedRequestID = _relatedRequestID;
                    else
                        _Request.RelatedRequestID = null;



                }
                if (RequestID == 0)
                {
                    if ((_installdetail.InstallReq.MethodOfPaymentForTelephoneConnection.HasValue == false))
                    {
                        throw new Exception("تعیین نحوه پرداخت هزینه اتصال تلفن الزامی میباشد");
                    }
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    RequestForInstallDB.SaveRequest(_Request, _installdetail.InstallReq, true);
                }
                else
                {
                    RequestForInstallDB.SaveRequest(_Request, _installdetail.InstallReq, false);
                }


                //SpecialCondition _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_Request.ID);
                //if (_installdetail.InstallReq.IsGSM)
                //{
                //    if (_specialCondition == null)
                //    {
                //        _specialCondition = new SpecialCondition();
                //        _specialCondition.RequestID = _Request.ID;
                //        _specialCondition.IsGSM = true;
                //        _specialCondition.Detach();
                //        DB.Save(_specialCondition, true);
                //    }
                //    else
                //    {
                //        _specialCondition.RequestID = _Request.ID;
                //        _specialCondition.IsGSM = true;
                //        _specialCondition.Detach();
                //        DB.Save(_specialCondition, false);
                //    }
                //}
                //else
                //{
                //    if (_specialCondition == null)
                //    {
                //        _specialCondition = new SpecialCondition();
                //        _specialCondition.RequestID = _Request.ID;
                //        _specialCondition.IsGSM = false;
                //        _specialCondition.Detach();
                //        DB.Save(_specialCondition, true);
                //    }
                //    else
                //    {
                //        _specialCondition.RequestID = _Request.ID;
                //        _specialCondition.IsGSM = false;
                //        _specialCondition.Detach();
                //        DB.Save(_specialCondition, false);
                //    }
                //}

                SpecialConditionFactory specialConditionFactory = new SpecialConditionFactory(_Request.ID, DB.SpecialConditions.IsGSM, _installdetail.InstallReq.IsGSM);
                SpecialCondition _specialCondition = specialConditionFactory.SpecialCondition;
                _specialCondition.Detach();
                DB.Save(_specialCondition, specialConditionFactory.IsNew);


                ts.Complete();
            }
        }

        private void removePaymentByRequestType(DB.RequestType requestType, long requestID)
        {
            List<RequestPayment> requestPayments = Data.RequestPaymentDB.GetRequestPaymentByRequestTypeID(requestType, requestID);
            requestPayments.ForEach(requestPayment =>
            {
                List<InstallmentRequestPayment> installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                if (installmentRequestPayments.Count != 0)
                {
                    DB.DeleteAll<InstallmentRequestPayment>(installmentRequestPayments.Select(t => t.ID).ToList());

                    if (requestPayment.RelatedRequestPaymentID != null)
                    {
                        RequestPayment PrePayment = Data.RequestPaymentDB.GetRequestPaymentByID((long)requestPayment.RelatedRequestPaymentID);
                        if (PrePayment != null && PrePayment.IsPaid == false)
                            DB.Delete<RequestPayment>(PrePayment.ID);
                        else
                            if (PrePayment != null && PrePayment.IsPaid == true)
                                Folder.MessageBox.ShowError("پیش پرداخت هزینه پرداخت شده است امکان حذف اقساط نمی باشد");

                    }
                }

                if (requestPayment.IsPaid == false)
                    DB.Delete<RequestPayment>(requestPayment.ID);
                else
                    if (requestPayment.IsPaid == true)
                        Folder.MessageBox.ShowError("هزینه پرداخت شده است امکان حذف نمی باشد");
            }

         );
        }

        private void Save_ChangeLocationCenterInsideRequest()
        {

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            _Request.RequestTypeID = RequestType.ID;
            // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.TelephoneNo = Telephone.TelephoneNo;
            _Request.IsVisible = true;

            _changeLocation.NewInstallAddressID = _ChangeLocation.InstallAddress.ID;
            _changeLocation.NewCorrespondenceAddressID = _ChangeLocation.CorrespondenceAddress.ID;

            if (_ChangeLocation.WithChangeNameCheckBox.IsChecked == false)
            {
                _changeLocation.NewCustomerID = null;
            }
            else
            {
                _changeLocation.NewCustomerID = _ChangeLocation.NewCustomerID;
            }

            if (RequestID == 0)
            {
                _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID; // Get start status

                AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(Telephone.TelephoneNo);

                if (assignmentInfo == null) throw new Exception("اطلاعات فنی تلفن یافت نشد");

                // save old information 

                _changeLocation.OldInstallAddressID = Telephone.InstallAddressID;
                _changeLocation.OldCorrespondenceAddressID = Telephone.CorrespondenceAddressID;

                _changeLocation.OldBuchtID = assignmentInfo.BuchtID;
                _changeLocation.OldCabinetInputID = assignmentInfo.InputNumberID;

                _changeLocation.OldTelephone = Telephone.TelephoneNo;
                _changeLocation.OldSwitchPortID = Telephone.SwitchPortID;

                _changeLocation.OldPostContactID = assignmentInfo.PostContactID;
                // save type of change location
                _changeLocation.ChangeLocationTypeID = (byte?)DB.ChangeLocationCenterType.InSideCenter;
            }

            _changeLocation.Equipment = (byte?)_ChangeLocation.EquipmentComboBox.SelectedValue;

            if (_ChangeLocation.NearestTelephonTextBox.Text != string.Empty)
                _changeLocation.NearestTelephon = Convert.ToInt64(_ChangeLocation.NearestTelephonTextBox.Text);


            Telephone.Status = (byte)DB.TelephoneStatus.ChangingLocation;

            if (RequestID == 0)
                RequestForChangeLocationDB.SaveRequest(_Request, _changeLocation, null, pastTelephone, true);
            else
                RequestForChangeLocationDB.SaveRequest(_Request, _changeLocation, null, pastTelephone, false);

            RequestID = _Request.ID;
            Documents.IsEnabled = true;
        }

        private void Save_ChangeLocationCenterToCenterRequest()
        {

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            _Request.RequestTypeID = RequestType.ID;
            //   _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;

            _changeLocation.NewInstallAddressID = _ChangeLocation.InstallAddress.ID;
            _changeLocation.NewCorrespondenceAddressID = _ChangeLocation.CorrespondenceAddress.ID;

            if (RequestID == 0)
            {
                _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(Telephone.TelephoneNo);
                if (assignmentInfo == null) throw new Exception("اطلاعات فنی تلفن یافت نشد");

                _Request.RequestTypeID = RequestType.ID; // (int)RequestTypecomboBox.SelectedValue;
                _Request.TelephoneNo = Telephone.TelephoneNo;
                _Request.IsVisible = true;

                // save old information
                _changeLocation.OldInstallAddressID = Telephone.InstallAddressID;
                _changeLocation.OldCorrespondenceAddressID = Telephone.CorrespondenceAddressID;


                _changeLocation.OldBuchtID = assignmentInfo.BuchtID;
                _changeLocation.OldCabinetInputID = assignmentInfo.InputNumberID;

                _changeLocation.OldTelephone = Telephone.TelephoneNo;
                _changeLocation.OldSwitchPortID = Telephone.SwitchPortID;

                _changeLocation.OldPostContactID = assignmentInfo.PostContactID;

                // save type of change location
                _changeLocation.ChangeLocationTypeID = (byte?)DB.ChangeLocationCenterType.CenterToCenter;

            }

            _changeLocation.Equipment = (byte?)_ChangeLocation.EquipmentComboBox.SelectedValue;

            if (_ChangeLocation.NearestTelephonTextBox.Text != string.Empty)
                _changeLocation.NearestTelephon = Convert.ToInt64(_ChangeLocation.NearestTelephonTextBox.Text);

            if (_ChangeLocation.TargetComboBox.SelectedValue == null && CenterComboBox.SelectedValue == null) { throw new Exception("مرکز مبدا و مرکز مقصد نمی تواند خالی باشد"); }

            if ((int)_ChangeLocation.TargetComboBox.SelectedValue == (int)CenterComboBox.SelectedValue)
            {
                throw new Exception("مرکز مبدا و مقصد نمی توانند یک مرکز باشند. می توانید از تغییر مکان داخل مرکز استفاده کنید.");
            }

            _changeLocation.TargetCenter = (int)_ChangeLocation.TargetComboBox.SelectedValue;
            _changeLocation.SourceCenter = (int)CenterComboBox.SelectedValue;
            Telephone.Status = (byte)DB.TelephoneStatus.ChangingLocation;

            // if isforward is true 
            if (IsForward)
                _Request.CenterID = (int)_ChangeLocation.TargetComboBox.SelectedValue;



            if (RequestID == 0)
                RequestForChangeLocationDB.SaveRequest(_Request, _changeLocation, null, pastTelephone, true);
            else
                RequestForChangeLocationDB.SaveRequest(_Request, _changeLocation, null, pastTelephone, false);
        }

        private void Save_DischarginTelephoneRequest()
        {
            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.IsVisible = true;
            _takePossession = _DischargeTelephone.DataContext as TakePossession;

            // Get technical information
            AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(Telephone.TelephoneNo);


            if (RequestID == 0)
            {
                _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID; // Get Start Status

                _Request.RequestTypeID = RequestType.ID;
                _Request.TelephoneNo = Telephone.TelephoneNo;

                // save old technical information
                _takePossession.OldTelephone = Telephone.TelephoneNo;
                _takePossession.CustomerID = Telephone.CustomerID;
                _takePossession.SwitchPortID = Telephone.SwitchPortID;
                _takePossession.InstallAddressID = Telephone.InstallAddressID;
                _takePossession.CorrespondenceAddressID = Telephone.CorrespondenceAddressID;

                if (assignmentInfo != null)
                {
                    _takePossession.BuchtID = assignmentInfo.BuchtID;
                    _takePossession.CabinetInputID = assignmentInfo.InputNumberID;
                    _takePossession.PostContactID = assignmentInfo.PostContactID;
                }

                Telephone.Status = (byte)DB.TelephoneStatus.Reserv;
            }
            //بررسی میکند که ایا تلفن در مرکز انتخاب شده باشد
            if (_Request.CenterID != Telephone.CenterID)
                throw new Exception("تلفن متعلق به مرکز انتخاب شده نمیباشد");


            // اگر تلفن دایر نباشد امکان تغیر مکان ندارد
            //if ((_DischargeTelephone.TelephoneDataGrid.SelectedItem as TelephoneDB.TeleInfo).TelephoneNo != pastTelephone && (_DischargeTelephone.TelephoneDataGrid.SelectedItem as TelephoneDB.TeleInfo).TelephoneNoStatus != (byte)DB.TelephoneStatus.Connecting)
            //    throw new Exception("لطفا یک تلفن دایر را انتخاب کنید");

            if (RequestID == 0)
                RequestForChangeLocationDB.SaveDischargeRequest(_Request, _takePossession, Telephone, pastTelephone, true);
            else
                RequestForChangeLocationDB.SaveDischargeRequest(_Request, _takePossession, Telephone, pastTelephone, false);


            SpecialConditionFactory specialConditionFactory = new SpecialConditionFactory(_Request.ID, DB.SpecialConditions.IsGSM, (Telephone.UsageType == (int)DB.TelephoneUsageType.GSM ? true : false));
            SpecialCondition _specialCondition = specialConditionFactory.SpecialCondition;

            if (assignmentInfo != null)
            {
                specialConditionFactory = new SpecialConditionFactory(_Request.ID, DB.SpecialConditions.IsOpticalCabinet, (assignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet ? true : false));
                _specialCondition.IsOpticalCabinet = specialConditionFactory.SpecialCondition.IsOpticalCabinet;
            }
            _specialCondition.Detach();
            DB.Save(_specialCondition, specialConditionFactory.IsNew);


            //SpecialCondition _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_Request.ID);
            //if (assignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet)
            //{
            //    if (_specialCondition == null)
            //    {
            //        _specialCondition = new SpecialCondition();
            //        _specialCondition.RequestID = _Request.ID;
            //        _specialCondition.IsOpticalCabinet = true;
            //        _specialCondition.Detach();
            //        DB.Save(_specialCondition, true);
            //    }
            //    else
            //    {
            //        _specialCondition.RequestID = _Request.ID;
            //        _specialCondition.IsOpticalCabinet = true;
            //        _specialCondition.Detach();
            //        DB.Save(_specialCondition, false);
            //    }
            //}
        }

        private void Save_ChangeNameRequest()
        {
            CRM.Data.ChangeName changeName = new CRM.Data.ChangeName();

            changeName.OldCustomerID = Customer.ID;
            changeName.NewCustomerID = _ChangeName.NewCustomer.ID;
            changeName.HasCourtLetter = (bool)_ChangeName.CourtCheckBox.IsChecked;
            changeName.LastCyleID = (int)_ChangeName.LastCycleNoComnboBox.SelectedValue;
            changeName.LastBillDate = _ChangeName.LastBillDate.SelectedDate;

            if (_ChangeName.CourtCheckBox.IsChecked == true)
            {
                changeName.CourtName = _ChangeName.CourtNameTextBox.Text;
                changeName.CourtVerdictNo = _ChangeName.CourtVerdictNoTextBox.Text;
                changeName.CourtVerdictDate = _ChangeName.CourtVerdictDate.SelectedDate;
            }
            else
            {
                changeName.CourtName = string.Empty;
                changeName.CourtVerdictNo = string.Empty;
                changeName.CourtVerdictDate = null;
            }

            _Request.TelephoneNo = _ChangeName.TelephoneNo;

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            _Request.RequestTypeID = RequestType.ID;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.IsVisible = true;

            if (RequestID == 0)
                _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;

            if (RequestID == 0)
                RequestForChangeNameDB.SaveChangeNameRequest(_Request, changeName, null, null, true);
            else
                RequestForChangeNameDB.SaveChangeNameRequest(_Request, changeName, null, null, false);
        }

        private void Save_CutAndEstablish()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Data.CutAndEstablish cutAndEstablish = new CRM.Data.CutAndEstablish();

                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.Connect:
                        cutAndEstablish.ActionEstablishDueDate = _CutAndEstablish.ActionEstablishDueDate.SelectedDate;
                        cutAndEstablish.EstablishListingDate = _CutAndEstablish.EstablishListingDate.SelectedDate;
                        cutAndEstablish.EstablishComment = _CutAndEstablish.EstablishCommentTextBox.Text;
                        _Request.TelephoneNo = _CutAndEstablish.TelephoneNo;
                        cutAndEstablish.Status = (int)DB.RequestType.Connect;
                        break;

                    case (int)DB.RequestType.CutAndEstablish:
                        cutAndEstablish.CutType = Convert.ToByte(_CutAndEstablish.CutTypeComboBox.SelectedValue);
                        cutAndEstablish.ActionCutDueDate = _CutAndEstablish.ActionCutDueDate.SelectedDate;
                        cutAndEstablish.ActionEstablishDueDate = _CutAndEstablish.ActionEstablishDueDate1.SelectedDate;
                        cutAndEstablish.CutListingDate = _CutAndEstablish.CutListingDate.SelectedDate;
                        cutAndEstablish.EstablishWithOrder = _CutAndEstablish.EstablishWithOrderCheckBox.IsChecked;
                        cutAndEstablish.CutComment = _CutAndEstablish.CutCommentTextBox.Text;
                        _Request.TelephoneNo = _CutAndEstablish.TelephoneNo;
                        cutAndEstablish.Status = (int)DB.RequestType.CutAndEstablish;
                        break;

                    default:
                        break;
                }

                // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Customer.ID;
                _Request.IsVisible = true;

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;

                _Request.RequestTypeID = RequestType.ID;

                if (RequestID == 0)
                    RequestForCutAndEstablishDB.SaveRequest(_Request, cutAndEstablish, null, null, true);
                else
                    RequestForCutAndEstablishDB.SaveRequest(_Request, cutAndEstablish, null, null, false);
                ts.Complete();
            }
        }

        private void Save_SpecialServiceRequest()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;

                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Customer.ID;
                _Request.RequestTypeID = RequestType.ID;
                _Request.TelephoneNo = Telephone.TelephoneNo;
                _Request.IsVisible = true;

                List<CheckableItem> specialServiceTypeListAll = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckableForTelephone(Telephone.TelephoneNo).Where(t => t.IsChecked == true).ToList();

                List<CheckableItem> ListSpecialService = (_SpecialServiceUserControl.SpecialServiceListView.ItemsSource as List<CheckableItem>).Where(t => t.IsChecked == true).ToList(); ;

                List<CheckableItem> InstallSpecialServices = ListSpecialService.Where(t => !specialServiceTypeListAll.Select(st => st.ID).Contains(t.ID)).ToList();
                List<CheckableItem> UnistallSpecialServices = specialServiceTypeListAll.Where(t => !ListSpecialService.Select(st => st.ID).Contains(t.ID)).ToList();


                Data.Schema.SequenceIDs InstallSpecialServicesSequenceIDs = new SequenceIDs();
                InstallSpecialServicesSequenceIDs.Ids = InstallSpecialServices.Select(t => t.ID).ToList();

                Data.Schema.SequenceIDs UnistallSpecialServicesSequenceIDs = new SequenceIDs();
                UnistallSpecialServicesSequenceIDs.Ids = UnistallSpecialServices.Select(t => t.ID).ToList();

                _specialService.InstallSpecialService = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.SequenceIDs>(InstallSpecialServicesSequenceIDs, true));
                _specialService.UninstallSpecialService = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.SequenceIDs>(UnistallSpecialServicesSequenceIDs, true));

                if (RequestID == 0)
                {
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.Detach();
                    DB.Save(_Request, true);

                    _specialService.ID = _Request.ID;
                    _specialService.IsApplied = false;
                    _specialService.UninstallDate = (_SpecialServiceUserControl.SpecialServiceUninstallDatePicker.SelectedDate.HasValue) ? _SpecialServiceUserControl.SpecialServiceUninstallDatePicker.SelectedDate : default(DateTime?);
                    _specialService.LetterNo = _SpecialServiceUserControl.LetterNoTextBox.Text.Trim();
                    _specialService.SpecialServiceRequestReference = _SpecialServiceUserControl.SpecialServiceRequestReferenceTextBox.Text.Trim();
                    _specialService.Detach();
                    DB.Save(_specialService, true);


                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);

                    _specialService.Detach();
                    DB.Save(_specialService, false);

                    List<RequestPayment> requestPayments = Data.RequestPaymentDB.GetRequestPaymentsByBaseCostID((int)DB.SpecialCostID.SpecialServiceTypeCostID, _Request.ID);
                    foreach (RequestPayment requestPayment in requestPayments)
                    {
                        if (requestPayment != null)
                        {

                            List<InstallmentRequestPayment> installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                            if (installmentRequestPayments.Count != 0)
                            {
                                DB.DeleteAll<InstallmentRequestPayment>(installmentRequestPayments.Select(t => t.ID).ToList());

                                if (requestPayment.RelatedRequestPaymentID != null)
                                {
                                    RequestPayment PrePayment = Data.RequestPaymentDB.GetRequestPaymentByID((long)requestPayment.RelatedRequestPaymentID);
                                    if (PrePayment != null && PrePayment.IsPaid == false)
                                        DB.Delete<RequestPayment>(PrePayment.ID);
                                    else
                                        if (PrePayment != null && PrePayment.IsPaid == true)
                                            Folder.MessageBox.ShowError("پیش پرداخت هزینه پرداخت شده است امکان حذف اقساط نمی باشد");

                                }
                            }

                            if (requestPayment.IsPaid == false)
                                DB.Delete<RequestPayment>(requestPayment.ID);
                            else
                                if (requestPayment.IsPaid == true)
                                    Folder.MessageBox.ShowError("هزینه پرداخت شده است امکان حذف نمی باشد");
                        }
                    }
                }


                List<SpecialServiceType> specialServiceTypes = Data.SpecialServiceTypeDB.GetSpecialService(InstallSpecialServices.Select(t => t.ID).ToList());
                foreach (SpecialServiceType item in specialServiceTypes.Where(t => t.Cost != 0).ToList())
                {

                    BaseCost cost = Data.BaseCostDB.GetBaseCostByID((int)DB.SpecialCostID.SpecialServiceTypeCostID);
                    RequestPayment requestPaymentSpecialService = new RequestPayment();
                    requestPaymentSpecialService.InsertDate = DB.GetServerDate();
                    requestPaymentSpecialService.BaseCostID = cost.ID;
                    requestPaymentSpecialService.RequestID = _Request.ID;
                    requestPaymentSpecialService.PaymentType = (byte)item.PaymentType;
                    requestPaymentSpecialService.IsKickedBack = false;
                    requestPaymentSpecialService.IsPaid = false;
                    requestPaymentSpecialService.Cost = item.Cost;
                    requestPaymentSpecialService.Tax = cost.Tax;

                    if (cost.Tax != null)
                        requestPaymentSpecialService.AmountSum = item.Cost + Convert.ToInt64(cost.Tax * 0.01 * item.Cost);
                    else
                        requestPaymentSpecialService.AmountSum = item.Cost;

                    DB.Save(requestPaymentSpecialService);
                }
                ts.Complete();
            }
        }

        private void Save_OpenAndCloseZeroRequest()
        {
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.TelephoneNo = Telephone.TelephoneNo;
            _Request.IsVisible = true;

            _zeroStatus = _openAndCloseZeroUserControl.DataContext as ZeroStatus;

            if (Telephone.ClassTelephone == _zeroStatus.ClassTelephone)
                throw new Exception("کلاس تلفن انتخاب شده برابر با کلاس فعلی تلفن می باشد.");

            if (RequestID == 0)
            {
                _Request.ID = DB.GenerateRequestID();
                _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                _Request.Detach();
                DB.Save(_Request, true);

                _zeroStatus.ID = _Request.ID;
                _zeroStatus.OldClassTelephone = Telephone.ClassTelephone;
                _zeroStatus.Detach();
                DB.Save(_zeroStatus, true);
            }
            else
            {
                _Request.Detach();
                DB.Save(_Request, false);

                _zeroStatus.Detach();
                DB.Save(_zeroStatus, false);

                //Data.RequestPaymentDB.DeleteAllReqeustPayment((int)DB.SpecialCostID.OpenSecondZero, _Request.ID);

                //Data.RequestPaymentDB.DeleteAllReqeustPayment((int)DB.SpecialCostID.BlockSecondZero, _Request.ID);
            }
        }

        private void Save_ADSLRequest()
        {
            if (_ADSL._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_ADSL._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            CRM.Data.ADSLRequest ADSLRequest = new CRM.Data.ADSLRequest();

            if (RequestID != 0)
                ADSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);

            ADSLRequest.TelephoneNo = 0;
            ADSLRequest.CustomerOwnerID = _ADSL.ADSLCustomer.ID;
            ADSLRequest.CustomerOwnerElkaID = _ADSL.ADSLCustomer.ElkaID;

            Customer aDSLCustomer = CustomerDB.GetCustomerByID(_ADSL.ADSLCustomer.ID);
            if (!string.IsNullOrWhiteSpace(_ADSL.MobileNoTextBox.Text))
            {
                if (_ADSL.MobileNoTextBox.Text.Trim().Count() != 11 || !_ADSL.MobileNoTextBox.Text.Trim().StartsWith("09"))
                    throw new Exception("لطفا شماره تلفن همراه صحیح را وارد نمایید");

                aDSLCustomer.MobileNo = _ADSL.MobileNoTextBox.Text.Trim();
            }
            else
                aDSLCustomer.MobileNo = "";
            //    throw new Exception("لطفا شماره تلفن همراه مالک ADSL را وارد نمایید");

            aDSLCustomer.Detach();
            Save(aDSLCustomer);

            /////////////////
            if (_ADSL.ADSLCustomer.ID == 0)
            {
                _ADSL.ADSLCustomer = DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", _ADSL.NationalCodeTextBox.Text.Trim())[0];
            }
            /////////////////            

            //if (_TelephoneNo == 0)
            //{
            //    if (Telephone != null)
            //        ADSLRequest.TelephoneNo = Telephone.TelephoneNo;
            //    else
            //        ADSLRequest.TelephoneNo = (long)_Request.TelephoneNo;
            //}
            //else
            //    ADSLRequest.TelephoneNo = _TelephoneNo;

            ADSLRequest.CustomerOwnerStatus = (byte)_ADSL.ADSLOwnerStatusComboBox.SelectedValue;

            if (!string.IsNullOrWhiteSpace(_ADSL.CommentCustomersTextBox.Text))
                ADSLRequest.CommentCustomers = _ADSL.CommentCustomersTextBox.Text;

            Service1 webService = new Service1();
            //if (ADSLRequest.CustomerOwnerStatus == (byte)DB.ADSLOwnerStatus.Owner)
            //    webService.Update_MobileNumber_By_FI_CODE("Admin", "alibaba123", aDSLCustomer.ElkaID.ToString(), (aDSLCustomer.PersonType == 0) ? "1" : "2", aDSLCustomer.MobileNo.ToString());

            if (_ADSL.CustomerGroupComboBox.SelectedValue != null)
                ADSLRequest.CustomerGroupID = (int)_ADSL.CustomerGroupComboBox.SelectedValue;
            else
                throw new Exception("لطفا گروه مشتری را تعیین نمایید");

            if (_ADSL.CustomerTypeComboBox.SelectedValue != null)
                ADSLRequest.CustomerTypeID = (int)_ADSL.CustomerTypeComboBox.SelectedValue;
            else
                throw new Exception("لطفا نوع مشتری را تعیین نمایید");

            if (_ADSL.JobGroupComboBox.SelectedValue != null)
                ADSLRequest.JobGroupID = (int)_ADSL.JobGroupComboBox.SelectedValue;
            else
                ADSLRequest.JobGroupID = null;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
            {
                ADSLRequest.ADSLSellerAgentID = user.SellerAgentID;
                _Request.SellerID = user.SellerAgentID;
            }
            else
                ADSLRequest.ADSLSellerAgentID = null;
            ADSLRequest.ReagentTelephoneNo = _ADSL.ReagentTelephoneNoTextBox.Text;
            ADSLRequest.Status = false;

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;
            }
            _Request.RequestTypeID = RequestType.ID;
            //if (RequestPaymentTypeListBox.SelectedValue != null)
            //    _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            //else
            //    _Request.RequestPaymentTypeID = 1;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = _ADSL.ADSLCustomer.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            if (RequestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
            {
                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
                _IsSalable = false;
            }

            if (RequestID != 0 && _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                if (_ADSL.ServiceComboBox.SelectedValue != null)
                {
                    if (_ADSL.ServiceInfo.DataContext != null)
                        ADSLRequest.ServiceID = (int)_ADSL.ServiceComboBox.SelectedValue;
                    else
                        throw new Exception("سرویس مورد نظر به درستی انتخاب نشده است");
                }
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                ADSLRequest.LicenseLetterNo = _ADSL.LicenceLetterNoTextBox.Text;

                if (_ADSL.AdditionalServiceComboBox.SelectedValue != null)
                    ADSLRequest.AdditionalServiceID = (int)_ADSL.AdditionalServiceComboBox.SelectedValue;
                else
                    ADSLRequest.AdditionalServiceID = null;

                //if (_ADSL.CustomerPriorityComboBox.SelectedValue != null)
                //    ADSLRequest.CustomerPriority = (byte)_ADSL.CustomerPriorityComboBox.SelectedValue;
                //else
                ADSLRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;

                ADSLRequest.RequiredInstalation = _ADSL.RequiredInstalationCheckBox.IsChecked;

                ADSLRequest.HasIP = _ADSL.HasIPStaticCheckBox.IsChecked;
                if (_ADSL.HasIPStaticCheckBox.IsChecked != null)
                {
                    if ((bool)_ADSL.HasIPStaticCheckBox.IsChecked)
                        if (_ADSL.IPTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا نوع IP مورد نظر را تعیین نمایید");
                        else
                        {
                            if (_ADSL.IPStatic == null && _ADSL.GroupIPStatic == null)
                                throw new Exception("لطفا IP مورد نظر را انتخاب نمایید، یا گزینه انتخاب IP را بردارید.");

                            if ((byte)Convert.ToInt16(_ADSL.IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                            {
                                if (ADSLRequest.IPStaticID != null && ADSLRequest.IPStaticID != _ADSL.IPStatic.ID)
                                {
                                    ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)ADSLRequest.IPStaticID);
                                    oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldIp.TelephoneNo = null;

                                    oldIp.Detach();
                                    Save(oldIp);
                                }

                                if (ADSLRequest.GroupIPStaticID != null)
                                {
                                    ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)ADSLRequest.GroupIPStaticID);
                                    oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldGroupIp.TelephoneNo = null;

                                    oldGroupIp.Detach();
                                    Save(oldGroupIp);
                                }

                                ADSLRequest.IPStaticID = _ADSL.IPStatic.ID;
                                ADSLRequest.IPDuration = (int)_ADSL.IPTimeComboBox.SelectedValue;
                                ADSLRequest.GroupIPStaticID = null;

                                _ADSL.IPStatic.TelephoneNo = _Request.TelephoneNo;
                                _ADSL.IPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                            }

                            if ((byte)Convert.ToInt16(_ADSL.IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Group)
                            {
                                if (ADSLRequest.GroupIPStaticID != null && ADSLRequest.GroupIPStaticID != _ADSL.GroupIPStatic.ID)
                                {
                                    ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)ADSLRequest.GroupIPStaticID);
                                    oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldGroupIp.TelephoneNo = null;

                                    oldGroupIp.Detach();
                                    Save(oldGroupIp);
                                }

                                if (ADSLRequest.IPStaticID != null)
                                {
                                    ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)ADSLRequest.IPStaticID);
                                    oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldIp.TelephoneNo = null;

                                    oldIp.Detach();
                                    Save(oldIp);
                                }

                                ADSLRequest.GroupIPStaticID = _ADSL.GroupIPStatic.ID;
                                ADSLRequest.IPDuration = (int)_ADSL.IPTimeComboBox.SelectedValue;
                                ADSLRequest.IPStaticID = null;

                                _ADSL.GroupIPStatic.TelephoneNo = _Request.TelephoneNo;
                                _ADSL.GroupIPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                            }
                        }
                }

                ADSLRequest.NeedModem = _ADSL.NeedModemCheckBox.IsChecked;
                if (_ADSL.NeedModemCheckBox.IsChecked != null)
                {
                    if ((bool)_ADSL.NeedModemCheckBox.IsChecked)
                        if (_ADSL.ModemTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                        {
                            if (_ADSL.ModemSerilaNoComboBox.SelectedValue == null)
                                throw new Exception("لطفا شماره سریال مودم را انتخاب نمایید، یا گزینه درخواست مودم را بردارید. ");

                            ADSLRequest.ModemID = (int)_ADSL.ModemTypeComboBox.SelectedValue;
                            ADSLRequest.ModemSerialNoID = (int)_ADSL.ModemSerilaNoComboBox.SelectedValue;
                            ADSLRequest.ModemMACAddress = _ADSL.ModemMACAddressTextBox.Text;

                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLRequest.ModemSerialNoID);
                            modem.TelephoneNo = _Request.TelephoneNo;
                            modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                            modem.Detach();
                            Save(modem);
                        }
                    else
                    {
                        if (ADSLRequest != null && ADSLRequest.ModemSerialNoID != null)
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLRequest.ModemSerialNoID);
                            modem.TelephoneNo = null;
                            modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                            modem.Detach();
                            Save(modem);

                            ADSLRequest.ModemID = null;
                            ADSLRequest.ModemSerialNoID = null;
                            ADSLRequest.ModemMACAddress = "";
                        }
                    }
                }
            }

            if (_IsSalable)
            {
                RequestPayment requestPayment = new RequestPayment();

                BaseCost baseCostInstall = BaseCostDB.GetInstallCostForADSL();

                if (_Request.ID != 0)
                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCostInstall.ID);

                if (requestPayment == null)
                    requestPayment = new RequestPayment();

                requestPayment.BaseCostID = baseCostInstall.ID;
                requestPayment.RequestID = _Request.ID;
                requestPayment.Cost = baseCostInstall.Cost;
                requestPayment.Tax = baseCostInstall.Tax;
                if (baseCostInstall.Tax != null && baseCostInstall.Tax != 0)
                    requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((requestPayment.Tax * 0.01) * requestPayment.Cost));
                else
                    requestPayment.AmountSum = requestPayment.Cost;
                requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                requestPayment.IsKickedBack = false;
                requestPayment.IsAccepted = false;

                requestPayment.Detach();
                DB.Save(requestPayment);

                if (ADSLRequest.ModemID != null)
                {
                    ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSLRequest.ModemID);
                    BaseCost baseCost = BaseCostDB.GetModemCostForADSL();
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.ServiceID);
                    int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                    List<InstallmentRequestPayment> instalmentList = null;

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                    {
                        requestPayment = new RequestPayment();

                        if (service.IsModemInstallment != null)
                        {
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            if ((bool)service.IsModemInstallment)
                            {
                                instalmentList = GenerateInstalments(true, requestPayment.ID, duration, (byte)DB.RequestType.ADSL, (long)requestPayment.Cost, false);
                                requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                            }
                            else
                                requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        }
                        else
                            requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    }
                    //else
                    //{
                    //    if ((bool)requestPayment.IsPaid)
                    //        throw new Exception("هزینه مودم پرداخت شده است، امکان تغییر مودم وجود ندارد"); 
                    //}

                    //if (!string.IsNullOrEmpty(requestPayment.BillID) || !string.IsNullOrEmpty(requestPayment.PaymentID))
                    //    if (requestPayment.AmountSum != service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price))
                    //        throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                    else
                        requestPayment.Cost = modem.Price;

                    if (baseCost.Tax != null)
                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                    else
                        requestPayment.AmountSum = baseCost.Cost;

                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);

                    if (instalmentList != null)
                    {
                        foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                        {
                            currentInstalment.RequestPaymentID = requestPayment.ID;

                            currentInstalment.Detach();
                            DB.Save(currentInstalment);
                        }
                    }
                }
                else
                {
                    BaseCost baseCost = BaseCostDB.GetModemCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment != null)
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                        DB.Delete<RequestPayment>(requestPayment.ID);
                    }
                }

                if (ADSLRequest.RequiredInstalation != null)
                {
                    if ((bool)ADSLRequest.RequiredInstalation)
                    {
                        ADSLInstalCostCenter installCost = ADSLInstallCostCenterDB.GetADSLInstallCostByCenterID(_Request.CenterID);
                        BaseCost baseCost = BaseCostDB.GetInstalCostForADSL();

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;
                        requestPayment.Cost = installCost.InstallADSLCost;
                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((requestPayment.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = installCost.InstallADSLCost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }
                    else
                    {
                        BaseCost baseCost = BaseCostDB.GetInstalCostForADSL();

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment != null)
                        {
                            if (requestPayment.IsPaid != null)
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش نصب حضوری پرداخت شده است، امکان حذف آن وجود ندارد");

                            DB.Delete<RequestPayment>(requestPayment.ID);
                        }
                    }
                }

                if (ADSLRequest.ServiceID != null)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.ServiceID);
                    BaseCost baseCost = BaseCostDB.GetServiceCostForADSL();

                    List<InstallmentRequestPayment> instalmentList = null;

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                    {
                        requestPayment = new RequestPayment();
                        requestPayment.PaymentType = (byte)service.PaymentTypeID;

                        if (service.IsInstalment != null)
                            if ((bool)service.IsInstalment)
                            {
                                requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                requestPayment.UserID = _Request.CreatorUserID;
                            }
                    }
                    else
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                if (requestPayment.AmountSum != service.PriceSum)
                                    throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                        if (requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                            if (requestPayment.AmountSum != service.PriceSum)
                            {
                                List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                                if (instalments.Count > 0)
                                    throw new Exception("هزینه سرویس قبلی قسط بندی شده است، لطفا ابتدا اقساط آن را حذف نمایید");
                            }
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = service.Price;
                    requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)service.DurationID : 0;
                    requestPayment.Tax = service.Tax;
                    requestPayment.AmountSum = service.PriceSum;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);

                    if (service.IsInstalment != null)
                        if ((bool)service.IsInstalment && requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                        {
                            instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                            if (instalmentList.Count != 0)
                                DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                            instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSL, (long)requestPayment.Cost, false);
                            DB.SaveAll(instalmentList);
                        }
                }

                if (ADSLRequest.AdditionalServiceID != null)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.AdditionalServiceID);
                    BaseCost baseCost = BaseCostDB.GetAdditionalServiceCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();
                    else
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                if (requestPayment.AmountSum != service.PriceSum)
                                    throw new Exception("هزینه ترافیک اضافی پرداخت شده است، امکان تغییر آن وجود ندارد");
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = service.Price;
                    requestPayment.Tax = service.Tax;
                    requestPayment.AmountSum = service.PriceSum;
                    requestPayment.PaymentType = (byte)service.PaymentTypeID;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }
                else
                {
                    BaseCost baseCost = BaseCostDB.GetAdditionalServiceCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);
                    if (requestPayment != null)
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                throw new Exception("هزینه مربوط به ترافیک اضافی پرداخت شده است");

                        DB.Delete<RequestPayment>(requestPayment.ID);
                    }
                }

                if (ADSLRequest.HasIP == true)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.ServiceID);

                    if (ADSLRequest.IPStaticID != null)
                    {
                        BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                        // ----- For finding time of IP , to compute cost
                        int serviceDuration = (int)ADSLRequest.IPDuration;

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();
                        //else
                        //{
                        //    if ((bool)requestPayment.IsPaid)
                        //        throw new Exception("هزینه IP پرداخت شده است، امکان تغییر آن وجود ندارد");
                        //}

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;

                        if (service.IPDiscount != null && service.IPDiscount != 0)
                            requestPayment.Cost = Convert.ToInt64((baseCost.Cost * serviceDuration) - (baseCost.Cost * serviceDuration * (service.IPDiscount * 0.01)));
                        else
                            requestPayment.Cost = baseCost.Cost * serviceDuration;

                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null && baseCost.Tax != 0)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = requestPayment.Cost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }

                    if (ADSLRequest.GroupIPStaticID != null)
                    {
                        BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                        // ----- For finding time of IP , to compute cost
                        int serviceduration = (int)ADSLRequest.IPDuration;

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;
                        ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)ADSLRequest.GroupIPStaticID);

                        if (service.IPDiscount != null && service.IPDiscount != 0)
                            requestPayment.Cost = Convert.ToInt64((groupIP.BlockCount * baseCost.Cost * serviceduration) - (groupIP.BlockCount * baseCost.Cost * serviceduration * (service.IPDiscount * 0.01)));
                        else
                            requestPayment.Cost = groupIP.BlockCount * baseCost.Cost * serviceduration;

                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null && baseCost.Tax != 0)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = requestPayment.Cost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }
                }
            }

            if (RequestID == 0)
                RequestForADSL.SaveADSLRequest(_Request, ADSLRequest, _ADSL.IPStatic, _ADSL.GroupIPStatic, null, null, true);
            else
                RequestForADSL.SaveADSLRequest(_Request, ADSLRequest, _ADSL.IPStatic, _ADSL.GroupIPStatic, null, null, false);

            RequestID = _Request.ID;
        }

        private void Save_WirelessRequest()
        {
            if (_wirelessUserControl._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_wirelessUserControl._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            wirelessRequest = _wirelessUserControl._WirelessRequest;

            if (RequestID == 0)
                _Request.TelephoneNo = _wirelessUserControl._telephoneNo;

            string errorMessage = string.Empty;

            Customer customer = _wirelessUserControl._customer;

            if (_wirelessUserControl._customer == null || _wirelessUserControl._customer.ID == 0)
            {
                errorMessage += "لطفا مشترک را وارد نمایید\n";
            }
            if (_wirelessUserControl._installAddress == null || _wirelessUserControl._installAddress.ID == 0)
            {
                errorMessage += "لطفا آدرس را وارد نمایید\n";
            }

            if (!string.IsNullOrWhiteSpace(_wirelessUserControl.MobileNoTextBox.Text.Trim()))
            {
                if (!(_wirelessUserControl.MobileNoTextBox.Text.Trim().Count() == 11 || _wirelessUserControl.MobileNoTextBox.Text.Trim().StartsWith("091") || _wirelessUserControl.MobileNoTextBox.Text.Trim().StartsWith("91")))
                    errorMessage += "لطفا شماره تلفن همراه صحیح را وارد نمایید\n";
            }

            if (_wirelessUserControl.CustomerGroupComboBox.SelectedValue != null)
                wirelessRequest.CustomerGroupID = (int)_wirelessUserControl.CustomerGroupComboBox.SelectedValue;
            else
                errorMessage += "\nلطفا گروه مشتری را تعیین نمایید";

            if (_wirelessUserControl.CustomerTypeComboBox.SelectedValue != null)
                wirelessRequest.CustomerTypeID = (int)_wirelessUserControl.CustomerTypeComboBox.SelectedValue;
            else
                errorMessage += "\nلطفا نوع مشتری را تعیین نمایید";

            if (_wirelessUserControl._CenterID != 0 && !DB.currentUser.CenterIDs.Contains(_wirelessUserControl._CenterID))
                errorMessage += "مرکز تلفن وارد شده در مراکز دسترسی شما نیست\n";

            double Longitude = 0;
            if (!double.TryParse(_wirelessUserControl.LongitudeTextBox.Text.Trim(), out Longitude))
                errorMessage += "طول وارد شده صحیح نمی باشد\n";

            double Latitude = 0;
            if (!double.TryParse(_wirelessUserControl.LatitudeTextBox.Text.Trim(), out Latitude))
                errorMessage += "عرض وارد شده صحیح نمی باشد\n";

            if (_wirelessUserControl.WirelessTypeComboBox.SelectedValue == null)
                errorMessage += "نوع وایرلس را انتخاب کنید\n";

            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);

            wirelessRequest.CustomerOwnerID = _wirelessUserControl._customer.ID;
            wirelessRequest.AddressID = _wirelessUserControl._installAddress.ID;
            wirelessRequest.Longitude = Longitude;
            wirelessRequest.Latitude = Latitude;
            wirelessRequest.WirelessType = (byte)_wirelessUserControl.WirelessTypeComboBox.SelectedValue;

            if (!string.IsNullOrWhiteSpace(_wirelessUserControl.MobileNoTextBox.Text.Trim()))
            {
                customer.MobileNo = _wirelessUserControl.MobileNoTextBox.Text.Trim();
            }

            customer.Detach();
            DB.Save(customer);

            // wirelessRequest.CustomerOwnerElkaID = _wirelessUserControl.ADSLCustomer.ElkaID;

            //else
            //    aDSLCustomer.MobileNo = "";
            //    throw new Exception("لطفا شماره تلفن همراه مالک ADSL را وارد نمایید");

            //aDSLCustomer.Detach();
            //Save(aDSLCustomer);

            /////////////////
            //if (_wirelessUserControl.ADSLCustomer.ID == 0)
            //{
            //    _wirelessUserControl.ADSLCustomer = DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", _wirelessUserControl.NationalCodeTextBox.Text.Trim())[0];
            //}
            /////////////////            

            //if (_TelephoneNo == 0)
            //{
            //    if (Telephone != null)
            //        ADSLRequest.TelephoneNo = Telephone.TelephoneNo;
            //    else
            //        ADSLRequest.TelephoneNo = (long)_Request.TelephoneNo;
            //}
            //else
            //    ADSLRequest.TelephoneNo = _TelephoneNo;

            wirelessRequest.CustomerOwnerStatus = (byte)_wirelessUserControl.ADSLOwnerStatusComboBox.SelectedValue;

            //if (!string.IsNullOrWhiteSpace(_wirelessUserControl.CommentCustomersTextBox.Text))
            //    wirelessRequest.CommentCustomers = _wirelessUserControl.CommentCustomersTextBox.Text;

            // Service1 webService = new Service1();
            //if (ADSLRequest.CustomerOwnerStatus == (byte)DB.ADSLOwnerStatus.Owner)
            //    webService.Update_MobileNumber_By_FI_CODE("Admin", "alibaba123", aDSLCustomer.ElkaID.ToString(), (aDSLCustomer.PersonType == 0) ? "1" : "2", aDSLCustomer.MobileNo.ToString());

            if (_wirelessUserControl.JobGroupComboBox.SelectedValue != null)
                wirelessRequest.JobGroupID = (int)_wirelessUserControl.JobGroupComboBox.SelectedValue;
            else
                wirelessRequest.JobGroupID = null;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
            {
                wirelessRequest.ADSLSellerAgentID = user.SellerAgentID;
                _Request.SellerID = user.SellerAgentID;
            }
            else
                wirelessRequest.ADSLSellerAgentID = null;
            wirelessRequest.ReagentTelephoneNo = _wirelessUserControl.ReagentTelephoneNoTextBox.Text;
            wirelessRequest.Status = false;

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            _Request.RequestTypeID = RequestType.ID;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = _wirelessUserControl._customer.ID;
            _Request.IsVisible = true;

            if (RequestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
            {
                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;

                if (_wirelessUserControl._telephoneNo != 0)
                    _Request.TelephoneNo = _wirelessUserControl._telephoneNo;
                else
                    _Request.TelephoneNo = null;

                _IsSalable = false;
            }
            else
            {
                if (_Request.TelephoneNo == null)
                {
                    long telephoneNo = 0;
                    //if (!long.TryParse(_wirelessUserControl.TelephoneNoTextBox.Text.Trim(), out telephoneNo) && _wirelessUserControl.TelephoneNoTextBox.Text.Trim() != string.Empty)
                    //{
                    //    throw new Exception("تلفن صحیح وارد نشده است");
                    //}
                    System.Data.DataTable telephoneInfo = new System.Data.DataTable();
                    Service1 service = new Service1();
                    if (DB.City.ToString().ToLower() == "semnan")
                    {
                        telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());
                        if (telephoneInfo.Rows.Count != 0)
                        {
                            wirelessRequest.InstallmentsTelephoneNo = telephoneNo;
                        }
                    }
                    else
                    {
                        CRM.Data.Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
                        if (telephone != null && telephone.TelephoneNo != 0)
                        {
                            wirelessRequest.InstallmentsTelephoneNo = telephoneNo;
                        }
                    }
                }
            }

            if (RequestID != 0 && _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                if (_wirelessUserControl.ServiceComboBox.SelectedValue != null)
                {
                    if (_wirelessUserControl.ServiceInfo.DataContext != null)
                        wirelessRequest.ServiceID = (int)_wirelessUserControl.ServiceComboBox.SelectedValue;
                    else
                        throw new Exception("سرویس مورد نظر به درستی انتخاب نشده است");
                }
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                wirelessRequest.LicenseLetterNo = _wirelessUserControl.LicenceLetterNoTextBox.Text;

                if (_wirelessUserControl.AdditionalServiceComboBox.SelectedValue != null)
                    wirelessRequest.AdditionalServiceID = (int)_wirelessUserControl.AdditionalServiceComboBox.SelectedValue;
                else
                    wirelessRequest.AdditionalServiceID = null;

                //if (_wirelessUserControl.CustomerPriorityComboBox.SelectedValue != null)
                //    ADSLRequest.CustomerPriority = (byte)_wirelessUserControl.CustomerPriorityComboBox.SelectedValue;
                //else
                wirelessRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;

                wirelessRequest.RequiredInstalation = _wirelessUserControl.RequiredInstalationCheckBox.IsChecked;

                wirelessRequest.HasIP = _wirelessUserControl.HasIPStaticCheckBox.IsChecked;
                if (_wirelessUserControl.HasIPStaticCheckBox.IsChecked != null)
                {
                    if ((bool)_wirelessUserControl.HasIPStaticCheckBox.IsChecked)
                        if (_wirelessUserControl.IPTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا نوع IP مورد نظر را تعیین نمایید");
                        else
                        {
                            if (_wirelessUserControl.IPStatic == null && _wirelessUserControl.GroupIPStatic == null)
                                throw new Exception("لطفا IP مورد نظر را انتخاب نمایید، یا گزینه انتخاب IP را بردارید.");

                            if ((byte)Convert.ToInt16(_wirelessUserControl.IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                            {
                                if (wirelessRequest.IPStaticID != null && wirelessRequest.IPStaticID != _wirelessUserControl.IPStatic.ID)
                                {
                                    ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)wirelessRequest.IPStaticID);
                                    oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldIp.TelephoneNo = null;

                                    oldIp.Detach();
                                    Save(oldIp);
                                }

                                if (wirelessRequest.GroupIPStaticID != null)
                                {
                                    ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)wirelessRequest.GroupIPStaticID);
                                    oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldGroupIp.TelephoneNo = null;

                                    oldGroupIp.Detach();
                                    Save(oldGroupIp);
                                }

                                wirelessRequest.IPStaticID = _wirelessUserControl.IPStatic.ID;
                                wirelessRequest.IPDuration = (int)_wirelessUserControl.IPTimeComboBox.SelectedValue;
                                wirelessRequest.GroupIPStaticID = null;

                                _wirelessUserControl.IPStatic.TelephoneNo = _Request.TelephoneNo;
                                _wirelessUserControl.IPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                            }

                            if ((byte)Convert.ToInt16(_wirelessUserControl.IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Group)
                            {
                                if (wirelessRequest.GroupIPStaticID != null && wirelessRequest.GroupIPStaticID != _wirelessUserControl.GroupIPStatic.ID)
                                {
                                    ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)wirelessRequest.GroupIPStaticID);
                                    oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldGroupIp.TelephoneNo = null;

                                    oldGroupIp.Detach();
                                    Save(oldGroupIp);
                                }

                                if (wirelessRequest.IPStaticID != null)
                                {
                                    ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)wirelessRequest.IPStaticID);
                                    oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldIp.TelephoneNo = null;

                                    oldIp.Detach();
                                    Save(oldIp);
                                }

                                wirelessRequest.GroupIPStaticID = _wirelessUserControl.GroupIPStatic.ID;
                                wirelessRequest.IPDuration = (int)_wirelessUserControl.IPTimeComboBox.SelectedValue;
                                wirelessRequest.IPStaticID = null;

                                _wirelessUserControl.GroupIPStatic.TelephoneNo = _Request.TelephoneNo;
                                _wirelessUserControl.GroupIPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                            }
                        }
                }

                wirelessRequest.NeedModem = _wirelessUserControl.NeedModemCheckBox.IsChecked;
                if (_wirelessUserControl.NeedModemCheckBox.IsChecked != null)
                {
                    if ((bool)_wirelessUserControl.NeedModemCheckBox.IsChecked)
                        if (_wirelessUserControl.ModemTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                        {
                            if (_wirelessUserControl.ModemSerilaNoComboBox.SelectedValue == null)
                                throw new Exception("لطفا شماره سریال مودم را انتخاب نمایید، یا گزینه درخواست مودم را بردارید. ");

                            wirelessRequest.ModemID = (int)_wirelessUserControl.ModemTypeComboBox.SelectedValue;
                            wirelessRequest.ModemSerialNoID = (int)_wirelessUserControl.ModemSerilaNoComboBox.SelectedValue;
                            wirelessRequest.ModemMACAddress = _wirelessUserControl.ModemMACAddressTextBox.Text;

                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)wirelessRequest.ModemSerialNoID);
                            modem.TelephoneNo = _Request.TelephoneNo;
                            modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                            modem.Detach();
                            Save(modem);
                        }
                    else
                    {
                        if (wirelessRequest != null && wirelessRequest.ModemSerialNoID != null)
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)wirelessRequest.ModemSerialNoID);
                            modem.TelephoneNo = null;
                            modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                            modem.Detach();
                            Save(modem);

                            wirelessRequest.ModemID = null;
                            wirelessRequest.ModemSerialNoID = null;
                            wirelessRequest.ModemMACAddress = "";
                        }
                    }
                }
            }

            if (_IsSalable)
            {
                RequestPayment requestPayment = new RequestPayment();

                // هزینه رانژه برداشته شده است
                //BaseCost baseCostInstall = BaseCostDB.GetInstallCostForADSL();

                //if (_Request.ID != 0)
                //    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCostInstall.ID);

                //if (requestPayment == null)
                //    requestPayment = new RequestPayment();

                //requestPayment.BaseCostID = baseCostInstall.ID;
                //requestPayment.RequestID = _Request.ID;
                //requestPayment.Cost = baseCostInstall.Cost;
                //requestPayment.Tax = baseCostInstall.Tax;
                //if (baseCostInstall.Tax != null && baseCostInstall.Tax != 0)
                //    requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((requestPayment.Tax * 0.01) * requestPayment.Cost));
                //else
                //    requestPayment.AmountSum = requestPayment.Cost;
                //requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                //requestPayment.IsKickedBack = false;
                //requestPayment.IsAccepted = false;

                //requestPayment.Detach();
                //DB.Save(requestPayment);

                if (wirelessRequest.ModemID != null)
                {
                    ADSLModem modem = ADSLModemDB.GetADSLModemById((int)wirelessRequest.ModemID);
                    BaseCost baseCost = BaseCostDB.GetModemCostForADSL();
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)wirelessRequest.ServiceID);
                    int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                    List<InstallmentRequestPayment> instalmentList = null;

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                    {
                        requestPayment = new RequestPayment();

                        if (service.IsModemInstallment != null)
                        {
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            if ((bool)service.IsModemInstallment)
                            {
                                instalmentList = GenerateInstalments(true, requestPayment.ID, duration, (byte)DB.RequestType.ADSL, (long)requestPayment.Cost, false);
                                requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                            }
                            else
                                requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        }
                        else
                            requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    }
                    //else
                    //{
                    //    if ((bool)requestPayment.IsPaid)
                    //        throw new Exception("هزینه مودم پرداخت شده است، امکان تغییر مودم وجود ندارد"); 
                    //}

                    //if (!string.IsNullOrEmpty(requestPayment.BillID) || !string.IsNullOrEmpty(requestPayment.PaymentID))
                    //    if (requestPayment.AmountSum != service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price))
                    //        throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                    else
                        requestPayment.Cost = modem.Price;

                    if (baseCost.Tax != null)
                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                    else
                        requestPayment.AmountSum = baseCost.Cost;

                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);

                    if (instalmentList != null)
                    {
                        foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                        {
                            currentInstalment.RequestPaymentID = requestPayment.ID;

                            currentInstalment.Detach();
                            DB.Save(currentInstalment);
                        }
                    }
                }
                else
                {
                    BaseCost baseCost = BaseCostDB.GetModemCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment != null)
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                        DB.Delete<RequestPayment>(requestPayment.ID);
                    }
                }

                if (wirelessRequest.RequiredInstalation != null)
                {
                    if ((bool)wirelessRequest.RequiredInstalation)
                    {
                        ADSLInstalCostCenter installCost = ADSLInstallCostCenterDB.GetADSLInstallCostByCenterID(_Request.CenterID);
                        BaseCost baseCost = BaseCostDB.GetInstalCostForWireless();

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;
                        requestPayment.Cost = installCost.InstallADSLCost;
                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((requestPayment.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = installCost.InstallADSLCost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }
                    else
                    {
                        BaseCost baseCost = BaseCostDB.GetInstalCostForWireless();

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment != null)
                        {
                            if (requestPayment.IsPaid != null)
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش نصب حضوری پرداخت شده است، امکان حذف آن وجود ندارد");

                            DB.Delete<RequestPayment>(requestPayment.ID);
                        }
                    }
                }

                if (wirelessRequest.ServiceID != null)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)wirelessRequest.ServiceID);
                    BaseCost baseCost = BaseCostDB.GetServiceCostForADSL();

                    List<InstallmentRequestPayment> instalmentList = null;

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                    {
                        requestPayment = new RequestPayment();
                        requestPayment.PaymentType = (byte)service.PaymentTypeID;

                        if (service.IsInstalment != null)
                            if ((bool)service.IsInstalment)
                            {
                                requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                requestPayment.UserID = _Request.CreatorUserID;
                            }
                    }
                    else
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                if (requestPayment.AmountSum != service.PriceSum)
                                    throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                        if (requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                            if (requestPayment.AmountSum != service.PriceSum)
                            {
                                List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                                if (instalments.Count > 0)
                                    throw new Exception("هزینه سرویس قبلی قسط بندی شده است، لطفا ابتدا اقساط آن را حذف نمایید");
                            }
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = service.Price;
                    requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)service.DurationID : 0;
                    requestPayment.Tax = service.Tax;
                    requestPayment.AmountSum = service.PriceSum;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);

                    if (service.IsInstalment != null)
                        if ((bool)service.IsInstalment && requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                        {
                            instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                            if (instalmentList.Count != 0)
                                DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                            instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSL, (long)requestPayment.Cost, false);
                            DB.SaveAll(instalmentList);
                        }
                }

                if (wirelessRequest.AdditionalServiceID != null)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)wirelessRequest.AdditionalServiceID);
                    BaseCost baseCost = BaseCostDB.GetAdditionalServiceCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();
                    else
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                if (requestPayment.AmountSum != service.PriceSum)
                                    throw new Exception("هزینه ترافیک اضافی پرداخت شده است، امکان تغییر آن وجود ندارد");
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = service.Price;
                    requestPayment.Tax = service.Tax;
                    requestPayment.AmountSum = service.PriceSum;
                    requestPayment.PaymentType = (byte)service.PaymentTypeID;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }
                else
                {
                    BaseCost baseCost = BaseCostDB.GetAdditionalServiceCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);
                    if (requestPayment != null)
                    {
                        if (requestPayment.IsPaid != null)
                            if ((bool)requestPayment.IsPaid)
                                throw new Exception("هزینه مربوط به ترافیک اضافی پرداخت شده است");

                        DB.Delete<RequestPayment>(requestPayment.ID);
                    }
                }

                if (wirelessRequest.HasIP == true)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)wirelessRequest.ServiceID);

                    if (wirelessRequest.IPStaticID != null)
                    {
                        BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                        // ----- For finding time of IP , to compute cost
                        int serviceDuration = (int)wirelessRequest.IPDuration;

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();
                        //else
                        //{
                        //    if ((bool)requestPayment.IsPaid)
                        //        throw new Exception("هزینه IP پرداخت شده است، امکان تغییر آن وجود ندارد");
                        //}

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;

                        if (service.IPDiscount != null && service.IPDiscount != 0)
                            requestPayment.Cost = Convert.ToInt64((baseCost.Cost * serviceDuration) - (baseCost.Cost * serviceDuration * (service.IPDiscount * 0.01)));
                        else
                            requestPayment.Cost = baseCost.Cost * serviceDuration;

                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null && baseCost.Tax != 0)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = requestPayment.Cost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }

                    if (wirelessRequest.GroupIPStaticID != null)
                    {
                        BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                        // ----- For finding time of IP , to compute cost
                        int serviceduration = (int)wirelessRequest.IPDuration;

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;
                        ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)wirelessRequest.GroupIPStaticID);

                        if (service.IPDiscount != null && service.IPDiscount != 0)
                            requestPayment.Cost = Convert.ToInt64((groupIP.BlockCount * baseCost.Cost * serviceduration) - (groupIP.BlockCount * baseCost.Cost * serviceduration * (service.IPDiscount * 0.01)));
                        else
                            requestPayment.Cost = groupIP.BlockCount * baseCost.Cost * serviceduration;

                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null && baseCost.Tax != 0)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = requestPayment.Cost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }
                }
            }

            if (RequestID == 0)
                RequestForWirelessDB.SaveWirelessRequest(_Request, wirelessRequest, _wirelessUserControl.IPStatic, _wirelessUserControl.GroupIPStatic, null, null, true);
            else
                RequestForWirelessDB.SaveWirelessRequest(_Request, wirelessRequest, _wirelessUserControl.IPStatic, _wirelessUserControl.GroupIPStatic, null, null, false);

            RequestID = _Request.ID;
        }

        private void Save_ADSLChangeServiceRequest()
        {
            if (_ADSLChangeService._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_ADSLChangeService._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            Data.ADSLChangeService ADSLChangeService = new CRM.Data.ADSLChangeService();
            Data.ADSL ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_ADSLChangeService.TeleInfo.TelephoneNo);

            ADSLChangeService.OldServiceID = ADSL.TariffID;
            if (_ADSLChangeService.ServiceComboBox.SelectedValue != null)
                ADSLChangeService.NewServiceID = (int)_ADSLChangeService.ServiceComboBox.SelectedValue;
            else
                throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

            ADSLChangeService.LicenseLetterNo = _ADSLChangeService.NewLicenceLetterNoTextBox.Text;
            ADSLChangeService.CommentCustomers = "";
            ADSLChangeService.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Presence;
            ADSLChangeService.ChangeServiceActionType = (byte)Convert.ToInt32(_ADSLChangeService.ActionTypeComboBox.SelectedValue);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;
            }

            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            if (RequestID == 0)
            {
                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            _Request.CreatorUserID = DB.CurrentUser.ID;
            _Request.ModifyUserID = DB.CurrentUser.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;

            ADSLChangeService.NeedModem = _ADSLChangeService.NeedModemCheckBox.IsChecked;
            if (_ADSLChangeService.NeedModemCheckBox.IsChecked != null)
            {
                if ((bool)_ADSLChangeService.NeedModemCheckBox.IsChecked)
                {
                    if (_ADSLChangeService.ModemTypeComboBox.SelectedValue == null)
                        throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                    else
                    {
                        if (_ADSLChangeService.ModemSerilaNoComboBox.SelectedValue == null)
                            throw new Exception("لطفا شماره سریال مودم را انتخاب نمایید، یا گزینه درخواست مودم را بردارید. ");

                        ADSLChangeService.ModemID = (int)_ADSLChangeService.ModemTypeComboBox.SelectedValue;
                        ADSLChangeService.ModemSerialNoID = (int)_ADSLChangeService.ModemSerilaNoComboBox.SelectedValue;
                        ADSLChangeService.ModemMACAddress = _ADSLChangeService.ModemMACAddressTextBox.Text;

                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLChangeService.ModemSerialNoID);
                        modem.TelephoneNo = _Request.TelephoneNo;
                        modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                        modem.Detach();
                        Save(modem);
                    }
                }
                else
                {
                    if (ADSLChangeService.ModemSerialNoID != null)
                    {
                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLChangeService.ModemSerialNoID);
                        modem.TelephoneNo = null;
                        modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                        modem.Detach();
                        Save(modem);

                        ADSLChangeService.ModemID = null;
                        ADSLChangeService.ModemSerialNoID = null;
                        ADSLChangeService.ModemMACAddress = "";
                    }
                }
            }

            if (RequestID == 0)
                RequestForADSL.SaveADSLChangeServiceRequest(_Request, ADSLChangeService, null, true);
            else
                RequestForADSL.SaveADSLChangeServiceRequest(_Request, ADSLChangeService, null, false);

            RequestPayment requestPayment = new RequestPayment();

            if (ADSLChangeService.NewServiceID != null)
            {
                BaseCost baseCost = BaseCostDB.GetServiceCostForADSLChangeService();

                ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLChangeService.NewServiceID);

                ADSLService oldService = ADSLServiceDB.GetADSLServiceById((int)ADSL.TariffID);
                List<InstallmentRequestPayment> instalmentList = null;
                //int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));

                switch ((byte)Convert.ToInt16(_ADSLChangeService.ActionTypeComboBox.SelectedValue))
                {
                    case (byte)DB.ADSLChangeServiceActionType.ExtensionService:
                        if (service.PaymentTypeID != (byte)DB.PaymentType.NoPayment)
                        {
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();
                                requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                        requestPayment.UserID = _Request.CreatorUserID;
                                    }
                            }
                            else
                            {
                                if (requestPayment.IsPaid != null)
                                    if ((bool)requestPayment.IsPaid)
                                        if (requestPayment.AmountSum != service.PriceSum)
                                            throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                                if (requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                                    if (requestPayment.AmountSum != service.PriceSum)
                                    {
                                        List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                                        if (instalments.Count > 0)
                                            throw new Exception("هزینه سرویس قبلی قسط بندی شده است، لطفا ابتدا اقساط آن را حذف نمایید");
                                    }
                            }

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            requestPayment.Cost = service.Price;
                            requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)service.DurationID : 0;
                            requestPayment.Tax = service.Tax;
                            requestPayment.AmountSum = service.PriceSum;
                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (service.IsInstalment != null)
                                if ((bool)service.IsInstalment)
                                {
                                    instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                                    if (instalmentList.Count != 0)
                                        DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                    instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                    DB.SaveAll(instalmentList);
                                }
                        }

                        if (ADSLChangeService.ModemID != null)
                        {
                            ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSLChangeService.ModemID);
                            baseCost = BaseCostDB.GetModemCostForADSL();
                            service = ADSLServiceDB.GetADSLServiceById((int)ADSLChangeService.NewServiceID);
                            //duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();

                                if (service.IsModemInstallment != null)
                                {
                                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                                    else
                                        requestPayment.Cost = modem.Price;

                                    if (baseCost.Tax != null)
                                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                                    else
                                        requestPayment.AmountSum = baseCost.Cost;

                                    if ((bool)service.IsModemInstallment)
                                    {
                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                    }
                                    else
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                                }
                                else
                                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                            }
                            //else
                            //{
                            //    if ((bool)requestPayment.IsPaid)
                            //        throw new Exception("هزینه مودم پرداخت شده است، امکان تغییر مودم وجود ندارد"); 
                            //}

                            //if (!string.IsNullOrEmpty(requestPayment.BillID) || !string.IsNullOrEmpty(requestPayment.PaymentID))
                            //    if (requestPayment.AmountSum != service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price))
                            //        throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (instalmentList != null && instalmentList.Count != 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                                {
                                    currentInstalment.RequestPaymentID = requestPayment.ID;

                                    currentInstalment.Detach();
                                    DB.Save(currentInstalment);
                                }
                            }
                        }
                        else
                        {
                            baseCost = BaseCostDB.GetModemCostForADSL();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment != null)
                            {
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                                DB.Delete<RequestPayment>(requestPayment.ID);
                            }
                        }
                        break;

                    case (byte)DB.ADSLChangeServiceActionType.ChangeService:
                        if (service.PaymentTypeID != (byte)DB.PaymentType.NoPayment)
                        {
                            if (oldService.PaymentTypeID == (byte)DB.ADSLPaymentType.PrePaid)
                            {
                                //double dayCount = 0;
                                //double useDayCount = 0;
                                //DateTime? now = DB.GetServerDate();
                                long refundAmount = 0;
                                long refundCustomer = 0;

                                //dayCount = (int)oldService.DurationID * 30;
                                //if (ADSL.InstallDate != null)
                                //    useDayCount = now.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                                //else
                                //    useDayCount = 0;
                                refundAmount = _ADSLChangeService.refundAmount;
                                refundCustomer = _ADSLChangeService.refundCustomer;

                                //if (oldService.ModemDiscount != null && oldService.ModemDiscount != 0)
                                //{
                                //    if (ADSL.ModemID != null)
                                //    {
                                //        ADSLModemProperty serialID = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSL.ModemID);
                                //        ADSLModem modem = ADSLModemDB.GetADSLModemById((int)serialID.ADSLModemID);
                                //        double discountCost = (long)modem.Price * (long)oldService.ModemDiscount * 0.01;
                                //        refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                                //    }
                                //}

                                if (_Request.ID != 0)
                                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                                if (requestPayment == null)
                                {
                                    requestPayment = new RequestPayment();
                                    requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                    if (service.IsInstalment != null)
                                        if ((bool)service.IsInstalment)
                                        {
                                            requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                            requestPayment.UserID = _Request.CreatorUserID;
                                        }
                                }

                                requestPayment.BaseCostID = baseCost.ID;
                                requestPayment.RequestID = _Request.ID;
                                requestPayment.Cost = service.Price - refundAmount + refundCustomer;
                                requestPayment.Tax = service.Tax;
                                requestPayment.AmountSum = service.PriceSum - refundAmount + refundCustomer;
                                requestPayment.IsKickedBack = false;
                                requestPayment.IsAccepted = false;

                                requestPayment.Detach();
                                DB.Save(requestPayment);

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                                        if (instalmentList.Count != 0)
                                            DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        DB.SaveAll(instalmentList);
                                    }
                            }

                            if (oldService.PaymentTypeID == (byte)DB.ADSLPaymentType.PostPaid)
                            {
                                //double dayCount = 0;
                                //double useDayCount = 0;
                                //DateTime? now = DB.GetServerDate();
                                long refundAmount = 0;
                                long refundCustomer = 0;

                                //dayCount = (int)oldService.DurationID * 30;
                                //useDayCount = now.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                                refundAmount = _ADSLChangeService.refundAmount;
                                refundCustomer = _ADSLChangeService.refundCustomer;

                                //if (oldService.ModemDiscount != null && oldService.ModemDiscount != 0)
                                //{
                                //    if (ADSL.ModemID != null)
                                //    {
                                //        ADSLModemProperty serialID = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSL.ModemID);
                                //        ADSLModem modem = ADSLModemDB.GetADSLModemById((int)serialID.ADSLModemID);
                                //        double discountCost = (long)modem.Price * (long)oldService.ModemDiscount * 0.01;
                                //        refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                                //    }
                                //}

                                if (_Request.ID != 0)
                                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                                if (requestPayment == null)
                                {
                                    requestPayment = new RequestPayment();
                                    requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                    if (service.IsInstalment != null)
                                        if ((bool)service.IsInstalment)
                                        {
                                            requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                            requestPayment.UserID = _Request.CreatorUserID;
                                        }
                                }

                                requestPayment.BaseCostID = baseCost.ID;
                                requestPayment.RequestID = _Request.ID;
                                requestPayment.Cost = service.Price + refundAmount + refundCustomer;
                                requestPayment.Tax = service.Tax;
                                requestPayment.AmountSum = service.PriceSum + refundAmount + refundCustomer;
                                requestPayment.IsKickedBack = false;
                                requestPayment.IsAccepted = false;

                                requestPayment.Detach();
                                DB.Save(requestPayment);

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                                        if (instalmentList.Count != 0)
                                            DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                        instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentRemainByTelephoneNo((long)_Request.TelephoneNo);

                                        if (instalmentList.Count != 0)
                                            DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        DB.SaveAll(instalmentList);
                                    }

                            }
                        }
                        if (ADSLChangeService.ModemID != null)
                        {
                            ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSLChangeService.ModemID);
                            baseCost = BaseCostDB.GetModemCostForADSL();
                            service = ADSLServiceDB.GetADSLServiceById((int)ADSLChangeService.NewServiceID);
                            //duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();

                                if (service.IsModemInstallment != null)
                                {
                                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                                    else
                                        requestPayment.Cost = modem.Price;

                                    if (baseCost.Tax != null)
                                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                                    else
                                        requestPayment.AmountSum = baseCost.Cost;

                                    if ((bool)service.IsModemInstallment)
                                    {
                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                    }
                                    else
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                                }
                                else
                                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                            }
                            //else
                            //{
                            //    if ((bool)requestPayment.IsPaid)
                            //        throw new Exception("هزینه مودم پرداخت شده است، امکان تغییر مودم وجود ندارد"); 
                            //}

                            //if (!string.IsNullOrEmpty(requestPayment.BillID) || !string.IsNullOrEmpty(requestPayment.PaymentID))
                            //    if (requestPayment.AmountSum != service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price))
                            //        throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (instalmentList != null && instalmentList.Count != 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                                {
                                    currentInstalment.RequestPaymentID = requestPayment.ID;

                                    currentInstalment.Detach();
                                    DB.Save(currentInstalment);
                                }
                            }
                        }
                        else
                        {
                            baseCost = BaseCostDB.GetModemCostForADSL();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment != null)
                            {
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                                DB.Delete<RequestPayment>(requestPayment.ID);
                            }
                        }

                        break;

                    default:
                        break;
                }
            }

            RequestID = _Request.ID;
        }

        private void Save_WirelessChangeServiceRequest()
        {
            if (_WirelessChangeService._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_WirelessChangeService._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");


            Data.WirelessChangeService WirelessChangeService = new Data.WirelessChangeService();
            Data.ADSL ADSL = new Data.ADSL();
            if (!string.IsNullOrWhiteSpace(_WirelessCode))
            {
                ADSL = Data.ADSLDB.GetWirelessbyCode(_WirelessCode);
                WirelessChangeService.WirelessCode = _WirelessCode;
            }
            else
            {
                if (RequestID != 0)
                {
                    WirelessChangeService = WirelessChangeServiceDB.GetWirelessChangeServicebyID(_Request.ID);
                    ADSL = ADSLDB.GetWirelessbyCode(WirelessChangeService.WirelessCode);
                }
            }

            WirelessChangeService.OldServiceID = ADSL.TariffID;
            if (_WirelessChangeService.ServiceComboBox.SelectedValue != null)
                WirelessChangeService.NewServiceID = (int)_WirelessChangeService.ServiceComboBox.SelectedValue;
            else
                throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

            WirelessChangeService.LicenseLetterNo = _WirelessChangeService.NewLicenceLetterNoTextBox.Text;
            WirelessChangeService.CommentCustomers = "";
            WirelessChangeService.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Presence;
            WirelessChangeService.ChangeServiceActionType = (byte)Convert.ToInt32(_WirelessChangeService.ActionTypeComboBox.SelectedValue);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = ADSL.CustomerOwnerID;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            if (RequestID == 0)
            {
                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            _Request.CreatorUserID = DB.CurrentUser.ID;
            _Request.ModifyUserID = DB.CurrentUser.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;

            WirelessChangeService.NeedModem = _WirelessChangeService.NeedModemCheckBox.IsChecked;
            if (_WirelessChangeService.NeedModemCheckBox.IsChecked != null)
            {
                if ((bool)_WirelessChangeService.NeedModemCheckBox.IsChecked)
                {
                    if (_WirelessChangeService.ModemTypeComboBox.SelectedValue == null)
                        throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                    else
                    {
                        if (_WirelessChangeService.ModemSerilaNoComboBox.SelectedValue == null)
                            throw new Exception("لطفا شماره سریال مودم را انتخاب نمایید، یا گزینه درخواست مودم را بردارید. ");

                        WirelessChangeService.ModemID = (int)_WirelessChangeService.ModemTypeComboBox.SelectedValue;
                        WirelessChangeService.ModemSerialNoID = (int)_WirelessChangeService.ModemSerilaNoComboBox.SelectedValue;
                        WirelessChangeService.ModemMACAddress = _WirelessChangeService.ModemMACAddressTextBox.Text;

                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)WirelessChangeService.ModemSerialNoID);
                        modem.TelephoneNo = _Request.TelephoneNo;
                        modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                        modem.Detach();
                        Save(modem);
                    }
                }
                else
                {
                    if (WirelessChangeService.ModemSerialNoID != null)
                    {
                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)WirelessChangeService.ModemSerialNoID);
                        modem.TelephoneNo = null;
                        modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                        modem.Detach();
                        Save(modem);

                        WirelessChangeService.ModemID = null;
                        WirelessChangeService.ModemSerialNoID = null;
                        WirelessChangeService.ModemMACAddress = "";
                    }
                }
            }

            if (RequestID == 0)
                RequestForWirelessDB.SaveWirelessChangeServiceRequest(_Request, WirelessChangeService, true);
            else
                RequestForWirelessDB.SaveWirelessChangeServiceRequest(_Request, WirelessChangeService, false);

            RequestPayment requestPayment = new RequestPayment();

            if (WirelessChangeService.NewServiceID != null)
            {
                BaseCost baseCost = BaseCostDB.GetServiceCostForADSLChangeService();

                ADSLService service = ADSLServiceDB.GetADSLServiceById((int)WirelessChangeService.NewServiceID);

                ADSLService oldService = ADSLServiceDB.GetADSLServiceById((int)ADSL.TariffID);
                List<InstallmentRequestPayment> instalmentList = null;
                //int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));

                switch ((byte)Convert.ToInt16(_WirelessChangeService.ActionTypeComboBox.SelectedValue))
                {
                    case (byte)DB.ADSLChangeServiceActionType.ExtensionService:
                        if (service.PaymentTypeID != (byte)DB.PaymentType.NoPayment)
                        {
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();
                                requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                        requestPayment.UserID = _Request.CreatorUserID;
                                    }
                            }
                            else
                            {
                                if (requestPayment.IsPaid != null)
                                    if ((bool)requestPayment.IsPaid)
                                        if (requestPayment.AmountSum != service.PriceSum)
                                            throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                                if (requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                                    if (requestPayment.AmountSum != service.PriceSum)
                                    {
                                        List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                                        if (instalments.Count > 0)
                                            throw new Exception("هزینه سرویس قبلی قسط بندی شده است، لطفا ابتدا اقساط آن را حذف نمایید");
                                    }
                            }

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            requestPayment.Cost = service.Price;
                            requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)service.DurationID : 0;
                            requestPayment.Tax = service.Tax;
                            requestPayment.AmountSum = service.PriceSum;
                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (service.IsInstalment != null)
                                if ((bool)service.IsInstalment)
                                {
                                    instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                                    if (instalmentList.Count != 0)
                                        DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                    instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                    DB.SaveAll(instalmentList);
                                }
                        }

                        if (WirelessChangeService.ModemID != null)
                        {
                            ADSLModem modem = ADSLModemDB.GetADSLModemById((int)WirelessChangeService.ModemID);
                            baseCost = BaseCostDB.GetModemCostForADSL();
                            service = ADSLServiceDB.GetADSLServiceById((int)WirelessChangeService.NewServiceID);
                            //duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();

                                if (service.IsModemInstallment != null)
                                {
                                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                                    else
                                        requestPayment.Cost = modem.Price;

                                    if (baseCost.Tax != null)
                                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                                    else
                                        requestPayment.AmountSum = baseCost.Cost;

                                    if ((bool)service.IsModemInstallment)
                                    {
                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                    }
                                    else
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                                }
                                else
                                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                            }
                            //else
                            //{
                            //    if ((bool)requestPayment.IsPaid)
                            //        throw new Exception("هزینه مودم پرداخت شده است، امکان تغییر مودم وجود ندارد"); 
                            //}

                            //if (!string.IsNullOrEmpty(requestPayment.BillID) || !string.IsNullOrEmpty(requestPayment.PaymentID))
                            //    if (requestPayment.AmountSum != service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price))
                            //        throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (instalmentList != null && instalmentList.Count != 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                                {
                                    currentInstalment.RequestPaymentID = requestPayment.ID;

                                    currentInstalment.Detach();
                                    DB.Save(currentInstalment);
                                }
                            }
                        }
                        else
                        {
                            baseCost = BaseCostDB.GetModemCostForADSL();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment != null)
                            {
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                                DB.Delete<RequestPayment>(requestPayment.ID);
                            }
                        }
                        break;

                    case (byte)DB.ADSLChangeServiceActionType.ChangeService:
                        if (service.PaymentTypeID != (byte)DB.PaymentType.NoPayment)
                        {
                            if (oldService.PaymentTypeID == (byte)DB.ADSLPaymentType.PrePaid)
                            {
                                //double dayCount = 0;
                                //double useDayCount = 0;
                                //DateTime? now = DB.GetServerDate();
                                long refundAmount = 0;
                                long refundCustomer = 0;

                                //dayCount = (int)oldService.DurationID * 30;
                                //if (ADSL.InstallDate != null)
                                //    useDayCount = now.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                                //else
                                //    useDayCount = 0;
                                refundAmount = _WirelessChangeService.refundAmount;
                                refundCustomer = _WirelessChangeService.refundCustomer;

                                //if (oldService.ModemDiscount != null && oldService.ModemDiscount != 0)
                                //{
                                //    if (ADSL.ModemID != null)
                                //    {
                                //        ADSLModemProperty serialID = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSL.ModemID);
                                //        ADSLModem modem = ADSLModemDB.GetADSLModemById((int)serialID.ADSLModemID);
                                //        double discountCost = (long)modem.Price * (long)oldService.ModemDiscount * 0.01;
                                //        refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                                //    }
                                //}

                                if (_Request.ID != 0)
                                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                                if (requestPayment == null)
                                {
                                    requestPayment = new RequestPayment();
                                    requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                    if (service.IsInstalment != null)
                                        if ((bool)service.IsInstalment)
                                        {
                                            requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                            requestPayment.UserID = _Request.CreatorUserID;
                                        }
                                }

                                requestPayment.BaseCostID = baseCost.ID;
                                requestPayment.RequestID = _Request.ID;
                                requestPayment.Cost = service.Price - refundAmount + refundCustomer;
                                requestPayment.Tax = service.Tax;
                                requestPayment.AmountSum = service.PriceSum - refundAmount + refundCustomer;
                                requestPayment.IsKickedBack = false;
                                requestPayment.IsAccepted = false;

                                requestPayment.Detach();
                                DB.Save(requestPayment);

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                                        if (instalmentList.Count != 0)
                                            DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        DB.SaveAll(instalmentList);
                                    }
                            }

                            if (oldService.PaymentTypeID == (byte)DB.ADSLPaymentType.PostPaid)
                            {
                                //double dayCount = 0;
                                //double useDayCount = 0;
                                //DateTime? now = DB.GetServerDate();
                                long refundAmount = 0;
                                long refundCustomer = 0;

                                //dayCount = (int)oldService.DurationID * 30;
                                //useDayCount = now.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                                refundAmount = _WirelessChangeService.refundAmount;
                                refundCustomer = _WirelessChangeService.refundCustomer;

                                //if (oldService.ModemDiscount != null && oldService.ModemDiscount != 0)
                                //{
                                //    if (ADSL.ModemID != null)
                                //    {
                                //        ADSLModemProperty serialID = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSL.ModemID);
                                //        ADSLModem modem = ADSLModemDB.GetADSLModemById((int)serialID.ADSLModemID);
                                //        double discountCost = (long)modem.Price * (long)oldService.ModemDiscount * 0.01;
                                //        refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                                //    }
                                //}

                                if (_Request.ID != 0)
                                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                                if (requestPayment == null)
                                {
                                    requestPayment = new RequestPayment();
                                    requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                    if (service.IsInstalment != null)
                                        if ((bool)service.IsInstalment)
                                        {
                                            requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                            requestPayment.UserID = _Request.CreatorUserID;
                                        }
                                }

                                requestPayment.BaseCostID = baseCost.ID;
                                requestPayment.RequestID = _Request.ID;
                                requestPayment.Cost = service.Price + refundAmount + refundCustomer;
                                requestPayment.Tax = service.Tax;
                                requestPayment.AmountSum = service.PriceSum + refundAmount + refundCustomer;
                                requestPayment.IsKickedBack = false;
                                requestPayment.IsAccepted = false;

                                requestPayment.Detach();
                                DB.Save(requestPayment);

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);

                                        if (instalmentList.Count != 0)
                                            DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                        instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentRemainByTelephoneNo((long)_Request.TelephoneNo);

                                        if (instalmentList.Count != 0)
                                            DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        DB.SaveAll(instalmentList);
                                    }

                            }
                        }
                        if (WirelessChangeService.ModemID != null)
                        {
                            ADSLModem modem = ADSLModemDB.GetADSLModemById((int)WirelessChangeService.ModemID);
                            baseCost = BaseCostDB.GetModemCostForADSL();
                            service = ADSLServiceDB.GetADSLServiceById((int)WirelessChangeService.NewServiceID);
                            //duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();

                                if (service.IsModemInstallment != null)
                                {
                                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                                    else
                                        requestPayment.Cost = modem.Price;

                                    if (baseCost.Tax != null)
                                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                                    else
                                        requestPayment.AmountSum = baseCost.Cost;

                                    if ((bool)service.IsModemInstallment)
                                    {
                                        instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.Cost, false);
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                    }
                                    else
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                                }
                                else
                                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                            }
                            //else
                            //{
                            //    if ((bool)requestPayment.IsPaid)
                            //        throw new Exception("هزینه مودم پرداخت شده است، امکان تغییر مودم وجود ندارد"); 
                            //}

                            //if (!string.IsNullOrEmpty(requestPayment.BillID) || !string.IsNullOrEmpty(requestPayment.PaymentID))
                            //    if (requestPayment.AmountSum != service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price))
                            //        throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (instalmentList != null && instalmentList.Count != 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                                {
                                    currentInstalment.RequestPaymentID = requestPayment.ID;

                                    currentInstalment.Detach();
                                    DB.Save(currentInstalment);
                                }
                            }
                        }
                        else
                        {
                            baseCost = BaseCostDB.GetModemCostForADSL();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment != null)
                            {
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                                DB.Delete<RequestPayment>(requestPayment.ID);
                            }
                        }

                        break;

                    default:
                        break;
                }
            }

            RequestID = _Request.ID;
        }

        private void Save_ADSLChangeIPRequest()
        {
            ADSLChangeIPRequest aDSLChangeIP = new CRM.Data.ADSLChangeIPRequest();

            if (RequestID != 0)
                aDSLChangeIP = ADSLChangeIPRequestDB.GetADSLChangeIPRequestByID(RequestID);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            Data.ADSL aDSL = Data.ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);

            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            if (aDSL.HasIP == true)
            {
                aDSLChangeIP.ChangeIPType = (byte)DB.ADSLChangeIPType.ChangeIP;
            }
            else
            {
                aDSLChangeIP.ChangeIPType = (byte)DB.ADSLChangeIPType.AddIP;
            }

            if (_ADSLChangeIP.IPTimeComboBox.SelectedValue != null)
                aDSLChangeIP.IPTime = (int)_ADSLChangeIP.IPTimeComboBox.SelectedValue;
            else
                throw new Exception("لطفا زمان مورد نظر را تعیین نمایید");

            if (_ADSLChangeIP.IPTypeComboBox.SelectedValue == null)
                throw new Exception("لطفا نوع IP مورد نظر را تعیین نمایید");
            else
            {
                if ((byte)Convert.ToInt16(_ADSLChangeIP.IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                {
                    if (aDSL.IPStaticID != _ADSLChangeIP.IPStatic.ID)
                    {
                        if ((aDSLChangeIP.NewIPStaticID != null) && (aDSLChangeIP.NewIPStaticID != _ADSLChangeIP.IPStatic.ID))
                        {
                            ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)aDSLChangeIP.NewIPStaticID);
                            oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                            oldIp.TelephoneNo = null;

                            oldIp.Detach();
                            Save(oldIp);
                        }

                        if (aDSLChangeIP.NewGroupIPStaticID != null)
                        {
                            ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)aDSLChangeIP.NewGroupIPStaticID);
                            oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                            oldGroupIp.TelephoneNo = null;

                            oldGroupIp.Detach();
                            Save(oldGroupIp);
                        }

                        _ADSLChangeIP.IPStatic.TelephoneNo = _Request.TelephoneNo;
                        _ADSLChangeIP.IPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                    }

                    aDSLChangeIP.NewIPStaticID = _ADSLChangeIP.IPStatic.ID;
                    aDSLChangeIP.NewGroupIPStaticID = null;
                }

                if ((byte)Convert.ToInt16(_ADSLChangeIP.IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Group)
                {
                    if (aDSL.GroupIPStaticID != _ADSLChangeIP.GroupIPStatic.ID)
                    {
                        if ((aDSLChangeIP.NewGroupIPStaticID != null) && (aDSLChangeIP.NewGroupIPStaticID != _ADSLChangeIP.GroupIPStatic.ID))
                        {
                            ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)aDSLChangeIP.NewGroupIPStaticID);
                            oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                            oldGroupIp.TelephoneNo = null;

                            oldGroupIp.Detach();
                            Save(oldGroupIp);
                        }

                        if (aDSLChangeIP.NewIPStaticID != null)
                        {
                            ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)aDSLChangeIP.NewIPStaticID);
                            oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                            oldIp.TelephoneNo = null;

                            oldIp.Detach();
                            Save(oldIp);
                        }

                        _ADSLChangeIP.GroupIPStatic.TelephoneNo = _Request.TelephoneNo;
                        _ADSLChangeIP.GroupIPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                    }

                    aDSLChangeIP.NewGroupIPStaticID = _ADSLChangeIP.GroupIPStatic.ID;
                    aDSLChangeIP.NewIPStaticID = null;
                }
            }

            if (RequestID == 0)
                RequestForADSL.SaveADSLChangeIPRequest(_Request, aDSLChangeIP, _ADSLChangeIP.IPStatic, _ADSLChangeIP.GroupIPStatic, null, null, true);
            else
                RequestForADSL.SaveADSLChangeIPRequest(_Request, aDSLChangeIP, _ADSLChangeIP.IPStatic, _ADSLChangeIP.GroupIPStatic, null, null, false);

            RequestPayment requestPayment = new RequestPayment();

            if (aDSL.HasIP == true)
            {
                if (aDSLChangeIP.NewIPStaticID != null)
                {
                    BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                    int serviceduration = 0;

                    if (_ADSLChangeIP.IPTimeComboBox.SelectedValue != null)
                        serviceduration = (int)_ADSLChangeIP.IPTimeComboBox.SelectedValue;
                    else
                        throw new Exception("لطفا مدت زمان استفاده را تعیین نمایید");

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    //ADSLIP iP = ADSLIPDB.GetADSLIPById((long)aDSLChangeIP.NewIPStaticID);
                    requestPayment.Cost = baseCost.Cost * aDSLChangeIP.IPTime;
                    requestPayment.Tax = baseCost.Tax;
                    if (baseCost.Tax != null && baseCost.Tax != 0)
                        requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost)) - Convert.ToInt64(_ADSLChangeIP.refundAmount);
                    else
                        requestPayment.AmountSum = requestPayment.Cost - Convert.ToInt64(_ADSLChangeIP.refundAmount);
                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }
                if (aDSLChangeIP.NewGroupIPStaticID != null)
                {
                    BaseCost baseCost = BaseCostDB.GetIPCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)aDSLChangeIP.NewGroupIPStaticID);
                    requestPayment.Cost = groupIP.BlockCount * baseCost.Cost * aDSLChangeIP.IPTime;
                    requestPayment.Tax = baseCost.Tax;
                    if (baseCost.Tax != null && baseCost.Tax != 0)
                        requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost)) - Convert.ToInt64(_ADSLChangeIP.refundAmount);
                    else
                        requestPayment.AmountSum = requestPayment.Cost - Convert.ToInt64(_ADSLChangeIP.refundAmount);
                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }
            }
            else
            {
                if (aDSLChangeIP.NewIPStaticID != null)
                {
                    BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                    // ----- For finding time of IP , to compute cost                    
                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();
                    //else
                    //{
                    //    if ((bool)requestPayment.IsPaid)
                    //        throw new Exception("هزینه IP پرداخت شده است، امکان تغییر آن وجود ندارد");
                    //}

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = baseCost.Cost * aDSLChangeIP.IPTime;
                    requestPayment.Tax = baseCost.Tax;
                    if (baseCost.Tax != null && baseCost.Tax != 0)
                        requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                    else
                        requestPayment.AmountSum = requestPayment.Cost;
                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }

                if (aDSLChangeIP.NewGroupIPStaticID != null)
                {
                    BaseCost baseCost = BaseCostDB.GetIPCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)aDSLChangeIP.NewGroupIPStaticID);
                    requestPayment.Cost = groupIP.BlockCount * baseCost.Cost * aDSLChangeIP.IPTime;
                    requestPayment.Tax = baseCost.Tax;
                    if (baseCost.Tax != null && baseCost.Tax != 0)
                        requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                    else
                        requestPayment.AmountSum = requestPayment.Cost;
                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }
            }

            RequestID = _Request.ID;
        }

        private void Save_ADSLInstallRequest()
        {
            ADSLInstallRequest aDSLInstallRequest = new CRM.Data.ADSLInstallRequest();

            if (RequestID != 0)
                aDSLInstallRequest = ADSLInstallRequestDB.GetADSLInstallRequestByID(RequestID);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            Data.ADSL aDSL = Data.ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);

            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            if (RequestID == 0)
                RequestForADSL.SaveADSLInstallRequest(_Request, aDSLInstallRequest, true);
            else
                RequestForADSL.SaveADSLInstallRequest(_Request, aDSLInstallRequest, false);

            Service1 service = new Service1();
            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

            ADSLInstalCostCenter installCost = ADSLInstallCostCenterDB.GetADSLInstallCostByCenterID(CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())));
            BaseCost baseCost = BaseCostDB.GetInstalCostForADSL();
            RequestPayment requestPayment = new RequestPayment();

            if ((RequestID != 0))
                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);
            List<long?> Tel = Data.ADSLInstallCostCenterDB.GetTelNos();
            if (((RequestID == 0) && (Tel.Contains(_Request.TelephoneNo))))
                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByTelNoandCostID(_Request.TelephoneNo, baseCost.ID);

            if (requestPayment == null)
                requestPayment = new RequestPayment();

            requestPayment.BaseCostID = baseCost.ID;
            requestPayment.RequestID = _Request.ID;
            requestPayment.Cost = installCost.InstallADSLCost;
            requestPayment.Tax = baseCost.Tax;
            if (baseCost.Tax != null)
                requestPayment.AmountSum = installCost.InstallADSLCost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * installCost.InstallADSLCost);
            else
                requestPayment.AmountSum = installCost.InstallADSLCost;
            requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
            requestPayment.IsKickedBack = false;
            requestPayment.IsAccepted = false;

            requestPayment.Detach();
            DB.Save(requestPayment);

            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

            RequestID = _Request.ID;
        }

        private void Save_ADSLCutRequest()
        {
            Data.ADSLCutTemporary ADSLCutTemporary = new CRM.Data.ADSLCutTemporary();
            Data.ADSL ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_ADSLCutTemporary.TeleInfo.TelephoneNo);

            if (RequestID != 0)
                ADSLCutTemporary = ADSLCutTemporaryDB.GetADSLCutTemproryByRequestID(RequestID);

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();
            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", _ADSLCutTemporary.TeleInfo.TelephoneNo.ToString());
                userInfos = ibsngService.GetUserInfo(userAuthentication);
            }
            catch (Exception ex)
            {

            }
            foreach (DictionaryEntry User in userInfos)
            {
                userInfo = (XmlRpcStruct)User.Value;
            }

            try
            {
                _ADSLCutTemporary.CutTypeTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["lock"]);
                ADSLCutTemporary.Status = (byte?)DB.CutAndEstablishStatus.Establish;
                ADSLCutTemporary.CutType = null;
            }

            catch (Exception ex)
            {
                ADSLCutTemporary.Status = (byte?)DB.CutAndEstablishStatus.Cut;
                ADSLCutTemporary.CutType = (byte)_ADSLCutTemporary.CutTypeComboBox.SelectedValue;
            }

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }
            // _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            ADSLCutTemporary.Comment = _ADSLCutTemporary.CommentCustomersTextBox.Text;

            if (RequestID == 0)
                RequestForADSL.SaveADSLCutTemporaryRequest(_Request, ADSLCutTemporary, null, true);
            else
                RequestForADSL.SaveADSLCutTemporaryRequest(_Request, ADSLCutTemporary, null, false);

            RequestID = _Request.ID;
        }

        private void Save_ADSLDischargeRequest()
        {
            ADSLDischarge aDSLDischarge = new CRM.Data.ADSLDischarge();
            CRM.Data.ADSL aDSL = new CRM.Data.ADSL();
            if (RequestID != 0)
                aDSLDischarge = ADSLDischargeDB.GetADSLDischargeByID(RequestID);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            aDSL = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);

            aDSLDischarge.Comment = _ADSLDischarge.CommentTextBox.Text;
            if (_ADSLDischarge.ADSLDischargeReasonComboBox.SelectedValue != null)
                aDSLDischarge.ReasonID = (int)_ADSLDischarge.ADSLDischargeReasonComboBox.SelectedValue;
            else
                throw new Exception("لطفا دلیل تخلیه را تعیین نمایید");

            aDSLDischarge.TarrifID = aDSL.TariffID;

            if (RequestID == 0)
                RequestForADSL.SaveADSLDischargeRequest(_Request, aDSLDischarge, true);
            else
                RequestForADSL.SaveADSLDischargeRequest(_Request, aDSLDischarge, false);

            RequestID = _Request.ID;
        }

        private void Save_ADSLSellTraffic()
        {
            if (_ADSLSellTrafficUserControl._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_ADSLSellTrafficUserControl._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            ADSLSellTraffic aDSLSellTraffic = new ADSLSellTraffic();
            RequestPayment requesrPayment = new RequestPayment();

            if (RequestID != 0)
                aDSLSellTraffic = ADSLSellTrafficDB.GetADSLSellTrafficById(RequestID);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            _Request.CreatorUserID = DB.CurrentUser.ID;
            _Request.ModifyUserID = DB.CurrentUser.ID;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            aDSLSellTraffic.AdditionalServiceID = (int)_ADSLSellTrafficUserControl.AdditionalServiceComboBox.SelectedValue;
            aDSLSellTraffic.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Presence;

            if (RequestID == 0)
                RequestForADSL.SaveADSLSellTrafficRequest(_Request, aDSLSellTraffic, null, null, true);
            else
                RequestForADSL.SaveADSLSellTrafficRequest(_Request, aDSLSellTraffic, null, null, false);

            Service1 service = new Service1();
            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

            //ADSLInstalCostCenter installCost = ADSLInstallCostCenterDB.GetADSLInstallCostByCenterID(CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())));
            RequestPayment requestPayment = new RequestPayment();

            ADSLService Service = ADSLServiceDB.GetADSLServiceById((int)aDSLSellTraffic.AdditionalServiceID);
            BaseCost baseCost = BaseCostDB.GetSellTrafficCostForADSL();

            if (_Request.ID != 0)
                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

            if (requestPayment == null)
                requestPayment = new RequestPayment();
            else
            {
                if (requestPayment.IsPaid != null)
                    if ((bool)requestPayment.IsPaid)
                        if (requestPayment.AmountSum != Service.PriceSum)
                            throw new Exception("هزینه خرید ترافیک  پرداخت شده است، امکان تغییر آن وجود ندارد");
            }

            requestPayment.BaseCostID = baseCost.ID;
            requestPayment.RequestID = _Request.ID;
            requestPayment.Cost = Service.Price;
            requestPayment.Tax = Service.Tax;
            requestPayment.AmountSum = Service.PriceSum;
            requestPayment.PaymentType = (byte)Service.PaymentTypeID;
            requestPayment.IsKickedBack = false;
            requestPayment.IsAccepted = false;

            requestPayment.Detach();
            DB.Save(requestPayment);

            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

            RequestID = _Request.ID;
        }

        private void Save_WirelessSellTraffic()
        {
            if (_ADSLSellTrafficUserControl._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_ADSLSellTrafficUserControl._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            WirelessSellTraffic wirelessSellTraffic = new WirelessSellTraffic();
            RequestPayment requesrPayment = new RequestPayment();

            if (RequestID != 0)
                wirelessSellTraffic = ADSLSellTrafficDB.GetWirelessSellTrafficById(RequestID);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            _Request.TelephoneNo = null;
            _Request.CreatorUserID = DB.CurrentUser.ID;
            _Request.ModifyUserID = DB.CurrentUser.ID;
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            if (Customer != null)
                _Request.CustomerID = Customer.ID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            wirelessSellTraffic.AdditionalServiceID = (int)_ADSLSellTrafficUserControl.AdditionalServiceComboBox.SelectedValue;
            wirelessSellTraffic.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Presence;
            if (_WirelessCode != null)
                wirelessSellTraffic.WirelessCode = _WirelessCode;

            if (RequestID == 0)
                RequestForADSL.SaveADSLSellTrafficRequest(_Request, null, wirelessSellTraffic, null, true);
            else
                RequestForADSL.SaveADSLSellTrafficRequest(_Request, null, wirelessSellTraffic, null, false);

            RequestPayment requestPayment = new RequestPayment();

            ADSLService Service = ADSLServiceDB.GetADSLServiceById((int)wirelessSellTraffic.AdditionalServiceID);
            BaseCost baseCost = BaseCostDB.GetSellTrafficCostForADSL();

            if (_Request.ID != 0)
                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

            if (requestPayment == null)
                requestPayment = new RequestPayment();
            else
            {
                if (requestPayment.IsPaid != null)
                    if ((bool)requestPayment.IsPaid)
                        if (requestPayment.AmountSum != Service.PriceSum)
                            throw new Exception("هزینه خرید ترافیک  پرداخت شده است، امکان تغییر آن وجود ندارد");
            }

            requestPayment.BaseCostID = baseCost.ID;
            requestPayment.RequestID = _Request.ID;
            requestPayment.Cost = Service.Price;
            requestPayment.Tax = Service.Tax;
            requestPayment.AmountSum = Service.PriceSum;
            requestPayment.PaymentType = (byte)Service.PaymentTypeID;
            requestPayment.IsKickedBack = false;
            requestPayment.IsAccepted = false;

            requestPayment.Detach();
            DB.Save(requestPayment);

            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

            RequestID = _Request.ID;
        }

        private void Save_ADSLChangePlaceRequest()
        {
            if (!_ADSLChangePlaceUserControl._HasPort)
                throw new Exception("در مرکز جدید تجهیزات فنی وجود ندارد");

            if (!_ADSLChangePlaceUserControl._IsNotADSL)
                throw new Exception("شماره جدید دارای ADSL می باشد");

            ADSLChangePlace aDSLChangePlace = new ADSLChangePlace();
            Data.ADSL aDSL = new Data.ADSL();

            int centerid = CenterDB.GetCenterIDbyTelephoneNo(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text));
            int currentMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text), centerid);
            List<Data.ADSLPort> portFreeList = ADSLMDFDB.GetFreeADSLPortByCenterID(centerid, currentMDFID);

            if (portFreeList.Count != 0)
            {
                if (_Request.StatusID != 0)
                {
                    Status StatusPlace = StatusDB.GetStatusByID(_Request.StatusID);
                    RequestStep requestStep = RequestStepDB.GetRequestStepByID(StatusPlace.RequestStepID);
                    if (requestStep.StepTitle.Contains(" ام دی اف - تخلیه"))
                    {
                        Data.ADSLPort port = portFreeList[0];
                        port.Status = (byte)DB.ADSLPortStatus.reserve;
                        port.TelephoneNo = Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text);
                        port.Detach();
                        Save(port);
                        aDSLChangePlace.NewPortID = port.ID;
                    }

                }
                if (RequestID != 0)
                    aDSLChangePlace = ADSLChangePlaceDB.GetADSLChangePlaceById(RequestID);

                if (RequestID == 0)
                {
                    if (_TelephoneNo == 0)
                    {
                        _Request.TelephoneNo = Telephone.TelephoneNo;
                        aDSL = ADSLDB.GetADSLByTelephoneNo(Telephone.TelephoneNo);
                    }
                    else
                    {
                        _Request.TelephoneNo = _TelephoneNo;
                        aDSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);
                    }

                    CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                    _Request.StatusID = status.ID;
                }
                else
                    aDSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);

                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Customer.ID;
                _Request.RequestTypeID = RequestType.ID;
                _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
                _Request.IsVisible = true;

                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                if (user != null)
                    _Request.SellerID = user.SellerAgentID;

                _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);

                aDSLChangePlace.NewTelephoneNo = Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text);
                aDSLChangePlace.NewCenterID = Data.CenterDB.GetCenterIDByName(_ADSLChangePlaceUserControl.NewCenterNameTextBox.Text);
                aDSLChangePlace.OldTelephoneNo = (long)_Request.TelephoneNo;
                aDSLChangePlace.OldCenterID = _Request.CenterID;

                if (aDSL.ADSLPortID != null && aDSL.ADSLPortID != 0)
                    aDSLChangePlace.OldPortID = (long)aDSL.ADSLPortID;
                else
                    throw new Exception("برای این شماره پورتی از قدیم وجود ندارد");

                aDSLChangePlace.CustomerOwnerID = aDSL.CustomerOwnerID;

                aDSLChangePlace.HasCost = _ADSLChangePlaceUserControl.CostCheckBox.IsChecked;

                if (RequestID == 0)
                    RequestForADSL.SaveADSLChangePlaceRequest(_Request, aDSLChangePlace, true);
                else
                    RequestForADSL.SaveADSLChangePlaceRequest(_Request, aDSLChangePlace, false);
            }
            else
                throw new Exception("در این مرکز تجهیزات فنی وجود ندارد");

            if (_ADSLChangePlaceUserControl.CostCheckBox.IsChecked == true)
            {
                RequestPayment requestPayment = new RequestPayment();
                BaseCost baseCost = BaseCostDB.GetChangeNumberForADSL();

                if (_Request.ID != 0)
                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                if (requestPayment == null)
                    requestPayment = new RequestPayment();

                requestPayment.BaseCostID = baseCost.ID;
                requestPayment.RequestID = _Request.ID;
                requestPayment.Cost = baseCost.Cost;
                requestPayment.Tax = baseCost.Tax;
                if (baseCost.Tax != null && baseCost.Tax != 0)
                    requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((requestPayment.Tax * 0.01) * requestPayment.Cost));
                else
                    requestPayment.AmountSum = requestPayment.Cost;
                requestPayment.PaymentType = (byte)baseCost.PaymentType;
                requestPayment.IsKickedBack = false;
                requestPayment.IsAccepted = false;

                requestPayment.Detach();
                DB.Save(requestPayment);

                RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
            }
            else
            {
                RequestPayment requestPayment = new RequestPayment();
                BaseCost baseCost = BaseCostDB.GetChangeNumberForADSL();

                if (_Request.ID != 0)
                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                if (requestPayment != null)
                    if (requestPayment.IsPaid != null && requestPayment.IsPaid == true)
                        throw new Exception("پرداخت مربوطه انجام شده است");
                    else
                        throw new Exception("لطفا پرداخت مورد نظر را حذف نمایید");
            }

            RequestID = _Request.ID;
        }

        private void Save_ADSLChangePortLRequest()
        {
            Data.ADSLChangePort1 aDSLChangePort = new CRM.Data.ADSLChangePort1();
            Data.ADSL aDSL = new Data.ADSL();

            if (RequestID != 0)
                aDSLChangePort = ADSLChangePortDB.GetADSLChangePortByID(RequestID);

            if (_relatedRequestID != 0)
                _Request.RelatedRequestID = _relatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                {
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                    aDSL = ADSLDB.GetADSLByTelephoneNo(Telephone.TelephoneNo);
                }
                else
                {
                    _Request.TelephoneNo = _TelephoneNo;
                    aDSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);
                }
                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }
            else
                aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(_Request.TelephoneNo));

            Customer = CustomerDB.GetCustomerByTelephone((long)_Request.TelephoneNo);
            _Request.CenterID = (int)CenterComboBox.SelectedValue;
            if (Customer != null)
                _Request.CustomerID = Customer.ID;

            _Request.RequestTypeID = RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            if (aDSL.ADSLPortID != null && aDSL.ADSLPortID != 0)
                aDSLChangePort.OldPortID = (long)aDSL.ADSLPortID;
            else
                throw new Exception("برای این شماره پورتی از قدیم وجود ندارد");

            if (_ADSLChangePort.StatusComboBox.SelectedValue != null)
                aDSLChangePort.OldPortStatusID = (byte)Convert.ToInt16(_ADSLChangePort.StatusComboBox.SelectedValue);
            else
                throw new Exception("لطفا وضعیت پورت قدیم را تعیین نمایید");
            aDSLChangePort.Comment = _ADSLChangePort.CommentTextBox.Text;

            if (_ADSLChangePort.ADSLChangePortReasonComboBox.SelectedValue != null)
                aDSLChangePort.ReasonID = (int)_ADSLChangePort.ADSLChangePortReasonComboBox.SelectedValue;
            else
                throw new Exception("لطفا علت تعویض پورت را تعیین نمایید");

            if (RequestID == 0)
                RequestForADSL.SaveADSLChangePortRequest(_Request, aDSLChangePort, true);
            else
                RequestForADSL.SaveADSLChangePortRequest(_Request, aDSLChangePort, false);

            RequestID = _Request.ID;
        }

        private void Save_ADSLChangeCustomerOwnerCharacteristics()
        {
            ADSLChangeCustomerOwnerCharacteristic adslChangeCustomerOwnerCharacteristic = new ADSLChangeCustomerOwnerCharacteristic();
            CRM.Data.ADSL adsl = new CRM.Data.ADSL();

            if (RequestID != 0)
                adslChangeCustomerOwnerCharacteristic = ADSLChangeCustomerOwnerCharacteristicsDB.GetADSLChangeCustomerOwnerCharacteristicsByID(RequestID);

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                {
                    _Request.TelephoneNo = Telephone.TelephoneNo;
                    adsl = ADSLDB.GetADSLByTelephoneNo(Telephone.TelephoneNo);
                }
                else
                {
                    _Request.TelephoneNo = _TelephoneNo;
                    adsl = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);
                }

                CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            else
                adsl = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);

            adslChangeCustomerOwnerCharacteristic.OldCustomerOwnerID = adsl.CustomerOwnerID;
            adslChangeCustomerOwnerCharacteristic.NewCustomerOwnerID = _ADSLChangeCustomerOwnerCharacteristicsUserControl.ADSLCustomer.ID;
            adslChangeCustomerOwnerCharacteristic.NewCustomerOwnerStatus = (byte)_ADSLChangeCustomerOwnerCharacteristicsUserControl.ADSLOwnerStatusComboBox.SelectedValue;
            _Request.CustomerID = adsl.CustomerOwnerID;
            _Request.RequestTypeID = RequestType.ID;
            _Request.IsVisible = true;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                _Request.SellerID = user.SellerAgentID;

            if (RequestID == 0)
                RequestForADSL.SaveADSLChangeCustomerCharacteristics(_Request, adslChangeCustomerOwnerCharacteristic, true);
            else
                RequestForADSL.SaveADSLChangeCustomerCharacteristics(_Request, adslChangeCustomerOwnerCharacteristic, false);

            RequestID = _Request.ID;
        }

        private void Save_TitleIn118Request()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //   _Request.RequestPaymentTypeID = (byte)RequestPaymentTypeListBox.SelectedValue;

                _Request.CenterID = (int)CenterComboBox.SelectedValue;
                _Request.CustomerID = Customer.ID;
                _Request.RequestTypeID = RequestType.ID;
                _Request.TelephoneNo = Telephone.TelephoneNo;
                _Request.IsVisible = true;

                _titleIn118 = _Title118.TitleGroupBox.DataContext as TitleIn118;
                _titleIn118.TelephoneNo = (long)Telephone.TelephoneNo;

                if (IsForward)
                {
                    _titleIn118.Date = DB.GetServerDate();
                }


                if (RequestID == 0)
                {
                    _Request.ID = DB.GenerateRequestID();
                    _Request.StatusID = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start).ID;
                    _Request.Detach();
                    DB.Save(_Request, true);


                    _titleIn118.ID = _Request.ID;
                    _titleIn118.Status = (byte)RequestType.ID;
                    _titleIn118.Detach();
                    DB.Save(_titleIn118, true);
                }
                else
                {
                    _Request.Detach();
                    DB.Save(_Request, false);



                    _titleIn118.Detach();
                    DB.Save(_titleIn118, false);
                }
                ts.Complete();
            }

        }

        private void Save_SpaceAndPowerRequest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (_V2SpaceAndPower.AntennaInfoGrid.Visibility == Visibility.Visible)
                {
                    long? count = 0;
                    if (string.IsNullOrEmpty(_V2SpaceAndPower.AntennaNameTextBox.Text))
                    {
                        throw new Exception(".تعیین نوع آنتن برای آنتن الزامی میباشد");
                    }
                    else if (string.IsNullOrEmpty(_V2SpaceAndPower.AntennaCountTextBox.Text))
                    {
                        throw new Exception(".تعیین تعداد برای آنتن الزامی میباشد");
                    }
                    else if (string.IsNullOrEmpty(_V2SpaceAndPower.AntennaHeightTextBox.Text))
                    {
                        throw new Exception(".تعیین ارتفاع برای آنتن الزامی میباشد");
                    }
                    else if (!string.IsNullOrEmpty(_V2SpaceAndPower.AntennaCountTextBox.Text) && !Helper.CheckDigitDataEntry(_V2SpaceAndPower.AntennaCountTextBox, out count))
                    {
                        throw new Exception(".برای تعیین تعداد فقط از اعداد استفاده نمائید");
                    }
                }

                if (_V2SpaceAndPower._Customer == null)
                {
                    throw new Exception(".تعیین متقاضی الزامی میباشد");
                }
                else if (_V2SpaceAndPower.SpaceTypeComboBox.SelectedValue == null)
                {
                    throw new Exception(".تعیین نوع فضا الزامی میباشد");
                }
                else if (_V2SpaceAndPower.EquipmentTypeComboBox.SelectedValue == null)
                {
                    throw new Exception(".تعیین نوع تجهیزات الزامی میباشد");
                }
                else if (_V2SpaceAndPower.PowerTypesComboBox.SelectedIDs.Count == 0)
                {
                    throw new Exception(".تعیین نوع پاور مصرفی الزامی میباشد");
                }
                else if (string.IsNullOrEmpty(_V2SpaceAndPower.SpaceSizeTextBox.Text))
                {
                    throw new Exception(".تعیین متراژ فضا - متر مربع الزامی میباشد");
                }
                else if (string.IsNullOrEmpty(_V2SpaceAndPower.EquipmentWeightTextBox.Text))
                {
                    throw new Exception(".تعیین وزن تجهیزات الزامی میباشد");
                }
                else if (string.IsNullOrEmpty(_V2SpaceAndPower.DurationTextBox.Text))
                {
                    throw new Exception(".تعیین مدت زمان - ماه الزامی میباشد");
                }
                else if (string.IsNullOrEmpty(_V2SpaceAndPower.SpaceUsageTextBox.Text))
                {
                    throw new Exception(".تعیین کاربری فضا الزامی میباشد");
                }
                else
                {
                    Customer = _V2SpaceAndPower._Customer;
                    Data.SpaceAndPower spaceAndPower = _V2SpaceAndPower.SpaceAndPowerGroupBox.DataContext as CRM.Data.SpaceAndPower;
                    spaceAndPower.SpaceAndPowerCustomerID = _V2SpaceAndPower._Customer.ID;
                    _Request.CenterID = (int)CenterComboBox.SelectedValue;
                    _Request.CustomerID = Customer.ID;

                    //نوع پاور و میزان آن در جدولی جداگانه برای هر رکورد فضا و پاور ذخیره میشود
                    spaceAndPower.PowerRate = string.Empty;

                    //در صورت توسعه باید شناسه درخواست درحال توسعه ذخیره شود
                    if (
                         (_V2SpaceAndPower.PreviousSpaceAndPowerRequestsDataGrid.Visibility == Visibility.Visible)
                         &&
                         (_V2SpaceAndPower.PreviousSpaceAndPowerRequestsDataGrid.SelectedItem != null)
                        )
                    {
                        SpaceAndPowerInfo previousSpaceAndPowerInfo = _V2SpaceAndPower.PreviousSpaceAndPowerRequestsDataGrid.SelectedItem as SpaceAndPowerInfo;
                        _Request.MainRequestID = previousSpaceAndPowerInfo.RequestID;

                        //لزوم یکسان بودن مرکز انتخاب شده در این فرم با مرکز درخواست در حال توسعه
                        if (previousSpaceAndPowerInfo.CenterID != (int)CenterComboBox.SelectedValue)
                        {
                            throw new Exception(".مرکز انتخاب شده باید با مرکز درخواست درحال توسعه یکسان باشد");
                        }
                    }
                    else if (_V2SpaceAndPower.PreviousSpaceAndPowerRequestsDataGrid.Visibility == Visibility.Collapsed) //کاربر تیک توسعه را برداشته است
                    {
                        _Request.MainRequestID = null;
                    }

                    if (RequestID == 0)
                    {
                        _Request.RequestTypeID = RequestType.ID;
                        _Request.IsVisible = true;
                        CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
                        _Request.StatusID = status.ID;
                        RequestForSpaceAndPower.SaveSpaceAndPowerRequest(_Request, spaceAndPower, null, true);
                    }
                    else
                    {
                        RequestForSpaceAndPower.SaveSpaceAndPowerRequest(_Request, spaceAndPower, null, false);
                    }

                    //ذخیره کردن رکوردهای نوع پاور مصرفی
                    PowerTypeDB.SavePowerTypes(spaceAndPower.ID, _V2SpaceAndPower.PowerTypesComboBox.SelectedIDs);

                    Antenna antenna = (_V2SpaceAndPower.AntennaInfoGrid.DataContext as Antenna);
                    if (_V2SpaceAndPower.AntennaCheckBox.IsChecked == true)
                    {
                        antenna.SpaceAndPowerID = spaceAndPower.ID;
                        antenna.Detach();
                        if (antenna.ID == 0)
                        {
                            DB.Save(antenna, true);
                        }
                        else
                        {
                            DB.Save(antenna, false);
                        }
                    }
                    else if ((_V2SpaceAndPower.AntennaCheckBox.IsChecked == false) && (antenna.ID != 0))
                    {
                        //آنتن مربوطه برای درخواست فضا و پاور ذخیره شده است اما کاربر در ویرایش تیک آنتن را برداشته است
                        //بنابراین باید رکورد آنتن ، فضا و پاور در حال ویرایش ، حذف شود
                        DB.Delete<Antenna>(antenna.ID);
                    }
                    scope.Complete();
                }
            }
        }

        private void Save_Failure117Request()
        {
            Data.Failure117 failure = new Data.Failure117();

            if (_Failure117.LineStatusComboBox.SelectedValue != null)
                failure.LineStatusID = (int)_Failure117.LineStatusComboBox.SelectedValue;
            else
                failure.LineStatusID = null;

            if (_Failure117.ActionStatusComboBox.SelectedValue != null)
                failure.ActionStatusID = (byte)_Failure117.ActionStatusComboBox.SelectedValue;
            else
                failure.ActionStatusID = null;

            if (_Failure117.FailureStatusComboBox.SelectedValue != null)
                failure.FailureStatusID = (int)_Failure117.FailureStatusComboBox.SelectedValue;
            else
                failure.FailureStatusID = null;

            if (city == "semnan")
            {
                Service1 service2 = new Service1();
                System.Data.DataTable telephoneInfo = service2.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                failure.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                failure.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                failure.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                failure.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
            }

            if (city == "kermanshah")
            {
                TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo(_TelephoneNo);

                failure.CabinetNo = technicalInfo.CabinetNo;
                failure.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                failure.PostNo = technicalInfo.PostNo;
                failure.PostEtesali = technicalInfo.ConnectionNo;
            }

            //if (_Failure117.MDFPersonnelComboBox.SelectedValue != null)
            failure.MDFPesonnelID = DB.CurrentUser.ID;// (int)_Failure117.MDFPersonnelComboBox.SelectedValue;
            //else
            //    throw new Exception("لطفا نام مامور MDF را انتخاب نمایید");
            failure.MDFCommnet = _Failure117.CommentTextBox.Text;

            _Request.IsViewed = false;
            _Request.TelephoneNo = _Failure117.TelephoneNo;
            _Request.RequestTypeID = RequestType.ID;
            _Request.IsVisible = true;
            // _Request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
            _Request.CreatorUserID = DB.CurrentUser.ID;
            _Request.ModifyUserID = DB.CurrentUser.ID;
            if (CenterComboBox.SelectedValue != null)
                _Request.CenterID = (int)CenterComboBox.SelectedValue;
            else
                throw new Exception("لطفا نام مرکز را انتحاب نمایید!");
            //_Request.CustomerID = Customer.ID;

            CRM.Data.Status status = DB.GetStatus(RequestType.ID, (int)DB.RequestStatusType.Start);
            _Request.StatusID = status.ID;

            if (RequestID == 0)
                RequestForFailure117.SaveFailureRequest(_Request, failure, true);
            else
                RequestForFailure117.SaveFailureRequest(_Request, failure, false);
        }

        public override bool Forward()
        {
            try
            {
                // For check whether the action is Forward
                IsForward = true;
                //
                switch (RequestType.ID)
                {
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                    case (byte)DB.RequestType.ChangeAddress:
                    case (byte)DB.RequestType.ChangeName:
                    case (byte)DB.RequestType.ChangeNo:
                    case (byte)DB.RequestType.CutAndEstablish:
                    case (byte)DB.RequestType.Connect:
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Dischargin:
                    case (byte)DB.RequestType.E1Fiber:
                    case (byte)DB.RequestType.OpenAndCloseZero:
                    case (byte)DB.RequestType.RefundDeposit:
                    case (byte)DB.RequestType.Reinstall:
                    case (byte)DB.RequestType.SpecialService:
                    case (byte)DB.RequestType.TitleIn118:
                    case (byte)DB.RequestType.RemoveTitleIn118:
                    case (byte)DB.RequestType.ChangeTitleIn118:
                        {
                            if (PreForward())
                            {
                                IsSaveSuccess = true;
                                IsForwardSuccess = true;
                            }
                            else
                            {
                                Save();

                                //TODO:rad 13950806
                                if (RequestType.ID == (int)DB.RequestType.Dayri)
                                {
                                    if (_Request.ID != 0)
                                    {
                                        CustomerToCalculateDebt();
                                    }
                                }
                                else
                                {
                                    //milad doran
                                    CustomerToCalculateDebt();
                                }

                                if (IsSaveSuccess == true)
                                    IsForwardSuccess = true;
                                else
                                    IsForwardSuccess = false;
                            }

                            // ----- Check Payment of Request -----
                            if (_Request.RequestTypeID == (int)DB.RequestType.Dayri) //چنانچه درخواست جاری دایری باشد فقط باید زمانی پرداخت های نقدی آن چک شود که وضعیت درخواست "دریافت مدرک و هزینه "باشد
                            {
                                Status status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);

                                if (status != null && status.StatusType == (int)DB.RequestStatusType.GetCosts)
                                {
                                    //CustomerType customerType = CustomerTypeDB.GetCustomerTypeByID(_installReqeust.TelephoneType);
                                    //CustomerGroup customerGroup = CustomerGroupDB.GetCustomerGroupById(_installReqeust.TelephoneTypeGroup.Value);
                                    //
                                    ////گروه تلفن هایی که چک کردن هزینه برای آنها الزامی نیست
                                    //string[] nonBindingCostsTelephoneGroups = { "استیجاری همگانی", "همگانی", "مخابرات", "موقت", "موقت دولتی" };
                                    //
                                    ////TODO:rad در صورتی که نوع تلفن همگانی ، موقت ، مخابرات باشد نباید هزینه نقدی بودن را چک کند
                                    ////if (nonBindingCostsTelephoneGroups.Where(item => item.Contains(customerGroup.Title.Trim())).Count() == 0)
                                    if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value != (byte)DB.MethodOfPaymentForTelephoneConnection.Other)
                                    {
                                        if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Installment)
                                        {
                                            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                            {
                                                throw new Exception("لطفاً پرداخت های نقدی مربوطه را بپردازید");
                                            }
                                            else if (!RequestDB.HasTelephoneConnectionInstallment(this._Request.ID))
                                            {
                                                throw new Exception("لطفاً اقساط مربوط به هزینه اتصال تلفن را مشخص نمائید");
                                            }
                                        }
                                        else if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Cash)
                                        {
                                            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                                throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                        }
                                        //else if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Other)
                                        //{
                                        //    //TODO:rad در صورتی که نوع تلفن همگانی ، موقت ، مخابرات باشد نباید هزینه نقدی بودن را چک کند
                                        //    CustomerType customerType = CustomerTypeDB.GetCustomerTypeByID(_installReqeust.TelephoneType);
                                        //    CustomerGroup customerGroup = CustomerGroupDB.GetCustomerGroupById(_installReqeust.TelephoneTypeGroup.Value);
                                        //
                                        //    //گروه تلفن هایی که چک کردن هزینه برای آنها الزامی نیست
                                        //    string[] nonBindingCostsTelephoneGroups = { "استیجاری همگانی", "همگانی", "مخابرات", "موقت" };
                                        //
                                        //    //در صورتی پرداخت هزینه را چک کند که گروه تلفن درخواست جاری ، جزء گروه تلفن های فوقالذکر نباشد
                                        //    if (!nonBindingCostsTelephoneGroups.Contains(customerGroup.Title.Trim()))
                                        //    {
                                        //        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                        //            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                        //    }
                                        //}
                                        else
                                        {
                                            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                                throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                            }
                            // ----- End : Check Payment of Request -----

                            CheckRequestDocument();
                        }
                        break;
                    case (byte)DB.RequestType.E1:
                    case (byte)DB.RequestType.E1Link:
                        {
                            Save();
                            // ----- Check Payment of Request -----
                            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                            // ----- End : Check Payment of Request -----

                            // ----- Check TelecomminucationServicePayments for current Request
                            if (
                                 (RequestType.ID == (byte)DB.RequestType.E1)
                                  &&
                                 (this._TelecomminucationServicePaymentInfos.Count == 0)
                                  &&
                                  (DB.City.ToLower() == "gilan")
                               )
                                throw new Exception("تعیین کالا وخدمات مخابرات الزامی است");
                            // ----- End : Check TelecomminucationServicePayments for current Request

                            CheckRequestDocument();

                            if (Data.StatusDB.IsStartStep(_Request.StatusID))
                            {
                                List<E1Link> e1Links = new List<E1Link>();

                                int i = 1;
                                if (RequestType.ID == (byte)DB.RequestType.E1Link)
                                {

                                    i = (int)(Data.E1DB.GetNumberOfLink((long)_Request.TelephoneNo) + 1);
                                }
                                int count = i + (int)_e1.NumberOfLine;
                                for (; i < count; i++)
                                {
                                    E1Link e1Link = new E1Link();
                                    e1Link.ReqeustID = _Request.ID;
                                    e1Link.LinkNumber = i;
                                    e1Links.Add(e1Link);
                                }

                                DB.SaveAll(e1Links);

                            }

                            if (IsSaveSuccess == true)
                            {
                                if (RequestType.ID == (byte)DB.RequestType.E1)
                                {
                                    Status status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                                    //باید در اینجا چک شود که آیا درخواست در حال توسعه فایلی داشته است یا خیر
                                    //در صورت فایل داشتن باید برای درخواست جدید هم ذخیره شود
                                    if (_Request.MainRequestID.HasValue && status.StatusType == (byte)DB.RequestStatusType.Start)
                                    {
                                        List<PowerOffice> powerOfficeFiles = PowerOfficeDB.GetPowerOfficeByRequestID(_Request.MainRequestID.Value);
                                        List<CableDesignOffice> cableOfficeFiles = CableDesignOfficeDB.GetCableDesignOfficeByRequestID(_Request.MainRequestID.Value);
                                        List<TransferDepartmentOffice> transferDepartmentFiles = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(_Request.MainRequestID.Value);
                                        List<SwitchOffice> switchOfficeFiles = SwitchOfficeDB.GetSwitchOfficeByRequestID(_Request.MainRequestID.Value);

                                        //کپی کردن فایل ها
                                        DB.CopyFiles(_Request.ID, powerOfficeFiles.Select(pf => pf.PowerFileID).ToList(), DB.FileOfficeType.PowerOfficeFile);//عملیات کپی فایل های اداره طراحی نیرو
                                        DB.CopyFiles(_Request.ID, cableOfficeFiles.Select(cf => cf.CableDesignFileID).ToList(), DB.FileOfficeType.CableOfficeFile);//عملیات کپی فایل های اداره شبکه و کابل
                                        DB.CopyFiles(_Request.ID, transferDepartmentFiles.Select(tf => tf.TransferDepartmentFileID).ToList(), DB.FileOfficeType.TransferDepartmentFile);//عملیات کپی فایل های اداره انتقال
                                        DB.CopyFiles(_Request.ID, switchOfficeFiles.Select(sf => sf.SwitchFileID).ToList(), DB.FileOfficeType.SwitchOfficeFile);//عملیات کپی فایل های اداره طراحی سوئیچ
                                    }
                                }
                                IsForwardSuccess = true;
                            }
                            else
                                IsForwardSuccess = false;
                        }
                        break;
                    case (byte)DB.RequestType.SpecialWire:
                        {
                            // only source center can change information
                            if (_Request.MainRequestID == null)
                            {
                                Save();
                                // ----- Check Payment of Request -----
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                // ----- End : Check Payment of Request -----

                                CheckRequestDocument();

                                // in start step save other center of reqeust
                                if (Data.StatusDB.IsStartStep(_Request.StatusID))
                                {
                                    CustomerToCalculateDebt();
                                    SaveOtherRequestCenterSpecialWire();
                                }

                                Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                                if (Status != null && Status.StatusType == (byte)DB.RequestStatusType.GetCosts)
                                {
                                    Data.SpecialWireDB.SpecialWireReturnOtherReqeust(_Request.ID);
                                }
                            }
                            else
                            {
                                // ----- Check Payment of Request -----
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                // ----- End : Check Payment of Request -----

                                CheckRequestDocument();
                                IsSaveSuccess = true;
                            }

                            if (IsSaveSuccess == true)
                                IsForwardSuccess = true;
                            else
                                IsForwardSuccess = false;
                        }
                        break;

                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        {
                            // only source center can change information
                            Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);

                            if ((Status != null && Status.StatusType != (byte)DB.RequestStatusType.GetCosts) || (Data.StatusDB.IsFinalStep(_Request.StatusID)))
                            {
                                // ----- Check Payment of Request -----
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                // ----- End : Check Payment of Request -----

                                CheckRequestDocument();

                                IsSaveSuccess = true;
                            }
                            else
                            {
                                Save();
                                // ----- Check Payment of Request -----
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                // ----- End : Check Payment of Request -----

                                CheckRequestDocument();

                                // in start step save other center of reqeust
                                if (Data.StatusDB.IsStartStep(_Request.StatusID) && IsSaveSuccess == true)
                                {
                                    CustomerToCalculateDebt();
                                    if (!OtherRequestCenterSavedInChangeLocationSpecialWire())
                                        throw new Exception("خطا در ارجاع درخواست");
                                }
                            }


                            if (IsSaveSuccess == true)
                                IsForwardSuccess = true;
                            else
                                IsForwardSuccess = false;
                        }
                        break;

                    case (byte)DB.RequestType.VacateSpecialWire:
                        {
                            // only source center can change information
                            if (_Request.MainRequestID == null && !Data.StatusDB.IsFinalStep(_Request.StatusID))
                            {
                                Save();

                                // ----- Check Payment of Request -----
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                // ----- End : Check Payment of Request -----

                                CheckRequestDocument();

                                // in start step save other center of reqeust
                                if (Data.StatusDB.IsStartStep(_Request.StatusID) && IsSaveSuccess == true)
                                {
                                    CustomerToCalculateDebt();

                                    if (!OtherRequestCenterSavedInVacateSpecialWire())
                                        throw new Exception("خطا در ایجاد درخواست مراکز دیگر");
                                }
                            }
                            else
                            {
                                // ----- Check Payment of Request -----
                                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                                // ----- End : Check Payment of Request -----

                                CheckRequestDocument();

                                IsSaveSuccess = true;
                            }

                            if (IsSaveSuccess == true)
                                IsForwardSuccess = true;
                            else
                                IsForwardSuccess = false;
                        }
                        break;

                    case (byte)DB.RequestType.ADSL:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        CheckRequestDocument();
                        Forward_ADSL();
                        break;

                    case (byte)DB.RequestType.ADSLInstall:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        CheckRequestDocument();
                        Forward_ADSLInstall();
                        break;

                    case (byte)DB.RequestType.ADSLDischarge:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        CheckRequestDocument();
                        IsForwardSuccess = true;
                        break;

                    case (byte)DB.RequestType.ADSLChangePort:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        CheckRequestDocument();
                        Forward_ADSLChangePort();
                        break;

                    case (byte)DB.RequestType.ADSLChangePlace:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        CheckRequestDocument();
                        Forward_ADSLChangePlace();

                        break;

                    case (byte)DB.RequestType.Failure117:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        Forward_Failure117();
                        break;

                    case (byte)DB.RequestType.Wireless:
                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        Forward_Wireless();
                        break;

                    case (byte)DB.RequestType.SpaceandPower:
                        {
                            Save();
                            if (IsSaveSuccess)
                            {

                                Status status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);


                                //// ----- Check TelecomminucationServicePayments for current Request
                                ////نداشتن کالا و خدمات فقط باید در مرحله عقدقرارداد بررسی شود
                                //بلاک ایف زیر به علت درخواست آقای کاوسی از استان گیلان فکس 1395/03/18 کامنت شد
                                //بررسی وجود کالا  و خدمات در فرم عقدقرارداد انجام میگیرد
                                //if (this._TelecomminucationServicePaymentInfos.Count == 0 && status.StatusType == (byte)DB.RequestStatusType.TelecomminucationServicePaymentChecking)
                                //{
                                //    RequestTelecomminucationServiceTabItem.IsSelected = true;
                                //    throw new Exception("تعیین کالا وخدمات مخابرات الزامی است");
                                //}
                                //// ----- End : Check TelecomminucationServicePayments for current Request

                                if (this._TelecomminucationServicePaymentInfos.Count == 0 && status.StatusType == (byte)DB.RequestStatusType.TelecomminucationServicePaymentChecking)
                                {
                                    RequestTelecomminucationServiceTabItem.IsSelected = true;
                                    throw new Exception("تعیین کالا وخدمات مخابرات الزامی است");
                                }

                                //باید در اینجا چک شود که آیا درخواست در حال توسعه فایلی داشته است یا خیر
                                //در صورت فایل داشتن باید برای درخواست جدید هم ذخیره شود
                                if (_Request.MainRequestID.HasValue && status.StatusType == (byte)DB.RequestStatusType.Start)
                                {
                                    List<PowerOffice> powerOfficeFiles = PowerOfficeDB.GetPowerOfficeByRequestID(_Request.MainRequestID.Value);
                                    List<CableDesignOffice> cableOfficeFiles = CableDesignOfficeDB.GetCableDesignOfficeByRequestID(_Request.MainRequestID.Value);
                                    List<TransferDepartmentOffice> transferDepartmentFiles = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(_Request.MainRequestID.Value);
                                    List<SwitchOffice> switchOfficeFiles = SwitchOfficeDB.GetSwitchOfficeByRequestID(_Request.MainRequestID.Value);

                                    //کپی کردن فایل ها
                                    DB.CopyFiles(_Request.ID, powerOfficeFiles.Select(pf => pf.PowerFileID).ToList(), DB.FileOfficeType.PowerOfficeFile);//عملیات کپی فایل های اداره طراحی نیرو
                                    DB.CopyFiles(_Request.ID, cableOfficeFiles.Select(cf => cf.CableDesignFileID).ToList(), DB.FileOfficeType.CableOfficeFile);//عملیات کپی فایل های اداره شبکه و کابل
                                    DB.CopyFiles(_Request.ID, transferDepartmentFiles.Select(tf => tf.TransferDepartmentFileID).ToList(), DB.FileOfficeType.TransferDepartmentFile);//عملیات کپی فایل های اداره انتقال
                                    DB.CopyFiles(_Request.ID, switchOfficeFiles.Select(sf => sf.SwitchFileID).ToList(), DB.FileOfficeType.SwitchOfficeFile);//عملیات کپی فایل های اداره طراحی سوئیچ
                                }
                                IsForwardSuccess = true;
                            }
                            else
                            {
                                IsForwardSuccess = false;
                            }

                        }
                        break;
                    default:

                        if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                            throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                        Save();
                        CheckRequestDocument();
                        IsForwardSuccess = true;
                        break;
                }
            }

            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در ذخیره و ارجاع درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در ذخیره و ارجاع درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره و ارجاع درخواست ، " + ex.Message + " !", ex);
            }

            return IsForwardSuccess;
        }

        private void Forward_Wireless()
        {
            Save_WirelessRequest();

            if (RequestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
            {
                string mobileNos = "";

                string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.WirelessRegister));

                string customerFullName = CustomerDB.GetFullNameByCustomerID(wirelessRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(wirelessRequest.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID(wirelessRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(mobileNos))
                {
                    SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.WirelessRegister));

                    if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
                    {
                        message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TeleohoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);

                        CRMWebServiceDB.SendMessage(mobileNos, message);
                    }
                }

            }
            else if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                AddUsertoIBSng();

                wirelessRequest = WirelessRequestDB.GetWirelessRequestByID(_Request.ID);
                Data.ADSL aDSL = ADSLDB.GetWirelessbyCode("WL" + _Request.ID.ToString());

                bool isNew = false;
                if (aDSL == null)
                {
                    aDSL = new Data.ADSL();
                    isNew = true;
                }

                if (_Request.TelephoneNo != null && _Request.TelephoneNo != 0)
                    aDSL.TelephoneNo = (long)_Request.TelephoneNo;
                else
                    aDSL.TelephoneNo = 2333388205;
                aDSL.Wireless = "WL" + _Request.ID.ToString();
                aDSL.TariffID = wirelessRequest.ServiceID;
                aDSL.CustomerOwnerID = (long)wirelessRequest.CustomerOwnerID;
                aDSL.CustomerOwnerStatus = wirelessRequest.CustomerOwnerStatus;
                aDSL.CustomerTypeID = wirelessRequest.CustomerTypeID;
                aDSL.CustomerGroupID = wirelessRequest.CustomerGroupID;
                aDSL.UserID = _UserID;
                aDSL.UserName = "WL" + _Request.ID.ToString();
                aDSL.OrginalPassword = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9);
                aDSL.HashPassword = GenerateMD5HashPassword(aDSL.OrginalPassword);
                aDSL.InstallDate = null;
                aDSL.ExpDate = null;
                aDSL.Status = (byte)DB.ADSLStatus.Connect;
                aDSL.WasPCM = null;

                aDSL.Detach();
                Save(aDSL, isNew);

                string mobileNos = "";

                string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.WirelessSale));

                string customerFullName = CustomerDB.GetFullNameByCustomerID(wirelessRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(wirelessRequest.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID(wirelessRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(mobileNos))
                {
                    SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.WirelessSale));

                    if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
                    {
                        message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("UserName", wirelessRequest.UserName).Replace("Password", wirelessRequest.OrginalPassword).Replace("Enter", Environment.NewLine);

                        CRMWebServiceDB.SendMessage(mobileNos, message);
                    }
                }

                CheckRequestDocument();
            }

            IsForwardSuccess = true;
        }

        public void AddUsertoIBSng()
        {
            string userName = string.Format("{0}{1}", "WL", _Request.ID.ToString());
            string passWord = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9);
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();
            object[] ids = new object[1];
            bool isAddable = false;

            userAuthentication.Clear();
            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", userName);
                userInfos = ibsngService.GetUserInfo(userAuthentication);
            }
            catch (Exception ex)
            {
                isAddable = true;
            }

            ADSLService service = ADSLServiceDB.GetADSLServiceById((int)wirelessRequest.ServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID);
            string credit = (((traffic.Credit != null) ? traffic.Credit : 1024) + ((service.ReserveCridit != null) ? service.ReserveCridit : 0)).ToString();

            if (isAddable)
            {
                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("count", 1);
                userAuthentication.Add("credit", credit);
                userAuthentication.Add("isp_name", "Main");
                userAuthentication.Add("group_name", service.IBSngGroupName);
                userAuthentication.Add("credit_comment", "");

                ids = ibsngService.AddNewUsers(userAuthentication);
                if (ids.Count() == 0)
                {
                    throw new Exception("ذخیره کاربر در سیستم IBSng با مشکل مواجه شد");
                }
                else
                {
                    wirelessRequest.UserID = ids[0].ToString();
                }
            }
            else
            {
                ids[0] = wirelessRequest.UserID;

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                XmlRpcStruct list = new XmlRpcStruct();
                list.Add("to_del_attrs", "lock");

                userAuthentication.Add("user_id", ids[0].ToString());
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", list);

                ibsngService.UpdateUserAttrs(userAuthentication);

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("user_id", ids[0].ToString());

                userAuthentication.Add("deposit", credit);
                userAuthentication.Add("is_absolute_change", false);
                userAuthentication.Add("deposit_type", "renew");
                userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Wireless Request (add new)");

                try
                {
                    ibsngService.changeDeposit(userAuthentication);
                }
                catch (Exception ex1)
                {
                    throw new Exception("خطا در ارتباط با IBSNG");
                }

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("user_id", ids[0].ToString());

                userInfo.Add("renew_next_group", service.IBSngGroupName);
                userInfo.Add("renew_remove_user_exp_dates", "1");
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", "");

                try
                {
                    ibsngService.UpdateUserAttrs(userAuthentication);
                }
                catch (Exception ex2)
                {
                    throw new Exception("خطا در ارتباط با IBSNG");
                }
            }

            userInfo = new XmlRpcStruct();
            XmlRpcStruct dictionary = new XmlRpcStruct();

            dictionary.Add("normal_username", userName);
            dictionary.Add("normal_password", "");

            userInfo.Add("normal_user_spec", dictionary);

            userInfo.Add("normal_password_bind_on_login", "");

            string customerName = CustomerDB.GetFullNameByCustomerID(wirelessRequest.CustomerOwnerID);
            userInfo.Add("name", customerName);

            Customer customer = CustomerDB.GetCustomerByID(wirelessRequest.CustomerOwnerID);
            Address address = AddressDB.GetAddressByID(wirelessRequest.AddressID);

            if (customer != null)
            {
                userInfo.Add("email", customer.Email);
                userInfo.Add("phone", wirelessRequest.UserName);
                userInfo.Add("cell_phone", customer.MobileNo);
                userInfo.Add("postal_code", address.PostalCode);
                userInfo.Add("address", address.AddressContent);
            }

            if (wirelessRequest.HasIP == true)
            {
                if (wirelessRequest.IPStaticID != null)
                {
                    ADSLIP iPStatic = ADSLIPDB.GetADSLIPById((long)wirelessRequest.IPStaticID);
                    userInfo.Add("assign_ip", iPStatic.IP);

                    iPStatic.Status = (byte)DB.ADSLIPStatus.Instal;
                    iPStatic.InstallDate = DB.GetServerDate();
                    iPStatic.ExpDate = DB.GetServerDate().AddMonths((int)wirelessRequest.IPDuration);

                    iPStatic.Detach();
                    DB.Save(iPStatic);
                }

                if (wirelessRequest.GroupIPStaticID != null)
                {
                    ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)wirelessRequest.GroupIPStaticID);
                    userInfo.Add("assign_ip", groupIP.VirtualRange);
                    userInfo.Add("assign_route_ip", groupIP.StartRange);

                    groupIP.Status = (byte)DB.ADSLIPStatus.Instal;
                    groupIP.InstallDate = DB.GetServerDate();
                    groupIP.ExpDate = DB.GetServerDate().AddMonths((int)wirelessRequest.IPDuration);

                    groupIP.Detach();
                    DB.Save(groupIP);
                }
            }

            _UserID = ids[0].ToString();

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", ids[0].ToString());
            userAuthentication.Add("attrs", userInfo);
            userAuthentication.Add("to_del_attrs", "");

            ibsngService.UpdateUserAttrs(userAuthentication);

            Update_FreeCounterWireless(userName, (int)service.OverNightCredit);

            if (wirelessRequest.AdditionalServiceID != null)
            {
                ADSLService additionalTraffic = ADSLServiceDB.GetADSLServiceById((int)wirelessRequest.AdditionalServiceID);
                ADSLServiceTraffic additionalTrafficTraffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)additionalTraffic.TrafficID);

                userAuthentication.Clear();

                userAuthentication.Add("user_id", ids[0].ToString());
                userAuthentication.Add("deposit", additionalTrafficTraffic.Credit.ToString());
                userAuthentication.Add("is_absolute_change", false);
                userAuthentication.Add("deposit_type", "recharge");
                userAuthentication.Add("deposit_comment", "Change by Pendar_CRM Sell ADSL Request");

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                try
                {
                    ibsngService.changeDeposit(userAuthentication);
                    Update_FreeCounterWireless(userName, (int)additionalTraffic.OverNightCredit);
                }
                catch (Exception ex)
                {
                    throw new Exception("خطا در ارتباط با IBSNG");
                }
            }

            wirelessRequest.UserName = userName;
            wirelessRequest.OrginalPassword = passWord;
            wirelessRequest.Detach();
            DB.Save(wirelessRequest);
        }

        private void SaveOtherRequestCenterSpecialWire()
        {
            int startStatus = DB.GetStatus((int)DB.RequestType.SpecialWireOtherPoint, (int)DB.RequestStatusType.Start).ID;
            List<SpecialWirePoint> specialWirePointList = Data.SpecialWirePointsDB.GetSpecialWirePointByRequestID(_Request.ID).ToList();

            //سیم خصوصی نقطه مبدا : آیتمی است که مرکز آن با مرکز ثبت درخواست اولیه یکسان باشد
            //نقطه مبدا sourceSpecialWire = _SpecialWireUserControl._specialWirePoints.Where(t => t.CenterID == (int)CenterComboBox.SelectedValue).Take(1).SingleOrDefault();
            //در نتیجه
            //_specialWire.InstallAddressID = (long)sourceSpecialWire.InstallAddressID;

            // کلیه ی نقاط ثبت شده در فرم درخواست سیم خصوصی به غیر از نقطه ی مبدا را برمیگردانیم تا به ازای هر کدام یک درخواست سیم خصوصی نقاط دیگر را ثبت کنیم تا در مرکز مربوطه به آن رسیدگی شود
            specialWirePointList = specialWirePointList.Where(t => t.InstallAddressID != _specialWire.InstallAddressID).ToList();
            specialWirePointList.ForEach(item =>
            {

                Request requestPoint = new Request();
                requestPoint.ID = DB.GenerateRequestID();

                SpecialCondition _specialCondition = null;
                // _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID); all request is new00


                if (item.CenterID == _Request.CenterID)
                    requestPoint.StatusID = WorkFlowDB.GetNextStatesID(DB.Action.Confirm, startStatus);
                else
                    requestPoint.StatusID = startStatus;

                requestPoint.CenterID = item.CenterID;
                requestPoint.InsertDate = _Request.InsertDate;
                requestPoint.RequestDate = _Request.RequestDate;
                requestPoint.MainRequestID = _Request.ID;
                requestPoint.CustomerID = _Request.CustomerID;
                requestPoint.IsCancelation = false;
                requestPoint.IsVisible = true;
                requestPoint.IsViewed = false;
                requestPoint.IsWaitingList = false;
                requestPoint.RequestTypeID = (int)DB.RequestType.SpecialWireOtherPoint;
                requestPoint.TelephoneNo = _Request.TelephoneNo;
                requestPoint.Detach();
                DB.Save(requestPoint, true);

                SpecialWire specialWirePoint = new SpecialWire();
                specialWirePoint.RequestID = requestPoint.ID;
                specialWirePoint.NearestTelephone = item.NearestTelephoneNo;
                specialWirePoint.InstallAddressID = item.InstallAddressID;
                specialWirePoint.CorrespondenceAddressID = _specialWire.CorrespondenceAddressID;
                specialWirePoint.BuchtType = _specialWire.BuchtType;
                specialWirePoint.CustomerTypeID = _specialWire.CustomerTypeID;
                specialWirePoint.CustomerGroupID = _specialWire.CustomerGroupID;
                specialWirePoint.CompanyCode = _specialWire.CompanyCode;
                specialWirePoint.TelephoneNo = _specialWire.TelephoneNo;
                specialWirePoint.SpecialWireType = item.SpecialWireType;

                specialWirePoint.Detach();
                DB.Save(specialWirePoint, true);


                if (item.SpecialWireType == (int)DB.SpecialWireType.Middle)
                {

                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = requestPoint.ID;
                        _specialCondition.MiddlePointSpecialWire = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = requestPoint.ID;
                        _specialCondition.MiddlePointSpecialWire = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }
                else
                {

                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = requestPoint.ID;
                        _specialCondition.MiddlePointSpecialWire = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = requestPoint.ID;
                        _specialCondition.MiddlePointSpecialWire = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }


            }
                                        );
        }

        private void CustomerToCalculateDebt()
        {
            if (RequestType.ID == (int)DB.RequestType.Dayri)
            {
                long totalDept = 0;

                List<Telephone> teleList = Data.TelephoneDB.GetTelephoneByCustomerID(_Request.CustomerID ?? 0);

                List<CRM.Data.BillingServiceReference.DebtInfo> DebtList = CRM.Application.Codes.ServiceReference.GetDebtInfo(teleList.Select(t => t.TelephoneNo.ToString()).ToList());
                if (DebtList.Count != 0)
                    totalDept = DebtList.Sum(t => t.DebtAmount);

                SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_Request.ID);

                if (totalDept > 0)
                {

                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _Request.ID;
                        specialCondition.IsDebt = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _Request.ID;
                        specialCondition.IsDebt = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }
                }
                else
                {
                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _Request.ID;
                        specialCondition.IsDebt = false;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _Request.ID;
                        specialCondition.IsDebt = false;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }

                }
            }
        }

        public bool PreForward()
        {
            Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
            if (Status != null && (Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter))
            {
                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.ChangeLocationCenterInside:
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        {
                            if (_changeLocation == null && _changeLocation.TargetCenter == null) { throw new Exception("مرکز مقصد یافت نشد"); }
                            _Request.CenterID = (int)_changeLocation.TargetCenter;
                            _Request.Detach();
                            DB.Save(_Request);
                        }
                        break;
                }
                return true;
            }
            if (Data.StatusDB.IsFinalStep(this.currentStat))
            {
                return true;
            }

            return false;
        }

        public void Forward_ADSL()
        {
            Save_ADSLRequest();

            ADSLRequest aDSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);

            if (RequestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
            {
                string mobileNos = "";
                string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ADSLRegister));
                string customerFullName = CustomerDB.GetFullNameByCustomerID(aDSLRequest.CustomerOwnerID);

                Data.ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);

                bool isNew = false;
                if (aDSL == null)
                {
                    aDSL = new Data.ADSL();
                    isNew = true;
                }

                aDSL.TelephoneNo = (long)_Request.TelephoneNo;
                aDSL.CustomerOwnerID = (long)aDSLRequest.CustomerOwnerID;
                aDSL.CustomerOwnerStatus = aDSLRequest.CustomerOwnerStatus;
                aDSL.CustomerTypeID = aDSLRequest.CustomerTypeID;
                aDSL.CustomerGroupID = aDSLRequest.CustomerGroupID;
                aDSL.UserName = _Request.TelephoneNo.ToString();
                aDSL.OrginalPassword = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9);
                aDSL.HashPassword = GenerateMD5HashPassword(aDSL.OrginalPassword);
                aDSL.InstallDate = null;
                aDSL.ExpDate = null;
                aDSL.Status = (byte)DB.ADSLStatus.Pending;
                aDSL.WasPCM = _ADSL._WasPCM;

                aDSL.Detach();
                Save(aDSL, isNew);

                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID);
                //else
                //    throw new Exception("شماره تلفن همراه مشترک موجود نمی باشد");

                if (!string.IsNullOrWhiteSpace(mobileNos))
                {
                    SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ADSLRegister));

                    if (SmsService.IsActive == true)
                    {
                        message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TeleohoneNo", _Request.TelephoneNo.ToString()).Replace("UserName", aDSL.UserName).Replace("Password", aDSL.OrginalPassword).Replace("Enter", Environment.NewLine);

                        CRMWebServiceDB.SendMessage(mobileNos, message);
                    }
                }

                IsForwardSuccess = true;
            }

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                if (_ADSL.ServiceComboBox.SelectedValue != null)
                    aDSLRequest.ServiceID = (int)_ADSL.ServiceComboBox.SelectedValue;
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                    if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                    {
                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                        throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                    }
                List<int> statuses = WorkFlowDB.GetListNextStatesID(DB.Action.Confirm, _Request.StatusID);
                if (statuses.Count == 1)
                {
                    int nextStatusID = StatusDB.GetStatueByStatusID(statuses[0]).ID;
                    switch (nextStatusID)
                    {
                        case 198:

                            int currentMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo((long)_Request.TelephoneNo, _Request.CenterID);

                            List<Data.ADSLPort> portFreeList = ADSLMDFDB.GetFreeADSLPortByCenterID(_Request.CenterID, currentMDFID);

                            if (portFreeList.Count != 0)
                            {
                                Data.ADSLPort port = portFreeList[0];

                                port.Status = (byte)DB.ADSLPortStatus.reserve;
                                port.TelephoneNo = _Request.TelephoneNo;
                                port.Detach();
                                DB.Save(port);

                                aDSLRequest.ADSLPortID = port.ID;
                                aDSLRequest.Detach();
                                DB.Save(aDSLRequest);
                            }
                            else
                                throw new Exception("در این مرکز تجهیزات فنی وجود ندارد");

                            break;

                        case 196:
                            break;

                        default:
                            break;
                    }
                }

                aDSLRequest.LicenseLetterNo = _ADSL.LicenceLetterNoTextBox.Text;

                //if (_ADSL.CustomerPriorityComboBox.SelectedValue != null)
                //    aDSLRequest.CustomerPriority = (byte)_ADSL.CustomerPriorityComboBox.SelectedValue;
                //else
                aDSLRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;

                aDSLRequest.RequiredInstalation = _ADSL.RequiredInstalationCheckBox.IsChecked;

                aDSLRequest.NeedModem = _ADSL.NeedModemCheckBox.IsChecked;
                aDSLRequest.Status = false;

                if (_ADSL.NeedModemCheckBox.IsChecked != null)
                {
                    if ((bool)_ADSL.NeedModemCheckBox.IsChecked)
                        if (_ADSL.ModemTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                        {
                            aDSLRequest.ModemID = (int)_ADSL.ModemTypeComboBox.SelectedValue;
                            if (aDSLRequest.RequiredInstalation == null || (bool)aDSLRequest.RequiredInstalation == false)
                            {
                                if (_ADSL.ModemSerilaNoComboBox.SelectedValue == null)
                                    throw new Exception("لطفا شماره سریال مودم را وارد نمایید");
                                if (string.IsNullOrEmpty(_ADSL.ModemMACAddressTextBox.Text))
                                    throw new Exception("لطفا آدرس فیزیکی مودم را وارد نمایید");
                            }
                        }

                    if (aDSLRequest.ModemSerialNoID != null)
                    {
                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)aDSLRequest.ModemSerialNoID);
                        modem.TelephoneNo = _Request.TelephoneNo;
                        modem.Status = (byte)DB.ADSLModemStatus.Sold;

                        modem.Detach();
                        Save(modem);
                    }
                }

                _Request.SellerID = null;
                _Request.Detach();
                DB.Save(_Request);

                string mobileNos = "";

                string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.SaleADSL));

                string customerFullName = CustomerDB.GetFullNameByCustomerID(aDSLRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID);
                //else
                //    throw new Exception("شماره تلفن همراه مشترک موجود نمی باشد");

                if (!string.IsNullOrWhiteSpace(mobileNos))
                {
                    SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.SaleADSL));

                    if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
                    {
                        message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);

                        CRMWebServiceDB.SendMessage(mobileNos, message);
                    }
                }

                IsForwardSuccess = true;
            }
        }

        public void Forward_ADSLInstall()
        {
            Save_ADSLInstallRequest();

            _Request.SellerID = null;
            _Request.Detach();
            DB.Save(_Request);

            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

            IsForwardSuccess = true;
        }

        public void Forward_ADSLChangePlace()
        {
            Save_ADSLChangePlaceRequest();

            _Request.SellerID = null;
            _Request.Detach();
            DB.Save(_Request);

            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                {
                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                }

            IsForwardSuccess = true;
        }

        public void Forward_ADSLChangePort()
        {
            Save_ADSLChangePortLRequest();

            Data.ADSLChangePort1 aDSLChangePort = ADSLChangePortDB.GetADSLChangePortByID(RequestID);

            int currentMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo((long)_Request.TelephoneNo, _Request.CenterID);

            List<Data.ADSLPort> portFreeList = ADSLMDFDB.GetFreeADSLPortByCenterID(_Request.CenterID, currentMDFID);

            if (portFreeList.Count != 0)
            {
                Data.ADSLPort port = portFreeList[0];

                port.Status = (byte)DB.ADSLPortStatus.reserve;
                port.TelephoneNo = _Request.TelephoneNo;
                port.Detach();
                Save(port);

                aDSLChangePort.NewPortID = port.ID;
                aDSLChangePort.Detach();
                Save(aDSLChangePort);
            }
            else
                throw new Exception("در این مرکز تجهیزات فنی وجود ندارد");

            _Request.SellerID = null;
            _Request.Detach();
            DB.Save(_Request);

            IsForwardSuccess = true;
        }

        public void Forward_Failure117()
        {
            Data.Failure117 failure = new Data.Failure117();
            Service1 service = new Service1();

            if (_Failure117.ActionStatusComboBox.SelectedValue == null)
                throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");
            else
            {
                if ((byte)_Failure117.ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                    throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                if (_Failure117.LineStatusComboBox.SelectedValue != null)
                    failure.LineStatusID = (int)_Failure117.LineStatusComboBox.SelectedValue;
                else
                    throw new Exception("لطفا وضعیت خط را تعیین نمایید");

                if (_Failure117.ActionStatusComboBox.SelectedValue != null)
                    failure.ActionStatusID = (byte)_Failure117.ActionStatusComboBox.SelectedValue;
                else
                    failure.ActionStatusID = null;

                if (_Failure117.FailureStatusComboBox.SelectedValue != null)
                    failure.FailureStatusID = (int)_Failure117.FailureStatusComboBox.SelectedValue;
                else
                    failure.FailureStatusID = null;


                failure.MDFUserID = DB.CurrentUser.ID;
                failure.MDFDate = DB.GetServerDate();
                failure.MDFCommnet = _Failure117.CommentTextBox.Text;

                _Request.IsViewed = false;
                _Request.TelephoneNo = _TelephoneNo;
                _Request.RequestTypeID = (byte)DB.RequestType.Failure117;

                if (city == "semnan")
                {
                    if (!string.IsNullOrEmpty(_Failure117.HearingTelephoneNoTextBox.Text))
                    {
                        if (!service.Is_Phone_Exist(_Failure117.HearingTelephoneNoTextBox.Text))
                            throw new Exception("* شماره تلفن همشنوایی وارد شده موجود نمی باشد !");
                        else
                            failure.HearingTelephoneNo = Convert.ToInt64(_Failure117.HearingTelephoneNoTextBox.Text);
                    }
                    else
                        failure.HearingTelephoneNo = null;

                    if (!string.IsNullOrEmpty(_Failure117.AdjacentTelephoneTextBox.Text))
                    {
                        if (!service.Is_Phone_Exist(_Failure117.AdjacentTelephoneTextBox.Text))
                            throw new Exception("* شماره تلفن همجوار وارد شده موجود نمی باشد !");
                        failure.AdjacentTelephoneNo = Convert.ToInt64(_Failure117.AdjacentTelephoneTextBox.Text);
                    }
                    else
                        failure.AdjacentTelephoneNo = null;

                    Service1 service2 = new Service1();
                    System.Data.DataTable telephoneInfo = service2.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                    failure.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                    failure.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                    failure.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                    failure.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();

                    failure.MDFPesonnelID = (int)_Failure117.MDFPersonnelComboBox.SelectedValue;

                    _Request.CenterID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]));
                }

                if (city == "kermanshah")
                {
                    if (!string.IsNullOrEmpty(_Failure117.HearingTelephoneNoTextBox.Text))
                    {
                        if (!TelephoneDB.HasTelephoneNo(Convert.ToInt64(_Failure117.HearingTelephoneNoTextBox.Text)))
                            throw new Exception("* شماره تلفن همشنوایی وارد شده موجود نمی باشد !");
                        else
                            failure.HearingTelephoneNo = Convert.ToInt64(_Failure117.HearingTelephoneNoTextBox.Text);
                    }
                    else
                        failure.HearingTelephoneNo = null;

                    if (!string.IsNullOrEmpty(_Failure117.AdjacentTelephoneTextBox.Text))
                    {
                        if (!TelephoneDB.HasTelephoneNo(Convert.ToInt64(_Failure117.AdjacentTelephoneTextBox.Text)))
                            throw new Exception("* شماره تلفن همجوار وارد شده موجود نمی باشد !");
                        failure.AdjacentTelephoneNo = Convert.ToInt64(_Failure117.AdjacentTelephoneTextBox.Text);
                    }
                    else
                        failure.AdjacentTelephoneNo = null;

                    TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo(_TelephoneNo);

                    failure.CabinetNo = technicalInfo.CabinetNo;
                    failure.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                    failure.PostNo = technicalInfo.PostNo;
                    failure.PostEtesali = technicalInfo.ConnectionNo;

                    failure.MDFPesonnelID = DB.CurrentUser.ID; // (int)_Failure117.MDFPersonnelComboBox.SelectedValue;

                    _Request.CenterID = CenterDB.GetCenterIDbyTelephoneNo(_TelephoneNo);
                }

                //_Request.CustomerID=
                // _Request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                _Request.RequestDate = DB.GetServerDate();
                _Request.InsertDate = DB.GetServerDate();
                _Request.CreatorUserID = DB.CurrentUser.ID;
                _Request.ModifyUserID = DB.CurrentUser.ID;
                _Request.IsWaitingList = false;
                _Request.IsCancelation = false;
                _Request.IsVisible = true;
                Data.Status status = DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;

                if (RequestID == 0)
                    RequestForFailure117.SaveFailureRequest(_Request, failure, true);
                else
                    RequestForFailure117.SaveFailureRequest(_Request, failure, false);

                switch ((byte)_Failure117.ActionStatusComboBox.SelectedValue)
                {
                    case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                        _Request.StatusID = 1364;
                        FailureForm form = new FailureForm();
                        form.FailureRequestID = _Request.ID;
                        form.RowNo = Failure117DB.GetFormRowNo(_Request.CenterID);
                        form.CableColor1 = null;
                        form.CableColor2 = null;
                        form.CableTypeID = 0;
                        form.FormInsertDate = DB.GetServerDate();

                        RequestForFailure117.SaveFailureForm(form, true);

                        break;

                    //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                    //    _Request.StatusID = 1365;
                    //    break;

                    case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                        _Request.StatusID = 1366;
                        break;

                    case (byte)DB.Failure117ActionStatus.RemovalFailure:
                        _Request.StatusID = 1367;
                        break;

                    default:
                        break;
                }

                _Request.Detach();
                Save(_Request, false);
            }

            this.DialogResult = true;

            IsForwardSuccess = false;
        }

        public override bool Refund()
        {
            try
            {
                switch (RequestType.ID)
                {
                    case (byte)DB.RequestType.ADSL:
                        Refund_ADSL();
                        break;

                    case (byte)DB.RequestType.Wireless:
                        Wireless_ADSL();
                        break;
                    case (byte)DB.RequestType.ADSLChangeIP:
                        Refund_ADSLChangeIP();
                        break;

                    default:
                        break;
                }

                IsRefundSuccess = true;
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در استرداد ودیعه، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در استرداد ودیعه، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                IsRefundSuccess = false;
                ShowErrorMessage("خطا در استرداد ودیعه ، " + ex.Message + " !", ex);
            }

            return IsRefundSuccess;
        }

        private void Wireless_ADSL()
        {

        }

        public void Refund_ADSL()
        {

        }

        public void Refund_ADSLChangeIP()
        {

        }

        public override bool Cancel()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    RequestID = _Request.ID;

                    List<RequestPayment> paymentList = RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstalmentbyRequestID(RequestID);
                    if (instalments != null)
                        if (instalments.Count != 0)
                            DB.DeleteAll<InstallmentRequestPayment>(instalments.Select(t => t.ID).ToList());

                    //if (paymentList != null)
                    //    if (paymentList.Count != 0)
                    //        foreach (RequestPayment currentPayment in paymentList)
                    //        {
                    //            if (currentPayment.IsPaid == true)
                    //                throw new Exception("پرداخت نقدی صورت گرفته است !");

                    //            if (currentPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                    //            {
                    //                instalments = InstalmentRequestPaymentDB.GetInstalmentRequestPaymentbyPaymentID(currentPayment.ID);
                    //                DB.DeleteAll<InstallmentRequestPayment>(instalments.Select(t => t.ID).ToList());
                    //            }
                    //        }

                    _Request.IsCancelation = true;

                    Data.CancelationRequestList cancelationRequest = new CancelationRequestList();

                    cancelationRequest.ID = RequestID;
                    cancelationRequest.EntryDate = DB.GetServerDate();
                    cancelationRequest.UserID = Folder.User.Current.ID;

                    cancelationRequest.Detach();
                    DB.Save(cancelationRequest, true);

                    switch (RequestType.ID)
                    {
                        case (int)DB.RequestType.ADSL:
                            Cancel_ADSL();
                            break;
                        case (int)DB.RequestType.Dischargin:
                            Cancel_DischarginTelephoneRequest();
                            break;

                        case (int)DB.RequestType.VacateE1:
                            Cancel_VacateE1Request();
                            break;

                        case (int)DB.RequestType.SpecialWire:
                            Cancel_specialWireRequest();
                            break;

                        case (int)DB.RequestType.VacateSpecialWire:
                            Cancel_VacateSpecialWireRequest();
                            break;

                        case (int)DB.RequestType.ChangeLocationSpecialWire:
                            Cancel_ChangeLocationSpecialWireRequest();
                            break;

                        case (int)DB.RequestType.CutAndEstablish:
                            Cancel_CutAndEstablish();
                            break;
                        case (int)DB.RequestType.OpenAndCloseZero:
                            Cancel_OpenAndCloseZero();
                            break;

                        default:
                            break;
                    }

                    _Request.Detach();
                    DB.Save(_Request);
                    ts.Complete();
                    IsCancelSuccess = true;
                }
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در ابطال درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در ابطال درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ابطال درخواست، " + ex.Message, ex);
                IsCancelSuccess = false;
            }

            return IsCancelSuccess;
        }

        private void Cancel_OpenAndCloseZero()
        {
        }

        private void Cancel_CutAndEstablish()
        {
        }

        private void Cancel_ADSL()
        {
            ADSLRequest _aDSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);

            if (_aDSLRequest.HasIP != null)
            {
                if ((bool)_aDSLRequest.HasIP)
                {
                    if (_aDSLRequest.IPStaticID != null)
                    {
                        ADSLIP iP = ADSLIPDB.GetADSLIPById((long)_aDSLRequest.IPStaticID);

                        iP.TelephoneNo = null;
                        iP.Status = (byte)DB.ADSLIPStatus.Free;
                        iP.InstallDate = null;
                        iP.ExpDate = null;

                        iP.Detach();
                        Save(iP, false);
                    }

                    if (_aDSLRequest.GroupIPStaticID != null)
                    {
                        ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)_aDSLRequest.GroupIPStaticID);

                        groupIP.TelephoneNo = null;
                        groupIP.Status = (byte)DB.ADSLIPStatus.Free;
                        groupIP.InstallDate = null;
                        groupIP.ExpDate = null;

                        groupIP.Detach();
                        Save(groupIP, false);
                    }
                }

                if (_aDSLRequest.NeedModem != null)
                {
                    if ((bool)_aDSLRequest.NeedModem)
                    {
                        if (_aDSLRequest.ModemSerialNoID != null)
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_aDSLRequest.ModemSerialNoID);

                            modem.TelephoneNo = null;
                            modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                            modem.Detach();
                            Save(modem, false);
                        }
                    }
                }
            }
        }

        private void Cancel_ChangeLocationSpecialWireRequest()
        {
            long telephoneNo = 0;
            if (!long.TryParse(_ChangeLocationSpecialWireUserControl.TelephoneNoTextBox.Text.Trim(), out telephoneNo))
                throw new Exception("تلفن یافت نشد");

            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(telephoneNo);
            if (telephone != null && telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {
                telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                telephone.Detach();
                DB.Save(telephone);
            }
        }

        private void Cancel_VacateSpecialWireRequest()
        {
            long telephoneNo = 0;
            if (!long.TryParse(_VacateSpecialWireUserControl.TelephoneNoTextBox.Text.Trim(), out telephoneNo))
                throw new Exception("تلفن یافت نشد");

            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(telephoneNo);
            if (telephone != null && telephone.Status != (byte)DB.TelephoneStatus.Reserv)
            {
                telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                telephone.Detach();
                DB.Save(telephone);
            }
        }

        private void Cancel_specialWireRequest()
        {
            _specialWire = _SpecialWireUserControl.DataContext as CRM.Data.SpecialWire;
            if (_SpecialWireUserControl.oldTelephone != 0)
            {
                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelePhoneNo(_SpecialWireUserControl.oldTelephone);
                telephone.CustomerID = null;
                telephone.InstallAddressID = null;
                telephone.CorrespondenceAddressID = null;
                telephone.Status = (byte)DB.TelephoneStatus.Free;
                telephone.Detach();
                DB.Save(telephone);
            }
        }

        private void Cancel_VacateE1Request()
        {
            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(Telephone.TelephoneNo);
            if (telephone != null && telephone.Status != (byte)DB.TelephoneStatus.Free)
            {
                telephone.Status = (byte)DB.TelephoneStatus.Free;
                telephone.Detach();
                DB.Save(telephone);
            }
        }

        private void Cancel_DischarginTelephoneRequest()
        {
            Telephone PastTelephont = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)Telephone.TelephoneNo);
            if (PastTelephont != null && PastTelephont.Status != (byte)DB.TelephoneStatus.Connecting)
            {
                PastTelephont.Status = (byte)DB.TelephoneStatus.Connecting;
                PastTelephont.Detach();
                DB.Save(PastTelephont);
            }
        }

        private void Cancel_InstallRequest()
        {

        }

        public override bool Confirm()
        {
            try
            {
                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.Dayri:
                        break;

                    case (int)DB.RequestType.CutAndEstablish:
                        Confirm_CutAndEstablishRequest();
                        break;

                    case (int)DB.RequestType.SpecialService:
                        Confirm_SpecialServiceRequest();
                        break;

                    case (int)DB.RequestType.OpenAndCloseZero:
                        Confirm_OpenAndCloseZeroRequest();
                        break;

                    case (int)DB.RequestType.ADSL:
                        Confirm_ADSLRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeIP:
                        Confirm_ADSLChangeIPRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        Confirm_ADSLChangeServiceRequest();
                        break;

                    case (int)DB.RequestType.ADSLSellTraffic:
                        Confirm_ADSLSellTrafficRequest();
                        break;

                    case (int)DB.RequestType.WirelessSellTraffic:
                        Confirm_WirelessSellTrafficRequest();
                        break;

                    case (int)DB.RequestType.WirelessChangeService:
                        Confirm_WirelessChangeServiceRequest();
                        break;

                    case (int)DB.RequestType.ADSLCutTemporary:
                        Confirm_ADSLCutRequest();
                        break;

                    case (int)DB.RequestType.TitleIn118:
                        Confirm_TitleIn118Request();
                        break;

                    case (int)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                        Confirm_ADSLChangeCustomerOwnerCharacteristics();
                        break;

                    default:
                        break;
                }

                IsConfirmSuccess = true;
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در تایید درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در تایید درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در تایید درخواست ، " + ex.Message + " !", ex);
            }

            return IsConfirmSuccess;
        }

        public override bool ConfirmEnd()
        {
            try
            {
                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.Failure117:
                        ConfirmEnd_Failure117();
                        break;
                }
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در تایید درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در تایید درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در تایید درخواست", ex);
            }

            return IsConfirmEndSuccess;
        }

        public void ConfirmEnd_Failure117()
        {
            try
            {
                Data.Failure117 failure = new Data.Failure117();

                if (_Failure117.ActionStatusComboBox.SelectedValue == null)
                    throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");

                if ((byte)_Failure117.ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.RemovalFailure)
                    throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                if (_Failure117.LineStatusComboBox.SelectedValue != null)
                    failure.LineStatusID = (int)_Failure117.LineStatusComboBox.SelectedValue;
                else
                    throw new Exception("لطفا وضعیت خط را تعیین نمایید");

                if (_Failure117.FailureStatusComboBox.SelectedValue != null)
                    failure.FailureStatusID = (int)_Failure117.FailureStatusComboBox.SelectedValue;

                failure.ActionStatusID = (byte)DB.Failure117ActionStatus.RemovalFailure;

                if (_Failure117.MDFPersonnelComboBox.SelectedValue != null)
                    failure.MDFPesonnelID = (int)_Failure117.MDFPersonnelComboBox.SelectedValue;
                else
                    throw new Exception("لطفا نام مامور بررسی را وارد نمایید");

                Service1 service2 = new Service1();
                System.Data.DataTable telephoneInfo = service2.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                failure.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                failure.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                failure.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                failure.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();

                failure.MDFUserID = DB.CurrentUser.ID;
                failure.MDFDate = DB.GetServerDate();
                failure.EndMDFUserID = DB.CurrentUser.ID;
                failure.EndMDFDate = DB.GetServerDate();

                if (_Failure117.ResultListBox.SelectedValue == null)
                    throw new Exception("لطفا نتیجه خرابی را انتخاب نمایید");
                else
                    failure.ResultAfterReturn = (byte)Convert.ToInt16(_Failure117.ResultListBox.SelectedValue);

                _Request.TelephoneNo = _Failure117.TelephoneNo;
                _Request.RequestTypeID = RequestType.ID;
                _Request.CreatorUserID = DB.CurrentUser.ID;
                if (CenterComboBox.SelectedValue != null)
                    _Request.CenterID = (int)CenterComboBox.SelectedValue;
                else
                    throw new Exception("لطفا نام مرکز را انتحاب نمایید!");

                _Request.StatusID = 1368;
                _Request.PreviousAction = (byte)DB.Action.Confirm;
                _Request.ModifyDate = DB.GetServerDate();
                _Request.ModifyUserID = DB.CurrentUser.ID;
                _Request.IsViewed = true;

                if (failure.FailureStatusID != null)
                {
                    Failure117FailureStatus failureStatus = DB.SearchByPropertyName<Failure117FailureStatus>("ID", failure.FailureStatusID).SingleOrDefault();
                    _Request.EndDate = DB.GetServerDate().AddHours((int)failureStatus.ArchivedTime);
                }
                else
                {
                    _Request.EndDate = DB.GetServerDate().AddHours(24);
                }

                if (failure.HelpDeskTicketID != null)
                {
                    if (string.IsNullOrEmpty(_Failure117.CommentTextBox.Text))
                        throw new Exception("لطفا توضیحات لازم را وارد نمایید");
                    else
                    {
                        bool inquiryResult = false;
                        bool result2 = true;

                        failure.MDFCommnet = _Failure117.CommentTextBox.Text;
                        HelpDeskService.HelpDeskService helpDeskServise = new HelpDeskService.HelpDeskService();
                        helpDeskServise.ReplyHelpDeskMDFInquiry((long)_Request.TelephoneNo, true, (long)failure.HelpDeskTicketID, true, _Failure117.CommentTextBox.Text, RequestID, true, out inquiryResult, out result2);
                        if (!inquiryResult)
                            throw new Exception("اختلالی در عمل تایید ایجاد شده است، لطفا مجددا سعی نمایید");

                    }
                }

                RequestForFailure117.SaveFailureRequest(_Request, failure, true);

                IsConfirmEndSuccess = false;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در تایید نهایی، " + ex.Message + "!", ex);
            }
        }

        private void Confirm_CutAndEstablishRequest()
        {
            //CRM.Data.CutAndEstablish cutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_Request.ID);

            ////CRM.Data.Schema.CutAndEstablish cutAndEstablishLog = new Data.Schema.CutAndEstablish();
            ////RequestLog _RequestLog = new RequestLog();
            ////_RequestLog.RequestID = _Request.ID;

            //switch (cutAndEstablish.Status)
            //{
            //    case (byte)DB.CutAndEstablishStatus.Cut:
            //        //_RequestLog.RequestTypeID = _Request.RequestTypeID;
            //        //_RequestLog.TelephoneNo = _Request.TelephoneNo;
            //        //cutAndEstablishLog.Status = (byte)DB.CutAndEstablishStatus.Cut;
            //        //_RequestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.CutAndEstablish>(cutAndEstablishLog, true));
            //        break;

            //    case (byte)DB.CutAndEstablishStatus.Establish:
            //        //_RequestLog.RequestTypeID = _Request.RequestTypeID;
            //        //_RequestLog.TelephoneNo = _Request.TelephoneNo;
            //        //cutAndEstablishLog.Status = (byte)DB.CutAndEstablishStatus.Establish;
            //        //_RequestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.CutAndEstablish>(cutAndEstablishLog, true));
            //        break;

            //    default:
            //        break;
            //}

            //RequestForCutAndEstablishDB.SaveRequest(_Request, cutAndEstablish, /*_RequestLog*/ null, null, false);
        }

        private void Confirm_SpecialServiceRequest()
        {
            //RequestLog requestLog = new RequestLog();
            //List<RequestLog> requestLogList = new List<RequestLog>();
            //List<Data.SpecialService> specialServiceList = SpecialServiceDB.GetSpecialServiceByRequetID(_Request.ID);
            //CRM.Data.Schema.SpecialService specialServiceLog = new Data.Schema.SpecialService();

            //foreach (Data.SpecialService item in specialServiceList)
            //{
            //    requestLog = new RequestLog();

            //    requestLog.RequestID = _Request.ID;
            //    requestLog.RequestTypeID = _Request.RequestTypeID;
            //    requestLog.TelephoneNo = _Request.TelephoneNo;

            //    specialServiceLog = new Data.Schema.SpecialService();
            //    specialServiceLog.SpecialServiceTypeID = item.SpecialServiceTypeID;
            //    specialServiceLog.Status = item.Status;
            //    requestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.SpecialService>(specialServiceLog, true));

            //    requestLogList.Add(requestLog);
            //}

            //RequestForSpecialServiceDB.SaveRequest(_Request, specialServiceList, requestLogList, Telephone, false);
        }

        private void Confirm_OpenAndCloseZeroRequest()
        {

            //   ZeroStatus zeroStatus =  Data.ZeroStatusDB.GetZeroStatusByID( RequestID);

            //   RequestDetail.IsEnabled = false;


            //   RequestLog requestLog = new RequestLog();
            //   requestLog.RequestID = _Request.ID;
            //   requestLog.RequestTypeID = _Request.RequestTypeID;
            //  // requestLog.TelephoneNo = zeroStatus.TelephoneNo;

            //   CRM.Data.Schema.OpenAndCloseZero openAndCloseZero = new Data.Schema.OpenAndCloseZero();
            ////   openAndCloseZero.ZeroStatus = zeroStatus.ZeroTypeID;
            //   openAndCloseZero.Status = zeroStatus.Status;
            //   requestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.OpenAndCloseZero>(openAndCloseZero, true));


            //   RequestForzeroStatusDB.SaveRequest(_Request, null, requestLog, null, false);
        }

        private void Confirm_ADSLRequest()
        {
            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                CRM.Data.ADSLRequest ADSLRequest = DB.SearchByPropertyName<Data.ADSLRequest>("ID", RequestID).SingleOrDefault();
                ADSLRequest.CommentCustomers = _ADSL.CommentCustomersTextBox.Text;

                ADSLRequest.Detach();
                DB.Save(ADSLRequest);
            }
        }

        private void Confirm_ADSLChangeIPRequest()
        {
            Save_ADSLChangeIPRequest();

            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

            ADSLChangeIPRequest aDSLChangeIP = ADSLChangeIPRequestDB.GetADSLChangeIPRequestByID(RequestID);
            Data.ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            aDSLChangeIP.SaleUserID = DB.CurrentUser.ID;
            aDSLChangeIP.SaleDate = DB.GetServerDate();

            //if (aDSL.HasIP != true)
            //{
            //    if (aDSLChangeIP.NewIPStaticID != null)
            //    {

            //    }
            //    if (aDSLChangeIP.NewGroupIPStaticID != null)
            //    { }

            //}

            aDSL.HasIP = true;

            if (aDSL.IPStaticID != aDSLChangeIP.NewIPStaticID)
            {
                if (aDSL.IPStaticID != null)
                {
                    ADSLIP iP = ADSLIPDB.GetADSLIPById((int)aDSL.IPStaticID);

                    iP.TelephoneNo = null;
                    iP.InstallDate = null;
                    iP.ExpDate = null;
                    iP.Status = (byte)DB.ADSLIPStatus.Free;

                    iP.Detach();
                    DB.Save(iP);
                }
            }

            if (aDSL.GroupIPStaticID != aDSLChangeIP.NewGroupIPStaticID)
            {
                if (aDSL.GroupIPStaticID != null)
                {
                    ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((int)aDSL.GroupIPStaticID);

                    groupIP.TelephoneNo = null;
                    groupIP.InstallDate = null;
                    groupIP.ExpDate = null;
                    groupIP.Status = (byte)DB.ADSLIPStatus.Free;

                    groupIP.Detach();
                    DB.Save(groupIP);
                }
            }

            ADSLIPHistory iPHistory = new ADSLIPHistory();

            if (aDSLChangeIP.NewIPStaticID != null)
            {
                aDSL.IPStaticID = aDSLChangeIP.NewIPStaticID;
                _ADSLChangeIP.IPStatic.TelephoneNo = _Request.TelephoneNo;
                _ADSLChangeIP.IPStatic.Status = (byte)DB.ADSLIPStatus.Instal;
                _ADSLChangeIP.IPStatic.InstallDate = DB.GetServerDate();
                _ADSLChangeIP.IPStatic.ExpDate = DB.GetServerDate().AddMonths((int)aDSLChangeIP.IPTime);

                userInfo.Add("assign_ip", _ADSLChangeIP.IPStatic.IP);

                iPHistory.IPID = aDSLChangeIP.NewIPStaticID;
                iPHistory.TelephoneNo = (long)_Request.TelephoneNo;
                iPHistory.StartDate = DB.GetServerDate();
                iPHistory.EndDate = null;
            }

            if (aDSLChangeIP.NewGroupIPStaticID != null)
            {
                aDSL.GroupIPStaticID = aDSLChangeIP.NewGroupIPStaticID;
                _ADSLChangeIP.GroupIPStatic.TelephoneNo = _Request.TelephoneNo;
                _ADSLChangeIP.GroupIPStatic.Status = (byte)DB.ADSLIPStatus.Instal;
                _ADSLChangeIP.GroupIPStatic.InstallDate = DB.GetServerDate();
                _ADSLChangeIP.GroupIPStatic.ExpDate = DB.GetServerDate().AddMonths((int)_ADSLChangeIP.IPTimeComboBox.SelectedValue);

                userInfo.Add("assign_ip", _ADSLChangeIP.GroupIPStatic.VirtualRange);
                userInfo.Add("assign_route_ip", _ADSLChangeIP.GroupIPStatic.StartRange);

                iPHistory.IPGroupID = aDSLChangeIP.NewGroupIPStaticID;
                iPHistory.TelephoneNo = (long)_Request.TelephoneNo;
                iPHistory.StartDate = DB.GetServerDate();
                iPHistory.EndDate = null;
            }

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", aDSL.UserID);
            userAuthentication.Add("attrs", userInfo);
            userAuthentication.Add("to_del_attrs", "");

            ibsngService.UpdateUserAttrs(userAuthentication);

            iPHistory.Detach();
            DB.Save(iPHistory);

            RequestForADSL.SaveADSLChangeIPRequest(_Request, aDSLChangeIP, _ADSLChangeIP.IPStatic, _ADSLChangeIP.GroupIPStatic, aDSL, null, false);
        }

        private void Confirm_ADSLChangeServiceRequest()
        {
            Save_ADSLChangeServiceRequest();
            LoadData();

            CRM.Data.ADSLChangeService aDSLChangeService = ADSLChangeTariffDB.GetADSLChangeServicebyID(RequestID);
            Data.ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);

            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                {
                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                }

            if (_ADSLChangeService.NeedModemCheckBox.IsChecked != null)
            {
                if ((bool)_ADSLChangeService.NeedModemCheckBox.IsChecked)
                    if (_ADSLChangeService.ModemTypeComboBox.SelectedValue == null)
                        throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                    else
                    {
                        aDSLChangeService.ModemID = (int)_ADSLChangeService.ModemTypeComboBox.SelectedValue;

                        if (_ADSLChangeService.ModemSerilaNoComboBox.SelectedValue == null)
                            throw new Exception("لطفا شماره سریال مودم را وارد نمایید");
                        else
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_ADSLChangeService.ModemSerilaNoComboBox.SelectedValue);
                            modem.TelephoneNo = _Request.TelephoneNo;
                            modem.Status = (byte)DB.ADSLModemStatus.Sold;

                            modem.Detach();
                            Save(modem);
                        }
                    }
            }

            ADSLService service = ADSLServiceDB.GetADSLServiceById((int)aDSLChangeService.NewServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID);
            string credit = (((traffic.Credit != null) ? traffic.Credit : 1024) + ((service.ReserveCridit != null) ? service.ReserveCridit : 0)).ToString();

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            switch ((byte)Convert.ToInt16(_ADSLChangeService.ActionTypeComboBox.SelectedValue))
            {
                case (byte)DB.ADSLChangeServiceActionType.ExtensionService:

                    CRMWebService.ChangeIBSngInfo ibsInfo = new CRMWebService.ChangeIBSngInfo();

                    ibsInfo.UserID = aDSL.UserID;
                    ibsInfo.Deposit = credit;
                    ibsInfo.IsAbsoluteChange = false;
                    ibsInfo.DepositType = "renew";
                    ibsInfo.DepositComment = "Change by Pendar_CRM, Extension Service Request (Renew)";

                    try
                    {
                        CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();
                        webService.changeDeposit(ibsInfo);

                        ibsInfo = new CRMWebService.ChangeIBSngInfo();

                        ibsInfo.UserID = aDSL.UserID.ToString();
                        ibsInfo.RenewNextGroup = service.IBSngGroupName;
                        ibsInfo.RenewRemoveUserExpDates = "1";

                        webService.UpdateUserAttrs(ibsInfo);

                        aDSLChangeService.IsIBSngUpdated = true;
                        Update_FreeCounter(_Request.TelephoneNo.ToString(), (int)service.OverNightCredit);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("درخواست مورد نظر در سیستم AAA اعمال نشد، لطفا مجددا تایید نمایید.");
                    }

                    break;

                case (byte)DB.ADSLChangeServiceActionType.ChangeService:

                    ////// Delete Days
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userInfo.Add("exp_date_temp_extend", "-1");
                    userInfo.Add("exp_date_temp_extend_unit", "Days");

                    userAuthentication.Add("user_id", aDSL.UserID);
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ////// Delete Credit
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userAuthentication.Add("credit", "0");
                    userAuthentication.Add("is_absolute_change", true);
                    userAuthentication.Add("credit_comment", "Change by Pendar_CRM, Change Service Request");

                    ibsngService.changeCredit(userAuthentication);

                    ////// Delete Renew Desposit
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userAuthentication.Add("deposit", credit);
                    userAuthentication.Add("is_absolute_change", true);
                    userAuthentication.Add("deposit_type", "renew");
                    userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

                    try
                    {
                        ibsngService.changeDeposit(userAuthentication);
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر اعتبار با موفقیت انجام نشد");
                    }

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userInfo.Add("renew_next_group", service.IBSngGroupName);
                    userInfo.Add("renew_remove_user_exp_dates", "1");
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    try
                    {
                        ibsngService.UpdateUserAttrs(userAuthentication);
                        aDSLChangeService.IsIBSngUpdated = true;
                        Update_FreeCounter(_Request.TelephoneNo.ToString(), (int)service.OverNightCredit);
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر گروه با موفقیت انجام نشد");
                    }
                    break;

                default:
                    break;
            }

            aDSL.TariffID = aDSLChangeService.NewServiceID;
            aDSL.LicenseLetterNo = aDSLChangeService.LicenseLetterNo;
            if (aDSLChangeService.NeedModem == true)
                aDSL.ModemID = aDSLChangeService.ModemSerialNoID;

            aDSL.InstallDate = DB.GetServerDate();
            aDSL.ExpDate = DB.GetServerDate().AddMonths((int)service.DurationID);

            aDSL.Detach();
            DB.Save(aDSL);

            aDSLChangeService.FinalUserID = DB.CurrentUser.ID;
            aDSLChangeService.FinalDate = DB.GetServerDate();
            //aDSLChangeTariff.FinalComment = _ADSLChangeTariff.FinalCommentTextBox.Text;
            aDSLChangeService.Status = true;

            aDSLChangeService.Detach();
            DB.Save(aDSLChangeService);

            ADSLHistory aDSLHistory = new ADSLHistory();
            aDSLHistory.TelephoneNo = _Request.TelephoneNo.ToString();
            aDSLHistory.ServiceID = aDSL.TariffID;
            aDSLHistory.UserID = DB.CurrentUser.ID;
            aDSLHistory.InsertDate = DB.GetServerDate();

            aDSLHistory.Detach();
            DB.Save(aDSLHistory, true);

            CRM.Data.Schema.ADSLChangeTariff ADSLChangeTariffLog = new Data.Schema.ADSLChangeTariff();
            ADSLChangeTariffLog.OldTariffID = (int)aDSLChangeService.OldServiceID;
            ADSLChangeTariffLog.NewTariffID = (int)aDSLChangeService.NewServiceID;

            RequestLog requestLog = new RequestLog();
            requestLog = new RequestLog();
            requestLog.RequestID = _Request.ID;
            requestLog.RequestTypeID = _Request.RequestTypeID;
            requestLog.TelephoneNo = _ADSLChangeService.TeleInfo.TelephoneNo;

            requestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.ADSLChangeTariff>(ADSLChangeTariffLog, true));

            requestLog.Detach();
            DB.Save(requestLog);

            string mobileNos = "";
            string customerFullName = "";

            if (aDSL.CustomerOwnerID != null)
            {
                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID);

                customerFullName = CustomerDB.GetFullNameByCustomerID((long)aDSL.CustomerOwnerID);
            }

            string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
            {
                message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);

                CRMWebServiceDB.SendMessage(mobileNos, message);
            }
        }

        private void Confirm_WirelessChangeServiceRequest()
        {
            Save_WirelessChangeServiceRequest();

            CRM.Data.WirelessChangeService wirelessChangeService = WirelessChangeServiceDB.GetWirelessChangeServicebyID(RequestID);
            Data.ADSL aDSL = ADSLDB.GetWirelessbyCode(wirelessChangeService.WirelessCode);

            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                {
                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                }

            if (_WirelessChangeService.NeedModemCheckBox.IsChecked != null)
            {
                if ((bool)_WirelessChangeService.NeedModemCheckBox.IsChecked)
                    if (_WirelessChangeService.ModemTypeComboBox.SelectedValue == null)
                        throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                    else
                    {
                        wirelessChangeService.ModemID = (int)_WirelessChangeService.ModemTypeComboBox.SelectedValue;

                        if (_WirelessChangeService.ModemSerilaNoComboBox.SelectedValue == null)
                            throw new Exception("لطفا شماره سریال مودم را وارد نمایید");
                        else
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_WirelessChangeService.ModemSerilaNoComboBox.SelectedValue);
                            modem.TelephoneNo = _Request.TelephoneNo;
                            modem.Status = (byte)DB.ADSLModemStatus.Sold;

                            modem.Detach();
                            Save(modem);
                        }
                    }
            }

            ADSLService service = ADSLServiceDB.GetADSLServiceById((int)wirelessChangeService.NewServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID);
            string credit = (((traffic.Credit != null) ? traffic.Credit : 1024) + ((service.ReserveCridit != null) ? service.ReserveCridit : 0)).ToString();

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            switch ((byte)Convert.ToInt16(_WirelessChangeService.ActionTypeComboBox.SelectedValue))
            {
                case (byte)DB.ADSLChangeServiceActionType.ExtensionService:

                    CRMWebService.ChangeIBSngInfo ibsInfo = new CRMWebService.ChangeIBSngInfo();

                    ibsInfo.UserID = aDSL.UserID;
                    ibsInfo.Deposit = credit;
                    ibsInfo.IsAbsoluteChange = false;
                    ibsInfo.DepositType = "renew";
                    ibsInfo.DepositComment = "Change by Pendar_CRM, Extension Service Request (Renew)";

                    try
                    {
                        CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();
                        webService.changeDeposit(ibsInfo);

                        ibsInfo = new CRMWebService.ChangeIBSngInfo();

                        ibsInfo.UserID = aDSL.UserID.ToString();
                        ibsInfo.RenewNextGroup = service.IBSngGroupName;
                        ibsInfo.RenewRemoveUserExpDates = "1";

                        webService.UpdateUserAttrs(ibsInfo);

                        wirelessChangeService.IsIBSngUpdated = true;
                        Update_FreeCounter(wirelessChangeService.WirelessCode, (int)service.OverNightCredit);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("درخواست مورد نظر در سیستم AAA اعمال نشد، لطفا مجددا تایید نمایید.");
                    }

                    break;

                case (byte)DB.ADSLChangeServiceActionType.ChangeService:

                    ////// Delete Days
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userInfo.Add("exp_date_temp_extend", "-1");
                    userInfo.Add("exp_date_temp_extend_unit", "Days");

                    userAuthentication.Add("user_id", aDSL.UserID);
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ////// Delete Credit
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userAuthentication.Add("credit", "0");
                    userAuthentication.Add("is_absolute_change", true);
                    userAuthentication.Add("credit_comment", "Change by Pendar_CRM, Change Service Request");

                    ibsngService.changeCredit(userAuthentication);

                    ////// Delete Renew Desposit
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userAuthentication.Add("deposit", credit);
                    userAuthentication.Add("is_absolute_change", true);
                    userAuthentication.Add("deposit_type", "renew");
                    userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

                    try
                    {
                        ibsngService.changeDeposit(userAuthentication);
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر اعتبار با موفقیت انجام نشد");
                    }

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userInfo.Add("renew_next_group", service.IBSngGroupName);
                    userInfo.Add("renew_remove_user_exp_dates", "1");
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    try
                    {
                        ibsngService.UpdateUserAttrs(userAuthentication);
                        wirelessChangeService.IsIBSngUpdated = true;
                        Update_FreeCounter(_Request.TelephoneNo.ToString(), (int)service.OverNightCredit);
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر گروه با موفقیت انجام نشد");
                    }
                    break;

                default:
                    break;
            }

            aDSL.TariffID = wirelessChangeService.NewServiceID;
            aDSL.LicenseLetterNo = wirelessChangeService.LicenseLetterNo;
            if (wirelessChangeService.NeedModem == true)
                aDSL.ModemID = wirelessChangeService.ModemSerialNoID;

            aDSL.InstallDate = DB.GetServerDate();
            aDSL.ExpDate = DB.GetServerDate().AddMonths((int)service.DurationID);

            aDSL.Detach();
            DB.Save(aDSL);

            wirelessChangeService.FinalUserID = DB.CurrentUser.ID;
            wirelessChangeService.FinalDate = DB.GetServerDate();
            //aDSLChangeTariff.FinalComment = _ADSLChangeTariff.FinalCommentTextBox.Text;
            wirelessChangeService.Status = true;

            wirelessChangeService.Detach();
            DB.Save(wirelessChangeService);

            ADSLHistory aDSLHistory = new ADSLHistory();
            aDSLHistory.TelephoneNo = _Request.TelephoneNo.ToString();
            aDSLHistory.ServiceID = aDSL.TariffID;
            aDSLHistory.UserID = DB.CurrentUser.ID;
            aDSLHistory.InsertDate = DB.GetServerDate();

            aDSLHistory.Detach();
            DB.Save(aDSLHistory, true);

            string mobileNos = "";
            string customerFullName = "";

            if (aDSL.CustomerOwnerID != null)
            {
                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID);

                customerFullName = CustomerDB.GetFullNameByCustomerID((long)aDSL.CustomerOwnerID);
            }

            string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
            {
                message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);

                CRMWebServiceDB.SendMessage(mobileNos, message);
            }
        }

        private void Confirm_ADSLSellTrafficRequest()
        {
            Save_ADSLSellTraffic();

            Data.ADSLSellTraffic aDSLSellTraffic = ADSLSellTrafficDB.GetADSLSellTrafficById(RequestID);
            Data.ADSL aDSl = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);

            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

            ADSLService AdditionalTraffic = ADSLServiceDB.GetADSLServiceById((int)aDSLSellTraffic.AdditionalServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)AdditionalTraffic.TrafficID);

            ADSLHistory aDSLHistory = new ADSLHistory();
            aDSLHistory.TelephoneNo = _Request.TelephoneNo.ToString();
            aDSLHistory.ServiceID = aDSLSellTraffic.AdditionalServiceID;
            aDSLHistory.UserID = DB.CurrentUser.ID;
            aDSLHistory.InsertDate = DB.GetServerDate();

            aDSLHistory.Detach();
            DB.Save(aDSLHistory, true);

            string mobileNos = "";
            string customerFullName = "";

            if (aDSl.CustomerOwnerID != null)
            {
                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID((long)aDSl.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID((long)aDSl.CustomerOwnerID);

                customerFullName = CustomerDB.GetFullNameByCustomerID((long)aDSl.CustomerOwnerID);
            }

            string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.SellTraffic));

            SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.SellTraffic));

            if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
            {
                message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);

                CRMWebServiceDB.SendMessage(mobileNos, message);
            }

            aDSLSellTraffic.FinalUserID = DB.CurrentUser.ID;
            aDSLSellTraffic.FinalDate = DB.GetServerDate();
            aDSLSellTraffic.FinalComment = "";

            CRMWebService.ChangeIBSngInfo ibsInfo = new CRMWebService.ChangeIBSngInfo();

            ibsInfo.UserID = aDSl.UserID;
            ibsInfo.Deposit = traffic.Credit.ToString();
            ibsInfo.IsAbsoluteChange = false;
            ibsInfo.DepositType = "recharge";
            ibsInfo.DepositComment = "Change by Pendar_CRM, Sell Traffice Request (Recharge)";

            try
            {
                CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();
                service.changeDeposit(ibsInfo);

                Update_FreeCounter(_Request.TelephoneNo.ToString(), (int)AdditionalTraffic.OverNightCredit);

                aDSLSellTraffic.IsIBSngUpdated = true;
            }
            catch (Exception ex)
            {
                throw new Exception("در خواست مورد نظر در سیستم AAA اعمال نشد، لطفا مجددا تایید نمایید.");
            }

            aDSLSellTraffic.Detach();
            DB.Save(aDSLSellTraffic);
        }

        private void Confirm_WirelessSellTrafficRequest()
        {
            Save_WirelessSellTraffic();

            Data.WirelessSellTraffic wirelessSellTraffic = ADSLSellTrafficDB.GetWirelessSellTrafficById(RequestID);
            Data.ADSL aDSl = ADSLDB.GetWirelessbyCode(wirelessSellTraffic.WirelessCode.ToString());

            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

            ADSLService AdditionalTraffic = ADSLServiceDB.GetADSLServiceById((int)wirelessSellTraffic.AdditionalServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)AdditionalTraffic.TrafficID);

            ADSLHistory aDSLHistory = new ADSLHistory();
            aDSLHistory.TelephoneNo = _Request.TelephoneNo.ToString();
            aDSLHistory.ServiceID = wirelessSellTraffic.AdditionalServiceID;
            aDSLHistory.UserID = DB.CurrentUser.ID;
            aDSLHistory.InsertDate = DB.GetServerDate();

            aDSLHistory.Detach();
            DB.Save(aDSLHistory, true);

            string mobileNos = "";
            string customerFullName = "";

            if (aDSl.CustomerOwnerID != null)
            {
                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID((long)aDSl.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID((long)aDSl.CustomerOwnerID);

                customerFullName = CustomerDB.GetFullNameByCustomerID((long)aDSl.CustomerOwnerID);
            }

            string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            if (SmsService.IsActive == true && !string.IsNullOrWhiteSpace(mobileNos))
            {
                message = message.Replace("CustomerName", ((!string.IsNullOrWhiteSpace(customerFullName)) ? customerFullName : "مشترک گرامی ")).Replace("TelephoneNo", _Request.ID.ToString()).Replace("Enter", Environment.NewLine);

                CRMWebServiceDB.SendMessage(mobileNos, message);
            }

            wirelessSellTraffic.FinalUserID = DB.CurrentUser.ID;
            wirelessSellTraffic.FinalDate = DB.GetServerDate();
            wirelessSellTraffic.FinalComment = "";

            CRMWebService.ChangeIBSngInfo ibsInfo = new CRMWebService.ChangeIBSngInfo();

            ibsInfo.UserID = aDSl.UserID;
            ibsInfo.Deposit = traffic.Credit.ToString();
            ibsInfo.IsAbsoluteChange = false;
            ibsInfo.DepositType = "recharge";
            ibsInfo.DepositComment = "Change by Pendar_CRM, Sell Wireless Traffice Request (Recharge)";

            try
            {
                CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();
                service.changeDeposit(ibsInfo);

                Update_FreeCounter(wirelessSellTraffic.WirelessCode, (int)AdditionalTraffic.OverNightCredit);

                wirelessSellTraffic.IsIBSngUpdated = true;
            }
            catch (Exception ex)
            {
                throw new Exception("در خواست مورد نظر در سیستم AAA اعمال نشد، لطفا مجددا تایید نمایید.");
            }

            wirelessSellTraffic.Detach();
            DB.Save(wirelessSellTraffic);
        }

        private void Confirm_ADSLCutRequest()
        {
            Save_ADSLCutRequest();

            CRM.Data.ADSLCutTemporary ADSLCUT = new CRM.Data.ADSLCutTemporary();
            ADSLCUT = ADSLCutTemporaryDB.GetADSLCutTemproryByRequestID(RequestID);

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            if (ADSLCUT.Status == (byte)DB.CutAndEstablishStatus.Cut)
            {

                if (_ADSLCutTemporary.CutTypeComboBox.SelectedIndex != -1)
                    userInfo.Add("lock", _ADSLCutTemporary.CutTypeComboBox.Text);
                else
                    throw new Exception("لطفا توضیحات مربوط به عملیات Lock را وارد نمایید");


                userAuthentication.Add("user_id", _ADSLCutTemporary.UserID);
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", "");

                ibsngService.UpdateUserAttrs(userAuthentication);
            }

            else if (ADSLCUT.Status == (byte)DB.CutAndEstablishStatus.Establish)
            {
                XmlRpcStruct list = new XmlRpcStruct();

                list.Add("to_del_attrs", "lock");

                userAuthentication.Add("user_id", _ADSLCutTemporary.UserID);
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", list);

                ibsngService.UpdateUserAttrs(userAuthentication);
            }

            CRM.Data.ADSLCutTemporary ADSLCutTemporary = DB.SearchByPropertyName<Data.ADSLCutTemporary>("ID", RequestID).SingleOrDefault();
            ADSLCutTemporary.Comment = _ADSLCutTemporary.CommentCustomersTextBox.Text;
            CRM.Data.Schema.ADSLCutTemporary ADSLCutLog = new Data.Schema.ADSLCutTemporary();

            if (ADSLCutTemporary.CutType != null)
                ADSLCutLog.CutType = (byte)ADSLCutTemporary.CutType;

            RequestLog requestLog = new RequestLog();
            requestLog = new RequestLog();
            requestLog.RequestID = _Request.ID;
            requestLog.RequestTypeID = _Request.RequestTypeID;
            requestLog.TelephoneNo = _ADSLCutTemporary.TeleInfo.TelephoneNo;

            requestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.ADSLCutTemporary>(ADSLCutLog, true));

            requestLog.Detach();
            DB.Save(requestLog);

            ADSLCutTemporary.Detach();
            DB.Save(ADSLCutTemporary);
        }

        private void Confirm_TitleIn118Request()
        {

        }

        private void Confirm_ADSLChangeCustomerOwnerCharacteristics()
        {
            Save_ADSLChangeCustomerOwnerCharacteristics();

            CRM.Data.ADSL adsl = new CRM.Data.ADSL();
            ADSLChangeCustomerOwnerCharacteristic change = new ADSLChangeCustomerOwnerCharacteristic();
            change = ADSLChangeCustomerOwnerCharacteristicsDB.GetADSLChangeCustomerOwnerCharacteristicsByID(RequestID);

            adsl = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);

            adsl.CustomerOwnerID = change.NewCustomerOwnerID;
            adsl.CustomerOwnerStatus = change.NewCustomerOwnerStatus;
            adsl.Detach();
            DB.Save(adsl);

            Telephone.CustomerID = change.NewCustomerOwnerID;
            Telephone.Detach();
            Save(Telephone);

            change.FinalDate = DB.GetServerDate().Date;
            change.FinalUserID = DB.CurrentUser.ID;
            change.Detach();
            DB.Save(change);

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            string customerName = CustomerDB.GetFullNameByCustomerID((long)adsl.CustomerOwnerID);
            userInfo.Add("name", customerName);

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", adsl.UserID.ToString());
            userAuthentication.Add("attrs", userInfo);
            userAuthentication.Add("to_del_attrs", "");

            ibsngService.UpdateUserAttrs(userAuthentication);
        }

        public override bool Deny()
        {
            try
            {
                List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstalmentbyRequestID(RequestID);
                if (instalments != null)
                    if (instalments.Count != 0)
                        DB.DeleteAll<InstallmentRequestPayment>(instalments.Select(t => t.ID).ToList());

                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.ADSL:
                        Deny_ADSLRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        Deny_ADSLChangeServiceRequest();
                        break;

                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationCenterToCenterReject();
                        break;
                }

                IsRejectSuccess = true;
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در رد درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در رد درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رد درخواست، " + ex.Message + " !", ex);
            }

            return IsRejectSuccess;
        }

        private void ChangeLocationCenterToCenterReject()
        {
            // اگر درخواست در مبدا است 
            if (_changeLocation.SourceCenter != null && _changeLocation.SourceCenter == requestCenterID)
            {
                if (_changeLocation.TargetCenter != null)
                {
                    _Request.CenterID = (int)_changeLocation.TargetCenter;
                    _Request.Detach();
                    DB.Save(_Request);
                }
                else
                {
                    throw new Exception("مرکز مقصد یافت نشد.");
                }
            }
            // اگر درخواست در مقصد است
            else if (_changeLocation.SourceCenter != null && _changeLocation.TargetCenter == requestCenterID)
            {
                if (_changeLocation.TargetCenter != null)
                {
                    _Request.CenterID = (int)_changeLocation.SourceCenter;
                    _Request.Detach();
                    DB.Save(_Request);
                }
                else
                {
                    throw new Exception("مرکز مبدا یافت نشد.");
                }
            }
        }

        private void Deny_ADSLRequest()
        {
            if (RequestPaymentDB.HasPaidRequestPaymentByRequestID(RequestID))
                throw new Exception("هزینه این درخواست پرداخت شده است، امکان رد آن وجود ندارد");
            else
            {
                //      تست کنم
                //    این رو هم تست کنم که اگر قسط نداشت و نال برگرداند مشکلی پیش نیاد                

                List<RequestPayment> noPaidPayments = RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                DB.DeleteAll<RequestPayment>(noPaidPayments.Select(t => t.ID).ToList());

                CRM.Data.ADSLRequest ADSLRequest = DB.SearchByPropertyName<Data.ADSLRequest>("ID", RequestID).SingleOrDefault();

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                    ADSLRequest.CommentCustomers = _ADSL.CommentCustomersTextBox.Text;

                ADSLRequest.Detach();
                DB.Save(ADSLRequest);
            }
        }

        private void Deny_ADSLChangeServiceRequest()
        {
            CRM.Data.ADSLChangeService ADSLChangeService = ADSLChangeTariffDB.GetADSLChangeServicebyID(RequestID);

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                ADSLChangeService.CommentCustomers = _ADSLChangeService.CommentCustomersTextBox.Text;

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
            {
                ADSLChangeService.OMCUserID = DB.CurrentUser.ID;
                ADSLChangeService.OMCDate = DB.GetServerDate();
                ADSLChangeService.OMCComment = _ADSLChangeService.OMCCommentTextBox.Text;
            }

            ADSLChangeService.Detach();
            DB.Save(ADSLChangeService);
        }

        public override bool SaveWaitingList()
        {
            try
            {
                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.Dayri:
                        SaveWaitingList_InstalRequest();
                        break;

                    case (int)DB.RequestType.ADSL:
                        SaveWaitingList_ADSLRequest();
                        break;
                }

                IsSaveWatingListSuccess = true;
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در ذخیره درخواست در لیست انتظار، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در ذخیره درخواست در لیست انتظار، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره درخواست در لیست انتظار، " + ex.Message + " !", ex);
            }

            return IsSaveWatingListSuccess;
        }

        private void SaveWaitingList_InstalRequest()
        {
        }

        private void SaveWaitingList_ADSLRequest()
        {
            //if (_ADSL.NextStatusListBox.SelectedValue != null && Convert.ToInt32(_ADSL.NextStatusListBox.SelectedValue) == 1)
            //    throw new Exception("لطفا صحت انتخاب وضعیت را بررسی نمایید");

            Save();

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                CRM.Data.ADSLRequest ADSLRequest = new CRM.Data.ADSLRequest();

                if (_ADSL.ServiceComboBox.SelectedValue != null)
                    ADSLRequest.ServiceID = (int)_ADSL.ServiceComboBox.SelectedValue;
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                ADSLRequest.LicenseLetterNo = _ADSL.LicenceLetterNoTextBox.Text;

                //if (_ADSL.CustomerPriorityComboBox.SelectedValue != null)
                //    ADSLRequest.CustomerPriority = (byte)_ADSL.CustomerPriorityComboBox.SelectedValue;
                //else
                ADSLRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;

                ADSLRequest.RequiredInstalation = _ADSL.RequiredInstalationCheckBox.IsChecked;

                ADSLRequest.NeedModem = _ADSL.NeedModemCheckBox.IsChecked;

                if (_ADSL.NeedModemCheckBox.IsChecked != null)
                {
                    if ((bool)_ADSL.NeedModemCheckBox.IsChecked)
                        if (_ADSL.ModemTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                            ADSLRequest.ModemID = (int)_ADSL.ModemTypeComboBox.SelectedValue;
                }

                if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                {
                    if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                        throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                }
            }

            WaitingList waitingList = new WaitingList();
            waitingList.ReasonID = (byte)DB.WaitingListReason.PortLess;
            waitingList.Status = false;

            ADSLRequest aDSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);
            RequestForADSL.SaveWaitingList(RequestID, _Request, waitingList);

            RequestID = _Request.ID;
        }

        private void RefreshForm()
        {
            RequestID = 0;
            LoadData();
        }

        private void InsertRequestPayment()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (_IsSalable)
                {
                    List<RequestPayment> requestPaymentList = new List<RequestPayment>();
                    DateTime dateTime = DB.GetServerDate();
                    switch (_Request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.ADSL:
                        case (byte)DB.RequestType.Wireless:
                        case (byte)DB.RequestType.ADSLChangeService:
                        case (byte)DB.RequestType.ADSLSellTraffic:
                        case (byte)DB.RequestType.ADSLChangeIP:
                        case (byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                        case (byte)DB.RequestType.ADSLChangePlace:
                        case (byte)DB.RequestType.ADSLChangePort:
                        case (byte)DB.RequestType.ADSLCutTemporary:
                        case (byte)DB.RequestType.ADSLDischarge:
                        case (byte)DB.RequestType.ADSLInstall:
                            break;
                        case (byte)DB.RequestType.ChangeNo:
                            {
                                if (_ChangeNo.CauseOfChangeNoID == (int)DB.CauseOfChangeNo.TechnicalWithoutCost)
                                {
                                    return;
                                }
                                else
                                {
                                    RequestPayment requestPayment = new RequestPayment();


                                    List<RequestPayment> allRecordedRequestPayments = RequestPaymentDB.GetAllRequestPaymentByRequestID(_Request.ID);

                                    // برای محاسبه دوباره هزینه ها می باشد
                                    if (allRecordedRequestPayments.Count != 0 && !StatusDB.IsFinalStep(_Request.StatusID))
                                    {
                                        //سیاست کاری بدین صورت است که چنانچه هزینه ای پرداخت نشده باشد ، آن هزینه حذف میگردد و در بلاک مربوطه دوباره ثبت میگردد
                                        List<RequestPayment> notPaidRequestPayments = allRecordedRequestPayments.Where(rp =>
                                                                                                                            (rp.IsPaid.HasValue ? rp.IsPaid.Value == false : rp.IsPaid == null) &&
                                                                                                                            (rp.IsKickedBack.HasValue && rp.IsKickedBack.Value == false)
                                                                                                                       )
                                                                                                                .ToList();
                                        if (notPaidRequestPayments.Count != 0)
                                        {
                                            DB.DeleteAll<RequestPayment>(notPaidRequestPayments.Select(rp => rp.ID).ToList());
                                        }
                                    }

                                    List<BaseCost> baseCosts = Data.BaseCostDB.GetBaseCostByRequestTypeID(_Request.RequestTypeID);
                                    DateTime now = DB.GetServerDate();
                                    foreach (BaseCost cost in baseCosts)
                                    {
                                        requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = now;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = _Request.ID;
                                        requestPayment.PaymentType = cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                        {
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                        }
                                        else
                                        {
                                            requestPayment.AmountSum = cost.Cost;
                                        }

                                        DB.Save(requestPayment);
                                    }
                                }
                                break;
                            }
                        case (byte)DB.RequestType.OpenAndCloseZero:
                            {
                                RequestPayment requestPayment = new RequestPayment();

                                //در کد زیر چنانچه برای درخواست جاری هزینه ای ثبت شده باشد ، آن را بازیابی می کنیم
                                List<RequestPayment> allRecordedRequestPayments = RequestPaymentDB.GetAllRequestPaymentByRequestID(_Request.ID);

                                if (allRecordedRequestPayments.Count != 0 && !StatusDB.IsFinalStep(_Request.StatusID))
                                {
                                    //سیاست کاری بدین صورت است که چنانچه هزینه ای پرداخت نشده باشد ، آن هزینه حذف میگردد و در بلاک مربوطه دوباره ثبت میگردد
                                    List<RequestPayment> notPaidRequestPayments = allRecordedRequestPayments.Where(rp =>
                                                                                                                        (rp.IsPaid.HasValue ? rp.IsPaid.Value == false : rp.IsPaid == null) &&
                                                                                                                        (rp.IsKickedBack.HasValue && rp.IsKickedBack.Value == false)
                                                                                                                   )
                                                                                                            .ToList();
                                    if (notPaidRequestPayments.Count != 0)
                                    {
                                        DB.DeleteAll<RequestPayment>(notPaidRequestPayments.Select(rp => rp.ID).ToList());
                                    }
                                }

                                List<BaseCost> baseCosts = Data.BaseCostDB.GetBaseCostByRequestTypeID(_Request.RequestTypeID);
                                DateTime now = DB.GetServerDate();

                                if (Telephone.ClassTelephone == (byte)DB.ClassTelephone.SecondZeroBlock && _zeroStatus.ClassTelephone == (byte)DB.ClassTelephone.LimitLess)
                                {
                                    baseCosts = baseCosts.Where(t => t.Title == "بازكردن صفردوم").ToList();
                                    foreach (BaseCost cost in baseCosts)
                                    {
                                        requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = now;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = _Request.ID;
                                        requestPayment.PaymentType = cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                        {
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                        }
                                        else
                                        {
                                            requestPayment.AmountSum = cost.Cost;
                                        }

                                        DB.Save(requestPayment);
                                    }
                                }
                                else if (Telephone.ClassTelephone == (byte)DB.ClassTelephone.LimitLess && _zeroStatus.ClassTelephone == (byte)DB.ClassTelephone.SecondZeroBlock)
                                {
                                    /*
                                     * چنانچه کاربر در هنگام ثبت درخواست انسداد صفر دوم 
                                     * آپشن همراه با هزینه را زده باشد وارد این بلاک میشود و هزینه مربوطه را لود میکند
                                     */
                                    if (_zeroStatus.HasSecondZeroBlockCost.HasValue && _zeroStatus.HasSecondZeroBlockCost.Value)
                                    {
                                        CustomerType customerType = CustomerTypeDB.GetCustomerTypeByTelephoneNo(Telephone.TelephoneNo);
                                        baseCosts = baseCosts.Where(t => t.Title == "بستن صفر دوم").ToList();

                                        //دولتی بودن تلفن چک میشود
                                        if (customerType != null && (customerType.Title == "دولتی" || customerType.Title == "دولتي"))
                                        {
                                            //در صورتی که نوع مشترک تلفن جاری دولتی باشد ، باید بررسی کنیم که برای این تلفن تا کنون انسداد صفری صورت گرفته است یا خیر
                                            bool hasAnySecondZeroBlock = ZeroStatusDB.HasAnySecondZeroBlock(Telephone.TelephoneNo);

                                            //اگر انسداد صفر دومی داشته باشد  ، باید هزینه برای تلفن ثبت شود
                                            if (hasAnySecondZeroBlock)
                                            {
                                                foreach (BaseCost cost in baseCosts)
                                                {
                                                    requestPayment = new RequestPayment();
                                                    requestPayment.InsertDate = now;
                                                    requestPayment.BaseCostID = cost.ID;
                                                    requestPayment.RequestID = _Request.ID;
                                                    requestPayment.PaymentType = cost.PaymentType;
                                                    requestPayment.IsKickedBack = false;
                                                    requestPayment.IsAccepted = false;
                                                    requestPayment.IsPaid = false;
                                                    requestPayment.Cost = cost.Cost;
                                                    requestPayment.Tax = cost.Tax;

                                                    if (cost.Tax != null)
                                                        requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                                    else
                                                        requestPayment.AmountSum = cost.Cost;

                                                    DB.Save(requestPayment);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //در این بلاک به نوع مشترک تلفن جاری توجهی نمیشود
                                            foreach (BaseCost cost in baseCosts)
                                            {
                                                requestPayment = new RequestPayment();
                                                requestPayment.InsertDate = now;
                                                requestPayment.BaseCostID = cost.ID;
                                                requestPayment.RequestID = _Request.ID;
                                                requestPayment.PaymentType = cost.PaymentType;
                                                requestPayment.IsKickedBack = false;
                                                requestPayment.IsAccepted = false;
                                                requestPayment.IsPaid = false;
                                                requestPayment.Cost = cost.Cost;
                                                requestPayment.Tax = cost.Tax;

                                                if (cost.Tax != null)
                                                    requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                                else
                                                    requestPayment.AmountSum = cost.Cost;

                                                DB.Save(requestPayment);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case (byte)DB.RequestType.Dayri:
                            {
                                //TODO: rad این قسمت کد کثیف شد به خاطر تز فضایی کرمانشاه در راستای اقساط

                                List<BaseCost> activeBaseCostsForCurrentRequestType = Data.BaseCostDB.GetBaseCostByRequestTypeID(_Request.RequestTypeID);

                                //TODO:rad 13950803
                                //به علت این که درخواست های ماقبل از مطرح شدن بحث هزینه قسطی اتصال تلفن دارای ستون زیر نیستند بلاک ایف تعریف شد
                                //MethodOfPaymentForTelephoneConnection
                                if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value != (byte)DB.MethodOfPaymentForTelephoneConnection.Unknown)
                                {
                                    BaseCost prepaymentBaseCost = BaseCostDB.GetBaseCostByID((int)DB.SpecialCostID.PrePaymentTypeCostID);
                                    activeBaseCostsForCurrentRequestType.Add(prepaymentBaseCost);
                                }

                                List<RequestPayment> allRecordedRequestPaymentsForCurrentRequest = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                                if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Cash)
                                {
                                    var installmentBaseCostRequestPayments = allRecordedRequestPaymentsForCurrentRequest.Where(rp =>
                                                                                                                        rp.PaymentType == (byte)DB.PaymentType.Instalment
                                                                                                                )
                                                                                                          .Select(rp => rp.ID)
                                                                                                          .ToList();
                                    DB.DeleteAll<RequestPayment>(installmentBaseCostRequestPayments);

                                    var prepaymentBaseCostRequestPayments = allRecordedRequestPaymentsForCurrentRequest.Where(rp =>
                                                                                                                                    rp.BaseCostID == (int)DB.SpecialCostID.PrePaymentTypeCostID
                                                                                                                              )
                                                                                                                       .Select(rp => rp.ID)
                                                                                                                       .ToList();

                                    DB.DeleteAll<RequestPayment>(prepaymentBaseCostRequestPayments);

                                    activeBaseCostsForCurrentRequestType.RemoveAll(baseCost => baseCost.PaymentType == (byte)DB.PaymentType.Instalment);
                                    activeBaseCostsForCurrentRequestType.RemoveAll(baseCost => baseCost.ID == (int)DB.SpecialCostID.PrePaymentTypeCostID);
                                }
                                else if (_installReqeust.MethodOfPaymentForTelephoneConnection.HasValue && _installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Installment)
                                {
                                    var cashBaseCosts = allRecordedRequestPaymentsForCurrentRequest.Where(rp =>
                                                                                                                rp.PaymentType == (byte)DB.PaymentType.Cash &&
                                                                                                                rp.BaseCostID != (int)DB.SpecialCostID.PrePaymentTypeCostID &&
                                                                                                                (BaseCostDB.IsOutBoundBaseCost(rp.BaseCostID.Value, (int)DB.RequestType.Dayri) == false)
                                                                                                         )
                                                                                                    .Select(rp => rp.ID)
                                                                                                    .ToList();
                                    DB.DeleteAll<RequestPayment>(cashBaseCosts);

                                    activeBaseCostsForCurrentRequestType.RemoveAll(baseCost =>
                                                                                                baseCost.PaymentType == (byte)DB.PaymentType.Cash &&
                                                                                                baseCost.ID != (int)DB.SpecialCostID.PrePaymentTypeCostID &&
                                                                                                (BaseCostDB.IsOutBoundBaseCost(baseCost.ID, (int)DB.RequestType.Dayri) == false)
                                                                                  );
                                }

                                // اگر دایری یا دایری مجدد است و درخواست ثبت در 118 دارد هزینه های 118 را هم به آن اضافه می کند
                                if (_Request.RequestTypeID == (byte)DB.RequestType.Dayri || _Request.RequestTypeID == (byte)DB.RequestType.Reinstall)
                                {
                                    if (_installReqeust != null && _installReqeust.RegisterAt118 == true)
                                    {
                                        activeBaseCostsForCurrentRequestType = activeBaseCostsForCurrentRequestType.Union(Data.BaseCostDB.GetBaseCostByRequestTypeID((byte)DB.RequestType.TitleIn118)).ToList();
                                    }

                                    if (_installReqeust != null && _installReqeust.ClassTelephone == (int)DB.ClassTelephone.SecondZeroBlock)
                                    {
                                        //currentRequestTypeCost = currentRequestTypeCost.Union(Data.BaseCostDB.GetBaseCostByRequestTypeID((byte)DB.RequestType.OpenAndCloseZero)).ToList();
                                        activeBaseCostsForCurrentRequestType = activeBaseCostsForCurrentRequestType.Union(new List<BaseCost> { Data.BaseCostDB.GetBaseCostByID((int)DB.SpecialCostID.BlockSecondZero) }).ToList();
                                    }
                                }

                                List<BaseCost> baseCostsThatNotRecordedForCurrentRequest = activeBaseCostsForCurrentRequestType.Where(t => !allRecordedRequestPaymentsForCurrentRequest.Select(rp => rp.BaseCostID).Contains(t.ID)).ToList();

                                long addressID = -1;
                                addressID = GetAddressOfRequest();
                                int? outBoundMeter = Data.VisitAddressDB.GetOutBoundMeterByRequestID(_Request.ID, addressID);
                                long? cableMeter = Data.VisitAddressDB.GetCableMeterByRequestID(_Request.ID, addressID);

                                foreach (BaseCost cost in baseCostsThatNotRecordedForCurrentRequest)
                                {
                                    if (cost.ID == (int)DB.SpecialCostID.PrePaymentTypeCostID)
                                    {
                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                            //requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * 2000000);
                                        else
                                            requestPayment.AmountSum = cost.Cost;

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                    //ستون 
                                    //cost.UseOutBound 
                                    //همان ستونی است که در زمان تعریف فرمول برای یک نوع درخواست ایجاد میکنیم و دکمه آنا را در ویرایشگر فرومل اضافه مینماییم
                                    else if (cost.Cost != 0 && cost.UseOutBound == false)
                                    {
                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                        else
                                            requestPayment.AmountSum = cost.Cost;

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                    if (cost.Cost != 0 && cost.UseOutBound == true && outBoundMeter.HasValue == true)
                                    {
                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                        else
                                            requestPayment.AmountSum = cost.Cost;

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                    else if (cost.Cost == 0 && cost.IsFormula == true)
                                    {
                                        // if cost is formula execute it, and if uses outBoundMeter is replaced with the value else not save cost,
                                        string formula = cost.Formula;
                                        if (cost.UseOutBound == true)
                                        {
                                            if (outBoundMeter.HasValue == true)
                                            {
                                                formula = cost.Formula.Replace("outBoundMeter", outBoundMeter.ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        if (cost.UseCableMeter == true)
                                        {
                                            if (cableMeter.HasValue == true)
                                            {
                                                formula = cost.Formula.Replace("CableMeter", cableMeter.ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        //if (cost.UseZeroBlock == true)
                                        //{
                                        //    byte? zeroBlockValue = Data.ZeroStatusDB.GetZeroBlockByRequestID(_Request);
                                        //    if (zeroBlockValue != null)
                                        //    {
                                        //        formula = cost.Formula.Replace("ZeroBlock", Convert.ToString(zeroBlockValue));
                                        //    }
                                        //    else
                                        //    {
                                        //        Folder.MessageBox.ShowInfo("برای این روال بستن صفر در نظر گرفته نشده است لطفا با مدیر سیستم تماس بگیرید");
                                        //        continue;
                                        //    }
                                        //}



                                        double costFormula = Calculate.Execute(formula);
                                        if (costFormula == 0) continue;

                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;

                                        requestPayment.Cost = 0;
                                        requestPayment.Tax = cost.Tax;
                                        if (cost.Tax != null)
                                            requestPayment.AmountSum = Convert.ToInt64(costFormula + cost.Tax * 0.01 * costFormula);
                                        else
                                            requestPayment.AmountSum = Convert.ToInt64(costFormula);

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                }
                            }
                            break;
                        default:
                            {
                                List<BaseCost> currentRequestTypeCost = Data.BaseCostDB.GetBaseCostByRequestTypeID(_Request.RequestTypeID);
                                //TODO:rad 13950803 برای درخواست دایری - دایری مجدد یک بلاک کیس مجزا ایجاد کرد بنابراین باید بلاک زیر کامنت میشد
                                //// اگر دایری یا دایری مجدد است و درخواست ثبت در 118 دارد هزینه های 118 را هم به آن اضافه می کند
                                //if (_Request.RequestTypeID == (byte)DB.RequestType.Dayri || _Request.RequestTypeID == (byte)DB.RequestType.Reinstall)
                                //{
                                //
                                //    if (_installReqeust != null && _installReqeust.RegisterAt118 == true)
                                //    {
                                //        currentRequestTypeCost = currentRequestTypeCost.Union(Data.BaseCostDB.GetBaseCostByRequestTypeID((byte)DB.RequestType.TitleIn118)).ToList();
                                //    }
                                //
                                //    if (_installReqeust != null && _installReqeust.ClassTelephone == (int)DB.ClassTelephone.SecondZeroBlock)
                                //    {
                                //        //currentRequestTypeCost = currentRequestTypeCost.Union(Data.BaseCostDB.GetBaseCostByRequestTypeID((byte)DB.RequestType.OpenAndCloseZero)).ToList();
                                //        currentRequestTypeCost = currentRequestTypeCost.Union(new List<BaseCost> { Data.BaseCostDB.GetBaseCostByID((int)DB.SpecialCostID.BlockSecondZero) }).ToList();
                                //    }
                                //
                                //}

                                List<RequestPayment> requestPayments = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                                List<BaseCost> NewRequestTypeCost = currentRequestTypeCost.Where(t => !requestPayments.Select(rp => rp.BaseCostID).Contains(t.ID)).ToList();


                                long addressID = -1;
                                addressID = GetAddressOfRequest();
                                int? outBoundMeter = Data.VisitAddressDB.GetOutBoundMeterByRequestID(_Request.ID, addressID);
                                long? cableMeter = Data.VisitAddressDB.GetCableMeterByRequestID(_Request.ID, addressID);

                                foreach (BaseCost cost in NewRequestTypeCost)
                                {
                                    //ستون 
                                    //cost.UseOutBound 
                                    //همان ستونی است که در زمان تعریف فرمول برای یک نوع درخواست ایجاد میکنیم و دکمه آنا را در ویرایشگر فرومل اضافه مینماییم
                                    if (cost.Cost != 0 && cost.UseOutBound == false)
                                    {
                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                        else
                                            requestPayment.AmountSum = cost.Cost;

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                    if (cost.Cost != 0 && cost.UseOutBound == true && outBoundMeter.HasValue == true)
                                    {
                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;
                                        requestPayment.Cost = cost.Cost;
                                        requestPayment.Tax = cost.Tax;

                                        if (cost.Tax != null)
                                            requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                        else
                                            requestPayment.AmountSum = cost.Cost;

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                    else if (cost.Cost == 0 && cost.IsFormula == true)
                                    {
                                        // if cost is formula execute it, and if uses outBoundMeter is replaced with the value else not save cost,
                                        string formula = cost.Formula;
                                        if (cost.UseOutBound == true)
                                        {
                                            if (outBoundMeter.HasValue == true)
                                            {
                                                formula = cost.Formula.Replace("outBoundMeter", outBoundMeter.ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        if (cost.UseCableMeter == true)
                                        {
                                            if (cableMeter.HasValue == true)
                                            {
                                                formula = cost.Formula.Replace("CableMeter", cableMeter.ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        //if (cost.UseZeroBlock == true)
                                        //{
                                        //    byte? zeroBlockValue = Data.ZeroStatusDB.GetZeroBlockByRequestID(_Request);
                                        //    if (zeroBlockValue != null)
                                        //    {
                                        //        formula = cost.Formula.Replace("ZeroBlock", Convert.ToString(zeroBlockValue));
                                        //    }
                                        //    else
                                        //    {
                                        //        Folder.MessageBox.ShowInfo("برای این روال بستن صفر در نظر گرفته نشده است لطفا با مدیر سیستم تماس بگیرید");
                                        //        continue;
                                        //    }
                                        //}



                                        double costFormula = Calculate.Execute(formula);
                                        if (costFormula == 0) continue;

                                        RequestPayment requestPayment = new RequestPayment();
                                        requestPayment.InsertDate = dateTime;
                                        requestPayment.BaseCostID = cost.ID;
                                        requestPayment.RequestID = RequestID;
                                        requestPayment.PaymentType = (byte)cost.PaymentType;
                                        requestPayment.IsKickedBack = false;
                                        requestPayment.IsAccepted = false;
                                        requestPayment.IsPaid = false;

                                        requestPayment.Cost = 0;
                                        requestPayment.Tax = cost.Tax;
                                        if (cost.Tax != null)
                                            requestPayment.AmountSum = Convert.ToInt64(costFormula + cost.Tax * 0.01 * costFormula);
                                        else
                                            requestPayment.AmountSum = Convert.ToInt64(costFormula);

                                        requestPaymentList.Add(requestPayment);

                                        requestPayment.Detach();
                                        DB.Save(requestPayment);
                                    }
                                }
                            }
                            break;
                    }
                }
                scope.Complete();
            }

            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetAllRequestPaymentByRequestID(RequestID);
        }

        public void CheckRequestDocument()
        {
            if (Customer != null)
            {
                List<DocumentsByCustomer> NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(RequestType.ID, DB.GetServerDate(), Customer.PersonType).Where(t => t.IsForcible == true).ToList();
                NeededDocs.ForEach(item =>
                {
                    if (!Data.RequestDB.CheckIsDocument(_Request.ID, item.DocumentTypeID))
                    {
                        Folder.MessageBox.ShowInfo("دریافت مدرک " + item.DocumentName + " اجباری است.");
                        throw new Exception("خطا در دریافت مدارک");
                    }
                });
            }
        }

        private void EnableRequestDetails()
        {
            RequestDocGrid.Visibility = Visibility.Visible;
            RequestFormDataGrid.Visibility = Visibility.Visible;
            RequestPermissionGrid.Visibility = Visibility.Visible;
            RequestContractGrid.Visibility = Visibility.Visible;
            RelatedRequestsGrid.Visibility = Visibility.Visible;
            // InstallmentsGrid.Visibility = Visibility.Visible;
            RequestPaymentDataGrid.Visibility = Visibility.Visible;
            RequestFormDataGrid.Visibility = Visibility.Visible;

            if (Customer != null)
            {
                var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 1).ToList();
                RequestPermissionGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
                RequestContractGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
            }
            else
            {
                var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 1).ToList();
                RequestPermissionGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
                RequestContractGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
            }
            RelatedRequestsGrid.ItemsSource = Data.RequestDB.GetRelatedRequestByID(_Request.ID);
            //   InstallmentsGrid.ItemsSource = Data.InstallmentDB.GetInstallmentByRequestID(RequestID);
            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
            RequestFormDataGrid.ItemsSource = Data.FormTemplateDB.GetFormTemplateByRequestTypeID(_Request.RequestTypeID);
        }

        private void EnableFormDetails()
        {
            using (MainDataContext context = new MainDataContext())
            {
                RequestFormDataGrid.ItemsSource = context.FormTemplates.Where(t => t.RequestTypeID == _Request.RequestTypeID).ToList();
            }
        }

        public string GenerateMD5HashPassword(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public List<InstallmentRequestPayment> GenerateInstalments(bool daily, long requestPaymentID, int instalmentCount, byte requestTypeID, long amount, bool isCheque)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int _floorValue = 1000;
                List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
                int PaymentAmountEachPart = 0;

                if (requestTypeID == (byte)DB.RequestType.ADSL || requestTypeID == (byte)DB.RequestType.ADSLChangeService)
                {
                    string startDate = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    string endateCount = Helper.AddMonthToPersianDate(startDate, instalmentCount);
                    //string endDate = EndDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                    string endDate = Helper.AddMonthToPersianDate(startDate, instalmentCount);

                    string startDateEachPart = startDate;

                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(amount / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(amount / (decimal)instalmentCount);

                    for (int i = 1; i <= instalmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = requestPaymentID;
                        installmentRequestPayment.TelephoneNo = _Request.TelephoneNo;
                        installmentRequestPayment.IsCheque = isCheque;
                        installmentRequestPayment.IsPaid = false;
                        installmentRequestPayment.IsDeleted = false;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;

                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (instalmentCount == i)
                            installmentRequestPayment.Cost = (long)(amount - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);

                        installmentRequestPayments.Add(installmentRequestPayment);
                    }
                }
                else
                {
                    string startDate = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    string endDate = Helper.AddMonthToPersianDate(startDate, instalmentCount);

                    string startDateEachPart = startDate;

                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(amount / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(amount / (decimal)instalmentCount);

                    for (int i = 1; i <= instalmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = requestPaymentID;
                        installmentRequestPayment.IsCheque = isCheque;
                        installmentRequestPayment.IsPaid = false;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;
                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (instalmentCount == i)
                            installmentRequestPayment.Cost = (long)(amount - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);
                        installmentRequestPayments.Add(installmentRequestPayment);
                    }
                }

                return installmentRequestPayments;
            }
        }

        private string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        private bool fixTelephoneRequests()
        {
            bool result = false;
            switch (RequestType.ID)
            {
                case (byte)DB.RequestType.ChangeLocationCenterInside:
                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                case (byte)DB.RequestType.ChangeAddress:
                case (byte)DB.RequestType.ChangeName:
                case (byte)DB.RequestType.ChangeNo:
                case (byte)DB.RequestType.CutAndEstablish:
                case (byte)DB.RequestType.Dischargin:
                case (byte)DB.RequestType.E1:
                case (byte)DB.RequestType.VacateE1:
                case (byte)DB.RequestType.E1Link:
                case (byte)DB.RequestType.E1Fiber:
                case (byte)DB.RequestType.OpenAndCloseZero:
                case (byte)DB.RequestType.RefundDeposit:
                case (byte)DB.RequestType.Reinstall:
                case (byte)DB.RequestType.SpecialService:
                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                case (byte)DB.RequestType.SpecialWire:
                case (byte)DB.RequestType.VacateSpecialWire:
                case (byte)DB.RequestType.SpecialWireOtherPoint:
                case (byte)DB.RequestType.TitleIn118:
                case (byte)DB.RequestType.Dayri:
                    {
                        result = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return result;
        }

        private double GetNightFree_NormalBWPhoneNumber(string PhoneNumber)
        {
            CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
            CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();

            ibsngInputInfo.NormalUsername = PhoneNumber.ToUpper();

            CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();
            ibsngUserInfo = service.GetUserInfo(ibsngInputInfo);

            return ibsngUserInfo.CustomFieldFreeCounter;
        }

        private double GetNightFree_NormalBWUsername(string username)
        {
            CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
            CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();

            ibsngInputInfo.NormalUsername = username;

            CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();
            ibsngUserInfo = service.GetUserInfo(ibsngInputInfo);

            return ibsngUserInfo.CustomFieldFreeCounter;
        }

        public void Update_FreeCounter(string telephoneNo, int overnightCredit)
        {

            Data.ADSL aDSL = null;
            if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
            else
                aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

            double custom_field_free_counter = 0;
            custom_field_free_counter = GetNightFree_NormalBWPhoneNumber(telephoneNo);

            string Free_Counter = Convert.ToInt32((custom_field_free_counter + overnightCredit)).ToString();

            CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();
            CRMWebService.ChangeIBSngInfo ibsInfo = new CRMWebService.ChangeIBSngInfo();

            try
            {
                ibsInfo.CustomFieldFreeCounter = Free_Counter;
                ibsInfo.UserID = aDSL.UserID.ToString();

                service.UpdateUserAttrs(ibsInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update_FreeCounterWireless(string username, int overnightCredit)
        {

            double custom_field_free_counter = 0;
            custom_field_free_counter = GetNightFree_NormalBWUsername(username);

            string Free_Counter = Convert.ToInt32((custom_field_free_counter + overnightCredit)).ToString();

            CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();
            CRMWebService.ChangeIBSngInfo ibsInfo = new CRMWebService.ChangeIBSngInfo();

            try
            {
                ibsInfo.CustomFieldFreeCounter = Free_Counter;
                ibsInfo.UserID = wirelessRequest.UserID.ToString();

                service.UpdateUserAttrs(ibsInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ShowReport(IEnumerable result, string Title, int ReportTemplateID, bool ShowOldAddressInPrint = false)
        {
            try
            {
                DateTime currentDateTime = DB.GetServerDate();

                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();
                string path = Data.ReportDB.GetReportPath(ReportTemplateID);
                stiReport.Load(path);
                int FindVariable = stiReport.Dictionary.Variables.Items.Where(t => t.Name == "ShowOldAddress").Count();
                if (FindVariable > 0)
                    stiReport.Dictionary.Variables["ShowOldAddress"].ValueObject = ShowOldAddressInPrint;

                stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

                stiReport.RegData("result", "result", result);


                if (stiReport != null)
                {
                    var frm = new ReportViewerForm(stiReport);
                    frm.ShowDialog();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return false;
        }

        public override bool Print()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                PrintCertificate();
                IsPrintSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در نمایش گزارش", ex);
                IsPrintSuccess = false;
            }
            this.Cursor = Cursors.Arrow;
            return IsPrintSuccess;

        }

        private void PrintCertificate()
        {
            List<EnumItem> AllSpecialWireType = Helper.GetEnumItem(typeof(DB.SpecialWireType));
            Stimulsoft.Report.Dictionary.StiVariable dateVariable = new Stimulsoft.Report.Dictionary.StiVariable("ReportDate", "ReportDate", DB.GetServerDate().ToPersian(Date.DateStringType.Short));
            IEnumerable result;
            ChangeLocationCenterInfo resultChangeLocation;
            switch (RequestType.ID)
            {
                case (int)DB.RequestType.Dayri:

                    result = ReportDB.GetInstallProcessReport(null, null, null, new List<long> { RequestID }, new List<int> { }, null, null, null);
                    List<EnumItem> ChargingGroup = Helper.GetEnumItem(typeof(DB.ChargingGroup));
                    List<EnumItem> OrderType = Helper.GetEnumItem(typeof(DB.OrderType));
                    List<EnumItem> PossessionType = Helper.GetEnumItem(typeof(DB.PossessionType));
                    List<EnumItem> PaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));

                    foreach (InstallRequestReport item in result)
                    {
                        item.ChargingType = string.IsNullOrEmpty(item.ChargingType) ? "" : ChargingGroup.Find(i => i.ID == byte.Parse(item.ChargingType)).Name;
                        item.OrderType = string.IsNullOrEmpty(item.ChargingType) ? "" : OrderType.Find(i => i.ID == byte.Parse(item.OrderType)).Name;
                        item.PosessionType = string.IsNullOrEmpty(item.PosessionType) ? "" : PossessionType.Find(i => i.ID == byte.Parse(item.PosessionType)).Name;
                        item.RequestPaymentType = string.IsNullOrEmpty(item.RequestPaymentType) ? "" : PaymentType.Find(i => i.ID == byte.Parse(item.RequestPaymentType)).Name;
                        item.PersianInstallationDate = item.InstallationDate.HasValue ? Helper.GetPersianDate(item.InstallationDate, Helper.DateStringType.Short) : "";
                        item.PersianInsertDate = item.InsertDate.HasValue ? Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short) : "";
                        item.Report_Date = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                        item.Report_Time = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                    }
                    List<RequestPaymentReport> result_Temp = ReportDB.GetRequestPayment(new List<long> { RequestID }, null, null, new List<int> { }, 0);
                    foreach (RequestPaymentReport item in result_Temp)
                    {
                        item.PersianFicheDate = (item.FicheDate.HasValue) ? Helper.GetPersianDate(item.FicheDate, Helper.DateStringType.Short) : "";
                    }

                    SendToPrint(result, result_Temp);

                    break;

                case (int)DB.RequestType.ChangeName:

                    result = ReportDB.GetChangeNameInfo(new List<int> { }, new List<int> { }, null, null, RequestID, null);

                    SendToChangeNamePrint(result);
                    break;
                case (int)DB.RequestType.RefundDeposit:
                    {
                        result = ReportDB.GetRefundDepositInfos(null, null, new List<int>(), new List<int>(), new List<int>(), -1, this.RequestID);
                        if (!result.IsNullOrEmpty())
                        {
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.RefundDepositCertificateReport);
                        }
                        else
                        {
                            MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    }
                case (int)DB.RequestType.ChangeLocationCenterInside:

                    resultChangeLocation = ReportDB.GetChangeLocationCenterInsideInfo(null, null, new List<long?> { RequestID }, null, null);

                    CounterLastInfo CounterLastInfo = ReportDB.GetLastCounterInfo(null, null, new List<long?> { RequestID }, null, null);
                    InstallRequestInfo InstallRequest = ReportDB.GetLastInstallrequestInfo(null, null, new List<long?> { RequestID }, null, null);

                    if (CounterLastInfo != null)
                    {
                        resultChangeLocation.CounterLocal = CounterLastInfo.Local.ToString();
                        resultChangeLocation.CounterBistalk = CounterLastInfo.BisTalk.ToString();
                        resultChangeLocation.CounterIA = CounterLastInfo.IA.ToString();
                        resultChangeLocation.CounterInternational = CounterLastInfo.International.ToString();
                        resultChangeLocation.CounterNonLocal = CounterLastInfo.NonLocal.ToString();


                    }
                    else
                    {
                        resultChangeLocation.CounterLocal = null;
                        resultChangeLocation.CounterBistalk = null;
                        resultChangeLocation.CounterIA = null;
                        resultChangeLocation.CounterInternational = null;
                        resultChangeLocation.CounterNonLocal = null;


                    }
                    if (InstallRequest != null)
                    {
                        resultChangeLocation.DayeriDate = InstallRequest.InstallationDate;
                        resultChangeLocation.TelephoneType = InstallRequest.TelephoneType.ToString();
                        resultChangeLocation.TelephoneTypeGroup = InstallRequest.TelephoneTypeGroup.ToString();

                    }
                    else
                    {
                        resultChangeLocation.DayeriDate = null;
                        resultChangeLocation.TelephoneType = null;
                        resultChangeLocation.TelephoneTypeGroup = null;

                    }

                    SendToPrintChangeLocationCenterInside(resultChangeLocation);
                    break;
                //TODO: rad
                case (int)DB.RequestType.ChangeLocationCenterToCenter:

                    Data.ChangeLocation changeLocation = ChangeLocationDB.GetChangeLocationByRequestID((long)RequestID);
                    resultChangeLocation = ReportDB.GetChangeLocationCenterToCenterInfo(null, null, new List<long?> { RequestID }, null, null);
                    //درخواست در مبدا

                    CounterLastInfo CounterLastCenterToCenterInfo = ReportDB.GetLastCounterInfo(null, null, new List<long?> { RequestID }, null, null);
                    InstallRequestInfo InstallRequestCenterToCenter = ReportDB.GetLastInstallrequestInfo(null, null, new List<long?> { RequestID }, null, null);

                    if (CounterLastCenterToCenterInfo != null)
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        resultChangeLocation.CounterLocal = CounterLastCenterToCenterInfo.Local != null ? CounterLastCenterToCenterInfo.Local.ToString() : string.Empty;
                        resultChangeLocation.CounterBistalk = CounterLastCenterToCenterInfo.BisTalk != null ? CounterLastCenterToCenterInfo.BisTalk.ToString() : string.Empty;
                        resultChangeLocation.CounterIA = CounterLastCenterToCenterInfo.IA != null ? CounterLastCenterToCenterInfo.IA.ToString() : string.Empty;
                        resultChangeLocation.CounterInternational = CounterLastCenterToCenterInfo.International != null ? CounterLastCenterToCenterInfo.International.ToString() : string.Empty;
                        resultChangeLocation.CounterNonLocal = CounterLastCenterToCenterInfo.NonLocal != null ? CounterLastCenterToCenterInfo.NonLocal.ToString() : string.Empty;

                        //}
                    }
                    else
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        resultChangeLocation.CounterLocal = null;
                        resultChangeLocation.CounterBistalk = null;
                        resultChangeLocation.CounterIA = null;
                        resultChangeLocation.CounterInternational = null;
                        resultChangeLocation.CounterNonLocal = null;

                        //}
                    }
                    if (InstallRequestCenterToCenter != null)
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        //TODO: rad question from milad
                        //علت این سه خط
                        //resultChangeLocation.DayeriDate = InstallRequestCenterToCenter.InstallationDate;
                        //resultChangeLocation.TelephoneType = InstallRequestCenterToCenter.TelephoneType.ToString();
                        //resultChangeLocation.TelephoneTypeGroup = InstallRequestCenterToCenter.TelephoneTypeGroup.ToString();
                        //}
                    }
                    else
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        //resultChangeLocation.DayeriDate = null;
                        //resultChangeLocation.TelephoneType = null;
                        //resultChangeLocation.TelephoneTypeGroup = null;
                        //}
                    }
                    SendToPrintChangeLocationCenterToCenter(resultChangeLocation);
                    break;

                case (int)DB.RequestType.Dischargin:
                    //List<int> Temp = new List<int>();
                    //List<InstallRequestInfo> TakePossessionInfo = ReportDB.GetDayeriInfo(Temp,
                    //                                                      Temp,
                    //                                                      0,
                    //                                                      0 , null);

                    result = ReportDB.GetTakePossessionInfo(new List<int> { }, new List<int> { }, null, null, 0, 0, new List<int> { }, new List<int> { }, new List<int> { }, new List<long?> { RequestID });


                    SendToPrintTakePossession(result);
                    break;

                case (int)DB.RequestType.Reinstall:

                    result = ReportDB.GetReInstallProcessReport(null, null, null, new List<long> { RequestID }, new List<int> { }, null, null, null);
                    List<EnumItem> ReInstallChargingGroup = Helper.GetEnumItem(typeof(DB.ChargingGroup));
                    List<EnumItem> ReInstallOrderType = Helper.GetEnumItem(typeof(DB.OrderType));
                    List<EnumItem> ReInstallPossessionType = Helper.GetEnumItem(typeof(DB.PossessionType));
                    List<EnumItem> ReInstallPaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));

                    foreach (InstallRequestReport item in result)
                    {
                        item.ChargingType = string.IsNullOrEmpty(item.ChargingType) ? "" : ReInstallChargingGroup.Find(i => i.ID == byte.Parse(item.ChargingType)).Name;
                        item.OrderType = string.IsNullOrEmpty(item.ChargingType) ? "" : ReInstallOrderType.Find(i => i.ID == byte.Parse(item.OrderType)).Name;
                        item.PosessionType = string.IsNullOrEmpty(item.PosessionType) ? "" : ReInstallPossessionType.Find(i => i.ID == byte.Parse(item.PosessionType)).Name;
                        item.RequestPaymentType = string.IsNullOrEmpty(item.RequestPaymentType) ? "" : ReInstallPaymentType.Find(i => i.ID == byte.Parse(item.RequestPaymentType)).Name;
                        item.PersianInstallInsertDate = item.InstallInsertDate.HasValue ? Helper.GetPersianDate(item.InstallInsertDate, Helper.DateStringType.Short) : "";
                        item.PersianInstallationDate = item.InstallationDate.HasValue ? Helper.GetPersianDate(item.InstallationDate, Helper.DateStringType.Short) : "";
                        item.PersianInsertDate = item.InsertDate.HasValue ? Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short) : "";
                        item.Report_Date = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                        item.Report_Time = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();


                    }
                    List<RequestPaymentReport> ReInstallresult_Temp = ReportDB.GetRequestPayment(new List<long> { RequestID }, null, null, null, 0);
                    foreach (RequestPaymentReport item in ReInstallresult_Temp)
                    {
                        item.PersianFicheDate = (item.FicheDate.HasValue) ? Helper.GetPersianDate(item.FicheDate, Helper.DateStringType.Short) : "";
                    }

                    SendToReInstallPrint(result, ReInstallresult_Temp);

                    break;

                case (int)DB.RequestType.ChangeNo:
                    result = ReportDB.GetChangeNoInfoPrintCertification(new List<long> { RequestID });

                    //foreach (ChangeNoInfo item in result)
                    //{
                    //    item.PersianChangeNoDate = (item.ChangeNoDate.HasValue) ? Helper.GetPersianDate(item.ChangeNoDate, Helper.DateStringType.Short) : "";
                    //}

                    List<RequestPaymentReport> ChangeNoresult_RquestPayment = ReportDB.GetRequestPayment(new List<long> { RequestID }, null, null, new List<int> { }, 0);


                    SendToChangeNoPrint(result, ChangeNoresult_RquestPayment);

                    break;

                case (int)DB.RequestType.CutAndEstablish:

                    result = ReportDB.GetCutAndEstablishInfos(null, null, new List<int>(), new List<int>(), -1, this.RequestID, false);
                    SendToCutPrint(result);
                    break;

                case (int)DB.RequestType.Connect:

                    result = ReportDB.GetCutAndEstablishInfos(null, null, new List<int>(), new List<int>(), -1, this.RequestID, true);
                    SendToEstablishPrint(result);
                    break;

                case (int)DB.RequestType.SpecialService:
                    long? TelephoneNo = RequestDB.GetTeleponeNoByRequestID(RequestID);
                    IEnumerable ResultSpecialService = ReportDB.GetSpecialServiceTypeListPrintCertificationInfo(TelephoneNo);
                    TelephoneSpecialServiceTypeInfo allInfoResult = ReportDB.GetSpecialServicePrintCertificationInfo(TelephoneNo);


                    SendToSpecialServicePrint(allInfoResult, ResultSpecialService);

                    break;

                case (int)DB.RequestType.OpenAndCloseZero:

                    result = ReportDB.GetZeroStatusReportPrintCertificationInfo(RequestID);
                    SendToOpenAndClosePrint(result);
                    break;

                case (int)DB.RequestType.ChangeAddress:
                    result = ReportDB.GetChangeAddressInfos(null, null, new List<int>(), new List<int>(), -1, this.RequestID);
                    if (!result.IsNullOrEmpty())
                    {
                        CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeAddressCertificateReport);
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;

                case (int)DB.RequestType.PBX:

                    TelephonePBXInfo ResultPBX = ReportDB.GetPBXPrintCertificationInfo(RequestID);
                    result = ReportDB.GetPBXOtherTelNoListPrintCertificationInfo(Telephone.TelephoneNo);

                    SendToPBXPrint(ResultPBX, result);
                    break;
                case (byte)DB.RequestType.SpecialWireOtherPoint:
                case (byte)DB.RequestType.SpecialWire:

                    var res = ReportDB.GetSpecialWireCertificatePrint(new List<int> { }, new List<int> { }, new List<long?> { _Request.ID }, null, null, null, null, null);

                    res.ForEach(t => t.SpecialType = ((t.SpecialType == "0") ? "" : AllSpecialWireType.Find(i => i.ID == Convert.ToByte(t.SpecialType)).Name));


                    SendPrint(res, DB.UserControlNames.SpecialWireCertificatePrintReport, "گواهی سیم خصوصی ");
                    break;
                case (byte)DB.RequestType.VacateSpecialWire:
                    SendPrint(ReportDB.GetVacateSpecialwireCertificateInfo(null, null, new List<int> { }, new List<int> { }, new List<long?> { _Request.ID }), DB.UserControlNames.VacateSpecialWireCertificate, "گواهی تخلیه سیم خصوصی ");

                    break;

                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                    SendPrint(ReportDB.GetChangeLocationSpecialwireCertificateInfo(null, null, new List<int> { }, new List<int> { }, new List<long?> { _Request.ID }), DB.UserControlNames.ChangeLocationSpecialWireCertificate, "گواهی تغییر مکان سیم خصوصی ");

                    break;
                case (byte)DB.RequestType.E1:
                    {
                        //TODO:rad
                        result = ReportDB.SearchE1CertificateByRequestID(_Request.ID);
                        CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.E1Certificate, dateVariable);
                        break;
                    }
                case (byte)DB.RequestType.SpaceandPower:
                    {
                        //TODO:rad
                        //CustomerReportInfo customer = new CustomerReportInfo();
                        //List<TelecomminucationServicePaymentReportInfo> telecomminucationServicePaymentReportInfo = ReportDB.SearchTelecomminucationServicePayments(_Request.ID, out customer, _Request.RequestTypeID);
                        //List<CustomerReportInfo> customers = new List<CustomerReportInfo>() { customer };

                        //Dictionary<string, IEnumerable> dictionary = new Dictionary<string, IEnumerable>();
                        //dictionary.Add("Customer", customers);
                        //dictionary.Add("TelecomminucationServicePayment", telecomminucationServicePaymentReportInfo);

                        //CRM.Application.Local.ReportBase.SendToPrint(297, dictionary, dateVariable);

                        //List<cat> cats = new List<cat>() {
                        //    new cat {  ID = 1 ,  CategoryName = "A"},
                        //    new cat {  ID = 2 ,  CategoryName = "A"},
                        //};

                        //List<subcat> subcats = new List<subcat>() {
                        //    new subcat { UnitPrice = 1 , Discontinued = false,  CategoryID = 1 ,  ProductName = "A1"},
                        //    new subcat { UnitPrice = 2 , Discontinued = false, CategoryID = 1  , ProductName = "A1"},
                        //    new subcat { UnitPrice = 3 , Discontinued = false, CategoryID = 2  , ProductName = "A3"}
                        //};

                        //StiReport report = new StiReport();


                        ////report.Load(@"C:\Users\ali\Documents\Visual Studio 2013\Projects\StimulSamples\StimulSamples\SubReportSample2.mrt" );

                        ////using(MainDataContext context = new MainDataContext())
                        ////{
                        ////    var x = context.ReportTemplates.Where(t=>t.ID == 298).SingleOrDefault();
                        ////    x.Template = report.SaveToByteArray();
                        ////    context.SubmitChanges();
                        ////}


                        //string path = ReportDB.GetReportPath(298);
                        //report.Load(path);
                        //report.RegData("Categories", "Categories", cats);
                        //report.RegData("Products", "Products", subcats);
                        //report.CacheAllData = true;
                        //report.ShowWithWpf();
                        //break;


                        //TODO:rad
                        //  CRM.Data.ReportDB.cat catI = new ReportDB.cat();
                        // List<CRM.Data.ReportDB.subcat> subcats = ReportDB.SearchTelecomminucationServicePayments2(_Request.ID, out catI, _Request.RequestTypeID);
                        // List<CRM.Data.ReportDB.cat> cats = new List<CRM.Data.ReportDB.cat> { catI };


                        //List<cat> cats = new List<cat>() {
                        //    new cat {  ID = 1 ,  CategoryName = "A"},
                        //    new cat {  ID = 2 ,  CategoryName = "A"},
                        //};

                        //List<subcat> subcats = new List<subcat>() {
                        //    new subcat { UnitPrice = 1 , Discontinued = false,  CategoryID = 1 ,  ProductName = "A1"},
                        //    new subcat { UnitPrice = 2 , Discontinued = false, CategoryID = 1  , ProductName = "A1"},
                        //    new subcat { UnitPrice = 3 , Discontinued = false, CategoryID = 2  , ProductName = "A3"}
                        //};

                        CustomerReportInfo customer = new CustomerReportInfo();
                        List<TelecomminucationServicePaymentReportInfo> telecomminucationServicePaymentReportInfo = TelecomminucationServicePaymentDB.SearchTelecomminucationServicePayments(_Request.ID, out customer, _Request.RequestTypeID);
                        List<CustomerReportInfo> customers = new List<CustomerReportInfo>() { customer };

                        StiReport report = new StiReport();
                        string path = ReportDB.GetReportPath((int)DB.UserControlNames.TelecomminucationServicePaymentReport);
                        report.Load(path);
                        report.RegData("Categories", "Categories", customers);
                        report.RegData("Products", "Products", telecomminucationServicePaymentReportInfo);
                        report.Dictionary.Variables["ReportDate"].ValueObject = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                        report.CacheAllData = true;
                        ReportViewerForm reportViewer = new ReportViewerForm(report);
                        reportViewer.ShowDialog();
                        break;
                    }
                default:
                    Folder.MessageBox.ShowInfo("گزارش مربوط به این روال در دست تهیه می باشد.");
                    break;
            }

            //switch (_Request.RequestTypeID)
            //{
            //    case (byte)DB.RequestType.SpecialWireOtherPoint:

            //    case (byte)DB.RequestType.SpecialWire:

            //        var res = ReportDB.GetSpecialWireCertificatePrint(new List<long> { _Request.ID }, null,null,null);

            //        res.ForEach(t => t.SpecialType = ((t.SpecialType == "0") ? "" : AllSpecialWireType.Find(i => i.ID == Convert.ToByte(t.SpecialType)).Name));


            //        SendPrint(res, DB.UserControlNames.SpecialWireCertificatePrintReport, "گواهی سیم خصوصی ");
            //        break;
            //}
        }


        private void SendPrint(IEnumerable objresult, DB.UserControlNames ReportID, string HeaderTitle)
        {
            if (objresult != null)
            {
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                string path = ReportDB.GetReportPath((int)ReportID);
                stiReport.Load(path);


                stiReport.CacheAllData = true;
                stiReport.RegData("result", "result", objresult);
                stiReport.Dictionary.Variables.Add("HeaderTitle", HeaderTitle);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("اطلاعاتی برای نمایش وجود ندارد");
            }
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            AddActionLog();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            //{
            //    CustomerNameTextBox.Text = string.Empty;

            //    if (DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", NationalCodeTextBox.Text.Trim()).Count > 0)
            //        Customer = DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", NationalCodeTextBox.Text.Trim())[0];
            //    if (Customer != null)
            //    {
            //        Request.CustomerID = Customer.ID;
            //        CustomerNameTextBox.Text = string.Empty;
            //        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
            //    }

            //    else
            //    {
            //        CustomerForm customerForm = new CustomerForm();
            //        customerForm.ShowDialog();
            //        if (customerForm.DialogResult ?? false)
            //        {
            //            Customer = Data.CustomerDB.GetCustomerByID( customerForm.ID)[0];
            //            Request.CustomerID = Customer.ID;
            //            CustomerNameTextBox.Text = string.Empty;
            //            CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
            //        }
            //    }
            //}
            //else
            //{
            //    CustomerForm customerForm = new CustomerForm();
            //    customerForm.ShowDialog();
            //    if (customerForm.DialogResult ?? false)
            //    {
            //        Customer = DB.GetEntitybyID<Customer>(customerForm.ID);
            //        Request.CustomerID = Customer.ID;
            //        CustomerNameTextBox.Text = string.Empty;
            //        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
            //    }
            //}
        }

        private void RequestTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (RequestType.ID)
            {
                case (int)DB.RequestType.Dayri:
                    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                    _installdetail = new Install(_Request.ID);
                    RequestDetail.Content = _installdetail;
                    RequestDetail.DataContext = _installdetail;
                    RequestDetail.Visibility = Visibility.Visible;

                    break;

                case (int)DB.RequestType.ChangeLocationCenterInside:
                    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                    if (Customer != null)
                        _ChangeLocation = new UserControls.ChangeLocationUserControl(_Request.ID, Customer.ID, 0, RequestType.ID);
                    else
                        _ChangeLocation = new UserControls.ChangeLocationUserControl(0, 0, 0, RequestType.ID);
                    RequestDetail.Content = _ChangeLocation;
                    RequestDetail.DataContext = _ChangeLocation;
                    RequestDetail.Visibility = Visibility.Visible;

                    break;

                case (int)DB.RequestType.Dischargin:
                    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                    if (Customer != null)
                        _DischargeTelephone = new UserControls.DischargeTelephonUserControl(_Request.ID, Customer.ID, 0);
                    else
                        _DischargeTelephone = new UserControls.DischargeTelephonUserControl(0, 0, 0);
                    RequestDetail.Content = _DischargeTelephone;
                    RequestDetail.DataContext = _DischargeTelephone;
                    RequestDetail.Visibility = Visibility.Visible;
                    break;

                case (int)DB.RequestType.ChangeName:
                    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                    if (Customer != null)
                        _ChangeName = new UserControls.ChangeName(_Request.ID, Customer.ID, 0);
                    else
                        _ChangeName = new UserControls.ChangeName(0, 0, 0);
                    RequestDetail.Content = _ChangeName;
                    RequestDetail.DataContext = _ChangeName;
                    RequestDetail.Visibility = Visibility.Visible;
                    break;

                case (int)DB.RequestType.CutAndEstablish:
                    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                    if (Customer != null)
                        _CutAndEstablish = new UserControls.CutAndEstablish(_Request.ID, Customer.ID, 0, (int)DB.RequestType.CutAndEstablish);
                    else
                        _CutAndEstablish = new UserControls.CutAndEstablish(0, 0, 0, (int)DB.RequestType.CutAndEstablish);
                    RequestDetail.Content = _CutAndEstablish;
                    RequestDetail.DataContext = _CutAndEstablish;
                    RequestDetail.Visibility = Visibility.Visible;
                    break;

                case (int)DB.RequestType.SpecialService:
                    RequestDetail.Header = string.Format("جزئیات درخواست {0}", RequestType.Title);
                    if (Customer != null)
                        _SpecialServiceUserControl = new UserControls.SpecialService(_Request.ID, 0);
                    else
                        _SpecialServiceUserControl = new UserControls.SpecialService(0, 0);
                    RequestDetail.Content = _SpecialServiceUserControl;
                    RequestDetail.DataContext = _SpecialServiceUserControl;
                    RequestDetail.Visibility = Visibility.Visible;
                    break;

                default:
                    RequestDetail.Content = null;
                    RequestDetail.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);

                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);

                (this.RequestInfo.DataContext as Request).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
                CenterComboBox.SelectedItem = (CenterComboBox.Items[0] as CenterInfo);
                CenterComboBox_SelectionChanged_1(null, null);

                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        _ChangeLocation.TargetCityID = city.ID;
                        break;
                    case (int)DB.RequestType.SpecialWire:
                        _SpecialWireUserControl.TargetCityID = city.ID;
                        break;
                }
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(CityID);

                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);

                    switch (RequestType.ID)
                    {
                        case (int)DB.RequestType.ChangeLocationCenterToCenter:
                            _ChangeLocation.TargetCityID = city.ID;
                            break;
                        case (int)DB.RequestType.SpecialWire:
                            _SpecialWireUserControl.TargetCityID = city.ID;
                            break;
                    }
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID); ;

                    switch (RequestType.ID)
                    {
                        case (int)DB.RequestType.ChangeLocationCenterToCenter:
                            _ChangeLocation.TargetCityID = city.ID;
                            break;
                        case (int)DB.RequestType.SpecialWire:
                            _SpecialWireUserControl.TargetCityID = city.ID;
                            break;
                    }
                }
            }
        }

        private void DocsItemInsert(object sender, RoutedEventArgs e)
        {
            if (RequestDocGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer docInfo = RequestDocGrid.SelectedItem as DocumentsByCustomer;
                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.TypeID && t.AnnounceID == docInfo.AnnounceID && t.DocumentTypeID == docInfo.DocumentTypeID).Take(1).SingleOrDefault();

                if (doclist != null) { MessageBox.Show("فایل پیوست شده موجود می باشد"); return; }
                if (docInfo == null) return;

                RequestDocumentForm window;

                if (doclist != null && doclist.RequestDocumentID != null)
                    window = new RequestDocumentForm(doclist.RequestDocumentID, 1);
                else
                    window = new RequestDocumentForm(_Request.CustomerID, docInfo.DocumentRequestTypeID, docInfo.TypeID, _Request.ID);

                window.ShowDialog();
                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();
                RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 1).ToList();
                this.RequestDocGrid.Items.Refresh();
            }

        }

        private void FormItemInsert(object sender, RoutedEventArgs e)
        {
            if (RequestFormDataGrid.SelectedIndex >= 0)
            {
                Data.FormTemplate forminfo = new Data.FormTemplate();
                forminfo = RequestFormDataGrid.SelectedItem as Data.FormTemplate;
                UsedForms Usedformlist = DocumentRequestTypeDB.GetUsedForms().Where(t => t.RequestID == _Request.ID && t.FormID == forminfo.ID).Take(1).SingleOrDefault();

                if (Usedformlist != null) { MessageBox.Show("فایل پیوست شده موجود می باشد"); return; }

                DocumentInputForm window = new DocumentInputForm(forminfo.ID, 1, 1, _Request.ID);

                //window.ShowDialog();
                //var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                //refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();
                //RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 1).ToList();
                //this.RequestDocGrid.Items.Refresh();
            }

        }

        private void DocsItemEdit(object sender, RoutedEventArgs e)
        {
            if (RequestDocGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer docInfo = RequestDocGrid.SelectedItem as DocumentsByCustomer;

                if (docInfo == null) return;
                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.TypeID && t.AnnounceID == docInfo.AnnounceID && t.DocumentTypeID == docInfo.DocumentTypeID).Take(1).SingleOrDefault();
                if (doclist != null)
                {
                    RequestDocumentForm window = new RequestDocumentForm(doclist.RequestDocumentID, 1);
                    window.ShowDialog();
                }
                else
                    ShowErrorMessage("مدرکی یافت نشد", new InvalidOperationException());

                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();
                RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == (byte)DB.DocumentType.Document).ToList();
                this.RequestDocGrid.Items.Refresh();
            }
        }

        private void DocsItemDelete(object sender, RoutedEventArgs e)
        {
            if (RequestDocGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer docInfo = RequestDocGrid.SelectedItem as DocumentsByCustomer;
                if (docInfo == null) return;
                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.TypeID && t.AnnounceID == docInfo.AnnounceID && t.DocumentTypeID == docInfo.DocumentTypeID).Take(1).SingleOrDefault();
                RequestDocument rd = DB.GetEntitybyID<RequestDocument>(doclist.RequestDocumentID);
                RequestDocumnetDB.DeleteRequestDocument(rd, _Request.ID);
                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }

                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();
                RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 1).ToList();
                this.RequestDocGrid.Items.Refresh();
            }
        }

        private void PermissionItemInsert(object sender, RoutedEventArgs e)
        {
            if (RequestPermissionGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer docInfo = RequestPermissionGrid.SelectedItem as DocumentsByCustomer;
                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.TypeID && t.AnnounceID == docInfo.AnnounceID && t.DocumentTypeID == docInfo.DocumentTypeID).Take(1).SingleOrDefault();
                if (docInfo == null) return;
                RequestDocumentForm window;
                if (doclist != null && doclist.RequestDocumentID != null)
                    window = new RequestDocumentForm(doclist.RequestDocumentID, 2);
                else
                    window = new RequestDocumentForm(_Request.CustomerID, docInfo.DocumentRequestTypeID, docInfo.TypeID, _Request.ID);

                window.ShowDialog();
                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();

                RequestPermissionGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
                this.RequestPermissionGrid.Items.Refresh();
            }
        }

        private void PermissionItemEdit(object sender, RoutedEventArgs e)
        {
            if (RequestPermissionGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer docInfo = RequestPermissionGrid.SelectedItem as DocumentsByCustomer;
                if (docInfo == null) return;
                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.TypeID && t.AnnounceID == docInfo.AnnounceID && t.DocumentTypeID == docInfo.DocumentTypeID).Take(1).SingleOrDefault();
                RequestDocumentForm window = new RequestDocumentForm(doclist.RequestDocumentID, 2);
                window.ShowDialog();
                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();

                RequestPermissionGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
                this.RequestPermissionGrid.Items.Refresh();
            }
        }

        private void PermissionItemDelete(object sender, RoutedEventArgs e)
        {
            if (RequestPermissionGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer docInfo = RequestPermissionGrid.SelectedItem as DocumentsByCustomer;
                if (docInfo == null) return;

                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.TypeID && t.AnnounceID == docInfo.AnnounceID && t.DocumentTypeID == docInfo.DocumentTypeID).Take(1).SingleOrDefault();
                RequestDocument rd = DB.GetEntitybyID<RequestDocument>(doclist.RequestDocumentID);
                RequestDocumnetDB.DeleteRequestDocument(rd, _Request.ID);
                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();

                RequestPermissionGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
                this.RequestPermissionGrid.Items.Refresh();
            }
        }

        private void RelatedRequestItemInsert(object sender, RoutedEventArgs e)
        {
            RequestForm window = new RequestForm(_Request.ID, Customer.ID);
            window.ShowDialog();
            RelatedRequestsGrid.ItemsSource = DB.GetAllEntity<Request>().Where(t => t.RelatedRequestID == _Request.ID).ToList();
            this.RelatedRequestsGrid.Items.Refresh();
        }

        private void ContractItemInsert(object sender, RoutedEventArgs e)
        {
            if (RequestContractGrid.SelectedIndex >= 0)
            {
                try
                {
                    ContractForm windowContract;
                    TelRoundSaleForm windowSale;

                    DocumentsByCustomer contractInfo = RequestContractGrid.SelectedItem as DocumentsByCustomer;
                    UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t =>
                                                                                      t.RequestID == _Request.ID &&
                                                                                      t.DocumentRequestTypeID == contractInfo.DocumentRequestTypeID &&
                                                                                      t.CustomerID == _customerID &&
                                                                                      t.TypeID == (byte)contractInfo.TypeID &&
                                                                                      t.AnnounceID == contractInfo.AnnounceID &&
                                                                                      t.DocumentTypeID == contractInfo.DocumentTypeID
                                                                                  ).Take(1).SingleOrDefault();
                    Contract contract = new Contract();

                    if (doclist != null)
                        contract = Data.ContractDB.GetContractsByRequestID(_Request.ID).Where(t => t.RequestDocumentID == doclist.RequestDocumentID).SingleOrDefault();
                    if (contract.ID != 0 && contractInfo.IsRelatedToRoundContract == true)
                    {
                        windowSale = new TelRoundSaleForm(contract.ID);
                        windowSale.ShowDialog();
                    }
                    else if (contract.ID == 0 && contractInfo.IsRelatedToRoundContract == true)
                    {
                        windowSale = new TelRoundSaleForm(_Request);
                        windowSale.ShowDialog();
                    }
                    else if (contract.ID != 0 && contractInfo.IsRelatedToRoundContract == false)
                    {
                        windowContract = new ContractForm(contract.ID, contractInfo.DocumentRequestTypeID);
                        windowContract.ShowDialog();
                    }
                    else if (contract.ID == 0 && contractInfo.IsRelatedToRoundContract == false)
                    {
                        windowContract = new ContractForm(_Request, contractInfo.DocumentRequestTypeID);
                        windowContract.ShowDialog();
                    }

                    else if (contract == null && contractInfo.IsRelatedToRoundContract == false)
                    {
                        windowContract = new ContractForm(_Request, contractInfo.DocumentRequestTypeID);
                        windowContract.ShowDialog();
                    }
                    List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                    if (Customer != null)
                    {
                        NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                    }
                    else
                    {
                        NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                    }
                    refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();

                    RequestContractGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
                    this.RequestContractGrid.Items.Refresh();
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, "خطا در افزودن فایل قرارداد");
                    MessageBox.Show("خطا در افزودن فایل!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ContractItemEdit(object sender, RoutedEventArgs e)
        {
            if (RequestContractGrid.SelectedIndex >= 0)
            {
                DocumentsByCustomer contractInfo = RequestContractGrid.SelectedItem as DocumentsByCustomer;
                ContractForm windowContract;
                TelRoundSaleForm windowSale;

                var x = DocumentRequestTypeDB.GetUsedDocs();
                UsedDocs doclist = x.Where(t => t.RequestID == _Request.ID
                                                  && t.DocumentRequestTypeID == contractInfo.DocumentRequestTypeID
                                                  && t.CustomerID == _customerID
                                                  && t.TypeID == (byte)contractInfo.TypeID
                                                  && t.AnnounceID == contractInfo.AnnounceID
                                                  && t.DocumentTypeID == contractInfo.DocumentTypeID
                                            )
                                      .Take(1).SingleOrDefault();

                Contract contract = new Contract();
                if (doclist != null)
                    contract = Data.ContractDB.GetContractsByRequestIDAndRequestDocumentID(_Request.ID, doclist.RequestDocumentID);
                if (contract.ID != 0 && contractInfo.IsRelatedToRoundContract == true)
                {
                    windowSale = new TelRoundSaleForm(contract.ID);
                    windowSale.ShowDialog();
                }
                else if (contract.ID == 0 && contractInfo.IsRelatedToRoundContract == true)
                {
                    windowSale = new TelRoundSaleForm(_Request);
                    windowSale.ShowDialog();
                }
                else if (contract.ID != 0 && contractInfo.IsRelatedToRoundContract == false)
                {
                    windowContract = new ContractForm(contract.ID, contractInfo.DocumentRequestTypeID);
                    windowContract.ShowDialog();
                }
                else if (contract.ID == 0 && contractInfo.IsRelatedToRoundContract == false)
                {
                    windowContract = new ContractForm(_Request, contractInfo.DocumentRequestTypeID);
                    windowContract.ShowDialog();
                }

                List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                if (Customer != null)
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                }
                else
                {
                    NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                }
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();

                RequestContractGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
                this.RequestContractGrid.Items.Refresh();
            }
        }

        private void ContractItemDelete(object sender, RoutedEventArgs e)
        {
            if (RequestContractGrid.SelectedIndex >= 0)
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    DocumentsByCustomer contractInfo = RequestContractGrid.SelectedItem as DocumentsByCustomer;
                    if (contractInfo == null) return;
                    UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == contractInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == contractInfo.TypeID && t.AnnounceID == contractInfo.AnnounceID && t.DocumentTypeID == contractInfo.DocumentTypeID).Take(1).SingleOrDefault();
                    Contract contract = DB.SearchByPropertyName<Contract>("RequestID", _Request.ID).Where(t => t.RequestDocumentID == doclist.RequestDocumentID).SingleOrDefault();
                    ContractDB.DeleteRequestContract(contract);
                    List<DocumentsByCustomer> NeededDocs = new List<DocumentsByCustomer>();
                    if (Customer != null)
                    {
                        NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, Customer.PersonType);
                    }
                    else
                    {
                        NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, 2);
                    }
                    refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == Customer.ID).ToList();

                    RequestContractGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
                    this.RequestContractGrid.Items.Refresh();
                }
            }
        }

        private void InstallmentsItemEdit(object sender, RoutedEventArgs e)
        {
            //if (InstallmentsGrid.SelectedIndex >= 0)
            //{
            //    InstallmentForm windows = new InstallmentForm(RequestID, 0);
            //    windows.ShowDialog();
            //}
        }

        private void InstallmentsItemDelete(object sender, RoutedEventArgs e)
        {
            //if (InstallmentsGrid.SelectedIndex < 0 || InstallmentsGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        Installment item = InstallmentsGrid.SelectedItem as Installment;

            //        DB.Delete<Data.Installment>(item.ID);
            //        ShowSuccessMessage("تقسیط مورد نظر حذف شد");
            //        LoadData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف تقسیط", ex);
            //}
        }

        private void InstallmentsItemInsert(object sender, RoutedEventArgs e)
        {
            //if (InstallmentsGrid.Items.Count == 0)
            //{
            //    InstallmentForm windows = new InstallmentForm(RequestID, 1);
            //    windows.ShowDialog();
            //    EnableRequestDetails();
            //}
            //else
            //    MessageBox.Show("برای این درخواست قبلا تقسیط ذخیره شده است", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void RequestPaymentEdit(object sender, RoutedEventArgs e)
        {
            if (RequestPaymentDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.RequestPayment item = RequestPaymentDataGrid.SelectedItem as CRM.Data.RequestPayment;
                if (item == null) return;
                if (item.PaymentType != (int)DB.PaymentType.Cash)
                {
                    Folder.MessageBox.ShowError("فقط هزینه های نقدی قابل پرداخت هستند!");
                    return;
                }
                RequestPaymentForm window = new RequestPaymentForm(item.ID, RequestID, false);
                window.ShowDialog();

                RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
            }
        }

        private void InstallmentRequestPayment(object sender, RoutedEventArgs e)
        {
            if (RequestPaymentDataGrid.SelectedIndex >= 0)
            {
                Data.RequestPayment item = RequestPaymentDataGrid.SelectedItem as Data.RequestPayment;

                if (item != null)
                {
                    ADSLService service = null;
                    if (item.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID)
                    {
                        if (_Request.RequestTypeID == (int)DB.RequestType.Wireless)
                        {
                            if (_Request.TelephoneNo == null || _Request.TelephoneNo == 0)
                            {
                                Folder.MessageBox.ShowError("امکان اقساط برای درخواست های بدون مالک نمی باشد");
                                return;

                            }
                            service = ADSLServiceDB.GetADSLServiceById((int)_wirelessUserControl.ServiceComboBox.SelectedValue);
                        }
                        else
                        {
                            service = ADSLServiceDB.GetADSLServiceById((int)_ADSL.ServiceComboBox.SelectedValue);
                        }
                        if (service == null || service.IsInstalment == null || service.IsInstalment == false)
                        {
                            Folder.MessageBox.ShowError("نحوه پرداخت این سرویس به صورت نقدی می باشد !");
                            return;
                        }
                    }
                    else
                    {
                        if (item.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
                        {
                            if (_Request.RequestTypeID == (int)DB.RequestType.Wireless)
                            {
                                service = ADSLServiceDB.GetADSLServiceById((int)_wirelessUserControl.ServiceComboBox.SelectedValue);
                            }
                            else
                            {
                                service = ADSLServiceDB.GetADSLServiceById((int)_ADSL.ServiceComboBox.SelectedValue);
                            }
                            if (service == null || !service.IsModemInstallment)
                            {
                                Folder.MessageBox.ShowError("نحوه پرداخت این مودم برای سرویس مورد نظر به صورت نقدی می باشد !");
                                return;
                            }
                        }
                        else
                        {
                            if (item.PaymentType != (int)DB.PaymentType.Instalment)
                            {
                                Folder.MessageBox.ShowError("فقط هزینه های قسطی قابل قسط بندی هستند!");
                                return;
                            }
                        }
                    }

                    InstallmentRequestPaymentForm window = new InstallmentRequestPaymentForm(item.ID);
                    window.ShowDialog();

                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                }
            }
        }

        private void ChangeCashInstalment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RequestPaymentDataGrid.SelectedIndex >= 0)
                {
                    Data.RequestPayment item = RequestPaymentDataGrid.SelectedItem as Data.RequestPayment;

                    if (item != null)
                    {
                        if (item.IsPaid == true)
                            throw new Exception("پرداخت انجام شده است، امکان تغییر نحوه پرداخت وجود ندارد !");

                        if (item.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID || item.BaseCostID == BaseCostDB.GetServiceCostForADSLChangeService().ID)
                        {
                            if (item.PaymentType == (byte)DB.PaymentType.Instalment)
                            {
                                List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);

                                DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                item.PaymentType = (byte)DB.PaymentType.Cash;

                                item.Detach();
                                Save(item);

                                RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                                Folder.MessageBox.ShowError("لطفا فاکتور را مجددا صادر نمایید !");
                                return;

                            }

                            if (item.PaymentType == (byte)DB.PaymentType.Cash)
                            {
                                ADSLService service = ADSLServiceDB.GetADSLServiceById((int)_ADSL.ServiceComboBox.SelectedValue);
                                if (service.IsInstalment != null)
                                {
                                    if ((bool)service.IsInstalment)
                                    {
                                        int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                                        List<InstallmentRequestPayment> instalList = GenerateInstalments(true, item.ID, duration, (byte)DB.RequestType.ADSL, (long)service.Price, false);

                                        DB.SaveAll(instalList);

                                        item.PaymentType = (byte)DB.PaymentType.Instalment;
                                        item.BillID = null;
                                        item.PaymentID = null;

                                        item.Detach();
                                        Save(item);

                                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                                        Folder.MessageBox.ShowError("لطفا فاکتور را مجددا صادر نمایید !");
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (item.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
                            {
                                if (item.PaymentType == (byte)DB.PaymentType.Instalment)
                                {
                                    List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);

                                    DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                    item.PaymentType = (byte)DB.PaymentType.Cash;

                                    item.Detach();
                                    Save(item);

                                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                                    Folder.MessageBox.ShowError("لطفا فاکتور را مجددا صادر نمایید !");
                                    return;
                                }

                                if (item.PaymentType == (byte)DB.PaymentType.Cash)
                                {
                                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)_ADSL.ServiceComboBox.SelectedValue);
                                    if (service.IsModemInstallment != null)
                                    {
                                        if ((bool)service.IsModemInstallment)
                                        {
                                            int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                                            List<InstallmentRequestPayment> instalList = GenerateInstalments(true, item.ID, duration, (byte)DB.RequestType.ADSL, (long)item.Cost, false);

                                            DB.SaveAll(instalList);

                                            item.PaymentType = (byte)DB.PaymentType.Instalment;
                                            item.BillID = null;
                                            item.PaymentID = null;

                                            item.Detach();
                                            Save(item);

                                            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                                            Folder.MessageBox.ShowError("لطفا فاکتور را مجددا صادر نمایید !");
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Folder.MessageBox.ShowError("عملیات تبدیل نحوه پرداخت برای پرداخت مورد نظر امکان پذیر نمی باشد !");
                                return;
                            }
                        }

                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void RequestPaymentInsert(object sender, RoutedEventArgs e)
        {
            RequestPaymentForm window = new RequestPaymentForm(0, RequestID, true);
            window.ShowDialog();
            EnableRequestDetails();

            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
        }

        private void RelatedRequestItemDelete(object sender, RoutedEventArgs e)
        {
            if (RequestPaymentDataGrid.SelectedIndex < 0 || RequestPaymentDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    RequestPayment item = RequestPaymentDataGrid.SelectedItem as RequestPayment;

                    List<InstallmentRequestPayment> installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);
                    if (item.BaseCostID == (int)DB.SpecialCostID.PrePaymentTypeCostID)
                    {
                        Folder.MessageBox.ShowInfo("این هزینه پیش پرداخت اقساط می باشد حذف آن از طریق حذف اقساط انجام می شود.");
                    }
                    else if (installmentRequestPayments.Count != 0)
                    {
                        Folder.MessageBox.ShowInfo("این هزینه شامل اقساط می باشد لطفا ابتدا اقساط را حذف کنید.");
                    }
                    else if (item.IsPaid != null && (bool)item.IsPaid)
                    {
                        Folder.MessageBox.ShowInfo("این فیش پرداخت شده است، امکان حذف آن وجود ندارد");
                    }
                    else
                    {
                        DB.Delete<RequestPayment>(item.ID);
                    }
                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف دریافت/پرداخت، " + ex.Message + " !", ex);
            }
        }

        private void requestPaymentDelete(object sender, RoutedEventArgs e)
        {
            if (RequestPaymentDataGrid.SelectedIndex < 0 || RequestPaymentDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                if (DB.IsFixRequest(RequestType.ID))
                {
                    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        RequestPayment item = RequestPaymentDataGrid.SelectedItem as RequestPayment;

                        List<InstallmentRequestPayment> installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);
                        if (item.BaseCostID == (int)DB.SpecialCostID.PrePaymentTypeCostID)
                        {
                            Folder.MessageBox.ShowInfo("این هزینه پیش پرداخت اقساط می باشد حذف آن از طریق حذف اقساط انجام می شود.");
                        }
                        else if (installmentRequestPayments.Count != 0)
                        {
                            Folder.MessageBox.ShowInfo("این هزینه شامل اقساط می باشد لطفا ابتدا اقساط را حذف کنید.");
                        }
                        else
                        {
                            item.PaymentWay = null;
                            item.BankID = null;
                            item.FicheNunmber = string.Empty;
                            item.FicheDate = null;
                            item.PaymentDate = null;
                            item.UserID = DB.CurrentUser.ID;
                            item.IsPaid = false;
                            item.FactorNumber = null;

                            if (item.DocumentsFileID != null)
                                DocumentsFileDB.DeleteDocumentsFileTable((Guid)item.DocumentsFileID);

                            item.DocumentsFileID = null;
                            item.Detach();
                            Save(item);
                        }
                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف دریافت/پرداخت، " + ex.Message + " !", ex);
            }

        }

        private void PrintFactor(object sender, RoutedEventArgs e)
        {
            if (RequestID != 0)
            {
                if (DB.IsFixRequest(RequestType.ID))
                {
                    RequestPayment selectedRequestPayment = RequestPaymentDataGrid.SelectedItem as RequestPayment;
                    if (selectedRequestPayment != null && selectedRequestPayment.PaymentType == (int)DB.PaymentType.Instalment)
                    {
                        MessageBox.Show(".امکان صدور فاکتور برای هزینه های اقساط نمیباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    //milad doran
                    //SaleFactorForm window = new SaleFactorForm(RequestID, RequestPaymentDataGrid.SelectedItems.Cast<RequestPayment>().Where(t => t.IsKickedBack == false && (t.IsPaid == null || t.IsPaid == false)).ToList());

                    //TODO:rad 13950809
                    List<RequestPayment> selectedRequestPayments = RequestPaymentDataGrid.SelectedItems.Cast<RequestPayment>().Where(t => t.IsKickedBack == false && (t.IsPaid == null || t.IsPaid == false)).ToList();
                    if (selectedRequestPayments.Any(rp => rp.PaymentType == (byte)DB.PaymentType.Instalment))
                    {
                        MessageBox.Show(".امکان صدور فاکتور برای هزینه های اقساط نمیباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    //***************************************************************************************************
                    SaleFactorForm window = new SaleFactorForm(RequestID, selectedRequestPayments);
                    window.ShowDialog();

                }
                else
                {
                    SaleFactorForm window = new SaleFactorForm(RequestID);
                    window.ShowDialog();

                    RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                }
            }
        }

        private void requestPaymentKickBack(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DB.IsFixRequest(RequestType.ID))
                {

                    SaleFactorForm window = new SaleFactorForm(RequestID, RequestPaymentDataGrid.SelectedItems.Cast<RequestPayment>().Where(t => t.IsKickedBack == false && t.IsPaid == true).ToList(), true);
                    window.ShowDialog();

                }
                else if (RequestType.ID == (int)DB.RequestType.Wireless)
                {
                    SaleFactorForm window = new SaleFactorForm(RequestID, RequestPaymentDataGrid.SelectedItems.Cast<RequestPayment>().Where(t => t.IsKickedBack == false && t.IsPaid == true).ToList(), true);
                    window.ShowDialog();
                }

                //CRM.Data.RequestPayment item = RequestPaymentDataGrid.SelectedItem as CRM.Data.RequestPayment;
                //if (item == null) return;

                //if (item.IsPaid == null || item.IsPaid == false)
                //    throw new Exception("این فیش پرداخت نشده است");

                //item.IsKickedBack = true;

                //item.Detach();
                //Save(item);

                //RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در بازپرداخت دریافت/پرداخت، " + ex.Message + " !", ex);
            }
        }

        private void PaidFactor(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RequestID != 0)
                {
                    if (DB.IsFixRequest(RequestType.ID))
                    {
                        PaidFactorForm window = new PaidFactorForm(RequestID);
                        window.ShowDialog();

                        var statusBar = Helper.GetChildOfType<CRM.Application.UserControls.StatusBar>(this);
                        if (statusBar != null)
                        {
                            statusBar.HideMessage();
                        }
                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetAllRequestPaymentByRequestID(RequestID);
                    }
                    else
                    {
                        if (RequestPaymentDB.HasRequestPaymentBillIDbyRequestID(RequestID))
                            throw new Exception("لطفا ابتدا فاکتور را صادر نمایید!");

                        PaidFactorForm window = new PaidFactorForm(RequestID);
                        window.ShowDialog();

                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void RequestRefundItemDelete(object sender, RoutedEventArgs e)
        {

        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                switch (RequestType.ID)
                {
                    case (int)DB.RequestType.Dayri:
                    case (int)DB.RequestType.Reinstall:
                        if (_installdetail != null)
                            _installdetail.CenterID = (int)CenterComboBox.SelectedValue;
                        break;

                    case (int)DB.RequestType.SpecialWire:
                        if (_SpecialWireUserControl != null)
                            _SpecialWireUserControl.CenterID = (int)CenterComboBox.SelectedValue;
                        break;
                }
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (RequestDocGrid.SelectedItem != null)
            {
                DocumentsByCustomer RequestDocGridItem = RequestDocGrid.SelectedItem as DocumentsByCustomer;

                RequestDocument requestDocument = Data.RequestDocumnetDB.GetRequestDocument(_Request.ID, RequestDocGridItem.DocumentRequestTypeID);

                if (requestDocument != null)
                {
                    CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)requestDocument.DocumentsFileID);
                    FileBytes = fileInfo.Content;
                    CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                    window.FileBytes = FileBytes;
                    window.FileType = fileInfo.FileType;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("فایل موجود نمی باشد.");
                }
            }

            else if (RequestContractGrid.SelectedItem != null)
            {
                DocumentsByCustomer RequestContractGridItem = RequestContractGrid.SelectedItem as DocumentsByCustomer;

                RequestDocument requestDocument = Data.RequestDocumnetDB.GetRequestDocument(_Request.ID, RequestContractGridItem.DocumentRequestTypeID);

                if (requestDocument != null && requestDocument.DocumentsFileID != null)
                {
                    CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)requestDocument.DocumentsFileID);
                    FileBytes = fileInfo.Content;
                    CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                    window.FileBytes = FileBytes;
                    window.FileType = fileInfo.FileType;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("فایل موجود نمی باشد.");
                }
            }
        }

        private void PrintCertificate_Click(object sender, RoutedEventArgs e)
        {
            //            List<InstallRequestReport> result = new List<InstallRequestReport>();
            IEnumerable result;
            ChangeLocationCenterInfo resultChangeLocation;
            switch (RequestType.ID)
            {
                case (int)DB.RequestType.Dayri:

                    result = ReportDB.GetInstallProcessReport(null, null, null, new List<long> { RequestID }, new List<int> { }, null, null, null);
                    List<EnumItem> ChargingGroup = Helper.GetEnumItem(typeof(DB.ChargingGroup));
                    List<EnumItem> OrderType = Helper.GetEnumItem(typeof(DB.OrderType));
                    List<EnumItem> PossessionType = Helper.GetEnumItem(typeof(DB.PossessionType));
                    List<EnumItem> PaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));

                    foreach (InstallRequestReport item in result)
                    {
                        item.ChargingType = string.IsNullOrEmpty(item.ChargingType) ? "" : ChargingGroup.Find(i => i.ID == byte.Parse(item.ChargingType)).Name;
                        item.OrderType = string.IsNullOrEmpty(item.ChargingType) ? "" : OrderType.Find(i => i.ID == byte.Parse(item.OrderType)).Name;
                        item.PosessionType = string.IsNullOrEmpty(item.PosessionType) ? "" : PossessionType.Find(i => i.ID == byte.Parse(item.PosessionType)).Name;
                        item.RequestPaymentType = string.IsNullOrEmpty(item.RequestPaymentType) ? "" : PaymentType.Find(i => i.ID == byte.Parse(item.RequestPaymentType)).Name;
                        item.PersianInstallInsertDate = item.InstallInsertDate.HasValue ? Helper.GetPersianDate(item.InstallInsertDate, Helper.DateStringType.Short) : "";
                        item.PersianInsertDate = item.InsertDate.HasValue ? Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short) : "";
                        item.Report_Date = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                        item.Report_Time = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                    }
                    List<RequestPaymentReport> result_Temp = ReportDB.GetRequestPayment(new List<long> { RequestID }, null, null, new List<int> { }, 0);
                    foreach (RequestPaymentReport item in result_Temp)
                    {
                        item.PersianFicheDate = (item.FicheDate.HasValue) ? Helper.GetPersianDate(item.FicheDate, Helper.DateStringType.Short) : "";
                    }

                    SendToPrint(result, result_Temp);

                    break;

                case (int)DB.RequestType.ChangeName:

                    result = ReportDB.GetChangeNameInfo(new List<int> { }, new List<int> { }, null, null, RequestID, null);

                    SendToChangeNamePrint(result);
                    break;

                case (int)DB.RequestType.ChangeLocationCenterInside:

                    resultChangeLocation = ReportDB.GetChangeLocationCenterInsideInfo(null, null, new List<long?> { RequestID }, null, null);

                    CounterLastInfo CounterLastInfo = ReportDB.GetLastCounterInfo(null, null, new List<long?> { RequestID }, null, null);
                    InstallRequestInfo InstallRequest = ReportDB.GetLastInstallrequestInfo(null, null, new List<long?> { RequestID }, null, null);

                    if (CounterLastInfo != null)
                    {
                        resultChangeLocation.CounterLocal = CounterLastInfo.Local.ToString();
                        resultChangeLocation.CounterBistalk = CounterLastInfo.BisTalk.ToString();
                        resultChangeLocation.CounterIA = CounterLastInfo.IA.ToString();
                        resultChangeLocation.CounterInternational = CounterLastInfo.International.ToString();
                        resultChangeLocation.CounterNonLocal = CounterLastInfo.NonLocal.ToString();


                    }
                    else
                    {
                        resultChangeLocation.CounterLocal = null;
                        resultChangeLocation.CounterBistalk = null;
                        resultChangeLocation.CounterIA = null;
                        resultChangeLocation.CounterInternational = null;
                        resultChangeLocation.CounterNonLocal = null;


                    }
                    if (InstallRequest != null)
                    {
                        resultChangeLocation.DayeriDate = InstallRequest.InstallationDate;
                        resultChangeLocation.TelephoneType = InstallRequest.TelephoneType.ToString();
                        resultChangeLocation.TelephoneTypeGroup = InstallRequest.TelephoneTypeGroup.ToString();

                    }
                    else
                    {
                        resultChangeLocation.DayeriDate = null;
                        resultChangeLocation.TelephoneType = null;
                        resultChangeLocation.TelephoneTypeGroup = null;

                    }

                    SendToPrintChangeLocationCenterInside(resultChangeLocation);
                    break;

                case (int)DB.RequestType.ChangeLocationCenterToCenter:

                    Data.ChangeLocation changeLocation = ChangeLocationDB.GetChangeLocationByRequestID((long)RequestID);
                    resultChangeLocation = ReportDB.GetChangeLocationCenterToCenterInfo(null, null, new List<long?> { RequestID }, null, null);
                    //درخواست در مبدا

                    CounterLastInfo CounterLastCenterToCenterInfo = ReportDB.GetLastCounterInfo(null, null, new List<long?> { RequestID }, null, null);
                    InstallRequestInfo InstallRequestCenterToCenter = ReportDB.GetLastInstallrequestInfo(null, null, new List<long?> { RequestID }, null, null);

                    if (CounterLastCenterToCenterInfo != null)
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        resultChangeLocation.CounterLocal = CounterLastCenterToCenterInfo.Local != null ? CounterLastCenterToCenterInfo.Local.ToString() : string.Empty;
                        resultChangeLocation.CounterBistalk = CounterLastCenterToCenterInfo.BisTalk != null ? CounterLastCenterToCenterInfo.BisTalk.ToString() : string.Empty;
                        resultChangeLocation.CounterIA = CounterLastCenterToCenterInfo.IA != null ? CounterLastCenterToCenterInfo.IA.ToString() : string.Empty;
                        resultChangeLocation.CounterInternational = CounterLastCenterToCenterInfo.International != null ? CounterLastCenterToCenterInfo.International.ToString() : string.Empty;
                        resultChangeLocation.CounterNonLocal = CounterLastCenterToCenterInfo.NonLocal != null ? CounterLastCenterToCenterInfo.NonLocal.ToString() : string.Empty;

                        //}
                    }
                    else
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        resultChangeLocation.CounterLocal = null;
                        resultChangeLocation.CounterBistalk = null;
                        resultChangeLocation.CounterIA = null;
                        resultChangeLocation.CounterInternational = null;
                        resultChangeLocation.CounterNonLocal = null;

                        //}
                    }
                    if (InstallRequestCenterToCenter != null)
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        resultChangeLocation.DayeriDate = InstallRequestCenterToCenter.InstallationDate;
                        resultChangeLocation.TelephoneType = InstallRequestCenterToCenter.TelephoneType.ToString();
                        resultChangeLocation.TelephoneTypeGroup = InstallRequestCenterToCenter.TelephoneTypeGroup.ToString();
                        //}
                    }
                    else
                    {
                        //foreach (ChangeLocationCenterInfo info in result)
                        //{
                        resultChangeLocation.DayeriDate = null;
                        resultChangeLocation.TelephoneType = null;
                        resultChangeLocation.TelephoneTypeGroup = null;
                        //}
                    }
                    SendToPrintChangeLocationCenterToCenter(resultChangeLocation);
                    break;

                case (int)DB.RequestType.Dischargin:
                    //List<int> Temp = new List<int>();
                    //List<InstallRequestInfo> TakePossessionInfo = ReportDB.GetDayeriInfo(Temp,
                    //                                                      Temp,
                    //                                                      0,
                    //                                                      0 , null);

                    result = ReportDB.GetTakePossessionInfo(new List<int> { }, new List<int> { }, null, null, 0, 0, new List<int> { }, new List<int> { }, new List<int> { }, new List<long?> { RequestID });


                    SendToPrintTakePossession(result);
                    break;
                case (int)DB.RequestType.VacateSpecialWire:
                    result = ReportDB.GetVacateSpecialwireCertificateInfo(null, null, new List<int> { }, new List<int> { }, new List<long?> { RequestID });
                    SendToPrintVacateSpecialwire(result);
                    break;

                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    result = ReportDB.GetChangeLocationSpecialwireCertificateInfo(null, null, new List<int> { }, new List<int> { }, new List<long?> { RequestID });
                    SendToPrint(result, (int)DB.UserControlNames.ChangeLocationSpecialWireCertificate);
                    break;


                case (int)DB.RequestType.Reinstall:

                    result = ReportDB.GetReInstallProcessReport(null, null, null, new List<long> { RequestID }, new List<int> { }, null, null, null);
                    List<EnumItem> ReInstallChargingGroup = Helper.GetEnumItem(typeof(DB.ChargingGroup));
                    List<EnumItem> ReInstallOrderType = Helper.GetEnumItem(typeof(DB.OrderType));
                    List<EnumItem> ReInstallPossessionType = Helper.GetEnumItem(typeof(DB.PossessionType));
                    List<EnumItem> ReInstallPaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));

                    foreach (InstallRequestReport item in result)
                    {
                        item.ChargingType = string.IsNullOrEmpty(item.ChargingType) ? "" : ReInstallChargingGroup.Find(i => i.ID == byte.Parse(item.ChargingType)).Name;
                        item.OrderType = string.IsNullOrEmpty(item.ChargingType) ? "" : ReInstallOrderType.Find(i => i.ID == byte.Parse(item.OrderType)).Name;
                        item.PosessionType = string.IsNullOrEmpty(item.PosessionType) ? "" : ReInstallPossessionType.Find(i => i.ID == byte.Parse(item.PosessionType)).Name;
                        item.RequestPaymentType = string.IsNullOrEmpty(item.RequestPaymentType) ? "" : ReInstallPaymentType.Find(i => i.ID == byte.Parse(item.RequestPaymentType)).Name;
                        item.PersianInstallInsertDate = item.InstallInsertDate.HasValue ? Helper.GetPersianDate(item.InstallInsertDate, Helper.DateStringType.Short) : "";
                        item.PersianInsertDate = item.InsertDate.HasValue ? Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short) : "";
                        item.Report_Date = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                        item.Report_Time = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();


                    }
                    List<RequestPaymentReport> ReInstallresult_Temp = ReportDB.GetRequestPayment(new List<long> { RequestID }, null, null, null, 0);
                    foreach (RequestPaymentReport item in ReInstallresult_Temp)
                    {
                        item.PersianFicheDate = (item.FicheDate.HasValue) ? Helper.GetPersianDate(item.FicheDate, Helper.DateStringType.Short) : "";
                    }

                    SendToReInstallPrint(result, ReInstallresult_Temp);

                    break;

                case (int)DB.RequestType.ChangeNo:
                    result = ReportDB.GetChangeNoInfoPrintCertification(new List<long> { RequestID });

                    List<RequestPaymentReport> ChangeNoresult_RquestPayment = ReportDB.GetRequestPayment(new List<long> { RequestID }, null, null, null, 0);
                    foreach (RequestPaymentReport item in ChangeNoresult_RquestPayment)
                    {
                        item.PersianFicheDate = (item.FicheDate.HasValue) ? Helper.GetPersianDate(item.FicheDate, Helper.DateStringType.Short) : "";
                    }

                    SendToChangeNoPrint(result, ChangeNoresult_RquestPayment);

                    break;

                case (int)DB.RequestType.CutAndEstablish:

                    result = ReportDB.GetCutAndEstablishInfos(null, null, new List<int>(), new List<int>(), -1, this.RequestID, false);
                    SendToCutPrint(result);
                    break;

                case (int)DB.RequestType.Connect:

                    result = ReportDB.GetCutAndEstablishInfos(null, null, new List<int>(), new List<int>(), -1, this.RequestID, true);
                    SendToEstablishPrint(result);
                    break;

                case (int)DB.RequestType.SpecialService:
                    long? TelephoneNo = RequestDB.GetTeleponeNoByRequestID(RequestID);
                    IEnumerable ResultSpecialService = ReportDB.GetSpecialServiceTypeListPrintCertificationInfo(TelephoneNo);
                    TelephoneSpecialServiceTypeInfo allInfoResult = ReportDB.GetSpecialServicePrintCertificationInfo(TelephoneNo);


                    SendToSpecialServicePrint(allInfoResult, ResultSpecialService);

                    break;

                case (int)DB.RequestType.OpenAndCloseZero:

                    result = ReportDB.GetZeroStatusReportPrintCertificationInfo(RequestID);
                    SendToOpenAndClosePrint(result);
                    break;

                case (int)DB.RequestType.ChangeAddress:

                    result = ReportDB.GetChangeAddressInfos(null, null, new List<int>(), new List<int>(), -1, this.RequestID);

                    SendToChangeAddressPrint(result);
                    break;

                case (int)DB.RequestType.PBX:

                    TelephonePBXInfo ResultPBX = ReportDB.GetPBXPrintCertificationInfo(RequestID);
                    result = ReportDB.GetPBXOtherTelNoListPrintCertificationInfo(Telephone.TelephoneNo);

                    SendToPBXPrint(ResultPBX, result);
                    break;
                default:
                    Folder.MessageBox.ShowInfo("گزارش مربوط به این روال در دست تهیه می باشد.");
                    break;
            }



        }

        private void SendToPrint(IEnumerable result, int ReportTemplateID)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath(ReportTemplateID);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.InstallRequestForm);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintChangeLocationCenterInside(ChangeLocationCenterInfo result)
        {
            string path;
            string title = "";
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            if (string.IsNullOrEmpty(result.NewCustomerName))
            {
                path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationCenterInsideCertificateReport);
                stiReport.Load(path);
                title = "گواهی تغییر مکان داخل مرکز";
            }
            else if (!string.IsNullOrEmpty(result.NewCustomerName))
            {
                path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationAndNameCenterInsideCertificateReport);
                stiReport.Load(path);
                title = "گزارش تغییر مکان و نام در مرکز  ";
            }
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintChangeLocationCenterToCenter(ChangeLocationCenterInfo result)
        {
            string path;
            string title = "";

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            //fathi
            //if (result.NewCustomerName == null)
            //{
            //    path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationCenterTCenterReport);
            //    stiReport.Load(path);
            //    title = "گزارش تغییر مکان  مرکز به مرکز  ";
            //}
            if (!result.NewCustomerID.HasValue)
            {
                path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationCenterToCenterCertificateReport);
                stiReport.Load(path);
                title = "گواهی تغییر مکان مرکز به مرکز";
            }
            else if (result.NewCustomerID.HasValue)
            {
                path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationAndNameCenterToCenterCertificateReport);
                stiReport.Load(path);
                title = "گواهی تغییر مکان و نام مرکز به مرکز  ";
            }

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrint(IEnumerable result, IEnumerable DetailsResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.InstallRequestForm);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("DetailsResult", "DetailsResult", DetailsResult);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToReInstallPrint(IEnumerable result, IEnumerable DetailsResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ReInstallPrintCertificationReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("DetailsResult", "DetailsResult", DetailsResult);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintTakePossession(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.DischargeCertificateReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintVacateSpecialwire(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.VacateSpecialWireCertificate);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToChangeNoPrint(IEnumerable result, IEnumerable DetailsResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeNoCertificateReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("DetailsResult", "DetailsResult", DetailsResult);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToChangeNamePrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeNameCertificateReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToCutPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CutCertificateReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToEstablishPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.EstablishCertificateReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToSpecialServicePrint(TelephoneSpecialServiceTypeInfo result, IEnumerable SpecialResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.SpecialServicePrintCertification);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("SpecialServiceTypeResult", "SpecialServiceTypeResult", SpecialResult);
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToOpenAndClosePrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ZeroStatusPrintCertificationReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToChangeAddressPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeAddressCertificateReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPBXPrint(TelephonePBXInfo result, IEnumerable OtherTelResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TelephonePBXPrintCertificationReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("OthertelNo", "OtherTelNo", OtherTelResult);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void FormPrint(object sender, RoutedEventArgs e)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            Forms formList = new Forms();

            if (RequestFormDataGrid.SelectedIndex >= 0)
            {
                //Forms formInfo = RequestFormDataGrid.SelectedItem as Forms;

                //if (formInfo == null) return;
                //else
                //{
                formList = DocumentRequestTypeDB.GetForms().Where(t => t.RequestTypeID == _Request.RequestTypeID).Take(1).SingleOrDefault();
                //}
            }

            string path = ReportDB.GetFormPath(formList.ID);
            stiReport.Load(path);

            stiReport.CacheAllData = true;

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private void DepositPaymentKickBack(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RequestPayment> DepositRequestPayment = DepositDataGrid.ItemsSource as List<RequestPayment>;
                DepositRequestPayment = DepositRequestPayment.Where(t => t.IsPaid == true).ToList();
                if (DepositRequestPayment.Count >= 0)
                {
                    DepositRequestPayment.ForEach(t => { t.IsKickedBack = true; t.Detach(); });
                    DB.UpdateAll(DepositRequestPayment);

                }
                else
                {
                    throw new Exception("هزینه ای برای باز پرداخت وجود ندارد");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در بازپرداخت ودیعه، ", ex);
            }
        }


        #endregion

        #region TelecomminucationService

        public List<TelecomminucationServiceInfo> _TelecomminucationServiceInfos { get; set; }

        public ObservableCollection<TelecomminucationServicePaymentInfo> _TelecomminucationServicePaymentInfos { get; set; }

        //چون به کنترل های تمپلیت ستون دیتاگرید به طور مستقیم دسترسی ندارین ، کنترل زیر را تعریف کردیم
        private ComboBox _TelecomminucationServiceComboBox;

        //از فیلد زیر برای نگهداری حالت سطر انتخابی در دیتا گرید صورتحساب کالا-خدمات مخابرات استفاده میشود
        private DataGridRow _currentEditingRow;

        private void RequestTelecomminucationServiceDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (_Request.ID == 0)
            {
                e.Cancel = true;
                ShowWarningMessage("ابتدا باید درخواست مورد نظر را ذخیره نمائید.");
            }
            else
            {
                if (!RequestTelecomminucationServiceDataGrid.IsInEditMode)
                {
                    RequestTelecomminucationServiceDataGrid.IsInEditMode = true;
                    _currentEditingRow = e.Row;
                }
                else if (e.Row != _currentEditingRow)
                {
                    e.Cancel = true;
                }
            }
        }

        private void RequestTelecomminucationServiceDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (_Request.ID != 0)
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    HideMessage();
                    TelecomminucationServicePaymentInfo paymentItem = RequestTelecomminucationServiceDataGrid.SelectedItem as TelecomminucationServicePaymentInfo;
                    if (paymentItem.TelecomminucationServiceID == 0)
                    {
                        MessageBox.Show(".تعیین نوع کالا/خدمات الزامی میباشد", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    TelecomminucationServiceInfo serviceItem = this._TelecomminucationServiceInfos.Where(t => t.ID == paymentItem.TelecomminucationServiceID).SingleOrDefault();
                    if (paymentItem != null)
                    {
                        //چنانچه کاربر به اشتباه سه فیلد زیر را به صورت منفی وارد نماید،مقدار مثبت خواهد شد
                        decimal quantity = (paymentItem.Quantity < 0) ? (paymentItem.Quantity * -1) : paymentItem.Quantity;
                        int discount = (paymentItem.Discount < 0) ? (paymentItem.Discount * -1) : paymentItem.Discount;
                        int taxAndTollAmount = paymentItem.TaxAndTollAmount;
                        long unitPrice = paymentItem.UnitPrice;

                        decimal netAmount = quantity * unitPrice;
                        decimal netAmountWithDiscount = (netAmount - discount) > 0 ? netAmount - discount : 0;
                        decimal amountSum = netAmountWithDiscount + taxAndTollAmount;

                        paymentItem.NetAmount = netAmount;
                        paymentItem.NetAmountWithDiscount = netAmountWithDiscount;
                        paymentItem.AmountSum = amountSum;

                        TelecomminucationServicePayment telecomminucationServicePayment = new TelecomminucationServicePayment();
                        if (paymentItem.ID != 0)
                        {
                            telecomminucationServicePayment.ID = paymentItem.ID;
                        }
                        //TODO:define column telecomminucationServicePayment.InsertDate
                        paymentItem.RequestID = telecomminucationServicePayment.RequestID = _Request.ID;
                        paymentItem.Quantity = telecomminucationServicePayment.Quantity = quantity;
                        paymentItem.Discount = telecomminucationServicePayment.Discount = discount;
                        paymentItem.TaxAndTollAmount = telecomminucationServicePayment.TaxAndTollAmount = taxAndTollAmount;
                        paymentItem.TelecomminucationServiceID = telecomminucationServicePayment.TelecomminucationServiceID = serviceItem.ID;
                        paymentItem.NetAmount = telecomminucationServicePayment.NetAmount = netAmount;
                        paymentItem.NetAmountWithDiscount = telecomminucationServicePayment.NetAmountWithDiscount = netAmountWithDiscount;
                        paymentItem.AmountSum = telecomminucationServicePayment.AmountSum = amountSum;
                        telecomminucationServicePayment.Detach();
                        DB.Save(telecomminucationServicePayment);

                        //مقداردهی شناسه رکورد متناظر در لیست 
                        //_TelecomminucationServiceInfos
                        paymentItem.ID = (paymentItem.ID == 0) ? telecomminucationServicePayment.ID : paymentItem.ID;
                        e.Cancel = RequestTelecomminucationServiceDataGrid.IsInEditMode;
                    }
                }
                else if (e.EditAction == DataGridEditAction.Cancel)
                {
                    RequestTelecomminucationServiceDataGrid.IsInEditMode = false;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TelecomminucationServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_TelecomminucationServiceComboBox != null && _TelecomminucationServiceComboBox.SelectedItem != null)
            {
                TelecomminucationServiceInfo serviceItem = _TelecomminucationServiceComboBox.SelectedItem as TelecomminucationServiceInfo;
                TelecomminucationServicePaymentInfo paymentItem = RequestTelecomminucationServiceDataGrid.SelectedItem as TelecomminucationServicePaymentInfo;
                if (serviceItem != null && paymentItem != null)
                {
                    this._TelecomminucationServicePaymentInfos[RequestTelecomminucationServiceDataGrid.SelectedIndex].UnitPrice = serviceItem.UnitPrice;
                    this._TelecomminucationServicePaymentInfos[RequestTelecomminucationServiceDataGrid.SelectedIndex].TaxAndTollAmount = serviceItem.Tax;
                    this._TelecomminucationServicePaymentInfos[RequestTelecomminucationServiceDataGrid.SelectedIndex].TelecomminucationServiceID = serviceItem.ID;
                    this._TelecomminucationServicePaymentInfos[RequestTelecomminucationServiceDataGrid.SelectedIndex].TelecomminucationServiceTitle = serviceItem.Title;
                }
            }
        }

        private void TelecomminucationServiceComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            //چون به کنترل های داخل ستون دیتاگرید به طور مستقیم دسترسی نداریم ، کنترل زیر را تعریف کردیم
            this._TelecomminucationServiceComboBox = sender as ComboBox;
        }

        private void RequestTelecomminucationServiceDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridRow row = Helper.GetRow(RequestTelecomminucationServiceDataGrid, RequestTelecomminucationServiceDataGrid.Items.IndexOf(RequestTelecomminucationServiceDataGrid.CurrentItem));
            //فقط با زدن اینتر ، باید یک رکورد جدید صورتحساب کالا-خدمات اضافه شود
            if (row.IsEditing && e.Key == Key.Enter)
            {
                RequestTelecomminucationServiceDataGrid.IsInEditMode = false;
            }
        }

        private void TelecomminucationServicePaymentItemDelete(object sender, RoutedEventArgs e)
        {
            if (RequestTelecomminucationServiceDataGrid.SelectedIndex < 0 || RequestTelecomminucationServiceDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    TelecomminucationServicePaymentInfo item = RequestTelecomminucationServiceDataGrid.SelectedItem as TelecomminucationServicePaymentInfo;
                    if (item.ID == 0) return;
                    DB.Delete<TelecomminucationServicePayment>(item.ID);
                    this._TelecomminucationServicePaymentInfos.Remove(item);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                string errorMessage = SqlExceptionHelper.ErrorMessage(sqlException);
                ShowErrorMessage(errorMessage, sqlException);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف", ex);
            }
        }

        #endregion

        private void InstallmentRegistrationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (
                 RequestPaymentDataGrid.SelectedIndex >= 0
                 &&
                 _Request.RequestTypeID == (int)DB.RequestType.Dayri
                 &&
                 _installReqeust.MethodOfPaymentForTelephoneConnection.HasValue
                 &&
                 _installReqeust.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Installment
               )
            {
                RequestPayment item = RequestPaymentDataGrid.SelectedItem as RequestPayment;
                if (item.PaymentType != (byte)DB.PaymentType.Instalment)
                {
                    MessageBox.Show(".امکان ایجاد اقساط فقط برای هزینه های اقساط میباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //برای ایجاد اقساط میباست ابتدا هزینه پیش پرداخت به صورت نقدی پرداخت شده باشد
                RequestPayment prepaymentRequestPayment = RequestPaymentDB.GetPaidedRequestPayment(_Request.ID, (int)DB.PaymentType.Cash, (int)DB.SpecialCostID.PrePaymentTypeCostID);
                if (prepaymentRequestPayment == null)
                {
                    MessageBox.Show(".برای ایجاد اقساط ابتدا باید هزینه 'پیش پرداخت' ، پرداخت شود", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //چک شود که آیا این درخواست ثبت قسط داشته است یا خیر
                long currentInstallmentId = TelephoneConnectionInstallmentDB.GetTelephoneConnectionInstallmnentIdByRequestPaymentId(item.ID);
                if (currentInstallmentId == 0)
                {
                    InstallmentRegistrationForm form = new InstallmentRegistrationForm(item.ID, item.RequestID, prepaymentRequestPayment, currentInstallmentId);
                    form.ShowDialog();
                    if (form.DialogResult.HasValue && form.DialogResult.Value)
                    {
                        InstallmentRegistrationMenuItem.Header = "ویرایش اقساط هزینه اتصال تلفن";
                        MessageBox.Show(".ذخیره اقساط با موفقیت انجام شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    InstallmentRegistrationForm form = new InstallmentRegistrationForm(item.ID, item.RequestID, prepaymentRequestPayment, currentInstallmentId);
                    form.ShowDialog();
                    if (form.DialogResult.HasValue && form.DialogResult.Value)
                    {
                        MessageBox.Show(".ویرایش اقساط با موفقیت انجام شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}