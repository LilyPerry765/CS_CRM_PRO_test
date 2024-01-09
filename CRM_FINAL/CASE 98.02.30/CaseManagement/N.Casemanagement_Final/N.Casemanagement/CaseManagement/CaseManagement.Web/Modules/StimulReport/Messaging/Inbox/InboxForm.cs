
namespace CaseManagement.Messaging.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Messaging.Inbox")]
    [BasedOnRow(typeof(Entities.InboxRow))]
    public class InboxForm
    {
        public String SenderDisplayName { get; set; }

        [TextAreaEditor(Rows = 2)]
        public String MessageSubject { get; set; }
      
        [TextAreaEditor(Rows = 10)]
        public String MessageBody { get; set; }        
        public String MessageFile { get; set; }        
       // [Category("مشاهده")]
       // public Boolean Seen { get; set; }
        
    }
}