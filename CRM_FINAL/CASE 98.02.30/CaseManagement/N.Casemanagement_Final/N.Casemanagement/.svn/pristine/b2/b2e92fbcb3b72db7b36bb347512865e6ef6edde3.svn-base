declare var Morris: any;

namespace CaseManagement.Common {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.resizable()
    @Serenity.Decorators.maximizable()
    export class DashboardIndex extends Serenity.TemplatedDialog<any> {

        private areaChart: any;

        static ProvinceProgram95() {

            var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport({}, response => {

                var bar = new Morris.Bar({
                    element: 'province-bar-chart',
                    resize: true,
                    parseTime: false,
                    data: response.Values,
                    xkey: 'Provinve',
                    ykeys: ['Program', 'Leak', 'Confirm'],
                    labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                    hideHover: 'auto'
                });
            });
        }        
    }
}  