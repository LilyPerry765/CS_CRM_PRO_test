namespace CaseManagement.Administration {
    export class LogForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.Log';

    }

    export interface LogForm {
        TableName: Serenity.StringEditor;
        PersianTableName: Serenity.StringEditor;
        RecordId: Serenity.StringEditor;
        RecordName: Serenity.IntegerEditor;
        ActionId: Serenity.EnumEditor;
        UserId: Serenity.LookupEditor;
        InsertDate: Serenity.DateEditor;
    }

    [['TableName', () => Serenity.StringEditor], ['PersianTableName', () => Serenity.StringEditor], ['RecordId', () => Serenity.StringEditor], ['RecordName', () => Serenity.IntegerEditor], ['ActionId', () => Serenity.EnumEditor], ['UserId', () => Serenity.LookupEditor], ['InsertDate', () => Serenity.DateEditor]].forEach(x => Object.defineProperty(LogForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

