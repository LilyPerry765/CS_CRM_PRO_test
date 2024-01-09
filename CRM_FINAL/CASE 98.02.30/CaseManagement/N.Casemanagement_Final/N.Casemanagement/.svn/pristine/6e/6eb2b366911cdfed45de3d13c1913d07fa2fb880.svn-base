
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ProvinceProgramLogDialog extends Serenity.EntityDialog<ProvinceProgramLogRow, any> {
        protected getFormKey() { return ProvinceProgramLogForm.formKey; }
        protected getIdProperty() { return ProvinceProgramLogRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceProgramLogRow.localTextPrefix; }
        protected getService() { return ProvinceProgramLogService.baseUrl; }

        protected form = new ProvinceProgramLogForm(this.idPrefix);
    }
}