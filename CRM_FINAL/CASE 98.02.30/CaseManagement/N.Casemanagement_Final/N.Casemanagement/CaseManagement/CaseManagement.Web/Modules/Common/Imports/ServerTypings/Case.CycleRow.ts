namespace CaseManagement.Case {
    export interface CycleRow {
        Id?: number;
        YearId?: number;
        Cycle?: number;
        CycleName?: string;
        IsEnabled?: boolean;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        Year?: string;
    }

    export namespace CycleRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CycleName';
        export const localTextPrefix = 'Case.Cycle';
        export const lookupKey = 'Case.Cycle';

        export function getLookup(): Q.Lookup<CycleRow> {
            return Q.getLookup<CycleRow>('Case.Cycle');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const YearId: string;
            export declare const Cycle: string;
            export declare const CycleName: string;
            export declare const IsEnabled: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
            export declare const Year: string;
        }

        ['Id', 'YearId', 'Cycle', 'CycleName', 'IsEnabled', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'Year'].forEach(x => (<any>Fields)[x] = x);
    }
}

