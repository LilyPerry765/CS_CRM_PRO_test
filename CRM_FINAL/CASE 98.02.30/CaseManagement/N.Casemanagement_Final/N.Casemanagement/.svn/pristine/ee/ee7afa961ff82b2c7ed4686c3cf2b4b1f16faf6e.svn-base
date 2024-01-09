
namespace CaseManagement.StimulReport {

    @Serenity.Decorators.registerClass()
    export class ActivityRequestDetailGrid extends Serenity.EntityGrid<ActivityRequestDetailRow, any> {
        protected getColumnsKey() { return 'StimulReport.ActivityRequestDetail'; }
        protected getDialogType() { return ActivityRequestDetailDialog; }
        protected getIdProperty() { return ActivityRequestDetailRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestDetailRow.localTextPrefix; }
        protected getService() { return ActivityRequestDetailService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected createSlickGrid() {
            var grid = super.createSlickGrid();
      
            // need to register this plugin for grouping or you'll have errors
            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
      
            this.view.setSummaryOptions({
                aggregators: [
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
                service: ActivityRequestDetailService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var AllFilters = document.getElementsByClassName("quick-filter-item");

                    var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);

                    var CreateTime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);

                    var CreateTime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);


                    var Province = document.getElementById("select2-chosen-1").innerHTML;


                    if (Province == null) { Province = ""; } //console.log(Province);


                    var Cycle = document.getElementById("select2-chosen-2").innerHTML;
   
                    if (Cycle == null) { Cycle = ""; } //console.log(Cycle);

                    window.location.href = "../Common/ActivityRequestDetailPrint?ActivityCode=" + ActivityCode + "&CreateTime_Start=" + CreateTime_Start
                        + "&CreateTime_End=" + CreateTime_End + "&Province=" + Province + "&Cycle=" + Cycle;
                }
            });

     
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);
     
            return buttons;
        }
    }
}