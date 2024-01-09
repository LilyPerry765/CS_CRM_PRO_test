
namespace CaseManagement.Messaging.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Messaging.VwMessages")]
    [BasedOnRow(typeof(Entities.VwMessagesRow))]
    public class VwMessagesColumns
    {
        public Int32 Id { get; set; }
        public Int32 SenderId { get; set; }
        public Boolean Seen { get; set; }
        public DateTime SeenDate { get; set; }
        [EditLink]
        public String Subject { get; set; }
        public String Body { get; set; }
        public DateTime InsertedDate { get; set; }
        public Int32 RecieverId { get; set; }
        public Int32 MessageId { get; set; }
        public String SenderName { get; set; }
        public String ReceiverName { get; set; }
    }
}