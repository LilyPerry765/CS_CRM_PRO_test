
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    export class ActivityRequestTechnicalGrid extends Serenity.EntityGrid<ActivityRequestTechnicalRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestTechnical'; }
        protected getDialogType() { return ActivityRequestTechnicalDialog; }
        protected getIdProperty() { return ActivityRequestTechnicalRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestTechnicalRow.localTextPrefix; }
        protected getService() { return ActivityRequestTechnicalService.baseUrl; }

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
                service: ActivityRequestTechnicalService.baseUrl + '/ListExcel',
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

                    if (IncomeFlow == null) { IncomeFlow = ""; } //console.log(IncomeFlow);


                   window.location.href = "../Common/ActivityRequestTechnicalPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                        + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                }
            });

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