namespace CaseManagement.Case {
    export class ActivityCorrectionOperationForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityCorrectionOperation';

    }

    export interface ActivityCorrectionOperationForm {
        Body: Serenity.TextAreaEditor;
    }

    [['Body', () => Serenity.TextAreaEditor]].forEach(x => Object.defineProperty(ActivityCorrectionOperationForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

