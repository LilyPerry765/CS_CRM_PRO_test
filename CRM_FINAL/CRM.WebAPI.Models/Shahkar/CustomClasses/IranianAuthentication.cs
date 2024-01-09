using CRM.WebAPI.Models.Interfaces;
using CRM.WebAPI.Models.Shahkar.Methods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    public class IranianAuthentication : IResult
    {
        #region Properties and Fields

        public string requestId { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string fatherName { get; set; }
        public int identificationType { get; set; }
        public string birthDate { get; set; }
        public string identificationNo { get; set; }
        public string certificateNo { get; set; }

        #endregion

        #region Constructors

        public IranianAuthentication()
        {
            
        }

        #endregion
    }
}
