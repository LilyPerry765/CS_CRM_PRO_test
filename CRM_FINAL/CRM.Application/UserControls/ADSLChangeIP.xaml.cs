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

namespace CRM.Application.UserControls
{
    public partial class ADSLChangeIP : UserControl
    {
        #region Properties

        private long _RequsetID = 0;

        private CRM.Data.ADSLChangeIPRequest _ADSLIPRequest { get; set; }
        private Request _Request { get; set; }
        private Data.ADSL _ADSL { get; set; }
        private ADSLService _Service { get; set; }
        public ADSLServiceInfo _ServiceInfo { get; set; }

        public ADSLIP IPStatic { get; set; }
        public ADSLGroupIP GroupIPStatic { get; set; }

        public long TelephoneNo { get; set; }
        public long _SumPriceIP = 0;

        public double refundAmount { get; set; }
        int maxDuration = 0;
        #endregion

        #region Constructors

        public ADSLChangeIP()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public ADSLChangeIP(long requestID, long customerID, long telephoneNo)
            : this()
        {
            _RequsetID = requestID;
            TelephoneNo = telephoneNo;

            LoadData(null, null);
        }

        #endregion 

        #region Methods

        private void Initialize()
        {
            IPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPType));
            GroupIPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLGroupIPBlockCount));
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            try
            {
                _ADSL = ADSLDB.GetADSLByTelephoneNo(TelephoneNo);
                _Service = ADSLServiceDB.GetADSLServiceById((int)_ADSL.TariffID);
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);

                if (_ADSL.ExpDate == null)
                {
                    CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
                    CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
                    CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();

                    ibsngInputInfo.NormalUsername = TelephoneNo.ToString();
                    ibsngUserInfo = webService.GetUserInfo(ibsngInputInfo);

                    if (ibsngUserInfo != null)
                    {
                        DateTime expDate = Convert.ToDateTime(ibsngUserInfo.NearestExpDate);
                        if (expDate != null)
                        {
                            maxDuration = Convert.ToInt32(expDate.Date.Subtract(DB.GetServerDate()).TotalDays);
                            maxDuration = (maxDuration / 30) + 1;

                            _ADSL.ExpDate = expDate;

                            _ADSL.Detach();
                            DB.Save(_ADSL);
                        }
                    }
                }
                else
                {
                    maxDuration = Convert.ToInt32(_ADSL.ExpDate.Value.Date.Subtract(DB.GetServerDate()).TotalDays);
                    if (maxDuration < 360)
                        maxDuration = (maxDuration / 30) + 1;
                    else
                        maxDuration = (maxDuration / 30);
                }

                if (_ADSL.HasIP == null)
                    ADSLOldIPGroupBox.Visibility = Visibility.Collapsed;
                else
                {
                    if (_ADSL.IPStaticID != null)
                    {
                        IPStatic = ADSLIPDB.GetADSLIPById((long)_ADSL.IPStaticID);

                        OldIPTypeTextBox.Text = "تکی";
                        OLdSingleIPTextBox.Text = IPStatic.IP;
                        OldIPTimeTextBox.Text = _ServiceInfo.Duration;

                        OldSingleIPLabel.Visibility = Visibility.Visible;
                        OLdSingleIPTextBox.Visibility = Visibility.Visible;
                    }

                    if (_ADSL.GroupIPStaticID != null)
                    {
                        GroupIPStatic = ADSLIPDB.GetADSLGroupIPById((long)_ADSL.GroupIPStaticID);

                        OldIPTypeTextBox.Text = "گروهی";
                        OldGroupIPTextBox.Text = GroupIPStatic.StartRange;
                        OldGroupIPTypeTextBox.Text = GroupIPStatic.BlockCount + " تایی";
                        OldIPTimeTextBox.Text = _ServiceInfo.Duration;

                        OldGroupIPLabel.Visibility = Visibility.Visible;
                        OldGroupIPTextBox.Visibility = Visibility.Visible;
                        OldGroupIPTypeLabel.Visibility = Visibility.Visible;
                        OldGroupIPTypeTextBox.Visibility = Visibility.Visible;
                    }
                }

                if (_RequsetID == 0)
                {
                    if (_ADSL.HasIP == null)
                        ADSLOldIPGroupBox.Visibility = Visibility.Collapsed;
                    else
                    {
                        if (_ADSL.IPStaticID != null)
                        {
                            IPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPType));
                            IPTypeComboBox.SelectedValue = (byte)Convert.ToInt16(DB.ADSLIPType.Single);

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

                            IPStatic = ADSLIPDB.GetADSLIPById((long)_ADSL.IPStaticID);

                            SingleIPTextBox.Text = IPStatic.IP;
                            BaseCost cost = BaseCostDB.GetIPCostForADSL();

                            if (_ServiceInfo.DurationID != 0 && _ServiceInfo.DurationID != -1)
                            {
                                IPCostTextBox.Text = (cost.Cost * maxDuration).ToString() + " ریا ل";

                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * maxDuration) + (cost.Cost * maxDuration * (int)cost.Tax * 0.01)).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }

                                IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(maxDuration);
                                IPTimeComboBox.SelectedValue = maxDuration;
                            }
                            else
                            {
                                IPCostTextBox.Text = (cost.Cost * 12).ToString() + " ریا ل";

                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * 12) + (cost.Cost * 12 * (int)cost.Tax * 0.01)).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }

                                IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(12);
                                IPTimeComboBox.SelectedValue = 12;
                            }
                        }

                        if (_ADSL.GroupIPStaticID != null)
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

                            GroupIPStatic = ADSLIPDB.GetADSLGroupIPById((long)_ADSL.GroupIPStaticID);
                            BaseCost cost = BaseCostDB.GetIPCostForADSL();

                            IPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPType)).Where(t => t.ID == 2);
                            IPTypeComboBox.SelectedValue = (byte)Convert.ToInt16(DB.ADSLIPType.Group);
                            GroupIPTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLGroupIPBlockCount)).Where(t => t.ID >= GroupIPStatic.BlockCount);

                            GroupIPTypeComboBox.SelectedValue = GroupIPStatic.BlockCount;

                            GroupIPTextBox.Text = GroupIPStatic.StartRange;
                            if (_ServiceInfo.DurationID != 0 && _ServiceInfo.DurationID != null)
                            {
                                IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(maxDuration);
                                IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * maxDuration).ToString() + " ریا ل";

                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * GroupIPStatic.BlockCount * maxDuration) + (cost.Cost * GroupIPStatic.BlockCount * maxDuration * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }

                                IPTimeComboBox.SelectedValue = maxDuration;
                            }
                            else
                            {
                                IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(12);
                                IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * 12).ToString() + " ریا ل";

                                if (cost.Tax != null && cost.Tax != 0)
                                {
                                    IPTaxTextBox.Text = cost.Tax + " درصد";
                                    IPSumCostTextBox.Text = ((cost.Cost * GroupIPStatic.BlockCount * 12) + (cost.Cost * GroupIPStatic.BlockCount * 12 * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                }
                                else
                                {
                                    IPTaxTextBox.Text = "0 درصد";
                                    IPSumCostTextBox.Text = IPCostTextBox.Text;
                                }

                                IPTimeComboBox.SelectedValue = 12;
                            }
                        }
                    }
                }
                else
                {
                    _Request = Data.RequestDB.GetRequestByID(_RequsetID);
                    _ADSLIPRequest = ADSLChangeIPRequestDB.GetADSLChangeIPRequestByID(_RequsetID);

                    if (_ADSLIPRequest.NewIPStaticID != null)
                    {
                        IPStatic = ADSLIPDB.GetADSLIPById((long)_ADSLIPRequest.NewIPStaticID);

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

                        if (_ServiceInfo.DurationID != null && _ServiceInfo.DurationID != 0)
                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(maxDuration);
                        else
                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(12);

                        IPTimeComboBox.SelectedValue = _ADSLIPRequest.IPTime;

                        if (IPTimeComboBox.SelectedValue != null)
                        {
                            IPCostTextBox.Text = (cost.Cost * (int)IPTimeComboBox.SelectedValue).ToString() + " ریا ل";
                            if (cost.Tax != null && cost.Tax != 0)
                            {
                                IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                IPSumCostTextBox.Text = ((cost.Cost * (int)IPTimeComboBox.SelectedValue) + (cost.Cost * (int)IPTimeComboBox.SelectedValue * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                            }
                            else
                            {
                                IPTaxTextBox.Text = "0 درصد";
                                IPSumCostTextBox.Text = IPCostTextBox.Text;
                            }

                            //IPTimeComboBox.SelectedValue = _ServiceInfo.DurationID;
                        }
                    }
                    if (_ADSLIPRequest.NewGroupIPStaticID != null)
                    {
                        GroupIPStatic = ADSLIPDB.GetADSLGroupIPById((long)_ADSLIPRequest.NewGroupIPStaticID);

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

                        if (_ServiceInfo.DurationID != null && _ServiceInfo.DurationID != 0)
                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(maxDuration);
                        else
                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(12);

                        IPTimeComboBox.SelectedValue = _ADSLIPRequest.IPTime;

                        if (IPTimeComboBox.SelectedValue != null)
                        {
                            IPCostTextBox.Text = (cost.Cost * (int)IPTimeComboBox.SelectedValue * GroupIPStatic.BlockCount).ToString() + " ریا ل";
                            if (cost.Tax != null && cost.Tax != 0)
                            {
                                IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                IPSumCostTextBox.Text = ((cost.Cost * (int)IPTimeComboBox.SelectedValue * GroupIPStatic.BlockCount) + (cost.Cost * (int)IPTimeComboBox.SelectedValue * GroupIPStatic.BlockCount * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                            }
                            else
                            {
                                IPTaxTextBox.Text = "0 درصد";
                                IPSumCostTextBox.Text = IPCostTextBox.Text;
                            }

                            //IPTimeComboBox.SelectedValue = _ServiceInfo.DurationID;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void IPTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IPTypeComboBox.SelectedValue != null)
            {
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

                    IPStatic = ADSLIPDB.GetADSLIPFree((int)_ServiceInfo.CustomerGroupID).FirstOrDefault();

                    if (IPStatic == null)
                    {
                        ErrorLabel.Visibility = Visibility.Visible;
                        ErrorLabel.Content = "IP آزاد موجود نمی باشد !";
                    }
                    else
                    {
                        SingleIPTextBox.Text = IPStatic.IP;
                        BaseCost cost = BaseCostDB.GetIPCostForADSL();

                        if (maxDuration != 0)
                        {
                            IPCostTextBox.Text = (cost.Cost * maxDuration).ToString() + " ریا ل";

                            if (cost.Tax != null && cost.Tax != 0)
                            {
                                IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                IPSumCostTextBox.Text = ((cost.Cost * maxDuration) + (cost.Cost * maxDuration * (int)cost.Tax * 0.01)).ToString() + " ریا ل";
                            }
                            else
                            {
                                IPTaxTextBox.Text = "0 درصد";
                                IPSumCostTextBox.Text = IPCostTextBox.Text;
                            }
                        }

                        IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(maxDuration);
                        IPTimeComboBox.SelectedValue = maxDuration;

                        _SumPriceIP = cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue);

                        ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                        if (user != null)
                        {
                            //long creditRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                            //if (creditRemain <= (_SumPriceService + _SumPriceIP))
                            //{
                            //    Folder.MessageBox.ShowError("اعتبار نمایندگی شما برای فروش کافی نمی باشد!");
                            //    return;
                            //}
                        }
                    }
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

                    GroupIPTypeComboBox.SelectedValue = null;
                    GroupIPTextBox.Text = string.Empty;
                    IPCostTextBox.Text = string.Empty;
                    IPTimeComboBox.SelectedValue = maxDuration;
                    IPTaxTextBox.Text = string.Empty;
                    IPSumCostTextBox.Text = string.Empty;
                }
            }

            if (_ADSL.HasIP == true)
            {
                IPUsingCostLabel.Visibility = Visibility.Visible;
                IPUsingCostTextBox.Visibility = Visibility.Visible;
                IPRefundCostLabel.Visibility = Visibility.Visible;
                IPRefundCostTextBox.Visibility = Visibility.Visible;

                BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                int serviceduration = _ServiceInfo.DurationID;

                long oldCost = 0;
                double dayCount = 0;
                double dayCost = 0;
                double useDayCount = 0;
                DateTime? now = DB.GetServerDate();
                refundAmount = 0;

                if (_ADSL.IPStaticID != null)
                {
                    ADSLIP oldIP = ADSLIPDB.GetADSLIPById((long)_ADSL.IPStaticID);
                    dayCount = oldIP.ExpDate.Value.Date.Subtract((DateTime)oldIP.InstallDate).TotalDays;
                    serviceduration = (int)(dayCount / 30);
                    oldCost = Convert.ToInt64(Convert.ToDouble(baseCost.Cost * serviceduration) + Convert.ToDouble((baseCost.Tax * 0.01) * baseCost.Cost * serviceduration));                    
                    dayCost = oldCost / dayCount;
                    if (_ADSL.InstallDate.Value.Date.Subtract((DateTime)oldIP.InstallDate).TotalDays < 0)
                    {
                        useDayCount = now.Value.Date.Subtract((DateTime)oldIP.InstallDate).TotalDays;
                        refundAmount = oldCost - (useDayCount * dayCost);
                    }
                    else
                    {
                        refundAmount = 0;
                        useDayCount = now.Value.Date.Subtract((DateTime)oldIP.InstallDate).TotalDays;
                    }
                    if (oldIP.ExpDate.Value.Date.Subtract(DB.GetServerDate()).TotalDays > 0)
                    {
                        refundAmount = oldCost - (useDayCount * dayCost);
                        IPUsingCostTextBox.Text = (useDayCount * dayCost).ToString().Split('.')[0] + " ریا ل";
                        IPRefundCostTextBox.Text = refundAmount.ToString().Split('.')[0] + " ریا ل";
                    }
                    else
                    {
                        refundAmount = 0;
                        IPUsingCostTextBox.Text = "";
                        IPRefundCostTextBox.Text = "";
                    }
                }
                if (_ADSL.GroupIPStaticID != null)
                {
                    ADSLGroupIP oldGroupIP = ADSLIPDB.GetADSLGroupIPById((long)_ADSL.GroupIPStaticID);
                    dayCount = oldGroupIP.ExpDate.Value.Date.Subtract((DateTime)oldGroupIP.InstallDate).TotalDays;
                    serviceduration =(int)(dayCount / 30);
                    oldCost = Convert.ToInt64(Convert.ToDouble(oldGroupIP.BlockCount * baseCost.Cost * serviceduration) + Convert.ToDouble((baseCost.Tax * 0.01) * oldGroupIP.BlockCount * baseCost.Cost * serviceduration));                    
                    dayCost = oldCost / dayCount;
                    useDayCount = now.Value.Date.Subtract((DateTime)oldGroupIP.InstallDate).TotalDays;

                    if (oldGroupIP.ExpDate.Value.Date.Subtract(DB.GetServerDate()).TotalDays > 0)
                    {
                        refundAmount = oldCost - (useDayCount * dayCost);
                        IPUsingCostTextBox.Text = (useDayCount * dayCost).ToString().Split('.')[0] + " ریا ل";
                        IPRefundCostTextBox.Text = refundAmount.ToString().Split('.')[0] + " ریا ل";
                    }
                    else
                    {
                        refundAmount = 0;
                        IPUsingCostTextBox.Text = "";
                        IPRefundCostTextBox.Text = "";
                    }
                }
            }
        }

        private void GroupIPTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (GroupIPTypeComboBox.SelectedValue != null)
                {
                    GroupIPStatic = ADSLIPDB.GetADSLGroupIPFree(Convert.ToInt32(GroupIPTypeComboBox.SelectedValue), (int)_ServiceInfo.CustomerGroupID).FirstOrDefault();

                    if (GroupIPStatic != null)
                    {
                        GroupIPLabel.Visibility = Visibility.Visible;
                        GroupIPTextBox.Visibility = Visibility.Visible;
                        ErrorGroupIPLabel.Visibility = Visibility.Collapsed;

                        BaseCost cost = BaseCostDB.GetIPCostForADSL();

                        _SumPriceIP = cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue);

                        ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                        if (user != null)
                        {
                            //long creditRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                            //if (creditRemain <= (_SumPriceService + _SumPriceIP))
                            //{
                            //    Folder.MessageBox.ShowError("اعتبار نمایندگی شما برای فروش کافی نمی باشد!");

                            //    GroupIPLabel.Visibility = Visibility.Collapsed;
                            //    GroupIPTextBox.Visibility = Visibility.Collapsed;
                            //    IPCostTextBox.Text = string.Empty;
                            //    IPTimeComboBox.Text = string.Empty;
                            //    IPTaxTextBox.Text = string.Empty;
                            //    IPSumCostTextBox.Text = string.Empty;
                            //    return;
                            //}
                        }

                        GroupIPTextBox.Text = GroupIPStatic.StartRange;
                        if (maxDuration != 0)
                        {
                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(maxDuration);
                            IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * maxDuration).ToString() + " ریا ل";

                            if (cost.Tax != null && cost.Tax != 0)
                            {
                                IPTaxTextBox.Text = cost.Tax + " درصد";
                                IPSumCostTextBox.Text = ((cost.Cost * GroupIPStatic.BlockCount * maxDuration) + (cost.Cost * GroupIPStatic.BlockCount * maxDuration * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                            }
                            else
                            {
                                IPTaxTextBox.Text = "0 درصد";
                                IPSumCostTextBox.Text = IPCostTextBox.Text;
                            }

                            IPTimeComboBox.SelectedValue = maxDuration;
                        }
                        else
                        {
                            IPTimeComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationForIpTime(12);
                            IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * 12).ToString() + " ریا ل";

                            if (cost.Tax != null && cost.Tax != 0)
                            {
                                IPTaxTextBox.Text = cost.Tax + " درصد";
                                IPSumCostTextBox.Text = ((cost.Cost * GroupIPStatic.BlockCount * 12) + (cost.Cost * GroupIPStatic.BlockCount * 12 * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                            }
                            else
                            {
                                IPTaxTextBox.Text = "0 درصد";
                                IPSumCostTextBox.Text = IPCostTextBox.Text;
                            }

                            IPTimeComboBox.SelectedValue = 12;
                        }
                    }
                    else
                    {
                        GroupIPLabel.Visibility = Visibility.Collapsed;
                        GroupIPTextBox.Visibility = Visibility.Collapsed;
                        ErrorGroupIPLabel.Visibility = Visibility.Visible;
                        ErrorGroupIPLabel.Content = "بلاک IP مورد نظر موجود نمی باشد";
                        IPCostTextBox.Text = string.Empty;
                        IPTimeComboBox.Text = string.Empty;
                        IPTaxTextBox.Text = string.Empty;
                        IPSumCostTextBox.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void IPTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IPTimeComboBox.SelectedValue != null)
            {
                BaseCost cost = BaseCostDB.GetIPCostForADSL();

                if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Single)
                {
                    long sumCost = Convert.ToInt64(Convert.ToDouble(cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue)) + Convert.ToDouble((cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue)) * ((int)cost.Tax * 0.01)));
                    if (sumCost < refundAmount)
                    {
                        ErrorLabel.Visibility = Visibility.Visible;
                        ErrorLabel.Content = "هزینه IP انتخابی کوچکتر از هزینه مصرف شده می باشد!";

                        IPTimeComboBox.SelectedValue = null;
                        IPCostTextBox.Text = string.Empty;
                        IPSumCostTextBox.Text = string.Empty;
                    }
                    else
                    {
                        ErrorLabel.Visibility = Visibility.Collapsed;

                        IPCostTextBox.Text = (cost.Cost * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";
                        IPSumCostTextBox.Text = sumCost.ToString() + " ریا ل";
                    }
                }
                if (Convert.ToInt16(IPTypeComboBox.SelectedValue) == (byte)DB.ADSLIPType.Group)
                {
                    long sumCost = Convert.ToInt64(Convert.ToDouble(cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue)) + Convert.ToDouble(cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue) * ((int)cost.Tax * 0.01)));

                    if (sumCost < refundAmount)
                    {
                        ErrorLabel.Visibility = Visibility.Visible;
                        ErrorLabel.Content = "هزینه IP انتخابی کوچکتر از هزینه مصرف شده می باشد!";

                        IPTimeComboBox.SelectedValue = null;
                        IPCostTextBox.Text = string.Empty;
                        IPSumCostTextBox.Text = string.Empty;
                    }
                    else
                    {
                        ErrorLabel.Visibility = Visibility.Collapsed;

                        IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";
                        if (cost.Tax != null)
                            IPSumCostTextBox.Text = sumCost.ToString() + " ریا ل";
                        else
                            IPSumCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeComboBox.SelectedValue)).ToString() + " ریا ل";
                    }
                }
            }
        }

        //private void ChangeIPTypeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        switch (ChangeIPTypeListBox.SelectedValue.ToString())
        //        {
        //            case "2":
        //                GroupIPTypeLabel.Visibility = Visibility.Visible;
        //                IPTypeComboBox.Visibility = Visibility.Visible;

        //                break;

        //            case "3":
        //                GroupIPTypeLabel.Visibility = Visibility.Collapsed;
        //                IPTypeComboBox.Visibility = Visibility.Collapsed;
        //                SingleIPLabel.Visibility = Visibility.Collapsed;
        //                SingleIPTextBox.Visibility = Visibility.Collapsed;
        //                GroupIPTypeLabel.Visibility = Visibility.Collapsed;
        //                GroupIPTypeComboBox.Visibility = Visibility.Collapsed;
        //                GroupIPLabel.Visibility = Visibility.Collapsed;
        //                GroupIPTextBox.Visibility = Visibility.Collapsed;
        //                IPCostLabel.Visibility = Visibility.Collapsed;
        //                IPCostTextBox.Visibility = Visibility.Collapsed;
        //                IPTimeLabel.Visibility = Visibility.Collapsed;
        //                IPTimeComboBox.Visibility = Visibility.Collapsed;
        //                IPTaxLabel.Visibility = Visibility.Collapsed;
        //                IPTaxTextBox.Visibility = Visibility.Collapsed;
        //                IPSumCostLabel.Visibility = Visibility.Collapsed;
        //                IPSumCostTextBox.Visibility = Visibility.Collapsed;
        //                break;

        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        #endregion
    }
}
