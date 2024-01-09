
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class NewMessageDialog extends Serenity.EntityDialog<NewMessageRow, any> {
        protected getFormKey() { return NewMessageForm.formKey; }
        protected getIdProperty() { return NewMessageRow.idProperty; }
        protected getLocalTextPrefix() { return NewMessageRow.localTextPrefix; }
        protected getNameProperty() { return NewMessageRow.nameProperty; }
        protected getService() { return NewMessageService.baseUrl; }

        protected form = new NewMessageForm(this.idPrefix);
    }
}