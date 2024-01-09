namespace CaseManagement.Case {
    export class CompanyForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Company';

    }

    export interface CompanyForm {
        Name: Serenity.StringEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }

    [['Name', () => Serenity.StringEditor], ['CreatedUserId', () => Serenity.IntegerEditor], ['CreatedDate', () => Serenity.DateEditor], ['ModifiedUserId', () => Serenity.IntegerEditor], ['ModifiedDate', () => Serenity.DateEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['DeletedUserId', () => Serenity.IntegerEditor], ['DeletedDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(CompanyForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

