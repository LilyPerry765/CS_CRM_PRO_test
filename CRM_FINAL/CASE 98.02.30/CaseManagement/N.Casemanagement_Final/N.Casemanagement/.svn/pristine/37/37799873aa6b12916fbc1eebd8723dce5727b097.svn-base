

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

    [ConnectionKey("Default"), DisplayName("ActivityCorrectionOperation"), InstanceName("ActivityCorrectionOperation"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    [LookupScript("Case.ActivityCorrectionOperation")]
    public sealed class ActivityCorrectionOperationRow : Row, IIdRow, INameRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Activity Id"), Column("ActivityID"), ForeignKey("[dbo].[Activity]", "ID"), LeftJoin("jActivity")]
        public Int32? ActivityId
        {
            get { return Fields.ActivityId[this]; }
            set { Fields.ActivityId[this] = value; }
        }

        [DisplayName("توضیحات"), NotNull, QuickSearch]
        public String Body
        {
            get { return Fields.Body[this]; }
            set { Fields.Body[this] = value; }
        }

        


        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.Body; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ActivityCorrectionOperationRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ActivityId;
            public StringField Body;
         

            public RowFields()
                : base("[dbo].[ActivityCorrectionOperation]")
            {
                LocalTextPrefix = "Case.ActivityCorrectionOperation";
            }
        }
    }
}