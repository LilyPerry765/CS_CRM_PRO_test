﻿

namespace CaseManagement.Case.Repositories
{
    using CaseManagement.Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.ActivityGroupRow;

    public class ActivityGroupRepository
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
                  /*  Row.CreatedUserId = int.Parse(Serenity.Authorization.UserId);
                    Row.CreatedDate = DateTime.Now;
                    Row.ModifiedUserId = int.Parse(Serenity.Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;
                    Row.IsDeleted = false;*/

                    Helper.SaveLog("ActivityGroup", "گروه فعالیت", 0, Row.Name, "", Connection, Administration.ActionLog.Insert);
                }
                else
                {
                    /*Row.ModifiedUserId = int.Parse(Serenity.Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;*/

                    object OldData = Connection.List<ActivityGroupRow>().Where(t => t.Id == Row.Id).SingleOrDefault();

                    Helper.SaveLog("ActivityGroup", "گروه فعالیت", Row.Id.Value, Row.Name, "", Connection, Administration.ActionLog.Update,OldData);
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
                
               // query.Where(fld.IsDeleted == Boolean.FalseString).ToString();

                Helper.SaveLog("ActivityGroup", "گروه فعالیت", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }

        }
    }
}