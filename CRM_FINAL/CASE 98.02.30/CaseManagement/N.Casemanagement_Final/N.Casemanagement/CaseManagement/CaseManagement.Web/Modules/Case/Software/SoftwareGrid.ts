
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class SoftwareGrid extends Serenity.EntityGrid<SoftwareRow, any> {
        protected getColumnsKey() { return 'Case.Software'; }
        protected getDialogType() { return SoftwareDialog; }
        protected getIdProperty() { return SoftwareRow.idProperty; }
        protected getLocalTextPrefix() { return SoftwareRow.localTextPrefix; }
        protected getService() { return SoftwareService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}