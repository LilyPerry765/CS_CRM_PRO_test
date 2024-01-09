
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityHelpGrid extends Serenity.EntityGrid<ActivityHelpRow, any> {
        protected getColumnsKey() { return 'Case.ActivityHelp'; }
        protected getDialogType() { return ActivityHelpDialog; }
        protected getIdProperty() { return ActivityHelpRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityHelpRow.localTextPrefix; }
        protected getService() { return ActivityHelpService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: ActivityService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }
    }
}