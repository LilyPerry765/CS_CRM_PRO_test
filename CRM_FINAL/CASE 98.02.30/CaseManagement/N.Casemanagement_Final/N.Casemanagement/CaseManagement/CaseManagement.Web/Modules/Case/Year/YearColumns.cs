
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.Year")]
    [BasedOnRow(typeof(Entities.YearRow))]
    public class YearColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int32 Year { get; set; }
        public Int32 CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int32 ModifiedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int32 DeletedUserId { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}