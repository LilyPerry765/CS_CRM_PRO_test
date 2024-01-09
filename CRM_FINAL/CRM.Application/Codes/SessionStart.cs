using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Folder;
using System.Globalization;
using System.Threading;
using CRM.Data;
using System.Net;
using System.Net.Sockets;
using Enterprise;

namespace CRM.Application
{

    public class StartUp : PluginGlobal
    {
        public override void SessionStart()
        {
            Logger.WriteInfo("CRM.Application.SessionStart() is loading ...");

            DB.City = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
            DB.PersianCity = DB.GetPerainCityNameByEnglishCityName(DB.City);

            Logger.WriteInfo("City is : " + DB.City);

            Data.DB.InitializeUserInfo(Folder.User.Current);
            Logger.WriteInfo("Folder.User.Current ..." + DB.CurrentUser.UserName);

            Folder.Console.Navigate(new Views.UserDashboard());

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string localIP = "";
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    localIP = ip.ToString();
            }

            Data.Schema.LoginLog loginLog = new Data.Schema.LoginLog();
            loginLog.IPAddress = localIP;
            Data.ActionLogDB.AddLoginLog((byte)DB.ActionLog.Login, Folder.User.Current.Username, loginLog);

            Logger.WriteInfo("CRM.Application.SessionStart() is loaded .");
        }

        public override void SessionEnd()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string localIP = "";
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    localIP = ip.ToString();
            }

            Data.Schema.LogoutLog logoutLog = new Data.Schema.LogoutLog();
            logoutLog.IPAddress = localIP;
            Data.ActionLogDB.AddLogoutLog((byte)DB.ActionLog.Logout, Folder.User.Current.Username, logoutLog);
        }
    }
}
