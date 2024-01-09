
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    export class CalendarEventGrid extends Serenity.EntityGrid<CalendarEventRow, any> {
        protected getColumnsKey() { return 'Administration.CalendarEvent'; }
        protected getDialogType() { return CalendarEventDialog; }
        protected getIdProperty() { return CalendarEventRow.idProperty; }
        protected getLocalTextPrefix() { return CalendarEventRow.localTextPrefix; }
        protected getService() { return CalendarEventService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}