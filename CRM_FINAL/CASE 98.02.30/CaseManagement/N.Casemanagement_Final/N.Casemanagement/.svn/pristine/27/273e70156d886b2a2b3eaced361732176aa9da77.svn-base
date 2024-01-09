namespace CaseManagement.Case {
    export class ActivityRequestForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequest';

    }

    export interface ActivityRequestForm {
        ActivityId: Serenity.LookupEditor;
        ProvinceName: Serenity.StringEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
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
        ConfirmTypeID: Serenity.EnumEditor;
        ActionID: Serenity.EnumEditor;
    }

    [['ActivityId', () => Serenity.LookupEditor], ['ProvinceName', () => Serenity.StringEditor], ['DiscoverLeakDate', () => Serenity.DateEditor], ['CycleId', () => Serenity.LookupEditor], ['IncomeFlowId', () => Serenity.LookupEditor], ['Count', () => Serenity.IntegerEditor], ['CycleCost', () => Serenity.DecimalEditor], ['Factor', () => Serenity.StringEditor], ['DelayedCost', () => Serenity.DecimalEditor], ['AccessibleCost', () => Serenity.DecimalEditor], ['InaccessibleCost', () => Serenity.DecimalEditor], ['YearCost', () => Serenity.DecimalEditor], ['TotalLeakage', () => Serenity.DecimalEditor], ['RecoverableLeakage', () => Serenity.DecimalEditor], ['Recovered', () => Serenity.DecimalEditor], ['EventDescription', () => Serenity.TextAreaEditor], ['MainReason', () => Serenity.TextAreaEditor], ['CommnetList', () => ActivityRequestCommentEditor], ['File1', () => Serenity.ImageUploadEditor], ['File2', () => Serenity.ImageUploadEditor], ['ConfirmTypeID', () => Serenity.EnumEditor], ['ActionID', () => Serenity.EnumEditor]].forEach(x => Object.defineProperty(ActivityRequestForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

