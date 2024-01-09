namespace CaseManagement.Case {
    export class ActivityMainReasonForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityMainReason';

    }

    export interface ActivityMainReasonForm {
        Body: Serenity.TextAreaEditor;
    }

    [['Body', () => Serenity.TextAreaEditor]].forEach(x => Object.defineProperty(ActivityMainReasonForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

