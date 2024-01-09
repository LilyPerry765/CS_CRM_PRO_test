namespace CaseManagement.Administration {
    export class UserForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.User';

    }

    export interface UserForm {
        Username: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        EmployeeID: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Rank: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
        PasswordConfirm: Serenity.PasswordEditor;
        TelephoneNo1: Serenity.StringEditor;
        MobileNo: Serenity.StringEditor;
        Degree: Serenity.StringEditor;
        ProvinceId: Serenity.LookupEditor;
        ProvinceList: Serenity.LookupEditor;
        IsActive: Serenity.BooleanEditor;
        IsDeleted: Serenity.BooleanEditor;
        ImagePath: Serenity.ImageUploadEditor;
    }

    [['Username', () => Serenity.StringEditor], ['DisplayName', () => Serenity.StringEditor], ['EmployeeID', () => Serenity.StringEditor], ['Email', () => Serenity.StringEditor], ['Rank', () => Serenity.StringEditor], ['Password', () => Serenity.PasswordEditor], ['PasswordConfirm', () => Serenity.PasswordEditor], ['TelephoneNo1', () => Serenity.StringEditor], ['MobileNo', () => Serenity.StringEditor], ['Degree', () => Serenity.StringEditor], ['ProvinceId', () => Serenity.LookupEditor], ['ProvinceList', () => Serenity.LookupEditor], ['IsActive', () => Serenity.BooleanEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['ImagePath', () => Serenity.ImageUploadEditor]].forEach(x => Object.defineProperty(UserForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

