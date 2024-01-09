namespace CaseManagement.Case {
    export class RepeatTermForm extends Serenity.PrefixedContext {
        static formKey = 'Case.RepeatTerm';

    }

    export interface RepeatTermForm {
        Name: Serenity.StringEditor;
        RequiredYearRepeatCount: Serenity.IntegerEditor;
    }

    [['Name', () => Serenity.StringEditor], ['RequiredYearRepeatCount', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(RepeatTermForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

