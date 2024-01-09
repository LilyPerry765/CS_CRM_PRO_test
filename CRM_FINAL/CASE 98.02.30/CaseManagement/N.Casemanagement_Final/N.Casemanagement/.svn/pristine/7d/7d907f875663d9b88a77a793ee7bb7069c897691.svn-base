namespace CaseManagement.Case {
    export class CommentReasonForm extends Serenity.PrefixedContext {
        static formKey = 'Case.CommentReason';

    }

    export interface CommentReasonForm {
        Comment: Serenity.StringEditor;
    }

    [['Comment', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(CommentReasonForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

