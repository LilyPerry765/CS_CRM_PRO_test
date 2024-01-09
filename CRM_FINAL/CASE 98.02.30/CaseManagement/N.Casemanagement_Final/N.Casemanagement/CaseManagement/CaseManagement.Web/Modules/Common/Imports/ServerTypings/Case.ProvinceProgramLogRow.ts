namespace CaseManagement.Case {
    export interface ProvinceProgramLogRow {
        Id?: number;
        ActivityRequestID?: number;
        ProvinceId?: number;
        YearId?: number;
        OldTotalLeakage?: number;
        NewTotalLeakage?: number;
        OldRecoverableLeakage?: number;
        NewRecoverableLeakage?: number;
        OldRecovered?: number;
        NewRecovered?: number;
        UserId?: number;
        InsertDate?: string;
    }

    export namespace ProvinceProgramLogRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Case.ProvinceProgramLog';

        export namespace Fields {
            export declare const Id: string;
            export declare const ActivityRequestID: string;
            export declare const ProvinceId: string;
            export declare const YearId: string;
            export declare const OldTotalLeakage: string;
            export declare const NewTotalLeakage: string;
            export declare const OldRecoverableLeakage: string;
            export declare const NewRecoverableLeakage: string;
            export declare const OldRecovered: string;
            export declare const NewRecovered: string;
            export declare const UserId: string;
            export declare const InsertDate: string;
        }

        ['Id', 'ActivityRequestID', 'ProvinceId', 'YearId', 'OldTotalLeakage', 'NewTotalLeakage', 'OldRecoverableLeakage', 'NewRecoverableLeakage', 'OldRecovered', 'NewRecovered', 'UserId', 'InsertDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

