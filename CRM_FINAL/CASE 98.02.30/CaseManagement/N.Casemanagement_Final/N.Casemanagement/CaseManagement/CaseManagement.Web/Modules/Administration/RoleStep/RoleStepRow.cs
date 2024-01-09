
namespace CaseManagement.Administration.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("RoleStep"), InstanceName("RoleStep"), TwoLevelCached]
    [ReadPermission(Administration.PermissionKeys.Permission)]
    [ModifyPermission(Administration.PermissionKeys.Permission)]
    public sealed class RoleStepRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Role"), Column("RoleID"), NotNull, ForeignKey("[dbo].[Roles]", "RoleId"), LeftJoin("jRole"), TextualField("RoleRoleName")]
        public Int32? RoleId
        {
            get { return Fields.RoleId[this]; }
            set { Fields.RoleId[this] = value; }
        }

        [DisplayName("Step"), Column("StepID"), NotNull, ForeignKey("[dbo].[WorkFlowStep]", "ID"), LeftJoin("jStep"), TextualField("StepName")]
        public Int32? StepId
        {
            get { return Fields.StepId[this]; }
            set { Fields.StepId[this] = value; }
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

        [DisplayName("Role Role Name"), Expression("jRole.[RoleName]")]
        public String RoleRoleName
        {
            get { return Fields.RoleRoleName[this]; }
            set { Fields.RoleRoleName[this] = value; }
        }

        [DisplayName("Step Name"), Expression("jStep.[Name]")]
        public String StepName
        {
            get { return Fields.StepName[this]; }
            set { Fields.StepName[this] = value; }
        }

        [DisplayName("Step Created User Id"), Expression("jStep.[CreatedUserID]")]
        public Int32? StepCreatedUserId
        {
            get { return Fields.StepCreatedUserId[this]; }
            set { Fields.StepCreatedUserId[this] = value; }
        }

        [DisplayName("Step Created Date"), Expression("jStep.[CreatedDate]")]
        public DateTime? StepCreatedDate
        {
            get { return Fields.StepCreatedDate[this]; }
            set { Fields.StepCreatedDate[this] = value; }
        }

        [DisplayName("Step Modified User Id"), Expression("jStep.[ModifiedUserID]")]
        public Int32? StepModifiedUserId
        {
            get { return Fields.StepModifiedUserId[this]; }
            set { Fields.StepModifiedUserId[this] = value; }
        }

        [DisplayName("Step Modified Date"), Expression("jStep.[ModifiedDate]")]
        public DateTime? StepModifiedDate
        {
            get { return Fields.StepModifiedDate[this]; }
            set { Fields.StepModifiedDate[this] = value; }
        }

        [DisplayName("Step Is Deleted"), Expression("jStep.[IsDeleted]")]
        public Boolean? StepIsDeleted
        {
            get { return Fields.StepIsDeleted[this]; }
            set { Fields.StepIsDeleted[this] = value; }
        }

        [DisplayName("Step Deleted User Id"), Expression("jStep.[DeletedUserID]")]
        public Int32? StepDeletedUserId
        {
            get { return Fields.StepDeletedUserId[this]; }
            set { Fields.StepDeletedUserId[this] = value; }
        }

        [DisplayName("Step Deleted Date"), Expression("jStep.[DeletedDate]")]
        public DateTime? StepDeletedDate
        {
            get { return Fields.StepDeletedDate[this]; }
            set { Fields.StepDeletedDate[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public RoleStepRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field RoleId;
            public Int32Field StepId;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;

            public StringField RoleRoleName;

            public StringField StepName;
            public Int32Field StepCreatedUserId;
            public DateTimeField StepCreatedDate;
            public Int32Field StepModifiedUserId;
            public DateTimeField StepModifiedDate;
            public BooleanField StepIsDeleted;
            public Int32Field StepDeletedUserId;
            public DateTimeField StepDeletedDate;

            public RowFields()
                : base("[dbo].[RoleStep]")
            {
                LocalTextPrefix = "Administration.RoleStep";
            }
        }
    }
}