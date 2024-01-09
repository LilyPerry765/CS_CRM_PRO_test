namespace CaseManagement.WorkFlow {
    export interface WorkFlowStatusRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        StepId?: number;
        StatusTypeId?: number;
        StepName?: string;
        StatusTypeName?: string;
        FullName?: string;
    }

    export namespace WorkFlowStatusRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FullName';
        export const localTextPrefix = 'WorkFlow.WorkFlowStatus';
        export const lookupKey = 'WorkFlow.WorkFlowStatus';

        export function getLookup(): Q.Lookup<WorkFlowStatusRow> {
            return Q.getLookup<WorkFlowStatusRow>('WorkFlow.WorkFlowStatus');
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
            export declare const StepId: string;
            export declare const StatusTypeId: string;
            export declare const StepName: string;
            export declare const StatusTypeName: string;
            export declare const FullName: string;
        }

        ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'StepId', 'StatusTypeId', 'StepName', 'StatusTypeName', 'FullName'].forEach(x => (<any>Fields)[x] = x);
    }
}

