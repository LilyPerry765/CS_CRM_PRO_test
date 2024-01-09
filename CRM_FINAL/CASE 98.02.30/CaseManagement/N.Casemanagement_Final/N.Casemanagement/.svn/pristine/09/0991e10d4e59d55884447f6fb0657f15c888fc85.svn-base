
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    export class UserSupportGroupGrid extends Serenity.EntityGrid<UserSupportGroupRow, any> {
        protected getColumnsKey() { return 'Administration.UserSupportGroup'; }
        protected getDialogType() { return UserSupportGroupDialog; }
        protected getIdProperty() { return UserSupportGroupRow.idProperty; }
        protected getLocalTextPrefix() { return UserSupportGroupRow.localTextPrefix; }
        protected getService() { return UserSupportGroupService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}