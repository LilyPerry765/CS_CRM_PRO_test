namespace CaseManagement.Case {
    export interface ActivityRequestHistoryRow {
        Id?: number;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }

    export namespace ActivityRequestHistoryRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Case.ActivityRequestHistory';
        export const lookupKey = 'Case.ActivityRequestHistory';

        export function getLookup(): Q.Lookup<ActivityRequestHistoryRow> {
            return Q.getLookup<ActivityRequestHistoryRow>('Case.ActivityRequestHistory');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const CycleCostHistory: string;
            export declare const DelayedCostHistory: string;
            export declare const YearCostHistory: string;
            export declare const AccessibleCostHistory: string;
            export declare const InaccessibleCostHistory: string;
            export declare const TotalLeakageHistory: string;
            export declare const RecoverableLeakageHistory: string;
            export declare const RecoveredHistory: string;
        }

        ['Id', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(x => (<any>Fields)[x] = x);
    }
}

