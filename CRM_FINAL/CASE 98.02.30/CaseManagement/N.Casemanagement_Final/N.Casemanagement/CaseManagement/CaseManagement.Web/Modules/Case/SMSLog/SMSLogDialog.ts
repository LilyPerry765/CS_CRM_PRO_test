
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SMSLogDialog extends Serenity.EntityDialog<SMSLogRow, any> {
        protected getFormKey() { return SMSLogForm.formKey; }
        protected getIdProperty() { return SMSLogRow.idProperty; }
        protected getLocalTextPrefix() { return SMSLogRow.localTextPrefix; }
        protected getNameProperty() { return SMSLogRow.nameProperty; }
        protected getService() { return SMSLogService.baseUrl; }

        protected form = new SMSLogForm(this.idPrefix);
    }
}