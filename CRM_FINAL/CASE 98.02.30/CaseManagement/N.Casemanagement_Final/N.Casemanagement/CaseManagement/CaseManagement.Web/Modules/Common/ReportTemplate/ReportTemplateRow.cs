﻿

namespace ReportTemplateDB.Common.Entities
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), DisplayName("ReportTemplate"), InstanceName("ReportTemplate"), TwoLevelCached]
    [ReadPermission(CaseManagement.Case.PermissionKeys.JustRead)]
    [ModifyPermission("Administration")]
    public sealed class ReportTemplateRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Template")]
        public Stream Template
        {
            get { return Fields.Template[this]; }
            set { Fields.Template[this] = value; }
        }

        [DisplayName("Title"), Size(100), QuickSearch]
        public String Title
        {
            get { return Fields.Title[this]; }
            set { Fields.Title[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Title; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ReportTemplateRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StreamField Template;
            public StringField Title;

            public RowFields()
                : base("[dbo].[ReportTemplate]")
            {
                LocalTextPrefix = "Common.ReportTemplate";
            }
        }
    }
}