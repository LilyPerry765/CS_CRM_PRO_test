
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class RiskLevelDialog extends Serenity.EntityDialog<RiskLevelRow, any> {
        protected getFormKey() { return RiskLevelForm.formKey; }
        protected getIdProperty() { return RiskLevelRow.idProperty; }
        protected getLocalTextPrefix() { return RiskLevelRow.localTextPrefix; }
        protected getNameProperty() { return RiskLevelRow.nameProperty; }
        protected getService() { return RiskLevelService.baseUrl; }

        protected form = new RiskLevelForm(this.idPrefix);
    }
}