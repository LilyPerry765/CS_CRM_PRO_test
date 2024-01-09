

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

    [ConnectionKey("Default"), DisplayName("سوییچ ترانزیت"), InstanceName("سوییچ ترانزیت"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class SwitchTransitProvinceRow : Row, IIdRow , INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [LookupEditor("Case.Province")]
        [DisplayName("Province"), Column("ProvinceID"), NotNull, ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvince"), TextualField("ProvinceName")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [LookupEditor("Case.SwitchTransit")]
        [DisplayName("عنوان"), Column("SwitchTransitID"), NotNull, ForeignKey("[dbo].[SwitchTransit]", "ID"), LeftJoin("jSwitchTransit"), TextualField("SwitchTransitName")]
        public Int32? SwitchTransitId
        {
            get { return Fields.SwitchTransitId[this]; }
            set { Fields.SwitchTransitId[this] = value; }
        }

        [DisplayName("Created User Id"), Column("CreatedUserID")]
        public Int32? CreatedUserId
        {
            get { return Fields.CreatedUserId[this]; }
            set { Fields.CreatedUserId[this] = value; }
        }

        [DisplayName("Created Date")]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [DisplayName("Modified User Id"), Column("ModifiedUserID")]
        public Int32? ModifiedUserId
        {
            get { return Fields.ModifiedUserId[this]; }
            set { Fields.ModifiedUserId[this] = value; }
        }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate
        {
            get { return Fields.ModifiedDate[this]; }
            set { Fields.ModifiedDate[this] = value; }
        }

        [DisplayName("Is Deleted")]
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

        [DisplayName("استان"), Expression("jProvince.[Name]")]
        public String ProvinceName
        {
            get { return Fields.ProvinceName[this]; }
            set { Fields.ProvinceName[this] = value; }
        }
        
        [DisplayName("عنوان"), Expression("jSwitchTransit.[Name]")]
        public String SwitchTransitName
        {
            get { return Fields.SwitchTransitName[this]; }
            set { Fields.SwitchTransitName[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }
        StringField INameRow.NameField
        {
            get { return Fields.ProvinceName; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public SwitchTransitProvinceRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProvinceId;
            public Int32Field SwitchTransitId;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;

            public StringField ProvinceName;
            public StringField SwitchTransitName;

            public RowFields()
                : base("[dbo].[SwitchTransitProvince]")
            {
                LocalTextPrefix = "Case.SwitchTransitProvince";
            }
        }
    }
}