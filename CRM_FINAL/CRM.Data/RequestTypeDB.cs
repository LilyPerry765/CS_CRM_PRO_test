using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class RequestTypeDB
    {

        public static List<CheckableItem> GetRequestTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes
                              .Select(t => new CheckableItem { ID = t.ID, Name = t.Title, IsChecked = false })
                              .OrderBy(ci => ci.Name)
                              .ToList();
            }
        }

        public static List<CheckableItemNullable> GetRequestTypeCheckableNullable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.OrderBy(t => t.Title)
                    .Select(t => new CheckableItemNullable { ID = t.ID, Name = t.Title, IsChecked = false }).ToList();
            }
        }

        public static RequestType GetRequestTypeByRequestType(int requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.Where(t => t.ID == requestType).SingleOrDefault();
            }
        }

        public static List<RequestType> GetAllEntity()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.OrderBy(t => t.Title).ToList();
            }
        }

        public static RequestType getRequestTypeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestTypes.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static RequestType GetRequestTypeByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == requestID).Select(t => t.RequestType).SingleOrDefault();
            }
        }
    }
}
