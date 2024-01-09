using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class VisitPlacesCabinetAndPostDB
    {

        public static VisitPlacesCabinetAndPost GetVisitPlacesCabinetAndPostByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VisitPlacesCabinetAndPosts.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
