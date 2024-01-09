

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

    [ConnectionKey("Default"), DisplayName("CommentReason"), InstanceName("CommentReason"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    [LookupScript("Case.CommentReason")]
    public sealed class CommentReasonRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("توضیحات"), NotNull, QuickSearch]
        public String Comment
        {
            get { return Fields.Comment[this]; }
            set { Fields.Comment[this] = value; }
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

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Comment; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public CommentReasonRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Comment;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;

            public RowFields()
                : base("[dbo].[CommentReason]")
            {
                LocalTextPrefix = "Case.CommentReason";
            }
        }
    }
}