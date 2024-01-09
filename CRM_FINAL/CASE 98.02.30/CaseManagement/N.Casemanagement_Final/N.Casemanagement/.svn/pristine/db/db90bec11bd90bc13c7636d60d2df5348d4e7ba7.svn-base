namespace CaseManagement.Case {
    export interface YearRow {
        Id?: number;
        Year?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }

    export namespace YearRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Year';
        export const localTextPrefix = 'Case.Year';
        export const lookupKey = 'Case.Year';

        export function getLookup(): Q.Lookup<YearRow> {
            return Q.getLookup<YearRow>('Case.Year');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Year: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
        }

        ['Id', 'Year', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

