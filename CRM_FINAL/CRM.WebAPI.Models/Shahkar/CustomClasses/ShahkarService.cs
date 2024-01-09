using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    public class ShahkarService
    {
        #region Properties and Fields

        public int type { get; set; }
        public string phoneNumber { get; set; }
        public string centerName { get; set; }
        public string province { get; set; }
        public string county { get; set; }
        public string kafu { get; set; }
        public string post { get; set; }
        public int credit { get; set; }
        public int status { get; set; }
        public int general { get; set; }

        #endregion

        #region Constructors

        public ShahkarService()
        {
            //تلفن ثابت
            this.type = 1;
            //فعال
            this.status = 1;
        } 

        #endregion
    }
}
