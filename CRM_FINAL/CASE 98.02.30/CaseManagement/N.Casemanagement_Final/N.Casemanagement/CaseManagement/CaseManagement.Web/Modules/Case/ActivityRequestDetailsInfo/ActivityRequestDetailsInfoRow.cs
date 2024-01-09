

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

    [ConnectionKey("Default"), DisplayName("ActivityRequestDetailsInfo"), InstanceName("ActivityRequestInfo"), TwoLevelCached]
   
    [ReadPermission(CaseManagement.Administration.PermissionKeys.All)]
    [ModifyPermission(CaseManagement.Administration.PermissionKeys.All)]
    public sealed class ActivityRequestDetailsInfoRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), NotNull]
        public Int64? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Province Id"), Column("ProvinceID")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [DisplayName("Activity Id"), Column("ActivityID"), NotNull]
        public Int32? ActivityId
        {
            get { return Fields.ActivityId[this]; }
            set { Fields.ActivityId[this] = value; }
        }

        [DisplayName("Cycle Id"), Column("CycleID")]
        public Int32? CycleId
        {
            get { return Fields.CycleId[this]; }
            set { Fields.CycleId[this] = value; }
        }

        [DisplayName("Income Flow Id"), Column("IncomeFlowID")]
        public Int32? IncomeFlowId
        {
            get { return Fields.IncomeFlowId[this]; }
            set { Fields.IncomeFlowId[this] = value; }
        }

        [DisplayName("Count")]
        public Int32? Count
        {
            get { return Fields.Count[this]; }
            set { Fields.Count[this] = value; }
        }

        [DisplayName("Cycle Cost")]
        public Int64? CycleCost
        {
            get { return Fields.CycleCost[this]; }
            set { Fields.CycleCost[this] = value; }
        }

        [DisplayName("Factor")]
        public Int64? Factor
        {
            get { return Fields.Factor[this]; }
            set { Fields.Factor[this] = value; }
        }

        [DisplayName("Delayed Cost")]
        public Int64? DelayedCost
        {
            get { return Fields.DelayedCost[this]; }
            set { Fields.DelayedCost[this] = value; }
        }

        [DisplayName("Year Cost")]
        public Int64? YearCost
        {
            get { return Fields.YearCost[this]; }
            set { Fields.YearCost[this] = value; }
        }

        [DisplayName("Accessible Cost")]
        public Int64? AccessibleCost
        {
            get { return Fields.AccessibleCost[this]; }
            set { Fields.AccessibleCost[this] = value; }
        }

        [DisplayName("Inaccessible Cost")]
        public Int64? InaccessibleCost
        {
            get { return Fields.InaccessibleCost[this]; }
            set { Fields.InaccessibleCost[this] = value; }
        }

        [DisplayName("Total Leakage")]
        public Int64? TotalLeakage
        {
            get { return Fields.TotalLeakage[this]; }
            set { Fields.TotalLeakage[this] = value; }
        }

        [DisplayName("Recoverable Leakage")]
        public Int64? RecoverableLeakage
        {
            get { return Fields.RecoverableLeakage[this]; }
            set { Fields.RecoverableLeakage[this] = value; }
        }

        [DisplayName("Recovered")]
        public Int64? Recovered
        {
            get { return Fields.Recovered[this]; }
            set { Fields.Recovered[this] = value; }
        }

        [DisplayName("Delayed Cost History")]
        public Int64? DelayedCostHistory
        {
            get { return Fields.DelayedCostHistory[this]; }
            set { Fields.DelayedCostHistory[this] = value; }
        }

        [DisplayName("Year Cost History")]
        public Int64? YearCostHistory
        {
            get { return Fields.YearCostHistory[this]; }
            set { Fields.YearCostHistory[this] = value; }
        }

        [DisplayName("Accessible Cost History")]
        public Int64? AccessibleCostHistory
        {
            get { return Fields.AccessibleCostHistory[this]; }
            set { Fields.AccessibleCostHistory[this] = value; }
        }

        [DisplayName("Inaccessible Cost History")]
        public Int64? InaccessibleCostHistory
        {
            get { return Fields.InaccessibleCostHistory[this]; }
            set { Fields.InaccessibleCostHistory[this] = value; }
        }

        [DisplayName("تاریخ شناسایی نشتی")]
        public DateTime? DiscoverLeakDate
        {
            get { return Fields.DiscoverLeakDate[this]; }
            set { Fields.DiscoverLeakDate[this] = value; }
        }

        [DisplayName("Reject Count")]
        public Int32? RejectCount
        {
            get { return Fields.RejectCount[this]; }
            set { Fields.RejectCount[this] = value; }
        }

        [DisplayName("Event Description"), QuickSearch]
        public String EventDescription
        {
            get { return Fields.EventDescription[this]; }
            set { Fields.EventDescription[this] = value; }
        }

        [DisplayName("Main Reason")]
        public String MainReason
        {
            get { return Fields.MainReason[this]; }
            set { Fields.MainReason[this] = value; }
        }

        [DisplayName("Cycle Name"), Size(255)]
        public String CycleName
        {
            get { return Fields.CycleName[this]; }
            set { Fields.CycleName[this] = value; }
        }

        [DisplayName("Name"), Size(1000), NotNull]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Expr1"), Size(1000), NotNull]
        public String Expr1
        {
            get { return Fields.Expr1[this]; }
            set { Fields.Expr1[this] = value; }
        }

        [DisplayName("Code Name"), Size(1000)]
        public String CodeName
        {
            get { return Fields.CodeName[this]; }
            set { Fields.CodeName[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.EventDescription; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ActivityRequestDetailsInfoRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int64Field Id;
            public Int32Field ProvinceId;
            public Int32Field ActivityId;
            public Int32Field CycleId;
            public Int32Field IncomeFlowId;
            public Int32Field Count;
            public Int64Field CycleCost;
            public Int64Field Factor;
            public Int64Field DelayedCost;
            public Int64Field YearCost;
            public Int64Field AccessibleCost;
            public Int64Field InaccessibleCost;
            public Int64Field TotalLeakage;
            public Int64Field RecoverableLeakage;
            public Int64Field Recovered;
            public Int64Field DelayedCostHistory;
            public Int64Field YearCostHistory;
            public Int64Field AccessibleCostHistory;
            public Int64Field InaccessibleCostHistory;
            public Int32Field RejectCount;
            public StringField EventDescription;
            public StringField MainReason;
            public StringField CycleName;
            public StringField Name;
            public StringField Expr1;
            public StringField CodeName;
            public DateTimeField DiscoverLeakDate;

            public RowFields()
                : base("[dbo].[ActivityRequestInfo]")
            {
                LocalTextPrefix = "Case.ActivityRequestDetailsInfo";
            }
        }
    }
}