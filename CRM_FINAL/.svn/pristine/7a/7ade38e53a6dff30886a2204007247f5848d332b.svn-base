using CRM.WebAPI.Models.Shahkar.CustomClasses;
using Enterprise;
using Newtonsoft.Json;
using PendarPajouhAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace PendarPajouhAPI.Controllers
{
    public class PendarPajouhCRMController : ApiController
    {
        [HttpPost]
        [ActionName("IranianPersonCustomerAuthentication")]
        public HttpResponseMessage SendIranianPersonCustomerAuthenticationToShahkar([FromBody]IranianAuthentication iranianCustomer)
        {
            string responseFromShahkar = string.Empty;
            string jsonOfShahkarResult = string.Empty;
            HttpResponseMessage response = new HttpResponseMessage();
            PendarWebApiResult shahkarResult = new PendarWebApiResult();
            
            try
            {
                //در اینجا باید شناسه مورد نظر شاهکار ایجاد شود
                iranianCustomer.requestId = HttpRequestHelpers.GenerateRequestId();

                //آیتم هایی که برای استفاده از سامانه شاهکار ضروری هستند
                string authorizationHeader = HttpRequestHelpers.GenerateBase64Authorization("TCI_Kermanshah", "T30!-Ke$m301an$$");

                //ارسال درخواست به سامانه شاهکار
                responseFromShahkar = HttpRequestHelpers.SendHttpWebRequest(iranianCustomer, "http://172.16.11.3:8083/rest/shahkar/estelaam", authorizationHeader);

                //پاسخ دریافتی از شاهکار را باید به سیستم خودمان برگردانیم
                shahkarResult.RawResultFromShahkar = responseFromShahkar;
                shahkarResult.SystemError = "";

                jsonOfShahkarResult = JsonConvert.SerializeObject(shahkarResult);
                response.Content = new StringContent(jsonOfShahkarResult, System.Text.Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.OK;

                this.Request.Properties["HasError"] = false;
            }
            catch (Exception ex)
            {
                //ثبت خطا در لاگر
                Logger.WriteError("PendarPajouhAPI => Following exception caught in {0}", this.ControllerContext.RouteData.Values["action"].ToString());
                Logger.WriteException("PendarPajouhAPI => {0}", ex.Message);

                //انتساب متن خطا
                shahkarResult.SystemError = ex.Message;
                shahkarResult.RawResultFromShahkar = responseFromShahkar;

                //آماده سازی نتیجه نهایی اکشن بر اساس خطای رخداده - این نتیجه برای کاربر اکشن ارسال میشود
                jsonOfShahkarResult = JsonConvert.SerializeObject(shahkarResult);
                response.Content = new StringContent(jsonOfShahkarResult, System.Text.Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.BadRequest;

                //چون بعد از اجرای اکشن خواه درست خواه همراه با خطا ، ما در دیتابیس لاگ میزنیم خط زیر را اضافه کردم تا بتوانم در دیتابیس بگویم که فلان درخواست با موفقیت تمام شده است یا خیر
                this.Request.Properties["HasError"] = true;
            }
            return response;
        }

        [HttpPost]
        [ActionName("IranianPersonCustomerInstallTelephone")]
        public HttpResponseMessage SendIranianPersonCustomerInstallTelephoneToShahkar([FromBody] IranianPersonCustomerInstallTelephone telephoneForInstall)
        {
            string responseFromShahkar = string.Empty;
            string jsonOfPendarWebApiResult = string.Empty;
            HttpResponseMessage response = new HttpResponseMessage();
            PendarWebApiResult pendarWebApiResult = new PendarWebApiResult();

            try
            {
                telephoneForInstall.requestId = HttpRequestHelpers.GenerateRequestId();

                string authorizationHeader = HttpRequestHelpers.GenerateBase64Authorization("TCI_Kermanshah", "T30!-Ke$m301an$$");

                //ارسال درخواست به شاهکار
                responseFromShahkar = HttpRequestHelpers.SendHttpWebRequest(telephoneForInstall, "http://172.16.11.3:8083/rest/shahkar/put", authorizationHeader);

                pendarWebApiResult.RawResultFromShahkar = responseFromShahkar;
                pendarWebApiResult.SystemError = "";

                jsonOfPendarWebApiResult = JsonConvert.SerializeObject(pendarWebApiResult);
                response.Content = new StringContent(jsonOfPendarWebApiResult, System.Text.Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.OK;
                this.Request.Properties["HasError"] = false;
            }
            catch (Exception ex)
            {
                Logger.WriteError("PendarPajouhAPI => Following exception caught in {0}", this.ControllerContext.RouteData.Values["action"].ToString());
                Logger.WriteException("PendarPajouhAPI => {0}", ex.Message);

                pendarWebApiResult.SystemError = ex.Message;
                pendarWebApiResult.RawResultFromShahkar = responseFromShahkar;

                jsonOfPendarWebApiResult = JsonConvert.SerializeObject(pendarWebApiResult);
                response.Content = new StringContent(jsonOfPendarWebApiResult, System.Text.Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.BadRequest;

                this.Request.Properties["HasError"] = true;
            }
            return response;
        }

        [HttpPost]
        [ActionName("IsShahkarAlive")]
        public HttpResponseMessage IsShahkarAlive()
        {
            string responseFromShahkar = string.Empty;
            string jsonOfPendarWebApiResult = string.Empty;
            HttpResponseMessage response = new HttpResponseMessage();
            PendarWebApiResult pendarWebApiResult = new PendarWebApiResult();

            try
            {
                string authorizationHeader = HttpRequestHelpers.GenerateBase64Authorization("TCI_Kermanshah", "T30!-Ke$m301an$$");

                //ارسال درخواست به شاهکار
                responseFromShahkar = HttpRequestHelpers.SendHttpWebRequestWithoutData("http://172.16.11.3:8083/rest/shahkar/hc", authorizationHeader);

                pendarWebApiResult.RawResultFromShahkar = responseFromShahkar;
                pendarWebApiResult.SystemError = "";

                jsonOfPendarWebApiResult = JsonConvert.SerializeObject(pendarWebApiResult);
                response.Content = new StringContent(jsonOfPendarWebApiResult, System.Text.Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.OK;
                this.Request.Properties["HasError"] = false;
            }
            catch (Exception ex)
            {
                Logger.WriteError("PendarPajouhAPI => Following exception caught in {0}", this.ControllerContext.RouteData.Values["action"].ToString());
                Logger.WriteException("PendarPajouhAPI => {0}", ex.Message);

                pendarWebApiResult.SystemError = ex.Message;
                pendarWebApiResult.RawResultFromShahkar = responseFromShahkar;

                jsonOfPendarWebApiResult = JsonConvert.SerializeObject(pendarWebApiResult);
                response.Content = new StringContent(jsonOfPendarWebApiResult, System.Text.Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.BadRequest;

                this.Request.Properties["HasError"] = true;
            }
            return response;
        }
    }
}