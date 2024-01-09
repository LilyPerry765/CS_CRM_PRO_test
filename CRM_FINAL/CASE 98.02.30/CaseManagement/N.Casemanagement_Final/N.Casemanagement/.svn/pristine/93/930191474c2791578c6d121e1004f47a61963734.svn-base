

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

    [ConnectionKey("Default"), DisplayName("ProvinceProgramLog"), InstanceName("ProvinceProgramLog"), TwoLevelCached]
    [ReadPermission("Administration")]
    [ModifyPermission("Administration")]
    public sealed class ProvinceProgramLogRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("ActivityRequestID")]
        public Int64? ActivityRequestID
        {
            get { return Fields.ActivityRequestID[this]; }
            set { Fields.ActivityRequestID[this] = value; }
        }

        [DisplayName("Province Id"), Column("provinceID")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [DisplayName("Year Id"), Column("YearID")]
        public Int32? YearId
        {
            get { return Fields.YearId[this]; }
            set { Fields.YearId[this] = value; }
        }

        [DisplayName("Old Total Leakage")]
        public Int64? OldTotalLeakage
        {
            get { return Fields.OldTotalLeakage[this]; }
            set { Fields.OldTotalLeakage[this] = value; }
        }

        [DisplayName("New Total Leakage")]
        public Int64? NewTotalLeakage
        {
            get { return Fields.NewTotalLeakage[this]; }
            set { Fields.NewTotalLeakage[this] = value; }
        }

        [DisplayName("Old Recoverable Leakage")]
        public Int64? OldRecoverableLeakage
        {
            get { return Fields.OldRecoverableLeakage[this]; }
            set { Fields.OldRecoverableLeakage[this] = value; }
        }

        [DisplayName("New Recoverable Leakage")]
        public Int64? NewRecoverableLeakage
        {
            get { return Fields.NewRecoverableLeakage[this]; }
            set { Fields.NewRecoverableLeakage[this] = value; }
        }

        [DisplayName("Old Recovered")]
        public Int64? OldRecovered
        {
            get { return Fields.OldRecovered[this]; }
            set { Fields.OldRecovered[this] = value; }
        }

        [DisplayName("New Recovered")]
        public Int64? NewRecovered
        {
            get { return Fields.NewRecovered[this]; }
            set { Fields.NewRecovered[this] = value; }
        }

        [DisplayName("User Id"), Column("UserID")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Insert Date")]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ProvinceProgramLogRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int64Field ActivityRequestID;
            public Int32Field ProvinceId;
            public Int32Field YearId;
            public Int64Field OldTotalLeakage;
            public Int64Field NewTotalLeakage;
            public Int64Field OldRecoverableLeakage;
            public Int64Field NewRecoverableLeakage;
            public Int64Field OldRecovered;
            public Int64Field NewRecovered;
            public Int32Field UserId;
            public DateTimeField InsertDate;

            public RowFields()
                : base("[dbo].[ProvinceProgramLog]")
            {
                LocalTextPrefix = "Case.ProvinceProgramLog";
            }
        }
    }
}