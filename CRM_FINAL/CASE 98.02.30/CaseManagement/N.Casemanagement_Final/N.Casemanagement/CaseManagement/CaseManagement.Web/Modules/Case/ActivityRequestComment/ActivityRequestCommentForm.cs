
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestComment")]
    [BasedOnRow(typeof(Entities.ActivityRequestCommentRow))]
    public class ActivityRequestCommentForm
    {
        [TextAreaEditor(Rows=10)]
        public String Comment { get; set; }
    }
}