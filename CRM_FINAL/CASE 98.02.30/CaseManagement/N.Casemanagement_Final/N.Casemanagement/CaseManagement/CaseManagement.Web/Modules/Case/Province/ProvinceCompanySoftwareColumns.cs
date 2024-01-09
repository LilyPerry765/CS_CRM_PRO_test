
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.ProvinceCompanySoftware")]
    [BasedOnRow(typeof(Entities.ProvinceCompanySoftwareRow))]
    public class ProvinceCompanySoftwareColumns
    {
        public string SoftwareName { get; set; }
        public string CompanyName { get; set; }
        public SoftwareStatus StatusID { get; set; }
    }
}