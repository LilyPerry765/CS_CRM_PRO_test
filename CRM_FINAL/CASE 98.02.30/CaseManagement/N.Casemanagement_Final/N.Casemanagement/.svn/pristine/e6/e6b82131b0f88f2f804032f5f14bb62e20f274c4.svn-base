
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SoftwareDialog extends Serenity.EntityDialog<SoftwareRow, any> {
        protected getFormKey() { return SoftwareForm.formKey; }
        protected getIdProperty() { return SoftwareRow.idProperty; }
        protected getLocalTextPrefix() { return SoftwareRow.localTextPrefix; }
        protected getNameProperty() { return SoftwareRow.nameProperty; }
        protected getService() { return SoftwareService.baseUrl; }

        protected form = new SoftwareForm(this.idPrefix);
    }
}