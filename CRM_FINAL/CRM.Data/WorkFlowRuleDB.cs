using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class WorkFlowRuleDB
    {
        
        public static WorkFlowRule GetWorkFlowRuleByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetWorkFlowRuleCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules
                    .Select(t => new CheckableItem
                    {
                        LongID = t.ID,
                        Name = t.ID.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}