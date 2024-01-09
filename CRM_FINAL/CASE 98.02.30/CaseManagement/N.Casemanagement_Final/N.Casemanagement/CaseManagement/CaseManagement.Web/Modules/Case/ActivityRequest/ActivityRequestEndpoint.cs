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
    using MyRepository = Repositories.ActivityRequestRepository;
    using MyRow = Entities.ActivityRequestRow;

    [RoutePrefix("Services/Case/ActivityRequest"), Route("{action}")]
    [ConnectionKey("Default"), ServiceAuthorize(Case.PermissionKeys.JustRead)]
    public class ActivityRequestController : ServiceEndpoint
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
            return null;// new MyRepository().Delete(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRepository().Retrieve(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ActivityRequestListRequest request)
        {
            return new MyRepository().List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ActivityRequestListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ActivityRequestColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "ProductList_" +
                System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        public ActivityRequestResponse ProvinceActivityReportList(IDbConnection connection, ActivityRequestRequest request, int groupId)
        {
            var fld = ActivityRequestRow.Fields;

            var response = new ActivityRequestResponse();

            List<int> provinceIDs = connection.List<ProvinceRow>().Select(t => (int)t.Id).ToList();
            List<string> activityCodes = connection.List<ActivityRow>().Where(t => t.GroupId == groupId).Select(t => t.Code).ToList();
            List<ActivityRequestRow> requestList = connection.List<ActivityRequestRow>().Where(t => t.Id >= 95101500000).ToList();

            response.Values = new List<Dictionary<string, object>>();

            foreach (int provinceID in provinceIDs)
            {
                foreach (string code in activityCodes)
                {
                    var d = new Dictionary<string, object>();
                    bool result = requestList.Where(t => t.ProvinceId == provinceID && t.ActivityCode == code).Any();

                    d["Province"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);
                    d["Code"] = code;
                    d["Result"] = result.ToString();

                    response.Values.Add(d);
                }
            }

            return response;
        }

        

    }
}
