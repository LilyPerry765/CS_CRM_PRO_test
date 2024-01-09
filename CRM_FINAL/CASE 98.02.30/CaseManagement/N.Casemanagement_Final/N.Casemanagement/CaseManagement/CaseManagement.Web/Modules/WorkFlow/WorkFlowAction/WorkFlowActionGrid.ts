
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    export class WorkFlowActionGrid extends Serenity.EntityGrid<WorkFlowActionRow, any> {
        protected getColumnsKey() { return 'WorkFlow.WorkFlowAction'; }
        protected getDialogType() { return WorkFlowActionDialog; }
        protected getIdProperty() { return WorkFlowActionRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowActionRow.localTextPrefix; }
        protected getService() { return WorkFlowActionService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}