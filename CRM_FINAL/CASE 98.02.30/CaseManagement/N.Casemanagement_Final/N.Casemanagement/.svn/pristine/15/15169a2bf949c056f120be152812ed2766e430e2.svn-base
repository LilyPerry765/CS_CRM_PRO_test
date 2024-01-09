
namespace CaseManagement.Administration.Endpoints
{
    using CaseManagement.Administration.Entities;
    using CaseManagement.Case;
    using CaseManagement.Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using System;
    using System.Threading;
    using Serenity.Reporting;
    using Serenity.Web;
    using MyRepository = Repositories.LogRepository;
    using MyRow = Entities.LogRow;

    [RoutePrefix("Services/Administration/Log"), Route("{action}")]
    [ConnectionKey("Default"), ServiceAuthorize(Administration.PermissionKeys.Permission)]
    public class LogController : ServiceEndpoint
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

        public LogResponse LogProvinceReport(IDbConnection connection, LogRequest request)
        {
            var fld = LogRow.Fields;

            var response = new LogResponse();

            List<int> provinceIDs = connection.List<ProvinceRow>().Select(t => (int)t.Id).ToList();

            response.Values = new List<Dictionary<string, object>>();

            foreach (int provinceID in provinceIDs)
            {
            //   List<int> userIDs = connection.List<LogRow>().Select(t => (int)t.UserId).ToList();
            //
            //   foreach (int userID in userIDs)
            //   {
            //       UserProvinceRow userProvince = connection.List<UserProvinceRow>().Where(t => t.UserId == userID && t.ProvinceId == provinceID).SingleOrDefault();
            //       if (userProvince != null)
            //           count = count + 1;
            //   }

                var d = new Dictionary<string, object>(); 
                int count = connection.List<LogRow>().Where(t => t.ProvinceId == provinceID).Count();

                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);
                d["Count"] = count.ToString();

                response.Values.Add(d);
            }

            return response;
        }

        public LogResponse LogLeaderReport(IDbConnection connection, LogRequest request)
        {
            var fld = LogRow.Fields;

            var response = new LogResponse();

            List<int> provinceIDs = connection.List<ProvinceRow>().Where(t => t.LeaderID == t.Id).Select(t => (int)t.Id).ToList();

            response.Values = new List<Dictionary<string, object>>();

            foreach (int provinceID in provinceIDs)
            {
             //  int count = 0;
             //  List<int> userIDs = connection.List<LogRow>().Select(t => (int)t.UserId).ToList();
             //
             //  foreach (int userID in userIDs)
             //  {
             //      UserProvinceRow userProvince = connection.List<UserProvinceRow>().Where(t => t.UserId == userID && t.ProvinceId == provinceID).SingleOrDefault();
             //      if (userProvince != null)
             //          count = count + 1;
             //  }

                var d = new Dictionary<string, object>();
                int count = connection.List<LogRow>().Where(t => t.ProvinceId == provinceID).Count();

                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);
                d["Count"] = count.ToString();

                response.Values.Add(d);
            }

            return response;
        }
    }
}
