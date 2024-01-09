using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CRM.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CRM.Website
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                //string tempPath = System.IO.Path.GetTempPath();
                string tempPath = Server.MapPath("~/Font");
                string filename = System.IO.Path.Combine(tempPath, "Yekan.TTF");
                
                if (!System.IO.File.Exists(filename))
                {
                    Stream fontStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CRM.Website.Fonts.Yekan.TTF");
                    System.IO.File.WriteAllBytes(filename, StreamToByteArray(fontStream));
                }

                string fontDir = Environment.GetEnvironmentVariable("windir") + "\\Fonts\\Yekan.TTF";
                System.IO.File.Copy(filename, fontDir);
            }
            catch
            {
            }

            //HttpCookie userNameCookie = Request.Cookies["Username"];

            //if (userNameCookie != null)
            //{
            //    DB.IsInWebSiteMode = true;
            //    try
            //    {
            //        //may usernameCookie be contain an old username that is changed since last login,in this wise we have an exception 
            //        DB.InitializeUserInfo(userNameCookie.Value);
            //        Enterprise.Logger.WriteImportant("CRM Application: User initialization started...");

            //        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            //        string localIP = "";
            //        foreach (IPAddress ip in host.AddressList)
            //        {
            //            if (ip.AddressFamily == AddressFamily.InterNetwork)
            //                localIP = ip.ToString();
            //        }

            //        Data.Schema.LoginLog loginLog = new Data.Schema.LoginLog();
            //        loginLog.IPAddress = localIP;
            //        Data.ActionLogDB.AddLoginLog((byte)DB.ActionLog.Login, DB.CurrentUser.UserName, loginLog);

            //        //Response.Redirect("MainForm.aspx");
            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Redirect("LoginForm.aspx");
            //        Enterprise.Logger.Write(ex);
            //    }
            //}
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
           // HttpCookie userNameCookie = Request.Cookies["Username"];
            try
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
                Data.ActionLogDB.AddLogoutLog((byte)DB.ActionLog.Logout, DB.CurrentUser.UserName, logoutLog);
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "Exit Log did not insert successfully.");
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static byte[] StreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }
                return memoryStream.ToArray();
            }
        }
    }
}