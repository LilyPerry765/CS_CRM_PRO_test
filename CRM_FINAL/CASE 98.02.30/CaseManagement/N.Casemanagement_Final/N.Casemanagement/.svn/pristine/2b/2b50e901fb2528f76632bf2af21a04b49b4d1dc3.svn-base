
namespace CaseManagement.Case.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Case.ActivityGroup")]
    [BasedOnRow(typeof(Entities.ActivityGroupRow))]
    public class ActivityGroupForm
    {
        [Width(1000)]
        public String Name { get; set; }
        [Width(1000)]
        public String EnglishName { get; set; }

        [Width(400),Insertable(false),Updatable(false)]
        public Int32 Code { get; set; }
        //public Int32 CreatedUserId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public Int32 ModifiedUserId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public Boolean IsDeleted { get; set; }
        //public Int32 DeletedUserId { get; set; }
        //public DateTime DeletedDate { get; set; }
    }
}