namespace CaseManagement.Case {
    export interface SMSLogRow {
        Id?: number;
        Is_modified?: boolean;
        InsertDate?: string;
        ActivityRequestId?: number;
        MessageId?: number;
        SenderUserId?: number;
        SenderUserName?: string;
        ReceiverProvinceId?: number;
        ReceiverUserId?: number;
        ReceiverUserName?: string;
        MobileNumber?: string;
        IsSent?: boolean;
        IsDelivered?: boolean;
        ReceiverRoleId?: number;
        ReceiverProvinceLeaderId?: number;
        ReceiverProvinceName?: string;
        ReceiverProvinceCode?: number;
        ReceiverProvinceEnglishName?: string;
        ReceiverProvinceManagerName?: string;
        ReceiverProvinceLetterNo?: string;
        ReceiverProvincePmoLevelId?: number;
        ReceiverProvinceInstallDate?: string;
        ModifiedDate?: string;
        ReceiverRoleRoleName?: string;
    }

    export namespace SMSLogRow {
        export const idProperty = 'Id';
        export const nameProperty = 'SenderUserName';
        export const localTextPrefix = 'Case.SMSLog';
        export const lookupKey = 'Case.SMSLogRow';

        export function getLookup(): Q.Lookup<SMSLogRow> {
            return Q.getLookup<SMSLogRow>('Case.SMSLogRow');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Is_modified: string;
            export declare const InsertDate: string;
            export declare const ActivityRequestId: string;
            export declare const MessageId: string;
            export declare const SenderUserId: string;
            export declare const SenderUserName: string;
            export declare const ReceiverProvinceId: string;
            export declare const ReceiverUserId: string;
            export declare const ReceiverUserName: string;
            export declare const MobileNumber: string;
            export declare const IsSent: string;
            export declare const IsDelivered: string;
            export declare const ReceiverRoleId: string;
            export declare const ReceiverProvinceLeaderId: string;
            export declare const ReceiverProvinceName: string;
            export declare const ReceiverProvinceCode: string;
            export declare const ReceiverProvinceEnglishName: string;
            export declare const ReceiverProvinceManagerName: string;
            export declare const ReceiverProvinceLetterNo: string;
            export declare const ReceiverProvincePmoLevelId: string;
            export declare const ReceiverProvinceInstallDate: string;
            export declare const ModifiedDate: string;
            export declare const ReceiverRoleRoleName: string;
        }

        ['Id', 'Is_modified', 'InsertDate', 'ActivityRequestId', 'MessageId', 'SenderUserId', 'SenderUserName', 'ReceiverProvinceId', 'ReceiverUserId', 'ReceiverUserName', 'MobileNumber', 'IsSent', 'IsDelivered', 'ReceiverRoleId', 'ReceiverProvinceLeaderId', 'ReceiverProvinceName', 'ReceiverProvinceCode', 'ReceiverProvinceEnglishName', 'ReceiverProvinceManagerName', 'ReceiverProvinceLetterNo', 'ReceiverProvincePmoLevelId', 'ReceiverProvinceInstallDate', 'ModifiedDate', 'ReceiverRoleRoleName'].forEach(x => (<any>Fields)[x] = x);
    }
}

