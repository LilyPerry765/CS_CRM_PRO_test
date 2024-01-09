
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SwitchTransitDialog extends Serenity.EntityDialog<SwitchTransitRow, any> {
        protected getFormKey() { return SwitchTransitForm.formKey; }
        protected getIdProperty() { return SwitchTransitRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchTransitRow.localTextPrefix; }
        protected getNameProperty() { return SwitchTransitRow.nameProperty; }
        protected getService() { return SwitchTransitService.baseUrl; }

        protected form = new SwitchTransitForm(this.idPrefix);
    }
}