using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseManagement.Case
{
    public class ActivityRequest : ServiceRequest
    {
    }

    public class ActivityResponse : ServiceResponse
    {
        public List<Dictionary<string, object>> Values { get; set; }
    }
}