
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityHelp")]
    [BasedOnRow(typeof(Entities.ActivityHelpRow))]
    public class ActivityHelpForm
    {
        [Category("عمومی")]
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
    }
}