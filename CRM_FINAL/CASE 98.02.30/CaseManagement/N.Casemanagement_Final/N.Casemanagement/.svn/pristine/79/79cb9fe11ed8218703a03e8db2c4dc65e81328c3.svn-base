
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class RiskLevelGrid extends Serenity.EntityGrid<RiskLevelRow, any> {
        protected getColumnsKey() { return 'Case.RiskLevel'; }
        protected getDialogType() { return RiskLevelDialog; }
        protected getIdProperty() { return RiskLevelRow.idProperty; }
        protected getLocalTextPrefix() { return RiskLevelRow.localTextPrefix; }
        protected getService() { return RiskLevelService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}