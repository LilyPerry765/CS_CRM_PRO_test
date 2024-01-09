declare var Morris: any;

namespace CaseManagement.StimulReport {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.resizable()
    @Serenity.Decorators.maximizable()
    export class UserProvinceActivityDetail extends Serenity.TemplatedDialog<any> {

        private areaChart: any;

        static initializePage() {
            
            var aaa = CaseManagement.Administration.LogService.LogProvinceReport({}, response => {
           
                var bar = new Morris.Bar({
                    element: 'ProvinceChart',
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