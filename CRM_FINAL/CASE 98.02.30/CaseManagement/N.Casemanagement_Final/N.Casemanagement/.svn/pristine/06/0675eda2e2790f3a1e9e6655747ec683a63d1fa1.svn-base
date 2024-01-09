namespace CaseManagement.Case {
    export class ActivityRequestCommentReasonForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestCommentReason';

    }

    export interface ActivityRequestCommentReasonForm {
        CommentReasonId: Serenity.IntegerEditor;
        ActivityRequestId: Serenity.IntegerEditor;
    }

    [['CommentReasonId', () => Serenity.IntegerEditor], ['ActivityRequestId', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(ActivityRequestCommentReasonForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

