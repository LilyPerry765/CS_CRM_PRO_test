
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ActivityRequestConfirm")]
    [BasedOnRow(typeof(Entities.ActivityRequestConfirmRow))]
    public class ActivityRequestConfirmColumns
    {
        [EditLink, DisplayName("شناسه"), Width(120), AlignRight, MinuteFormatter]
        public Int64 Id { get; set; }
        [Width(100),MinuteFormatter, QuickFilter ]
        public string ActivityCode { get; set; }
      //  [Width(400), QuickFilter]
      //  public string ActivityName { get; set; }
        [Width(150), QuickFilter]
        public string ProvinceName { get; set; }
        [Width(100), QuickFilter, MinuteFormatter]
        public string CycleName { get; set; }
        [Width(100)]
        public string IncomeFlowName { get; set; }

        [AlignRight, Width(105), QuickFilter]
        public DateTime CreatedDate { get; set; }
        [AlignRight, Width(150), QuickFilter]
        public DateTime DiscoverLeakDate { get; set; }
        [AlignRight, Width(125)]
        public DateTime EndDate { get; set; }
        [Width(150), NumberFormatter]
        public Int64 CycleCost { get; set; }
        [Width(150), NumberFormatter]
        public Int64 DelayedCost { get; set; }
        [Width(150), NumberFormatter]
        public Int64 TotalLeakage { get; set; }
        [Width(150), NumberFormatter]
        public Int64 RecoverableLeakage { get; set; }
        [Width(150), NumberFormatter]
        public Int64 Recovered { get; set; }     
    }
}