
namespace CaseManagement.Administration.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Extensibility;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using MyRow = Entities.RoleStepRow;

    public class RoleStepRepository
    {
        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Update(IUnitOfWork uow, RoleStepUpdateRequest request)
        {
            Check.NotNull(request, "request");
            Check.NotNull(request.RoleID, "roleID");
            Check.NotNull(request.Steps, "permissions");

            var roleID = request.RoleID.Value;
            var oldList = new HashSet<Int32>(
                GetExisting(uow.Connection, roleID)
                .Select(x => x.StepId.Value));

            var newList = new HashSet<Int32>(request.Steps.ToList());

            if (oldList.SetEquals(newList))
                return new SaveResponse();

            foreach (var k in oldList)
            {
                if (newList.Contains(k))
                    continue;

                new SqlDelete(fld.TableName)
                    .Where(
                        new Criteria(fld.RoleId) == roleID &
                        new Criteria(fld.StepId) == k)
                    .Execute(uow.Connection);
            }

            foreach (var k in newList)
            {
                if (oldList.Contains(k))
                    continue;

                uow.Connection.Insert(new MyRow
                {
                    RoleId = roleID,
                    StepId = k,
                    CreatedUserId=Convert.ToInt32(Authorization.UserId),
                    CreatedDate=DateTime.Now,
                    ModifiedUserId = Convert.ToInt32(Authorization.UserId),
                    ModifiedDate=DateTime.Now,
                    IsDeleted=false
                });
            }

            BatchGenerationUpdater.OnCommit(uow, fld.GenerationKey);
            BatchGenerationUpdater.OnCommit(uow, Entities.UserPermissionRow.Fields.GenerationKey);

            return new SaveResponse();
        }

        public RoleStepListResponse List(IDbConnection connection, RoleStepListRequest request)
        {
            Check.NotNull(request, "request");
            Check.NotNull(request.RoleID, "rolID");

            var response = new RoleStepListResponse();

            response.Entities = GetExisting(connection, request.RoleID.Value)
                .Select(x => x.StepId.Value).ToList();

            return response;
        }

        private List<MyRow> GetExisting(IDbConnection connection, Int32 roleID)
        {
            return connection.List<MyRow>(q =>
            {
                q.Select(fld.Id, fld.StepId)
                    .Where(new Criteria(fld.RoleId) == roleID);
            });
        }

        private void ProcessAttributes<TAttr>(HashSet<string> hash, MemberInfo member, Func<TAttr, string> getStep)
            where TAttr : Attribute
        {
            foreach (var attr in member.GetCustomAttributes<TAttr>())
            {
                var permission = getStep(attr);
                if (!permission.IsEmptyOrNull())
                    hash.Add(permission);
            }
        }
    }
}