
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ProvinceProgram")]
    [BasedOnRow(typeof(Entities.ProvinceProgramRow))]
    public class ProvinceProgramColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [ Width(150), QuickFilter]
        public string ProvinceName { get; set; }
        [QuickFilter, Width(50), MinuteFormatter]
        public string Year { get; set; }
      //  [Width(150)]
       // public Int32 ActivityCount { get; set; }
       // [Width(150)]
       // public Int32 ActivityNonRepeatCount { get; set; }
       // [Width(150)]
      //  public DateTime LastActivityDate { get; set; }
        [Width(150), NumberFormatter]
        public Int64 Program { get; set; }
        [Width(120),NumberFormatter]
        public Int64 TotalLeakage { get; set; }
        [Width(120), NumberFormatter]
        public Int64 RecoverableLeakage { get; set; }
        [Width(120), NumberFormatter]
        public Int64 Recovered { get; set; }
        [Width(140), NumberFormatter]
        public string PercentTotalLeakage { get; set; }
        [Width(155), NumberFormatter]
        public Int64 PercentRecoverableLeakage { get; set; }
        [Width(140), NumberFormatter]
        public string PercentRecovered { get; set; }
        [Width(200), NumberFormatter]
        public string PercentRecoveredonTotal { get; set; }        
        [Width(200), NumberFormatter]
        public string PercentTotal94to95 { get; set; }
        [Width(200), NumberFormatter]
        public string PercentRecovered94to95 { get; set; }
    }
}