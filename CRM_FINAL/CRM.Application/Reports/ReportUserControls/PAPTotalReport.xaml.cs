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
using Stimulsoft.Report;
using CRM.Data;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    public partial class PAPTotalReport : Local.ReportBase
    {
        public PAPTotalReport()
        {
            InitializeComponent();

            PAPComboBox.ItemsSource = Data.PAPInfoDB.GetPAPInfoCheckable();
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

                List<PAPTotalReportInfo> result = new List<PAPTotalReportInfo>();
                DateTime? toDate = null;
                if (Todate.SelectedDate.HasValue)
                    toDate = Todate.SelectedDate.Value.AddDays(1);

                List<int> centerIDs = CityCenterComboBox.CenterComboBox.SelectedIDs;
                List<int> papIDs = PAPComboBox.SelectedIDs;

                result = GetPAPRequestReportInfo(centerIDs, papIDs, FromDate.SelectedDate, toDate);

                stiReport.RegData("Result", "Result", result);


                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private List<PAPTotalReportInfo> GetPAPRequestReportInfo(List<int> centerIDs, List<int> papIDs, DateTime? fromDate, DateTime? toDate)
        {
            List<PAPTotalReportInfo> resultList = new List<PAPTotalReportInfo>();
            List<PAPTotalReportInfo> resultList1 = new List<PAPTotalReportInfo>();

            resultList = ADSLPAPRequestDB.GetADSLRequestTotalReport(centerIDs, papIDs, fromDate, toDate);

            foreach (PAPTotalReportInfo currentItem in resultList)
            {
                currentItem.Ports = ADSLPAPRequestDB.GetADSLPAPPortsReport(currentItem.CenterID, currentItem.PAPID, fromDate, toDate);
                currentItem.Busy = ADSLPAPRequestDB.GetADSLPAPPortBusyReport(currentItem.CenterID, currentItem.PAPID, fromDate, toDate);

                PAPTotalReportInfo thisYearItems = ADSLPAPRequestDB.GetADSLRequestTotalReportThisYear(currentItem.CenterID, currentItem.PAPID);
                currentItem.InstalCompletedThisYear = thisYearItems.InstalCompletedThisYear;
                currentItem.DischargeCompletedThisYear = thisYearItems.DischargeCompletedThisYear;
                currentItem.ExchangeCompletedThisYear = thisYearItems.ExchangeCompletedThisYear;

                resultList1.Add(currentItem);
            }

            return resultList1;
        }
    }
}

