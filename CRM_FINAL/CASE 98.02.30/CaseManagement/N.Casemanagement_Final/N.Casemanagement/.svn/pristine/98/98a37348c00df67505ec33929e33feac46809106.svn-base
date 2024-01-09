

namespace CaseManagement.Messaging.Repositories
{
    using CaseManagement.Messaging.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Linq;
    using System.Data;
    using MyRow = Entities.InboxRow;
    using CaseManagement.Case;
    using System.Collections.Generic;
    using CaseManagement.Administration.Entities;

    public class InboxRepository
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

        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow>
        {
            protected override void OnReturn()
            {
                base.OnReturn();

                if (Row.Seen == null)
                {
                    MessagesReceiversRow row = Connection.List<MessagesReceiversRow>().Where(t => t.Id == Row.Id).SingleOrDefault();
                    row.SeenDate = DateTime.Now;
                    row.Seen = true;

                    Connection.UpdateById<MessagesReceiversRow>(row);
                }
            }
        }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int userID = int.Parse(Authorization.UserId);

                if ((userID != 1) && (userID != 210))
                {
                    query.Where(fld.RecieverId == userID).ToString();
                }

                Helper.SaveLog("Message", "پیام های دریافتی", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }

        }
    }
}