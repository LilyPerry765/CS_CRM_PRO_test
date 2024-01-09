using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class RoleWebServiceDB
    {
        public static List<CheckableItem> GetWebServiceCheckable(int roleId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var role = context.Roles.Where(rol =>
                                                    (rol.ID == roleId) &&
                                                    (rol.IsServiceRole) //چنانچه نقش فقط برای سرویس ایجاد شده باشد مورد قبول است
                                               )
                                  .SingleOrDefault();

                if (role == null)
                {
                    result = null;
                    return result;
                }

                //var query = context.WebServices
                //                   .GroupJoin(context.RoleWebServices, ws => ws.ID, ross => ross.ID, (ws, ross) => new { _RoleWebServices = ross, _WebService = ws })
                //                   .SelectMany(a => a._RoleWebServices.Where(inner => inner.RoleID == role.ID).DefaultIfEmpty(), (a, ros) => new { _RoleWebService = ros, _WebService = a._WebService })

                //                   .Select(final => new CheckableItem
                //                                  {
                //                                      ID = final._WebService.ID,
                //                                      Name = final._WebService.Name,
                //                                      IsChecked = (final._RoleWebService.WebServiceID == null) ? false : true,
                //                                      Description = final._WebService.Description
                //                                  }
                //                          )
                //                   .AsQueryable();
                //result = query.ToList();
                //return result;
                var query = (from c in context.WebServices
                             join rc in context.RoleWebServices
                             on c.ID equals rc.WebServiceID into rows
                             from t in rows.Where(f => f.RoleID == roleId).DefaultIfEmpty()
                             orderby c.Name
                             select new CheckableItem
                             {
                                 ID = c.ID,
                                 IsChecked = t.WebServiceID == null ? false : true,
                                 Name = c.Name,
                                 Description = c.Description
                             });

                result = query.ToList();
                return result;
            }
        }

    }
}
