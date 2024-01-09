namespace CaseManagement.Case {
    export class ActivityRequestLogForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestLog';

    }

    export interface ActivityRequestLogForm {
        StatusId: Serenity.IntegerEditor;
    }

    [['StatusId', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(ActivityRequestLogForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

