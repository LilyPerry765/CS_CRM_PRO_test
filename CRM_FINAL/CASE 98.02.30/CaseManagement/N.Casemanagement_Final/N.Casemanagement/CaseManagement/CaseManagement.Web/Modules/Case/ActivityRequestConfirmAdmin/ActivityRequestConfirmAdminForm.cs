
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestConfirmAdmin")]
    [BasedOnRow(typeof(Entities.ActivityRequestConfirmAdminRow))]
    public class ActivityRequestConfirmAdminForm
    {
        [Category("استان")]
        [Insertable(false), Updatable(false)]
        public string ProvinceName { get; set; }
        [Insertable(false), Updatable(false)]
        public Int64 Id { get; set; }

        [Category("فعالیت")]

        [Insertable(false), Updatable(false)]
        public Int32 ActivityId { get; set; }

        [Category("عمومی")]

        [Insertable(false), Updatable(false)]
        public Int32 CycleId { get; set; }
        [Insertable(false), Updatable(false)]
        public Int32 IncomeFlowId { get; set; }
        [Insertable(false), Updatable(false)]
        public DateTime DiscoverLeakDate { get; set; }

        [Category("مبالغ ورودی")]
        [Insertable(false), Updatable(false)]
        public Int32 Count { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal CycleCost { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal Factor { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal DelayedCost { get; set; }
        [Insertable(false), Updatable(false)]
        public Decimal AccessibleCost { get; set; }
        [Insertable(false), Updatable(false)]
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
        [TextAreaEditor(Rows = 5), Insertable(false), Updatable(false)]        
        public string EventDescription { get; set; }
        [TextAreaEditor(Rows = 5), Insertable(false), Updatable(false)]
        public string MainReason { get; set; }
        //[TextAreaEditor(Rows = 5)]
        //public string CorrectionOperation { get; set; }
        //[TextAreaEditor(Rows = 5)]
        //public string AvoidRepeatingOperation { get; set; }
        [EditorType("Case.ActivityRequestComment")]
        public List<Entities.ActivityRequestCommentRow> CommnetList { get; set; }

        [Category("پیوست")]
        [Insertable(false), Updatable(false)]
        public String File1 { get; set; }
        [Insertable(false), Updatable(false)]
        public String File2 { get; set; }
        // public String File3 { get; set; }

        [Category("عملیات")]
        public RequestActionAdmin ActionID { get; set; }
    }
}