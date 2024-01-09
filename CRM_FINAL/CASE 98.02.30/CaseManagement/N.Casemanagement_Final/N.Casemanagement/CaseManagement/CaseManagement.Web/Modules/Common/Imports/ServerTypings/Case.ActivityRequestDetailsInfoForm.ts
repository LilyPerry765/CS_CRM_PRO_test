namespace CaseManagement.Case {
    export class ActivityRequestDetailsInfoForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestDetailsInfo';

    }

    export interface ActivityRequestDetailsInfoForm {
        Id: Serenity.StringEditor;
        ProvinceId: Serenity.IntegerEditor;
        ActivityId: Serenity.IntegerEditor;
        CycleId: Serenity.IntegerEditor;
        IncomeFlowId: Serenity.IntegerEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.StringEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.StringEditor;
        YearCost: Serenity.StringEditor;
        AccessibleCost: Serenity.StringEditor;
        InaccessibleCost: Serenity.StringEditor;
        TotalLeakage: Serenity.StringEditor;
        RecoverableLeakage: Serenity.StringEditor;
        Recovered: Serenity.StringEditor;
        DelayedCostHistory: Serenity.StringEditor;
        YearCostHistory: Serenity.StringEditor;
        AccessibleCostHistory: Serenity.StringEditor;
        InaccessibleCostHistory: Serenity.StringEditor;
        RejectCount: Serenity.IntegerEditor;
        EventDescription: Serenity.StringEditor;
        MainReason: Serenity.StringEditor;
        CycleName: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Expr1: Serenity.StringEditor;
        CodeName: Serenity.StringEditor;
    }

    [['Id', () => Serenity.StringEditor], ['ProvinceId', () => Serenity.IntegerEditor], ['ActivityId', () => Serenity.IntegerEditor], ['CycleId', () => Serenity.IntegerEditor], ['IncomeFlowId', () => Serenity.IntegerEditor], ['Count', () => Serenity.IntegerEditor], ['CycleCost', () => Serenity.StringEditor], ['Factor', () => Serenity.StringEditor], ['DelayedCost', () => Serenity.StringEditor], ['YearCost', () => Serenity.StringEditor], ['AccessibleCost', () => Serenity.StringEditor], ['InaccessibleCost', () => Serenity.StringEditor], ['TotalLeakage', () => Serenity.StringEditor], ['RecoverableLeakage', () => Serenity.StringEditor], ['Recovered', () => Serenity.StringEditor], ['DelayedCostHistory', () => Serenity.StringEditor], ['YearCostHistory', () => Serenity.StringEditor], ['AccessibleCostHistory', () => Serenity.StringEditor], ['InaccessibleCostHistory', () => Serenity.StringEditor], ['RejectCount', () => Serenity.IntegerEditor], ['EventDescription', () => Serenity.StringEditor], ['MainReason', () => Serenity.StringEditor], ['CycleName', () => Serenity.StringEditor], ['Name', () => Serenity.StringEditor], ['Expr1', () => Serenity.StringEditor], ['CodeName', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(ActivityRequestDetailsInfoForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

