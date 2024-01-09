

namespace CaseManagement.Messaging.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("Vw_Message"), InstanceName("Vw_Message"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class VwMessagesRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), NotNull]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Sender Id")]
        public Int32? SenderId
        {
            get { return Fields.SenderId[this]; }
            set { Fields.SenderId[this] = value; }
        }

        [DisplayName("Seen")]
        public Boolean? Seen
        {
            get { return Fields.Seen[this]; }
            set { Fields.Seen[this] = value; }
        }

        [DisplayName("Seen Date")]
        public DateTime? SeenDate
        {
            get { return Fields.SeenDate[this]; }
            set { Fields.SeenDate[this] = value; }
        }

        [DisplayName("Subject"), Size(200), QuickSearch]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Body")]
        public String Body
        {
            get { return Fields.Body[this]; }
            set { Fields.Body[this] = value; }
        }

        [DisplayName("Inserted Date"), NotNull]
        public DateTime? InsertedDate
        {
            get { return Fields.InsertedDate[this]; }
            set { Fields.InsertedDate[this] = value; }
        }

        [DisplayName("Reciever Id")]
        public Int32? RecieverId
        {
            get { return Fields.RecieverId[this]; }
            set { Fields.RecieverId[this] = value; }
        }

        [DisplayName("Message Id")]
        public Int32? MessageId
        {
            get { return Fields.MessageId[this]; }
            set { Fields.MessageId[this] = value; }
        }

        [DisplayName("Sender Name"), Size(100), NotNull]
        public String SenderName
        {
            get { return Fields.SenderName[this]; }
            set { Fields.SenderName[this] = value; }
        }

        [DisplayName("Receiver Name"), Size(100), NotNull]
        public String ReceiverName
        {
            get { return Fields.ReceiverName[this]; }
            set { Fields.ReceiverName[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Subject; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public VwMessagesRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field SenderId;
            public BooleanField Seen;
            public DateTimeField SeenDate;
            public StringField Subject;
            public StringField Body;
            public DateTimeField InsertedDate;
            public Int32Field RecieverId;
            public Int32Field MessageId;
            public StringField SenderName;
            public StringField ReceiverName;

            public RowFields()
                : base("[dbo].[Vw_Message]")
            {
                LocalTextPrefix = "Messaging.VwMessages";
            }
        }
    }
}