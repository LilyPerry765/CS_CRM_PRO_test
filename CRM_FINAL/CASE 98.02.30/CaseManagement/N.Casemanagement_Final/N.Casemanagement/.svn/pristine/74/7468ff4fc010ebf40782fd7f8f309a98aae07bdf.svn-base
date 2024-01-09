
namespace CaseManagement.Messaging.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Messaging.NewMessage")]
    [BasedOnRow(typeof(Entities.NewMessageRow))]
    public class NewMessageForm
    {
     //   public Int32 SenderId { get; set; }
        public List<Int32> ReceiverList { get; set; }
        [TextAreaEditor(Rows = 2)]
        public String Subject { get; set; }
        
        [TextAreaEditor(Rows = 10)]
        public String Body { get; set; }
        public String File { get; set; }
       // public DateTime InsertedDate { get; set; }
    }
}