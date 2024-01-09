namespace CaseManagement.Case {
    export interface ActivityRow {
        Id?: number;
        Code?: string;
        Name?: string;
        CodeName?: string;
        EnglishName?: string;
        Objective?: string;
        EnglishObjective?: string;
        GroupId?: number;
        RepeatTermId?: number;
        RequiredYearRepeatCount?: number;
        GroupName?: string;
        RepeatTermName?: string;
        KeyCheckArea?: string;
        DataSource?: string;
        Methodology?: string;
        KeyFocus?: string;
        Action?: string;
        KPI?: string;
        EventDescription?: string;
        MainReasonList?: ActivityMainReasonRow[];
        CorrectionOperationList?: ActivityCorrectionOperationRow[];
        Factor?: number;
    }

    export namespace ActivityRow {
        export const idProperty = 'Id';
        export const nameProperty = 'CodeName';
        export const localTextPrefix = 'Case.Activity';
        export const lookupKey = 'Case.Activity';

        export function getLookup(): Q.Lookup<ActivityRow> {
            return Q.getLookup<ActivityRow>('Case.Activity');
        }

        export namespace Fields {
            export declare const Id: string;
            export declare const Code: string;
            export declare const Name: string;
            export declare const CodeName: string;
            export declare const EnglishName: string;
            export declare const Objective: string;
            export declare const EnglishObjective: string;
            export declare const GroupId: string;
            export declare const RepeatTermId: string;
            export declare const RequiredYearRepeatCount: string;
            export declare const GroupName: string;
            export declare const RepeatTermName: string;
            export declare const KeyCheckArea: string;
            export declare const DataSource: string;
            export declare const Methodology: string;
            export declare const KeyFocus: string;
            export declare const Action: string;
            export declare const KPI: string;
            export declare const EventDescription: string;
            export declare const MainReasonList: string;
            export declare const CorrectionOperationList: string;
            export declare const Factor: string;
        }

        ['Id', 'Code', 'Name', 'CodeName', 'EnglishName', 'Objective', 'EnglishObjective', 'GroupId', 'RepeatTermId', 'RequiredYearRepeatCount', 'GroupName', 'RepeatTermName', 'KeyCheckArea', 'DataSource', 'Methodology', 'KeyFocus', 'Action', 'KPI', 'EventDescription', 'MainReasonList', 'CorrectionOperationList', 'Factor'].forEach(x => (<any>Fields)[x] = x);
    }
}

