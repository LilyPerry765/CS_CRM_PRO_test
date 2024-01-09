
namespace CaseManagement.Administration.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Administration.Notification")]
    [BasedOnRow(typeof(Entities.NotificationRow))]
    public class NotificationColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int32 GroupId { get; set; }
        public Int32 UserId { get; set; }
        [EditLink]
        public String Message { get; set; }
    }
}