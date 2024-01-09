
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityMainReasonDialog extends Serenity.EntityDialog<ActivityMainReasonRow, any> {
        protected getFormKey() { return ActivityMainReasonForm.formKey; }
        protected getIdProperty() { return ActivityMainReasonRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityMainReasonRow.localTextPrefix; }
        protected getNameProperty() { return ActivityMainReasonRow.nameProperty; }
        protected getService() { return ActivityMainReasonService.baseUrl; }

        protected form = new ActivityMainReasonForm(this.idPrefix);
    }
}