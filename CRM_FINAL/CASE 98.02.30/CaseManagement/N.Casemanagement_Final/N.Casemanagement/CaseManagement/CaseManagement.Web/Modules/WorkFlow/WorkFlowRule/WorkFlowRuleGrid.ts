
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    export class WorkFlowRuleGrid extends Serenity.EntityGrid<WorkFlowRuleRow, any> {
        protected getColumnsKey() { return 'WorkFlow.WorkFlowRule'; }
        protected getDialogType() { return WorkFlowRuleDialog; }
        protected getIdProperty() { return WorkFlowRuleRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowRuleRow.localTextPrefix; }
        protected getService() { return WorkFlowRuleService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}