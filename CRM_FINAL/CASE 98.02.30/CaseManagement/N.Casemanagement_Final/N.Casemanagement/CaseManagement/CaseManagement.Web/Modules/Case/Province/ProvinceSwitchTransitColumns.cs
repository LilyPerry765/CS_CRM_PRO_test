
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.SwitchTransitProvince")]
    [BasedOnRow(typeof(Entities.SwitchTransitProvinceRow))]
    public class ProvinceSwitchTransitColumns
    {
        [EditLink]
        public String SwitchTransitName { get; set; }
    }
}