
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ProvinceCompanySoftwareDialog extends Serenity.EntityDialog<ProvinceCompanySoftwareRow, any> {
        protected getFormKey() { return ProvinceCompanySoftwareForm.formKey; }
        protected getIdProperty() { return ProvinceCompanySoftwareRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceCompanySoftwareRow.localTextPrefix; }
        protected getService() { return ProvinceCompanySoftwareService.baseUrl; }

        protected form = new ProvinceCompanySoftwareForm(this.idPrefix);
    }
}