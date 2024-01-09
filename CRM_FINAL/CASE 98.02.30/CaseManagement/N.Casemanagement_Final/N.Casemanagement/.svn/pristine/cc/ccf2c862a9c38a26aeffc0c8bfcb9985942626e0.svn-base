namespace CaseManagement.Case {
    export class ActivityRequestPenddingForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestPendding';

    }

    export interface ActivityRequestPenddingForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.DecimalEditor;
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

    [['ProvinceName', () => Serenity.StringEditor], ['Id', () => Serenity.StringEditor], ['ActivityId', () => Serenity.LookupEditor], ['CycleId', () => Serenity.LookupEditor], ['IncomeFlowId', () => Serenity.LookupEditor], ['DiscoverLeakDate', () => Serenity.DateEditor], ['CycleCostHistory', () => Serenity.DecimalEditor], ['DelayedCostHistory', () => Serenity.DecimalEditor], ['AccessibleCostHistory', () => Serenity.DecimalEditor], ['InaccessibleCostHistory', () => Serenity.DecimalEditor], ['Count', () => Serenity.IntegerEditor], ['CycleCost', () => Serenity.DecimalEditor], ['Factor', () => Serenity.DecimalEditor], ['DelayedCost', () => Serenity.DecimalEditor], ['AccessibleCost', () => Serenity.DecimalEditor], ['InaccessibleCost', () => Serenity.DecimalEditor], ['YearCost', () => Serenity.DecimalEditor], ['TotalLeakage', () => Serenity.DecimalEditor], ['RecoverableLeakage', () => Serenity.DecimalEditor], ['Recovered', () => Serenity.DecimalEditor], ['EventDescription', () => Serenity.TextAreaEditor], ['MainReason', () => Serenity.TextAreaEditor], ['CommnetList', () => ActivityRequestCommentEditor], ['File1', () => Serenity.ImageUploadEditor], ['File2', () => Serenity.ImageUploadEditor], ['ConfirmTypeID', () => Serenity.EnumEditor], ['ActionID', () => Serenity.EnumEditor]].forEach(x => Object.defineProperty(ActivityRequestPenddingForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

