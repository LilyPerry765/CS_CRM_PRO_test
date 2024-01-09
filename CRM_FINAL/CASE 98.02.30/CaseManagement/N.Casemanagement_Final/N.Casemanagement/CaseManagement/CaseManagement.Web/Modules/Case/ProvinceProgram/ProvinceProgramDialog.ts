
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ProvinceProgramDialog extends Serenity.EntityDialog<ProvinceProgramRow, any> {
        protected getFormKey() { return ProvinceProgramForm.formKey; }
        protected getIdProperty() { return ProvinceProgramRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceProgramRow.localTextPrefix; }
        protected getService() { return ProvinceProgramService.baseUrl; }

        protected form = new ProvinceProgramForm(this.idPrefix);
    }
}