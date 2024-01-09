
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.Activity")]
    [BasedOnRow(typeof(Entities.ActivityRow))]
    public class ActivityForm
    {
        [Category("عمومی")]
        [Insertable(false), Updatable(false)]
        public Int32 Code { get; set; }
        [TextAreaEditor(Rows = 2)]
        public String Name { get; set; }
        [TextAreaEditor(Rows = 2)]
        public String EnglishName { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String Objective { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String EnglishObjective { get; set; }
        public Int32 GroupId { get; set; }
        public Int32 RepeatTermId { get; set; }
        public Int32 RequiredYearRepeatCount { get; set; }
        public Int64 Factor { get; set; }
        
        [Category("توضیحات")]           
        [TextAreaEditor(Rows = 5)]
        public String KeyCheckArea { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String DataSource { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String Methodology { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String KeyFocus { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String Action { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String KPI { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String EventDescription { get; set; }
        [EditorType("Case.ActivityMainReason")]
        public List<Int32> MainReasonList { get; set; }
        [EditorType("Case.ActivityCorrectionOperation")]
        public List<Int32> CorrectionOperationList { get; set; }
    }    
}