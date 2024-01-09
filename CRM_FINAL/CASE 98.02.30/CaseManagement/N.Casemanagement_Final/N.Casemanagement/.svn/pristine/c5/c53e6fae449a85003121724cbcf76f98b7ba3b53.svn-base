

namespace CaseManagement.Case.Repositories
{
    using CaseManagement.Administration.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Data;
    using System.Linq;
    using MyRow = Entities.ActivityRequestConfirmAdminRow;

    public class ActivityRequestConfirmAdminRepository
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

                try
                {
                    switch (Row.ActionID)
                    {
                        case RequestActionAdmin.Delete:
                            Row.IsDeleted = true;
                            Row.DeletedDate = DateTime.Now;
                            Row.DeletedUserId = int.Parse(Authorization.UserId);

                            ActivityRequestLogDB.SaveActivityRequestLog(Row.Id.Value, (int)Row.StatusID, RequestAction.Delete, Connection);
                            Helper.SaveLog("Activity", "فعالیت های تایید شده مدیریت", Row.Id.Value, Row.Id.ToString(), "", Connection, Administration.ActionLog.Delete);
                            break;

                        case RequestActionAdmin.Deny:

                            ActivityRequestLogDB.SaveActivityRequestLog(Row.Id.Value, (int)Row.StatusID, RequestAction.Deny, Connection);
                            
                            Row.EndDate = null;
                            Row.SendDate = DateTime.Now;
                            Row.SendUserId = int.Parse(Authorization.UserId);
                            Row.IsRejected = true;
                            Row.RejectCount = Row.RejectCount + 1;
                            Row.StatusID = 9;

                            if (Row.CommnetList != null)
                            {
                                var activityComment = Entities.ActivityRequestCommentRow.Fields;
                                var oldList = IsCreate ? null :

                                Connection.List<Entities.ActivityRequestCommentRow>(activityComment.ActivityRequestId == this.Row.Id.Value);

                                foreach (Entities.ActivityRequestCommentRow comment in Row.CommnetList)
                                {
                                    if (comment.Id == null)
                                    {
                                        comment.CreatedDate = DateTime.Now;
                                        comment.CreatedUserId = int.Parse(Authorization.UserId);
                                    }
                                }

                                new Common.DetailListSaveHandler<Entities.ActivityRequestCommentRow>(oldList, Row.CommnetList,
                                x => x.ActivityRequestId = Row.Id.Value).Process(this.UnitOfWork);
                            }

                            Helper.SaveLog("Activity", "فعالیت های تایید شده مدیریت", Row.Id.Value, Row.Id.ToString(), "", Connection, Administration.ActionLog.Update);
                            break;
                    }

                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
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

                Helper.SaveLog("ActivityRequest", "فعالیت های تایید شده مدیریت", 0, "مشاهده لیست", "", Connection, Administration.ActionLog.View);
            }
        }
    }
}