
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestDeleteDialog extends Serenity.EntityDialog<ActivityRequestDeleteRow, any> {
        protected getFormKey() { return ActivityRequestDeleteForm.formKey; }
        protected getIdProperty() { return ActivityRequestDeleteRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestDeleteRow.localTextPrefix; }
        protected getNameProperty() { return ActivityRequestDeleteRow.nameProperty; }
        protected getService() { return ActivityRequestDeleteService.baseUrl; }

        protected form = new ActivityRequestDeleteForm(this.idPrefix);

        protected getToolbarButtons(): Serenity.ToolButton[] {
            let buttons = super.getToolbarButtons();

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "save-and-close-button"), 1);
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "apply-changes-button"), 1);

            return buttons;
        }

        protected updateInterface(): void {

            super.updateInterface();

            Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
            this.element.find('sup').hide();
        }
    }
}