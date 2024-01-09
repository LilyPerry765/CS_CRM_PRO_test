
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class IncomeFlowDialog extends Serenity.EntityDialog<IncomeFlowRow, any> {
        protected getFormKey() { return IncomeFlowForm.formKey; }
        protected getIdProperty() { return IncomeFlowRow.idProperty; }
        protected getLocalTextPrefix() { return IncomeFlowRow.localTextPrefix; }
        protected getNameProperty() { return IncomeFlowRow.nameProperty; }
        protected getService() { return IncomeFlowService.baseUrl; }

        protected form = new IncomeFlowForm(this.idPrefix);
    }
}