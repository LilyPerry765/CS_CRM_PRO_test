using CRM.Data;
using CRM.Data.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Enterprise;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Configuration;

namespace ServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConfigurationName = "bindingConfig", MaxItemsInObjectGraph = 2147483647)]
    public class CRMService : ICRMService
    {

        #region Properties And Fields

        public List<string> AuthorizedWebServices { get; set; }

        public UserInfo CurrentUserInfo { get; set; }

        #endregion

        #region Constructors

        public CRMService()
        {
            Logger.WriteInfo("CRMService's constructor is called.");

            this.AuthorizedWebServices = new List<string>();

            //string connectionString = ConfigurationManager.ConnectionStrings["CRM.Service.Properties.Settings.CRMConnectionString"].ConnectionString;
            //DB.SetConnectionString(connectionString);
        }

        #endregion

        #region WebService Methods

        // [PrincipalPermission(SecurityAction.Demand, Role = "data")]
        public CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneInfo GetTelephoneInfo(long telephonNo, out bool hasError, out string errorMessages)
        {
            CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneInfo result = new CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneInfo();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetTelephoneInfo(telephonNo);
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "TelephoneInfo");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //  [PrincipalPermission(SecurityAction.Demand, Role = "billing")] 
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> GetChangeTelephone(DateTime fromDateTime, DateTime toDateTime, List<int> centersCode, List<int> requestTypesID, out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetChangeTelephone(fromDateTime, toDateTime, centersCode, requestTypesID);
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "ChangeTelephoneInfo");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //  [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.CityInfo> GetCity(out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.CityInfo> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.CityInfo>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetCity();
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "CityInfo");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //   [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.CenterInfo> GetCenter(out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.CenterInfo> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.CenterInfo>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetCenter();
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "CenterInfo");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //   [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.RequestType> GetRequestType(out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.RequestType> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.RequestType>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetRequestType();
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "RequestTypeInfo");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //  [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneType> GetTelephoneType(out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneType> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneType>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetTelephoneType();
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "TelephoneType");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //  [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneGroupType> GetTelephoneGroupType(out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneGroupType> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneGroupType>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetTelephoneGroupType();
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "TelephoneGroupType");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        //  [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        public List<CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo> GetCauseOfCut(out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo> result = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetCauseOfCut();
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, "CauseOfCut");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }

            return result;
        }

        public List<ServiceHostCustomClass.CashPaymentInfo> GetCashPaymentInfo(DateTime fromDate, DateTime toDate, List<int> centersCode, out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.CashPaymentInfo> result = new List<ServiceHostCustomClass.CashPaymentInfo>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetCashPaymentInfo(fromDate, toDate, centersCode);
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }
            return result;
        }

        public List<ServiceHostCustomClass.TelephoneConnectionInstallmentInfo> GetTelephoneConnectionInstallmentInfo(DateTime fromDate, DateTime toDate, List<int> centersCode, out bool hasError, out string errorMessages)
        {
            List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneConnectionInstallmentInfo> result = new List<ServiceHostCustomClass.TelephoneConnectionInstallmentInfo>();
            errorMessages = string.Empty;
            hasError = false;
            try
            {
                CheckPermission();
                result = ServiceHostDB.GetTelephoneConnectionInstallmentInfo(fromDate, toDate, centersCode);
                Logger.WriteInfo("User : {0} calles method :{1} successfully", this.CurrentUserInfo.UserName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessages = ex.Message;
            }
            return result;
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        //TODO:rad 13950719 متدهای وب سرویس مربوط به شرکت های پپ با تاییدیه از خانم نیک پور از این فایل کامنت شد
        //public List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPInstallRequestCount(DateTime fromDate, DateTime toDate)
        //{
        //    List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> papBillingList = new List<ServiceHostCustomClass.PAPBillingInfo>();

        //    papBillingList = ADSLPAPRequestDB.GetPAPBillingInfoList(fromDate, toDate, (byte)DB.RequestType.ADSLInstalPAPCompany);

        //    return papBillingList;
        //}

        ////  [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        //public List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPDischargeRequestCount(DateTime fromDate, DateTime toDate)
        //{
        //    List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> papBillingList = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo>();

        //    papBillingList = ADSLPAPRequestDB.GetPAPBillingInfoList(fromDate, toDate, (byte)DB.RequestType.ADSLDischargePAPCompany);

        //    return papBillingList;
        //}

        ////   [PrincipalPermission(SecurityAction.Demand, Role = "billing")]
        //public List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPExchangeRequestCount(DateTime fromDate, DateTime toDate)
        //{
        //    List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> papBillingList = new List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo>();

        //    papBillingList = ADSLPAPRequestDB.GetPAPBillingInfoList(fromDate, toDate, (byte)DB.RequestType.ADSLExchangePAPCompany);

        //    return papBillingList;
        //}        

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string GetCustomerProperties(long telephoneNo)
        //{
        //    string result = "Niki";

        //    return new JavaScriptSerializer().Serialize(result);
        //}

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string GetCustomerPropertiesArray(long telephoneNo)
        //{
        //    string[] aaa = { "1", "2", "3" };

        //    return new JavaScriptSerializer().Serialize(aaa);
        //}


        #endregion

        #region Security Methods

        private void CheckPermission()
        {
            string action = OperationContext.Current.IncomingMessageHeaders.Action;
            string webServiceMethodName = default(string);

            var dispatcherOperation = OperationContext.Current.EndpointDispatcher.DispatchRuntime.Operations.FirstOrDefault(dto => dto.Action == action);
            if (dispatcherOperation != null)
            {
                //در این بلاک نام متد فراخوانی شده از سمت کلاینت برگردانده میشود
                webServiceMethodName = dispatcherOperation.Name;
            }

            //چنانچه نام متد فراخوانی شده در لیست متدهای قابل دسترس کاربر مورد نظر باشد متغیر زیر مقدار "صحیح" میگیرد
            bool hasAccess = this.AuthorizedWebServices.Contains(webServiceMethodName);

            if (!hasAccess) //یعنی کاربر استفاده کننده حق دسترسی به متد را ندارد
            {
                Logger.WriteException("Fullname : {0} - ID : {1} **** has no access to web service method : {2}", this.CurrentUserInfo.FullName, this.CurrentUserInfo.ID, webServiceMethodName);
                string exceptionMessage = string.Format("{0} :  خطا در دسترسی وب سرویس ", webServiceMethodName);
                throw new Exception(exceptionMessage);
            }
        }

        public bool Login(string userName, string password)
        {
            string folderConnectionString = ConfigurationManager.ConnectionStrings["CRM.Service.Properties.Settings.FolderConnectionString"].ConnectionString;
            var user = SecurityDB.GetUserInfoByFolderUsernameAndPassword(userName, password, folderConnectionString);

            if (user == null)
            {
                Logger.WriteException("There is no user with userName : {0} - passWord : {1}", userName, password);
                throw new Exception("نام کاربری و یا کلمه عبور صحیح نمیباشد");
            }

            this.AuthorizedWebServices = SecurityDB.GetWebServiceMethodsNameByRoleId(user.RoleID);
            this.CurrentUserInfo = user;

            return true;
        }

        #endregion

    }
}
