
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestDetailsInfo")]
    [BasedOnRow(typeof(Entities.ActivityRequestDetailsInfoRow))]
    public class ActivityRequestDetailsInfoForm
    {
        public Int64 Id { get; set; }
        public Int32 ProvinceId { get; set; }
        public Int32 ActivityId { get; set; }
        public Int32 CycleId { get; set; }
        public Int32 IncomeFlowId { get; set; }
        public Int32 Count { get; set; }
        public Int64 CycleCost { get; set; }
        public Int64 Factor { get; set; }
        public Int64 DelayedCost { get; set; }
        public Int64 YearCost { get; set; }
        public Int64 AccessibleCost { get; set; }
        public Int64 InaccessibleCost { get; set; }
        public Int64 TotalLeakage { get; set; }
        public Int64 RecoverableLeakage { get; set; }
        public Int64 Recovered { get; set; }
        public Int64 DelayedCostHistory { get; set; }
        public Int64 YearCostHistory { get; set; }
        public Int64 AccessibleCostHistory { get; set; }
        public Int64 InaccessibleCostHistory { get; set; }
        public Int32 RejectCount { get; set; }
        public String EventDescription { get; set; }
        public String MainReason { get; set; }
        public String CycleName { get; set; }
        public String Name { get; set; }
        public String Expr1 { get; set; }
        public String CodeName { get; set; }
    }
}