namespace CaseManagement.Case {
    export class ActivityGroupForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityGroup';

    }

    export interface ActivityGroupForm {
        Name: Serenity.StringEditor;
        EnglishName: Serenity.StringEditor;
        Code: Serenity.IntegerEditor;
    }

    [['Name', () => Serenity.StringEditor], ['EnglishName', () => Serenity.StringEditor], ['Code', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(ActivityGroupForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

