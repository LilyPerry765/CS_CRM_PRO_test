using System;
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
using System.Windows.Shapes;
using CRM.Data;
using CRM.Application.Reports.Viewer;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    public partial class Failure117TotalInfoUserControl : Local.ReportBase
    {
        public Failure117TotalInfoUserControl()
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

                Failure117TotalReport result = new Failure117TotalReport();

                DateTime? fromDate = null;
                if (FromDate.SelectedDate.HasValue)
                {
                    fromDate = FromDate.SelectedDate.Value.AddDays(-1);
                    string shamsiDate = Helper.GetPersianDate(fromDate, Helper.DateStringType.Short);
                    string[] shamsiList = shamsiDate.Split('/');
                    if (Convert.ToInt32(shamsiList[1]) <= 6)
                        fromDate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 17, 01, 0);
                    else
                        fromDate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 16, 01, 0);
                }

                DateTime? toDate = null;
                if (Todate.SelectedDate.HasValue)
                {
                    string shamsiDate = Helper.GetPersianDate(Todate.SelectedDate.Value, Helper.DateStringType.Short);
                    string[] shamsiList = shamsiDate.Split('/');
                    if (Convert.ToInt32(shamsiList[1]) <= 6)
                        toDate = Todate.SelectedDate.Value.AddHours(17);//.AddDays(1);
                    else
                        toDate = Todate.SelectedDate.Value.AddHours(16);//.AddDays(1);
                }

                result = GetFailure117Total(CityCenterComboBox.CenterComboBox.SelectedIDs, fromDate, toDate);

                stiReport.RegData("Result", "Result", result);
                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private Failure117TotalReport GetFailure117Total(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            Failure117TotalReport result = new Failure117TotalReport();

            result.Total = Failure117DB.GetCountTotalTelephone(centerIDs);
            result.FailureTotalThisMonth = Failure117DB.GetCountTotalThisMonth(centerIDs, fromDate, toDate) - Failure117DB.GetCountRemaindThisMonthMDF(centerIDs, fromDate, toDate);
            result.RemaindPastMonthNetwork = Failure117DB.GetCountRemaindPastMonth(centerIDs, fromDate, toDate);
            result.Repetitive = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 42);
            result.NightUB = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 36);
            result.MeditationCut = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 13);
            result.WellAfterTest = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 30);
            result.Bargardankhesarat = Failure117DB.GetCountLineStatus(centerIDs, fromDate, toDate, 32);
            result.MDFCompelete = Failure117DB.GetCountMDFCompelete(centerIDs, fromDate, toDate) -
                                  (result.Repetitive + result.WellAfterTest + result.NightUB + result.MeditationCut + result.Bargardankhesarat);
            result.SalonDastgah = Failure117DB.GetCountSalonDastgah(centerIDs, fromDate, toDate);

            result.AdamTatbigheKhat = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 42);
            result.CableandSimHavaii = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 18);
            result.GholabandPost = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 13);
            result.JabeTaghsim = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 11);
            result.PCM_4 = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 29);
            result.SarDakhele = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 15);
            result.RanjeKafo = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 14);
            result.SimkeshiGheireMojaz = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 16);
            result.BehineSazi = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 35);
            result.Unknown = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 61);
            result.AbooneCabel = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 21);
            result.MarkaziCabel = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 24);
            result.ErtebatCabel = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 23);
            result.EkhtesasiCabel = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 22);
            result.MafaselHavaii = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 20);
            result.PostandSarCabel = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 19);
            result.Khesarat = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 34);
            result.Bargardan = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 36);
            result.BuchExchange = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 47);
            result.PostExchange = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 48);
            result.EtesaliExchange = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 46);
            result.KafoNoori = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 51);
            result.WLL = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 60);
            result.MDFInside = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 62);
            result.SimkeshiDakheli = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 31);
            result.EslahMoshtarek = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 39);
            result.TelephoneDevice = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 30);
            result.SalempasazMoraje = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 40);
            result.Customernabood = Failure117DB.GetCountFilaureStatus(centerIDs, fromDate, toDate, 41);

            result.RemaindThisMonthMDF = Failure117DB.GetCountRemaindThisMonthMDF(centerIDs, fromDate, toDate);
            result.RemaindThisMonthNetwork = Failure117DB.GetCountRemaindThisMonthNetwork(centerIDs, fromDate, toDate);

            result.Hour_1 = Failure117DB.GetCountFailurebyTime(0, 1, centerIDs, fromDate, toDate);
            result.Hour_2 = Failure117DB.GetCountFailurebyTime(1, 2, centerIDs, fromDate, toDate);
            result.Hour_3 = Failure117DB.GetCountFailurebyTime(2, 3, centerIDs, fromDate, toDate);
            result.Hour_6 = Failure117DB.GetCountFailurebyTime(3, 6, centerIDs, fromDate, toDate);
            result.Hour_12 = Failure117DB.GetCountFailurebyTime(6, 12, centerIDs, fromDate, toDate);
            result.Hour_24 = Failure117DB.GetCountFailurebyTime(12, 24, centerIDs, fromDate, toDate);
            result.Hour_36 = Failure117DB.GetCountFailurebyTime(24, 36, centerIDs, fromDate, toDate);
            result.Hour_48 = Failure117DB.GetCountFailurebyTime(36, 48, centerIDs, fromDate, toDate);
            result.Hour_72 = Failure117DB.GetCountFailurebyTime(48, 72, centerIDs, fromDate, toDate);
            result.Hour_N = Failure117DB.GetCountFailurebyTime(72, 100000, centerIDs, fromDate, toDate);

            int sumdakheli = result.SimkeshiDakheli + result.EslahMoshtarek + result.TelephoneDevice + result.SalempasazMoraje + result.Customernabood;
            //result.FailureCompleteTotalThisMonth = (Convert.ToInt32(Failure117DB.GetCountCompleteThisMonth(centerIDs, fromDate, toDate)) - sumdakheli).ToString();

            result.FailureCompleteTotalThisMonth = result.MDFCompelete + result.SalonDastgah + result.AdamTatbigheKhat +
                                                   result.CableandSimHavaii + result.GholabandPost + result.JabeTaghsim +
                                                   result.PCM_4 + result.SarDakhele + result.RanjeKafo +
                                                   result.SimkeshiGheireMojaz + result.BehineSazi + result.Unknown +
                                                   result.AbooneCabel + result.MarkaziCabel + result.ErtebatCabel +
                                                   result.EkhtesasiCabel + result.MafaselHavaii + result.PostandSarCabel +
                                                   result.Khesarat + result.Bargardan + result.BuchExchange +
                                                   result.PostExchange + result.EtesaliExchange + result.KafoNoori + result.WLL;

            return result;
        }
    }
}
