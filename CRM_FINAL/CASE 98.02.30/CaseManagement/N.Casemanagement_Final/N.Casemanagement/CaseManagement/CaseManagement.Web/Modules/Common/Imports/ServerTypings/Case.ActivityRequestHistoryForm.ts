namespace CaseManagement.Case {
    export class ActivityRequestHistoryForm extends Serenity.PrefixedContext {
        static formKey = 'Case.ActivityRequestHistory';

    }

    export interface ActivityRequestHistoryForm {
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        YearCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        TotalLeakageHistory: Serenity.DecimalEditor;
        RecoverableLeakageHistory: Serenity.DecimalEditor;
        RecoveredHistory: Serenity.DecimalEditor;
    }

    [['CycleCostHistory', () => Serenity.DecimalEditor], ['DelayedCostHistory', () => Serenity.DecimalEditor], ['YearCostHistory', () => Serenity.DecimalEditor], ['AccessibleCostHistory', () => Serenity.DecimalEditor], ['InaccessibleCostHistory', () => Serenity.DecimalEditor], ['TotalLeakageHistory', () => Serenity.DecimalEditor], ['RecoverableLeakageHistory', () => Serenity.DecimalEditor], ['RecoveredHistory', () => Serenity.DecimalEditor]].forEach(x => Object.defineProperty(ActivityRequestHistoryForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

