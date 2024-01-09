namespace CaseManagement.Case {
    export class ProvinceCompanySoftwareForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ProvinceCompanySoftware';

    }

    export interface ProvinceCompanySoftwareForm {
        ProvinveId: Serenity.IntegerEditor;
        CompanyId: Serenity.IntegerEditor;
        SoftwareId: Serenity.IntegerEditor;
        StatusId: Serenity.EnumEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }

    [['ProvinveId', () => Serenity.IntegerEditor], ['CompanyId', () => Serenity.IntegerEditor], ['SoftwareId', () => Serenity.IntegerEditor], ['StatusId', () => Serenity.EnumEditor], ['CreatedUserId', () => Serenity.IntegerEditor], ['CreatedDate', () => Serenity.DateEditor], ['ModifiedUserId', () => Serenity.IntegerEditor], ['ModifiedDate', () => Serenity.DateEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['DeletedUserId', () => Serenity.IntegerEditor], ['DeletedDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(ProvinceCompanySoftwareForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

