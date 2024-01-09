declare var Morris: any;

namespace CaseManagement.StimulReport {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.resizable()
    @Serenity.Decorators.maximizable()
    export class UserLeaderActivityDetail extends Serenity.TemplatedDialog<any> {

        private areaChart: any;

        static initializePage() {

            var aaa = CaseManagement.Administration.LogService.LogLeaderReport({}, response => {

                var bar = new Morris.Bar({
                    element: 'UserLeaderChart',
                    resize: true,
                    parseTime: false,
                    data: response.Values,
                    xkey: 'Provinve',
                    ykeys: ['Count'],
                    labels: ['تعداد'],
                    hideHover: 'auto'
                });
            });                
        }
    }
}  