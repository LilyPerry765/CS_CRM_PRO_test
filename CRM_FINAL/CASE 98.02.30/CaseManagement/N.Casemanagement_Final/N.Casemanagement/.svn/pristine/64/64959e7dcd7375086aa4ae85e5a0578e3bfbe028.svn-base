namespace CaseManagement.Case {
    export class ActivityRequestDeleteForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestDelete';

    }

    export interface ActivityRequestDeleteForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
    }

    [['ProvinceName', () => Serenity.StringEditor], ['Id', () => Serenity.StringEditor], ['ActivityId', () => Serenity.LookupEditor], ['CycleId', () => Serenity.LookupEditor], ['IncomeFlowId', () => Serenity.LookupEditor], ['DiscoverLeakDate', () => Serenity.DateEditor], ['Count', () => Serenity.IntegerEditor], ['CycleCost', () => Serenity.DecimalEditor], ['Factor', () => Serenity.StringEditor], ['DelayedCost', () => Serenity.DecimalEditor], ['AccessibleCost', () => Serenity.DecimalEditor], ['InaccessibleCost', () => Serenity.DecimalEditor], ['YearCost', () => Serenity.DecimalEditor], ['TotalLeakage', () => Serenity.DecimalEditor], ['RecoverableLeakage', () => Serenity.DecimalEditor], ['Recovered', () => Serenity.DecimalEditor], ['EventDescription', () => Serenity.TextAreaEditor], ['MainReason', () => Serenity.TextAreaEditor], ['CommnetList', () => ActivityRequestCommentEditor], ['File1', () => Serenity.ImageUploadEditor], ['File2', () => Serenity.ImageUploadEditor]].forEach(x => Object.defineProperty(ActivityRequestDeleteForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

