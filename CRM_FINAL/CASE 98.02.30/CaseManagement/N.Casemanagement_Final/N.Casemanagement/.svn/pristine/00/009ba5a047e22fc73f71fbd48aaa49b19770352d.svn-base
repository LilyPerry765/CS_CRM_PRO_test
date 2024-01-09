namespace CaseManagement.Case {
    export class ProvinceProgramForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ProvinceProgram';

    }

    export interface ProvinceProgramForm {
        Program: Serenity.StringEditor;
        YearId: Serenity.LookupEditor;
    }

    [['Program', () => Serenity.StringEditor], ['YearId', () => Serenity.LookupEditor]].forEach(x => Object.defineProperty(ProvinceProgramForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

