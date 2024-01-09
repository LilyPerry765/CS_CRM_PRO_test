
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.CommentReason")]
    [BasedOnRow(typeof(Entities.CommentReasonRow))]
    public class CommentReasonColumns
    {
        
        [EditLink]
        public String Comment { get; set; }        
    }
}