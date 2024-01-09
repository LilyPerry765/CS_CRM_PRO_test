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
using System.Security.Cryptography;

namespace CRMConvertApplication
{
    public partial class EndSupportStep : Window
    {
        public EndSupportStep()
        {
            InitializeComponent();

            List<Request> requestList = ADSLRequestDB.GetADSLRequestinSupportStep();            

            ADSLRequest aDSLRequest = new ADSLRequest();
            RequestLog requestLog = new RequestLog();
            int adslFinishedRequestCount = 0;

            if (requestList != null && requestList.Count != 0)
            {
                foreach (Request currentRequest in requestList)
                {
                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("normal_username", currentRequest.TelephoneNo.ToString());
                    try
                    {
                        userInfos = ibsngService.GetUserInfo(userAuthentication);                        
                    }
                    catch (Exception ex)
                    {
                    }

                    foreach (DictionaryEntry User in userInfos)
                    {
                        userInfo = (XmlRpcStruct)User.Value;                        
                    }

                    string realFirstLogin = string.Empty;

                    try
                    {
                        realFirstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["real_first_login"]);                        
                    }
                    catch (Exception)
                    {

                    }

                    if (!string.IsNullOrWhiteSpace(realFirstLogin))
                    {
                        string userName = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                        string password = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);

                        aDSLRequest = ADSLRequestDB.GetADSLRequestByID(currentRequest.ID);

                        DateTime realFirstLoginDate = Convert.ToDateTime(realFirstLogin);
                        currentRequest.EndDate = realFirstLoginDate;

                        aDSLRequest.InstallDate = realFirstLoginDate;
                        aDSLRequest.Status = true;

                        ADSL aDSL = ADSLDB.GetADSLbyTelephoneNo((long)currentRequest.TelephoneNo);

                        aDSL.UserID = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                        aDSL.InstallDate = realFirstLoginDate;
                        aDSL.UserName = userName;
                        aDSL.AAAPassword = password;
                        aDSL.OrginalPassword = password;
                        aDSL.HashPassword = GenerateMD5HashPassword(password);
                        aDSL.Detach();
                        DB.Save(aDSL);

                        List<int> nextStates = WorkFlowDB.GetListNextStatesID(1/*DB.Action.Confirm*/, currentRequest.StatusID);

                        if (nextStates.Count == 1)
                        {
                            currentRequest.StatusID = nextStates[0];
                            currentRequest.PreviousAction = 1 /*(byte)DB.Action.Confirm*/;
                            currentRequest.IsViewed = false;
                        }

                        RequestForADSL.SaveADSLRequest(currentRequest, aDSLRequest, false);

                        SOAPSendSMS.MessageRelayService SOAPSMS = new SOAPSendSMS.MessageRelayService();
                        string message = "";
                        string[] list = new string[1];

                        if (aDSL.CustomerOwnerID != null)
                        {
                            Customer customer = ADSLDB.GetCustomerbyID((long)aDSL.CustomerOwnerID);
                            string date = Date.GetPersianDate(realFirstLoginDate, Date.DateStringType.Short);                            

                            if (!string.IsNullOrWhiteSpace(customer.MobileNo))
                            {
                                message = "کاربر گرامی اولین ورود شما به سیستم در تاریخ " + date + " با موفقیت انجام شد." + Environment.NewLine + "شرکت مخابرات استان سمنان";

                                string mobileNo = customer.MobileNo;
                                if (mobileNo.StartsWith("0"))
                                    mobileNo = mobileNo.Remove(0, 1);
                                mobileNo = "98" + mobileNo;
                                list[0] = mobileNo;

                                try
                                {
                                    SOAPSMS.sendMessageOneToMany("tctsemnan", "6311256", "500042020", list, message);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }

                        adslFinishedRequestCount = adslFinishedRequestCount + 1;
                    }
                }

                ADSLAgentLog agentLog = new ADSLAgentLog();

                agentLog.Count = adslFinishedRequestCount;
                agentLog.AgentID = 8;
                agentLog.InsertDate = DB.GetServerDate();

                agentLog.Detach();
                DB.Save(agentLog);
            }
            
            List<Request> installRequestList = ADSLRequestDB.GetADSLInstallRequestinSupportStep();

            if (installRequestList != null && installRequestList.Count != 0)
            {
                foreach (Request currentRequest in installRequestList)
                {
                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");                    

                    userAuthentication.Add("normal_username", currentRequest.TelephoneNo.ToString());
                    try
                    {
                        userInfos = ibsngService.GetUserInfo(userAuthentication);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    foreach (DictionaryEntry User in userInfos)
                    {
                        userInfo = (XmlRpcStruct)User.Value;
                    }

                    string realFirstLogin = string.Empty;

                    try
                    {
                        realFirstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["real_first_login"]);
                    }
                    catch (Exception)
                    {

                    }

                    if (!string.IsNullOrWhiteSpace(realFirstLogin))
                    {
                        string userName = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                        string password = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);

                        DateTime realFirstLoginDate = Convert.ToDateTime(realFirstLogin);
                        currentRequest.EndDate = realFirstLoginDate;

                        ADSL aDSL = ADSLDB.GetADSLbyTelephoneNo((long)currentRequest.TelephoneNo);

                        aDSL.UserID = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                        aDSL.InstallDate = realFirstLoginDate;

                        aDSL.Detach();
                        DB.Save(aDSL);

                        List<int> nextStates = WorkFlowDB.GetListNextStatesID(1/*DB.Action.Confirm*/, currentRequest.StatusID);

                        if (nextStates.Count == 1)
                        {
                            currentRequest.StatusID = nextStates[0];
                            currentRequest.PreviousAction = 1 /*(byte)DB.Action.Confirm*/;
                            currentRequest.IsViewed = false;
                        }

                        RequestForADSL.SaveADSLRequest(currentRequest, null, false);
                    }
                }
            }

            adslFinishedRequestCount = 0;
              List<Request> wirelessRequestList = ADSLRequestDB.GetWirelessInstallRequestinSupportStep();

              if (wirelessRequestList != null && wirelessRequestList.Count != 0)
              {
                  foreach (Request currentRequest in wirelessRequestList)
                  {
                      WirelessRequest wirelessRequest = new WirelessRequest();
                      using (MainDataContext context = new MainDataContext())
                      {
                           wirelessRequest = context.WirelessRequests.Where(t => t.ID == currentRequest.ID).SingleOrDefault();
                      }

                      if (wirelessRequest != null && wirelessRequest.UserName != null)
                      {
                          IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                          XmlRpcStruct userAuthentication = new XmlRpcStruct();
                          XmlRpcStruct userInfos = new XmlRpcStruct();
                          XmlRpcStruct userInfo = new XmlRpcStruct();

                          userAuthentication.Clear();

                          userAuthentication.Add("auth_name", "pendar");
                          userAuthentication.Add("auth_pass", "Pendar#!$^");
                          userAuthentication.Add("auth_type", "ADMIN");

                          userAuthentication.Add("normal_username", wirelessRequest.UserName);
                          try
                          {
                              userInfos = ibsngService.GetUserInfo(userAuthentication);
                          }
                          catch (Exception ex)
                          {

                          }
                          foreach (DictionaryEntry User in userInfos)
                          {
                              userInfo = (XmlRpcStruct)User.Value;
                          }

                          string realFirstLogin = string.Empty;

                          try
                          {
                              realFirstLogin = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["real_first_login"]);
                          }
                          catch (Exception)
                          {

                          }

                          if (!string.IsNullOrWhiteSpace(realFirstLogin))
                          {
                              string userName = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                              string password = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);

                              DateTime realFirstLoginDate = Convert.ToDateTime(realFirstLogin);
                              currentRequest.EndDate = realFirstLoginDate;

                              using (MainDataContext context = new MainDataContext())
                              {
                                  wirelessRequest.UserID = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                                  wirelessRequest.InstallDate = realFirstLoginDate;
                                  context.SubmitChanges();

                              }

                              List<int> nextStates = WorkFlowDB.GetListNextStatesID(1/*DB.Action.Confirm*/, currentRequest.StatusID);

                              if (nextStates.Count == 1)
                              {
                                  currentRequest.StatusID = nextStates[0];
                                  currentRequest.PreviousAction = 1 /*(byte)DB.Action.Confirm*/;
                                  currentRequest.IsViewed = false;
                              }

                              RequestForADSL.SaveADSLRequest(currentRequest, null, false);


                              SOAPSendSMS.MessageRelayService SOAPSMS = new SOAPSendSMS.MessageRelayService();
                              string message = "";
                              string[] list = new string[1];

                              if (wirelessRequest.CustomerOwnerID != null)
                              {
                                  Customer customer = ADSLDB.GetCustomerbyID((long)wirelessRequest.CustomerOwnerID);
                                  string date = Date.GetPersianDate(realFirstLoginDate, Date.DateStringType.Short);

                                  if (!string.IsNullOrWhiteSpace(customer.MobileNo))
                                  {
                                      message = "کاربر گرامی اولین ورود شما به سیستم در تاریخ " + date + " با موفقیت انجام شد." + Environment.NewLine + "شرکت مخابرات استان سمنان";

                                      string mobileNo = customer.MobileNo;
                                      if (mobileNo.StartsWith("0"))
                                          mobileNo = mobileNo.Remove(0, 1);
                                      mobileNo = "98" + mobileNo;
                                      list[0] = mobileNo;

                                      try
                                      {
                                          SOAPSMS.sendMessageOneToMany("tctsemnan", "6311256", "500042020", list, message);
                                      }
                                      catch (Exception ex)
                                      {
                                      }
                                  }
                              }

                              adslFinishedRequestCount = adslFinishedRequestCount + 1;

                          }
                      }

                      ADSLAgentLog agentLog = new ADSLAgentLog();

                      agentLog.Count = adslFinishedRequestCount;
                      agentLog.AgentID = 8;
                      agentLog.InsertDate = DB.GetServerDate();

                      agentLog.Detach();
                      DB.Save(agentLog);
                  }
              }

            Application.Current.Shutdown();
        }

        public static string ToStringSpecial(object value)
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

        public string GenerateMD5HashPassword(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
