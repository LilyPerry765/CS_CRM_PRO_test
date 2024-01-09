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
using System.Globalization;
using System.IO;
using CRM.Data.Schema;
using System.Text;
using System.Collections;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLCityWeeklyContactsReportUserControl.xaml
    /// </summary>
    public partial class ADSLCityWeeklyContactsReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion

        #region Constructor
        public ADSLCityWeeklyContactsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Initializer
        public void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }
        #endregion

        #region Methods

        public override void Search()
        {
            //var calendar = new PersianCalendar();
            //var week = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay,
                                     //DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

            //int WeekNo = 0; 
            List<ADSLCityWeeklyContacts> Dayeri = ReportDB.GetADSLCityWeeklyContactsDayeri
                                                    (CityComboBox.SelectedIDs);
            List<ADSLCityWeeklyContacts> Discharge = ReportDB.GetADSLCityWeeklyContactsDischarge(CityComboBox.SelectedIDs);

            List<ADSLCityWeeklyContacts> DayeriChart = ReportDB.GetADSLCityWeeklyContactsDayeriforChart(CityComboBox.SelectedIDs);
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
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplaiantaionTextBox.Text;

            if(Dayeri.Count!=0)
            {
                Dayeri[0].DayeriTitle = "دایری هفتگی";
                Dayeri[0].PureDayeriTitle = "دایری خالص هفتگی";
            }
            if (Discharge.Count != 0)
            {
                Discharge[0].DischargeTitle = "تخلیه هفتگی";
            }
             
            //DateTime StartDate = new DateTime(2012,10, 29);
            //DateTime EndDate = StartDate.AddDays(6);

            //for (int i = 0; i < week; i++)
            //{
            //    WeekNo++;

            //    if (i != 0)
            //    {
            //        StartDate = EndDate.AddDays(1);
            //        EndDate = StartDate.AddDays(6);
            //    }

            //    List<ADSLGeneralContactsInfo> GetContactsADSL = ReportDB.GetContactsADSL(CityComboBox.SelectedIDs, StartDate, EndDate);
            //    List<ADSLGeneralContactsInfo> GetContactsDischarge = ReportDB.GetContactsDischarge(CityComboBox.SelectedIDs, StartDate, EndDate);

            //  for(int j=0;j<GetContactsADSL.Count;j++)
            //      foreach(ADSLGeneralContactsInfo info in Result)
            //          if (info.CityName == GetContactsADSL[j].CityName & info.EndDate >= StartDate & info.EndDate <= EndDate)
            //          {
            //              info.WeekTotalDayeri = GetContactsADSL[j].WeekTotalDayeri;
            //              info.WeekNo = WeekNo;
            //          }

            //  for (int k = 0; k < GetContactsDischarge.Count; k++)
            //      foreach (ADSLGeneralContactsInfo info in Result)
            //          if (info.CityName == GetContactsDischarge[k].CityName & info.EndDate >= StartDate & info.EndDate <= EndDate)
            //          {
            //              info.WeekDischarge = GetContactsDischarge[k].WeekDischarge;
            //              info.WeekNo = WeekNo;
            //          }
            //}
            //foreach (ADSLGeneralContactsInfo info in Result)
            //{
            //    info.WeekPureDayeri = info.WeekTotalDayeri - info.WeekDischarge;
            //}
            ////Result.GroupBy(t => new { WeekNo = t.WeekNo , city=t.CityName}).ToList();

            title = "گزارش هفتگی کلي ADSL مخابرات شهرستان به شهرستان";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Dayeri", "Dayeri", Dayeri);
            stiReport.RegData("Discharge", "Discharge", Discharge);
            stiReport.RegData("DayeriChart", "DayeriChart", DayeriChart);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLCityWeeklyContacts> LoadData()
        {
            List<ADSLCityWeeklyContacts> result = ReportDB.GetADSLCityWeeklyContactsDayeri
                                                    (CityComboBox.SelectedIDs);
            return result;
        }
        #endregion Methods
    }
}
