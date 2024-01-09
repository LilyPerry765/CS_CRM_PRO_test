
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityRequestTechnical")]
    [BasedOnRow(typeof(Entities.ActivityRequestTechnicalRow))]
    public class ActivityRequestTechnicalForm
    {
        [Category("استان")]
        [Insertable(false), Updatable(false)]
        public string ProvinceName { get; set; }
        [Insertable(false), Updatable(false)]
        public Int64 Id { get; set; }
        
        [Category("فعالیت")]
        public Int32 ActivityId { get; set; }

        [Category("عمومی")]
        public Int32 CycleId { get; set; }
        public Int32 IncomeFlowId { get; set; }
        public DateTime DiscoverLeakDate { get; set; }        

        [Category("مبالغ ورودی")]
        public Int32 Count { get; set; }
        public Decimal CycleCost { get; set; }
        [Insertable(false), Updatable(false)]
        public Int64 Factor { get; set; }
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
        //public bool FinancialControllerConfirm { get; set; }

        [Category("توضیحات")]
        //public List<Int32> CommentReasonList { get; set; }
        [Insertable(false), Updatable(false)]
        public String RejectCount { get; set; }

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

        [Category("عملیات")]
        
        public ConfirmType ConfirmTypeID { get; set; }
        public RequestAction ActionID { get; set; }
    }
}