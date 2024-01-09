namespace ReportTemplateDB.Common {
    export interface ReportTemplateRow {
        Id?: number;
        Template?: number[];
        Title?: string;
    }

    export namespace ReportTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Title';
        export const localTextPrefix = 'Common.ReportTemplate';

        export namespace Fields {
            export declare const Id: string;
            export declare const Template: string;
            export declare const Title: string;
        }

        ['Id', 'Template', 'Title'].forEach(x => (<any>Fields)[x] = x);
    }
}

