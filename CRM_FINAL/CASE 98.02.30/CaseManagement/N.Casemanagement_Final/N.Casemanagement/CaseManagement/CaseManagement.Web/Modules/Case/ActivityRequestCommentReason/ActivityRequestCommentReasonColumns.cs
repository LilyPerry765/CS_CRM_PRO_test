
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ActivityRequestCommentReason")]
    [BasedOnRow(typeof(Entities.ActivityRequestCommentReasonRow))]
    public class ActivityRequestCommentReasonColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int32 CommentReasonId { get; set; }
        public Int32 ActivityRequestId { get; set; }
    }
}