using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public  class ChangeLocationDB
    {
        public static ChangeLocation GetChangeLocationByRequestID(long _RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeLocations.Where(t => t.ID == _RequestID).SingleOrDefault();
            }
        }

        public static List<ChangeLocation> GetChangeLocationByRequestIDs(List<long> _RequestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeLocations.Where(t => _RequestIDs.Contains(t.ID)).OrderBy(t=> t.OldTelephone).ToList();
            }
        }
        public static List<ChangeLocation> GetChangeLocation()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangeLocations.ToList();
            }
        }

        public static bool ChechForAutoForwardInChangeLocation(ref Request _request, ChangeLocation changeLocation)
        {
            if (changeLocation.TargetCenter != null && changeLocation.ConfirmTheSourceCenter == null)
            {
                _request.CenterID = (int)changeLocation.SourceCenter;
                _request.Detach();
                DB.Save(_request);
                return true;
            }
            else
            {
                return false;
            }

            
        }
    }
}
