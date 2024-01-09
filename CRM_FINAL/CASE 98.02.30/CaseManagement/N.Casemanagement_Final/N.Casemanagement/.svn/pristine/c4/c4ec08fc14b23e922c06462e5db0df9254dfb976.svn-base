
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class IncomeFlowGrid extends Serenity.EntityGrid<IncomeFlowRow, any> {
        protected getColumnsKey() { return 'Case.IncomeFlow'; }
        protected getDialogType() { return IncomeFlowDialog; }
        protected getIdProperty() { return IncomeFlowRow.idProperty; }
        protected getLocalTextPrefix() { return IncomeFlowRow.localTextPrefix; }
        protected getService() { return IncomeFlowService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}