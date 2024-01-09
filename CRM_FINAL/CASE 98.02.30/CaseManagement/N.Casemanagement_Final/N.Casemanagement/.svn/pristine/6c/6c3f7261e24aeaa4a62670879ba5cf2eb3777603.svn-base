namespace CaseManagement.Case {
    export interface ActivityCorrectionOperationRow {
        Id?: number;
        ActivityId?: number;
        Body?: string;
    }

    export namespace ActivityCorrectionOperationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Body';
        export const localTextPrefix = 'Case.ActivityCorrectionOperation';
        export const lookupKey = 'Case.ActivityCorrectionOperation';

        export function getLookup(): Q.Lookup<ActivityCorrectionOperationRow> {
            return Q.getLookup<ActivityCorrectionOperationRow>('Case.ActivityCorrectionOperation');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const ActivityId: string;
            export declare const Body: string;
        }

        ['Id', 'ActivityId', 'Body'].forEach(x => (<any>Fields)[x] = x);
    }
}

