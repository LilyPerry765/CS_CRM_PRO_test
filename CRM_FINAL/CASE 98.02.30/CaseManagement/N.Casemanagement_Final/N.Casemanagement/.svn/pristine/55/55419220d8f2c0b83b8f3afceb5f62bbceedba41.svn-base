
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ActivityRequestDeny")]
    [BasedOnRow(typeof(Entities.ActivityRequestDenyRow))]
    public class ActivityRequestDenyColumns
    {
        [EditLink,Width(120), DisplayName("شناسه"), AlignRight, MinuteFormatter]
        public Int64 Id { get; set; }
        [EditLink,Width(80), MinuteFormatter, QuickFilter]
        public string ActivityCode { get; set; }
       // [ Width(400), QuickFilter]
       // public string ActivityName { get; set; }
        [Width(130), QuickFilter]
        public string ProvinceName { get; set; }
        [Width(50), QuickFilter, MinuteFormatter]
        public string CycleName { get; set; }
        [Width(100), QuickFilter]
        public string IncomeFlowName { get; set; }
        [Width(166), NumberFormatter]
        public string TotalLeakage { get; set; }
        [AlignRight, Width(105), QuickFilter]
        public DateTime CreatedDate { get; set; }
        [AlignRight, Width(150), QuickFilter]
        public DateTime DiscoverLeakDate { get; set; }
        [Width(150)]
        public string CreatedUserName { get; set; }
        [Width(150)]
        public string SendUserName { get; set; }
        [AlignRight, Width(100)]
        public DateTime SendDate { get; set; }
        [Width(270)]
        public string StatusName { get; set; }         
        
    }
}