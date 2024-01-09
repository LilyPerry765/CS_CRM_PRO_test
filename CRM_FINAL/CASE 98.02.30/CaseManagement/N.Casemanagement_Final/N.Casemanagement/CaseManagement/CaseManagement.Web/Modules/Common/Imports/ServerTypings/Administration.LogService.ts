namespace CaseManagement.Administration {
    export namespace LogService {
        export const baseUrl = 'Administration/Log';

        export declare function Create(request: Serenity.SaveRequest<LogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<LogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<LogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<LogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function LogProvinceReport(request: LogRequest, onSuccess?: (response: LogResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function LogLeaderReport(request: LogRequest, onSuccess?: (response: LogResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export namespace Methods {
            export declare const Create: string;
            export declare const Update: string;
            export declare const Delete: string;
            export declare const Retrieve: string;
            export declare const List: string;
            export declare const LogProvinceReport: string;
            export declare const LogLeaderReport: string;
        }

        ['Create', 'Update', 'Delete', 'Retrieve', 'List', 'LogProvinceReport', 'LogLeaderReport'].forEach(x => {
            (<any>LogService)[x] = function (r, s, o) { return Q.serviceRequest(baseUrl + '/' + x, r, s, o); };
            (<any>Methods)[x] = baseUrl + '/' + x;
        });
    }
}

