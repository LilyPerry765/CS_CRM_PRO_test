using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class AgentDB
    {
        public static Agent GetAgentById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                Agent foundAgent = context.Agents.Where(ag => ag.ID == id).SingleOrDefault();
                return foundAgent;
            }
        }
    }
}
