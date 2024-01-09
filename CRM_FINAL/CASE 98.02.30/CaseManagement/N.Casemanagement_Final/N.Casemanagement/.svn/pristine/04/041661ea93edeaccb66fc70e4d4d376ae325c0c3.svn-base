namespace CaseManagement.Messaging {
    export interface InboxRow {
        Id?: number;
        MessageId?: number;
        RecieverId?: number;
        SenderId?: number;
        Seen?: boolean;
        SeenDate?: string;
        MessageSubject?: string;
        MessageBody?: string;
        MessageFile?: string;
        MessageInsertedDate?: string;
        RecieverDisplayName?: string;
        SenderDisplayName?: string;
    }

    export namespace InboxRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Messaging.Inbox';

        export namespace Fields {
            export declare const Id: string;
            export declare const MessageId: string;
            export declare const RecieverId: string;
            export declare const SenderId: string;
            export declare const Seen: string;
            export declare const SeenDate: string;
            export declare const MessageSubject: string;
            export declare const MessageBody: string;
            export declare const MessageFile: string;
            export declare const MessageInsertedDate: string;
            export declare const RecieverDisplayName: string;
            export declare const SenderDisplayName: string;
        }

        ['Id', 'MessageId', 'RecieverId', 'SenderId', 'Seen', 'SeenDate', 'MessageSubject', 'MessageBody', 'MessageFile', 'MessageInsertedDate', 'RecieverDisplayName', 'SenderDisplayName'].forEach(x => (<any>Fields)[x] = x);
    }
}

