namespace CaseManagement.Messaging {
    export class NewMessageForm extends Serenity.PrefixedContext {
        static formKey = 'Messaging.NewMessage';

    }

    export interface NewMessageForm {
        ReceiverList: Serenity.LookupEditor;
        Subject: Serenity.TextAreaEditor;
        Body: Serenity.TextAreaEditor;
        File: Serenity.ImageUploadEditor;
    }

    [['ReceiverList', () => Serenity.LookupEditor], ['Subject', () => Serenity.TextAreaEditor], ['Body', () => Serenity.TextAreaEditor], ['File', () => Serenity.ImageUploadEditor]].forEach(x => Object.defineProperty(NewMessageForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

