using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class RequestRejectReasonDB
    {
        public static List<RequestRejectReason> SearchRequesrRejectReason(string Description,
                                                                              List<int> RequestSteps,
                                                                            bool? IsActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestRejectReasons.Where
                   (t =>
                       (string.IsNullOrEmpty(Description) || t.Description.Contains(Description)) &&
                      (RequestSteps.Count == 0 || RequestSteps.Contains((int)t.RequestStepID)) &&
                       (!IsActive.HasValue || (IsActive == t.Status))
                       ).ToList();
            }
        }

        public static RequestRejectReason GetRequestRejectReasonByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                RequestRejectReason requestRejectReason = GetRequestRejectReasonByIdQuery(context, id).SingleOrDefault();
                return requestRejectReason;
            }
        }

        public static RequestRejectReason GetRequestRejectReasonByID(int requestStep, int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestRejectReasons.Where(t => t.RequestStepID == requestStep && t.ID == id).SingleOrDefault();
            }
        }

        private static Func<MainDataContext, int, IQueryable<RequestRejectReason>> GetRequestRejectReasonByIdQuery = CompiledQuery.Compile((MainDataContext context, int id) => (context.RequestRejectReasons.Where(t => t.ID == id)));

        public static List<CheckableItem> GetRequestReasonCheckable(int requestStep)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestRejectReasons.Where(t => t.RequestStepID == requestStep)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Description,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static List<CheckableItem> GetRequestReasonCheckableByRequestStatusID(int requestStatusID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return  context.RequestRejectReasons.Where(t => t.RequestStepID == context.RequestSteps.Where(rs => rs.ID == context.Status.Where(s => s.ID == requestStatusID).SingleOrDefault().RequestStepID).SingleOrDefault().ID).Select(t => new CheckableItem { ID = t.ID, Name = t.Description, IsChecked = false }).ToList();
            }
        }
    }
}
