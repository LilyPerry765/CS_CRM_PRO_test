﻿
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.Software")]
    [BasedOnRow(typeof(Entities.SoftwareRow))]
    public class SoftwareColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public Int32 CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int32 ModifiedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int32 DeletedUserId { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}