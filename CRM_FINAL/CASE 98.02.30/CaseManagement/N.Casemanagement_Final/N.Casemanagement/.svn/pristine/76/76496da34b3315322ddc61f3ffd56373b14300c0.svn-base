

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
    using MyRow = Entities.ActivityRequestConfirmRow;

    public class ActivityRequestConfirmRepository
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
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int userID = int.Parse(Authorization.UserId);
                int? provinceID = Connection.List<UserRow>().Where(t => t.UserId == userID).Select(t => t.ProvinceId).SingleOrDefault();

                if (provinceID != null)
                    query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(provinceID) && fld.EndDate.IsNotNull()).ToString();
                else
                    query.Where(fld.IsDeleted == Boolean.FalseString && fld.EndDate.IsNotNull()).ToString();

                Helper.SaveLog("ActivityRequest", "فعالیت های تایید شده", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}