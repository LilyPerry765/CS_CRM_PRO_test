using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CRM.Data
{
    public static class CutAndEstablishDB
    {
        public static CutAndEstablish GetCutAndEstablishByRequestID(long _RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CutAndEstablishes.Where(t => t.ID == _RequestID).SingleOrDefault();
            }
        }

        public static List<CutAndEstablish> GetCutAndEstablishByTelephoneNo(long TelephoneNo, byte TelephoneStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CutAndEstablishes
                              .Where(t =>
                                         (t.Request.TelephoneNo == TelephoneNo) &&
                                         (t.Status == TelephoneStatus)
                                    )
                              .OrderByDescending(t => t.ID)
                              .ToList();
            }
        }

        public static string GetCutReason(long telephoneNo)
        {
            string result = "";

            using (MainDataContext context = new MainDataContext())
            {
                CutAndEstablish cut = context.CutAndEstablishes.Where(t => t.Request.TelephoneNo == telephoneNo).OrderByDescending(t => t.Request.InsertDate).FirstOrDefault();

                if (cut != null)
                    result = context.CauseOfCuts.Where(t => t.ID == cut.CutType).SingleOrDefault().Name;

                return result;
            }
        }
    }
}
