
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ActivityGroup")]
    [BasedOnRow(typeof(Entities.ActivityGroupRow))]
    public class ActivityGroupColumns
    {
       // [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink]
        [Width(500)]        
        public String Name { get; set; }
        [Width(500)]
        public String EnglishName { get; set; }
        [Width(200)]
        public Int32 Code { get; set; }
        //public Int32 CreatedUserId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public Int32 ModifiedUserId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public Boolean IsDeleted { get; set; }
        //public Int32 DeletedUserId { get; set; }
        //public DateTime DeletedDate { get; set; }
    }
}