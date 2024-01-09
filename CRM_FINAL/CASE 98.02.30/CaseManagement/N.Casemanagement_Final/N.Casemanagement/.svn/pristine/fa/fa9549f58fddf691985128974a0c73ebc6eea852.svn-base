
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class WorkFlowStatusDialog extends Serenity.EntityDialog<WorkFlowStatusRow, any> {
        protected getFormKey() { return WorkFlowStatusForm.formKey; }
        protected getIdProperty() { return WorkFlowStatusRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowStatusRow.localTextPrefix; }
        protected getNameProperty() { return WorkFlowStatusRow.nameProperty; }
        protected getService() { return WorkFlowStatusService.baseUrl; }

        protected form = new WorkFlowStatusForm(this.idPrefix);
    }
}