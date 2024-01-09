
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class VwMessagesDialog extends Serenity.EntityDialog<VwMessagesRow, any> {
        protected getFormKey() { return VwMessagesForm.formKey; }
        protected getLocalTextPrefix() { return VwMessagesRow.localTextPrefix; }
        protected getNameProperty() { return VwMessagesRow.nameProperty; }
        protected getService() { return VwMessagesService.baseUrl; }

        protected form = new VwMessagesForm(this.idPrefix);
    }
}