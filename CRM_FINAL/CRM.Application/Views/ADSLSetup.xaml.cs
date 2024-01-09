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
using System.Transactions;
using System.Xml.Linq;
using CRM.Application.UserControls;

namespace CRM.Application.Views
{
    public partial class ADSLSetup : Local.RequestFormBase
    {
        #region Properties

        private Request _Request { get; set; }
        private ADSLRequest _ADSLRequest { get; set; }
        private Data.ADSL _ADSL { get; set; }
        private Data.WirelessRequest _wirelessRequest { get; set; }
        private Telephone _Telephone { get; set; }
        private Customer _Customer { get; set; }
        private ADSLServiceInfo _ADSLServiceInfo { get; set; }
        private RequestLog _RequestLog { get; set; }
        private TeleInfo TeleInfo { get; set; }
        private UserControls.TelephoneInformation _TelephoneInformation;
        public RequestType RequestType { get; set; }
        private ADSLInstallRequest _ADSLInstallRequest { get; set; }

        #endregion

        #region Constructors

        public ADSLSetup(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
            LoadData();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            RequestType = RequestTypeDB.getRequestTypeByID(_Request.RequestTypeID);

            switch (RequestType.ID)
            {
                case ((byte)DB.RequestType.ADSL):
                    _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);
                    _ADSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                    _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                    _Customer = Data.CustomerDB.GetCustomerByID(_ADSLRequest.CustomerOwnerID);
                    _ADSLServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLRequest.ServiceID);
                    if (_ADSLRequest.ModemID != null && _ADSLRequest.ModemID != 0)
                        ModemSerilaNoComboBox.ItemsSource = ADSLModemPropertyDB.GetADSLModemPropertiesList((int)_ADSLRequest.ModemID, _Request.CenterID);

                    //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Feedback).ID)
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };

                    //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                    //    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };

                    //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                    //    ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };

                    //ContractorComboBox.ItemsSource = ContractorDB.GetUndeContractContractor();
                    break;

                case ((byte)DB.RequestType.ADSLInstall):
                    _ADSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                    _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                    _Customer = Data.CustomerDB.GetCustomerByID((long)_Request.CustomerID);
                    _ADSLServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                    break;
                case ((byte)DB.RequestType.Wireless):
                    _wirelessRequest = WirelessRequestDB.GetWirelessRequestByID((long)_Request.ID);
                    if (_Request.TelephoneNo.HasValue)
                    {
                        _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                    }
                    _Customer = Data.CustomerDB.GetCustomerByID((long)_Request.CustomerID);
                    _ADSLServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_wirelessRequest.ServiceID);

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                    break;
            }
        }

        public void LoadData()
        {
            switch (RequestType.ID)
            {
                case (byte)DB.RequestType.ADSL:
                    _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                    TelephoneInfo.Content = _TelephoneInformation;

                    ServiceInfo.DataContext = _ADSLServiceInfo;
                    LicenceLetterNoTextBox.Text = _ADSLRequest.LicenseLetterNo;

                    CustomerPriorityTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLCustomerPriority), (byte)_ADSLRequest.CustomerPriority);
                    ServiceCostTextBox.Text = _ADSLServiceInfo.PaymentType;
                    RequiredInstalationCheckBox.IsChecked = (_ADSLRequest.RequiredInstalation != null) ? _ADSLRequest.RequiredInstalation : false;
                    NeedModemCheckBox.IsChecked = (_ADSLRequest.NeedModem != null) ? _ADSLRequest.NeedModem : false;
                    if (_ADSLRequest.NeedModem != null && (bool)_ADSLRequest.NeedModem)
                    {
                        ModemTypeTextBox.Text = ADSLModemDB.GetSalableModemsTitleByID((int)_ADSLRequest.ModemID);
                        if (_ADSLRequest.ModemSerialNoID != null && _ADSLRequest.ModemSerialNoID != 0)
                        {
                            ModemSerialNoTextBox.Text = ADSLModemPropertyDB.GetSerialNobyID((int)_ADSLRequest.ModemSerialNoID);
                            ModemSerilaNoComboBox.SelectedValue = (int)_ADSLRequest.ModemSerialNoID;
                        }
                        ModemMACAddressTextBox.Text = _ADSLRequest.ModemMACAddress;
                    }

                    if (_ADSLRequest.IPStaticID != null)
                        IPTextBox.Text = ADSLIPDB.GetIPValuebyID((int)_ADSLRequest.IPStaticID);
                    if (_ADSLRequest.GroupIPStaticID != null)
                        IPGroupTextBox.Text = ADSLIPDB.GetIPGroupValuebyID((int)_ADSLRequest.GroupIPStaticID);

                    PortTextBox.Text = ADSLPortDB.GetADSlPortInfoByID((long)_ADSLRequest.ADSLPortID).Port;

                    UserNameTextBox.Text = _ADSL.UserName;
                    PasswordTextBox.Text = _ADSL.OrginalPassword;

                    MDFUserTextBox.Text = UserDB.GetUserFullName(_ADSLRequest.MDFUserID);
                    MDFDateTextBox.Text = Helper.GetPersianDate((DateTime)_ADSLRequest.MDFDate, Helper.DateStringType.DateTime);

                    ItemsDataGrid.ItemsSource = ADSLSetupContactInformationDB.GetContactInformantionbyUserID(RequestID);

                    break;
                case (byte)DB.RequestType.Wireless:
                    _TelephoneInformation = new TelephoneInformation(_Request.ID);
                    TelephoneInfo.Content = _TelephoneInformation;

                    ServiceInfo.DataContext = _ADSLServiceInfo;
                    LicenceLetterNoTextBox.Text = _wirelessRequest.LicenseLetterNo;

                    CustomerPriorityTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLCustomerPriority), (byte)_wirelessRequest.CustomerPriority);
                    ServiceCostTextBox.Text = _ADSLServiceInfo.PaymentType;
                    RequiredInstalationCheckBox.IsChecked = (_wirelessRequest.RequiredInstalation != null) ? _wirelessRequest.RequiredInstalation : false;
                    NeedModemCheckBox.IsChecked = (_wirelessRequest.NeedModem != null) ? _wirelessRequest.NeedModem : false;
                    if (_wirelessRequest.NeedModem != null && (bool)_wirelessRequest.NeedModem)
                    {
                        ModemTypeTextBox.Text = ADSLModemDB.GetSalableModemsTitleByID((int)_wirelessRequest.ModemID);
                        if (_wirelessRequest.ModemSerialNoID != null && _wirelessRequest.ModemSerialNoID != 0)
                            ModemSerilaNoComboBox.SelectedValue = (int)_wirelessRequest.ModemSerialNoID;
                        ModemMACAddressTextBox.Text = _wirelessRequest.ModemMACAddress;
                    }

                    UserNameTextBox.Text = _wirelessRequest.UserName;
                    PasswordTextBox.Text = _wirelessRequest.OrginalPassword;

                    ItemsDataGrid.ItemsSource = ADSLSetupContactInformationDB.GetContactInformantionbyUserID(RequestID);

                    //if (_ADSLRequest.ContractorID != null)
                    //    ContractorComboBox.SelectedValue = _ADSLRequest.ContractorID;

                    //if (_ADSLRequest.CustomerSatisfaction != null)
                    //    CustomerSatisfactionListBox.SelectedValue = _ADSLRequest.CustomerSatisfaction;

                    //if (_ADSLRequest.InstallDate != null)
                    //    InstallDate.Text = Helper.GetPersianDate(_ADSLRequest.InstallDate, Helper.DateStringType.DateTime);

                    //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Feedback).ID)
                    //    CustomerSatisfactionGroupBox.Visibility = Visibility.Collapsed;

                    //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                    //    ContractorComboBox.IsEnabled = false;

                    //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                    //{
                    //    ContractorComboBox.IsEnabled = false;
                    //    CustomerSatisfactionListBox.IsEnabled = false;
                    //}
                    break;

                case (byte)DB.RequestType.ADSLInstall:
                    _TelephoneInformation = new TelephoneInformation((long)_Request.TelephoneNo, _Request.RequestTypeID);
                    TelephoneInfo.Content = _TelephoneInformation;

                    ServiceInfo.DataContext = _ADSLServiceInfo;
                    ServiceCostTextBox.Text = _ADSLServiceInfo.PaymentType;
                    RequiredInstalationCheckBox.IsChecked = true;
                    UserNameTextBox.Text = _ADSL.UserName;
                    PasswordTextBox.Text = _ADSL.OrginalPassword;

                    ItemsDataGrid.ItemsSource = ADSLSetupContactInformationDB.GetContactInformantionbyUserID(RequestID);
                    break;
            }

            ResizeWindow();
        }

        public override bool Save()
        {
            try
            {
                //switch (RequestType.ID)
                //{
                //    case ((byte)DB.RequestType.ADSL):
                if (ModemSerilaNoComboBox.SelectedValue != null)
                {
                    _ADSLRequest.ModemSerialNoID = (int)ModemSerilaNoComboBox.SelectedValue;
                    _ADSLRequest.ModemMACAddress = ModemMACAddressTextBox.Text;

                    _ADSLRequest.Detach();
                    Save(_ADSLRequest);
                }

                IsSaveSuccess = true;

                ShowSuccessMessage("با موفقیت ذخیره شد");
                //break;
                //case ((byte)DB.RequestType.ADSLInstall):
                //    _ADSLInstallRequest.ID = _Request.ID;
                //    _ADSLInstallRequest.Detach();
                //    DB.Save(_ADSLInstallRequest);
                //    break;
                //}                
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره ، " + ex.Message + " !", ex);
            }

            return IsSaveSuccess;
        }

        public override bool Confirm()
        {
            try
            {
                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Feedback).ID)
                {
                    _ADSLRequest.Status = true;
                    //_ADSLRequest.ContractorID = (ContractorComboBox.SelectedItem as Contractor).ID;

                    //_ADSLRequest.SetupCommnet = SetupCommentTextBox.Text;
                    _ADSLRequest.SetupDate = DB.GetServerDate();
                    _ADSLRequest.SetupUserID = DB.CurrentUser.ID;
                }
                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                {
                    _ADSLRequest.InstallDate = DB.GetServerDate();
                }
                //_ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_ADSLRequest.TelephoneNo).SingleOrDefault();

                //if (_ADSL == null)

                _ADSL.ModemID = (int)ModemSerilaNoComboBox.SelectedValue;

                _ADSL.Detach();
                Save(_ADSL);

                ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ModemSerilaNoComboBox.SelectedValue);

                modem.TelephoneNo = _ADSL.TelephoneNo;
                modem.Status = (byte)DB.ADSLModemStatus.Sold;

                modem.Detach();
                Save(modem);

                CRM.Data.Schema.ADSL ADSLLog = new Data.Schema.ADSL();
                ADSLLog.CustomerOwnerID = (long)_ADSL.CustomerOwnerID;
                ADSLLog.TariffID = (int)_ADSL.TariffID;
                //ADSLLog.ServiceType = (byte)_ADSL.ServiceType;
                ADSLLog.ADSLPortID = (long)_ADSL.ADSLPortID;
                //ADSLLog.ContractorID = (int)_ADSLRequest.ContractorID;

                _RequestLog = new RequestLog();
                _RequestLog.RequestID = _Request.ID;
                _RequestLog.RequestTypeID = _Request.RequestTypeID;
                _RequestLog.Date = DB.GetServerDate();
                _RequestLog.TelephoneNo = _Telephone.TelephoneNo;

                _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ADSL>(ADSLLog, true));

                RequestForADSL.SaveADSLRequest(null, _ADSLRequest, null, null, null, _RequestLog, false);

                IsConfirmSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("تایید انجام نگرفت !", ex);
            }

            return IsConfirmSuccess;
        }

        private void SavedValueLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ADSLSetupContactInformationForm window = new ADSLSetupContactInformationForm(0, RequestID);
            window.ShowDialog();

            ItemsDataGrid.ItemsSource = ADSLSetupContactInformationDB.GetContactInformantionbyUserID(RequestID);
        }

        #endregion

        #region Event Handlers

        //private void ContractorTitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ContractorComboBox.SelectedValue != null)
        //    {
        //        Contractor contractor = DB.SearchByPropertyName<Contractor>("ID", ContractorComboBox.SelectedValue).SingleOrDefault();
        //        ContractorInfo.DataContext = contractor;
        //    }
        //    else
        //        ContractorComboBox.SelectedValue = _ADSLRequest.ContractorID;
        //}

        #endregion
    }
}
