namespace CaseManagement.Case {
    export interface CommentReasonRow {
        Id?: number;
        Comment?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
    }

    export namespace CommentReasonRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Comment';
        export const localTextPrefix = 'Case.CommentReason';
        export const lookupKey = 'Case.CommentReason';

        export function getLookup(): Q.Lookup<CommentReasonRow> {
            return Q.getLookup<CommentReasonRow>('Case.CommentReason');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Comment: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
        }

        ['Id', 'Comment', 'CreatedUserId', 'CreatedDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

