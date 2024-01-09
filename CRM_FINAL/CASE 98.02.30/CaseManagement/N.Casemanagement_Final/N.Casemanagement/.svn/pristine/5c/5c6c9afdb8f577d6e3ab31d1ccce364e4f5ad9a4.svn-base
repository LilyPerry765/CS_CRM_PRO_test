
namespace CaseManagement.StimulReport {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestDetailDialog extends Serenity.EntityDialog<ActivityRequestDetailRow, any> {
        protected getFormKey() { return ActivityRequestDetailForm.formKey; }
        protected getIdProperty() { return ActivityRequestDetailRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestDetailRow.localTextPrefix; }
        protected getService() { return ActivityRequestDetailService.baseUrl; }

        protected form = new ActivityRequestDetailForm(this.idPrefix);
    }
} 