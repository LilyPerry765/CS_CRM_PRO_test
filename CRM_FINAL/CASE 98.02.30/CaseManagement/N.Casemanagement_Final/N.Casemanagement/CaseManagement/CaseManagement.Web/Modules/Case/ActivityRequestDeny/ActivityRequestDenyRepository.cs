

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
    using MyRow = Entities.ActivityRequestDenyRow;
    using CaseManagement.Administration.Entities;

    public class ActivityRequestDenyRepository
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
                
                if (provinceID != null)
                    query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID) && fld.EndDate.IsNull() && fld.IsRejected == Boolean.TrueString).ToString();
                else
                    query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNull() && fld.IsRejected == Boolean.TrueString).ToString();

                Helper.SaveLog("ActivityRequest", "فعالیت های ارجاع شده", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}