namespace CaseManagement.Case {
    export interface ActivityGroupRow {
        Id?: number;
        Name?: string;
        EnglishName?: string;
        Code?: number;
    }

    export namespace ActivityGroupRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Case.ActivityGroup';
        export const lookupKey = 'Case.ActivityGroup';

        export function getLookup(): Q.Lookup<ActivityGroupRow> {
            return Q.getLookup<ActivityGroupRow>('Case.ActivityGroup');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Name: string;
            export declare const EnglishName: string;
            export declare const Code: string;
        }

        ['Id', 'Name', 'EnglishName', 'Code'].forEach(x => (<any>Fields)[x] = x);
    }
}

