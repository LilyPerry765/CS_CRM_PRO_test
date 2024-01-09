
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.CommentReason")]
    [BasedOnRow(typeof(Entities.CommentReasonRow))]
    public class CommentReasonForm
    {
        
        public String Comment { get; set; }
    }
}