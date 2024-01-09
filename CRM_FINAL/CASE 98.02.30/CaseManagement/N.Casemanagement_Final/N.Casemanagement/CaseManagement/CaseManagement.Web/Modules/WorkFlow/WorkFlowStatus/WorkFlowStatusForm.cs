﻿
namespace CaseManagement.WorkFlow.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("WorkFlow.WorkFlowStatus")]
    [BasedOnRow(typeof(Entities.WorkFlowStatusRow))]
    public class WorkFlowStatusForm
    {
        
        //public string Name { get; set; }
        //public Int32 CreatedUserId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public Int32 ModifiedUserId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public Boolean IsDeleted { get; set; }
        //public Int32 DeletedUserId { get; set; }
        //public DateTime DeletedDate { get; set; }
        public Int32 StepId { get; set; }
        public Int32 StatusTypeId { get; set; }
    }
}