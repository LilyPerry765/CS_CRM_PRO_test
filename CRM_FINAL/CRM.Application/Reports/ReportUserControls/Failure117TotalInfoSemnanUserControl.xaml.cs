﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;
using CRM.Application.Reports.Viewer;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    public partial class Failure117TotalInfoSemnanUserControl : Local.ReportBase
    {
        public Failure117TotalInfoSemnanUserControl()
        {
            InitializeComponent();
        }

        public override void Search()
        {
            try
            {
                string title = string.Empty;
                string path;

                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);

                if (CityCenterComboBox.CityComboBox.SelectedIDs.Count != 0)
                {
                    List<string> cityNameList = CityDB.GetCityNameByIDs(CityCenterComboBox.CityComboBox.SelectedIDs);

                    stiReport.Dictionary.Variables["City"].Value = "";

                    foreach (string cityName in cityNameList)
                    {
                        stiReport.Dictionary.Variables["City"].Value = stiReport.Dictionary.Variables["City"].Value + cityName + " ، ";
                    }
                }
                else
                {
                    MessageBox.Show("لطفا شهر مورد نظر را انتخاب نمایید!");
                    return;
                }

                if (CityCenterComboBox.CenterComboBox.SelectedIDs.Count != 0)
                {
                    List<string> centerNameList = CenterDB.GetCenterNameByIDs(CityCenterComboBox.CenterComboBox.SelectedIDs);

                    stiReport.Dictionary.Variables["Center"].Value = "";

                    foreach (string centerName in centerNameList)
                    {
                        stiReport.Dictionary.Variables["Center"].Value = stiReport.Dictionary.Variables["Center"].Value + centerName + " ، ";
                    }
                }
                else
                {
                    MessageBox.Show("لطفا مرکز مورد نظر را انتخاب نمایید!");
                    return;
                }

                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                if (FromDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (Todate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(Todate.SelectedDate, Helper.DateStringType.Short).ToString();

                Failure117TotalReportSemnan result = new Failure117TotalReportSemnan();
                DateTime? toDate = null;
                if (Todate.SelectedDate.HasValue)
                    toDate = Todate.SelectedDate.Value.AddHours(16);

                result = GetFailure117Total(CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                     FromDate.SelectedDate,
                                                                     toDate);
                stiReport.RegData("Result", "Result", result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private Failure117TotalReportSemnan GetFailure117Total(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            Failure117TotalReportSemnan result = new Failure117TotalReportSemnan();

            result.FailureTotalThisMonth = Failure117DB.GetCountTotalThisMonth(centerIDs, fromDate, toDate);

            result.AnotherCenter = 0;
            result.Repetitive = 0;// Failure117DB.GetCountRepetitive(centerIDs, fromDate, toDate); 
            result.cancelation = Failure117DB.GetCountCancelationThisMonth(centerIDs, fromDate, toDate);
            result.WrongBusy = 0;// Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 13); ////
            result.CustomerSection = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 13);
            result.RightAfterTest = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 1007);
            result.Bargardan_Nosazi = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 1009);
            result.Changes = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 1031);
            result.SendToSalon = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 1033);

            result.RemaindPastMonth = Failure117DB.GetCountRemaindPastMonthSemnan(centerIDs, fromDate, toDate);
            result.FailuroFormCount = Failure117DB.GetCountFilaureForm(centerIDs, fromDate, toDate);
            result.FailuroFormCountandCancelation = Failure117DB.GetCountFilaureFormandCancelation(centerIDs, fromDate, toDate);

            result.Network = Failure117DB.GetCountFilaureParentStatus(centerIDs, fromDate, toDate, 1);
            result.Cable = Failure117DB.GetCountFilaureParentStatus(centerIDs, fromDate, toDate, 2);
            result.PCM = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 29);
            result.KafoNoori = Failure117DB.GetCountFilaureParentStatus(centerIDs, fromDate, toDate, 50);
            result.MDF = Failure117DB.GetCountFilaureParentStatus(centerIDs, fromDate, toDate, 3);
            result.Salon = Failure117DB.GetCountSalonDastgah(centerIDs, fromDate, toDate);
            result.ClinetNormal = Failure117DB.GetCountFilaureParentStatus(centerIDs, fromDate, toDate, 6);
            result.ClientFX = 0;
            result.ClientSpecialWire = 0;
            result.ClinetPublic = 0;

            result.RemaindThisMonthMDF = Failure117DB.GetCountRemaindThisMonthMDF(centerIDs, fromDate, toDate);
            result.RemaindThisMonthNetwork = Failure117DB.GetCountRemaindThisMonthNetwork(centerIDs, fromDate, toDate);


            List<Failure117> failureTimeList = Failure117DB.GetFailurebyTimeSemnanforTime(centerIDs, fromDate, toDate);
 
            DateTime insertDate = new DateTime();

            List<FailureTimeReportInfo> timeInfoList = new List<FailureTimeReportInfo>();
            FailureTimeReportInfo timeInfo = new FailureTimeReportInfo();

            foreach (Failure117 item in failureTimeList)
            {
                timeInfo = new FailureTimeReportInfo();

                insertDate = RequestDB.GetRequestInsertDateByID(item.ID);

                if (insertDate.Hour < 8)
                {
                    timeInfo.ID = item.ID;
                    timeInfo.InsertDate = new DateTime(insertDate.Year, insertDate.Month, insertDate.Day, 8, 0, 0);

                    if (item.EndMDFDate.Value.Hour > 16)
                        timeInfo.EndMDFDate = new DateTime(item.EndMDFDate.Value.Year, item.EndMDFDate.Value.Month, item.EndMDFDate.Value.Day, 16, 0, 0);
                    if (item.EndMDFDate.Value.Hour < 8)
                        timeInfo.EndMDFDate = new DateTime(item.EndMDFDate.Value.Year, item.EndMDFDate.Value.Month, item.EndMDFDate.Value.Day, 8, 0, 0);
                    else
                        timeInfo.EndMDFDate = (DateTime)item.EndMDFDate;

                    timeInfoList.Add(timeInfo);
                }
                else
                {
                    if (insertDate.Hour > 16)
                    {
                        timeInfo.ID = item.ID;
                        timeInfo.InsertDate = new DateTime(insertDate.AddDays(1).Year, insertDate.AddDays(1).Month, insertDate.AddDays(1).Day, 8, 0, 0);

                        if (item.EndMDFDate.Value.Hour > 16)
                            timeInfo.EndMDFDate = new DateTime(item.EndMDFDate.Value.Year, item.EndMDFDate.Value.Month, item.EndMDFDate.Value.Day, 16, 0, 0);
                        if (item.EndMDFDate.Value.Hour < 8)
                            timeInfo.EndMDFDate = new DateTime(item.EndMDFDate.Value.Year, item.EndMDFDate.Value.Month, item.EndMDFDate.Value.Day, 8, 0, 0);
                        else
                            timeInfo.EndMDFDate = (DateTime)item.EndMDFDate;

                        timeInfoList.Add(timeInfo);
                    }
                    else
                    {
                        timeInfo.ID = item.ID;
                        timeInfo.InsertDate = insertDate;
                        timeInfo.EndMDFDate = (DateTime)item.EndMDFDate;

                        timeInfoList.Add(timeInfo);
                    }
                }
            }

            result.Hour_1 = Failure117DB.GetCountFailurebyTimeSemnan(0, 1, timeInfoList);
            result.Hour_2 = Failure117DB.GetCountFailurebyTimeSemnan(1, 2, timeInfoList);
            result.Hour_3 = Failure117DB.GetCountFailurebyTimeSemnan(2, 3, timeInfoList);
            result.Hour_4 = Failure117DB.GetCountFailurebyTimeSemnan(3, 4, timeInfoList);
            result.Hour_5 = Failure117DB.GetCountFailurebyTimeSemnan(4, 5, timeInfoList);
            result.Hour_6 = Failure117DB.GetCountFailurebyTimeSemnan(3, 6, timeInfoList);
            result.Hour_12 = Failure117DB.GetCountFailurebyTimeSemnan(6, 12,  timeInfoList);
            result.Hour_24 = Failure117DB.GetCountFailurebyTimeSemnan(12, 24,  timeInfoList);
            result.Hour_36 = Failure117DB.GetCountFailurebyTimeSemnan(24, 36,  timeInfoList);
            result.Hour_48 = Failure117DB.GetCountFailurebyTimeSemnan(36, 48,  timeInfoList);
            result.Hour_72 = Failure117DB.GetCountFailurebyTimeSemnan(48, 72,  timeInfoList);
            result.Hour_N = Failure117DB.GetCountFailurebyTimeSemnan(72, 100000,  timeInfoList);

            result.NetworkPercent = Math.Round(Convert.ToDouble(result.Network) / Convert.ToDouble(result.FailuroFormCount), 2);
            result.CablePercent = Math.Round(Convert.ToDouble(result.Cable) / Convert.ToDouble(result.FailuroFormCount), 2);
            result.ClinetNormalPercent = Math.Round(Convert.ToDouble(result.ClinetNormal) / Convert.ToDouble(result.FailuroFormCount), 2);

            return result;
        }
    }
}