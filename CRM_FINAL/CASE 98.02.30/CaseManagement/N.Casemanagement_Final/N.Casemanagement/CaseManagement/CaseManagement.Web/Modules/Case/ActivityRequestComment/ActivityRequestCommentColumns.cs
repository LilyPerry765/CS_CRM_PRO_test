
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ActivityRequestComment")]
    [BasedOnRow(typeof(Entities.ActivityRequestCommentRow))]
    public class ActivityRequestCommentColumns
    {        
        [EditLink, Width(400) , AlignRight]
        public String Comment { get; set; }
        [Width(150)]
        public string CreatedUserName { get; set; }
        [Width(100)]
        public DateTime CreatedDate { get; set; }
    }
}