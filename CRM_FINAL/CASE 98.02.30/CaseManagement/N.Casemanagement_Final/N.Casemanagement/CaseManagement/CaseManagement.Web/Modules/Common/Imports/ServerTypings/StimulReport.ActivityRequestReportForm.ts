namespace CaseManagement.StimulReport {
    export class ActivityRequestReportForm extends Serenity.PrefixedContext {
        static formKey = 'StimulReport.ActivityRequestReport';

    }

    export interface ActivityRequestReportForm {
        Id2: Serenity.IntegerEditor;
        ProvinceId: Serenity.LookupEditor;
        ActivityId: Serenity.LookupEditor;
        ActivityCode: Serenity.StringEditor;
        CycleId: Serenity.LookupEditor;
        CustomerEffectId: Serenity.LookupEditor;
        RiskLevelId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
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
        Financial: Serenity.StringEditor;
        LeakDate: Serenity.DateEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        DiscoverLeakDateShamsi: Serenity.StringEditor;
        EventDescription: Serenity.StringEditor;
        MainReason: Serenity.StringEditor;
        CorrectionOperation: Serenity.StringEditor;
        AvoidRepeatingOperation: Serenity.StringEditor;
        CreatedUserId: Serenity.LookupEditor;
        CreatedDate: Serenity.DateEditor;
        CreatedDateShamsi: Serenity.StringEditor;
        ModifiedUserId: Serenity.LookupEditor;
        ModifiedDate: Serenity.DateEditor;
        SendDate: Serenity.DateEditor;
        SendUserId: Serenity.LookupEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.LookupEditor;
        DeletedDate: Serenity.DateEditor;
        EndDate: Serenity.DateEditor;
        StatusId: Serenity.LookupEditor;
        ActionId: Serenity.IntegerEditor;
        File1: Serenity.StringEditor;
        File2: Serenity.StringEditor;
        File3: Serenity.StringEditor;
        ConfirmTypeId: Serenity.EnumEditor;
        IsRejected: Serenity.BooleanEditor;
        FinancialControllerConfirm: Serenity.BooleanEditor;
    }

    [['Id2', () => Serenity.IntegerEditor], ['ProvinceId', () => Serenity.LookupEditor], ['ActivityId', () => Serenity.LookupEditor], ['ActivityCode', () => Serenity.StringEditor], ['CycleId', () => Serenity.LookupEditor], ['CustomerEffectId', () => Serenity.LookupEditor], ['RiskLevelId', () => Serenity.LookupEditor], ['IncomeFlowId', () => Serenity.LookupEditor], ['Count', () => Serenity.IntegerEditor], ['CycleCost', () => Serenity.StringEditor], ['Factor', () => Serenity.StringEditor], ['DelayedCost', () => Serenity.StringEditor], ['YearCost', () => Serenity.StringEditor], ['AccessibleCost', () => Serenity.StringEditor], ['InaccessibleCost', () => Serenity.StringEditor], ['TotalLeakage', () => Serenity.StringEditor], ['RecoverableLeakage', () => Serenity.StringEditor], ['Recovered', () => Serenity.StringEditor], ['Financial', () => Serenity.StringEditor], ['LeakDate', () => Serenity.DateEditor], ['DiscoverLeakDate', () => Serenity.DateEditor], ['DiscoverLeakDateShamsi', () => Serenity.StringEditor], ['EventDescription', () => Serenity.StringEditor], ['MainReason', () => Serenity.StringEditor], ['CorrectionOperation', () => Serenity.StringEditor], ['AvoidRepeatingOperation', () => Serenity.StringEditor], ['CreatedUserId', () => Serenity.LookupEditor], ['CreatedDate', () => Serenity.DateEditor], ['CreatedDateShamsi', () => Serenity.StringEditor], ['ModifiedUserId', () => Serenity.LookupEditor], ['ModifiedDate', () => Serenity.DateEditor], ['SendDate', () => Serenity.DateEditor], ['SendUserId', () => Serenity.LookupEditor], ['IsDeleted', () => Serenity.BooleanEditor], ['DeletedUserId', () => Serenity.LookupEditor], ['DeletedDate', () => Serenity.DateEditor], ['EndDate', () => Serenity.DateEditor], ['StatusId', () => Serenity.LookupEditor], ['ActionId', () => Serenity.IntegerEditor], ['File1', () => Serenity.StringEditor], ['File2', () => Serenity.StringEditor], ['File3', () => Serenity.StringEditor], ['ConfirmTypeId', () => Serenity.EnumEditor], ['IsRejected', () => Serenity.BooleanEditor], ['FinancialControllerConfirm', () => Serenity.BooleanEditor]].forEach(x => Object.defineProperty(ActivityRequestReportForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

