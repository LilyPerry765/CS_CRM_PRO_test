namespace CaseManagement.Case {
    export class ActivityRequestCommentForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestComment';

    }

    export interface ActivityRequestCommentForm {
        Comment: Serenity.TextAreaEditor;
    }

    [['Comment', () => Serenity.TextAreaEditor]].forEach(x => Object.defineProperty(ActivityRequestCommentForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

