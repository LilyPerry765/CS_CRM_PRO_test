namespace CaseManagement.Case {
    export interface ProvinceRow {
        Id?: number;
        LeaderID?: number;
        Name?: string;
        Code?: number;
        EnglishName?: string;
        ManagerName?: string;
        LetterNo?: string;
        PmoLevelId?: number;
        InstallDate?: string;
        PmoLevelName?: string;
        LeaderName?: string;
    }

    export namespace ProvinceRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Case.Province';
        export const lookupKey = 'Case.Province';

        export function getLookup(): Q.Lookup<ProvinceRow> {
            return Q.getLookup<ProvinceRow>('Case.Province');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const LeaderID: string;
            export declare const Name: string;
            export declare const Code: string;
            export declare const EnglishName: string;
            export declare const ManagerName: string;
            export declare const LetterNo: string;
            export declare const PmoLevelId: string;
            export declare const InstallDate: string;
            export declare const PmoLevelName: string;
            export declare const LeaderName: string;
        }

        ['Id', 'LeaderID', 'Name', 'Code', 'EnglishName', 'ManagerName', 'LetterNo', 'PmoLevelId', 'InstallDate', 'PmoLevelName', 'LeaderName'].forEach(x => (<any>Fields)[x] = x);
    }
}

