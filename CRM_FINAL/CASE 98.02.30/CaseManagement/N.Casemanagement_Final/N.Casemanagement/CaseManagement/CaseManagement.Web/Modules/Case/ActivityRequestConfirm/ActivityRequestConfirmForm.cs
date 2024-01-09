﻿
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestConfirm")]
    [BasedOnRow(typeof(Entities.ActivityRequestConfirmRow))]
    public class ActivityRequestConfirmForm
    {      

        [Category("استان")]
        [Insertable(false), Updatable(false)]
        public string ProvinceName { get; set; }
        public Int64 Id { get; set; }

        [Category("فعالیت")]
        public Int32 ActivityId { get; set; }

        [Category("عمومی")]
        public Int32 CycleId { get; set; }
        public Int32 IncomeFlowId { get; set; }
        public DateTime DiscoverLeakDate { get; set; }

        [Category("سوابق فنی")]
        [Insertable(false), Updatable(false)]
        public Decimal CycleCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal DelayedCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal AccessibleCostHistory { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal InaccessibleCostHistory { get; set; }

        [Category("مبالغ ورودی")]
        public Int32 Count { get; set; }
        public Decimal CycleCost { get; set; }
        public Decimal Factor { get; set; }
        public Decimal DelayedCost { get; set; }
        public Decimal AccessibleCost { get; set; }
        public Decimal InaccessibleCost { get; set; }
        // public Int64 Financial { get; set; }

        [Category("مبالغ نتیجه")]
        [Insertable(false), Updatable(false)]
        public Decimal YearCost { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal TotalLeakage { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal RecoverableLeakage { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal Recovered { get; set; }

        [Category("توضیحات")]
        //public List<Int32> CommentReasonList { get; set; }
        [TextAreaEditor(Rows = 5)]
        public string EventDescription { get; set; }
        [TextAreaEditor(Rows = 5)]
        public string MainReason { get; set; }
        //[TextAreaEditor(Rows = 5)]
        //public string CorrectionOperation { get; set; }
        //[TextAreaEditor(Rows = 5)]
        //public string AvoidRepeatingOperation { get; set; }
        [EditorType("Case.ActivityRequestComment")]
        public List<Entities.ActivityRequestCommentRow> CommnetList { get; set; }

        [Category("پیوست")]
        public String File1 { get; set; }
        public String File2 { get; set; }
        // public String File3 { get; set; }
    }    
}