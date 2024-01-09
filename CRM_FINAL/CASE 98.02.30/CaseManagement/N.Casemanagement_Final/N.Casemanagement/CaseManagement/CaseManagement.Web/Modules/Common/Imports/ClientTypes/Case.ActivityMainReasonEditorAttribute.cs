using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace CaseManagement.Case
{
    public partial class ActivityMainReasonEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "CaseManagement.Case.ActivityMainReasonEditor";

        public ActivityMainReasonEditorAttribute()
            : base(Key)
        {
        }
    }
}

