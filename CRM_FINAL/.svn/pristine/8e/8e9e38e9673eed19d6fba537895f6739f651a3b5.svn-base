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
using CRM.Data;
using CRM.Application.Views;

namespace CRM.Application.UserControls
{
    public partial class ADSL : UserControl
    {
        #region Properties

        private long _RequsetID = 0;
        private long _CustomerID = 0;
        private int _CenterID = 0;
        private int _CityID = 0;

        private CRM.Data.ADSLRequest _ADSLRequest { get; set; }
        private Request _Request { get; set; }
        private Telephone _Telephone { get; set; }
        private Bucht _Bucht { get; set; }
        private Data.ADSL _ADSL { get; set; }

        public long TelephoneNo { get; set; }
        public bool _IsWaitingList { get; set; }
        public Customer ADSLCustomer { get; set; }
        public List<Customer> ADSLCustomerList { get; set; }
        public ADSLIP IPStatic { get; set; }
        public ADSLGroupIP GroupIPStatic { get; set; }
        private ADSLServiceInfo serviceInfo { get; set; }

        private Service1 service = new Service1();
        public System.Data.DataTable telephoneInfo { get; set; }

        public long _SumPriceService = 0;
        public long _SumPriceTraffic = 0;
        public long _SumPriceIP = 0;
        public long _SumPriceModem = 0;

        public bool _WasPCM = false;
        public bool _HasCreditAgent = true;
        public bool _HasCreditUser = true;

        private int sellerAgentID { get; set; }
        private List<int> _ServiceAccessList { get; set; }
        private List<int> _ServiceGroupAccessList { get; set; }

        #endregion

        #region Costructors

        public ADSL()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public ADSL(long requestID, long customerID, long telephoneNo)
            : this()
        {
            _RequsetID = requestID;
            _CustomerID = customerID;
            TelephoneNo = telephoneNo;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ADSLCustomer = new Customer();

            ADSLOwnerStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupList();
            CustomerTypeComboBox.ItemsSource = ADSLCustomerTypeDB.GetCustomerTypeList();
            JobGroupComboBox.ItemsSource = DB.GetAllEntity<JobGroup>();
            //CustomerPriorityComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLCustomerPriority));
            //RegisterProjectTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLRegistrationProjectType));
            ModemTypeComboBox.ItemsSource = ADSLModemDB.GetSalableModemsTitle();
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLService();
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
            AdditionalServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLAdditionalService();
            IPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPType));
            GroupIPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLGroupIPBlockCount));
        }

        private void DisableControls()
        {
            ADSLOwnerStatusComboBox.IsEnabled = false;
            NationalCodeTextBox.IsReadOnly = true;
            CustomerNameTextBox.IsReadOnly = true;
            //MobileNoTextBox.IsReadOnly = true;
            searchButton.Visibility = Visibility.Collapsed;
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

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            #region ADSL Without Web Service

            //if (_ReqID == 0)
            //{
            //    List<Data.ADSLPort> emptyPorts = DB.SearchByPropertyName<Data.ADSLPort>("Status", (byte)DB.ADSLPortStatus.Free);
            //    if (emptyPorts.Count < 20)
            //    {
            //        PortLabel.Content = " * در حال حاضر تجهیزات کافی برای دایری ADSL موجود نمی باشد !";
            //        PortLabel.Visibility = Visibility.Visible;
            //        _IsWaitingList = true;
            //    }

            //    _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);
            //    _Bucht = Data.BuchtDB.GetBuchetBySwitchPortand((int)_Telephone.SwitchPortID);
            //    if ((_Bucht.BuchtTypeID == (byte)DB.BuchtType.InLine) || (_Bucht.BuchtTypeID == (byte)DB.BuchtType.OutLine))
            //    {
            //        PCMLabel.Content = " * نیاز به تعویض بوخت می باشد !";
            //        PCMLabel.Visibility = Visibility.Visible;
            //        _IsWaitingList = true;
            //    }

            //    SwitchPort switchPort = Data.SwitchPortDB.GetSwitchPortByID((int)_Telephone.SwitchPortID);
            //    Switch switch1 = Data.SwitchDB.GetSwitchByID((int)switchPort.SwitchID);
            //    SwitchType switchType = DB.SearchByPropertyName<SwitchType>("ID", switch1.SwitchTypeID).SingleOrDefault();

            //    switch (switchType.SwitchType1)
            //    {
            //        case (byte)DB.SwitchTypeCode.ONUABWire:
            //        case (byte)DB.SwitchTypeCode.ONUCopper:
            //        case (byte)DB.SwitchTypeCode.ONUVWire:
            //            List<SwitchPort> switchPortList = DB.SearchByPropertyName<SwitchPort>("SwitchID", switch1.ID);

            //            int count = 0;

            //            foreach (SwitchPort item in switchPortList)
            //            {
            //                if (DB.SearchByPropertyName<Telephone>("SwitchPortID", item.ID).SingleOrDefault() != null)
            //                    count = count + 1;
            //            }
            //            if (count > (switch1.OperationalCapacity * (Decimal.Divide((decimal)switch1.DataCapacity, 100))))
            //            {
            //                ONULabel.Content = " * این شماره روی ONU می باشد !";
            //                ONULabel.Visibility = Visibility.Visible;
            //                _IsWaitingList = true;
            //            }
            //            break;

            //        default:
            //            break;
            //    }

            //    if (_IsWaitingList)
            //        ADSlCheckPossibilityGroupBox.Visibility = Visibility.Visible;

            //    ADSLOwnerStatusComboBox.SelectedValue = (byte)DB.ADSLOwnerStatus.Owner;
            //    ADSLCustomer = Data.CustomerDB.GetCustomerByID(_CustomerID);
            //    NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
            //    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
            //    NationalCodeTextBox.IsReadOnly = true;
            //    searchButton.IsEnabled = false;

            //    ServiceTypeComboBox.SelectedValue = (byte)DB.ADSLServiceType.Internet;
            //    ServiceCostComboBox.SelectedValue = (byte)DB.ADSLServiceCostPaymentType.PostPaid;
            //    CustomerPriorityComboBox.SelectedValue = (byte)DB.ADSLCustomerPriority.Normal;
            //    RegisterProjectTypeComboBox.SelectedValue = (byte)DB.ADSLRegistrationProjectType.None;
            //    RequiredInstalationCheckBox.IsChecked = true;
            //    NeedModemCheckBox.IsChecked = true;
            //}
            //else
            //{
            //    _Request = Data.RequestDB.GetRequestByID(_ReqID);
            //    _ADSLRequest = DB.SearchByPropertyName<CRM.Data.ADSLRequest>("ID", _ReqID).Take(1).SingleOrDefault();

            //    ADSLOwnerStatusComboBox.SelectedValue = _ADSLRequest.CustomerOwnerStatus;
            //    ADSLCustomer = Data.CustomerDB.GetCustomerByID(_ADSLRequest.CustomerOwnerID);
            //    NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
            //    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
            //    NationalCodeTextBox.IsReadOnly = true;
            //    searchButton.IsEnabled = false;
            //    CustomerEndRentDate.SelectedDate = _ADSLRequest.CustomerEndRentDate;

            //    ServiceTypeComboBox.SelectedValue = _ADSLRequest.ServiceType;
            //    ServiceCostComboBox.SelectedValue = _ADSLRequest.ServiceCostPaymentType;
            //    CustomerPriorityComboBox.SelectedValue = _ADSLRequest.CustomerPriority;
            //    RegisterProjectTypeComboBox.SelectedValue = _ADSLRequest.RegistrationProjectType;
            //    TariffComboBox.SelectedValue = _ADSLRequest.TariffID;
            //    LicenceLetterNoTextBox.Text = _ADSLRequest.LicenseLetterNo;
            //    RequiredInstalationCheckBox.IsChecked = _ADSLRequest.RequiredInstalation;
            //    NeedModemCheckBox.IsChecked = _ADSLRequest.NeedModem;
            //    if ((bool)_ADSLRequest.NeedModem)
            //        ModemTypeComboBox.SelectedValue = _ADSLRequest.ModemID;
            //    else
            //    {
            //        ModemTypeComboBox.SelectedValue = -1;
            //        ModemTypeComboBox.IsEnabled = false;
            //    }

            //    if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID) && (_Request.PreviousAction == (byte)DB.Action.Reject))
            //    {
            //        CommentsGroupBox.Visibility = Visibility.Visible;
            //        CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
            //        CommentCustomersTextBox.Foreground = Brushes.Red;
            //    }

            //    if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            //    {
            //        DisableControls();

            //        CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
            //        CommentsGroupBox.Visibility = Visibility.Visible;
            //    }

            //    if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Observation).ID)
            //    {
            //        DisableControls();

            //        CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
            //        CommentCustomersTextBox.IsReadOnly = true;

            //        CommentsGroupBox.Visibility = Visibility.Visible;

            //        PortInfo.DataContext = ADSLPortDB.GetADSlPortInfoByID((long)_ADSLRequest.ADSLPortID);
            //        ADSLPortGroupBox.Visibility = Visibility.Visible;
            //    }
            //}

            #endregion

            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", TelephoneNo.ToString());

            if (telephoneInfo.Rows.Count != 0)
                _CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
            else
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);
                if (telephone != null)
                    _CenterID = telephone.CenterID;
            }
            _CityID = CityDB.GetCityByCenterID(_CenterID).ID;

            List<Data.ADSLPort> emptyPorts = ADSLPortDB.GetFreePortListbyeCenterID(_CenterID);

            string mDFDescription = ADSLMDFDB.GetMDFDescriptionbyTelephoneNo(TelephoneNo, _CenterID);
            if (!string.IsNullOrWhiteSpace(mDFDescription))
                MDFInfoLabel.Content = "این شماره تلفن بر روی ام دی اف، " + mDFDescription + " قرار دارد.";
            else
                MDFInfoLabel.Content = "در اطلاعات فنی رنجی برای این شماره تلفن تعیین نشده است !";

            sellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);

            if (_RequsetID == 0)
            {
                ADSLOwnerStatusComboBox.SelectedValue = (byte)DB.ADSLOwnerStatus.Owner;
                if (_CustomerID != 0)
                {
                    ADSLCustomer = Data.CustomerDB.GetCustomerByID(_CustomerID);
                    if (ADSLCustomer.NationalCodeOrRecordNo != null)
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
                    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
                    if (telephoneInfo.Rows.Count != 0)
                        MobileNoTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();
                }
                else
                {
                    ADSLCustomer.CustomerID = "000000000000000";
                    ADSLCustomer.FirstNameOrTitle = telephoneInfo.Rows[0]["FIRSTNAME"].ToString();
                    ADSLCustomer.LastName = telephoneInfo.Rows[0]["LASTNAME"].ToString();
                    ADSLCustomer.FatherName = telephoneInfo.Rows[0]["FATHERNAME"].ToString();
                    ADSLCustomer.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                    ADSLCustomer.BirthCertificateID = telephoneInfo.Rows[0]["SHENASNAME"].ToString();
                    ADSLCustomer.MobileNo = telephoneInfo.Rows[0]["MOBILE"].ToString();
                    ADSLCustomer.Email = telephoneInfo.Rows[0]["EMAIL"].ToString();

                    ADSLCustomer.Detach();
                    DB.Save(ADSLCustomer, true);
                }

                NationalCodeTextBox.IsReadOnly = true;
                searchButton.IsEnabled = false;
                ADSLServiceGroupBox.Visibility = Visibility.Collapsed;
                ADSLServiceAdditionalGroupBox.Visibility = Visibility.Collapsed;
                ADSLIPGroupBox.Visibility = Visibility.Collapsed;
                ADSLFacilitiesGroupBox.Visibility = Visibility.Collapsed;

                if (emptyPorts.Count == 0)
                {
                    PortErrorLabel.Visibility = Visibility.Visible;
                    _IsWaitingList = true;
                }

                if (sellerAgentID != 0)
                {
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(sellerAgentID);
                    CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupbyServiceGroupList(_ServiceGroupAccessList);
                }
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequsetID);
                _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(_RequsetID);

                if (sellerAgentID == 0)
                {
                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup((int)_ADSLRequest.CustomerGroupID);
                    GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupID((int)_ADSLRequest.CustomerGroupID);
                }
                else
                {
                    _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(sellerAgentID);
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(sellerAgentID);

                    ServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent((int)_ADSLRequest.CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList);
                    GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent((int)_ADSLRequest.CustomerGroupID, _ServiceGroupAccessList);
                    CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupbyServiceGroupList(_ServiceGroupAccessList);
                }

                ADSLOwnerStatusComboBox.SelectedValue = _ADSLRequest.CustomerOwnerStatus;
                if (_CustomerID != 0)
                {
                    ADSLCustomer = Data.CustomerDB.GetCustomerByID(_ADSLRequest.CustomerOwnerID);
                    if (ADSLCustomer.NationalCodeOrRecordNo != null)
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
                    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
                    if (!string.IsNullOrWhiteSpace(ADSLCustomer.MobileNo))
                        MobileNoTextBox.Text = ADSLCustomer.MobileNo;
                }

                NationalCodeTextBox.IsReadOnly = true;
                searchButton.IsEnabled = false;
                CustomerEndRentDate.SelectedDate = _ADSLRequest.CustomerEndRentDate;

                CustomerGroupComboBox.SelectedValue = _ADSLRequest.CustomerGroupID;
                CustomerTypeComboBox.SelectedValue = _ADSLRequest.CustomerTypeID;
                JobGroupComboBox.SelectedValue = _ADSLRequest.JobGroupID;
                ReagentTelephoneNoTextBox.Text = _ADSLRequest.ReagentTelephoneNo;
                //if (_ADSLRequest.CustomerPriority == null)
                //    CustomerPriorityComboBox.SelectedValue = (byte)DB.ADSLCustomerPriority.Normal;
                //else
                //CustomerPriorityComboBox.SelectedValue = _ADSLRequest.CustomerPriority;
                //RegisterProjectTypeComboBox.SelectedValue = _ADSLRequest.RegistrationProjectType;
                ServiceComboBox.SelectedValue = _ADSLRequest.ServiceID;

                ADSLService currentService = new ADSLService();
                if (_ADSLRequest.ServiceID != null)
                    currentService = ADSLServiceDB.GetADSLServiceById((int)_ADSLRequest.ServiceID);

                if (_ADSLRequest.AdditionalServiceID != null)
                {
                    HasAdditionalTrafficCheckBox.IsChecked = true;
                    AdditionalTrafficInfoGrid.Visibility = Visibility.Visible;
                    AdditionalServiceComboBox.SelectedValue = _ADSLRequest.AdditionalServiceID;
                }

                if (_ADSLRequest.HasIP == null)
                {
                    HasIPStaticCheckBox.IsChecked = false;
                    IPTypeComboBox.SelectedValue = null;
                    IPTypeComboBox.IsEnabled = false;
                }
                else
                {
                    HasIPStaticCheckBox.IsChecked = _ADSLRequest.HasIP;

                    if ((bool)_ADSLRequest.HasIP)
                    {
                        if (_ADSLRequest.IPStaticID != null)
                        {
                            IPStatic = ADSLIPDB.GetADSLIPById((long)_ADSLRequest.IPStaticID);

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
                            if (_ADSLRequest.IPDuration != null)
                            {
                                IPCostTextBox.Text = (cost.Cost * _ADSLRequest.IPDuration).ToString() + " ریا ل";
                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * _ADSLRequest.IPDuration) + (cost.Cost * _ADSLRequest.IPDuration * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }
                                IPTimeComboBox.SelectedValue = _ADSLRequest.IPDuration;
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
                        if (_ADSLRequest.GroupIPStaticID != null)
                        {
                            GroupIPStatic = ADSLIPDB.GetADSLGroupIPById((long)_ADSLRequest.GroupIPStaticID);

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

                            if (_ADSLRequest.IPDuration != null)
                            {
                                IPCostTextBox.Text = (cost.Cost * _ADSLRequest.IPDuration * GroupIPStatic.BlockCount).ToString() + " ریا ل";
                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * _ADSLRequest.IPDuration * GroupIPStatic.BlockCount) + (cost.Cost * _ADSLRequest.IPDuration * GroupIPStatic.BlockCount * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }
                                IPTimeComboBox.SelectedValue = _ADSLRequest.IPDuration;
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

                LicenceLetterNoTextBox.Text = _ADSLRequest.LicenseLetterNo;
                if (_ADSLRequest.RequiredInstalation == null)
                    RequiredInstalationCheckBox.IsChecked = false;
                else
                    RequiredInstalationCheckBox.IsChecked = _ADSLRequest.RequiredInstalation;

                if (_ADSLRequest.NeedModem == null)
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
                    NeedModemCheckBox.IsChecked = _ADSLRequest.NeedModem;

                    if ((bool)_ADSLRequest.NeedModem)
                    {
                        ModemTypeComboBox.SelectedValue = _ADSLRequest.ModemID;
                        if (_ADSLRequest.ModemSerialNoID != null)
                            ModemSerilaNoComboBox.SelectedValue = _ADSLRequest.ModemSerialNoID;
                        ModemMACAddressTextBox.Text = _ADSLRequest.ModemMACAddress;

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

                    if (emptyPorts.Count == 0)
                    {
                        PortErrorLabel.Visibility = Visibility.Visible;
                        //NextStatusLabel.Visibility = Visibility.Visible;
                        //NextStatusListBox.Visibility = Visibility.Visible;
                        _IsWaitingList = true;
                    }
                }

                if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID) && (_Request.PreviousAction == (byte)DB.Action.Reject))
                {
                    CommentsGroupBox.Visibility = Visibility.Visible;
                    CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
                    CommentCustomersTextBox.Foreground = Brushes.Red;
                    CommentCustomersTextBox.IsReadOnly = true;
                }

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                {
                    ADSLOwnerStatusComboBox.IsEnabled = false;
                    NationalCodeTextBox.IsReadOnly = true;
                    CustomerNameTextBox.IsReadOnly = true;
                    //MobileNoTextBox.IsReadOnly = true;
                    searchButton.Visibility = Visibility.Collapsed;
                    CustomerGroupComboBox.IsEnabled = false;
                    CustomerTypeComboBox.IsEnabled = false;
                    JobGroupComboBox.IsEnabled = false;
                    ReagentTelephoneNoTextBox.IsReadOnly = true;
                    //DisableControls();

                    CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
                    CommentsGroupBox.Visibility = Visibility.Visible;

                    if (emptyPorts.Count == 0)
                    {
                        PortErrorLabel.Visibility = Visibility.Visible;
                        //NextStatusLabel.Visibility = Visibility.Collapsed;
                        //NextStatusListBox.Visibility = Visibility.Collapsed;
                        _IsWaitingList = true;
                    }
                }

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Observation).ID)
                {
                    DisableControls();

                    CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
                    CommentCustomersTextBox.IsReadOnly = true;

                    CommentsGroupBox.Visibility = Visibility.Visible;

                    PortInfo.DataContext = ADSLPortDB.GetADSlPortInfoByID((long)_ADSLRequest.ADSLPortID);
                    ADSLPortGroupBox.Visibility = Visibility.Visible;
                }
            }

            Service1 aDSLService = new Service1();
            if (aDSLService.Phone_Is_PCM(TelephoneNo.ToString()))
            {
                PCMErrorrLabel.Visibility = Visibility.Visible;
                _WasPCM = true;
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;
                MobileNoTextBox.Text = string.Empty;

                ADSLCustomer = null;
                ADSLCustomerList = null;
                ADSLCustomerList = CustomerDB.GetCustomerListByNationalCode(NationalCodeTextBox.Text.Trim());


                if (ADSLCustomerList != null)
                {
                    if (ADSLCustomerList.Count == 1)
                    {
                        ADSLCustomer = ADSLCustomerList[0];
                        NationalCodeTextBox.Text = string.Empty;
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                        MobileNoTextBox.Text = string.Empty;
                        MobileNoTextBox.Text = ADSLCustomer.MobileNo;
                    }
                    else
                    {
                        CustomerSearchForm customerSearchForm = new CustomerSearchForm(NationalCodeTextBox.Text.Trim());
                        customerSearchForm.ShowDialog();
                        if (customerSearchForm.DialogResult ?? false)
                        {
                            ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerSearchForm.ID);

                            NationalCodeTextBox.Text = string.Empty;
                            NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                            CustomerNameTextBox.Text = string.Empty;
                            CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                            MobileNoTextBox.Text = string.Empty;
                            MobileNoTextBox.Text = ADSLCustomer.MobileNo;
                        }
                    }
                }
                else
                {
                    CustomerSearchForm customerSearchForm = new CustomerSearchForm();
                    customerSearchForm.ShowDialog();
                    if (customerSearchForm.DialogResult ?? false)
                    {
                        ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerSearchForm.ID);

                        NationalCodeTextBox.Text = string.Empty;
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                        MobileNoTextBox.Text = string.Empty;
                        MobileNoTextBox.Text = ADSLCustomer.MobileNo;
                    }

                    //CustomerForm customerForm = new CustomerForm();
                    //customerForm.ShowDialog();
                    //if (customerForm.DialogResult ?? false)
                    //{
                    //    ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    //    CustomerNameTextBox.Text = string.Empty;
                    //    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                    //}
                }


            }
            else
            {
                CustomerSearchForm customerSearchForm = new CustomerSearchForm();
                customerSearchForm.ShowDialog();
                if (customerSearchForm.DialogResult ?? false)
                {
                    ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerSearchForm.ID);
                    NationalCodeTextBox.Text = string.Empty;
                    NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                    MobileNoTextBox.Text = string.Empty;
                    MobileNoTextBox.Text = ADSLCustomer.MobileNo;

                }
                //CustomerForm customerForm = new CustomerForm();
                //customerForm.ShowDialog();
                //if (customerForm.DialogResult ?? false)
                //{
                //    ADSLCustomer = DB.GetEntitybyID<Customer>(customerForm.ID);
                //    CustomerNameTextBox.Text = string.Empty;
                //    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                //}
            }
        }

        private void EditCustomerButto_Click(object sender, RoutedEventArgs e)
        {
            if (ADSLCustomer != null)
            {
                CustomerForm window = new CustomerForm(ADSLCustomer.ID);
                window.ShowDialog();
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
                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", TelephoneNo.ToString());

                    NationalCodeTextBox.Text = telephoneInfo.Rows[0]["MelliCode"].ToString();
                    CustomerNameTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                    MobileNoTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();

                    NationalCodeTextBox.IsReadOnly = true;
                    searchButton.IsEnabled = false;

                    CustomerEndRentLabel.Visibility = Visibility.Collapsed;
                    CustomerEndRentDate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    NationalCodeTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = string.Empty;
                    MobileNoTextBox.Text = string.Empty;

                    NationalCodeTextBox.IsReadOnly = false;
                    searchButton.IsEnabled = true;
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
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList);
            //else
            //{
            //    if (_ADSLRequest.ServiceID != 0)
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
                if (_ADSLRequest.ServiceID != 0)
                    serviceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLRequest.ServiceID);
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
            _ADSLRequest.AdditionalServiceID = null;

            _SumPriceTraffic = 0;
        }

        private void AdditionalServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ADSLServiceInfo additionalServiceInfo = new ADSLServiceInfo();

            if (AdditionalServiceComboBox.SelectedValue != null)
                additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)AdditionalServiceComboBox.SelectedValue);
            else
            {
                if (_ADSLRequest.AdditionalServiceID != 0)
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_ADSLRequest.AdditionalServiceID);
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

                    IPStatic = ADSLIPDB.GetADSLIPFree((int)_ADSLRequest.CustomerGroupID).FirstOrDefault();
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
                GroupIPStatic = ADSLIPDB.GetADSLGroupIPFree(Convert.ToInt32(GroupIPTypeComboBox.SelectedValue), (int)_ADSLRequest.CustomerGroupID).FirstOrDefault();

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
                    modem = ADSLModemDB.GetADSLModemById((int)_ADSLRequest.ModemID);
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

                //if (_ADSLRequest.HasIP != null)
                //    if ((bool)_ADSLRequest.HasIP)
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
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList);
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
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList);
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
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList);
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
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList);
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
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID);
            else
                ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID, groupID, bandWidthID, trafficID, durationID, _ServiceAccessList, _ServiceGroupAccessList);
        }

        private void ModemSerilaNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModemSerilaNoComboBox.SelectedValue != null)
            {
                ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ModemSerilaNoComboBox.SelectedValue);
                ModemMACAddressTextBox.Text = modemProperty.MACAddress;
            }
            else
                if (_ADSLRequest.ModemSerialNoID != null)
                {
                    ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_ADSLRequest.ModemSerialNoID);
                    ModemMACAddressTextBox.Text = modemProperty.MACAddress;
                }
        }

        #endregion
    }
}
