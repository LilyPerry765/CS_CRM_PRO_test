﻿
namespace CaseManagement.Case.Endpoints
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using CaseManagement.Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using MyRepository = Repositories.ProvinceProgramRepository;
    using MyRow = Entities.ProvinceProgramRow;
    using System.Web.Script.Serialization;

    [RoutePrefix("Services/Case/ProvinceProgram"), Route("{action}")]
    [ConnectionKey("Default"), ServiceAuthorize(Case.PermissionKeys.JustRead)]
    public class ProvinceProgramController : ServiceEndpoint
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
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ProvinceProgramColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "ProductList_" +
                System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        public ProvinceProgramResponse ProvinceProgramLineReport96(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 7).ToList();

            response.Values = new List<Dictionary<string, object>>();

            for (var i = 0; i < provincePrograms.Count; i++)
            {
                var program = provincePrograms[i];

                var d = new Dictionary<string, object>();

                int provinceID = (int)program.ProvinceId;
                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);

                d["Leak"] = program.TotalLeakage.ToString();
                d["Confirm"] = program.Recovered.ToString();
                d["Program"] = program.Program.ToString();


                response.Values.Add(d);
            }

            return response;
        }

        public ProvinceProgramResponse ProvinceProgramLineReport(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 6).ToList();

            response.Values = new List<Dictionary<string, object>>();

            for (var i = 0; i < provincePrograms.Count; i++)
            {
                var program = provincePrograms[i];

                var d = new Dictionary<string, object>();

                int provinceID = (int)program.ProvinceId;
                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);

                d["Leak"] = program.TotalLeakage.ToString();
                d["Confirm"] = program.Recovered.ToString();
                d["Program"] = program.Program.ToString();


                response.Values.Add(d);
            }

            return response;
        }

        public ProvinceProgramResponse ProvinceProgramLineReport94(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 5).ToList();

            response.Values = new List<Dictionary<string, object>>();

            for (var i = 0; i < provincePrograms.Count; i++)
            {
                var program = provincePrograms[i];

                var d = new Dictionary<string, object>();

                int provinceID = (int)program.ProvinceId;
                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);

                d["Leak"] = program.TotalLeakage.ToString();
                d["Confirm"] = program.Recovered.ToString();
                d["Program"] = program.Program.ToString();


                response.Values.Add(d);
            }

            return response;
        }

        public ProvinceProgramResponse ProvinceProgramLineReport93(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 4).ToList();

            response.Values = new List<Dictionary<string, object>>();

            for (var i = 0; i < provincePrograms.Count; i++)
            {
                var program = provincePrograms[i];

                var d = new Dictionary<string, object>();

                int provinceID = (int)program.ProvinceId;
                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);

                d["Leak"] = program.TotalLeakage.ToString();
                d["Confirm"] = program.Recovered.ToString();
                d["Program"] = program.Program.ToString();

                response.Values.Add(d);
            }

            return response;
        }

        public ProvinceProgramResponse ProvinceProgramLineReport92(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 3).ToList();

            response.Values = new List<Dictionary<string, object>>();

            for (var i = 0; i < provincePrograms.Count; i++)
            {
                var program = provincePrograms[i];

                var d = new Dictionary<string, object>();

                int provinceID = (int)program.ProvinceId;
                d["Provinve"] = connection.List<ProvinceRow>().Where(t => t.Id == provinceID).Select(t => t.Name);

                d["Leak"] = program.TotalLeakage.ToString();
                d["Confirm"] = program.Recovered.ToString();
                d["Program"] = program.Program.ToString();

                response.Values.Add(d);
            }

            return response;
        }

        public ProvinceProgramResponse LeakProgramReport95(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 6).ToList();

            long sumProgram = provincePrograms.Sum(t => (long)t.Program);
            long sumLeak = provincePrograms.Sum(t => (long)t.TotalLeakage);
            long sumOther = sumProgram - sumLeak;

            response.Values = new List<Dictionary<string, object>>();
            //var dlist = new List<Dictionary<string, object>>();
            var d = new Dictionary<string, object>();

            d["label"] = "نشتی اولیه";
            d["data"] = sumLeak.ToString();
            d["color"] = "#f56954";
            //d["highlight"] = "#f56954";
            response.Values.Add(d);

            d = new Dictionary<string, object>();

            d["label"] = "باقیمانده تا برنامه هدف";
            d["data"] = sumOther.ToString();
            d["color"] = "#00a65a";
            //  d["highlight"] = "#00a65a";
            response.Values.Add(d);           

            return response;
        }

        public ProvinceProgramResponse ConfirmProgramReport95(IDbConnection connection, ProvinceProgramRequest request)
        {
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 6).ToList();

            long sumProgram = provincePrograms.Sum(t => (long)t.Program);
            long sumConfirm = provincePrograms.Sum(t => (long)t.Recovered);
            long sumOther = sumProgram - sumConfirm;

            response.Values = new List<Dictionary<string, object>>();
            //var dlist = new List<Dictionary<string, object>>();
            var d = new Dictionary<string, object>();

            d["label"] = "تایید شده";
            d["data"] = sumConfirm.ToString();
            d["color"] = "#f56954";
            //d["highlight"] = "#f56954";
            response.Values.Add(d);

            d = new Dictionary<string, object>();

            d["label"] = "باقیمانده تا برنامه هدف";
            d["data"] = sumOther.ToString();
            d["color"] = "#00a65a";
            //  d["highlight"] = "#00a65a";
            response.Values.Add(d);

            return response;
        }

        public ProvinceProgramResponse LeakConfirmReport95(IDbConnection connection, ProvinceProgramRequest request)
        { 
            var fld = ProvinceProgramRow.Fields;

            var response = new ProvinceProgramResponse();
            var provincePrograms = connection.List<ProvinceProgramRow>(q => q.SelectTableFields().OrderBy(ProvinceProgramRow.Fields.ProvinceName)).Where(q => q.YearId == 6).ToList();
                        
            long sumLeak = provincePrograms.Sum(t => (long)t.TotalLeakage);
            long sumConfirm = provincePrograms.Sum(t => (long)t.Recovered);
            long sumOther = sumLeak - sumConfirm;

            response.Values = new List<Dictionary<string, object>>();
            //var dlist = new List<Dictionary<string, object>>();
            var d = new Dictionary<string, object>();

            d["label"] = "تایید شده";
            d["data"] = sumLeak.ToString();
            d["color"] = "#f56954";
            //d["highlight"] = "#f56954";
            response.Values.Add(d);

            d = new Dictionary<string, object>();

            d["label"] = "نشتی اولیه";
            d["data"] = sumOther.ToString();
            d["color"] = "#00a65a";
            //  d["highlight"] = "#00a65a";
            response.Values.Add(d);

            return response;
        }
    }
}

