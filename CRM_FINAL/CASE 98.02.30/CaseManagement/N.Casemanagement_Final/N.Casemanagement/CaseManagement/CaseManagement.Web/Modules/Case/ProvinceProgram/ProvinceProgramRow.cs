

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

    [ConnectionKey("Default"), DisplayName("برنامه سالانه استان ها"), InstanceName("برنامه سالانه استان ها"), TwoLevelCached]
    [ReadPermission(Case.PermissionKeys.JustRead)]
    [ModifyPermission(Case.PermissionKeys.Activity)]
    public sealed class ProvinceProgramRow : Row, IIdRow
    {
        [DisplayName("Id"), Column("ID"), Identity]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("برنامه"), NotNull]
        public Int64? Program
        {
            get { return Fields.Program[this]; }
            set { Fields.Program[this] = value; }
        }

        [DisplayName("نشتی کل"), NotNull]
        public Int64? TotalLeakage
        {
            get { return Fields.TotalLeakage[this]; }
            set { Fields.TotalLeakage[this] = value; }
        }

        [DisplayName("نشتی قابل وصول"), NotNull]
        public Int64? RecoverableLeakage
        {
            get { return Fields.RecoverableLeakage[this]; }
            set { Fields.RecoverableLeakage[this] = value; }
        }

        [DisplayName("مبلغ مصوب"), NotNull]
        public Int64? Recovered
        {
            get { return Fields.Recovered[this]; }
            set { Fields.Recovered[this] = value; }
        }

        [DisplayName("درصد نشتی کل")]
        public String PercentTotalLeakage
        {
            get { return Fields.PercentTotalLeakage[this]; }
            set { Fields.PercentTotalLeakage[this] = value; }
        }

        [DisplayName("درصد نشتی قابل وصول")]
        public String PercentRecoverableLeakage
        {
            get { return Fields.PercentRecoverableLeakage[this]; }
            set { Fields.PercentRecoverableLeakage[this] = value; }
        }

        [DisplayName("درصد مصوب")]
        public String PercentRecovered
        {
            get { return Fields.PercentRecovered[this]; }
            set { Fields.PercentRecovered[this] = value; }
        }

        [DisplayName("درصد مصوب به نشتی کل")]
        public String PercentRecoveredonTotal
        {
            get { return Fields.PercentRecoveredonTotal[this]; }
            set { Fields.PercentRecoveredonTotal[this] = value; }
        }

        [DisplayName(" پیشرفت نشتی کل نسبت به سال گذشته")]
        public String PercentTotal94to95
        {
            get { return Fields.PercentTotal94to95[this]; }
            set { Fields.PercentTotal94to95[this] = value; }
        }

        [DisplayName("پیشرفت مصوب نسبت به سال گذشته")]
        public String PercentRecovered94to95
        {
            get { return Fields.PercentRecovered94to95[this]; }
            set { Fields.PercentRecovered94to95[this] = value; }
        }

        [DisplayName("تعداد فعالیت"), NotNull]
        public Int32? ActivityCount
        {
            get { return Fields.ActivityCount[this]; }
            set { Fields.ActivityCount[this] = value; }
        }

        [DisplayName("فعالیت بدون تکرار"), NotNull]
        public Int32? ActivityNonRepeatCount
        {
            get { return Fields.ActivityNonRepeatCount[this]; }
            set { Fields.ActivityNonRepeatCount[this] = value; }
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

        [DisplayName("تاریخ آخرین اجرا")]
        public DateTime? LastActivityDate
        {
            get { return Fields.LastActivityDate[this]; }
            set { Fields.LastActivityDate[this] = value; }
        }

        [DisplayName("استان"), Column("ProvinceID"), ForeignKey("[dbo].[Province]", "ID"), LeftJoin("jProvince"), TextualField("ProvinceName")]
        [LookupEditor("Case.Province")]
        public Int32? ProvinceId
        {
            get { return Fields.ProvinceId[this]; }
            set { Fields.ProvinceId[this] = value; }
        }

        [DisplayName("سال"), Column("YearID"), ForeignKey("[dbo].[Year]", "ID"), LeftJoin("jYear"), TextualField("Year")]
        [LookupEditor("Case.Year")]
        public Int32? YearId
        {
            get { return Fields.YearId[this]; }
            set { Fields.YearId[this] = value; }
        }

        [DisplayName("استان"), Expression("jProvince.[Name]")]
        public String ProvinceName
        {
            get { return Fields.ProvinceName[this]; }
            set { Fields.ProvinceName[this] = value; }
        }

        [DisplayName("سال"), Expression("jYear.[Year]")]
        public string Year
        {
            get { return Fields.Year[this]; }
            set { Fields.Year[this] = value; }
        }       

        IIdField IIdRow.IdField
        {
            get { return Fields.Id; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public ProvinceProgramRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int64Field Program;
            public Int64Field TotalLeakage;
            public Int64Field RecoverableLeakage;
            public Int64Field Recovered;
            public StringField PercentTotalLeakage;
            public StringField PercentRecoverableLeakage;
            public StringField PercentRecovered;
            public StringField PercentRecoveredonTotal;
            public StringField PercentTotal94to95;
            public StringField PercentRecovered94to95;
            public Int32Field ActivityCount;
            public Int32Field ActivityNonRepeatCount;
            public Int32Field CreatedUserId;
            public DateTimeField CreatedDate;
            public Int32Field ModifiedUserId;
            public DateTimeField ModifiedDate;
            public BooleanField IsDeleted;
            public Int32Field DeletedUserId;
            public DateTimeField DeletedDate;
            public DateTimeField LastActivityDate;
            public Int32Field ProvinceId;
            public Int32Field YearId;

            public StringField ProvinceName;
            public StringField Year;

            public RowFields()
                : base("[dbo].[ProvinceProgram]")
            {
                LocalTextPrefix = "Case.ProvinceProgram";
            }
        }
    }
}