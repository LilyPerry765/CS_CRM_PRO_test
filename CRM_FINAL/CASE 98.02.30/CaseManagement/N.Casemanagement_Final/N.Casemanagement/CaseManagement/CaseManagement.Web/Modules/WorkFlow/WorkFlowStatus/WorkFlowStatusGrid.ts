
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    export class WorkFlowStatusGrid extends Serenity.EntityGrid<WorkFlowStatusRow, any> {
        protected getColumnsKey() { return 'WorkFlow.WorkFlowStatus'; }
        protected getDialogType() { return WorkFlowStatusDialog; }
        protected getIdProperty() { return WorkFlowStatusRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowStatusRow.localTextPrefix; }
        protected getService() { return WorkFlowStatusService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}