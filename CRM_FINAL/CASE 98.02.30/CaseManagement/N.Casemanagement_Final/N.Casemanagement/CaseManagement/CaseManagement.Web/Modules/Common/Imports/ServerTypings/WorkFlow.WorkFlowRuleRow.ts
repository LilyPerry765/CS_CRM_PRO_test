namespace CaseManagement.WorkFlow {
    export interface WorkFlowRuleRow {
        Id?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ActionId?: number;
        CurrentStatusId?: number;
        NextStatusId?: number;
        Version?: number;
        ActionName?: string;
        CurrentStatusName?: string;
        NextStatusName?: string;
    }

    export namespace WorkFlowRuleRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'WorkFlow.WorkFlowRule';

        export namespace Fields {
            export declare const Id: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
            export declare const ActionId: string;
            export declare const CurrentStatusId: string;
            export declare const NextStatusId: string;
            export declare const Version: string;
            export declare const ActionName: string;
            export declare const CurrentStatusName: string;
            export declare const NextStatusName: string;
        }

        ['Id', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ActionId', 'CurrentStatusId', 'NextStatusId', 'Version', 'ActionName', 'CurrentStatusName', 'NextStatusName'].forEach(x => (<any>Fields)[x] = x);
    }
}

