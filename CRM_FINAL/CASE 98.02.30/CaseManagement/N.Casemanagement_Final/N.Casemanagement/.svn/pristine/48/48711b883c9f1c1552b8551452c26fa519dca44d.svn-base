
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestCommentReasonDialog extends Serenity.EntityDialog<ActivityRequestCommentReasonRow, any> {
        protected getFormKey() { return ActivityRequestCommentReasonForm.formKey; }
        protected getIdProperty() { return ActivityRequestCommentReasonRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestCommentReasonRow.localTextPrefix; }
        protected getService() { return ActivityRequestCommentReasonService.baseUrl; }

        protected form = new ActivityRequestCommentReasonForm(this.idPrefix);
    }
}