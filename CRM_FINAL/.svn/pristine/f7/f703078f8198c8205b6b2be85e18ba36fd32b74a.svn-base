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
    public partial class ADSLSellTrafficUserControl : UserControl
    {
        #region Properties

        private long _ReqID = 0;
        private long _TelephoneNo { get; set; }
        private int _RequestTypeID = 0;
        public long _SumPriceService = 0;
        private CRM.Data.ADSLSellTraffic _ADSLSellTraffic { get; set; }
        private CRM.Data.WirelessSellTraffic _WirelessSellTraffic { get; set; }

        public bool _HasCreditAgent = true;
        public bool _HasCreditUser = true;

        #endregion

        #region Constructor

        public ADSLSellTrafficUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellTrafficUserControl(long requestID, long telephoneNo, int requestTypeID)
            : this()
        {
            _ReqID = requestID;
            _TelephoneNo = telephoneNo;
            _RequestTypeID = requestTypeID;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            AdditionalServiceComboBox.ItemsSource = ADSLServiceDB.GetAllowedADSLAdditionalService();
        }

        #endregion

        #region Event Handler

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_ReqID != 0)
            {
                ADSLServiceInfo additionalServiceInfo = new ADSLServiceInfo();

                if (_RequestTypeID == (byte)DB.RequestType.ADSLSellTraffic)
                {
                    _ADSLSellTraffic = ADSLSellTrafficDB.GetADSLSellTrafficById(_ReqID);
                    AdditionalServiceComboBox.SelectedValue = _ADSLSellTraffic.AdditionalServiceID;
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_ADSLSellTraffic.AdditionalServiceID);
                }

                if (_RequestTypeID == (byte)DB.RequestType.WirelessSellTraffic)
                {
                    _WirelessSellTraffic = ADSLSellTrafficDB.GetWirelessSellTrafficById(_ReqID);
                    AdditionalServiceComboBox.SelectedValue = _WirelessSellTraffic.AdditionalServiceID;
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_WirelessSellTraffic.AdditionalServiceID);
                }

                ServiceInfo.DataContext = additionalServiceInfo;
            }
        }

        private void AdditionalServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ADSLServiceInfo additionalServiceInfo = new ADSLServiceInfo();
            if (_RequestTypeID == (byte)DB.RequestType.ADSLSellTraffic)
            {
                _ADSLSellTraffic = ADSLSellTrafficDB.GetADSLSellTrafficById(_ReqID);

                if (AdditionalServiceComboBox.SelectedValue != null)
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)AdditionalServiceComboBox.SelectedValue);
                else
                {
                    if (_ADSLSellTraffic.AdditionalServiceID != 0)
                        additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_ADSLSellTraffic.AdditionalServiceID);
                }
            }
            if (_RequestTypeID == (byte)DB.RequestType.WirelessSellTraffic)
            {
                _WirelessSellTraffic = ADSLSellTrafficDB.GetWirelessSellTrafficById(_ReqID);

                if (AdditionalServiceComboBox.SelectedValue != null)
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)AdditionalServiceComboBox.SelectedValue);
                else
                {
                    if (_WirelessSellTraffic.AdditionalServiceID != 0)
                        additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_WirelessSellTraffic.AdditionalServiceID);
                }
            }


            if (additionalServiceInfo != null)
            {
                _SumPriceService = Convert.ToInt64(additionalServiceInfo.PriceSum.Split(' ')[0]);

                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                if (user != null)
                {
                    long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditAgentRemain <= _SumPriceService)
                    {
                        ServiceInfo.DataContext = null;
                        _HasCreditAgent = false;

                        ErrorCreditLabel.Content = "اعتبار نمایندگی شما تمام شده است!";

                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= _SumPriceService)
                    {
                        ServiceInfo.DataContext = null;
                        _HasCreditUser = false;

                        ErrorCreditLabel.Content = "اعتبار کاربری شما تمام شده است!";

                        return;
                    }
                }

                ErrorCreditLabel.Content = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;

                ServiceInfo.DataContext = additionalServiceInfo;

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

        #endregion
    }
}

