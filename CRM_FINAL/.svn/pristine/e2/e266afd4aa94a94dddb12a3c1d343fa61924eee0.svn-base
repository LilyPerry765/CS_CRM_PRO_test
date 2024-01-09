using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class WorkFlowDB
    {
        public static List<WorkFlowRule> GetWorkFlowRules(long requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules.Where(t => t.RequestTypeID == requestTypeID).ToList();
            }
        }

        public static List<CheckableItem> GetVersionsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowVersions
                    .Select(t => new CheckableItem { Name = t.Name, ID = t.ID })
                    .ToList();
            }
        }

        public static void InsertWorkFlowRule(WorkFlowRule workFlowRule)
        {
            IEnumerable<WorkFlowRule> WorkFlowRules;
            using (MainDataContext context = new MainDataContext())
            {
                WorkFlowRules = context.WorkFlowRules.OrderBy(w => w.Priority);
                workFlowRule.Priority = workFlowRule.Priority < 1 ? 1 : workFlowRule.Priority;
                workFlowRule.Priority = (workFlowRule.Priority > WorkFlowRules.Last().Priority && (workFlowRule.Priority - WorkFlowRules.Last().Priority) > 1)
                    ? WorkFlowRules.Last().Priority + 1 : workFlowRule.Priority;
                WorkFlowRules = WorkFlowRules.Where(w => w.Priority >= workFlowRule.Priority);

                foreach (WorkFlowRule item in WorkFlowRules)
                    item.Priority++;

                context.WorkFlowRules.InsertOnSubmit(workFlowRule);
                context.SubmitChanges();
            }
        }

        public static void UpdateWorkFlowRulePriority(WorkFlowRule workFlowRule)
        {
            IEnumerable<WorkFlowRule> WorkFlowRules;
            WorkFlowRule Target;
            using (MainDataContext context = new MainDataContext())
            {
                WorkFlowRules = context.WorkFlowRules.OrderBy(w => w.Priority);
                workFlowRule.Priority = workFlowRule.Priority < 1 ? 1 : workFlowRule.Priority;
                workFlowRule.Priority = workFlowRule.Priority > WorkFlowRules.Last().Priority ? WorkFlowRules.Last().Priority : workFlowRule.Priority;

                Target = context.WorkFlowRules.Where(w => w.ID == workFlowRule.ID).SingleOrDefault();

                if (workFlowRule.Priority > Target.Priority)
                {
                    WorkFlowRules = WorkFlowRules.Where(w => w.Priority <= workFlowRule.Priority && w.Priority > Target.Priority);
                    foreach (WorkFlowRule wf in WorkFlowRules)
                        wf.Priority--;
                }
                else
                {
                    WorkFlowRules = WorkFlowRules.Where(w => w.Priority >= workFlowRule.Priority && w.Priority < Target.Priority);
                    foreach (WorkFlowRule wf in WorkFlowRules)
                        wf.Priority++;
                }

                Target.Priority = workFlowRule.Priority;

                context.SubmitChanges();
            }
        }

        public static void DeleteWorkFlowRule(WorkFlowRule workFlowRule)
        {
            IEnumerable<WorkFlowRule> WorkFlowRules;
            WorkFlowRule Target;
            using (MainDataContext context = new MainDataContext())
            {
                Target = context.WorkFlowRules.Where(w => w.ID == workFlowRule.ID).SingleOrDefault();
                context.WorkFlowRules.DeleteOnSubmit(Target);
                WorkFlowRules = context.WorkFlowRules.OrderBy(w => w.Priority);

                WorkFlowRules = WorkFlowRules.Where(w => w.Priority > workFlowRule.Priority);

                foreach (WorkFlowRule item in WorkFlowRules)
                    item.Priority--;

                context.SubmitChanges();
            }
        }

        public static byte GetTypeOfState(int currentStatusID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context
                    .Status.Where(s => s.ID == currentStatusID)
                    .Select(s => s.StatusType)
                    .FirstOrDefault();
            }
        }

        public static int GetNextStatesID(DB.Action actionID, int currentStatusID, int? workUnitID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules.Where(t => t.ActionID == (int)actionID && t.CurrentStatusID == currentStatusID && (!workUnitID.HasValue || t.SenderID == workUnitID) && t.SpecialConditionsID == null)
                   .OrderBy(t => t.SenderID).SingleOrDefault().NextStatusID;
            }
        }



        public static List<int> GetListNextStatesID(DB.Action actionID, int currentStatusID, int? workUnitID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules.Where(t => t.ActionID == (int)actionID && t.CurrentStatusID == currentStatusID && (!workUnitID.HasValue || t.SenderID == workUnitID) && t.SpecialConditionsID == null)
                   .OrderBy(t => t.SenderID)
                   .Select(t => t.NextStatusID).ToList();
            }
        }

        public static List<nextStates> GetNextStates(DB.Action actionID, int currentStatusID, int? workUnitID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules.Where(t => t.ActionID == (int)actionID && t.CurrentStatusID == currentStatusID && (!workUnitID.HasValue || t.SenderID == workUnitID))
                   .OrderBy(t => t.SenderID)
                   .Select(t => new nextStates { nextState = t.NextStatusID, SpecialCondition = t.SpecialConditionsID })
                   .ToList();
            }
        }

        public static void SetNextState(DB.Action actionID, int currentStatusID, long requestID, long? subFlowID = null, string description = null, int? requestRejectReason = null)
        {
            DateTime ServerDate = DB.GetServerDate();
            using (MainDataContext context = new MainDataContext())
            {
                StatusLog statusLog = new StatusLog();

                List<nextStates> nextStates = GetNextStates(actionID, currentStatusID);

                if (nextStates.Count == 0) throw new Exception("وضعیت بعدی در چرخه کاری وجود ندارد.");

                if (!nextStates.All(t => t.SpecialCondition == null))
                {

                    List<CheckableItem> list = typeof(DB.SpecialConditions).GetFields()
                                                                           .Where(t => t.IsLiteral && nextStates.Where(t2 => t2.SpecialCondition != null).Select(t2 => t2.SpecialCondition).Contains(Convert.ToInt32(t.GetRawConstantValue())))
                                                                           .Select(t => new CheckableItem
                                                                                   {
                                                                                       ID = Convert.ToInt32(t.GetRawConstantValue()),
                                                                                       Name = t.Name,
                                                                                   })
                                                                           .ToList();


                    SpecialCondition specialCondition = context.SpecialConditions.Where(t => t.RequestID == requestID).SingleOrDefault();
                    if (specialCondition != null)
                    {
                        List<System.Data.Linq.Mapping.MetaDataMember> columns = context.Mapping.GetTable(specialCondition.GetType()).RowType.DataMembers.Where(t => list.Select(t2 => t2.Name).Contains(t.MappedName)).ToList();
                        List<SpecialConditionsNextStates> specialConditionsNextStates = columns.Select(t => new SpecialConditionsNextStates { Value = (bool)t.MemberAccessor.GetBoxedValue(specialCondition), Name = t.MappedName }).ToList();

                        if (specialConditionsNextStates.All(t => t.Value == false))
                        {
                            nextStates = nextStates.Where(t => t.SpecialCondition == null).ToList();
                        }
                        else if (specialConditionsNextStates.Count(t => t.Value == true) == 1)
                        {
                            DB.SpecialConditions specialConditionsItem = (DB.SpecialConditions)System.Enum.Parse(typeof(DB.SpecialConditions), specialConditionsNextStates.Where(t => t.Value == true).SingleOrDefault().Name);
                            nextStates = nextStates.Where(t => t.SpecialCondition == (int)specialConditionsItem).ToList();

                            if (nextStates.Count > 1)
                            {
                                throw new Exception("برای شرایط خاص انتخاب شده چند وضعیت بعدی انتخاب شده است، لطفا چرخه کاری را اصلاح کنید.");
                            }


                        }
                        else if (specialConditionsNextStates.Count(t => t.Value == true) > 1)
                        {

                            DB.SpecialConditions? higherPriority = Data.SpecialConditionPrioritizeDB.GetHighestPriority(nextStates.Where(t => t.SpecialCondition != null).Select(t => (int)t.SpecialCondition).ToList());
                            if (higherPriority != null)
                            {
                                nextStates = nextStates.Where(t => t.SpecialCondition == (int)higherPriority).ToList();

                                if (nextStates.Count > 1)
                                {
                                    throw new Exception("برای شرایط خاص انتخاب شده چند وضعیت بعدی انتخاب شده است، لطفا چرخه کاری را اصلاح کنید.");
                                }
                            }
                            else
                            {
                                throw new Exception("چند شرایط خاص برای این وضعیت رخ داده است، لطفا چرخه کاری را اصلاح کنید یا برای مشخص کردن شرایط خاص با اولویت بالاتر با مدیر سیستم تماس بگیرید");
                            }
                        }
                    }
                    else
                    {
                        nextStates = nextStates.Where(t => t.SpecialCondition == null).ToList();
                    }


                }
                if (nextStates.Count() == 1)
                {
                    if (!subFlowID.HasValue)
                    {
                        if (context.SubFlowStatus.Where(t => t.ParentID == requestID).Count() == 0)
                        {
                            context.Requests.Where(t => t.ID == requestID).Single().StatusID = nextStates[0].nextState;
                            context.Requests.Where(t => t.ID == requestID).Single().PreviousAction = (byte)actionID;
                            context.Requests.Where(t => t.ID == requestID).Single().ModifyDate = ServerDate;
                            context.Requests.Where(t => t.ID == requestID).Single().ModifyUserID = DB.CurrentUser.ID;
                            context.Requests.Where(t => t.ID == requestID).Single().IsViewed = false;
                        }
                        else
                        {
                            context.Requests.Where(t => t.ID == requestID).Single().StatusID = nextStates[0].nextState;
                            context.Requests.Where(t => t.ID == requestID).Single().PreviousAction = (byte)actionID;
                            context.Requests.Where(t => t.ID == requestID).Single().ModifyDate = ServerDate;
                            context.Requests.Where(t => t.ID == requestID).Single().ModifyUserID = DB.CurrentUser.ID;
                            context.Requests.Where(t => t.ID == requestID).Single().IsViewed = false;
                        }

                        // change status log
                        statusLog.ID = 0;
                        statusLog.ReqeustID = requestID;
                        statusLog.ActionID = (byte)actionID;
                        statusLog.FromStatusID = currentStatusID;
                        statusLog.ToStatusID = nextStates[0].nextState;
                        statusLog.UserID = DB.CurrentUser.ID;
                        statusLog.RequestRejectReasonID = requestRejectReason;
                        statusLog.Description = description == string.Empty ? null : description;
                        statusLog.Date = ServerDate;
                        context.StatusLogs.InsertOnSubmit(statusLog);
                        // 

                        if ((actionID != DB.Action.Reject) && (context.Status.Where(t2 => t2.ID == nextStates[0].nextState && t2.StatusType == (int)DB.RequestStatusType.End).Any()))
                        {
                            DB.SaveRequestLog(requestID, ServerDate);
                            context.Requests.Where(t => t.ID == requestID).Single().EndDate = ServerDate;
                            context.Requests.Where(t => t.ID == requestID).Single().IsViewed = true;
                        }


                    }
                }
                else
                {

                    throw new Exception("چند وضعیت در چرخه کاری برای وضعیت فعلی تعیین شده است ");
                    //foreach (int stateID in nextStates.Select(t => t.nextState))
                    //{
                    //    SubFlowStatus subFlowStatus = new SubFlowStatus();
                    //    subFlowStatus.RequestID = requestID;
                    //    subFlowStatus.StatusID = stateID;
                    //    context.SubFlowStatus.InsertOnSubmit(subFlowStatus);
                    //}
                }



                context.SubmitChanges();
            }
        }

        public static int? GetProperForm(long requestID, int statusID, int? workUnitID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // TODO: Performance Improvement
                int requestTypeID = context.Requests.Where(t => t.ID == requestID).Select(t => t.RequestTypeID).SingleOrDefault();
                int currentStepID = context.Requests.Where(t => t.ID == requestID).Select(t => t.Status.RequestStepID).SingleOrDefault();

                // TODO: Logic Improvement
                int? formID = context.WorkFlowRules.Where(t => t.CurrentStatusID == statusID).Select(t => t.FormID).FirstOrDefault();

                if (formID.HasValue) return formID;

                return context
                    .WorkFlowRules.Where(t => t.RequestTypeID == requestTypeID && t.Status.RequestStepID == currentStepID && (!workUnitID.HasValue || t.SenderID == workUnitID))
                    .OrderBy(t => t.SenderID)
                    .Select(t => t.FormID)
                    .Take(1)
                    .SingleOrDefault();
            }
        }

        public static List<RequestStep> GetRequestSteps(int requestType = -1)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.WorkFlowRules
                    .Where(t => (requestType == -1 || t.RequestTypeID == requestType) && !t.ParentRowID.HasValue)
                    .Select(t => t.Status.RequestStep)
                    .Distinct()
                    .ToList();
                //List<RequestStep> x = context.WorkFlowRules
                //    .Where(t => (requestType == -1 || t.RequestTypeID == requestType) && !t.ParentRowID.HasValue)
                //    .OrderBy(t => t.CurrentStatusID)
                //    .Select(t => t.Status.RequestStep)
                //    .ToList();
                //x = x.Distinct().ToList();
                //return x;
            }
        }

        //milad doran
        //public static List<RequestStep> GetRecursiveRequestSteps(int requestType)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        // Please add RecursiveRequestStepsProcedure to Main from Stored Procedures
        //        return context.RecursiveRequestStepsProcedure((byte)DB.Action.Confirm, (int)requestType, (byte)DB.RequestStatusType.Start)
        //            .Select(t => new RequestStep
        //                     {
        //                         ID = t.ID,
        //                         IsVisible = t.IsVisible,
        //                         RequestTypeID = t.RequestTypeID,
        //                         StepTitle = t.StepTitle,
        //                     }).ToList();
        //    }
        //}

        //TODO:rad 13950225
        public static List<RequestStep> GetRecursiveRequestSteps(int requestType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                // Please add RecursiveRequestStepsProcedure to Main from Stored Procedures
                List<RequestStep> result = new List<RequestStep>();

                var query = context.RecursiveRequestStepsProcedure((byte)DB.Action.Confirm, (int)requestType, (byte)DB.RequestStatusType.Start)
                                   .Select(t => new RequestStep
                                                {
                                                    ID = t.ID,
                                                    IsVisible = t.IsVisible,
                                                    RequestTypeID = t.RequestTypeID,
                                                    StepTitle = t.StepTitle,
                                                }
                                          )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        //public static RequestStep GetRecursiveRequestSteps(int statusID, List<RequestStep> statusList)
        //{

        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        WorkFlowRule workFlowRule = context.WorkFlowRules.Where(t => t.CurrentStatusID == statusID).SingleOrDefault();

        //        if (workFlowRule.Status1.StatusType != (byte)DB.RequestStatusType.End)
        //        {
        //            statusList.Add(GetRecursiveRequestSteps(workFlowRule.NextStatusID, statusList));

        //        }
        //        return workFlowRule.Status.RequestStep;
        //    }
        //}



        public static List<WorkFlowNode> GetAllWorkFlowNode(int requestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules
                       .Where(t => t.RequestTypeID == requestTypeID)
                       .Select(t => new WorkFlowNode
                       {
                           ID = t.ID,
                           ParentID = t.ParentRowID,
                           Name = t.Status.RequestStep.StepTitle,
                           CurrentStatusTypeID = t.Status.StatusType,
                           NextStatusTypeID = t.Status1.StatusType,
                           CurrentStatusID = t.CurrentStatusID,
                           NextStatusID = t.NextStatusID,
                           Level = -1,
                           IsIncludedInTree = false
                       })
                       .ToList();
            }
        }

        private static void TraceNodes(List<WorkFlowNode> nodes, long nodeID)
        {
            WorkFlowNode node = nodes.Where(t => t.ID == nodeID).Single();
            List<WorkFlowNode> nextNodes = nodes
                .Where(t => t.CurrentStatusID == node.NextStatusID)
                .OrderByDescending(t => t.NextStatusTypeID)
                .ToList();

            if (node.CurrentStatusTypeID == (byte)DB.RequestStatusType.Completed || node.NextStatusTypeID == (byte)DB.RequestStatusType.End || node.CurrentStatusTypeID == (byte)DB.RequestStatusType.Start)
                node.IsIncludedInTree = true;

            if (nextNodes.Count == 0 || node.NextStatusTypeID == (byte)DB.RequestStatusType.End) return;


            foreach (WorkFlowNode item in nextNodes)
                TraceNodes(nodes, item.ID);
        }

        private static void UpdateLevel(List<WorkFlowNode> nodes, long nodeID, int level)
        {
            WorkFlowNode node = nodes.Where(t => t.ID == nodeID).Single();

            List<WorkFlowNode> children = nodes.Where(t => t.ParentID == node.ID).OrderByDescending(t => t.NextStatusTypeID).ToList();

            node.Level = level + 1;

            if (children.Count > 0)
                foreach (WorkFlowNode child in children)
                    UpdateLevel(nodes, child.ID, level + 1);
        }


        //milad doran
        //public static List<CheckableItem> GetRequestStepsCheckable(int requestType = -1)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.RequestSteps
        //            .Where(t => (requestType == -1 || t.RequestTypeID == requestType))
        //            .Select(t => new CheckableItem() { ID = t.ID, Name = t.StepTitle, IsChecked = false })
        //            .ToList();
        //    }
        //}

        //TODO:rad 13950301
        public static List<CheckableItem> GetRequestStepsCheckable(int requestType = -1)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.RequestSteps
                                   .Where(t => (requestType == -1 || t.RequestTypeID == requestType))
                                   .Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = t.StepTitle,
                                                    IsChecked = false
                                                }
                                          )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetRequestStepsCheckable(List<int> requestTypes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps
                    .Where(t => requestTypes.Contains(t.RequestTypeID))
                    .Select(t => new CheckableItem() { ID = t.ID, Name = t.StepTitle, IsChecked = false })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetRequestStepsCheckableByIDs(List<int> stepIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps
                    .Where(t => stepIDs.Contains(t.ID))
                    .Select(t => new CheckableItem() { ID = t.ID, Name = t.StepTitle })
                    .ToList();
            }
        }



        public static int GetCurrentStep(int currentStatusID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.Where(s => s.ID == currentStatusID).Select(s => s.RequestStepID).SingleOrDefault();

            }
        }
        public static List<int?> GetActionsCurrentStatus(int currentStatusID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.WorkFlowRules.Where(t => t.CurrentStatusID == currentStatusID).Select(t => t.ActionID).ToList();
            }
        }




    }


    public class WorkFlowNode
    {
        public long ID { get; set; }
        public long? ParentID { get; set; }
        public byte CurrentStatusTypeID { get; set; }
        public byte NextStatusTypeID { get; set; }
        public int CurrentStatusID { get; set; }
        public int NextStatusID { get; set; }
        public string Name { get; set; }
        public List<WorkFlowNode> ChildNodes { get; set; }
        public int Level { get; set; }
        public bool IsIncludedInTree { get; set; }
    }
}
