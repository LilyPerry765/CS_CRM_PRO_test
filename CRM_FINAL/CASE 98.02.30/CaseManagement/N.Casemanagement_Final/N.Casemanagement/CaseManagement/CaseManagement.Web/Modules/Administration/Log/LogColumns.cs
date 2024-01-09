
namespace CaseManagement.Administration.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Administration.Log")]
    [BasedOnRow(typeof(Entities.LogRow))]
    public class LogColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        //[EditLink]
        //public String TableName { get; set; }
        [Width(200)]
        public String PersianTableName { get; set; }
        //public Int32 RecordId { get; set; }
        [Width(200)]
        public Int32 RecordName { get; set; }
        [Width(350)]
        public string IP { get; set; }
        [Width(140), QuickFilter]
        public Int32 ActionID { get; set; }
        [QuickFilter]
        public string DisplayName { get; set; }
        [Width(180), QuickFilter]
        public string ProvinceName { get; set; }
        [QuickFilter]
        public DateTime InsertDate { get; set; }
    }
}