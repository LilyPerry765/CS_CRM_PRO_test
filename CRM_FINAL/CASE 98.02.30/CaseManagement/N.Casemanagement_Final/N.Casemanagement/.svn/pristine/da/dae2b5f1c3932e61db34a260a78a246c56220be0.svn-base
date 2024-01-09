

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
    public sealed class MessagesReceiversRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Message"), ForeignKey("[dbo].[Messages]", "ID"), LeftJoin("jMessage"), TextualField("MessageSubject")]
        public Int32? MessageId
        {
            get { return Fields.MessageId[this]; }
            set { Fields.MessageId[this] = value; }
        }

        [DisplayName("Reciever"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jReciever"), TextualField("RecieverUsername")]
        public Int32? RecieverId
        {
            get { return Fields.RecieverId[this]; }
            set { Fields.RecieverId[this] = value; }
        }

        [DisplayName("Sender"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jSender"), TextualField("SenderDisplayName")]
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

        [DisplayName("Message Sender Id"), Expression("jMessage.[SenderId]")]
        public Int32? MessageSenderId
        {
            get { return Fields.MessageSenderId[this]; }
            set { Fields.MessageSenderId[this] = value; }
        }

        [DisplayName("Message Subject"), Expression("jMessage.[Subject]")]
        public String MessageSubject
        {
            get { return Fields.MessageSubject[this]; }
            set { Fields.MessageSubject[this] = value; }
        }

        [DisplayName("Message Body"), Expression("jMessage.[Body]")]
        public String MessageBody
        {
            get { return Fields.MessageBody[this]; }
            set { Fields.MessageBody[this] = value; }
        }

        [DisplayName("Message File"), Expression("jMessage.[File]")]
        public String MessageFile
        {
            get { return Fields.MessageFile[this]; }
            set { Fields.MessageFile[this] = value; }
        }

        [DisplayName("Message Inserted Date"), Expression("jMessage.[InsertedDate]")]
        public DateTime? MessageInsertedDate
        {
            get { return Fields.MessageInsertedDate[this]; }
            set { Fields.MessageInsertedDate[this] = value; }
        }

        [DisplayName("Reciever Oldcase Id"), Expression("jReciever.[OldcaseID]")]
        public Int32? RecieverOldcaseId
        {
            get { return Fields.RecieverOldcaseId[this]; }
            set { Fields.RecieverOldcaseId[this] = value; }
        }

        [DisplayName("Reciever Username"), Expression("jReciever.[Username]")]
        public String RecieverUsername
        {
            get { return Fields.RecieverUsername[this]; }
            set { Fields.RecieverUsername[this] = value; }
        }        

        [DisplayName("Reciever Display Name"), Expression("jReciever.[DisplayName]")]
        public String RecieverDisplayName
        {
            get { return Fields.RecieverDisplayName[this]; }
            set { Fields.RecieverDisplayName[this] = value; }
        }        

        [DisplayName("Reciever Employee Id"), Expression("jReciever.[EmployeeID]")]
        public String RecieverEmployeeId
        {
            get { return Fields.RecieverEmployeeId[this]; }
            set { Fields.RecieverEmployeeId[this] = value; }
        }

        [DisplayName("Reciever Email"), Expression("jReciever.[Email]")]
        public String RecieverEmail
        {
            get { return Fields.RecieverEmail[this]; }
            set { Fields.RecieverEmail[this] = value; }
        }

        [DisplayName("Reciever Rank"), Expression("jReciever.[Rank]")]
        public String RecieverRank
        {
            get { return Fields.RecieverRank[this]; }
            set { Fields.RecieverRank[this] = value; }
        }

        [DisplayName("Reciever Source"), Expression("jReciever.[Source]")]
        public String RecieverSource
        {
            get { return Fields.RecieverSource[this]; }
            set { Fields.RecieverSource[this] = value; }
        }

        [DisplayName("Reciever Password"), Expression("jReciever.[Password]")]
        public String RecieverPassword
        {
            get { return Fields.RecieverPassword[this]; }
            set { Fields.RecieverPassword[this] = value; }
        }

        [DisplayName("Reciever Password Hash"), Expression("jReciever.[PasswordHash]")]
        public String RecieverPasswordHash
        {
            get { return Fields.RecieverPasswordHash[this]; }
            set { Fields.RecieverPasswordHash[this] = value; }
        }

        [DisplayName("Reciever Password Salt"), Expression("jReciever.[PasswordSalt]")]
        public String RecieverPasswordSalt
        {
            get { return Fields.RecieverPasswordSalt[this]; }
            set { Fields.RecieverPasswordSalt[this] = value; }
        }

        [DisplayName("Reciever Last Login Date"), Expression("jReciever.[LastLoginDate]")]
        public DateTime? RecieverLastLoginDate
        {
            get { return Fields.RecieverLastLoginDate[this]; }
            set { Fields.RecieverLastLoginDate[this] = value; }
        }

        [DisplayName("Reciever Insert Date"), Expression("jReciever.[InsertDate]")]
        public DateTime? RecieverInsertDate
        {
            get { return Fields.RecieverInsertDate[this]; }
            set { Fields.RecieverInsertDate[this] = value; }
        }

        [DisplayName("Reciever Insert User Id"), Expression("jReciever.[InsertUserId]")]
        public Int32? RecieverInsertUserId
        {
            get { return Fields.RecieverInsertUserId[this]; }
            set { Fields.RecieverInsertUserId[this] = value; }
        }

        [DisplayName("Reciever Update Date"), Expression("jReciever.[UpdateDate]")]
        public DateTime? RecieverUpdateDate
        {
            get { return Fields.RecieverUpdateDate[this]; }
            set { Fields.RecieverUpdateDate[this] = value; }
        }

        [DisplayName("Reciever Update User Id"), Expression("jReciever.[UpdateUserId]")]
        public Int32? RecieverUpdateUserId
        {
            get { return Fields.RecieverUpdateUserId[this]; }
            set { Fields.RecieverUpdateUserId[this] = value; }
        }

        [DisplayName("Reciever Is Active"), Expression("jReciever.[IsActive]")]
        public Int16? RecieverIsActive
        {
            get { return Fields.RecieverIsActive[this]; }
            set { Fields.RecieverIsActive[this] = value; }
        }

        [DisplayName("Reciever Last Directory Update"), Expression("jReciever.[LastDirectoryUpdate]")]
        public DateTime? RecieverLastDirectoryUpdate
        {
            get { return Fields.RecieverLastDirectoryUpdate[this]; }
            set { Fields.RecieverLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Reciever Role Id"), Expression("jReciever.[RoleID]")]
        public Int32? RecieverRoleId
        {
            get { return Fields.RecieverRoleId[this]; }
            set { Fields.RecieverRoleId[this] = value; }
        }

        [DisplayName("Reciever Telephone No1"), Expression("jReciever.[TelephoneNo1]")]
        public String RecieverTelephoneNo1
        {
            get { return Fields.RecieverTelephoneNo1[this]; }
            set { Fields.RecieverTelephoneNo1[this] = value; }
        }

        [DisplayName("Reciever Telephone No2"), Expression("jReciever.[TelephoneNo2]")]
        public String RecieverTelephoneNo2
        {
            get { return Fields.RecieverTelephoneNo2[this]; }
            set { Fields.RecieverTelephoneNo2[this] = value; }
        }

        [DisplayName("Reciever Mobile No"), Expression("jReciever.[MobileNo]")]
        public String RecieverMobileNo
        {
            get { return Fields.RecieverMobileNo[this]; }
            set { Fields.RecieverMobileNo[this] = value; }
        }

        [DisplayName("Reciever Degree"), Expression("jReciever.[Degree]")]
        public String RecieverDegree
        {
            get { return Fields.RecieverDegree[this]; }
            set { Fields.RecieverDegree[this] = value; }
        }

        [DisplayName("Reciever Province Id"), Expression("jReciever.[ProvinceID]")]
        public Int32? RecieverProvinceId
        {
            get { return Fields.RecieverProvinceId[this]; }
            set { Fields.RecieverProvinceId[this] = value; }
        }

        [DisplayName("Reciever Is Iran Tci"), Expression("jReciever.[IsIranTCI]")]
        public Int32? RecieverIsIranTci
        {
            get { return Fields.RecieverIsIranTci[this]; }
            set { Fields.RecieverIsIranTci[this] = value; }
        }

        [DisplayName("Reciever Is Deleted"), Expression("jReciever.[IsDeleted]")]
        public Boolean? RecieverIsDeleted
        {
            get { return Fields.RecieverIsDeleted[this]; }
            set { Fields.RecieverIsDeleted[this] = value; }
        }

        [DisplayName("Reciever Deleted User Id"), Expression("jReciever.[DeletedUserID]")]
        public Int32? RecieverDeletedUserId
        {
            get { return Fields.RecieverDeletedUserId[this]; }
            set { Fields.RecieverDeletedUserId[this] = value; }
        }

        [DisplayName("Reciever Deleted Date"), Expression("jReciever.[DeletedDate]")]
        public DateTime? RecieverDeletedDate
        {
            get { return Fields.RecieverDeletedDate[this]; }
            set { Fields.RecieverDeletedDate[this] = value; }
        }

        [DisplayName("Reciever Image Path"), Expression("jReciever.[ImagePath]")]
        public String RecieverImagePath
        {
            get { return Fields.RecieverImagePath[this]; }
            set { Fields.RecieverImagePath[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public MessagesReceiversRow()
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

            public Int32Field MessageSenderId;
            public StringField MessageSubject;
            public StringField MessageBody;
            public StringField MessageFile;
            public DateTimeField MessageInsertedDate;

            public StringField SenderDisplayName;

            public Int32Field RecieverOldcaseId;
            public StringField RecieverUsername;
            public StringField RecieverDisplayName;
            public StringField RecieverEmployeeId;
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
            public StringField RecieverImagePath;

            public RowFields()
                : base("[dbo].[MessagesReceivers]")
            {
                LocalTextPrefix = "Messaging.MessagesReceivers";
            }
        }
    }
}