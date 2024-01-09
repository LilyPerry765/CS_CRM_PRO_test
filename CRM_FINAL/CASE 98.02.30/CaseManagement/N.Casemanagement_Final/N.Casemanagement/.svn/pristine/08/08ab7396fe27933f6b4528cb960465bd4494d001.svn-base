namespace CaseManagement.Case {
    export class SwitchDslamForm extends Serenity.PrefixedContext {
        static formKey = 'Case.SwitchDslam';

    }

    export interface SwitchDslamForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(SwitchDslamForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

