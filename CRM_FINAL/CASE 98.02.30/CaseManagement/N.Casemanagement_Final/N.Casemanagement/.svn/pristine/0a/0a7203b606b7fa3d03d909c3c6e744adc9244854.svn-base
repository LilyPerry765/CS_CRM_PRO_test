

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

    [ConnectionKey("Default"), DisplayName("CalendarEvent"), InstanceName("CalendarEvent"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    public sealed class CalendarEventRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Title"), Size(100), NotNull, QuickSearch]
        public String Title
        {
            get { return Fields.Title[this]; }
            set { Fields.Title[this] = value; }
        }

        [DisplayName("All Day")]
        public Boolean? AllDay
        {
            get { return Fields.AllDay[this]; }
            set { Fields.AllDay[this] = value; }
        }

        [DisplayName("Start Date")]
        public DateTime? StartDate
        {
            get { return Fields.StartDate[this]; }
            set { Fields.StartDate[this] = value; }
        }

        [DisplayName("End Date")]
        public DateTime? EndDate
        {
            get { return Fields.EndDate[this]; }
            set { Fields.EndDate[this] = value; }
        }

        [DisplayName("Url"), Column("URL"), Size(200)]
        public String Url
        {
            get { return Fields.Url[this]; }
            set { Fields.Url[this] = value; }
        }

        [DisplayName("Class Name"), Size(100)]
        public String ClassName
        {
            get { return Fields.ClassName[this]; }
            set { Fields.ClassName[this] = value; }
        }

        [DisplayName("Is Editable")]
        public Boolean? IsEditable
        {
            get { return Fields.IsEditable[this]; }
            set { Fields.IsEditable[this] = value; }
        }

        [DisplayName("Is Overlap")]
        public Boolean? IsOverlap
        {
            get { return Fields.IsOverlap[this]; }
            set { Fields.IsOverlap[this] = value; }
        }

        [DisplayName("Color"), Size(50)]
        public String Color
        {
            get { return Fields.Color[this]; }
            set { Fields.Color[this] = value; }
        }

        [DisplayName("Background Color"), Column("backgroundColor"), Size(50)]
        public String BackgroundColor
        {
            get { return Fields.BackgroundColor[this]; }
            set { Fields.BackgroundColor[this] = value; }
        }

        [DisplayName("Text Color"), Size(50)]
        public String TextColor
        {
            get { return Fields.TextColor[this]; }
            set { Fields.TextColor[this] = value; }
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

        public CalendarEventRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Title;
            public BooleanField AllDay;
            public DateTimeField StartDate;
            public DateTimeField EndDate;
            public StringField Url;
            public StringField ClassName;
            public BooleanField IsEditable;
            public BooleanField IsOverlap;
            public StringField Color;
            public StringField BackgroundColor;
            public StringField TextColor;

            public RowFields()
                : base("[dbo].[CalendarEvent]")
            {
                LocalTextPrefix = "Administration.CalendarEvent";
            }
        }
    }
}