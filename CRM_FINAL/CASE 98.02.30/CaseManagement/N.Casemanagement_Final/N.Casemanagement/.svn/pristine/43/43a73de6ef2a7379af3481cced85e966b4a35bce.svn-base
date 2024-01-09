
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class ProvinceProgramGrid extends Serenity.EntityGrid<ProvinceProgramRow, any> {
        protected getColumnsKey() { return 'Case.ProvinceProgram'; }
        protected getDialogType() { return ProvinceProgramDialog; }
        protected getIdProperty() { return ProvinceProgramRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceProgramRow.localTextPrefix; }
        protected getService() { return ProvinceProgramService.baseUrl; }

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
                    new Slick.Aggregators.Sum('Recovered'),
                    new Slick.Aggregators.Sum('Program')
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
                service: ProvinceProgramService.baseUrl + '/ListExcel',
                onViewSubmit: () => this.onViewSubmit(),
                separator: true
            }));
            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var Province = document.getElementById("select2-chosen-1").innerHTML;

                    if (Province == null) { Province = ""; }

                    var Year = document.getElementById("select2-chosen-2").innerHTML;

                    if (Year == null) { Year = ""; }

                    window.location.href = "../Common/ProvinceProgramPrint?Year=" + Year + "&Province=" + Province;    
                }
            });

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }
    }
}