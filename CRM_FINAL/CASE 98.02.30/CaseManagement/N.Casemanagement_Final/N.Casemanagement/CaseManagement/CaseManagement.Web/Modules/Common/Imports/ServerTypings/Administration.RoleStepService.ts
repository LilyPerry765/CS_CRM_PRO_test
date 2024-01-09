namespace CaseManagement.Administration {
    export namespace RoleStepService {
        export const baseUrl = 'Administration/RoleStep';

        export declare function Update(request: RoleStepUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: RoleStepListRequest, onSuccess?: (response: RoleStepListResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export namespace Methods {
            export declare const Update: string;
            export declare const List: string;
        }

        ['Update', 'List'].forEach(x => {
            (<any>RoleStepService)[x] = function (r, s, o) { return Q.serviceRequest(baseUrl + '/' + x, r, s, o); };
            (<any>Methods)[x] = baseUrl + '/' + x;
        });
    }
}

