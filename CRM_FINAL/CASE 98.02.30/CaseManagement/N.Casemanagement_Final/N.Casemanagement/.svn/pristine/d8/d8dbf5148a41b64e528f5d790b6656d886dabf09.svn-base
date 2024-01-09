
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class RepeatTermDialog extends Serenity.EntityDialog<RepeatTermRow, any> {
        protected getFormKey() { return RepeatTermForm.formKey; }
        protected getIdProperty() { return RepeatTermRow.idProperty; }
        protected getLocalTextPrefix() { return RepeatTermRow.localTextPrefix; }
        protected getNameProperty() { return RepeatTermRow.nameProperty; }
        protected getService() { return RepeatTermService.baseUrl; }

        protected form = new RepeatTermForm(this.idPrefix);
    }
}