

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
    using MyRow = Entities.ActivityRequestRow;
    using CaseManagement.Case.Entities;

    public class ActivityRequestRepository
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
            return null;// new MyDeleteHandler().Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler().Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ActivityRequestListRequest request)
        {
            return new MyListHandler().Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {           
        }
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
        private class MyListHandler : ListRequestHandler<MyRow, ActivityRequestListRequest>
        {
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int userID = int.Parse(Authorization.UserId);

                List<int> provinceIDs = Connection.List<Entities.UserProvinceRow>().Where(t => t.UserId == userID).Select(t => (int)t.ProvinceId).ToList();
                List<int> roleIDs = Connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID).Select(t => (int)t.RoleId).ToList();

                if (roleIDs != null && roleIDs.Count != 0 && !roleIDs.Contains(10) && !roleIDs.Contains(11) && !roleIDs.Contains(14))
                {
                    List<int> stepIDs = Connection.List<Administration.Entities.RoleStepRow>().Where(t => roleIDs.Contains((int)t.RoleId)).Select(s => (int)s.StepId).ToList();
                    List<int> statusIDs = Connection.List<WorkFlow.Entities.WorkFlowStatusRow>().Where(t=>stepIDs.Contains((int)t.StepId)).Select(s => (int)s.Id).ToList();

                    query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceIDs) && fld.CreatedDate >= "2016-03-20").ToString();
                }
                else
                    query.Where(fld.IsDeleted == Boolean.FalseString  && fld.CreatedDate >= "2016-03-20").ToString();  
                
                //query.Where(fld.IsDeleted == Boolean.FalseString && fld.CreatedDate >= "2016-03-20" ).ToString();

            }
        }
    }
}