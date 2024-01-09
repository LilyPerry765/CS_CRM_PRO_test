using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Web.Security;
using System.Net;
using System.Net.Sockets;
using Enterprise;

namespace CRM.Website.Viewes
{
    public partial class LoginForm : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            UsernameTextBox.Focus();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            bool authenticated = SecurityDB.WebAuthentication(UsernameTextBox.Text, PasswordTextBox.Text);
            if (authenticated)
            {
                DB.IsInWebSiteMode = true;
                DB.InitializeUserInfo(UsernameTextBox.Text);
                Enterprise.Logger.WriteImportant("CRM Application: User initialization completed...");
                Logger.WriteInfo("Folder.User.Current ..." + DB.CurrentUser.UserName);

                //if (PersistCheckBox.Checked)
                //{
                //    HttpCookie usernameCookie = new HttpCookie("Username");
                //    usernameCookie.Value = DB.CurrentUser.UserName;
                //    usernameCookie.Expires = DateTime.Now.AddDays(30);
                //    Response.Cookies.Add(usernameCookie);
                //}

               // FormsAuthentication.RedirectFromLoginPage(UsernameTextBox.Text, PersistCheckBox.Checked);

                FormsAuthentication.RedirectFromLoginPage(UsernameTextBox.Text, false);
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                string localIP = "";
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        localIP = ip.ToString();
                }

                Data.Schema.LoginLog loginLog = new Data.Schema.LoginLog();
                loginLog.IPAddress = localIP;
                Data.ActionLogDB.AddLoginLog((byte)DB.ActionLog.Login, DB.CurrentUser.UserName , loginLog);
                Response.Redirect("MainForm.aspx");
            }
            else
            {
                ErrorLabel.Visible = true;
            }
        }

        #endregion
    }
}