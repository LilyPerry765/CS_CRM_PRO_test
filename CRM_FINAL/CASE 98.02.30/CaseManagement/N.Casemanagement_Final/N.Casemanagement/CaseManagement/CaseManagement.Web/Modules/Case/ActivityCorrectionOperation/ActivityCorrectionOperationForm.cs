
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityCorrectionOperation")]
    [BasedOnRow(typeof(Entities.ActivityCorrectionOperationRow))]
    public class ActivityCorrectionOperationForm
    {
        [TextAreaEditor(Rows = 10)]
        public String Body { get; set; }
    }
}