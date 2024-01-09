using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ReportTemplateDB
    {
        public static int? GetReportTemplateIdByRequestStepId(int requestStepId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int? reportTemplateId = null;
                RequestStep result = context.RequestSteps.Where(t => t.ID == requestStepId).SingleOrDefault();
                if (result != null)
                {
                    reportTemplateId = result.ReportTemplateID;
                }
                return reportTemplateId;
            }
        }
    }
}
