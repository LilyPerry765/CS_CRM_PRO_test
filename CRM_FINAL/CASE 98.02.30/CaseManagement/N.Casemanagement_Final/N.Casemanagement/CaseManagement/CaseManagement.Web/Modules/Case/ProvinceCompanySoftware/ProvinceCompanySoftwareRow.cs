﻿

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

    [ConnectionKey("Default"), DisplayName("ProvinceCompanySoftware"), InstanceName("ProvinceCompanySoftware"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class ProvinceCompanySoftwareRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("استان"), Column("ProvinveID"), NotNull, ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvinve"), TextualField("ProvinveName")]
        public Int32? ProvinveId
        {
            get { return Fields.ProvinveId[this]; }
            set { Fields.ProvinveId[this] = value; }
        }

        [DisplayName("شرکت"), Column("CompanyID"), ForeignKey("[dbo].[Company]", "ID"), LeftJoin("jCompany"), TextualField("CompanyName")]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }

        [DisplayName("نرم افزار"), Column("SoftwareID"), NotNull, ForeignKey("[dbo].[Software]", "ID"), LeftJoin("jSoftware"), TextualField("SoftwareName")]
        public Int32? SoftwareId
        {
            get { return Fields.SoftwareId[this]; }
            set { Fields.SoftwareId[this] = value; }
        }

        [DisplayName("وضعیت")]
        public SoftwareStatus? StatusID
        {
            get { return (SoftwareStatus?)Fields.StatusID[this]; }
            set { Fields.StatusID[this] = (Int32?)value; }
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

        [DisplayName("Modified User Id"), Column("ModifiedUserID"), NotNull]
        public Int32? ModifiedUserId
        {
            get { return Fields.ModifiedUserId[this]; }
            set { Fields.ModifiedUserId[this] = value; }
        }

        [DisplayName("Modified Date"), NotNull]
        public DateTime? ModifiedDate
        {
            get { return Fields.ModifiedDate[this]; }
            set { Fields.ModifiedDate[this] = value; }
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

        [DisplayName("استان"), Expression("jProvinve.[Name]")]
        public String ProvinveName
        {
            get { return Fields.ProvinveName[this]; }
            set { Fields.ProvinveName[this] = value; }
        }

        [DisplayName("شرکت"), Expression("jCompany.[Name]")]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("نرم افزار"), Expression("jSoftware.[Name]")]
        public String SoftwareName
        {
            get { return Fields.SoftwareName[this]; }
            set { Fields.SoftwareName[this] = value; }
        }
        
        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ProvinceCompanySoftwareRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProvinveId;
            public Int32Field CompanyId;
            public Int32Field SoftwareId;
            public Int32Field StatusID;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;

            public StringField ProvinveName;
            public StringField CompanyName;
            public StringField SoftwareName;

            public RowFields()
                : base("[dbo].[ProvinceCompanySoftware]")
            {
                LocalTextPrefix = "Case.ProvinceCompanySoftware";
            }
        }
    }
}