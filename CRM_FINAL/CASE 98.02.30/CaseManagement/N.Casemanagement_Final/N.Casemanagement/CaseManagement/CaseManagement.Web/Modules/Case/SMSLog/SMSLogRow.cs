

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

    [ConnectionKey("Default"), DisplayName("MessageLog"), InstanceName("MessageLog"), TwoLevelCached]
   
    [ReadPermission(Administration.PermissionKeys.Permission)]
    [ModifyPermission(Administration.PermissionKeys.Permission)]
    [LookupScript("Case.SMSLogRow")]
    public sealed class SMSLogRow : Row, IIdRow, INameRow
    {
        [DisplayName("شناسه"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("تاریخ ارسال"), NotNull]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }

        [DisplayName("شناسه فعالیت"), Column("ActivityRequestID")]
        public Int64? ActivityRequestId
        {
            get { return Fields.ActivityRequestId[this]; }
            set { Fields.ActivityRequestId[this] = value; }
        }

        [DisplayName("Message Id"), Column("MessageID"), NotNull]
        public Int32? MessageId
        {
            get { return Fields.MessageId[this]; }
            set { Fields.MessageId[this] = value; }
        }

        [DisplayName("Sender User Id"), Column("SenderUserID"), NotNull]
        public Int32? SenderUserId
        {
            get { return Fields.SenderUserId[this]; }
            set { Fields.SenderUserId[this] = value; }
        }

        [DisplayName("Sender User Name"), Size(255), QuickSearch]
        public String SenderUserName
        {
            get { return Fields.SenderUserName[this]; }
            set { Fields.SenderUserName[this] = value; }
        }

        [LookupEditor("Case.Province")]
        [DisplayName("Province ID"), Column("ReceiverProvinceID"), NotNull, ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jReceiverProvince"), TextualField("ReceiverProvinceName")]
        public Int32? ReceiverProvinceId
        {
            get { return Fields.ReceiverProvinceId[this]; }
            set { Fields.ReceiverProvinceId[this] = value; }
        }

        [DisplayName("Receiver User Id"), Column("ReceiverUserID"), NotNull]
        public Int32? ReceiverUserId
        {
            get { return Fields.ReceiverUserId[this]; }
            set { Fields.ReceiverUserId[this] = value; }
        }

        [DisplayName("نام کاربر"), Size(255)]
        public String ReceiverUserName
        {
            get { return Fields.ReceiverUserName[this]; }
            set { Fields.ReceiverUserName[this] = value; }
        }

        [DisplayName("شماره موبایل"), Size(15)]
        public String MobileNumber
        {
            get { return Fields.MobileNumber[this]; }
            set { Fields.MobileNumber[this] = value; }
        }

        //[DisplayName("متن پیام کوتاه"), Size(1000)]
        //public String TextSent
        //{
        //    get { return Fields.TextSent[this]; }
        //    set { Fields.TextSent[this] = value; }
        //}

        [DisplayName("Is Sent"), NotNull]
        public Boolean? IsSent
        {
            get { return Fields.IsSent[this]; }
            set { Fields.IsSent[this] = value; }
        }

        [DisplayName("Is Delivered"), NotNull]
        public Boolean? IsDelivered
        {
            get { return Fields.IsDelivered[this]; }
            set { Fields.IsDelivered[this] = value; }
        }

        [LookupEditor("Administration.Role")]
        [DisplayName("Receiver Role"), Column("ReceiverRoleID"), ForeignKey("[dbo].[Roles]", "RoleId"), LeftJoin("jReceiverRole"), TextualField("ReceiverRoleRoleName")]
        public Int32? ReceiverRoleId
        {
            get { return Fields.ReceiverRoleId[this]; }
            set { Fields.ReceiverRoleId[this] = value; }
        }

        [DisplayName("Receiver Province Leader Id"), Expression("jReceiverProvince.[LeaderID]")]
        public Int32? ReceiverProvinceLeaderId
        {
            get { return Fields.ReceiverProvinceLeaderId[this]; }
            set { Fields.ReceiverProvinceLeaderId[this] = value; }
        }

        [DisplayName("استان"), Expression("jReceiverProvince.[Name]")]
        public String ReceiverProvinceName
        {
            get { return Fields.ReceiverProvinceName[this]; }
            set { Fields.ReceiverProvinceName[this] = value; }
        }

        [DisplayName("Receiver Province Code"), Expression("jReceiverProvince.[Code]")]
        public Int32? ReceiverProvinceCode
        {
            get { return Fields.ReceiverProvinceCode[this]; }
            set { Fields.ReceiverProvinceCode[this] = value; }
        }

        [DisplayName("Receiver Province English Name"), Expression("jReceiverProvince.[EnglishName]")]
        public String ReceiverProvinceEnglishName
        {
            get { return Fields.ReceiverProvinceEnglishName[this]; }
            set { Fields.ReceiverProvinceEnglishName[this] = value; }
        }

        [DisplayName("Receiver Province Manager Name"), Expression("jReceiverProvince.[ManagerName]")]
        public String ReceiverProvinceManagerName
        {
            get { return Fields.ReceiverProvinceManagerName[this]; }
            set { Fields.ReceiverProvinceManagerName[this] = value; }
        }

        [DisplayName("Receiver Province Letter No"), Expression("jReceiverProvince.[LetterNo]")]
        public String ReceiverProvinceLetterNo
        {
            get { return Fields.ReceiverProvinceLetterNo[this]; }
            set { Fields.ReceiverProvinceLetterNo[this] = value; }
        }

        [DisplayName("Receiver Province Pmo Level Id"), Expression("jReceiverProvince.[PMOLevelID]")]
        public Int32? ReceiverProvincePmoLevelId
        {
            get { return Fields.ReceiverProvincePmoLevelId[this]; }
            set { Fields.ReceiverProvincePmoLevelId[this] = value; }
        }

        [DisplayName("Receiver Province Install Date"), Expression("jReceiverProvince.[InstallDate]")]
        public DateTime? ReceiverProvinceInstallDate
        {
            get { return Fields.ReceiverProvinceInstallDate[this]; }
            set { Fields.ReceiverProvinceInstallDate[this] = value; }
        }

        [DisplayName("سمت"), Expression("jReceiverRole.[RoleName]")]
        public String ReceiverRoleRoleName
        {
            get { return Fields.ReceiverRoleRoleName[this]; }
            set { Fields.ReceiverRoleRoleName[this] = value; }
        }
        
        [DisplayName("بروزرسانی گردید")]
        public Boolean? Is_modified
        {
            get { return Fields.Is_modified[this]; }
            set { Fields.Is_modified[this] = value; }
        }

        [DisplayName("تاریخ آخرین بروزرسانی ")]
        public DateTime? ModifiedDate
        {
            get { return Fields.ModifiedDate[this]; }
            set { Fields.ModifiedDate[this] = value; }
        }

      

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.SenderUserName; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public SMSLogRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public BooleanField Is_modified;
            public DateTimeField InsertDate;
            public Int64Field ActivityRequestId;
            public Int32Field MessageId;
            public Int32Field SenderUserId;
            public StringField SenderUserName;
            public Int32Field ReceiverProvinceId;
            public Int32Field ReceiverUserId;
            public StringField ReceiverUserName;
            public StringField MobileNumber;
      //      public StringField TextSent;
            public BooleanField IsSent;
            public BooleanField IsDelivered;
            public Int32Field ReceiverRoleId;

            public Int32Field ReceiverProvinceLeaderId;
            public StringField ReceiverProvinceName;
            public Int32Field ReceiverProvinceCode;
            public StringField ReceiverProvinceEnglishName;
            public StringField ReceiverProvinceManagerName;
            public StringField ReceiverProvinceLetterNo;
            public Int32Field ReceiverProvincePmoLevelId;
            public DateTimeField ReceiverProvinceInstallDate;
            public DateTimeField ModifiedDate;

            public StringField ReceiverRoleRoleName;

            public RowFields()
                : base("[dbo].[ShortMessages]")
            {
                LocalTextPrefix = "Case.SMSLog";
            }
        }
    }
}