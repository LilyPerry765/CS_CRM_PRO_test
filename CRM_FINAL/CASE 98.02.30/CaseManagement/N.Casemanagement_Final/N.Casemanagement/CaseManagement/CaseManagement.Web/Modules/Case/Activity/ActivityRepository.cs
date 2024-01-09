

namespace CaseManagement.Case.Repositories
{
    using CaseManagement.Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.ActivityRow;

    public class ActivityRepository
    {
        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler().Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            //protected override void 


            return new MySaveHandler().Process(uow, request, SaveRequestType.Update);
        }


        
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            int id = Convert.ToInt32(request.EntityId);
            //var activityField = Entities.ActivityRow.Fields;
            //Entities.ActivityRow activity = new MyDeleteHandler().Connection.List<Entities.ActivityRow>(q => q
            //     .SelectTableFields()
            //     .Select(activityField.Id)
            //     .Where(activityField.Id == id)).SingleOrDefault();

            //activity.IsDeleted = true;
            //activity.DeletedUserId = int.Parse(Authorization.UserId);
            //activity.DeletedDate = DateTime.Now;

            DeleteResponse response = new DeleteResponse();
            return response;
            //return new MyDeleteHandler().Process(uow, request);
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

                if (Row.MainReasonList != null)
                {
                    var activityMainReason = Entities.ActivityMainReasonRow.Fields;
                    var oldList = IsCreate ? null :

                    Connection.List<Entities.ActivityMainReasonRow>(activityMainReason.ActivityId == this.Row.Id.Value);

                    foreach (Entities.ActivityMainReasonRow reason in Row.MainReasonList)
                    {
                        if (reason.Id == null)
                        {
                            reason.CreatedDate = DateTime.Now;
                            reason.CreatedUserId = int.Parse(Authorization.UserId);
                        }
                    }

                    new Common.DetailListSaveHandler<Entities.ActivityMainReasonRow>(oldList, Row.MainReasonList,
                    x => x.ActivityId = Row.Id.Value).Process(this.UnitOfWork);
                }

                if (Row.CorrectionOperationList != null)
                {
                    var activityCorrectionOperation = Entities.ActivityCorrectionOperationRow.Fields;
                    var oldList = IsCreate ? null :

                    Connection.List<Entities.ActivityCorrectionOperationRow>(activityCorrectionOperation.ActivityId == this.Row.Id.Value);

                    foreach (Entities.ActivityCorrectionOperationRow reason in Row.CorrectionOperationList)
                    {
                       /* if (reason.Id == null)
                        {
                            reason.CreatedDate = DateTime.Now;
                            reason.CreatedUserId = int.Parse(Authorization.UserId);
                        }*/
                    }

                    new Common.DetailListSaveHandler<Entities.ActivityCorrectionOperationRow>(oldList, Row.CorrectionOperationList,
                    x => x.ActivityId = Row.Id.Value).Process(this.UnitOfWork);
                }

                if (IsCreate)
                {
                    //Row.CreatedUserId = int.Parse(Authorization.UserId);
                   // Row.CreatedDate = DateTime.Now;
                    //Row.ModifiedUserId = int.Parse(Authorization.UserId);
                   // Row.ModifiedDate = DateTime.Now;
                    //Row.IsDeleted = false;

                  //  Helper.SaveLog("Activity", "فعالیت", 0, Row.Name, "", Connection, Administration.ActionLog.Insert);
                }
                else
                { 
                  
                  // MySaveHandler h = new MySaveHandler();
                  //  object oldData = h.Old;
                    //Row.ModifiedUserId = int.Parse(Authorization.UserId);
                    //Row.ModifiedDate = DateTime.Now;
                    object oldData = Connection.List<ActivityRow>().Where(t => t.Id == Row.Id).SingleOrDefault();
                    
                    Helper.SaveLog("Activity", "فعالیت", Row.Id.Value, Row.Name, "", Connection, Administration.ActionLog.Update,oldData);
                }

                 
            }
          
        }

        
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> {
            protected override void OnReturn()
            {
               
                base.OnReturn();
                var reason = Entities.ActivityMainReasonRow.Fields;
                Row.MainReasonList = Connection.List<Entities.ActivityMainReasonRow>(reason.ActivityId == Row.Id.Value);

                var correction = Entities.ActivityCorrectionOperationRow.Fields;
                Row.CorrectionOperationList = Connection.List<Entities.ActivityCorrectionOperationRow>(correction.ActivityId == Row.Id.Value);
            }
        }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);
                Helper.SaveLog("Activity", "فعالیت", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}