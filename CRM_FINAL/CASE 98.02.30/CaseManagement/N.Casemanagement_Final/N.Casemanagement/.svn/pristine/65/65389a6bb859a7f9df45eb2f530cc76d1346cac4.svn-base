

namespace CaseManagement.Case.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("ActivityRequest"), InstanceName("ActivityRequest"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    [LookupScript("Case.ActivityRequestHistory")]
    public sealed class ActivityRequestHistoryRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity, QuickSearch]
        public Int64? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("مبلغ سیکل")]
        public Int64? CycleCostHistory
        {
            get { return Fields.CycleCostHistory[this]; }
            set { Fields.CycleCostHistory[this] = value; }
        }

        [DisplayName("مبلغ معوق")]
        public Int64? DelayedCostHistory
        {
            get { return Fields.DelayedCostHistory[this]; }
            set { Fields.DelayedCostHistory[this] = value; }
        }

        [DisplayName("مبلغ سال")]
        public Int64? YearCostHistory
        {
            get { return Fields.YearCostHistory[this]; }
            set { Fields.YearCostHistory[this] = value; }
        }

        [DisplayName("مبلغ معوق قابل وصول")]
        public Int64? AccessibleCostHistory
        {
            get { return Fields.AccessibleCostHistory[this]; }
            set { Fields.AccessibleCostHistory[this] = value; }
        }

        [DisplayName("مبلغ معوق غیرقابل وصول")]
        public Int64? InaccessibleCostHistory
        {
            get { return Fields.InaccessibleCostHistory[this]; }
            set { Fields.InaccessibleCostHistory[this] = value; }
        }

        [DisplayName("نشتی شناسایی شده کل")]
        public Int64? TotalLeakageHistory
        {
            get { return Fields.TotalLeakageHistory[this]; }
            set { Fields.TotalLeakageHistory[this] = value; }
        }

        [DisplayName("نشتی شناسایی شده قابل وصول")]
        public Int64? RecoverableLeakageHistory
        {
            get { return Fields.RecoverableLeakageHistory[this]; }
            set { Fields.RecoverableLeakageHistory[this] = value; }
        }

        [DisplayName("مبلغ مصوب")]
        public Int64? RecoveredHistory
        {
            get { return Fields.RecoveredHistory[this]; }
            set { Fields.RecoveredHistory[this] = value; }
        }       

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }
                
        public static readonly RowFields Fields = new RowFields().Init();

        public ActivityRequestHistoryRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int64Field Id;
            public Int64Field CycleCostHistory;
            public Int64Field DelayedCostHistory;
            public Int64Field YearCostHistory;
            public Int64Field AccessibleCostHistory;
            public Int64Field InaccessibleCostHistory;
            public Int64Field TotalLeakageHistory;
            public Int64Field RecoverableLeakageHistory;
            public Int64Field RecoveredHistory;

            public RowFields()
                : base("[dbo].[ActivityRequest]")
            {
                LocalTextPrefix = "Case.ActivityRequestHistory";
            }
        }
    }
}