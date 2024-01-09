namespace CaseManagement.Case {
    export class ActivityForm extends Serenity.PrefixedContext {
        static formKey = 'Case.Activity';

    }

    export interface ActivityForm {
        Code: Serenity.IntegerEditor;
        Name: Serenity.TextAreaEditor;
        EnglishName: Serenity.TextAreaEditor;
        Objective: Serenity.TextAreaEditor;
        EnglishObjective: Serenity.TextAreaEditor;
        GroupId: Serenity.LookupEditor;
        RepeatTermId: Serenity.LookupEditor;
        RequiredYearRepeatCount: Serenity.LookupEditor;
        Factor: Serenity.StringEditor;
        KeyCheckArea: Serenity.TextAreaEditor;
        DataSource: Serenity.TextAreaEditor;
        Methodology: Serenity.TextAreaEditor;
        KeyFocus: Serenity.TextAreaEditor;
        Action: Serenity.TextAreaEditor;
        KPI: Serenity.TextAreaEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReasonList: ActivityMainReasonEditor;
        CorrectionOperationList: ActivityCorrectionOperationEditor;
    }

    [['Code', () => Serenity.IntegerEditor], ['Name', () => Serenity.TextAreaEditor], ['EnglishName', () => Serenity.TextAreaEditor], ['Objective', () => Serenity.TextAreaEditor], ['EnglishObjective', () => Serenity.TextAreaEditor], ['GroupId', () => Serenity.LookupEditor], ['RepeatTermId', () => Serenity.LookupEditor], ['RequiredYearRepeatCount', () => Serenity.LookupEditor], ['Factor', () => Serenity.StringEditor], ['KeyCheckArea', () => Serenity.TextAreaEditor], ['DataSource', () => Serenity.TextAreaEditor], ['Methodology', () => Serenity.TextAreaEditor], ['KeyFocus', () => Serenity.TextAreaEditor], ['Action', () => Serenity.TextAreaEditor], ['KPI', () => Serenity.TextAreaEditor], ['EventDescription', () => Serenity.TextAreaEditor], ['MainReasonList', () => ActivityMainReasonEditor], ['CorrectionOperationList', () => ActivityCorrectionOperationEditor]].forEach(x => Object.defineProperty(ActivityForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

