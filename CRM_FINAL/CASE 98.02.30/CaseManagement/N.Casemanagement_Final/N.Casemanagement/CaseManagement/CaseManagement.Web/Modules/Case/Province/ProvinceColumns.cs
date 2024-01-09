
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.Province")]
    [BasedOnRow(typeof(Entities.ProvinceRow))]
    public class ProvinceColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(200)]
        public String Name { get; set; }
        [NumberFormatter]
        public Int32 Code{ get; set; }        
        [Width(200)]
        public String LeaderName { get; set; }
        public String ManagerName { get; set; }       
    }
}