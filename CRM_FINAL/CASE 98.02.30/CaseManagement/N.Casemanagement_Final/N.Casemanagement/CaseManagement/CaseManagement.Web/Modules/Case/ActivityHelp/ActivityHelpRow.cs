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

    [ConnectionKey("Default"), DisplayName("Activity"), InstanceName("Activity"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    [LookupScript("Case.ActivityHelp")]
    public sealed class ActivityHelpRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("کد"), NotNull]
        public String Code
        {
            get { return Fields.Code[this]; }
            set { Fields.Code[this] = value; }
        }

        [DisplayName("عنوان"), Size(1000), NotNull, QuickSearch]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("عنوان"), Size(1000), NotNull, QuickSearch]
        public String CodeName
        {
            get { return Fields.CodeName[this]; }
            set { Fields.CodeName[this] = value; }
        }

        [DisplayName("عنوان انگلیسی"), Size(1000)]
        public String EnglishName
        {
            get { return Fields.EnglishName[this]; }
            set { Fields.EnglishName[this] = value; }
        }

        [DisplayName("هدف"), Size(1000), NotNull]
        public String Objective
        {
            get { return Fields.Objective[this]; }
            set { Fields.Objective[this] = value; }
        }

        [DisplayName("هدف انگلیسی"), Size(1000)]
        public String EnglishObjective
        {
            get { return Fields.EnglishObjective[this]; }
            set { Fields.EnglishObjective[this] = value; }
        }

        

        [DisplayName("گروه فعالیت"), Column("GroupID"), ForeignKey("[dbo].[ActivityGroup]", "ID"), LeftJoin("jGroup"), TextualField("GroupName")]
        [LookupEditor("Case.ActivityGroup")]
        public Int32? GroupId
        {
            get { return Fields.GroupId[this]; }
            set { Fields.GroupId[this] = value; }
        }

        [DisplayName("دوره تکرار"), Column("RepeatTermID"), ForeignKey("[dbo].[RepeatTerm]", "ID"), LeftJoin("jRepeatTerm"), TextualField("RepeatTermName")]
        [LookupEditor("Case.RepeatTerm")]
        public Int32? RepeatTermId
        {
            get { return Fields.RepeatTermId[this]; }
            set { Fields.RepeatTermId[this] = value; }
        }

        [DisplayName("گروه فعالیت"), Expression("jGroup.[Name]")]
        public String GroupName
        {
            get { return Fields.GroupName[this]; }
            set { Fields.GroupName[this] = value; }
        }

        [DisplayName("دوره تکرار"), Expression("jRepeatTerm.[Name]")]
        public String RepeatTermName
        {
            get { return Fields.RepeatTermName[this]; }
            set { Fields.RepeatTermName[this] = value; }
        }

        [DisplayName("ناحیه بررسی کلیدی"), Size(1000)]
        public String KeyCheckArea
        {
            get { return Fields.KeyCheckArea[this]; }
            set { Fields.KeyCheckArea[this] = value; }
        }

        [DisplayName("منبع داده"), Size(1000)]
        public String DataSource
        {
            get { return Fields.DataSource[this]; }
            set { Fields.DataSource[this] = value; }
        }

        [DisplayName("روش عملکرد"), Size(1000)]
        public String Methodology
        {
            get { return Fields.Methodology[this]; }
            set { Fields.Methodology[this] = value; }
        }

        [DisplayName("نقاط کلیدی"), Size(1000)]
        public String KeyFocus
        {
            get { return Fields.KeyFocus[this]; }
            set { Fields.KeyFocus[this] = value; }
        }

        [DisplayName("فعالیت"), Size(1000)]
        public String Action
        {
            get { return Fields.Action[this]; }
            set { Fields.Action[this] = value; }
        }

        [DisplayName("شاخص عملکردی"), Size(1000)]
        public String KPI
        {
            get { return Fields.KPI[this]; }
            set { Fields.KPI[this] = value; }
        }

        [DisplayName("شرح رویداد"), Size(1000)]
        public String EventDescription
        {
            get { return Fields.EventDescription[this]; }
            set { Fields.EventDescription[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.CodeName; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ActivityHelpRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Code;
            public StringField Name;
            public StringField CodeName;
            public StringField EnglishName;
            public StringField Objective;
            public StringField EnglishObjective;
       
            public Int32Field GroupId;
            public Int32Field RepeatTermId;
            public StringField GroupName;
            public StringField RepeatTermName;

            public StringField KeyCheckArea;
            public StringField DataSource;
            public StringField Methodology;
            public StringField KeyFocus;
            public StringField Action;
            public StringField KPI;
            public StringField EventDescription;

            public RowFields()
                : base("[dbo].[Activity]")
            {
                LocalTextPrefix = "Case.ActivityHelp";
            }
        }
    }
}