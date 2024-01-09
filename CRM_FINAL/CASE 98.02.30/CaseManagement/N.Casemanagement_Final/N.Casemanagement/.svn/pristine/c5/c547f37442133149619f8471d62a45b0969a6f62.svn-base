
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestConfirmAdminGrid extends Serenity.EntityGrid<ActivityRequestConfirmAdminRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestConfirmAdmin'; }
        protected getDialogType() { return ActivityRequestConfirmAdminDialog; }
        protected getIdProperty() { return ActivityRequestConfirmAdminRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestConfirmAdminRow.localTextPrefix; }
        protected getService() { return ActivityRequestConfirmAdminService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            // need to register this plugin for grouping or you'll have errors
            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('CycleCost'),
                    new Slick.Aggregators.Sum('DelayedCost'),
                    new Slick.Aggregators.Sum('TotalLeakage'),
                    new Slick.Aggregators.Sum('RecoverableLeakage'),
                    new Slick.Aggregators.Sum('Recovered')
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
                service: ActivityRequestConfirmService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var AllFilters = document.getElementsByClassName("quick-filter-item");

                    var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);

                    var CreateTime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(CreateTime_Start);

                    var CreateTime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(CreateTime_End);

                    var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);

                    var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);

                    //var EndTime_Start = AllFilters[5].getElementsByTagName('input')[0].value; //console.log(EndTime_Start);

                    //var EndTime_End = AllFilters[5].getElementsByTagName('input')[1].value; //console.log(AllFilters[5].getElementsByTagName('input')[1].innerHTML);

                    var Province = document.getElementById("select2-chosen-1").innerHTML;


                    if (Province == null) { Province = ""; }


                    var Cycle = document.getElementById("select2-chosen-2").innerHTML;

                    if (Cycle == null) { Cycle = ""; }

                   // window.location.href = "../Common/ActivityRequestConfirmPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                     //  + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle;

                    window.location.href = "../Common/ActivityRequestConfirmPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                        + "&DiscoverTime_End=" + DiscoverTime_End + "&CreateTime_Start=" + CreateTime_Start + "&CreateTime_End=" + CreateTime_End + "&Province=" + Province + "&Cycle=" + Cycle;
                }
            });

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        } 
    }
}