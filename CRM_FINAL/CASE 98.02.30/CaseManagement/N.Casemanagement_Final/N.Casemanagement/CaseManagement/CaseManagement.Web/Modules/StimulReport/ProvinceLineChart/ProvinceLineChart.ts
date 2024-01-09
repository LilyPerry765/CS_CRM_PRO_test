﻿declare var Morris: any;

namespace CaseManagement.StimulReport {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.resizable()
    @Serenity.Decorators.maximizable()
    export class ProvinceLineChart extends Serenity.TemplatedDialog<any> {

        private areaChart: any;

        static ProvinceProgram96() {

            var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport96({}, response => {

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

        static ProvinceProgram94() {

            var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport94({}, response => {

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

        static ProvinceProgram93() {

            var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport93({}, response => {

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

        static ProvinceProgram92() {

            var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport92({}, response => {

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
        //var bar = new Morris.Bar({
        //    element: 'province-bar-chart',
        //    resize: true,
        //    data: [
        //        { a: 15, b: 10, c: 20 },
        //        { a: 10, b: 8, c: 12 },
        //        { a: 20, b: 18, c: 17 },
        //        { a: 5, b: 15, c: 7 },
        //        { a: 12, b: 10, c: 20 },
        //        { a: 18, b: 13, c: 10 },
        //        { a: 10, b: 16, c: 10 },
        //        { a: 10, b: 4, c: 10 },
        //        { a: 15, b: 10, c: 10 },
        //        { a: 10, b: 8, c: 10 },
        //        { a: 20, b: 18, c: 10 },
        //        { a: 5, b: 15, c: 10 },
        //        { a: 12, b: 10, c: 10 },
        //        { a: 18, b: 13, c: 10 },
        //        { a: 10, b: 16, c: 10 },
        //        { a: 10, b: 4, c: 10 },
        //        { a: 15, b: 10, c: 10 },
        //        { a: 10, b: 8, c: 10 },
        //        { a: 20, b: 18, c: 10 },
        //        { a: 5, b: 15, c: 10 },
        //        { a: 12, b: 10, c: 10 },
        //        { a: 18, b: 13, c: 10 },
        //        { a: 10, b: 16, c: 10 },
        //        { a: 10, b: 4, c: 10 },
        //        { a: 15, b: 10, c: 10 },
        //        { a: 10, b: 8, c: 10 },
        //        { a: 20, b: 18, c: 10 },
        //        { a: 5, b: 15, c: 10 },
        //        { a: 12, b: 10, c: 10 },
        //        { a: 18, b: 13, c: 10 }
        //    ],
        //    barColors: ['#f39c12', '#3d9970', '#dd4b39'],
        //    xkey: response.Provinces,
        //    ykeys: ['a', 'b', 'c'],
        //    labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
        //    hideHover: 'auto'
        //});



    }
} 