
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    export class WorkFlowStepGrid extends Serenity.EntityGrid<WorkFlowStepRow, any> {
        protected getColumnsKey() { return 'WorkFlow.WorkFlowStep'; }
        protected getDialogType() { return WorkFlowStepDialog; }
        protected getIdProperty() { return WorkFlowStepRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowStepRow.localTextPrefix; }
        protected getService() { return WorkFlowStepService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}