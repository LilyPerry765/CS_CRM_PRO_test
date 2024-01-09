

namespace CaseManagement.WorkFlow.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("جریان کاری"), InstanceName("جریان کاری"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.Workflow)]
    [ModifyPermission(Case.PermissionKeys.Manager)]
    public sealed class WorkFlowRuleRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Created User Id"), Column("CreatedUserID"), NotNull]
        public Int32? CreatedUserId
        {
            get { return Fields.CreatedUserId[this]; }
            set { Fields.CreatedUserId[this] = value; }
        }

        [DisplayName("Created Date"), NotNull]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [DisplayName("Modified User Id"), Column("ModifiedUserID"), NotNull]
        public Int32? ModifiedUserId
        {
            get { return Fields.ModifiedUserId[this]; }
            set { Fields.ModifiedUserId[this] = value; }
        }

        [DisplayName("Modified Date"), NotNull]
        public DateTime? ModifiedDate
        {
            get { return Fields.ModifiedDate[this]; }
            set { Fields.ModifiedDate[this] = value; }
        }

        [DisplayName("Is Deleted"), NotNull]
        public Boolean? IsDeleted
        {
            get { return Fields.IsDeleted[this]; }
            set { Fields.IsDeleted[this] = value; }
        }

        [DisplayName("Deleted User Id"), Column("DeletedUserID")]
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

        [LookupEditor("WorkFlow.WorkFlowAction")]
        [DisplayName("عملیات"), Column("ActionID"), ForeignKey("[dbo].[WorkFlowAction]", "ID"), LeftJoin("jAction"), TextualField("ActionName")]
        public Int32? ActionId
        {
            get { return Fields.ActionId[this]; }
            set { Fields.ActionId[this] = value; }
        }

        [LookupEditor("WorkFlow.WorkFlowStatus")]
        [DisplayName("وضعیت اولیه"), Column("CurrentStatusID"), ForeignKey("[dbo].[WorkFlowStatus]", "ID"), LeftJoin("jCurrentStatus"), TextualField("CurrentStatusName")]
        public Int32? CurrentStatusId
        {
            get { return Fields.CurrentStatusId[this]; }
            set { Fields.CurrentStatusId[this] = value; }
        }

        [LookupEditor("WorkFlow.WorkFlowStatus")]
        [DisplayName("وضعیت  ثانویه"), Column("NextStatusID"), ForeignKey("[dbo].[WorkFlowStatus]", "ID"), LeftJoin("jNextStatus"), TextualField("NextStatusName")]
        public Int32? NextStatusId
        {
            get { return Fields.NextStatusId[this]; }
            set { Fields.NextStatusId[this] = value; }
        }

        [DisplayName("عملیات"), Expression("jAction.[Name]")]
        public String ActionName
        {
            get { return Fields.ActionName[this]; }
            set { Fields.ActionName[this] = value; }
        }

        [DisplayName("وضعیت اولیه"), Expression("jCurrentStatus.[Name]")]
        public String CurrentStatusName
        {
            get { return Fields.CurrentStatusName[this]; }
            set { Fields.CurrentStatusName[this] = value; }
        }

        [DisplayName("وضعیت  ثانویه"), Expression("jNextStatus.[Name]")]
        public String NextStatusName
        {
            get { return Fields.NextStatusName[this]; }
            set { Fields.NextStatusName[this] = value; }
        }

        [DisplayName("نسخه"), Column("Version"), NotNull]
        public Int32? Version
        {
            get { return Fields.Version[this]; }
            set { Fields.Version[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public WorkFlowRuleRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;
            public Int32Field ActionId;
            public Int32Field CurrentStatusId;
            public Int32Field NextStatusId;
            public Int32Field Version;

            public StringField ActionName;

            public StringField CurrentStatusName;

            public StringField NextStatusName;

            public RowFields()
                : base("[dbo].[WorkFlowRule]")
            {
                LocalTextPrefix = "WorkFlow.WorkFlowRule";
            }
        }
    }
}