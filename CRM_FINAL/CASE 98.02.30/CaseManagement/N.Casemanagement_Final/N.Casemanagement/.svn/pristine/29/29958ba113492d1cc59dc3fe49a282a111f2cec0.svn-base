namespace CaseManagement.Case {
    export class CycleForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Cycle';

    }

    export interface CycleForm {
        YearId: Serenity.LookupEditor;
        Cycle: Serenity.IntegerEditor;
        IsEnabled: Serenity.BooleanEditor;
    }

    [['YearId', () => Serenity.LookupEditor], ['Cycle', () => Serenity.IntegerEditor], ['IsEnabled', () => Serenity.BooleanEditor]].forEach(x => Object.defineProperty(CycleForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

