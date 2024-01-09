namespace CaseManagement.Case {
    export interface ActivityMainReasonRow {
        Id?: number;
        ActivityId?: number;
        Body?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }

    export namespace ActivityMainReasonRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Body';
        export const localTextPrefix = 'Case.ActivityMainReason';
        export const lookupKey = 'Case.ActivityMainReason';

        export function getLookup(): Q.Lookup<ActivityMainReasonRow> {
            return Q.getLookup<ActivityMainReasonRow>('Case.ActivityMainReason');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const ActivityId: string;
            export declare const Body: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
        }

        ['Id', 'ActivityId', 'Body', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

