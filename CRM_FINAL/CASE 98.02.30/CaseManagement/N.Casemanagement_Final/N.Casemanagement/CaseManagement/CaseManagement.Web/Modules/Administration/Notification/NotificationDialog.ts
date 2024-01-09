
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class NotificationDialog extends Serenity.EntityDialog<NotificationRow, any> {
        protected getFormKey() { return NotificationForm.formKey; }
        protected getIdProperty() { return NotificationRow.idProperty; }
        protected getLocalTextPrefix() { return NotificationRow.localTextPrefix; }
        protected getNameProperty() { return NotificationRow.nameProperty; }
        protected getService() { return NotificationService.baseUrl; }

        protected form = new NotificationForm(this.idPrefix);
    }
}