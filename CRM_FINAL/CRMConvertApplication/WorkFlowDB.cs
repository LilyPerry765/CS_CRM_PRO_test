using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class WorkFlowDB
    {
        public static List<int> GetListNextStatesID(int actionID, int currentStatusID, int? workUnitID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules.Where(t => t.ActionID == (int)actionID && t.CurrentStatusID == currentStatusID && (!workUnitID.HasValue || t.SenderID == workUnitID) && t.SpecialConditionsID == null)
                   .OrderBy(t => t.SenderID)
                   .Select(t => t.NextStatusID).ToList();
            }
        }
    }
}
