
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ProvinceProgram")]
    [BasedOnRow(typeof(Entities.ProvinceProgramRow))]
    public class ProvinceProgramForm
    {
        [Width(600)]
        public Int64 Program { get; set; }
        //[Width(600)]
        //public Int32 ActivityCount { get; set; }
        //public Int32 ActivityNonRepeatCount { get; set; }
        //public Int32 CreatedUserId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public Int32 ModifiedUserId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public Boolean IsDeleted { get; set; }
        //public Int32 DeletedUserId { get; set; }
        //public DateTime DeletedDate { get; set; }
        //[Width(600)]
        //public Int32 ProvinceId { get; set; }
        [Width(600)]
        public Int32 YearId { get; set; }
    }
}