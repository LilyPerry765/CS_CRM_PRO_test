
namespace CaseManagement.WorkFlow.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("WorkFlow.WorkFlowRule")]
    [BasedOnRow(typeof(Entities.WorkFlowRuleRow))]
    public class WorkFlowRuleColumns
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
        [EditLink, Width(400)]
        public Int32 CurrentStatusName { get; set; }
        [EditLink, Width(200)]
        public Int32 ActionName { get; set; }
        [EditLink, Width(400)]
        public Int32 NextStatusName { get; set; }
        public Int32 Version { get; set; }
    }
}