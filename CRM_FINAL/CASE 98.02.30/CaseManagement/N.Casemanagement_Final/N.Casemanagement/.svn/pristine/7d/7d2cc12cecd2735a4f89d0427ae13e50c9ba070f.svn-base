

namespace CaseManagement.WorkFlow.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = Entities.WorkFlowStatusRow;

    public class WorkFlowStatusRepository
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

                    var step = Entities.WorkFlowStepRow.Fields;
                    List<Entities.WorkFlowStepRow> stepList = Connection.List<Entities.WorkFlowStepRow>(q => q
                    .SelectTableFields()
                    .Select(step.Name)
                    .Where(step.Id == Row.StepId.Value));

                    var statusType = Entities.WorkFlowStatusTypeRow.Fields;
                    List<Entities.WorkFlowStatusTypeRow> statusTypeList = Connection.List<Entities.WorkFlowStatusTypeRow>(q => q
                    .SelectTableFields()
                    .Select(statusType.Name)
                    .Where(statusType.Id == Row.StatusTypeId.Value));

                    Row.Name = stepList[0].Name + " - " + statusTypeList[0].Name;

                    Case.Helper.SaveLog("WorkFlowStatus", "وضعیت جریان کاری", 0, Row.Name, "", Connection, Administration.ActionLog.Insert);
                }
                else
                {
                    Row.ModifiedUserId = int.Parse(Authorization.UserId);
                    Row.ModifiedDate = DateTime.Now;
                    
                    var step = Entities.WorkFlowStepRow.Fields;
                    List<Entities.WorkFlowStepRow> stepList = Connection.List<Entities.WorkFlowStepRow>(q => q
                    .SelectTableFields()
                    .Select(step.Name)
                    .Where(step.Id == Row.StepId.Value));

                    var statusType = Entities.WorkFlowStatusTypeRow.Fields;
                    List<Entities.WorkFlowStatusTypeRow> statusTypeList = Connection.List<Entities.WorkFlowStatusTypeRow>(q => q
                    .SelectTableFields()
                    .Select(statusType.Name)
                    .Where(statusType.Id == Row.StatusTypeId.Value));

                    Row.Name = stepList[0].Name + " - " + statusTypeList[0].Name;

                    Case.Helper.SaveLog("WorkFlowStatus", "وضعیت جریان کاری", Row.Id.Value, Row.Name, "", Connection, Administration.ActionLog.Update);
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

                Case.Helper.SaveLog("WorkFlowStatus", "وضعیت جریان کاری", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}