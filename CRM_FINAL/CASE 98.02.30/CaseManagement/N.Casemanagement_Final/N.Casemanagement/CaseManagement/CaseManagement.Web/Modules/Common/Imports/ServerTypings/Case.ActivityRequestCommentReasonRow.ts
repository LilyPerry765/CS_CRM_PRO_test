namespace CaseManagement.Case {
    export interface ActivityRequestCommentReasonRow {
        Id?: number;
        CommentReasonId?: number;
        ActivityRequestId?: number;
        CommentReasonComment?: string;
        CommentReasonCreatedUserId?: number;
        CommentReasonCreatedDate?: string;
        ActivityRequestOldCaseId?: number;
        ActivityRequestProvinceId?: number;
        ActivityRequestActivityId?: number;
        ActivityRequestCycleId?: number;
        ActivityRequestCustomerEffectId?: number;
        ActivityRequestRiskLevelId?: number;
        ActivityRequestIncomeFlowId?: number;
        ActivityRequestCount?: number;
        ActivityRequestCycleCost?: number;
        ActivityRequestFactor?: number;
        ActivityRequestDelayedCost?: number;
        ActivityRequestYearCost?: number;
        ActivityRequestAccessibleCost?: number;
        ActivityRequestInaccessibleCost?: number;
        ActivityRequestFinancial?: number;
        ActivityRequestLeakDate?: string;
        ActivityRequestDiscoverLeakDate?: string;
        ActivityRequestDiscoverLeakDateShamsi?: string;
        ActivityRequestEventDescription?: string;
        ActivityRequestMainReason?: string;
        ActivityRequestCorrectionOperation?: string;
        ActivityRequestAvoidRepeatingOperation?: string;
        ActivityRequestCreatedUserId?: number;
        ActivityRequestCreatedDate?: string;
        ActivityRequestCreatedDateShamsi?: string;
        ActivityRequestModifiedUserId?: number;
        ActivityRequestModifiedDate?: string;
        ActivityRequestIsDeleted?: boolean;
        ActivityRequestDeletedUserId?: number;
        ActivityRequestDeletedDate?: string;
        ActivityRequestEndDate?: string;
        ActivityRequestStatusId?: number;
        ActivityRequestActionId?: number;
    }

    export namespace ActivityRequestCommentReasonRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Case.ActivityRequestCommentReason';

        export namespace Fields {
            export declare const Id: string;
            export declare const CommentReasonId: string;
            export declare const ActivityRequestId: string;
            export declare const CommentReasonComment: string;
            export declare const CommentReasonCreatedUserId: string;
            export declare const CommentReasonCreatedDate: string;
            export declare const ActivityRequestOldCaseId: string;
            export declare const ActivityRequestProvinceId: string;
            export declare const ActivityRequestActivityId: string;
            export declare const ActivityRequestCycleId: string;
            export declare const ActivityRequestCustomerEffectId: string;
            export declare const ActivityRequestRiskLevelId: string;
            export declare const ActivityRequestIncomeFlowId: string;
            export declare const ActivityRequestCount: string;
            export declare const ActivityRequestCycleCost: string;
            export declare const ActivityRequestFactor: string;
            export declare const ActivityRequestDelayedCost: string;
            export declare const ActivityRequestYearCost: string;
            export declare const ActivityRequestAccessibleCost: string;
            export declare const ActivityRequestInaccessibleCost: string;
            export declare const ActivityRequestFinancial: string;
            export declare const ActivityRequestLeakDate: string;
            export declare const ActivityRequestDiscoverLeakDate: string;
            export declare const ActivityRequestDiscoverLeakDateShamsi: string;
            export declare const ActivityRequestEventDescription: string;
            export declare const ActivityRequestMainReason: string;
            export declare const ActivityRequestCorrectionOperation: string;
            export declare const ActivityRequestAvoidRepeatingOperation: string;
            export declare const ActivityRequestCreatedUserId: string;
            export declare const ActivityRequestCreatedDate: string;
            export declare const ActivityRequestCreatedDateShamsi: string;
            export declare const ActivityRequestModifiedUserId: string;
            export declare const ActivityRequestModifiedDate: string;
            export declare const ActivityRequestIsDeleted: string;
            export declare const ActivityRequestDeletedUserId: string;
            export declare const ActivityRequestDeletedDate: string;
            export declare const ActivityRequestEndDate: string;
            export declare const ActivityRequestStatusId: string;
            export declare const ActivityRequestActionId: string;
        }

        ['Id', 'CommentReasonId', 'ActivityRequestId', 'CommentReasonComment', 'CommentReasonCreatedUserId', 'CommentReasonCreatedDate', 'ActivityRequestOldCaseId', 'ActivityRequestProvinceId', 'ActivityRequestActivityId', 'ActivityRequestCycleId', 'ActivityRequestCustomerEffectId', 'ActivityRequestRiskLevelId', 'ActivityRequestIncomeFlowId', 'ActivityRequestCount', 'ActivityRequestCycleCost', 'ActivityRequestFactor', 'ActivityRequestDelayedCost', 'ActivityRequestYearCost', 'ActivityRequestAccessibleCost', 'ActivityRequestInaccessibleCost', 'ActivityRequestFinancial', 'ActivityRequestLeakDate', 'ActivityRequestDiscoverLeakDate', 'ActivityRequestDiscoverLeakDateShamsi', 'ActivityRequestEventDescription', 'ActivityRequestMainReason', 'ActivityRequestCorrectionOperation', 'ActivityRequestAvoidRepeatingOperation', 'ActivityRequestCreatedUserId', 'ActivityRequestCreatedDate', 'ActivityRequestCreatedDateShamsi', 'ActivityRequestModifiedUserId', 'ActivityRequestModifiedDate', 'ActivityRequestIsDeleted', 'ActivityRequestDeletedUserId', 'ActivityRequestDeletedDate', 'ActivityRequestEndDate', 'ActivityRequestStatusId', 'ActivityRequestActionId'].forEach(x => (<any>Fields)[x] = x);
    }
}

