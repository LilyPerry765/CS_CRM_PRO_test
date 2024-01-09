
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestCommentReason")]
    [BasedOnRow(typeof(Entities.ActivityRequestCommentReasonRow))]
    public class ActivityRequestCommentReasonForm
    {
        public Int32 CommentReasonId { get; set; }
        public Int32 ActivityRequestId { get; set; }
    }
}