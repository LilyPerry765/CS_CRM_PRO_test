namespace CaseManagement.Case {
    export class RiskLevelForm extends Serenity.PrefixedContext {
        static formKey = 'Case.RiskLevel';

    }

    export interface RiskLevelForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(RiskLevelForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

