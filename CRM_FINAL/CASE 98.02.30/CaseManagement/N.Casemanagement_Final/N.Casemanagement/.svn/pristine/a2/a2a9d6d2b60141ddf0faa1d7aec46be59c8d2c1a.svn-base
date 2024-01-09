namespace CaseManagement.Administration {
    export class UserSupportGroupForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.UserSupportGroup';

    }

    export interface UserSupportGroupForm {
        UserId: Serenity.IntegerEditor;
        GroupId: Serenity.IntegerEditor;
    }

    [['UserId', () => Serenity.IntegerEditor], ['GroupId', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(UserSupportGroupForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

