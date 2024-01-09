
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class CycleDialog extends Serenity.EntityDialog<CycleRow, any> {
        protected getFormKey() { return CycleForm.formKey; }
        protected getIdProperty() { return CycleRow.idProperty; }
        protected getLocalTextPrefix() { return CycleRow.localTextPrefix; }
        protected getNameProperty() { return CycleRow.nameProperty; }
        protected getService() { return CycleService.baseUrl; }

        protected form = new CycleForm(this.idPrefix);
    }
}