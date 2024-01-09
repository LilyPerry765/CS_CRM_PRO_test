using Serenity.Services;
using System.Collections.Generic;

namespace CaseManagement.Case
{
    public class ActivityRequestListRequest : ListRequest
    {
        public List<int> ActivityRequests { get; set; }
    }
}