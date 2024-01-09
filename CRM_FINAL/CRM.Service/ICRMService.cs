using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceHost
{
    [ServiceContract(SessionMode = SessionMode.Required)]

    [XmlSerializerFormat]
    public interface ICRMService
    {
        [OperationContract]
        bool Login(string UserName, string Password);

        [OperationContract(Name = "TelephoneInfo")]
        CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneInfo GetTelephoneInfo(long telephonNo, out bool hasError, out string errorMessages);

        [OperationContract(Name = "ChangeTelephoneInfo")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.ChangeTelephone> GetChangeTelephone(DateTime fromDateTime, DateTime toDateTime, List<int> centersCode, List<int> requestTypesID, out bool hasError, out string errorMessages);

        [OperationContract(Name = "CityInfo")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.CityInfo> GetCity(out bool hasError, out string errorMessages);

        [OperationContract(Name = "CenterInfo")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.CenterInfo> GetCenter(out bool hasError, out string errorMessages);

        [OperationContract(Name = "RequestTypeInfo")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.RequestType> GetRequestType(out bool hasError, out string errorMessages);

        [OperationContract(Name = "TelephoneType")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneType> GetTelephoneType(out bool hasError, out string errorMessages);

        [OperationContract(Name = "TelephoneGroupType")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneGroupType> GetTelephoneGroupType(out bool hasError, out string errorMessages);

        [OperationContract(Name = "CauseOfCut")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo> GetCauseOfCut(out bool hasError, out string ErrorMessages);

        [OperationContract(Name = "CashPaymentInfo")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.CashPaymentInfo> GetCashPaymentInfo(DateTime fromDate, DateTime toDate, List<int> centersCode, out bool hasError, out string errorMessages);

        [OperationContract(Name = "TelephoneConnectionInstallmentInfo")]
        List<CRM.Data.ServiceHost.ServiceHostCustomClass.TelephoneConnectionInstallmentInfo> GetTelephoneConnectionInstallmentInfo(DateTime fromDate, DateTime toDate, List<int> centersCode, out bool hasError, out string errorMessages);

        //[OperationContract(Name = "GetPAPInstallRequestCount")]
        //List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPInstallRequestCount(DateTime fromDate, DateTime toDate);

        //[OperationContract(Name = "GetPAPDischargeRequestCount")]
        //List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPDischargeRequestCount(DateTime fromDate, DateTime toDate);

        //[OperationContract(Name = "GetPAPExchangeRequestCount")]
        //List<CRM.Data.ServiceHost.ServiceHostCustomClass.PAPBillingInfo> GetPAPExchangeRequestCount(DateTime fromDate, DateTime toDate);

        //[OperationContract(Name = "GetCustomerProperties")]
        //string GetCustomerProperties(long telephoneNo);
    }
}
