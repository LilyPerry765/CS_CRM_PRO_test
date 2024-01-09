
namespace CaseManagement.StimulReport.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("StimulReport.ActivityRequestReport")]
    [BasedOnRow(typeof(Entities.ActivityRequestReportRow))]
    public class ActivityRequestReportColumns
    {
        [DisplayName("شناسه"), Width(120), AlignRight, MinuteFormatter]
        public Int64 Id { get; set; }
        [Width(100), MinuteFormatter, QuickFilter]
        public string ActivityCode { get; set; }
        [Width(150), QuickFilter]
        public string ProvinceName { get; set; }
        [Width(150), QuickFilter]
        public string IncomeFlowName { get; set; }
        
        [Width(100),  QuickFilter]
        public DateTime CreatedDate { get; set; }
        [Width(100), QuickFilter]
        public DateTime DiscoverLeakDate { get; set; }
        [Width(100)]
        public DateTime EndDate { get; set; }   
        [Width(150), NumberFormatter]
        public Int64 TotalLeakage { get; set; }
        [Width(150), NumberFormatter]
        public Int64 RecoverableLeakage { get; set; }
        [Width(150), NumberFormatter]
        public Int64 Recovered { get; set; }  
    }
}