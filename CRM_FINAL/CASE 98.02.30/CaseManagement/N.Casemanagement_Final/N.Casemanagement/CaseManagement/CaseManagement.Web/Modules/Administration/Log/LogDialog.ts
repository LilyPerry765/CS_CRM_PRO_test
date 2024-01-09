
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class LogDialog extends Serenity.EntityDialog<LogRow, any> {
        protected getFormKey() { return LogForm.formKey; }
        protected getIdProperty() { return LogRow.idProperty; }
        protected getLocalTextPrefix() { return LogRow.localTextPrefix; }
        protected getNameProperty() { return LogRow.nameProperty; }
        protected getService() { return LogService.baseUrl; }

        protected form = new LogForm(this.idPrefix);
    }
}