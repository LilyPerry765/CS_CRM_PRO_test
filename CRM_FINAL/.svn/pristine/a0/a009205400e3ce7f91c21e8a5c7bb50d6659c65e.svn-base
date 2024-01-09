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
using CRM.Application.UserControls;

namespace CRM.Application.Views
{
    public partial class RequestRefundForm : Local.RequestFormBase
    {
        #region

        private Request _Request { get; set; }
        private UserControls.TelephoneInformation _TelephoneInformation;

        #endregion

        #region Constructors

        public RequestRefundForm()
        {
            InitializeComponent();
            Initialize();
        }
        public RequestRefundForm(long requestID):this()
        {
           
            RequestID = requestID;
            _Request = Data.RequestDB.GetRequestByID(RequestID);


            if (_Request.RequestTypeID != (int)DB.RequestType.Wireless)
            {
                _TelephoneInformation = new TelephoneInformation(_Request.TelephoneNo ?? 0, _Request.RequestTypeID);
            }
            else
            {
                _TelephoneInformation = new TelephoneInformation(_Request.ID);
            }

        }

        private void Initialize()
        {
            //_Request = Data.RequestDB.GetRequestByID(RequestID);

            PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            CostTitleColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();

            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            TelephoneInfo.Content = _TelephoneInformation;
            TelephoneInfo.DataContext = _TelephoneInformation;
            RequestInfo.DataContext = _Request;
            CenterTextBox.Text = CenterDB.GetCenterNamebyCenterID(_Request.CenterID);
            RequestPaymentDataGrid.ItemsSource = RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
            CostSumTextBox.Text = RequestPaymentDB.GetAmountSumforPaidPayment(RequestID, (int)DB.PaymentType.Cash).ToString() + " ریال";
        }

        public override bool Confirm()
        {
            try
            {
                switch (_Request.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:
                        Confirm_ADSLRequest();
                        break;
                    case (byte)DB.RequestType.Wireless:
                        Confirm_WirelessRequest();
                        break;
                    case (byte)DB.RequestType.ADSLChangeService:
                        Confirm_ADSLChangeServiceRequest();
                        break;
                    case (byte)DB.RequestType.ADSLSellTraffic:
                        Confirm_ADSLSellTrafficRequest();
                        break;
                    default:
                        break;
                }

                ADSLSellerAgentUserCredit credit = ADSLSellerGroupDB.GetCreditbyRequestID(RequestID);
                if (credit != null)
                {
                    ADSLSellerAgentUser user = ADSLSellerGroupDB.GetADSLSellerAgentUserByID(credit.UserID);

                    if (user != null)
                    {
                        user.CreditCashRemain = user.CreditCashRemain + credit.Cost;
                        user.CreditCashUse = user.CreditCashUse - credit.Cost;

                        user.Detach();
                        DB.Save(user);

                        ADSLSellerAgent agent = ADSLSellerGroupDB.GetADSLSellerAgentByID(user.SellerAgentID);

                        if (agent != null)
                        {
                            agent.CreditCashRemain = agent.CreditCashRemain + credit.Cost;
                            agent.CreditCashUse = agent.CreditCashUse - credit.Cost;

                            agent.Detach();
                            DB.Save(agent);
                        }
                    }

                    DB.Delete<ADSLSellerAgentUserCredit>(credit.ID);
                }

                List<RequestPayment> paymentsList = RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);

                foreach (RequestPayment currentPayment in paymentsList)
                {
                    if (currentPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                    {
                        List<InstallmentRequestPayment> instalmentsList = InstalmentRequestPaymentDB.GetInstalmentRequestPaymentbyPaymentID(currentPayment.ID);

                        foreach (InstallmentRequestPayment currentinstalment in instalmentsList)
                        {
                            DB.Delete<InstallmentRequestPayment>(currentinstalment.ID);
                        }
                    }

                    DB.Delete<RequestPayment>(currentPayment.ID);
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

        private void Confirm_ADSLRequest()
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

                        _aDSLRequest.HasIP = null;
                        _aDSLRequest.IPStaticID = null;
                        _aDSLRequest.IPDuration = null;

                        _aDSLRequest.Detach();
                        DB.Save(_aDSLRequest);
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

                        _aDSLRequest.HasIP = null;
                        _aDSLRequest.GroupIPStaticID = null;
                        _aDSLRequest.IPDuration = null;

                        _aDSLRequest.Detach();
                        DB.Save(_aDSLRequest);
                    }
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

                    _aDSLRequest.NeedModem = null;
                    _aDSLRequest.ModemID = null;
                    _aDSLRequest.ModemSerialNoID = null;
                    _aDSLRequest.ModemMACAddress = null;

                    _aDSLRequest.Detach();
                    DB.Save(_aDSLRequest);
                }
            }
        }
        private void Confirm_WirelessRequest()
        {
            WirelessRequest wirelessRequest = WirelessRequestDB.GetWirelessRequestByID(RequestID);

            if (wirelessRequest.HasIP != null)
            {
                if ((bool)wirelessRequest.HasIP)
                {
                    if (wirelessRequest.IPStaticID != null)
                    {
                        ADSLIP iP = ADSLIPDB.GetADSLIPById((long)wirelessRequest.IPStaticID);

                        iP.TelephoneNo = null;
                        iP.Status = (byte)DB.ADSLIPStatus.Free;
                        iP.InstallDate = null;
                        iP.ExpDate = null;

                        iP.Detach();
                        Save(iP, false);

                        wirelessRequest.HasIP = null;
                        wirelessRequest.IPStaticID = null;
                        wirelessRequest.IPDuration = null;

                        wirelessRequest.Detach();
                        DB.Save(wirelessRequest);
                    }

                    if (wirelessRequest.GroupIPStaticID != null)
                    {
                        ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)wirelessRequest.GroupIPStaticID);

                        groupIP.TelephoneNo = null;
                        groupIP.Status = (byte)DB.ADSLIPStatus.Free;
                        groupIP.InstallDate = null;
                        groupIP.ExpDate = null;

                        groupIP.Detach();
                        Save(groupIP, false);

                        wirelessRequest.HasIP = null;
                        wirelessRequest.GroupIPStaticID = null;
                        wirelessRequest.IPDuration = null;

                        wirelessRequest.Detach();
                        DB.Save(wirelessRequest);
                    }
                }
            }

            if (wirelessRequest.NeedModem != null)
            {
                if ((bool)wirelessRequest.NeedModem)
                {
                    if (wirelessRequest.ModemSerialNoID != null)
                    {
                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)wirelessRequest.ModemSerialNoID);

                        modem.TelephoneNo = null;
                        modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                        modem.Detach();
                        Save(modem, false);
                    }

                    wirelessRequest.NeedModem = null;
                    wirelessRequest.ModemID = null;
                    wirelessRequest.ModemSerialNoID = null;
                    wirelessRequest.ModemMACAddress = null;

                    wirelessRequest.Detach();
                    DB.Save(wirelessRequest);
                }
            }
        }

        private void Confirm_ADSLChangeServiceRequest()
        {

        }

        private void Confirm_ADSLSellTrafficRequest()
        {
 
        }

        public override bool Deny()
        {
            IsRejectSuccess = true;
            return IsRejectSuccess;
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}
