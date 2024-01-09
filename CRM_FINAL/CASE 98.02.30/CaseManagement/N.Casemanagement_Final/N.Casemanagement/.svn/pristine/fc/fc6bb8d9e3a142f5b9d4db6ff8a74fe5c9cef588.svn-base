
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ProvinceProgramLogGrid extends Serenity.EntityGrid<ProvinceProgramLogRow, any> {
        protected getColumnsKey() { return 'Case.ProvinceProgramLog'; }
        protected getDialogType() { return ProvinceProgramLogDialog; }
        protected getIdProperty() { return ProvinceProgramLogRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceProgramLogRow.localTextPrefix; }
        protected getService() { return ProvinceProgramLogService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}