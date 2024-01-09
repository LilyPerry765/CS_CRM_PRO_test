using System;
using System.Collections;
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
using System.Text.RegularExpressions;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for SendingFailure117ToNetworlAndCableReportUserControl.xaml
    /// </summary>
    public partial class SendingFailure117ToNetworlAndCableReportUserControl : Local.ReportBase
    {
        public SendingFailure117ToNetworlAndCableReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public override void Search()
        {
            IEnumerable result = LoadData();
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
        private IEnumerable LoadData()
        {
            IEnumerable result = ReportDB.GetSendingFailure117RequestsToNetworkCable(RequestPropertisUC.FromDate.SelectedDate
                                                                    , RequestPropertisUC.ToDate.SelectedDate
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.RequestIdFrom.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdFrom.Text.Trim())
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.RequestIdTo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdTo.Text.Trim())
                                                                    , RequestPropertisUC.CenterCheckableComboBox.SelectedIDs
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.TelephoneNo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.TelephoneNo.Text.Trim())
                                                                    , string.IsNullOrEmpty(RadifIdFrom.Text.Trim()) ? (int?)null : int.Parse(RadifIdFrom.Text.Trim())
                                                                    , string.IsNullOrEmpty(RadifIdTo.Text.Trim()) ? (int?)null : int.Parse(RadifIdTo.Text.Trim())
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.IdentityId.Text.Trim()) ? null : RequestPropertisUC.IdentityId.Text.Trim());
            foreach (Failure117Requests item in result)
            {
                double MinDiff = (item.SendToCableDate != null && item.CableDate != null) ? (DateTime.Parse(item.CableDate) - DateTime.Parse(item.SendToCableDate)).TotalMinutes : 0;
                item.FailureDuration = string.Format("{1} : {0}", Math.Round(MinDiff % 60, 2).ToString(), Math.Round(MinDiff / 60).ToString());
                item.CableDate = (item.CableDate != null) ? Date.GetPersianDate(DateTime.Parse(item.CableDate), Date.DateStringType.Short) : "";
                item.SendToCableDate = (item.SendToCableDate != null) ? Date.GetPersianDate(DateTime.Parse(item.SendToCableDate), Date.DateStringType.Short) : "";
            }                                                        
            return result;
        }
    }
}
