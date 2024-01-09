
namespace CaseManagement.Messaging {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SentDialog extends Serenity.EntityDialog<SentRow, any> {
        protected getFormKey() { return SentForm.formKey; }
        protected getIdProperty() { return SentRow.idProperty; }
        protected getLocalTextPrefix() { return SentRow.localTextPrefix; }
        protected getService() { return SentService.baseUrl; }

        protected form = new SentForm(this.idPrefix);

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