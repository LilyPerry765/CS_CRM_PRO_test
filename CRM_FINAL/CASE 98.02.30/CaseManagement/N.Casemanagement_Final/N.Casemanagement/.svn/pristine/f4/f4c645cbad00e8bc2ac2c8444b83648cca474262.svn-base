
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class PmoLevelGrid extends Serenity.EntityGrid<PmoLevelRow, any> {
        protected getColumnsKey() { return 'Case.PmoLevel'; }
        protected getDialogType() { return PmoLevelDialog; }
        protected getIdProperty() { return PmoLevelRow.idProperty; }
        protected getLocalTextPrefix() { return PmoLevelRow.localTextPrefix; }
        protected getService() { return PmoLevelService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}