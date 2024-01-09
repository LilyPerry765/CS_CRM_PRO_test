
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ActivityRequestHistory")]
    [BasedOnRow(typeof(Entities.ActivityRequestHistoryRow))]
    public class ActivityRequestHistoryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int64 Id { get; set; }
    }
}