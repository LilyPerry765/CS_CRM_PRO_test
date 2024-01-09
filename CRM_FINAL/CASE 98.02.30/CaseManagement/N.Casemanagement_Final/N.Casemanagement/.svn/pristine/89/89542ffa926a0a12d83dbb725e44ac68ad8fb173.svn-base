
namespace CaseManagement.Administration.Endpoints
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System.Data;
    using System.Web.Mvc;
    using MyRepository = Repositories.RoleStepRepository;
    using MyRow = Entities.RoleStepRow;

    [RoutePrefix("Services/Administration/RoleStep"), Route("{action}")]
    [ConnectionKey("Default"), ServiceAuthorize(Administration.PermissionKeys.Permission)]
    public class RoleStepController : ServiceEndpoint
    {
        [HttpPost]
        public SaveResponse Update(IUnitOfWork uow, RoleStepUpdateRequest request)
        {
            return new MyRepository().Update(uow, request);
        }

        public RoleStepListResponse List(IDbConnection connection, RoleStepListRequest request)
        {
            return new MyRepository().List(connection, request);
        }
    }
}
