
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    export class VwMessagesGrid extends Serenity.EntityGrid<VwMessagesRow, any> {
        protected getColumnsKey() { return 'Messaging.VwMessages'; }
        protected getDialogType() { return VwMessagesDialog; }
        protected getLocalTextPrefix() { return VwMessagesRow.localTextPrefix; }
        protected getService() { return VwMessagesService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}