

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

    [ConnectionKey("Default"), DisplayName("UserSupportGroup"), InstanceName("UserSupportGroup"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    public sealed class UserSupportGroupRow : Row, IIdRow
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

        [DisplayName("Group Id"), Column("GroupID"), NotNull]
        public Int32? GroupId
        {
            get { return Fields.GroupId[this]; }
            set { Fields.GroupId[this] = value; }
        }

        [DisplayName("User Oldcase Id"), Expression("jUser.[OldcaseID]")]
        public Int32? UserOldcaseId
        {
            get { return Fields.UserOldcaseId[this]; }
            set { Fields.UserOldcaseId[this] = value; }
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

        [DisplayName("User Employee Id"), Expression("jUser.[EmployeeID]")]
        public String UserEmployeeId
        {
            get { return Fields.UserEmployeeId[this]; }
            set { Fields.UserEmployeeId[this] = value; }
        }

        [DisplayName("User Email"), Expression("jUser.[Email]")]
        public String UserEmail
        {
            get { return Fields.UserEmail[this]; }
            set { Fields.UserEmail[this] = value; }
        }

        [DisplayName("User Rank"), Expression("jUser.[Rank]")]
        public String UserRank
        {
            get { return Fields.UserRank[this]; }
            set { Fields.UserRank[this] = value; }
        }

        [DisplayName("User Source"), Expression("jUser.[Source]")]
        public String UserSource
        {
            get { return Fields.UserSource[this]; }
            set { Fields.UserSource[this] = value; }
        }

        [DisplayName("User Password"), Expression("jUser.[Password]")]
        public String UserPassword
        {
            get { return Fields.UserPassword[this]; }
            set { Fields.UserPassword[this] = value; }
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

        [DisplayName("User Role Id"), Expression("jUser.[RoleID]")]
        public Int32? UserRoleId
        {
            get { return Fields.UserRoleId[this]; }
            set { Fields.UserRoleId[this] = value; }
        }

        [DisplayName("User Telephone No1"), Expression("jUser.[TelephoneNo1]")]
        public String UserTelephoneNo1
        {
            get { return Fields.UserTelephoneNo1[this]; }
            set { Fields.UserTelephoneNo1[this] = value; }
        }

        [DisplayName("User Telephone No2"), Expression("jUser.[TelephoneNo2]")]
        public String UserTelephoneNo2
        {
            get { return Fields.UserTelephoneNo2[this]; }
            set { Fields.UserTelephoneNo2[this] = value; }
        }

        [DisplayName("User Mobile No"), Expression("jUser.[MobileNo]")]
        public String UserMobileNo
        {
            get { return Fields.UserMobileNo[this]; }
            set { Fields.UserMobileNo[this] = value; }
        }

        [DisplayName("User Degree"), Expression("jUser.[Degree]")]
        public String UserDegree
        {
            get { return Fields.UserDegree[this]; }
            set { Fields.UserDegree[this] = value; }
        }

        [DisplayName("User Province Id"), Expression("jUser.[ProvinceID]")]
        public Int32? UserProvinceId
        {
            get { return Fields.UserProvinceId[this]; }
            set { Fields.UserProvinceId[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public UserSupportGroupRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field UserId;
            public Int32Field GroupId;

            public Int32Field UserOldcaseId;
            public StringField UserUsername;
            public StringField UserDisplayName;
            public StringField UserEmployeeId;
            public StringField UserEmail;
            public StringField UserRank;
            public StringField UserSource;
            public StringField UserPassword;
            public StringField UserPasswordHash;
            public StringField UserPasswordSalt;
            public DateTimeField UserInsertDate;
            public Int32Field UserInsertUserId;
            public DateTimeField UserUpdateDate;
            public Int32Field UserUpdateUserId;
            public Int16Field UserIsActive;
            public DateTimeField UserLastDirectoryUpdate;
            public Int32Field UserRoleId;
            public StringField UserTelephoneNo1;
            public StringField UserTelephoneNo2;
            public StringField UserMobileNo;
            public StringField UserDegree;
            public Int32Field UserProvinceId;

            public RowFields()
                : base("[dbo].[UserSupportGroup]")
            {
                LocalTextPrefix = "Administration.UserSupportGroup";
            }
        }
    }
}