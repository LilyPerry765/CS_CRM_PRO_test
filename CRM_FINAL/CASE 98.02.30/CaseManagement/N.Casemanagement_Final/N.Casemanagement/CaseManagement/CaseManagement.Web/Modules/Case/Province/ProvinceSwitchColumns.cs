
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.SwitchProvince")]
    [BasedOnRow(typeof(Entities.SwitchProvinceRow))]
    public class ProvinceSwitchColumns
    {
        [EditLink]
        public String SwitchName { get; set; }
    }
}