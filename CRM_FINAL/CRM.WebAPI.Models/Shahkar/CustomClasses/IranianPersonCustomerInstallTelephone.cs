using CRM.WebAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    public class IranianPersonCustomerInstallTelephone : IResult
    {
        #region Properties and Fields

        #region Customer Info

        public string name { get; set; }

        public string family { get; set; }

        public string fatherName { get; set; }
        public string certificateNo { get; set; }
        public string birthDate { get; set; }
        public string birthPlace { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }

        /// <summary>
        /// 1  = مرد
        /// 2 = زن
        /// 3 = ناشناخته
        /// </summary>
        public int gender { get; set; }
        public string identificationNo { get; set; }
        public int identificationType { get; set; }

        public int iranian { get; set; }
        /// <summary>
        /// نوع شخص
        /// 1 = حقیقی
        /// </summary>
        public int person { get; set; }

        #endregion

        public ShahkarAddress address { get; set; }

        public ShahkarService service { get; set; }

        public string requestId { get; set; }

        #endregion

        #region Constructor

        public IranianPersonCustomerInstallTelephone()
        {
            this.iranian = 1;
            this.person = 1;
            this.identificationType = 0;
        }

        #endregion
    }
}
