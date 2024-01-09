

namespace CaseManagement.Case.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.ProvinceProgramRow;

    public class ProvinceProgramRepository
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

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyListHandler().Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            protected override void SetInternalFields()
            {
                base.SetInternalFields();

                if (IsCreate)
                {
                    Row.CreatedUserId = int.Parse(Authorization.UserId);
                    Row.CreatedDate = DateTime.Now;
                    Row.ModifiedUserId = int.Parse(Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;
                    Row.IsDeleted = false;

                    Helper.SaveLog("ProvinceProgram", "برنامه سالانه استان", 0, Row.ProvinceName + " - " + Row.Year, "", Connection, Administration.ActionLog.Insert);
                }
                else
                {
                    Row.ModifiedUserId = int.Parse(Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;

                    Helper.SaveLog("ProvinceProgram", "برنامه سالانه استان", Row.Id.Value, Row.ProvinceName + " - " + Row.Year, "", Connection, Administration.ActionLog.Update);
                }
            }
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { 
        
        }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int userID = int.Parse(Authorization.UserId);

                var userProvince = Entities.UserProvinceRow.Fields;
                List<int> UserProvinceRowList = Connection.List<Entities.UserProvinceRow>(t => t
                .SelectTableFields()
                .Select(userProvince.ProvinceId)
                .Where(userProvince.UserId == userID)).Select(p => p.ProvinceId.Value).ToList();

                query.Where(fld.IsDeleted == Boolean.FalseString && fld.ProvinceId.In(UserProvinceRowList)).ToString();

                Helper.SaveLog("ProvinceProgram", "برنامه سالانه استان", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}