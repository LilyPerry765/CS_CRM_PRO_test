using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class SpecialConditionPrioritizeDB
    {
        public static DB.SpecialConditions? GetHighestPriority(List<int> specialCondition)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<string> specialConditionNames = new List<string>();
                specialCondition.ForEach(item => specialConditionNames.Add(Enum.GetName(typeof(DB.SpecialConditions), item)));

                List<SpecialConditionPrioritize> specialConditionPrioritize = context.SpecialConditionPrioritizes.Where(t => specialConditionNames.Contains(t.SpecialCondition)).ToList();
                if (specialConditionPrioritize.Count == specialConditionNames.Count)
                {
                   return (DB.SpecialConditions)System.Enum.Parse(typeof(DB.SpecialConditions), specialConditionPrioritize.OrderByDescending(t=>t.Prioritize).First().SpecialCondition);
                }
                else
                {
                    return null;
                }

            }
        }
    }
}
