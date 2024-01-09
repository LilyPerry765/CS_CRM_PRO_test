
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestCommentReasonGrid extends Serenity.EntityGrid<ActivityRequestCommentReasonRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestCommentReason'; }
        protected getDialogType() { return ActivityRequestCommentReasonDialog; }
        protected getIdProperty() { return ActivityRequestCommentReasonRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestCommentReasonRow.localTextPrefix; }
        protected getService() { return ActivityRequestCommentReasonService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}