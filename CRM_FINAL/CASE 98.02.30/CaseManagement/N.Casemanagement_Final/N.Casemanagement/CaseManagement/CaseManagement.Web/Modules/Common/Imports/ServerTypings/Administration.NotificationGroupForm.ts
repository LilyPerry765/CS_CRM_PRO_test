namespace CaseManagement.Administration {
    export class NotificationGroupForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.NotificationGroup';

    }

    export interface NotificationGroupForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(NotificationGroupForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

