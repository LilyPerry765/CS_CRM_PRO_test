
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class WorkFlowRuleDialog extends Serenity.EntityDialog<WorkFlowRuleRow, any> {
        protected getFormKey() { return WorkFlowRuleForm.formKey; }
        protected getIdProperty() { return WorkFlowRuleRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowRuleRow.localTextPrefix; }
        protected getService() { return WorkFlowRuleService.baseUrl; }

        protected form = new WorkFlowRuleForm(this.idPrefix);
    }
}