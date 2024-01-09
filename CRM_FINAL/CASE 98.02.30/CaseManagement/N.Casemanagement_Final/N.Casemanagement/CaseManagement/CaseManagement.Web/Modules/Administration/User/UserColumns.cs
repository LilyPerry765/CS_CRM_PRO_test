
namespace CaseManagement.Administration.Forms
{
    using Serenity.ComponentModel;
    using System;

    [ColumnsScript("Administration.User")]
    [BasedOnRow(typeof(Entities.UserRow))]
    public class UserColumns
    {
        //[EditLink, AlignRight, Width(55)]
        //public String UserId { get; set; }
        [EditLink, Width(200), MinuteFormatter]
        public String Username { get; set; }
        [Width(300)]
        public String DisplayName { get; set; }
        [Width(150), QuickFilter]
        public string ProvinceName { get; set; }
        [Width(100), QuickFilter]
        public Int32 IsIranTCI { get; set; }
        [Width(150)]
        public String Email { get; set; }
        [Width(200)]
        public String Rank { get; set; }
        [Width(150)]
        public String EmployeeID { get; set; }
        public bool IsActive { get; set; }
        [AlignRight, Width(150), QuickFilter]
        public DateTime LastLoginDate { get; set; }
        

        //[Width(100)]
        //public String Source { get; set; }
    }
}