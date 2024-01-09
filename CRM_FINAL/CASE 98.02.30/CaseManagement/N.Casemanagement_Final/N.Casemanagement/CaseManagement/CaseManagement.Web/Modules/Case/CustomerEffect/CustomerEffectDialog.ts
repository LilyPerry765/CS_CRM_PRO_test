
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class CustomerEffectDialog extends Serenity.EntityDialog<CustomerEffectRow, any> {
        protected getFormKey() { return CustomerEffectForm.formKey; }
        protected getIdProperty() { return CustomerEffectRow.idProperty; }
        protected getLocalTextPrefix() { return CustomerEffectRow.localTextPrefix; }
        protected getNameProperty() { return CustomerEffectRow.nameProperty; }
        protected getService() { return CustomerEffectService.baseUrl; }

        protected form = new CustomerEffectForm(this.idPrefix);
    }
}