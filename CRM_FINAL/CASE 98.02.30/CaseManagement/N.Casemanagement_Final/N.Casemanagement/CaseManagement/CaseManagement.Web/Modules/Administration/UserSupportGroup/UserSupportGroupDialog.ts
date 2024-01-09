
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class UserSupportGroupDialog extends Serenity.EntityDialog<UserSupportGroupRow, any> {
        protected getFormKey() { return UserSupportGroupForm.formKey; }
        protected getIdProperty() { return UserSupportGroupRow.idProperty; }
        protected getLocalTextPrefix() { return UserSupportGroupRow.localTextPrefix; }
        protected getService() { return UserSupportGroupService.baseUrl; }

        protected form = new UserSupportGroupForm(this.idPrefix);
    }
}