using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class RequestStepDB
    {
        public static List<CheckableItem> GetRequestStepCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps
                          .OrderBy(t => t.RequestType.Title)
                          .Select(t => new CheckableItem()
                          {
                              ID = t.ID,
                              IsChecked = false,
                              Name = t.RequestType.Title + " : " + t.StepTitle
                          })
                          .ToList();
            }
        }

        public static List<CheckableItem> GetRequestTypeCheckableByRequestTypeID(List<int> requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps.Where(t => requestTypeID.Contains(t.RequestTypeID)).Select(t => new CheckableItem { ID = t.ID, Name = t.StepTitle, IsChecked = false }).ToList();
            }
        }

        public static List<RequestStep> GetRequestStepByRequestTypeID(int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps.Where(t => t.RequestTypeID == requestTypeID).ToList();
            }
        }
        /// <summary>
        /// A compiled query that returns a list of Orders.  
        /// Since it is static, it will be re-used over and over again, but since the query is parameterized, it is very versatile.
        /// </summary>
        private static Func<MainDataContext, int, IQueryable<RequestStep>> GetGetRequestStepByIDQuery =
            CompiledQuery.Compile((MainDataContext context, int id) =>
                                  (
                                     context.RequestSteps.Where(t => t.ID == id)
                                   ));


        public static RequestStep GetRequestStepByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                RequestStep requestStep = GetGetRequestStepByIDQuery(context, id).SingleOrDefault();
                return requestStep;
            }
        }
    }
}
