
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class WorkFlowStepDialog extends Serenity.EntityDialog<WorkFlowStepRow, any> {
        protected getFormKey() { return WorkFlowStepForm.formKey; }
        protected getIdProperty() { return WorkFlowStepRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowStepRow.localTextPrefix; }
        protected getNameProperty() { return WorkFlowStepRow.nameProperty; }
        protected getService() { return WorkFlowStepService.baseUrl; }

        protected form = new WorkFlowStepForm(this.idPrefix);
    }
}