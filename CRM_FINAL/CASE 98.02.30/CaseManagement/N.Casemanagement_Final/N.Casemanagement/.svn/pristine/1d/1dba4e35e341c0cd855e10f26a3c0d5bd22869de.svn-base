namespace CaseManagement.Messaging {
    export class InboxForm extends Serenity.PrefixedContext {
        static formKey = 'Messaging.Inbox';

    }

    export interface InboxForm {
        SenderDisplayName: Serenity.StringEditor;
        MessageSubject: Serenity.TextAreaEditor;
        MessageBody: Serenity.TextAreaEditor;
        MessageFile: Serenity.ImageUploadEditor;
    }

    [['SenderDisplayName', () => Serenity.StringEditor], ['MessageSubject', () => Serenity.TextAreaEditor], ['MessageBody', () => Serenity.TextAreaEditor], ['MessageFile', () => Serenity.ImageUploadEditor]].forEach(x => Object.defineProperty(InboxForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

