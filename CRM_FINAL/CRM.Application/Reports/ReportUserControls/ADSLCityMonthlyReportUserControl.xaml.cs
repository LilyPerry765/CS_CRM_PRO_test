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
    public partial class ADSLCityMonthlyReportUserControl : Local.ReportBase
    {
        public ADSLCityMonthlyReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #region Initializer
        public void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }
        #endregion

        #region Method
        public override void Search()
        {
            List<ADSLCityMonthlyInfo> Dayeri = ReportDB.GetADSLCityDayeriMonthlyInfo(CityComboBox.SelectedIDs);
            List<ADSLCityMonthlyInfo> Discharge = ReportDB.GetADSLCityDischargeMonthlyInfo(CityComboBox.SelectedIDs);
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
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            if (Dayeri.Count != 0)
            {
                Dayeri[0].DayeriTitle = "دایری ماهانه";
                Dayeri[0].PureDayeriTitle = "دایری خالص ماهانه";
            }
            if (Discharge.Count != 0)
            {
                Discharge[0].DischargeTitle = "تخلیه ماهانه";
            }

            title = " گزارش ماهانه ADSL شهرستان به شهرستان";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Dayeri", "Dayeri", Dayeri);
            stiReport.RegData("Discharge", "Discharge", Discharge);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion Methods
    }
}
