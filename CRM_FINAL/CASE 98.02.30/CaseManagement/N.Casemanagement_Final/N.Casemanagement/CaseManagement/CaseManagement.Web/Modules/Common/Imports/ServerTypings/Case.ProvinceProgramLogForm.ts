namespace CaseManagement.Case {
    export class ProvinceProgramLogForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ProvinceProgramLog';

    }

    export interface ProvinceProgramLogForm {
        ProvinceId: Serenity.IntegerEditor;
        YearId: Serenity.IntegerEditor;
        OldTotalLeakage: Serenity.StringEditor;
        NewTotalLeakage: Serenity.StringEditor;
        OldRecoverableLeakage: Serenity.StringEditor;
        NewRecoverableLeakage: Serenity.StringEditor;
        OldRecovered: Serenity.StringEditor;
        NewRecovered: Serenity.StringEditor;
        UserId: Serenity.IntegerEditor;
        InsertDate: Serenity.DateEditor;
    }

    [['ProvinceId', () => Serenity.IntegerEditor], ['YearId', () => Serenity.IntegerEditor], ['OldTotalLeakage', () => Serenity.StringEditor], ['NewTotalLeakage', () => Serenity.StringEditor], ['OldRecoverableLeakage', () => Serenity.StringEditor], ['NewRecoverableLeakage', () => Serenity.StringEditor], ['OldRecovered', () => Serenity.StringEditor], ['NewRecovered', () => Serenity.StringEditor], ['UserId', () => Serenity.IntegerEditor], ['InsertDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(ProvinceProgramLogForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

