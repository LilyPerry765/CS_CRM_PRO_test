﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="v2Soap", Namespace="http://tempuri.org/")]
public partial class SMSService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback SendSMSOperationCompleted;
    
    private System.Threading.SendOrPostCallback GetMessageStatusOperationCompleted;
    
    private System.Threading.SendOrPostCallback GetCreditOperationCompleted;
    
    private System.Threading.SendOrPostCallback CheckMessageIDsOperationCompleted;
    
    private System.Threading.SendOrPostCallback GetReceiveMessagesOperationCompleted;
    
    /// <remarks/>
    public SMSService() {
        this.Url = "https://sms.tci.ir/webservice/v2.asmx";
    }
    
    /// <remarks/>
    public event SendSMSCompletedEventHandler SendSMSCompleted;
    
    /// <remarks/>
    public event GetMessageStatusCompletedEventHandler GetMessageStatusCompleted;
    
    /// <remarks/>
    public event GetCreditCompletedEventHandler GetCreditCompleted;
    
    /// <remarks/>
    public event CheckMessageIDsCompletedEventHandler CheckMessageIDsCompleted;
    
    /// <remarks/>
    public event GetReceiveMessagesCompletedEventHandler GetReceiveMessagesCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendSMS", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    
    public long[] SendSMS(string username, string password, string[] senderNumbers, string[] recipientNumbers, string[] messageBodies, string[] sendDate, int[] messageClasses, long[] checkingMessageIds) {
        object[] results = this.Invoke("SendSMS", new object[] {
                    username,
                    password,
                    senderNumbers,
                    recipientNumbers,
                    messageBodies,
                    sendDate,
                    messageClasses,
                    checkingMessageIds});
        return ((long[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginSendSMS(string username, string password, string[] senderNumbers, string[] recipientNumbers, string[] messageBodies, string[] sendDate, int[] messageClasses, long[] checkingMessageIds, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("SendSMS", new object[] {
                    username,
                    password,
                    senderNumbers,
                    recipientNumbers,
                    messageBodies,
                    sendDate,
                    messageClasses,
                    checkingMessageIds}, callback, asyncState);
    }
    
    /// <remarks/>
    public long[] EndSendSMS(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((long[])(results[0]));
    }
    
    /// <remarks/>
    public void SendSMSAsync(string username, string password, string[] senderNumbers, string[] recipientNumbers, string[] messageBodies, string[] sendDate, int[] messageClasses, long[] checkingMessageIds) {
        this.SendSMSAsync(username, password, senderNumbers, recipientNumbers, messageBodies, sendDate, messageClasses, checkingMessageIds, null);
    }
    
    /// <remarks/>
    public void SendSMSAsync(string username, string password, string[] senderNumbers, string[] recipientNumbers, string[] messageBodies, string[] sendDate, int[] messageClasses, long[] checkingMessageIds, object userState) {
        if ((this.SendSMSOperationCompleted == null)) {
            this.SendSMSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendSMSOperationCompleted);
        }
        this.InvokeAsync("SendSMS", new object[] {
                    username,
                    password,
                    senderNumbers,
                    recipientNumbers,
                    messageBodies,
                    sendDate,
                    messageClasses,
                    checkingMessageIds}, this.SendSMSOperationCompleted, userState);
    }
    
    private void OnSendSMSOperationCompleted(object arg) {
        if ((this.SendSMSCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.SendSMSCompleted(this, new SendSMSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetMessageStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public int[] GetMessageStatus(string username, string password, long[] MessageIDs) {
        object[] results = this.Invoke("GetMessageStatus", new object[] {
                    username,
                    password,
                    MessageIDs});
        return ((int[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetMessageStatus(string username, string password, long[] MessageIDs, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetMessageStatus", new object[] {
                    username,
                    password,
                    MessageIDs}, callback, asyncState);
    }
    
    /// <remarks/>
    public int[] EndGetMessageStatus(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((int[])(results[0]));
    }
    
    /// <remarks/>
    public void GetMessageStatusAsync(string username, string password, long[] MessageIDs) {
        this.GetMessageStatusAsync(username, password, MessageIDs, null);
    }
    
    /// <remarks/>
    public void GetMessageStatusAsync(string username, string password, long[] MessageIDs, object userState) {
        if ((this.GetMessageStatusOperationCompleted == null)) {
            this.GetMessageStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMessageStatusOperationCompleted);
        }
        this.InvokeAsync("GetMessageStatus", new object[] {
                    username,
                    password,
                    MessageIDs}, this.GetMessageStatusOperationCompleted, userState);
    }
    
    private void OnGetMessageStatusOperationCompleted(object arg) {
        if ((this.GetMessageStatusCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetMessageStatusCompleted(this, new GetMessageStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCredit", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public double GetCredit(string username, string password) {
        object[] results = this.Invoke("GetCredit", new object[] {
                    username,
                    password});
        return ((double)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetCredit(string username, string password, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetCredit", new object[] {
                    username,
                    password}, callback, asyncState);
    }
    
    /// <remarks/>
    public double EndGetCredit(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((double)(results[0]));
    }
    
    /// <remarks/>
    public void GetCreditAsync(string username, string password) {
        this.GetCreditAsync(username, password, null);
    }
    
    /// <remarks/>
    public void GetCreditAsync(string username, string password, object userState) {
        if ((this.GetCreditOperationCompleted == null)) {
            this.GetCreditOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCreditOperationCompleted);
        }
        this.InvokeAsync("GetCredit", new object[] {
                    username,
                    password}, this.GetCreditOperationCompleted, userState);
    }
    
    private void OnGetCreditOperationCompleted(object arg) {
        if ((this.GetCreditCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetCreditCompleted(this, new GetCreditCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckMessageIDs", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public long[] CheckMessageIDs(string username, string password, long[] checkingids) {
        object[] results = this.Invoke("CheckMessageIDs", new object[] {
                    username,
                    password,
                    checkingids});
        return ((long[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginCheckMessageIDs(string username, string password, long[] checkingids, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("CheckMessageIDs", new object[] {
                    username,
                    password,
                    checkingids}, callback, asyncState);
    }
    
    /// <remarks/>
    public long[] EndCheckMessageIDs(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((long[])(results[0]));
    }
    
    /// <remarks/>
    public void CheckMessageIDsAsync(string username, string password, long[] checkingids) {
        this.CheckMessageIDsAsync(username, password, checkingids, null);
    }
    
    /// <remarks/>
    public void CheckMessageIDsAsync(string username, string password, long[] checkingids, object userState) {
        if ((this.CheckMessageIDsOperationCompleted == null)) {
            this.CheckMessageIDsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckMessageIDsOperationCompleted);
        }
        this.InvokeAsync("CheckMessageIDs", new object[] {
                    username,
                    password,
                    checkingids}, this.CheckMessageIDsOperationCompleted, userState);
    }
    
    private void OnCheckMessageIDsOperationCompleted(object arg) {
        if ((this.CheckMessageIDsCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.CheckMessageIDsCompleted(this, new CheckMessageIDsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetReceiveMessages", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public Messages[] GetReceiveMessages(string username, string password, string destNumber, int isRead) {
        object[] results = this.Invoke("GetReceiveMessages", new object[] {
                    username,
                    password,
                    destNumber,
                    isRead});
        return ((Messages[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetReceiveMessages(string username, string password, string destNumber, int isRead, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetReceiveMessages", new object[] {
                    username,
                    password,
                    destNumber,
                    isRead}, callback, asyncState);
    }
    
    /// <remarks/>
    public Messages[] EndGetReceiveMessages(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((Messages[])(results[0]));
    }
    
    /// <remarks/>
    public void GetReceiveMessagesAsync(string username, string password, string destNumber, int isRead) {
        this.GetReceiveMessagesAsync(username, password, destNumber, isRead, null);
    }
    
    /// <remarks/>
    public void GetReceiveMessagesAsync(string username, string password, string destNumber, int isRead, object userState) {
        if ((this.GetReceiveMessagesOperationCompleted == null)) {
            this.GetReceiveMessagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetReceiveMessagesOperationCompleted);
        }
        this.InvokeAsync("GetReceiveMessages", new object[] {
                    username,
                    password,
                    destNumber,
                    isRead}, this.GetReceiveMessagesOperationCompleted, userState);
    }
    
    private void OnGetReceiveMessagesOperationCompleted(object arg) {
        if ((this.GetReceiveMessagesCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetReceiveMessagesCompleted(this, new GetReceiveMessagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }

    
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
public partial class Messages {
    
    private long messageIDField;
    
    private string recipientNumberField;
    
    private string senderNumberField;
    
    private string bodyField;
    
    private string receiveDateField;
    
    /// <remarks/>
    public long MessageID {
        get {
            return this.messageIDField;
        }
        set {
            this.messageIDField = value;
        }
    }
    
    /// <remarks/>
    public string RecipientNumber {
        get {
            return this.recipientNumberField;
        }
        set {
            this.recipientNumberField = value;
        }
    }
    
    /// <remarks/>
    public string SenderNumber {
        get {
            return this.senderNumberField;
        }
        set {
            this.senderNumberField = value;
        }
    }
    
    /// <remarks/>
    public string Body {
        get {
            return this.bodyField;
        }
        set {
            this.bodyField = value;
        }
    }
    
    /// <remarks/>
    public string ReceiveDate {
        get {
            return this.receiveDateField;
        }
        set {
            this.receiveDateField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void SendSMSCompletedEventHandler(object sender, SendSMSCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SendSMSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal SendSMSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public long[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((long[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void GetMessageStatusCompletedEventHandler(object sender, GetMessageStatusCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetMessageStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetMessageStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public int[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((int[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void GetCreditCompletedEventHandler(object sender, GetCreditCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetCreditCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetCreditCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public double Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((double)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void CheckMessageIDsCompletedEventHandler(object sender, CheckMessageIDsCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CheckMessageIDsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal CheckMessageIDsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public long[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((long[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void GetReceiveMessagesCompletedEventHandler(object sender, GetReceiveMessagesCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetReceiveMessagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetReceiveMessagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public Messages[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((Messages[])(this.results[0]));
        }
    }
}
