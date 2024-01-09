

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

    [ConnectionKey("Default"), DisplayName("UserProvince"), InstanceName("UserProvince"), TwoLevelCached]
    [ReadPermission(Administration.PermissionKeys.Permission)]
    [ModifyPermission(Administration.PermissionKeys.Permission)]
    public sealed class UserProvinceRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("User"), Column("UserID"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUser"), TextualField("UserUsername")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Province"), Column("ProvinceID"), NotNull, ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvince"), TextualField("ProvinceName")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [DisplayName("Created User Id"), Column("CreatedUserID"), NotNull]
        public Int32? CreatedUserId
        {
            get { return Fields.CreatedUserId[this]; }
            set { Fields.CreatedUserId[this] = value; }
        }

        [DisplayName("Created Date"), NotNull]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [DisplayName("Is Deleted"), NotNull]
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

        [DisplayName("End Date")]
        public DateTime? EndDate
        {
            get { return Fields.EndDate[this]; }
            set { Fields.EndDate[this] = value; }
        }

        [DisplayName("User Username"), Expression("jUser.[Username]")]
        public String UserUsername
        {
            get { return Fields.UserUsername[this]; }
            set { Fields.UserUsername[this] = value; }
        }

        [DisplayName("User Display Name"), Expression("jUser.[DisplayName]")]
        public String UserDisplayName
        {
            get { return Fields.UserDisplayName[this]; }
            set { Fields.UserDisplayName[this] = value; }
        }

        [DisplayName("User Email"), Expression("jUser.[Email]")]
        public String UserEmail
        {
            get { return Fields.UserEmail[this]; }
            set { Fields.UserEmail[this] = value; }
        }

        [DisplayName("User Source"), Expression("jUser.[Source]")]
        public String UserSource
        {
            get { return Fields.UserSource[this]; }
            set { Fields.UserSource[this] = value; }
        }

        [DisplayName("User Password Hash"), Expression("jUser.[PasswordHash]")]
        public String UserPasswordHash
        {
            get { return Fields.UserPasswordHash[this]; }
            set { Fields.UserPasswordHash[this] = value; }
        }

        [DisplayName("User Password Salt"), Expression("jUser.[PasswordSalt]")]
        public String UserPasswordSalt
        {
            get { return Fields.UserPasswordSalt[this]; }
            set { Fields.UserPasswordSalt[this] = value; }
        }

        [DisplayName("User Insert Date"), Expression("jUser.[InsertDate]")]
        public DateTime? UserInsertDate
        {
            get { return Fields.UserInsertDate[this]; }
            set { Fields.UserInsertDate[this] = value; }
        }

        [DisplayName("User Insert User Id"), Expression("jUser.[InsertUserId]")]
        public Int32? UserInsertUserId
        {
            get { return Fields.UserInsertUserId[this]; }
            set { Fields.UserInsertUserId[this] = value; }
        }

        [DisplayName("User Update Date"), Expression("jUser.[UpdateDate]")]
        public DateTime? UserUpdateDate
        {
            get { return Fields.UserUpdateDate[this]; }
            set { Fields.UserUpdateDate[this] = value; }
        }

        [DisplayName("User Update User Id"), Expression("jUser.[UpdateUserId]")]
        public Int32? UserUpdateUserId
        {
            get { return Fields.UserUpdateUserId[this]; }
            set { Fields.UserUpdateUserId[this] = value; }
        }

        [DisplayName("User Is Active"), Expression("jUser.[IsActive]")]
        public Int16? UserIsActive
        {
            get { return Fields.UserIsActive[this]; }
            set { Fields.UserIsActive[this] = value; }
        }

        [DisplayName("User Last Directory Update"), Expression("jUser.[LastDirectoryUpdate]")]
        public DateTime? UserLastDirectoryUpdate
        {
            get { return Fields.UserLastDirectoryUpdate[this]; }
            set { Fields.UserLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Province Name"), Expression("jProvince.[Name]")]
        public String ProvinceName
        {
            get { return Fields.ProvinceName[this]; }
            set { Fields.ProvinceName[this] = value; }
        }

        [DisplayName("Province English Name"), Expression("jProvince.[EnglishName]")]
        public String ProvinceEnglishName
        {
            get { return Fields.ProvinceEnglishName[this]; }
            set { Fields.ProvinceEnglishName[this] = value; }
        }

        [DisplayName("Province Manager Name"), Expression("jProvince.[ManagerName]")]
        public String ProvinceManagerName
        {
            get { return Fields.ProvinceManagerName[this]; }
            set { Fields.ProvinceManagerName[this] = value; }
        }

        [DisplayName("Province Letter No"), Expression("jProvince.[LetterNo]")]
        public String ProvinceLetterNo
        {
            get { return Fields.ProvinceLetterNo[this]; }
            set { Fields.ProvinceLetterNo[this] = value; }
        }

        [DisplayName("Province Pmo Level Id"), Expression("jProvince.[PMOLevelID]")]
        public Int32? ProvincePmoLevelId
        {
            get { return Fields.ProvincePmoLevelId[this]; }
            set { Fields.ProvincePmoLevelId[this] = value; }
        }

        [DisplayName("Province Install Date"), Expression("jProvince.[InstallDate]")]
        public DateTime? ProvinceInstallDate
        {
            get { return Fields.ProvinceInstallDate[this]; }
            set { Fields.ProvinceInstallDate[this] = value; }
        }

        [DisplayName("Province Created User Id"), Expression("jProvince.[CreatedUserID]")]
        public Int32? ProvinceCreatedUserId
        {
            get { return Fields.ProvinceCreatedUserId[this]; }
            set { Fields.ProvinceCreatedUserId[this] = value; }
        }

        [DisplayName("Province Created Date"), Expression("jProvince.[CreatedDate]")]
        public DateTime? ProvinceCreatedDate
        {
            get { return Fields.ProvinceCreatedDate[this]; }
            set { Fields.ProvinceCreatedDate[this] = value; }
        }

        [DisplayName("Province Modified User Id"), Expression("jProvince.[ModifiedUserID]")]
        public Int32? ProvinceModifiedUserId
        {
            get { return Fields.ProvinceModifiedUserId[this]; }
            set { Fields.ProvinceModifiedUserId[this] = value; }
        }

        [DisplayName("Province Modified Date"), Expression("jProvince.[ModifiedDate]")]
        public DateTime? ProvinceModifiedDate
        {
            get { return Fields.ProvinceModifiedDate[this]; }
            set { Fields.ProvinceModifiedDate[this] = value; }
        }

        [DisplayName("Province Is Deleted"), Expression("jProvince.[IsDeleted]")]
        public Boolean? ProvinceIsDeleted
        {
            get { return Fields.ProvinceIsDeleted[this]; }
            set { Fields.ProvinceIsDeleted[this] = value; }
        }

        [DisplayName("Province Deleted User Id"), Expression("jProvince.[DeletedUserID]")]
        public Int32? ProvinceDeletedUserId
        {
            get { return Fields.ProvinceDeletedUserId[this]; }
            set { Fields.ProvinceDeletedUserId[this] = value; }
        }

        [DisplayName("Province Deleted Date"), Expression("jProvince.[DeletedDate]")]
        public DateTime? ProvinceDeletedDate
        {
            get { return Fields.ProvinceDeletedDate[this]; }
            set { Fields.ProvinceDeletedDate[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public UserProvinceRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field UserId;
            public Int32Field ProvinceId;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;
            public DateTimeField EndDate;

            public StringField UserUsername;
            public StringField UserDisplayName;
            public StringField UserEmail;
            public StringField UserSource;
            public StringField UserPasswordHash;
            public StringField UserPasswordSalt;
            public DateTimeField UserInsertDate;
            public Int32Field UserInsertUserId;
            public DateTimeField UserUpdateDate;
            public Int32Field UserUpdateUserId;
            public Int16Field UserIsActive;
            public DateTimeField UserLastDirectoryUpdate;

            public StringField ProvinceName;
            public StringField ProvinceEnglishName;
            public StringField ProvinceManagerName;
            public StringField ProvinceLetterNo;
            public Int32Field ProvincePmoLevelId;
            public DateTimeField ProvinceInstallDate;
            public Int32Field ProvinceCreatedUserId;
            public DateTimeField ProvinceCreatedDate;
            public Int32Field ProvinceModifiedUserId;
            public DateTimeField ProvinceModifiedDate;
            public BooleanField ProvinceIsDeleted;
            public Int32Field ProvinceDeletedUserId;
            public DateTimeField ProvinceDeletedDate;

            public RowFields()
                : base("[dbo].[UserProvince]")
            {
                LocalTextPrefix = "Case.UserProvince";
            }
        }
    }
}