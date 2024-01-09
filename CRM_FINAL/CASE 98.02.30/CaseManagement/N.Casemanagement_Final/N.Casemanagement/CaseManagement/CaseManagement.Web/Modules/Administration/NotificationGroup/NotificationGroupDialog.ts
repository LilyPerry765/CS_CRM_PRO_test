
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class NotificationGroupDialog extends Serenity.EntityDialog<NotificationGroupRow, any> {
        protected getFormKey() { return NotificationGroupForm.formKey; }
        protected getIdProperty() { return NotificationGroupRow.idProperty; }
        protected getLocalTextPrefix() { return NotificationGroupRow.localTextPrefix; }
        protected getNameProperty() { return NotificationGroupRow.nameProperty; }
        protected getService() { return NotificationGroupService.baseUrl; }

        protected form = new NotificationGroupForm(this.idPrefix);
    }
}