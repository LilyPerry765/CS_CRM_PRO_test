
namespace CaseManagement.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.Notification")]
    [BasedOnRow(typeof(Entities.NotificationRow))]
    public class NotificationForm
    {
        public Int32 GroupId { get; set; }
        public Int32 UserId { get; set; }
        public String Message { get; set; }
    }
}