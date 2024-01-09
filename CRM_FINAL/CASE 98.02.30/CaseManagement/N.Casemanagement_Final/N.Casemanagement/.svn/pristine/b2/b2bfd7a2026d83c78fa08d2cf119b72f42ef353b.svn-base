namespace CaseManagement.Case {
    export class IncomeFlowForm extends Serenity.PrefixedContext {
        static formKey = 'Case.IncomeFlow';

    }

    export interface IncomeFlowForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(IncomeFlowForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

