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
        
        var url = window.location.href + "/Services/Case/ProvinceProgram/List";
        url = url.replace('/Common/DashboardUser96','');
        //console.log("URL:" + url);

        //FIXME
        $.ajaxSetup({ async: false });
        $.getJSON(url, function (data) {

            //console.log(data);

            for (var i = 0; i < data.TotalCount; i++) {

                if (data.Entities[i].YearId == 7) {

                    ProvinceData[data.Entities[i].ProvinceId - 1][1] = CostSeperator(data.Entities[i].TotalLeakage);
                    ProvinceData[data.Entities[i].ProvinceId - 1][2] = CostSeperator(data.Entities[i].RecoverableLeakage);
                    ProvinceData[data.Entities[i].ProvinceId - 1][3] = CostSeperator(data.Entities[i].Recovered);
                    ProvinceData[data.Entities[i].ProvinceId - 1][4] = CostSeperator(data.Entities[i].Program);

                    Total_Leakage += data.Entities[i].TotalLeakage;
                    Recoverable_Leakage += data.Entities[i].RecoverableLeakage;

                    Recovered_ += data.Entities[i].Recovered;
                    Program_ += data.Entities[i].Program;

                    //alert(Recoverable_Leakage);

                    if (data.Entities[i].PercentLeak > 60) {
                        ProvinceData[data.Entities[i].ProvinceId - 1][0] = 'green';
                    } else if (data.Entities[i].PercentLeak > 30) {
                        ProvinceData[data.Entities[i].ProvinceId - 1][0] = 'yellow';
                    } else {
                        ProvinceData[data.Entities[i].ProvinceId - 1][0] = 'red';
                    }
                }
            }

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
            //console.log("Program : " + Program_ / 1000000);
            //console.log("Recovered : " + Recovered_ / 1000000);
			
            var  OnlyForRoundedPrograms= 0.0;
            if(((Program_ / 1000000)%10).toFixed() == 0 ){
                OnlyForRoundedPrograms = 0.6;
            }
            //console.log((Program_ / 1000000) + OnlyForRoundedNumbers);
            $("#Program_SevenSegment").sevenSeg({  
                colorBackground: 'white',
                colorOff: 'rgb(244, 245, 247)',
                colorOn: 'rgba(0, 207, 61, 0.7)',
                digits: (Math.round(Program_ / 1000000)).toString().length,
                // value: Number(Math.round((Program_ / 1000000) + OnlyForRoundedPrograms).toString().split("").reverse().join(""))
                value: (Math.round(Program_ / 1000000)) 
            });

            var OnlyForRoundedTotal = 0.0;
            if((Math.round(Total_Leakage / 1000000)%10).toFixed() == 0 ){
                OnlyForRoundedTotal = 0.6;
            }
		
            $("#TotalLeakage_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(247, 247, 247)',
                colorOn: Total_LeaKage_Color,
                digits: (Math.round(Total_Leakage / 1000000)).toString().length,
                //value: Number(Math.round((Total_Leakage / 1000000) + OnlyForRoundedTotal).toString().split("").reverse().join(""))
                value: (Math.round(Total_Leakage / 1000000))
            });

            var OnlyForRoundedRecoverable = 0.0;
            if((Math.round(Recoverable_Leakage / 1000000)%10).toFixed() == 0 ){
                OnlyForRoundedRecoverable = 0.6;
            }
            $("#Recoverable_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(247, 247, 247)',
                colorOn: Recoverable_Leakage_Color,
                digits: (Math.round(Recoverable_Leakage / 1000000).toString()).length,
                //  value: Number(Math.round((Recoverable_Leakage / 1000000) + OnlyForRoundedRecoverable ).toString().split("").reverse().join(""))
                value: (Math.round(Recoverable_Leakage / 1000000))
            });

            var OnlyForRoundedRecovered = 0.0;
            if(((Recovered_ / 1000000)%10).toFixed() == 0 ){
                OnlyForRoundedRecovered = 0.6;
            }
            $("#Recovered_SevenSegment").sevenSeg({
                colorBackground: 'white',
                colorOff: 'rgb(247, 247, 247)',
                colorOn: Recovered_Color,
                digits: (Math.round(Recovered_ / 1000000).toString()).length,
                // value: Number(Math.round((Recovered_ / 1000000) + OnlyForRoundedRecovered).toString().split("").reverse().join(""))
                value: (Math.round(Recovered_ / 1000000))
            });


            ////////////////////////////////////////////
            //Canvas Radial Chart //////////////////////

            if (Total_Leakage < Program_) {
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
            } else {
                var TotalLeakage = new RadialGauge({
                    renderTo: 'gauge-TotalLeakage',
                    width: 150,
                    height: 150,
                    units: '$',
                    minValue: 0,
                    maxValue: Total_Leakage,
                    majorTicks: [
                        '0',
                    ],
                    minorTicks: 2,
                    ticksAngle: 270,
                    startAngle: 45,
                    strokeTicks: true,
                    highlights: [
                          { from: 0, to: (Total_Leakage * 0.33), color: 'rgba(151, 40, 0, 0.7)' },
                         { from: (Total_Leakage * 0.33), to: (Total_Leakage * 0.66), color: 'rgba(185, 144, 0, 0.7)' },
                         { from: (Total_Leakage * 0.66), to: Total_Leakage, color: 'rgba(0, 207, 61, 0.7)' }
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

            }
          
            if (Recoverable_Leakage < Program_) {
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
            } else {
                var Recoverable = new RadialGauge({
                    renderTo: 'gauge-Recoverable',
                    width: 150,
                    height: 150,
                    units: '$',
                    minValue: 0,
                    maxValue: Recoverable_Leakage,
                    majorTicks: [
                        '0',

                    ],
                    minorTicks: 2,
                    ticksAngle: 270,
                    startAngle: 45,
                    strokeTicks: true,
                    highlights: [
                        { from: 0, to: (Recoverable_Leakage * 0.33), color: 'rgba(151, 40, 0, 0.7)' },
                         { from: (Recoverable_Leakage * 0.33), to: (Recoverable_Leakage * 0.66), color: 'rgba(185, 144, 0, 0.7)' },
                         { from: (Recoverable_Leakage * 0.66), to: Recoverable_Leakage, color: 'rgba(0, 207, 61, 0.7)' }
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

            }
            

            if (Recovered_ < Program_) {
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
            } else {
                var Recovered = new RadialGauge({
                    renderTo: 'gauge-Recovered',
                    width: 150,
                    height: 150,
                    units: '$',
                    minValue: 0,
                    maxValue: Recovered_,
                    majorTicks: [
                        '0',

                    ],
                    minorTicks: 2,
                    ticksAngle: 270,
                    startAngle: 45,
                    strokeTicks: true,
                    highlights: [
                         { from: 0, to: (Recovered_ * 0.33), color: 'rgba(151, 40, 0, 0.7)' },
                         { from: (Recovered_ * 0.33), to: (Recovered_ * 0.66), color: 'rgba(185, 144, 0, 0.7)' },
                         { from: (Recovered_ * 0.66), to: Recovered_, color: 'rgba(0, 207, 61, 0.7)' }
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
            }
        

            //console.log(ProvinceData);			
        });

        //FIXME
        // $.ajaxSetup({ async: true });
        //var url = window.location.href + "/Services/Case/ActivityRequest/List";
        //console.log("URL:" + url);

        var ActivityRequestCount = 0;
        var ActivityRequest;
        $.ajaxSetup({ async: false });
        var ActivityRequestResult = CaseManagement.Case.ActivityRequestService.List({"Take":5,"Sort":["CreatedDate DESC"]
        },response => {

            //FIXME
            //$.ajaxSetup({ async: false });
            //$.getJSON(url, function (data) {

            ActivityRequest = new Array(5)
            for (i = 0; i < 5; i++) {
                ActivityRequest[i] = new Array(3);
            }
        ActivityRequestCount = 5;

        // alert(data.TotalCount);
        //count = data.TotalCount;
        // console.log(response.TotalCount);
        for (var i = 0; i < 5; i++) {

            ActivityRequest[i][0] = response.Entities[i].Id; // ActivityRequest ID index -> 0
            ActivityRequest[i][1] = response.Entities[i].ActivityId; // Activity ID index -> 1
            // ActivityRequest[i][2] = data.Entities[i].CreatedDate; // CreateDate index -> 2
            // ActivityRequest[i][3] = data.Entities[i].ModifiedDate; // ModifiedDate index -> 3 

            /* var li = document.createElement("li");
             li.appendChild(document.createTextNode(data.Entities[i].Id));
             ul.appendChild(li);*/
        }
    });
    // $.ajaxSetup({ async: false });
    var ActivityResult =  CaseManagement.Case.ActivityService.List({}, response => {
        //var url = window.location.href + "/Services/Case/Activity/List";
        // $.getJSON(url, function (data) {

        for (var ActivityCount = 0; ActivityCount < response.TotalCount; ActivityCount++) {
            for (var i = 0; i < ActivityRequestCount; i++) {

                if (ActivityRequest[i][1] == response.Entities[ActivityCount].Id) {

                    ActivityRequest[i][2] = response.Entities[ActivityCount].CodeName; // Activity Code index -> 4
                    //ActivityRequest[i][3] = data.Entities[ActivityCount].Name; // Activity Name index -> 5

                }
            }
             
        }

    $("#first_activity_id").text("شناسه : " + ActivityRequest[0][0]);
    $("#first_activity_Code").text(ActivityRequest[0][2]);
    //$("#first_activity_Name").text(ActivityRequest[0][3]);

    $("#second_activity_id").text("شناسه : " + ActivityRequest[1][0]);
    $("#second_activity_Code").text(ActivityRequest[1][2]);
    //$("#second_activity_Name").text(ActivityRequest[1][3]);

    $("#third_activity_id").text("شناسه : " + ActivityRequest[2][0]);
    $("#third_activity_Code").text(ActivityRequest[2][2]);
    //$("#third_activity_Name").text(ActivityRequest[2][3]);

    $("#fourth_activity_id").text("شناسه : " + ActivityRequest[3][0]);
    $("#fourth_activity_Code").text(ActivityRequest[3][2]);
    // $("#fourth_activity_Name").text(ActivityRequest[3][3]);

    $("#fifth_activity_id").text("شناسه : " + ActivityRequest[4][0]);
    $("#fifth_activity_Code").text(ActivityRequest[4][2]);
    // $("#fifth_activity_Name").text(ActivityRequest[4][3]);


    //count1 = data.TotalCount;
});
//console.log(ActivityRequestCount - 1);
        
//}


};


////////////////////////////////////////////////////
// The Map:   
//ko.applyBindings(new ProvinceProgram_ViewModel() );
ProvinceProgram_ViewModel();

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

});
