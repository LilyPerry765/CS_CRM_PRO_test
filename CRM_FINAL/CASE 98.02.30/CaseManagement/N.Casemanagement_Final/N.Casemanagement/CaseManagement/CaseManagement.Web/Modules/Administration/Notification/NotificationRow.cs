

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

    [ConnectionKey("Default"), DisplayName("Notification"), InstanceName("Notification"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    public sealed class NotificationRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Group Id"), Column("GroupID")]
        public Int32? GroupId
        {
            get { return Fields.GroupId[this]; }
            set { Fields.GroupId[this] = value; }
        }

        [DisplayName("User Id"), Column("UserID")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Message"), QuickSearch]
        public String Message
        {
            get { return Fields.Message[this]; }
            set { Fields.Message[this] = value; }
        }

        [DisplayName("InsertDate"), QuickSearch]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }


        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Message; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public NotificationRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field GroupId;
            public Int32Field UserId;
            public StringField Message;
            public DateTimeField InsertDate;

            public RowFields()
                : base("[dbo].[Notification]")
            {
                LocalTextPrefix = "Administration.Notification";
            }
        }
    }
}