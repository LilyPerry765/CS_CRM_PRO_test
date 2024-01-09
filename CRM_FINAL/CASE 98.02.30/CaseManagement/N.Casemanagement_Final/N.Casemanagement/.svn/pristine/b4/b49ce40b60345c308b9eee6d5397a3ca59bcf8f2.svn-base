
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class InboxDialog extends Serenity.EntityDialog<InboxRow, any> {
        protected getFormKey() { return InboxForm.formKey; }
        protected getIdProperty() { return InboxRow.idProperty; }
        protected getLocalTextPrefix() { return InboxRow.localTextPrefix; }
        protected getService() { return InboxService.baseUrl; }

        protected form = new InboxForm(this.idPrefix);
        
        protected getToolbarButtons(): Serenity.ToolButton[] {
            let buttons = super.getToolbarButtons();
      
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "save-and-close-button"), 1);
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "apply-changes-button"), 1);
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "delete-button"), 1);
      
            return buttons;
        }
      
        protected updateInterface(): void {
      
            super.updateInterface();
           Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
      
      
            this.element.find('sup').hide();
            this.deleteButton.hide();
        }
    }
}