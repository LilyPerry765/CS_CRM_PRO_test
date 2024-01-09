
namespace CaseManagement.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.UserSupportGroup")]
    [BasedOnRow(typeof(Entities.UserSupportGroupRow))]
    public class UserSupportGroupForm
    {
        public Int32 UserId { get; set; }
        public Int32 GroupId { get; set; }
    }
}