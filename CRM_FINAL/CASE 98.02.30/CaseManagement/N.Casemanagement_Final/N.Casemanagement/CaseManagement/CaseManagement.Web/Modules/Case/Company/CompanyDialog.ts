
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class CompanyDialog extends Serenity.EntityDialog<CompanyRow, any> {
        protected getFormKey() { return CompanyForm.formKey; }
        protected getIdProperty() { return CompanyRow.idProperty; }
        protected getLocalTextPrefix() { return CompanyRow.localTextPrefix; }
        protected getNameProperty() { return CompanyRow.nameProperty; }
        protected getService() { return CompanyService.baseUrl; }

        protected form = new CompanyForm(this.idPrefix);
    }
}