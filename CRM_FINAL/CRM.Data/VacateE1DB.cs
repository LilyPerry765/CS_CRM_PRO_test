using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class VacateE1DB
    {
        public static List<VacateE1Info> E1Info(long telephone)
        {
            using(MainDataContext context = new MainDataContext())
            {

                return context.Telephones.Where(t => t.TelephoneNo == telephone)
                               .Join(context.Buchts, t => t.SwitchPortID, t2 => t2.SwitchPortID, (t, t2) => new { telephone = t, otherBucht = t2 })
                               .Join(context.Buchts, t => t.otherBucht.ID, t2 => t2.BuchtIDConnectedOtherBucht, (t, t2) => new { telephone = t.telephone, otherBucht = t.otherBucht, Bucht = t2 })
                               .Select(t => new VacateE1Info
                               {
                                   E1Link = context.E1Links.Where(t2 => t2.Request.InvestigatePossibilities.Take(1).SingleOrDefault().BuchtID == t.Bucht.ID && (t2.E1.Request.RequestTypeID == (int)DB.RequestType.E1 || t2.E1.Request.RequestTypeID == (int)DB.RequestType.E1Link)).OrderByDescending(t2 => t2.MDFDate).FirstOrDefault(),
                               }
                               ).ToList();
            }
        }



        public static List<VacateE1> GetVacateE1ByReqeustID(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.VacateE1s.Where(t => t.RequestID == requestID).ToList();
            }
        }
    }
}
