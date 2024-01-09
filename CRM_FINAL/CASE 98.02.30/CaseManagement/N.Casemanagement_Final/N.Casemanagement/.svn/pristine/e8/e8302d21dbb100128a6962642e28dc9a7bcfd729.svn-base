namespace CaseManagement.Case {
    export class YearForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Year';

    }

    export interface YearForm {
        Year: Serenity.IntegerEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }

    [['Year', () => Serenity.IntegerEditor], ['CreatedUserId', () => Serenity.IntegerEditor], ['CreatedDate', () => Serenity.DateEditor], ['ModifiedUserId', () => Serenity.IntegerEditor], ['ModifiedDate', () => Serenity.DateEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['DeletedUserId', () => Serenity.IntegerEditor], ['DeletedDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(YearForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

