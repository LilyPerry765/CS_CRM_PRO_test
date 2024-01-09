namespace CaseManagement.Case {
    export class SoftwareForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Software';

    }

    export interface SoftwareForm {
        Name: Serenity.StringEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }

    [['Name', () => Serenity.StringEditor], ['CreatedUserId', () => Serenity.IntegerEditor], ['CreatedDate', () => Serenity.DateEditor], ['ModifiedUserId', () => Serenity.IntegerEditor], ['ModifiedDate', () => Serenity.DateEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['DeletedUserId', () => Serenity.IntegerEditor], ['DeletedDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(SoftwareForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

