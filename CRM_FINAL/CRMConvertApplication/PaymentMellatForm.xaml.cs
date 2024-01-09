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
using CookComputing.XmlRpc;
using CRM.Data.Services;
using System.Collections;

namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for PaymentMellatForm.xaml
    /// </summary>
    public partial class PaymentMellatForm : Window
    {
        public PaymentMellatForm()
        {
            InitializeComponent();

            SellTrafic();
            ChangeService();

            Application.Current.Shutdown();
        }

        private void ChangeService()
        {
            List<RequestPayment> requestPaymentList = PaymentDB.GetADSLChangeServiceRequest();

            foreach (RequestPayment currentPayment in requestPaymentList)
            {
                DateTime time = DateTime.Now;
                string format = "yyyyMMdd";
                string Localdate = time.ToString(format);
                format = "HHmmss";
                string localtime = time.ToString(format);

                Mellat_BillPaymentGateway.BillPaymentGatewayImplService mp = new Mellat_BillPaymentGateway.BillPaymentGatewayImplService();
                string resultBank = mp.bpBillInquiryRequest(1127363, "semnanmo", "se67mn", Convert.ToInt64(currentPayment.BillID), Convert.ToInt64(currentPayment.PaymentID), Localdate, localtime, "info@tcsem.ir");

                if (string.Equals(resultBank, "51"))
                {
                    Request request = PaymentDB.GertRequestbyID(currentPayment.RequestID);
                    ADSLChangeService aDSLChangeServiceRequest = PaymentDB.GertADSLChangeServiceRequestbyID(currentPayment.RequestID);
                    ADSL aDSL = PaymentDB.GertADSLbyTelephoneNo((long)request.TelephoneNo);
                    ADSLService service = PaymentDB.GetADSLServicebyServiceID((int)aDSL.TariffID);
                    int? credit = PaymentDB.GetADSLServiceCreditbyServiceID((int)aDSLChangeServiceRequest.NewServiceID);
                    string insertDateString = Date.GetPersianDate(request.InsertDate, Date.DateStringType.Short);

                    if (aDSLChangeServiceRequest.IsIBSngUpdated != true)
                    {
                        if (currentPayment.IsPaid != true)
                        {
                            aDSLChangeServiceRequest.FinalUserID = 1;
                            aDSLChangeServiceRequest.FinalDate = DB.GetServerDate();
                            aDSLChangeServiceRequest.FinalComment = "";

                            aDSLChangeServiceRequest.Detach();
                            DB.Save(aDSLChangeServiceRequest);

                            request.EndDate = DB.GetServerDate();
                            Status status = DB.GetStatus(request.RequestTypeID, 11);
                            request.StatusID = status.ID;

                            request.Detach();
                            DB.Save(request);

                            currentPayment.PaymentWay = 3;
                            currentPayment.BankID = 3;
                            currentPayment.FicheNunmber = ""; ;
                            currentPayment.FicheDate = DB.GetServerDate();
                            currentPayment.PaymentDate = DB.GetServerDate();
                            currentPayment.IsPaid = true;

                            currentPayment.Detach();
                            DB.Save(currentPayment);
                        }

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

                        string reewNextGroup = string.Empty;

                        try
                        {
                            reewNextGroup = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["renew_next_group"]);
                        }
                        catch (Exception ex)
                        {
                            reewNextGroup = string.Empty;
                        }

                        if (string.IsNullOrWhiteSpace(reewNextGroup))
                        {
                            DateTime startDate = DB.GetServerDate();
                            string startDateString = Date.GetPersianDate(startDate, Date.DateStringType.Short);
                            string[] startDateStringList = startDateString.Split('/');

                            if (startDateStringList[1].Count() < 2)
                                startDateStringList[1] = "0" + startDateStringList[1];

                            if (startDateStringList[2].Count() < 2)
                                startDateStringList[2] = "0" + startDateStringList[2];

                            startDateString = startDateStringList[0] + "/" + startDateStringList[1] + "/" + startDateStringList[2];

                            DateTime endDate = DB.GetServerDate().AddDays(1);
                            string endDateString = Date.GetPersianDate(endDate, Date.DateStringType.Short);
                            string[] endDateStringList = endDateString.Split('/');

                            if (endDateStringList[1].Count() < 2)
                                endDateStringList[1] = "0" + endDateStringList[1];

                            if (endDateStringList[2].Count() < 2)
                                endDateStringList[2] = "0" + endDateStringList[2];

                            endDateString = endDateStringList[0] + "/" + endDateStringList[1] + "/" + endDateStringList[2];

                            XmlRpcStruct arguments = new XmlRpcStruct();
                            XmlRpcStruct conds = new XmlRpcStruct();

                            arguments.Add("auth_name", "pendar");
                            arguments.Add("auth_pass", "Pendar#!$^");
                            arguments.Add("auth_type", "ADMIN");

                            conds.Add("change_time_from", startDateString);
                            conds.Add("change_time_from_unit", "jalali");
                            conds.Add("change_time_to", endDateString);
                            conds.Add("change_time_to_unit", "jalali");

                            conds.Add("user_ids", aDSL.UserID);
                            arguments.Add("conds", conds);

                            arguments.Add("from", 0);
                            arguments.Add("to", 1000);
                            arguments.Add("sort_by", "change_time");
                            arguments.Add("desc", true);

                            XmlRpcStruct result = ibsngService.GetUserAuditLogs(arguments);

                            object[] report = (object[])result["report"];
                            bool isUpdate = false;

                            if (report.Count() != 0)
                            {
                                foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                                {
                                    DateTime resultDate = Convert.ToDateTime(item["change_time_formatted"].ToString());
                                    string resultDateString = Date.GetPersianDate(resultDate, Date.DateStringType.Short);

                                    if (resultDateString.Split('/')[2] == insertDateString.Split('/')[2] && item["object_id"].ToString() == aDSL.UserID)
                                    {
                                        isUpdate = true;
                                        aDSLChangeServiceRequest.IsIBSngUpdated = true;
                                        break;
                                    }
                                }
                            }
                            if (isUpdate == false)
                            {
                                userAuthentication.Clear();

                                userAuthentication.Add("auth_name", "pendar");
                                userAuthentication.Add("auth_pass", "Pendar#!$^");
                                userAuthentication.Add("auth_type", "ADMIN");

                                userAuthentication.Add("user_id", aDSL.UserID);

                                userAuthentication.Add("deposit", credit.ToString());
                                userAuthentication.Add("is_absolute_change", false);
                                userAuthentication.Add("deposit_type", "renew");
                                userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

                                try
                                {
                                    ibsngService.changeDeposit(userAuthentication);
                                }
                                catch (Exception ex1)
                                {
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
                                catch (Exception ex2)
                                {
                                }

                                aDSL.TariffID = service.ID;
                                aDSL.InstallDate = DB.GetServerDate();
                                if (service.DurationID != null)
                                    aDSL.ExpDate = DB.GetServerDate().AddMonths((int)service.DurationID);

                                aDSL.Detach();
                                DB.Save(aDSL);
                            }
                        }

                        aDSLChangeServiceRequest.Detach();
                        DB.Save(aDSLChangeServiceRequest);
                    }
                }
            }
        }

        private void SellTrafic()
        {
            List<RequestPayment> requestPaymentList = PaymentDB.GetADSLSellTrafficRequest();

            foreach (RequestPayment currentPayment in requestPaymentList)
            {
                DateTime time = DateTime.Now;
                string format = "yyyyMMdd";
                string Localdate = time.ToString(format);
                format = "HHmmss";
                string localtime = time.ToString(format);

                Mellat_BillPaymentGateway.BillPaymentGatewayImplService mp = new Mellat_BillPaymentGateway.BillPaymentGatewayImplService();
                string resultBank = mp.bpBillInquiryRequest(1127363, "semnanmo", "se67mn", Convert.ToInt64(currentPayment.BillID), Convert.ToInt64(currentPayment.PaymentID), Localdate, localtime, "info@tcsem.ir");

                if (string.Equals(resultBank, "51"))
                {
                    Request request = PaymentDB.GertRequestbyID(currentPayment.RequestID);
                    ADSLSellTraffic aDSLSellTrafficRequest = PaymentDB.GertADSLSellTrafficRequestbyID(currentPayment.RequestID);
                    ADSL aDSL = PaymentDB.GertADSLbyTelephoneNo((long)request.TelephoneNo);
                    int? credit = PaymentDB.GetADSLServiceCreditbyServiceID((int)aDSLSellTrafficRequest.AdditionalServiceID);
                    string insertDateString = Date.GetPersianDate(request.InsertDate, Date.DateStringType.Short);

                    if (aDSLSellTrafficRequest.IsIBSngUpdated != true)
                    {
                        if (currentPayment.IsPaid == false)
                        {
                            aDSLSellTrafficRequest.FinalUserID = 1;
                            aDSLSellTrafficRequest.FinalDate = DB.GetServerDate();
                            aDSLSellTrafficRequest.FinalComment = "";

                            aDSLSellTrafficRequest.Detach();
                            DB.Save(aDSLSellTrafficRequest);

                            request.EndDate = DB.GetServerDate();
                            Status status = DB.GetStatus(request.RequestTypeID, 11);
                            request.StatusID = status.ID;

                            request.Detach();
                            DB.Save(request);

                            currentPayment.PaymentWay = 3;
                            currentPayment.BankID = 3;
                            currentPayment.FicheNunmber = ""; ;
                            currentPayment.FicheDate = DB.GetServerDate();
                            currentPayment.PaymentDate = DB.GetServerDate();
                            currentPayment.IsPaid = true;

                            currentPayment.Detach();
                            DB.Save(currentPayment);
                        }

                        DateTime startDate = DB.GetServerDate();
                        string startDateString = Date.GetPersianDate(startDate, Date.DateStringType.Short);
                        string[] startDateStringList = startDateString.Split('/');

                        if (startDateStringList[1].Count() < 2)
                            startDateStringList[1] = "0" + startDateStringList[1];

                        if (startDateStringList[2].Count() < 2)
                            startDateStringList[2] = "0" + startDateStringList[2];

                        startDateString = startDateStringList[0] + "/" + startDateStringList[1] + "/" + startDateStringList[2];

                        DateTime endDate = DB.GetServerDate().AddDays(1);
                        string endDateString = Date.GetPersianDate(endDate, Date.DateStringType.Short);
                        string[] endDateStringList = endDateString.Split('/');

                        if (endDateStringList[1].Count() < 2)
                            endDateStringList[1] = "0" + endDateStringList[1];

                        if (endDateStringList[2].Count() < 2)
                            endDateStringList[2] = "0" + endDateStringList[2];

                        endDateString = endDateStringList[0] + "/" + endDateStringList[1] + "/" + endDateStringList[2];

                        IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

                        XmlRpcStruct arguments = new XmlRpcStruct();
                        XmlRpcStruct conds = new XmlRpcStruct();

                        arguments.Add("auth_name", "pendar");
                        arguments.Add("auth_pass", "Pendar#!$^");
                        arguments.Add("auth_type", "ADMIN");

                        conds.Add("change_time_from", startDateString);
                        conds.Add("change_time_from_unit", "jalali");
                        conds.Add("change_time_to", endDateString);
                        conds.Add("change_time_to_unit", "jalali");

                        conds.Add("user_ids", aDSL.UserID);
                        arguments.Add("conds", conds);

                        arguments.Add("from", 0);
                        arguments.Add("to", 30);
                        arguments.Add("sort_by", "change_time");
                        arguments.Add("desc", true);

                        XmlRpcStruct result = null;

                        try
                        {
                            result = ibsngService.GetUserDepositChanges(arguments);
                        }
                        catch (Exception ex)
                        {
                        }
                        object[] report = (object[])result["report"];
                        bool isUpdate = false;

                        if (report.Count() != 0)
                        {
                            foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                            {
                                DateTime resultDate = Convert.ToDateTime(item["change_time_formatted"].ToString());
                                string resultDateString = Date.GetPersianDate(resultDate, Date.DateStringType.Short);

                                if (resultDateString.Split('/')[2] == insertDateString.Split('/')[2] && item["user_id"].ToString() == aDSL.UserID && item["deposit_change"].ToString() == credit.ToString())
                                {
                                    isUpdate = true;
                                    aDSLSellTrafficRequest.IsIBSngUpdated = true;
                                    break;
                                }
                            }
                        }
                        if (isUpdate == false)
                        {
                            XmlRpcStruct userAuthentication = new XmlRpcStruct();

                            userAuthentication.Clear();

                            userAuthentication.Add("user_id", aDSL.UserID);
                            userAuthentication.Add("deposit", credit.ToString());
                            userAuthentication.Add("is_absolute_change", false);
                            userAuthentication.Add("deposit_type", "recharge");
                            userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Sell Traffice Request (Recharge)");

                            userAuthentication.Add("auth_name", "pendar");
                            userAuthentication.Add("auth_pass", "Pendar#!$^");
                            userAuthentication.Add("auth_type", "ADMIN");

                            try
                            {
                                ibsngService.changeDeposit(userAuthentication);
                                aDSLSellTrafficRequest.IsIBSngUpdated = true;
                            }
                            catch (Exception ex)
                            { }
                        }

                        aDSLSellTrafficRequest.Detach();
                        DB.Save(aDSLSellTrafficRequest);
                    }
                }

            }
        }

        private static string ToStringSpecial(object value)
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
    }
}
