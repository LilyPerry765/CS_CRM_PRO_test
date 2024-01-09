namespace CaseManagement.Case {
    export class ActivityHelpForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityHelp';

    }

    export interface ActivityHelpForm {
        Code: Serenity.IntegerEditor;
        Name: Serenity.TextAreaEditor;
        EnglishName: Serenity.TextAreaEditor;
        Objective: Serenity.TextAreaEditor;
        EnglishObjective: Serenity.TextAreaEditor;
        GroupId: Serenity.LookupEditor;
        RepeatTermId: Serenity.LookupEditor;
        KeyCheckArea: Serenity.TextAreaEditor;
        DataSource: Serenity.TextAreaEditor;
        Methodology: Serenity.TextAreaEditor;
        KeyFocus: Serenity.TextAreaEditor;
        Action: Serenity.TextAreaEditor;
        KPI: Serenity.TextAreaEditor;
        EventDescription: Serenity.TextAreaEditor;
    }

    [['Code', () => Serenity.IntegerEditor], ['Name', () => Serenity.TextAreaEditor], ['EnglishName', () => Serenity.TextAreaEditor], ['Objective', () => Serenity.TextAreaEditor], ['EnglishObjective', () => Serenity.TextAreaEditor], ['GroupId', () => Serenity.LookupEditor], ['RepeatTermId', () => Serenity.LookupEditor], ['KeyCheckArea', () => Serenity.TextAreaEditor], ['DataSource', () => Serenity.TextAreaEditor], ['Methodology', () => Serenity.TextAreaEditor], ['KeyFocus', () => Serenity.TextAreaEditor], ['Action', () => Serenity.TextAreaEditor], ['KPI', () => Serenity.TextAreaEditor], ['EventDescription', () => Serenity.TextAreaEditor]].forEach(x => Object.defineProperty(ActivityHelpForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

