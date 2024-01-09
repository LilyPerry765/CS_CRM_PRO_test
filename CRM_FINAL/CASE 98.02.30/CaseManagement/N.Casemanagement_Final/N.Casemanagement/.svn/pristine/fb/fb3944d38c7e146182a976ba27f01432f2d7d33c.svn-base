
namespace CaseManagement.Messaging.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Messaging.Sent")]
    [BasedOnRow(typeof(Entities.SentRow))]
    public class SentForm
    {
        public String RecieverDisplayName { get; set; }


        [TextAreaEditor(Rows = 2)]
        public String MessageSubject { get; set; }


        [TextAreaEditor(Rows = 10)]
        public String MessageBody { get; set; }

        public String MessageFile { get; set; }
    }
}