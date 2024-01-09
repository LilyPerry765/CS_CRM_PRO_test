
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestHistoryGrid extends Serenity.EntityGrid<ActivityRequestHistoryRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestHistory'; }
        protected getDialogType() { return ActivityRequestHistoryDialog; }
        protected getIdProperty() { return ActivityRequestHistoryRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestHistoryRow.localTextPrefix; }
        protected getService() { return ActivityRequestHistoryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}