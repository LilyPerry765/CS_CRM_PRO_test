using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace CaseManagement.Case
{
    public partial class ActivityCorrectionOperationEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "CaseManagement.Case.ActivityCorrectionOperationEditor";

        public ActivityCorrectionOperationEditorAttribute()
            : base(Key)
        {
        }
    }
}

