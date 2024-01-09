
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestHistory")]
    [BasedOnRow(typeof(Entities.ActivityRequestHistoryRow))]
    public class ActivityRequestHistoryForm
    {
        [Insertable(false), Updatable(false)]
        public Decimal CycleCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal DelayedCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal YearCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal AccessibleCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal InaccessibleCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal TotalLeakageHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal RecoverableLeakageHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal RecoveredHistory { get; set; }
    }
}