using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.ComponentModel;

namespace CRM.Data.Services
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "CRMWebServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class CRMWebService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback SaveFailure117OperationCompleted;

        private System.Threading.SendOrPostCallback SaveFailure117KermanshahOperationCompleted;

        private System.Threading.SendOrPostCallback GetFailureRequestStateOperationCompleted;

        private System.Threading.SendOrPostCallback CheckCabinetAccuracyOperationCompleted;

        private System.Threading.SendOrPostCallback SaveFailure117fromHelpDeskOperationCompleted;

        private System.Threading.SendOrPostCallback SendMessageOperationCompleted;

        private System.Threading.SendOrPostCallback SaveADSLChangeServiceOperationCompleted;

        private System.Threading.SendOrPostCallback ConfirmADSLChangeServiceOperationCompleted;

        private System.Threading.SendOrPostCallback SaveADSLSaleTrafficeOperationCompleted;

        private System.Threading.SendOrPostCallback ConfirmADSLSaleTrafficOperationCompleted;

        private System.Threading.SendOrPostCallback SaveRequestPaymentOperationCompleted;

        private System.Threading.SendOrPostCallback PaidRequestPaymentOperationCompleted;

        private System.Threading.SendOrPostCallback GenerateBillIDOperationCompleted;

        private System.Threading.SendOrPostCallback GeneratePaymentIDOperationCompleted;

        private System.Threading.SendOrPostCallback GetADSLPaymentOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public CRMWebService()
        {
            this.Url = "http://crm.tcsem.ir:82/CRMWebService.asmx";
            //this.Url = global::ConsumeCRMWebApplication.Properties.Settings.Default.ConsumeCRMWebApplication_CRMWEbService_CRMWebService;
            //if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
            //    this.UseDefaultCredentials = true;
            //    this.useDefaultCredentialsSetExplicitly = false;
            //}
            //else {
            //    this.useDefaultCredentialsSetExplicitly = true;
            //}
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event SaveFailure117CompletedEventHandler SaveFailure117Completed;

        /// <remarks/>
        public event SaveFailure117KermanshahCompletedEventHandler SaveFailure117KermanshahCompleted;

        /// <remarks/>
        public event GetFailureRequestStateCompletedEventHandler GetFailureRequestStateCompleted;

        /// <remarks/>
        public event CheckCabinetAccuracyCompletedEventHandler CheckCabinetAccuracyCompleted;

        /// <remarks/>
        public event SaveFailure117fromHelpDeskCompletedEventHandler SaveFailure117fromHelpDeskCompleted;

        /// <remarks/>
        public event SendMessageCompletedEventHandler SendMessageCompleted;

        /// <remarks/>
        public event SaveADSLChangeServiceCompletedEventHandler SaveADSLChangeServiceCompleted;

        /// <remarks/>
        public event ConfirmADSLChangeServiceCompletedEventHandler ConfirmADSLChangeServiceCompleted;

        /// <remarks/>
        public event SaveADSLSaleTrafficeCompletedEventHandler SaveADSLSaleTrafficeCompleted;

        /// <remarks/>
        public event ConfirmADSLSaleTrafficCompletedEventHandler ConfirmADSLSaleTrafficCompleted;

        /// <remarks/>
        public event SaveRequestPaymentCompletedEventHandler SaveRequestPaymentCompleted;

        /// <remarks/>
        public event PaidRequestPaymentCompletedEventHandler PaidRequestPaymentCompleted;

        /// <remarks/>
        public event GenerateBillIDCompletedEventHandler GenerateBillIDCompleted;

        /// <remarks/>
        public event GeneratePaymentIDCompletedEventHandler GeneratePaymentIDCompleted;

        /// <remarks/>
        public event GetADSLPaymentCompletedEventHandler GetADSLPaymentCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveFailure117", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SaveFailure117(long telephoneNo, long callingNo, int centercode, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] recordeSound, out bool result, out bool isConfirmed)
        {
            object[] results = this.Invoke("SaveFailure117", new object[] {
                        telephoneNo,
                        callingNo,
                        centercode,
                        recordeSound});
            result = ((bool)(results[1]));
            isConfirmed = ((bool)(results[2]));
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void SaveFailure117Async(long telephoneNo, long callingNo, int centercode, byte[] recordeSound)
        {
            this.SaveFailure117Async(telephoneNo, callingNo, centercode, recordeSound, null);
        }

        /// <remarks/>
        public void SaveFailure117Async(long telephoneNo, long callingNo, int centercode, byte[] recordeSound, object userState)
        {
            if ((this.SaveFailure117OperationCompleted == null))
            {
                this.SaveFailure117OperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveFailure117OperationCompleted);
            }
            this.InvokeAsync("SaveFailure117", new object[] {
                        telephoneNo,
                        callingNo,
                        centercode,
                        recordeSound}, this.SaveFailure117OperationCompleted, userState);
        }

        private void OnSaveFailure117OperationCompleted(object arg)
        {
            if ((this.SaveFailure117Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveFailure117Completed(this, new SaveFailure117CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveFailure117Kermanshah", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SaveFailure117Kermanshah(long telephoneNo, long callingNo, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] recordeSound, out bool result, out bool isConfirmed)
        {
            object[] results = this.Invoke("SaveFailure117Kermanshah", new object[] {
                        telephoneNo,
                        callingNo,
                        recordeSound});
            result = ((bool)(results[1]));
            isConfirmed = ((bool)(results[2]));
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void SaveFailure117KermanshahAsync(long telephoneNo, long callingNo, byte[] recordeSound)
        {
            this.SaveFailure117KermanshahAsync(telephoneNo, callingNo, recordeSound, null);
        }

        /// <remarks/>
        public void SaveFailure117KermanshahAsync(long telephoneNo, long callingNo, byte[] recordeSound, object userState)
        {
            if ((this.SaveFailure117KermanshahOperationCompleted == null))
            {
                this.SaveFailure117KermanshahOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveFailure117KermanshahOperationCompleted);
            }
            this.InvokeAsync("SaveFailure117Kermanshah", new object[] {
                        telephoneNo,
                        callingNo,
                        recordeSound}, this.SaveFailure117KermanshahOperationCompleted, userState);
        }

        private void OnSaveFailure117KermanshahOperationCompleted(object arg)
        {
            if ((this.SaveFailure117KermanshahCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveFailure117KermanshahCompleted(this, new SaveFailure117KermanshahCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFailureRequestState", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GetFailureRequestState(long telephoneNo, out bool isFinished, out int resultFailure, out int color1, out int color2, out int cableType)
        {
            object[] results = this.Invoke("GetFailureRequestState", new object[] {
                        telephoneNo});
            isFinished = ((bool)(results[1]));
            resultFailure = ((int)(results[2]));
            color1 = ((int)(results[3]));
            color2 = ((int)(results[4]));
            cableType = ((int)(results[5]));
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void GetFailureRequestStateAsync(long telephoneNo)
        {
            this.GetFailureRequestStateAsync(telephoneNo, null);
        }

        /// <remarks/>
        public void GetFailureRequestStateAsync(long telephoneNo, object userState)
        {
            if ((this.GetFailureRequestStateOperationCompleted == null))
            {
                this.GetFailureRequestStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFailureRequestStateOperationCompleted);
            }
            this.InvokeAsync("GetFailureRequestState", new object[] {
                        telephoneNo}, this.GetFailureRequestStateOperationCompleted, userState);
        }

        private void OnGetFailureRequestStateOperationCompleted(object arg)
        {
            if ((this.GetFailureRequestStateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFailureRequestStateCompleted(this, new GetFailureRequestStateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckCabinetAccuracy", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckCabinetAccuracy(int cabinetNo, int postNo, long teleophoneNo, int centercode)
        {
            object[] results = this.Invoke("CheckCabinetAccuracy", new object[] {
                        cabinetNo,
                        postNo,
                        teleophoneNo,
                        centercode});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void CheckCabinetAccuracyAsync(int cabinetNo, int postNo, long teleophoneNo, int centercode)
        {
            this.CheckCabinetAccuracyAsync(cabinetNo, postNo, teleophoneNo, centercode, null);
        }

        /// <remarks/>
        public void CheckCabinetAccuracyAsync(int cabinetNo, int postNo, long teleophoneNo, int centercode, object userState)
        {
            if ((this.CheckCabinetAccuracyOperationCompleted == null))
            {
                this.CheckCabinetAccuracyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckCabinetAccuracyOperationCompleted);
            }
            this.InvokeAsync("CheckCabinetAccuracy", new object[] {
                        cabinetNo,
                        postNo,
                        teleophoneNo,
                        centercode}, this.CheckCabinetAccuracyOperationCompleted, userState);
        }

        private void OnCheckCabinetAccuracyOperationCompleted(object arg)
        {
            if ((this.CheckCabinetAccuracyCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckCabinetAccuracyCompleted(this, new CheckCabinetAccuracyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveFailure117fromHelpDesk", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SaveFailure117fromHelpDesk(long telephoneNo, long ticketID, string description, out long requestID)
        {
            object[] results = this.Invoke("SaveFailure117fromHelpDesk", new object[] {
                        telephoneNo,
                        ticketID,
                        description});
            requestID = ((long)(results[1]));
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void SaveFailure117fromHelpDeskAsync(long telephoneNo, long ticketID, string description)
        {
            this.SaveFailure117fromHelpDeskAsync(telephoneNo, ticketID, description, null);
        }

        /// <remarks/>
        public void SaveFailure117fromHelpDeskAsync(long telephoneNo, long ticketID, string description, object userState)
        {
            if ((this.SaveFailure117fromHelpDeskOperationCompleted == null))
            {
                this.SaveFailure117fromHelpDeskOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveFailure117fromHelpDeskOperationCompleted);
            }
            this.InvokeAsync("SaveFailure117fromHelpDesk", new object[] {
                        telephoneNo,
                        ticketID,
                        description}, this.SaveFailure117fromHelpDeskOperationCompleted, userState);
        }

        private void OnSaveFailure117fromHelpDeskOperationCompleted(object arg)
        {
            if ((this.SaveFailure117fromHelpDeskCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveFailure117fromHelpDeskCompleted(this, new SaveFailure117fromHelpDeskCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendMessage", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SendMessage(string telephoneNos, string message)
        {
            this.Invoke("SendMessage", new object[] {
                        telephoneNos,
                        message});
        }

        /// <remarks/>
        public void SendMessageAsync(string telephoneNos, string message)
        {
            this.SendMessageAsync(telephoneNos, message, null);
        }

        /// <remarks/>
        public void SendMessageAsync(string telephoneNos, string message, object userState)
        {
            if ((this.SendMessageOperationCompleted == null))
            {
                this.SendMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageOperationCompleted);
            }
            this.InvokeAsync("SendMessage", new object[] {
                        telephoneNos,
                        message}, this.SendMessageOperationCompleted, userState);
        }

        private void OnSendMessageOperationCompleted(object arg)
        {
            if ((this.SendMessageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveADSLChangeService", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long SaveADSLChangeService(long telephoneNo, int oldServiceID, int newServiceID, out bool result)
        {
            object[] results = this.Invoke("SaveADSLChangeService", new object[] {
                        telephoneNo,
                        oldServiceID,
                        newServiceID});
            result = ((bool)(results[1]));
            return ((long)(results[0]));
        }

        /// <remarks/>
        public void SaveADSLChangeServiceAsync(long telephoneNo, int oldServiceID, int newServiceID)
        {
            this.SaveADSLChangeServiceAsync(telephoneNo, oldServiceID, newServiceID, null);
        }

        /// <remarks/>
        public void SaveADSLChangeServiceAsync(long telephoneNo, int oldServiceID, int newServiceID, object userState)
        {
            if ((this.SaveADSLChangeServiceOperationCompleted == null))
            {
                this.SaveADSLChangeServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveADSLChangeServiceOperationCompleted);
            }
            this.InvokeAsync("SaveADSLChangeService", new object[] {
                        telephoneNo,
                        oldServiceID,
                        newServiceID}, this.SaveADSLChangeServiceOperationCompleted, userState);
        }

        private void OnSaveADSLChangeServiceOperationCompleted(object arg)
        {
            if ((this.SaveADSLChangeServiceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveADSLChangeServiceCompleted(this, new SaveADSLChangeServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConfirmADSLChangeService", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ConfirmADSLChangeService(long requestID, int newServiceID, bool isIBSngUpdated)
        {
            object[] results = this.Invoke("ConfirmADSLChangeService", new object[] {
                        requestID,
                        newServiceID,
                        isIBSngUpdated});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void ConfirmADSLChangeServiceAsync(long requestID, int newServiceID, bool isIBSngUpdated)
        {
            this.ConfirmADSLChangeServiceAsync(requestID, newServiceID, isIBSngUpdated, null);
        }

        /// <remarks/>
        public void ConfirmADSLChangeServiceAsync(long requestID, int newServiceID, bool isIBSngUpdated, object userState)
        {
            if ((this.ConfirmADSLChangeServiceOperationCompleted == null))
            {
                this.ConfirmADSLChangeServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmADSLChangeServiceOperationCompleted);
            }
            this.InvokeAsync("ConfirmADSLChangeService", new object[] {
                        requestID,
                        newServiceID,
                        isIBSngUpdated}, this.ConfirmADSLChangeServiceOperationCompleted, userState);
        }

        private void OnConfirmADSLChangeServiceOperationCompleted(object arg)
        {
            if ((this.ConfirmADSLChangeServiceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmADSLChangeServiceCompleted(this, new ConfirmADSLChangeServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveADSLSaleTraffice", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long SaveADSLSaleTraffice(long telephoneNo, int trafficeID, out bool result)
        {
            object[] results = this.Invoke("SaveADSLSaleTraffice", new object[] {
                        telephoneNo,
                        trafficeID});
            result = ((bool)(results[1]));
            return ((long)(results[0]));
        }

        /// <remarks/>
        public void SaveADSLSaleTrafficeAsync(long telephoneNo, int trafficeID)
        {
            this.SaveADSLSaleTrafficeAsync(telephoneNo, trafficeID, null);
        }

        /// <remarks/>
        public void SaveADSLSaleTrafficeAsync(long telephoneNo, int trafficeID, object userState)
        {
            if ((this.SaveADSLSaleTrafficeOperationCompleted == null))
            {
                this.SaveADSLSaleTrafficeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveADSLSaleTrafficeOperationCompleted);
            }
            this.InvokeAsync("SaveADSLSaleTraffice", new object[] {
                        telephoneNo,
                        trafficeID}, this.SaveADSLSaleTrafficeOperationCompleted, userState);
        }

        private void OnSaveADSLSaleTrafficeOperationCompleted(object arg)
        {
            if ((this.SaveADSLSaleTrafficeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveADSLSaleTrafficeCompleted(this, new SaveADSLSaleTrafficeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConfirmADSLSaleTraffic", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ConfirmADSLSaleTraffic(long requestID, bool isIBSngUpdated)
        {
            object[] results = this.Invoke("ConfirmADSLSaleTraffic", new object[] {
                        requestID,
                        isIBSngUpdated});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void ConfirmADSLSaleTrafficAsync(long requestID, bool isIBSngUpdated)
        {
            this.ConfirmADSLSaleTrafficAsync(requestID, isIBSngUpdated, null);
        }

        /// <remarks/>
        public void ConfirmADSLSaleTrafficAsync(long requestID, bool isIBSngUpdated, object userState)
        {
            if ((this.ConfirmADSLSaleTrafficOperationCompleted == null))
            {
                this.ConfirmADSLSaleTrafficOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmADSLSaleTrafficOperationCompleted);
            }
            this.InvokeAsync("ConfirmADSLSaleTraffic", new object[] {
                        requestID,
                        isIBSngUpdated}, this.ConfirmADSLSaleTrafficOperationCompleted, userState);
        }

        private void OnConfirmADSLSaleTrafficOperationCompleted(object arg)
        {
            if ((this.ConfirmADSLSaleTrafficCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmADSLSaleTrafficCompleted(this, new ConfirmADSLSaleTrafficCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveRequestPayment", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long SaveRequestPayment(long requestID, string orderID)
        {
            object[] results = this.Invoke("SaveRequestPayment", new object[] {
                        requestID,
                        orderID});
            return ((long)(results[0]));
        }

        /// <remarks/>
        public void SaveRequestPaymentAsync(long requestID, string orderID)
        {
            this.SaveRequestPaymentAsync(requestID, orderID, null);
        }

        /// <remarks/>
        public void SaveRequestPaymentAsync(long requestID, string orderID, object userState)
        {
            if ((this.SaveRequestPaymentOperationCompleted == null))
            {
                this.SaveRequestPaymentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveRequestPaymentOperationCompleted);
            }
            this.InvokeAsync("SaveRequestPayment", new object[] {
                        requestID,
                        orderID}, this.SaveRequestPaymentOperationCompleted, userState);
        }

        private void OnSaveRequestPaymentOperationCompleted(object arg)
        {
            if ((this.SaveRequestPaymentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveRequestPaymentCompleted(this, new SaveRequestPaymentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PaidRequestPayment", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void PaidRequestPayment(long requestPaymentID, int bankCode, string traceNo)
        {
            this.Invoke("PaidRequestPayment", new object[] {
                        requestPaymentID,
                        bankCode,
                        traceNo});
        }

        /// <remarks/>
        public void PaidRequestPaymentAsync(long requestPaymentID, int bankCode, string traceNo)
        {
            this.PaidRequestPaymentAsync(requestPaymentID, bankCode, traceNo, null);
        }

        /// <remarks/>
        public void PaidRequestPaymentAsync(long requestPaymentID, int bankCode, string traceNo, object userState)
        {
            if ((this.PaidRequestPaymentOperationCompleted == null))
            {
                this.PaidRequestPaymentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPaidRequestPaymentOperationCompleted);
            }
            this.InvokeAsync("PaidRequestPayment", new object[] {
                        requestPaymentID,
                        bankCode,
                        traceNo}, this.PaidRequestPaymentOperationCompleted, userState);
        }

        private void OnPaidRequestPaymentOperationCompleted(object arg)
        {
            if ((this.PaidRequestPaymentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PaidRequestPaymentCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GenerateBillID", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GenerateBillID(long telephoneNo, int centerID, byte subsidiaryCodeType)
        {
            object[] results = this.Invoke("GenerateBillID", new object[] {
                        telephoneNo,
                        centerID,
                        subsidiaryCodeType});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GenerateBillIDAsync(long telephoneNo, int centerID, byte subsidiaryCodeType)
        {
            this.GenerateBillIDAsync(telephoneNo, centerID, subsidiaryCodeType, null);
        }

        /// <remarks/>
        public void GenerateBillIDAsync(long telephoneNo, int centerID, byte subsidiaryCodeType, object userState)
        {
            if ((this.GenerateBillIDOperationCompleted == null))
            {
                this.GenerateBillIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerateBillIDOperationCompleted);
            }
            this.InvokeAsync("GenerateBillID", new object[] {
                        telephoneNo,
                        centerID,
                        subsidiaryCodeType}, this.GenerateBillIDOperationCompleted, userState);
        }

        private void OnGenerateBillIDOperationCompleted(object arg)
        {
            if ((this.GenerateBillIDCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerateBillIDCompleted(this, new GenerateBillIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GeneratePaymentID", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GeneratePaymentID(long amount, long telephoneNo, string billID, byte subsidiaryCodeType, bool isAddCycle)
        {
            object[] results = this.Invoke("GeneratePaymentID", new object[] {
                        amount,
                        telephoneNo,
                        billID,
                        subsidiaryCodeType,
                        isAddCycle});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GeneratePaymentIDAsync(long amount, long telephoneNo, string billID, byte subsidiaryCodeType, bool isAddCycle)
        {
            this.GeneratePaymentIDAsync(amount, telephoneNo, billID, subsidiaryCodeType, isAddCycle, null);
        }

        /// <remarks/>
        public void GeneratePaymentIDAsync(long amount, long telephoneNo, string billID, byte subsidiaryCodeType, bool isAddCycle, object userState)
        {
            if ((this.GeneratePaymentIDOperationCompleted == null))
            {
                this.GeneratePaymentIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGeneratePaymentIDOperationCompleted);
            }
            this.InvokeAsync("GeneratePaymentID", new object[] {
                        amount,
                        telephoneNo,
                        billID,
                        subsidiaryCodeType,
                        isAddCycle}, this.GeneratePaymentIDOperationCompleted, userState);
        }

        private void OnGeneratePaymentIDOperationCompleted(object arg)
        {
            if ((this.GeneratePaymentIDCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GeneratePaymentIDCompleted(this, new GeneratePaymentIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetADSLPayment", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long GetADSLPayment(long telephoneNo)
        {
            object[] results = this.Invoke("GetADSLPayment", new object[] {
                        telephoneNo});
            return ((long)(results[0]));
        }

        /// <remarks/>
        public void GetADSLPaymentAsync(long telephoneNo)
        {
            this.GetADSLPaymentAsync(telephoneNo, null);
        }

        /// <remarks/>
        public void GetADSLPaymentAsync(long telephoneNo, object userState)
        {
            if ((this.GetADSLPaymentOperationCompleted == null))
            {
                this.GetADSLPaymentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetADSLPaymentOperationCompleted);
            }
            this.InvokeAsync("GetADSLPayment", new object[] {
                        telephoneNo}, this.GetADSLPaymentOperationCompleted, userState);
        }

        private void OnGetADSLPaymentOperationCompleted(object arg)
        {
            if ((this.GetADSLPaymentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetADSLPaymentCompleted(this, new GetADSLPaymentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SaveFailure117CompletedEventHandler(object sender, SaveFailure117CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveFailure117CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveFailure117CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }

        /// <remarks/>
        public bool result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }

        /// <remarks/>
        public bool isConfirmed
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[2]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SaveFailure117KermanshahCompletedEventHandler(object sender, SaveFailure117KermanshahCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveFailure117KermanshahCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveFailure117KermanshahCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }

        /// <remarks/>
        public bool result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }

        /// <remarks/>
        public bool isConfirmed
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[2]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetFailureRequestStateCompletedEventHandler(object sender, GetFailureRequestStateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFailureRequestStateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetFailureRequestStateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }

        /// <remarks/>
        public bool isFinished
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }

        /// <remarks/>
        public int resultFailure
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[2]));
            }
        }

        /// <remarks/>
        public int color1
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[3]));
            }
        }

        /// <remarks/>
        public int color2
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[4]));
            }
        }

        /// <remarks/>
        public int cableType
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[5]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void CheckCabinetAccuracyCompletedEventHandler(object sender, CheckCabinetAccuracyCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckCabinetAccuracyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CheckCabinetAccuracyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SaveFailure117fromHelpDeskCompletedEventHandler(object sender, SaveFailure117fromHelpDeskCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveFailure117fromHelpDeskCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveFailure117fromHelpDeskCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }

        /// <remarks/>
        public long requestID
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SendMessageCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SaveADSLChangeServiceCompletedEventHandler(object sender, SaveADSLChangeServiceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveADSLChangeServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveADSLChangeServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public long Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }

        /// <remarks/>
        public bool result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void ConfirmADSLChangeServiceCompletedEventHandler(object sender, ConfirmADSLChangeServiceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmADSLChangeServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConfirmADSLChangeServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SaveADSLSaleTrafficeCompletedEventHandler(object sender, SaveADSLSaleTrafficeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveADSLSaleTrafficeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveADSLSaleTrafficeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public long Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }

        /// <remarks/>
        public bool result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void ConfirmADSLSaleTrafficCompletedEventHandler(object sender, ConfirmADSLSaleTrafficCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmADSLSaleTrafficCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConfirmADSLSaleTrafficCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SaveRequestPaymentCompletedEventHandler(object sender, SaveRequestPaymentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveRequestPaymentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveRequestPaymentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public long Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void PaidRequestPaymentCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GenerateBillIDCompletedEventHandler(object sender, GenerateBillIDCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerateBillIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GenerateBillIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GeneratePaymentIDCompletedEventHandler(object sender, GeneratePaymentIDCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GeneratePaymentIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GeneratePaymentIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetADSLPaymentCompletedEventHandler(object sender, GetADSLPaymentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetADSLPaymentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetADSLPaymentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public long Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }

}