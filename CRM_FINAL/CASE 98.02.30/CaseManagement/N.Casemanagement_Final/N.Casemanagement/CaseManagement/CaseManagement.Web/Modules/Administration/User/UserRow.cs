
namespace CaseManagement.Administration.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using CaseManagement.Case.Entities;

    [ConnectionKey("Default"), DisplayName("Users"), InstanceName("User"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    [LookupScript("Administration.User")]
    public sealed class UserRow : LoggingRow, IIdRow, INameRow, IIsActiveRow
    {
        [DisplayName("User Id"), Identity]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Username"), Size(100), NotNull, QuickSearch]
        public String Username
        {
            get { return Fields.Username[this]; }
            set { Fields.Username[this] = value; }
        }

        [DisplayName("Source"), Size(4), NotNull, Insertable(false), Updatable(false), DefaultValue("site")]
        public String Source
        {
            get { return Fields.Source[this]; }
            set { Fields.Source[this] = value; }
        }

        [DisplayName("Password Hash"), Size(86), NotNull, Insertable(false), Updatable(false), MinSelectLevel(SelectLevel.Never)]
        public String PasswordHash
        {
            get { return Fields.PasswordHash[this]; }
            set { Fields.PasswordHash[this] = value; }
        }

        [DisplayName("Password Salt"), Size(10), NotNull, Insertable(false), Updatable(false), MinSelectLevel(SelectLevel.Never)]
        public String PasswordSalt
        {
            get { return Fields.PasswordSalt[this]; }
            set { Fields.PasswordSalt[this] = value; }
        }

        [DisplayName("Display Name"), Size(100), NotNull, QuickSearch]
        public String DisplayName
        {
            get { return Fields.DisplayName[this]; }
            set { Fields.DisplayName[this] = value; }
        }

        [DisplayName("شماره مستخدم"), Size(50)]
        public String EmployeeID
        {
            get { return Fields.EmployeeID[this]; }
            set { Fields.EmployeeID[this] = value; }
        }

        [DisplayName("سمت سازمانی"), Size(50)]
        public String Rank
        {
            get { return Fields.Rank[this]; }
            set { Fields.Rank[this] = value; }
        }

        [DisplayName("Email"), Size(100)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Password"), Size(50), SetFieldFlags(FieldFlags.ClientSide)]
        public String Password
        {
            get { return Fields.Password[this]; }
            set { Fields.Password[this] = value; }
        }

        [DisplayName("فعال"), NotNull, Insertable(false), Updatable(true)]
        public Int16? IsActive
        {
            get { return Fields.IsActive[this]; }
            set { Fields.IsActive[this] = value; }
        }

        [DisplayName("Confirm Password"), Size(50), SetFieldFlags(FieldFlags.ClientSide)]
        public String PasswordConfirm
        {
            get { return Fields.PasswordConfirm[this]; }
            set { Fields.PasswordConfirm[this] = value; }
        }

        [DisplayName("Last Directory Update"), Insertable(false), Updatable(false)]
        public DateTime? LastDirectoryUpdate
        {
            get { return Fields.LastDirectoryUpdate[this]; }
            set { Fields.LastDirectoryUpdate[this] = value; }
        }

        [DisplayName("تلفن ثابت"), Size(50)]
        public String TelephoneNo1
        {
            get { return Fields.TelephoneNo1[this]; }
            set { Fields.TelephoneNo1[this] = value; }
        }

        [DisplayName("تلفن 2"), Size(50)]
        public String TelephoneNo2
        {
            get { return Fields.TelephoneNo2[this]; }
            set { Fields.TelephoneNo2[this] = value; }
        }

        [DisplayName("تلفن همراه"), Size(50)]
        public String MobileNo
        {
            get { return Fields.MobileNo[this]; }
            set { Fields.MobileNo[this] = value; }
        }

        [DisplayName("مدرک تحصیلی"), Size(50)]
        public String Degree
        {
            get { return Fields.Degree[this]; }
            set { Fields.Degree[this] = value; }
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

        [DisplayName("استان ها")]
        [LookupEditor(typeof(ProvinceRow), Multiple = true), ClientSide]
        [LinkingSetRelation(typeof(UserProvinceRow), "UserId", "ProvinceId")]
        public List<Int32> ProvinceList
        {
            get { return Fields.ProvinceList[this]; }
            set { Fields.ProvinceList[this] = value; }
        }

        [DisplayName("مخابرات ایران"), NotNull]
        public UserTCI? IsIranTCI
        {
            get { return (UserTCI?)Fields.IsIranTCI[this]; }
            set { Fields.IsIranTCI[this] = (Int32?)value; }
        }

        [DisplayName("حذف"), NotNull]
        public Boolean? IsDeleted
        {
            get { return Fields.IsDeleted[this]; }
            set { Fields.IsDeleted[this] = value; }
        }

        [DisplayName("Deleted User Id"), Column("DeletedUserID")]
        public Int32? DeletedUserId
        {
            get { return Fields.DeletedUserId[this]; }
            set { Fields.DeletedUserId[this] = value; }
        }

        [DisplayName("Deleted Date")]
        public DateTime? DeletedDate
        {
            get { return Fields.DeletedDate[this]; }
            set { Fields.DeletedDate[this] = value; }
        }

        [DisplayName("تاریخ آخرین ورود")]
        public DateTime? LastLoginDate
        {
            get { return Fields.LastLoginDate[this]; }
            set { Fields.LastLoginDate[this] = value; }
        }

        [DisplayName("تصویر"), Size(300)]
        [FileUploadEditor(FilenameFormat = "UserImage/~", CopyToHistory = true)]
        public String ImagePath
        {
            get { return Fields.ImagePath[this]; }
            set { Fields.ImagePath[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.UserId; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.DisplayName; }
        }

        Int16Field IIsActiveRow.IsActiveField
        {
            get { return Fields.IsActive; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public UserRow()
            : base(Fields)
        {
        }

        public class RowFields : LoggingRowFields
        {
            public Int32Field UserId;
            public StringField Username;
            public StringField Source;
            public StringField PasswordHash;
            public StringField PasswordSalt;
            public StringField DisplayName;
            public StringField EmployeeID;
            public StringField Rank;
            public StringField Email;
            public DateTimeField LastDirectoryUpdate;
            public Int16Field IsActive;            
            public StringField Password;
            public StringField PasswordConfirm;
            public StringField TelephoneNo1;
            public StringField TelephoneNo2;
            public StringField MobileNo;
            public StringField Degree;
            public Int32Field ProvinceId;
            public StringField ProvinceName;
            public Int32Field IsIranTCI;

            public ListField<Int32> ProvinceList;

            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;
            public DateTimeField LastLoginDate;

            public StringField ImagePath;

            public RowFields()
                : base("Users")
            {
                LocalTextPrefix = "Administration.User";
            }
        }
    }
}