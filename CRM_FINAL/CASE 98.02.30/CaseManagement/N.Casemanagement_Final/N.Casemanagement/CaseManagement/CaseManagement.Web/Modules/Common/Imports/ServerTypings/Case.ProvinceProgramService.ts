﻿namespace CaseManagement.Case {
    export namespace ProvinceProgramService {
        export const baseUrl = 'Case/ProvinceProgram';

        export declare function Create(request: Serenity.SaveRequest<ProvinceProgramRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ProvinceProgramRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ProvinceProgramRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ProvinceProgramRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ProvinceProgramLineReport96(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ProvinceProgramLineReport(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ProvinceProgramLineReport94(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ProvinceProgramLineReport93(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ProvinceProgramLineReport92(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function LeakProgramReport95(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ConfirmProgramReport95(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function LeakConfirmReport95(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export namespace Methods {
            export declare const Create: string;
            export declare const Update: string;
            export declare const Delete: string;
            export declare const Retrieve: string;
            export declare const List: string;
            export declare const ProvinceProgramLineReport96: string;
            export declare const ProvinceProgramLineReport: string;
            export declare const ProvinceProgramLineReport94: string;
            export declare const ProvinceProgramLineReport93: string;
            export declare const ProvinceProgramLineReport92: string;
            export declare const LeakProgramReport95: string;
            export declare const ConfirmProgramReport95: string;
            export declare const LeakConfirmReport95: string;
        }

        ['Create', 'Update', 'Delete', 'Retrieve', 'List', 'ProvinceProgramLineReport96', 'ProvinceProgramLineReport', 'ProvinceProgramLineReport94', 'ProvinceProgramLineReport93', 'ProvinceProgramLineReport92', 'LeakProgramReport95', 'ConfirmProgramReport95', 'LeakConfirmReport95'].forEach(x => {
            (<any>ProvinceProgramService)[x] = function (r, s, o) { return Q.serviceRequest(baseUrl + '/' + x, r, s, o); };
            (<any>Methods)[x] = baseUrl + '/' + x;
        });
    }
}
