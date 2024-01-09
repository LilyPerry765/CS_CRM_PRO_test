
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SwitchDialog extends Serenity.EntityDialog<SwitchRow, any> {
        protected getFormKey() { return SwitchForm.formKey; }
        protected getIdProperty() { return SwitchRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchRow.localTextPrefix; }
        protected getNameProperty() { return SwitchRow.nameProperty; }
        protected getService() { return SwitchService.baseUrl; }

        protected form = new SwitchForm(this.idPrefix);
    }
}