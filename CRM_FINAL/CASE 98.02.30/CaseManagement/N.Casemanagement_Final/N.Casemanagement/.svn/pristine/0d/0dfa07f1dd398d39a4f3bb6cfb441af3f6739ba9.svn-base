

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

    [ConnectionKey("Default"), DisplayName("MessagesReceivers"), InstanceName("MessagesReceivers"), TwoLevelCached]
    [ReadPermission(CaseManagement.Administration.PermissionKeys.All)]
    [ModifyPermission(CaseManagement.Administration.PermissionKeys.All)]
    public sealed class SentRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("پیام"), ForeignKey("[dbo].[Messages]", "ID"), LeftJoin("jMessage"), TextualField("MessageSubject")]
        public Int32? MessageId
        {
            get { return Fields.MessageId[this]; }
            set { Fields.MessageId[this] = value; }
        }

        [DisplayName("دریافت کننده"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jReciever"), TextualField("RecieverUsername")]
        public Int32? RecieverId
        {
            get { return Fields.RecieverId[this]; }
            set { Fields.RecieverId[this] = value; }
        }

        [DisplayName("ارسال کننده"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jSender"), TextualField("SenderDisplayName")]
        public Int32? SenderId
        {
            get { return Fields.SenderId[this]; }
            set { Fields.SenderId[this] = value; }
        }

        [DisplayName("Sender Display Name"), Expression("jSender.[DisplayName]")]
        public String SenderDisplayName
        {
            get { return Fields.SenderDisplayName[this]; }
            set { Fields.SenderDisplayName[this] = value; }
        }

        [DisplayName("مشاهده گردید")]
        public Boolean? Seen
        {
            get { return Fields.Seen[this]; }
            set { Fields.Seen[this] = value; }
        }

        [DisplayName("تاریخ مشاهده")]
        public DateTime? SeenDate
        {
            get { return Fields.SeenDate[this]; }
            set { Fields.SeenDate[this] = value; }
        }

      //  [DisplayName("ارسال کننده"), Expression("jMessage.[SenderId]")]
      //  public Int32? MessageSenderId
      //  {
      //      get { return Fields.MessageSenderId[this]; }
      //      set { Fields.MessageSenderId[this] = value; }
      //  }

        [DisplayName("موضوع"), Expression("jMessage.[Subject]")]
        public String MessageSubject
        {
            get { return Fields.MessageSubject[this]; }
            set { Fields.MessageSubject[this] = value; }
        }

        [DisplayName("متن"), Expression("jMessage.[Body]")]
        public String MessageBody
        {
            get { return Fields.MessageBody[this]; }
            set { Fields.MessageBody[this] = value; }
        }

        [DisplayName("پیوست"), Expression("jMessage.[File]")]
        [FileUploadEditor(FilenameFormat = "MessageFile/~", CopyToHistory = true)]
        public String MessageFile
        {
            get { return Fields.MessageFile[this]; }
            set { Fields.MessageFile[this] = value; }
        }

        [DisplayName("تاریخ دریافت"), Expression("jMessage.[InsertedDate]")]
        public DateTime? MessageInsertedDate
        {
            get { return Fields.MessageInsertedDate[this]; }
            set { Fields.MessageInsertedDate[this] = value; }
        }

        [DisplayName("دریافت کننده"), Expression("jReciever.[DisplayName]")]
        public String RecieverDisplayName
        {
            get { return Fields.RecieverDisplayName[this]; }
            set { Fields.RecieverDisplayName[this] = value; }
        }


        

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public SentRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field MessageId;
            public Int32Field RecieverId;
            public Int32Field SenderId;
            public BooleanField Seen;
            public DateTimeField SeenDate;

            //public Int32Field MessageSenderId;
            public StringField MessageSubject;
            public StringField MessageBody;
            public StringField MessageFile;
            public DateTimeField MessageInsertedDate;
            public StringField RecieverDisplayName;

            public StringField SenderDisplayName;

            //public Int32Field RecieverOldcaseId;
            //public StringField RecieverUsername;
            
          /*  public StringField RecieverEmployeeId;
            public StringField RecieverEmail;
            public StringField RecieverRank;
            public StringField RecieverSource;
            public StringField RecieverPassword;
            public StringField RecieverPasswordHash;
            public StringField RecieverPasswordSalt;
            public DateTimeField RecieverLastLoginDate;
            public DateTimeField RecieverInsertDate;
            public Int32Field RecieverInsertUserId;
            public DateTimeField RecieverUpdateDate;
            public Int32Field RecieverUpdateUserId;
            public Int16Field RecieverIsActive;
            public DateTimeField RecieverLastDirectoryUpdate;
            public Int32Field RecieverRoleId;
            public StringField RecieverTelephoneNo1;
            public StringField RecieverTelephoneNo2;
            public StringField RecieverMobileNo;
            public StringField RecieverDegree;
            public Int32Field RecieverProvinceId;
            public Int32Field RecieverIsIranTci;
            public BooleanField RecieverIsDeleted;
            public Int32Field RecieverDeletedUserId;
            public DateTimeField RecieverDeletedDate;
            public StringField RecieverImagePath;*/

            public RowFields()
                : base("[dbo].[MessagesReceivers]")
            {
                LocalTextPrefix = "Messaging.Sent";
            }
        }
    }
}