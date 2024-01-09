
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityHelpDialog extends Serenity.EntityDialog<ActivityHelpRow, any> {
        protected getFormKey() { return ActivityHelpForm.formKey; }
        protected getIdProperty() { return ActivityHelpRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityHelpRow.localTextPrefix; }
        protected getNameProperty() { return ActivityHelpRow.nameProperty; }
        protected getService() { return ActivityHelpService.baseUrl; }
        
        protected form = new ActivityHelpForm(this.idPrefix);

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

    export interface ActivityHelpDialogOptions {
        activityID?: number;
    }
}