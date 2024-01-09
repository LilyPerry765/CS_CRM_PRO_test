
namespace CaseManagement.WorkFlow.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("WorkFlow.WorkFlowStatus")]
    [BasedOnRow(typeof(Entities.WorkFlowStatusRow))]
    public class WorkFlowStatusColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        
        
        //public Int32 CreatedUserId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public Int32 ModifiedUserId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public Boolean IsDeleted { get; set; }
        //public Int32 DeletedUserId { get; set; }
        //public DateTime DeletedDate { get; set; }
        //public string FullName { get; set; }
        [EditLink]
        public string StepName { get; set; }
        [EditLink]
        public string StatusTypeName { get; set; }
    }
}