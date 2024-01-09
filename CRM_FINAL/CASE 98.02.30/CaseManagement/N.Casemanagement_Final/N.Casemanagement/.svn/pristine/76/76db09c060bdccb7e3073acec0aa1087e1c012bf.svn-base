
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class YearDialog extends Serenity.EntityDialog<YearRow, any> {
        protected getFormKey() { return YearForm.formKey; }
        protected getIdProperty() { return YearRow.idProperty; }
        protected getLocalTextPrefix() { return YearRow.localTextPrefix; }
        protected getNameProperty() { return YearRow.nameProperty; }
        protected getService() { return YearService.baseUrl; }

        protected form = new YearForm(this.idPrefix);
    }
}