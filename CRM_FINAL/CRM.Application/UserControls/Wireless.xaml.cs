using CRM.Application.Views;
using CRM.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    public partial class Wireless : UserControl
    {
        #region Properties

        private UserControls.TelephoneInformation _TelephoneInformation;
        Telephone _telephone = new Telephone();
        public Customer _customer = new Customer();
        private Service1 service = new Service1();
        public Address _installAddress { get; set; }

        private long _CustomerID = 0;
        public int _CenterID = 0;
        private int _CityID = 0;
        private long _requestID;

        public bool _HasCreditAgent = true;
        public bool _HasCreditUser = true;
        private int sellerAgentID { get; set; }

        public CRM.Data.WirelessRequest _WirelessRequest { get; set; }
        private Request _Request { get; set; }
        public System.Data.DataTable telephoneInfo { get; set; }
        public long _telephoneNo = 0;

        public long _SumPriceService = 0;
        public long _SumPriceTraffic = 0;
        public long _SumPriceIP = 0;
        public long _SumPriceModem = 0;

        public ADSLIP IPStatic { get; set; }
        public ADSLGroupIP GroupIPStatic { get; set; }
        private ADSLServiceInfo serviceInfo { get; set; }

        private List<int> _ServiceAccessList { get; set; }
        private List<int> _ServiceGroupAccessList { get; set; }

        #endregion

        #region Constructors

        public Wireless()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public Wireless(long RequestID)
            : this()
        {
            this._requestID = RequestID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ADSLOwnerStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupList();
            CustomerTypeComboBox.ItemsSource = ADSLCustomerTypeDB.GetCustomerTypeList();
            JobGroupComboBox.ItemsSource = DB.GetAllEntity<JobGroup>();
            //CustomerPriorityComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLCustomerPriority));
            //RegisterProjectTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLRegistrationProjectType));
            ModemTypeComboBox.ItemsSource = ADSLModemDB.GetSalableModemsTitle(true);
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLService(true);
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
            AdditionalServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLAdditionalService(true);
            IPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPType));
            GroupIPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLGroupIPBlockCount));
            WirelessTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.WirelessType));
        }

        private void DisableControls()
        {
            ADSLOwnerStatusComboBox.IsEnabled = false;
            NationalCodeTextBox.IsReadOnly = true;
            CustomerNameTextBox.IsReadOnly = true;
            MobileNoTextBox.IsReadOnly = true;
            // searchButton.Visibility = Visibility.Collapsed;
            CustomerNameRow.Width = new GridLength(180);
            CustomerGroupComboBox.IsEnabled = false;
            CustomerTypeComboBox.IsEnabled = false;
            JobGroupComboBox.IsEnabled = false;
            ReagentTelephoneNoTextBox.IsReadOnly = true;
            //CustomerPriorityComboBox.IsEnabled = false;
            RequiredInstalationCheckBox.IsEnabled = false;
            NeedModemCheckBox.IsEnabled = false;
            ModemTypeComboBox.IsEnabled = false;
            //RegisterProjectTypeComboBox.IsEnabled = false;
            ServiceComboBox.IsEnabled = false;
            LicenceLetterNoTextBox.IsReadOnly = true;
            CustomerEndRentDate.IsEnabled = false;
        }

        private void LoadData()
        {
            if (_requestID == 0)
            {
                ADSLOwnerStatusComboBox.SelectedValue = (byte)DB.ADSLOwnerStatus.Owner;
                ADSLServiceGroupBox.Visibility = Visibility.Collapsed;
                ADSLServiceAdditionalGroupBox.Visibility = Visibility.Collapsed;
                ADSLIPGroupBox.Visibility = Visibility.Collapsed;
                ADSLFacilitiesGroupBox.Visibility = Visibility.Collapsed;
                _WirelessRequest = new WirelessRequest();
                if (sellerAgentID != 0)
                {
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(sellerAgentID);
                    CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupbyServiceGroupList(_ServiceGroupAccessList);
                }
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_requestID);
                _WirelessRequest = WirelessRequestDB.GetWirelessRequestByID(_requestID);

                if (_Request.TelephoneNo == null || _Request.TelephoneNo == 0)
                {
                    TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType)).Where(t => t.ID == 1);
                    GroupComboBox.IsEnabled = false;
                    BandWidthComboBox.IsEnabled = false;
                    TrafficComboBox.IsEnabled = false;
                    DurationComboBox.IsEnabled = false;
                    ServiceComboBox.ItemsSource = null;
                }

                _customer = CustomerDB.GetCustomerByID(_WirelessRequest.CustomerOwnerID);
                NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                SearchCustomer_Click(null, null);
                _installAddress = AddressDB.GetAddressByID(_WirelessRequest.AddressID);
                InstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                SearchInstallAddress_Click(null, null);

                LongitudeTextBox.Text = _WirelessRequest.Longitude.ToString();
                LatitudeTextBox.Text = _WirelessRequest.Latitude.ToString();
                WirelessTypeComboBox.SelectedValue = _WirelessRequest.WirelessType;

                if (sellerAgentID == 0)
                {
                    if (_Request.TelephoneNo != null && _Request.TelephoneNo != 0)
                    {
                        ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup((int)_WirelessRequest.CustomerGroupID, true);
                        GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupID((int)_WirelessRequest.CustomerGroupID);
                    }
                }
                else
                {
                    _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(sellerAgentID);
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(sellerAgentID);

                    if (_Request.TelephoneNo != null && _Request.TelephoneNo != 0)
                    {
                        ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent((int)_WirelessRequest.CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList, true);
                        GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent((int)_WirelessRequest.CustomerGroupID, _ServiceGroupAccessList);
                        CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupbyServiceGroupList(_ServiceGroupAccessList);
                    }
                }

                ADSLOwnerStatusComboBox.SelectedValue = _WirelessRequest.CustomerOwnerStatus;
                if (_customer != null)
                {
                    _customer = Data.CustomerDB.GetCustomerByID(_WirelessRequest.CustomerOwnerID);
                    if (_customer.NationalCodeOrRecordNo != null)
                        NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo.ToString();
                    CustomerNameTextBox.Text = _customer.FirstNameOrTitle + " " + _customer.LastName;
                    if (!string.IsNullOrWhiteSpace(_customer.MobileNo))
                        MobileNoTextBox.Text = _customer.MobileNo;
                }

                NationalCodeTextBox.IsReadOnly = true;
                //searchButton.IsEnabled = false;
                CustomerEndRentDate.SelectedDate = _WirelessRequest.CustomerEndRentDate;

                CustomerGroupComboBox.SelectedValue = _WirelessRequest.CustomerGroupID;
                CustomerTypeComboBox.SelectedValue = _WirelessRequest.CustomerTypeID;
                JobGroupComboBox.SelectedValue = _WirelessRequest.JobGroupID;
                ReagentTelephoneNoTextBox.Text = _WirelessRequest.ReagentTelephoneNo;
                //if (_WirelessRequest.CustomerPriority == null)
                //    CustomerPriorityComboBox.SelectedValue = (byte)DB.ADSLCustomerPriority.Normal;
                //else
                //CustomerPriorityComboBox.SelectedValue = _WirelessRequest.CustomerPriority;
                //RegisterProjectTypeComboBox.SelectedValue = _WirelessRequest.RegistrationProjectType;
                ServiceComboBox.SelectedValue = _WirelessRequest.ServiceID;

                ADSLService currentService = new ADSLService();
                if (_WirelessRequest.ServiceID != null)
                    currentService = ADSLServiceDB.GetADSLServiceById((int)_WirelessRequest.ServiceID);

                if (_WirelessRequest.AdditionalServiceID != null)
                {
                    HasAdditionalTrafficCheckBox.IsChecked = true;
                    AdditionalTrafficInfoGrid.Visibility = Visibility.Visible;
                    AdditionalServiceComboBox.SelectedValue = _WirelessRequest.AdditionalServiceID;
                }

                if (_WirelessRequest.HasIP == null)
                {
                    HasIPStaticCheckBox.IsChecked = false;
                    IPTypeComboBox.SelectedValue = null;
                    IPTypeComboBox.IsEnabled = false;
                }
                else
                {
                    HasIPStaticCheckBox.IsChecked = _WirelessRequest.HasIP;
                    #region IP
                    if ((bool)_WirelessRequest.HasIP)
                    {
                        if (_WirelessRequest.IPStaticID != null)
                        {
                            IPStatic = ADSLIPDB.GetADSLIPById((long)_WirelessRequest.IPStaticID);

                            IPTypeComboBox.SelectedValue = (byte)DB.ADSLIPType.Single;

                            SingleIPLabel.Visibility = Visibility.Visible;
                            SingleIPTextBox.Visibility = Visibility.Visible;
                            GroupIPTypeLabel.Visibility = Visibility.Collapsed;
                            GroupIPTypeComboBox.Visibility = Visibility.Collapsed;
                            GroupIPLabel.Visibility = Visibility.Collapsed;
                            GroupIPTextBox.Visibility = Visibility.Collapsed;
                            IPCostLabel.Visibility = Visibility.Visible;
                            IPCostTextBox.Visibility = Visibility.Visible;
                            IPTimeLabel.Visibility = Visibility.Visible;
                            IPTimeComboBox.Visibility = Visibility.Visible;
                            IPTaxLabel.Visibility = Visibility.Visible;
                            IPTaxTextBox.Visibility = Visibility.Visible;
                            IPSumCostLabel.Visibility = Visibility.Visible;
                            IPSumCostTextBox.Visibility = Visibility.Visible;

                            SingleIPTextBox.Text = IPStatic.IP;
                            BaseCost cost = BaseCostDB.GetIPCostForADSL();

                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)currentService.DurationID);
                            if (_WirelessRequest.IPDuration != null)
                            {
                                IPCostTextBox.Text = (cost.Cost * _WirelessRequest.IPDuration).ToString() + " ریا ل";
                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * _WirelessRequest.IPDuration) + (cost.Cost * _WirelessRequest.IPDuration * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }
                                IPTimeComboBox.SelectedValue = _WirelessRequest.IPDuration;
                            }
                            else
                            {
                                if (currentService.DurationID != null)
                                {
                                    IPCostTextBox.Text = (cost.Cost * currentService.DurationID).ToString() + " ریا ل";
                                    if (cost.Tax != null && cost.Tax != 0)
                                    {
                                        IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                        IPSumCostTextBox.Text = ((cost.Cost * currentService.DurationID) + (cost.Cost * currentService.DurationID * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                    }
                                    else
                                    {
                                        IPTaxTextBox.Text = "0 درصد";
                                        IPSumCostTextBox.Text = IPCostTextBox.Text;
                                    }
                                    IPTimeComboBox.SelectedValue = currentService.DurationID;
                                }
                            }
                        }
                        if (_WirelessRequest.GroupIPStaticID != null)
                        {
                            GroupIPStatic = ADSLIPDB.GetADSLGroupIPById((long)_WirelessRequest.GroupIPStaticID);

                            IPTypeComboBox.SelectedValue = (byte)DB.ADSLIPType.Group;
                            GroupIPTypeComboBox.SelectedValue = GroupIPStatic.BlockCount;

                            SingleIPLabel.Visibility = Visibility.Collapsed;
                            SingleIPTextBox.Visibility = Visibility.Collapsed;
                            GroupIPTypeLabel.Visibility = Visibility.Visible;
                            GroupIPTypeComboBox.Visibility = Visibility.Visible;
                            GroupIPLabel.Visibility = Visibility.Visible;
                            GroupIPTextBox.Visibility = Visibility.Visible;
                            IPCostLabel.Visibility = Visibility.Visible;
                            IPCostTextBox.Visibility = Visibility.Visible;
                            IPTimeLabel.Visibility = Visibility.Visible;
                            IPTimeComboBox.Visibility = Visibility.Visible;
                            IPTaxLabel.Visibility = Visibility.Visible;
                            IPTaxTextBox.Visibility = Visibility.Visible;
                            IPSumCostLabel.Visibility = Visibility.Visible;
                            IPSumCostTextBox.Visibility = Visibility.Visible;

                            GroupIPTextBox.Text = GroupIPStatic.StartRange;
                            BaseCost cost = BaseCostDB.GetIPCostForADSL();

                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)currentService.DurationID);

                            if (_WirelessRequest.IPDuration != null)
                            {
                                IPCostTextBox.Text = (cost.Cost * _WirelessRequest.IPDuration * GroupIPStatic.BlockCount).ToString() + " ریا ل";
                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * _WirelessRequest.IPDuration * GroupIPStatic.BlockCount) + (cost.Cost * _WirelessRequest.IPDuration * GroupIPStatic.BlockCount * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }
                                IPTimeComboBox.SelectedValue = _WirelessRequest.IPDuration;
                            }
                            else
                            {
                                if (currentService.DurationID != null)
                                {
                                    IPCostTextBox.Text = (cost.Cost * currentService.DurationID * GroupIPStatic.BlockCount).ToString() + " ریا ل";
                                    if (cost.Tax != null && cost.Tax != 0)
                                    {
                                        IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                        IPSumCostTextBox.Text = ((cost.Cost * currentService.DurationID * GroupIPStatic.BlockCount) + (cost.Cost * currentService.DurationID * GroupIPStatic.BlockCount * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                    }
                                    else
                                    {
                                        IPTaxTextBox.Text = "0 درصد";
                                        IPSumCostTextBox.Text = IPCostTextBox.Text;
                                    }
                                    IPTimeComboBox.SelectedValue = currentService.DurationID;
                                }
                            }
                        }
                    }
                    else
                    {
                        IPTypeComboBox.SelectedValue = null;
                        IPTypeComboBox.IsEnabled = false;
                    }
                }

                    #endregion
                LicenceLetterNoTextBox.Text = _WirelessRequest.LicenseLetterNo;
                if (_WirelessRequest.RequiredInstalation == null)
                    RequiredInstalationCheckBox.IsChecked = false;
                else
                    RequiredInstalationCheckBox.IsChecked = _WirelessRequest.RequiredInstalation;

                if (_WirelessRequest.NeedModem == null)
                {
                    NeedModemCheckBox.IsChecked = false;

                    ModemTypeLabel.Visibility = Visibility.Collapsed;
                    ModemTypeComboBox.Visibility = Visibility.Collapsed;
                    ModemCostLabel.Visibility = Visibility.Collapsed;
                    ModemCostTextBox.Visibility = Visibility.Collapsed;
                    ModemSerilaNoLabel.Visibility = Visibility.Collapsed;
                    ModemSerilaNoComboBox.Visibility = Visibility.Collapsed;
                    ModemMACAddressLabel.Visibility = Visibility.Collapsed;
                    ModemMACAddressTextBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    NeedModemCheckBox.IsChecked = _WirelessRequest.NeedModem;

                    if ((bool)_WirelessRequest.NeedModem)
                    {
                        ModemTypeComboBox.SelectedValue = _WirelessRequest.ModemID;
                        if (_WirelessRequest.ModemSerialNoID != null)
                            ModemSerilaNoComboBox.SelectedValue = _WirelessRequest.ModemSerialNoID;
                        ModemMACAddressTextBox.Text = _WirelessRequest.ModemMACAddress;

                        ModemTypeLabel.Visibility = Visibility.Visible;
                        ModemTypeComboBox.Visibility = Visibility.Visible;
                        ModemCostLabel.Visibility = Visibility.Visible;
                        ModemCostTextBox.Visibility = Visibility.Visible;
                        ModemSerilaNoLabel.Visibility = Visibility.Visible;
                        ModemSerilaNoComboBox.Visibility = Visibility.Visible;
                        ModemMACAddressLabel.Visibility = Visibility.Visible;
                        ModemMACAddressTextBox.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ModemTypeLabel.Visibility = Visibility.Collapsed;
                        ModemTypeComboBox.Visibility = Visibility.Collapsed;
                        ModemCostLabel.Visibility = Visibility.Collapsed;
                        ModemCostTextBox.Visibility = Visibility.Collapsed;
                        ModemSerilaNoLabel.Visibility = Visibility.Collapsed;
                        ModemSerilaNoComboBox.Visibility = Visibility.Collapsed;
                        ModemMACAddressLabel.Visibility = Visibility.Collapsed;
                        ModemMACAddressTextBox.Visibility = Visibility.Collapsed;
                    }
                }
                if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID))
                {
                    ADSLServiceGroupBox.Visibility = Visibility.Collapsed;

                    ADSLServiceAdditionalGroupBox.Visibility = Visibility.Collapsed;
                    ADSLIPGroupBox.Visibility = Visibility.Collapsed;
                    ADSLFacilitiesGroupBox.Visibility = Visibility.Collapsed;

                    //if (emptyPorts.Count == 0)
                    //{
                    //    PortErrorLabel.Visibility = Visibility.Visible;
                    //    //NextStatusLabel.Visibility = Visibility.Visible;
                    //    //NextStatusListBox.Visibility = Visibility.Visible;
                    //    _IsWaitingList = true;
                    //}
                }
                else
                {
                    WirelessInfoGroupBox.IsEnabled = false;
                    CustomeGroupBox.IsEnabled = false;
                    AddressInfoGroupBox.IsEnabled = false;
                }

                if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID) && (_Request.PreviousAction == (byte)DB.Action.Reject))
                {
                    CommentsGroupBox.Visibility = Visibility.Visible;
                    CommentCustomersTextBox.Text = _WirelessRequest.CommentCustomers;
                    CommentCustomersTextBox.Foreground = Brushes.Red;
                    CommentCustomersTextBox.IsReadOnly = true;
                }

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                {
                    ADSLOwnerStatusComboBox.IsEnabled = false;
                    NationalCodeTextBox.IsReadOnly = true;
                    CustomerNameTextBox.IsReadOnly = true;
                    MobileNoTextBox.IsReadOnly = true;
                    // searchButton.Visibility = Visibility.Collapsed;
                    CustomerGroupComboBox.IsEnabled = false;
                    CustomerTypeComboBox.IsEnabled = false;
                    JobGroupComboBox.IsEnabled = false;
                    ReagentTelephoneNoTextBox.IsReadOnly = true;
                    //DisableControls();

                    CommentCustomersTextBox.Text = _WirelessRequest.CommentCustomers;
                    CommentsGroupBox.Visibility = Visibility.Visible;

                    //if (emptyPorts.Count == 0)
                    //{
                    //    PortErrorLabel.Visibility = Visibility.Visible;
                    //    //NextStatusLabel.Visibility = Visibility.Collapsed;
                    //    //NextStatusListBox.Visibility = Visibility.Collapsed;
                    //    _IsWaitingList = true;
                    //}
                }

                Status status = DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Observation);
                if (status != null && _Request.StatusID == status.ID)
                {
                    DisableControls();

                    CommentCustomersTextBox.Text = _WirelessRequest.CommentCustomers;
                    CommentCustomersTextBox.IsReadOnly = true;

                    CommentsGroupBox.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion

        #region Event Handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void TelephoneNoButton_Click(object sender, RoutedEventArgs e)
        {

            //if (!long.TryParse(TelephoneNoTextBox.Text.Trim(), out _telephoneNo))
            //{
            //    return;
            //}
            System.Data.DataTable telephoneInfo = new System.Data.DataTable();
            if (DB.City.ToLower() == "semnan")
            {
                //  telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _telephoneNo.ToString());

                if (telephoneInfo.Rows.Count != 0)
                {
                    _TelephoneInformation = new TelephoneInformation(_telephoneNo, (int)DB.RequestType.Wireless);
                    //TelephoneInfo.Content = _TelephoneInformation;
                    //TelephoneInfo.DataContext = _TelephoneInformation;

                    _CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
                    _CityID = CityDB.GetCityByCenterID(_CenterID).ID;
                    sellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);

                    if (_requestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                    {
                        string nationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                        if (!string.IsNullOrEmpty(nationalCodeOrRecordNo))
                        {
                            _customer = CustomerDB.GetCustomerByNationalCode(nationalCodeOrRecordNo);
                            _customer.FirstNameOrTitle = telephoneInfo.Rows[0]["FIRSTNAME"].ToString();
                            _customer.LastName = telephoneInfo.Rows[0]["LASTNAME"].ToString();
                            _customer.FatherName = telephoneInfo.Rows[0]["FATHERNAME"].ToString();
                            _customer.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                            _customer.BirthCertificateID = telephoneInfo.Rows[0]["SHENASNAME"].ToString();
                            _customer.MobileNo = telephoneInfo.Rows[0]["MOBILE"].ToString();
                            _customer.Email = telephoneInfo.Rows[0]["EMAIL"].ToString();
                            _customer.Detach();
                            DB.Save(_customer);

                            NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                            CustomerNameTextBox.Text = string.Format("{0} {1}", _customer.FirstNameOrTitle, _customer.LastName);
                            SearchCustomer_Click(null, null);

                        }

                        string postallCode = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                        if (!string.IsNullOrEmpty(postallCode))
                        {
                            _installAddress = Data.AddressDB.GetAddressByPostalCode(postallCode).Take(1).SingleOrDefault();
                            _installAddress.CenterID = _CenterID;
                            _installAddress.AddressContent = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                            _installAddress.PostalCode = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                            _installAddress.Detach();
                            DB.Save(_installAddress);

                            InstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                            SearchInstallAddress_Click(null, null);
                        }
                    }
                }
                else
                    Folder.MessageBox.ShowInfo("اطاعات تلفن از سیستم جامع دریافت نشد");
            }
            else
            {
                Data.AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(_telephoneNo);
                _TelephoneInformation = new TelephoneInformation(_telephoneNo, (int)DB.RequestType.Wireless);
                //TelephoneInfo.Content = _TelephoneInformation;
                //TelephoneInfo.DataContext = _TelephoneInformation;

                _CenterID = assignmentInfo.CenterID;
                _CityID = CityDB.GetCityByCenterID(_CenterID).ID;
                sellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);

                if (_requestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                {
                    string nationalCodeOrRecordNo = assignmentInfo.Customer.NationalCodeOrRecordNo;
                    if (!string.IsNullOrEmpty(nationalCodeOrRecordNo))
                    {
                        _customer = CustomerDB.GetCustomerByNationalCode(nationalCodeOrRecordNo);

                        NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                        SearchCustomer_Click(null, null);
                    }

                    string postallCode = assignmentInfo.InstallAddress.PostalCode;
                    if (!string.IsNullOrEmpty(postallCode))
                    {
                        _installAddress = Data.AddressDB.GetAddressByPostalCode(postallCode).Take(1).SingleOrDefault();
                        _installAddress.Detach();
                        DB.Save(_installAddress);

                        InstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                        SearchInstallAddress_Click(null, null);
                    }
                }
            }
        }

        private void EditSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_customer != null)
            {
                CustomerForm window = new CustomerForm(_customer.ID);
                window.ShowDialog();
            }
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                Data.Service service = new Data.Service();

                service.CallParammeter = (byte)DB.ServicCallParameter.ByNationalCode;
                service.NationalCode = NationalCodeTextBox.Text.Trim();
                telephoneInfo = service.GetSMSService();

                TelephoneDataGrid.ItemsSource = telephoneInfo.DefaultView;
                if (telephoneInfo.Rows.Count > 0)
                {
                    Center center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]));
                    if (center == null)
                        throw new Exception("مرکز پیدا نشد");
                    _CenterID = center.ID;

                    if (_requestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                    {
                        string nationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                        if (!string.IsNullOrEmpty(nationalCodeOrRecordNo))
                        {
                            string tel = telephoneInfo.Rows[0]["PHONENO"].ToString();
                            _customer = CustomerDB.GetCustomerByNationalCode(nationalCodeOrRecordNo);
                            _customer.FirstNameOrTitle = telephoneInfo.Rows[0]["FIRSTNAME"].ToString();
                            _customer.LastName = telephoneInfo.Rows[0]["LASTNAME"].ToString();
                            _customer.FatherName = telephoneInfo.Rows[0]["FATHERNAME"].ToString();
                            _customer.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                            _customer.BirthCertificateID = telephoneInfo.Rows[0]["SHENASNAME"].ToString();
                            _customer.MobileNo = telephoneInfo.Rows[0]["MOBILE"].ToString();
                            _customer.Email = telephoneInfo.Rows[0]["EMAIL"].ToString();
                            _customer.Detach();
                            DB.Save(_customer);

                            NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                            CustomerNameTextBox.Text = string.Format("{1} {0}", _customer.FirstNameOrTitle, _customer.LastName);
                            //  SearchCustomer_Click(null, null);
                        }
                    }
                }
                else
                {
                    _customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());

                    if (_customer != null)
                    {
                        //  _request.CustomerID = _customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = _customer.FirstNameOrTitle + ' ' + _customer.LastName;
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.CenterID = _CenterID;
                        //  customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                        customerForm.ShowDialog();
                        if (customerForm.DialogResult ?? false)
                        {
                            _customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                            //  _request.CustomerID = _customer.ID;
                            CustomerNameTextBox.Text = string.Empty;
                            CustomerNameTextBox.Text = _customer.FirstNameOrTitle + ' ' + _customer.LastName;
                        }
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.CenterID = _CenterID;
                // customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    _customer = DB.GetEntitybyID<Customer>(customerForm.ID);
                    //   _request.CustomerID = _customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = _customer.FirstNameOrTitle + ' ' + _customer.LastName;
                }
            }
        }

        private void InstallAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (_installAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(_installAddress.ID);
                window.ShowDialog();
                SearchInstallAddress_Click(null, null);
            }
        }

        private void SearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(InstallPostalCodeTextBox.Text.Trim()))
            {
                InstallAddressTextBox.Text = string.Empty;

                if (BlackListDB.ExistPostallCodeInBlackList(InstallPostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {
                    if (Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Count != 0)
                    {
                        _installAddress = Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                        // _E1.InstallAddressID = InstallAddress.ID;
                        InstallAddressTextBox.Text = string.Empty;
                        InstallAddressTextBox.Text = _installAddress.AddressContent;
                    }

                    else
                    {
                        CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                        customerAddressForm.PostallCode = InstallPostalCodeTextBox.Text.Trim();
                        customerAddressForm.ShowDialog();
                        if (customerAddressForm.DialogResult ?? false)
                        {
                            _installAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                            // _E1.InstallAddressID = InstallAddress.ID;

                            InstallPostalCodeTextBox.Text = string.Empty;
                            InstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                            InstallAddressTextBox.Text = string.Empty;
                            InstallAddressTextBox.Text = _installAddress.AddressContent;
                        }
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = InstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    _installAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                    //  _E1.InstallAddressID = InstallAddress.ID;

                    InstallPostalCodeTextBox.Text = string.Empty;
                    InstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                    InstallAddressTextBox.Text = string.Empty;
                    InstallAddressTextBox.Text = _installAddress.AddressContent;
                }
            }
        }

        private void ADSLOwnerStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region ADSL Without Web Service

            //if (ADSLOwnerStatusComboBox.SelectedValue != null)
            //{
            //    if ((byte)ADSLOwnerStatusComboBox.SelectedValue == (byte)DB.ADSLOwnerStatus.Owner)
            //    {
            //        ADSLCustomer = Data.CustomerDB.GetCustomerByID(_CustomerID);
            //        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
            //        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;

            //        NationalCodeTextBox.IsReadOnly = true;
            //        searchButton.IsEnabled = false;

            //        CustomerEndRentLabel.Visibility = Visibility.Collapsed;
            //        CustomerEndRentDate.Visibility = Visibility.Collapsed;
            //    }
            //    else
            //    {
            //        NationalCodeTextBox.Text = string.Empty;
            //        CustomerNameTextBox.Text = string.Empty;

            //        NationalCodeTextBox.IsReadOnly = false;
            //        searchButton.IsEnabled = true;

            //        CustomerEndRentDate.SelectedDate = null;
            //        CustomerEndRentLabel.Visibility = Visibility.Visible;
            //        CustomerEndRentDate.Visibility = Visibility.Visible;
            //    }
            //}

            #endregion

            if (ADSLOwnerStatusComboBox.SelectedValue != null)
            {
                if ((byte)ADSLOwnerStatusComboBox.SelectedValue == (byte)DB.ADSLOwnerStatus.Owner)
                {
                    if (!string.IsNullOrEmpty(_customer.NationalCodeOrRecordNo))
                    {
                        //Data.Service service = new Data.Service();
                        //service.CallParammeter = (int)DB.ServicCallParameter.ByNationalCode;
                        //service.NationalCode = _customer.NationalCodeOrRecordNo;
                        //System.Data.DataTable telephoneInfo = service.GetSMSService();

                        //NationalCodeTextBox.Text = telephoneInfo.Rows[0]["MelliCode"].ToString();
                        //CustomerNameTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                        //  MobileNoTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();

                        // NationalCodeTextBox.IsReadOnly = true;
                        //searchButton.IsEnabled = false;
                    }
                    CustomerEndRentLabel.Visibility = Visibility.Collapsed;
                    CustomerEndRentDate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //NationalCodeTextBox.Text = string.Empty;
                    //CustomerNameTextBox.Text = string.Empty;
                    //MobileNoTextBox.Text = string.Empty;

                    //NationalCodeTextBox.IsReadOnly = false;
                    //searchButton.IsEnabled = true;
                    if ((byte)ADSLOwnerStatusComboBox.SelectedValue == (byte)DB.ADSLOwnerStatus.Tenant)
                    {
                        CustomerEndRentDate.SelectedDate = null;
                        CustomerEndRentLabel.Visibility = Visibility.Visible;
                        CustomerEndRentDate.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CustomerEndRentDate.SelectedDate = null;
                        CustomerEndRentLabel.Visibility = Visibility.Collapsed;
                        CustomerEndRentDate.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void PropertiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceComboBox.SelectedValue = null;
            ServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                //BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                //  DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                //  TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
            {
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);
            }

            if (sellerAgentID == 0)
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, true);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList, true);
            //else
            //{
            //    if (_WirelessRequest.ServiceID != 0)
            //        ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)ServiceComboBox.SelectedValue, (int)TrafficComboBox.SelectedValue, (int)DurationComboBox.SelectedValue);
            //}
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            serviceInfo = new ADSLServiceInfo();

            if (ServiceComboBox.SelectedValue != null)
                serviceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)ServiceComboBox.SelectedValue);
            else
            {
                if (_WirelessRequest.ServiceID != 0)
                    serviceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_WirelessRequest.ServiceID);
            }

            if (serviceInfo != null)
            {
                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                if (user != null && serviceInfo.IsInstalment == false)
                {
                    long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditAgentRemain <= (_SumPriceService + _SumPriceModem + _SumPriceIP + _SumPriceTraffic + 100000))
                    {
                        ServiceInfo.DataContext = null;
                        _HasCreditAgent = false;

                        ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= (_SumPriceService + _SumPriceModem + _SumPriceIP + _SumPriceTraffic + 100000))
                    {
                        ServiceInfo.DataContext = null;
                        _HasCreditUser = false;

                        ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                        return;
                    }
                }

                if (serviceInfo.IsInstalment == true)
                    _SumPriceService = 0;

                ErrorCreditLabel.Content = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;

                ServiceInfo.DataContext = serviceInfo;

                switch (serviceInfo.IsRequiredLicense)
                {
                    case true:
                        LicenceLetterNoLabel.Visibility = Visibility.Visible;
                        LicenceLetterNoTextBox.Visibility = Visibility.Visible;
                        break;

                    case false:
                    case null:
                        LicenceLetterNoLabel.Visibility = Visibility.Collapsed;
                        LicenceLetterNoTextBox.Visibility = Visibility.Collapsed;
                        break;

                    default:
                        break;
                }

                if (HasIPStaticCheckBox.IsChecked != null)
                    if ((bool)HasIPStaticCheckBox.IsChecked)
                    {
                        BaseCost cost = BaseCostDB.GetIPCostForADSL();

                        IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)serviceInfo.DurationID);
                        IPTimeComboBox.SelectedValue = serviceInfo.DurationID;

                        if (cost.Tax != null && cost.Tax != 0)
                            IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                        else
                            IPTaxTextBox.Text = "0 درصد";

                        if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                            IPCostTextBox.Text = (cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";
                        if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Group)
                            IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";

                        if (serviceInfo.IPDiscount == 0)
                        {
                            IPDiscountLabel.Visibility = Visibility.Collapsed;
                            IPDiscountTextBox.Visibility = Visibility.Collapsed;
                            IPCostDiscountLabel.Visibility = Visibility.Collapsed;
                            IPCostDiscountTextBox.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            IPDiscountLabel.Visibility = Visibility.Visible;
                            IPDiscountTextBox.Visibility = Visibility.Visible;
                            IPCostDiscountLabel.Visibility = Visibility.Visible;
                            IPCostDiscountTextBox.Visibility = Visibility.Visible;
                        }
                    }

                if (serviceInfo.IsModemMandatory)
                {
                    NeedModemCheckBox.IsChecked = true;
                    NeedModemCheckBox.IsEnabled = false;
                }

                if (serviceInfo.ModemDiscount == 0)
                {
                    ModemDiscountLabel.Visibility = Visibility.Collapsed;
                    ModemDiscountTextBox.Visibility = Visibility.Collapsed;
                    ModemCostDiscountLabel.Visibility = Visibility.Collapsed;
                    ModemCostDiscountTextBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ModemDiscountLabel.Visibility = Visibility.Visible;
                    ModemDiscountTextBox.Visibility = Visibility.Visible;
                    ModemCostDiscountLabel.Visibility = Visibility.Visible;
                    ModemCostDiscountTextBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void HasAdditionalTrafficCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AdditionalTrafficInfoGrid.Visibility = Visibility.Visible;
        }

        private void HasAdditionalTrafficCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AdditionalTrafficInfoGrid.Visibility = Visibility.Collapsed;
            AdditionalServiceComboBox.SelectedValue = null;
            AdditionalServiceInfo.DataContext = null;
            _WirelessRequest.AdditionalServiceID = null;

            _SumPriceTraffic = 0;
        }

        private void AdditionalServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ADSLServiceInfo additionalServiceInfo = new ADSLServiceInfo();

            if (AdditionalServiceComboBox.SelectedValue != null)
                additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)AdditionalServiceComboBox.SelectedValue);
            else
            {
                if (_WirelessRequest.AdditionalServiceID != 0)
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_WirelessRequest.AdditionalServiceID);
            }

            if (additionalServiceInfo != null)
            {
                _SumPriceTraffic = Convert.ToInt64(additionalServiceInfo.PriceSum.Split(' ')[0]);

                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                if (user != null)
                {
                    if (serviceInfo.IsInstalment == true)
                        _SumPriceService = 0;

                    long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditAgentRemain <= (_SumPriceService + _SumPriceModem + _SumPriceIP + _SumPriceTraffic + 100000))
                    {
                        AdditionalServiceInfo.DataContext = null;
                        _HasCreditAgent = false;

                        ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= (_SumPriceService + _SumPriceModem + _SumPriceIP + _SumPriceTraffic + 100000))
                    {
                        AdditionalServiceInfo.DataContext = null;
                        _HasCreditUser = false;

                        ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                        return;
                    }
                }

                ErrorCreditLabel.Content = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;

                AdditionalServiceInfo.DataContext = additionalServiceInfo;

                switch (additionalServiceInfo.IsRequiredLicense)
                {
                    case true:
                        AdditionalLicenceLetterNoLabel.Visibility = Visibility.Visible;
                        AdditionalLicenceLetterNoTextBox.Visibility = Visibility.Visible;
                        break;

                    case false:
                    case null:
                        AdditionalLicenceLetterNoLabel.Visibility = Visibility.Collapsed;
                        AdditionalLicenceLetterNoTextBox.Visibility = Visibility.Collapsed;
                        break;

                    default:
                        break;
                }
            }
        }

        private void HasIPStaticCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (ServiceComboBox.SelectedValue == null)
                //    throw new Exception("لطفا ابتدا سرویس مورد نظر را انتخاب نمایید");

                IPTypeComboBox.IsEnabled = true;
            }
            catch (Exception)
            {

            }
        }

        private void HasIPStaticCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IPTypeComboBox.IsEnabled = false;
            IPTypeComboBox.SelectedValue = null;

            SingleIPLabel.Visibility = Visibility.Collapsed;
            SingleIPTextBox.Visibility = Visibility.Collapsed;
            GroupIPTypeLabel.Visibility = Visibility.Collapsed;
            GroupIPTypeComboBox.Visibility = Visibility.Collapsed;
            GroupIPLabel.Visibility = Visibility.Collapsed;
            GroupIPTextBox.Visibility = Visibility.Collapsed;
            IPCostLabel.Visibility = Visibility.Collapsed;
            IPCostTextBox.Visibility = Visibility.Collapsed;
            IPTimeLabel.Visibility = Visibility.Collapsed;
            IPTimeComboBox.Visibility = Visibility.Collapsed;
            IPTaxLabel.Visibility = Visibility.Collapsed;
            IPTaxTextBox.Visibility = Visibility.Collapsed;
            IPSumCostLabel.Visibility = Visibility.Collapsed;
            IPSumCostTextBox.Visibility = Visibility.Collapsed;
            IPDiscountLabel.Visibility = Visibility.Collapsed;
            IPDiscountTextBox.Visibility = Visibility.Collapsed;
            IPCostDiscountLabel.Visibility = Visibility.Collapsed;
            IPCostDiscountTextBox.Visibility = Visibility.Collapsed;

            _SumPriceIP = 0;
        }

        private void IPTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IPTypeComboBox.SelectedValue != null)
                if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                {
                    SingleIPLabel.Visibility = Visibility.Visible;
                    SingleIPTextBox.Visibility = Visibility.Visible;
                    GroupIPTypeLabel.Visibility = Visibility.Collapsed;
                    GroupIPTypeComboBox.Visibility = Visibility.Collapsed;
                    GroupIPLabel.Visibility = Visibility.Collapsed;
                    GroupIPTextBox.Visibility = Visibility.Collapsed;
                    IPCostLabel.Visibility = Visibility.Visible;
                    IPCostTextBox.Visibility = Visibility.Visible;
                    IPTimeLabel.Visibility = Visibility.Visible;
                    IPTimeComboBox.Visibility = Visibility.Visible;
                    IPTaxLabel.Visibility = Visibility.Visible;
                    IPTaxTextBox.Visibility = Visibility.Visible;
                    IPSumCostLabel.Visibility = Visibility.Visible;
                    IPSumCostTextBox.Visibility = Visibility.Visible;
                    IPDiscountLabel.Visibility = Visibility.Collapsed;
                    IPDiscountTextBox.Visibility = Visibility.Collapsed;
                    IPCostDiscountLabel.Visibility = Visibility.Collapsed;
                    IPCostDiscountTextBox.Visibility = Visibility.Collapsed;

                    IPStatic = ADSLIPDB.GetADSLIPFree((int)_WirelessRequest.CustomerGroupID).FirstOrDefault();
                    if (IPStatic != null)
                    {
                        SingleIPTextBox.Text = IPStatic.IP;
                        BaseCost cost = BaseCostDB.GetIPCostForADSL();

                        long baseCost = cost.Cost * Convert.ToInt64(serviceInfo.Duration.Split(' ')[0]);
                        IPCostTextBox.Text = baseCost.ToString() + " ریا ل";

                        if (serviceInfo.IPDiscount != 0)
                        {
                            IPDiscountLabel.Visibility = Visibility.Visible;
                            IPDiscountTextBox.Visibility = Visibility.Visible;
                            IPCostDiscountLabel.Visibility = Visibility.Visible;
                            IPCostDiscountTextBox.Visibility = Visibility.Visible;

                            IPDiscountTextBox.Text = serviceInfo.IPDiscount.ToString() + " درصد";
                            IPCostDiscountTextBox.Text = (baseCost - (baseCost * serviceInfo.IPDiscount * 0.01)).ToString() + " ریا ل";
                            baseCost = baseCost - Convert.ToInt64(baseCost * serviceInfo.IPDiscount * 0.01);
                        }

                        if (cost.Tax != null && cost.Tax != 0)
                        {
                            IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                            IPSumCostTextBox.Text = (baseCost + (baseCost * (int)cost.Tax * 0.01)).ToString() + " ریا ل";
                        }
                        else
                        {
                            IPTaxTextBox.Text = "0 درصد";
                            IPSumCostTextBox.Text = baseCost.ToString() + " ریا ل";
                        }

                        IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)serviceInfo.DurationID);
                        IPTimeComboBox.SelectedValue = serviceInfo.DurationID;

                        _SumPriceIP = cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue);

                        ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                        if (user != null)
                        {
                            if (serviceInfo.IsInstalment == true)
                                _SumPriceService = 0;

                            long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                            if (creditAgentRemain <= (_SumPriceService + _SumPriceIP + _SumPriceModem + _SumPriceTraffic + 100000))
                            {
                                SingleIPTextBox.Text = string.Empty;
                                IPCostTextBox.Text = string.Empty;
                                IPDiscountTextBox.Text = string.Empty;
                                IPCostDiscountTextBox.Text = string.Empty;
                                IPTaxTextBox.Text = string.Empty;
                                IPSumCostTextBox.Text = string.Empty;
                                IPTimeComboBox.ItemsSource = null;
                                _HasCreditAgent = false;

                                ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                                return;
                            }

                            long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                            if (creditUserRemain <= (_SumPriceService + _SumPriceIP + _SumPriceModem + _SumPriceTraffic + 100000))
                            {
                                SingleIPTextBox.Text = string.Empty;
                                IPCostTextBox.Text = string.Empty;
                                IPDiscountTextBox.Text = string.Empty;
                                IPCostDiscountTextBox.Text = string.Empty;
                                IPTaxTextBox.Text = string.Empty;
                                IPSumCostTextBox.Text = string.Empty;
                                IPTimeComboBox.ItemsSource = null;
                                _HasCreditUser = false;

                                ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                                return;
                            }
                        }

                        ErrorCreditLabel.Content = string.Empty;
                        _HasCreditAgent = true;
                        _HasCreditUser = true;
                    }
                    else
                        SingleIPTextBox.Text = "IP تکی خالی موجود نمی باشد.";
                }
                else
                {
                    SingleIPLabel.Visibility = Visibility.Collapsed;
                    SingleIPTextBox.Visibility = Visibility.Collapsed;
                    GroupIPTypeLabel.Visibility = Visibility.Visible;
                    GroupIPTypeComboBox.Visibility = Visibility.Visible;
                    GroupIPLabel.Visibility = Visibility.Visible;
                    GroupIPTextBox.Visibility = Visibility.Visible;
                    IPCostLabel.Visibility = Visibility.Visible;
                    IPCostTextBox.Visibility = Visibility.Visible;
                    IPTimeLabel.Visibility = Visibility.Visible;
                    IPTimeComboBox.Visibility = Visibility.Visible;
                    IPTaxLabel.Visibility = Visibility.Visible;
                    IPTaxTextBox.Visibility = Visibility.Visible;
                    IPSumCostLabel.Visibility = Visibility.Visible;
                    IPSumCostTextBox.Visibility = Visibility.Visible;

                    if (serviceInfo.IPDiscount != 0)
                    {
                        IPDiscountLabel.Visibility = Visibility.Visible;
                        IPDiscountTextBox.Visibility = Visibility.Visible;
                        IPCostDiscountLabel.Visibility = Visibility.Visible;
                        IPCostDiscountTextBox.Visibility = Visibility.Visible;
                    }

                    GroupIPTypeComboBox.SelectedValue = null;
                    GroupIPTextBox.Text = string.Empty;
                    IPCostTextBox.Text = string.Empty;
                    IPTimeComboBox.Text = string.Empty;
                    IPTaxTextBox.Text = string.Empty;
                    IPSumCostTextBox.Text = string.Empty;
                    IPDiscountTextBox.Text = string.Empty;
                    IPCostDiscountTextBox.Text = string.Empty;
                }
        }

        private void GroupIPTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupIPTypeComboBox.SelectedValue != null)
            {
                GroupIPStatic = ADSLIPDB.GetADSLGroupIPFree(Convert.ToInt32(GroupIPTypeComboBox.SelectedValue), (int)_WirelessRequest.CustomerGroupID).FirstOrDefault();

                if (GroupIPStatic != null)
                {
                    GroupIPLabel.Visibility = Visibility.Visible;
                    GroupIPTextBox.Visibility = Visibility.Visible;
                    ErrorGroupIPLabel.Visibility = Visibility.Collapsed;

                    BaseCost cost = BaseCostDB.GetIPCostForADSL();

                    if (serviceInfo.DurationID != null)
                        _SumPriceIP = cost.Cost * GroupIPStatic.BlockCount * serviceInfo.DurationID;

                    ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                    if (user != null)
                    {
                        if (serviceInfo.IsInstalment == true)
                            _SumPriceService = 0;

                        long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                        if (creditAgentRemain <= (_SumPriceService + _SumPriceIP + _SumPriceModem + _SumPriceTraffic + 100000))
                        {
                            GroupIPLabel.Visibility = Visibility.Collapsed;
                            GroupIPTextBox.Visibility = Visibility.Collapsed;
                            IPCostTextBox.Text = string.Empty;
                            IPTimeComboBox.Text = string.Empty;
                            IPTaxTextBox.Text = string.Empty;
                            IPSumCostTextBox.Text = string.Empty;
                            _HasCreditAgent = false;

                            ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                            return;
                        }

                        long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                        if (creditUserRemain <= (_SumPriceService + _SumPriceIP + _SumPriceModem + _SumPriceTraffic + 100000))
                        {
                            GroupIPLabel.Visibility = Visibility.Collapsed;
                            GroupIPTextBox.Visibility = Visibility.Collapsed;
                            IPCostTextBox.Text = string.Empty;
                            IPTimeComboBox.Text = string.Empty;
                            IPTaxTextBox.Text = string.Empty;
                            IPSumCostTextBox.Text = string.Empty;
                            _HasCreditUser = false;

                            ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                            return;
                        }
                    }

                    ErrorCreditLabel.Content = string.Empty;
                    _HasCreditAgent = true;
                    _HasCreditUser = true;

                    GroupIPTextBox.Text = GroupIPStatic.StartRange;
                    if (serviceInfo.DurationID != null)
                    {
                        IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)serviceInfo.DurationID);

                        long baseCost = cost.Cost * GroupIPStatic.BlockCount * serviceInfo.DurationID;
                        IPCostTextBox.Text = baseCost.ToString() + " ریا ل";

                        if (serviceInfo.IPDiscount != 0)
                        {
                            IPDiscountLabel.Visibility = Visibility.Visible;
                            IPDiscountTextBox.Visibility = Visibility.Visible;
                            IPCostDiscountLabel.Visibility = Visibility.Visible;
                            IPCostDiscountTextBox.Visibility = Visibility.Visible;

                            IPDiscountTextBox.Text = serviceInfo.IPDiscount.ToString() + " درصد";
                            IPCostDiscountTextBox.Text = (baseCost - (baseCost * serviceInfo.IPDiscount * 0.01)).ToString() + " ریا ل";
                            baseCost = baseCost - Convert.ToInt64(baseCost * serviceInfo.IPDiscount * 0.01);
                        }

                        if (cost.Tax != null && cost.Tax != 0)
                        {
                            IPTaxTextBox.Text = cost.Tax + " درصد";
                            IPSumCostTextBox.Text = (baseCost + (baseCost * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                        }
                        else
                        {
                            IPTaxTextBox.Text = "0 درصد";
                            IPSumCostTextBox.Text = IPCostTextBox.Text;
                        }

                        IPTimeComboBox.SelectedValue = serviceInfo.DurationID;
                    }

                }
                else
                {
                    GroupIPLabel.Visibility = Visibility.Collapsed;
                    GroupIPTextBox.Visibility = Visibility.Collapsed;
                    ErrorGroupIPLabel.Visibility = Visibility.Visible;
                    IPCostTextBox.Text = string.Empty;
                    IPTimeComboBox.Text = string.Empty;
                    IPTaxTextBox.Text = string.Empty;
                    IPSumCostTextBox.Text = string.Empty;
                }
            }
        }

        private void NeedModemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ModemTypeLabel.Visibility = Visibility.Visible;
            ModemTypeComboBox.Visibility = Visibility.Visible;
            ModemCostLabel.Visibility = Visibility.Visible;
            ModemCostTextBox.Visibility = Visibility.Visible;
            ModemSerilaNoLabel.Visibility = Visibility.Visible;
            ModemSerilaNoComboBox.Visibility = Visibility.Visible;
            ModemMACAddressLabel.Visibility = Visibility.Visible;
            ModemMACAddressTextBox.Visibility = Visibility.Visible;

            ModemTypeComboBox.SelectedValue = null;
            ModemSerilaNoComboBox.SelectedValue = null;
            ModemSerilaNoComboBox.ItemsSource = null;
            ModemCostTextBox.Text = string.Empty;
        }

        private void NeedModemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ModemTypeLabel.Visibility = Visibility.Collapsed;
            ModemTypeComboBox.Visibility = Visibility.Collapsed;
            ModemCostLabel.Visibility = Visibility.Collapsed;
            ModemCostTextBox.Visibility = Visibility.Collapsed;
            ModemSerilaNoLabel.Visibility = Visibility.Collapsed;
            ModemSerilaNoComboBox.Visibility = Visibility.Collapsed;
            ModemMACAddressLabel.Visibility = Visibility.Collapsed;
            ModemMACAddressTextBox.Visibility = Visibility.Collapsed;

            ModemTypeComboBox.SelectedValue = null;
            ModemSerilaNoComboBox.ItemsSource = null;
            ModemSerilaNoComboBox.SelectedValue = null;
            ModemCostTextBox.Text = string.Empty;

            _SumPriceModem = 0;
        }

        private void ModemTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ADSLModem modem = null;

            if (serviceInfo != null)
            {
                if (ModemTypeComboBox.SelectedValue != null)
                {
                    modem = ADSLModemDB.GetADSLModemById((int)ModemTypeComboBox.SelectedValue);
                    ModemSerilaNoComboBox.ItemsSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);

                    if (modem != null)
                    {
                        ModemCostTextBox.Text = modem.Price + " ریا ل";

                        if (serviceInfo.ModemDiscount != 0)
                        {
                            ModemDiscountTextBox.Text = serviceInfo.ModemDiscount.ToString() + " درصد";
                            ModemCostDiscountTextBox.Text = (modem.Price - (modem.Price * serviceInfo.ModemDiscount * 0.01)).ToString() + " ریا ل";
                        }
                    }
                }
                else
                {
                    modem = ADSLModemDB.GetADSLModemById((int)_WirelessRequest.ModemID);
                    ModemSerilaNoComboBox.ItemsSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);

                    if (modem != null)
                    {
                        ModemCostTextBox.Text = modem.Price + " ریا ل";
                        if (serviceInfo.ModemDiscount != 0)
                        {
                            ModemDiscountTextBox.Text = serviceInfo.ModemDiscount.ToString() + " درصد";
                            ModemCostDiscountTextBox.Text = (modem.Price - (modem.Price * serviceInfo.ModemDiscount * 0.01)).ToString() + " ریا ل";
                        }
                    }
                }


                _SumPriceModem = Convert.ToInt64(modem.Price);

                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                if (user != null)
                {
                    if (serviceInfo.IsInstalment == true)
                        _SumPriceService = 0;

                    long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditAgentRemain <= (_SumPriceService + _SumPriceModem + _SumPriceIP + _SumPriceTraffic + 100000))
                    {
                        ModemTypeComboBox.SelectedValue = null;
                        ModemSerilaNoComboBox.ItemsSource = null;
                        ModemCostTextBox.Text = string.Empty;
                        _HasCreditAgent = false;

                        ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= (_SumPriceService + _SumPriceModem + _SumPriceIP + _SumPriceTraffic + 100000))
                    {
                        ModemTypeComboBox.SelectedValue = null;
                        ModemSerilaNoComboBox.ItemsSource = null;
                        ModemCostTextBox.Text = string.Empty;
                        _HasCreditUser = false;

                        ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                        return;
                    }
                }

                ErrorCreditLabel.Content = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;
            }
        }

        private void IPTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IPTimeComboBox.SelectedValue != null)
            {
                BaseCost cost = BaseCostDB.GetIPCostForADSL();

                //if (_WirelessRequest.HasIP != null)
                //    if ((bool)_WirelessRequest.HasIP)
                //    {
                if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                {
                    IPCostTextBox.Text = (cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";
                    IPSumCostTextBox.Text = ((cost.Cost * Convert.ToInt32(IPTimeComboBox.SelectedValue)) + ((cost.Cost * Convert.ToInt32(IPTimeComboBox.SelectedValue)) * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                }
                if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Group)
                {
                    IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";
                    IPSumCostTextBox.Text = ((cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt32(IPTimeComboBox.SelectedValue)) + (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt32(IPTimeComboBox.SelectedValue) * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                }
                //}
            }
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupComboBox.SelectedValue = null;
            BandWidthComboBox.SelectedValue = null;
            DurationComboBox.SelectedValue = null;
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            ServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                //BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                //  DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                //  TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
            {
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);
            }

            if (sellerAgentID == 0)
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, true);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList, true);
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BandWidthComboBox.SelectedValue = null;
            DurationComboBox.SelectedValue = null;
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            ServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                //GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID, typeID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                //DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                //TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
            {
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);
            }

            if (sellerAgentID == 0)
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, true);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList, true);
        }

        private void BandWidthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DurationComboBox.SelectedValue = null;
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            ServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                // GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                //BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID, groupID, typeID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                //TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
            {
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);
            }

            if (sellerAgentID == 0)
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, true);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList, true);
        }

        private void DurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrafficComboBox.SelectedValue = null;

            ServiceComboBox.SelectedValue = null;
            ServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                //GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                //BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                //  DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID, bandWidthID, groupID, typeID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
            {
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);
            }

            if (sellerAgentID == 0)
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, true);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList, true);
        }

        private void TrafficComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceComboBox.SelectedValue = null;
            ServiceInfo.DataContext = null;

            int typeID = -1;
            if (TypeComboBox.SelectedValue != null)
            {
                typeID = Convert.ToInt32(TypeComboBox.SelectedValue);
                //GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
            }

            int groupID = -1;
            if (GroupComboBox.SelectedValue != null)
            {
                groupID = Convert.ToInt32(GroupComboBox.SelectedValue);
                //BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupID(groupID);
            }

            int bandWidthID = -1;
            if (BandWidthComboBox.SelectedValue != null)
            {
                bandWidthID = Convert.ToInt32(BandWidthComboBox.SelectedValue);
                //  DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckablebyBandWidthID(bandWidthID);
            }

            int durationID = -1;
            if (DurationComboBox.SelectedValue != null)
            {
                durationID = Convert.ToInt32(DurationComboBox.SelectedValue);
                //  TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationID(durationID);
            }

            int trafficID = -1;
            if (TrafficComboBox.SelectedValue != null)
            {
                trafficID = Convert.ToInt32(TrafficComboBox.SelectedValue);
            }

            if (sellerAgentID == 0)
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, true);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_WirelessRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList, true);
        }

        private void ModemSerilaNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModemSerilaNoComboBox.SelectedValue != null)
            {
                ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ModemSerilaNoComboBox.SelectedValue);
                ModemMACAddressTextBox.Text = modemProperty.MACAddress;
            }
            else
                if (_WirelessRequest.ModemSerialNoID != null)
                {
                    ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_WirelessRequest.ModemSerialNoID);
                    ModemMACAddressTextBox.Text = modemProperty.MACAddress;
                }
        }

        private void TelephoneDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneDataGrid.SelectedItem != null)
            {
                System.Data.DataRowView row = (System.Data.DataRowView)TelephoneDataGrid.SelectedItem;

                _telephoneNo = Convert.ToInt64(row["PHONENO"]);
                MobileNoTextBox.Text = row["MOBILE"].ToString();
                _TelephoneInformation = new TelephoneInformation(Convert.ToInt64(row["PHONENO"]), (int)DB.RequestType.Wireless);
                //TelephoneInfo.Content = _TelephoneInformation;
                //TelephoneInfo.DataContext = _TelephoneInformation;


                string postallCode = row["CODE_POSTI"].ToString();
                if (!string.IsNullOrEmpty(postallCode))
                {
                    _installAddress = Data.AddressDB.GetAddressByPostalCode(postallCode).Take(1).SingleOrDefault();

                    if (_installAddress == null && postallCode != string.Empty)
                        _installAddress = new Address();

                    if (_installAddress != null)
                    {
                        _installAddress.CenterID = _CenterID;
                        _installAddress.AddressContent = row["ADDRESS"].ToString();
                        _installAddress.PostalCode = row["CODE_POSTI"].ToString();
                        _installAddress.Detach();
                        DB.Save(_installAddress);

                        InstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                        SearchInstallAddress_Click(null, null);
                    }
                }
            }
        }

        #endregion
    }
}
