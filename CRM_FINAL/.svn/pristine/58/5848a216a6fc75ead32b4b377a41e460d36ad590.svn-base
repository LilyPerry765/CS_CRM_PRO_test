using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    public class ShahkarRawResult
    {
        #region Properties and Fields

        public string requestId { get; set; }
        public int response { get; set; }
        public string id { get; set; }
        public string result { get; set; }
        public string comment { get; set; }
        public string followNo { get; set; }

        private bool _hasInvalidData;
        public bool HasInvalidData
        {
            get
            {
                if (this.response != 200)
                {
                    _hasInvalidData = true;
                }
                else
                {
                    _hasInvalidData = false;
                }
                return _hasInvalidData;
            }
            set
            {
                _hasInvalidData = value;
            }
        }

        #endregion
    }
}
