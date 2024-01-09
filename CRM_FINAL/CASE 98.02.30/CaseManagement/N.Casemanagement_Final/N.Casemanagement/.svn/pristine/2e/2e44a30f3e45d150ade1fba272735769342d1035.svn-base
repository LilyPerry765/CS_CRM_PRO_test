namespace CaseManagement.Messaging {
    export class SentForm extends Serenity.PrefixedContext {
        static formKey = 'Messaging.Sent';

    }

    export interface SentForm {
        RecieverDisplayName: Serenity.StringEditor;
        MessageSubject: Serenity.TextAreaEditor;
        MessageBody: Serenity.TextAreaEditor;
        MessageFile: Serenity.ImageUploadEditor;
    }

    [['RecieverDisplayName', () => Serenity.StringEditor], ['MessageSubject', () => Serenity.TextAreaEditor], ['MessageBody', () => Serenity.TextAreaEditor], ['MessageFile', () => Serenity.ImageUploadEditor]].forEach(x => Object.defineProperty(SentForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

