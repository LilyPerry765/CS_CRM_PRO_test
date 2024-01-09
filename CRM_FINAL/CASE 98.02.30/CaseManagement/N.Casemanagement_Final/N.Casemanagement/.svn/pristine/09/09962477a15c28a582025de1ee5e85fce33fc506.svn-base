
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class RepeatTermGrid extends Serenity.EntityGrid<RepeatTermRow, any> {
        protected getColumnsKey() { return 'Case.RepeatTerm'; }
        protected getDialogType() { return RepeatTermDialog; }
        protected getIdProperty() { return RepeatTermRow.idProperty; }
        protected getLocalTextPrefix() { return RepeatTermRow.localTextPrefix; }
        protected getService() { return RepeatTermService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}