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
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;

namespace CRM.Application.Views
{
    public partial class ADSLSupportForm : Local.RequestFormBase
    {
        #region Properties

        private Request _Request = new Request();
        private Data.ADSLSupportRequest _SupportRequest = new Data.ADSLSupportRequest();

        private double Charge_Remaind { get; set; }

        #endregion

        #region Constructors

        public ADSLSupportForm(long requestID)
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
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
        }

        private void LoadData()
        {
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            _SupportRequest = ADSLSupportRequestDB.GetADSLSupportRequestByID(RequestID);

            FirstDescriptionTextBox.Text = _SupportRequest.FirstDescription;
            ResultDescriptionTextBox.Text = _SupportRequest.ResultDescription;

            GetADSLInfoByPhoneNumber(_Request.TelephoneNo.ToString());

            ResizeWindow();
        }

        private void GetADSLInfoByPhoneNumber(string telephoneNo)
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

            //CRMWebService.CRMWebService webServive = new CRMWebService.CRMWebService();

            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();
            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", telephoneNo);
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
                try
                {
                    NameTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["name"]);
                }
                catch (Exception ex)
                {
                    NameTextBox.Text = string.Empty;
                }
                try
                {
                    TelephoneNoTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                }
                catch (Exception ex)
                {
                    TelephoneNoTextBox.Text = string.Empty;
                }
                try
                {
                    PasswordTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);
                }
                catch (Exception ex)
                {
                    PasswordTextBox.Text = string.Empty;
                }
                try
                {
                    System.String[][] internetOnlinesList = ((System.String[][])(userInfo["internet_onlines"]));
                    IPTextBox.Text = internetOnlinesList[0][4];
                }
                catch (Exception ex)
                {
                    IPTextBox.Text = string.Empty;
                }
                try
                {
                    UserIDTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                }
                catch (Exception ex)
                {
                    UserIDTextBox.Text = string.Empty;
                }
                try
                {
                    MobileTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["cell_phone"]);
                }
                catch (Exception ex)
                {
                    MobileTextBox.Text = string.Empty;
                }
                try
                {
                    EmailTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["email"]);
                }
                catch (Exception ex)
                {
                    EmailTextBox.Text = string.Empty;
                }
                try
                {
                    PostalCodeTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["postal_code"]);
                }
                catch (Exception ex)
                {
                    PostalCodeTextBox.Text = string.Empty;
                }
                try
                {
                    CRM.Data.Telephone telephone = CRM.Data.TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(telephoneNo));
                    CenterTextBox.Text = CRM.Data.CenterDB.GetCenterById(telephone.CenterID).CenterName;
                }
                catch (Exception ex)
                {
                    CenterTextBox.Text = string.Empty;
                }

                try
                {
                    CRM.Data.ADSL adsl = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
                    CRM.Data.ADSLService service = CRM.Data.ADSLServiceDB.GetADSLServiceById((int)adsl.TariffID);
                    ServiceTitleTextBox.Text = service.Title;

                    BandWithTextBox.Text = Data.ADSLServiceDB.GetADSLServiceBandWidthByID((int)service.BandWidthID).Title;
                    DurationTextBox.Text = Data.ADSLServiceDB.GetADSLServiceDurationByID((int)service.DurationID).Title;
                    TrafficTextBox.Text = Data.ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID).Title;
                }
                catch (Exception ex)
                {
                    ServiceTitleTextBox.Text = string.Empty;
                }

                try
                {
                    AddressTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["address"]);
                }
                catch (Exception ex)
                {
                    AddressTextBox.Text = string.Empty;
                }
                try
                {
                    string realFirstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["real_first_login"]);
                    DateTime realFirstLoginDate = Convert.ToDateTime(realFirstLogin);
                    RealFisrtLoginTextBox.Text = Helper.GetPersianDate(realFirstLoginDate, Helper.DateStringType.DateTime);
                }
                catch (Exception ex)
                {
                    RealFisrtLoginTextBox.Text = string.Empty;
                }
                try
                {
                    string firstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["first_login"]);
                    DateTime firstLoginDate = Convert.ToDateTime(firstLogin);
                    FisrtLoginTextBox.Text = Helper.GetPersianDate(firstLoginDate, Helper.DateStringType.DateTime);
                }
                catch (Exception ex)
                {
                    FisrtLoginTextBox.Text = string.Empty;
                }
                try
                {
                    string nearestExp = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["nearest_exp_date"]);
                    DateTime nearestExpDate = Convert.ToDateTime(nearestExp);
                    int DayRemaid = Math.Abs((DB.GetServerDate() - nearestExpDate).Days);
                    RemainedDayTextBox.Text = DayRemaid.ToString() + " روز ";
                    NearestExpDateTextBox.Text = Helper.GetPersianDate(nearestExpDate, Helper.DateStringType.DateTime);
                }
                catch (Exception ex)
                {
                    NearestExpDateTextBox.Text = string.Empty;
                }

                string RechargeDeposit = "0";
                try
                {
                    RechargeDeposit = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["recharge_deposit"]);
                }
                catch (Exception ex)
                {
                    RechargeDeposit = "0";
                }


                string CreditLabel = "0";
                try
                {
                    CreditLabel = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["credit"]);
                    if (double.Parse(CreditLabel) < 0)
                        CreditLabel = "0";
                }
                catch (Exception ex)
                {
                    CreditLabel = "0";
                }

                Charge_Remaind = Convert.ToDouble(Convert.ToDouble(RechargeDeposit).ToString("0.0##")) + Convert.ToDouble(Convert.ToDouble(CreditLabel).ToString("0.0##"));
                RemainedCreditTextBox.Text = Charge_Remaind.ToString();

                try
                {
                    bool UserLoginSatus = (System.Boolean)(userInfo["online_status"]);
                    if (UserLoginSatus)
                    {
                        UserStatusImage.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Online_User_64x64.png"));
                    }
                    else
                    {
                        UserStatusImage.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Offline_User_64x64.png"));
                    }
                }
                catch (Exception ex)
                {
                    UserStatusImage.Source = null;
                }
                try
                {
                    LimitMacTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["limit_mac"]);
                }
                catch (Exception ex)
                {
                    LimitMacTextBox.Text = string.Empty;
                }

                string CreationDate = "";
                try
                {
                    CreationDate = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["creation_date"]);
                    DateTime CreationDateDate = Convert.ToDateTime(CreationDate);

                    FisrtServiceLoginTextBox.Text = Helper.GetPersianDate(CreationDateDate, Helper.DateStringType.DateTime);
                }
                catch (Exception ex)
                {
                    CreationDate = string.Empty;
                }
            }
            catch (Exception ex)
            {

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

        public override bool Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ResultDescriptionTextBox.Text))
                    throw new Exception("لطفا توضیحات لازم را وارد نمایید");

                _SupportRequest.ResultDescription = ResultDescriptionTextBox.Text.Trim();

                _SupportRequest.Detach();
                Save(_SupportRequest);

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره درخواست، " + ex.Message+" !", ex);
            }

            return IsSaveSuccess;
        }

        public override bool Confirm()
        {
            Save();

            IsConfirmSuccess = true;

            return IsConfirmSuccess;
        }

        #endregion

        #region Event Handlers

        #endregion
    }
}
