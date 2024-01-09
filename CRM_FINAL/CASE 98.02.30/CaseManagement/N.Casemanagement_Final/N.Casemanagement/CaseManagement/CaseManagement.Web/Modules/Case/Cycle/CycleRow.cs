

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

    [ConnectionKey("Default"), DisplayName("دوره"), InstanceName("دوره"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    [LookupScript("Case.Cycle")]
    public sealed class CycleRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("سال"), Column("YearID"), ForeignKey("[dbo].[Year]", "ID"), LeftJoin("jYear"), TextualField("Year")]
        [LookupEditor("Case.Year")]
        public Int32? YearId
        {
            get { return Fields.YearId[this]; }
            set { Fields.YearId[this] = value; }
        }

        [DisplayName("دوره"), NotNull, LookupInclude]
        public Int32? Cycle
        {
            get { return Fields.Cycle[this]; }
            set { Fields.Cycle[this] = value; }
        }

        [DisplayName("عنوان")]
        public string CycleName
        {
            get { return Fields.CycleName[this]; }
            set { Fields.CycleName[this] = value; }
        }

        [DisplayName("سال"), Expression("jYear.[Year]")]
        public String Year
        {
            get { return Fields.Year[this]; }
            set { Fields.Year[this] = value; }
        }

        [DisplayName("Created User Id"), Column("CreatedUserID"), NotNull]
        public Int32? CreatedUserId
        {
            get { return Fields.CreatedUserId[this]; }
            set { Fields.CreatedUserId[this] = value; }
        }

        [DisplayName("فعال"), NotNull]
        public Boolean? IsEnabled
        {
            get { return Fields.IsEnabled[this]; }
            set { Fields.IsEnabled[this] = value; }
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

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.CycleName; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public CycleRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field YearId;
            public Int32Field Cycle;
            public StringField CycleName;
            public BooleanField IsEnabled;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;

            public StringField Year;

            public RowFields()
                : base("[dbo].[Cycle]")
            {
                LocalTextPrefix = "Case.Cycle";
            }
        }
    }
}