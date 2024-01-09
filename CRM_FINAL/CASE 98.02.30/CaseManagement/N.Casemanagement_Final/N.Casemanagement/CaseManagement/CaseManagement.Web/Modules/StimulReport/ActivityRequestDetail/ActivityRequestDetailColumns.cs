
namespace CaseManagement.StimulReport.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("StimulReport.ActivityRequestDetail")]
    [BasedOnRow(typeof(Entities.ActivityRequestDetailRow))]
    public class ActivityRequestDetailColumns
    {
        [DisplayName("شناسه"), AlignRight, Width(120), MinuteFormatter]
        public Int64 Id { get; set; }
        [Width(90), MinuteFormatter, QuickFilter]
        public string ActivityCode { get; set; }        
        [Width(120), QuickFilter]
        public string ProvinceName { get; set; }
        [Width(80), QuickFilter, MinuteFormatter]
        public string CycleName { get; set; }
        [AlignRight, Width(105), QuickFilter]
        public DateTime CreatedDate { get; set; }
        [AlignRight, Width(150), QuickFilter]
        public DateTime DiscoverLeakDate { get; set; }
        [AlignRight, Width(105), QuickFilter]
        public DateTime EndDate { get; set; }
        [Width(150),NumberFormatter]
        public Int64 CycleCost { get; set; }
        [Width(150),NumberFormatter]
        public Int64 DelayedCost { get; set; }
        [Width(150),NumberFormatter]
        public Int64 TotalLeakage { get; set; }
        [Width(150),NumberFormatter]
        public Int64 RecoverableLeakage { get; set; }
        [Width(150),NumberFormatter]
        public Int64 Recovered { get; set; }
        [Width(270), QuickFilter]
        public String StatusName { get; set; }   
    }
}