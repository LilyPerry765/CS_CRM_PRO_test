﻿
namespace CaseManagement.Case.Endpoints
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Web.Mvc;
    using MyRepository = Repositories.ProvinceRepository;
    using MyRow = Entities.ProvinceRow;

    [RoutePrefix("Services/Case/Province"), Route("{action}")]
    [ConnectionKey("Default"), ServiceAuthorize(Case.PermissionKeys.JustRead)]
    public class ProvinceController : ServiceEndpoint
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

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository().List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ProvinceColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "ProductList_" +
                System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }
    }
}