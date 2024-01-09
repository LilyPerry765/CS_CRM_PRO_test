
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestDetailsInfoGrid extends Serenity.EntityGrid<ActivityRequestDetailsInfoRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestDetailsInfo'; }
        protected getDialogType() { return ActivityRequestDetailsInfoDialog; }
        protected getLocalTextPrefix() { return ActivityRequestDetailsInfoRow.localTextPrefix; }
        protected getService() { return ActivityRequestDetailsInfoService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}