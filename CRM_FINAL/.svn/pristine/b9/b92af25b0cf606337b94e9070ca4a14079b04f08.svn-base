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
    public partial class PAPRequestMontlyReport : Local.ReportBase
    {
        public PAPRequestMontlyReport()
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

                if (PAPComboBox.SelectedValue != null)
                {
                    stiReport.Dictionary.Variables["PAPName"].Value = PAPComboBox.Text;
                }
                else
                {
                    MessageBox.Show("لطفا شرکت PAP مورد نظر را انتخاب نمایید!");
                    return;
                }

                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                if (FromDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (Todate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(Todate.SelectedDate, Helper.DateStringType.Short).ToString();

                PAPTotalReportInfo result = new PAPTotalReportInfo();
                DateTime? toDate = null;
                if (Todate.SelectedDate.HasValue)
                    toDate = Todate.SelectedDate.Value.AddDays(1);

                List<int> centerIDs = CityCenterComboBox.CenterComboBox.SelectedIDs;
                int papID = (int)PAPComboBox.SelectedValue;

                stiReport.Dictionary.Variables["InstalCompleted"].Value = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate, (byte)DB.ADSLPAPRequestType.Install, (byte)DB.ADSLPAPRequestStatus.Completed);
                stiReport.Dictionary.Variables["InstalRejected"].Value = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate, (byte)DB.ADSLPAPRequestType.Install, (byte)DB.ADSLPAPRequestStatus.Reject);
                stiReport.Dictionary.Variables["DischargeCompleted"].Value = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate, (byte)DB.ADSLPAPRequestType.Discharge, (byte)DB.ADSLPAPRequestStatus.Completed);
                stiReport.Dictionary.Variables["DischargeRejected"].Value = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate, (byte)DB.ADSLPAPRequestType.Discharge, (byte)DB.ADSLPAPRequestStatus.Reject);
                stiReport.Dictionary.Variables["ExchangeCompleted"].Value = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate, (byte)DB.ADSLPAPRequestType.Exchange, (byte)DB.ADSLPAPRequestStatus.Completed);
                stiReport.Dictionary.Variables["ExchangeRejected"].Value = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate, (byte)DB.ADSLPAPRequestType.Exchange, (byte)DB.ADSLPAPRequestStatus.Reject);
                stiReport.Dictionary.Variables["Busy"].Value = ADSLPAPRequestDB.GetPAPBusyInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate);
                stiReport.Dictionary.Variables["Available"].Value = ADSLPAPRequestDB.GetPAPAvailableInfoReport(centerIDs, papID, FromDate.SelectedDate, toDate);

                stiReport.RegData("Result", "Result", result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private PAPTotalReportInfo GetPAPRequestReportInfo(List<int> centerIDs, int papID, DateTime? fromDate, DateTime? toDate)
        {
            PAPTotalReportInfo result = new PAPTotalReportInfo();
            try
            {
                result.InstalCompleted = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, fromDate, toDate, (byte)DB.ADSLPAPRequestType.Install, (byte)DB.ADSLPAPRequestStatus.Completed);
                result.InstalRejected = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, fromDate, toDate, (byte)DB.ADSLPAPRequestType.Install, (byte)DB.ADSLPAPRequestStatus.Reject);
                result.DischargeCompleted = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, fromDate, toDate, (byte)DB.ADSLPAPRequestType.Discharge, (byte)DB.ADSLPAPRequestStatus.Completed);
                result.DischargeRejected = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, fromDate, toDate, (byte)DB.ADSLPAPRequestType.Discharge, (byte)DB.ADSLPAPRequestStatus.Reject);
                result.ExchangeCompleted = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, fromDate, toDate, (byte)DB.ADSLPAPRequestType.Exchange, (byte)DB.ADSLPAPRequestStatus.Completed);
                result.ExchangeRejected = ADSLPAPRequestDB.GetPAPInstallMontlyInfoReport(centerIDs, papID, fromDate, toDate, (byte)DB.ADSLPAPRequestType.Exchange, (byte)DB.ADSLPAPRequestStatus.Reject);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
