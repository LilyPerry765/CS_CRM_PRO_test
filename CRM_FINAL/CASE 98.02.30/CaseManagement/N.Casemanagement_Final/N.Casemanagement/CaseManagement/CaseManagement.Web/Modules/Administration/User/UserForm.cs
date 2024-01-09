namespace CaseManagement.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [FormScript("Administration.User")]
    [BasedOnRow(typeof(Entities.UserRow))]
    public class UserForm
    {
        [Category("اطلاعات عمومی")]
        public String Username { get; set; }
        public String DisplayName { get; set; }
        public String EmployeeID { get; set; }
       // [EmailEditor]
        public String Email { get; set; }
        public String Rank { get; set; }
        [PasswordEditor, Required(true)]
        public String Password { get; set; }
        [PasswordEditor, OneWay, Required(true)]
        public String PasswordConfirm { get; set; }
        
        [Category("اطلاعات تماس")]
        public String TelephoneNo1 { get; set; }
       // public String TelephoneNo2 { get; set; }
        public String MobileNo { get; set; }
        public String Degree { get; set; }

        [Category("دسترسی")]
        public Int32 ProvinceId { get; set; }
        public List<Int32> ProvinceList { get; set; }

        [Category("عملیات")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Category("تصویر")]
        public string ImagePath { get; set; }

    }
}