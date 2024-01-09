
namespace CaseManagement.WorkFlow {
    
    @Serenity.Decorators.registerClass()
    export class WorkFlowStatusTypeGrid extends Serenity.EntityGrid<WorkFlowStatusTypeRow, any> {
        protected getColumnsKey() { return 'WorkFlow.WorkFlowStatusType'; }
        protected getDialogType() { return WorkFlowStatusTypeDialog; }
        protected getIdProperty() { return WorkFlowStatusTypeRow.idProperty; }
        protected getLocalTextPrefix() { return WorkFlowStatusTypeRow.localTextPrefix; }
        protected getService() { return WorkFlowStatusTypeService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}