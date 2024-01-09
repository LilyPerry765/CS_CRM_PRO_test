

namespace CaseManagement.Messaging.Repositories
{
    using CaseManagement.Messaging.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.NewMessageRow;

    public class NewMessageRepository
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

        private class MySaveHandler : SaveRequestHandler<MyRow> {
            protected override void SetInternalFields()
            {
                base.SetInternalFields();

                Row.InsertedDate = DateTime.Now;
                Row.SenderId = Convert.ToInt32(Authorization.UserId);            
            }

            protected override void AfterSave()
            {
                base.AfterSave();
                
                List<MessagesReceiversRow> messagesReceiverList = Connection.List<MessagesReceiversRow>().Where(t => t.MessageId == Row.Id).ToList();

                foreach (MessagesReceiversRow item in messagesReceiverList)
                {
                    item.SenderId = Row.SenderId;

                    Connection.UpdateById<MessagesReceiversRow>(item);
                }
            }
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { }
        private class MyListHandler : ListRequestHandler<MyRow> {
           protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

            }
        }
    }
}