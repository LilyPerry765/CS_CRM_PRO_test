using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class AdameEmkanatDB
    {
        public static List<AdameEmkanat> SearchAdameEmkanat(List<int> center, string fISH_NO, string kAFO, string pOST, string tYPE_POST, string eLAT, string tA_SABT, string tA_BARE, string nAME, string aDR, string tOZEH, int startRowIndex,int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {


                var x = context.AdameEmkanats
                       .Where(t =>
   (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains((int)t.CenterID) : center.Contains((int)t.CenterID)) &&
   (string.IsNullOrWhiteSpace(fISH_NO) || t.FISH_NO.Contains(fISH_NO)) &&
   (string.IsNullOrWhiteSpace(kAFO) || t.KAFO.Contains(kAFO)) &&
   (string.IsNullOrWhiteSpace(pOST) || t.POST.Contains(pOST)) &&
   (string.IsNullOrWhiteSpace(tYPE_POST) || t.TYPE_POST.Contains(tYPE_POST)) &&
   (string.IsNullOrWhiteSpace(eLAT) || t.ELAT.Contains(eLAT)) &&
   (string.IsNullOrWhiteSpace(tA_SABT) || t.TA_SABT.Contains(tA_SABT)) &&
   (string.IsNullOrWhiteSpace(tA_BARE) || t.TA_BARE.Contains(tA_BARE)) &&
   (string.IsNullOrWhiteSpace(nAME) || t.NAME.Contains(nAME)) &&
   (string.IsNullOrWhiteSpace(aDR) || t.ADR.Contains(aDR)) &&
   (string.IsNullOrWhiteSpace(tOZEH) || t.TOZEH.Contains(tOZEH))
                             );
                count = x.Count();
                    return x.Skip(startRowIndex).Take(pageSize).ToList();

            }
        }

        public static AdameEmkanat GetAdameEmkanatByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.AdameEmkanats
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

   
    }
}