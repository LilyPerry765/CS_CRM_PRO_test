
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ProvinceGrid extends Serenity.EntityGrid<ProvinceRow, any> {
        protected getColumnsKey() { return 'Case.Province'; }
        protected getDialogType() { return ProvinceDialog; }
        protected getIdProperty() { return ProvinceRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceRow.localTextPrefix; }
        protected getService() { return ProvinceService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            //var buttons = super.getButtons();

            //buttons.push(Common.ExcelExportHelper.createToolButton({
            //    grid: this,
            //    service: ProvinceService.baseUrl + '/ListExcel',
            //    onViewSubmit: () => this.onViewSubmit(),
            //    separator: true
            //}));

            //return buttons;

            return [{
                title: 'دسته بندی سرگروه',
                cssClass: 'expand-all-button',
                onClick: () => this.view.setGrouping(
                    [{
                        getter: 'LeaderName'
                    }])
                 },
                 {
                    title: 'بدون گروه بندی',
                    cssClass: 'collapse-all-button',
                    onClick: () => this.view.setGrouping([])
                }];
        }
    }
}