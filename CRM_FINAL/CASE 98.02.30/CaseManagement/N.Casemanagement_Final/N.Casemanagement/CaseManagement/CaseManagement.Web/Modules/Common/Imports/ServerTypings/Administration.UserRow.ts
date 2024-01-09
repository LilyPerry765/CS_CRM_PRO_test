namespace CaseManagement.Administration {
    export interface UserRow {
        UserId?: number;
        Username?: string;
        Source?: string;
        PasswordHash?: string;
        PasswordSalt?: string;
        DisplayName?: string;
        EmployeeID?: string;
        Rank?: string;
        Email?: string;
        LastDirectoryUpdate?: string;
        IsActive?: number;
        Password?: string;
        PasswordConfirm?: string;
        TelephoneNo1?: string;
        TelephoneNo2?: string;
        MobileNo?: string;
        Degree?: string;
        ProvinceId?: number;
        ProvinceName?: string;
        IsIranTCI?: UserTCI;
        ProvinceList?: number[];
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        LastLoginDate?: string;
        ImagePath?: string;
        InsertUserId?: number;
        InsertDate?: string;
        UpdateUserId?: number;
        UpdateDate?: string;
    }

    export namespace UserRow {
        export const idProperty = 'UserId';
        export const isActiveProperty = 'IsActive';
        export const nameProperty = 'DisplayName';
        export const localTextPrefix = 'Administration.User';
        export const lookupKey = 'Administration.User';

        export function getLookup(): Q.Lookup<UserRow> {
            return Q.getLookup<UserRow>('Administration.User');
        }

        export namespace Fields {
            export declare const UserId: string;
            export declare const Username: string;
            export declare const Source: string;
            export declare const PasswordHash: string;
            export declare const PasswordSalt: string;
            export declare const DisplayName: string;
            export declare const EmployeeID: string;
            export declare const Rank: string;
            export declare const Email: string;
            export declare const LastDirectoryUpdate: string;
            export declare const IsActive: string;
            export declare const Password: string;
            export declare const PasswordConfirm: string;
            export declare const TelephoneNo1: string;
            export declare const TelephoneNo2: string;
            export declare const MobileNo: string;
            export declare const Degree: string;
            export declare const ProvinceId: string;
            export declare const ProvinceName: string;
            export declare const IsIranTCI: string;
            export declare const ProvinceList: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
            export declare const LastLoginDate: string;
            export declare const ImagePath: string;
            export declare const InsertUserId: string;
            export declare const InsertDate: string;
            export declare const UpdateUserId: string;
            export declare const UpdateDate: string;
        }

        ['UserId', 'Username', 'Source', 'PasswordHash', 'PasswordSalt', 'DisplayName', 'EmployeeID', 'Rank', 'Email', 'LastDirectoryUpdate', 'IsActive', 'Password', 'PasswordConfirm', 'TelephoneNo1', 'TelephoneNo2', 'MobileNo', 'Degree', 'ProvinceId', 'ProvinceName', 'IsIranTCI', 'ProvinceList', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'LastLoginDate', 'ImagePath', 'InsertUserId', 'InsertDate', 'UpdateUserId', 'UpdateDate'].forEach(x => (<any>Fields)[x] = x);
    }
}

