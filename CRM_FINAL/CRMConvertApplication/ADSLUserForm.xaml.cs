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
    public partial class ADSLUserForm : Window
    {
        public ADSLUserForm()
        {
            InitializeComponent();
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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object[] ids = new object[1];

                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();

                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");

                conds.Add("normal_username", "2313388246");
                arguments.Add("conds", conds);
                arguments.Add("from", 0);
                arguments.Add("to", 34000);
                arguments.Add("sort_by", "change_time");
                arguments.Add("desc", true);
                arguments.Add("order_by", "change_time");

                ids = ibsngService.SearchUser(arguments);

                int[] userIDs = (int[])ids[2];

                foreach (object currentUserID in userIDs)
                {
                    XmlRpcStruct userAuthentication = new XmlRpcStruct();
                    XmlRpcStruct userInfos = new XmlRpcStruct();
                    XmlRpcStruct userInfo = new XmlRpcStruct();

                    ADSL1 aDSL = new ADSL1();

                    userAuthentication.Clear();
                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    try
                    {
                        userAuthentication.Add("user_id", currentUserID.ToString());
                        userInfos = ibsngService.GetUserInfo(userAuthentication);
                    }
                    catch (Exception ex)
                    {

                    }

                    foreach (DictionaryEntry User in userInfos)
                    {
                        userInfo = (XmlRpcStruct)User.Value;
                    }

                    string telephoneNo = "";
                    try
                    {
                        telephoneNo = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                    }
                    catch (Exception ex)
                    {

                    }

                    if (!string.IsNullOrWhiteSpace(telephoneNo))
                    {
                        try
                        {
                            aDSL.TelephoneNo = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                        }
                        catch (Exception ex)
                        {
                            aDSL.TelephoneNo = string.Empty;
                        }

                        try
                        {
                            aDSL.UserID = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
                        }
                        catch (Exception ex)
                        {
                            aDSL.UserID = string.Empty;
                        }

                        try
                        {
                            aDSL.UserName = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
                        }
                        catch (Exception ex)
                        {
                            aDSL.UserName = string.Empty;
                        }

                        try
                        {
                            aDSL.AAAPassword = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);
                        }
                        catch (Exception ex)
                        {
                            aDSL.AAAPassword = string.Empty;
                        }

                        try
                        {
                            aDSL.AAAPassword = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);
                        }
                        catch (Exception ex)
                        {
                            aDSL.AAAPassword = string.Empty;
                        }

                        try
                        {
                            string iBSngGroupName = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["group_name"]);

                            if (!string.IsNullOrWhiteSpace(iBSngGroupName))
                            {
                                ADSLService service = ADSLServiceDB.GetADSLServiecNamebyIBSngName(iBSngGroupName);

                                if (service != null)
                                    aDSL.TariffID = service.ID;
                            }
                        }
                        catch (Exception ex)
                        {
                            aDSL.TariffID = null;
                        }

                        try
                        {
                            string creationDate = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["creation_date"]);

                            if (!string.IsNullOrWhiteSpace(creationDate))
                                aDSL.InsertDate = Convert.ToDateTime(creationDate);
                        }
                        catch (Exception ex)
                        {
                            aDSL.InsertDate = null;
                        }

                        try
                        {
                            string installDate = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["first_login"]);

                            if (!string.IsNullOrWhiteSpace(installDate))
                                aDSL.InstallDate = Convert.ToDateTime(installDate);
                        }
                        catch (Exception ex)
                        {
                            aDSL.InstallDate = null;
                        }

                        try
                        {
                            string expDate = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["nearest_exp_date"]);

                            if (!string.IsNullOrWhiteSpace(expDate))
                                aDSL.ExpDate = Convert.ToDateTime(expDate);
                        }
                        catch (Exception ex)
                        {
                            aDSL.ExpDate = null;
                        }

                        try
                        {
                            string groupIPStatic = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["assign_route_ip"]);

                            if (!string.IsNullOrWhiteSpace(groupIPStatic))
                            {
                                ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIP(groupIPStatic);
                                if (groupIP != null)
                                {
                                    aDSL.HasIP = true;
                                    aDSL.GroupIPStaticID = groupIP.ID;

                                    groupIP.TelephoneNo = Convert.ToInt64(telephoneNo);
                                    groupIP.InstallDate = aDSL.InstallDate;
                                    groupIP.ExpDate = aDSL.ExpDate;

                                    groupIP.Detach();
                                    DB.Save(groupIP);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            aDSL.HasIP = null;
                            aDSL.GroupIPStaticID = null;
                        }

                        try
                        {
                            if (aDSL.GroupIPStaticID == null)
                            {
                                string iPStatic = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["assign_ip"]);

                                if (!string.IsNullOrWhiteSpace(iPStatic))
                                {
                                    ADSLIP iP = ADSLIPDB.GetADSLIP(iPStatic);
                                    if (iP != null)
                                    {
                                        aDSL.HasIP = true;
                                        aDSL.IPStaticID = iP.ID;

                                        iP.TelephoneNo = Convert.ToInt64(telephoneNo);
                                        iP.InstallDate = aDSL.InstallDate;
                                        iP.ExpDate = aDSL.ExpDate;

                                        iP.Detach();
                                        DB.Save(iP);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            aDSL.HasIP = null;
                            aDSL.IPStaticID = null;
                        }

                        aDSL.Detach();
                        DB.Save(aDSL);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ReStartButton_Click(object sender, RoutedEventArgs e)
        {
            List<ADSL> aDSLList = ADSLDB.GetADSLList();

            int count = 0;
            int countSave = 0;

            foreach (ADSL currentADSL in aDSLList)
            {
                //ADSL currentADSL = ADSLDB.GetADSLbyTelephoneNo(2313349163);

                bool isSave = false;

                if (!string.IsNullOrWhiteSpace(currentADSL.UserID))
                {
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
                        userAuthentication.Add("user_id", currentADSL.UserID.ToString());
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
                        string creationDate = ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["creation_date"]);

                        if (!string.IsNullOrWhiteSpace(creationDate))
                        {
                            if (currentADSL.InsertDate != null)
                            {
                                if (currentADSL.InsertDate.Value.Day != Convert.ToDateTime(creationDate).Day || currentADSL.InsertDate.Value.Month != Convert.ToDateTime(creationDate).Month || currentADSL.InsertDate.Value.Year != Convert.ToDateTime(creationDate).Year)
                                {
                                    currentADSL.InsertDate = Convert.ToDateTime(creationDate);
                                    isSave = true;
                                }
                            }
                            else
                            {
                                currentADSL.InsertDate = Convert.ToDateTime(creationDate);
                                isSave = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        currentADSL.InsertDate = null;
                    }

                    try
                    {
                        string installDate = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["first_login"]);

                        if (!string.IsNullOrWhiteSpace(installDate))
                        {
                            if (currentADSL.InstallDate != null)
                            {
                                if (currentADSL.InstallDate.Value.Day != Convert.ToDateTime(installDate).Day || currentADSL.InstallDate.Value.Month != Convert.ToDateTime(installDate).Month || currentADSL.InstallDate.Value.Year != Convert.ToDateTime(installDate).Year)
                                {
                                    currentADSL.InstallDate = Convert.ToDateTime(installDate);
                                    isSave = true;
                                }
                            }
                            else
                            {
                                currentADSL.InstallDate = Convert.ToDateTime(installDate);
                                isSave = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        currentADSL.InstallDate = null;
                    }

                    try
                    {
                        string expDate = ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["nearest_exp_date"]);

                        if (!string.IsNullOrWhiteSpace(expDate))
                        {
                            if (currentADSL.ExpDate != null)
                            {
                                if (currentADSL.ExpDate.Value.Day != Convert.ToDateTime(expDate).Day || currentADSL.ExpDate.Value.Month != Convert.ToDateTime(expDate).Month || currentADSL.ExpDate.Value.Year != Convert.ToDateTime(expDate).Year)
                                {
                                    currentADSL.ExpDate = Convert.ToDateTime(expDate);
                                    isSave = true;
                                }
                            }
                            else
                            {
                                currentADSL.ExpDate = Convert.ToDateTime(expDate);
                                isSave = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        currentADSL.ExpDate = null;
                    }

                    if (isSave)
                    {
                        currentADSL.Detach();
                        DB.Save(currentADSL);

                        countSave = countSave + 1;
                    }

                    count = count + 1;
                }
            }

            CountLabel.Content = CountLabel.Content + count.ToString();
            CountSaveLabel.Content = CountSaveLabel.Content + countSave.ToString();
        }
    }
}
