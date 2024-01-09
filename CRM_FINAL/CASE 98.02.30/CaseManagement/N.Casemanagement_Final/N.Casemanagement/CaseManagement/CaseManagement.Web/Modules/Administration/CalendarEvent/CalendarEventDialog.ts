
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class CalendarEventDialog extends Serenity.EntityDialog<CalendarEventRow, any> {
        protected getFormKey() { return CalendarEventForm.formKey; }
        protected getIdProperty() { return CalendarEventRow.idProperty; }
        protected getLocalTextPrefix() { return CalendarEventRow.localTextPrefix; }
        protected getNameProperty() { return CalendarEventRow.nameProperty; }
        protected getService() { return CalendarEventService.baseUrl; }

        protected form = new CalendarEventForm(this.idPrefix);
    }
}