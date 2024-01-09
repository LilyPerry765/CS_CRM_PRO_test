﻿namespace CaseManagement.Administration {
    export interface LogRow {
        Id?: number;
        TableName?: string;
        PersianTableName?: string;
        RecordId?: number;
        RecordName?: string;
        IP?: string;
        ActionID?: ActionLog;
        UserId?: number;
        InsertDate?: string;
        DisplayName?: string;
        ProvinceId?: number;
        ProvinceName?: string;
        OldData?: string;
    }

    export namespace LogRow {
        export const idProperty = 'Id';
        export const nameProperty = 'TableName';
        export const localTextPrefix = 'Administration.Log';

        export namespace Fields {
            export declare const Id: string;
            export declare const TableName: string;
            export declare const PersianTableName: string;
            export declare const RecordId: string;
            export declare const RecordName: string;
            export declare const IP: string;
            export declare const ActionID: string;
            export declare const UserId: string;
            export declare const InsertDate: string;
            export declare const DisplayName: string;
            export declare const ProvinceId: string;
            export declare const ProvinceName: string;
            export declare const OldData: string;
        }

        ['Id', 'TableName', 'PersianTableName', 'RecordId', 'RecordName', 'IP', 'ActionID', 'UserId', 'InsertDate', 'DisplayName', 'ProvinceId', 'ProvinceName', 'OldData'].forEach(x => (<any>Fields)[x] = x);
    }
}
