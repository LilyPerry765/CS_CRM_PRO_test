﻿

namespace CaseManagement.Case.Repositories
{
    using CaseManagement.Administration.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.ActivityRequestPenddingRow;

    public class ActivityRequestPenddingRepository
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

        private class MySaveHandler : SaveRequestHandler<MyRow> { }
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
                //   List<int> roleIDs = Connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID).Select(t => (int)t.RoleId).ToList();

                //  if (roleIDs != null && roleIDs.Count != 0)
                //  {
                // List<int> stepIDs = Connection.List<Administration.Entities.RoleStepRow>().Where(t => roleIDs.Contains((int)t.RoleId)).Select(s => (int)s.StepId).ToList();
                // List<int> statusIDs = Connection.List<WorkFlow.Entities.WorkFlowStatusRow>().Where(t => stepIDs.Contains((int)t.StepId)).Select(s => (int)s.Id).ToList();

                if (provinceID != null)
                    query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID) /*&& fld.StatusID.NotIn(statusIDs)*/ && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20").ToString();
                else
                    query.Where(fld.IsDeleted == Boolean.FalseString /*&& fld.StatusID.NotIn(statusIDs)*/ && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20").ToString();
                // }
                //   else
                //       if (userID == 1)
                //           query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNull() && fld.CreatedDate >= "2016-03-20").ToString();

                Helper.SaveLog("ActivityRequest", "فعالیت های جاری استان", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}