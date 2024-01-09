

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

    [ConnectionKey("Default"), DisplayName("Log"), InstanceName("Log"), TwoLevelCached]
    [ReadPermission(Administration.PermissionKeys.Permission)]
    [ModifyPermission(Administration.PermissionKeys.Permission)]
    public sealed class LogRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int64? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Table Name"), Size(1000), NotNull, QuickSearch]
        public String TableName
        {
            get { return Fields.TableName[this]; }
            set { Fields.TableName[this] = value; }
        }

        [DisplayName("نام موجودیت"), Size(1000), NotNull]
        public String PersianTableName
        {
            get { return Fields.PersianTableName[this]; }
            set { Fields.PersianTableName[this] = value; }
        }

        [DisplayName("Record Id"), Column("RecordID"), NotNull]
        public Int64? RecordId
        {
            get { return Fields.RecordId[this]; }
            set { Fields.RecordId[this] = value; }
        }

        [DisplayName("نام رکورد"), Size(1000), NotNull, QuickSearch]
        public string RecordName
        {
            get { return Fields.RecordName[this]; }
            set { Fields.RecordName[this] = value; }
        }

        [DisplayName("IP"), Size(1000), NotNull, QuickSearch]
        public string IP
        {
            get { return Fields.IP[this]; }
            set { Fields.IP[this] = value; }
        }

        [DisplayName("عملیات")]
        public ActionLog? ActionID
        {
            get { return (ActionLog?)Fields.ActionID[this]; }
            set { Fields.ActionID[this] = (Int32?)value; }
        }

        [DisplayName("کاربر"), Column("UserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUsers1"), TextualField("DisplayName")]
        [LookupEditor("Administration.User")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("کاربر"), Expression("jUsers1.[DisplayName]")]
        public String DisplayName
        {
            get { return Fields.DisplayName[this]; }
            set { Fields.DisplayName[this] = value; }
        }

        [DisplayName("تاریخ"), NotNull]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }

        [LookupEditor("Case.Province")]
        [DisplayName("استان"), Column("ProvinceID"), ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvince"), TextualField("ProvinceName")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [DisplayName("استان"), Expression("jProvince.[Name]")]
        public String ProvinceName
        {
            get { return Fields.ProvinceName[this]; }
            set { Fields.ProvinceName[this] = value; }
        }

        [DisplayName("Old Data")]
        public String OldData
        {
            get { return Fields.OldData[this]; }
            set { Fields.OldData[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.TableName; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public LogRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int64Field Id;
            public StringField TableName;
            public StringField PersianTableName;
            public Int64Field RecordId;
            public StringField RecordName;
            public StringField IP;
            public Int32Field ActionID;
            public Int32Field UserId;
            public DateTimeField InsertDate;
            public StringField DisplayName;
            public Int32Field ProvinceId;
            public StringField ProvinceName;
            public StringField OldData;

            public RowFields()
                : base("[dbo].[Log]")
            {
                LocalTextPrefix = "Administration.Log";
            }
        }
    }
}