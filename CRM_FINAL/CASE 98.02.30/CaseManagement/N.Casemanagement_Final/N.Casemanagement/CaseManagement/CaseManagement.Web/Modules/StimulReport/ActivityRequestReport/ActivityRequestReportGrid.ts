
namespace CaseManagement.StimulReport {
    
    @Serenity.Decorators.registerClass()
    export class ActivityRequestReportGrid extends Serenity.EntityGrid<ActivityRequestReportRow, any> {
        protected getColumnsKey() { return 'StimulReport.ActivityRequestReport'; }
        //protected getDialogType() { return ActivityRequestReportDialog; }
        protected getIdProperty() { return ActivityRequestReportRow.idProperty; }
        protected getService() { return ActivityRequestReportService.baseUrl; }

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

        protected usePager() {
            return false;
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push(Common.ExcelExportHelper.createToolButton({
                grid: this,
                service: ActivityRequestReportService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));

            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var AllFilters = document.getElementsByClassName("quick-filter-item");

                    var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value;// console.log(ActivityCode);

                    var CreatedTime_Start = AllFilters[3].getElementsByTagName('input')[0].value;// console.log(CreatedTime_Start);

                    var CreatedTime_End = AllFilters[3].getElementsByTagName('input')[1].value;// console.log(CreatedTime_End);

                    var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value;// console.log(DiscoverTime_Start);

                    var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value;// console.log(DiscoverTime_End);


                    var TotalLeakage_From = AllFilters[5].getElementsByTagName('input')[0].value;// console.log(TotalLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));

                    var TotalLeakage_To = AllFilters[5].getElementsByTagName('input')[1].value; //console.log(TotalLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));


                    var RecoverableLeakage_From = AllFilters[6].getElementsByTagName('input')[0].value;// console.log(RecoverableLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));

                    var RecoverableLeakage_To = AllFilters[6].getElementsByTagName('input')[1].value;// console.log(RecoverableLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));


                    var Recovered_From = AllFilters[7].getElementsByTagName('input')[0].value;// console.log(Recovered_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));

                    var Recovered_To = AllFilters[7].getElementsByTagName('input')[1].value;// console.log(Recovered_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));


                    var Province = document.getElementById("select2-chosen-1").innerHTML;


                    if (Province == null) { Province = ""; } //console.log(Province);


                 /*   var Cycle = document.getElementById("select2-chosen-2").innerHTML;

                    if (Cycle == null) { Cycle = ""; } //console.log(Cycle);*/


                    var IncomeFlow = document.getElementById("select2-chosen-2").innerHTML;

                    if (IncomeFlow == null) { IncomeFlow = ""; } //console.log(IncomeFlow);


                    window.location.href = "../Common/ActivityRequestReportPrint?ActivityCode=" + ActivityCode + "&CreatedTime_Start=" + CreatedTime_Start + "&CreatedTime_End=" + CreatedTime_End
                        + "&DiscoverTime_Start=" + DiscoverTime_Start+ "&DiscoverTime_End=" + DiscoverTime_End + "&TotalLeakage_From=" + TotalLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&TotalLeakage_To="
                        + TotalLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&RecoverableLeakage_From=" + RecoverableLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1")
                        + "&RecoverableLeakage_To=" + RecoverableLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&Recovered_From=" + Recovered_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1")
                        + "&Recovered_To=" + Recovered_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&Province=" + Province + "&IncomeFlow=" + IncomeFlow;
                }
            });

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }

         protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[] {         
             let filters = super.getQuickFilters();
             let fld = Case.ActivityRequestRow.Fields;


             let endTotalLeakage: Serenity.DecimalEditor = null;

             filters.push({
                 field: fld.TotalLeakage,
                 type: Serenity.DecimalEditor,
                 title: 'مبلغ نشتی کل از تا',
                 element: e1 => {
                     e1.css("width", "80px");
                     endTotalLeakage = Serenity.Widget.create({
                         type: Serenity.DecimalEditor,
                         element: e2 => e2.insertAfter(e1).css("width", "80px")
                     });

                     endTotalLeakage.element.change(x => e1.triggerHandler("change"));
                     $("<span/>").addClass("range-separator").text("-").insertAfter(e1);
                 },
                 handler: h => {
                     var active1 = h.value != null && !isNaN(h.value);
                     var active2 = endTotalLeakage.value != null && !isNaN(endTotalLeakage.value);
                     h.active = active1 || active2;

                     if (active1)
                         h.request.Criteria = Serenity.Criteria.and(h.request.Criteria,
                             [[fld.TotalLeakage], '>=', h.value]);

                     if (active2)
                         h.request.Criteria = Serenity.Criteria.and(h.request.Criteria,
                             [[fld.TotalLeakage], '<=', endTotalLeakage.value]);
                 }
             });

             let endRecoverableLeakage: Serenity.DecimalEditor = null;

             filters.push({
                 field: fld.RecoverableLeakage,
                 type: Serenity.DecimalEditor,
                 title: 'مبلغ نشتی قابل وصول از تا',
                 element: e1 => {
                     e1.css("width", "80px");
                     endRecoverableLeakage = Serenity.Widget.create({
                         type: Serenity.DecimalEditor,
                         element: e2 => e2.insertAfter(e1).css("width", "80px")
                     });

                     endRecoverableLeakage.element.change(x => e1.triggerHandler("change"));
                     $("<span/>").addClass("range-separator").text("-").insertAfter(e1);
                 },
                 handler: h => {
                     var active1 = h.value != null && !isNaN(h.value);
                     var active2 = endRecoverableLeakage.value != null && !isNaN(endRecoverableLeakage.value);
                     h.active = active1 || active2;

                     if (active1)
                         h.request.Criteria = Serenity.Criteria.and(h.request.Criteria,
                             [[fld.RecoverableLeakage], '>=', h.value]);

                     if (active2)
                         h.request.Criteria = Serenity.Criteria.and(h.request.Criteria,
                             [[fld.RecoverableLeakage], '<=', endRecoverableLeakage.value]);
                 }
             });

             let endRecovered: Serenity.DecimalEditor = null;

             filters.push({
                 field: fld.Recovered,
                 type: Serenity.DecimalEditor,
                 title: 'مبلغ مصوب از تا',
                 element: e1 => {
                     e1.css("width", "80px");
                     endRecovered = Serenity.Widget.create({
                         type: Serenity.DecimalEditor,
                         element: e2 => e2.insertAfter(e1).css("width", "80px")
                     });

                     endRecovered.element.change(x => e1.triggerHandler("change"));
                     $("<span/>").addClass("range-separator").text("-").insertAfter(e1);
                 },
                 handler: h => {
                     var active1 = h.value != null && !isNaN(h.value);
                     var active2 = endRecovered.value != null && !isNaN(endRecovered.value);
                     h.active = active1 || active2;

                     if (active1)
                         h.request.Criteria = Serenity.Criteria.and(h.request.Criteria,
                             [[fld.Recovered], '>=', h.value]);

                     if (active2)
                         h.request.Criteria = Serenity.Criteria.and(h.request.Criteria,
                             [[fld.Recovered], '<=', endRecovered.value]);
                 }
             });

             return filters;
         }

    }
} 