namespace CaseManagement.StimulReport {
    export interface ActivityRequestDetailRow {
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
    }

    export namespace ActivityRequestDetailRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'StimulReport.ActivityRequestDetail';

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
        }

        ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName'].forEach(x => (<any>Fields)[x] = x);
    }
}

