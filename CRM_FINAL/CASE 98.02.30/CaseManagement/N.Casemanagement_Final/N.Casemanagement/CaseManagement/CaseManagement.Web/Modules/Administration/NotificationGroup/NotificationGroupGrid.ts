
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    export class NotificationGroupGrid extends Serenity.EntityGrid<NotificationGroupRow, any> {
        protected getColumnsKey() { return 'Administration.NotificationGroup'; }
        protected getDialogType() { return NotificationGroupDialog; }
        protected getIdProperty() { return NotificationGroupRow.idProperty; }
        protected getLocalTextPrefix() { return NotificationGroupRow.localTextPrefix; }
        protected getService() { return NotificationGroupService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}