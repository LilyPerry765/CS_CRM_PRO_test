
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SwitchDslamDialog extends Serenity.EntityDialog<SwitchDslamRow, any> {
        protected getFormKey() { return SwitchDslamForm.formKey; }
        protected getIdProperty() { return SwitchDslamRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchDslamRow.localTextPrefix; }
        protected getNameProperty() { return SwitchDslamRow.nameProperty; }
        protected getService() { return SwitchDslamService.baseUrl; }

        protected form = new SwitchDslamForm(this.idPrefix);
    }
}