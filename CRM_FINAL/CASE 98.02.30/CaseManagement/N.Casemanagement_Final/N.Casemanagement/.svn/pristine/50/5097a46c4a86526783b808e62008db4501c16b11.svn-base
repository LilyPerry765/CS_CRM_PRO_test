namespace CaseManagement.Case {
    export interface SwitchTransitRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }

    export namespace SwitchTransitRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Case.SwitchTransit';
        export const lookupKey = 'Case.SwitchTransit';

        export function getLookup(): Q.Lookup<SwitchTransitRow> {
            return Q.getLookup<SwitchTransitRow>('Case.SwitchTransit');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Name: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
        }

        ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

