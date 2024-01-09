

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

    [ConnectionKey("Default"), DisplayName("توضیحات"), InstanceName("توضیحات"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]    
    [LookupScript("Case.ActivityRequestComment")]
    public sealed class ActivityRequestCommentRow : Row, IIdRow, INameRow
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

        [LookupEditor("Administration.User")]
        [DisplayName("کاربر ایجاد کننده"), Column("CreatedUserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUsers"), TextualField("CreatedUserName"),LookupInclude]
        public Int32? CreatedUserId
        {
            get { return Fields.CreatedUserId[this]; }
            set { Fields.CreatedUserId[this] = value; }
        }

        [DisplayName("تاریخ"), NotNull]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [DisplayName("Activity Request"), Column("ActivityRequestID"), ForeignKey("[dbo].[ActivityRequest]", "ID"), LeftJoin("jActivityRequest")]
        public Int64? ActivityRequestId
        {
            get { return Fields.ActivityRequestId[this]; }
            set { Fields.ActivityRequestId[this] = value; }
        }

        [DisplayName("کاربر"), Expression("jUsers.[DisplayName]")]
        //[DisplayName("کاربر")]
        public String CreatedUserName
        {
            get { return Fields.CreatedUserName[this]; }
            set { Fields.CreatedUserName[this] = value; }
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

        public ActivityRequestCommentRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Comment;
            public Int64Field ActivityRequestId;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;            
            public StringField CreatedUserName;

            public RowFields()
                : base("[dbo].[ActivityRequestComment]")
            {
                LocalTextPrefix = "Case.ActivityRequestComment";
            }
        }
    }
}