﻿

namespace CaseManagement.Case.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Extensibility;
    using Serenity.Services;
    using System;
    using System.Data;
    using System.Collections.Generic;
    using CaseManagement.Administration;
    using System.Linq;
    using System.Reflection;
    using CaseManagement.Case.Entities;
    using MyRow = Entities.ActivityRequestTechnicalRow;
    using CaseManagement.Administration.Entities;

    public class ActivityRequestTechnicalRepository
    {
        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler().Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler().Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler().Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler().Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            
                return new MyListHandler().Process(connection, request);
           
            
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {


            //protected override void BeforeSave()
            //{
            //    base.BeforeSave();

            //    if (IsCreate)
            //    {

            //        int userID = int.Parse(Authorization.UserId);
            //        string userName = Connection.List<Administration.Entities.UserRow>().Where(t => t.UserId == userID).Select(t => t.DisplayName).SingleOrDefault();
            //        Row.ActivityCode = Connection.List<ActivityRow>().Where(t => t.Id == (int)Row.ActivityId).Select(t => t.Code).SingleOrDefault();


            //        int provinceID = Connection.List<Administration.Entities.UserRow>().Where(t => t.UserId == userID).Select(t => (int)t.ProvinceId).SingleOrDefault();
            //        Row.ProvinceId = provinceID;

            //        int provinceCode = Connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(p => (int)p.Code).SingleOrDefault();
            //        DateTime createdDate = Connection.List<Entities.ActivityRequestRow>().Max(t => (DateTime)t.CreatedDate);
            //        long id = Connection.List<ActivityRequestRow>().Where(t => t.CreatedDate == createdDate).Select(t => (long)t.Id).SingleOrDefault();
            //        string idCode = id.ToString().Substring(10, 4);
            //        string incomeFlowCode = Connection.List<IncomeFlowRow>().Where(t => t.Id == (int)Row.IncomeFlowId).Select(t => t.Code).SingleOrDefault();
            //        Row.Id = Convert.ToInt64("96" + provinceCode.ToString() + incomeFlowCode + Row.ActivityCode.Trim() + idCode) + 1;

            //    }

            //}

            protected override void SetInternalFields()
            {
                base.SetInternalFields();

                try
                {
                    if (Row.DelayedCost == 0 || Row.DelayedCost == null)
                        if (Row.AccessibleCost != null)
                            if (Row.AccessibleCost != 0)
                                throw new Exception("به دلیل وارد نکردن مبلغ معوق لطفا مبلغ قابل وصول را صفر نمایید");

                    if (Row.DelayedCost == 0 || Row.DelayedCost == null)
                        if (Row.InaccessibleCost != null)
                            if (Row.InaccessibleCost != 0)
                                throw new Exception("به دلیل وارد نکردن مبلغ معوق لطفا مبلغ غیر قابل وصول را صفر نمایید");

                    if (Row.DelayedCost != null && Row.DelayedCost != 0)
                        if (Row.AccessibleCost == null || Row.AccessibleCost == 0)
                            if (Row.DelayedCost != Row.InaccessibleCost)
                                throw new Exception("لطفا مبلغ قابل وصول را وارد نمایید");

                    if (Row.DelayedCost != null && Row.DelayedCost != 0)
                        if (Row.InaccessibleCost == null || Row.InaccessibleCost == 0)
                            if (Row.DelayedCost != Row.AccessibleCost)
                                throw new Exception("لطفا مبلغ غیرقابل وصول را وارد نمایید");

                    if (Row.DelayedCost != null && Row.DelayedCost != 0)
                        if (Row.AccessibleCost != null && Row.AccessibleCost != 0)
                            if (Row.InaccessibleCost != null && Row.InaccessibleCost != 0)
                            {
                                long sum = (long)Row.AccessibleCost + (long)Row.InaccessibleCost;
                                if (Row.DelayedCost != sum)
                                    throw new Exception("جمع مبلغ قابل وصول و غیرقابل وصول، با مقدار معوق برابر نمی باشد");
                            }


                 
                    if (Row.CycleCost != null && Row.Factor != null)
                        Row.YearCost = Row.CycleCost * Row.Factor;
                    else
                        Row.YearCost = null;

                    //if (Row.CommnetList != null)
                    //{
                    //    var activityComment = Entities.ActivityRequestCommentRow.Fields;
                    //    var oldList = IsCreate ? null : Connection.List<Entities.ActivityRequestCommentRow>(activityComment.ActivityRequestId == this.Row.Id.Value);

                    //    foreach (Entities.ActivityRequestCommentRow comment in Row.CommnetList)
                    //    {
                    //        if (comment.Id == null)
                    //        {
                    //            comment.CreatedDate = DateTime.Now;
                    //            comment.CreatedUserId = int.Parse(Authorization.UserId);
                    //        }
                    //    }

                    //    new Common.DetailListSaveHandler<Entities.ActivityRequestCommentRow>(oldList, Row.CommnetList,
                    //    x => x.ActivityRequestId = Row.Id.Value).Process(this.UnitOfWork);
                    //}

                    int userID = int.Parse(Authorization.UserId);
                    string userName = Connection.List<Administration.Entities.UserRow>().Where(t => t.UserId == userID).Select(t => t.DisplayName).SingleOrDefault();
                    Row.ActivityCode = Connection.List<ActivityRow>().Where(t => t.Id == (int)Row.ActivityId).Select(t => t.Code).SingleOrDefault();

                    if (IsCreate)
                    {
                        if (Int32.Parse(Helper.GetPersianDate(Row.DiscoverLeakDate, Helper.DateStringType.TwoDigitsYear).Substring(0, 2)) < Int32.Parse(Helper.GetPersianDate(DateTime.Now, Helper.DateStringType.TwoDigitsYear).Substring(0, 2)))
                        {
                            throw new Exception("تاریخ شناسایی نشتی باید در سال جاری باشد.");
                        }



                        int provinceID = Connection.List<Administration.Entities.UserRow>().Where(t => t.UserId == userID).Select(t => (int)t.ProvinceId).SingleOrDefault();
                        Row.ProvinceId = provinceID;


                        Row.RejectCount = 0;                        
                        Row.CreatedUserId = int.Parse(Authorization.UserId);
                        Row.CreatedDate = DateTime.Now;
                        Row.ModifiedUserId = int.Parse(Authorization.UserId);
                        Row.ModifiedDate = DateTime.Now;
                        Row.IsDeleted = false;
                        Row.StatusID = 1;

                        int provinceCode = Connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(p => (int)p.Code).SingleOrDefault();
                        DateTime createdDate = Connection.List<Entities.ActivityRequestRow>().Max(t => (DateTime)t.CreatedDate);
                        long id = Connection.List<ActivityRequestRow>().Where(t => t.CreatedDate == createdDate).Select(t => (long)t.Id).SingleOrDefault();
                        string idCode = id.ToString().Substring(10, 4);
                        string incomeFlowCode = Connection.List<IncomeFlowRow>().Where(t => t.Id == (int)Row.IncomeFlowId).Select(t => t.Code).SingleOrDefault();
                        Row.Id = Convert.ToInt64(Helper.GetPersianDate(DateTime.Now, Helper.DateStringType.TwoDigitsYear).Substring(0, 2) + provinceCode.ToString() + incomeFlowCode + Row.ActivityCode.Trim() + idCode) + 1;

                        Helper.SaveLog("Activity", "در دست اقدام فنی", 0, Row.ActivityId.ToString() + " - " + Row.ProvinceId.ToString(), "", Connection, Administration.ActionLog.Insert);
                    }
                    else
                    {

                        if (Int32.Parse(Helper.GetPersianDate(Row.DiscoverLeakDate, Helper.DateStringType.TwoDigitsYear).Substring(0, 2)) < Int32.Parse(Helper.GetPersianDate(Row.CreatedDate, Helper.DateStringType.TwoDigitsYear).Substring(0, 2)))
                        {
                            throw new Exception("تاریخ شناسایی نشتی باید در سال جاری باشد.");
                        }



                        if (Row.CommnetList != null)
                        {
                            var activityComment = Entities.ActivityRequestCommentRow.Fields;
                            var oldList = IsCreate ? null : Connection.List<Entities.ActivityRequestCommentRow>(activityComment.ActivityRequestId == this.Row.Id.Value);

                            foreach (Entities.ActivityRequestCommentRow comment in Row.CommnetList)
                            {
                                if (comment.Id == null)
                                {
                                    comment.CreatedDate = DateTime.Now;
                                    comment.CreatedUserId = int.Parse(Authorization.UserId);
                                }
                            }

                            new Common.DetailListSaveHandler<Entities.ActivityRequestCommentRow>(oldList, Row.CommnetList,
                            x => x.ActivityRequestId = Row.Id.Value).Process(this.UnitOfWork);
                        }

                        Row.ModifiedUserId = int.Parse(Authorization.UserId);
                        Row.ModifiedDate = DateTime.Now;

                        Helper.SaveLog("Activity", "در دست اقدام فنی", Row.Id.Value, Row.Id.ToString(), "", Connection, Administration.ActionLog.Update);
                        if (Row.StatusID != null)
                            ActivityRequestLogDB.SaveActivityRequestLog(Row.Id.Value, (int)Row.StatusID, Row.ActionID.Value, Connection);
                    }

                    if (Row.DiscoverLeakDate > Row.CreatedDate)
                        throw new Exception("تاریخ شناسایی نمی تواند بعد از تاریخ ایجاد باشد");

                    Row.ActivityCode = Connection.List<ActivityRow>().Where(t => t.Id == (int)Row.ActivityId).Select(t => t.Code).SingleOrDefault();

                    int? statusID = Row.StatusID;

                    if (Row.ActionID == null)
                        throw new Exception("لطفا عملیات مورد نظر را انتخاب نمایید");

                    List<int> roleIDs = Connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID).Select(t => (int)t.RoleId).ToList();

                    switch (Row.ActionID)
                    {
                        case RequestAction.Save:
                            if (Row.ConfirmTypeID == ConfirmType.Financial)
                                throw new Exception("لطفا گزارش اولیه فنی را انتخاب نمایید.");
                            break;

                        case RequestAction.Forward:

                            if (Row.ConfirmTypeID == ConfirmType.Financial)
                                throw new Exception("لطفا گزارش اولیه فنی را انتخاب نمایید.");

                            int nextStatusIDForward = Connection.List<WorkFlow.Entities.WorkFlowRuleRow>().Where(t => t.CurrentStatusId == Row.StatusID.Value && t.ActionId == 1)
                                                                .Select(t => (int)t.NextStatusId).SingleOrDefault();

                            Row.StatusID = nextStatusIDForward;
                            Row.SendDate = DateTime.Now;
                            Row.SendUserId = userID;

                            if (roleIDs != null && roleIDs.Count != 0)
                            {
                                if (roleIDs.Contains(10) || roleIDs.Contains(14))
                                {
                                    Row.CycleCostHistory = Row.CycleCost;
                                    Row.DelayedCostHistory = Row.DelayedCost;
                                    Row.AccessibleCostHistory = Row.AccessibleCost;
                                    Row.InaccessibleCostHistory = Row.InaccessibleCost;

                                    if (Row.ConfirmTypeID == ConfirmType.Technical)
                                        Row.ConfirmTypeID = ConfirmType.Financial;

                                    Row.IsRejected = false;
                                }
                            
                            }
                            break;

                        case RequestAction.Deny:
                            if (roleIDs != null && roleIDs.Count != 0)
                            {

                                if (roleIDs.Contains(10))
                                {
                                    if (Row.RejectCount == null || Row.RejectCount < 3)
                                    {
                                        Row.RejectCount = Row.RejectCount + 1;

                                        int nextStatusIDDeny = Connection.List<WorkFlow.Entities.WorkFlowRuleRow>().Where(t => t.CurrentStatusId == Row.StatusID.Value && t.ActionId == 2)
                                                             .Select(s => s.NextStatusId.Value).SingleOrDefault();

                                        Row.StatusID = nextStatusIDDeny;
                                        Row.IsRejected = true;
                                        Row.SendDate = DateTime.Now;
                                        Row.SendUserId = userID;
                                    }
                                    else
                                    {
                                        Row.IsDeleted = true;
                                        Row.DeletedUserId = int.Parse(Authorization.UserId);
                                        Row.DeletedDate = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    int nextStatusIDDeny = Connection.List<WorkFlow.Entities.WorkFlowRuleRow>().Where(t => t.CurrentStatusId == Row.StatusID.Value && t.ActionId == 2)
                                                                                                .Select(s => s.NextStatusId.Value).SingleOrDefault();
                                    if (nextStatusIDDeny != 0)
                                    {
                                        Row.StatusID = nextStatusIDDeny;
                                        Row.IsRejected = true;
                                        Row.SendDate = DateTime.Now;
                                        Row.SendUserId = userID;
                                    }
                                    else throw new Exception("عملیات بازگشت برای شما امکانپذیر نمی باشد.");

                                }
                            }
                            break;

                        case RequestAction.Delete:
                            Row.IsDeleted = true;
                            Row.DeletedUserId = int.Parse(Authorization.UserId);
                            Row.DeletedDate = DateTime.Now;

                            Helper.SaveLog("Activity", "در دست اقدام فنی", Row.Id.Value, Row.Id.ToString(), "", Connection, Administration.ActionLog.Delete);
                            break;

                        default:
                            break;
                    }

                    Row.ActionID = RequestAction.Save;

                    if (Row.ActivityId != null)
                    {

                        var qury = from a in Connection.List<ActivityRow>()
                                   join b in Connection.List<RepeatTermRow>()
                                   on a.RepeatTermId equals b.Id
                                   where a.Id == Row.ActivityId
                                   select new
                                   {
                                       b.RequiredYearRepeatCount
                                   };
                        //int RepetTermID = Connection.List<ActivityRow>().Join(Connection.List<RepeatTermRow>(),t=>t.RepeatTermId, () .Where(t => t.Id == Row.ActivityId).Select(t => (int)t.RepeatTermId).SingleOrDefault();
                        int? RequiredYearRepeatCount = qury.SingleOrDefault().RequiredYearRepeatCount;
                        Row.Factor = RequiredYearRepeatCount;

                    }
                        
                    Row.YearCost = Row.CycleCost * Row.Factor;
                    Row.TotalLeakage = Row.CycleCost + Row.DelayedCost;
                    Row.RecoverableLeakage = Row.CycleCost + Row.AccessibleCost;
                    Row.Recovered = Row.CycleCost + Row.AccessibleCost;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                if ((Row.CommnetList != null) && IsCreate)
                {
                    var activityComment = Entities.ActivityRequestCommentRow.Fields;
                    var oldList = IsCreate ? null : Connection.List<Entities.ActivityRequestCommentRow>(activityComment.ActivityRequestId == this.Row.Id.Value);

                    foreach (Entities.ActivityRequestCommentRow comment in Row.CommnetList)
                    {
                        if (comment.Id == null)
                        {
                            comment.CreatedDate = DateTime.Now;
                            comment.CreatedUserId = int.Parse(Authorization.UserId);
                        }
                    }
                    int userID = int.Parse(Authorization.UserId);
                    int provinceID = Connection.List<Administration.Entities.UserRow>().Where(t => t.UserId == userID).Select(t => (int)t.ProvinceId).SingleOrDefault();
                    int provinceCode = Connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(p => (int)p.Code).SingleOrDefault();
                    DateTime createdDate = Connection.List<Entities.ActivityRequestRow>().Max(t => (DateTime)t.CreatedDate);
                    long id = Connection.List<ActivityRequestRow>().Where(t => t.CreatedDate == createdDate).Select(t => (long)t.Id).SingleOrDefault();
                    string idCode = id.ToString().Substring(10, 4);
                    string incomeFlowCode = Connection.List<IncomeFlowRow>().Where(t => t.Id == (int)Row.IncomeFlowId).Select(t => t.Code).SingleOrDefault();

                    new Common.DetailListSaveHandler<Entities.ActivityRequestCommentRow>(oldList, Row.CommnetList,
                    x => x.ActivityRequestId = Convert.ToInt64(Helper.GetPersianDate(DateTime.Now, Helper.DateStringType.TwoDigitsYear).Substring(0, 2) + provinceCode.ToString() + incomeFlowCode + Row.ActivityCode.Trim() + idCode)).Process(this.UnitOfWork);
                }
 
            }
            

       
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow>
        {
            protected override void OnReturn()
            {
                base.OnReturn();
                var activityComment = Entities.ActivityRequestCommentRow.Fields;
                Row.CommnetList = Connection.List<Entities.ActivityRequestCommentRow>(activityComment.ActivityRequestId == Row.Id.Value);

                for (int CommentCount = 0; CommentCount < Row.CommnetList.Count; CommentCount++)
                {
                    Row.CommnetList[CommentCount].CreatedUserName = Connection.List<UserRow>().Where(t => t.UserId == (int)Row.CommnetList[CommentCount].CreatedUserId).Select(t => t.DisplayName).SingleOrDefault();                
                }

            }
        }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            protected override void ApplyFilters(SqlQuery query)
           {
                base.ApplyFilters(query);               

                int userID = int.Parse(Authorization.UserId);
                int? provinceID = Connection.List<UserRow>().Where(t => t.UserId == userID).Select(t => t.ProvinceId).SingleOrDefault();
                List<int> roleIDs = Connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID).Select(t => (int)t.RoleId).ToList();

                if (roleIDs != null && roleIDs.Count != 0)
                {
                    List<int> stepIDs = Connection.List<Administration.Entities.RoleStepRow>().Where(t => roleIDs.Contains((int)t.RoleId)).Select(s => (int)s.StepId).ToList();
                    List<int> statusIDs = Connection.List<WorkFlow.Entities.WorkFlowStatusRow>().Where(t => stepIDs.Contains((int)t.StepId)).Select(s => (int)s.Id).ToList();

                    if (provinceID != null)
                    {
                        if (statusIDs==null || statusIDs.Count == 0)
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID)  && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 1).ToString();
                        }
                        else
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID) && fld.StatusID.In(statusIDs) && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 1).ToString();
                        }
                        
                    }
                    else
                    {
                        if (statusIDs == null ||  statusIDs.Count == 0)
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 1).ToString();
                        }
                        else
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.StatusID.In(statusIDs) && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 1).ToString();
                        }

                    }
                }
                else
                    if (userID == 1)
                        query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 1).ToString();


                Helper.SaveLog("ActivityRequest", "در دست اقدام فنی", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}