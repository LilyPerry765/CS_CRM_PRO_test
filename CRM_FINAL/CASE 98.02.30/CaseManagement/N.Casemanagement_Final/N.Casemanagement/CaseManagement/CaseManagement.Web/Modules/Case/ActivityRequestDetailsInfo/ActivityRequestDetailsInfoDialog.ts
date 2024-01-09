
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestDetailsInfoDialog extends Serenity.EntityDialog<ActivityRequestDetailsInfoRow, any> {
        protected getFormKey() { return ActivityRequestDetailsInfoForm.formKey; }
        protected getLocalTextPrefix() { return ActivityRequestDetailsInfoRow.localTextPrefix; }
        protected getNameProperty() { return ActivityRequestDetailsInfoRow.nameProperty; }
        protected getService() { return ActivityRequestDetailsInfoService.baseUrl; }

        protected form = new ActivityRequestDetailsInfoForm(this.idPrefix);
    }
}