

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

    [ConnectionKey("Default"), DisplayName("گروه فعالیت"), InstanceName("گروه فعالیت"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    [LookupScript("Case.ActivityGroup")]
    public sealed class ActivityGroupRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("عنوان"), Size(1000), NotNull, QuickSearch]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("عنوان انگلیسی"), Size(1000)]
        public String EnglishName
        {
            get { return Fields.EnglishName[this]; }
            set { Fields.EnglishName[this] = value; }
        }

        [DisplayName("کد"), NotNull]
        public Int32? Code
        {
            get { return Fields.Code[this]; }
            set { Fields.Code[this] = value; }
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

        public ActivityGroupRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField EnglishName;
            public Int32Field Code;
            

            public RowFields()
                : base("[dbo].[ActivityGroup]")
            {
                LocalTextPrefix = "Case.ActivityGroup";
            }
        }
    }
}