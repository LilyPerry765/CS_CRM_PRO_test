
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class SwitchGrid extends Serenity.EntityGrid<SwitchRow, any> {
        protected getColumnsKey() { return 'Case.Switch'; }
        protected getDialogType() { return SwitchDialog; }
        protected getIdProperty() { return SwitchRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchRow.localTextPrefix; }
        protected getService() { return SwitchService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: SwitchService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            return buttons;
        }
    }
}