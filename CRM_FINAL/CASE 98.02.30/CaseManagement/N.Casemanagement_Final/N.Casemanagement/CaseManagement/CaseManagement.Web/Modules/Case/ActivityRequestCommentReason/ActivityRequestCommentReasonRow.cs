﻿

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

    [ConnectionKey("Default"), DisplayName("ActivityRequestCommentReason"), InstanceName("ActivityRequestCommentReason"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class ActivityRequestCommentReasonRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Comment Reason"), Column("CommentReasonID"), NotNull, ForeignKey("[dbo].[CommentReason]", "ID"), LeftJoin("jCommentReason"), TextualField("CommentReasonComment")]
        public Int32? CommentReasonId
        {
            get { return Fields.CommentReasonId[this]; }
            set { Fields.CommentReasonId[this] = value; }
        }

        [DisplayName("Activity Request"), Column("ActivityRequestID"), NotNull, ForeignKey("[dbo].[ActivityRequest]", "ID"), LeftJoin("jActivityRequest"), TextualField("ActivityRequestDiscoverLeakDateShamsi")]
        public Int64? ActivityRequestId
        {
            get { return Fields.ActivityRequestId[this]; }
            set { Fields.ActivityRequestId[this] = value; }
        }

        [DisplayName("Comment Reason Comment"), Expression("jCommentReason.[Comment]")]
        public String CommentReasonComment
        {
            get { return Fields.CommentReasonComment[this]; }
            set { Fields.CommentReasonComment[this] = value; }
        }

        [DisplayName("Comment Reason Created User Id"), Expression("jCommentReason.[CreatedUserID]")]
        public Int32? CommentReasonCreatedUserId
        {
            get { return Fields.CommentReasonCreatedUserId[this]; }
            set { Fields.CommentReasonCreatedUserId[this] = value; }
        }

        [DisplayName("Comment Reason Created Date"), Expression("jCommentReason.[CreatedDate]")]
        public DateTime? CommentReasonCreatedDate
        {
            get { return Fields.CommentReasonCreatedDate[this]; }
            set { Fields.CommentReasonCreatedDate[this] = value; }
        }

        [DisplayName("Activity Request Old Case Id"), Expression("jActivityRequest.[OldCaseID]")]
        public Int32? ActivityRequestOldCaseId
        {
            get { return Fields.ActivityRequestOldCaseId[this]; }
            set { Fields.ActivityRequestOldCaseId[this] = value; }
        }

        [DisplayName("Activity Request Province Id"), Expression("jActivityRequest.[ProvinceID]")]
        public Int32? ActivityRequestProvinceId
        {
            get { return Fields.ActivityRequestProvinceId[this]; }
            set { Fields.ActivityRequestProvinceId[this] = value; }
        }

        [DisplayName("Activity Request Activity Id"), Expression("jActivityRequest.[ActivityID]")]
        public Int32? ActivityRequestActivityId
        {
            get { return Fields.ActivityRequestActivityId[this]; }
            set { Fields.ActivityRequestActivityId[this] = value; }
        }

        [DisplayName("Activity Request Cycle Id"), Expression("jActivityRequest.[CycleID]")]
        public Int32? ActivityRequestCycleId
        {
            get { return Fields.ActivityRequestCycleId[this]; }
            set { Fields.ActivityRequestCycleId[this] = value; }
        }

        [DisplayName("Activity Request Customer Effect Id"), Expression("jActivityRequest.[CustomerEffectID]")]
        public Int32? ActivityRequestCustomerEffectId
        {
            get { return Fields.ActivityRequestCustomerEffectId[this]; }
            set { Fields.ActivityRequestCustomerEffectId[this] = value; }
        }

      
        [DisplayName("Activity Request Risk Level Id"), Expression("jActivityRequest.[RiskLevelID]")]
        public Int32? ActivityRequestRiskLevelId
        {
            get { return Fields.ActivityRequestRiskLevelId[this]; }
            set { Fields.ActivityRequestRiskLevelId[this] = value; }
        }

        [DisplayName("Activity Request Income Flow Id"), Expression("jActivityRequest.[IncomeFlowID]")]
        public Int32? ActivityRequestIncomeFlowId
        {
            get { return Fields.ActivityRequestIncomeFlowId[this]; }
            set { Fields.ActivityRequestIncomeFlowId[this] = value; }
        }

        [DisplayName("Activity Request Count"), Expression("jActivityRequest.[Count]")]
        public Int32? ActivityRequestCount
        {
            get { return Fields.ActivityRequestCount[this]; }
            set { Fields.ActivityRequestCount[this] = value; }
        }

        [DisplayName("Activity Request Cycle Cost"), Expression("jActivityRequest.[CycleCost]")]
        public Int64? ActivityRequestCycleCost
        {
            get { return Fields.ActivityRequestCycleCost[this]; }
            set { Fields.ActivityRequestCycleCost[this] = value; }
        }

        [DisplayName("Activity Request Factor"), Expression("jActivityRequest.[Factor]")]
        public Int32? ActivityRequestFactor
        {
            get { return Fields.ActivityRequestFactor[this]; }
            set { Fields.ActivityRequestFactor[this] = value; }
        }

        [DisplayName("Activity Request Delayed Cost"), Expression("jActivityRequest.[DelayedCost]")]
        public Int64? ActivityRequestDelayedCost
        {
            get { return Fields.ActivityRequestDelayedCost[this]; }
            set { Fields.ActivityRequestDelayedCost[this] = value; }
        }

        [DisplayName("Activity Request Year Cost"), Expression("jActivityRequest.[YearCost]")]
        public Int64? ActivityRequestYearCost
        {
            get { return Fields.ActivityRequestYearCost[this]; }
            set { Fields.ActivityRequestYearCost[this] = value; }
        }

        [DisplayName("Activity Request Accessible Cost"), Expression("jActivityRequest.[AccessibleCost]")]
        public Int64? ActivityRequestAccessibleCost
        {
            get { return Fields.ActivityRequestAccessibleCost[this]; }
            set { Fields.ActivityRequestAccessibleCost[this] = value; }
        }

        [DisplayName("Activity Request Inaccessible Cost"), Expression("jActivityRequest.[InaccessibleCost]")]
        public Int64? ActivityRequestInaccessibleCost
        {
            get { return Fields.ActivityRequestInaccessibleCost[this]; }
            set { Fields.ActivityRequestInaccessibleCost[this] = value; }
        }

        [DisplayName("Activity Request Financial"), Expression("jActivityRequest.[Financial]")]
        public Int64? ActivityRequestFinancial
        {
            get { return Fields.ActivityRequestFinancial[this]; }
            set { Fields.ActivityRequestFinancial[this] = value; }
        }

        [DisplayName("Activity Request Leak Date"), Expression("jActivityRequest.[LeakDate]")]
        public DateTime? ActivityRequestLeakDate
        {
            get { return Fields.ActivityRequestLeakDate[this]; }
            set { Fields.ActivityRequestLeakDate[this] = value; }
        }

        [DisplayName("Activity Request Discover Leak Date"), Expression("jActivityRequest.[DiscoverLeakDate]")]
        public DateTime? ActivityRequestDiscoverLeakDate
        {
            get { return Fields.ActivityRequestDiscoverLeakDate[this]; }
            set { Fields.ActivityRequestDiscoverLeakDate[this] = value; }
        }

        [DisplayName("Activity Request Discover Leak Date Shamsi"), Expression("jActivityRequest.[DiscoverLeakDateShamsi]")]
        public String ActivityRequestDiscoverLeakDateShamsi
        {
            get { return Fields.ActivityRequestDiscoverLeakDateShamsi[this]; }
            set { Fields.ActivityRequestDiscoverLeakDateShamsi[this] = value; }
        }

        [DisplayName("Activity Request Event Description"), Expression("jActivityRequest.[EventDescription]")]
        public String ActivityRequestEventDescription
        {
            get { return Fields.ActivityRequestEventDescription[this]; }
            set { Fields.ActivityRequestEventDescription[this] = value; }
        }

        [DisplayName("Activity Request Main Reason"), Expression("jActivityRequest.[MainReason]")]
        public String ActivityRequestMainReason
        {
            get { return Fields.ActivityRequestMainReason[this]; }
            set { Fields.ActivityRequestMainReason[this] = value; }
        }

        [DisplayName("Activity Request Correction Operation"), Expression("jActivityRequest.[CorrectionOperation]")]
        public String ActivityRequestCorrectionOperation
        {
            get { return Fields.ActivityRequestCorrectionOperation[this]; }
            set { Fields.ActivityRequestCorrectionOperation[this] = value; }
        }

        [DisplayName("Activity Request Avoid Repeating Operation"), Expression("jActivityRequest.[AvoidRepeatingOperation]")]
        public String ActivityRequestAvoidRepeatingOperation
        {
            get { return Fields.ActivityRequestAvoidRepeatingOperation[this]; }
            set { Fields.ActivityRequestAvoidRepeatingOperation[this] = value; }
        }

        [DisplayName("Activity Request Created User Id"), Expression("jActivityRequest.[CreatedUserID]")]
        public Int32? ActivityRequestCreatedUserId
        {
            get { return Fields.ActivityRequestCreatedUserId[this]; }
            set { Fields.ActivityRequestCreatedUserId[this] = value; }
        }

        [DisplayName("Activity Request Created Date"), Expression("jActivityRequest.[CreatedDate]")]
        public DateTime? ActivityRequestCreatedDate
        {
            get { return Fields.ActivityRequestCreatedDate[this]; }
            set { Fields.ActivityRequestCreatedDate[this] = value; }
        }

        [DisplayName("Activity Request Created Date Shamsi"), Expression("jActivityRequest.[CreatedDateShamsi]")]
        public String ActivityRequestCreatedDateShamsi
        {
            get { return Fields.ActivityRequestCreatedDateShamsi[this]; }
            set { Fields.ActivityRequestCreatedDateShamsi[this] = value; }
        }

        [DisplayName("Activity Request Modified User Id"), Expression("jActivityRequest.[ModifiedUserID]")]
        public Int32? ActivityRequestModifiedUserId
        {
            get { return Fields.ActivityRequestModifiedUserId[this]; }
            set { Fields.ActivityRequestModifiedUserId[this] = value; }
        }

        [DisplayName("Activity Request Modified Date"), Expression("jActivityRequest.[ModifiedDate]")]
        public DateTime? ActivityRequestModifiedDate
        {
            get { return Fields.ActivityRequestModifiedDate[this]; }
            set { Fields.ActivityRequestModifiedDate[this] = value; }
        }

        [DisplayName("Activity Request Is Deleted"), Expression("jActivityRequest.[IsDeleted]")]
        public Boolean? ActivityRequestIsDeleted
        {
            get { return Fields.ActivityRequestIsDeleted[this]; }
            set { Fields.ActivityRequestIsDeleted[this] = value; }
        }

        [DisplayName("Activity Request Deleted User Id"), Expression("jActivityRequest.[DeletedUserID]")]
        public Int32? ActivityRequestDeletedUserId
        {
            get { return Fields.ActivityRequestDeletedUserId[this]; }
            set { Fields.ActivityRequestDeletedUserId[this] = value; }
        }

        [DisplayName("Activity Request Deleted Date"), Expression("jActivityRequest.[DeletedDate]")]
        public DateTime? ActivityRequestDeletedDate
        {
            get { return Fields.ActivityRequestDeletedDate[this]; }
            set { Fields.ActivityRequestDeletedDate[this] = value; }
        }

        [DisplayName("Activity Request End Date"), Expression("jActivityRequest.[EndDate]")]
        public DateTime? ActivityRequestEndDate
        {
            get { return Fields.ActivityRequestEndDate[this]; }
            set { Fields.ActivityRequestEndDate[this] = value; }
        }

        [DisplayName("Activity Request Status Id"), Expression("jActivityRequest.[StatusID]")]
        public Int32? ActivityRequestStatusId
        {
            get { return Fields.ActivityRequestStatusId[this]; }
            set { Fields.ActivityRequestStatusId[this] = value; }
        }

        [DisplayName("Activity Request Action Id"), Expression("jActivityRequest.[ActionID]")]
        public Int32? ActivityRequestActionId
        {
            get { return Fields.ActivityRequestActionId[this]; }
            set { Fields.ActivityRequestActionId[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ActivityRequestCommentReasonRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field CommentReasonId;
            public Int64Field ActivityRequestId;

            public StringField CommentReasonComment;
            public Int32Field CommentReasonCreatedUserId;
            public DateTimeField CommentReasonCreatedDate;

            public Int32Field ActivityRequestOldCaseId;
            public Int32Field ActivityRequestProvinceId;
            public Int32Field ActivityRequestActivityId;
            public Int32Field ActivityRequestCycleId;
            public Int32Field ActivityRequestCustomerEffectId;
          
            public Int32Field ActivityRequestRiskLevelId;
            public Int32Field ActivityRequestIncomeFlowId;
            public Int32Field ActivityRequestCount;
            public Int64Field ActivityRequestCycleCost;
            public Int32Field ActivityRequestFactor;
            public Int64Field ActivityRequestDelayedCost;
            public Int64Field ActivityRequestYearCost;
            public Int64Field ActivityRequestAccessibleCost;
            public Int64Field ActivityRequestInaccessibleCost;
            public Int64Field ActivityRequestFinancial;
            public DateTimeField ActivityRequestLeakDate;
            public DateTimeField ActivityRequestDiscoverLeakDate;
            public StringField ActivityRequestDiscoverLeakDateShamsi;
            public StringField ActivityRequestEventDescription;
            public StringField ActivityRequestMainReason;
            public StringField ActivityRequestCorrectionOperation;
            public StringField ActivityRequestAvoidRepeatingOperation;
            public Int32Field ActivityRequestCreatedUserId;
            public DateTimeField ActivityRequestCreatedDate;
            public StringField ActivityRequestCreatedDateShamsi;
            public Int32Field ActivityRequestModifiedUserId;
            public DateTimeField ActivityRequestModifiedDate;
            public BooleanField ActivityRequestIsDeleted;
            public Int32Field ActivityRequestDeletedUserId;
            public DateTimeField ActivityRequestDeletedDate;
            public DateTimeField ActivityRequestEndDate;
            public Int32Field ActivityRequestStatusId;
            public Int32Field ActivityRequestActionId;

            public RowFields()
                : base("[dbo].[ActivityRequestCommentReason]")
            {
                LocalTextPrefix = "Case.ActivityRequestCommentReason";
            }
        }
    }
}