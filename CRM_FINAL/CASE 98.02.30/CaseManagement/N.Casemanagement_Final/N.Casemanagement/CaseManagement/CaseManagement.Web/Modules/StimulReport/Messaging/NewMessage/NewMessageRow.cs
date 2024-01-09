

namespace CaseManagement.Messaging.Entities
{
    using CaseManagement.Administration.Entities;
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("Messages"), InstanceName("Messages"), TwoLevelCached]
    [ReadPermission(CaseManagement.Administration.PermissionKeys.All)]
    [ModifyPermission(CaseManagement.Administration.PermissionKeys.All)]
    [LookupScript("Messaging.NewMessage")]
    public sealed class NewMessageRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("ارسال کننده"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jSender"), TextualField("SenderUsername")]
        public Int32? SenderId
        {
            get { return Fields.SenderId[this]; }
            set { Fields.SenderId[this] = value; }
        }

        [DisplayName("موضوع"), Size(200), QuickSearch]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("متن")]
        public String Body
        {
            get { return Fields.Body[this]; }
            set { Fields.Body[this] = value; }
        }

        [DisplayName("پیوست"), Size(300)]
        [FileUploadEditor(FilenameFormat = "MessageFile/~", CopyToHistory = true)]
        public String File
        {
            get { return Fields.File[this]; }
            set { Fields.File[this] = value; }
        }

        [DisplayName("تاریخ ارسال"), NotNull]
        public DateTime? InsertedDate
        {
            get { return Fields.InsertedDate[this]; }
            set { Fields.InsertedDate[this] = value; }
        }


        [DisplayName("ارسال کننده"), Expression("jSender.[DisplayName]")]
        public String SenderDisplayName
        {
            get { return Fields.SenderDisplayName[this]; }
            set { Fields.SenderDisplayName[this] = value; }
        }

        [DisplayName("دریافت کننده ها")]
        [LookupEditor(typeof(UserRow), Multiple = true), ClientSide]
        [LinkingSetRelation(typeof(MessagesReceiversRow), "MessageId", "RecieverId")]
        public List<Int32> ReceiverList
        {
            get { return Fields.ReceiverList[this]; }
            set { Fields.ReceiverList[this] = value; }
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

        public NewMessageRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field SenderId;
            public StringField Subject;
            public StringField Body;
            public StringField File;
            public DateTimeField InsertedDate;
            public StringField SenderDisplayName;

            public ListField<Int32> ReceiverList;


            //public Int32Field SenderOldcaseId;
            //public StringField SenderUsername;

            /*  public StringField SenderEmployeeId;
              public StringField SenderEmail;
              public StringField SenderRank;
              public StringField SenderSource;
              public StringField SenderPassword;
              public StringField SenderPasswordHash;
              public StringField SenderPasswordSalt;
              public DateTimeField SenderLastLoginDate;
              public DateTimeField SenderInsertDate;
              public Int32Field SenderInsertUserId;
              public DateTimeField SenderUpdateDate;
              public Int32Field SenderUpdateUserId;
              public Int16Field SenderIsActive;
              public DateTimeField SenderLastDirectoryUpdate;
              public Int32Field SenderRoleId;
              public StringField SenderTelephoneNo1;
              public StringField SenderTelephoneNo2;
              public StringField SenderMobileNo;
              public StringField SenderDegree;
              public Int32Field SenderProvinceId;
              public Int32Field SenderIsIranTci;
              public BooleanField SenderIsDeleted;
              public Int32Field SenderDeletedUserId;
              public DateTimeField SenderDeletedDate;
              public StringField SenderImagePath;
              */
            public RowFields()
                : base("[dbo].[Messages]")
            {
                LocalTextPrefix = "Messaging.NewMessage";
            }
        }
    }
}