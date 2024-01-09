using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TelecomminucationServiceDB
    {
        public static TelecomminucationService GetTelecomminucationServiceByID(long telecomminucationServiceId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                TelecomminucationService result = new TelecomminucationService();
                result = context.TelecomminucationServices
                                .Where(ts => ts.ID == telecomminucationServiceId)
                                .SingleOrDefault();
                return result;
            }
        }

        public static List<TelecomminucationServiceInfo> SearchTelecomminucationServices(string title, long unitPrice, int tax, List<int> requestTypes, bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TelecomminucationServiceInfo> result = new List<TelecomminucationServiceInfo>();
                var query = context.TelecomminucationServices
                                   .Where(ts =>
                                             (string.IsNullOrEmpty(title) || ts.Title.Contains(title)) &&
                                             (unitPrice == -1 || unitPrice == ts.UnitPrice) &&
                                             (tax == -1 || tax == ts.Tax) &&
                                             (requestTypes.Count == 0 || requestTypes.Contains(ts.RequestTypeID)) &&
                                             (!isActive.HasValue || isActive == ts.IsActive)
                                         )
                                   .Select(ts => new TelecomminucationServiceInfo
                                                 {
                                                     ID = ts.ID,
                                                     Title = ts.Title,
                                                     UnitPrice = ts.UnitPrice,
                                                     Tax = ts.Tax,
                                                     RequestTypeTitle = ts.RequestType.Title,
                                                     UnitMeasureName = ts.UnitMeasure.Name,
                                                     IsActiveStatus = (ts.IsActive) ? "بله" : "خیر"
                                                 }
                                           )
                                   .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static List<TelecomminucationServiceInfo> GetTelecomminucationServiceInfosByRequestTypeID(long requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TelecomminucationServiceInfo> result = new List<TelecomminucationServiceInfo>();
                result = context.TelecomminucationServices
                                .Where(t =>
                                            (t.RequestTypeID == requestTypeID) &&
                                            (t.IsActive)
                                      )
                                .Select(t => new TelecomminucationServiceInfo
                                            {
                                                ID = t.ID,
                                                Title = t.Title,
                                                UnitPrice = t.UnitPrice,
                                                Tax = t.Tax,
                                                UnitMeasureName = t.UnitMeasure.Name
                                            }
                                       )
                                .ToList();
                return result;
            }
        }

    }
}
