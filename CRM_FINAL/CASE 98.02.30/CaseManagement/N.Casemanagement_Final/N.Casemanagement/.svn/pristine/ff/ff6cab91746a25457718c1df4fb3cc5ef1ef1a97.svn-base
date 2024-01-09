
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestDeleteGrid extends Serenity.EntityGrid<ActivityRequestDeleteRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestDelete'; }
        protected getDialogType() { return ActivityRequestDeleteDialog; }
        protected getIdProperty() { return ActivityRequestDeleteRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestDeleteRow.localTextPrefix; }
        protected getService() { return ActivityRequestDeleteService.baseUrl; }

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