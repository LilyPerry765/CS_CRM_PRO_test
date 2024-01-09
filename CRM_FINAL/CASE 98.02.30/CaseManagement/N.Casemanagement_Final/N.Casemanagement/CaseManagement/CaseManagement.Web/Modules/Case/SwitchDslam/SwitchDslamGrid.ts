
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class SwitchDslamGrid extends Serenity.EntityGrid<SwitchDslamRow, any> {
        protected getColumnsKey() { return 'Case.SwitchDslam'; }
        protected getDialogType() { return SwitchDslamDialog; }
        protected getIdProperty() { return SwitchDslamRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchDslamRow.localTextPrefix; }
        protected getService() { return SwitchDslamService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: SwitchDslamService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            return buttons;
        }
    }
}