
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class SwitchTransitGrid extends Serenity.EntityGrid<SwitchTransitRow, any> {
        protected getColumnsKey() { return 'Case.SwitchTransit'; }
        protected getDialogType() { return SwitchTransitDialog; }
        protected getIdProperty() { return SwitchTransitRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchTransitRow.localTextPrefix; }
        protected getService() { return SwitchTransitService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: SwitchTransitService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            return buttons;
        }
    }
}