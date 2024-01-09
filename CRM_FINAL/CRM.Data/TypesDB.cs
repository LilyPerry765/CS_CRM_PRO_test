using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class TypesDB
    {
        public static List<RequestType> GetRequestTypes()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.OrderBy(rt => rt.Title).ToList();
            }
        }

        public static List<CheckableItem> GetRequestTypesCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes
                    .Select(t => new CheckableItem
                                {
                                    ID = t.ID,
                                    Name = t.Title,
                                    IsChecked = false
                                }
                            )
                    .OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static List<CheckableItem> GetHaveForeignSupportStepRequestTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.Where(t =>
                    t.ID == (byte)DB.RequestType.ADSL || t.ID == (byte)DB.RequestType.ADSLInstall).Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false

                    }).ToList();
            }
        }

        public static List<CheckableItem> GetRequestTypeCheckableByID(List<int> requestTypeIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.Where(t => requestTypeIDs.Contains(t.ID))
                              .Select(t => new CheckableItem
                              {
                                  ID = t.ID,
                                  Name = t.Title
                              }
                                     )
                              .ToList();
            }
        }
    }
}