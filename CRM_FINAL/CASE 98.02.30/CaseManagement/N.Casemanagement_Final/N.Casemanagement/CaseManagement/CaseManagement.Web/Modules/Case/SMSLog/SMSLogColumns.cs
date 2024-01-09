
namespace CaseManagement.Case.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Case.SMSLog")]
    [BasedOnRow(typeof(Entities.SMSLogRow))]
    public class SMSLogColumns
    {
        [Width(105), DisplayName("شناسه"), AlignRight, MinuteFormatter]
        public Int32 Id { get; set; }

        [AlignRight, Width(120), QuickFilter]
        public string ReceiverProvinceName { get; set; }

        [AlignRight, Width(150), MinuteFormatter]
        public Int64 ActivityRequestId { get; set; }

        [AlignRight, Width(150)]
        public string ReceiverUserName { get; set; }

        [AlignRight, Width(200), QuickFilter]
        public string ReceiverRoleRoleName { get; set; }

        [AlignRight, Width(130), MinuteFormatter]    
        public string MobileNumber { get; set; }

        [AlignRight, Width(120), QuickFilter]
        public DateTime InsertDate { get; set; }

        [AlignRight, Width(120)]
        public Boolean Is_modified { get; set; }

        [AlignRight, Width(150)]
        public DateTime ModifiedDate { get; set; }
 
    }
}