

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

    [ConnectionKey("Default"), DisplayName("استان"), InstanceName("استان"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    [LookupScript("Case.Province")]
    public sealed class ProvinceRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }
        
        [LookupEditor("Case.Province")]
        [DisplayName("سرگروه"), Column("LeaderID"), ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvince"), TextualField("LeaderName")]
        public Int32? LeaderID
        {
            get { return Fields.LeaderID[this]; }
            set { Fields.LeaderID[this] = value; }
        }

        [DisplayName("عنوان"), Size(1000), NotNull, QuickSearch]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("کد استان"), NotNull]
        public Int32? Code
        {
            get { return Fields.Code[this]; }
            set { Fields.Code[this] = value; }
        }

        [DisplayName("عنوان انگلیسی"), Size(1000)]
        public String EnglishName
        {
            get { return Fields.EnglishName[this]; }
            set { Fields.EnglishName[this] = value; }
        }

        [DisplayName("مدیر منطقه"), Size(1000), NotNull]
        public String ManagerName
        {
            get { return Fields.ManagerName[this]; }
            set { Fields.ManagerName[this] = value; }
        }

        [DisplayName("شماره نامه"), Size(1000), NotNull]
        public String LetterNo
        {
            get { return Fields.LetterNo[this]; }
            set { Fields.LetterNo[this] = value; }
        }

        [LookupEditor("Case.PmoLevel")]
        [DisplayName("سطح PMO"), Column("PMOLevelID"), ForeignKey("[dbo].[PMOLevel]", "ID"), LeftJoin("jPmoLevel"), TextualField("PmoLevelName")]
        public Int32? PmoLevelId
        {
            get { return Fields.PmoLevelId[this]; }
            set { Fields.PmoLevelId[this] = value; }
        }

        [DisplayName("تاریخ نصب"), NotNull]
        public DateTime? InstallDate
        {
            get { return Fields.InstallDate[this]; }
            set { Fields.InstallDate[this] = value; }
        }

        

        [DisplayName("سطح PMO"), Expression("jPmoLevel.[Name]")]
        public String PmoLevelName
        {
            get { return Fields.PmoLevelName[this]; }
            set { Fields.PmoLevelName[this] = value; }
        }

        [DisplayName("سرگروه"), Expression("jProvince.[Name]")]
        public String LeaderName
        {
            get { return Fields.LeaderName[this]; }
            set { Fields.LeaderName[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Name; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ProvinceRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field LeaderID;
            public StringField Name;
            public Int32Field Code;
            public StringField EnglishName;
            public StringField ManagerName;
            public StringField LetterNo;
            public Int32Field PmoLevelId;
            public DateTimeField InstallDate;
          

            public StringField PmoLevelName;
            public StringField LeaderName; 

            public RowFields()
                : base("[dbo].[Province]")
            {
                LocalTextPrefix = "Case.Province";
            }
        }
    }
}