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

namespace CRMConvertApplication
{
    public partial class IPExpired : Window
    {
        public IPExpired()
        {
            InitializeComponent();

            DeleteSingleIP();
            DeleteGroupIP();
            

            Application.Current.Shutdown();
        }

        private void DeleteSingleIP()
        {
            try
            {
                List<ADSLIP> iPList = ADSLIPDB.GetADSLIPExpired();
                int iPListCount = 0;
                //List<ADSLIP> iPList = ADSLIPDB.GetADSLIP30DaysExprire();

                foreach (ADSLIP currentIP in iPList)
                {
                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("normal_username", currentIP.TelephoneNo.ToString());
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

                    string assignIP = string.Empty;
                    string userID = string.Empty;
                    try
                    {
                        assignIP = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["assign_ip"]);
                        userID = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                    }
                    catch (Exception)
                    {

                    }

                    if (!string.IsNullOrWhiteSpace(assignIP))
                    {
                        XmlRpcStruct list = new XmlRpcStruct();
                        list.Add("to_del_attrs", "assign_ip");

                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        userAuthentication.Add("user_id", userID);
                        userAuthentication.Add("attrs", userInfo);
                        userAuthentication.Add("to_del_attrs", list);

                        ibsngService.UpdateUserAttrs(userAuthentication);
                    }

                    currentIP.Status = 5;

                    currentIP.Detach();
                    DB.Save(currentIP);

                    ADSLIPHistory history = ADSLIPDB.GetHistorybyTelephoneNoandIP((long)currentIP.TelephoneNo, currentIP.ID);

                    if (history != null)
                    {
                        history.EndDate = currentIP.ExpDate;

                        history.Detach();
                        DB.Save(history);
                    }
                    else
                    {
                        history = new ADSLIPHistory();

                        history.TelephoneNo = (long)currentIP.TelephoneNo;
                        history.IPID = currentIP.ID;
                        history.StartDate = currentIP.InstallDate;
                        history.EndDate = currentIP.ExpDate;

                        history.Detach();
                        DB.Save(history, true);
                    }

                    iPListCount = iPListCount + 1;
                }

                ADSLAgentLog agentLog = new ADSLAgentLog();

                agentLog.Count = iPListCount;
                agentLog.AgentID = 13;
                agentLog.InsertDate = DB.GetServerDate();

                agentLog.Detach();
                DB.Save(agentLog);

                List<ADSLIP> iPEXPList = ADSLIPDB.GetADSLIPExpired20();
                int iPEXPListCount = 0;

                foreach (ADSLIP currentIP in iPEXPList)
                {
                    if (ADSLIPDB.HasHistorybyTelephoneNoandIP((long)currentIP.TelephoneNo, currentIP.ID) == false)
                    {
                        ADSLIPHistory history = new ADSLIPHistory();

                        history.TelephoneNo = (long)currentIP.TelephoneNo;
                        history.IPID = currentIP.ID;
                        history.StartDate = currentIP.InstallDate;
                        history.EndDate = currentIP.ExpDate;

                        history.Detach();
                        DB.Save(history, true);
                    }

                    ADSL aDSL = ADSLDB.GetADSLbyTelephoneNo((long)currentIP.TelephoneNo);

                    if (aDSL != null)
                    {
                        aDSL.IPStaticID = null;

                        aDSL.Detach();
                        DB.Save(aDSL);
                    }

                    currentIP.Status = 0;
                    currentIP.TelephoneNo = null;
                    currentIP.InstallDate = null;
                    currentIP.ExpDate = null;

                    currentIP.Detach();
                    DB.Save(currentIP);

                    iPEXPListCount = iPEXPListCount + 1;
                }

                ADSLAgentLog agentLog1 = new ADSLAgentLog();

                agentLog1.Count = iPEXPListCount;
                agentLog1.AgentID = 14;
                agentLog1.InsertDate = DB.GetServerDate();

                agentLog1.Detach();
                DB.Save(agentLog1);
            }
            catch (Exception ex)
            {
            }
        }

        private void DeleteGroupIP()
        {
            try
            {
                List<ADSLGroupIP> groupIPList = ADSLIPDB.GetADSLGroupIPExpired();
                int iPListCount = 0;
                //List<ADSLGroupIP> groupIPList = ADSLIPDB.GetADSLIP30DaysExprire();

                foreach (ADSLGroupIP currentIP in groupIPList)
                {
                    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("normal_username", currentIP.TelephoneNo.ToString());
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

                    string assignIP = string.Empty;
                    string userID = string.Empty;
                    try
                    {
                        assignIP = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["assign_ip"]);
                        userID = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                    }
                    catch (Exception)
                    {

                    }

                    if (!string.IsNullOrWhiteSpace(assignIP))
                    {
                        XmlRpcStruct list = new XmlRpcStruct();
                        list.Add("to_del_attrs", "assign_ip");
                        
                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        userAuthentication.Add("user_id", userID);
                        userAuthentication.Add("attrs", userInfo);
                        userAuthentication.Add("to_del_attrs", list);

                        ibsngService.UpdateUserAttrs(userAuthentication);

                        XmlRpcStruct list1 = new XmlRpcStruct();
                        list1.Add("to_del_attrs", "assign_route_ip");

                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        userAuthentication.Add("user_id", userID);
                        userAuthentication.Add("attrs", userInfo);
                        userAuthentication.Add("to_del_attrs", list1);

                        ibsngService.UpdateUserAttrs(userAuthentication);
                    }

                    currentIP.Status = 5;

                    currentIP.Detach();
                    DB.Save(currentIP);

                    ADSLIPHistory history = ADSLIPDB.GetHistorybyTelephoneNoandGroupIP((long)currentIP.TelephoneNo, currentIP.ID);

                    if (history != null)
                    {
                        history.EndDate = currentIP.ExpDate;

                        history.Detach();
                        DB.Save(history);
                    }
                    else
                    {
                        history = new ADSLIPHistory();
                        
                        history.TelephoneNo = (long)currentIP.TelephoneNo;
                        history.IPGroupID = currentIP.ID;
                        history.StartDate = currentIP.InstallDate;
                        history.EndDate = currentIP.ExpDate;

                        history.Detach();
                        DB.Save(history, true);
                    }

                    iPListCount = iPListCount + 1;
                }

                ADSLAgentLog agentLog = new ADSLAgentLog();

                agentLog.Count = iPListCount;
                agentLog.AgentID = 15;
                agentLog.InsertDate = DB.GetServerDate();

                agentLog.Detach();
                DB.Save(agentLog);

                List<ADSLGroupIP> iPEXPList = ADSLIPDB.GetADSLGroupIPExpired20();
                int iPEXPListCount = 0;

                foreach (ADSLGroupIP currentIP in iPEXPList)
                {
                    if (ADSLIPDB.HasHistorybyTelephoneNoandGroupIP((long)currentIP.TelephoneNo, currentIP.ID) == false)
                    {
                        ADSLIPHistory history = new ADSLIPHistory();

                        history.TelephoneNo = (long)currentIP.TelephoneNo;
                        history.IPGroupID = currentIP.ID;
                        history.StartDate = currentIP.InstallDate;
                        history.EndDate = currentIP.ExpDate;

                        history.Detach();
                        DB.Save(history, true);
                    }

                    ADSL aDSL = ADSLDB.GetADSLbyTelephoneNo((long)currentIP.TelephoneNo);

                    if (aDSL != null)
                    {
                        aDSL.IPStaticID = null;

                        aDSL.Detach();
                        DB.Save(aDSL);
                    }

                    currentIP.Status = 0;
                    currentIP.TelephoneNo = null;
                    currentIP.InstallDate = null;
                    currentIP.ExpDate = null;

                    currentIP.Detach();
                    DB.Save(currentIP);

                    iPEXPListCount = iPEXPListCount + 1;
                }

                ADSLAgentLog agentLog1 = new ADSLAgentLog();

                agentLog1.Count = iPEXPListCount;
                agentLog1.AgentID = 16;
                agentLog1.InsertDate = DB.GetServerDate();

                agentLog1.Detach();
                DB.Save(agentLog1);
            }
            catch (Exception ex)
            {
            }
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
    }
}
