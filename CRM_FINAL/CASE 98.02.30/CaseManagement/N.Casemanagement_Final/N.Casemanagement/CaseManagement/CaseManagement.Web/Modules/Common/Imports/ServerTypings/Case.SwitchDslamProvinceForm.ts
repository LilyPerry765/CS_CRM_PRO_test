namespace CaseManagement.Case {
    export class SwitchDslamProvinceForm extends Serenity.PrefixedContext {
        static formKey = 'Case.SwitchDslamProvince';

    }

    export interface SwitchDslamProvinceForm {
        ProvinceId: Serenity.IntegerEditor;
        SwitchDslamid: Serenity.IntegerEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }

    [['ProvinceId', () => Serenity.IntegerEditor], ['SwitchDslamid', () => Serenity.IntegerEditor], ['CreatedUserId', () => Serenity.IntegerEditor], ['CreatedDate', () => Serenity.DateEditor], ['ModifiedUserId', () => Serenity.IntegerEditor], ['ModifiedDate', () => Serenity.DateEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['DeletedUserId', () => Serenity.IntegerEditor], ['DeletedDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(SwitchDslamProvinceForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

