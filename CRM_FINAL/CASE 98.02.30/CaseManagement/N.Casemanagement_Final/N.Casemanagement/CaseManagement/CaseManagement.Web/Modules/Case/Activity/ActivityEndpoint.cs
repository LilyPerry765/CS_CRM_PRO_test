﻿
namespace CaseManagement.Case.Endpoints
{    
    using CaseManagement.Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using MyRepository = Repositories.ActivityRepository;
    using MyRow = Entities.ActivityRow;

    [RoutePrefix("Services/Case/Activity"), Route("{action}")]
    [ConnectionKey("Default"), ServiceAuthorize(Case.PermissionKeys.JustRead)]
    public class ActivityController : ServiceEndpoint
    {
        [HttpPost]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository().Create(uow, request);
        }

        [HttpPost]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository().Update(uow, request);
        }

        [HttpPost]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyRepository().Delete(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRepository().Retrieve(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository().List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ActivityColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "ActivityList_" +
                System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        public ActivityResponse ActivitybyGroupList(IDbConnection connection, ActivityRequest request)
        {
            var fld = ActivityRow.Fields;

            var response = new ActivityResponse();

            List<int> groupIDs = connection.List<ActivityGroupRow>().Select(t => (int)t.Id).ToList();
            response.Values = new List<Dictionary<string, object>>();

            foreach (int groupID in groupIDs)
            {
                var d = new Dictionary<string, object>();                

                d["GroupId"] = connection.List<ActivityGroupRow>().Where(t => t.Id == groupID).Select(t => t.Id);
                d["GroupName"] = connection.List<ActivityGroupRow>().Where(t => t.Id == groupID).Select(t => t.Name);
                d["Code"] = connection.List<ActivityRow>().Where(t => t.GroupId == groupID).Select(t => t.Code).ToList();
                d["Name"] = connection.List<ActivityRow>().Where(t => t.GroupId == groupID).Select(t => t.Name).ToList();

                response.Values.Add(d);
            }

            return response;
        }
    }
}