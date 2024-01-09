namespace CaseManagement.Case {
    export interface ActivityRequestDetailsInfoRow {
        Id?: number;
        ProvinceId?: number;
        ActivityId?: number;
        CycleId?: number;
        IncomeFlowId?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        RejectCount?: number;
        EventDescription?: string;
        MainReason?: string;
        CycleName?: string;
        Name?: string;
        Expr1?: string;
        CodeName?: string;
        DiscoverLeakDate?: string;
    }

    export namespace ActivityRequestDetailsInfoRow {
        export const idProperty = 'Id';
        export const nameProperty = 'EventDescription';
        export const localTextPrefix = 'Case.ActivityRequestDetailsInfo';

        export namespace Fields {
            export declare const Id: string;
            export declare const ProvinceId: string;
            export declare const ActivityId: string;
            export declare const CycleId: string;
            export declare const IncomeFlowId: string;
            export declare const Count: string;
            export declare const CycleCost: string;
            export declare const Factor: string;
            export declare const DelayedCost: string;
            export declare const YearCost: string;
            export declare const AccessibleCost: string;
            export declare const InaccessibleCost: string;
            export declare const TotalLeakage: string;
            export declare const RecoverableLeakage: string;
            export declare const Recovered: string;
            export declare const DelayedCostHistory: string;
            export declare const YearCostHistory: string;
            export declare const AccessibleCostHistory: string;
            export declare const InaccessibleCostHistory: string;
            export declare const RejectCount: string;
            export declare const EventDescription: string;
            export declare const MainReason: string;
            export declare const CycleName: string;
            export declare const Name: string;
            export declare const Expr1: string;
            export declare const CodeName: string;
            export declare const DiscoverLeakDate: string;
        }

        ['Id', 'ProvinceId', 'ActivityId', 'CycleId', 'IncomeFlowId', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'RejectCount', 'EventDescription', 'MainReason', 'CycleName', 'Name', 'Expr1', 'CodeName', 'DiscoverLeakDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

