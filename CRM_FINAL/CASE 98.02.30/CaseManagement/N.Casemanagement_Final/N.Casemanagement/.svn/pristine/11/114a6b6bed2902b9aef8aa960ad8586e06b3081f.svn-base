namespace CaseManagement.Case {
    export class SwitchForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Switch';

    }

    export interface SwitchForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(SwitchForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

