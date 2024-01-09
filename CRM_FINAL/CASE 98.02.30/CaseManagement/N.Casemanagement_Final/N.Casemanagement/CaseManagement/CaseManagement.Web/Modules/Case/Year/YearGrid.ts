
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class YearGrid extends Serenity.EntityGrid<YearRow, any> {
        protected getColumnsKey() { return 'Case.Year'; }
        protected getDialogType() { return YearDialog; }
        protected getIdProperty() { return YearRow.idProperty; }
        protected getLocalTextPrefix() { return YearRow.localTextPrefix; }
        protected getService() { return YearService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}