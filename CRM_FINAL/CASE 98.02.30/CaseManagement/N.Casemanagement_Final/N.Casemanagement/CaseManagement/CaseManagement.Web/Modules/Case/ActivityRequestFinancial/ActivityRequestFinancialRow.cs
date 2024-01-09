

namespace CaseManagement.Case.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("گزارش مالی فعالیت"), InstanceName("گزارش مالی فعالیت"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class ActivityRequestFinancialRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity, QuickSearch, LookupInclude]
        public Int64? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("تعداد")]
        public Int32? Count
        {
            get { return Fields.Count[this]; }
            set { Fields.Count[this] = value; }
        }

        [DisplayName("مبلغ سیکل")]
        public Int64? CycleCost
        {
            get { return Fields.CycleCost[this]; }
            set { Fields.CycleCost[this] = value; }
        }

        [DisplayName("ضریب")]
        public Int64? Factor
        {
            get { return Fields.Factor[this]; }
            set { Fields.Factor[this] = value; }
        }

        [DisplayName("مبلغ معوق")]
        public Int64? DelayedCost
        {
            get { return Fields.DelayedCost[this]; }
            set { Fields.DelayedCost[this] = value; }
        }

        [DisplayName("مبلغ سال")]
        public Int64? YearCost
        {
            get { return Fields.YearCost[this]; }
            set { Fields.YearCost[this] = value; }
        }

        [DisplayName("مبلغ قابل وصول معوق")]
        public Int64? AccessibleCost
        {
            get { return Fields.AccessibleCost[this]; }
            set { Fields.AccessibleCost[this] = value; }
        }

        [DisplayName("مبلغ غیر قابل وصول معوق")]
        public Int64? InaccessibleCost
        {
            get { return Fields.InaccessibleCost[this]; }
            set { Fields.InaccessibleCost[this] = value; }
        }

        [DisplayName("مالی")]
        public Int64? Financial
        {
            get { return Fields.Financial[this]; }
            set { Fields.Financial[this] = value; }
        }

        [DisplayName("نشتی شناسایی شده کل")]
        public Int64? TotalLeakage
        {
            get { return Fields.TotalLeakage[this]; }
            set { Fields.TotalLeakage[this] = value; }
        }

        [DisplayName("نشتی شناسایی شده قابل وصول")]
        public Int64? RecoverableLeakage
        {
            get { return Fields.RecoverableLeakage[this]; }
            set { Fields.RecoverableLeakage[this] = value; }
        }

        [DisplayName("مبلغ مصوب")]
        public Int64? Recovered
        {
            get { return Fields.Recovered[this]; }
            set { Fields.Recovered[this] = value; }
        }

        [DisplayName("شرح رویداد")]
        public string EventDescription
        {
            get { return Fields.EventDescription[this]; }
            set { Fields.EventDescription[this] = value; }
        }

        [DisplayName("تحلیل علت اصلی")]
        //[LookupEditor(typeof(ActivityMainReasonRow), Multiple = true), ClientSide]
        //[LinkingSetRelation(typeof(ActivityRequestCommentReasonRow), "ActivityId", "CommentReasonId")]        
        public string MainReason
        {
            get { return Fields.MainReason[this]; }
            set { Fields.MainReason[this] = value; }
        }

        [DisplayName("عملیات اصلاح")]
        public string CorrectionOperation
        {
            get { return Fields.CorrectionOperation[this]; }
            set { Fields.CorrectionOperation[this] = value; }
        }

        [DisplayName("عملیات عدم تکرار")]
        public string AvoidRepeatingOperation
        {
            get { return Fields.AvoidRepeatingOperation[this]; }
            set { Fields.AvoidRepeatingOperation[this] = value; }
        }

        [LookupEditor("Administration.User")]
        [DisplayName("کاربر ایجاد کننده"), Column("CreatedUserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUsers1"), TextualField("CreatedUserName")]
        public Int32? CreatedUserId
        {
            get { return Fields.CreatedUserId[this]; }
            set { Fields.CreatedUserId[this] = value; }
        }

        [DisplayName("تاریخ ایجاد"), NotNull]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [LookupEditor("Administration.User")]
        [DisplayName("کاربر ویرایش کننده"), Column("ModifiedUserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUsers2"), TextualField("ModifiedUserName")]
        public Int32? ModifiedUserId
        {
            get { return Fields.ModifiedUserId[this]; }
            set { Fields.ModifiedUserId[this] = value; }
        }

        [DisplayName("تاریخ آخرین ویرایش"), NotNull]
        public DateTime? ModifiedDate
        {
            get { return Fields.ModifiedDate[this]; }
            set { Fields.ModifiedDate[this] = value; }
        }

        [DisplayName("تاریخ ارسال"), NotNull]
        public DateTime? SendDate
        {
            get { return Fields.SendDate[this]; }
            set { Fields.SendDate[this] = value; }
        }

        [LookupEditor("Administration.User")]
        [DisplayName("کاربر ارسال کننده"), Column("SendUserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUsers4"), TextualField("SendUserName")]
        public Int32? SendUserId
        {
            get { return Fields.SendUserId[this]; }
            set { Fields.SendUserId[this] = value; }
        }

        [DisplayName("Is Deleted"), NotNull]
        public Boolean? IsDeleted
        {
            get { return Fields.IsDeleted[this]; }
            set { Fields.IsDeleted[this] = value; }
        }

        [LookupEditor("Administration.User")]
        [DisplayName("Deleted User Id"), Column("DeletedUserID"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUsers3"), TextualField("DeletedUserName")]
        public Int32? DeletedUserId
        {
            get { return Fields.DeletedUserId[this]; }
            set { Fields.DeletedUserId[this] = value; }
        }

        [DisplayName("Deleted Date")]
        public DateTime? DeletedDate
        {
            get { return Fields.DeletedDate[this]; }
            set { Fields.DeletedDate[this] = value; }
        }

        [DisplayName("تاریخ نشتی")]
        public DateTime? LeakDate
        {
            get { return Fields.LeakDate[this]; }
            set { Fields.LeakDate[this] = value; }
        }

        [DisplayName("تاریخ شناسایی نشتی")]
        public DateTime? DiscoverLeakDate
        {
            get { return Fields.DiscoverLeakDate[this]; }
            set { Fields.DiscoverLeakDate[this] = value; }
        }

        [DisplayName("تاریخ اتمام")]
        public DateTime? EndDate
        {
            get { return Fields.EndDate[this]; }
            set { Fields.EndDate[this] = value; }
        }

        [LookupEditor("Case.ActivityHelp", InplaceAdd = true)]
        [DisplayName("فعالیت"), Column("ActivityID"), ForeignKey("[dbo].[Activity]", "ID"), LeftJoin("jActivity"), TextualField("ActivityName"), NotNull]
        public Int32? ActivityId
        {
            get { return Fields.ActivityId[this]; }
            set { Fields.ActivityId[this] = value; }
        }

        [LookupEditor("Case.Province")]
        [DisplayName("استان"), Column("ProvinceID"), ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvince"), TextualField("ProvinceName")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [LookupEditor("Case.Cycle")]
        [DisplayName("دوره"), Column("CycleID"), ForeignKey("[dbo].[Cycle]", "ID"), LeftJoin("jCycle"), TextualField("CycleName")]
        public Int32? CycleId
        {
            get { return Fields.CycleId[this]; }
            set { Fields.CycleId[this] = value; }
        }

        [LookupEditor("Case.CustomerEffect")]
        [DisplayName("اثر بر مشتری"), Column("CustomerEffectID"), ForeignKey("[dbo].[CustomerEffect]", "ID"), LeftJoin("jCustomerEffect"), TextualField("CustomerEffectName")]
        public Int32? CustomerEffectId
        {
            get { return Fields.CustomerEffectId[this]; }
            set { Fields.CustomerEffectId[this] = value; }
        }

      

        [LookupEditor("Case.IncomeFlow")]
        [DisplayName("جریان درآمدی"), Column("IncomeFlowID"), ForeignKey("[dbo].[IncomeFlow]", "ID"), LeftJoin("jIncomeFlow"), TextualField("IncomeFlowName")]
        public Int32? IncomeFlowId
        {
            get { return Fields.IncomeFlowId[this]; }
            set { Fields.IncomeFlowId[this] = value; }
        }

        [LookupEditor("Case.RiskLevel")]
        [DisplayName("سطح ریسک"), Column("RiskLevelID"), ForeignKey("[dbo].[RiskLevel]", "ID"), LeftJoin("jRiskLevel"), TextualField("RiskLevelName")]
        public Int32? RiskLevelId
        {
            get { return Fields.RiskLevelId[this]; }
            set { Fields.RiskLevelId[this] = value; }
        }

        [LookupEditor("Case.WorkFlowStatus")]
        [DisplayName("وضعیت"), Column("StatusID"), ForeignKey("[dbo].[WorkFlowStatus]", "ID"), LeftJoin("jWorkFlowStatus"), TextualField("StatusName")]
        public Int32? StatusID
        {
            get { return Fields.StatusID[this]; }
            set { Fields.StatusID[this] = value; }
        }

        [DisplayName("کاربر ایجاد کننده"), Expression("jUsers1.[DisplayName]")]
        public String CreatedUserName
        {
            get { return Fields.CreatedUserName[this]; }
            set { Fields.CreatedUserName[this] = value; }
        }

        [DisplayName("کاربر ویرایش کننده"), Expression("jUsers2.[DisplayName]")]
        public String ModifiedUserName
        {
            get { return Fields.ModifiedUserName[this]; }
            set { Fields.ModifiedUserName[this] = value; }
        }

        [DisplayName("کاربر حذف کننده"), Expression("jUsers3.[DisplayName]")]
        public String DeletedUserName
        {
            get { return Fields.DeletedUserName[this]; }
            set { Fields.DeletedUserName[this] = value; }
        }

        [DisplayName("کاربر ارسال کننده"), Expression("jUsers4.[DisplayName]")]
        public String SendUserName
        {
            get { return Fields.SendUserName[this]; }
            set { Fields.SendUserName[this] = value; }
        }

        [DisplayName("کد فعالیت")]
        public String ActivityCode
        {
            get { return Fields.ActivityCode[this]; }
            set { Fields.ActivityCode[this] = value; }
        }

        [DisplayName("عنوان فعالیت"), Expression("jActivity.[Name]")]
        public String ActivityName
        {
            get { return Fields.ActivityName[this]; }
            set { Fields.ActivityName[this] = value; }
        }

        [DisplayName("هدف فعالیت"), Expression("jActivity.[Objective]")]
        public String ActivityObjective
        {
            get { return Fields.ActivityObjective[this]; }
            set { Fields.ActivityObjective[this] = value; }
        }

        [DisplayName("Activity Group Id"), Expression("jActivity.[GroupID]")]
        public Int32? ActivityGroupId
        {
            get { return Fields.ActivityGroupId[this]; }
            set { Fields.ActivityGroupId[this] = value; }
        }

        [DisplayName("استان"), Expression("jProvince.[Name]")]
        public String ProvinceName
        {
            get { return Fields.ProvinceName[this]; }
            set { Fields.ProvinceName[this] = value; }
        }

        // [DisplayName("دوره"), Expression("jCycle.[Cycle]")]
        // public Int32? Cycle
        // {
        //     get { return Fields.Cycle[this]; }
        //     set { Fields.Cycle[this] = value; }
        // }

        [DisplayName("دوره"), Expression("jCycle.[CycleName]")]
        public String CycleName
        {
            get { return Fields.CycleName[this]; }
            set { Fields.CycleName[this] = value; }
        }

        [DisplayName("اثر بر مشتری"), Expression("jCustomerEffect.[Name]")]
        public String CustomerEffectName
        {
            get { return Fields.CustomerEffectName[this]; }
            set { Fields.CustomerEffectName[this] = value; }
        }

     

        [DisplayName("جریان درآمدی"), Expression("jIncomeFlow.[Name]")]
        public String IncomeFlowName
        {
            get { return Fields.IncomeFlowName[this]; }
            set { Fields.IncomeFlowName[this] = value; }
        }

        [DisplayName("سطح ریسک"), Expression("jRiskLevel.[Name]")]
        public String RiskLevelName
        {
            get { return Fields.RiskLevelName[this]; }
            set { Fields.RiskLevelName[this] = value; }
        }

        [DisplayName("وضعیت"), Expression("jWorkFlowStatus.[Name]")]
        public String StatusName
        {
            get { return Fields.StatusName[this]; }
            set { Fields.StatusName[this] = value; }
        }

        [DisplayName("توضیحات"), SetFieldFlags(FieldFlags.ClientSide)]
        public List<ActivityRequestCommentRow> CommnetList
        {
            get { return Fields.CommnetList[this]; }
            set { Fields.CommnetList[this] = value; }
        }

        [DisplayName("عملیات"), NotNull]
        public RequestAction? ActionID
        {
            get { return (RequestAction?)Fields.ActionID[this]; }
            set { Fields.ActionID[this] = (Int32?)value; }
        }

        [DisplayName("نوع تایید"), NotNull]
        public ConfirmType? ConfirmTypeID
        {
            get { return (ConfirmType?)Fields.ConfirmTypeID[this]; }
            set { Fields.ConfirmTypeID[this] = (Int32?)value; }
        }

        [DisplayName("Is Rejected"), NotNull]
        public Boolean? IsRejected
        {
            get { return Fields.IsRejected[this]; }
            set { Fields.IsRejected[this] = value; }
        }

        [DisplayName("تعداد دفعات بازگشت")]
        public Int32? RejectCount
        {
            get { return Fields.RejectCount[this]; }
            set { Fields.RejectCount[this] = value; }
        }

        [DisplayName("توضیحات")]
        [LookupEditor(typeof(CommentReasonRow), Multiple = true), ClientSide]
        [LinkingSetRelation(typeof(ActivityRequestCommentReasonRow), "ActivityRequestId", "CommentReasonId")]
        public List<Int32> CommentReasonList
        {
            get { return Fields.CommentReasonList[this]; }
            set { Fields.CommentReasonList[this] = value; }
        }

        [DisplayName("فایل 1"), Size(300)]
        [FileUploadEditor(FilenameFormat = "ActivityFile/~", CopyToHistory = true)]
        public String File1
        {
            get { return Fields.File1[this]; }
            set { Fields.File1[this] = value; }
        }

        [DisplayName("فایل 2"), Size(300)]
        [FileUploadEditor(FilenameFormat = "ActivityFile/~", CopyToHistory = true)]
        public String File2
        {
            get { return Fields.File2[this]; }
            set { Fields.File2[this] = value; }
        }

        [DisplayName("فایل 3"), Size(300)]
        [FileUploadEditor(FilenameFormat = "ActivityFile/~", CopyToHistory = true)]
        public String File3
        {
            get { return Fields.File3[this]; }
            set { Fields.File3[this] = value; }
        }

        [DisplayName("تایید ناظر مالی")]
        public Boolean? FinancialControllerConfirm
        {
            get { return Fields.FinancialControllerConfirm[this]; }
            set { Fields.FinancialControllerConfirm[this] = value; }
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

        public ActivityRequestFinancialRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int64Field Id;
            public Int32Field Count;
            public Int64Field CycleCost;
            public Int64Field Factor;
            public Int64Field DelayedCost;
            public Int64Field YearCost;
            public Int64Field AccessibleCost;
            public Int64Field InaccessibleCost;
            public Int64Field Financial;
            public Int64Field TotalLeakage;
            public Int64Field RecoverableLeakage;
            public Int64Field Recovered;
            public StringField EventDescription;
            public StringField MainReason;
            public StringField CorrectionOperation;
            public StringField AvoidRepeatingOperation;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public DateTimeField SendDate;
            public Int32Field SendUserId;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;
            public DateTimeField EndDate;
            public Int32Field ActivityId;
            public Int32Field ProvinceId;
            public Int32Field CycleId;
            public Int32Field CustomerEffectId;
           
            public Int32Field IncomeFlowId;
            public Int32Field RiskLevelId;
            public Int32Field StatusID;
            public DateTimeField LeakDate;
            public DateTimeField DiscoverLeakDate;

            public StringField ActivityCode;
            public StringField ActivityName;
            public StringField ActivityObjective;
            public Int32Field ActivityGroupId;
            public StringField ProvinceName;
            //public Int32Field CycleYear;
            // public Int32Field Cycle;
            public StringField CycleName;
            public StringField CustomerEffectName;
            
            public StringField IncomeFlowName;
            public StringField RiskLevelName;
            public StringField StatusName;
            public StringField CreatedUserName;
            public StringField ModifiedUserName;
            public StringField DeletedUserName;
            public StringField SendUserName;

            public Int32Field ActionID;
            public Int32Field ConfirmTypeID;
            public BooleanField IsRejected;
            public Int32Field RejectCount;
            public ListField<Int32> CommentReasonList;
            public readonly RowListField<ActivityRequestCommentRow> CommnetList;

            public StringField File1;
            public StringField File2;
            public StringField File3;

            public BooleanField FinancialControllerConfirm;

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
                LocalTextPrefix = "Case.ActivityRequestFinancial";
            }
        }
    }
}