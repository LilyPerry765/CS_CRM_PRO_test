
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityCorrectionOperationDialog extends Serenity.EntityDialog<ActivityCorrectionOperationRow, any> {
        protected getFormKey() { return ActivityCorrectionOperationForm.formKey; }
        protected getIdProperty() { return ActivityCorrectionOperationRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityCorrectionOperationRow.localTextPrefix; }
        protected getNameProperty() { return ActivityCorrectionOperationRow.nameProperty; }
        protected getService() { return ActivityCorrectionOperationService.baseUrl; }

        protected form = new ActivityCorrectionOperationForm(this.idPrefix);
    }
}