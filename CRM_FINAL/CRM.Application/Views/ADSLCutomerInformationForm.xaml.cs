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
using CookComputing.XmlRpc;
using CRM.Data.Services;
using System.Collections;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Views
{
    public partial class ADSLCutomerInformationForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;

        #endregion

        #region Constructors

        public ADSLCutomerInformationForm()
        {
            InitializeComponent();
        }

        public ADSLCutomerInformationForm(long id)
        {
            InitializeComponent();

            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (_ID != 0)
            {
                ADSLCustomerInfo aDSL = ADSLDB.GetADSLCustomerInfobyID(_ID);
                ADSLInfo.DataContext = aDSL;

                if (aDSL.ServiceID != null)
                {
                    ADSLServiceInfo serviceInfo = ADSLServiceDB.GetADSLServiceInfoById(aDSL.ServiceID);
                    ServiceInfo.DataContext = serviceInfo;
                }

                StartDateTextBox.Text = aDSL.InstallDate;
                EndDateTextBox.Text = aDSL.ExpDate;

                if (aDSL.PortID != null)
                {
                    ADSLPortInfo portInfo = ADSLPortDB.GetADSlPortInfoByID((long)aDSL.PortID);
                    PortInfo.DataContext = portInfo;
                }

                if (aDSL.Modem != null)
                {
                    ADSLModemPropertyInfo modemInfo = ADSLModemPropertyDB.GetADSLModemPropertyById((int)aDSL.ModemID);
                    ModemInfo.DataContext = modemInfo;
                }

                IPStaticTextBox.Text = aDSL.IPStatic;
                GroupIPStaticTextBox.Text = aDSL.GroupIPStatic;
                IpStartDateTextBox.Text = aDSL.IPStartDate;
                IPEndDateTextBox.Text = aDSL.IPEndDate;

                InstalmentDataGrid.ItemsSource = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));

                string paidInstalment = InstallmentRequestPaymentDB.GetSumPaidInstallmenttByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));

                if (!string.IsNullOrWhiteSpace(paidInstalment))
                    PaidInstalmentTextBox.Text = paidInstalment + " ریا ل";
                else
                    PaidInstalmentTextBox.Text = "0" + " ریا ل";

                string remainInstalment = InstallmentRequestPaymentDB.GetSumRemainInstallmenttByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));
                if (!string.IsNullOrWhiteSpace(remainInstalment))
                    RemainInstalmentsTextBox.Text = remainInstalment + " ریا ل";
                else
                    RemainInstalmentsTextBox.Text = "0" + " ریا ل";

                HistoryDataGrid.ItemsSource = ADSLHistoryDB.GetADSLHistorybyTelephoneNo(aDSL.TelephoneNo);
                HistoryRequestDataGrid.ItemsSource = ADSLHistoryDB.GetADSLHistoryRequest(Convert.ToInt64(aDSL.TelephoneNo));

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
                    userAuthentication.Add("normal_username", aDSL.TelephoneNo.ToString());
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
                    string nearestExp = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["nearest_exp_date"]);
                    DateTime nearestExpDate = Convert.ToDateTime(nearestExp);
                    if ((Convert.ToInt32((nearestExpDate - DB.GetServerDate()).TotalDays)) > 0)
                        RemainedDayTextBox.Text = (Convert.ToInt32((nearestExpDate - DB.GetServerDate()).TotalDays)) + "روز";
                    else
                        RemainedDayTextBox.Text = " صفر " + "روز";

                    NearestExpDateTextBox.Text = Helper.GetPersianDate(nearestExpDate, Helper.DateStringType.DateTime);
                    EndDateTextBox.Text = Helper.GetPersianDate(nearestExpDate, Helper.DateStringType.DateTime);
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

                try
                {
                    string realFirstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["real_first_login"]);
                    DateTime realFirstLoginDate = Convert.ToDateTime(realFirstLogin);
                    RealFisrtLoginTextBox.Text = Helper.GetPersianDate(realFirstLoginDate, Helper.DateStringType.DateTime);
                    StartDateTextBox.Text = Helper.GetPersianDate(realFirstLoginDate, Helper.DateStringType.DateTime);
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

                try
                {
                    string remainedCredit = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["credit"]);
                    if (!string.IsNullOrWhiteSpace(remainedCredit))
                    {
                        if (remainedCredit.Length > 10)
                            RemainedCreditTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["credit"]).Substring(0, 10);
                        else
                            RemainedCreditTextBox.Text = remainedCredit;
                    }
                    else
                        RemainedCreditTextBox.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    RemainedCreditTextBox.Text = string.Empty;
                }   

                try
                {
                    LimitMacTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["limit_mac"]);
                }
                catch (Exception ex)
                {
                    LimitMacTextBox.Text = string.Empty;
                }

                try
                {
                    NextGroupTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["renew_next_group"]);
                }
                catch (Exception ex)
                {
                    NextGroupTextBox.Text = "ندارد";
                }

                try
                {
                    rechargeDepositTextBox.Text = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["recharge_deposit"]);
                }
                catch (Exception ex)
                {
                    rechargeDepositTextBox.Text = string.Empty;
                }

                ResizeWindow();
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

        private void SendToPrint(IEnumerable result)
        {
            ADSLCustomerInfo aDSL = ADSLDB.GetADSLCustomerInfobyID(_ID);
            string paidInstalment = InstallmentRequestPaymentDB.GetSumPaidInstallmenttByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));
            string PaidInstalmentVariable;
            string RemainInstalmentVariable;

            if (!string.IsNullOrWhiteSpace(paidInstalment))
                PaidInstalmentVariable = paidInstalment + " ریا ل";
            else
                PaidInstalmentVariable = "0" + " ریا ل";

            string remainInstalment = InstallmentRequestPaymentDB.GetSumRemainInstallmenttByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));
            if (!string.IsNullOrWhiteSpace(remainInstalment))
                RemainInstalmentVariable = remainInstalment + " ریا ل";
            else
                RemainInstalmentVariable = "0" + " ریا ل";

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ADSLInstalmetTabReport);
            stiReport.Load(path);

            stiReport.Dictionary.Variables["PaidInstalment"].Value = PaidInstalmentVariable;
            stiReport.Dictionary.Variables["RemainInstalment"].Value = RemainInstalmentVariable;
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        
        private void Print_InstalmentTab(object sender, RoutedEventArgs e)
        {
            ADSLCustomerInfo aDSL = ADSLDB.GetADSLCustomerInfobyID(_ID);
            List<ADSLInstalmentInfo> Result=ReportDB.GetInstallmentRequestPaymentByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));
            foreach (ADSLInstalmentInfo info in Result)
            {
                if (info.IsPaidBool == true)
                {
                    info.IsPaid = "پرداخت شده";
                }
                else if (info.IsPaidBool == false)
                {
                    info.IsPaid = "پرداخت نشده";
                }
            }
            SendToPrint(Result);
            
        }

        private void CorrectionInstalment_Click(object sender, RoutedEventArgs e)
        {
            ADSLCustomerInfo aDSL = ADSLDB.GetADSLCustomerInfobyID(_ID);

            InstallmentRequestPaymentCorrectionForm Window = new InstallmentRequestPaymentCorrectionForm(Convert.ToInt64(aDSL.TelephoneNo));
            Window.ShowDialog();

            if (Window.DialogResult == true)
            {
                InstalmentDataGrid.ItemsSource = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));

                string paidInstalment = InstallmentRequestPaymentDB.GetSumPaidInstallmenttByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));

                if (!string.IsNullOrWhiteSpace(paidInstalment))
                    PaidInstalmentTextBox.Text = paidInstalment + " ریا ل";
                else
                    PaidInstalmentTextBox.Text = "0" + " ریا ل";
            }

            InstalmentDataGrid.ItemsSource = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByTelephoneNo(Convert.ToInt64(aDSL.TelephoneNo));
        }

        #endregion
    }
}
