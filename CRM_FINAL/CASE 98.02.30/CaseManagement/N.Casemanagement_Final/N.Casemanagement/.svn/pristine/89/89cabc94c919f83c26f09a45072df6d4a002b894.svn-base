namespace CaseManagement.Case {
    export interface ActivityRequestCommentRow {
        Id?: number;
        Comment?: string;
        ActivityRequestId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        CreatedUserName?: string;
    }

    export namespace ActivityRequestCommentRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Comment';
        export const localTextPrefix = 'Case.ActivityRequestComment';
        export const lookupKey = 'Case.ActivityRequestComment';

        export function getLookup(): Q.Lookup<ActivityRequestCommentRow> {
            return Q.getLookup<ActivityRequestCommentRow>('Case.ActivityRequestComment');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Comment: string;
            export declare const ActivityRequestId: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const CreatedUserName: string;
        }

        ['Id', 'Comment', 'ActivityRequestId', 'CreatedUserId', 'CreatedDate', 'CreatedUserName'].forEach(x => (<any>Fields)[x] = x);
    }
}

