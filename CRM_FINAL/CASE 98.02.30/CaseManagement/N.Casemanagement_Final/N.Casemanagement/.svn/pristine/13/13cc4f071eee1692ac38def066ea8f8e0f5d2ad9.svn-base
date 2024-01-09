

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
    using MyRow = Entities.ActivityRequestLeaderRow;

    public class ActivityRequestLeaderRepository
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
                {
                    int? leaderID = Connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.LeaderID).SingleOrDefault();
                    if (provinceID == leaderID)
                    {
                        List<int> provinces = Connection.List<ProvinceRow>().Where(t => t.LeaderID == leaderID && t.Id != provinceID).Select(t => (int)t.Id).ToList();

                        query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinces) &&  fld.CreatedDate >= "2016-03-20").ToString();
                    }
                    else
                        query.Where(fld.ProvinceId.NotIn(fld.ProvinceId)).ToString();
                }
                Helper.SaveLog("ActivityRequest", "لیست زیرگروه ها", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);

            }
        }
    }
}