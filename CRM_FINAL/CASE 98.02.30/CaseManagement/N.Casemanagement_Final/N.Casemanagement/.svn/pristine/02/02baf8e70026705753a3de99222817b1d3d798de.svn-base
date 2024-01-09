namespace CaseManagement.Messaging {
    export interface NewMessageRow {
        Id?: number;
        SenderId?: number;
        Subject?: string;
        Body?: string;
        File?: string;
        InsertedDate?: string;
        SenderDisplayName?: string;
        ReceiverList?: number[];
    }

    export namespace NewMessageRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Subject';
        export const localTextPrefix = 'Messaging.NewMessage';
        export const lookupKey = 'Messaging.NewMessage';

        export function getLookup(): Q.Lookup<NewMessageRow> {
            return Q.getLookup<NewMessageRow>('Messaging.NewMessage');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const SenderId: string;
            export declare const Subject: string;
            export declare const Body: string;
            export declare const File: string;
            export declare const InsertedDate: string;
            export declare const SenderDisplayName: string;
            export declare const ReceiverList: string;
        }

        ['Id', 'SenderId', 'Subject', 'Body', 'File', 'InsertedDate', 'SenderDisplayName', 'ReceiverList'].forEach(x => (<any>Fields)[x] = x);
    }
}

