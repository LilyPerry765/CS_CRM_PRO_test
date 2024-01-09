
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class WorkFlowStatusTypeDialog extends Serenity.EntityDialog<WorkFlowStatusTypeRow, any> {
        protected getFormKey() { return WorkFlowStatusTypeForm.formKey; }
        protected getIdProperty() { return WorkFlowStatusTypeRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowStatusTypeRow.localTextPrefix; }
        protected getNameProperty() { return WorkFlowStatusTypeRow.nameProperty; }
        protected getService() { return WorkFlowStatusTypeService.baseUrl; }

        protected form = new WorkFlowStatusTypeForm(this.idPrefix);
    }
}