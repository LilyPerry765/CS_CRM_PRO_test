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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLIntranetDailySaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLIntranetDailySaleReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLIntranetDailySaleReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
                //List<ADSLIntranetCityDailyInfo> Result = LoadData();
                //List<ADSLIntranetCityDailyInfo> ResultDischarge = LoadDataDischarge();
               List<ADSLIntarnetDailySaleInfo> Result=LoadData();

                string title = string.Empty;
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                if (fromDayeriDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDayeriDate"].Value = Helper.GetPersianDate(fromDayeriDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (toDayeriDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDayeriDate"].Value = Helper.GetPersianDate(toDayeriDate.SelectedDate, Helper.DateStringType.Short).ToString();

             if (fromDischargeDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDischargeDate"].Value = Helper.GetPersianDate(fromDischargeDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (toDischargeDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDischargeDate"].Value = Helper.GetPersianDate(toDischargeDate.SelectedDate, Helper.DateStringType.Short).ToString();

                foreach (ADSLIntarnetDailySaleInfo Info in Result)
                {
                    Info.SumOfDayeriWeek = Info.Saturday + Info.Sunday + Info.Monday + Info.Tuesday + Info.Wednesday + Info.Thursday + Info.Friday;
                    Info.SumOfDischargeWeek = Info.DischargeSaturday + Info.DischargeSunday + Info.DischargeMonday + Info.DischargeTuesday + Info.DischargeWednesday + Info.DischargeThursday + Info.DischargeFriday;
                }


                title = " آمار فروش روزانه اینترانت شرکت مخابرات ";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            
        }



        public List<ADSLIntarnetDailySaleInfo> LoadDataDayeri()
        {
           

            List<ADSLIntarnetDailySaleInfo> result = ReportDB.GetADSLIntarnetDayeriCityCenterDailyInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                                       CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                                       (!fromDayeriDate.SelectedDate.HasValue) ? null : fromDayeriDate.SelectedDate.Value.ToShortDateString(),
                                                                                                       (!toDayeriDate.SelectedDate.HasValue) ? null : toDayeriDate.SelectedDate.Value.AddDays(1).ToShortDateString());

            return result;
        }



        public List<ADSLIntarnetDailySaleInfo> LoadDataDischarge()
        {
            List<ADSLIntarnetDailySaleInfo> resultDisharge = ReportDB.GetADSLIntarnetDischargeCityCenterDailyInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                                                        CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                                                        (!fromDischargeDate.SelectedDate.HasValue)?null: fromDischargeDate.SelectedDate.Value.ToShortDateString(),
                                                                                                                        (!toDischargeDate.SelectedDate.HasValue)?null: toDischargeDate.SelectedDate.Value.AddDays(1).ToShortDateString());

           

            return resultDisharge;
        }

        private List<ADSLIntarnetDailySaleInfo> LoadData()
        {

           List<ADSLIntarnetDailySaleInfo> resultDisharge=LoadDataDischarge();
           List<ADSLIntarnetDailySaleInfo> resultDayeri = LoadDataDayeri();
           foreach (ADSLIntarnetDailySaleInfo Info in resultDisharge)
           {
               for(int i=0; i<resultDayeri.Count;i++)
               {
                   if(Info.CenterName==resultDayeri[i].CenterName)
                   {
                       resultDayeri[i].DischargeSaturday = Info.Saturday;
                       resultDayeri[i].DischargeSunday = Info.Sunday;
                       resultDayeri[i].DischargeMonday = Info.Monday;
                       resultDayeri[i].DischargeTuesday = Info.Tuesday;
                       resultDayeri[i].DischargeWednesday = Info.Wednesday;
                       resultDayeri[i].DischargeThursday = Info.Thursday;
                       resultDayeri[i].DischargeFriday = Info.Friday;
                   }
               }

               for (int i = 0; i < resultDisharge.Count(); i++)
                    {
                        for (int m = 0; m < resultDayeri.Count(); m++)
                        {

                            if (resultDayeri[m].CityName == resultDisharge[i].CityName && resultDayeri[m].CenterName == resultDayeri[i].CenterName)
                        {
                               resultDayeri[i].DischargeSaturday = Info.DischargeSaturday;
                               resultDayeri[i].DischargeSunday = Info.DischargeSunday;
                               resultDayeri[i].DischargeMonday = Info.DischargeMonday;
                               resultDayeri[i].DischargeTuesday = Info.DischargeTuesday;
                               resultDayeri[i].DischargeWednesday = Info.DischargeWednesday;
                               resultDayeri[i].DischargeThursday = Info.DischargeThursday;
                               resultDayeri[i].DischargeFriday = Info.DischargeFriday;
                               resultDayeri[m].SumOfDischargeWeek = resultDayeri[i].SumOfDischargeWeek;
                        }
                        else if(m==resultDayeri.Count()-1)
                        {
                            resultDayeri.Add(resultDisharge[i]);

                            break;
                        }
                    }}
           }

           return resultDayeri;
        }

        #endregion Methods
    }
}
