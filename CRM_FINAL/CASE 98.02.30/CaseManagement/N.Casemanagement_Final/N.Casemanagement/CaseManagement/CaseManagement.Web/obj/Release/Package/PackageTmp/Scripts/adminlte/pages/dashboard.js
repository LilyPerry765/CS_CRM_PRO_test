﻿/*
 * Author: Abdullah A Almsaeed
 * Date: 4 Jan 2014
 * Description:
 *      This is a demo file used only for the main dashboard (index.html)
 **/

$(function () {

    "use strict";

    var Total_Leakage = 0;
    var Recoverable_Leakage = 0;

    var Recovered_ = 0;
    var Program_ = 0;

    //Make the dashboard widgets sortable Using jquery UI
    $(".connectedSortable").sortable({
        placeholder: "sort-highlight",
        connectWith: ".connectedSortable",
        handle: ".box-header, .nav-tabs",
        forcePlaceholderSize: true,
        zIndex: 999999
    });
    $(".connectedSortable .box-header, .connectedSortable .nav-tabs-custom").css("cursor", "move");

    //jQuery UI sortable for the todo list
    $(".todo-list").sortable({
        placeholder: "sort-highlight",
        handle: ".handle",
        forcePlaceholderSize: true,
        zIndex: 999999
    });

    //bootstrap WYSIHTML5 - text editor
    $(".textarea").wysihtml5();

    $('.daterange').daterangepicker({
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        startDate: moment().subtract(29, 'days'),
        endDate: moment()
    }, function (start, end) {
        window.alert("You chose: " + start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    });

    function CostSeperator(value) {
        if (value != null) {
            var persianNumbers = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
            value = value.toString();
            value = value.replace(/\,/g, '');
            var objRegex = new RegExp('(-?[0-9]+)([0-9]{3})');
            while (objRegex.test(value))
                value = value.replace(objRegex, '$1,$2');

            value = value.replace(/[0-9]/g, function (w) { return persianNumbers[+w] });
        }
        return value;
    }

    var ProvinceData = new Array();
    for (var i = 0; i < 32; i++) {
        ProvinceData[i] = new Array();
        ProvinceData[i][0] = 'white';
        for (var j = 1; j < 4; j++)
            ProvinceData[i][j] = 0;
    }

    function ProvinceProgram_ViewModel() {
        /* $.ajaxSetup({ async: false });
         $.getJSON(window.location.href + "/Services/Administration/CalendarEvent/List", function (data) { alert("success"); });*/
        //this.ProvinceData = ko.observableArray([40]);

        var url = window.location.href + "/Services/Case/ProvinceProgram/List";
        //console.log("URL:" + url);

        //FIXME
        $.ajaxSetup({ async: false });
        $.getJSON(url, function (data) {

            //console.log(data);

            for (var i = 0; i < data.TotalCount; i++) {

                if (data.Entities[i].YearId == 8) {

                    ProvinceData[data.Entities[i].ProvinceId - 1][1] = CostSeperator(data.Entities[i].TotalLeakage);
                    ProvinceData[data.Entities[i].ProvinceId - 1][2] = CostSeperator(data.Entities[i].RecoverableLeakage);
                    ProvinceData[data.Entities[i].ProvinceId - 1][3] = CostSeperator(data.Entities[i].Recovered);
                    ProvinceData[data.Entities[i].ProvinceId - 1][4] = CostSeperator(data.Entities[i].Program);

                    Total_Leakage += data.Entities[i].TotalLeakage;
                    Recoverable_Leakage += data.Entities[i].RecoverableLeakage;
                    Recovered_ += data.Entities[i].Recovered;
                    // Recovered_ = 491163000000;
                    Program_ += data.Entities[i].Program;

                    //alert(Recoverable_Leakage);

                    if (data.Entities[i].PercentRecovered > 60) {
                        ProvinceData[data.Entities[i].ProvinceId - 1][0] = 'green';
                    } else if (data.Entities[i].PercentRecovered > 30) {
                        ProvinceData[data.Entities[i].ProvinceId - 1][0] = 'yellow';
                    } else {
                        ProvinceData[data.Entities[i].ProvinceId - 1][0] = 'red';
                    }
                }
            } 
           // console.log(ProvinceData[31][4]);

            var Recovered_Color, Recoverable_Leakage_Color, Total_LeaKage_Color;

            if ((Recovered_ / Program_) < 0.33) {
                Recovered_Color = 'rgba(151, 40, 0, 0.7)';
            } else if ((0.33 < (Recovered_ / Program_)) && ((Recovered_ / Program_) < 0.66)) {
                Recovered_Color = 'rgba(185, 144, 0, 0.7)';
            } else {
                Recovered_Color = 'rgba(0, 207, 61, 0.7)';
            }

            if ((Recoverable_Leakage / Program_) < 0.33) {
                Recoverable_Leakage_Color = 'rgba(151, 40, 0, 0.7)';
            } else if ((0.33 < (Recoverable_Leakage / Program_)) && ((Recoverable_Leakage / Program_) < 0.66)) {
                Recoverable_Leakage_Color = 'rgba(185, 144, 0, 0.7)';
            } else {
                Recoverable_Leakage_Color = 'rgba(0, 207, 61, 0.7)';
            }

            if ((Total_Leakage / Program_) < 0.33) {
                Total_LeaKage_Color = 'rgba(151, 40, 0, 0.7)';
            } else if ((0.33 < (Total_Leakage / Program_)) && ((Total_Leakage / Program_) < 0.66)) {
                Total_LeaKage_Color = 'rgba(185, 144, 0, 0.7)';
            } else {
                Total_LeaKage_Color = 'rgba(0, 207, 61, 0.7)';
            }

            var data = [Recovered_ / 1000000, Recoverable_Leakage / 1000000, Total_Leakage / 1000000, Program_ / 1000000];
            var hprogress = new RGraph.HBar({
                id: 'Progress_bar',
                data: data,
                options: {
                    backgroundBarcolor1: 'white',
                    backgroundBarcolor2: 'white',
                    backgroundGrid: false,
                    xlabels: true,
                    xlabelsCount: 2,
                    //xlabelsSpecific: [ CostSeperator(-2000000),   CostSeperator(-1000000), CostSeperator(0)],
                    colorsSequential: true,
                    textSize: 8,
                    numxticks: 2,
                    colors: [Recovered_Color, Recoverable_Leakage_Color, Total_LeaKage_Color, 'rgba(0, 207, 61, 0.7)'],
                    tooltips: [
                   'مبلغ مصوب : ' + CostSeperator(Math.round(Recovered_ / 1000000)) + ' میلیون ریال  ',
                    'نشتی قابل وصول : ' + CostSeperator(Math.round(Recoverable_Leakage / 1000000)) + ' میلیون ریال  ',
                    'نشتی کل : ' + CostSeperator(Math.round(Total_Leakage / 1000000)) + ' میلیون ریال  ',
                    'برنامه هدف : ' + CostSeperator(Math.round(Program_ / 1000000)) + ' میلیون ریال  '

                    ],
                    //tooltipsHighlight:false ,
                    tooltipsNohideonclear: false,
                    tooltipsEvent: 'onmousemove'

                    //labels: [ ' مصوب',' قابل وصول', ' کل', ' هدف']
                    // eventsMouseover:MouseOver
                }

            }).grow({ frames: 180 });

            hprogress.canvas.onmouseout = function (e) {
                RGraph.hideTooltip();
                RGraph.redraw();
            }


            $("#Program_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(244, 245, 247)',
                colorOn: 'rgba(0, 207, 61, 0.7)',
                digits: (Math.round(Program_ / 1000000)).toString().length,
                value: Math.round(Program_ / 1000000) 
            });
            $("#TotalLeakage_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(247, 247, 247)',
                colorOn: Total_LeaKage_Color,
                digits: (Math.round(Total_Leakage / 1000000)).toString().length,
                value: (Math.round(Total_Leakage / 1000000) / 1000)
            });
            // console.log(Recoverable_Leakage / 1000000000);
            $("#Recoverable_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(247, 247, 247)',
                colorOn: Recoverable_Leakage_Color,
                digits: (Math.round(Recoverable_Leakage / 1000000).toString()).length,
                value: (Math.round(Recoverable_Leakage / 1000000) / 1000)
            });
            $("#Recovered_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(247, 247, 247)',
                colorOn: Recovered_Color,
                digits: (Math.round(Recovered_ / 1000000)).toString().length,
                value: (Math.round(Recovered_ / 1000000) / 1000)
            });


            ////////////////////////////////////////////
            //Canvas Radial Chart //////////////////////


            var TotalLeakage = new RadialGauge({
                renderTo: 'gauge-TotalLeakage',
                width: 150,
                height: 150,
                units: '$',
                minValue: 0,
                maxValue: Program_,
                majorTicks: [
                    '0',

                ],
                minorTicks: 2,
                ticksAngle: 270,
                startAngle: 45,
                strokeTicks: true,
                highlights: [
                      { from: 0, to: (Program_ * 0.33), color: 'rgba(151, 40, 0, 0.7)' },
                     { from: (Program_ * 0.33), to: (Program_ * 0.66), color: 'rgba(185, 144, 0, 0.7)' },
                     { from: (Program_ * 0.66), to: Program_, color: 'rgba(0, 207, 61, 0.7)' }
                ],
                valueInt: 1,
                valueDec: 0,
                colorPlate: "#fff",
                colorMajorTicks: "#686868",
                colorMinorTicks: "#686868",
                colorTitle: "#000",
                colorUnits: "#000",
                colorNumbers: "#686868",
                valueBox: true,
                colorValueText: "#000",
                colorValueBoxRect: "#fff",
                colorValueBoxRectEnd: "#fff",
                colorValueBoxBackground: "#fff",
                colorValueBoxShadow: false,
                colorValueTextShadow: false,
                colorNeedleShadowUp: true,
                colorNeedleShadowDown: false,
                colorNeedle: "rgba(200, 50, 50, .75)",
                colorNeedleEnd: "rgba(200, 50, 50, .75)",
                colorNeedleCircleOuter: "rgba(200, 200, 200, 1)",
                colorNeedleCircleOuterEnd: "rgba(200, 200, 200, 1)",
                borderShadowWidth: 0,
                borders: true,
                borderInnerWidth: 2,
                borderMiddleWidth: 4,
                borderOuterWidth: 10,
                colorBorderOuter: "#fafafa",
                colorBorderOuterEnd: "#cdcdcd",
                needleType: "arrow",
                needleWidth: 2,
                needleCircleSize: 7,
                needleCircleOuter: true,
                needleCircleInner: false,
                animationDuration: 6000,
                animationRule: "dequint",
                fontNumbers: "Verdana",
                fontTitle: "Verdana",
                fontUnits: "Verdana",
                fontValue: "Led",
                fontValueStyle: 'italic',
                fontNumbersSize: 20,
                fontNumbersStyle: 'italic',
                fontNumbersWeight: 'bold',
                fontTitleSize: 24,
                fontUnitsSize: 22,
                fontValueSize: 25,
                animatedValue: true
            });
            TotalLeakage.draw();
            TotalLeakage.value = Total_Leakage.toString();

            var Recoverable = new RadialGauge({
                renderTo: 'gauge-Recoverable',
                width: 150,
                height: 150,
                units: '$',
                minValue: 0,
                maxValue: Program_,
                majorTicks: [
                    '0',

                ],
                minorTicks: 2,
                ticksAngle: 270,
                startAngle: 45,
                strokeTicks: true,
                highlights: [
                    { from: 0, to: (Program_ * 0.33), color: 'rgba(151, 40, 0, 0.7)' },
                     { from: (Program_ * 0.33), to: (Program_ * 0.66), color: 'rgba(185, 144, 0, 0.7)' },
                     { from: (Program_ * 0.66), to: Program_, color: 'rgba(0, 207, 61, 0.7)' }
                ],
                valueInt: 1,
                valueDec: 0,
                colorPlate: "#fff",
                colorMajorTicks: "#686868",
                colorMinorTicks: "#686868",
                colorTitle: "#000",
                colorUnits: "#000",
                colorNumbers: "#686868",
                valueBox: true,
                colorValueText: "#000",
                colorValueBoxRect: "#fff",
                colorValueBoxRectEnd: "#fff",
                colorValueBoxBackground: "#fff",
                colorValueBoxShadow: false,
                colorValueTextShadow: false,
                colorNeedleShadowUp: true,
                colorNeedleShadowDown: false,
                colorNeedle: "rgba(200, 50, 50, .75)",
                colorNeedleEnd: "rgba(200, 50, 50, .75)",
                colorNeedleCircleOuter: "rgba(200, 200, 200, 1)",
                colorNeedleCircleOuterEnd: "rgba(200, 200, 200, 1)",
                borderShadowWidth: 0,
                borders: true,
                borderInnerWidth: 2,
                borderMiddleWidth: 4,
                borderOuterWidth: 10,
                colorBorderOuter: "#fafafa",
                colorBorderOuterEnd: "#cdcdcd",
                needleType: "arrow",
                needleWidth: 2,
                needleCircleSize: 7,
                needleCircleOuter: true,
                needleCircleInner: false,
                animationDuration: 6000,
                animationRule: "dequint",
                fontNumbers: "Verdana",
                fontTitle: "Verdana",
                fontUnits: "Verdana",
                fontValue: "Led",
                fontValueStyle: 'italic',
                fontNumbersSize: 20,
                fontNumbersStyle: 'italic',
                fontNumbersWeight: 'bold',
                fontTitleSize: 24,
                fontUnitsSize: 22,
                fontValueSize: 25,
                animatedValue: true
            });
            Recoverable.draw();
            Recoverable.value = Recoverable_Leakage.toString();


            var Recovered = new RadialGauge({
                renderTo: 'gauge-Recovered',
                width: 150,
                height: 150,
                units: '$',
                minValue: 0,
                maxValue: Program_,
                majorTicks: [
                    '0',

                ],
                minorTicks: 2,
                ticksAngle: 270,
                startAngle: 45,
                strokeTicks: true,
                highlights: [
                     { from: 0, to: (Program_ * 0.33), color: 'rgba(151, 40, 0, 0.7)' },
                     { from: (Program_ * 0.33), to: (Program_ * 0.66), color: 'rgba(185, 144, 0, 0.7)' },
                     { from: (Program_ * 0.66), to: Program_, color: 'rgba(0, 207, 61, 0.7)' }
                ],
                valueInt: 1,
                valueDec: 0,
                colorPlate: "#fff",
                colorMajorTicks: "#686868",
                colorMinorTicks: "#686868",
                colorTitle: "#000",
                colorUnits: "#000",
                colorNumbers: "#686868",
                valueBox: true,
                colorValueText: "#000",
                colorValueBoxRect: "#fff",
                colorValueBoxRectEnd: "#fff",
                colorValueBoxBackground: "#fff",
                colorValueBoxShadow: false,
                colorValueTextShadow: false,
                colorNeedleShadowUp: true,
                colorNeedleShadowDown: false,
                colorNeedle: "rgba(200, 50, 50, .75)",
                colorNeedleEnd: "rgba(200, 50, 50, .75)",
                colorNeedleCircleOuter: "rgba(200, 200, 200, 1)",
                colorNeedleCircleOuterEnd: "rgba(200, 200, 200, 1)",
                borderShadowWidth: 0,
                borders: true,
                borderInnerWidth: 2,
                borderMiddleWidth: 4,
                borderOuterWidth: 10,
                colorBorderOuter: "#fafafa",
                colorBorderOuterEnd: "#cdcdcd",
                needleType: "arrow",
                needleWidth: 2,
                needleCircleSize: 7,
                needleCircleOuter: true,
                needleCircleInner: false,
                animationDuration: 6000,
                animationRule: "dequint",
                fontNumbers: "Verdana",
                fontTitle: "Verdana",
                fontUnits: "Verdana",
                fontValue: "Led",
                fontValueStyle: 'italic',
                fontNumbersSize: 20,
                fontNumbersStyle: 'italic',
                fontNumbersWeight: 'bold',
                fontTitleSize: 24,
                fontUnitsSize: 22,
                fontValueSize: 25,
                animatedValue: true
            });
            Recovered.draw();
            Recovered.value = Recovered_.toString();


            //console.log(ProvinceData);			
        });

        //FIXME
        $.ajaxSetup({ async: true });
    }


    ////////////////////////////////////////////////////
    // The Map:   
    //ko.applyBindings(new ProvinceProgram_ViewModel() );
    ProvinceProgram_ViewModel();
    //console.log("##" + ProvinceData[0]);

    // Load Province state from server, convert it to provice colors and set legends..
    var map = new jvm.Map({
        container: $('#map'),
        map: 'iran_map', // 'us_aea_en', //'iran_map',
        backgroundColor: '#E4F1FE', //'#fff',//'white', //'#f7f7f7', //'#0073b7', //'#357ca5', //'#3c8dbc',
        labels: {
            regions: {
                render: function (code) {
                    switch (code) {
                        case 'IR-01': return "آذر. شرقی"/*"آذربایجان شرقی"*/;
                        case 'IR-02': return "آذر. غربی"/*"آذربایجان غربی"*/;
                        case 'IR-03': return "اردبیل"; case 'IR-04': return "اصفهان";
                        case 'IR-05': return "ایلام"; case 'IR-06': return "بوشهر";
                        case 'IR-07': return "تهران"; case 'IR-08': return "چهارمحال" /*"چهارمحال و بختیاری"*/;
                        case 'IR-09': return "خراسان جنوبی"; case 'IR-10': return "خراسان رضوی";
                        case 'IR-11': return "خراسان شمالی"; case 'IR-12': return "خوزستان";
                        case 'IR-13': return "زنجان"; case 'IR-14': return "سمنان";
                        case 'IR-15': return "سیستان و بلوچستان"; case 'IR-16': return "فارس";
                        case 'IR-17': return "قزوین"; case 'IR-18': return "قم";
                        case 'IR-19': return "کردستان"; case 'IR-20': return "کرمان";
                        case 'IR-21': return "کرمانشاه"; case 'IR-22': return "کهگیلویه" /*"کهگیلویه و بویراحمد"*/;
                        case 'IR-23': return "گلستان"; case 'IR-24': return "گیلان";
                        case 'IR-25': return "لرستان"; case 'IR-26': return "مازندران";
                        case 'IR-27': return "مرکزی"; case 'IR-28': return "هرمزگان";
                        case 'IR-29': return "همدان"; case 'IR-30': return "یزد";
                        case 'IR-31': return "البرز"; case 'IR-40': return "دریای خزر";
                        default: return code;
                    }
                }, 
                offsets: function (code) {
                    return {
                        '01': [-10, 10], '02': [10, 60], '03': [0, 0], '04': [0, -20], '05': [0, 0], '06': [10, 10], '07': [0, 0],
                        '08': [0, 0], '09': [-15, 0], '10': [5, -10], '11': [5, 5], '12': [-5, 15], '13': [0, 0], '14': [5, 10],
                        '15': [-20, 30], '16': [-2, 0], '17': [3, -3], '18': [0, 0], '19': [10, 0], '20': [10, -30], '21': [0, 0],
                        '22': [0, 0], '23': [-10, 15], '24': [0, 20], '25': [0, 5], '26': [0, 2], '27': [-5, 20], '28': [5, -10],
                        '29': [0, 0], '30': [0, 0], '31': [0, 5], '40': [0, 0]
                    }[code.split('-')[1]];
                }
            }
        },
        series: {
            regions: [{
                scale: {
                    green: '#00a65a', //'#A4D886', 
                    yellow: '#f39c12', //'#FCECA2',
                    red: '#dd4b39' //'#F9573B'
                },
                attribute: 'fill',
                /*values: {
                    "IR-01": 'red', "IR-02": 'yellow', "IR-03": 'green', "IR-04": 'yellow',"IR-05": 'red', "IR-06": 'green', "IR-07": 'green', "IR-08": 'green',
                    "IR-09": 'red', "IR-10": 'yellow', "IR-11": 'green', "IR-12": 'yellow',"IR-13": 'red', "IR-14": 'yellow', "IR-15": 'green', "IR-16": 'green',
                    "IR-17": 'red', "IR-18": 'green', "IR-19": 'green', "IR-20": 'yellow',"IR-21": 'red', "IR-22": 'yellow', "IR-23": 'green', "IR-24": 'yellow',
                    "IR-25": 'red', "IR-26": 'yellow', "IR-27": 'green', "IR-28": 'green',"IR-29": 'red', "IR-30": 'green', "IR-31": 'green'
                },*/
                values: {
                    "IR-01": ProvinceData[0][0], "IR-02": ProvinceData[1][0], "IR-03": ProvinceData[2][0], "IR-04": ProvinceData[3][0],
                    "IR-05": ProvinceData[4][0], "IR-06": ProvinceData[5][0], "IR-07": ProvinceData[6][0], "IR-08": ProvinceData[7][0],
                    "IR-09": ProvinceData[8][0], "IR-10": ProvinceData[9][0], "IR-11": ProvinceData[10][0], "IR-12": ProvinceData[11][0],
                    "IR-13": ProvinceData[12][0], "IR-14": ProvinceData[13][0], "IR-15": ProvinceData[14][0], "IR-16": ProvinceData[15][0],
                    "IR-17": ProvinceData[16][0], "IR-18": ProvinceData[17][0], "IR-19": ProvinceData[18][0], "IR-20": ProvinceData[19][0],
                    "IR-21": ProvinceData[20][0], "IR-22": ProvinceData[21][0], "IR-23": ProvinceData[22][0], "IR-24": ProvinceData[23][0],
                    "IR-25": ProvinceData[24][0], "IR-26": ProvinceData[25][0], "IR-27": ProvinceData[26][0], "IR-28": ProvinceData[27][0],
                    "IR-29": ProvinceData[28][0], "IR-30": ProvinceData[29][0], "IR-31": ProvinceData[31][0], "IR-40": 'white'
                },
                legend: {
                    horizontal: false,
                    title: 'مبلغ مصوب' + '<br />' + 'نسبت به برنامه' + '<br />' + 'هدف سالیانه',
                    labelRender: function (v) {
                        return {
                            green: 'بیشتر<br>از<br>60%',
                            yellow: 'بین<br>30%<br>60%',
                            red: 'کمتر<br>از<br>30%'
                        }[v];
                    }
                }
            }]
        },
        regionsSelectable: true,
        regionsSelectableOne: true,
        zoomOnScroll: false,
        //selectedRegions: JSON.parse(window.localStorage.getItem('jvectormap-selected-regions') || '[]'),

        onRegionTipShow: function (event, tip, code) {
            /*var CodeNumber = parseInt(code.substring(3, 5));
            //console.log("Code:" + parseInt(code.substring(3,5)) + " Program:" + ProvinceData[parseInt(code.substring(3,5)-1)][1] );

            if (code != 'IR-40')
                tip.html(tip.html() + ' </br> نشتی کل: ' + ProvinceData[CodeNumber - 1][1]
									+ ' </br> نشتی قابل وصول: ' + ProvinceData[CodeNumber - 1][2]
                                    + ' </br> مبلغ مصوب: ' + ProvinceData[CodeNumber - 1][3]
									+ ' </br> برنامه هدف: ' + ProvinceData[CodeNumber - 1][4]);*/
            event.preventDefault();
        },
        /*onRegionOver: function(event, code){
            console.log('region-over', code, map.getRegionName(code));
        },
        onRegionOut: function(event, code){
            console.log('region-out', code);
        },
        onRegionClick: function (event, code) {            
            //console.log('region-click', code);
        },*/

        onRegionSelected: function (event, code, isSelected, selectedRegions) {

            var customTip = $('#customTip');
            //console.log('region-select', code, isSelected, selectedRegions);

            if (isSelected) {
                var CodeNumber = parseInt(code.substring(3, 5)); 
                if (code != 'IR-40') {
                    customTip.hide();
                    if (CodeNumber == 31) { CodeNumber = 32; }
                    customTip.html(' برنامه هدف: ' + ProvinceData[CodeNumber - 1][4]
                                   + ' </br> نشتی کل: ' + ProvinceData[CodeNumber - 1][1]
					        	   + ' </br> نشتی قابل وصول: ' + ProvinceData[CodeNumber - 1][2]
                                   + ' </br> مبلغ مصوب: ' + ProvinceData[CodeNumber - 1][3]);
                    customTip.css({ left: left, top: top })
                    customTip.show();

                    customTip.click(function () {
                        map.clearSelectedRegions();
                        customTip.hide();
                    })
                }

            }
            /*if (window.localStorage) {
                window.localStorage.setItem(
                  'jvectormap-selected-regions',
                  JSON.stringify(selectedRegions)
                );
            }*/
        },
    });

    var left, top;
    $('#map').vectorMap('get', 'mapObject').container.mousemove(function (e) {
        var offset = $('#map').offset();
        var x = e.pageX - parseInt(offset.left), y = e.pageY - parseInt(offset.top);
        //console.log("pos", x, y, map.width, map.height);

        if ((map.width - x < 300) && (map.height - y < 100)) {
            left = x - 250;
            top = y - 50;
        } else if (map.width - x < 300) {
            left = x - 200;
            top = y + 50;
        } else if (map.height - y < 100) {
            left = x;
            top = y - 50;
        } else {
            left = x;
            top = y + 50;
        }
    });

    //Sparkline charts
    var myvalues = [1000, 1200, 920, 927, 931, 1027, 819, 930, 1021];
    $('#sparkline-1').sparkline(myvalues, {
        type: 'line',
        lineColor: '#92c1dc',
        fillColor: "#ebf4f9",
        height: '50',
        width: '80'
    });
    myvalues = [515, 519, 520, 522, 652, 810, 370, 627, 319, 630, 921];
    $('#sparkline-2').sparkline(myvalues, {
        type: 'line',
        lineColor: '#92c1dc',
        fillColor: "#ebf4f9",
        height: '50',
        width: '80'
    });
    myvalues = [15, 19, 20, 22, 33, 27, 31, 27, 19, 30, 21];
    $('#sparkline-3').sparkline(myvalues, {
        type: 'line',
        lineColor: '#92c1dc',
        fillColor: "#ebf4f9",
        height: '50',
        width: '80'
    });
    //////////////////////////////////////////////////////////////

    /////////////////////////////
    //The Calender

    /* initialize the external events
    -----------------------------------------------------------------*/
    function ini_events(ele) {
        ele.each(function () {

            // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
            // it doesn't need to have a start or end
            var eventObject = {
                title: $.trim($(this).text()) // use the element's text as the event title
            };

            // store the Event Object in the DOM element so we can get to it later
            $(this).data('eventObject', eventObject);

            // make the event draggable using jQuery UI
            $(this).draggable({
                zIndex: 1070,
                revert: true, // will cause the event to go back to its
                revertDuration: 0  //  original position after the drag
            });

        });
    }
    ini_events($('#external-events div.external-event'));

    /* initialize the calendar
     -----------------------------------------------------------------*/
    //Date for the calendar events (dummy data)
    var date = new Date();
    var d = date.getDate(), m = date.getMonth(), y = date.getFullYear();
    $('#calendar').fullCalendar({
        //aspectRatio: 2.5,
        height: 550, //'parent',
        //contentHeight:400,
        //handleWindowResize: true,
        lang: 'fa',
        isJalaali: true,
        isRTL: true,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        //Random default events
        events: [
          {
              title: 'All Day Event',
              start: new Date(y, m, 1),
              backgroundColor: "#f56954", //red
              borderColor: "#f56954" //red
          },
          {
              title: 'Birthday Party',
              start: new Date(y, m, d + 1, 19, 0),
              end: new Date(y, m, d + 1, 22, 30),
              allDay: false,
              backgroundColor: "#00a65a", //Success (green)
              borderColor: "#00a65a" //Success (green)
          },
          {
              title: 'Click for Google',
              start: new Date(y, m, 28),
              end: new Date(y, m, 29),
              url: 'http://google.com/',
              backgroundColor: "#3c8dbc", //Primary (light-blue)
              borderColor: "#3c8dbc" //Primary (light-blue)
          }
        ],
        editable: true,
        droppable: true, // this allows things to be dropped onto the calendar !!!
        drop: function (date, allDay) { // this function is called when something is dropped

            // retrieve the dropped element's stored Event Object
            var originalEventObject = $(this).data('eventObject');

            // we need to copy it, so that multiple events don't have a reference to the same object
            var copiedEventObject = $.extend({}, originalEventObject);

            // assign it the date that was reported
            copiedEventObject.start = date;
            copiedEventObject.allDay = allDay;
            copiedEventObject.backgroundColor = $(this).css("background-color");
            copiedEventObject.borderColor = $(this).css("border-color");

            // render the event on the calendar
            // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
            $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

            // is the "remove after drop" checkbox checked?
            if ($('#drop-remove').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }

        }
    });

    /* ADDING EVENTS */
    var currColor = "#3c8dbc"; //Red by default
    //Color chooser button
    var colorChooser = $("#color-chooser-btn");
    $("#color-chooser > li > a").click(function (e) {
        e.preventDefault();
        //Save color
        currColor = $(this).css("color");
        //Add color effect to button
        $('#add-new-event').css({ "background-color": currColor, "border-color": currColor });
    });
    $("#add-new-event").click(function (e) {
        e.preventDefault();
        //Get value and make sure it is not null
        var val = $("#new-event").val();
        if (val.length == 0) {
            return;
        }

        //Create events
        var event = $("<div />");
        event.css({ "background-color": currColor, "border-color": currColor, "color": "#fff" }).addClass("external-event");
        event.html(val);
        $('#external-events').prepend(event);

        //Add draggable funtionality
        ini_events(event);

        //Remove event from text input
        $("#new-event").val("");
    });

    ////////////////////////////////////

    //SLIMSCROLL FOR CHAT WIDGET
    $('#chat-box').slimScroll({
        height: '250px'
    });

    /* Morris.js Charts */
    //Donut Chart
    //var donut = new Morris.Donut({
    //    element: 'activity-donut-chart',
    //    resize: true,
    //    colors: ["#d6f5d6", "#adebad", "#84e184", "#5bd75b", "#32cd32", "#28a428", "#1e7b1e", "#145214"],
    //    data: [
    //      { label: "مدیریت و استفاده از شبکه", value: 15 },
    //      { label: "مدیریت وتامین سفارشات", value: 10 },
    //      { label: "نرخ گذاری و صورتحساب گیری", value: 20 },
    //      { label: "مدیریت وصولی ها", value: 5 },
    //      { label: "مالي و حسابداري", value: 12 },
    //      { label: "مديريت مشتري", value: 18 },
    //      { label: "مدیریت شرکاء", value: 10 },
    //      { label: "مديريت محصولات", value: 10 }
    //    ],
    //    hideHover: 'auto'
    //});    

    //BAR CHART
    //   var bar = new Morris.Bar({
    //       element: 'activity-bar-chart',
    //       resize: true,
    //       data: [
    //         { y: 'مدیریت و استفاده از شبکه', a: 15, b: 10 },
    //         { y: 'مدیریت وتامین سفارشات', a: 10, b: 8 },
    //         { y: 'نرخ گذاری و صورتحساب گیری', a: 20, b: 18 },
    //         { y: 'مدیریت وصولی ها', a: 5, b: 15 },
    //         { y: 'مالي و حسابداري', a: 12, b: 10 },
    //         { y: 'مديريت مشتري', a: 18, b: 13 },
    //         { y: 'مديريت شرکاء', a: 10, b: 16 },
    //         { y: 'مديريت محصولات', a: 10, b: 4 }
    //       ],
    //       barColors: ['#f39c12', '#00c0ef'],
    //       xkey: 'y',
    //       ykeys: ['a', 'b'],
    //       labels: ['نشتی اولیه', 'نشتی تایید شده'],
    //       hideHover: 'auto'
    //   });

    //var bar = new Morris.Bar({
    //    element: 'province-bar-chart',
    //    resize: true,
    //    data: [
    //      { y: 'آذربایجان شرقی', a: 15, b: 10, c: 20 },
    //      { y: 'آذربایجان غربی', a: 10, b: 8, c: 12 },
    //      { y: 'اردبیل', a: 20, b: 18, c: 17 },
    //      { y: 'اصفهان', a: 5, b: 15, c: 7 },
    //      { y: 'ایلام', a: 12, b: 10, c: 20 },
    //      { y: 'بوشهر', a: 18, b: 13, c: 10 },
    //      { y: 'تهران', a: 10, b: 16, c: 10 },
    //      { y: 'چهارمحال وبختیاری', a: 10, b: 4, c: 10 },
    //      { y: 'خراسان جنوبی', a: 15, b: 10, c: 10 },
    //      { y: 'خراسان رضوی', a: 10, b: 8, c: 10 },
    //      { y: 'خراسان شمالی', a: 20, b: 18, c: 10 },
    //      { y: 'خوزستان', a: 5, b: 15, c: 10 },
    //      { y: 'زنجان', a: 12, b: 10, c: 10 },
    //      { y: 'سمنان', a: 18, b: 13, c: 10 },
    //      { y: 'سيستان و بلوچستان', a: 10, b: 16, c: 10 },
    //      { y: 'فارس', a: 10, b: 4, c: 10 },
    //      { y: 'قزوین', a: 15, b: 10, c: 10 },
    //      { y: 'قم', a: 10, b: 8, c: 10 },
    //      { y: 'کردستان', a: 20, b: 18, c: 10 },
    //      { y: 'کرمان', a: 5, b: 15, c: 10 },
    //      { y: 'کرمانشاه', a: 12, b: 10, c: 10 },
    //      { y: 'کهکیلویه بویراحمد', a: 18, b: 13, c: 10 },
    //      { y: 'گلستان', a: 10, b: 16, c: 10 },
    //      { y: 'گیلان', a: 10, b: 4, c: 10 },
    //      { y: 'لرستان', a: 15, b: 10, c: 10 },
    //      { y: 'مازندران', a: 10, b: 8, c: 10 },
    //      { y: 'مرکزی', a: 20, b: 18, c: 10 },
    //      { y: 'هرمزگان', a: 5, b: 15, c: 10 },
    //      { y: 'همدان', a: 12, b: 10, c: 10 },
    //      { y: 'یزد', a: 18, b: 13, c: 10 }
    //    ],        
    //    xkey: 'y',       
    //    ykeys: ['a', 'b', 'c'],
    //    labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
    //    hideHover: 'auto'
    //});   

    /* The todo list plugin */
    $(".todo-list").todolist({
        onCheck: function (ele) {
            window.console.log("The element has been checked");
            return ele;
        },
        onUncheck: function (ele) {
            window.console.log("The element has been unchecked");
            return ele;
        }
    });

    /*var pieChartCanvas1 = $("#year-donut-chart1").get(0).getContext("2d");
    var pieChart1 = new Chart(pieChartCanvas1);
    var PieData1 = [
        {
            value: 300,
            color: "#f56954",
            highlight: "#f56954",
            label: "برنامه هدف"
        },
        {
            value: 50,
            color: "#00a65a",
            highlight: "#00a65a",
            label: "نشتی تایید شده"
        },
        {
            value: 100,
            color: "#f39c12",
            highlight: "#f39c12",
            label: "نشتی اولیه"
        }
    ];

    var pieChartCanvas2 = $("#year-donut-chart2").get(0).getContext("2d");
    var pieChart2 = new Chart(pieChartCanvas2);
    var PieData2 = [
        {
            value: 260,
            color: "#f56954",
            highlight: "#f56954",
            label: "برنامه هدف"
        },
        {
            value: 100,
            color: "#00a65a",
            highlight: "#00a65a",
            label: "نشتی تایید شده"
        },
        {
            value: 200,
            color: "#f39c12",
            highlight: "#f39c12",
            label: "نشتی اولیه"
        }
    ];

    var pieChartCanvas3 = $("#year-donut-chart3").get(0).getContext("2d");
    var pieChart3 = new Chart(pieChartCanvas3);
    var PieData3 = [
        {
            value: 350,
            color: "#f56954",
            highlight: "#f56954",
            label: "برنامه هدف"
        },
        {
            value: 650,
            color: "#00a65a",
            highlight: "#00a65a",
            label: "نشتی تایید شده"
        },
        {
            value: 700,
            color: "#f39c12",
            highlight: "#f39c12",
            label: "نشتی اولیه"
        }
    ];

    var pieOptions = {
        //Boolean - Whether we should show a stroke on each segment
        segmentShowStroke: true,
        //String - The colour of each segment stroke
        segmentStrokeColor: "#fff",
        //Number - The width of each segment stroke
        segmentStrokeWidth: 2,
        //Number - The percentage of the chart that we cut out of the middle
        percentageInnerCutout: 50, // This is 0 for Pie charts
        //Number - Amount of animation steps
        animationSteps: 100,
        //String - Animation easing effect
        animationEasing: "easeOutBounce",
        //Boolean - Whether we animate the rotation of the Doughnut
        animateRotate: true,
        //Boolean - Whether we animate scaling the Doughnut from the centre
        animateScale: false,
        //Boolean - whether to make the chart responsive to window resizing
        responsive: true,
        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        maintainAspectRatio: true,
        //String - A legend template
        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++)
        {%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label)
        {%><%=segments[i].label%><%}%></li><%}%></ul>"
    };
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    pieChart1.Doughnut(PieData1, pieOptions);
    pieChart2.Doughnut(PieData2, pieOptions);
    pieChart3.Doughnut(PieData3, pieOptions);
	*/
});
