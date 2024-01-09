
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class WorkFlowActionDialog extends Serenity.EntityDialog<WorkFlowActionRow, any> {
        protected getFormKey() { return WorkFlowActionForm.formKey; }
        protected getIdProperty() { return WorkFlowActionRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowActionRow.localTextPrefix; }
        protected getNameProperty() { return WorkFlowActionRow.nameProperty; }
        protected getService() { return WorkFlowActionService.baseUrl; }

        protected form = new WorkFlowActionForm(this.idPrefix);
    }
}