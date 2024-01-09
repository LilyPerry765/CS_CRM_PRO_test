
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    export class SentGrid extends Serenity.EntityGrid<SentRow, any> {
        protected getColumnsKey() { return 'Messaging.Sent'; }
        protected getDialogType() { return SentDialog; }
        protected getIdProperty() { return SentRow.idProperty; }
        protected getLocalTextPrefix() { return SentRow.localTextPrefix; }
        protected getService() { return SentService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }
    }
}