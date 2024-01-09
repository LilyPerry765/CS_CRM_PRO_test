
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class PmoLevelDialog extends Serenity.EntityDialog<PmoLevelRow, any> {
        protected getFormKey() { return PmoLevelForm.formKey; }
        protected getIdProperty() { return PmoLevelRow.idProperty; }
        protected getLocalTextPrefix() { return PmoLevelRow.localTextPrefix; }
        protected getNameProperty() { return PmoLevelRow.nameProperty; }
        protected getService() { return PmoLevelService.baseUrl; }

        protected form = new PmoLevelForm(this.idPrefix);
    }
}