
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestGrid extends Serenity.EntityGrid<ActivityRequestRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequest'; }
        protected getDialogType() { return ActivityRequestDialog; }
        protected getIdProperty() { return ActivityRequestRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestRow.localTextPrefix; }
        protected getService() { return ActivityRequestService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({ 
                grid: this,
                service: ActivityRequestService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            return buttons;
        }

        protected getItemCssClass(item: Case.ActivityRequestRow, index: number): string {
            let klass: string = "";           

           // if (item.IsRejected == true) 
           //     klass += " actionReject";            

            if (item.ConfirmTypeID == 2)
                klass += " financialConfirm";

            return Q.trimToNull(klass);
        }
    }
}