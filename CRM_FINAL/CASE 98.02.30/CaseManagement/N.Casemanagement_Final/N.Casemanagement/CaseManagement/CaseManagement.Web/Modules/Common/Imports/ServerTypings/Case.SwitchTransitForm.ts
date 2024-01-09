namespace CaseManagement.Case {
    export class SwitchTransitForm extends Serenity.PrefixedContext {
        static formKey = 'Case.SwitchTransit';

    }

    export interface SwitchTransitForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(SwitchTransitForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

