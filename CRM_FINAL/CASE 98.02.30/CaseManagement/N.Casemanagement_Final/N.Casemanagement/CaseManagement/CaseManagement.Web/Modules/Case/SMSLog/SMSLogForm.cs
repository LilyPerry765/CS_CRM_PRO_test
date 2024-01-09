﻿
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.SMSLog")]
    [BasedOnRow(typeof(Entities.SMSLogRow))]
    public class SMSLogForm
    {
        //public DateTime InsertDate { get; set; }
        public Int64 ActivityRequestId { get; set; }
        //public Int32 MessageId { get; set; }
        //public Int32 SenderUserId { get; set; }
        //public String SenderUserName { get; set; }
        public Int32 ReceiverProvinceId { get; set; }
        //public Int32 ReceiverUserId { get; set; }
        public String ReceiverUserName { get; set; }
       // public String MobileNumber { get; set; }
        public String TextSent { get; set; }
       // public Boolean IsSent { get; set; }
       // public Boolean IsDelivered { get; set; }
       // public Int32 ReceiverRoleId { get; set; }
    }
}