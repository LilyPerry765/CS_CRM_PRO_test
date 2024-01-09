
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    export class NewMessageGrid extends Serenity.EntityGrid<NewMessageRow, any> {
        protected getColumnsKey() { return 'Messaging.NewMessage'; }
        protected getDialogType() { return NewMessageDialog; }
        protected getIdProperty() { return NewMessageRow.idProperty; }
        protected getLocalTextPrefix() { return NewMessageRow.localTextPrefix; }
        protected getService() { return NewMessageService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}