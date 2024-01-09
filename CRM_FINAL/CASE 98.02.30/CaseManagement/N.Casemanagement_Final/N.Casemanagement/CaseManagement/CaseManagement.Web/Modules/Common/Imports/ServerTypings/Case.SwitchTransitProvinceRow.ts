namespace CaseManagement.Case {
    export interface SwitchTransitProvinceRow {
        Id?: number;
        ProvinceId?: number;
        SwitchTransitId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ProvinceName?: string;
        SwitchTransitName?: string;
    }

    export namespace SwitchTransitProvinceRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ProvinceName';
        export const localTextPrefix = 'Case.SwitchTransitProvince';

        export namespace Fields {
            export declare const Id: string;
            export declare const ProvinceId: string;
            export declare const SwitchTransitId: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
            export declare const ProvinceName: string;
            export declare const SwitchTransitName: string;
        }

        ['Id', 'ProvinceId', 'SwitchTransitId', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ProvinceName', 'SwitchTransitName'].forEach(x => (<any>Fields)[x] = x);
    }
}

