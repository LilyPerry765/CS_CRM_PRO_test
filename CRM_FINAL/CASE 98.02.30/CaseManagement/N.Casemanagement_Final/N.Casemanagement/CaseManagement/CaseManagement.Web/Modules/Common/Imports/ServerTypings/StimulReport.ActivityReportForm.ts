

namespace CaseManagement.StimulReport {
    export class ActivityReportForm extends Serenity.PrefixedContext {
        static formKey = 'StimulReport.ActivityReport';
    }

    export interface ActivityReportForm {
        RequestId: Serenity.IntegerEditor;
        ProvinceId: Serenity.IntegerEditor;
        ActivityId: Serenity.IntegerEditor;
        DelayCost: Serenity.IntegerEditor;
        YearCost: Serenity.IntegerEditor;
        LeakCost: Serenity.IntegerEditor;
        ConfirmDelayCost: Serenity.IntegerEditor;
        ConfirmYearCost: Serenity.IntegerEditor;
        ConfirmCost: Serenity.IntegerEditor;
        ProgramCost: Serenity.IntegerEditor;
        Percent: Serenity.StringEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        DiscoverLeakDateShamsi: Serenity.StringEditor;
        EndDate: Serenity.DateEditor;
        ConfirmUserId: Serenity.IntegerEditor;
        ActionId: Serenity.IntegerEditor;
    }

    [['Id', () => Serenity.IntegerEditor], ['RequestId', () => Serenity.IntegerEditor], ['ProvinceId', () => Serenity.IntegerEditor], ['ActivityId', () => Serenity.IntegerEditor], ['DelayCost', () => Serenity.IntegerEditor], ['YearCost', () => Serenity.IntegerEditor], ['LeakCost', () => Serenity.IntegerEditor], ['ConfirmDelayCost', () => Serenity.IntegerEditor], ['ConfirmYearCost', () => Serenity.IntegerEditor], ['ConfirmCost', () => Serenity.IntegerEditor], ['ProgramCost', () => Serenity.IntegerEditor], ['Percent', () => Serenity.StringEditor], ['CreatedUserId', () => Serenity.IntegerEditor], ['CreatedDate', () => Serenity.DateEditor], ['DiscoverLeakDate', () => Serenity.DateEditor], ['DiscoverLeakDateShamsi', () => Serenity.StringEditor], ['EndDate', () => Serenity.DateEditor], ['ConfirmUserId', () => Serenity.IntegerEditor], ['ActionId', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(ActivityReportForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}