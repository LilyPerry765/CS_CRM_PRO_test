
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestHistoryDialog extends Serenity.EntityDialog<ActivityRequestHistoryRow, any> {
        protected getFormKey() { return ActivityRequestHistoryForm.formKey; }
        protected getIdProperty() { return ActivityRequestHistoryRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestHistoryRow.localTextPrefix; }
        protected getNameProperty() { return ActivityRequestHistoryRow.nameProperty; }
        protected getService() { return ActivityRequestHistoryService.baseUrl; }

        protected form = new ActivityRequestHistoryForm(this.idPrefix);
    }
}