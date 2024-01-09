
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.Province")]
    [BasedOnRow(typeof(Entities.ProvinceRow))]
    public class ProvinceForm
    {
        [TextAreaEditor(Rows = 2)]
        public String Name { get; set; }
      //  [TextAreaEditor(Rows = 2)]
      //  public String EnglishName { get; set; }
        
        public String ManagerName { get; set; }
        public Int32 Code { get; set; }  
    }
}