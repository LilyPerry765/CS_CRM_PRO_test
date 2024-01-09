namespace CaseManagement.Case {
    export class PmoLevelForm extends Serenity.PrefixedContext {
        static formKey = 'Case.PmoLevel';

    }

    export interface PmoLevelForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(PmoLevelForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

