
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    export class NotificationGrid extends Serenity.EntityGrid<NotificationRow, any> {
        protected getColumnsKey() { return 'Administration.Notification'; }
        protected getDialogType() { return NotificationDialog; }
        protected getIdProperty() { return NotificationRow.idProperty; }
        protected getLocalTextPrefix() { return NotificationRow.localTextPrefix; }
        protected getService() { return NotificationService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}