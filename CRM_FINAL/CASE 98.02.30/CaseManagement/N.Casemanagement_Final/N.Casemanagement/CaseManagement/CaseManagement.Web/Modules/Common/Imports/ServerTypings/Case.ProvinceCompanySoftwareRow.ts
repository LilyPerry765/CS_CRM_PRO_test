namespace CaseManagement.Case {
    export interface ProvinceCompanySoftwareRow {
        Id?: number;
        ProvinveId?: number;
        CompanyId?: number;
        SoftwareId?: number;
        StatusID?: SoftwareStatus;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ProvinveName?: string;
        CompanyName?: string;
        SoftwareName?: string;
    }

    export namespace ProvinceCompanySoftwareRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Case.ProvinceCompanySoftware';

        export namespace Fields {
            export declare const Id: string;
            export declare const ProvinveId: string;
            export declare const CompanyId: string;
            export declare const SoftwareId: string;
            export declare const StatusID: string;
            export declare const CreatedUserId: string;
            export declare const CreatedDate: string;
            export declare const ModifiedUserId: string;
            export declare const ModifiedDate: string;
            export declare const IsDeleted: string;
            export declare const DeletedUserId: string;
            export declare const DeletedDate: string;
            export declare const ProvinveName: string;
            export declare const CompanyName: string;
            export declare const SoftwareName: string;
        }

        ['Id', 'ProvinveId', 'CompanyId', 'SoftwareId', 'StatusID', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ProvinveName', 'CompanyName', 'SoftwareName'].forEach(x => (<any>Fields)[x] = x);
    }
}

