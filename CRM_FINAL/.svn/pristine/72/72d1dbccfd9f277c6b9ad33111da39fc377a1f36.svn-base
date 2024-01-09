using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CRM.Application.Reports.Viewer;
using System.Text.RegularExpressions;

namespace CRM.Application.Reports.ReportUserControls
{
    public partial class Failure117RequestsReportUserControl : Local.ReportBase
    {
        public Failure117RequestsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                FailureStatusCheckableCombobox.ItemsSource = Failure117DB.GetChildFailureStatusCheckable();
                LineStatusCheckableCombobox.ItemsSource = Failure117DB.GetChildLineStatusCheckable();
                FailureCombobox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117RequestStatus));
                FailureCombobox.SelectedValue = (int)DB.Failure117RequestStatus.All;
                ContractorOfficerCombobox.ItemsSource = Failure117NetworkContractorDB.GetContractorOfficerName();
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public override void Search()
        {
            List<FailureFormRowInfo> result = LoadData();

            if (result.Count > 0)
            {
                string title = string.Empty;
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                string CenterIds = string.Empty;
                string RegionIds = string.Empty;
                List<Center> centerList = CenterDB.GetAllCenter();
                List<int> CenterIDs = RequestPropertisUC.CenterCheckableComboBox.SelectedIDs;
                List<int> CityIDs = RequestPropertisUC.CityComboBox.SelectedIDs;

                if (CenterIDs.Count != 0)
                {
                    foreach (int _centerIds in CenterIDs)
                    {
                        CenterIds += centerList.Find(item => item.ID == _centerIds).CenterName + " ,";
                    }
                    CenterIds = CenterIds.Substring(0, CenterIds.Length - 1);
                }

                List<City> Citylist = CityDB.GetAllCity();
                if (CityIDs.Count != 0)
                {
                    foreach (int _cityrIds in CityIDs)
                    {
                        RegionIds += Citylist.Find(item => item.ID == _cityrIds).Name + " ,";
                    }
                    RegionIds = RegionIds.Substring(0, RegionIds.Length - 1);
                }

                stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
                stiReport.Dictionary.Variables["Region"].Value = RegionIds;
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(RequestPropertisUC.FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(RequestPropertisUC.ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["TelephoneNO"].Value = RequestPropertisUC.TelephoneNo.Text.Trim();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                stiReport.RegData("Result", "Result", result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        private List<FailureFormRowInfo> LoadData()
        {
            List<FailureFormRowInfo> result = new List<FailureFormRowInfo>();
            int FailureStatus = int.Parse(FailureCombobox.SelectedValue.ToString());
            int? PerformanceFrom = string.IsNullOrEmpty(PerformanceWiringNetworkFromTextBox.Text.Trim()) ? (int?)null : int.Parse(PerformanceWiringNetworkFromTextBox.Text.Trim());
            int? PerformanceTo = string.IsNullOrEmpty(PerformanceWiringNetworkToTextBox.Text.Trim()) ? (int?)null : int.Parse(PerformanceWiringNetworkToTextBox.Text.Trim());
            if (PerformanceFrom.HasValue && PerformanceTo.HasValue)
            {
                if (PerformanceFrom > PerformanceTo)
                {
                    MessageBox.Show("مقدار عملکرد شبکه هوایی معتبر نمی باشد");
                    return result;
                }
            }

            DateTime? toDate = null;
            if (RequestPropertisUC.ToDate.SelectedDate.HasValue)
            {
                string shamsiDate = Helper.GetPersianDate(RequestPropertisUC.ToDate.SelectedDate.Value, Helper.DateStringType.Short);
                string[] shamsiList = shamsiDate.Split('/');
                if (Convert.ToInt32(shamsiList[1]) <= 6)
                    toDate = RequestPropertisUC.ToDate.SelectedDate.Value.AddHours(17);//.AddDays(1);
                else
                    toDate = RequestPropertisUC.ToDate.SelectedDate.Value.AddHours(16);//.AddDays(1);
            }
            result = ReportDB.GetFailureRequest(RequestPropertisUC.FromDate.SelectedDate
                                              , toDate
                                              , string.IsNullOrEmpty(RequestPropertisUC.RequestIdFrom.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdFrom.Text.Trim())
                                              , string.IsNullOrEmpty(RequestPropertisUC.RequestIdTo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdTo.Text.Trim())
                                              , RequestPropertisUC.CenterCheckableComboBox.SelectedIDs
                                              , string.IsNullOrEmpty(RequestPropertisUC.TelephoneNo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.TelephoneNo.Text.Trim())
                                              , string.IsNullOrEmpty(RequestPropertisUC.IdentityId.Text.Trim()) ? null : RequestPropertisUC.IdentityId.Text.Trim()
                                              , LineStatusCheckableCombobox.SelectedIDs
                                              , FailureStatusCheckableCombobox.SelectedIDs
                                              , FailureStatus
                                              , (ContractorOfficerCombobox.SelectedValue != null) ? (int)ContractorOfficerCombobox.SelectedValue : (int?)null
                                              , string.IsNullOrEmpty(RowNoFromTextBox.Text.Trim()) ? (int?)null : int.Parse(RowNoFromTextBox.Text.Trim())
                                              , string.IsNullOrEmpty(RowNoToTextBox.Text.Trim()) ? (int?)null : int.Parse(RowNoToTextBox.Text.Trim()));
            //foreach (FailureFormRowInfo item in result)
            //{
            //    item.Step = RequestDB.GetRequestInfoByID(item.RequestID).CurrentStep;
            //    if (item.EndMDFDate != null)
            //    {
            //        //item.MinDiff = (DateTime.Parse(item.EndMDFDate) - DateTime.Parse(item.InsertDate)).TotalMinutes;
            //        //item.EliminateFailureDate = string.Format("{1} : {0}", Math.Round(item.MinDiff % 60, 2).ToString(), Math.Round(item.MinDiff / 60).ToString());
            //        item.InsertDate = Date.GetPersianDate(DateTime.Parse(item.InsertDate), Date.DateStringType.DateTime);
            //        item.EndMDFDate = Date.GetPersianDate(DateTime.Parse(item.EndMDFDate), Date.DateStringType.DateTime);
            //    }
            //    else
            //    {
            //        item.InsertDate = Date.GetPersianDate(DateTime.Parse(item.InsertDate), Date.DateStringType.DateTime);
            //        //item.EliminateFailureDate = "-";
            //    }
            //    if (item.GiveNetworkDate != null)
            //        item.GiveNetworkDateString = Helper.GetPersianDate((DateTime)item.GiveNetworkDate, Helper.DateStringType.DateTime);
            //    if (item.GetNetworkDate != null)
            //        item.GetNetworkDateString = Helper.GetPersianDate((DateTime)item.GetNetworkDate, Helper.DateStringType.DateTime);
            //    if (item.GiveNetworkDate != null && item.GetNetworkDate != null)
            //    {
            //        item.MinDiffNetwork = ((DateTime)item.GetNetworkDate - (DateTime)item.GiveNetworkDate).TotalMinutes;
            //        if (item.MinDiffNetwork % 60 <= 30)
            //            item.EliminateNetworkFailureDate = string.Format("{1} : {0}", Math.Round(item.MinDiffNetwork % 60, 2).ToString(), Math.Round(item.MinDiffNetwork / 60).ToString());
            //        else
            //            item.EliminateNetworkFailureDate = string.Format("{1} : {0}", Math.Round(item.MinDiffNetwork % 60, 2).ToString(), (Math.Round(item.MinDiffNetwork / 60) - 1).ToString());
            //    }
            //    else
            //        item.EliminateNetworkFailureDate = "";
            //}
            result = result.Where(t => (!PerformanceFrom.HasValue || PerformanceFrom <= t.MinDiff)
                                && (!PerformanceTo.HasValue || PerformanceTo >= t.MinDiff)).ToList();

            return result;
        }
    }
}
