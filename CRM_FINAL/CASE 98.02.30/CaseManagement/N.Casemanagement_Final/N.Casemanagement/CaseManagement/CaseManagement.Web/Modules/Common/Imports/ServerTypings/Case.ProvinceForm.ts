namespace CaseManagement.Case {
    export class ProvinceForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Province';

    }

    export interface ProvinceForm {
        Name: Serenity.TextAreaEditor;
        ManagerName: Serenity.StringEditor;
        Code: Serenity.IntegerEditor;
    }

    [['Name', () => Serenity.TextAreaEditor], ['ManagerName', () => Serenity.StringEditor], ['Code', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(ProvinceForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

