namespace CaseManagement.Administration {
    export interface UserSupportGroupRow {
        Id?: number;
        UserId?: number;
        GroupId?: number;
        UserOldcaseId?: number;
        UserUsername?: string;
        UserDisplayName?: string;
        UserEmployeeId?: string;
        UserEmail?: string;
        UserRank?: string;
        UserSource?: string;
        UserPassword?: string;
        UserPasswordHash?: string;
        UserPasswordSalt?: string;
        UserInsertDate?: string;
        UserInsertUserId?: number;
        UserUpdateDate?: string;
        UserUpdateUserId?: number;
        UserIsActive?: number;
        UserLastDirectoryUpdate?: string;
        UserRoleId?: number;
        UserTelephoneNo1?: string;
        UserTelephoneNo2?: string;
        UserMobileNo?: string;
        UserDegree?: string;
        UserProvinceId?: number;
    }

    export namespace UserSupportGroupRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Administration.UserSupportGroup';

        export namespace Fields {
            export declare const Id: string;
            export declare const UserId: string;
            export declare const GroupId: string;
            export declare const UserOldcaseId: string;
            export declare const UserUsername: string;
            export declare const UserDisplayName: string;
            export declare const UserEmployeeId: string;
            export declare const UserEmail: string;
            export declare const UserRank: string;
            export declare const UserSource: string;
            export declare const UserPassword: string;
            export declare const UserPasswordHash: string;
            export declare const UserPasswordSalt: string;
            export declare const UserInsertDate: string;
            export declare const UserInsertUserId: string;
            export declare const UserUpdateDate: string;
            export declare const UserUpdateUserId: string;
            export declare const UserIsActive: string;
            export declare const UserLastDirectoryUpdate: string;
            export declare const UserRoleId: string;
            export declare const UserTelephoneNo1: string;
            export declare const UserTelephoneNo2: string;
            export declare const UserMobileNo: string;
            export declare const UserDegree: string;
            export declare const UserProvinceId: string;
        }

        ['Id', 'UserId', 'GroupId', 'UserOldcaseId', 'UserUsername', 'UserDisplayName', 'UserEmployeeId', 'UserEmail', 'UserRank', 'UserSource', 'UserPassword', 'UserPasswordHash', 'UserPasswordSalt', 'UserInsertDate', 'UserInsertUserId', 'UserUpdateDate', 'UserUpdateUserId', 'UserIsActive', 'UserLastDirectoryUpdate', 'UserRoleId', 'UserTelephoneNo1', 'UserTelephoneNo2', 'UserMobileNo', 'UserDegree', 'UserProvinceId'].forEach(x => (<any>Fields)[x] = x);
    }
}

