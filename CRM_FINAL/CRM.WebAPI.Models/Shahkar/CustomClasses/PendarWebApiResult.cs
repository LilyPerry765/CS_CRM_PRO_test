using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    public class PendarWebApiResult
    {
        #region Properties and Fields

        public string RawResultFromShahkar { get; set; }

        public string SystemError { get; set; }

        public bool SystemHasError
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SystemError))
                {
                    return true;
                }
                return false;
            }
        }

        public bool ShahkarServerIsAccessible { get; set; }

        #endregion

        #region Constructors

        public PendarWebApiResult()
        {
            this.ShahkarServerIsAccessible = true;
        }

        #endregion
    }
}
