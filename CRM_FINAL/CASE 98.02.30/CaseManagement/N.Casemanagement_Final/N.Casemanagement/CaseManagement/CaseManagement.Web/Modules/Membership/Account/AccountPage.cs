﻿
namespace CaseManagement.Membership.Pages
{
    using CaseManagement.Case;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Linq;
    using System.Net;
    using System.Web.UI;
    using Serenity.Web;


    [RoutePrefix("Account"), Route("{action=index}")]
    public partial class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string activated)
        {
            ViewData["Activated"] = activated;
            ViewData["HideLeftNavigation"] = true;
            return View(MVC.Views.Membership.Account.AccountLogin);
        }

        [HttpPost, JsonFilter]
        public Result<ServiceResponse> Login(LoginRequest request)
        {
            return this.ExecuteMethod(() =>
            {
                request.CheckNotNull();

                if (string.IsNullOrEmpty(request.Username))
                    throw new ArgumentNullException("username");

                var username = request.Username;

                if (WebSecurityHelper.Authenticate(ref username, request.Password, false))
                {
                    return new ServiceResponse();
                }
                throw new ValidationError("AuthenticationError", Texts.Validation.AuthenticationError);
            });
        }

        private ActionResult Error(string message)
        {
            return View(MVC.Views.Errors.ValidationError,
                new ValidationError(Texts.Validation.InvalidResetToken));
        }

        public ActionResult Signout()
        {
            using (var connection = SqlConnections.NewFor<Administration.Entities.UserRow>())
            {
                Helper.SaveLog("خروج", "کاربر", int.Parse(Serenity.Authorization.UserId), Serenity.Authorization.UserDefinition.DisplayName, "", connection, Administration.ActionLog.Logout);
            }

            Session.Abandon();
            FormsAuthentication.SignOut();
            return new RedirectResult("~/");
        }


        [HttpPost]
        public void ClientIP(string publicIP, string machineIP, string username, string password)
        {
            if (WebSecurityHelper.Authenticate(ref username, password, false))
            {
                try
                {
                    string mobileNo = string.Empty;
                    
                    string ip = "";
                    
                    if (machineIP != "0" && publicIP != "0")
                        ip = "Local IP: " + machineIP + ", Public IP: " + publicIP;
                    else
                        ip = String.Empty; 
                     
                    using (var connection = SqlConnections.NewFor<Administration.Entities.UserRow>())
                    {
                        mobileNo = connection.List<Administration.Entities.UserRow>().Where(t => t.Username == username).Select(t => t.MobileNo).SingleOrDefault();
                        Administration.Entities.UserRow user = connection.List<Administration.Entities.UserRow>().Where(t => t.Username == username).SingleOrDefault();
                        user.LastLoginDate = DateTime.Now;
                        connection.UpdateById<Administration.Entities.UserRow>(user);

                        
                        Helper.SaveLog("ورود", "کاربر", (int)user.UserId, user.DisplayName, ip, connection, Administration.ActionLog.Login);
                    }

                    System.Net.ServicePointManager.Expect100Continue = false;
                    SMSService sms = new SMSService();
                    string[] senderNumbers = { "60020184040" };

                    if (!string.IsNullOrEmpty(mobileNo))
                    {
                        string[] recipientNumbers = { mobileNo };
                        string[] date = { DateTime.Now.ToString() };
                        long[] ids = { 1 };
                        int[] messageClasses = { 1 };
                        //string ip = GetIPAddress();
                        string now = Helper.GetPersianDate(DateTime.Now, Helper.DateStringType.DateTime);

                        if (!String.IsNullOrEmpty(ip))
                        {
                            string[] body = { " شما در تاریخ " + now + " با  IP زیر وارد سیستم تضمین درآمد شدید.\n" + "Local IP : " + machineIP + "\n" + "Public IP : " + publicIP };
                            long[] sample = sms.SendSMS("tazmin", "mpsms7521", senderNumbers, recipientNumbers, body, null, null, null);
                        }else
                        {
                            string[] body = { " شما در تاریخ " + now + " وارد سیستم تضمین درآمد شدید.\n"  };
                            long[] sample = sms.SendSMS("tazmin", "mpsms7521", senderNumbers, recipientNumbers, body, null, null, null);
                        }


                        
                    }
                }
                catch (Exception ex)
                {
                    // throw new Exception(ex.Message);
                }
            }
        }

    }
}