namespace CaseManagement.Administration {
    export class NotificationForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.Notification';

    }

    export interface NotificationForm {
        GroupId: Serenity.IntegerEditor;
        UserId: Serenity.IntegerEditor;
        Message: Serenity.StringEditor;
    }

    [['GroupId', () => Serenity.IntegerEditor], ['UserId', () => Serenity.IntegerEditor], ['Message', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(NotificationForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

