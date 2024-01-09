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
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;
using System.Data;
using CRM.Data;
using System.Security.Cryptography;

namespace CRM.Application.Views
{
    public partial class ADSLAAAInfo : Local.PopupWindow
    {
        #region Properties

        private string telephoneNo { get; set; }
        private double Charge_Remaind { get; set; }

        #endregion

        #region Constructors

        public ADSLAAAInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            MainTelephoneNoTextBox.Focus();
            ResizeWindow();
        }

        private void GetADSLInfoByPhoneNumber(string telephoneNo)
        {
            CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
            CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
            CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();

            ibsngInputInfo.NormalUsername = telephoneNo.ToUpper();
            ibsngUserInfo = webService.GetUserInfo(ibsngInputInfo);

            ADSL adsl = null;

            if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
            {
                adsl = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));

                if (ibsngUserInfo == null)
                {
                    ADSLChangePlace changePlace = ADSLChangePlaceDB.GetADSLChangePlaceByOldTelephoneNoAAA(Convert.ToInt64(telephoneNo));

                    if (changePlace != null)
                        throw new Exception("این شماره تعویض شماره شده است، شماره جدید : " + changePlace.NewTelephoneNo.ToString() + " !");
                }

                ADSLChangePlace changePlaceNew = ADSLChangePlaceDB.GetADSLChangePlaceByNewTelephoneNo(Convert.ToInt64(telephoneNo));

                if (changePlaceNew != null)
                    ShowSuccessMessage(" شماره قدیم پیش از تعویض شماره : " + changePlaceNew.OldTelephoneNo.ToString() + " !");

                CRM.Data.Telephone telephone = CRM.Data.TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(telephoneNo));
                CenterTextBox.Text = CRM.Data.CenterDB.GetCenterById(telephone.CenterID).CenterName;
            }
            else
                adsl = ADSLDB.GetWirelessbyCode(telephoneNo);

            try
            {
                if (adsl != null)
                {
                    ADSLService service = CRM.Data.ADSLServiceDB.GetADSLServiceById((int)adsl.TariffID);
                    if (adsl.TariffID != null)
                    {
                        ServiceTitleTextBox.Text = service.Title;
                        BandWithTextBox.Text = Data.ADSLServiceDB.GetADSLServiceBandWidthByID((int)service.BandWidthID).Title;
                        DurationTextBox.Text = Data.ADSLServiceDB.GetADSLServiceDurationByID((int)service.DurationID).Title;
                        TrafficTextBox.Text = Data.ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID).Title;
                    }
                    else
                    {
                        ServiceTitleTextBox.Text = string.Empty;
                        BandWithTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        TrafficTextBox.Text = string.Empty;
                    }

                    if (adsl.CustomerOwnerStatus != null)
                        StatusCustomerTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), Convert.ToInt32(adsl.CustomerOwnerStatus));
                }
            }
            catch (Exception ex)
            {
                ServiceTitleTextBox.Text = string.Empty;
            }

            NameTextBox.Text = ibsngUserInfo.Name;
            TelephoneNoTextBox.Text = ibsngUserInfo.NormalUsername;
            PasswordTextBox.Text = ChangePasswordTextBox.Text = ibsngUserInfo.NormalPassword;

            if (ibsngUserInfo.InternetOnlines != null)
            {
                System.Object[][] internetOnlinesList = ibsngUserInfo.InternetOnlines;
                if (internetOnlinesList[0][4] != null)
                    IPTextBox.Text = internetOnlinesList[0][4].ToString();
            }

            UserIDTextBox.Text = ibsngUserInfo.UserID;
            MobileTextBox.Text = ibsngUserInfo.CellPhone;
            EmailTextBox.Text = ibsngUserInfo.Email;
            PostalCodeTextBox.Text = ibsngUserInfo.PostalCode;
            AddressTextBox.Text = ibsngUserInfo.Address;

            string realFirstLogin = ibsngUserInfo.RealFirstLogin;
            if (!string.IsNullOrWhiteSpace(realFirstLogin))
            {
                DateTime realFirstLoginDate = Convert.ToDateTime(realFirstLogin);
                RealFisrtLoginTextBox.Text = Helper.GetPersianDate(realFirstLoginDate, Helper.DateStringType.DateTime);
            }

            string firstLogin = ibsngUserInfo.FirstLogin;
            if (!string.IsNullOrWhiteSpace(firstLogin))
            {
                DateTime firstLoginDate = Convert.ToDateTime(firstLogin);
                FisrtLoginTextBox.Text = Helper.GetPersianDate(firstLoginDate, Helper.DateStringType.DateTime);
            }

            string nearestExp = ibsngUserInfo.NearestExpDate;
            if (!string.IsNullOrWhiteSpace(nearestExp))
            {
                DateTime nearestExpDate = Convert.ToDateTime(nearestExp);
                if ((Convert.ToInt32((nearestExpDate - DB.GetServerDate()).TotalDays)) > 0)
                {
                    RemainedDayTextBox.Text = (Convert.ToInt32((nearestExpDate - DB.GetServerDate()).TotalDays)) + "روز";
                    ExpStatusImage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    RemainedDayTextBox.Text = " صفر " + "روز";
                    ExpStatusImage.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Exp_Uers_64x64.png"));
                }

                NearestExpDateTextBox.Text = Helper.GetPersianDate(nearestExpDate, Helper.DateStringType.DateTime);
            }

            string remainedCredit = ibsngUserInfo.Credit;
            if (!string.IsNullOrWhiteSpace(remainedCredit))
            {
                if (remainedCredit.Length > 10)
                    RemainedCreditTextBox.Text = ibsngUserInfo.Credit.Substring(0, 10);
                else
                    RemainedCreditTextBox.Text = remainedCredit;
            }
            else
                RemainedCreditTextBox.Text = string.Empty;

            bool UserLoginSatus = ibsngUserInfo.OnlineStatus;
            if (UserLoginSatus)
            {
                UserStatusImage.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Online_User_64x64.png"));
                UserStatusImage1.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Online_User_32x32.png"));
            }
            else
            {
                UserStatusImage.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Offline_User_64x64.png"));
                UserStatusImage1.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Offline_User_32x32.png"));
            }

            LimitMacTextBox.Text = MacAddressTextBox.Text = ibsngUserInfo.LimitMac;
            NextGroupTextBox.Text = ibsngUserInfo.RenewNextGroup;
            rechargeDepositTextBox.Text = ibsngUserInfo.RechargeDeposit;
            LockCommentTextBox.Text = ibsngUserInfo.Lock;
            if (!string.IsNullOrWhiteSpace(ibsngUserInfo.Lock))
                LockStatusImage.Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Lock_User_64x64.png"));
            else
                LockStatusImage.Source = null;

            MultiLoginTextBox.Text = ibsngUserInfo.MultiLogin;

            string CreationDate = "";

            CreationDate = ibsngUserInfo.CreationDate;
            DateTime CreationDateDate = Convert.ToDateTime(CreationDate);
            FisrtServiceLoginTextBox.Text = Helper.GetPersianDate(CreationDateDate, Helper.DateStringType.DateTime);
        }

        private double GetUserLastSalesDeposit(string IBS_User_ID, double ChargeRemaid)//DateTime startDate, DateTime endDate)
        {
            int SumDeposit = 0;

            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();
                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");
                conds.Add("user_ids", IBS_User_ID);
                arguments.Add("conds", conds);

                arguments.Add("from", 0);
                arguments.Add("to", 50);
                arguments.Add("sort_by", "change_time");
                arguments.Add("desc", true);

                XmlRpcStruct result = ibsngService.GetUserDepositChanges(arguments);

                double sum_deposit_change = 0, deposit_change = 0, count = 1, action, Last_deposit_change = 0;

                object[] report = (object[])result["report"];

                if (report.Count() != 0)
                {
                    foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                    {
                        deposit_change = Convert.ToDouble(item["deposit_change"]);
                        action = Convert.ToDouble(item["action"]);

                        if (deposit_change < 0 && Last_deposit_change > 0 && count != 1)
                        {
                            if (sum_deposit_change > ChargeRemaid)
                                return sum_deposit_change;
                        }
                        else
                        {
                            if (deposit_change > 0 && action == 2)
                            {
                                sum_deposit_change += deposit_change;
                                Last_deposit_change = deposit_change;
                            }
                        }
                        count++;
                    }
                }

                return sum_deposit_change; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ADSLConnectionLogsInfo> GetUserConnectionLogs(string IBS_User_ID, string PConnectionType, string fromDate, string toDate)
        {
            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();

                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");
                //if (PConnectionType != "All")
                conds.Add("successful", "yes");
                // conds.Add("service", "Internet");
                //conds.Add("username", PUserName);
                //conds.Add("username", PUserName);
                conds.Add("user_ids", IBS_User_ID);

                conds.Add("login_time_from", fromDate);
                conds.Add("login_time_from_unit", "jalali");
                conds.Add("login_time_to", toDate);
                conds.Add("login_time_to_unit", "jalali");
                conds.Add("show_total_credit_used", 1);
                conds.Add("show_total_duration", 1);

                arguments.Add("conds", conds);
                arguments.Add("from", 0);
                arguments.Add("to", 1000);
                arguments.Add("sort_by", "login_time");
                arguments.Add("desc", true);

                XmlRpcStruct result = ibsngService.GetConnections(arguments);

                ADSLConnectionLogsInfo connectionLog = new ADSLConnectionLogsInfo();
                List<ADSLConnectionLogsInfo> connectionLogList = new List<ADSLConnectionLogsInfo>();

                object[] report = (object[])result["report"];

                if (report.Count() != 0)
                {
                    foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                    {
                        connectionLog = new ADSLConnectionLogsInfo();

                        DateTime startDate = Convert.ToDateTime(item["login_time_formatted"].ToString());
                        connectionLog.StartDate = Helper.GetPersianDate(startDate, Helper.DateStringType.DateTime);

                        DateTime endDate = Convert.ToDateTime(item["logout_time_formatted"].ToString());
                        connectionLog.EndDate = Helper.GetPersianDate(endDate, Helper.DateStringType.DateTime);

                        double cu = 0;
                        if (double.TryParse(item["credit_used"].ToString(), out cu))
                        {
                            connectionLog.CreditUsed = (item["credit_used"]).ToString();
                        }
                        else
                            connectionLog.CreditUsed = "0";

                        connectionLog.IPAddress = (item["remote_ip"]).ToString();
                        connectionLog.Duration = (item["duration_seconds"]).ToString();
                        connectionLog.ByteIN = (item["bytes_in"]).ToString();
                        connectionLog.ByteOut = (item["bytes_out"]).ToString();
                        connectionLog.Details = (System.String[][])item["details"];

                        connectionLogList.Add(connectionLog);
                    }
                }

                return connectionLogList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ADSLDepositChangesInfo> GetUserDepositChanges(string IBS_User_ID, string PConnectionType, string startDate, string endDate)
        {
            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();

                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");

                conds.Add("change_time_from", startDate);
                conds.Add("change_time_from_unit", "jalali");
                conds.Add("change_time_to", endDate);
                conds.Add("change_time_to_unit", "jalali");

                conds.Add("user_ids", IBS_User_ID);
                arguments.Add("conds", conds);

                arguments.Add("from", 0);
                arguments.Add("to", 1000);
                arguments.Add("sort_by", "change_time");
                arguments.Add("desc", true);

                XmlRpcStruct result = ibsngService.GetUserDepositChanges(arguments);

                //double sum_deposit_change = 0, deposit_change = 0, count = 1, action, Last_deposit_change = 0;

                ADSLDepositChangesInfo depositInfo = new ADSLDepositChangesInfo();
                List<ADSLDepositChangesInfo> depositInfoList = new List<ADSLDepositChangesInfo>();

                object[] report = (object[])result["report"];

                if (report.Count() != 0)
                {
                    foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                    {
                        depositInfo = new ADSLDepositChangesInfo();

                        DateTime date = Convert.ToDateTime(item["change_time_formatted"].ToString());
                        depositInfo.Date = Helper.GetPersianDate(date, Helper.DateStringType.DateTime);

                        depositInfo.Action = item["action_text"].ToString();
                        depositInfo.Deposit = item["deposit_change"].ToString();
                        depositInfo.ISPName = item["isp_name"].ToString();
                        depositInfo.AdminName = item["admin_name"].ToString();
                        depositInfo.RemoteAddress = item["remote_addr"].ToString();
                        depositInfo.Comment = item["comment"].ToString();

                        depositInfoList.Add(depositInfo);
                    }
                }

                return depositInfoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ADSLCreditChangesInfo> GetUserCreditChanges(string IBS_User_ID, string PConnectionType, string startDate, string endDate)
        {
            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();

                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");

                conds.Add("change_time_from", startDate);
                conds.Add("change_time_from_unit", "jalali");
                conds.Add("change_time_to", endDate);
                conds.Add("change_time_to_unit", "jalali");

                conds.Add("user_ids", IBS_User_ID);
                arguments.Add("conds", conds);

                arguments.Add("from", 0);
                arguments.Add("to", 1000);
                arguments.Add("sort_by", "change_time");
                arguments.Add("desc", true);

                XmlRpcStruct result = ibsngService.GetUserCreditChanges(arguments);

                ADSLCreditChangesInfo creditInfo = new ADSLCreditChangesInfo();
                List<ADSLCreditChangesInfo> creditInfoList = new List<ADSLCreditChangesInfo>();

                object[] report = (object[])result["report"];

                if (report.Count() != 0)
                {
                    foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                    {
                        creditInfo = new ADSLCreditChangesInfo();

                        DateTime date = Convert.ToDateTime(item["change_time_formatted"].ToString());
                        creditInfo.Date = Helper.GetPersianDate(date, Helper.DateStringType.DateTime);

                        creditInfo.Action = item["action_text"].ToString();
                        creditInfo.ISPName = item["isp_name"].ToString();
                        creditInfo.AdminName = item["admin_name"].ToString();
                        creditInfo.Credit = item["per_user_credit"].ToString();
                        creditInfo.Comment = item["comment"].ToString();

                        object[][] userIDs = (object[][])item["user_ids"];
                        creditInfo.BeforeCredit = userIDs[0][1].ToString();
                        if (creditInfo.BeforeCredit.StartsWith("-"))
                            creditInfo.AfterCredit = (Convert.ToDouble(creditInfo.Credit) - Convert.ToDouble(creditInfo.BeforeCredit.Substring(1))).ToString();
                        else
                            creditInfo.AfterCredit = (Convert.ToDouble(creditInfo.Credit) + Convert.ToDouble(creditInfo.BeforeCredit)).ToString();

                        creditInfoList.Add(creditInfo);
                    }
                }

                return creditInfoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ADSLAuditChangesInfo> GetUserAuditChanges(string IBS_User_ID, string PConnectionType, string startDate, string endDate)
        {
            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();

                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");

                conds.Add("change_time_from", startDate);
                conds.Add("change_time_from_unit", "jalali");
                conds.Add("change_time_to", endDate);
                conds.Add("change_time_to_unit", "jalali");

                conds.Add("user_ids", IBS_User_ID);
                arguments.Add("conds", conds);

                arguments.Add("from", 0);
                arguments.Add("to", 1000);
                arguments.Add("sort_by", "change_time");
                arguments.Add("desc", true);

                XmlRpcStruct result = ibsngService.GetUserAuditLogs(arguments);

                //double sum_deposit_change = 0, deposit_change = 0, count = 1, action, Last_deposit_change = 0;

                ADSLAuditChangesInfo auditInfo = new ADSLAuditChangesInfo();
                List<ADSLAuditChangesInfo> auditInfoList = new List<ADSLAuditChangesInfo>();

                object[] report = (object[])result["report"];

                if (report.Count() != 0)
                {
                    foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                    {
                        auditInfo = new ADSLAuditChangesInfo();

                        DateTime date = Convert.ToDateTime(item["change_time_formatted"].ToString());
                        auditInfo.Date = Helper.GetPersianDate(date, Helper.DateStringType.DateTime);

                        auditInfo.AttrName = item["attr_name"].ToString();
                        auditInfo.AdminName = item["admin_name"].ToString();
                        auditInfo.OldValue = item["old_value"].ToString();
                        auditInfo.NewValue = item["new_value"].ToString();
                        auditInfo.RemoteIP = item["remote_ip"].ToString();
                        if (string.Equals(item["is_user"].ToString(), "t"))
                            auditInfo.IsUser = "کاربر";
                        else
                            auditInfo.IsUser = "گروه";

                        auditInfoList.Add(auditInfo);
                    }
                }

                return auditInfoList;
            }
            catch (Exception ex)
            {
                throw ex;
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

        public static string EncodePassword_MD5(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ShowInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                telephoneNo = MainTelephoneNoTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(telephoneNo))
                {
                    NameTextBox.Text = string.Empty;
                    StatusCustomerTextBox.Text = string.Empty;
                    TelephoneNoTextBox.Text = string.Empty;
                    PasswordTextBox.Text = string.Empty;
                    ChangePasswordTextBox.Text = string.Empty;
                    IPTextBox.Text = string.Empty;
                    UserIDTextBox.Text = string.Empty;
                    MobileTextBox.Text = string.Empty;
                    EmailTextBox.Text = string.Empty;
                    PostalCodeTextBox.Text = string.Empty;
                    CenterTextBox.Text = string.Empty;
                    ServiceTitleTextBox.Text = string.Empty;
                    AddressTextBox.Text = string.Empty;
                    RealFisrtLoginTextBox.Text = string.Empty;
                    FisrtLoginTextBox.Text = string.Empty;
                    NearestExpDateTextBox.Text = string.Empty;
                    RemainedCreditTextBox.Text = string.Empty;
                    UserStatusImage.Source = null;
                    LimitMacTextBox.Text = string.Empty;
                    MacAddressTextBox.Text = string.Empty;
                    NextGroupTextBox.Text = string.Empty;
                    rechargeDepositTextBox.Text = string.Empty;
                    LockCommentTextBox.Text = string.Empty;
                    LockStatusImage.Visibility = Visibility.Collapsed;
                    FisrtServiceLoginTextBox.Text = string.Empty;
                    RemainedDayTextBox.Text = string.Empty;
                    MultiLoginTextBox.Text = string.Empty;
                    GroupTextBox.Text = string.Empty;
                    BandWithTextBox.Text = string.Empty;
                    TrafficTextBox.Text = string.Empty;
                    DurationTextBox.Text = string.Empty;
                    PortInfoGrid.DataContext = null;
                    PortNoTextBox.Text = string.Empty;
                    MDFDescriptionTextBox.Text = string.Empty;

                    throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");
                }

                ADSL aDSL = null;

                if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                {
                    aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));

                    if (aDSL == null)
                    {
                        NameTextBox.Text = string.Empty;
                        StatusCustomerTextBox.Text = string.Empty;
                        TelephoneNoTextBox.Text = string.Empty;
                        PasswordTextBox.Text = string.Empty;
                        ChangePasswordTextBox.Text = string.Empty;
                        IPTextBox.Text = string.Empty;
                        UserIDTextBox.Text = string.Empty;
                        MobileTextBox.Text = string.Empty;
                        EmailTextBox.Text = string.Empty;
                        PostalCodeTextBox.Text = string.Empty;
                        CenterTextBox.Text = string.Empty;
                        ServiceTitleTextBox.Text = string.Empty;
                        AddressTextBox.Text = string.Empty;
                        RealFisrtLoginTextBox.Text = string.Empty;
                        FisrtLoginTextBox.Text = string.Empty;
                        NearestExpDateTextBox.Text = string.Empty;
                        RemainedCreditTextBox.Text = string.Empty;
                        UserStatusImage.Source = null;
                        LimitMacTextBox.Text = string.Empty;
                        MacAddressTextBox.Text = string.Empty;
                        NextGroupTextBox.Text = string.Empty;
                        rechargeDepositTextBox.Text = string.Empty;
                        LockCommentTextBox.Text = string.Empty;
                        LockStatusImage.Visibility = Visibility.Collapsed;
                        FisrtServiceLoginTextBox.Text = string.Empty;
                        RemainedDayTextBox.Text = string.Empty;
                        MultiLoginTextBox.Text = string.Empty;
                        GroupTextBox.Text = string.Empty;
                        BandWithTextBox.Text = string.Empty;
                        TrafficTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        PortInfoGrid.DataContext = null;
                        PortNoTextBox.Text = string.Empty;
                        MDFDescriptionTextBox.Text = string.Empty;

                        throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");
                    }

                    int centerID = CenterDB.GetCenterIDbyTelephoneNo(aDSL.TelephoneNo);

                    if (!DB.CurrentUser.CenterIDs.Contains(centerID))
                        throw new Exception("دسترسی مرکز برای مشاهده مشخصات ADSL وجود ندارد !");

                    if (aDSL.ADSLPortID != null)
                    {
                        ADSLPortInfo DischargePortInfo = Data.ADSLPortDB.GetADSlPortInfoByID((long)aDSL.ADSLPortID);

                        PortInfoGrid.DataContext = DischargePortInfo;
                        PortNoTextBox.Text = DischargePortInfo.Port;
                        MDFDescriptionTextBox.Text = DischargePortInfo.MDFTitle;
                    }
                    else
                    {
                        PortInfoGrid.DataContext = null;
                        PortNoTextBox.Text = string.Empty;
                        MDFDescriptionTextBox.Text = string.Empty;
                    }

                    ItemsDataGrid.ItemsSource = ADSLSupportRequestDB.GetADSLSupportRequestList(Convert.ToInt64(telephoneNo));
                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());
                    CommnetsDataGrid.ItemsSource = ADSLSupportCommnetDB.GetCommnetbytelephoneNo(Convert.ToInt64(telephoneNo));
                }
                else
                {
                    aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

                    if (aDSL == null)
                    {
                        NameTextBox.Text = string.Empty;
                        StatusCustomerTextBox.Text = string.Empty;
                        TelephoneNoTextBox.Text = string.Empty;
                        PasswordTextBox.Text = string.Empty;
                        ChangePasswordTextBox.Text = string.Empty;
                        IPTextBox.Text = string.Empty;
                        UserIDTextBox.Text = string.Empty;
                        MobileTextBox.Text = string.Empty;
                        EmailTextBox.Text = string.Empty;
                        PostalCodeTextBox.Text = string.Empty;
                        CenterTextBox.Text = string.Empty;
                        ServiceTitleTextBox.Text = string.Empty;
                        AddressTextBox.Text = string.Empty;
                        RealFisrtLoginTextBox.Text = string.Empty;
                        FisrtLoginTextBox.Text = string.Empty;
                        NearestExpDateTextBox.Text = string.Empty;
                        RemainedCreditTextBox.Text = string.Empty;
                        UserStatusImage.Source = null;
                        LimitMacTextBox.Text = string.Empty;
                        MacAddressTextBox.Text = string.Empty;
                        NextGroupTextBox.Text = string.Empty;
                        rechargeDepositTextBox.Text = string.Empty;
                        LockCommentTextBox.Text = string.Empty;
                        LockStatusImage.Visibility = Visibility.Collapsed;
                        FisrtServiceLoginTextBox.Text = string.Empty;
                        RemainedDayTextBox.Text = string.Empty;
                        MultiLoginTextBox.Text = string.Empty;
                        GroupTextBox.Text = string.Empty;
                        BandWithTextBox.Text = string.Empty;
                        TrafficTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        PortInfoGrid.DataContext = null;
                        PortNoTextBox.Text = string.Empty;
                        MDFDescriptionTextBox.Text = string.Empty;

                        throw new Exception("کد وارد شده دارای ADSL نمی باشد !");
                    }
                }

                GetADSLInfoByPhoneNumber(telephoneNo);

                ADSLCustomerInfo aDSLInfo = ADSLDB.GetADSLCustomerInfobyID(aDSL.ID);
                IPStaticTextBox.Text = aDSLInfo.IPStatic;
                GroupIPStaticTextBox.Text = aDSLInfo.GroupIPStatic;
                IpStartDateTextBox.Text = aDSLInfo.IPStartDate;
                IPEndDateTextBox.Text = aDSLInfo.IPEndDate;

                ConnectionLogsDataGrid.ItemsSource = null;
                DepositChangeDataGrid.ItemsSource = null;
                CreditChangeDataGrid.ItemsSource = null;
                AuditChangeDataGrid.ItemsSource = null;
                HistoryServiceDataGrid.ItemsSource = null;
                HistoryTrafficDataGrid.ItemsSource = null;

                StartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                EndDate.SelectedDate = DB.GetServerDate();

                DepositStartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                DepositEndDate.SelectedDate = DB.GetServerDate();

                CreditStartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                CreditEndDate.SelectedDate = DB.GetServerDate();

                AuditStartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                AuditEndDate.SelectedDate = DB.GetServerDate();

                HistoryServiceStartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                HistoryServiceEndDate.SelectedDate = DB.GetServerDate();

                HistoryTrafficStartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                HistoryTrafficEndDate.SelectedDate = DB.GetServerDate();

                HistoryRequestStartDate.SelectedDate = DB.GetServerDate().AddDays(-10);
                HistoryRequestEndDate.SelectedDate = DB.GetServerDate();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void SearchConnectionLogsButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)StartDate.SelectedDate;
            string startDateString = (StartDate.SelectedDate != null) ? Helper.GetPersianDate(startDate.AddDays(-1), Helper.DateStringType.Short) : RealFisrtLoginTextBox.Text;
            string[] startDateStringList = startDateString.Split('/');

            if (startDateStringList[1].Count() < 2)
                startDateStringList[1] = "0" + startDateStringList[1];

            if (startDateStringList[2].Count() < 2)
                startDateStringList[2] = "0" + startDateStringList[2];

            startDateString = startDateStringList[0] + "/" + startDateStringList[1] + "/" + startDateStringList[2];

            DateTime endDate = (DateTime)EndDate.SelectedDate;
            string endDateString = (EndDate.SelectedDate != null) ? Helper.GetPersianDate(endDate.AddDays(1), Helper.DateStringType.Short) : Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short);
            string[] endDateStringList = endDateString.Split('/');

            if (endDateStringList[1].Count() < 2)
                endDateStringList[1] = "0" + endDateStringList[1];

            if (endDateStringList[2].Count() < 2)
                endDateStringList[2] = "0" + endDateStringList[2];

            endDateString = endDateStringList[0] + "/" + endDateStringList[1] + "/" + endDateStringList[2];

            ConnectionLogsDataGrid.ItemsSource = GetUserConnectionLogs(UserIDTextBox.Text.Trim(), "All", startDateString, endDateString);
        }

        private void DeleteRemainDayImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از صفر کردن روزهای باقیمانده مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    ADSL aDSL = null;

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
                    else
                        aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

                    if (aDSL == null)
                        throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    //XmlRpcStruct list = new XmlRpcStruct();
                    //list.Add("exp_date_temp_extend", "");
                    //list.Add("exp_date_temp_extend_unit", "-1");

                    userInfo.Add("exp_date_temp_extend", "-1");
                    userInfo.Add("exp_date_temp_extend_unit", "Days");

                    //XmlRpcStruct list = new XmlRpcStruct();
                    //list.Add("to_del_attrs", "temporary_extend");

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.DeleteRemainDay;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = RemainDaysTextBox.Text.Trim();
                    log.NewValue = "0";

                    log.Detach();
                    Save(log);

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات صفر کردن روزهای باقی مانده با موفقیت انجام شد.");

                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void ChangePasswordImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از تغییر رمز ورود مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    if (string.IsNullOrWhiteSpace(ChangePasswordTextBox.Text))
                        throw new Exception("لطفا رمز ورود مورد نظر را وارد نمایید !");

                    ADSL aDSL = null;

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
                    else
                        aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    XmlRpcStruct dictionary = new XmlRpcStruct();
                    dictionary.Add("normal_username", telephoneNo);
                    dictionary.Add("normal_password", ChangePasswordTextBox.Text.Trim());
                    userInfo.Add("normal_user_spec", dictionary);

                    userAuthentication.Clear();
                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.ChangePassword;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = PasswordTextBox.Text.Trim();
                    log.NewValue = ChangePasswordTextBox.Text.Trim();

                    log.Detach();
                    Save(log);

                    aDSL.AAAPassword = ChangePasswordTextBox.Text.Trim();

                    aDSL.Detach();
                    Save(aDSL);

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات تغییر رمز ورود با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void ChangeWebPasswordImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از تغییر رمز ورود پنل مشترک مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    if (string.IsNullOrWhiteSpace(ChangeWebPasswordTextBox.Text))
                        throw new Exception("لطفا رمز ورود مورد نظر را وارد نمایید !");

                    ADSL aDSL = null;

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
                    else
                        aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

                    string md5NewPassword = EncodePassword_MD5(ChangeWebPasswordTextBox.Text.Trim()).Replace("-", "").ToLower();

                    aDSL.HashPassword = md5NewPassword;
                    aDSL.OrginalPassword = ChangeWebPasswordTextBox.Text.Trim();

                    aDSL.Detach();
                    Save(aDSL);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.ChangeWebPassword;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = "";
                    log.NewValue = ChangeWebPasswordTextBox.Text.Trim();

                    log.Detach();
                    Save(log);

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    if (aDSL.CustomerOwnerID != null)
                    {
                        string[] mobileNos = new string[1];

                        //SOAPSendSMS.MessageRelayService sms = new SOAPSendSMS.MessageRelayService();

                        //if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID)))
                        //    mobileNos[0] = CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID);

                        //sms.sendMessageOneToMany("tctsemnan", "6311256", "00042020", mobileNos, "رمز عبور پنل شما به " + ChangeWebPasswordTextBox.Text.Trim() + " تغییر یافته است.");
                    }

                    ShowSuccessMessage("عملیات تغییر رمز ورود با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void DeleteMACAddressImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از حذف آدر فیزیکی مودم مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    //ADSL aDSL = ADSLDB.GetADSLByUserName(telephoneNo);

                    //if (aDSL == null)
                    //    throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    //userInfo.Add("limit_mac", "11:11:11:11:11:11");

                    XmlRpcStruct list = new XmlRpcStruct();
                    list.Add("to_del_attrs", "limit_mac");

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", list);

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.DeleteMACAddress;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = MacAddressTextBox.Text;
                    log.NewValue = string.Empty;

                    log.Detach();
                    Save(log);

                    MacAddressTextBox.Text = string.Empty;

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات حذف آدرس فیزیکی با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void ChangeMACAddressImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از تغییر آدرس فیزیکی مودم مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    if (string.IsNullOrWhiteSpace(NewMacAddressTextBox.Text.Trim()))
                        throw new Exception("لطفا آدرس فیزیکی جدید را وارد نمایید");

                    string newMacAddress = NewMacAddressTextBox.Text.Trim();

                    if (newMacAddress.Split(':').Count() != 6)
                        throw new Exception("لطفا آدرس فیزیکی صحیح وارد نمایید !");

                    string[] newMACAddressList = newMacAddress.Split(':');
                    foreach (string item in newMACAddressList)
                    {
                        if (item.Count() != 2)
                            throw new Exception("لطفا آدرس فیزیکی صحیح وارد نمایید !");
                    }

                    //ADSL aDSL = ADSLDB.GetADSLByUserName(MainTelephoneNoTextBox.Text.Trim());

                    //if (aDSL == null)
                    //    throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    if (!string.IsNullOrWhiteSpace(MacAddressTextBox.Text))
                        userInfo.Add("limit_mac", MacAddressTextBox.Text + "," + newMacAddress);
                    else
                        userInfo.Add("limit_mac", newMacAddress);

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.ChangeMACAddress;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = MacAddressTextBox.Text;
                    if (!string.IsNullOrWhiteSpace(MacAddressTextBox.Text))
                        log.NewValue = MacAddressTextBox.Text + " , " + newMacAddress;
                    else
                        log.NewValue = newMacAddress;

                    log.Detach();
                    Save(log);

                    if (!string.IsNullOrWhiteSpace(MacAddressTextBox.Text))
                        MacAddressTextBox.Text = MacAddressTextBox.Text + " , " + newMacAddress;
                    else
                        MacAddressTextBox.Text = newMacAddress;
                    NewMacAddressTextBox.Text = string.Empty;

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات افزودن آدرس فیزیکی با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void ChangeMultiLoginImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از تغییر تعداد اتصال ها مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(MultiLoginTextBox.Text.Trim()))
                        throw new Exception("لطفا تعداد اتصال ها را تعیین نمایید !");

                    int multiLoginCount = Convert.ToInt32(MultiLoginTextBox.Text.Trim());

                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    //ADSL aDSL = ADSLDB.GetADSLByUserName(MainTelephoneNoTextBox.Text.Trim());

                    //if (aDSL == null)
                    //    throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    if (multiLoginCount == 0)
                    {
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

                        string oldMultiLogin = "";

                        try
                        {
                            oldMultiLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["multi_login"]);
                        }
                        catch (Exception ex)
                        {
                            oldMultiLogin = "0";
                        }

                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        XmlRpcStruct list = new XmlRpcStruct();
                        list.Add("to_del_attrs", "multi_login");

                        userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                        userAuthentication.Add("attrs", userInfo);
                        userAuthentication.Add("to_del_attrs", list);

                        ibsngService.UpdateUserAttrs(userAuthentication);

                        ADSLAAAActionLog log = new ADSLAAAActionLog();

                        if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                            log.TelephoneNo = Convert.ToInt64(telephoneNo);
                        else
                            log.Wireless = telephoneNo;
                        log.ActionID = (byte)DB.ADSLAAAction.ChangeMultiLogin;
                        log.UserID = DB.CurrentUser.ID;
                        log.OldValue = oldMultiLogin;
                        log.NewValue = MultiLoginTextBox.Text.Trim();

                        log.Detach();
                        Save(log);

                        ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                        ShowSuccessMessage("عملیات تغییر تعداد اتصال ها با موفقیت انجام شد.");
                    }
                    else
                    {
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

                        string oldMultiLogin = "";

                        try
                        {
                            oldMultiLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["multi_login"]);
                        }
                        catch (Exception ex)
                        {
                            oldMultiLogin = "0";
                        }

                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        userInfo.Add("multi_login", MultiLoginTextBox.Text.Trim());

                        userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                        userAuthentication.Add("attrs", userInfo);
                        userAuthentication.Add("to_del_attrs", "");

                        ibsngService.UpdateUserAttrs(userAuthentication);

                        ADSLAAAActionLog log = new ADSLAAAActionLog();

                        if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                            log.TelephoneNo = Convert.ToInt64(telephoneNo);
                        else
                            log.Wireless = telephoneNo;
                        log.ActionID = (byte)DB.ADSLAAAction.ChangeMultiLogin;
                        log.UserID = DB.CurrentUser.ID;
                        log.OldValue = oldMultiLogin;
                        log.NewValue = MultiLoginTextBox.Text.Trim();

                        log.Detach();
                        Save(log);

                        ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                        ShowSuccessMessage("عملیات تغییر تعداد اتصال ها با موفقیت انجام شد.");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void KillUserImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از Kill کردن کاربر مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    //ADSL aDSL = ADSLDB.GetADSLByUserName(MainTelephoneNoTextBox.Text.Trim());

                    //if (aDSL == null)
                    //    throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

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

                    System.String[][] internetOnlinesList = ((System.String[][])(userInfo["internet_onlines"]));
                    string rasIP = internetOnlinesList[0][0];
                    string uniqueID = internetOnlinesList[0][2];

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    //userInfo.Add("multi_login", "0");

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("ras_ip", rasIP);
                    userAuthentication.Add("unique_id_val", uniqueID);
                    userAuthentication.Add("kill", "1");
                    //userAuthentication.Add("unique_id", uniqueID);
                    //userAuthentication.Add("attrs", userInfo);
                    //userAuthentication.Add("to_del_attrs", "");

                    ibsngService.KillUser(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.KillUser;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = string.Empty;
                    log.NewValue = string.Empty;

                    log.Detach();
                    Save(log);

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات Kill کردن کاربر با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void LockUserImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از Lock کردن کاربر مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    //ADSL aDSL = ADSLDB.GetADSLByUserName(MainTelephoneNoTextBox.Text.Trim());

                    //if (aDSL == null)
                    //    throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    if (!string.IsNullOrWhiteSpace(LockCommentTextBox.Text.Trim()))
                        userInfo.Add("lock", LockCommentTextBox.Text.Trim());
                    else
                        throw new Exception("لطفا توضیحات مربوط به عملیات Lock را وارد نمایید");

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.LockUser;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = string.Empty;
                    log.NewValue = LockCommentTextBox.Text.Trim();

                    log.Detach();
                    Save(log);

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات Lock کردم کاربر با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void UnLockUserImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("آیا از UnLock کردن کاربر مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(telephoneNo))
                        throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                    //ADSL aDSL = ADSLDB.GetADSLByUserName(MainTelephoneNoTextBox.Text.Trim());

                    //if (aDSL == null)
                    //    throw new Exception("شماره تلفن مورد نظر دارای ADSL نمی باشد !");

                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    XmlRpcStruct list = new XmlRpcStruct();
                    list.Add("to_del_attrs", "lock");

                    userAuthentication.Add("user_id", UserIDTextBox.Text.Trim());
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", list);

                    ibsngService.UpdateUserAttrs(userAuthentication);

                    ADSLAAAActionLog log = new ADSLAAAActionLog();

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        log.TelephoneNo = Convert.ToInt64(telephoneNo);
                    else
                        log.Wireless = telephoneNo;
                    log.ActionID = (byte)DB.ADSLAAAction.UnLockUser;
                    log.UserID = DB.CurrentUser.ID;
                    log.OldValue = LockCommentTextBox.Text;
                    log.NewValue = string.Empty;

                    log.Detach();
                    Save(log);

                    LockCommentTextBox.Text = string.Empty;

                    ActionLogDataGrid.ItemsSource = ADSLAAATypeDB.GetADSLAAALog(telephoneNo, UserTextBox.Text.Trim());

                    ShowSuccessMessage("عملیات Unlock کردن کاربر با موفقیت انجام شد.");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void ImageView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[][] detail = (string[][])(sender as Image).Tag;

            ADSLAAAInfoConnectionLog window = new ADSLAAAInfoConnectionLog(detail);
            window.ShowDialog();
        }

        private void SearchChangeCreditButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)CreditStartDate.SelectedDate;
            string startDateString = (CreditStartDate.SelectedDate != null) ? Helper.GetPersianDate(startDate, Helper.DateStringType.Short) : RealFisrtLoginTextBox.Text;
            string[] startDateStringList = startDateString.Split('/');

            if (startDateStringList[1].Count() < 2)
                startDateStringList[1] = "0" + startDateStringList[1];

            if (startDateStringList[2].Count() < 2)
                startDateStringList[2] = "0" + startDateStringList[2];

            startDateString = startDateStringList[0] + "/" + startDateStringList[1] + "/" + startDateStringList[2];

            DateTime endDate = (DateTime)CreditEndDate.SelectedDate;
            string endDateString = (CreditEndDate.SelectedDate != null) ? Helper.GetPersianDate(endDate.AddDays(1), Helper.DateStringType.Short) : Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short);
            string[] endDateStringList = endDateString.Split('/');

            if (endDateStringList[1].Count() < 2)
                endDateStringList[1] = "0" + endDateStringList[1];

            if (endDateStringList[2].Count() < 2)
                endDateStringList[2] = "0" + endDateStringList[2];

            endDateString = endDateStringList[0] + "/" + endDateStringList[1] + "/" + endDateStringList[2];

            CreditChangeDataGrid.ItemsSource = GetUserCreditChanges(UserIDTextBox.Text.Trim(), "All", startDateString, endDateString);
        }

        private void SearchChangeDepositButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)DepositStartDate.SelectedDate;
            string startDateString = (DepositStartDate.SelectedDate != null) ? Helper.GetPersianDate(startDate, Helper.DateStringType.Short) : RealFisrtLoginTextBox.Text;
            string[] startDateStringList = startDateString.Split('/');

            if (startDateStringList[1].Count() < 2)
                startDateStringList[1] = "0" + startDateStringList[1];

            if (startDateStringList[2].Count() < 2)
                startDateStringList[2] = "0" + startDateStringList[2];

            startDateString = startDateStringList[0] + "/" + startDateStringList[1] + "/" + startDateStringList[2];

            DateTime endDate = (DateTime)DepositEndDate.SelectedDate;
            string endDateString = (EndDate.SelectedDate != null) ? Helper.GetPersianDate(endDate.AddDays(1), Helper.DateStringType.Short) : Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short);
            string[] endDateStringList = endDateString.Split('/');

            if (endDateStringList[1].Count() < 2)
                endDateStringList[1] = "0" + endDateStringList[1];

            if (endDateStringList[2].Count() < 2)
                endDateStringList[2] = "0" + endDateStringList[2];

            endDateString = endDateStringList[0] + "/" + endDateStringList[1] + "/" + endDateStringList[2];

            DepositChangeDataGrid.ItemsSource = GetUserDepositChanges(UserIDTextBox.Text.Trim(), "All", startDateString, endDateString);
        }

        private void SearchChangeAuditButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)AuditStartDate.SelectedDate;
            string startDateString = (StartDate.SelectedDate != null) ? Helper.GetPersianDate(startDate, Helper.DateStringType.Short) : RealFisrtLoginTextBox.Text;
            string[] startDateStringList = startDateString.Split('/');

            if (startDateStringList[1].Count() < 2)
                startDateStringList[1] = "0" + startDateStringList[1];

            if (startDateStringList[2].Count() < 2)
                startDateStringList[2] = "0" + startDateStringList[2];

            startDateString = startDateStringList[0] + "/" + startDateStringList[1] + "/" + startDateStringList[2];

            DateTime endDate = (DateTime)AuditEndDate.SelectedDate;
            string endDateString = (EndDate.SelectedDate != null) ? Helper.GetPersianDate(endDate.AddDays(1), Helper.DateStringType.Short) : Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short);
            string[] endDateStringList = endDateString.Split('/');

            if (endDateStringList[1].Count() < 2)
                endDateStringList[1] = "0" + endDateStringList[1];

            if (endDateStringList[2].Count() < 2)
                endDateStringList[2] = "0" + endDateStringList[2];

            endDateString = endDateStringList[0] + "/" + endDateStringList[1] + "/" + endDateStringList[2];

            AuditChangeDataGrid.ItemsSource = GetUserAuditChanges(UserIDTextBox.Text.Trim(), "All", startDateString, endDateString);
        }

        private void HistoryServiceButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)HistoryServiceStartDate.SelectedDate;
            DateTime endDate = (DateTime)HistoryServiceEndDate.SelectedDate;

            HistoryServiceDataGrid.ItemsSource = ADSLChangeTariffDB.GetADSLChangeServiceInfo(telephoneNo, startDate, endDate.AddDays(1));
        }

        private void HistoryTrafficButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)HistoryTrafficStartDate.SelectedDate;
            DateTime endDate = (DateTime)HistoryTrafficEndDate.SelectedDate;

            if (!telephoneNo.Contains("wl") && !telephoneNo.Contains("WL"))
                HistoryTrafficDataGrid.ItemsSource = ADSLSellTrafficDB.GetADSLSellTrafficInfo(telephoneNo, startDate, endDate.AddDays(1));
            else
                HistoryTrafficDataGrid.ItemsSource = ADSLSellTrafficDB.GetWirelessSellTrafficInfo(telephoneNo, startDate, endDate.AddDays(1));
        }

        private void HistoryRequestButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)HistoryRequestStartDate.SelectedDate;
            DateTime endDate = (DateTime)HistoryRequestEndDate.SelectedDate;

            HistoryRequestDataGrid.ItemsSource = ADSLHistoryDB.GetADSLHistoryRequestbyDate(Convert.ToInt64(telephoneNo), startDate, endDate.AddDays(1));
        }

        private void SupportSaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Request request = new Request();
                ADSLSupportRequest supportRequest = new ADSLSupportRequest();

                if (string.IsNullOrWhiteSpace(telephoneNo))
                    throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                request.TelephoneNo = Convert.ToInt64(telephoneNo);

                CRM.Data.Status status = DB.GetStatus((byte)DB.RequestType.ADSLSupport, (int)DB.RequestStatusType.Start);
                request.StatusID = status.ID;

                request.RequestDate = DB.GetServerDate();
                request.CenterID = CRM.Data.TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(telephoneNo)).CenterID;
                request.RequestTypeID = (byte)DB.RequestType.ADSLSupport;
                request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;

                if (string.IsNullOrWhiteSpace(CommentTextBox.Text))
                    throw new Exception("لطفا توضیحات لازم را وارد نمایید !");

                supportRequest.FirstDescription = CommentTextBox.Text.Trim();

                RequestForADSLSupport.SaveADSLSupportRequest(request, supportRequest, null, true);

                ShowSuccessMessage("درخواست پیگیری با موفقیت ارسال شد.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void NewMacAddressTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            string s = e.Text;

            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            if (string.Equals(s, ":") || string.Equals(s, "a") || string.Equals(s, "b") || string.Equals(s, "c") || string.Equals(s, "d") || string.Equals(s, "e") || string.Equals(s, "4"))
                e.Handled = false;
        }

        private void MultiLoginTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);

            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void AddTrafficIBSngItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HistoryTrafficDataGrid.SelectedIndex >= 0)
                {
                    ADSLSellTrafficRequestInfo item = HistoryTrafficDataGrid.SelectedItem as ADSLSellTrafficRequestInfo;

                    if (item == null)
                        return;

                    ADSL aDSL = null;

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
                    else
                        aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

                    Data.ADSLSellTraffic aDSLSellTrassic = ADSLSellTrafficDB.GetADSLSellTrafficById(item.RequestID);
                    ADSLService AdditionalTraffic = ADSLServiceDB.GetADSLServiceById((int)aDSLSellTrassic.AdditionalServiceID);
                    ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)AdditionalTraffic.TrafficID);

                    if (item.IBSngStatus == (byte)DB.IBSngStatus.OK)
                        throw new Exception("پیش از این در سیستم AAA اعمال شده است!");

                    if (item.IBSngStatus == (byte)DB.IBSngStatus.Null)
                        throw new Exception("هزینه این درخواست پرداخت نشده است!");

                    if (item.IBSngStatus == (byte)DB.IBSngStatus.No)
                    {
                        IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                        XmlRpcStruct userAuthentication = new XmlRpcStruct();
                        XmlRpcStruct userInfos = new XmlRpcStruct();
                        XmlRpcStruct userInfo = new XmlRpcStruct();

                        userAuthentication.Clear();

                        userAuthentication.Add("user_id", aDSL.UserID);
                        userAuthentication.Add("deposit", traffic.Credit.ToString());
                        userAuthentication.Add("is_absolute_change", false);
                        userAuthentication.Add("deposit_type", "recharge");
                        userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Sell Traffice Request (Recharge)");

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        try
                        {
                            ibsngService.changeDeposit(userAuthentication);
                        }
                        catch (Exception ex)
                        { }
                    }

                    aDSLSellTrassic.IsIBSngUpdated = true;

                    aDSLSellTrassic.Detach();
                    Save(aDSLSellTrassic);

                    ShowSuccessMessage("با موفقیت در AAA ثبت شد.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void AddServiceIBSngItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HistoryServiceDataGrid.SelectedIndex >= 0)
                {
                    ADSLChangeServiceRequestInfo item = HistoryServiceDataGrid.SelectedItem as ADSLChangeServiceRequestInfo;

                    if (item == null)
                        return;

                    ADSL aDSL = null;

                    if (!telephoneNo.Contains("WL") && !telephoneNo.Contains("wl"))
                        aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(telephoneNo));
                    else
                        aDSL = ADSLDB.GetWirelessbyCode(telephoneNo);

                    Data.ADSLChangeService aDSLChangeService = ADSLChangeTariffDB.GetADSLChangeServicebyID(item.RequestID);
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)aDSLChangeService.NewServiceID);
                    ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID);

                    if (item.IBSngStatus == (byte)DB.IBSngStatus.OK)
                        throw new Exception("پیش از این در سیستم AAA اعمال شده است!");

                    if (item.IBSngStatus == (byte)DB.IBSngStatus.Null)
                        throw new Exception("هزینه این درخواست پرداخت نشده است!");

                    if (item.IBSngStatus == (byte)DB.IBSngStatus.No)
                    {
                        IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                        XmlRpcStruct userAuthentication = new XmlRpcStruct();
                        XmlRpcStruct userInfos = new XmlRpcStruct();
                        XmlRpcStruct userInfo = new XmlRpcStruct();

                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        userAuthentication.Add("user_id", aDSL.UserID);

                        userAuthentication.Add("deposit", traffic.Credit.ToString());
                        userAuthentication.Add("is_absolute_change", false);
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
                        }
                        catch (Exception)
                        {
                            throw new Exception("تغییر گروه با موفقیت انجام نشد");
                        }
                    }

                    aDSLChangeService.IsIBSngUpdated = true;

                    aDSLChangeService.Detach();
                    Save(aDSLChangeService);

                    ShowSuccessMessage("با موفقیت در AAA ثبت شد.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void SearchUserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveCommnetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Comment1TextBox.Text))
                    throw new Exception("لطفا توضیحات را وارد نمایید !");

                ADSLSupportCommnet commnet = new ADSLSupportCommnet();

                commnet.TelephoneNo = Convert.ToInt64(telephoneNo);
                commnet.UserID = DB.CurrentUser.ID;
                commnet.Commnet = Comment1TextBox.Text;

                commnet.Detach();
                DB.Save(commnet, true);

                Comment1TextBox.Text = string.Empty;

                CommnetsDataGrid.ItemsSource = ADSLSupportCommnetDB.GetCommnetbytelephoneNo(Convert.ToInt64(telephoneNo));
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        #endregion
    }
}
