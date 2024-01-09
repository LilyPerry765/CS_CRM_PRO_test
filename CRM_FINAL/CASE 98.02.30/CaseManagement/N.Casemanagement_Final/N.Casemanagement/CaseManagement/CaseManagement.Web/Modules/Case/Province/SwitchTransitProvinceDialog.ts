
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SwitchTransitProvinceDialog extends Serenity.EntityDialog<SwitchTransitProvinceRow, any> {
        protected getFormKey() { return SwitchTransitProvinceForm.formKey; }
        protected getIdProperty() { return SwitchTransitProvinceRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchTransitProvinceRow.localTextPrefix; }
        protected getNameProperty() { return SwitchTransitProvinceRow.nameProperty; }
        protected getService() { return SwitchTransitProvinceService.baseUrl; }

        protected form = new SwitchTransitProvinceForm(this.idPrefix);

        updateInterface() {
            super.updateInterface();

            Serenity.EditorUtils.setReadOnly(this.form.ProvinceId, false);
        }
    }
} 