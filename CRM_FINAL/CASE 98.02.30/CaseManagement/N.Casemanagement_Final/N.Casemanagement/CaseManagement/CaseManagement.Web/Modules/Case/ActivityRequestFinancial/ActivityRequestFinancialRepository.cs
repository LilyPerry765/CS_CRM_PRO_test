﻿

namespace CaseManagement.Case.Repositories
{
    using CaseManagement.Administration.Entities;
    using CaseManagement.Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.ActivityRequestFinancialRow;

    public class ActivityRequestFinancialRepository
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
            protected override void SetInternalFields()
            {
                base.SetInternalFields();

                try
                {
                    if (Row.DelayedCost == 0 || Row.DelayedCost == null)
                        if (Row.AccessibleCost != null)
                            if (Row.AccessibleCost != 0)
                                throw new Exception("به دلیل وارد نکردن مبلغ معوق لطفا مبلغ قایل وصول را صفر نمایید");

                    if (Row.DelayedCost == 0 || Row.DelayedCost == null)
                        if (Row.InaccessibleCost != null)
                            if (Row.InaccessibleCost != 0)
                                throw new Exception("به دلیل وارد نکردن مبلغ معوق لطفا مبلغ غیر قایل وصول را صفر نمایید");

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

                    if (Row.DiscoverLeakDate > Row.CreatedDate)
                        throw new Exception("تاریخ شناسایی نمی تواند بعد از تاریخ ایجاد باشد");


                   
                    if (Row.CycleCost != null && Row.Factor != null)
                        Row.YearCost = Row.CycleCost * Row.Factor;
                    else
                        Row.YearCost = null;

                    Row.ActivityCode = Connection.List<ActivityRow>().Where(t => t.Id == (int)Row.ActivityId).Select(t => t.Code).SingleOrDefault();

                    if (Row.CommnetList != null)
                    {
                        var activityComment = Entities.ActivityRequestCommentRow.Fields;
                        var oldList = IsCreate ? null :

                        Connection.List<Entities.ActivityRequestCommentRow>(activityComment.ActivityRequestId == this.Row.Id.Value);

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

                    Helper.SaveLog("Activity", "در دست اقدام مالی", Row.Id.Value, Row.Id.ToString(), "", Connection, Administration.ActionLog.Update);
                    if (Row.StatusID != null)
                        ActivityRequestLogDB.SaveActivityRequestLog(Row.Id.Value, (int)Row.StatusID, Row.ActionID.Value, Connection);


                    int? statusID = Row.StatusID;

                    if (Row.ActionID == null)
                        throw new Exception("لطفا عملیات مورد نظر را انتخاب نمایید");

                    int userID = int.Parse(Authorization.UserId);
                    List<int> roleIDs = Connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID).Select(t => (int)t.RoleId).ToList();




                    if (Int32.Parse(Helper.GetPersianDate(Row.DiscoverLeakDate, Helper.DateStringType.TwoDigitsYear).Substring(0, 2)) < Int32.Parse(Helper.GetPersianDate(DateTime.Now, Helper.DateStringType.TwoDigitsYear).Substring(0, 2)))
                    {
                        throw new Exception("تاریخ شناسایی نشتی باید در سال جاری باشد.");
                    }
                    


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

                    switch (Row.ActionID)
                    {
                        case RequestAction.Save:
                            break;

                        case RequestAction.Forward:

                            if (Row.ConfirmTypeID == ConfirmType.Technical)
                                throw new Exception("لطفا تایید مالی را انتخاب نمایید.");


                            if (Row.StatusID == 9)
                            {
                                if (Row.CycleCost == null)
                                    throw new Exception("لطفا مبلغ سیکل را وارد نمایید");
                                if (Row.DelayedCost == null)
                                    throw new Exception("لطفا مبلغ معوق را وارد نمایید");
                                if (Row.AccessibleCost == null)
                                    throw new Exception("لطفا مبلغ قابل وصول معوق را وارد نمایید");
                                if (Row.InaccessibleCost == null)
                                    throw new Exception("لطفا مبلغ غیر قابل وصول معوق را وارد نمایید");
                            }

                            int nextStatusIDForward = Connection.List<WorkFlow.Entities.WorkFlowRuleRow>().Where(t => t.CurrentStatusId == Row.StatusID.Value && t.ActionId == 1)
                                                                .Select(t => (int)t.NextStatusId).SingleOrDefault();

                            Row.StatusID = nextStatusIDForward;
                            Row.SendDate = DateTime.Now;
                            Row.SendUserId = userID;
                            Row.IsRejected = false;

                            if (roleIDs != null && roleIDs.Count != 0)
                            {
                                if (roleIDs.Contains(10) || roleIDs.Contains(14))
                                {
                                    DateTime date97 = new DateTime(2018, 3, 21);
                                   // DateTime date96 = new DateTime(2017, 3, 21);
                                    Entities.ProvinceProgramRow program1 = new ProvinceProgramRow();
                                    Entities.ProvinceProgramRow program2 = new ProvinceProgramRow();

                                    if (Row.DiscoverLeakDate >= date97)
                                    {
                                        program1 = Connection.List<Entities.ProvinceProgramRow>().Where(t => t.ProvinceId == Row.ProvinceId && t.YearId == 8).SingleOrDefault();
                                        program2 = Connection.List<Entities.ProvinceProgramRow>().Where(t => t.ProvinceId == Row.ProvinceId && t.YearId == 7).SingleOrDefault();

                                        if(program1 == null)
                                        {
                                            throw new Exception("برنامه هدف سال جدید تعیین نشده است.");
                                        }

                                    }
                                    else
                                    {
                                        program1 = Connection.List<Entities.ProvinceProgramRow>().Where(t => t.ProvinceId == Row.ProvinceId && t.YearId == 7).SingleOrDefault();
                                        program2 = Connection.List<Entities.ProvinceProgramRow>().Where(t => t.ProvinceId == Row.ProvinceId && t.YearId == 6).SingleOrDefault();
                                    }

                                    if (program1 != null)
                                    {
                                        Entities.ProvinceProgramLogRow programLog = new Entities.ProvinceProgramLogRow();
                                        programLog.ActivityRequestID = Row.Id;
                                        programLog.ProvinceId = program1.ProvinceId;
                                        programLog.YearId = program1.YearId;
                                        programLog.OldTotalLeakage = program1.TotalLeakage;
                                        programLog.OldRecoverableLeakage = program1.RecoverableLeakage;
                                        programLog.OldRecovered = program1.Recovered;

                                        program1.TotalLeakage = program1.TotalLeakage + Row.TotalLeakage;
                                        program1.RecoverableLeakage = program1.RecoverableLeakage + Row.RecoverableLeakage;
                                        program1.Recovered = program1.Recovered + Row.Recovered;

                                        decimal percent = (((decimal)program1.Recovered / (decimal)program1.Program) * 100);
                                        program1.PercentRecovered = Math.Round(percent, 2).ToString();

                                        percent = 0;
                                        percent = (((decimal)program1.TotalLeakage / (decimal)program1.Program) * 100);
                                        program1.PercentTotalLeakage = Math.Round(percent, 2).ToString();

                                        percent = 0;
                                        percent = (((decimal)program1.RecoverableLeakage / (decimal)program1.Program) * 100);
                                        program1.PercentRecoverableLeakage = Math.Round(percent, 2).ToString();

                                        percent = 0;
                                        percent = (((decimal)program1.Recovered / (decimal)program1.TotalLeakage) * 100);
                                        program1.PercentRecoveredonTotal = Math.Round(percent, 2).ToString();

                                        if (program2.Recovered == 0)
                                            program1.PercentRecovered94to95 = "100";
                                        else
                                        {
                                            percent = ((decimal)program1.Recovered / (decimal)program2.Recovered);
                                            program1.PercentRecovered94to95 = Math.Round(percent, 2).ToString();
                                        }

                                        if (program2.TotalLeakage == 0)
                                            program1.PercentTotal94to95 = "100";
                                        else
                                        {
                                            percent = ((decimal)program1.TotalLeakage / (decimal)program2.TotalLeakage);
                                            program1.PercentTotal94to95 = Math.Round(percent, 2).ToString();
                                        }

                                        Connection.UpdateById<Entities.ProvinceProgramRow>(program1);

                                        programLog.NewTotalLeakage = program1.TotalLeakage;
                                        programLog.NewRecoverableLeakage = program1.RecoverableLeakage;
                                        programLog.NewRecovered = program1.Recovered;
                                        programLog.UserId = userID;
                                        programLog.InsertDate = DateTime.Now;


                                        Connection.Insert<Entities.ProvinceProgramLogRow>(programLog);
                                    }

                                    Row.EndDate = DateTime.Now;
                                }
                                if (roleIDs.Contains(13) && roleIDs.Contains(15))
                                    Row.StatusID = 8;
                            }

                            break;

                        case RequestAction.Deny:
                            if (roleIDs != null && roleIDs.Count != 0)
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

                                        //Send sms to head of Province and Inform for this Rejection

                               
                                        int ProvinceID = (int)Row.ProvinceId;

                                        if (ProvinceID != null)
                                        {
                                            List<UserRow> ProvinceUsers = null;


                                            using (var connection = SqlConnections.NewFor<UserRow>())
                                            {
                                                ProvinceUsers = connection.List<UserRow>().Where(t => t.ProvinceId == ProvinceID).ToList();
                                            }

                                             foreach( UserRow User in ProvinceUsers)
                                             {
                                                 List<int> UserRoles = null;
                                                
                                                 using (var connection = SqlConnections.NewFor<UserRoleRow>())
                                                 {
                                                     UserRoles = Connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == User.UserId).Select(t => (int)t.RoleId).ToList();

                                                       //= connection.List<UserRoleRow>().Where(t => t.UserId == User.UserId).ToList();
                                                 }
                                                 if (UserRoles != null)
                                                 {
                                                     if (UserRoles.Contains(1))
                                                     {

                                                         string mobileNo = string.Empty;

                                                         mobileNo = User.MobileNo;

                                                         System.Net.ServicePointManager.Expect100Continue = false;
                                                         SMSService sms = new SMSService();
                                                         string[] senderNumbers = { "60020184040" };

                                                         if (!string.IsNullOrEmpty(mobileNo))
                                                         {

                                                             string[] recipientNumbers = { mobileNo };
                                                             string[] date = { DateTime.Now.ToString() };
                                                             long[] ids = { 1 };
                                                             int[] messageClasses = { 1 };
                                                             //string ip = GetIPAddress();
                                                             string now = Helper.GetPersianDate(DateTime.Now, Helper.DateStringType.DateTime);
                                                             string[] body = { "فعالیت با شناسه " + Helper.ToPersianNumber(Row.Id.ToString()) + " بازگشت پذیرفته و در کارتابل شما قابل مشاهده است. " };

                                                             long[] sample = sms.SendSMS("tazmin", "mpsms7521", senderNumbers, recipientNumbers, body, null, null, null);

                                                             Helper.SaveLog("باز گشت فعالیت", "پیام کوتاه بازگشت فعالیت", (int)Row.Id, User.DisplayName, " ", Connection, Administration.ActionLog.Update);
                                                         }
                                                     }
                                                 }

                                             }

                                        }

                                    }
                                    else
                                    {
                                        Row.RejectCount = Row.RejectCount + 1;
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
                            break;

                        case RequestAction.Delete:
                            Row.IsDeleted = true;
                            Row.DeletedUserId = int.Parse(Authorization.UserId);
                            Row.DeletedDate = DateTime.Now;

                            Helper.SaveLog("Activity", "در دست اقدام مالی", Row.Id.Value, Row.Id.ToString(), "", Connection, Administration.ActionLog.Delete);
                            break;


                        // case RequestAction.ConfirmTechnical:
                        //     if (roleIDs != null && roleIDs.Count != 0)
                        //     {
                        //         foreach (int roleID in roleIDs)
                        //         {
                        //             if (roleID != 10)
                        //                 throw new Exception("لطفا عملیات تایید و ارسال را انتخاب نمایید");
                        //         }
                        //     }
                        //
                        //     var ruleConfirm = WorkFlow.Entities.WorkFlowRuleRow.Fields;
                        //     int nextStatusIDConfirm = Connection.List<WorkFlow.Entities.WorkFlowRuleRow>(ruleConfirm.CurrentStatusId == Row.StatusID.Value && ruleConfirm.ActionId == 1)
                        //                                         .Select(s => (int)s.NextStatusId).SingleOrDefault();
                        //
                        //     Row.StatusID = nextStatusIDConfirm;
                        //     Row.IsRejected = false;
                        //     Row.ConfirmTypeID = ConfirmType.Financial;
                        //     Row.SendDate = DateTime.Now;
                        //     Row.SendUserId = userID;
                        //
                        //     break;

                        // case RequestAction.ConfirmFinancial:
                        //     if (roleIDs != null && roleIDs.Count != 0)
                        //     {
                        //         foreach (int roleID in roleIDs)
                        //         {
                        //             if (roleID != 10)
                        //                 throw new Exception("لطفا عملیات تایید و ارسال را انتخاب نمایید");
                        //         }
                        //     }
                        //
                        //     var programField1 = Entities.ProvinceProgramRow.Fields;
                        //     Entities.ProvinceProgramRow program1 = Connection.List<Entities.ProvinceProgramRow>(programField1.ProvinceId.In(Row.ProvinceId) &&
                        //                                                                                        programField1.YearId.In(6)).SingleOrDefault();
                        //
                        //     if (program1 != null)
                        //     {
                        //         Entities.ProvinceProgramLogRow programLog = new Entities.ProvinceProgramLogRow();
                        //         programLog.ActivityRequestID = Row.Id;
                        //         programLog.ProvinceId = program1.ProvinceId;
                        //         programLog.YearId = program1.YearId;
                        //         programLog.OldTotalLeakage = program1.TotalLeakage;
                        //         programLog.OldRecoverableLeakage = program1.RecoverableLeakage;
                        //         programLog.OldRecovered = program1.Recovered;
                        //
                        //         program1.TotalLeakage = program1.TotalLeakage + Row.TotalLeakage;
                        //         program1.RecoverableLeakage = program1.RecoverableLeakage + Row.RecoverableLeakage;
                        //         program1.Recovered = program1.Recovered + Row.Recovered;
                        //
                        //         decimal percent = Convert.ToDecimal(program1.PercentRecovered) + (((decimal)program1.Recovered / (decimal)program1.Program) * 100);
                        //         //percent = percent * 100;
                        //         program1.PercentRecovered = Math.Round(percent, 2).ToString();
                        //
                        //         percent = 0;
                        //         percent = Convert.ToDecimal(program1.PercentTotalLeakage) + (((decimal)program1.TotalLeakage / (decimal)program1.Program) * 100);
                        //         // percent = percent * 100;
                        //         program1.PercentTotalLeakage = Math.Round(percent, 2).ToString();
                        //
                        //         Connection.UpdateById<Entities.ProvinceProgramRow>(program1);
                        //
                        //         programLog.NewTotalLeakage = program1.TotalLeakage;
                        //         programLog.NewRecoverableLeakage = program1.RecoverableLeakage;
                        //         programLog.NewRecovered = program1.Recovered;
                        //         programLog.UserId = userID;
                        //         programLog.InsertDate = DateTime.Now;
                        //
                        //         Connection.Insert<Entities.ProvinceProgramLogRow>(programLog);
                        //     }
                        //
                        //     // Row.ActionID = RequestAction.ConfirmFinancial;
                        //     Row.EndDate = DateTime.Now;
                        //     Row.StatusID = 5;
                        //     Row.SendUserId = userID;
                        //
                        //     break;
                        default:
                            break;
                    }
                    Row.ActionID = RequestAction.Save;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
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
                        if (statusIDs == null || statusIDs.Count == 0)
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID) && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 2).ToString();
                        }
                        else
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID) && fld.StatusID.In(statusIDs) && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 2).ToString();
                        }

                    }
                    else
                    {
                        if (statusIDs == null || statusIDs.Count == 0)
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 2).ToString();
                        }
                        else
                        {
                            query.Where(fld.IsDeleted == Boolean.FalseString && fld.StatusID.In(statusIDs) && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 2).ToString();
                        }

                    }

               }
                else
                    if (userID == 1)
                        query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20" && fld.ConfirmTypeID == 2).ToString();

                Helper.SaveLog("ActivityRequest", "در دست اقدام مالی", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}