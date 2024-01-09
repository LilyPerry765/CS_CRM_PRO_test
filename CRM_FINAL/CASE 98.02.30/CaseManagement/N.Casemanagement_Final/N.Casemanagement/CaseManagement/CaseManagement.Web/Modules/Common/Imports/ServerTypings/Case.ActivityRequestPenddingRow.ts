﻿namespace CaseManagement.Case {
    export interface ActivityRequestPenddingRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }

    export namespace ActivityRequestPenddingRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Case.ActivityRequestPendding';
        export const lookupKey = 'Case.ActivityRequestPenddingRow';

        export function getLookup(): Q.Lookup<ActivityRequestPenddingRow> {
            return Q.getLookup<ActivityRequestPenddingRow>('Case.ActivityRequestPenddingRow');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Count: string;
            export declare const CycleCost: string;
            export declare const Factor: string;
            export declare const DelayedCost: string;
            export declare const YearCost: string;
            export declare const AccessibleCost: string;
            export declare const InaccessibleCost: string;
            export declare const Financial: string;
            export declare const TotalLeakage: string;
            export declare const RecoverableLeakage: string;
            export declare const Recovered: string;
            export declare const EventDescription: string;
            export declare const MainReason: string;
            export declare const CorrectionOperation: string;
            export declare const AvoidRepeatingOperation: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const SendDate: string;
            export declare const SendUserId: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
            export declare const EndDate: string;
            export declare const ActivityId: string;
            export declare const ProvinceId: string;
            export declare const CycleId: string;
            export declare const CustomerEffectId: string;
            export declare const IncomeFlowId: string;
            export declare const RiskLevelId: string;
            export declare const StatusID: string;
            export declare const LeakDate: string;
            export declare const DiscoverLeakDate: string;
            export declare const ActivityCode: string;
            export declare const ActivityName: string;
            export declare const ActivityObjective: string;
            export declare const ActivityGroupId: string;
            export declare const ProvinceName: string;
            export declare const CycleName: string;
            export declare const CustomerEffectName: string;
            export declare const IncomeFlowName: string;
            export declare const RiskLevelName: string;
            export declare const StatusName: string;
            export declare const CreatedUserName: string;
            export declare const ModifiedUserName: string;
            export declare const DeletedUserName: string;
            export declare const SendUserName: string;
            export declare const ActionID: string;
            export declare const ConfirmTypeID: string;
            export declare const IsRejected: string;
            export declare const CommentReasonList: string;
            export declare const CommnetList: string;
            export declare const File1: string;
            export declare const File2: string;
            export declare const File3: string;
            export declare const FinancialControllerConfirm: string;
            export declare const CycleCostHistory: string;
            export declare const DelayedCostHistory: string;
            export declare const YearCostHistory: string;
            export declare const AccessibleCostHistory: string;
            export declare const InaccessibleCostHistory: string;
            export declare const TotalLeakageHistory: string;
            export declare const RecoverableLeakageHistory: string;
            export declare const RecoveredHistory: string;
        }

        ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(x => (<any>Fields)[x] = x);
    }
}

