using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class LinesmanDB
    {
        public static List<CRM.Data.AssignmentDB.NearestTelephonInfo> GetPostByChangeLocationByID(long _RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Linesmans
                       .Where(t => t.ChangeLocationID == _RequestID)
                       .Select(t => new CRM.Data.AssignmentDB.NearestTelephonInfo 
                                    {LinemanID = t.ID, CabinetID = t.CabinetID, PostID = t.PostID , CabinetNumber= t.Cabinet.CabinetNumber.ToString() , PostNumber= t.Post.Number.ToString()  }).ToList();
            }
        }
    }
}
