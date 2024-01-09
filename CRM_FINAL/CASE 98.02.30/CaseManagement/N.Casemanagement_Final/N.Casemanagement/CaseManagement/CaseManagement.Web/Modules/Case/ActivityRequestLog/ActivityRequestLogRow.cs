

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

    [ConnectionKey("Default"), DisplayName("رویداد"), InstanceName("رویداد"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class ActivityRequestLogRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Activity Request"), Column("ActivityRequestID"), NotNull, ForeignKey("[dbo].[ActivityRequest]", "ID"), LeftJoin("jActivityRequest"), TextualField("ActivityRequestEventDescription")]
        public Int64? ActivityRequestId
        {
            get { return Fields.ActivityRequestId[this]; }
            set { Fields.ActivityRequestId[this] = value; }
        }

        [DisplayName("وضعیت"), Column("StatusID"), NotNull, ForeignKey("[dbo].[WorkFlowStatus]", "ID"), LeftJoin("jStatus"), TextualField("StatusName")]
        public Int32? StatusId
        {
            get { return Fields.StatusId[this]; }
            set { Fields.StatusId[this] = value; }
        }

        //[DisplayName("Action Id"), Column("ActionID"), NotNull]
        //public Int32? ActionId
        //{
        //    get { return Fields.ActionId[this]; }
        //    set { Fields.ActionId[this] = value; }
        //}

        [DisplayName("عملیات")]
        public RequestAction? ActionID
        {
            get { return (RequestAction?)Fields.ActionID[this]; }
            set { Fields.ActionID[this] = (Int32?)value; }
        }

        [DisplayName("User"), Column("UserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUser"), TextualField("UserUsername")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("تاریخ"), NotNull]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }

        [DisplayName("وضعیت"), Expression("jStatus.[Name]")]
        public String StatusName
        {
            get { return Fields.StatusName[this]; }
            set { Fields.StatusName[this] = value; }
        }       

        [DisplayName("کاربر"), Expression("jUser.[DisplayName]")]
        public String UserDisplayName
        {
            get { return Fields.UserDisplayName[this]; }
            set { Fields.UserDisplayName[this] = value; }
        }     

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ActivityRequestLogRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int64Field ActivityRequestId;
            public Int32Field StatusId;
            public Int32Field ActionID;
            public Int32Field UserId;
            public DateTimeField InsertDate;

            public StringField StatusName;
            public StringField UserDisplayName;

            public RowFields()
                : base("[dbo].[ActivityRequestLog]")
            {
                LocalTextPrefix = "Case.ActivityRequestLog";
            }
        }
    }
}