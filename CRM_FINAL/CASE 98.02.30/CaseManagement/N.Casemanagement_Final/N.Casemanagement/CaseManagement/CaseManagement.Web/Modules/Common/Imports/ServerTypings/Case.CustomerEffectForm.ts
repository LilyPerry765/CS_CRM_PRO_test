namespace CaseManagement.Case {
    export class CustomerEffectForm extends Serenity.PrefixedContext {
        static formKey = 'Case.CustomerEffect';

    }

    export interface CustomerEffectForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(CustomerEffectForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

