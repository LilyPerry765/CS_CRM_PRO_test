
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class CycleGrid extends Serenity.EntityGrid<CycleRow, any> {
        protected getColumnsKey() { return 'Case.Cycle'; }
        protected getDialogType() { return CycleDialog; }
        protected getIdProperty() { return CycleRow.idProperty; }
        protected getLocalTextPrefix() { return CycleRow.localTextPrefix; }
        protected getService() { return CycleService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}