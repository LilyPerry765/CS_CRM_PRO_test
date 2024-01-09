
namespace CaseManagement.Messaging.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Messaging.Inbox")]
    [BasedOnRow(typeof(Entities.InboxRow))]
    public class InboxColumns
    {        
        [EditLink, Width(200)]
        public String SenderDisplayName { get; set; }
        [EditLink, Width(300)]
        public String MessageSubject { get; set; }
        [Width(100)]
        public Boolean Seen { get; set; }
        [Width(200)]
        public DateTime SeenDate { get; set; }
    }
}