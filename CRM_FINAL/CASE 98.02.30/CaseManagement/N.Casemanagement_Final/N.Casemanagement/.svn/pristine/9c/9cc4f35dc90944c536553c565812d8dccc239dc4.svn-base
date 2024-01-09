
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestFinancialGrid extends Serenity.EntityGrid<ActivityRequestFinancialRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestFinancial'; }
        protected getDialogType() { return ActivityRequestFinancialDialog; }
        protected getIdProperty() { return ActivityRequestFinancialRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestFinancialRow.localTextPrefix; }
        protected getService() { return ActivityRequestFinancialService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            // need to register this plugin for grouping or you'll have errors
            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('TotalLeakage')
                ]
            });

            return grid;
        }

        protected getSlickOptions() {
            var opt = super.getSlickOptions();
            opt.showFooterRow = true;
            return opt;
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: ActivityRequestFinancialService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var AllFilters = document.getElementsByClassName("quick-filter-item");

                    var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);

                    var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);

                    var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);


                    var Province = document.getElementById("select2-chosen-1").innerHTML;


                    if (Province == null) { Province = ""; } //console.log(Province);


                    var Cycle = document.getElementById("select2-chosen-2").innerHTML;

                    if (Cycle == null) { Cycle = ""; } //console.log(Cycle);


                    var IncomeFlow = document.getElementById("select2-chosen-3").innerHTML;

                    if (IncomeFlow == null) { IncomeFlow = ""; }// console.log(IncomeFlow);


                    window.location.href = "../Common/ActivityRequestFinancialPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                        + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                }
            });



            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }

        protected getItemCssClass(item: Case.ActivityRequestRow, index: number): string {
            let klass: string = "";

            if (item.IsRejected == true)
                klass += "actionReject";

            return Q.trimToNull(klass);
        }
    }
}