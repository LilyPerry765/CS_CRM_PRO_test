
namespace CaseManagement.Administration {
    
    @Serenity.Decorators.registerClass()
    export class LogGrid extends Serenity.EntityGrid<LogRow, any> {
        protected getColumnsKey() { return 'Administration.Log'; }
        protected getDialogType() { return LogDialog; }
        protected getIdProperty() { return LogRow.idProperty; }
        protected getLocalTextPrefix() { return LogRow.localTextPrefix; }
        protected getService() { return LogService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var AllFilters = document.getElementsByClassName("quick-filter-item");


                    var Logtime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(Logtime_Start);

                    var Logtime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(Logtime_End);


                    var Action = document.getElementById("select2-chosen-1").innerHTML;


                    if (Action == null) { Action = ""; }


                    var User = document.getElementById("select2-chosen-2").innerHTML;

                    if (User == null) { User = ""; }

                    var Province = document.getElementById("select2-chosen-3").innerHTML;

                    if (Province == null) { Province = ""; }


                    window.location.href = "../Common/LogPrint?Logtime_Start=" + Logtime_Start + "&Logtime_End=" + Logtime_End + "&Action=" + Action + "&User=" + User + "&Province=" + Province;
                }
            });



            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);
            return buttons;
        }
    }
}