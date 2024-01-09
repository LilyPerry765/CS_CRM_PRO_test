
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestLogDialog extends Serenity.EntityDialog<ActivityRequestLogRow, any> {
        protected getFormKey() { return ActivityRequestLogForm.formKey; }
        protected getIdProperty() { return ActivityRequestLogRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestLogRow.localTextPrefix; }
        protected getService() { return ActivityRequestLogService.baseUrl; }

        protected form = new ActivityRequestLogForm(this.idPrefix);
    }
}