namespace CaseManagement.WorkFlow {
    export interface WorkFlowActionRow {
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

    export namespace WorkFlowActionRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'WorkFlow.WorkFlowAction';
        export const lookupKey = 'WorkFlow.WorkFlowAction';

        export function getLookup(): Q.Lookup<WorkFlowActionRow> {
            return Q.getLookup<WorkFlowActionRow>('WorkFlow.WorkFlowAction');
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

