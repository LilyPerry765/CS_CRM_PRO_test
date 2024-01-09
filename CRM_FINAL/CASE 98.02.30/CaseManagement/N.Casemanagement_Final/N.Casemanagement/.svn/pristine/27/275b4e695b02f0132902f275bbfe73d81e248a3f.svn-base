namespace CaseManagement.Case {
    export interface RepeatTermRow {
        Id?: number;
        Name?: string;
        DayValue?: number;
        RequiredYearRepeatCount?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }

    export namespace RepeatTermRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Case.RepeatTerm';
        export const lookupKey = 'Case.RepeatTerm';

        export function getLookup(): Q.Lookup<RepeatTermRow> {
            return Q.getLookup<RepeatTermRow>('Case.RepeatTerm');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Name: string;
            export declare const DayValue: string;
            export declare const RequiredYearRepeatCount: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
        }

        ['Id', 'Name', 'DayValue', 'RequiredYearRepeatCount', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

