
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    export class InboxGrid extends Serenity.EntityGrid<InboxRow, any> {
        protected getColumnsKey() { return 'Messaging.Inbox'; }
        protected getDialogType() { return InboxDialog; }
        protected getIdProperty() { return InboxRow.idProperty; }
        protected getLocalTextPrefix() { return InboxRow.localTextPrefix; }
        protected getService() { return InboxService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }

        protected getItemCssClass(item: Messaging.InboxRow, index: number): string {
            let klass: string = "";

            if ( (item.Seen == null) || (item.Seen == false))
                klass += "actionNotSeen";

            return Q.trimToNull(klass);
        }
    }
}