
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityGroupGrid extends Serenity.EntityGrid<ActivityGroupRow, any> {
        protected getColumnsKey() { return 'Case.ActivityGroup'; }
        protected getDialogType() { return ActivityGroupDialog; }
        protected getIdProperty() { return ActivityGroupRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityGroupRow.localTextPrefix; }
        protected getService() { return ActivityGroupService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: ActivityGroupService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            return buttons;
        }
    }
}