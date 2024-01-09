﻿

namespace CaseManagement.Case.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Data;
    using MyRow = Entities.RepeatTermRow;

    public class RepeatTermRepository
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

                if (IsCreate)
                {
                    Row.CreatedUserId = int.Parse(Authorization.UserId);
                    Row.CreatedDate = DateTime.Now;
                    Row.ModifiedUserId = int.Parse(Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;
                    Row.IsDeleted = false;

                    Helper.SaveLog("RepeatTerm", "تکرار دوره", 0, Row.Name, "", Connection, Administration.ActionLog.Insert);
                }
                else
                {
                    Row.ModifiedUserId = int.Parse(Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;

                    Helper.SaveLog("RepeatTerm", "تکرار دوره", Row.Id.Value, Row.Name, "", Connection, Administration.ActionLog.Update);
                }
            }
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                query.Where(fld.IsDeleted == Boolean.FalseString).ToString();

                Helper.SaveLog("RepeatTerm", "تکرار دوره", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}