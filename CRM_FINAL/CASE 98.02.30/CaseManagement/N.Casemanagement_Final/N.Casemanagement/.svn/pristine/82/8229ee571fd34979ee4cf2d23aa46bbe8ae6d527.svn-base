namespace CaseManagement.Case {
    export interface IncomeFlowRow {
        Id?: number;
        Name?: string;
        Code?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }

    export namespace IncomeFlowRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Case.IncomeFlow';
        export const lookupKey = 'Case.IncomeFlow';

        export function getLookup(): Q.Lookup<IncomeFlowRow> {
            return Q.getLookup<IncomeFlowRow>('Case.IncomeFlow');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Name: string;
            export declare const Code: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
        }

        ['Id', 'Name', 'Code', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

