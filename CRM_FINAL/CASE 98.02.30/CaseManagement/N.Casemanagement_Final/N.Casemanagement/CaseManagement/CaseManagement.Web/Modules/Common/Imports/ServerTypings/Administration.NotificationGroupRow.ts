namespace CaseManagement.Administration {
    export interface NotificationGroupRow {
        Id?: number;
        Name?: string;
    }

    export namespace NotificationGroupRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Administration.NotificationGroup';

        export namespace Fields {
            export declare const Id: string;
            export declare const Name: string;
        }

        ['Id', 'Name'].forEach(x => (<any>Fields)[x] = x);
    }
}

