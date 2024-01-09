
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class CustomerEffectGrid extends Serenity.EntityGrid<CustomerEffectRow, any> {
        protected getColumnsKey() { return 'Case.CustomerEffect'; }
        protected getDialogType() { return CustomerEffectDialog; }
        protected getIdProperty() { return CustomerEffectRow.idProperty; }
        protected getLocalTextPrefix() { return CustomerEffectRow.localTextPrefix; }
        protected getService() { return CustomerEffectService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}