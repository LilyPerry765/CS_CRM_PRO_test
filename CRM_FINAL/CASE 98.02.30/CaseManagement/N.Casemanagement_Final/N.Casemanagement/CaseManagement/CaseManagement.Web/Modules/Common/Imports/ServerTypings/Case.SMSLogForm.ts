namespace CaseManagement.Case {
    export class SMSLogForm extends Serenity.PrefixedContext {
        static formKey = 'Case.SMSLog';

    }

    export interface SMSLogForm {
        ActivityRequestId: Serenity.StringEditor;
        ReceiverProvinceId: Serenity.LookupEditor;
        ReceiverUserName: Serenity.StringEditor;
        TextSent: Serenity.StringEditor;
    }

    [['ActivityRequestId', () => Serenity.StringEditor], ['ReceiverProvinceId', () => Serenity.LookupEditor], ['ReceiverUserName', () => Serenity.StringEditor], ['TextSent', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(SMSLogForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

