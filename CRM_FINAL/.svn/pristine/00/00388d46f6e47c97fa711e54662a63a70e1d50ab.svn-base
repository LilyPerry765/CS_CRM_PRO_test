using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class StatusDB
    {
        public static List<StatusInfo> SearchStatus(List<int> requestType, List<int> statusType, string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status
                    .Where(t =>
                            (requestType.Count == 0 || requestType.Contains(t.RequestStepID)) &&
                            (statusType.Count == 0 || statusType.Contains(t.StatusType)) && 
                            (string.IsNullOrWhiteSpace(title) || t.Title == title)
						  ).Select(t=> new StatusInfo{
                              ID = t.ID ,
                              RequestStepID = t.RequestStepID ,
                              Title = t.Title ,
                              ParentID = t.ParentID ,
                              InsertDate = t.InsertDate ,
                              StatusType = t.StatusType ,
                              RequestTypeTitle = t.RequestStep.RequestType.Title})


                    .ToList();
            }
        }

        public static Status GetStatusByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetStatusCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status
                    .Select(t => new CheckableItem
                                {
                                    ID = t.ID,
                                    Name = t.Title,
                                    IsChecked = false
                                }
                            )
                    .ToList();
            }
        }

        public static List<Status> GetStatus()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.ToList();
            }
        }

        public static List<CheckableItem> GetStatesNameValue(int requestTypeID = -1)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status
                        .Where(t => requestTypeID == -1 || t.RequestStep.RequestTypeID == requestTypeID)
                        .Select(t => new CheckableItem { Name = t.RequestStep.StepTitle + " > " + t.Title, ID = t.ID })
                        .OrderBy(t=>t.Name)
                        .ToList();
            }
        }

        public class StatusInfo
        {
            public int ID { get; set; }
            public int RequestStepID {get;set;}
            public string Title { get; set; }
            public int? ParentID { get; set; }
            public DateTime InsertDate { get; set; }
            public byte StatusType { get; set; }
            public string RequestTypeTitle { get; set; }
        }



        public static List<Status> GetStatusByRequestStepID(List<CheckableItem> requestStepID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.Status.Where(t => requestStepID.Select(rs=>rs.ID).Contains(t.RequestStepID)).ToList();
                return x;
            }
            
        }

        public static Status GetStatueByStatusID(int statusID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.Where(t => t.ID == statusID).SingleOrDefault();
            }
        }
        public static List<Status> GetStatuesByStatusID(int statusID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var status = context.Status.Where(t => t.ID == statusID).SingleOrDefault();
                if (status != null)
                {
                    return context.Status.Where(t => t.RequestStepID == status.RequestStepID).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool IsFinalStep(int currentStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {

            return   context.Status.Where(t =>t.RequestStepID == context.RequestSteps.Where(rs => rs.ID == context.Status.Where(s => s.ID == currentStatus).SingleOrDefault().RequestStepID).SingleOrDefault().ID)
                                   .Any(t => t.StatusType == (byte)DB.RequestStatusType.End);
            }
        }

        public static bool IsStartStep(int currentStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.Status.Where(t => t.RequestStepID == context.RequestSteps.Where(rs => rs.ID == context.Status.Where(s => s.ID == currentStatus).SingleOrDefault().RequestStepID).SingleOrDefault().ID)
                                       .Any(t => t.StatusType == (byte)DB.RequestStatusType.Start);
            }
        }

        public static Status GetStatusbyStatusType(int requestType, int statusType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.Where(t => context.RequestSteps.Where(rs => requestType == rs.RequestTypeID).Select(rs => rs.ID).Contains(t.RequestStepID)).Where(t => t.StatusType == statusType).SingleOrDefault(); 
            }
        }

        public static Status GetStatusInCurrentStepByStatusType(int currentStatus, DB.RequestStatusType requestStatusType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.Where(t => t.RequestStepID == context.RequestSteps.Where(rs => rs.ID == context.Status.Where(s => s.ID == currentStatus).SingleOrDefault().RequestStepID).SingleOrDefault().ID)
                                           .Where(t => t.StatusType == (byte)requestStatusType).Take(1).SingleOrDefault();
            }
        }

        public static Status GetFirstStatusByRequstTypeID(int requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.Where(t => t.RequestStep.RequestTypeID == requestType && t.StatusType == (byte)DB.RequestStatusType.Start).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetStatusCheckableByType(DB.RequestStatusType requestStatusType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status
                    .Where(t=>t.StatusType == (int)requestStatusType)
                    .Select(t => new CheckableItem
                               {
                                 ID = t.ID,
                                 Name = t.RequestStep.RequestType.Title + "->" + t.Title,
                                 IsChecked = false
                               }
                            ).OrderBy(t=>t.Name)
                    .ToList();
            }
        }
    }
}