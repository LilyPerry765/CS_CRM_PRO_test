
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityGrid extends Serenity.EntityGrid<ActivityRow, any> {
        protected getColumnsKey() { return 'Case.Activity'; }
        protected getDialogType() { return ActivityDialog; }
        protected getIdProperty() { return ActivityRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRow.localTextPrefix; }
        protected getService() { return ActivityService.baseUrl; }

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