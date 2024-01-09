
namespace ReportTemplateDB.Common.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Common.ReportTemplate")]
    [BasedOnRow(typeof(Entities.ReportTemplateRow))]
    public class ReportTemplateColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Stream Template { get; set; }
        [EditLink]
        public String Title { get; set; }
    }
}